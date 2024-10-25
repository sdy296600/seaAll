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
    public partial class 주조로트조회 : BaseForm1
    {

        public 주조로트조회()
        {
            MainFind_DisplayData();

            Load += new EventHandler(Form_Load);
        }

        private void Form_Load(object sender, EventArgs e)
        {
           // MergeColumnHeaders(fpMain.Sheets[0]);
        }

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);
                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT DISTINCT
      A.[RESOURCE_NO]
 
      ,A.[LOT_NO]
   
     , A.[BARCODE_DATE]
    
      ,A.[P_QTY]
      ,A.[BARCODE_NO] AS PRODUCT_BARCODE
      ,A.[COMMENT] AS PRODUCT_COMMENT
    
	  ,B.BARCODE_NO AS MATERIAL_BARCODE
	  ,B.WEIGHT AS MATERIAL_WEIGHT
	  
  FROM [HS_MES].[dbo].[PRODUCT_BARCODE] AS A
  INNER JOIN IN_BARCODE AS B

  ON A.RESOURCE_NO = B.RESOURCE_NO
  AND A.LOT_NO = B.LOT_NO
  AND B.TYPE ='검증'
                                WHERE 1=1 ";

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

                    fpMain.Sheets[0].SetColumnMerge(0, FarPoint.Win.Spread.Model.MergePolicy.Always);
                    fpMain.Sheets[0].SetColumnMerge(1, FarPoint.Win.Spread.Model.MergePolicy.Always);
                    fpMain.Sheets[0].SetColumnMerge(2, FarPoint.Win.Spread.Model.MergePolicy.Always);
                    fpMain.Sheets[0].SetColumnMerge(3, FarPoint.Win.Spread.Model.MergePolicy.Always);
                    fpMain.Sheets[0].SetColumnMerge(4, FarPoint.Win.Spread.Model.MergePolicy.Always);
                    fpMain.Sheets[0].SetColumnMerge(5, FarPoint.Win.Spread.Model.MergePolicy.Always);

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
