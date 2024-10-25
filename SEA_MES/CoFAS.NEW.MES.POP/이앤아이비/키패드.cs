using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.POP.Function;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class 키패드 : Form
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
                //this.WindowState = FormWindowState.Maximized;
                //this.Refresh();
                //this.Invalidate();
                this.SetStyle(ControlStyles.ResizeRedraw, true);
            }
        }

        #endregion

        #region ○ 변수선언
        //public My_Settings _MY = null;

       // private UserEntity _UserEntity = new UserEntity();

        public string _code = string.Empty;
        #endregion

        #region ○ 생성자

        public 키패드( )
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);

            //this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                txt_키패드.Font = new Font("맑은 고딕", 24, FontStyle.Bold);

                btn_0.Click += button_Click;
                btn_1.Click += button_Click;
                btn_2.Click += button_Click;
                btn_3.Click += button_Click;
                btn_4.Click += button_Click;
                btn_5.Click += button_Click;
                btn_6.Click += button_Click;
                btn_7.Click += button_Click;
                btn_8.Click += button_Click;
                btn_9.Click += button_Click;

                btn_C.Click += btn_C_Click;
                btn_del.Click += btn_del_Click;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }

        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_키패드.Text.Length == 10)
                {
                    return;
                }

                Button btn = sender as Button;

                txt_키패드.Text += btn.Text;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void btn_del_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_키패드.Text.Length != 0)
                {
                    txt_키패드.Text = txt_키패드.Text.Substring(0, txt_키패드.Text.Length - 1);
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void btn_C_Click(object sender, EventArgs e)
        {
            try
            {
                txt_키패드.Text = "";
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        #endregion


        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _code = txt_키패드.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}





