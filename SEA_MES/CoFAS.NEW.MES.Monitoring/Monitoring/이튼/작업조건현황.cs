using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Monitoring
{

    public partial class 작업조건현황 : Form
    {
        public System.Threading.Timer _timer;

        string strcon = $"Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
       // string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
        public 작업조건현황(string pStrcon)
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

        private void 작업조건현황_Load(object sender, EventArgs e)
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

                    DataTable dt1 = Get_작업조건현황().Tables[0];

                    panel2.Controls.Clear();

                    int he = (panel2.Size.Height/ (dt1.Rows.Count+1));
                    _uc_Date _Uc = new _uc_Date(dt1);
                    _Uc.Location = new Point(0, 0);
                    _Uc.Size = new Size(panel2.Size.Width, he);
                    panel2.Controls.Add(_Uc);

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        _uc_Date _Ucs = new _uc_Date(dt1.Rows[i]);
                        _Ucs.Location = new Point(0, (i + 1) * he);
                        _Ucs.Size = new Size(panel2.Size.Width, he);
                        panel2.Controls.Add(_Ucs);
                    }

                }));
            }
            catch (Exception pExcption)
            {

            }

        }

        public DataSet Get_작업조건현황()
        {
          
            try
            {
               
                SqlConnection con = new SqlConnection(strcon);
                string strSql_Insert = $@"select B.code_name as 공정
                                      ,A.code_name as 대분류
                                      ,A.code_etc1 as 소분류
                                      ,C.VALUE     as 값
                                      ,C.READ_DATE as 수집시간
                                  from [dbo].[Code_Mst] A
                                  INNER JOIN [dbo].[Code_Mst] B ON A.code_type = B.code
                                   LEFT JOIN 
                                   (
                                    SELECT A.*
                                    　FROM [dbo].[OPC_MST] A
                                    　INNER JOIN 
                                    　 (
                                    　 SELECT NAME ,MAX(READ_DATE) AS READ_DATE
                                    　 FROM [dbo].[OPC_MST]
                                    　GROUP BY NAME 
                                    　 ) B ON A.NAME = B.NAME AND  A.READ_DATE  = B.READ_DATE
                                   ) C ON  A.code_etc1 = C.NAME
                                  where 1=1
                                  and A.code_type like '%PR%'
                                  and A.code_type != 'PR00'
                                  ORDER BY A.code";



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
