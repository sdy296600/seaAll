using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 제품출고현황 : BaseForm1
    {
        public 제품출고현황()
        {
            InitializeComponent();
        }
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT A.ID					   AS 	'A.ID'				 
                                      ,A.OUT_CODE			   AS 	'A.OUT_CODE'			 
                                      ,A.OUT_STOCK_DATE		   AS 	'A.OUT_STOCK_DATE'		 
                                      ,A.OUT_TYPE			   AS 	'A.OUT_TYPE'			                       
                                      ,A.ORDER_DETAIL_ID	   AS 	'A.ORDER_DETAIL_ID'	 
                                      ,A.PRODUCTION_RESULT_ID  AS 	'A.PRODUCTION_RESULT_ID'
                                      ,A.STOCK_MST_ID		   AS 	'A.STOCK_MST_ID'
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
                                  AND B.TYPE like '%SD04%'";
                               
                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString()
                    + this._pMenuSettingEntity.BASE_WHERE
                    + this._pMenuSettingEntity.BASE_ORDER;
                   // +" ORDER BY B.OUT_CODE,A.REG_DATE";

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



    }
}
