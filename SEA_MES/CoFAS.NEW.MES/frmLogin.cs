using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;



namespace CoFAS.NEW.MES
{
    public partial class frmLogin : frmBaseNone
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

        private UserEntity _UserEntity = null;
        private LoginEntity _LoginEntity = null;
        private SystemLogEntity _SystemLogEntity = null;

        #endregion

        #region ○ 생성자

        public frmLogin()
        {
            InitializeComponent();
            InitializeControl();

            Load += FrmLogin_Load;
            Activated += FrmLogin_Activated;

        }

        #endregion

        #region ○ 폼 이벤트 영역
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect
                                                     , int nTopRect
                                                     , int nRightRect
                                                     , int nBottomRect
                                                     , int nWidthEllipse
                                                     , int nHeightEllipse);
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(Application.StartupPath + "\\" + "logo.png"))
                {
                    _imgBox01.Image = Image.FromFile(Application.StartupPath + "\\" + "logo.png");
                }
                else
                {
                    _imgBox01.Image = null;
                }

                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 25, 25));

                if (Properties.Settings.Default.accountSave == "Y")
                {
                    chkBox01.Checked = true;
                    _UserAccount.Text = Properties.Settings.Default.account;
                }
                else
                {
                    chkBox01.Checked = false;
                    _UserAccount.Text = "";
                }

                _Password.Text = "";

                string path = Application.StartupPath + "\\STARTDATA.txt";

                System.IO.FileInfo info = new System.IO.FileInfo(path);

                //bool startYN = true;

                if (info.Exists)
                {
                    lbl_V.Text += System.IO.File.ReadAllText(path);
                }
                else
                {
                    lbl_V.Text += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void FrmLogin_Activated(object sender, EventArgs e)
        {
            if (_UserAccount.Text.Length > 1)
                _Password.Focus();
            else
                _UserAccount.Focus();
        }

        #endregion

        private void InitializeControl()
        {
            try
            {
                _UserEntity = new UserEntity();
                _LoginEntity = new LoginEntity();
                _SystemLogEntity = new SystemLogEntity();

                _UserEntity.ftp_ip = Properties.Settings.Default.ftpIp;
                _UserEntity.ftp_port = Properties.Settings.Default.ftpPort;
                _UserEntity.ftp_id = Properties.Settings.Default.ftpId;
                _UserEntity.ftp_pw = Properties.Settings.Default.ftpPw;
            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Information", MessageBoxButtons.OK);
            }
        }

        #region ○ 기타 이벤트

        private void lbl_Click(object obj, EventArgs e)
        {
            Label pCmd = obj as Label;
            string pCls = pCmd.Name.Substring(4);

            switch (pCls)
            {
                case "Exit":
                    this.Close();
                    Application.Exit();
                    break;
            }
        }
        private void img_Click(object obj, EventArgs e)
        {
            PictureBox pCmd = obj as PictureBox;

            string pCls = pCmd.Name.Substring(7, 2);

            switch (pCls)
            {
                case "04":  // Login
                    // DB 연결정보 설정
                    DBManager.PrimaryDBManagerType = DBManagerType.SQLServer;

                    Properties.Settings.Default.Reset();

                    switch (Properties.Settings.Default.connection)
                    {
                        case "Server":
                            DBManager.PrimaryConnectionString = string.Format
                            (
                                "Server={0};Database={1};UID={2};PWD={3}",
                                Properties.Settings.Default.serverIp,
                                Properties.Settings.Default.serverName,
                                Properties.Settings.Default.serverId,
                                Properties.Settings.Default.serverPw
                            );
                            break;
                        case "Test":
                            DBManager.PrimaryConnectionString = string.Format
                            (
                                "Server={0};Database={1};UID={2};PWD={3}",
                                Properties.Settings.Default.testServerIp,
                                Properties.Settings.Default.testServerName,
                                Properties.Settings.Default.testServerId,
                                Properties.Settings.Default.testServerPw
                            );
                            break;
                    }

                    // 아이디, 비밀번호 확인
                    if (_UserAccount.Text == "")
                    {
                        CustomMsg.ShowMessage("아이디를 입력하시기 바랍니다.");
                        return;
                    }
                    if (_Password.Text == "")
                    {
                        CustomMsg.ShowMessage("비밀번호를 입력하시기 바랍니다.");
                        return;
                    }
                    if (!DB_Open_Check(DBManager.PrimaryConnectionString))
                    {
                        CustomMsg.ShowMessage("서버 연결을 확인해주세요.\n\r(서버관리자에게 문의)");
                        return;
                    }
                    if (_UserAccount.Text != "" && _Password.Text != "")
                    {
                        _LoginEntity.user_account = _UserAccount.Text;
                        _LoginEntity.user_password = SHA256Encryption.Encrypt(_Password.Text);
                    }
                    // 아이디 저장 여부 확인
                    DataTable _DataTable = new LoginBusiness().Login_Info(_LoginEntity);
                    if (_DataTable != null && _DataTable.Rows.Count > 0)
                    {
                        if (chkBox01.Checked)
                        {
                            Properties.Settings.Default.account = _UserAccount.Text;
                            Properties.Settings.Default.accountSave = "Y";
                        }
                        else
                        {
                            //Properties.Settings.Default.account = "";
                            Properties.Settings.Default.account = _UserAccount.Text;
                            Properties.Settings.Default.accountSave = "";
                        }

                        Properties.Settings.Default.Save();

                        // 로그인 이력저장

                        _UserEntity.user_account  = _DataTable.Rows[0]["user_account"].ToString();
                        _UserEntity.user_password = _DataTable.Rows[0]["user_password"].ToString();
                        _UserEntity.user_name = _DataTable.Rows[0]["user_name"].ToString();
                        _UserEntity.user_authority = _DataTable.Rows[0]["user_authority"].ToString();
                        _UserEntity.user_dept      = _DataTable.Rows[0]["user_dept"].ToString();
                        _UserEntity.dept_name      = _DataTable.Rows[0]["dept_name"].ToString();
                        _UserEntity.user_title = _DataTable.Rows[0]["user_title"].ToString();
                        _UserEntity.title_name = _DataTable.Rows[0]["title_name"].ToString();
                        _UserEntity.V_NAME = lbl_V.Text;

                        _SystemLogEntity.user_account = _UserEntity.user_account;
                        _SystemLogEntity.user_ip = SystemLog.Get_IpAddress();
                        _SystemLogEntity.user_mac = SystemLog.Get_MacAddress();
                        _SystemLogEntity.user_pc = SystemLog.Get_PcName();
                        _SystemLogEntity.event_type = "Login";
                        _SystemLogEntity.event_log = this.Name;
                        bool _ErrorYn = new SystemLogBusiness().SystemLog_Info(_SystemLogEntity);

                        if (_ErrorYn)
                            CustomMsg.ShowMessage("EventLog 저장이 실패하였습니다.", "Information", MessageBoxButtons.OK);

                        Core.Function.Core.Log_API(_SystemLogEntity);

                        this.Hide();
                        if (_UserEntity.user_password == "T3jGtfrKjLiV1NYEu2kS0ST2FPxr9vioXX184Vn+de10msDxabBxOX9Kns1xcAgpLmJPcDJ9MH7pisgui1t6fg==")
                        {
                            frmPwChange pwc = new frmPwChange(_UserEntity);

        
                            if(pwc.ShowDialog() != DialogResult.OK)
                            {
                                Application.ExitThread();
                                return;
                            }
                            
                       
                        }
                        frmMain _frmMain = new frmMain(_UserEntity, _SystemLogEntity);
                        _frmMain.ShowDialog();
                    }
                    else
                    {
                        CustomMsg.ShowMessage("사용자정보를 확인하시기 바랍니다.", "Information", MessageBoxButtons.OK);
                    }
                    break;
                 
            }
        }

        private bool DB_Open_Check(string sqlcon)
        {
            bool check = true;


            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(sqlcon + ";Connection Timeout=3;");
            try
            {
                // DB 연결          
                conn.Open();

                // 연결여부에 따라 다른 메시지를 보여준다
                if (conn.State != ConnectionState.Open)
                {
                    check = false;
                }

                conn.Close();

                return check;
            }
            catch (Exception _Exception)
            {
                Console.WriteLine(_Exception.Message);
                conn.Close();
                return check = false;               
            }

        }
        private void txtBox_KeyDown(object obj, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                img_Click(_imgBox04, null);
        }

        private void _MouseHover(object pSender, EventArgs e)
        {
            Label pCmd = pSender as Label;
            //pCmd.BackColor = Color.FromArgb(0, 126, 249);
            pCmd.BackColor = Color.Gainsboro;
        }
        private void _MouseLeave(object pSender, EventArgs e)
        {
            Label pCmd = pSender as Label;
            //pCmd.BackColor = Color.FromArgb(46, 51, 73);
            pCmd.BackColor = Color.Transparent;
        }


        #endregion

        private void lblDB_Click(object sender, EventArgs e)
        {
            frmSettingPw _frmSettingPw = new frmSettingPw();
            _frmSettingPw.ShowDialog();
        }

     
    }
}
