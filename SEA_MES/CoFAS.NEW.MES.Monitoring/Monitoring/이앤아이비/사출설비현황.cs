using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Monitoring
{

    public partial class 사출설비현황 : Form
    {
        public System.Threading.Timer _timer;

        string strcon = "Server = 222.113.146.82,11433; Database = HS_MES;UID = sa;PWD = coever1191!";
        //string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
        //string strcon = "Server = 192.168.10.126; Database = HS_MES;UID = sa;PWD = coever1191!";
        public 사출설비현황(string pStrcon)
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

        private void 사출설비현황_Load(object sender, EventArgs e)
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
                    //ucCorechipsWorkStatus_이앤아이비_사출타이틀 uc_title = new ucCorechipsWorkStatus_이앤아이비_사출타이틀();

                    //uc_title.Font = this.Font;
                    //uc_title.Location = new Point(0, 0);
                    //uc_title.Margin = new Padding(0, 0, 0, 0);
                    //uc_title.Size = new Size(1910, panel3.Height);
                    //panel3.Controls.Add(uc_title);

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

                        uc.Size = new Size(1910, he);
                        //uc.Size = tableLayoutPanel2.Size;

                        panel2.Controls.Add(uc);

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
                string strSql_Insert = $@"WITH LatestInstruct AS (
    SELECT 
        B.EQUIPMENT_ID, 
        B.PRODUCTION_PLAN_ID, 
        B.STOCK_MST_ID,
		B.ID,
        ROW_NUMBER() OVER (PARTITION BY B.EQUIPMENT_ID ORDER BY B.REG_DATE DESC) AS rn
    FROM PRODUCTION_INSTRUCT B
    WHERE B.COMPLETE_YN != 'Y'
    AND CONVERT(CHAR(23), B.REG_DATE, 23) = CONVERT(CHAR(23), GETDATE(), 23)
)
SELECT 
    A.NAME AS EQUIPMENT_NAME,
    ISNULL(D.NAME, '-') AS STOCK_MST_NAME,
    ISNULL(D.STANDARD, '-') AS STOCK_MST_STANDARD,
    ISNULL(D.OUT_CODE, '-') AS STOCK_MST_CODE,
    ISNULL(C.PLAN_QTY, 0) AS PLAN_QTY,
    ISNULL(E.QTY, 0) AS RESULT_QTY,
    CASE
        WHEN E.START_DATE IS NOT NULL AND E.END_DATE IS NULL THEN '생산중'
        ELSE '생산대기'
    END AS EQUIPMENT_OPERATION
FROM EQUIPMENT A
LEFT JOIN LatestInstruct B ON A.ID = B.EQUIPMENT_ID AND B.rn = 1
LEFT JOIN PRODUCTION_PLAN C ON B.PRODUCTION_PLAN_ID = C.ID
LEFT JOIN STOCK_MST D ON B.STOCK_MST_ID = D.ID
LEFT JOIN PRODUCTION_RESULT E ON B.ID = E.PRODUCTION_INSTRUCT_ID
    AND E.START_DATE IS NOT NULL
    AND E.END_DATE IS NULL
WHERE A.COLUMN1 != ''
AND A.NAME != '11'
AND A.TYPE = 'CD14001'
ORDER BY A.ID ASC;";



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
