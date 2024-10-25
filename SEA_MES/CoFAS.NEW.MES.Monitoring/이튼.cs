using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.Monitoring
{
    public partial class 이튼 : Form
    {
        //DBClass dc = new DBClass();
        string strcon = $"Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
        //string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
        System.Threading.Timer _timer;

        public 이튼()
        {
            CheckForIllegalCrossThreadCalls = false; //디버그 할때 크로스 쓰레드 방지

            InitializeComponent();

            this.Focus();

          
        }


        // 마우스가 menuStrip에 진입하면 호출되는 이벤트
      
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



        private void 이튼_Load(object sender, EventArgs e)
        {

            try
            {
                MdiChildrenShow("생산진형현황2");
            }
            catch (Exception ex)
            {

            }

        }
     
   


     

    

      

      
    }
}






