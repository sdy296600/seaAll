using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Monitoring
{
    public partial class 이앤아이비 : Form
    {
        //DBClass dc = new DBClass();
        //string strcon = $"Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
        string strcon = "Server = 222.113.146.82,11433; Database = HS_MES;UID = sa;PWD = coever1191!";
        System.Threading.Timer _timer;

        public 이앤아이비()
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
                    form = (Form)Activator.CreateInstance(type,new object[] {strcon});
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



        private void 이앤아이비_Load(object sender, EventArgs e)
        {

            try
            {
                this.MinimumSize = this.Size;
                //MdiChildrenShow("Eaton_Station");
                //MdiChildrenShow("Eaton3");
                //System.Threading.Timer _timer = new System.Threading.Timer(CallBack);

                //_timer.Change(0, 30 * 1000);
            }
            catch (Exception ex)
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

        private void 생산진행현황ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChildrenShow("생산진형현황");
        }

        private void 작업조건현황ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChildrenShow("작업조건현황");
        }

        private void 설비가동현황ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChildrenShow("설비가동현황");
        }

        private void 설비별불량현황ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChildrenShow("설비별불량현황");
        }


        private void 자동실행ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (사출설비현황ToolStripMenuItem.Enabled)
            {
                사출설비현황ToolStripMenuItem.Enabled = false;
                압출설비현황ToolStripMenuItem.Enabled = false;
                압출설비현황3ToolStripMenuItem.Enabled = false;

                no = 0;
                _timer = new System.Threading.Timer(CallBack);

                _timer.Change(0, 30 * 1000);
            }
            else
            {
                사출설비현황ToolStripMenuItem.Enabled = true;
                압출설비현황ToolStripMenuItem.Enabled = true;
                압출설비현황3ToolStripMenuItem.Enabled = true;

                _timer.Dispose();
                _timer = null;
            }
        }
        int no = 0;
        public void CallBack(Object state)
        {
            try
            {


                Invoke(new MethodInvoker(delegate ()
                {
                    switch (no)
                    {
                        case 0:
                            MdiChildrenShow("사출설비현황");
                            no++;
                            break;
                        case 1:
                            MdiChildrenShow("압출설비현황");
                            no++;
                            break;
                        case 2:
                            MdiChildrenShow("압출설비현황3");
                            no++;
                            break;

                        default:
                            no = 0;
                            break;
                    }


                }));
            }
            catch (Exception pExcption)
            {

            }

        }

        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChildrenShow("생산진형현황2");
        }

        private void 사출설비현황ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChildrenShow("사출설비현황");
        }

        private void 압출설비현황ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChildrenShow("압출설비현황");
        }

        private void 압출설비현황2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChildrenShow("압출설비현황2");
        }

        private void 압출설비현황3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MdiChildrenShow("압출설비현황3");
        }
    }
}






