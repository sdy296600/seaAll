using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CoFAS.NEW.MES.Monitoring
{

    public partial class 대성금형2 : Form
    {
        public System.Threading.Timer _timer;


        public 대성금형2()
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

        private void 대성금형2_Load(object sender, EventArgs e)
        {





            chart1.ChartAreas[0].AxisY.Interval = 50;
            chart2.ChartAreas[0].AxisY.Interval = 50;


            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Minimum = 0;


            chart1.ChartAreas[0].AxisX.Interval = 10;
            chart2.ChartAreas[0].AxisX.Interval = 10;

            int x = 3;
            int y = 3;

            panel2.Controls.Clear();



            SPC_DATA spc = new SPC_DATA();
            foreach (PropertyInfo item in typeof(SPC_DATA).GetProperties())
            {

                if (item.Name != "SPC_DATETIME")
                {
                    CheckBox check = new CheckBox();
                    check.Name = item.Name;
                    check.Text = item.GetValue(spc).ToString();
                    check.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
                    check.TextAlign = ContentAlignment.MiddleCenter;
                    check.Checked = true;
                    check.CheckedChanged += CheckBox_CheckedChanged;
                    if (x + 120 >= panel2.Size.Width - 800)
                    {
                        x = 3;
                        y += 15;
                        check.Location = new Point(x, y);
                    }
                    else
                    {
                        check.Location = new Point(x, y);
                    }


                    x += 120;

                    panel2.Controls.Add(check);
                }
            }

            CheckBox allcheck1 = new CheckBox();
            allcheck1.Name = "ALL1";
            allcheck1.Text = "ALL";
            allcheck1.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
            allcheck1.TextAlign = ContentAlignment.MiddleCenter;
            allcheck1.Checked = true;
            allcheck1.CheckedChanged += CheckBox_CheckedChanged;

            if (x + 120 >= panel2.Size.Width - 800)
            {
                x = 3;
                y += 15;
                allcheck1.Location = new Point(x, y);
            }
            else
            {
                allcheck1.Location = new Point(x, y);
            }

            panel2.Controls.Add(allcheck1);

            this.Dock = DockStyle.None;

            this.Dock = DockStyle.Fill;

            this.Font = new Font("맑은 고딕", 15, FontStyle.Bold);

            BTN_RESET.PerformClick();

            _timer = new System.Threading.Timer(CallBack2);

            _timer.Change(0, 60 * 1000);


            //CallBack(null);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Name == "ALL1")
            {
                foreach (Control item in panel2.Controls)
                {
                    if (item.GetType() == typeof(CheckBox))
                    {
                        CheckBox check = (CheckBox)item;
                        check.CheckedChanged -= CheckBox_CheckedChanged;
                        check.Checked = checkBox.Checked;
                        check.CheckedChanged += CheckBox_CheckedChanged;
                    }
                }

            }

            foreach (Control item in 제품박스.Controls)
            {
                RadioButton radio = item as RadioButton;
                if (radio.Checked)
                {
                    CallBack(radio.Name);
                }
            }

        }
        public void CallBack(string str)
        {
            try
            {
                chart1.Series.Clear();
                chart2.Series.Clear();

                chart1.ChartAreas[0].AxisY.Maximum = 20;
                chart1.ChartAreas[0].AxisY.Minimum = 2000;

                chart2.ChartAreas[0].AxisY.Maximum = 20;
                chart2.ChartAreas[0].AxisY.Minimum = 2000;

                DataSet ds = frmCorechipsWorkStatus(str);

                DataTable dt  =ds.Tables[0];

                Chart chart = null;
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName != "SPC_DATETIME")
                    {
                        if (col.ColumnName.Contains("CV") || col.ColumnName.Contains("SV"))
                        {
                            chart = chart2;
                        }
                        else
                        {
                            chart = chart1;
                        }

                        foreach (Control item in panel2.Controls)
                        {
                            if (item.GetType() == typeof(CheckBox))
                            {
                                CheckBox check = (CheckBox)item;

                                if (item.Name == col.ColumnName)
                                {
                                    if (check.Checked)
                                    {
                                        Series series = new Series();

                                        series.Name = item.Text;

                                        series.ChartType = SeriesChartType.StepLine;

                                        //series.ChartType = SeriesChartType.Area;

                                        series.BorderWidth = 3;

                                        if (chart.Series.FindByName(item.Text) == null)
                                        {

                                            chart.Series.Add(series);


                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                //if (Convert.ToInt32(dt.Rows[i][col.ColumnName]) != 0)
                                                //{
                                                    double vel = Convert.ToDouble(dt.Rows[i][col.ColumnName]);
                                                    if (chart.ChartAreas[0].AxisY.Maximum < vel)
                                                    {
                                                        chart.ChartAreas[0].AxisY.Maximum = (vel + 50);
                                                    }
                                                    if (chart.ChartAreas[0].AxisY.Minimum > vel)
                                                    {
                                                        chart.ChartAreas[0].AxisY.Minimum = (vel - 50);
                                                    }

                                                    chart.Series[item.Text].Points.AddXY(Convert.ToDateTime(dt.Rows[i]["SPC_DATETIME"]).ToString("HH:mm:ss"), dt.Rows[i][col.ColumnName].ToString());
                                                    //chart.Series[item.Text].Points.AddXY(i, dt.Rows[i][col.ColumnName].ToString());
                                                    if (i == 10)
                                                    {
                                                        chart.Series[item.Text].Points[chart.Series[item.Text].Points.Count-1].Label = item.Text;
                                                        chart.Series[item.Text].Points[chart.Series[item.Text].Points.Count - 1].Font = check.Font = new Font(this.Font.FontFamily, 9, FontStyle.Bold);
                                                        //chart.Series[item.Text].Points[i].LabelForeColor = chart.Series[item.Text].Color;
                                                    }
                                                //}
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(col.ColumnName);
                                        }
                                        // return;
                                    }
                                }
                            }
                        }



                    }
                }


                chart1.ChartAreas[0].AxisX.Maximum = dt.Rows.Count;
                chart1.ChartAreas[0].AxisX.Interval = dt.Rows.Count / 10;
                chart1.ChartAreas[0].AxisY.Interval = (chart1.ChartAreas[0].AxisY.Maximum - chart1.ChartAreas[0].AxisY.Minimum) / 10;
                chart2.ChartAreas[0].AxisX.Maximum = dt.Rows.Count;
                chart2.ChartAreas[0].AxisX.Interval = dt.Rows.Count / 10;
                chart2.ChartAreas[0].AxisY.Interval = (chart2.ChartAreas[0].AxisY.Maximum - chart2.ChartAreas[0].AxisY.Minimum) / 10;


            }
            catch (Exception pExcption)
            {
                MessageBox.Show(pExcption.ToString());
            }

        }


        public DataSet frmCorechipsWorkStatus(string id)
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



                    cmd.CommandText = $@"SELECT SPC_DATETIME
                      ,CV_001 
                      ,CV_002 
                      ,CV_003 
                      ,CV_004 
                      ,CV_005 
                      ,CV_006 
                      ,CV_007 
                      ,CV_008 
                      ,CV_009 
                      ,CV_010 
                      ,CV_011 
                      ,CV_012 
                      ,CV_013 
                      ,CV_014 
                      ,CV_015 
                      ,CV_016 
                      ,CV_017 
                      ,CV_018 
                      ,CV_019 
                      ,CV_020 
                      ,CV_021 
                      ,CV_022 
                      ,CV_023 
                      ,CV_024 
                      ,CV_025 
                      ,CV_026 
                      ,CV_027 
                      ,CV_028 
                      ,SV_008 
                      ,SV_009 
                      ,SV_010 
                      ,weight    AS '예측값'	
                      ,d.consume_qty      AS '목표중량'
                      ,d.consume_qty * 1.03 AS '상한치'                                      
                      ,d.consume_qty * 0.97 AS '하한치'                                       
                       FROM spc_data_weight_ai a
                      INNER JOIN spc_data b ON a.UNIQ_NO = B.UNIQ_NO AND DATE_FORMAT(SPC_DATETIME , '%Y-%m-%d') = '{일자.Value.ToString("yyyy-MM-dd")}'
                      INNER JOIN bom c ON a.stock_mst_code = c.stock_mst_code AND c.parent_id IS null
					  INNER JOIN bom d ON c.id = d.parent_id 
                      WHERE A.stock_mst_code = '{id}'";

            

                    cmd.CommandType = CommandType.Text;

                    //파라메터 선언
                    //cmd.Parameters.Add(new MySqlParameter("@p_ProductionInstructp_Id", MySqlDbType.Int32));


                    //값할당
                    //cmd.Parameters["@p_ProductionInstructp_Id"].Value = productionInstructpId;


                    // DB처리
                    DataSet dt = new DataSet();

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

        public DataTable GET_EQ()
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



                    cmd.CommandText = $@"SELECT 
                                               C.id
                                              ,C.name AS '설비명'
                                              FROM spc_data_weight_ai A
                                              INNER JOIN spc_data B ON A.UNIQ_NO = B.UNIQ_NO
                                              INNER JOIN equipment C ON C.id = B.INJ_NO
                                              WHERE DATE_FORMAT(B.SPC_DATETIME , '%Y-%m-%d') = '{일자.Value.ToString("yyyy-MM-dd")}'
                                              GROUP BY C.name
                                              ORDER BY C.name";



                    cmd.CommandType = CommandType.Text;

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
        private void BTN_RESET_Click(object sender, EventArgs e)
        {
            일자.Value = DateTime.Now;
        }

        private void 일자_ValueChanged(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart2.Series.Clear();
            설비박스.Controls.Clear();
            제품박스.Controls.Clear();

            DataTable dt =  GET_EQ();

            int x = 20;
            int y = 30;
            int xp = 120;
            foreach (DataRow item in dt.Rows)
            {
                RadioButton check = new RadioButton();
                check.Name = item["id"].ToString();
                check.Text = item["설비명"].ToString();
                check.Font = new Font(this.Font.FontFamily, 12, FontStyle.Bold);
                check.Size = new Size(120, 50);
                check.TextAlign = ContentAlignment.MiddleLeft;
                check.Location = new Point(x, y);
                check.CheckedChanged += 설비박스_CheckedChanged;

                x += xp;

                설비박스.Controls.Add(check);
            }
            if (dt.Rows.Count != 0)
            {
                (설비박스.Controls[0] as RadioButton).Checked = true;
            }
        }

        private void 설비박스_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                제품박스.Controls.Clear();

                DataTable dt =  GET_ST(radio.Name);

                int x = 20;
                int y = 30;
                int xp = 200;
                foreach (DataRow item in dt.Rows)
                {
                    RadioButton check = new RadioButton();
                    check.Name = item["code"].ToString();
                    check.Text = item["제품명"].ToString();
                    check.Font = new Font(this.Font.FontFamily, 12, FontStyle.Bold);
                    check.Size = new Size(200, 50);
                    check.TextAlign = ContentAlignment.MiddleLeft;
                    check.Location = new Point(x, y);
                    check.CheckedChanged += 제품박스_CheckedChanged;

                    x += xp;

                    제품박스.Controls.Add(check);
                }
                if (dt.Rows.Count != 0)
                {
                    (제품박스.Controls[0] as RadioButton).Checked = true;
                }
            }

        }
        private void 제품박스_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                CallBack(radio.Name);
            }

        }
        public DataTable GET_ST(string id)
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



                    cmd.CommandText = $@"SELECT 
                                        D.code
                                       ,D.name AS  '제품명'

                                        FROM spc_data_weight_ai A
                                        INNER JOIN spc_data B ON A.UNIQ_NO = B.UNIQ_NO
                                        INNER JOIN equipment C ON C.id = B.INJ_NO
                                        INNER JOIN stock_mst D ON A.stock_mst_code = D.code
                                        WHERE DATE_FORMAT(B.SPC_DATETIME , '%Y-%m-%d') = '{일자.Value.ToString("yyyy-MM-dd")}'
                                        AND C.id = {id}
                                        GROUP BY D.name
                                        ORDER BY D.name;";



                    cmd.CommandType = CommandType.Text;

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

        private void btn_auto_Click(object sender, EventArgs e)
        {
            if (일자박스.Enabled)
            {
                일자박스.Enabled = false;
                제품박스.Enabled = false;
                설비박스.Enabled = false;
                panel2.Enabled = false;
                btn_auto.Text = "STOP";
            }
            else
            {
                일자박스.Enabled = true;
                제품박스.Enabled = true;
                설비박스.Enabled = true;
                panel2.Enabled = true;
                btn_auto.Text = "AUTO";
            }


        }
        public void CallBack2(object str)
        {
            if (일자박스.Enabled == false)
            {
                BeginInvoke(new MethodInvoker(delegate ()
                {
                    foreach (Control item in 제품박스.Controls)
                    {
                        RadioButton radio = item as RadioButton;
                        if (radio.Checked)
                        {
                            CallBack(radio.Name);
                        }
                    }
                
                }));
            }
        }
    }

    public class SPC_DATA
    {

        public string CV_001 { get; set; } = "싸이클시간1";
        public string CV_002 { get; set; } = "형개완료위치";
        public string CV_003 { get; set; } = "사출시간1";
        public string CV_004 { get; set; } = "보압절환위치1";
        public string CV_005 { get; set; } = "쿠션위치1";
        public string CV_006 { get; set; } = "계량시간1";
        public string CV_007 { get; set; } = "계량위치1";
        public string CV_008 { get; set; } = "사출시간2";
        public string CV_009 { get; set; } = "보압절환위치2";
        public string CV_010 { get; set; } = "쿠션위치2";
        public string CV_011 { get; set; } = "계량시간2";
        public string CV_012 { get; set; } = "계량위치2";
        public string CV_013 { get; set; } = "온도1-0";
        public string CV_014 { get; set; } = "온도1-1";
        public string CV_015 { get; set; } = "온도1-2";
        public string CV_016 { get; set; } = "온도1-3";
        ////public string CV_017 { get; set; } = "온도1-4";
        ////public string CV_018 { get; set; } = "온도1-5";
        ////public string CV_019 { get; set; } = "온도1-6";
        public string CV_020 { get; set; } = "온도1-7";
        public string CV_021 { get; set; } = "온도2-0";
        public string CV_022 { get; set; } = "온도2-1";
        public string CV_023 { get; set; } = "온도2-2";
        public string CV_024 { get; set; } = "온도2-3";
        public string CV_025 { get; set; } = "온도2-4";
        public string CV_026 { get; set; } = "온도2-5";
        public string CV_027 { get; set; } = "온도2-6";
        public string CV_028 { get; set; } = "온도2-7";
        //public string SV_001 { get; set; } = "실린더온도(NH)";
        //public string SV_002 { get; set; } = "실린더온도(H1)";
        //public string SV_003 { get; set; } = "실린더온도(H2)";
        //public string SV_004 { get; set; } = "실린더온도(H3)";
        //public string SV_005 { get; set; } = "실린더온도(H4)";
        //public string SV_006 { get; set; } = "온도5";
        //public string SV_007 { get; set; } = "온도6";
        public string SV_008 { get; set; } = "사출1차속도";
        public string SV_009 { get; set; } = "사출2차속도";
        public string SV_010 { get; set; } = "사출3차속도";
        //public string SV_011 { get; set; } = "사출4차속도";
        //public string SV_012 { get; set; } = "사출5차속도";
        //public string SV_013 { get; set; } = "사출1차압력";
        //public string SV_014 { get; set; } = "사출2차압력";
        //public string SV_015 { get; set; } = "사출3차압력";
        //public string SV_016 { get; set; } = "사출4차압력";
        //public string SV_017 { get; set; } = "사출5차압력";
        //public string SV_018 { get; set; } = "사출1차위치";
        //public string SV_019 { get; set; } = "사출2차위치";
        //public string SV_020 { get; set; } = "사출3차위치";
        //public string SV_021 { get; set; } = "사출4차위치";
        //public string SV_022 { get; set; } = "사출5차위치";
        //public string SV_023 { get; set; } = "사출1차시간";
        //public string SV_024 { get; set; } = "사출2차시간";
        //public string SV_025 { get; set; } = "사출3차시간";
        //public string SV_026 { get; set; } = "사출4차시간";
        //public string SV_027 { get; set; } = "사출5차시간";
        //public string SV_028 { get; set; } = "보압1차속도";
        //public string SV_029 { get; set; } = "보압2차속도";
        //public string SV_030 { get; set; } = "보압3차속도";
        //public string SV_031 { get; set; } = "보압1차압력";
        //public string SV_032 { get; set; } = "보압2차압력";
        //public string SV_033 { get; set; } = "보압3차압력";
        //public string SV_034 { get; set; } = "보압1차시간";
        //public string SV_035 { get; set; } = "보압2차시간";
        //public string SV_036 { get; set; } = "보압3차시간";
        //public string SV_037 { get; set; } = "계량1차속도";
        //public string SV_038 { get; set; } = "계량2차속도";
        //public string SV_039 { get; set; } = "계량3차속도";
        //public string SV_040 { get; set; } = "계량4차속도";
        //public string SV_041 { get; set; } = "계량5차속도";
        //public string SV_042 { get; set; } = "계량1차압력";
        //public string SV_043 { get; set; } = "계량2차압력";
        //public string SV_044 { get; set; } = "계량3차압력";
        //public string SV_045 { get; set; } = "계량4차압력";
        //public string SV_046 { get; set; } = "계량5차압력";
        //public string SV_047 { get; set; } = "계량1차위치";
        //public string SV_048 { get; set; } = "계량2차위치";
        //public string SV_049 { get; set; } = "계량3차위치";
        //public string SV_050 { get; set; } = "계량4차위치";
        //public string SV_051 { get; set; } = "계량5차위치";
        //public string SV_052 { get; set; } = "계량1차배압";
        //public string SV_053 { get; set; } = "계량2차배압";
        //public string SV_054 { get; set; } = "계량3차배압";
        //public string SV_055 { get; set; } = "계량4차배압";
        //public string SV_056 { get; set; } = "계량5차배압";
        //public string SV_057 { get; set; } = "강제1속도";
        //public string SV_058 { get; set; } = "강제2속도";
        //public string SV_059 { get; set; } = "강제1압력";
        //public string SV_060 { get; set; } = "강제2압력";
        //public string SV_061 { get; set; } = "강제1위치";
        //public string SV_062 { get; set; } = "강제2위치";
        //public string SV_063 { get; set; } = "냉각시간";
        //public string SV_064 { get; set; } = "싸이클시간2";

        public string 예측값 { get; set; } = "예측값";
        public string 목표중량 { get; set; } = "목표중량";
        public string 상한치 { get; set; } = "상한치";
        public string 하한치 { get; set; } = "하한치";




    }

}
