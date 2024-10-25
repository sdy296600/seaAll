using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Monitoring
{
    public partial class Main : Form
    {
        //DBClass dc = new DBClass();



        public Main()
        {
            CheckForIllegalCrossThreadCalls = false; //디버그 할때 크로스 쓰레드 방지

            InitializeComponent();

            this.Focus();
        }

        public void ShowForm<Tform>(string frmName) where Tform : Form, new()
        {
            foreach (var frm in this.MdiChildren)
            {
                if (frm.Name == frmName)
                {
                    frm.BringToFront();

                    frm.MaximizeBox = false;
                    frm.MinimizeBox = false;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Focus();
                    return;
                }
            }

            Tform tFrm = new Tform()
            {
                MaximizeBox = false,
                MinimizeBox = false,
                WindowState = FormWindowState.Maximized,
                MdiParent = this,

            };

            tFrm.BringToFront();
            tFrm.Focus();
            tFrm.Show();
        }

        public Form MdiChildrenShow(string formName)
        {

            CoFAS.NEW.MES.Monitoring.Properties.Settings.Default.START_FROM = formName;

            CoFAS.NEW.MES.Monitoring.Properties.Settings.Default.Save();

            foreach (var frm in this.MdiChildren)
            {
                frm.Close();
            }

            Type type = Type.GetType("CoFAS.NEW.MES.Monitoring." + formName);



            Form form = null;

            if (type != null)
            {

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.GetType() == type && frm.IsMdiChild)
                    {
                        frm.Dock = DockStyle.Fill;
                        frm.Left = 0;
                        frm.Top = 0;
                        frm.FormBorderStyle = FormBorderStyle.None;
                        frm.Activate();
                        frm.BringToFront();

                        form = frm;
                    }
                }

                if (form == null)
                {
                    form = (Form)Activator.CreateInstance(type);
                    form.MdiParent = this;
                    form.Dock = DockStyle.Fill;
                    form.Left = 0;
                    form.Top = 0;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Show();

                }


                //form.ShowDialog();
            }
            return null;
        }



        private void Main_Load(object sender, EventArgs e)
        {

            try
            {
                this.MinimumSize = this.Size;
                //MdiChildrenShow("Eaton_Station");
                MdiChildrenShow("Eaton3");
                //System.Threading.Timer _timer = new System.Threading.Timer(CallBack);

                //_timer.Change(0, 30 * 1000);
            }
            catch (Exception ex)
            {

            }

        }
        bool _bo = true;
        public void CallBack(Object state)
        {
            try
            {
               

                Invoke(new MethodInvoker(delegate ()
                {
                    if(_bo)
                    {
                        _bo = false;
                        MdiChildrenShow("Eaton2");
                    }
                    else
                    {
                        _bo = true;
                        MdiChildrenShow("Eaton_Station");
                    }


                }));
            }
            catch (Exception pExcption)
            {

            }

        }

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();

                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                this.WindowState = FormWindowState.Maximized;
                this.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var frm in this.MdiChildren)
            {
                frm.Close();
            }

            this.Close();
        }
    }
}






