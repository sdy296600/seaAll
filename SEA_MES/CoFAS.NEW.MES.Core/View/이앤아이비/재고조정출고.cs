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
    public partial class 재고조정출고 : DoubleBaseForm1
    {
        public 재고조정출고()
        {
            InitializeComponent();
  
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

                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.IN_STOCK_DETAIL_ID".Trim(), _Mst_Id);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_ID  ".Trim(), fpMain.Sheets[0].GetValue(row, "STOCK_MST_ID".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "B.OUT_CODE    ".Trim(), fpMain.Sheets[0].GetValue(row, "B.OUT_CODE     ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "B.NAME        ".Trim(), fpMain.Sheets[0].GetValue(row, "B.NAME         ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "B.STANDARD    ".Trim(), fpMain.Sheets[0].GetValue(row, "B.STANDARD     ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "B.TYPE        ".Trim(), fpMain.Sheets[0].GetValue(row, "B.TYPE         ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.OUT_CODE    ".Trim(), fpMain.Sheets[0].GetValue(row, "A.OUT_CODE     ".Trim()));
                //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.START_DATE   ".Trim(), null);
                //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.END_DATE     ".Trim(), null);


            }


        }

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;


                string str = $@"SELECT A.ID					   AS 	'ID'				 
                                      ,A.OUT_CODE			   AS 	'A.OUT_CODE'			 
                                      ,A.IN_STOCK_DATE		   AS 	'A.IN_STOCK_DATE'		 
                                      ,A.IN_TYPE			   AS 	'A.IN_TYPE'			                       
                                      ,A.ORDER_DETAIL_ID	   AS 	'A.ORDER_DETAIL_ID'	 
                                      ,A.PRODUCTION_RESULT_ID  AS 	'A.PRODUCTION_RESULT_ID'
                                      ,A.STOCK_MST_ID		   AS 	'STOCK_MST_ID'
                                      ,B.NAME	          	   AS 	'B.NAME'	
                                      ,B.OUT_CODE			   AS 	'B.OUT_CODE'			 
                                      ,B.STANDARD			   AS 	'B.STANDARD'			 
                                      ,B.TYPE				   AS 	'B.TYPE'				 
                                      ,A.IN_QTY				   AS 	'A.IN_QTY'				 
                                      ,A.USED_QTY			   AS 	'A.USED_QTY'			 
                                      ,A.REMAIN_QTY			   AS 	'A.REMAIN_QTY'			 
                                      ,A.COMMENT			   AS 	'A.COMMENT'			 
                                      ,A.COMPLETE_YN		   AS 	'A.COMPLETE_YN'		 
                                      ,A.USE_YN				   AS 	'A.USE_YN'				                            
                                      ,A.REG_USER			   AS 	'A.REG_USER'			 
                                      ,A.REG_DATE			   AS 	'A.REG_DATE'			 
                                      ,A.UP_USER			   AS 	'A.UP_USER'			 
                                      ,A.UP_DATE			   AS 	'A.UP_DATE'			 
                                FROM [dbo].[IN_STOCK_DETAIL] A
                                INNER JOIN [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID
                                WHERE 1=1 ";

                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString()
                  + this._pMenuSettingEntity.BASE_WHERE
                  + this._pMenuSettingEntity.BASE_ORDER;

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


                string str = $@"SELECT A.ID					   AS 	'ID'				 
                                      ,A.OUT_CODE			   AS 	'A.OUT_CODE'			 
                                      ,A.OUT_STOCK_DATE		   AS 	'A.OUT_STOCK_DATE'		 
                                      ,A.OUT_TYPE			   AS 	'A.OUT_TYPE'			                       
                                      ,A.ORDER_DETAIL_ID	   AS 	'A.ORDER_DETAIL_ID'	 
                                      ,A.PRODUCTION_RESULT_ID  AS 	'A.PRODUCTION_RESULT_ID'
                                      ,A.STOCK_MST_ID		   AS 	'STOCK_MST_ID'
                                      ,B.NAME	          	   AS 	'B.NAME'	
                                      ,B.OUT_CODE			   AS 	'B.OUT_CODE'			 
                                      ,B.STANDARD			   AS 	'B.STANDARD'			 
                                      ,B.TYPE				   AS 	'B.TYPE'		 
                                      ,A.OUT_QTY			   AS 	'A.OUT_QTY'				 		 
                                      ,A.COMMENT			   AS 	'A.COMMENT'			 
                                      ,A.COMPLETE_YN		   AS 	'A.COMPLETE_YN'		 
                                      ,A.USE_YN				   AS 	'A.USE_YN'				 
                                      ,A.REG_USER			   AS 	'A.REG_USER'			 
                                      ,A.REG_DATE			   AS 	'A.REG_DATE'			 
                                      ,A.UP_USER			   AS 	'A.UP_USER'			 
                                      ,A.UP_DATE			   AS 	'A.UP_DATE'			 
                                FROM [dbo].[OUT_STOCK_DETAIL] A
                                INNER JOIN [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID
                                WHERE 1=1 
                                  
                                  AND A.IN_STOCK_DETAIL_ID = {_Mst_Id} ";


                DataTable _DataTable = new CoreBusiness().SELECT(str);

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

  
    }
}
