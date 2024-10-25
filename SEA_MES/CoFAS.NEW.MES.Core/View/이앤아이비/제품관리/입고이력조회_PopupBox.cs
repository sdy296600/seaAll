using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 입고이력조회_PopupBox : Form
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
            }
        }

        #endregion

        #region ○ 변수선언

        public string _UserAccount = string.Empty;
        public string _UserAuthority = string.Empty;

        //UserEntity _UserEntity = new UserEntity();
        string _pORDER_MST_ID = string.Empty;
        string _pID = string.Empty;
        string _pdetail_id = string.Empty;
        private bool _FirstYn = true;

        MenuSettingEntity _MenuSettingEntity = null;
        #endregion


        #region ○ 생성자

        public 입고이력조회_PopupBox(string pORDER_MST_ID, string userEntity)
        {
            _UserAccount = userEntity;
            _pORDER_MST_ID = pORDER_MST_ID;
            InitializeComponent();

            Load += new EventHandler(Form_Load);
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                fpMain._ChangeEventHandler += FpMain_Change;
                _MenuSettingEntity = new MenuSettingEntity();
                _MenuSettingEntity.BASE_ORDER = "";
                _MenuSettingEntity.MENU_WINDOW_NAME = this.Name;
                _MenuSettingEntity.BASE_TABLE = "IN_STOCK_DETAIL";

                DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME,fpMain,_MenuSettingEntity.BASE_TABLE.Split('/')[0]);
                if (pDataTable != null)
                {

                    Function.Core.initializeSpread(pDataTable, fpMain, this._MenuSettingEntity.MENU_WINDOW_NAME, _UserAccount);
                    Function.Core.InitializeControl(pDataTable, fpMain, this, _PAN_WHERE, _MenuSettingEntity);
                }

                //LeftFind_DisplayData();
                MainFind_DisplayData();


            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }



        #endregion

        #region ○ 초기화 영역

       
       
      

        private void FpMain_Change(object sender, ChangeEventArgs e)
        {
            try
            {

                string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                switch (pHeaderLabel)
                {
                    case "":
                        fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text = "수정";
                        break;
                    case "입력":
                        break;
                    case "수정":
                        break;
                    case "삭제":
                        break;
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 데이터 영역


        private void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

               
                //string str = $@"SELECT 
                // A.ID AS ID
                //,A.OUT_STOCK_DATE AS OUT_STOCK_DATE
                //,A.OUT_CODE AS OUT_CODE
                //,A.OUT_TYPE AS OUT_TYPE
                //,A.OUT_STOCK_MST_ID AS OUT_STOCK_MST_ID
                //,A.ORDER_DETAIL_ID AS ORDER_DETAIL_ID
                //,A.PRODUCTION_RESULT_ID AS PRODUCTION_RESULT_ID
                //,A.STOCK_MST_ID AS STOCK_MST_ID
                //,B.OUT_CODE AS STOCK_MST_OUT_CODE
                //,B.STANDARD AS STOCK_MST_STANDARD
                //,B.TYPE AS STOCK_MST_TYPE
                //,A.OUT_QTY AS OUT_QTY
                //,A.COMMENT AS COMMENT
                //,A.COMPLETE_YN AS COMPLETE_YN
                //,A.USE_YN AS USE_YN
                //,A.REG_USER AS REG_USER
                //,A.REG_DATE AS REG_DATE
                //,A.UP_USER AS UP_USER
                //,A.UP_DATE AS UP_DATE

                //from OUT_STOCK_DETAIL A
                //INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID
                //               WHERE 1=1
                //               and A.STOCK_MST_ID = {_pORDER_MST_ID}
                //               and A.ORDER_DETAIL_ID = {_pdetail_id}";

                string str = $@"SELECT 
                                A.ID                            AS 'ID'
							   ,A.OUT_CODE                      AS 'A.OUT_CODE'
							   ,A.IN_STOCK_DATE                 AS 'A.IN_STOCK_DATE'
							   ,A.IN_TYPE                       AS 'A.IN_TYPE'
                               ,A.ORDER_DETAIL_ID               AS 'A.ORDER_DETAIL_ID'
                               ,A.STOCK_MST_ID                  AS 'A.STOCK_MST_ID'
                               ,C.OUT_CODE						AS 'C.OUT_CODE'
                               ,C.NAME							AS 'C.NAME'
							   ,E.code_name						AS 'E.CODE_NAME'
                               ,C.OUT_SCHEDULE					AS 'C.OUT_SCHEDULE'
                               ,C.IN_SCHEDULE					AS 'C.IN_SCHEDULE'
                               ,C.QTY							AS 'C.QTY'
                               ,A.IN_QTY              　　　    AS 'A.IN_QTY'  
                               ,A.COMMENT              　　　   AS 'A.COMMENT'  
                               ,A.COMPLETE_YN          　　　   AS 'A.COMPLETE_YN'
                               ,A.USE_YN               　　　   AS 'A.USE_YN'  
                               ,A.REG_USER             　　　   AS 'A.REG_USER'
                               ,A.REG_DATE             　　　   AS 'A.REG_DATE'
                               ,A.UP_USER              　　　   AS 'A.UP_USER'
                               ,A.UP_DATE              　　　   AS 'A.UP_DATE'
                               FROM IN_STOCK_DETAIL A
							   INNER JOIN STOCK_MST C ON A.STOCK_MST_ID = C.ID                           
                              INNER JOIN Code_Mst E ON C.TYPE = E.code
							  WHERE 1=1
							   AND A.USE_YN = 'Y'
                               AND A.STOCK_MST_ID = {_pORDER_MST_ID}";

                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString();

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpMain.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName].ToString());
                        }
                    }
                    fpMain.Sheets[0].Visible = true;


                }
                else
                {
                    fpMain.Sheets[0].Rows.Count = 0;
                    CustomMsg.ShowMessage("조회 내역이 없습니다.");
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

  

        #endregion

     

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _AuthCopy_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                {
                    if(fpMain.Sheets[0].GetValue(i, "OUT_QTY".Trim()).ToString() == "0.00")
                    {
                        fpMain.Sheets[0].RowHeader.Cells[i, 0].Text = "";
                    }
                }
                fpMain.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(_MenuSettingEntity,fpMain,_MenuSettingEntity.BASE_TABLE.Split('/')[0]);
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


    }
}
