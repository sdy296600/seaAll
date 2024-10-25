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
    public partial class SalesReport현황 : BaseForm1
    {
        public SalesReport현황()
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
                               A.ID               AS 'A.ORDER_DETAIL_ID'
                              ,C.ORDER_TYPE       AS 'C.ORDER_TYPE'
                              ,I.CODE_NAME        AS 'I.CODE_NAME'
                              ,C.ORDER_DATE       AS 'C.ORDER_DATE'
                              ,C.NAME             AS 'C.NAME'
                              ,C.OUT_CODE         AS 'C.OUT_CODE'
							　,B.NAME             AS 'B.NAME'
                              ,A.DEMAND_DATE      AS 'A.DEMAND_DATE'
                              ,A.STOCK_MST_ID     AS 'A.STOCK_MST_ID'
							  ,D.TYPE2            AS 'D.TYPE2'
							  ,D.OUT_CODE         AS 'D.OUT_CODE'
							  ,D.NAME             AS 'D.NAME'
							  ,D.STANDARD         AS 'D.STANDARD'
							  ,E.code_name        AS 'D.TYPE'
							  ,F.code_name        AS 'D.UNIT'
							  ,G.code_name		  AS 'A.CONVERSION_UNIT'
							  ,A.ORDER_QTY        AS 'A.ORDER_QTY'
							  ,A.ORDER_REMAIN_QTY AS 'A.ORDER_REMAIN_QTY'
							  ,A.STOCK_MST_PRICE  AS 'A.STOCK_MST_PRICE'
							  ,A.COST             AS 'A.COST'
							  ,A.COMMENT          AS 'A.COMMENT'
							  ,A.STOP_YN          AS 'A.STOP_YN'
							  ,H.code_name        AS 'A.OUT_TYPE'
							  ,A.SUPPLY_PRICE_KRW AS 'A.SUPPLY_PRICE_KRW'
							  ,A.SUPPLY_TYPE      AS 'A.SUPPLY_TYPE'
                              ,J.code_name	      AS 'J.CODE_NAME'
							  ,A.FOREIGN_CURRENY  AS 'A.FOREIGN_CURRENY'
							  ,A.PUBLISH_DATE     AS 'A.PUBLISH_DATE'
							  ,A.DEADLINE         AS 'A.DEADLINE'
							  ,A.COMPLETE_YN      AS 'A.COMPLETE_YN'
							  ,A.USE_YN           AS 'A.USE_YN'
                              ,A.REG_DATE         AS 'A.REG_DATE'
							  ,A.REG_USER         AS 'A.REG_USER'
							  ,A.UP_USER          AS 'A.UP_USER'
							  ,A.UP_DATE          AS 'A.UP_DATE'
                              ,A.EXPORT_ID        AS 'A.EXPORT_ID'
                              from [dbo].[ORDER_DETAIL] A
                              INNER JOIN [dbo].[COMPANY] B ON A.COMPANY_ID = B.ID
                              INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                              INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
							  INNER JOIN [dbo].[Code_Mst] E ON D.TYPE = E.code
							  and E.code_type = 'SD04'
							  INNER JOIN [dbo].[Code_Mst] F ON D.UNIT = F.code
							  and F.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] G ON A.CONVERSION_UNIT = G.code
							  and G.code_type = 'CD04'
							  INNER JOIN [dbo].[Code_Mst] H ON A.OUT_TYPE = H.code
							  and H.code_type = 'CD20'
							  INNER JOIN [dbo].[Code_Mst] I ON C.ORDER_TYPE = I.code
							  and I.code_type = 'SD09'
							  LEFT JOIN [dbo].[Code_Mst] J ON A.SUPPLY_TYPE = J.code
							  and J.code_type = 'SD28'
                              WHERE 1=1
                              AND A.USE_YN = 'Y'";

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
