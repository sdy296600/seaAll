using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.POP.Function;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class frmInstructSelect : Form
    {
        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void SetDoubleBuffered_Control(Control.ControlCollection controls)

        {
            foreach (Control item in controls)
            {
                if (item.Controls.Count != 0)
                {
                    SetDoubleBuffered_Control(item.Controls);
                }

                SetDoubleBuffered(item);
            }
        }
        #endregion

        #region ○ 폼 이동

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


        private void tspMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                this.WindowState = FormWindowState.Maximized;
                this.Refresh();
                this.Invalidate();
                this.SetStyle(ControlStyles.ResizeRedraw, true);
            }
        }

        #endregion

        #region ○ 변수선언
        public My_Settings _MY = null;

        private UserEntity _UserEntity = new UserEntity();

        public int? _ProductionInstruct_id = null;
        #endregion

        #region ○ 생성자

        public frmInstructSelect(UserEntity pUserEntity)
        {
            InitializeComponent();
            _UserEntity = pUserEntity;


            _MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                dtp_startInstructDate.DateTime = DateTime.Parse(DateTime.Now.ToShortDateString());
                dtp_endInstructDate.DateTime = dtp_startInstructDate.DateTime.AddDays(1).AddHours(23).AddMinutes(59);
                dtp_startInstructDate._pDateEdit.Properties.Mask.EditMask = "yyyy-MM-dd";
                dtp_startInstructDate._pDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtp_endInstructDate._pDateEdit.Properties.Mask.EditMask = "yyyy-MM-dd";
                dtp_endInstructDate._pDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;

                Line_Setting();

                //DataTable  dt1 = new MS_DBClass(_MY).Spread_ComobBox("CODE","CD14","");

                //comboBox1.Items.Add(new ComboboxItem() {Text = "ALL",Value="" });
                //foreach (DataRow item in dt1.Rows)
                //{
                //    ComboboxItem coitme = new ComboboxItem(){ };
                //    coitme.Text = item["CD_NM"].ToString();
                //    coitme.Value = item["CD"].ToString();

                //    comboBox1.Items.Add(coitme);
                //}
                //comboBox1.SelectedIndex = 0;

                //DataTable  dt2 = new MS_DBClass(_MY).Spread_ComobBox("CODE","SD08","");
                //comboBox2.Items.Add(new ComboboxItem() { Text = "all", Value = "" });
                //foreach (DataRow item in dt2.Rows)
                //{
                //    ComboboxItem coitme = new ComboboxItem();
                //    coitme.Text = item["CD_NM"].ToString();
                //    coitme.Value = item["CD"].ToString();

                //    comboBox2.Items.Add(coitme);
                //}
                //comboBox2.SelectedIndex = 0;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }



        }

        #endregion
        private void Line_Setting()
        {
            try
            {
                int x = 0;

                int y = 1;

                int btnY = panel2.Height / 3 - 5;

                int btnX = panel2.Width;

                DataTable  dt = new MS_DBClass(_MY).work_Setting();

                foreach (DataRow item in dt.Rows)
                {
                    Button btn = new Button();

                    btn.Name = item["code"].ToString().Trim();

                    btn.Text = item["code_name"].ToString().Trim();


                    //btn.Font = this.Font;

                    if (x > btnX - 180)
                    {
                        y += btnY;
                        x = 0;
                    }

                    btn.Location = new Point(1 + x, y);

                    x += 180;

                    btn.Size = new Size(180, btnY);

                    btn.Click += Line_Click;

                    btn.TextAlign = ContentAlignment.MiddleCenter;

                    btn.ForeColor = Color.White;
                    btn.BackColor = Color.FromArgb(116, 142, 172);

                    panel2.Controls.Add(btn);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void instruct_Setting(string work)
        {
            try
            {
                panel1.Controls.Clear();

                DataTable  dt = new MS_DBClass(_MY).instruct_Setting(dtp_startInstructDate.DateTime,dtp_endInstructDate.DateTime,work);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];

                    int loc = 80 * i;

                    Button btn = new Button();

                    btn.Name = row["ID"].ToString().Trim();

                    btn.Text = "순번 : " + row["SORT"].ToString().Trim().PadRight(3);

                    btn.Text += " 생산계획코드 : " + row["OUT_CODE"].ToString().Trim().PadRight(30);

                    btn.Text += " 지시일자 : " + row["INSTRUCT_DATE"].ToString().Trim().PadRight(12);

                    btn.Text += " 지시수량 : " + Convert.ToInt32(row["INSTRUCT_QTY"]).ToString().Trim().PadRight(5);

                    btn.Text += "\r\n​";

                    btn.Text += "제품코드 : " + row["STOCK_OUT_CODE"].ToString().Trim().PadRight(20);

                    btn.Text += " 제품명 : " + row["NAME"].ToString().Trim().PadRight(20);

                    btn.Text = btn.Text.Trim();

                    btn.Location = new Point(1, 1 + loc);

                    btn.Size = new Size((panel1.Size.Width - 50), 80);

                    btn.Click += ProductionInstruct_Click;
                    btn.TextAlign = ContentAlignment.MiddleLeft;

                    btn.ForeColor = Color.White;

                    //if (row["COMPLETE_YN"].ToString() == "Y")
                    //{
                    //    btn.BackColor = Color.SeaGreen;
                    //}
                    //else
                    //{
                    if (row["START_INSTRUCT_DATE"] == DBNull.Value)
                    {
                        btn.BackColor = Color.FromArgb(216, 110, 110);
                    }
                    else
                    {
                        if (row["END_INSTRUCT_DATE"] == DBNull.Value)
                        {
                            btn.BackColor = Color.DarkGray;
                 
                        }
                        else
                        {
                            btn.BackColor = Color.SeaGreen;
                        }
                    }
                    //}

                    panel1.Controls.Add(btn);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        private void Line_Click(object sender, EventArgs e)
        {
            Button btn1 = sender as Button;

            instruct_Setting(btn1.Name);
        }
        private void ProductionInstruct_Click(object sender, EventArgs e)
        {
            try
            {

                Button btn = sender as Button;

                //_productionInstruct = JsonConvert.DeserializeObject<List<ProductionInstructEntity>>(result)[0];  
                _ProductionInstruct_id = Convert.ToInt32(btn.Name);

                this.DialogResult = DialogResult.OK;

                this.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            instruct_Setting("");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}





