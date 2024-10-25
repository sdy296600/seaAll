using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Monitoring
{

    public partial class 생산진형현황 : Form
    {
        public System.Threading.Timer _timer;
        //string strcon = $"Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
        string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";

        public 생산진형현황(string pStrcon)
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

        private void 생산진형현황_Load(object sender, EventArgs e)
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
                    DateTime  currentTime = DateTime.Now;

                    // 만약 현재 시간이 오후 12시 30분부터 오후 1시 30분 사이라면
                    if (currentTime.Hour == 12 && currentTime.Minute >= 30 ||
                        currentTime.Hour == 13 && currentTime.Minute <= 30)
                    {
                        return;
                    }
                    else
                    {
                        DataSet ds = Get_생산진형현황();

                        if (ds != null && ds.Tables.Count != 0)
                        {
                            DataTable dt = ds.Tables[0];
                            DataRow dr = dt.Rows[0];

                            lab_생산아이템.Text = $"{dr[1].ToString()}";

                            lbl_Today.Text = $"Date : ({DateTime.Now.ToString("yyyy-MM-dd")})";

                            dt = ds.Tables[1];
                            dr = dt.Rows[0];

                            o_act.Text = Convert.ToDouble(dr["OEE"]).ToString("F2") + "%";

                            double oee = Convert.ToDouble(dr["OEE"]);
                            if (oee > 62.1)
                            {
                                o_act.BackColor = Color.FromArgb(102, 255, 51);

                            }
                            else
                            {
                                double oee1 = ((62.1)/100)*90 ;
                                if (oee > oee1)
                                {
                                    o_act.BackColor = Color.Yellow;
                                }
                                else
                                {
                                    o_act.BackColor = Color.Red;
                                }
                            }

                            double TOTAL_QTY = Convert.ToDouble(dr["TOTAL_QTY"]);
                            double INSTRUCT_QTY= Convert.ToDouble(dr["효율생산수량"]);

                            p_plan.Text = INSTRUCT_QTY.ToString("F0");

                            p_act.Text = TOTAL_QTY.ToString("F0");

                            if (TOTAL_QTY > INSTRUCT_QTY)
                            {
                                p_act.BackColor = Color.FromArgb(102, 255, 51);

                            }
                            else
                            {
                                double fpy1 = ((INSTRUCT_QTY)/100)*90 ;
                                if (TOTAL_QTY > fpy1)
                                {
                                    p_act.BackColor = Color.Yellow;
                                }
                                else
                                {
                                    p_act.BackColor = Color.Red;
                                }
                            }

                            t_plan.Text = Convert.ToDouble(dr["PL"]).ToString("F2") + "%";

                            double OK_QTY = Convert.ToDouble(dr["OK_QTY"]);

                            if (TOTAL_QTY == 0 && OK_QTY == 0)
                            {
                                f_act.Text = (0).ToString("F2") + "%";
                            }
                            else
                            {
                                f_act.Text = ((OK_QTY / TOTAL_QTY) * 100).ToString("F2") + "%";

                                double fpy =(OK_QTY / TOTAL_QTY) * 100;
                                if (fpy > 98.3)
                                {
                                    f_act.BackColor = Color.FromArgb(102, 255, 51);

                                }
                                else
                                {
                                    double fpy1 = ((98.3)/100)*90 ;
                                    if (fpy > fpy1)
                                    {
                                        f_act.BackColor = Color.Yellow;
                                    }
                                    else
                                    {
                                        f_act.BackColor = Color.Red;
                                    }
                                }
                            }

                        }
                    }
                  
                 

                }));
            }
            catch (Exception pExcption)
            {

            }

        }
        public DataSet Get_생산진형현황()
        {

            try
            {

                SqlConnection con = new SqlConnection(strcon);
                string strSql_Insert = $@"select 
                                         ISNULL(C.OUT_CODE,'') AS OUT_CODE
                                        ,ISNULL(C.NAME,'')     AS NAME                                       
                                          from 
                                          (
                                           SELECT  CASE 
                                             WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                             THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                             ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END AS 'NOWTIME'
                                          ) A
                                          LEFT JOIN [dbo].[PRODUCTION_INSTRUCT] B ON A.NOWTIME = FORMAT(B.INSTRUCT_DATE, 'yyyy-MM-dd') AND B.USE_YN = 'Y'  AND B.START_INSTRUCT_DATE IS NOT NULL and B.END_INSTRUCT_DATE IS NULL
                                          LEFT JOIN [dbo].[STOCK_MST] C ON B.STOCK_MST_ID = C.ID
                                          ORDER BY B.START_INSTRUCT_DATE DESC;

 SET ANSI_WARNINGS OFF
 SET ARITHIGNORE ON
 SET ARITHABORT OFF                                          
SELECT 
 ISNULL((((SUM(AA.가용가능시간) - SUM(AA.입력비가동시간))/Convert(Decimal,SUM(AA.가용가능시간)))),0)*100 AS A
,ISNULL(SUM(AA.TOTAL)/SUM(가용생산수량),0)*100 AS P
,ISNULL(SUM(AA.OK)/SUM(AA.TOTAL),0)*100 AS Q   
,ISNULL((((SUM(AA.가용가능시간) - SUM(AA.입력비가동시간))/Convert(Decimal,SUM(AA.가용가능시간)))),0)
*ISNULL(SUM(AA.TOTAL)/SUM(가용생산수량),0)
*ISNULL(SUM(AA.OK)/SUM(AA.TOTAL),0)*100 AS OEE   
,ISNULL(SUM(AA.가용가능시간),0)         AS 가용가능시간
,ISNULL(SUM(AA.입력비가동시간),0)       AS 입력비가동시간
,ISNULL(SUM(AA.TOTAL),0)                AS TOTAL_QTY
,ISNULL(SUM(AA.OK),0)                   AS OK_QTY
,ISNULL(SUM(AA.NG),0)                   AS NG_QTY
,ISNULL(SUM(AA.가용생산수량),0)         AS TIME_QTY
,ISNULL(SUM(AA.목표생산수량),0)         AS 목표생산수량
,ISNULL(SUM(AA.효율생산수량),0)         AS 효율생산수량
,ISNULL(ISNULL(SUM(AA.TOTAL),0)/ISNULL(SUM(AA.효율생산수량),0),0)*100 AS PL
FROM 
(
SELECT
(DATEDIFF(SS,B.START_INSTRUCT_DATE,isnull(B.END_INSTRUCT_DATE,GETDATE())))-ISNULL(고정비가동.STOP_TIME,0) AS 가용가능시간
,ISNULL(비가동.STOP_TIME,0)      AS 입력비가동시간
,D.CYCLE_TIME
,(CASE 
 WHEN D.PERFORMANCE = 0
 THEN 1
 ELSE D.PERFORMANCE END) as PERFORMANCE
,(CASE 
 WHEN B.END_INSTRUCT_DATE is not null  
 THEN ISNULL(실적수량.OK_QTY,0) 
 ELSE ISNULL(실적수량.OK_QTY,0)    + ISNULL(수집정상.OK,0) 
 END  ) as OK
 ,(CASE 
 WHEN B.END_INSTRUCT_DATE is not null  
 THEN ISNULL(실적수량.NG_QTY,0) 
 ELSE ISNULL(실적수량.NG_QTY,0)    + ISNULL(수집불량.NG,0)
 END  ) as NG
  ,(CASE 
 WHEN B.END_INSTRUCT_DATE is not null  
 THEN ISNULL(실적수량.TOTAL_QTY,0)
 ELSE ISNULL(실적수량.TOTAL_QTY,0) + ISNULL((수집정상.OK + 수집불량.NG),0)
 END  ) as TOTAL
,((DATEDIFF(SS,B.START_INSTRUCT_DATE,isnull(B.END_INSTRUCT_DATE,GETDATE()))) -ISNULL(고정비가동.STOP_TIME,0) - ISNULL(비가동.STOP_TIME,0))/D.CYCLE_TIME AS 가용생산수량
,((DATEDIFF(SS,B.START_INSTRUCT_DATE,isnull(B.END_INSTRUCT_DATE,GETDATE())))-ISNULL(고정비가동.STOP_TIME,0))/D.CYCLE_TIME AS 목표생산수량
,(((DATEDIFF(SS,B.START_INSTRUCT_DATE,isnull(B.END_INSTRUCT_DATE,GETDATE())))-ISNULL(고정비가동.STOP_TIME,0))/D.CYCLE_TIME) * 
(CASE 
 WHEN D.PERFORMANCE = 0
 THEN 1
 ELSE D.PERFORMANCE END) AS 효율생산수량
from 
(
 SELECT  CASE 
   WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
   THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
   ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END AS 'NOWTIME'
) A
INNER JOIN [dbo].[PRODUCTION_INSTRUCT] B ON A.NOWTIME = FORMAT(B.INSTRUCT_DATE, 'yyyy-MM-dd') AND B.USE_YN = 'Y'  AND B.START_INSTRUCT_DATE IS NOT NULL
INNER JOIN [dbo].[PRODUCTION_PLAN] C ON B.PRODUCTION_PLAN_ID = C.ID AND C.LINE ='CD14002'
INNER JOIN [dbo].[WORK_CAPA] D ON B.WORK_CAPA_STD_OPERATOR = D.ID
LEFT JOIN 
(
SELECT SUM(STOP_TIME) AS STOP_TIME 
,PRODUCTION_INSTRUCT_ID
FROM [dbo].[EQUIPMENT_STOP] a
inner join Code_Mst b on a.TYPE = b.code and b.code_description =''
WHERE a.USE_YN = 'Y'
GROUP BY PRODUCTION_INSTRUCT_ID
)비가동 ON  B.ID = 비가동.PRODUCTION_INSTRUCT_ID
LEFT JOIN 
(
SELECT SUM(STOP_TIME) AS STOP_TIME 
,PRODUCTION_INSTRUCT_ID
FROM [dbo].[EQUIPMENT_STOP] a
inner join Code_Mst b on a.TYPE = b.code and b.code_description ='고정비가동'
WHERE a.USE_YN = 'Y'
GROUP BY PRODUCTION_INSTRUCT_ID
)고정비가동 ON  B.ID = 고정비가동.PRODUCTION_INSTRUCT_ID
LEFT JOIN 
  (
  SELECT a.ID,COUNT(b.id) AS OK
  FROM [dbo].[PRODUCTION_INSTRUCT] a
   LEFT JOIN [dbo].[OPC_MST_OK] b ON b.READ_DATE BETWEEN a.START_INSTRUCT_DATE AND ISNULL(a.END_INSTRUCT_DATE,GETDATE())
   where 1 = 1
 AND a.USE_YN = 'Y'
 AND a.START_INSTRUCT_DATE IS NOT NULL
 AND FORMAT(a.INSTRUCT_DATE, 'yyyy-MM-dd')　=
 (
 SELECT  CASE 
 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
 THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
 ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END 
 )
   group by a.ID
   )수집정상 ON B.ID = 수집정상.ID
   LEFT JOIN 
  (
  SELECT a.ID,COUNT(b.id) AS NG
  FROM [dbo].[PRODUCTION_INSTRUCT] a
   LEFT JOIN [dbo].[OPC_MST_NG] b ON b.READ_DATE BETWEEN a.START_INSTRUCT_DATE AND ISNULL(a.END_INSTRUCT_DATE,GETDATE()) AND b.name = 'MES_FSt030_Pass'
   where 1 = 1
 AND a.USE_YN = 'Y'
 AND a.START_INSTRUCT_DATE IS NOT NULL
 AND FORMAT(a.INSTRUCT_DATE, 'yyyy-MM-dd')　=
 (
  SELECT  CASE 
  WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
  THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
  ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END 
  )
   group by a.ID
   )수집불량 ON B.ID = 수집불량.ID
    LEFT JOIN 	
      (	 
       SELECT a.ID
       ,SUM(b.OK_QTY) AS OK_QTY
       ,SUM(b.NG_QTY) AS NG_QTY
       ,SUM(b.TOTAL_QTY) AS TOTAL_QTY
         FROM [dbo].[PRODUCTION_INSTRUCT] a
         LEFT JOIN [dbo].[PRODUCTION_RESULT] b on a.ID = b.PRODUCTION_INSTRUCT_ID
         where 1 = 1
    AND	a.USE_YN = 'Y'
    AND	b.USE_YN = 'Y'
    AND a.START_INSTRUCT_DATE IS NOT NULL
    AND FORMAT(a.INSTRUCT_DATE, 'yyyy-MM-dd')　=
    (
     SELECT  CASE 
     WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
     THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
     ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END 
     )
         GROUP BY a.id
      )실적수량 ON B.ID = 실적수량.ID
) AA;";



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
