using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Provider;
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
using FarPoint.Win.Spread.CellType;

namespace CoFAS.NEW.MES.Core
{
    public partial class 초중종검사결과등록_PopupBox : Form
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

        public string _STOCK_ID = string.Empty;
        public string _INSPECTION_ID = string.Empty;
        public string _INSPECT_TYPE = string.Empty;

        MenuSettingEntity _MenuSettingEntity = null;
        #endregion

        #region ○ 생성자

        public 초중종검사결과등록_PopupBox(string ID
            , string userEntity
            , string INSPECTION_ID
            , string INSPECT_TYPE)
        {
            _UserAccount = userEntity;

            _STOCK_ID = ID;
            _INSPECTION_ID = INSPECTION_ID;
            _INSPECT_TYPE = INSPECT_TYPE;

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
                _MenuSettingEntity.BASE_TABLE = "SELF_INSPECTION_DETAIL";

                DataTable pDataTable = new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME, fpMain, _MenuSettingEntity.BASE_TABLE.Split('/')[0]);
                if (pDataTable != null)
                {

                    Function.Core.initializeSpread(pDataTable, fpMain, this._MenuSettingEntity.MENU_WINDOW_NAME, _UserAccount);
                    //Function.Core.InitializeControl(pDataTable, fpMain, this, _PAN_WHERE, _MenuSettingEntity);
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

                string str = $@"SELECT 
	                                   A.ID							AS 'C.INSPECTION_STANDARD_ID'
									  ,A.STOCK_MST_ID				AS 'C.STOCK_MST_ID'	
                                      ,A.STANDARD_VALUE			    AS 'A.STANDARD_VALUE'
                                      ,A.COMMENT				    AS 'A.COMMENT'
                                      ,A.TYPE				        AS 'A.TYPE'	
                                      ,A.USE_YN						AS 'A.USE_YN'
									  ,ISNULL(C.ID,0)				AS 'ID'
									  ,B.ID	                        AS 'C.SELF_INSPECTION_MST_ID'
									  ,A.STANDARD_VALUE				AS 'A.STANDARD_VALUE'
									  ,C.VALUE						AS 'C.VALUE'
									  ,C.COMMENT					AS 'C.COMMENT'
									  ,C.USE_YN			            AS 'C.USE_YN'
                                      ,C.REG_USER					AS 'C.REG_USER'				
                                      ,C.REG_DATE					AS 'C.REG_DATE'				
                                      ,C.UP_USER					AS 'C.UP_USER'				
                                      ,C.UP_DATE					AS 'C.UP_DATE'				
	                                  ,D.OUT_CODE					AS 'D.OUT_CODE'				
	                                  ,D.NAME						AS 'D.NAME'					
	                                  ,D.STANDARD					AS 'D.STANDARD'				
	                                  ,D.TYPE						AS 'D.TYPE'
                                      FROM STOCK_INSPECTION_STANDARD A
                                      INNER JOIN SELF_INSPECTION_MST B ON A.STOCK_MST_ID = B.STOCK_MST_ID
									  LEFT JOIN SELF_INSPECTION_DETAIL C ON A.ID = C.INSPECTION_STANDARD_ID
									  INNER JOIN STOCK_MST D ON A.STOCK_MST_ID = D.ID
                                      WHERE 1=1 
                                      AND A.STOCK_MST_ID = {_STOCK_ID}
                                      AND A.USE_YN = 'Y'
                                      AND B.ID = {_INSPECTION_ID}
                                      AND A.TYPE = '{_INSPECT_TYPE}'";
                //LEFT JOIN [dbo].[OUT_STOCK_DETAIL]  E ON C.ID = E.IN_STOCK_DETAIL_ID AND A.ID = E.PRODUCTION_INSTRUCT_ID AND E.USE_YN = 'Y'
                StringBuilder sb = new StringBuilder();

                //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str /*+ sb.ToString()*/;

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpMain);

                if (_DataTable.Rows.Count > 1)
                {
                    label1.Text = "현재 품목 : " + fpMain.Sheets[0].GetValue(0,"D.OUT_CODE") + " , " + fpMain.Sheets[0].GetValue(0,"D.NAME");
                }

                for (int i = 0; i < _DataTable.Rows.Count; i++)
                {
                    if (_DataTable.Rows[i]["ID"].ToString() == "0")
                    {
                        fpMain.Sheets[0].RowHeader.Cells[i, 0].Text = "입력";
                        fpMain.Sheets[0].SetValue(i, "C.USE_YN ".Trim(), "Y");
                        fpMain.Sheets[0].SetValue(i, "C.REG_USER ".Trim(), _UserAccount);
                        fpMain.Sheets[0].SetValue(i, "C.REG_DATE ".Trim(), DateTime.Now);
                        fpMain.Sheets[0].SetValue(i, "C.UP_USER  ".Trim(), _UserAccount);
                        fpMain.Sheets[0].SetValue(i, "C.UP_DATE  ".Trim(), DateTime.Now);
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

        private void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                //for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                //{
                //    if (fpMain.Sheets[0].RowHeader.Cells[i, 0].Text != "수정")
                //    {
                //        if (Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "OUT_QTY".Trim())) == 0)
                //        {
                //            fpMain.Sheets[0].RowHeader.Cells[i, 0].Text = "";
                //        }
                //    }
                //}
                fpMain.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(_MenuSettingEntity, fpMain, "SELF_INSPECTION_DETAIL");
                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");
                    //DisplayMessage("저장 되었습니다.");

                    fpMain.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
                }
            }
            catch (Exception pExcption)
            {
                int start = pExcption.Message.IndexOf(" (") + 1;
                int end = pExcption.Message.IndexOf(")", start) + 1;
                string constraintName = pExcption.Message.Substring(start, end - start);
                CustomMsg.ShowExceptionMessage($"중복 값을 입력 하실수 없습니다. 중복값 {constraintName} 입니다.", "Error", MessageBoxButtons.OK);
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

        private void BTN_저장_Click(object sender, EventArgs e)
        {
            try
            {
                if (Function.Core._SaveButtonClicked(fpMain))
                {

                    if (fpMain.Sheets[0].Rows.Count > 0)
                        MainSave_InputData();
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

     
    }
}