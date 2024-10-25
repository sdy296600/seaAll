using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
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
    public partial class from_세아초중종 : Form
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
                //this.SetStyle(ControlStyles.ResizeRedraw, true);
            }
        }

        #endregion

        #region ○ 변수선언
        public My_Settings _MY =  utility.My_Settings_Get();

        private UserEntity _UserEntity = new UserEntity();

        public string _pLOT  = string.Empty;

        public string _p품번 = string.Empty;
        public string _p실적 = string.Empty;

  
        #endregion



        #region ○ 생성자

        public from_세아초중종(UserEntity pUserEntity)
        {
            InitializeComponent();
            _UserEntity = pUserEntity;


            _MY = utility.My_Settings_Get();

            Load += new EventHandler(Form_Load);

            this.Font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        public from_세아초중종()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);

            this.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
        }
        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
       
                _uc_Gross.lbl_제목.Text = "총 중량\n\r(Gross)";
                _uc_Gete.lbl_제목.Text = "게이트\n\r(Gete+런너)";
                _uc_Net.lbl_제목.Text = "본체형상\n\r(Net)";
                _uc_Scrap.lbl_제목.Text = "Scrap";
                _uc_Loss.lbl_제목.Text = "기타Loss\n\r(공통)";

                _uc_Gross._pTYPE = "Gross";
                _uc_Gete._pTYPE = "Gete";
                _uc_Net._pTYPE = "Net";
                _uc_Scrap._pTYPE = "Scrap";
                _uc_Loss._pTYPE = "Loss";


                _uc_Gross._pLOT = _pLOT;
                _uc_Gross._p품번 = _p품번;
                _uc_Gross._p실적 = _p실적;

                _uc_Gete._pLOT = _pLOT;
                _uc_Gete._p품번 = _p품번;
                _uc_Gete._p실적 = _p실적;

                _uc_Net._pLOT = _pLOT;
                _uc_Net._p품번 = _p품번;
                _uc_Net._p실적 = _p실적;

                _uc_Scrap._pLOT = _pLOT;
                _uc_Scrap._p품번 = _p품번;
                _uc_Scrap._p실적 = _p실적;

                _uc_Loss._pLOT = _pLOT;
                _uc_Loss._p품번 = _p품번;
                _uc_Loss._p실적 = _p실적;


                string sql = $@"SELECT *
                                  FROM [dbo].[WORK_RECYCLE]
                                 WHERE 1=1
                              AND RESOURCE_NO = '{_p품번}'
                              AND LOT_NO = '{_pLOT}'";
                DataTable pDataTable =  new CoreBusiness().SELECT(sql);

                _uc_Gete.Enabled = true;
                _uc_Net.Enabled = true;
                _uc_Scrap.Enabled = false;
                _uc_Loss.Enabled = true;

                if (pDataTable.Rows.Count == 0)
                {

                    new MS_DBClass(utility.My_Settings_Get()).USP_WORK_RECYCLE_POP_A10(
                     _p품번
                   , _pLOT
                   , DateTime.Now.ToString("yyyy-MM-dd")
                   , "N"
                   , "N"
                   , "N"
                   , _UserEntity.user_account
                   , _UserEntity.user_account
                   );
                    sql = $@"INSERT INTO [dbo].[WORK_RECYCLE_DETAIL]
                               (
                                [WORK_PERFORMANCE_ID]
                               ,[SEQ]
                               ,[RESOURCE_NO]
                               ,[LOT_NO]
                               ,[TYPE]
                               ,[VALUE]
                               ,[USE_YN]
                               ,[REG_USER]
                               ,[REG_DATE]
                               ,[UP_USER]
                               ,[UP_DATE])
                                  select  '{_p실적}',1,'{_p품번}','{_pLOT}','Net',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                  select  '{_p실적}',2,'{_p품번}','{_pLOT}','Net',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',3,'{_p품번}','{_pLOT}','Net',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',4,'{_p품번}','{_pLOT}','Net',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',5,'{_p품번}','{_pLOT}','Net',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union
                                   select '{_p실적}',1,'{_p품번}','{_pLOT}','Net',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                  select  '{_p실적}',2,'{_p품번}','{_pLOT}','Net',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',3,'{_p품번}','{_pLOT}','Net',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',4,'{_p품번}','{_pLOT}','Net',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',5,'{_p품번}','{_pLOT}','Net',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union
                                   select '{_p실적}',1,'{_p품번}','{_pLOT}','Gete',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                  select  '{_p실적}',2,'{_p품번}','{_pLOT}','Gete',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',3,'{_p품번}','{_pLOT}','Gete',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',4,'{_p품번}','{_pLOT}','Gete',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',5,'{_p품번}','{_pLOT}','Gete',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union
                                   select '{_p실적}',1,'{_p품번}','{_pLOT}','Gross',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                  select  '{_p실적}',2,'{_p품번}','{_pLOT}','Gross',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',3,'{_p품번}','{_pLOT}','Gross',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',4,'{_p품번}','{_pLOT}','Gross',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',5,'{_p품번}','{_pLOT}','Gross',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                    union
                                   select '{_p실적}',1,'{_p품번}','{_pLOT}','Scrap',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                  select  '{_p실적}',2,'{_p품번}','{_pLOT}','Scrap',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',3,'{_p품번}','{_pLOT}','Scrap',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',4,'{_p품번}','{_pLOT}','Scrap',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',5,'{_p품번}','{_pLOT}','Scrap',0,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union
                                   select '{_p실적}',1,'{_p품번}','{_pLOT}','Loss',1.2,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                  select  '{_p실적}',2,'{_p품번}','{_pLOT}','Loss',1.2,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',3,'{_p품번}','{_pLOT}','Loss',1.2,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',4,'{_p품번}','{_pLOT}','Loss',1.2,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                   union 
                                   select '{_p실적}',5,'{_p품번}','{_pLOT}','Loss',1.2,'Y','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{"admin"}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'";

                    pDataTable = new CoreBusiness().SELECT(sql);
                }
        

                sql = $@"SELECT *
                             FROM [dbo].[WORK_RECYCLE_DETAIL]
                             WHERE 1=1
                             AND RESOURCE_NO = '{_p품번}'
                             AND LOT_NO = '{_pLOT}'
                             AND WORK_PERFORMANCE_ID = '{_p실적}'";

                pDataTable = new CoreBusiness().SELECT(sql);

                _uc_Gross.DATE_SET(pDataTable);
                _uc_Gete.DATE_SET(pDataTable);
                _uc_Net.DATE_SET(pDataTable);
                _uc_Scrap.DATE_SET(pDataTable);
                _uc_Loss.DATE_SET(pDataTable);

                _uc_Gross._uc_초중종_Load(null,null);
                _uc_Gete._uc_초중종_Load(null, null);
                _uc_Net._uc_초중종_Load(null, null);
                _uc_Scrap._uc_초중종_Load(null, null);
                _uc_Loss._uc_초중종_Load(null, null);

                _uc_Gross.btn_1.Click += button_Click;
                _uc_Gross.btn_2.Click += button_Click;
                _uc_Gross.btn_3.Click += button_Click;
                _uc_Gross.btn_4.Click += button_Click;
                _uc_Gross.btn_5.Click += button_Click;

                _uc_Gete.btn_1.Click += button_Click;
                _uc_Gete.btn_2.Click += button_Click;
                _uc_Gete.btn_3.Click += button_Click;
                _uc_Gete.btn_4.Click += button_Click;
                _uc_Gete.btn_5.Click += button_Click;

                _uc_Net.btn_1.Click += button_Click;
                _uc_Net.btn_2.Click += button_Click;
                _uc_Net.btn_3.Click += button_Click;
                _uc_Net.btn_4.Click += button_Click;
                _uc_Net.btn_5.Click += button_Click;

                _uc_Scrap.btn_1.Click += button_Click;
                _uc_Scrap.btn_2.Click += button_Click;
                _uc_Scrap.btn_3.Click += button_Click;
                _uc_Scrap.btn_4.Click += button_Click;
                _uc_Scrap.btn_5.Click += button_Click;

               _uc_Loss.btn_1.Click += button_Click;
               _uc_Loss.btn_2.Click += button_Click;
               _uc_Loss.btn_3.Click += button_Click;
               _uc_Loss.btn_4.Click += button_Click;
               _uc_Loss.btn_5.Click += button_Click;






            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        #endregion

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            
            string 회차 = btn.Name.Split('_')[1];

            string 타입 = btn.Name.Split('_')[2];

            from_중량 from =new from_중량();

            if (from.ShowDialog() == DialogResult.OK)
            {
                btn.Text = from._중량.Text;

                string sql = $@"UPDATE [dbo].[WORK_RECYCLE_DETAIL]
                                   SET [VALUE] = '{btn.Text}'
                                 WHERE 1=1
                                 and WORK_PERFORMANCE_ID = '{_p실적}' 
                                 and SEQ				 = '{회차}'
                                 and RESOURCE_NO 		 = '{_p품번}'
                                 and LOT_NO 			 = '{_pLOT}'
                                 and TYPE 				 = '{타입}'";

                DataTable dt =  new CoreBusiness().SELECT(sql);

                sql = $@"SELECT *
                             FROM [dbo].[WORK_RECYCLE_DETAIL]
                             WHERE 1=1
                             AND RESOURCE_NO = '{_p품번}'
                             AND LOT_NO = '{_pLOT}'
                             AND WORK_PERFORMANCE_ID = '{_p실적}'";

                dt = new CoreBusiness().SELECT(sql);

                _uc_Gross.DATE_SET(dt);
                _uc_Gete.DATE_SET(dt);
                _uc_Net.DATE_SET(dt);
                _uc_Scrap.DATE_SET(dt);
                _uc_Loss.DATE_SET(dt);
            }
            
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}





