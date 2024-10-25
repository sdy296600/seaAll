using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.POP.Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class 작업일지 : Form
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

        //private void SetDoubleBuffered_Control(Control.ControlCollection controls)

        //{
        //    foreach (Control item in controls)
        //    {
        //        if (item.Controls.Count != 0)
        //        {
        //            SetDoubleBuffered_Control(item.Controls);
        //        }

        //        SetDoubleBuffered(item);
        //    }
        //}
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

        private UserEntity _UserEntity = new UserEntity();

        public DataRow _ProductionInstruct = null;

        public DateTime _ENDdate = new DateTime();
        public int _Qty;

        public List<BAD_ITEM> _list = new List<BAD_ITEM>();
        #endregion

        #region ○ 생성자

        public 작업일지(int pQty, DataRow pProductionInstruct, UserEntity pUserEntity,DateTime pEND_TIME)
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        public 작업일지()
        {
            InitializeComponent();


            Load += new EventHandler(Form_Load);

           // this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                this.MaximizeBox = false;
                this.MaximumSize = new System.Drawing.Size(1280, 1024);
                this.MinimizeBox = false;
                this.MinimumSize = new System.Drawing.Size(1280, 1024);

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
    
     
      
        private void btn_종료_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

       
    }



}





