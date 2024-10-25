using DevExpress.XtraEditors;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoFAS.NEW.MES.HS_SI.Entity;
using CoFAS.NEW.MES.HS_SI.Business;
using CoFAS.NEW.MES.Core;

namespace CoFAS.NEW.MES.HT_SI
{
    public partial class MenuInformation_P10 : Form
    {
        #region○ 폼 이동

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

        public MenuInformation_Entity _Entity = null;
        public delegate void ModuleAddEventHandler(object obj);
        public event ModuleAddEventHandler _ModuleAddEvent;


        public MenuInformation_P10()
        {
            InitializeComponent();
        }

        #region○ 데이터베이스영역

        private void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                // 모듈코드가 있는지 확인 후, 저장로직
                DataTable _DataTable = new MenuInformation_Business().MenuInformation_R30(_ModuleCode.Text);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    if (_DataTable.Rows[0]["p_rtn_key"].ToString() == "00")
                    {
                        // 공통코드에도 추가해주기
                        bool _bool = new MenuInformation_Business().MenuInformation_A20(_ModuleCode.Text, _ModuleName.Text, _Entity.user_account);
                        if (!_bool)
                        {
                            CustomMsg.ShowMessage("모듈이 추가되었습니다.");
                            this._ModuleAddEvent("");
                            this.Close();
                        }
                    }
                    else if(_DataTable.Rows[0]["p_rtn_key"].ToString() == "10")
                    {
                    CustomMsg.ShowMessage("동일한 모듈이 존재합니다.");
                    }
                }

            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {
                DevExpressManager.SetCursor(this, Cursors.Default);
            }
        }

        #endregion

        private void SimpleButton_Click(object sender, EventArgs e)
        {
            SimpleButton pCmd = sender as SimpleButton;
            string pCls = pCmd.Name.Substring(1);

            switch (pCls)
            {
                case "ModuleAdd":
                    MainSave_InputData();
                    break;
                case "Close":
                    this.Close();
                    break;
            }
        }
    }
}