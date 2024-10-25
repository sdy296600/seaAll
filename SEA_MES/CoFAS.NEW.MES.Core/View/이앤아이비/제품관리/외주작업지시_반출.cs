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
    public partial class 외주작업지시_반출 : Form
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

        public string _INSTRUCT_ID = string.Empty;
        public string _QTY = string.Empty;
        public string _NAME = string.Empty;

        public string _출고타입 = "SD14005";

        MenuSettingEntity _MenuSettingEntity = null;
        #endregion


        #region ○ 생성자

        public 외주작업지시_반출(string ID ,string userEntity ,string name ,  string qty)
        {
            _INSTRUCT_ID = ID;
            _UserAccount = userEntity;
            _QTY = qty;
            _NAME = name;
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
                _MenuSettingEntity.BASE_TABLE = "OUT_STOCK_DETAIL";
                label1.Text = "현재 제품 : " + _NAME;
                label2.Text = "지시 수량 : " + _QTY.ToString().Replace(".00","");
                DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME,fpMain,_MenuSettingEntity.BASE_TABLE.Split('/')[0]);
                DataTable pDataTable2 = new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME, fpSub, _MenuSettingEntity.BASE_TABLE.Split('/')[0]);
                if (pDataTable != null)
                {

                    Function.Core.initializeSpread(pDataTable, fpMain, this._MenuSettingEntity.MENU_WINDOW_NAME, _UserAccount);
                    Function.Core.InitializeControl(pDataTable, fpMain, this, _PAN_WHERE, _MenuSettingEntity);
                }

                if (pDataTable2 != null)
                {
                    Function.Core.initializeSpread(pDataTable2, fpSub, _MenuSettingEntity.MENU_WINDOW_NAME, _UserAccount);
                    //initializeSpread(pDataTable2, fpSub);
                }

                //LeftFind_DisplayData();
                MainFind_DisplayData();
                SubFind_DisplayData();
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

                string str = $@"SELECT 0	                                       AS 'ID'				   
                                      ,C.OUT_CODE		                           AS 'C.OUT_CODE'		   
                                      ,C.IN_STOCK_DATE	                           AS 'C.IN_STOCK_DATE'	   
                                      ,C.IN_TYPE		                           AS 'C.IN_TYPE'		   
                                      ,C.STOCK_MST_ID                              AS 'C.STOCK_MST_ID'   
	                                  ,D.OUT_CODE	                               AS 'D.OUT_CODE'	    
                                      ,D.NAME	                                   AS 'D.NAME'	        
	                                  ,D.STANDARD	                               AS 'D.STANDARD'	    
	                                  ,D.TYPE                                      AS 'D.TYPE'           
                                      ,C.IN_QTY			                           AS 'C.IN_QTY'			   
                                      ,C.USED_QTY		                           AS 'C.USED_QTY'		   
                                      ,C.REMAIN_QTY		                           AS 'C.REMAIN_QTY'	
                                      ,0                                           AS 'OUT_QTY'
                                      ,GETDATE()                                   AS 'E.OUT_STOCK_DATE'
                                      ,'{_출고타입}'                                   AS 'E.OUT_TYPE'
                                      ,A.ID                                        AS 'E.PRODUCTION_INSTRUCT_ID'
                                      ,C.ID                                        AS 'E.IN_STOCK_DETAIL_ID'
                                      ,''                                          AS 'E.COMMENT'	
                                      ,'N'                                         AS 'E.COMPLETE_YN'      
                                      ,'Y'                                         AS 'E.USE_YN'		
                                      ,''                                          AS 'E.REG_USER'	
                                      ,''                                          AS 'E.REG_DATE'
                                      ,''                                          AS 'E.UP_USER'
                                      ,''                                          AS 'E.UP_DATE'
                                      from [dbo].[PRODUCTION_INSTRUCT] A
                                      INNER JOIN [dbo].[BOM] B ON A.STOCK_MST_ID = B.STOCK_MST_ID AND B.STOCK_MST_ID != B.SUB_STOCK_MST_ID
                                      INNER JOIN [dbo].[IN_STOCK_DETAIL] C ON B.SUB_STOCK_MST_ID = C.STOCK_MST_ID AND C.USE_YN = 'Y' AND C.REMAIN_QTY >0
                                      INNER JOIN [dbo].[STOCK_MST] D ON C.STOCK_MST_ID = D.ID 
                                      WHERE A.ID = {_INSTRUCT_ID}";
                //LEFT JOIN [dbo].[OUT_STOCK_DETAIL]  E ON C.ID = E.IN_STOCK_DETAIL_ID AND A.ID = E.PRODUCTION_INSTRUCT_ID AND E.USE_YN = 'Y'
                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString()+ "ORDER BY C.IN_STOCK_DATE,C.OUT_CODE";



                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpMain);

                for (int i = 0; i < _DataTable.Rows.Count; i++)
                {
                    if (_DataTable.Rows[i]["ID"].ToString() == "0")
                    {
                        fpMain.Sheets[0].RowHeader.Cells[i, 0].Text = "입력";
                        fpMain.Sheets[0].SetValue(i, "OUT_QTY ".Trim(), Convert.ToDecimal(_QTY.ToString().Replace(".00", "")));
                        fpMain.Sheets[0].SetValue(i, "E.REG_USER ".Trim(), _UserAccount);
                        fpMain.Sheets[0].SetValue(i, "E.REG_DATE ".Trim(), DateTime.Now);
                        fpMain.Sheets[0].SetValue(i, "E.UP_USER  ".Trim(), _UserAccount);
                        fpMain.Sheets[0].SetValue(i, "E.UP_DATE  ".Trim(), DateTime.Now);
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

                for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                {
                    if (fpMain.Sheets[0].RowHeader.Cells[i, 0].Text != "수정")
                    {
                        if (Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "OUT_QTY".Trim())) == 0)
                        {
                            fpMain.Sheets[0].RowHeader.Cells[i, 0].Text = "";
                        }
                    }
                }
                fpMain.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(_MenuSettingEntity,fpMain,"OUT_STOCK_DETAIL");
                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");
                    //DisplayMessage("저장 되었습니다.");

                    fpMain.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
                    SubFind_DisplayData();
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

        private void SubFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;

                string sql = $@"SELECT 
                                A.ID                            AS 'ID'
							   ,A.OUT_CODE                      AS 'OUT_CODE'
							   ,A.OUT_STOCK_DATE                AS 'OUT_STOCK_DATE'
							   ,A.OUT_TYPE                      AS 'OUT_TYPE'
                               ,A.IN_STOCK_DETAIL_ID            AS 'IN_STOCK_DETAIL_ID'
							   ,H.IN_STOCK_DATE                 AS 'IN_STOCK_DATE'
							   ,H.OUT_CODE                      AS 'IN_STOCK_CODE'
                               ,A.STOCK_MST_ID                  AS 'STOCK_MST_ID'
                               ,C.OUT_CODE						AS 'STOCK_MST_OUT_CODE'
                               ,C.NAME							AS 'STOCK_MST_NAME'
                               ,C.OUT_SCHEDULE					AS 'OUT_SCHEDULE'
                               ,C.IN_SCHEDULE					AS 'IN_SCHEDULE'
                               ,C.QTY							AS 'QTY'
                               ,A.OUT_QTY              　　　   AS 'OUT_QTY'  
                               ,A.COMMENT              　　　   AS 'COMMENT'  
                               ,A.COMPLETE_YN          　　　   AS 'COMPLETE_YN'
                               ,A.USE_YN               　　　   AS 'USE_YN'  
                               ,A.REG_USER             　　　   AS 'REG_USER'
                               ,A.REG_DATE             　　　   AS 'REG_DATE'
                               ,A.UP_USER              　　　   AS 'UP_USER'
                               ,A.UP_DATE              　　　   AS 'UP_DATE'
                               FROM OUT_STOCK_DETAIL A
							   INNER JOIN STOCK_MST C ON A.STOCK_MST_ID = C.ID                           
							  INNER JOIN IN_STOCK_DETAIL H ON A.IN_STOCK_DETAIL_ID = H.ID
							  WHERE 1=1
							   AND A.USE_YN = 'Y'
							   AND A.PRODUCTION_INSTRUCT_ID = {_INSTRUCT_ID};";

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpSub.Sheets[0].Visible = false;
                    fpSub.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpSub.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName]);
                        }

                    }
                    Function.Core._AddItemSUM(fpSub);
                    fpSub.Sheets[0].Visible = true;


                }
                else
                {
                    fpSub.Sheets[0].Rows.Count = 0;
                    //CustomMsg.ShowMessage("조회 내역이 없습니다.");
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

        private void _AuthCopy_Click(object sender, EventArgs e)
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MainFind_DisplayData();
        }

     
    }
    }
