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
    public partial class KPI : BaseForm1
    {

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string str = $@" 
 SET ANSI_WARNINGS OFF
 SET ARITHIGNORE ON
 SET ARITHABORT OFF　　　　　　　　　　　　　　

select 
D.OUT_CODE
,D.NAME
,sum(B.OK_QTY) as 'B.OK_QTY'
,ISNULL(AVG(B.TIMEQTY), sum(B.OK_QTY))/5 *  7.25      as 'B.TIMEQTY'
,sum(B.OK_QTY)/SUM(B.작업시간)/5*7.25 as SQTY
,SUM(B.작업시간)　AS 작업시간 
,ISNULL(SUM(STOP_TIEM),0)AS 비가동시간
,sum(C.NG_QTY)        as 'C.NG_QTY'
,(sum(C.NG_QTY)/(sum(B.OK_QTY)+sum(C.NG_QTY)))*100 AS BAD   
FROM 
(
SELECT FORMAT(REG_DATE, 'yyyy-MM-dd') AS REG_DATE
,STOCK_MST_ID
from [dbo].[PRODUCTION_RESULT]
WHERE 1=1  {sb.ToString()}
group by FORMAT(REG_DATE, 'yyyy-MM-dd'),STOCK_MST_ID
) A  
LEFT JOIN 
(
  select SUM(OK_QTY) AS OK_QTY
,((SUM(DATEDIFF(SECOND,B.START_INSTRUCT_DATE ,B.END_INSTRUCT_DATE)))-ISNULL(SUM(STOP_TIEM),0) )/3600.00 AS 작업시간
,SUM(OK_QTY) /((SUM(DATEDIFF(SECOND,B.START_INSTRUCT_DATE ,B.END_INSTRUCT_DATE))-ISNULL(SUM(STOP_TIEM),0))/3600.00) AS TIMEQTY
,SUM(STOP_TIEM)AS STOP_TIEM
,FORMAT(A.REG_DATE, 'yyyy-MM-dd') REG_DATE
,A.STOCK_MST_ID
from [dbo].[PRODUCTION_RESULT] A
INNER JOIN [dbo].[PRODUCTION_INSTRUCT] B ON A.PRODUCTION_INSTRUCT_ID = B.ID
LEFT JOIN
(
 SELECT SUM(STOP_TIME) AS STOP_TIEM ,PRODUCTION_INSTRUCT_ID
 FROM [dbo].[EQUIPMENT_STOP]
 WHERE USE_YN = 'Y'
 GROUP BY PRODUCTION_INSTRUCT_ID
)C ON A.PRODUCTION_INSTRUCT_ID = C.PRODUCTION_INSTRUCT_ID
WHERE 1=1 
AND A.RESULT_TYPE  ='CD16001'
 group by FORMAT(A.REG_DATE, 'yyyy-MM-dd'),A.STOCK_MST_ID
) B ON A.REG_DATE = B.REG_DATE and A.STOCK_MST_ID = B.STOCK_MST_ID
LEFT JOIN 
(
select SUM(NG_QTY) AS NG_QTY
,FORMAT(REG_DATE, 'yyyy-MM-dd') REG_DATE
,STOCK_MST_ID
from [dbo].[PRODUCTION_RESULT] 
WHERE 1=1 
group by FORMAT(REG_DATE, 'yyyy-MM-dd'),STOCK_MST_ID
) C ON A.REG_DATE = C.REG_DATE and A.STOCK_MST_ID = C.STOCK_MST_ID
INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID  AND D.NAME != 'GM11.5'                                  
GROUP BY A.STOCK_MST_ID,D.OUT_CODE,D.NAME
union ALL 
SELECT 
'전체'
,'전체'
,sum(B.OK_QTY) as 'B.OK_QTY'
,ISNULL(AVG(B.TIMEQTY), sum(B.OK_QTY)) /5 * 7.25     as 'B.TIMEQTY'

,sum(B.OK_QTY)/SUM(B.작업시간)/5*7.25 as SQTY
,SUM(B.작업시간)　AS 작업시간 
,ISNULL(SUM(STOP_TIEM),0)AS 비가동시간
,sum(C.NG_QTY)        as 'C.NG_QTY'
,(sum(C.NG_QTY)/(sum(B.OK_QTY)+sum(C.NG_QTY)))*100 AS BAD   
FROM 
(
SELECT FORMAT(REG_DATE, 'yyyy-MM-dd') AS REG_DATE
,STOCK_MST_ID
from [dbo].[PRODUCTION_RESULT]
WHERE 1=1 {sb.ToString()}

group by FORMAT(REG_DATE, 'yyyy-MM-dd'),STOCK_MST_ID
) A  
LEFT JOIN 
(
  select SUM(OK_QTY) AS OK_QTY
,((SUM(DATEDIFF(SECOND,B.START_INSTRUCT_DATE ,B.END_INSTRUCT_DATE)))-ISNULL(SUM(STOP_TIEM),0) )/3600.00 AS 작업시간
,SUM(OK_QTY) /((SUM(DATEDIFF(SECOND,B.START_INSTRUCT_DATE ,B.END_INSTRUCT_DATE))-ISNULL(SUM(STOP_TIEM),0))/3600.00) AS TIMEQTY
,SUM(STOP_TIEM)AS STOP_TIEM
,FORMAT(A.REG_DATE, 'yyyy-MM-dd') REG_DATE
,A.STOCK_MST_ID
from [dbo].[PRODUCTION_RESULT] A
INNER JOIN [dbo].[PRODUCTION_INSTRUCT] B ON A.PRODUCTION_INSTRUCT_ID = B.ID
LEFT JOIN
(
 SELECT SUM(STOP_TIME) AS STOP_TIEM ,PRODUCTION_INSTRUCT_ID
 FROM [dbo].[EQUIPMENT_STOP]
 WHERE USE_YN = 'Y'
 GROUP BY PRODUCTION_INSTRUCT_ID
)C ON A.PRODUCTION_INSTRUCT_ID = C.PRODUCTION_INSTRUCT_ID
WHERE 1=1 
AND A.RESULT_TYPE  ='CD16001'
 group by FORMAT(A.REG_DATE, 'yyyy-MM-dd'),A.STOCK_MST_ID
) B ON A.REG_DATE = B.REG_DATE and A.STOCK_MST_ID = B.STOCK_MST_ID
 LEFT JOIN 
 (
 select SUM(NG_QTY) AS NG_QTY
 ,FORMAT(REG_DATE, 'yyyy-MM-dd') REG_DATE
 ,STOCK_MST_ID
 from [dbo].[PRODUCTION_RESULT] 
 WHERE 1=1 
 group by FORMAT(REG_DATE, 'yyyy-MM-dd'),STOCK_MST_ID
 ) C ON A.REG_DATE = C.REG_DATE and A.STOCK_MST_ID = C.STOCK_MST_ID
 INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID AND D.NAME != 'GM11.5'" ;

                DataTable _DataTable = new CoreBusiness().SELECT(str);

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

                    //MyCore._AddItemSUM(fpMain, MainForm.UserEntity.user_account);
                    //MyCore._AddItemAGV(fpMain, MainForm.UserEntity.user_account);
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
