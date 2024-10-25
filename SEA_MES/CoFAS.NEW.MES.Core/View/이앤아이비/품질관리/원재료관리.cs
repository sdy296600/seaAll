using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.Spreadsheet;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 원재료관리 : DoubleBaseForm1
    {
        public 원재료관리()
        {
            InitializeComponent();   
        }
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT 
	                                   A.ID						AS 'ID'						
                                      ,A.ORDER_DETAIL_ID		AS 'A.ORDER_DETAIL_ID'		
                                      ,A.PRODUCTION_INSTRUCT_ID	AS 'A.PRODUCTION_INSTRUCT_ID'	
									  ,C.COMPANY_ID             AS 'C.COMPANY_ID'
                                      ,D.NAME                   AS 'D.NAME'
                                      ,A.IN_TYPE				AS 'A.IN_TYPE'				
                                      ,A.IN_DATE				AS 'A.IN_DATE'				
                                      ,A.OUT_CODE				AS 'A.OUT_CODE'				
                                      ,A.STOCK_MST_ID			AS 'A.STOCK_MST_ID'			
                                      ,A.IN_QTY					AS 'A.IN_QTY'					
                                      ,ISNULL(A.COMPLETE_QTY,0) AS 'A.COMPLETE_QTY'			
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
                                      ,A.INSPECTION_YN		    AS 'A.INSPECTION_YN'	
                                      FROM IN_STOCK_WAIT_DETAIL A
                                      INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID
                                      INNER JOIN ORDER_DETAIL C ON A.ORDER_DETAIL_ID = C.ID
                                      INNER JOIN COMPANY D ON C.COMPANY_ID = D.ID
                                      WHERE 1=1         
                                      AND A.USE_YN = 'Y'
                                      AND A.IN_TYPE = 'SD13003' 
                                      AND B.INSPECTION_YN = 'Y'";

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

        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {

            if (_Mst_Id == string.Empty)
            {
             
            }
            else
            {

                int row = 0;

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "ID").ToString() == _Mst_Id)
                    {
                        row = i;
                    }
                }

                if (fpMain.Sheets[0].GetValue(row, "A.INSPECTION_YN").ToString() != "Y")
                {
                    Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);

                    string mst = this._pMenuSettingEntity.BASE_TABLE.Split('/')[0] + "_ID";
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "IN_STOCK_WAIT_DETAIL_ID", _Mst_Id);
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_ID".Trim(), fpMain.Sheets[0].GetValue(row, "A.STOCK_MST_ID").ToString());
                    //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.MAKER".Trim(), fpMain.Sheets[0].GetValue(row, "C.COMPANY_ID").ToString());
                    //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "C.NAME".Trim(), fpMain.Sheets[0].GetValue(row, "D.NAME").ToString());
                    //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "B.NAME".Trim(), fpMain.Sheets[0].GetValue(row, "B.NAME").ToString());
                    //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.LOT_NO".Trim(), fpMain.Sheets[0].GetValue(row, "A.OUT_CODE").ToString());

                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "QTY".Trim(), fpMain.Sheets[0].GetValue(row, "A.IN_QTY").ToString());
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "OK_YN".Trim(), "SD26002");
                    //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "REG_USER ".Trim(), MainForm.UserEntity.user_account);
                    //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "REG_DATE ".Trim(), DateTime.Now);
                    //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "UP_USER  ".Trim(), MainForm.UserEntity.user_account);
                    //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "UP_DATE  ".Trim(), DateTime.Now);
                }
                else
                {
                    CustomMsg.ShowMessage("검사가 완료된 항목입니다.");
                    return;
                }
            }
        }
    }
}
