using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.POP.Function;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    public partial class frmPOPLogin : Form
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

        #region ○ 변수선언
        public My_Settings _MY = null;

        private Font _font = null;

        private UserEntity _UserEntity = new UserEntity();
        private LoginEntity _LoginEntity = new LoginEntity();
        private SystemLogEntity _SystemLogEntity = new SystemLogEntity();

        #endregion

        #region ○ 생성자

        public frmPOPLogin()
        {

            InitializeComponent();
          
            _MY = utility.My_Settings_Get();
            Load += new EventHandler(Form_Load);
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                Initialize();

                if (_ckUserIDSave.Checked && _txtAccount.Text.Trim().Length > 1)
                    _txtPassword.Select();
                else
                    _txtAccount.Select();
                _txtAccount.KeyDown += _KeyDown;
                _txtPassword.KeyDown += _KeyDown;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }



        #endregion

        #region ○ 로그인 초기화 - Initialize()
        private void Initialize()
        {
            try
            {
                _ckUserIDSave.Checked = _MY.USER_ID_SAVE;

                if (_ckUserIDSave.Checked)
                {
                    _txtAccount.Text = _MY.USER_ID.ToString();
                }
                else
                {
                    _txtAccount.Text = "";
                }


                _UserEntity.FONT_TYPE = _MY.FONT_TYPE.ToString();
                _UserEntity.FONT_SIZE = _MY.FONT_SIZE;

                _font = new Font(_UserEntity.FONT_TYPE, _UserEntity.FONT_SIZE);

   
            }
            catch (Exception pException)
            {
                
            }
        }
        #endregion



        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _btLoginOk_Click(object sender, EventArgs e)
        {
          


            if (_txtAccount.Text == "")
            {
                CustomMsg.ShowMessage("아이디를 입력하시기 바랍니다.");
                return;
            }
            if (_txtPassword.Text == "")
            {
                CustomMsg.ShowMessage("비밀번호를 입력하시기 바랍니다.");
                return;
            }
            if (!new MS_DBClass(_MY).DB_Open_Check())
            {
                CustomMsg.ShowMessage("서버 연결을 확인해주세요.\n\r(서버관리자에게 문의)");
                return;
            }
            if (_txtAccount.Text != "" && _txtPassword.Text != "")
            {
                _LoginEntity.user_account = _txtAccount.Text;
                _LoginEntity.user_password = SHA256Encryption.Encrypt(_txtPassword.Text);
            }

            DataTable _DataTable = new MS_DBClass(_MY).Login_Info(_LoginEntity);

            if (_DataTable != null && _DataTable.Rows.Count > 0)
            {
                if (_ckUserIDSave.Checked)
                {
                    _MY.USER_ID = _LoginEntity.user_account;
                    _MY.USER_ID_SAVE = true;
                }
                else
                {
                    _MY.USER_ID = "";
                    _MY.USER_ID_SAVE = false;
                }

                utility.My_Settings_Set(_MY);

                // 로그인 이력저장

                _UserEntity.user_account   = _DataTable.Rows[0]["user_account"].ToString();
                _UserEntity.user_password  = _DataTable.Rows[0]["user_password"].ToString();
                _UserEntity.user_name      = _DataTable.Rows[0]["user_name"].ToString();
                _UserEntity.user_authority = _DataTable.Rows[0]["user_authority"].ToString();
                _UserEntity.user_dept      = _DataTable.Rows[0]["user_dept"].ToString();
                _UserEntity.dept_name      = _DataTable.Rows[0]["dept_name"].ToString();
                _UserEntity.user_title     = _DataTable.Rows[0]["user_title"].ToString();
                _UserEntity.title_name     = _DataTable.Rows[0]["title_name"].ToString();

                _SystemLogEntity.user_account = _UserEntity.user_account;
                _SystemLogEntity.user_ip = SystemLog.Get_IpAddress();
                _SystemLogEntity.user_mac = SystemLog.Get_MacAddress();
                _SystemLogEntity.user_pc = SystemLog.Get_PcName();
                _SystemLogEntity.event_type = "Login";
                _SystemLogEntity.event_log = this.Name;
                bool _ErrorYn = new MS_DBClass(_MY).SystemLog_Info(_SystemLogEntity);

                if (_ErrorYn)
                    CustomMsg.ShowMessage("EventLog 저장이 실패하였습니다.", "Information", MessageBoxButtons.OK);

                this.Hide();
             
                frmPOPMain _frmMain = new frmPOPMain(_UserEntity, _SystemLogEntity);

                _frmMain.ShowDialog();
            }
            else
            {
                CustomMsg.ShowMessage("사용자정보를 확인하시기 바랍니다.", "Information", MessageBoxButtons.OK);
            }
           


        }

        private void _KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    _btLoginOk.PerformClick();
            }
            catch { }
        }

    }
    }





