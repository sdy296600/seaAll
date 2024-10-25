using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 자재입고대기 : BaseForm1
    {
        public 자재입고대기()
        {
            InitializeComponent();
        }
        public override void MainFind_DisplayData()
        {
            try
            {
                fpMain._EditorNotifyEventHandler -= new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                fpMain._EditorNotifyEventHandler += new EditorNotifyEventHandler(pfpSpread_ButtonClicked);

                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT 
	                                   A.ID						    AS 'ID'						
                                      ,A.ORDER_DETAIL_ID		    AS 'A.ORDER_DETAIL_ID'		
                                      ,A.PRODUCTION_INSTRUCT_ID	    AS 'A.PRODUCTION_INSTRUCT_ID'	
                                      ,A.IN_TYPE				    AS 'A.IN_TYPE'				
                                      ,A.IN_DATE				    AS 'A.IN_DATE'				
                                      ,A.OUT_CODE				    AS 'A.OUT_CODE'				
                                      ,A.STOCK_MST_ID			    AS 'A.STOCK_MST_ID'			
                                      ,A.IN_QTY					    AS 'A.IN_QTY'					
                                      ,ISNULL(A.COMPLETE_QTY,0)	    AS 'A.COMPLETE_QTY'			
                                      ,A.COMMENT				    AS 'A.COMMENT'				
                                      ,A.COMPLETE_YN			    AS 'A.COMPLETE_YN'			
                                      ,A.USE_YN					    AS 'A.USE_YN'					
                                      ,A.REG_USER				    AS 'A.REG_USER'				
                                      ,A.REG_DATE				    AS 'A.REG_DATE'				
                                      ,A.UP_USER				    AS 'A.UP_USER'				
                                      ,A.UP_DATE				    AS 'A.UP_DATE'				
	                                  ,B.OUT_CODE				    AS 'B.OUT_CODE'				
	                                  ,B.NAME					    AS 'B.NAME'					
	                                  ,B.STANDARD				    AS 'B.STANDARD'				
	                                  ,B.TYPE					    AS 'B.TYPE'	
                                      ,A.INSPECTION_YN		        AS 'A.INSPECTION_YN'	
                                      FROM IN_STOCK_WAIT_DETAIL A
                                      INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID
                                      WHERE 1=1         
                                      AND A.USE_YN = 'Y'
                                      AND A.IN_TYPE = 'SD13003'
                                      AND A.INSPECTION_YN = 'Y'
                                      AND B.TYPE like '%SD03%'";

                //WHERE IN_TYPE = 'SD13001';
                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString()
                    + this._pMenuSettingEntity.BASE_WHERE
                    + this._pMenuSettingEntity.BASE_ORDER;
                   // +" ORDER BY B.OUT_CODE,A.REG_DATE";

                DataTable _DataTable = new CoreBusiness().SELECT(sql);
                Function.Core.DisplayData_Set(_DataTable, fpMain);

                for (int x = 0; x < fpMain.Sheets[0].Rows.Count; x++)
                {
                    if (fpMain.Sheets[0].RowHeader.Cells[x, 0].Text != "합계" &&
                        fpMain.Sheets[0].GetValue(x, "A.COMPLETE_YN").ToString() != "Y")
                    {
                        for (int a = 0; a < fpMain.Sheets[0].ColumnCount; a++)
                        {
                            if (fpMain.Sheets[0].Columns[a].CellType.GetType() != typeof(ButtonCellType) &&
                                fpMain.Sheets[0].Columns[a].CellType.GetType() != typeof(FileButtonCellType))
                            {
                                fpMain.Sheets[0].Cells[x, a].Locked = false;
                            }

                        }

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
        private  void pfpSpread_ButtonClicked(object obj, EditorNotifyEventArgs e)
        {
            try
            {
                xFpSpread pfpSpread = obj as xFpSpread;

                if (e.EditingControl == null)
                {
                    return;
                }

                if (e.EditingControl.Text == "입고확정")
                {
                    if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0")
                    {
                        if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                        {
                            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                            {

                                if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "입고확정")
                                {
                                    if (CustomMsg.ShowExceptionMessage("입고대기를 확정하시겠습니까?", "입고대기확정", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    {

                                        string str = $@"SELECT 
	                                   A.ID						    AS 'ID'						
                                      ,A.IN_STOCK_WAIT_DETAIL_ID	AS 'A.IN_STOCK_WAIT_DETAIL_ID'		
                                      ,A.ORDER_DETAIL_ID	        AS 'A.ORDER_DETAIL_ID'	
                                      ,A.PRODUCTION_RESULT_ID	    AS 'A.PRODUCTION_RESULT_ID'		
                                      ,A.OUT_CODE				    AS 'A.OUT_CODE'		
                                      ,A.IN_STOCK_DATE				AS 'A.IN_STOCK_DATE'
                                      ,A.IN_TYPE				    AS 'A.IN_TYPE'
                                      ,A.STOCK_MST_ID			    AS 'A.STOCK_MST_ID'			
                                      ,A.IN_QTY					    AS 'A.IN_QTY'
                                      ,A.USED_QTY					AS 'A.USED_QTY'
                                      ,A.REMAIN_QTY					AS 'A.REMAIN_QTY'		
                                      ,A.COMMENT				    AS 'A.COMMENT'				
                                      ,A.COMPLETE_YN			    AS 'A.COMPLETE_YN'			
                                      ,A.USE_YN					    AS 'A.USE_YN'					
                                      ,A.REG_USER				    AS 'A.REG_USER'				
                                      ,A.REG_DATE				    AS 'A.REG_DATE'				
                                      ,A.UP_USER				    AS 'A.UP_USER'				
                                      ,A.UP_DATE				    AS 'A.UP_DATE'				
                                      FROM IN_STOCK_DETAIL A
                                      INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID
                                      WHERE 1=1         
                                      AND A.USE_YN = 'Y'
                                      AND A.IN_TYPE = 'SD13003'
                                      AND A.IN_STOCK_WAIT_DETAIL_ID = {pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()}";

                                        DataTable _DataTable = new CoreBusiness().SELECT(str);

                                        if (_DataTable.Rows.Count > 0)
                                        {
                                            CustomMsg.ShowMessage("이미 입고처리된 제품입니다.");
                                            return;
                                        }

                                        else
                                        {
                                            bool _Error = new SI_Business().IN_STOCK_WAIT_Y_A10(pfpSpread, pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString());
                                            if (_Error == false)
                                            {
                                                CustomMsg.ShowMessage("저장되었습니다.");
                                                MainFind_DisplayData();
                                            }
                                        }
                                    }
                                }
                               
                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {

            }
        }

        public override void _SaveButtonClicked(object sender, EventArgs e)
        {
            int check_count = 0;
            for(int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
            {
                if(fpMain.Sheets[0].GetValue(i,"CK").ToString() == "True" && fpMain.Sheets[0].GetValue(i, "A.COMPLETE_YN").ToString() != "Y")
                {
                    check_count++;
                    bool _Error = new SI_Business().IN_STOCK_WAIT_Y_A10(fpMain, fpMain.Sheets[0].GetValue(i, "ID").ToString());
                    if (_Error == false)
                    {

                    }
                }
            }

            if(check_count == 0)
            {
                CustomMsg.ShowMessage("선택한 자재가 없습니다.");
                return;
            }

            CustomMsg.ShowMessage("저장되었습니다.");
            MainFind_DisplayData();
        }
    }
}
