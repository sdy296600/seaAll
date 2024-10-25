using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.HS_SI.Entity;
using DevExpress.XtraEditors;
using FarPoint.Win.Spread;
using CoFAS.NEW.MES.SW_SI.Business;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoFAS.NEW.MES.HT_SI;
using CoFAS.NEW.MES.Core;

namespace CoFAS.NEW.MES.HS_SI
{
    public partial class AuthInformation : frmBaseNone
    {
        #region ○ 변수선언

        public AuthInformation_Entity _Entity = null;
        public SystemLogEntity _SystemLogEntity = null;
        private bool _FirstYn = true;
        public string _CellClickAccount = string.Empty;

        #endregion

        #region ○ 생성자

        public AuthInformation()
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

                fpLeft.CellClick += new CellClickEventHandler(fpLeft_CellClick);
                fpMain._ChangeEventHandler += new ChangeEventHandler(fpMain_Change);
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
                    _Entity = new AuthInformation_Entity();
                    _Entity.user_account = MainForm.UserEntity.user_account;
                    _Entity.user_name = MainForm.UserEntity.user_name;
                    _Entity.dept_code = MainForm.UserEntity.user_dept;
                    _Entity.dept_name = MainForm.UserEntity.dept_name;
                    _Entity.user_authority = MainForm.UserEntity.user_authority;

                }

                _Authority_name.Text = "";
                _CellClickAccount = "";

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
            // fpMain ---------------------------------------------------------------------------------------------------------------------------------------------------
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
            pSetData += ",체크               :21  :1   :2   :1   :12  :0   :0   :             :0                     :                       "; // 체크
            pSetData += ",선택               :21  :1   :2   :1   :12  :0   :0   :             :2                     :                       "; // 선택
            pSetData += ",예비4              :100 :1   :2   :1   :32  :100 :0   :             :                      :                       ";
            pSetData += ",예비5              :100 :1   :2   :1   :32  :100 :0   :             :                      :                       ";
            // ▲  공통부분 -------------------------------------------------------------------------------------------------------------------------------------------
            // ▼  추기 필요한 부분 -----------------------------------------------------------------------------------------------------------------------------------
            pSetData += ",권한ID           :100 :1   :2   :1   :32  :100 :0   :             :                      :code           ";
            pSetData += ",권한명           :200 :0   :2   :1   :32  :100 :0   :             :                      :code_name      ";
            // ----------------------------------------------------------------------------------------------------------------------------------------------------

            FpSpreadManager.SpreadSetHeader(fpLeft, "조회", 0, pSetData, this.Name, _Entity.user_account);



            // fpMain ---------------------------------------------------------------------------------------------------------------------------------------------------
            // 콤보가 있을때 반드시 설정
            FpSpreadManager.pConnectionString = ConfigrationManager.ConnectionString;

            // 스프레드 기본설정
            FpSpreadManager.pOperationType = OperationMode.Normal;  // 읽기전용설정
            FpSpreadManager.SpreadSetStyle(fpMain);                 // 스프레드 전체설정      
            FpSpreadManager.SpreadSetSheetStyle(fpMain, 0);         // 스프레드 쉬트설정

            // 스프레드 쉬트 설정
            pSetData = string.Empty;
            //           11 버튼 12 체크박스 14 콤보박스 25 날짜시간 28 마스크셀타입 29 숫자 32 텍스트
            //           라벨//넓이//보임0,숨김1//일기전용0,수정가능1,읽기전용기본색상2//왼쪽정렬0,중앙정렬1,오른쪽정렬2//셀타입//길이//소숫점//유형//그룹코드
            //           --------------------:넓이:보임:읽기:정렬:타입:길이:소수:유형:그룹코드(추가사항등)  :필드명
            pSetData += " 에러               :21  :1   :2   :1   :12  :0   :0   :             :1                     :                       "; // 에러체크
            pSetData += ",에러명             :21  :1   :2   :0   :32  :2000:0   :             :                      :                       "; // 에러메시지
            pSetData += ",체크               :21  :1   :2   :1   :12  :0   :0   :             :0                     :                       "; // 체크
            pSetData += ",선택               :21  :1   :2   :1   :12  :0   :0   :             :2                     :                       "; // 선택
            pSetData += ",예비4              :100 :1   :2   :1   :32  :100 :0   :             :                      :                       ";
            pSetData += ",예비5              :100 :1   :2   :1   :32  :100 :0   :             :                      :                       ";
            // ▲  공통부분 -------------------------------------------------------------------------------------------------------------------------------------------
            // ▼  추기 필요한 부분 -----------------------------------------------------------------------------------------------------------------------------------
            pSetData += ",메뉴아이디         :100 :1   :1   :1   :32  :100 :0   :             :                      :menu_id                ";
            pSetData += ",모듈               :100 :0   :1   :1   :32  :100 :0   :             :                      :p_menu_name            ";
            pSetData += ",메뉴명             :250 :0   :1   :0   :32  :100 :0   :             :                      :menu_name              ";
            pSetData += ",사용*              :100 :0   :1   :1   :12  :100 :0   :             :5                     :menu_useyn             ";
            pSetData += ",조회*              :100 :0   :1   :1   :12  :100 :0   :             :5                     :menu_search            ";
            pSetData += ",인쇄*              :100 :0   :1   :1   :12  :100 :0   :             :5                     :menu_print             ";
            pSetData += ",삭제*              :100 :0   :1   :1   :12  :100 :0   :             :5                     :menu_delete            ";
            pSetData += ",저장*              :100 :0   :1   :1   :12  :100 :0   :             :5                     :menu_save              ";
            pSetData += ",가져오기*          :100 :0   :1   :1   :12  :100 :0   :             :5                     :menu_import            ";
            pSetData += ",내보내기*          :100 :0   :1   :1   :12  :100 :0   :             :5                     :menu_export            ";
            pSetData += ",초기화*            :100 :0   :1   :1   :12  :100 :0   :             :5                     :menu_initialize        ";
            pSetData += ",신규추가*          :100 :0   :1   :1   :12  :100 :0   :             :5                     :menu_newadd            ";
            // ----------------------------------------------------------------------------------------------------------------------------------------------------

            FpSpreadManager.SpreadSetHeader(fpMain, "조회", 0, pSetData, this.Name, _Entity.user_account);

            fpMain.Sheets[0].SetColumnMerge(7, FarPoint.Win.Spread.Model.MergePolicy.Always);
        }

        private void fpLeft_CellClick(object obj, CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            _CellClickAccount = fpLeft.Sheets[0].GetValue(e.Row, "code").ToString();
            MainFind_DisplayData(_CellClickAccount);
        }

        private void fpMain_Change(object sender, ChangeEventArgs e)
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

                _Entity._Authority_name = _Authority_name.Text;

                fpLeft.Sheets[0].Rows.Count = 0;
                fpMain.Sheets[0].Rows.Count = 0;
                DataTable _DataTable = new AuthInformation_Business().AuthInformation_R10(_Entity);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpLeft.Sheets[0].Visible = false;
                    fpLeft.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpLeft.Sheets[0].SetValue(i, "code     ".Trim(), _DataTable.Rows[i]["code     ".Trim()].ToString());
                        fpLeft.Sheets[0].SetValue(i, "code_name".Trim(), _DataTable.Rows[i]["code_name".Trim()].ToString());
                      
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

        private void MainFind_DisplayData(string _UserAccount)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;
                DataTable _DataTable = new AuthInformation_Business().AuthInformation_R20(_UserAccount);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpMain.Sheets[0].SetValue(i, "menu_id         ".Trim(), _DataTable.Rows[i]["menu_id         ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "p_menu_name     ".Trim(), _DataTable.Rows[i]["p_menu_name     ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "menu_name       ".Trim(), _DataTable.Rows[i]["menu_name       ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "menu_useyn      ".Trim(), _DataTable.Rows[i]["menu_useyn      ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "menu_search     ".Trim(), _DataTable.Rows[i]["menu_search     ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "menu_print      ".Trim(), _DataTable.Rows[i]["menu_print      ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "menu_delete     ".Trim(), _DataTable.Rows[i]["menu_delete     ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "menu_save       ".Trim(), _DataTable.Rows[i]["menu_save       ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "menu_import     ".Trim(), _DataTable.Rows[i]["menu_import     ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "menu_export     ".Trim(), _DataTable.Rows[i]["menu_export     ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "menu_initialize ".Trim(), _DataTable.Rows[i]["menu_initialize ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "menu_newadd     ".Trim(), _DataTable.Rows[i]["menu_newadd     ".Trim()].ToString());

                        if (fpMain.Sheets[0].GetValue(i, "menu_name").ToString().Contains("0."))
                        {
                            fpMain.Sheets[0].Rows[i].BackColor = Color.Linen;
                            // 버튼 설정 컬럼 사용금지 할 것!!!
                            fpMain.Sheets[0].Cells[i, 10].Locked = true;
                            fpMain.Sheets[0].Cells[i, 11].Locked = true;
                            fpMain.Sheets[0].Cells[i, 12].Locked = true;
                            fpMain.Sheets[0].Cells[i, 13].Locked = true;
                            fpMain.Sheets[0].Cells[i, 14].Locked = true;
                            fpMain.Sheets[0].Cells[i, 15].Locked = true;
                            fpMain.Sheets[0].Cells[i, 16].Locked = true;
                            fpMain.Sheets[0].Cells[i, 17].Locked = true;
                        }
                    }
                    fpMain.Sheets[0].Visible = true;
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

                fpMain.Focus();
                bool _Error = new AuthInformation_Business().AuthInformation_A10(_Entity, _CellClickAccount, ref fpMain);
                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");
                    DisplayMessage("저장 되었습니다.");

                    fpMain.Sheets[0].Rows.Count = 0;

                    int pRow = 0;
                    for (int i = 0; i < fpLeft.Sheets[0].Rows.Count; i++)
                    {
                        if (_CellClickAccount == fpLeft.Sheets[0].GetText(i, "user_account"))
                        {
                            pRow = i;
                            break;
                        }
                    }

                    fpLeft.Sheets[0].SetActiveCell(pRow, 6, true);
                    fpLeft.ShowActiveCell(VerticalPosition.Center, HorizontalPosition.Center);
                    fpLeft_CellClick(fpLeft, new CellClickEventArgs(null, pRow, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));
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

        private void textEdit_KeyDown(object obj, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LeftFind_DisplayData();
            }
        }

        private void LookupEdit_ValueChanged(object sender, EventArgs e)
        {
            if (!_FirstYn)
                LeftFind_DisplayData();
        }

        private void SimpleButton_Click(object obj, EventArgs e)
        {
            SimpleButton pCmd = obj as SimpleButton;
            string pCls = pCmd.Name.Substring(1);

            switch (pCls)
            {
                case "AuthCopy":
                    if (_Entity.user_authority == "CD03003" || _Entity.user_account == "administrator")
                    {
                        AuthInformation_P10 _AuthInformation = new AuthInformation_P10();
                        _AuthInformation.Title = "권한 복사";
                        _AuthInformation._UserAccount = _Entity.user_account;
                        _AuthInformation._UserAuthority = _Entity.user_authority;
                        _AuthInformation.ShowDialog();

                        LeftFind_DisplayData();
                    }
                    break;
                default:
                    break;
            }

        }

        #endregion

      
    }
}
