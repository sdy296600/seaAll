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
    public partial class 자재발주현황 : BaseForm1
    {
        public 자재발주현황()
        {
            InitializeComponent();
        }
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"select 
                               A.REG_DATE         AS 'A.REG_DATE'
                              ,B.NAME             AS 'B.NAME'
                              ,C.OUT_CODE         AS 'C.OUT_CODE'
                              ,A.DEMAND_DATE      AS 'A.DEMAND_DATE'
                              ,D.OUT_CODE         AS 'D.OUT_CODE'
                              ,D.NAME             AS 'D.NAME'
                              ,D.STANDARD         AS 'D.STANDARD'
                              ,A.ORDER_QTY        AS 'A.ORDER_QTY'
                              ,A.ORDER_REMAIN_QTY AS 'A.ORDER_REMAIN_QTY'
                              ,D.QTY			  AS 'D.QTY'
                              ,D.OUT_SCHEDULE	  AS 'D.OUT_SCHEDULE'
                              ,D.IN_SCHEDULE	  AS 'D.IN_SCHEDULE'
                              from [dbo].[ORDER_DETAIL] A
                              INNER JOIN [dbo].[COMPANY] B ON A.COMPANY_ID = B.ID
                              INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                              INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
                              WHERE 1=1
                              AND A.USE_YN = 'Y'
                              AND D.TYPE LIKE '%SD03%'";

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
