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
    public partial class 월별원가분석 : BaseForm1
    {

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"
SELECT 
     
      A.[RESOURCE_NO] AS 품번
     , C.[DESCRIPTION] AS 품명
     , YEAR(A.ORDER_DATE) AS [YEAR]
     , MONTH(A.ORDER_DATE) AS [MONTH]
     , A.[QTY_COMPLETE] 
     , A.[START_TIME] AS 생산일자
     , A.[SHIFT] AS [주/야 구분]
     , A.[IN_PER] AS 투입인원
     , A.[WORK_TIME] AS 작업시간
    
  FROM [HS_MES].[dbo].[WORK_PERFORMANCE] AS A

  INNER JOIN [sea_mfg].[dbo].[RESOURCE] AS C
ON A.RESOURCE_NO = C.RESOURCE_NO
where A.[QTY_COMPLETE] >= 0
ORDER BY [YEAR], [MONTH]";

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str /*+ sb.ToString()*/;

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
                            if (item.ColumnName == "작업시간")
                            {
                                //fpMain.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName]);
                                Double totalSeconds = Convert.ToDouble(_DataTable.Rows[i][item.ColumnName].ToString());
                                TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);

                                // TimeSpan을 사용하여 시간, 분, 초 형식으로 설정
                                fpMain.Sheets[0].SetValue(i, item.ColumnName, $"{timeSpan.Hours}시간 {timeSpan.Minutes}분 {timeSpan.Seconds}초");
                            }
                            else {
                                fpMain.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName]);
                            }


                        }


                    }

                    Function.Core._AddItemSUM(fpMain);

                    fpMain.Sheets[0].Visible = true;

                    fpMain.Sheets[0].SetColumnMerge(0, FarPoint.Win.Spread.Model.MergePolicy.Always);
                    fpMain.Sheets[0].SetColumnMerge(1, FarPoint.Win.Spread.Model.MergePolicy.Always);
                    fpMain.Sheets[0].SetColumnMerge(2, FarPoint.Win.Spread.Model.MergePolicy.Always);
                    fpMain.Sheets[0].SetColumnMerge(3, FarPoint.Win.Spread.Model.MergePolicy.Always);
                    fpMain.Sheets[0].SetColumnMerge(10, FarPoint.Win.Spread.Model.MergePolicy.Always);
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

    }
}
