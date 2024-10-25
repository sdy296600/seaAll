using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 생산계획등록_단독 : BaseForm1
    {
        public 생산계획등록_단독()
        {
            InitializeComponent();
        }
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"select A.ID                         AS  'ID'                
                                      ,A.OUT_CODE                   AS  'A.OUT_CODE'           
                                      ,A.PLAN_DATE					AS  'PLAN_DATE'
									  ,A.PLAN_END_DATE              AS  'PLAN_END_DATE'
                                      ,A.STOCK_MST_ID               AS  'STOCK_MST_ID'       
                                      ,B.OUT_CODE                   AS  'B.OUT_CODE'   
                                      ,B.NAME                       AS  'B.NAME'    
                                      ,B.STANDARD                   AS  'B.STANDARD'           
                                      ,B.TYPE                       AS  'B.TYPE'                                                  
									  ,A.PLAN_QTY                   AS  'PLAN_QTY'
                                      ,A.INSTRUCT_QTY               AS  'INSTRUCT_QTY'
									  ,A.REMAIN_QTY                 AS  'REMAIN_QTY'
									  ,B.QTY						AS	'B.QTY'
									  ,B.IN_SCHEDULE				AS	'B.IN_SCHEDULE'
									  ,B.OUT_SCHEDULE				AS	'B.OUT_SCHEDULE'
                                      ,A.SORT                       AS  'SORT'               
                                      ,A.COMMENT                    AS  'COMMENT'       
                                      ,A.COMPLETE_YN                AS  'COMPLETE_YN'        
                                      ,A.USE_YN                     AS  'USE_YN'             
                                      ,A.REG_USER                   AS  'REG_USER'           
                                      ,A.REG_DATE                   AS  'REG_DATE'           
                                      ,A.UP_USER                    AS  'UP_USER'            
                                      ,A.UP_DATE                    AS  'UP_DATE'
                                 from [dbo].[PRODUCTION_PLAN] A
                                 INNER JOIN [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID                              
                                 WHERE 1=1
                                 AND A.USE_YN = 'Y'
								 AND A.ORDER_DETAIL_ID is null ";
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
    }
}
