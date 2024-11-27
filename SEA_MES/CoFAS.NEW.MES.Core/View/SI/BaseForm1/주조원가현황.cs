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
    public partial class 주조원가현황 : BaseForm1
    {

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@" 
SET ANSI_WARNINGS OFF;
 SET ARITHIGNORE ON;
 SET ARITHABORT OFF ;



SELECT A.CASTING_YEAR , A.CASTING_TYPE ,A.[1] , A.[2], A.[3], A.[4], A.[5], A.[6], A.[7], A.[8], A.[9],A.[10], A.[11], A.[12]
FROM
(SELECT *
FROM (
    SELECT CASTING_YEAR,CASTING_TYPE, CASTING_MONTH, CASTING_VALUE
    FROM [HS_MES].[dbo].[CASTING_SEARCH]
) AS A
PIVOT (
    SUM(CASTING_VALUE  )
    FOR CASTING_MONTH IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12])
) AS PivotTable

UNION ALL 

SELECT CASTING_YEAR,CASTING_TYPE, CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[1]) AS [1] ,
CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[2]) AS [2],  -- 직접노무비 : 초당시간임율 * 사이클타임(CT) * 투입인원 * NET
CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[3]) AS [3],
CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[4])AS[4],
CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[5])AS[5],
CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[6])AS[6],
CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[7])AS[7],
CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[8])AS[8],
CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[9])AS[9],
CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[10])AS[10],
CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[11])AS[11],
CONVERT(decimal(18,2), CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[12])AS[12]
FROM (               
    SELECT CASTING_YEAR,'직접노무비' AS CASTING_TYPE, CASTING_MONTH, CASTING_VALUE
    FROM [HS_MES].[dbo].[CASTING_SEARCH] WHERE CASTING_TYPE ='NET'
) AS B
PIVOT (
    SUM(CASTING_VALUE)
    FOR CASTING_MONTH IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12])
) AS PivotTable
INNER JOIN MATERIALCOST_PROCESS AS C
ON C.PROCESS_ID ='주조'

UNION ALL 

SELECT CASTING_YEAR,CASTING_TYPE, 
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[1])* CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100) AS [1] ,
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[2])* CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100) AS [2],  -- 직접노무비 : 초당시간임율 * 사이클타임(CT) * 투입인원 * NET
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[3])* CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100) AS [3],  --  직접노무비 *간접노무비
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[4])* CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100)AS[4],
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[5])* CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100)AS[5],
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[6])* CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100)AS[6],
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[7])* CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100)AS[7],
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[8])* CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100)AS[8],
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[9])* CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100)AS[9],
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[10])*CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100)AS[10],
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[11])*CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100)AS[11],
CONVERT(decimal(18,2), (CONVERT(decimal(18,2),C.HOURLY_WAGE_PER_SECOND)*10*1/[12])*CONVERT(decimal(18,2),INDIRECT_LABOR_RATIO)/100)AS[12]
FROM (                               
    SELECT CASTING_YEAR,'간접노무비' AS CASTING_TYPE, CASTING_MONTH, CASTING_VALUE
    FROM [HS_MES].[dbo].[CASTING_SEARCH] WHERE CASTING_TYPE ='NET'
) AS B
PIVOT (
    SUM(CASTING_VALUE)
    FOR CASTING_MONTH IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12])
) AS PivotTable
INNER JOIN MATERIALCOST_PROCESS AS C
ON C.PROCESS_ID ='주조'


UNION ALL 

SELECT CASTING_YEAR,CASTING_TYPE, 
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[1])) AS [1] , -- 직접노무비 : 초당시간임율 * 사이클타임(CT) * 투입인원 * NET
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[2])) AS [2] , --  직접노무비 *간접노무비
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[3])) AS [3] ,
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[4])) AS [4] ,
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[5])) AS [5] ,
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[6])) AS [6] ,
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[7])) AS [7] ,
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[8])) AS [8] ,
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[9])) AS [9] ,
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[10])) AS [10] ,
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[11])) AS [11] ,
(CONVERT(decimal(18,2),C.EQUIPMENT_COST)/CONVERT(decimal(18,2),C.EQUIPMENT_USE_YEAR)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_DAY)/CONVERT(decimal(18,2),C.EQUIPMENT_OPERATION_TIME*10*[12])) AS [12] 

FROM (
    SELECT CASTING_YEAR,'설비감상비' AS CASTING_TYPE, CASTING_MONTH, CASTING_VALUE
    FROM [HS_MES].[dbo].[CASTING_SEARCH] WHERE CASTING_TYPE ='NET'
) AS B
PIVOT (
    SUM(CASTING_VALUE)
    FOR CASTING_MONTH IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12])
) AS PivotTable
INNER JOIN MATERIALCOST_EQUIPMENT AS C
ON C.EQUIPMENT_ID ='설비850TON'

)AS A
                                WHERE 1=1";

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString() + " order by CASTING_YEAR";

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

    }
}
