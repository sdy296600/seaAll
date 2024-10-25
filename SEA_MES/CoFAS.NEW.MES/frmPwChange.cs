using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace CoFAS.NEW.MES
{
    public partial class frmPwChange : Form
    {
        private LoginEntity _LoginEntity = null;


        public UserEntity _pUserEntity = null;

        public frmPwChange(UserEntity pUserEntity)
        {
            _pUserEntity = pUserEntity;
            InitializeComponent();
            Size = new Size(381, 279);
            panel2.Size = new Size(356, 228);
            SetTool();
           
            lblUserID.Text = pUserEntity.user_account;
        }

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

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdatePw();
        }

        private void SetTool()
        {
            string msg = "* 비밀번호는 8자리 이상\r\n* 숫자는 1자리 이상 필수\r\n* 특수문자는 1자리 이상 필수";

            toolTip1.SetToolTip(txtChkPW, msg);
            toolTip1.SetToolTip(txtNewPW, msg);
            toolTip1.SetToolTip(label4, msg);
            toolTip1.SetToolTip(label5, msg);
        }

        private void UpdatePw()
        {
            try
            {
                lblLoginPw.Visible = false;
                lblErrMsg.Visible = false;

                _LoginEntity = new LoginEntity();

                _LoginEntity.user_account = lblUserID.Text;
                _LoginEntity.user_password = SHA256Encryption.Encrypt(txtPw.Text);

                DataTable _DataTable = new LoginBusiness().Login_Info(_LoginEntity);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    string pw = txtNewPW.Text;

                    if (pw.Length < 8)
                    {
                        lblErrMsg.Visible = true;

                        lblErrMsg.Text = "비밀번호는 8자리 이상으로 입력해주세요.";

                        txtNewPW.Text = "";
                        txtChkPW.Text = "";
                        txtNewPW.Focus();
                    }

                    else
                    {
                        string strPattern = "";

                        Regex regex = null;

                        strPattern = @"[~!@#$%^&*()_+=\[{\]};:<>|./?,-]";

                        regex = new Regex(strPattern);
                        bool chkSp = regex.IsMatch(pw);

                        strPattern = @"[0-9]+";

                        regex = new Regex(strPattern);
                        bool chkNum = regex.IsMatch(pw);

                        regex = null;

                        if (!chkSp || !chkNum)
                        {
                            lblErrMsg.Visible = true;

                            lblErrMsg.Text = "숫자 1자리 이상\r\n특수문자 1자리 이상 필수로 입력해주세요.";

                            txtChkPW.Text = "";
                            txtNewPW.SelectAll();
                            txtNewPW.Focus();
                        }

                        else
                        {
                            if (pw.Equals(txtChkPW.Text))
                            {
                                lblLoginPw.Visible = false;
                                lblErrMsg.Visible = false;

                                //비밀번호 변경
                                _LoginEntity.user_account = lblUserID.Text;
                                _LoginEntity.user_newpassword = SHA256Encryption.Encrypt(txtNewPW.Text);
                     
                                DataTable dt = new LoginBusiness().UserPWChg(_LoginEntity);

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    CustomMsg.ShowMessage("비밀번호가 변경되었습니다.");
                                    this.DialogResult = DialogResult.OK;
                                    this.Close();
                                }
                            }

                            else
                            {
                                lblErrMsg.Visible = true;

                                lblErrMsg.Text = "비밀번호가 일치하지 않습니다.";

                                txtChkPW.SelectAll();
                                txtChkPW.Focus();
                            }
                        }
                    }
                }
                else
                {
                    lblLoginPw.Visible = true;

                    txtPw.SelectAll();
                    txtPw.Focus();
                }
            }
            catch { }

        }

       

   

      
    }
}
