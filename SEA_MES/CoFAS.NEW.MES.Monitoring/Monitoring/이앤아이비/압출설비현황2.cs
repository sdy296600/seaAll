using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Monitoring
{

    public partial class 압출설비현황2 : Form
    {
        public System.Threading.Timer _timer;

        string strcon = "Server = 222.113.146.82,11433; Database = HS_MES;UID = sa;PWD = coever1191!";
        //string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
        //string strcon = "Server = 192.168.10.126; Database = HS_MES;UID = sa;PWD = coever1191!";
        public 압출설비현황2(string pStrcon)
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

        private void 압출설비현황2_Load(object sender, EventArgs e)
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

                    //label4.Text = $"Month : ({time.Year}-1 ~ {time.Month})";

                    DataSet ds = Get_설비가동현황();

                    DataTable dt = ds.Tables[0];


                    int he = panel2.Size.Height / dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        DataRow dr = dt.Rows[i];

                        ucCorechipsWorkStatus_이앤아이비 uc = new ucCorechipsWorkStatus_이앤아이비(dr);

                        int loc = he * i;

                        uc.Font = this.Font;

                        uc.Location = new Point(0, loc);

                        uc.Margin = new Padding(0, 0, 0, 0);

                        uc.Size = new Size(1908, he);
                        //uc.Size = tableLayoutPanel2.Size;

                        panel2.Controls.Add(uc);

                    }

                    //DataSet ds_oee = Get_OEE();

                    //DataTable dt_oee = ds_oee.Tables[0];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //string englishMonth = new DateTime(2024, Convert.ToInt32(dt.Rows[i][0]), 1).ToString("MMM", CultureInfo.InvariantCulture);
                    }
            
                    //for (int i = 0; i < dt_oee.Rows.Count; i++)
                    //{
                    //    string englishMonth = new DateTime(2024, Convert.ToInt32(dt_oee.Rows[i][0]), 1).ToString("MMM", CultureInfo.InvariantCulture);
                    //}


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
                string strSql_Insert = $@"select

A.NAME							   AS EQUIPMENT_NAME
,'-'							   AS MODEL_NAME
,'-'						       AS LOT_NUMBER
,ISNULL(B.PV_STATUS,0)			   AS STATUS
,ISNULL(B.PV_LOAD,0)			   AS LOAD
,ISNULL(B.PV_RPM,0)				   AS RPM
,'-'							   AS 'CYL-1'
from EQUIPMENT A
LEFT JOIN CCS_EXTRUSION_SPC B ON B.ID = 
(SELECT TOP(1) ID
FROM CCS_EXTRUSION_SPC AS B2
WHERE B2.EXT_ID = A.COLUMN1
ORDER BY B2.ID)
and CONVERT(CHAR(23), B.REG_DATE, 23) = CONVERT(CHAR(23), GETDATE(), 23)
where A.COLUMN1 != ''
and A.NAME != '11'
and A.TYPE = 'CD14003'
-- and CONVERT(CHAR(23), B.REG_DATE, 23) = CONVERT(CHAR(23), GETDATE(), 23)
order by A.ID ASC";



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
