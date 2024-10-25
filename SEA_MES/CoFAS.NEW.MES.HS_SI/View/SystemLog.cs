using FarPoint.Win.Spread;
using CoFAS.NEW.MES.HS_SI.Business;
using CoFAS.NEW.MES.HS_SI.Entity;
using System;
using System.Data;
using System.Windows.Forms;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core;

namespace CoFAS.NEW.MES.HS_SI
{
    public partial class SystemLog : frmBaseNone
    {
        #region ○ 변수생성

        private SystemLog_Entity _Entity = null;
        private SystemLogEntity _SystemLogEntity = null;
        private bool _FirstYn = true;

        #endregion

        #region ○ 생성자

        public SystemLog()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);
        }

        #endregion

        #region ○ 폼 이벤트 영역

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            InitializeButton();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeControl();
                initializeSpread();

                _SystemLogEntity = new SystemLogEntity();

                // 버튼이벤트 생성
                SearchButtonClicked += new EventHandler(_SearchButtonClicked);
                PrintButtonClicked += new EventHandler(_PrintButtonClicked);
                DeleteButtonClicked += new EventHandler(_DeleteButtonClicked);
                SaveButtonClicked += new EventHandler(_SaveButtonClicked);
                ImportButtonClicked += new EventHandler(_ImportButtonClicked);
                ExportButtonClicked += new EventHandler(_ExportButtonClicked);
                InitialButtonClicked += new EventHandler(_InitialButtonClicked);
                AddItemButtonClicked += new EventHandler(_AddItemButtonClicked);
                CloseButtonClicked += new EventHandler(_CloseButtonClicked);

                fpLeft.CellClick += FpLeft_CellClick;

                LeftFind_DisplayData();
            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 버튼 이벤트 영역

        private void _SearchButtonClicked(object sender, EventArgs e)
        {
            LeftFind_DisplayData();
        }

        private void _PrintButtonClicked(object sender, EventArgs e)
        {
            CustomMsg.ShowMessage("기능을 지원하지 않습니다..");
            return;
        }

        private void _DeleteButtonClicked(object sender, EventArgs e)
        {
            CustomMsg.ShowMessage("기능을 지원하지 않습니다..");
            return;
        }

        private void _SaveButtonClicked(object sender, EventArgs e)
        {
            CustomMsg.ShowMessage("기능을 지원하지 않습니다..");
            return;
        }

        private void _ImportButtonClicked(object sender, EventArgs e)
        {
            CustomMsg.ShowMessage("기능을 지원하지 않습니다..");
            return;
        }

        private void _ExportButtonClicked(object sender, EventArgs e)
        {
            CustomMsg.ShowMessage("기능을 지원하지 않습니다..");
            return;
        }

        private void _InitialButtonClicked(object sender, EventArgs e)
        {
            CustomMsg.ShowMessage("기능을 지원하지 않습니다..");
            return;
        }

        private void _AddItemButtonClicked(object sender, EventArgs e)
        {
            CustomMsg.ShowMessage("기능을 지원하지 않습니다..");
            return;
        }

        private void _CloseButtonClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeButton()
        {
            try
            {
                MainFormButtonSetting _MainFormButtonSetting = new MainFormButtonSetting();

                DataTable _DataTable = new MenuButton_Business().MenuButton_Select(_pMenuSettingEntity.MENU_NO, _Entity.user_authority);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    _MainFormButtonSetting.UseYNSearchButton = _DataTable.Rows[0]["menu_search"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNPrintButton = _DataTable.Rows[0]["menu_print"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNDeleteButton = _DataTable.Rows[0]["menu_delete"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNSaveButton = _DataTable.Rows[0]["menu_save"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNImportButton = _DataTable.Rows[0]["menu_import"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNExportButton = _DataTable.Rows[0]["menu_export"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNInitialButton = _DataTable.Rows[0]["menu_initialize"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNAddItemButton = _DataTable.Rows[0]["menu_newadd"].ToString() == "Y" ? true : false;
                    _MainFormButtonSetting.UseYNFormCloseButton = true;
                }
                else
                {
                    _MainFormButtonSetting.UseYNSearchButton = false;
                    _MainFormButtonSetting.UseYNPrintButton = false;
                    _MainFormButtonSetting.UseYNDeleteButton = false;
                    _MainFormButtonSetting.UseYNSaveButton = false;
                    _MainFormButtonSetting.UseYNImportButton = false;
                    _MainFormButtonSetting.UseYNExportButton = false;
                    _MainFormButtonSetting.UseYNInitialButton = false;
                    _MainFormButtonSetting.UseYNAddItemButton = false;
                    _MainFormButtonSetting.UseYNFormCloseButton = true;
                }

                MainForm.SetButtonSetting(_MainFormButtonSetting);
            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
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
                    _Entity = new SystemLog_Entity();
                    _Entity.user_account = MainForm.UserEntity.user_account;
                    _Entity.user_name = MainForm.UserEntity.user_name;
                    _Entity.dept_code = MainForm.UserEntity.user_dept;
                    _Entity.dept_name = MainForm.UserEntity.dept_name;
                    _Entity.user_authority = MainForm.UserEntity.user_authority;

                    //_SearchStart.DateTime = DateTime.Now.AddDays(-3);
                    //_SearchEnd.DateTime = DateTime.Now;
                }

                fpLeft.Sheets[0].Rows.Count = 0;
                fpMain.Sheets[0].Rows.Count = 0;
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

        #endregion

        #region 스프레드 영역

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
            pSetData += " 에러               :21  :1   :2   :1   :12  :0   :0   :    :1                     :                       "; // 에러체크
            pSetData += ",에러명             :21  :1   :2   :0   :32  :2000:0   :    :                      :                       "; // 에러메시지
            pSetData += ",체크               :21  :1   :2   :1   :12  :0   :0   :    :0                     :                       "; // 체크
            pSetData += ",선택               :21  :1   :2   :1   :12  :0   :0   :    :2                     :                       "; // 선택
            pSetData += ",예비4              :100 :1   :2   :1   :32  :100 :0   :    :                      :                       ";
            pSetData += ",예비5              :100 :1   :2   :1   :32  :100 :0   :    :                      :                       ";
            // ▲  공통부분 -------------------------------------------------------------------------------------------------------------------------------------------
            // ▼  추기 필요한 부분 -----------------------------------------------------------------------------------------------------------------------------------
            pSetData += ",사용자계정         :100 :0   :2   :1   :32  :100 :0   :    :                      :user_account           ";
            pSetData += ",이름               :100 :0   :2   :1   :32  :100 :0   :    :                      :User_name              ";
            // ----------------------------------------------------------------------------------------------------------------------------------------------------
   

            FpSpreadManager.SpreadSetHeader(fpLeft, "조회", 0, pSetData, this.Name, _Entity.user_account);
            // fpMain ---------------------------------------------------------------------------------------------------------------------------------------------------
            // 콤보가 있을때 반드시 설정
            FpSpreadManager.pConnectionString = ConfigrationManager.ConnectionString;

            // 스프레드 기본설정
            FpSpreadManager.pOperationType = OperationMode.RowMode; // 읽기전용설정
            FpSpreadManager.SpreadSetStyle(fpMain);                 // 스프레드 전체설정      
            FpSpreadManager.SpreadSetSheetStyle(fpMain, 0);         // 스프레드 쉬트설정

            // 스프레드 쉬트 설정
            pSetData = string.Empty;
            //           11 버튼 12 체크박스 14 콤보박스 25 날짜시간 28 마스크셀타입 29 숫자 32 텍스트
            //           라벨//넓이//보임0,숨김1//일기전용0,수정가능1,읽기전용기본색상2//왼쪽정렬0,중앙정렬1,오른쪽정렬2//셀타입//길이//소숫점//유형//그룹코드
            //           --------------------:넓이:보임:읽기:정렬:타입:길이:소수:유형:그룹코드(추가사항등)  :필드명
            pSetData += " 에러               :21  :1   :2   :1   :12  :0   :0   :    :1                     :                       "; // 에러체크
            pSetData += ",에러명             :21  :1   :2   :0   :32  :2000:0   :    :                      :                       "; // 에러메시지
            pSetData += ",체크               :21  :1   :2   :1   :12  :0   :0   :    :0                     :                       "; // 체크
            pSetData += ",선택               :21  :1   :2   :1   :12  :0   :0   :    :2                     :                       "; // 선택
            pSetData += ",예비4              :100 :1   :2   :1   :32  :100 :0   :    :                      :                       ";
            pSetData += ",예비5              :100 :1   :2   :1   :32  :100 :0   :    :                      :                       ";
            // ▲  공통부분 -------------------------------------------------------------------------------------------------------------------------------------------
            // ▼  추기 필요한 부분 -----------------------------------------------------------------------------------------------------------------------------------
            pSetData += ",Event_Time         :200 :0   :2   :1   :32  :100 :0   :    :                      :event_time             ";
            pSetData += ",Event_IP           :150 :0   :2   :1   :32  :100 :0   :    :                      :event_ip               ";
            pSetData += ",Event_Mac          :150 :0   :2   :1   :32  :100 :0   :    :                      :event_mac              ";
            pSetData += ",PC_Name            :150 :0   :2   :1   :32  :100 :0   :    :                      :event_name             ";
            pSetData += ",Event_Type         :100 :0   :2   :1   :32  :100 :0   :    :                      :event_type             ";
            pSetData += ",Event_Log          :400 :0   :2   :0   :32  :100 :0   :    :                      :event_log              ";
            // ----------------------------------------------------------------------------------------------------------------------------------------------------
    
            FpSpreadManager.SpreadSetHeader(fpMain, "조회", 0, pSetData, this.Name, _Entity.user_account);

        }

        private void FpLeft_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.RowHeader)
                return;

            _Entity._UserName = fpLeft.Sheets[0].GetValue(e.Row, "user_account").ToString();
            MainFind_DisplayData(_Entity._UserName);
        }

        #endregion

        #region 데이터 영역

        private void LeftFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                DataTable _DataTable = new SystemLog_Business().SystemLogStatus_R10();
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpLeft.Sheets[0].Visible = false;
                    fpLeft.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpLeft.Sheets[0].SetValue(i, "user_account".Trim(), _DataTable.Rows[i]["user_account".Trim()].ToString());
                        fpLeft.Sheets[0].SetValue(i, "User_name   ".Trim(), _DataTable.Rows[i]["User_name   ".Trim()].ToString());
                    }

                    fpLeft.Sheets[0].Visible = true;
                }
                else
                {
                    fpLeft.Sheets[0].Rows.Count = 0;
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

        private void MainFind_DisplayData(string _userAccount)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                _Entity._SearchStart = base_FromtoDateTime1.StartValue.ToString("yyyy-MM-dd");
                _Entity._SearchEnd = base_FromtoDateTime1.EndValue.ToString("yyyy-MM-dd");

                DataTable _DataTable = new SystemLog_Business().SystemLogStatus_R20(_Entity);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpMain.Sheets[0].SetValue(i, "event_time  ".Trim(), _DataTable.Rows[i]["event_time  ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "event_ip    ".Trim(), _DataTable.Rows[i]["event_ip    ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "event_mac   ".Trim(), _DataTable.Rows[i]["event_mac   ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "event_name  ".Trim(), _DataTable.Rows[i]["event_name  ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "event_type  ".Trim(), _DataTable.Rows[i]["event_type  ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "event_log   ".Trim(), _DataTable.Rows[i]["event_log   ".Trim()].ToString());
                    }

                    fpMain.Sheets[0].Visible = true;

                }
                else
                {
                    fpMain.Sheets[0].Rows.Count = 0;
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

        #endregion

        #region 기타이벤트 영역

        #endregion
    }
}
