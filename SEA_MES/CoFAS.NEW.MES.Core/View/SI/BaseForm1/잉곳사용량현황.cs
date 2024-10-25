using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 잉곳사용량현황 : BaseForm1
    {

        public 잉곳사용량현황()
        {
            MainFind_DisplayData();

            Load += new EventHandler(Form_Load);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MergeColumnHeaders(fpMain.Sheets[0]);
        }

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);
                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT  A.[ORDER_DATE] AS 'A.ORDER_DATE',
	                                    A.[SHIFT] AS 'A.SHIFT',
	                                    i.description AS 생산호기,
                                        A.[RESOURCE_NO] AS 'A.RESOURCE_NO',
                                        A.[LOT_NO] AS 'A.LOT_NO',
	                                    C.resource_used AS 투입자재,
                                        ISNULL(A.[QTY_COMPLETE]+ D.BAD_QTY,0) AS MES_작업,
		                                ISNULL(CONVERT( INT ,E.qty_worked ),0)+ISNULL(CONVERT( INT ,F.qty_bad ),0)+ISNULL(CONVERT( INT , G.qty_bad ),0) AS ERP_작업,
										(ISNULL(A.[QTY_COMPLETE]+ D.BAD_QTY,0)) - (ISNULL(CONVERT( INT ,E.qty_worked ),0)+ISNULL(CONVERT( INT ,F.qty_bad ),0)+ISNULL(CONVERT( INT , G.qty_bad ),0)) AS 작업차이,
		                                
		                                (CONVERT( INT,ISNULL(A.[QTY_COMPLETE],0))) AS 'A.MES_양품',
		                                CONVERT( INT ,ISNULL(E.qty_worked,0) ) AS ERP_양품,
		                                 CONVERT( INT ,ISNULL(A.[QTY_COMPLETE],0)-ISNULL(E.qty_worked,0)) AS 양품차이,
		                                ISNULL(D.BAD_QTY,0) AS MES_불량,
		                                CONVERT( INT ,ISNULL(F.qty_bad,0) ) AS ERP_불량,
		                                 CONVERT( INT ,ISNULL(D.BAD_QTY,0) - ISNULL(F.qty_bad,0) )AS 불량차이,
		                                CONVERT( INT , ISNULL(G.qty_bad,0) ) AS 육안불량판정,
		                                ISNULL(J.TOTAL_WEIGHT,0) AS MES_잉곳사용량,
		                                 FORMAT(ISNULL(J.TOTAL_WEIGHT,0),'0.00') AS MES_잉곳사용량,
		                                ISNULL(J.TOTAL_WEIGHT -(E.qty_worked *C.qty_per),0) AS ERP_잉곳사용차이,
		                                ISNULL(K.IN_WEIGHT,0) AS INGOT투입현황
                                FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A
                                INNER JOIN [sea_mfg].[dbo].[DEMAND_MSTR] AS B
                                ON  B.resource_no = A.RESOURCE_NO
                                AND B.lot = A.LOT_NO
								INNER JOIN [sea_mfg].[dbo].[cproduct_defn] AS C
								ON  A.resource_no = C.RESOURCE_NO
								and C.ENG_CHG_CODE ='A'
								INNER JOIN (SELECT RESOURCE_NO , LOT_NO , SUM(BAD_QTY) BAD_QTY  FROM [HS_MES].[dbo].[BAD_PERFORMANCE] GROUP BY RESOURCE_NO , LOT_NO ) AS D
								ON D.RESOURCE_NO = A.RESOURCE_NO
								AND D.LOT_NO = A.LOT_NO
								LEFT OUTER JOIN (SELECT order_no,lot,SUM(qty_worked) AS qty_worked FROM [sea_mfg].[dbo].[labor_hours]  WITH (NOLOCK)
								 GROUP BY order_no,lot)	 AS E
								ON E.order_no = A.RESOURCE_NO
								AND E.lot  = A.LOT_NO 
								LEFT OUTER JOIN (SELECT order_no,lot,SUM(qty_bad) qty_bad   FROM [sea_mfg].[dbo].[claim_inline_header]  WITH (NOLOCK) WHERE  error_no = 'BOSCHAAA24'	 GROUP BY order_no , lot) AS F 
								ON F.order_no = A.RESOURCE_NO
								AND F.lot = A.LOT_NO
								
								LEFT OUTER JOIN (SELECT order_no,lot,SUM(qty_bad) qty_bad   FROM [sea_mfg].[dbo].[claim_inline_header]  WITH (NOLOCK) WHERE  error_no = 'BOSCHAAA04'	 GROUP BY order_no , lot) AS G 
								ON G.order_no = A.RESOURCE_NO
								AND G.lot = A.LOT_NO
							
								INNER JOIN [sea_mfg].[dbo].[schedrtg] AS H WITH (NOLOCK)
								ON H.order_no = A.RESOURCE_NO
								AND H.lot = A.LOT_NO
								INNER JOIN [sea_mfg].[dbo].[resource] AS I WITH (NOLOCK)
								ON I.RESOURCE_NO = H.workcenter
								LEFT OUTER JOIN 
								(SELECT RESOURCE_NO , LOT_NO ,SUM(WEIGHT) AS TOTAL_WEIGHT FROM [HS_MES].[dbo].[IN_BARCODE] WITH (NOLOCK)
								WHERE TYPE = '생산'
								GROUP BY RESOURCE_NO,LOT_NO) AS J
								ON J.RESOURCE_NO = A.RESOURCE_NO
								AND J.LOT_NO = A.LOT_NO
								LEFT OUTER JOIN 
								(SELECT RESOURCE_NO , LOT_NO ,SUM(WEIGHT) AS IN_WEIGHT FROM [HS_MES].[dbo].[IN_BARCODE] WITH (NOLOCK)
								WHERE TYPE = '검증'
								GROUP BY RESOURCE_NO,LOT_NO) AS K
								ON K.RESOURCE_NO = A.RESOURCE_NO
								AND K.LOT_NO = A.LOT_NO
                                WHERE 1=1
	                            AND A.QTY_COMPLETE  <>0";

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString();

                DataTable _DataTable = new CoreBusiness().SELECT(sql);
                fpMain.Sheets[0].Rows.Count = 0;
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpMain.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName]);

                           
                        }


                    }

                    Function.Core._AddItemSUM(fpMain);

                    fpMain.Sheets[0].Visible = true;


                }
                else
                {
                    fpMain.Sheets[0].Rows.Count = 0;
                    CustomMsg.ShowMessage("조회 내역이 없습니다.");
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

        private void MergeColumnHeaders(SheetView sheet)
        {
            // 컬럼 헤더 행 수 설정
            sheet.ColumnHeader.RowCount = 3;

            // 컬럼 헤더 텍스트 설정
            sheet.ColumnHeader.Cells[0, 6].Text = "작업수(양+불)";
            sheet.ColumnHeader.Cells[0, 7].Text = "작업수(양+불)";
            sheet.ColumnHeader.Cells[0, 8].Text = "작업수(양+불)";

            sheet.ColumnHeader.Cells[0, 9].Text = "양품수";
            sheet.ColumnHeader.Cells[0, 10].Text = "양품수";
            sheet.ColumnHeader.Cells[0, 11].Text = "양품수";

            sheet.ColumnHeader.Cells[0, 12].Text = "불량수";
            sheet.ColumnHeader.Cells[0, 13].Text = "불량수";
            sheet.ColumnHeader.Cells[0, 14].Text = "불량수";
            sheet.ColumnHeader.Cells[0, 15].Text = "불량수";

            sheet.ColumnHeader.Cells[0, 16].Text = "잉곳 총 사용량(kg)";
            sheet.ColumnHeader.Cells[0, 17].Text = "잉곳 총 사용량(kg)";
            sheet.ColumnHeader.Cells[0, 18].Text = "잉곳 총 사용량(kg)";

            sheet.ColumnHeader.Cells[1, 12].Text = "예열타";
            sheet.ColumnHeader.Cells[1, 13].Text = "예열타";
            sheet.ColumnHeader.Cells[1, 14].Text = "예열타";

            sheet.ColumnHeader.Cells[1, 15].Text = "육안불량판정분";

            // 컬럼 헤더 병합
            sheet.ColumnHeader.Cells[0, 0].RowSpan = 3;
            sheet.ColumnHeader.Cells[0, 1].RowSpan = 3;
            sheet.ColumnHeader.Cells[0, 2].RowSpan = 3;
            sheet.ColumnHeader.Cells[0, 3].RowSpan = 3;
            sheet.ColumnHeader.Cells[0, 4].RowSpan = 3;
            sheet.ColumnHeader.Cells[0, 5].RowSpan = 3;

            sheet.ColumnHeader.Cells[0, 6].ColumnSpan = 3;

            sheet.ColumnHeader.Cells[0, 6].RowSpan = 2;

            sheet.ColumnHeader.Cells[0, 9].ColumnSpan = 3;

            sheet.ColumnHeader.Cells[0, 9].RowSpan = 2;

            sheet.ColumnHeader.Cells[0, 12].ColumnSpan = 4;

            sheet.ColumnHeader.Cells[0, 16].ColumnSpan = 3;

            sheet.ColumnHeader.Cells[0, 16].RowSpan = 2;

            sheet.ColumnHeader.Cells[0, 19].RowSpan = 3;

            sheet.ColumnHeader.Cells[1, 15].RowSpan = 2;

            sheet.ColumnHeader.Cells[1, 12].ColumnSpan = 3;

            // 개별 컬럼 헤더 텍스트 설정

            sheet.ColumnHeader.Cells[2, 6].Text = "MES";
            sheet.ColumnHeader.Cells[2, 7].Text = "ERP";
            sheet.ColumnHeader.Cells[2, 8].Text = "△t";

            sheet.ColumnHeader.Cells[2, 9].Text = "MES";
            sheet.ColumnHeader.Cells[2, 10].Text = "ERP";
            sheet.ColumnHeader.Cells[2, 11].Text = "△t";

            sheet.ColumnHeader.Cells[2, 12].Text = "MES";
            sheet.ColumnHeader.Cells[2, 13].Text = "ERP";
            sheet.ColumnHeader.Cells[2, 14].Text = "△t";

            sheet.ColumnHeader.Cells[2, 16].Text = "MES";
            sheet.ColumnHeader.Cells[2, 17].Text = "ERP";
            sheet.ColumnHeader.Cells[2, 18].Text = "△t";
        }
    }
}
