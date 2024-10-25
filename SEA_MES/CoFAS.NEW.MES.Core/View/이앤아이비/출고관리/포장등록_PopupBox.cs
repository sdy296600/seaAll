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
    public partial class 포장등록_PopupBox : Form
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
        //public string _UserAuthority = string.Empty;

        //UserEntity _UserEntity = new UserEntity();
        string _pORDER_MST_ID = string.Empty;
        string _pORDER_DETAIL_ID = string.Empty;
        string _pID = string.Empty;
        string _pQTY = string.Empty;


        MenuSettingEntity _MenuSettingEntity = null;
        #endregion


        #region ○ 생성자

        public 포장등록_PopupBox(string pORDER_MST_ID, string pID, string userEntity)
        {
            _UserAccount = userEntity;
            _pID = pID;
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
                fpSub2._ChangeEventHandler += FpMain_Change;


                fpMain.CellDoubleClick -= new CellClickEventHandler(pfpSpread_CellClick);
                fpMain.CellDoubleClick += new CellClickEventHandler(pfpSpread_CellClick);
                _MenuSettingEntity = new MenuSettingEntity();
                _MenuSettingEntity.BASE_ORDER = "";
                _MenuSettingEntity.MENU_WINDOW_NAME = this.Name;
                _MenuSettingEntity.BASE_TABLE = "OUT_STOCK_WAIT_DETAIL";

                DataTable pDataTable1 =  new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME,fpMain,_MenuSettingEntity.BASE_TABLE.Split('/')[0]);
                DataTable pDataTable2 =  new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME,fpSub1,_MenuSettingEntity.BASE_TABLE.Split('/')[0]);
                DataTable pDataTable3 =  new CoreBusiness().BASE_MENU_SETTING_R10(_MenuSettingEntity.MENU_WINDOW_NAME,fpSub2,_MenuSettingEntity.BASE_TABLE.Split('/')[0]);
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
                        if (pfpSpread.Sheets[0].GetValue(e.Row, "A.OUT_TYPE").ToString() != "CD20001")
                        {
                            _pORDER_DETAIL_ID = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();

                            _pQTY = pfpSpread.Sheets[0].GetValue(e.Row, "A.ORDER_QTY").ToString();

                            Sub1Find_DisplayData(pfpSpread.Sheets[0].GetValue(e.Row, "A.STOCK_MST_ID").ToString()
                                               , _pORDER_DETAIL_ID);

                            Sub2Find_DisplayData(pfpSpread.Sheets[0].GetValue(e.Row, "A.STOCK_MST_ID").ToString()
                                               , _pORDER_DETAIL_ID);
                        }
                        else
                        {
                            CustomMsg.ShowMessage("출고상태가 미진행 입니다.");
                            return;
                        }
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


                string str = $@"SELECT A.ID					AS	'ID'					
                                      ,A.ORDER_MST_ID		AS	'A.ORDER_MST_ID'	
                                      ,A.STOCK_MST_ID		AS	'A.STOCK_MST_ID'		
	                                  ,A.SUPPLY_TYPE		AS	'A.SUPPLY_TYPE'
                                      ,A.STOCK_MST_PRICE    AS	'A.STOCK_MST_PRICE'
                                      ,C.OUT_CODE			AS	'C.OUT_CODE'
                                      ,C.NAME		    	AS	'C.NAME'	
                                      ,C.STANDARD			AS	'C.STANDARD'			
                                      ,C.TYPE				AS	'C.TYPE'				
                                      ,C.UNIT				AS	'C.UNIT'				
                                      ,C.PRICE				AS	'C.PRICE'				
                                      ,C.TYPE2				AS	'C.TYPE2'	
                                      ,C.QTY                AS  'C.QTY'        
                                      ,C.IN_SCHEDULE        AS  'C.IN_SCHEDULE' 
                                      ,C.OUT_SCHEDULE       AS  'C.OUT_SCHEDULE'
                                      ,A.CONVERSION_UNIT	AS	'A.CONVERSION_UNIT'	
                                      ,A.ORDER_QTY			AS	'A.ORDER_QTY'			
                                      ,A.ORDER_REMAIN_QTY	AS	'A.ORDER_REMAIN_QTY'	
                                      ,A.BOX_QTY			AS	'A.BOX_QTY'			
                                      ,A.OUT_QTY			AS	'A.OUT_QTY'			
                                      ,A.COST				AS	'A.COST'				
                                      ,A.COMPANY_ID			AS	'A.COMPANY_ID'			
	                                  ,B.NAME				AS	'B.NAME'				
                                      ,A.DEMAND_DATE		AS	'A.DEMAND_DATE'		
                                      ,A.OUT_TYPE			AS	'A.OUT_TYPE'			
                                      ,A.STOP_YN			AS	'A.STOP_YN'			
                                      ,A.COMMENT			AS	'A.COMMENT'			
                                      ,A.SUPPLY_PRICE_KRW	AS	'A.SUPPLY_PRICE_KRW'	
                                      ,A.FOREIGN_CURRENY	AS	'A.FOREIGN_CURRENY'	
                                      ,A.PUBLISH_DATE		AS	'A.PUBLISH_DATE'		
                                      ,A.DEADLINE			AS	'A.DEADLINE'			
                                      ,A.EXPORT_ID			AS	'A.EXPORT_ID'			
                                      ,A.INSPECTION_YN		AS	'A.INSPECTION_YN'		
                                      ,A.COMPLETE_YN		AS	'A.COMPLETE_YN'		
                                      ,A.USE_YN				AS	'A.USE_YN'				
                                      ,A.REG_USER			AS	'A.REG_USER'			
                                      ,A.REG_DATE			AS	'A.REG_DATE'			
                                      ,A.UP_USER			AS	'A.UP_USER'			
                                      ,A.UP_DATE			AS	'A.UP_DATE'												 
                                 FROM [dbo].[ORDER_DETAIL] A 
                                 LEFT JOIN [dbo].[COMPANY] B ON A.COMPANY_ID = B.ID 
                                 INNER JOIN [dbo].[STOCK_MST] C ON A.STOCK_MST_ID = C.ID
								 INNER JOIN [dbo].[ORDER_MST] D ON A.ORDER_MST_ID = D.ID
                                 WHERE 1=1 					                              　
                              　　　AND A.USE_YN = 'Y'
                              　　　AND A.ORDER_MST_ID = {_pORDER_MST_ID}";
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

        private void Sub1Find_DisplayData(string pSTOCK_MST_ID, string pORDER_DETAIL)
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
                                      ,C.USED_QTY	+ ISNULL(E.OUT_QTY,0)	       AS 'C.USED_QTY'		   
                                      ,C.REMAIN_QTY - ISNULL(E.OUT_QTY,0)		   AS 'C.REMAIN_QTY'	
                                      ,0										   AS 'OUT_QTY'
                                      ,GETDATE()								   AS 'E.OUT_STOCK_DATE'
                                      ,'SD14003'								   AS 'E.OUT_TYPE'
                                      ,A.ID										   AS 'E.ORDER_DETAIL_ID'
                                      ,C.ID                                        AS 'E.IN_STOCK_DETAIL_ID'
                                      ,''										   AS 'E.COMMENT'	
                                      ,'N'										   AS 'E.COMPLETE_YN'      
                                      ,'Y'										   AS 'E.USE_YN'		
                                      ,''										   AS 'E.REG_USER'	
                                      ,''										   AS 'E.REG_DATE'
                                      ,''										   AS 'E.UP_USER'
                                      ,''										   AS 'E.UP_DATE'
                                      ,{_pID}                                      AS OUT_STOCK_WAIT_MST_ID
                                      from [dbo].[ORDER_DETAIL] A
                                      INNER JOIN [dbo].[IN_STOCK_DETAIL] C ON A.STOCK_MST_ID = C.STOCK_MST_ID AND C.USE_YN = 'Y' AND C.REMAIN_QTY >0
                                      INNER JOIN [dbo].[STOCK_MST] D ON C.STOCK_MST_ID = D.ID
									   LEFT JOIN
									   (
									   SELECT SUM (OUT_QTY) AS OUT_QTY									       
											 ,IN_STOCK_DETAIL_ID
									  　 FROM [dbo].[OUT_STOCK_WAIT_DETAIL]
									    WHERE  USE_YN ='Y' AND COMPLETE_YN ='N'
										GROUP BY IN_STOCK_DETAIL_ID
									   ) E ON C.ID = E.IN_STOCK_DETAIL_ID

                                      WHERE 1=1
                                      AND A.STOCK_MST_ID = {pSTOCK_MST_ID} 
                                      AND A.ID = {pORDER_DETAIL}
                                      AND C.USE_YN = 'Y'
                                      AND (C.REMAIN_QTY - ISNULL(E.OUT_QTY,0)) > 0";

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
                    fpSub1.Sheets[0].SetValue(i, "E.REG_USER ".Trim(), _UserAccount);
                    fpSub1.Sheets[0].SetValue(i, "E.REG_DATE ".Trim(), DateTime.Now);
                    fpSub1.Sheets[0].SetValue(i, "E.UP_USER  ".Trim(), _UserAccount);
                    fpSub1.Sheets[0].SetValue(i, "E.UP_DATE  ".Trim(), DateTime.Now);
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
        private void Sub2Find_DisplayData(string pSTOCK_MST_ID, string pORDER_DETAIL)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub2.Sheets[0].Rows.Count = 0;

                string sql = $@"SELECT A.ID						AS	 'ID'					
                                     ,A.OUT_STOCK_WAIT_MST_ID	AS	 'A.OUT_STOCK_WAIT_MST_ID'	
                                     ,A.ORDER_DETAIL_ID		    AS	 'A.ORDER_DETAIL_ID'		
                                     ,A.STOCK_MST_ID			AS	 'A.STOCK_MST_ID'			
                                     ,A.IN_STOCK_DETAIL_ID		AS	 'A.IN_STOCK_DETAIL_ID'		
                                	 ,C.OUT_CODE 				AS	 'C.OUT_CODE' 				
                                	 ,B.OUT_CODE				AS	 'B.OUT_CODE'				
                                	 ,B.NAME					AS	 'B.NAME'					
                                	 ,B.STANDARD				AS	 'B.STANDARD'				
                                	 ,B.TYPE					AS	 'B.TYPE'					
                                     ,A.OUT_QTY				    AS	 'A.OUT_QTY'				
                                     ,A.COMMENT				    AS	 'A.COMMENT'				
                                     ,A.COMPLETE_YN			    AS	 'A.COMPLETE_YN'			
                                     ,A.USE_YN					AS	 'A.USE_YN'					
                                     ,A.REG_USER				AS	 'A.REG_USER'				
                                     ,A.REG_DATE				AS	 'A.REG_DATE'				
                                     ,A.UP_USER				    AS	 'A.UP_USER'				
                                     ,A.UP_DATE				    AS	 'A.UP_DATE'				
                                FROM [dbo].[OUT_STOCK_WAIT_DETAIL] A							  
                                INNER JOIN STOCK_MST B ON Ａ.STOCK_MST_ID = B.ID                           
                                INNER JOIN IN_STOCK_DETAIL C ON A.IN_STOCK_DETAIL_ID = C.ID
                                WHERE 1=1 
                                      AND A.OUT_STOCK_WAIT_MST_ID = {_pID}
                                      AND A.STOCK_MST_ID = {pSTOCK_MST_ID} 
                                      AND A.ORDER_DETAIL_ID = {pORDER_DETAIL};";

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
                    if (_pORDER_DETAIL_ID == fpMain.Sheets[0].GetValue(i, "ID").ToString())
                    {
                        maxqty = Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "A.ORDER_QTY"))
                               - Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "A.BOX_QTY"));

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
                    CustomMsg.ShowMessage("포장 수량은 수주수량을 넘을수 없습니다.");
                    return;
                }

                fpMain.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(_MenuSettingEntity, fpSub1, "OUT_STOCK_WAIT_DETAIL");

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
                        if (_pORDER_DETAIL_ID == fpMain.Sheets[0].GetValue(i, "ID").ToString())
                        {
                            maxqty = Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "A.ORDER_QTY"));
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
                    CustomMsg.ShowMessage("포장 수량은 수주수량을 넘을수 없습니다.");
                    return;
                }

                fpMain.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(_MenuSettingEntity, fpSub2, "OUT_STOCK_WAIT_DETAIL");

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
    }
}
