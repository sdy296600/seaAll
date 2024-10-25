using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Monitoring
{

    public partial class 설비가동현황 : Form
    {
        public System.Threading.Timer _timer;

        string strcon = $"Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
        //string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
        //string strcon = "Server = 192.168.10.126; Database = HS_MES;UID = sa;PWD = coever1191!";
        public 설비가동현황(string pStrcon)
        {
            strcon = pStrcon;
            InitializeComponent();
            ResizeRedraw = true;
        }

        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        #endregion

        #region .. code for Flucuring ..

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }





        #endregion

        private void 설비가동현황_Load(object sender, EventArgs e)
        {

            this.Dock = DockStyle.None;

            this.Dock = DockStyle.Fill;

            _timer = new System.Threading.Timer(CallBack);

            _timer.Change(0, 60 * 1000);

        }
        public void CallBack(Object state)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    DateTime time = DateTime.Now;

                    label4.Text = $"Month : ({time.Year}-1 ~ {time.Month})";

                    DataSet ds = Get_설비가동현황();

                    DataTable dt = ds.Tables[0];

                    DataSet ds_oee = Get_OEE();

                    DataTable dt_oee = ds_oee.Tables[0];

                    chart1.ChartAreas[0].AxisX.Interval = 1;

                    chart2.ChartAreas[0].AxisX.Interval = 1;

                    chart3.ChartAreas[0].AxisX.Interval = 1;

                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MMM";

                    chart2.ChartAreas[0].AxisX.LabelStyle.Format = "MMM";

                    chart3.ChartAreas[0].AxisX.LabelStyle.Format = "MMM";

                    chart1.Series[0].Points.Clear();

                    chart2.Series[0].Points.Clear();

                    chart3.Series[0].Points.Clear();

                    chart1.Series[1].Points.Clear();

                    chart2.Series[1].Points.Clear();

                    chart3.Series[1].Points.Clear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string englishMonth = new DateTime(2024, Convert.ToInt32(dt.Rows[i][0]), 1).ToString("MMM", CultureInfo.InvariantCulture);
                        
                        chart2.Series[0].Points.AddXY(englishMonth.ToString(), dt.Rows[i][5]);
                        
                        chart2.Series[0].Points[i].Label = dt.Rows[i][5].ToString();
                        
                        chart2.Series[1].Points.AddXY(englishMonth.ToString(), "0.26");
             
                        chart3.Series[0].Points.AddXY(englishMonth.ToString(), dt.Rows[i][6]);
                        
                        chart3.Series[0].Points[i].Label = dt.Rows[i][6].ToString();
                        
                        chart3.Series[1].Points.AddXY(englishMonth.ToString(), "23");


                    }

                    chart2.Series[1].Points[11].Label = "0.26";
                    chart3.Series[1].Points[11].Label = "23";
            
                    for (int i = 0; i < dt_oee.Rows.Count; i++)
                    {
                        string englishMonth = new DateTime(2024, Convert.ToInt32(dt_oee.Rows[i][0]), 1).ToString("MMM", CultureInfo.InvariantCulture);
                        chart1.Series[0].Points.AddXY(englishMonth.ToString(),Convert.ToDecimal(dt_oee.Rows[i][5]).ToString("F2"));
                        chart1.Series[0].Points[i].Label = Convert.ToDecimal(dt_oee.Rows[i][5]).ToString("F2");
                        chart1.Series[1].Points.AddXY(englishMonth.ToString(), 62);
                    }
                    if (chart1.Series[1].Points.Count != 0)
                    {
                        chart1.Series[1].Points[chart1.Series[1].Points.Count - 1].Label = "62";
                    }


                }));
            }
            catch (Exception pExcption)
            {

            }

        }

        public DataSet Get_설비가동현황()
        {
          
            try
            {
               
                SqlConnection con = new SqlConnection(strcon);
                string strSql_Insert = $@" SET ANSI_WARNINGS OFF;
 SET ARITHIGNORE ON;
 SET ARITHABORT OFF ;
WITH cte AS (
                                          SELECT 0 AS n
                                          UNION ALL
                                          SELECT n + 1 
                                          FROM cte
                                          WHERE n < DATEDIFF(MONTH
                                          ,DATEFROMPARTS(YEAR(GETDATE()), 1, 1)
                                          ,DATEFROMPARTS(YEAR(GETDATE()), 12, 31)) -- HOUR 대신 다른 시간 단위 설정가능
                                        )
                                        
                                        SELECT 
                                         FORMAT(DATEADD(MONTH, AA.n, DATEFROMPARTS(YEAR(GETDATE()), 1, 1)), 'MM')
                                        ,isnull(A.전체가동시간,0)
                                        ,isnull(C.고정비가동,0)
                                        ,isnull(D.비가동,0)
                                        ,isnull(D.정지횟수,0)
                                        , CAST((((isnull(D.비가동,0))/isnull(D.정지횟수,0))/3600 ) AS decimal(18,2)) AS MTTR
                                        , CAST((((isnull(A.전체가동시간,0)-isnull(C.고정비가동,0))/isnull(D.정지횟수,0))/3600)AS decimal(18,2)) AS MTTF
                                        , CAST(((((isnull(D.비가동,0))/isnull(D.정지횟수,0)) +((isnull(A.전체가동시간,0)-isnull(C.고정비가동,0))/isnull(D.정지횟수,0)))/3600)AS decimal(18,2)) AS MTBF
                                        FROM cte AA
                                        LEFT JOIN  
                                        (
                                        SELECT ISNULL(SUM(DATEDIFF(second,A.START_INSTRUCT_DATE,
                                         ISNULL(A.END_INSTRUCT_DATE,GETDATE()))),0) AS 전체가동시간
                                         ,FORMAT(A.INSTRUCT_DATE, 'yyyy-MM') AS INSTRUCT_DATE
                                        FROM [dbo].[PRODUCTION_INSTRUCT] A
                                        INNER JOIN [dbo].[PRODUCTION_PLAN] B ON A.PRODUCTION_PLAN_ID = B.ID AND B.LINE = 'CD14002'
                                         WHERE A.USE_YN = 'Y'　
                                         AND A.START_INSTRUCT_DATE IS NOT NULL
                                         AND A.END_INSTRUCT_DATE IS NOT NULL
                                         GROUP BY FORMAT(A.INSTRUCT_DATE, 'yyyy-MM')
                                        ) A ON A.INSTRUCT_DATE = FORMAT(DATEADD(MONTH, AA.n, DATEFROMPARTS(YEAR(GETDATE()), 1, 1)), 'yyyy-MM') 
                                         LEFT JOIN 
                                         (
                                          SELECT SUM(STOP_TIME) AS 고정비가동
                                          ,FORMAT(A.START_TIME, 'yyyy-MM') AS START_TIME
                                          FROM [dbo].[EQUIPMENT_STOP] A
                                          INNER JOIN [dbo].[Code_Mst] B on A.TYPE = B.code and B.code_description ='고정비가동'
                                          INNER JOIN [dbo].[PRODUCTION_INSTRUCT] C ON A.PRODUCTION_INSTRUCT_ID = C.ID
                                          INNER JOIN [dbo].[PRODUCTION_PLAN] D ON C.PRODUCTION_PLAN_ID = D.ID AND D.LINE = 'CD14002'
                                          WHERE A.USE_YN = 'Y'
                                          GROUP BY FORMAT(A.START_TIME, 'yyyy-MM')
                                         ) C ON A.INSTRUCT_DATE = C.START_TIME
                                         LEFT JOIN 
                                         (
                                          SELECT SUM(STOP_TIME) AS 비가동
                                          ,FORMAT(A.START_TIME, 'yyyy-MM') AS START_TIME
                                          , CAST(COUNT(STOP_TIME) AS decimal(18,2)) AS 정지횟수
                                          FROM [dbo].[EQUIPMENT_STOP] A
                                          INNER JOIN [dbo].[Code_Mst] B on A.TYPE = B.code and B.code_description !='고정비가동'
                                          INNER JOIN [dbo].[PRODUCTION_INSTRUCT] C ON A.PRODUCTION_INSTRUCT_ID = C.ID
                                          INNER JOIN [dbo].[PRODUCTION_PLAN] D ON C.PRODUCTION_PLAN_ID = D.ID AND D.LINE = 'CD14002'
                                          WHERE A.USE_YN = 'Y' AND STOP_TIME >0
                                          GROUP BY FORMAT(A.START_TIME, 'yyyy-MM')
                                         ) D ON  A.INSTRUCT_DATE = D.START_TIME";



                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, con);


                // DB처리
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_Insert;
                DataSet ds = new System.Data.DataSet();
                con.Open();
                da.Fill(ds);
                con.Close();
                return ds;

             }
            catch (Exception err)
            {
                return null;
            }
        }

        public DataSet Get_OEE()
        {

            try
            {
               
                //string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
                //string strcon = "Server = 192.168.10.126; Database = HS_MES;UID = sa;PWD = coever1191!";
                SqlConnection con = new SqlConnection(strcon);
                string strSql_Insert = $@"   WITH cte AS (
                                          SELECT 0 AS n
                                          UNION ALL
                                          SELECT n + 1 
                                          FROM cte
                                          WHERE n < DATEDIFF(MONTH
                                          ,DATEFROMPARTS(YEAR(GETDATE()), 1, 1)
                                          ,DATEFROMPARTS(YEAR(GETDATE()), 12, 31)) -- HOUR 대신 다른 시간 단위 설정가능
                                        )
						

SELECT 
 FORMAT(DATEADD(MONTH, AAA.n, DATEFROMPARTS(YEAR(GETDATE()), 1, 1)), 'MM')
,SUM(가용가능시간)/SUM(가용한시간)*100 AS A
,(SUM(TOTAL)/SUM(가용생산수량)) *100 AS P
,(SUM(OK)/SUM(TOTAL))*100 AS Q
,SUM(OK) AS OK_SUM
,ISNULL(((SUM(가용가능시간)/SUM(가용한시간)) *
((SUM(TOTAL)/SUM(가용생산수량))) *
((SUM(OK)/SUM(TOTAL))))*100,0)　as OEE
,AA.INSTRUCT_DATE
FROM cte AAA
left join
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
,FORMAT(A.INSTRUCT_DATE, 'yyyy-MM') AS INSTRUCT_DATE
,(CASE 
 WHEN A.END_INSTRUCT_DATE is not null  
 THEN ISNULL(OK,0) + ISNULL(OK_QTY,0) 
 ELSE ISNULL(OK,0) + ISNULL(OK_QTY,0)
 END  ) as OK
 ,(CASE 
 WHEN A.END_INSTRUCT_DATE is not null  
 THEN ISNULL(NG,0) + ISNULL(NG_QTY,0) 
 ELSE ISNULL(NG,0) + ISNULL(NG_QTY,0) 
 END  ) as NG
  ,(CASE 
 WHEN A.END_INSTRUCT_DATE is not null  
 THEN OK + NG + OK_QTY + NG_QTY
 ELSE ISNULL(OK,0) + ISNULL(NG,0) + ISNULL(OK_QTY,0) + ISNULL(NG_QTY,0) 
 END  ) as TOTAL
FROM [dbo].[PRODUCTION_INSTRUCT] A 
INNER JOIN [dbo].[PRODUCTION_PLAN] B ON A.PRODUCTION_PLAN_ID = B.ID AND B.LINE = 'CD14002'
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
AND FORMAT(A.INSTRUCT_DATE, 'yyyy') = FORMAT(GETDATE(), 'yyyy')
) AA on AA.INSTRUCT_DATE = FORMAT(DATEADD(MONTH, AAA.n, DATEFROMPARTS(YEAR(GETDATE()), 1, 1)), 'yyyy-MM') 
 group by AA.INSTRUCT_DATE,aaa.n;";



                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, con);


                // DB처리
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_Insert;
                DataSet ds = new System.Data.DataSet();
                con.Open();
                da.Fill(ds);
                con.Close();
                return ds;

            }
            catch (Exception err)
            {
                return null;
            }
        }


    }
}
