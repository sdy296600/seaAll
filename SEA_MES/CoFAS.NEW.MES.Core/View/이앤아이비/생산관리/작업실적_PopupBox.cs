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
    public partial class 작업실적_PopupBox : Form
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
                this.WindowState = FormWindowState.Maximized;
            }
        }

        #endregion

        #region ○ 변수선언

        public string _UserAccount = string.Empty;

        string _pPRODUCTION_RESULT_ID = string.Empty;

        string _pSTOCK_MST_ID = string.Empty;
        string _pMAIN_STOCK_MST_ID = string.Empty;
        MenuSettingEntity _MenuSettingEntity = null;
        #endregion


        #region ○ 생성자

        public 작업실적_PopupBox(string pPRODUCTION_RESULT_ID , string userEntity,string main_stock_mst)
        {
            _UserAccount = userEntity;

            _pPRODUCTION_RESULT_ID = pPRODUCTION_RESULT_ID;

            _pMAIN_STOCK_MST_ID = main_stock_mst;
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
                fpSub2._ChangeEventHandler += FpMain_Change;


                fpMain.CellDoubleClick -= new CellClickEventHandler(pfpSpread_CellClick);
                fpMain.CellDoubleClick += new CellClickEventHandler(pfpSpread_CellClick);
                _MenuSettingEntity = new MenuSettingEntity();
                _MenuSettingEntity.BASE_ORDER = "";
                _MenuSettingEntity.MENU_WINDOW_NAME = this.Name;
                _MenuSettingEntity.BASE_TABLE = "OUT_STOCK_DETAIL";

                DataTable pDataTable1 =  new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME,fpMain,"");
                DataTable pDataTable2 =  new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME,fpSub1,"OUT_STOCK_DETAIL");
                DataTable pDataTable3 =  new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME,fpSub2,"OUT_STOCK_DETAIL");
                if (pDataTable1 != null)
                {

                    Function.Core.initializeSpread(pDataTable1, fpMain, this._MenuSettingEntity.MENU_WINDOW_NAME, _UserAccount);
                    //Function.Core.InitializeControl(pDataTable, fpMain, this, panel1, _MenuSettingEntity);
                }
                if (pDataTable2 != null)
                {

                    Function.Core.initializeSpread(pDataTable2, fpSub1, this._MenuSettingEntity.MENU_WINDOW_NAME, _UserAccount);
                    Function.Core.InitializeControl(pDataTable2, fpSub1, this, _PAN_WHERE, _MenuSettingEntity);
                }

                if (pDataTable3 != null)
                {

                    Function.Core.initializeSpread(pDataTable3, fpSub2, this._MenuSettingEntity.MENU_WINDOW_NAME, _UserAccount);

                }

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
                xFpSpread xFpSpread = sender as xFpSpread;

                string pHeaderLabel = xFpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                switch (pHeaderLabel)
                {
                    case "":
                        xFpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text = "수정";
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

        private void pfpSpread_CellClick(object obj, CellClickEventArgs e)
        {
            try
            {
                if (e.ColumnHeader)
                {
                    return;

                }
                else
                {
                 
                    xFpSpread pfpSpread = obj as xFpSpread;
                    if (pfpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text != "합계")
                    {
                        _pSTOCK_MST_ID = pfpSpread.Sheets[0].GetValue(e.Row, "C.ID").ToString();

                        Sub1Find_DisplayData();

                        Sub2Find_DisplayData();
                    }
                }

            }
            catch (Exception err)
            {

            }

        }

        #region ○ 데이터 영역


        private void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;


                string str = $@"                                SELECT A.ID
                                      ,C.ID   AS 'C.ID'
                                      ,C.OUT_CODE
                                      ,C.NAME
                                      ,C.STANDARD
                                      ,C.TYPE
	                                  ,ISNULL(C.QTY,0)                        AS 'QTY'
	                                  ,ISNULL(SUM((A.QTY * B.CONSUME_QTY)),0) AS 'CONSUME_QTY'
	                                  ,ISNULL(D.OUT_QTY,0)                    AS 'OUT_QTY'
                                 FROM [dbo].[PRODUCTION_RESULT] A
                                 INNER JOIN BOM B ON A.STOCK_MST_ID = B.STOCK_MST_ID
                                 INNER JOIN STOCK_MST C ON B.SUB_STOCK_MST_ID = C.ID
                                  LEFT JOIN
                                  (
                                  SELECT SUM(OUT_QTY) AS OUT_QTY
                                  ,STOCK_MST_ID
                                  ,PRODUCTION_RESULT_ID
                                    FROM OUT_STOCK_DETAIL
                               	WHERE 1=1
                               	AND PRODUCTION_RESULT_ID IS NOT NULL
                               	AND USE_YN = 'Y'
                               	GROUP BY STOCK_MST_ID,PRODUCTION_RESULT_ID
                                  ) D ON A.ID = D.PRODUCTION_RESULT_ID AND C.ID = D.STOCK_MST_ID
                               where 1=1
                               AND A.ID = {_pPRODUCTION_RESULT_ID} 
                               AND B.STOCK_MST_ID !=  B.SUB_STOCK_MST_ID
							   GROUP BY  A.ID
							            ,C.ID  
										,C.OUT_CODE
										,C.NAME
										,C.STANDARD
										,C.TYPE
                                        ,D.OUT_QTY
                                        ,C.QTY";
                //AND D.ORDER_TYPE LIKE '%SD09%'";

                DataTable _DataTable = new CoreBusiness().SELECT(str);

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

        private void Sub1Find_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub1.Sheets[0].Rows.Count = 0;


                string str = $@"SELECT								   
                                       0										   AS 'ID'				   
                                      ,C.OUT_CODE		                           AS 'C.OUT_CODE'		   
                                      ,C.IN_STOCK_DATE	                           AS 'C.IN_STOCK_DATE'	   
                                      ,C.IN_TYPE		                           AS 'C.IN_TYPE'		   
                                      ,C.STOCK_MST_ID                              AS 'C.STOCK_MST_ID'   
	                                  ,D.OUT_CODE	                               AS 'D.OUT_CODE'	    
                                      ,D.NAME	                                   AS 'D.NAME'	        
	                                  ,D.STANDARD	                               AS 'D.STANDARD'	    
	                                  ,D.TYPE                                      AS 'D.TYPE'           
                                      ,C.IN_QTY			                           AS 'C.IN_QTY'			   
                                      ,C.USED_QTY	                     	       AS 'C.USED_QTY'		   
                                      ,C.REMAIN_QTY                      		   AS 'C.REMAIN_QTY'	
                                      ,0										   AS 'OUT_QTY'
                                      ,{_pPRODUCTION_RESULT_ID} 				   AS 'E.PRODUCTION_RESULT_ID'
                                      ,GETDATE()								   AS 'E.OUT_STOCK_DATE'
                                      ,'SD14002'								   AS 'E.OUT_TYPE'
                                      ,C.ID                                        AS 'E.IN_STOCK_DETAIL_ID'
                                      ,''										   AS 'E.COMMENT'	
                                      ,'N'										   AS 'E.COMPLETE_YN'      
                                      ,'Y'										   AS 'E.USE_YN'		
                                      ,'{_UserAccount}'							   AS 'E.REG_USER'	
                                      ,GETDATE()								   AS 'E.REG_DATE'
                                      ,'{_UserAccount}'							   AS 'E.UP_USER'
                                      ,GETDATE()								   AS 'E.UP_DATE'
                                      from [dbo].[IN_STOCK_DETAIL] C  
                                      INNER JOIN [dbo].[STOCK_MST] D ON C.STOCK_MST_ID = D.ID			 
                                      WHERE 1=1
									  AND C.USE_YN = 'Y' 
									  AND C.REMAIN_QTY >0
                                      AND C.STOCK_MST_ID = {_pSTOCK_MST_ID}";

                StringBuilder sb = new StringBuilder();
                ;
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString();

                DataTable _DataTable = new CoreBusiness().SELECT(sql);


                Function.Core.DisplayData_Set(_DataTable, fpSub1);

                for (int i = 0; i < _DataTable.Rows.Count; i++)
                {
                    fpSub1.Sheets[0].RowHeader.Cells[i, 0].Text = "입력";
                    //fpSub1.Sheets[0].SetValue(i, "OUT_QTY".Trim(), Convert.ToDecimal(_pQTY.ToString().Replace(".00", "")));
                    //fpSub1.Sheets[0].SetValue(i, "E.REG_USER ".Trim(), _UserAccount);
                    //fpSub1.Sheets[0].SetValue(i, "E.REG_DATE ".Trim(), DateTime.Now);
                    //fpSub1.Sheets[0].SetValue(i, "E.UP_USER  ".Trim(), _UserAccount);
                    //fpSub1.Sheets[0].SetValue(i, "E.UP_DATE  ".Trim(), DateTime.Now);
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
        private void Sub2Find_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub2.Sheets[0].Rows.Count = 0;

                string sql = $@"SELECT A.ID						AS	 'ID'					                                  
                                      ,A.PRODUCTION_RESULT_ID	AS	 'A.PRODUCTION_RESULT_ID'		
                                      ,A.STOCK_MST_ID			AS	 'A.STOCK_MST_ID'			
                                      ,A.IN_STOCK_DETAIL_ID		AS	 'A.IN_STOCK_DETAIL_ID'		
                                	  ,C.OUT_CODE 				AS	 'C.OUT_CODE' 				
                                	  ,B.OUT_CODE				AS	 'B.OUT_CODE'				
                                	  ,B.NAME					AS	 'B.NAME'					
                                	  ,B.STANDARD				AS	 'B.STANDARD'				
                                	  ,B.TYPE					AS	 'B.TYPE'					
                                      ,A.OUT_QTY			    AS	 'A.OUT_QTY'				
                                      ,A.COMMENT			    AS	 'A.COMMENT'				
                                      ,A.COMPLETE_YN		    AS	 'A.COMPLETE_YN'			
                                      ,A.USE_YN					AS	 'A.USE_YN'					
                                      ,A.REG_USER				AS	 'A.REG_USER'				
                                      ,A.REG_DATE				AS	 'A.REG_DATE'				
                                      ,A.UP_USER				AS	 'A.UP_USER'				
                                      ,A.UP_DATE				AS	 'A.UP_DATE'				
                                FROM [dbo].[OUT_STOCK_DETAIL] A							  
                                INNER JOIN STOCK_MST B ON Ａ.STOCK_MST_ID = B.ID                           
                                INNER JOIN IN_STOCK_DETAIL C ON A.IN_STOCK_DETAIL_ID = C.ID
                                WHERE 1=1                                 
                                      AND A.STOCK_MST_ID         = {_pSTOCK_MST_ID} 
                                      AND A.PRODUCTION_RESULT_ID = {_pPRODUCTION_RESULT_ID};";

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpSub2);
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

        private void btn_저장_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                decimal maxqty = 0;

                for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                {
                    if (_pSTOCK_MST_ID == fpMain.Sheets[0].GetValue(i, "C.ID").ToString())
                    {
                        maxqty = Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "CONSUME_QTY"))
                               - Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "OUT_QTY"));

                    }
                }



                decimal inqty = 0;

                for (int i = 0; i < fpSub1.Sheets[0].Rows.Count; i++)
                {
                    decimal out_qty = Convert.ToDecimal(fpSub1.Sheets[0].GetValue(i, "OUT_QTY".Trim()));
                    if (out_qty == 0)
                    {
                        fpSub1.Sheets[0].RowHeader.Cells[i, 0].Text = "";
                    }
                    else
                    {
                        inqty += out_qty;
                    }
                }
                if (maxqty < inqty)
                {
                    CustomMsg.ShowMessage("출고 수량은 소요 수량을 넘을수 없습니다.");
                    return;
                }

                fpMain.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(_MenuSettingEntity, fpSub1, "OUT_STOCK_DETAIL");

                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");
                    //DisplayMessage("저장 되었습니다.");

                    fpSub1.Sheets[0].Rows.Count = 0;
                    fpSub2.Sheets[0].Rows.Count = 0;
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

        private void btn_포장이력저장_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                decimal maxqty = 0;

                for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                {
                    if (fpMain.Sheets[0].RowHeader.Cells[i, 0].Text != "합계")
                    {
                        if (_pSTOCK_MST_ID == fpMain.Sheets[0].GetValue(i, "C.ID").ToString())
                        {
                            maxqty = Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "CONSUME_QTY"));
                        }
                    }
                }


                decimal inqty = 0;

                for (int i = 0; i < fpSub2.Sheets[0].Rows.Count; i++)
                {
                    if (fpSub2.Sheets[0].RowHeader.Cells[i, 0].Text != "합계")
                    {
                        decimal out_qty = Convert.ToDecimal(fpSub2.Sheets[0].GetValue(i, "A.OUT_QTY".Trim()));
                        inqty += out_qty;
                    }
                }

                if (maxqty < inqty)
                {
                    CustomMsg.ShowMessage("출고 수량은 소요수량을 넘을수 없습니다.");
                    return;
                }

                fpMain.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(_MenuSettingEntity, fpSub2, "OUT_STOCK_DETAIL");

                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");
                    //DisplayMessage("저장 되었습니다.");
                    fpSub1.Sheets[0].Rows.Count = 0;
                    fpSub2.Sheets[0].Rows.Count = 0;
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

        private void btn_bom_Click(object sender, EventArgs e)
        {
            try
            {

                BaseBomPopupBox baseBomPopupBox = new BaseBomPopupBox(_pMAIN_STOCK_MST_ID);
                baseBomPopupBox.Show();

            }
            catch (Exception pExcption)
            {
              
            }
            finally
            {
                DevExpressManager.SetCursor(this, Cursors.Default);
            }
        }
    }
}
