using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.POP.Function;
using FarPoint.Win.Spread;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static CoFAS.NEW.MES.POP.Barcode_Class;

namespace CoFAS.NEW.MES.POP
{
    public partial class from_불량입력 : Form
    {
        #region ○ 폼 이동

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        private void tspMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                //this.WindowState = FormWindowState.Maximized;
                //this.Refresh();
                //this.Invalidate();
                //this.SetStyle(ControlStyles.ResizeRedraw, true);
            }
        }

        #endregion

        #region ○ 변수선언

        public string _p품번 = string.Empty;

        public string _p품명 = string.Empty;

        public string _pLOT = string.Empty;

        public string _p실적 = string.Empty;

        private UserEntity _UserEntity = new UserEntity();

        #endregion

        #region ○ 생성자

        public from_불량입력(UserEntity _pUserEntity)
        {
            _UserEntity = _pUserEntity;
            InitializeComponent();

            Load += new EventHandler(Form_Load);


        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                DataTable pDataTable1 =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpMain,"BAD_PERFORMANCE");



                if (pDataTable1 != null && pDataTable1.Rows.Count != 0)
                {
                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable1, fpMain, this.Name, _UserEntity.user_account);

                }

                this.MinimumSize = this.Size;
                this.MaximumSize = this.Size;
                MainFind_DisplayData();


                fpMain._ChangeEventHandler += FpMain_Change;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void FpMain_Change(object sender, ChangeEventArgs e)
        {
            try
            {
                xFpSpread xFp = sender as xFpSpread;

                for (int i = 0; i < xFp.Sheets[0].Columns.Count; i++)
                {
                    if (xFp.Sheets[0].Columns[i].Tag.ToString() == "COMPLETE_YN")
                    {
                        if (xFp.Sheets[0].GetValue(e.Row, i).ToString() != "N")
                        {
                            return;
                        }
                    }
                }
                string pHeaderLabel = xFp.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                switch (pHeaderLabel)
                {
                    case "":
                        xFp.Sheets[0].RowHeader.Cells[e.Row, 0].Text = "수정";
                        break;
                    case "입력":
                        break;
                    case "수정":
                        break;
                    case "삭제":
                        break;
                }
                if (xFp.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(FarPoint.Win.Spread.CellType.NumberCellType))
                {
                    //Function.Core._AddItemSUM(xFp);
                    xFp.ActiveSheet.SetActiveCell(e.Row, e.Column);
                    xFp.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
     
        private void MainFind_DisplayData()
        {
            string sql = $@"SELECT ISNULL([ID],0) AS ID
                                      ,ISNULL([WORK_PERFORMANCE_ID],'{_p실적}') AS WORK_PERFORMANCE_ID
                                      ,[BAD_NO]
                                      ,ISNULL([BAD_DATE],GETDATE()) AS BAD_DATE
                                      ,ISNULL([RESOURCE_NO],'{_p품번}') AS RESOURCE_NO
                                      ,[ERROR_NO] AS BAD_TYPE
                                 	  ,[DESCRIPTION]
                                      ,ISNULL([BAD_QTY],0) AS BAD_QTY
                                      ,[BAD_FLAG]
                                      ,[ORDER_NO]
                                      ,ISNULL([LOT_NO],'{_pLOT}') AS LOT_NO
                                      ,[REG_USER]
                                      ,[REG_DATE]
                                      ,[UP_USER]
                                      ,[UP_DATE]
                                 FROM [SEA_MFG].[DBO].[SCRAP_ERR_CODES] A 
                                 LEFT JOIN [dbo].[BAD_PERFORMANCE] B ON A.error_no = B.BAD_TYPE 
                                 AND B.WORK_PERFORMANCE_ID = '{_p실적}'
                                 AND B.LOT_NO              = '{_pLOT}'
                                 AND B.RESOURCE_NO         = '{_p품번}'
                                 WHERE 1=1
                                 AND SCRAP_ERR_COMMODITY IN ( 'ALLNN','BOSCHAA','BOSCHMM','BOSCHNN','BOSCHKK')
                                 AND ACTIVE ='A'";

            DataTable dt1 =  new CoreBusiness().SELECT(sql);

            CoFAS.NEW.MES.Core.Function.Core.DisplayData_Set(dt1, fpMain);


            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["ID"].ToString() == "0")
                {
                    fpMain.Sheets[0].RowHeader.Cells[i, 0].Text = "입력";
                    fpMain.Sheets[0].SetValue(i, "REG_USER ".Trim(), _UserEntity.user_account);
                    fpMain.Sheets[0].SetValue(i, "REG_DATE ".Trim(), DateTime.Now);
                    fpMain.Sheets[0].SetValue(i, "UP_USER  ".Trim(), _UserEntity.user_account);
                    fpMain.Sheets[0].SetValue(i, "UP_DATE  ".Trim(), DateTime.Now);
                }
            }
        }



        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_닫기_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_저장_Click(object sender, EventArgs e)
        {
            try
            {
              

                for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "BAD_QTY".Trim()).ToString() == "0")
                    {
                        fpMain.Sheets[0].RowHeader.Cells[i, 0].Text = "";
                    }
                }

                fpMain.Focus();
                MenuSettingEntity  entity = new MenuSettingEntity();

                entity.BASE_TABLE = "BAD_PERFORMANCE";

                bool _Error = new CoreBusiness().BaseForm1_A10(entity,fpMain,entity.BASE_TABLE.Split('/')[0]);
                
                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");
                    // DisplayMessage("저장 되었습니다.");

                    fpMain.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

    }
}



