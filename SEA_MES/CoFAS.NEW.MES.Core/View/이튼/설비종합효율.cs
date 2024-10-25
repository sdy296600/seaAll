using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 설비종합효율 : TripleBaseForm1
    {
        public 설비종합효율()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);


        }

        private void DoubleBaseForm2_Load(object sender, EventArgs e)
        {
            splitContainer2.Orientation = Orientation.Vertical;

            splitContainer2.SplitterDistance = 150;
        }
        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        public override void _DeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public void SubFind_DisplayData(string code, DateTime dateTime1, DateTime dateTime2)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;


                string str = $@"  SET ANSI_WARNINGS OFF
 SET ARITHIGNORE ON
 SET ARITHABORT OFF 

SELECT SUM(가용가능시간)/SUM(가용한시간)*100 AS A
,(SUM(TOTAL)/SUM(가용생산수량)) *100 AS P
,(SUM(OK)/SUM(TOTAL))*100 AS Q
,SUM(OK) AS OK_SUM
,((SUM(가용가능시간)/SUM(가용한시간)) *
((SUM(TOTAL)/SUM(가용생산수량))) *
((SUM(OK)/SUM(TOTAL))))*100　as OEE
FROM 
(
SELECT 
 Convert(Decimal,DATEDIFF(SS,A.START_INSTRUCT_DATE
,ISNULL(A.END_INSTRUCT_DATE,GETDATE())) 
-ISNULL(D.고정비가동,0))                 AS 가용한시간
,Convert(Decimal, DATEDIFF(SS,A.START_INSTRUCT_DATE
,ISNULL(A.END_INSTRUCT_DATE,GETDATE())) 
-ISNULL(D.고정비가동,0)                  
-ISNULL(E.비가동,0) )                    AS 가용가능시간
--,C.CYCLE_TIME                            AS CT
--,C.PERFORMANCE                           AS PERFORMANCE
,ceiling((DATEDIFF(SS,A.START_INSTRUCT_DATE,ISNULL(A.END_INSTRUCT_DATE,GETDATE())) - ISNULL(D.고정비가동,0)) /C.CYCLE_TIME)
                                         AS 가용생산수량
,ceiling(((DATEDIFF(SS,A.START_INSTRUCT_DATE,ISNULL(A.END_INSTRUCT_DATE,GETDATE())) - ISNULL(D.고정비가동,0)) /C.CYCLE_TIME) * C.PERFORMANCE)
                                         AS 효율생산수량
,ISNULL(OK_QTY,0)                    AS   입력_OK_QTY      
,ISNULL(NG_QTY,0)                    AS   입력_NG_QTY      
,ISNULL(TOTAL_QTY,0)                 AS   입력_TOTAL_QTY
,ISNULL(OK,0)                            AS   수집_OK_QTY
,ISNULL(NG,0)                            AS   수집_NG_QTY
,(CASE 
 WHEN A.END_INSTRUCT_DATE is not null  
 THEN ISNULL(OK_QTY,0) 
 ELSE ISNULL(OK,0) + ISNULL(OK_QTY,0)
 END  ) as OK
 ,(CASE 
 WHEN A.END_INSTRUCT_DATE is not null  
 THEN  ISNULL(NG_QTY,0) 
 ELSE ISNULL(NG,0) + ISNULL(NG_QTY,0) 
 END  ) as NG
  ,(CASE 
 WHEN A.END_INSTRUCT_DATE is not null  
 THEN OK_QTY + NG_QTY
 ELSE ISNULL(OK,0) + ISNULL(NG,0) + ISNULL(OK_QTY,0) + ISNULL(NG_QTY,0) 
 END  ) as TOTAL
FROM [dbo].[PRODUCTION_INSTRUCT] A 
INNER JOIN [dbo].[PRODUCTION_PLAN] B ON A.PRODUCTION_PLAN_ID = B.ID AND B.LINE = '{code}'
INNER JOIN [dbo].[WORK_CAPA] C ON A.WORK_CAPA_STD_OPERATOR = C.ID
LEFT JOIN
(
SELECT SUM(STOP_TIME) AS '고정비가동' 
,a.PRODUCTION_INSTRUCT_ID
FROM [dbo].[EQUIPMENT_STOP] a
inner join Code_Mst b on a.TYPE = b.code and b.code_description ='고정비가동'
WHERE a.USE_YN = 'Y'
GROUP BY a.PRODUCTION_INSTRUCT_ID
) D ON A.ID = D.PRODUCTION_INSTRUCT_ID
LEFT JOIN 
(
SELECT SUM(STOP_TIME) AS '비가동' 
,a.PRODUCTION_INSTRUCT_ID
FROM [dbo].[EQUIPMENT_STOP] a
inner join Code_Mst b on a.TYPE = b.code and b.code_description =''
WHERE a.USE_YN = 'Y'
GROUP BY a.PRODUCTION_INSTRUCT_ID
)E ON A.ID = E.PRODUCTION_INSTRUCT_ID
LEFT JOIN
(
 SELECT a.PRODUCTION_INSTRUCT_ID
 ,SUM(a.OK_QTY)    AS OK_QTY
 ,SUM(a.NG_QTY)    AS NG_QTY
 ,SUM(a.TOTAL_QTY) AS TOTAL_QTY
 FROM [dbo].[PRODUCTION_RESULT] a
 where 1=1
  AND a.USE_YN = 'Y'
 GROUP BY PRODUCTION_INSTRUCT_ID
)F ON A.ID = F.PRODUCTION_INSTRUCT_ID
LEFT JOIN
(
 SELECT  a.ID
          ,COUNT(b.id) AS OK
  FROM [dbo].[PRODUCTION_INSTRUCT] a
   LEFT JOIN [dbo].[OPC_MST_OK] b ON b.READ_DATE BETWEEN a.START_INSTRUCT_DATE AND ISNULL(a.END_INSTRUCT_DATE,GETDATE())
   where 1 = 1
 AND a.USE_YN = 'Y'
 AND a.START_INSTRUCT_DATE IS NOT NULL
   GROUP BY a.ID
) G ON A.ID = G.ID
LEFT JOIN
(
 SELECT a.ID
        ,COUNT(b.id) AS NG
  FROM [dbo].[PRODUCTION_INSTRUCT] a
   LEFT JOIN [dbo].[OPC_MST_NG] b ON b.READ_DATE BETWEEN a.START_INSTRUCT_DATE AND ISNULL(a.END_INSTRUCT_DATE,GETDATE()) AND b.name = 'MES_FSt030_Pass'
   where 1 = 1
 AND a.USE_YN = 'Y'
 AND a.START_INSTRUCT_DATE IS NOT NULL
   GROUP BY a.ID
) H ON A.ID = H.ID
WHERE 1=1
AND A.START_INSTRUCT_DATE IS NOT NULL 
AND A.USE_YN = 'Y'
AND FORMAT(A.INSTRUCT_DATE, 'yyyy-MM-dd') Between '{dateTime1.ToString("yyyy-MM-dd")}' and '{dateTime2.ToString("yyyy-MM-dd")}'
) AA ";
                DataTable _DataTable = new CoreBusiness().SELECT(str);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpSub.Sheets[0].Visible = false;
                    fpSub.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpSub.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName]);
                        }

                    }

                    fpSub.Sheets[0].Visible = true;


                }
                else
                {
                    fpSub.Sheets[0].Rows.Count = 0;
                    //CustomMsg.ShowMessage("조회 내역이 없습니다.");
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

        public void SubFind_DisplayData2(string code, DateTime dateTime1, DateTime dateTime2)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub2.Sheets[0].Rows.Count = 0;


                string str = $@"  SET ANSI_WARNINGS OFF
 SET ARITHIGNORE ON
 SET ARITHABORT OFF 

SELECT 
 Convert(Decimal,DATEDIFF(SS,A.START_INSTRUCT_DATE
,ISNULL(A.END_INSTRUCT_DATE,GETDATE())) 
 -ISNULL(D.고정비가동,0))                 AS 가용한시간
,Convert(Decimal, DATEDIFF(SS,A.START_INSTRUCT_DATE
,ISNULL(A.END_INSTRUCT_DATE,GETDATE())) 
-ISNULL(D.고정비가동,0)                  
-ISNULL(E.비가동,0) )                    AS 가용가능시간
,ISNULL(D.고정비가동,0)                  AS 고정비가동
,ISNULL(E.비가동,0)                      AS 비가동
--,C.CYCLE_TIME                            AS CT
--,C.PERFORMANCE                           AS PERFORMANCE
,ceiling((DATEDIFF(SS,A.START_INSTRUCT_DATE,ISNULL(A.END_INSTRUCT_DATE,GETDATE())) - ISNULL(D.고정비가동,0)) /C.CYCLE_TIME)
                                         AS 가용생산수량
,ceiling(((DATEDIFF(SS,A.START_INSTRUCT_DATE,ISNULL(A.END_INSTRUCT_DATE,GETDATE())) - ISNULL(D.고정비가동,0)) /C.CYCLE_TIME) * C.PERFORMANCE)
                                         AS 효율생산수량
,ISNULL(OK_QTY,0)                    AS   입력_OK_QTY      
,ISNULL(NG_QTY,0)                    AS   입력_NG_QTY      
,ISNULL(TOTAL_QTY,0)                 AS   입력_TOTAL_QTY
,ISNULL(OK,0)                            AS   수집_OK_QTY
,ISNULL(NG,0)                            AS   수집_NG_QTY
,(CASE 
 WHEN A.END_INSTRUCT_DATE is not null  
 THEN ISNULL(OK_QTY,0) 
 ELSE ISNULL(OK,0) + ISNULL(OK_QTY,0)
 END  ) as OK
 ,(CASE 
 WHEN A.END_INSTRUCT_DATE is not null  
 THEN ISNULL(NG_QTY,0) 
 ELSE ISNULL(NG,0) + ISNULL(NG_QTY,0) 
 END  ) as NG
  ,(CASE 
 WHEN A.END_INSTRUCT_DATE is not null  
 THEN ISNULL(OK_QTY,0) + ISNULL(NG_QTY,0)
 ELSE ISNULL(OK,0) +ISNULL(NG,0) + ISNULL(OK_QTY,0) + ISNULL(NG_QTY,0) 
 END  ) as TOTAL
FROM [dbo].[PRODUCTION_INSTRUCT] A 
INNER JOIN [dbo].[PRODUCTION_PLAN] B ON A.PRODUCTION_PLAN_ID = B.ID AND B.LINE = '{code}'
INNER JOIN [dbo].[WORK_CAPA] C ON A.WORK_CAPA_STD_OPERATOR = C.ID
LEFT JOIN
(
SELECT SUM(STOP_TIME) AS '고정비가동' 
,a.PRODUCTION_INSTRUCT_ID
FROM [dbo].[EQUIPMENT_STOP] a
inner join Code_Mst b on a.TYPE = b.code and b.code_description ='고정비가동'
WHERE a.USE_YN = 'Y'
GROUP BY a.PRODUCTION_INSTRUCT_ID
) D ON A.ID = D.PRODUCTION_INSTRUCT_ID
LEFT JOIN 
(
SELECT SUM(STOP_TIME) AS '비가동' 
,a.PRODUCTION_INSTRUCT_ID
FROM [dbo].[EQUIPMENT_STOP] a
inner join Code_Mst b on a.TYPE = b.code and b.code_description =''
WHERE a.USE_YN = 'Y'
GROUP BY a.PRODUCTION_INSTRUCT_ID
)E ON A.ID = E.PRODUCTION_INSTRUCT_ID
LEFT JOIN
(
 SELECT a.PRODUCTION_INSTRUCT_ID
 --,(select sum(OK_QTY) from PRODUCTION_RESULT where RESULT_TYPE = 'CD16001' and PRODUCTION_INSTRUCT_ID = a.PRODUCTION_INSTRUCT_ID) AS OK_QTY
 -- ,(select sum(OK_QTY)+sum(NG_QTY) from PRODUCTION_RESULT where RESULT_TYPE != 'CD16001' and PRODUCTION_INSTRUCT_ID = a.PRODUCTION_INSTRUCT_ID) AS NG_QTY
 ,SUM(a.OK_QTY)    AS OK_QTY
 ,SUM(a.NG_QTY)    AS NG_QTY
 ,SUM(a.TOTAL_QTY) AS TOTAL_QTY
 FROM [dbo].[PRODUCTION_RESULT] a
 where 1=1
  AND a.USE_YN = 'Y'
 GROUP BY PRODUCTION_INSTRUCT_ID
)F ON A.ID = F.PRODUCTION_INSTRUCT_ID
LEFT JOIN
(
 SELECT  a.ID
          ,COUNT(b.id) AS OK
  FROM [dbo].[PRODUCTION_INSTRUCT] a
   LEFT JOIN [dbo].[OPC_MST_OK] b ON b.READ_DATE BETWEEN a.START_INSTRUCT_DATE AND ISNULL(a.END_INSTRUCT_DATE,GETDATE())
   where 1 = 1
 AND a.USE_YN = 'Y'
 AND a.START_INSTRUCT_DATE IS NOT NULL
   GROUP BY a.ID
) G ON A.ID = G.ID
LEFT JOIN
(
 SELECT a.ID
        ,COUNT(b.id) AS NG
  FROM [dbo].[PRODUCTION_INSTRUCT] a
   LEFT JOIN [dbo].[OPC_MST_NG] b ON b.READ_DATE BETWEEN a.START_INSTRUCT_DATE AND ISNULL(a.END_INSTRUCT_DATE,GETDATE()) AND b.name = 'MES_FSt030_Pass'
   where 1 = 1
 AND a.USE_YN = 'Y'
 AND a.START_INSTRUCT_DATE IS NOT NULL
   GROUP BY a.ID
) H ON A.ID = H.ID
WHERE 1=1
AND A.START_INSTRUCT_DATE IS NOT NULL 
AND A.USE_YN = 'Y'
AND FORMAT(A.INSTRUCT_DATE, 'yyyy-MM-dd') Between '{dateTime1.ToString("yyyy-MM-dd")}' and '{dateTime2.ToString("yyyy-MM-dd")}'";
                DataTable _DataTable = new CoreBusiness().SELECT(str);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpSub2.Sheets[0].Visible = false;
                    fpSub2.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpSub2.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName]);
                        }

                    }

                    fpSub2.Sheets[0].Visible = true;


                }
                else
                {
                    fpSub2.Sheets[0].Rows.Count = 0;
                    //CustomMsg.ShowMessage("조회 내역이 없습니다.");
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
     
        public override void fpMain_CellClick(object sender, CellClickEventArgs e)
        {
            try
            {
                string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                if (pHeaderLabel != "입력" && pHeaderLabel != "합계")
                {
                    string code =  fpMain.Sheets[0].GetValue(e.Row, "CODE").ToString();

                    BaseCalendarPopupBox baseMonthCalendarPopupBox = new BaseCalendarPopupBox();
                    baseMonthCalendarPopupBox.CalendarForm_name = "시작일";
                    if (baseMonthCalendarPopupBox.ShowDialog() == DialogResult.OK)
                    {
                        if (baseMonthCalendarPopupBox._SelectionStart != null)
                        {
                            BaseCalendarPopupBox baseMonthCalendarPopupBox2 = new BaseCalendarPopupBox();
                            baseMonthCalendarPopupBox2.CalendarForm_name = "종료일";
                            if (baseMonthCalendarPopupBox2.ShowDialog() == DialogResult.OK)
                            {
                                if (baseMonthCalendarPopupBox2._SelectionStart != null)
                                {
                                    SubFind_DisplayData(code, baseMonthCalendarPopupBox._SelectionStart.Value, baseMonthCalendarPopupBox2._SelectionStart.Value);
                                    SubFind_DisplayData2(code, baseMonthCalendarPopupBox._SelectionStart.Value, baseMonthCalendarPopupBox2._SelectionStart.Value);
                                }
                            }


                        }
                    }

                }

            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
    }
}
