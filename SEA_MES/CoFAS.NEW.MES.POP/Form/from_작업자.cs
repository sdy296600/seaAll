using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
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
    public partial class from_작업자 : Form
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
             
            }
        }

        #endregion

        #region ○ 변수선언
        public My_Settings _MY =  utility.My_Settings_Get();

        private UserEntity _UserEntity = new UserEntity();


        #endregion

        #region ○ 생성자

        public string _code = string.Empty ;
        public from_작업자(string code)
        {
            InitializeComponent();
            _code = code;
              Load += new EventHandler(Form_Load);

            this.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                작업자_Setting();
 
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        #endregion
        private void 작업자_Setting()
        {
            try
            {
                int x = 0;

                int y = 1;

                int btnY = panel1.Height / 3 - 5;

                int btnX = panel1.Width;

                string str = $@"SELECT 
                                 code       as  ID
                                ,code_name  as   NAME
                                from [dbo].[Code_Mst]
                                where code_type = '{_code}'";
                DataTable dt = new CoreBusiness().SELECT(str);

                foreach (DataRow item in dt.Rows)
                {
                    Button btn = new Button();

                    btn.Name = item["ID"].ToString().Trim();

                    btn.Text = item["NAME"].ToString().Trim();


                    btn.Font = new Font(this.Font.Name, 12, FontStyle.Bold);
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    if (x > btnX - 210)
                    {
                        y += btnY;
                        x = 0;
                    }

                    btn.Location = new Point(1 + x, y);

                    x += 210;

                    btn.Size = new Size(210, btnY);

                    btn.Click += 작업자_Click;

                    btn.TextAlign = ContentAlignment.MiddleCenter;

                    btn.ForeColor = Color.White;
                    btn.BackColor = Color.FromArgb(116, 142, 172);

                    panel1.Controls.Add(btn);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void 작업자_Click(object sender, EventArgs e)
        {
            try
            {

                _code = (sender as Button).Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
    
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        
    }
}





