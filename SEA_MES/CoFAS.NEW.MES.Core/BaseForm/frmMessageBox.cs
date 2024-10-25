using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class frmMessageBox : Form
    {
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

        public string Message
        {
            get { return _Message.Text; }
            set { _Message.Text = value; }
        }

        public string Title
        {
            get { return _Title.Text; }
            set { _Title.Text = "  - " + value + " -  "; }
        }
        public Color Title_BackColor
        {
            get { return _Title.BackColor; }
            set { _Title.BackColor = value; }
        }
        public frmMessageBox()
        {
            InitializeComponent();

            this.Width = this.Width + Message.Length * 50;
            //_OK.Location = new Point(this.Width / 2 - 70, 100);
        }

        private void _OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
    }
}
