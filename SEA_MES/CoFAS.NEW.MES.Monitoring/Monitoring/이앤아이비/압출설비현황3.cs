using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Monitoring
{

    public partial class 압출설비현황3 : Form
    {
        public System.Threading.Timer _timer;

        string strcon = "Server = 222.113.146.82,11433; Database = HS_MES;UID = sa;PWD = coever1191!";
        //string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
        //string strcon = "Server = 192.168.10.126; Database = HS_MES;UID = sa;PWD = coever1191!";
        public 압출설비현황3(string pStrcon)
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

        private void 압출설비현황3_Load(object sender, EventArgs e)
        {

            this.Dock = DockStyle.None;

            this.Dock = DockStyle.Fill;

            _timer = new System.Threading.Timer(CallBack);

            _timer.Change(0, 10 * 1000);

        }
        public void CallBack(Object state)
        {
            try
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    ucCorechipsWorkStatus_이앤아이비_압출타이틀 uc_title = new ucCorechipsWorkStatus_이앤아이비_압출타이틀();

                    uc_title.Font = this.Font;
                    uc_title.Location = new Point(0, 0);
                    uc_title.Margin = new Padding(0, 0, 0, 0);
                    uc_title.Size = new Size(1910, panel3.Height);
                    panel3.Controls.Add(uc_title);

                    DateTime time = DateTime.Now;

                    //label4.Text = $"Month : ({time.Year}-1 ~ {time.Month})";

                    DataSet ds = Get_설비가동현황();

                    DataTable dt = ds.Tables[0];


                    int he = panel2.Size.Height / dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        DataRow dr = dt.Rows[i];

                        ucCorechipsWorkStatus_이앤아이비_압출 uc = new ucCorechipsWorkStatus_이앤아이비_압출(dr);

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
                string strSql_Insert = $@"WITH LatestData AS (
    SELECT 
        B2.EXT_ID, 
        B2.PV_STATUS, 
        B2.PV_LOAD, 
        B2.PV_RPM,
		B2.PV_TEMP1,
		B2.PV_TEMP2,
		B2.PV_TEMP3,
		B2.PV_TEMP4,
		B2.PV_TEMP5,
		B2.PV_TEMP6,
		B2.PV_TEMP7,
		B2.PV_TEMP8,
        B2.REG_DATE,
        ROW_NUMBER() OVER (PARTITION BY B2.EXT_ID ORDER BY B2.REG_DATE DESC) AS rn
    FROM CCS_EXTRUSION_SPC B2
)
SELECT 
    A.NAME AS EQUIPMENT_NAME,
    '-' AS MODEL_NAME,
    '-' AS LOT_NUMBER,
    ISNULL(B.PV_STATUS, 0) AS STATUS,
    ISNULL(B.PV_LOAD, 0) AS LOAD,
    ISNULL(B.PV_RPM, 0) AS RPM,
    ISNULL(B.PV_TEMP1, 0) AS 'CYL-1',
    ISNULL(B.PV_TEMP2, 0) AS 'CYL-2',
    ISNULL(B.PV_TEMP3, 0) AS 'CYL-3',
    ISNULL(B.PV_TEMP4, 0) AS 'CYL-4',
    ISNULL(B.PV_TEMP5, 0) AS 'ADT-1',
    ISNULL(B.PV_TEMP6, 0) AS 'DIES-1',
    ISNULL(B.PV_TEMP7, 0) AS 'DIES-2',
    ISNULL(B.PV_TEMP8, 0) AS 'DIES-3'
FROM EQUIPMENT A
LEFT JOIN LatestData B ON A.COLUMN1 = B.EXT_ID AND B.rn = 1
WHERE A.COLUMN1 != ''
AND A.NAME != '11'
AND A.TYPE = 'CD14003'
ORDER BY A.ID ASC;;";



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
