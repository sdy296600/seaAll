using FarPoint.Win.Spread;
using CoFAS.NEW.MES.HS_SI.Business;
using CoFAS.NEW.MES.HS_SI.Entity;
using System;
using System.Data;
using System.Windows.Forms;
using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core.Business;
using DevExpress.XtraEditors;
using CoFAS.NEW.MES.HT_SI;

namespace CoFAS.NEW.MES.HS_SI
{
    public partial class MenuInformation : frmBaseNone
    {
        #region ○ 변수선언

        private MenuInformation_Entity _Entity = null;
        private SystemLogEntity _SystemLogEntity = null;
        private bool _FirstYn = true;

        #endregion

        #region ○ 생성자

        public MenuInformation()
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
                fpMain.Change += FpMain_Change;

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
            if (fpMain.Sheets[0].Rows.Count > 0)
                MainSave_InputData();
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
            InitializeControl();
        }

        private void _AddItemButtonClicked(object sender, EventArgs e)
        {
            // 신규등록 : 메뉴리스트 라인추가

            if (_Entity.p_menu_id == "")
            {
                CustomMsg.ShowMessage("상위메뉴를 선택하시기 바랍니다.");
                return;
            }

            FpSpreadManager.SpreadRowAdd(fpMain, 0);
            int pRow = fpMain.Sheets[0].ActiveRowIndex;
            fpMain.Sheets[0].SetValue(pRow, "menu_id", "0");
            fpMain.Sheets[0].SetValue(pRow, "menu_name", "");
            fpMain.Sheets[0].SetValue(pRow, "p_menu_id", _Entity.p_menu_id);
            fpMain.Sheets[0].SetValue(pRow, "window_name", "");
            fpMain.Sheets[0].SetValue(pRow, "module", _Entity.p_module);
            fpMain.Sheets[0].SetValue(pRow, "icon", "menu_icon");
            fpMain.Sheets[0].SetValue(pRow, "sort", "0");
            fpMain.Sheets[0].SetValue(pRow, "description", "");
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
                    _Entity = new MenuInformation_Entity();
                    _Entity.user_account = MainForm.UserEntity.user_account;
                    _Entity.user_name = MainForm.UserEntity.user_name;
                    _Entity.dept_code = MainForm.UserEntity.user_dept;
                    _Entity.dept_name = MainForm.UserEntity.dept_name;
                    _Entity.user_authority = MainForm.UserEntity.user_authority;

                    c.AddValue(CodeSelect.ComboBoxSet("모듈"), 0, 0, "", true);
                }

                _Entity.p_module = "";
                _Entity.p_menu_id = "";

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

        #region ○ 스프레드 영역

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
            pSetData += ",메뉴아이디         :70  :1   :2   :1   :32  :100 :0   :    :                      :menu_id                ";
            pSetData += ",모듈               :70  :0   :2   :1   :32  :100 :0   :    :                      :module                 ";
            pSetData += ",메뉴명             :120 :0   :2   :1   :32  :100 :0   :    :                      :module_name            ";
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
            pSetData += ",메뉴\r\n아이디     :70  :1   :0   :1   :32  :100 :0   :    :                      :menu_id                ";
            pSetData += ",메뉴명*            :200 :0   :1   :0   :32  :100 :0   :    :                      :menu_name              ";
            pSetData += ",상위메뉴           :70  :1   :0   :1   :32  :100 :0   :    :                      :p_menu_id              ";
            pSetData += ",윈도우명*          :250 :0   :1   :0   :32  :100 :0   :    :                      :window_name            ";
            pSetData += ",모듈               :70  :0   :0   :1   :32  :100 :0   :    :                      :module                 ";
            pSetData += ",아이콘             :80  :0   :0   :1   :32  :100 :0   :    :                      :icon                   ";
            pSetData += ",사용               :50  :0   :1   :1   :12  :100 :0   :    :5                     :use_yn                 ";
            pSetData += ",순서               :70  :0   :1   :1   :32  :100 :0   :    :                      :sort                   ";
            pSetData += ",비고               :250 :0   :1   :0   :32  :100 :0   :    :                      :description            ";
            // ----------------------------------------------------------------------------------------------------------------------------------------------------
            FpSpreadManager.SpreadSetHeader(fpMain, "조회", 0, pSetData, this.Name, _Entity.user_account);
        }

        private void FpLeft_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            _Entity.p_menu_id = fpLeft.Sheets[0].GetValue(e.Row, "menu_id").ToString();
            _Entity.p_module = fpLeft.Sheets[0].GetValue(e.Row, "module").ToString();
            MainFind_DisplayData();
        }


        private void FpMain_Change(object sender, ChangeEventArgs e)
        {
            try
            {
                if (e.Column < 6)
                    return;

                string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                switch (pHeaderLabel)
                {
                    case "":
                        fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text = "수정";
                        break;
                    case "입력":
                        break;
                    case "수정":
                        break;
                    case "삭제":
                        break;
                }
            }
            catch (Exception ex)
            {
                CustomMsg.ShowMessage(ex.ToString());
            }
        }

        #endregion

        #region ○ 데이터 영역

        private void LeftFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                _Entity._SearchModule = c.GetValue();

                DataTable _DataTable = new MenuInformation_Business().MenuInformation_R10(_Entity);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpLeft.Sheets[0].Visible = false;
                    fpLeft.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpLeft.Sheets[0].SetValue(i, "menu_id    ".Trim(), _DataTable.Rows[i]["menu_id    ".Trim()].ToString());
                        fpLeft.Sheets[0].SetValue(i, "module     ".Trim(), _DataTable.Rows[i]["module     ".Trim()].ToString());
                        fpLeft.Sheets[0].SetValue(i, "module_name".Trim(), _DataTable.Rows[i]["module_name".Trim()].ToString());
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

        private void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                DataTable _DataTable = new MenuInformation_Business().MenuInformation_R20(_Entity.p_menu_id);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpMain.Sheets[0].SetValue(i, "menu_id     ".Trim(), _DataTable.Rows[i]["menu_id     ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "menu_name   ".Trim(), _DataTable.Rows[i]["menu_name   ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "p_menu_id   ".Trim(), _DataTable.Rows[i]["p_menu_id   ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "window_name ".Trim(), _DataTable.Rows[i]["window_name ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "module      ".Trim(), _DataTable.Rows[i]["module      ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "icon        ".Trim(), _DataTable.Rows[i]["icon        ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "use_yn      ".Trim(), _DataTable.Rows[i]["use_yn      ".Trim()].ToString() == "Y" ? true : false);
                        fpMain.Sheets[0].SetValue(i, "sort        ".Trim(), _DataTable.Rows[i]["sort        ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "description ".Trim(), _DataTable.Rows[i]["description ".Trim()].ToString());
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

        private void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Focus();
                bool _Error = new MenuInformation_Business().MenuInformation_A10(_Entity, ref fpMain);
                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");

                    string pMenu_id = _Entity.p_menu_id;

                    int pRow = 0;
                    for (int i = 0; i < fpLeft.Sheets[0].Rows.Count; i++)
                    {
                        if (pMenu_id == fpLeft.Sheets[0].GetText(i, "menu_id"))
                        {
                            pRow = i;
                            break;
                        }
                    }

                    fpMain.Sheets[0].Rows.Count = 0;
                    fpLeft.Sheets[0].SetActiveCell(pRow, 6, true);
                    fpLeft.ShowActiveCell(VerticalPosition.Center, HorizontalPosition.Center);
                    FpLeft_CellClick(fpLeft, new CellClickEventArgs(null, pRow, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
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

        #region ○ 기타이벤트 영역

        private void SimpleButton_Click(object sender, EventArgs e)
        {
            SimpleButton pCmd = sender as SimpleButton;
            string pCls = pCmd.Name.Substring(1);

            switch (pCls)
            {
                case "ModuleAdd":
                    MenuInformation_P10 _MenuInformation_P10 = new MenuInformation_P10();
                    _MenuInformation_P10._ModuleAddEvent += _MenuInformation_P10__ModuleAddEvent;
                    _MenuInformation_P10._Entity = _Entity;
                    _MenuInformation_P10.ShowDialog();
                    break;
            }
        }

        private void _MenuInformation_P10__ModuleAddEvent(object obj)
        {
            c.AddValue(CodeSelect.ComboBoxSet("모듈"), 0, 0, "", true);
            LeftFind_DisplayData();
        }

        private void _LookUpEdit_ValueChanged(object obj, EventArgs e)
        {
            if (!_FirstYn)
            {
                LeftFind_DisplayData();
            }
        }

        #endregion
    }
}
