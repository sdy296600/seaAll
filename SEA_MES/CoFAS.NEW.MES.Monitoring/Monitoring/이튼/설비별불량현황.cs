using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Monitoring
{

    public partial class 설비별불량현황 : Form
    {
        public System.Threading.Timer _timer;

        string strcon = $"Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
        //string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
        public 설비별불량현황(string pStrcon)
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

        private void 설비별불량현황_Load(object sender, EventArgs e)
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
                //BeginInvoke(new MethodInvoker(delegate ()

                Invoke(new MethodInvoker(delegate ()
                {

                    DataSet ds = Get_설비별불량현황();

                    if (ds != null && ds.Tables.Count != 0)
                    {
         
                        DataTable dt1 = ds.Tables[0];

                        불량율1.Text = Convert.ToDecimal(dt1.Rows[0]["불량율"]).ToString("F0") + "%"; //0.1% 0.11일때 노란색 0.11보다 크면 빨간색
                        if(Convert.ToDouble(dt1.Rows[0]["불량율"].ToString()) > 0.11)
                        {
                            불량율1.BackColor = Color.Red;
                        }
                        else if (Convert.ToDouble(dt1.Rows[0]["불량율"].ToString()) <= 0.1)
                        {
                            불량율1.BackColor = Color.FromArgb(102, 255, 51);
                        }
                        else if(Convert.ToDouble(dt1.Rows[0]["불량율"].ToString()) <= 0.11)
                        {
                            불량율1.BackColor = Color.Yellow;
                        }

                        불량율2.Text = Convert.ToDecimal(dt1.Rows[1]["불량율"]).ToString("F0") + "%"; //0.1%
                        if (Convert.ToDouble(dt1.Rows[1]["불량율"].ToString()) > 0.11)
                        {
                            불량율2.BackColor = Color.Red;
                        }
                        else if (Convert.ToDouble(dt1.Rows[1]["불량율"].ToString()) <= 0.1)
                        {
                            불량율2.BackColor = Color.FromArgb(102, 255, 51);
                        }
                        else if (Convert.ToDouble(dt1.Rows[1]["불량율"].ToString()) <= 0.11)
                        {
                            불량율2.BackColor = Color.Yellow;
                        }

                        불량율3.Text = Convert.ToDecimal(dt1.Rows[2]["불량율"]).ToString("F0") + "%"; //0.1%
                        if (Convert.ToDouble(dt1.Rows[2]["불량율"].ToString()) > 0.11)
                        {
                            불량율3.BackColor = Color.Red;
                        }
                        else if (Convert.ToDouble(dt1.Rows[2]["불량율"].ToString()) <= 0.1)
                        {
                            불량율3.BackColor = Color.FromArgb(102, 255, 51);
                        }
                        else if (Convert.ToDouble(dt1.Rows[2]["불량율"].ToString()) <= 0.11)
                        {
                            불량율3.BackColor = Color.Yellow;
                        }

                        불량율4.Text = Convert.ToDecimal(dt1.Rows[3]["불량율"]).ToString("F0") + "%"; //0.1%
                        if (Convert.ToDouble(dt1.Rows[3]["불량율"].ToString()) > 0.11)
                        {
                            불량율4.BackColor = Color.Red;
                        }
                        else if (Convert.ToDouble(dt1.Rows[3]["불량율"].ToString()) <= 0.1)
                        {
                            불량율4.BackColor = Color.FromArgb(102, 255, 51);
                        }
                        else if (Convert.ToDouble(dt1.Rows[3]["불량율"].ToString()) <= 0.11)
                        {
                            불량율4.BackColor = Color.Yellow;
                        }

                        불량율5.Text = Convert.ToDecimal(dt1.Rows[4]["불량율"]).ToString("F0") + "%"; //0.2% 0.22일때 노란색 0.22보다 크면 빨간색
                        if (Convert.ToDouble(dt1.Rows[4]["불량율"].ToString()) > 0.22)
                        {
                            불량율5.BackColor = Color.Red;
                        }
                        else if (Convert.ToDouble(dt1.Rows[4]["불량율"].ToString()) <= 0.2)
                        {
                            불량율5.BackColor = Color.FromArgb(102, 255, 51);
                        }
                        else if (Convert.ToDouble(dt1.Rows[4]["불량율"].ToString()) <= 0.22)
                        {
                            불량율5.BackColor = Color.Yellow;
                        }

                        불량율6.Text = Convert.ToDecimal(dt1.Rows[5]["불량율"]).ToString("F0") + "%"; //1.73% 1.903일때 노란색 1.903보다 크면 빨간색
                        if (Convert.ToDouble(dt1.Rows[5]["불량율"].ToString()) > 1.903)
                        {
                            불량율6.BackColor = Color.Red;
                        }
                        else if (Convert.ToDouble(dt1.Rows[5]["불량율"].ToString()) <= 1.73)
                        {
                            불량율6.BackColor = Color.FromArgb(102, 255, 51);
                        }
                        else if (Convert.ToDouble(dt1.Rows[5]["불량율"].ToString()) <= 1.903)
                        {
                            불량율6.BackColor = Color.Yellow;
                        }

                        DataTable dt2 = ds.Tables[1];

                        DataRow dr = dt2.Rows[0];

                        lab_생산아이템.Text = $"{dr[1].ToString()}";

                        lbl_Today.Text = $"Date : ({DateTime.Now.ToString("yyyy-MM-dd")})";
                    }

                }));
            }
            catch (Exception pExcption)
            {

            }

        }

        public DataSet Get_설비별불량현황()
        {
          
            try
            {
       
                //string strcon = "Server = 192.168.10.126; Database = HS_MES;UID = sa;PWD = coever1191!";
                SqlConnection con = new SqlConnection(strcon);
                string strSql_Insert = $@"
                                SET ANSI_WARNINGS OFF
　　　　　　　　　　　　　　　　SET ARITHIGNORE ON
　　　　　　　　　　　　　　　　SET ARITHABORT OFF
　　　　　　　　　　　　　　　　SELECT A.COLUMN1          AS 공정
 　　　　　　　　　           　 ,A.NAME                  AS 공정명
              
　　　　　　　　　　　　　　　　,ISNULL((COUNT(C.ID)/(COUNT(B.ID) + COUNT(C.ID)))*100,0) AS 불량율
　　　　　　　　　　　　　　　　FROM [dbo].[EQUIPMENT] A
　　　　　　　　　　　　　　　　LEFT JOIN [dbo].[OPC_MST] B ON A.OUT_CODE+'_Pass' = B.NAME AND B.READ_DATE BETWEEN 
                                (SELECT  CASE 
                                 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                 THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                 ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END + ' 08:30')
                                AND 
                                (SELECT  CASE 
                                 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                 THEN FORMAT(DATEADD(DAY ,1, GETDATE()), 'yyyy-MM-dd')
                                 ELSE FORMAT(GETDATE(), 'yyyy-MM-dd')　END + ' 08:30')
　　　　　　　　　　　　　　　　LEFT JOIN [dbo].[OPC_MST] C ON A.OUT_CODE+'_Fail' = C.NAME AND C.READ_DATE BETWEEN 
                                (SELECT  CASE 
                                 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                 THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                 ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END + ' 08:30')
                                AND 
                                (SELECT  CASE 
                                 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                 THEN FORMAT(DATEADD(DAY ,1, GETDATE()), 'yyyy-MM-dd')
                                 ELSE FORMAT(GETDATE(), 'yyyy-MM-dd')　END + ' 08:30')
                                 WHERE A.ID IN(4,7,12,14,15,17)
                                 GROUP BY A.COLUMN1,A.NAME,A.ID
                                 ORDER BY A.ID;

                                         select 
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
                                          ORDER BY B.START_INSTRUCT_DATE DESC;";



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
