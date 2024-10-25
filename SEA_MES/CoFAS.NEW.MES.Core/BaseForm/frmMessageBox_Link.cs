using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class frmMessageBox_Link : Form
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
            set {
                    _Message.Text = value;

                string[] str = _Message.Text.Split('\n');
                if(str.Length > 1)
                {
                    _Message.Links.Add(str[0].Length + 1, str[1].Length, str[1]);
                }

                }
        }

        public string Title
        {
            get { return _Title.Text; }
            set { _Title.Text = "  - " + value + " -  "; }
        }
               

        public frmMessageBox_Link()
        {
            InitializeComponent();
            
            this.Width = this.Width + Message.Length * 50;
            //_OK.Location = new Point(this.Width / 2 - 70, 100);
        }

        private void _OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _Message_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] str = _Message.Text.Split('\n');
            if (str.Length > 1)
                Process.Start(System.IO.Path.Combine(Application.StartupPath, str[1]));
        }
    }
}
