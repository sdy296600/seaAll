using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Upload
{
    public partial class frmBase : Form
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
        #endregion


        public frmBase()
        {
            InitializeComponent();

            this.BackColor = Color.White;
        }

        //#region ○ 폼 이동

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void Form_Moving(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();

                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);

                this.Refresh();
            }
        }

        //#endregion

        #region ○ Form_Load
        private void Form_Load(object pSender, EventArgs pEventArgs)
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.ResizeRedraw = true;
            this.ControlAdded += Form_ControlAdded;
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

        public void DisplayMessage(string pMessage)
        {
            DateTime dt = DateTime.Now;

           string message = string.Format("[{0}:{1}:{2}] {3}\r\n", dt.ToString("HH"), dt.ToString("mm"), dt.ToString("ss"), pMessage);

            BeginInvoke(new MethodInvoker(delegate ()
            {
                _pMessage.Text = message;
            }));

        }

     
    }
}