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
    public partial class 작업지시등록 : BaseForm1
    {
        public 작업지시등록()
        {
            InitializeComponent();

            fpMain._EditorNotifyEventHandler -= fpMain_ButtonClicked;
            fpMain._EditorNotifyEventHandler += fpMain_ButtonClicked;
        }
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT 
                                A.ID                            AS ID 
                               ,A.PRODUCTION_PLAN_ID            AS 'A.PRODUCTION_PLAN_ID'
                               ,A.INSTRUCT_DATE                 AS 'A.INSTRUCT_DATE'     
                               ,A.EQUIPMENT_ID                  AS 'A.EQUIPMENT_ID'     
                               ,A.STOCK_MST_ID                  AS 'A.STOCK_MST_ID'      
                               ,B.OUT_CODE                      AS 'B.OUT_CODE'          
                               ,B.NAME                          AS 'B.NAME'                
                               ,B.STANDARD                      AS 'B.STANDARD'          
                               ,B.TYPE                          AS 'B.TYPE'              
                               ,A.PROCESS_ID                    AS 'A.PROCESS_ID'        
                               ,A.INSTRUCT_QTY                  AS 'A.INSTRUCT_QTY' 
                               ,ISNULL(A.DEMAND_DATE,GETDATE()) AS 'A.DEMAND_DATE'
                               ,A.MATERIAL                      AS 'A.MATERIAL'
                               ,A.COMPANY_ID                    AS 'A.COMPANY_ID'
                               ,C.NAME                          AS 'C.NAME'
                               ,A.SORT                          AS 'A.SORT'              
                               ,A.COMMENT                       AS 'A.COMMENT'           
                               ,A.COMPLETE_YN                   AS 'A.COMPLETE_YN'       
                               ,A.USE_YN                        AS 'A.USE_YN'           
                               ,A.REG_USER                      AS 'A.REG_USER'          
                               ,A.REG_DATE                      AS 'A.REG_DATE'         
                               ,A.UP_USER                       AS 'A.UP_USER'           
                               ,A.UP_DATE                       AS 'A.UP_DATE'           
                               ,B.QTY				            AS 'B.QTY'				  
                               ,B.IN_SCHEDULE		            AS 'B.IN_SCHEDULE'		  
                               ,B.OUT_SCHEDULE	                AS 'B.OUT_SCHEDULE'	  
                                FROM [dbo].[PRODUCTION_INSTRUCT] A
                                INNER JOIN STOCK_MST B on A.STOCK_MST_ID = B.id
                                 LEFT JOIN COMPANY C ON A.COMPANY_ID = C.ID
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

        private void fpMain_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            try
            {
                xFpSpread pfpSpread = sender as xFpSpread;

                if (e.EditingControl == null)
                {
                    return;
                }

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
