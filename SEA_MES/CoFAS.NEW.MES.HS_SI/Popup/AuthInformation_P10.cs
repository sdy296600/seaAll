using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.SW_SI.Business;
using DevExpress.XtraEditors;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.HT_SI
{
    public partial class AuthInformation_P10 : Form
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

        public string _UserAccount = string.Empty;
        public string _UserAuthority = string.Empty;
        public SystemLogEntity _SystemLogEntity = null;

        private bool _FirstYn = true;
        public string Title
        {
            get { return _Title.Text; }
            set { _Title.Text = "  - " + value + " -  "; }
        }

        #endregion


        #region ○ 생성자

        public AuthInformation_P10()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
        }

        #endregion

        #region ○ 폼 이벤트

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeControl();
                initializeSpread();

                _SystemLogEntity = new SystemLogEntity();

                fpLeft.CellDoubleClick += new CellClickEventHandler(fpLeft_CellDoubleClick);

                LeftFind_DisplayData();
                MainFind_DisplayData();
            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }



        #endregion

        #region ○ 초기화 영역

        private void InitializeControl()
        {
            try
            {
                if (_FirstYn)
                {

                }

                fpLeft.Sheets[0].Rows.Count = 0;
                fpRight.Sheets[0].Rows.Count = 0;
            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
            finally
            {
                _FirstYn = false;
            }
        }

        private void initializeSpread()
        {
            // fpLeft ---------------------------------------------------------------------------------------------------------------------------------------------------
            // 콤보가 있을때 반드시 설정
            FpSpreadManager.pConnectionString = ConfigrationManager.ConnectionString;

            // 스프레드 기본설정
            FpSpreadManager.pOperationType = OperationMode.RowMode; // 읽기전용설정
            FpSpreadManager.SpreadSetStyle(fpLeft);                 // 스프레드 전체설정      
            FpSpreadManager.SpreadSetSheetStyle(fpLeft, 0);         // 스프레드 쉬트설정

            // 스프레드 쉬트 설정
            string pSetData = string.Empty;
            //           11 버튼 12 체크박스 14 콤보박스 25 날짜시간 28 마스크셀타입 29 숫자 32 텍스트
            //           라벨//넓이//보임0,숨김1//일기전용0,수정가능1,읽기전용기본색상2//왼쪽정렬0,중앙정렬1,오른쪽정렬2//셀타입//길이//소숫점//유형//그룹코드
            //           --------------------:넓이:보임:읽기:정렬:타입:길이:소수:유형:그룹코드(추가사항등)  :필드명
            pSetData += " 에러               :21  :1   :2   :1   :12  :0   :0   :             :1                     :                       "; // 에러체크
            pSetData += ",에러명             :21  :1   :2   :0   :32  :2000:0   :             :                      :                       "; // 에러메시지
            pSetData += ",체크               :21  :1   :1   :1   :12  :0   :0   :             :5                     :check                  "; // 체크
            pSetData += ",선택               :21  :1   :2   :1   :12  :0   :0   :             :2                     :                       "; // 선택
            pSetData += ",예비4              :100 :1   :2   :1   :32  :100 :0   :             :                      :                       ";
            pSetData += ",예비5              :100 :1   :2   :1   :32  :100 :0   :             :                      :                       ";
            // ▲  공통부분 -------------------------------------------------------------------------------------------------------------------------------------------
            // ▼  추기 필요한 부분 -----------------------------------------------------------------------------------------------------------------------------------
            pSetData += ",사용자\r\n아이디   :100 :0   :2   :1   :32  :100 :0   :             :                      :user_account           ";
            pSetData += ",사용자명           :100 :0   :2   :1   :32  :100 :0   :             :                      :user_name              ";
            pSetData += ",부서명             :100 :0   :2   :1   :32  :100 :0   :             :                      :dept_name              ";
            // ----------------------------------------------------------------------------------------------------------------------------------------------------
            FpSpreadManager.SpreadSetHeader(fpLeft, "조회", 0, pSetData, this.Name, _UserAccount);
            fpLeft.Sheets[0].Columns["user_account ".Trim()].AllowAutoSort = true;
            fpLeft.Sheets[0].Columns["user_name    ".Trim()].AllowAutoSort = true;
            fpLeft.Sheets[0].Columns["dept_name    ".Trim()].AllowAutoSort = true;

            // fpRight ---------------------------------------------------------------------------------------------------------------------------------------------------
            // 콤보가 있을때 반드시 설정
            FpSpreadManager.pConnectionString = ConfigrationManager.ConnectionString;

            // 스프레드 기본설정
            FpSpreadManager.pOperationType = OperationMode.Normal; // 읽기전용설정
            FpSpreadManager.SpreadSetStyle(fpRight);                 // 스프레드 전체설정      
            FpSpreadManager.SpreadSetSheetStyle(fpRight, 0);         // 스프레드 쉬트설정

            // 스프레드 쉬트 설정
            pSetData = string.Empty;
            //           11 버튼 12 체크박스 14 콤보박스 25 날짜시간 28 마스크셀타입 29 숫자 32 텍스트
            //           라벨//넓이//보임0,숨김1//일기전용0,수정가능1,읽기전용기본색상2//왼쪽정렬0,중앙정렬1,오른쪽정렬2//셀타입//길이//소숫점//유형//그룹코드
            //           --------------------:넓이:보임:읽기:정렬:타입:길이:소수:유형:그룹코드(추가사항등)  :필드명
            pSetData += " 에러               :21  :1   :2   :1   :12  :0   :0   :             :1                     :                       "; // 에러체크
            pSetData += ",에러명             :21  :1   :2   :0   :32  :2000:0   :             :                      :                       "; // 에러메시지
            pSetData += ",체크               :21  :0   :1   :1   :12  :0   :0   :             :5                     :check                  "; // 체크
            pSetData += ",선택               :21  :1   :2   :1   :12  :0   :0   :             :2                     :                       "; // 선택
            pSetData += ",예비4              :100 :1   :2   :1   :32  :100 :0   :             :                      :                       ";
            pSetData += ",예비5              :100 :1   :2   :1   :32  :100 :0   :             :                      :                       ";
            // ▲  공통부분 -------------------------------------------------------------------------------------------------------------------------------------------
            // ▼  추기 필요한 부분 -----------------------------------------------------------------------------------------------------------------------------------
            pSetData += ",사용자\r\n아이디   :100 :0   :2   :1   :32  :100 :0   :             :                      :user_account           ";
            pSetData += ",사용자명           :100 :0   :2   :1   :32  :100 :0   :             :                      :user_name              ";
            pSetData += ",부서명             :100 :0   :2   :1   :32  :100 :0   :             :                      :dept_name              ";
            // ----------------------------------------------------------------------------------------------------------------------------------------------------
  
            FpSpreadManager.SpreadSetHeader(fpRight, "조회", 0, pSetData, this.Name, _UserAccount);
          
            fpRight.Sheets[0].Columns["user_account ".Trim()].AllowAutoSort = true;
            fpRight.Sheets[0].Columns["user_name    ".Trim()].AllowAutoSort = true;
            fpRight.Sheets[0].Columns["dept_name    ".Trim()].AllowAutoSort = true;
        }

        private void fpLeft_CellDoubleClick(object obj, CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            _lblAccount.Text = fpLeft.Sheets[0].GetValue(e.Row, "user_account").ToString();
            _lblName.Text = fpLeft.Sheets[0].GetValue(e.Row, "user_name").ToString();
        }

        #endregion

        #region ○ 데이터 영역

        private void LeftFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpLeft.Sheets[0].Rows.Count = 0;
                DataTable _DataTable = new AuthInformation_Business().AuthInformation_R30();
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpLeft.Sheets[0].Visible = false;
                    fpLeft.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpLeft.Sheets[0].SetValue(i, "user_account ".Trim(), _DataTable.Rows[i]["user_account ".Trim()].ToString());
                        fpLeft.Sheets[0].SetValue(i, "user_name    ".Trim(), _DataTable.Rows[i]["user_name    ".Trim()].ToString());
                        fpLeft.Sheets[0].SetValue(i, "dept_name    ".Trim(), _DataTable.Rows[i]["dept_name    ".Trim()].ToString());
                    }
                    fpLeft.Sheets[0].Visible = true;
                }
                else
                {
                    CustomMsg.ShowMessage("조회 내역이 없습니다.");
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

        private void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpRight.Sheets[0].Rows.Count = 0;
                DataTable _DataTable = new AuthInformation_Business().AuthInformation_R30();
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpRight.Sheets[0].Visible = false;
                    fpRight.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpRight.Sheets[0].SetValue(i, "user_account ".Trim(), _DataTable.Rows[i]["user_account ".Trim()].ToString());
                        fpRight.Sheets[0].SetValue(i, "user_name    ".Trim(), _DataTable.Rows[i]["user_name    ".Trim()].ToString());
                        fpRight.Sheets[0].SetValue(i, "dept_name    ".Trim(), _DataTable.Rows[i]["dept_name    ".Trim()].ToString());
                    }
                    fpRight.Sheets[0].Visible = true;
                }
                else
                {
                    CustomMsg.ShowMessage("조회 내역이 없습니다.");
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

        private void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpRight.Focus();
                bool _Error = new AuthInformation_Business().AuthInformation_A20(_UserAccount, _lblAccount.Text, ref fpRight);
                if (!_Error)
                {
                    CustomMsg.ShowMessage("권한 복사가 완료 되었습니다.");
                    this.Close();
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

        #region ○ 기타이벤트

        private void SimpleButton_Click(object sender, EventArgs e)
        {
            SimpleButton pCmd = sender as SimpleButton;
            string pCls = pCmd.Name.Substring(1);

            switch (pCls)
            {
                case "AuthCopy":
                    DialogResult _DialogResult = CustomMsg.ShowMessage("권한 복사를 진행하시겠습니까?", "Question", MessageBoxButtons.YesNo);
                    if (_DialogResult == DialogResult.Yes)
                    {
                        MainSave_InputData();
                    }
                    break;
                case "Close":
                    this.Close();
                    break;
            }
        }

        #endregion
    }
}
