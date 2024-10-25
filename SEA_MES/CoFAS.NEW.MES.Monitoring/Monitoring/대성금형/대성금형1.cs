using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Monitoring
{

    public partial class 대성금형1 : Form
    {
        public System.Threading.Timer _timer;


        public 대성금형1()
        {
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
        private void Form_ControlAdded(object sender, ControlEventArgs e)
        {
            try
            {
                Control control = sender as Control;

                SetDoubleBuffered(control);
            }
            catch (Exception err)
            {

            }
        }
        private void 대성금형1_Load(object sender, EventArgs e)
        {


            this.Dock = DockStyle.None;

            this.Dock = DockStyle.Fill;

            this.Font = new Font("맑은 고딕", 15,FontStyle.Bold);
            //CallBack(null);
            _timer = new System.Threading.Timer(CallBack);

            _timer.Change(0, 30 * 1000);


        }
        public void CallBack(Object state)
        {
            try
            {
                BeginInvoke(new MethodInvoker(delegate ()
                {


                    DataTable db = frmCorechipsWorkStatus();

                    if (db == null)
                    {
                        return;
                    }

                    panel1.Controls.Clear();

                    int he = panel1.Size.Height / (db.Rows.Count+1);

                    ucCorechipsWorkStatus fuc = new ucCorechipsWorkStatus();

                    fuc.Font = this.Font;

                    fuc.Location = new Point(0, 0);

                    fuc.Margin = new Padding(0, 0, 0, 0);

                    fuc.Size = new Size(panel1.Size.Width, he);

                    fuc.Font = this.Font;

                    panel1.Controls.Add(fuc);

                    for (int i = 0; i < db.Rows.Count; i++)
                    {

                        DataRow dr = db.Rows[i];

                        ucCorechipsWorkStatus uc = new ucCorechipsWorkStatus(dr);

                        int loc = he * (i+1);

                        uc.Font = this.Font;

                        uc.Location = new Point(0, loc);

                        uc.Margin = new Padding(0, 0, 0, 0);

                        uc.Size = new Size(panel1.Size.Width, he);

                        panel1.Controls.Add(uc);

                    }

                }));
            }
            catch (Exception pExcption)
            {

            }

        }


        public DataTable frmCorechipsWorkStatus()
        {
            try
            {
                string con = "Server = 211.221.27.249; Port = 13306; Database = coplatform; UID = root; PWD = developPassw@rd";

                using (MySqlConnection conn = new MySqlConnection(con))
                {

                    MySqlDataAdapter DBAdapter = new MySqlDataAdapter();

                    MySqlCommand cmd = new MySqlCommand();

                    DBAdapter.SelectCommand = cmd;

                    cmd.Connection = conn;

                    cmd.CommandText = "SP_frmCorechipsWorkStatus";

                    cmd.CommandType = CommandType.StoredProcedure;

                    //파라메터 선언
                    //cmd.Parameters.Add(new MySqlParameter("@p_ProductionInstructp_Id", MySqlDbType.Int32));


                    //값할당
                    //cmd.Parameters["@p_ProductionInstructp_Id"].Value = productionInstructpId;


                    // DB처리
                    DataTable dt = new DataTable();

                    conn.Open();

                    DBAdapter.Fill(dt);

                    conn.Close();

                    return dt;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

    }
}
