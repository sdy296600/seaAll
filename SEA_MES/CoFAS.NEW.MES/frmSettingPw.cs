using CoFAS.NEW.MES.Core;
using System;
using System.Windows.Forms;

namespace CoFAS.NEW.MES
{
    public partial class frmSettingPw : Form
    {
        public frmSettingPw()
        {
            InitializeComponent();
        }

        private void _txtPw_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (_txtPw.Text == "admin!@#$")
                    {
                        frmSetting _frmSetting = new frmSetting();
                        _frmSetting.ShowDialog();

                        this.Close();
                    }
                    else
                    {
                        CustomMsg.ShowMessage("비밀번호가 잘못되었습니다.");
                        return;
                    }
                }


                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception) { }
        }
    }
}
