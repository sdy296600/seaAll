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
    public partial class frmProductionProgressStatus : Form
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


        #endregion

        #region ○ 생성자

        public frmProductionProgressStatus(UserEntity pUserEntity)
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
                DataGridView1ColumnSet();

                GETDATE();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        #endregion
        private void DataGridView1ColumnSet()
        {
            try
            {
                DataGridViewUtil.InitSettingGridView(dataGridView1);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "제품코드     ".Trim(), "제품코드    ".Trim(), true, 250, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "제품명       ".Trim(), "제품명      ".Trim(), true, 250, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "작업시간     ".Trim(), "작업시간    ".Trim(), true, 250, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "계획         ".Trim(), "계획        ".Trim(), true, 150, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "실적         ".Trim(), "실적        ".Trim(), true, 150, DataGridViewContentAlignment.MiddleCenter);
                DataGridViewUtil.AddNewColumnToDataGridView(dataGridView1, "누적         ".Trim(), "누적        ".Trim(), true, 150, DataGridViewContentAlignment.MiddleCenter);

                dataGridView1.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE - 5, FontStyle.Bold);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void GETDATE()
        {

            dataGridView1.DataSource = new MS_DBClass(_MY).USP_Production_Progress_Status_R10();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}





