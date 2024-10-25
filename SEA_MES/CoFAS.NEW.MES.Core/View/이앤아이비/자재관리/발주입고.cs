using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.Spreadsheet;
using FarPoint.Win.Spread.CellType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 발주입고 : DoubleBaseForm1
    {
        public 발주입고()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);


        }

        private void DoubleBaseForm2_Load(object sender, EventArgs e)
        {

        }
        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            //NewMethod();
            return;
            //if (_Mst_Id == string.Empty)
            //{
            //    Function.Core._AddItemButtonClicked(fpMain, MainForm.UserEntity.user_account);
            //}
            //else
            //{
            //    Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);
            //    int row = 0;

            //    for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
            //    {
            //        if (fpMain.Sheets[0].GetValue(i, "ID").ToString() == _Mst_Id)
            //        {
            //            row = i;
            //        }
            //    }
            //    string mst=  this._pMenuSettingEntity.BASE_TABLE.Split('/')[0] +"_ID";
            //    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, mst, _Mst_Id);
            //    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "DEMAND_COMPANY".Trim(), fpMain.Sheets[0].GetValue(row, "ORDER_COMPANY".Trim()));
            //}


        }

        public override void SubFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;

                string sql = $@" SELECT 
                                      'false'                                 AS  'CK'          
                                      ,A.ID				                      AS  'ORDER_DETAIL_ID'
                                      ,A.DEMAND_DATE	                      AS  'DEMAND_DATE'
									  ,A.ORDER_MST_ID                         AS  'ORDER_MST_ID'
									  ,A.STOCK_MST_ID                         AS  'A.STOCK_MST_ID'
                                      ,B.NAME	                              AS  'B.NAME'	 
                                      ,B.OUT_CODE	                          AS  'B.OUT_CODE'
                                      ,B.STANDARD		                      AS  'B.STANDARD'
                                      ,B.TYPE	                              AS  'B.TYPE'	 
									  ,A.SUPPLY_TYPE                          AS  'SUPPLY_TYPE'
									  ,B.UNIT                                 AS  'B.UNIT'
									  ,A.STOCK_MST_PRICE                      AS  'STOCK_MST_PRICE'
									  ,A.ORDER_QTY			                  AS  'ORDER_QTY'
									  ,A.ORDER_REMAIN_QTY                     AS  'IN_QTY'
									  ,A.COST				                  AS  'COST'
									  ,A.COMPANY_ID                           AS  'COMPANY_ID'
                                      ,A.COMMENT			                  AS  'COMMENT'
									  ,A.INSPECTION_YN                        AS  'INSPECTION_YN'
                                      ,A.COMPLETE_YN		                  AS  'COMPLETE_YN'
                                      ,A.USE_YN				                  AS  'USE_YN'
                                        ,'{MainForm.UserEntity.user_account}' AS  'REG_USER'
                                      ,GETDATE()			                  AS  'REG_DATE'
                                      ,'{MainForm.UserEntity.user_account}'   AS  'UP_USER'
                                      ,GETDATE()			                  AS  'UP_DATE'
                                      ,'SD13003'        	                  AS  'IN_TYPE'
                                      ,GETDATE()			                  AS  'IN_DATE'
                                      FROM [dbo].[ORDER_DETAIL] A
                                      INNER JOIN [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID     
									  LEFT JOIN
                                      (
                                      SELECT 
                                      SUM(IN_QTY) AS IN_QTY
                                      ,ORDER_DETAIL_ID
                                      FROM IN_STOCK_WAIT_DETAIL A
                                      WHERE USE_YN = 'Y'
                                      GROUP BY ORDER_DETAIL_ID
                                      ) C ON A.ID = C.ORDER_DETAIL_ID
									  WHERE 1=1
                                      AND A.USE_YN = 'Y'
							          AND A.ORDER_MST_ID = {_Mst_Id}";

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpSub);

                for (int x = 0; x < fpSub.Sheets[0].Rows.Count; x++)
                {
                    if (fpSub.Sheets[0].RowHeader.Cells[x, 0].Text != "합계" &&
                        fpSub.Sheets[0].GetValue(x, "COMPLETE_YN").ToString() != "Y")
                    {
                        fpSub.Sheets[0].RowHeader.Cells[x, 0].Text = "입력";

                    }
                    decimal qty  = Convert.ToDecimal(fpSub.Sheets[0].GetValue(x, "IN_QTY"));

                    if (fpSub.Sheets[0].RowHeader.Cells[x, 0].Text != "합계" && qty > 0)
                    {
                        for (int a = 0; a < fpSub.Sheets[0].ColumnCount; a++)
                        {
                            if (fpSub.Sheets[0].Columns[a].CellType.GetType() != typeof(ButtonCellType) &&
                                fpSub.Sheets[0].Columns[a].CellType.GetType() != typeof(FileButtonCellType))
                            {
                                fpSub.Sheets[0].Cells[x, a].Locked = false;
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
