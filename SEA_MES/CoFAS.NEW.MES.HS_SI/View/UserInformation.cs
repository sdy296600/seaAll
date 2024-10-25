using FarPoint.Win.Spread;
using CoFAS.NEW.MES.HS_SI.Entity;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.HS_SI.Business;
using CoFAS.NEW.MES.Core.Business;

namespace CoFAS.NEW.MES.HS_SI
{
    public partial class UserInformation : frmBaseNone
    {
        #region ○ 변수선언

        public UserInformation_Entity _Entity = null;
        public SystemLogEntity _SystemLogEntity = null;
        private bool _FirstYn = true;

        #endregion

        #region ○ 생성자

        public UserInformation()
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

                fpMain.Change += FpMain_Change;
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
            MainFind_DisplayData();
        }

        private void _PrintButtonClicked(object sender, EventArgs e)
        {
        }

        private void _DeleteButtonClicked(object sender, EventArgs e)
        {
            if (fpMain.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text == "입력")
            {
                DialogResult _DialogResult1 = CustomMsg.ShowMessage("현재 입력하는 로우를 삭제하시겠습니까?", "Question", MessageBoxButtons.YesNo);
                if (_DialogResult1 == DialogResult.Yes)
                {
                    FpSpreadManager.SpreadRowRemove(fpMain, 0, fpMain.Sheets[0].ActiveRowIndex);
                }
            }
            else
            {
                DialogResult _DialogResult2 = CustomMsg.ShowMessage("'체크' 선택된 사용자를 삭제하시겠습니까?\r\n'입력' 중 내용도 삭제가 됩니다.", "Question", MessageBoxButtons.YesNo);
                if (_DialogResult2 == DialogResult.Yes)
                {
                    MainDelete_InputData();
                }
            }
        }

        private void _SaveButtonClicked(object sender, EventArgs e)
        {
            int _emptyQty = 0;
            for(int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
            {
                if (fpMain.Sheets[0].GetValue(i, "user_account  ".Trim()).ToString() == "")
                    _emptyQty++;
                if (fpMain.Sheets[0].GetValue(i, "user_name     ".Trim()).ToString() == "")
                    _emptyQty++;
                if (fpMain.Sheets[0].GetValue(i, "user_authority".Trim()).ToString() == "")
                    _emptyQty++;
                if (fpMain.Sheets[0].GetValue(i, "user_dept     ".Trim()).ToString() == "")
                    _emptyQty++;
                if (fpMain.Sheets[0].GetValue(i, "user_title     ".Trim()).ToString() == "")
                    _emptyQty++;
            }

            if(_emptyQty > 0)
            {
                CustomMsg.ShowMessage("필수 항목을 입력하시기 바랍니다.\r\n[필수항목 : 사용자아이디, 사용자명, 권한, 부서, 직책]", "Error", MessageBoxButtons.OK);
                return;
            }

            if (fpMain.Sheets[0].Rows.Count > 0)
                MainSave_InputData();
        }

        private void _ImportButtonClicked(object sender, EventArgs e)
        {
        }

        private void _ExportButtonClicked(object sender, EventArgs e)
        {
        }

        private void _InitialButtonClicked(object sender, EventArgs e)
        {
            InitializeControl();
        }

        private void _AddItemButtonClicked(object sender, EventArgs e)
        {
            FpSpreadManager.SpreadRowAdd(fpMain, 0);
            fpMain.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text = "입력";
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
                    _Entity = new UserInformation_Entity();
                    _Entity.user_account = MainForm.UserEntity.user_account;
                    _Entity.user_name = MainForm.UserEntity.user_name;
                    _Entity.dept_code = MainForm.UserEntity.user_dept;
                    _Entity.dept_name = MainForm.UserEntity.dept_name;
                    _Entity.user_authority = MainForm.UserEntity.user_authority;


                    _SearchDept.AddValue(CodeSelect.ComboBoxSet("CD01"), 0, 0, "", true);
                  
                }

                _SearchUser.Text = "";

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
            FpSpreadManager.pOperationType = OperationMode.Normal; // 읽기전용설정
            FpSpreadManager.SpreadSetStyle(fpMain);                 // 스프레드 전체설정      
            FpSpreadManager.SpreadSetSheetStyle(fpMain, 0);         // 스프레드 쉬트설정

            // 스프레드 쉬트 설정
            string pSetData = string.Empty;
            //           11 버튼 12 체크박스 14 콤보박스 17 이미지 25 날짜시간 28 마스크셀타입 29 숫자 32 텍스트
            //           라벨//넓이//보임0,숨김1//일기전용0,수정가능1,읽기전용기본색상2//왼쪽정렬0,중앙정렬1,오른쪽정렬2//셀타입//길이//소숫점//유형//그룹코드
            //           --------------------:넓이:보임:읽기:정렬:타입:길이:소수:유형:그룹코드(추가사항등)  :필드명
            pSetData += " 에러               :21  :1   :2   :1   :12  :0   :0   :         :1                     :error          "; // 에러체크
            pSetData += ",에러명             :21  :1   :2   :0   :32  :2000:0   :         :                      :error_name     "; // 에러메시지
            pSetData += ",체크               :21  :0   :1   :1   :12  :0   :0   :         :5                     :check          "; // 체크
            pSetData += ",선택               :21  :1   :2   :1   :12  :0   :0   :         :2                     :               "; // 선택
            pSetData += ",예비4              :100 :1   :2   :1   :32  :100 :0   :         :                      :sub4           ";
            pSetData += ",예비5              :100 :1   :2   :1   :32  :100 :0   :         :                      :sub5           ";
            // ▲  공통부분 -------------------------------------------------------------------------------------------------------------------------------------------
            // ▼  추기 필요한 부분 -----------------------------------------------------------------------------------------------------------------------------------
            pSetData += ",사용자아이디*      :130 :0   :1   :1   :32  :100 :0   :         :                      :user_account   ";        
            pSetData += ",사용자명*          :100 :0   :1   :1   :32  :100 :0   :         :                      :user_name      ";
            pSetData += ",권한*              :100 :0   :1   :1   :14  :100 :0   :authority:                      :user_authority ";
            pSetData += ",공장*              :100 :0   :1   :1   :14  :100 :0   :factory  :                      :user_factory   ";
            pSetData += ",부서*              :100 :0   :1   :1   :14  :100 :0   :dept     :                      :user_dept      ";
            pSetData += ",직책*              :100 :0   :1   :1   :14  :100 :0   :title    :                      :user_title     ";
            pSetData += ",메일               :200 :0   :1   :0   :32  :100 :0   :         :                      :user_mail      ";
            pSetData += ",전화               :100 :0   :1   :1   :32  :100 :0   :         :                      :user_phone     ";
            pSetData += ",팩스               :100 :0   :1   :1   :32  :100 :0   :         :                      :user_fax       ";
            pSetData += ",내선번호           :100 :0   :1   :1   :32  :100 :0   :         :                      :user_in_tel    ";
            pSetData += ",입사일             :100 :0   :1   :1   :28  :100 :0   :         :####-##-##            :user_entry     ";
            pSetData += ",퇴사일             :100 :0   :1   :1   :28  :100 :0   :         :####-##-##            :user_leave     ";
            pSetData += ",사원번호           :80  :0   :1   :1   :32  :100 :0   :         :                      :user_emp       ";
            pSetData += ",비고               :200 :0   :1   :0   :32  :100 :0   :         :                      :description    ";

            // ----------------------------------------------------------------------------------------------------------------------------------------------------
            FpSpreadManager.SpreadSetHeader(fpMain, "조회", 0, pSetData,this.Name, _Entity.user_account);


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

        private void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

           
                _Entity._SearchUser = _SearchUser.Text;
                _Entity._SearchDept = _SearchDept.GetValue().ToString();


                DataTable _DataTable = new UserInformation_Business().UserInformation_R10(_Entity);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpMain.Sheets[0].SetValue(i, "user_account   ".Trim(), _DataTable.Rows[i]["user_account   ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "user_name      ".Trim(), _DataTable.Rows[i]["user_name      ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "user_authority ".Trim(), _DataTable.Rows[i]["user_authority ".Trim()].ToString());                  
                        fpMain.Sheets[0].SetValue(i, "user_factory   ".Trim(), _DataTable.Rows[i]["user_factory   ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "user_dept      ".Trim(), _DataTable.Rows[i]["user_dept      ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "user_title     ".Trim(), _DataTable.Rows[i]["user_title     ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "user_mail      ".Trim(), _DataTable.Rows[i]["user_mail      ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "user_phone     ".Trim(), _DataTable.Rows[i]["user_phone     ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "user_fax       ".Trim(), _DataTable.Rows[i]["user_fax       ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "user_in_tel    ".Trim(), _DataTable.Rows[i]["user_in_tel    ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "user_entry     ".Trim(), _DataTable.Rows[i]["user_entry     ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "user_leave     ".Trim(), _DataTable.Rows[i]["user_leave     ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "user_emp       ".Trim(), _DataTable.Rows[i]["user_emp       ".Trim()].ToString());
                        fpMain.Sheets[0].SetValue(i, "description    ".Trim(), _DataTable.Rows[i]["description    ".Trim()].ToString());
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
                bool _Error = new UserInformation_Business().UserInformation_A10(_Entity, ref fpMain);
                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");
                    DisplayMessage("저장 되었습니다.");

                    fpMain.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
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

        private void MainDelete_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Focus();

                for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "check     ".Trim()).ToString() == "True")
                    {
                        if(_Entity.user_account  == fpMain.Sheets[0].GetValue(i, "user_account     ".Trim()).ToString())
                        {
                            CustomMsg.ShowMessage("자신에 계정은 삭제하실수 없습니다.");
                            return;
                        }
                    }
                }
                bool _Error = new UserInformation_Business().UserInformation_A20(_Entity, ref fpMain);
                if (!_Error)
                {
                    CustomMsg.ShowMessage("삭제되었습니다.");
                    DisplayMessage("삭제 되었습니다.");

                    fpMain.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
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

        private void txtEdit_KeyDown(object obj, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                MainFind_DisplayData();
        }

        private void _LookupEdit_ValueChanged(object obj, EventArgs e)
        {
            if (!_FirstYn)
            {
                MainFind_DisplayData();
            }
        }

        #endregion

      
    }
}
