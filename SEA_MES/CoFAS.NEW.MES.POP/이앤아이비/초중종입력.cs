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
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class 초중종입력 : Form
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

        public string _pPRODUCTION_INSTRUCT_ID = string.Empty;
        public string _pSTOCK_MST_ID = string.Empty;

        private UserEntity _UserEntity = new UserEntity();

        #endregion

        #region ○ 생성자

        public 초중종입력(UserEntity _pUserEntity)
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

                DataTable pDataTable1 =  new CoreBusiness().BASE_MENU_SETTING_R10(this.Name,fpMain,"SELF_INSPECTION");



                if (pDataTable1 != null && pDataTable1.Rows.Count != 0)
                {
                    CoFAS.NEW.MES.Core.Function.Core.initializeSpread(pDataTable1, fpMain, this.Name, _UserEntity.user_account);
                    CoFAS.NEW.MES.Core.Function.Core.InitializeControl(pDataTable1, fpMain, this, _PAN_WHERE,  new MenuSettingEntity() { BASE_TABLE = "" });
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
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT 
                                      ISNULL(C.ID,0)					                          AS  'ID'	
                                     ,D.CODE_NAME                                                 AS  'TYPE'
                                     ,A.INSPECTION_POINT                                          AS  'POINT'
                                     ,ISNULL(C.PRODUCTION_INSTRUCT_ID,{_pPRODUCTION_INSTRUCT_ID}) AS  'PRODUCTION_INSTRUCT_ID'
                                     ,A.ID					                                      AS  'INSPECTION_STANDARD_ID'			 					 
                                     ,A.STOCK_MST_ID		                                      AS  'STOCK_MST_ID'			 
                                     ,C.VALUE1				                                      AS  'VALUE1'
									 ,C.VALUE2				                                      AS  'VALUE2'
									 ,C.VALUE3				                                      AS  'VALUE3'
									 ,C.VALUE4				                                      AS  'VALUE4'
									 ,C.VALUE5				                                      AS  'VALUE5'
                                     ,ISNULL(C.USE_YN  ,'Y')				                      AS  'USE_YN'				 
                                     ,ISNULL(C.COMMENT ,'')				                          AS  'COMMENT'				 
                                     ,ISNULL(C.REG_USER,'{_UserEntity.user_account}')	          AS  'REG_USER'				 
                                     ,ISNULL(C.REG_DATE,GETDATE())	                              AS  'REG_DATE'				 
                                     ,ISNULL(C.UP_USER ,'{_UserEntity.user_account}')	          AS  'UP_USER'				 
                                     ,ISNULL(C.UP_DATE ,GETDATE())	                              AS  'UP_DATE'				 
                                     FROM STOCK_INSPECTION_STANDARD A
                                     INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID
                                     INNER JOIN CODE_MST D ON A.TYPE = D.CODE
                                      LEFT JOIN [dbo].[SELF_INSPECTION] C ON A.ID = C.INSPECTION_STANDARD_ID AND C.PRODUCTION_INSTRUCT_ID = {_pPRODUCTION_INSTRUCT_ID}
                                     WHERE 1=1 
                                       AND A.STOCK_MST_ID = {_pSTOCK_MST_ID}";
 
             
                StringBuilder sb = new StringBuilder();

                CoFAS.NEW.MES.Core.Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString();



                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                CoFAS.NEW.MES.Core.Function.Core.DisplayData_Set(_DataTable, fpMain);

                for (int i = 0; i < _DataTable.Rows.Count; i++)
                {
                    if (_DataTable.Rows[i]["ID"].ToString() == "0")
                    {
                        fpMain.Sheets[0].RowHeader.Cells[i, 0].Text = "입력";                      
                    }
                }


            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {
                DevExpressManager.SetCursor(this, Cursors.Default);
            }
        }



        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_저장_Click(object sender, EventArgs e)
        {
            try
            {
              

                fpMain.Focus();
                MenuSettingEntity  entity = new MenuSettingEntity();

                entity.BASE_TABLE = "SELF_INSPECTION";

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


        private void BTN_조회_Click(object sender, EventArgs e)
        {
            MainFind_DisplayData();
        }













        #endregion



     
    }
}



