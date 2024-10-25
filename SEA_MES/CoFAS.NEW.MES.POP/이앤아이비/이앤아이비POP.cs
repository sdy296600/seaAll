using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class 이앤아이비POP : Form
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

        public Loading waitform = new Loading();

        private UserEntity _UserEntity = new UserEntity();

        private SystemLogEntity _SystemLogEntity = new SystemLogEntity();

        #endregion
        public 이앤아이비POP(UserEntity pUserEntity, SystemLogEntity pSystemLogEntity)
        {
            InitializeComponent();
            _UserEntity = pUserEntity;
            _SystemLogEntity = pSystemLogEntity;
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.Size = new Size(1280, 1024);
            this.FormClosing += Form_Closing;
        }

        private void btn_종료_Click(object sender, EventArgs e)
        {
            if (CustomMsg.ShowMessage("프로그램을 종료 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();

                Application.Exit();
            }
        }

        private void 이앤아이비POP_Load(object sender, EventArgs e)
        {
            try
            {
                BTN_설정_Click(null,null);

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);

            }
        }
        private void Form_Closing(object pSender, FormClosingEventArgs pFormClosingEventArgs)
        {
            try
            {
                //화면 레이아웃 저장 ?
                //_timer.Dispose();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);

            }
        }
        private void 사출()
        {
            panel1.Controls.Clear();
            _Title.Text = "[사출]";
            string sql = $@"select *
                              from [dbo].[EQUIPMENT]
                              where 1=1
                              and TYPE = 'CD14001'
                              and USE_YN = 'Y'
                              ORDER BY ID DESC";

            DataTable dt = new CoreBusiness().SELECT(sql);

            xFpSpread fpMain = new xFpSpread();

            fpMain.Name = "fpPOP";

            DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10("사출POP",fpMain,"");
            //panel1.SuspendLayout();
            //panel1.Visible = false;

            foreach (DataRow item in dt.Rows)
            {
                UC_이앤아이비_POP uc = new UC_이앤아이비_POP(item,pDataTable,_UserEntity,"사출POP");
                uc.Dock = DockStyle.Top;
                panel1.Controls.Add(uc);
            }

            //panel1.Visible = true;
            //panel1.ResumeLayout(); // 레이아웃 로직 재개
            //panel1.Refresh(); // 패널을 다시 그리기

            //this.WindowState = FormWindowState.Maximized;
            //this.Refresh();
            //this.Invalidate();
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        private void 압출()
        {
            panel1.Controls.Clear();
            _Title.Text = "[압출]";
            string sql = $@"select *
                              from [dbo].[EQUIPMENT]
                              where 1=1
                              and TYPE = 'CD14003'
                              and USE_YN = 'Y'
                              ORDER BY ID DESC";

            DataTable dt = new CoreBusiness().SELECT(sql);

            xFpSpread fpMain = new xFpSpread();

            fpMain.Name = "fpPOP";

            DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10("압출POP",fpMain,"");
            //panel1.SuspendLayout();
            //panel1.Visible = false;

            foreach (DataRow item in dt.Rows)
            {
                UC_이앤아이비_POP uc = new UC_이앤아이비_POP(item,pDataTable,_UserEntity,"압출POP");
                uc.Dock = DockStyle.Top;
                panel1.Controls.Add(uc);
            }

           // panel1.Visible = true;
            //panel1.ResumeLayout(); // 레이아웃 로직 재개
            //panel1.Refresh(); // 패널을 다시 그리기

            //this.WindowState = FormWindowState.Maximized;
            //this.Refresh();
            //this.Invalidate();
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void 조립()
        {
            panel1.Controls.Clear();
            _Title.Text = "[조립]";
            string sql = $@"select *
                              from [dbo].[PROCESS]
                              where id = 8";

            DataTable dt = new CoreBusiness().SELECT(sql);

            xFpSpread fpMain = new xFpSpread();

            fpMain.Name = "fpPOP";

            DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10("조립POP",fpMain,"");
            //panel1.SuspendLayout();
            //panel1.Visible = false;

            foreach (DataRow item in dt.Rows)
            {
                UC_이앤아이비_POP uc = new UC_이앤아이비_POP(item,pDataTable,_UserEntity,"조립POP");
                uc.Dock = DockStyle.Top;
                panel1.Controls.Add(uc);
            }


        }

        private void 포장()
        {
            panel1.Controls.Clear();
            _Title.Text = "[포장]";
            string sql = $@"select *
                              from [dbo].[PROCESS]
                              where id = 9";

            DataTable dt = new CoreBusiness().SELECT(sql);

            xFpSpread fpMain = new xFpSpread();

            fpMain.Name = "fpPOP";

            DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10("포장POP",fpMain,"");

            foreach (DataRow item in dt.Rows)
            {
                UC_이앤아이비_POP uc = new UC_이앤아이비_POP(item,pDataTable,_UserEntity,"포장POP");
                uc.Dock = DockStyle.Top;
                panel1.Controls.Add(uc);
            }

        }

        private void BTN_설정_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();

                공정선택 공정선택 = new 공정선택();
                if(공정선택.ShowDialog() == DialogResult.OK)
                {
                    switch (공정선택._pNAME)
                    {
                        case"사출":
                            사출();
                            break;

                        case"압출":
                            압출();
                            break;

                        case "포장":
                            포장();
                            break;

                        case "조립":
                            조립();
                            break;

                        default:
                            break;
                    }
                }
                
                this.Show();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);

            }
        }
    }
}
