using CoFAS.NEW.MES.Core;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES
{
    public partial class frmSetting : Form
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

        #region ○ 생성자

        public frmSetting()
        {
            InitializeComponent();
            Load += FrmSetting_Load;
        }

        #endregion

        #region ○ 폼 이벤트

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            

            _txtServerIP.Text = Properties.Settings.Default.serverIp;
            _txtServerID.Text = Properties.Settings.Default.serverId;
            _txtServerPw.Text = Properties.Settings.Default.serverPw;
            _txtServerDB.Text = Properties.Settings.Default.serverName;

            _txtFtpIP.Text = Properties.Settings.Default.ftpIp;
            _txtFtpPort.Text = Properties.Settings.Default.ftpPort;
            _txtFtpID.Text = Properties.Settings.Default.ftpId;
            _txtFtpPw.Text = Properties.Settings.Default.ftpPw;

            _txtTestIP.Text = Properties.Settings.Default.testServerIp;
            _txtTestID.Text = Properties.Settings.Default.testServerId;
            _txtTestPw.Text = Properties.Settings.Default.testServerPw;
            _txtTestDB.Text = Properties.Settings.Default.testServerName;

            _Connection.AddValue("Server", "Server");
            _Connection.AddValue("Test", "Test");
            _Connection.SetValue(Properties.Settings.Default.connection);

        }

        #endregion

        #region ○ 라벨버튼 이벤트

        private void _lblClick(object obj, EventArgs e)
        {
            try
            {
                Label pCmd = obj as Label;
                string pCls = pCmd.Name.Substring(4);

                switch (pCls)
                {
                    case "Save":
                        Properties.Settings.Default.serverIp = _txtServerIP.Text;
                        Properties.Settings.Default.serverId = _txtServerID.Text;
                        Properties.Settings.Default.serverPw = _txtServerPw.Text;
                        Properties.Settings.Default.serverName = _txtServerDB.Text;

                        Properties.Settings.Default.ftpIp = _txtFtpIP.Text;
                        Properties.Settings.Default.ftpPort = _txtFtpPort.Text;
                        Properties.Settings.Default.ftpId = _txtFtpID.Text;
                        Properties.Settings.Default.ftpPw = _txtFtpPw.Text;

                        Properties.Settings.Default.testServerIp = _txtTestIP.Text;
                        Properties.Settings.Default.testServerId = _txtTestID.Text;
                        Properties.Settings.Default.testServerPw = _txtTestPw.Text;
                        Properties.Settings.Default.testServerName = _txtTestDB.Text;

                        Properties.Settings.Default.connection = _Connection.GetValue().ToString();
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Reload();
                        CustomMsg.ShowMessage("저장 되었습니다.");
                        this.Close();
                        break;
                    case "Close":
                        this.Close();
                        break;
                }
            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "오류", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 기타이벤트

        private void _MouseHover(object pSender, EventArgs e)
        {
            Label pCmd = pSender as Label;
            pCmd.BackColor = Color.FromArgb(90,100,140);
            pCmd.ForeColor = Color.White;
        }
        private void _MouseLeave(object pSender, EventArgs e)
        {
            Label pCmd = pSender as Label;
            pCmd.BackColor = Color.White;
            pCmd.ForeColor = Color.FromArgb(46, 51, 73);
        }

        #endregion

  
    }
}
