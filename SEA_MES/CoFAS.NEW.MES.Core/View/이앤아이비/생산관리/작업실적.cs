using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 작업실적 : DoubleBaseForm1
    {
        public 작업실적()
        {
            InitializeComponent();
            fpSub._EditorNotifyEventHandler -= fpSub_ButtonClicked;
            fpSub._EditorNotifyEventHandler += fpSub_ButtonClicked;
        }
    
        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            if (_Mst_Id != string.Empty)
            {
                Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);
                int row = 0;

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "ID").ToString() == _Mst_Id)
                    {
                        row = i;
                    }
                }

                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.PRODUCTION_INSTRUCT_ID".Trim(), _Mst_Id);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.STOCK_MST_ID".Trim(), fpMain.Sheets[0].GetValue(row, "A.STOCK_MST_ID ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "B.OUT_CODE    ".Trim(), fpMain.Sheets[0].GetValue(row, "B.OUT_CODE     ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "B.NAME        ".Trim(), fpMain.Sheets[0].GetValue(row, "B.NAME         ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "B.STANDARD    ".Trim(), fpMain.Sheets[0].GetValue(row, "B.STANDARD     ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "B.TYPE        ".Trim(), fpMain.Sheets[0].GetValue(row, "B.TYPE         ".Trim()));
          
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.QTY         ".Trim(), fpMain.Sheets[0].GetValue(row, " A.INSTRUCT_QTY".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.START_DATE   ".Trim(), null);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.END_DATE     ".Trim(), null);


            }


        }

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;


                string str = $@"SELECT 
                                A.ID                 AS ID 
                               ,A.PRODUCTION_PLAN_ID AS 'A.PRODUCTION_PLAN_ID'
                               ,A.INSTRUCT_DATE      AS 'A.INSTRUCT_DATE'     
                               ,A.STOCK_MST_ID       AS 'A.STOCK_MST_ID'      
                               ,B.OUT_CODE           AS 'B.OUT_CODE'          
                               ,B.NAME               AS 'B.NAME'                
                               ,B.STANDARD           AS 'B.STANDARD'          
                               ,B.TYPE               AS 'B.TYPE'              
                               ,A.PROCESS_ID         AS 'A.PROCESS_ID'        
                               ,A.INSTRUCT_QTY       AS 'A.INSTRUCT_QTY'      
                               ,A.SORT               AS 'A.SORT'              
                               ,A.COMMENT            AS 'A.COMMENT'           
                               ,A.COMPLETE_YN        AS 'A.COMPLETE_YN'       
                               ,A.USE_YN             AS 'A.USE_YN'           
                               ,A.REG_USER           AS 'A.REG_USER'          
                               ,A.REG_DATE           AS 'A.REG_DATE'         
                               ,A.UP_USER            AS 'A.UP_USER'           
                               ,A.UP_DATE            AS 'A.UP_DATE'           
                               ,B.QTY				   AS 'B.QTY'				  
                               ,B.IN_SCHEDULE		   AS 'B.IN_SCHEDULE'		  
                               ,B.OUT_SCHEDULE	   AS 'B.OUT_SCHEDULE'	  
                                FROM [dbo].[PRODUCTION_INSTRUCT] A
                                inner join STOCK_MST B on A.STOCK_MST_ID = B.id
                                WHERE 1=1
                                AND A.PROCESS_ID  != '6'";

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

        public override void SubFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;


                string sql = $@"SELECT A.ID						AS 'ID'						
                                      ,A.LOT_NO					AS 'A.LOT_NO'					
                                      ,A.PRODUCTION_INSTRUCT_ID	AS 'A.PRODUCTION_INSTRUCT_ID'
                                      ,A.STOCK_MST_ID			AS 'A.STOCK_MST_ID'			
                                	  ,B.OUT_CODE				AS 'B.OUT_CODE'				
                                	  ,B.NAME					AS 'B.NAME'					
                                	  ,B.STANDARD				AS 'B.STANDARD'	
	                                  ,B.TYPE			    	AS 'B.TYPE'	
                                      ,A.RESULT_TYPE			AS 'A.RESULT_TYPE'			
                                      ,A.QTY					AS 'A.QTY'					
                                      ,A.START_DATE				AS 'A.START_DATE'				
                                      ,A.END_DATE				AS 'A.END_DATE'				
                                      ,A.COMMENT				AS 'A.COMMENT'
                                      ,A.COMPLETE_YN			AS 'A.COMPLETE_YN'
                                      ,A.USE_YN					AS 'A.USE_YN'					
                                      ,A.REG_USER				AS 'A.REG_USER'				
                                      ,A.REG_DATE				AS 'A.REG_DATE'				
                                      ,A.UP_USER				AS 'A.UP_USER'				
                                      ,A.UP_DATE                AS 'A.UP_DATE'                                  	  
                                FROM [dbo].[PRODUCTION_RESULT] A
                                inner join STOCK_MST B on A.STOCK_MST_ID = B.id
                                WHERE 1=1 AND A.PRODUCTION_INSTRUCT_ID = {_Mst_Id}";


                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpSub);
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

        private void fpSub_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            try
            {
                xFpSpread pfpSpread = sender as xFpSpread;

                if (e.EditingControl == null)
                {
                    return;
                }

                if (e.EditingControl.Text == "소요량")
                {
                    if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0")
                    {
                        if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                        {
                            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                            {
                                string id = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();

                                if (id != "")
                                {
                                    string sql = $@"select COMPLETE_YN
                                                  from [dbo].[PRODUCTION_RESULT]
                                                 WHERE ID = {id} AND USE_YN = 'Y'AND COMPLETE_YN != 'Y'";

                                    DataTable _DataTable = new CoreBusiness().SELECT(sql);

                                    if (_DataTable != null && _DataTable.Rows.Count == 1)
                                    {
                                        작업실적_PopupBox basePopupBox = new 작업실적_PopupBox(
                                          id
                                        , pfpSpread._user_account
                                        , pfpSpread.Sheets[0].GetValue(e.Row, "A.STOCK_MST_ID").ToString());
                                        basePopupBox.ShowDialog();
                                        MainFind_DisplayData();
                                        SubFind_DisplayData();
                                    }
                                    else
                                    {
                                        CustomMsg.ShowMessage("완료된 실적입니다.");
                                        return;
                                    }
                                }
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
    }
}
