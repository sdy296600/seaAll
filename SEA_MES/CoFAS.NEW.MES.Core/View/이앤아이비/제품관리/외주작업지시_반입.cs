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
    public partial class 외주작업지시_반입 : Form
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

        public string _PRODUCTION_INSTRUCT_ID = string.Empty;


        public string _STOCK_ID       = string.Empty;
        public string _STOCK_OUT_CODE = string.Empty;
        public string _STOCK_NAME     = string.Empty;
        public string _STOCK_STANDARD = string.Empty;
        public string _STOCK_TYPE     = string.Empty;
        public string _QTY            = string.Empty;

        MenuSettingEntity _MenuSettingEntity = null;
        #endregion


        #region ○ 생성자

        public 외주작업지시_반입(string ID 
            , string userEntity
            , string STOCK_ID
            , string STOCK_OUT_CODE
            , string STOCK_NAME
            , string STOCK_STANDARD
            , string STOCK_TYPE
            , string QTY)
        {
            _PRODUCTION_INSTRUCT_ID = ID;
            _UserAccount = userEntity;

            _STOCK_ID       = STOCK_ID;
            _STOCK_OUT_CODE = STOCK_OUT_CODE;
            _STOCK_NAME     = STOCK_NAME;
            _STOCK_STANDARD = STOCK_STANDARD;
            _STOCK_TYPE     = STOCK_TYPE;
            _QTY            = QTY;

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
                _MenuSettingEntity.BASE_TABLE = "IN_STOCK_WAIT_DETAIL";

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

                string str = $@"SELECT 
	                                   A.ID						AS 'ID'						
                                      ,A.ORDER_DETAIL_ID		AS 'A.ORDER_DETAIL_ID'		
                                      ,A.PRODUCTION_INSTRUCT_ID	AS 'A.PRODUCTION_INSTRUCT_ID'	
                                      ,A.IN_TYPE				AS 'A.IN_TYPE'				
                                      ,A.IN_DATE				AS 'A.IN_DATE'				
                                      ,A.OUT_CODE				AS 'A.OUT_CODE'				
                                      ,A.STOCK_MST_ID			AS 'A.STOCK_MST_ID'			
                                      ,A.IN_QTY					AS 'A.IN_QTY'					
                                      ,A.COMPLETE_QTY			AS 'A.COMPLETE_QTY'			
                                      ,A.COMMENT				AS 'A.COMMENT'				
                                      ,A.COMPLETE_YN			AS 'A.COMPLETE_YN'			
                                      ,A.USE_YN					AS 'A.USE_YN'					
                                      ,A.REG_USER				AS 'A.REG_USER'				
                                      ,A.REG_DATE				AS 'A.REG_DATE'				
                                      ,A.UP_USER				AS 'A.UP_USER'				
                                      ,A.UP_DATE				AS 'A.UP_DATE'				
	                                  ,B.OUT_CODE				AS 'B.OUT_CODE'				
	                                  ,B.NAME					AS 'B.NAME'					
	                                  ,B.STANDARD				AS 'B.STANDARD'				
	                                  ,B.TYPE					AS 'B.TYPE'					
                                      FROM IN_STOCK_WAIT_DETAIL A
                                      INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID
                                      WHERE 1=1 
                                      AND A.PRODUCTION_INSTRUCT_ID = {_PRODUCTION_INSTRUCT_ID}
                                      AND A.USE_YN = 'Y'";
                //LEFT JOIN [dbo].[OUT_STOCK_DETAIL]  E ON C.ID = E.IN_STOCK_DETAIL_ID AND A.ID = E.PRODUCTION_INSTRUCT_ID AND E.USE_YN = 'Y'
                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString();



                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpMain);



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
                bool _Error = new CoreBusiness().BaseForm1_A10(_MenuSettingEntity,fpMain,"IN_STOCK_WAIT_DETAIL");
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
                int start = pExcption.Message.IndexOf(" (")+1;
                int end = pExcption.Message.IndexOf(")", start)+1;
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

        private void BTN_조회_Click(object sender, EventArgs e)
        {
            MainFind_DisplayData();
        }

        private void BTN_추가_Click(object sender, EventArgs e)
        {
            Function.Core._AddItemButtonClicked(fpMain, _UserAccount);
            fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "A.STOCK_MST_ID          ".Trim(), _STOCK_ID);
            fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "B.OUT_CODE 	            ".Trim(), _STOCK_OUT_CODE);
            fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "B.NAME                  ".Trim(), _STOCK_NAME);
            fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "B.STANDARD              ".Trim(), _STOCK_STANDARD);
            fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "B.TYPE	                ".Trim(), _STOCK_TYPE);
            fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "A.PRODUCTION_INSTRUCT_ID".Trim(), _PRODUCTION_INSTRUCT_ID);
            fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "A.IN_TYPE               ".Trim(), "SD13006");
            fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "A.IN_QTY                ".Trim(), _QTY);


            //AND A.PRODUCTION_INSTRUCT_ID = { _PRODUCTION_INSTRUCT_ID}

            //string mst=  this._pMenuSettingEntity.BASE_TABLE.Split('/')[0] +"_ID";
            //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, mst, _Mst_Id);
            //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "DEMAND_COMPANY".Trim(), fpMain.Sheets[0].GetValue(row, "ORDER_COMPANY".Trim()));
        }
    }
    }
