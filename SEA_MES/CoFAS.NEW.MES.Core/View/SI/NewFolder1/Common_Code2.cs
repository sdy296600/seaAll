using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class Common_Code2 : frmBaseNone
    {
        #region ○ 변수선언

        Common_Code_Entity _Entity = null;
        private SystemLogEntity _SystemLogEntity = null;
        private bool _FirstYn = true;

        private string _Lv1 = string.Empty;
        private string _Lv2 = string.Empty;
        private string _Lv3 = string.Empty;

        private Color orig_BackColor = Color.FromArgb(61, 68, 97);
        private Color orig_FontColor = Color.White;

        private Color trans_BackColor = Color.FromArgb(220, 20, 60);
        private Color trnas_FontColor = Color.Black;



        #endregion

        #region ○ 생성자

        public Common_Code2()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);
        }

        #endregion

        #region 폼 이벤트 영역

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

                fpSub1.CellClick += new CellClickEventHandler(fpSub1_CellClick);
                fpSub2.CellClick += new CellClickEventHandler(fpSub2_CellClick);    
                fpMain.CellClick += new CellClickEventHandler(fpMain_CellClick);
                fpMain._ChangeEventHandler += new ChangeEventHandler(Change);
                fpSub1._ChangeEventHandler += new ChangeEventHandler(Change);
                fpSub2._ChangeEventHandler += new ChangeEventHandler(Change);

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region 버튼 이벤트 영역

        private void _SearchButtonClicked(object sender, EventArgs e)
        {
            MainFind_DisplayData();
        }

        private void _PrintButtonClicked(object sender, EventArgs e)
        {
        }

        private void _DeleteButtonClicked(object sender, EventArgs e)
        {
            MainDelete_InputData();
        }

        private void _SaveButtonClicked(object sender, EventArgs e)
        {
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
            initializeSpread();
        }

        private void _AddItemButtonClicked(object sender, EventArgs e)
        {
            if (_lv1.BackColor == trans_BackColor)
            {
                FpSpreadManager.SpreadRowAdd(fpMain, 0);
                fpMain.Sheets[0].SetText(fpMain.Sheets[0].ActiveRowIndex, "code_type", "*");
                fpMain.Sheets[0].SetText(fpMain.Sheets[0].ActiveRowIndex, "code", "");
                fpMain.Sheets[0].SetText(fpMain.Sheets[0].ActiveRowIndex, "code_name", "");
            }

            if (_lv2.BackColor == trans_BackColor)
            {
                if (_Lv1 == "")
                {
                    CustomMsg.ShowMessage("대분류 그룹을 먼저 생성하시기 바랍니다.");
                    return;
                }

                FpSpreadManager.SpreadRowAdd(fpSub1, 0);
                fpSub1.Sheets[0].SetText(fpSub1.Sheets[0].ActiveRowIndex, "code_type", _Lv1);
                fpSub1.Sheets[0].SetText(fpSub1.Sheets[0].ActiveRowIndex, "code", "");
                fpSub1.Sheets[0].SetText(fpSub1.Sheets[0].ActiveRowIndex, "code_name", "");
            }

            if (_lv3.BackColor == trans_BackColor)
            {
                if (_Lv2 == "")
                {
                    CustomMsg.ShowMessage("중분류 그룹을 먼저 생성하시기 바랍니다.");
                    return;
                }
                FpSpreadManager.SpreadRowAdd(fpSub2, 0);
                fpSub2.Sheets[0].SetText(fpSub2.Sheets[0].ActiveRowIndex, "CODE_TYPE	   ".Trim(), _Lv2);
                fpSub2.Sheets[0].SetText(fpSub2.Sheets[0].ActiveRowIndex, "CODE		       ".Trim(), "");
                fpSub2.Sheets[0].SetText(fpSub2.Sheets[0].ActiveRowIndex, "CODE_NAME	   ".Trim(), "");
                fpSub2.Sheets[0].SetText(fpSub2.Sheets[0].ActiveRowIndex, "CODE_DESCRIPTION".Trim(), "");
                fpSub2.Sheets[0].SetText(fpSub2.Sheets[0].ActiveRowIndex, "CODE_ETC1	   ".Trim(), "");
                fpSub2.Sheets[0].SetText(fpSub2.Sheets[0].ActiveRowIndex, "CODE_ETC2	   ".Trim(), "");
                fpSub2.Sheets[0].SetText(fpSub2.Sheets[0].ActiveRowIndex, "CODE_ETC3	   ".Trim(), "");
                fpSub2.Sheets[0].SetText(fpSub2.Sheets[0].ActiveRowIndex, "DESCRIPTION	   ".Trim(), ""); 
                fpSub2.Sheets[0].SetValue(fpSub2.Sheets[0].ActiveRowIndex,"SORT		       ".Trim(), fpSub2.Sheets[0].Rows.Count);

            }
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
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region 초기화 영역

        private void InitializeControl()
        {
            try
            {
                if (_FirstYn)
                {
                    _Entity = new Common_Code_Entity();
                    _Entity.user_account = MainForm.UserEntity.user_account;
                    _Entity.user_name = MainForm.UserEntity.user_name;
                    _Entity.dept_code = MainForm.UserEntity.user_dept;
                    _Entity.dept_name = MainForm.UserEntity.dept_name;
                    _Entity.user_authority = MainForm.UserEntity.user_authority;
                }


                fpSub1.Sheets[0].Rows.Count = 0;
                fpSub2.Sheets[0].Rows.Count = 0;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
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
            // fpMain
            // 콤보가 있을때 반드시 설정
            FpSpreadManager.pConnectionString = ConfigrationManager.ConnectionString;

            // 스프레드 기본설정
            FpSpreadManager.pOperationType = OperationMode.Normal; // 읽기전용설정
            FpSpreadManager.SpreadSetStyle(fpMain);                 // 스프레드 전체설정      
            FpSpreadManager.SpreadSetSheetStyle(fpMain, 0);         // 스프레드 쉬트설정

            // 스프레드 쉬트 설정
            string pSetData = string.Empty;
            //           11 버튼 12 체크박스 14 콤보박스 25 날짜시간 28 마스크셀타입 29 숫자 32 텍스트
            //           라벨//넓이//보임0,숨김1//일기전용0,수정가능1,읽기전용기본색상2//왼쪽정렬0,중앙정렬1,오른쪽정렬2//셀타입//길이//소숫점//유형//그룹코드
            //           ----------------:넓이:보임:읽기:정렬:타입:길이:소수:유형:그룹코드(추가사항등)  :필드명
            pSetData += " 에러           :21  :1   :2   :1   :12  :0   :0   :    :1                     :                       "; // 에러체크
            pSetData += ",에러명         :21  :1   :2   :0   :32  :2000:0   :    :                      :                       "; // 에러메시지
            pSetData += ",체크           :21  :1   :2   :1   :12  :0   :0   :    :0                     :                       "; // 체크
            pSetData += ",선택           :21  :1   :2   :1   :12  :0   :0   :    :2                     :                       "; // 선택
            pSetData += ",예비4          :100 :1   :2   :1   :32  :100 :0   :    :                      :                       ";
            pSetData += ",예비5          :100 :1   :2   :1   :32  :100 :0   :    :                      :                       ";
            // ▲  공통부분 ---------------------------------------------------------------------------------------------------------------------------------------
            // ▼  추기 필요한 부분 -------------------------------------------------------------------------------------------------------------------------------
            pSetData += ",그룹           :70  :1   :0   :1   :32  :100 :0   :    :                      :code_type              ";
            pSetData += ",코드           :100 :0   :1   :1   :32  :100 :0   :    :                      :code                   ";
            pSetData += ",코드명         :100 :0   :1   :1   :32  :100 :0   :    :                      :code_name              ";
            // ----------------------------------------------------------------------------------------------------------------------------------------------------

            FpSpreadManager.SpreadSetHeader(fpMain, "조회", 0, pSetData, this.Name, _Entity.user_account);
            // fpSub1
            // 콤보가 있을때 반드시 설정
            FpSpreadManager.pConnectionString = ConfigrationManager.ConnectionString;

            // 스프레드 기본설정
            FpSpreadManager.pOperationType = OperationMode.Normal; // 읽기전용설정
            FpSpreadManager.SpreadSetStyle(fpSub1);                 // 스프레드 전체설정      
            FpSpreadManager.SpreadSetSheetStyle(fpSub1, 0);         // 스프레드 쉬트설정

            // 스프레드 쉬트 설정
            pSetData = string.Empty;
            //           11 버튼 12 체크박스 14 콤보박스 25 날짜시간 28 마스크셀타입 29 숫자 32 텍스트
            //           라벨//넓이//보임0,숨김1//일기전용0,수정가능1,읽기전용기본색상2//왼쪽정렬0,중앙정렬1,오른쪽정렬2//셀타입//길이//소숫점//유형//그룹코드
            //           ----------------:넓이:보임:읽기:정렬:타입:길이:소수:유형:그룹코드(추가사항등)  :필드명
            pSetData += " 에러           :21  :1   :2   :1   :12  :0   :0   :    :1                     :                       "; // 에러체크
            pSetData += ",에러명         :21  :1   :2   :0   :32  :2000:0   :    :                      :                       "; // 에러메시지
            pSetData += ",체크           :21  :1   :2   :1   :12  :0   :0   :    :0                     :                       "; // 체크
            pSetData += ",선택           :21  :1   :2   :1   :12  :0   :0   :    :2                     :                       "; // 선택
            pSetData += ",예비4          :100 :1   :2   :1   :32  :100 :0   :    :                      :                       ";
            pSetData += ",예비5          :100 :1   :2   :1   :32  :100 :0   :    :                      :                       ";
            // ▲  공통부분 ---------------------------------------------------------------------------------------------------------------------------------------
            // ▼  추기 필요한 부분 -------------------------------------------------------------------------------------------------------------------------------
            pSetData += ",그룹           :70  :0   :0   :1   :32  :100 :0   :    :                      :code_type              ";
            pSetData += ",코드           :100 :0   :0   :1   :32  :100 :0   :    :                      :code                   ";
            pSetData += ",코드명         :100 :0   :1   :1   :32  :100 :0   :    :                      :code_name              ";
            // ----------------------------------------------------------------------------------------------------------------------------------------------------

            FpSpreadManager.SpreadSetHeader(fpSub1, "조회", 0, pSetData, this.Name, _Entity.user_account);
            // fpSub2
            // 콤보가 있을때 반드시 설정
            FpSpreadManager.pConnectionString = ConfigrationManager.ConnectionString;

            // 스프레드 기본설정
            FpSpreadManager.pOperationType = OperationMode.Normal; // 읽기전용설정
            FpSpreadManager.SpreadSetStyle(fpSub2);                 // 스프레드 전체설정      
            FpSpreadManager.SpreadSetSheetStyle(fpSub2, 0);         // 스프레드 쉬트설정

            // 스프레드 쉬트 설정
            pSetData = string.Empty;
            //           11 버튼 12 체크박스 14 콤보박스 25 날짜시간 28 마스크셀타입 29 숫자 32 텍스트
            //           라벨//넓이//보임0,숨김1//일기전용0,수정가능1,읽기전용기본색상2//왼쪽정렬0,중앙정렬1,오른쪽정렬2//셀타입//길이//소숫점//유형//그룹코드
            //           ----------------:넓이:보임:읽기:정렬:타입:길이:소수:유형:그룹코드(추가사항등)  :필드명
            pSetData += " 에러           :21  :1   :2   :1   :12  :0   :0   :    :1                     :                       "; // 에러체크
            pSetData += ",에러명         :21  :1   :2   :0   :32  :2000:0   :    :                      :                       "; // 에러메시지
            pSetData += ",체크           :21  :1   :2   :1   :12  :0   :0   :    :0                     :                       "; // 체크
            pSetData += ",선택           :21  :1   :2   :1   :12  :0   :0   :    :2                     :                       "; // 선택
            pSetData += ",예비4          :100 :1   :2   :1   :32  :100 :0   :    :                      :                       ";
            pSetData += ",예비5          :100 :1   :2   :1   :32  :100 :0   :    :                      :                       ";
            // ▲  공통부분 ---------------------------------------------------------------------------------------------------------------------------------------
            // ▼  추기 필요한 부분 -------------------------------------------------------------------------------------------------------------------------------
            pSetData += ",그룹           :70  :0   :0   :1   :32  :100 :0   :    :                      :CODE_TYPE		       ";
            pSetData += ",코드           :80  :0   :0   :1   :32  :100 :0   :    :                      :CODE		           ";
            pSetData += ",코드명         :300 :0   :1   :0   :32  :100 :0   :    :                      :CODE_NAME		       ";
            pSetData += ",설명           :300 :0   :1   :0   :32  :100 :0   :    :                      :CODE_DESCRIPTION      ";
            pSetData += ",값1            :100 :0   :1   :0   :32  :100 :0   :    :                      :CODE_ETC1		       ";
            pSetData += ",값2            :100 :0   :1   :0   :32  :100 :0   :    :                      :CODE_ETC2		       ";
            pSetData += ",값3            :100 :0   :1   :0   :32  :100 :0   :    :                      :CODE_ETC3		       ";
            pSetData += ",순서           :70  :0   :1   :1   :29  :100 :0   :    :                      :SORT		           ";
            pSetData += ",비고           :200 :0   :1   :0   :32  :100 :0   :    :                      :DESCRIPTION		   "; 
            // ----------------------------------------------------------------------------------------------------------------------------------------------------

            FpSpreadManager.SpreadSetHeader(fpSub2, "조회", 0, pSetData, this.Name, _Entity.user_account);
        }
        private void Change(object obj, ChangeEventArgs e)
        {
            try
            {

                if (e.Column < 6)
                    return;

                xFpSpread fpSpread = obj as xFpSpread;

                string pHeaderLabel = fpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                switch (pHeaderLabel)
                {
                    case "":
                        fpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text = "수정";
                        break;
                    case "입력":
                        break;
                    case "수정":
                        break;
                    case "삭제":
                        break;
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void fpMain_CellClick(object obj, CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            InitializeControl();
            _Lv1 = fpMain.Sheets[0].GetText(e.Row, "code").ToString();
            Sub1Find_DisplayData(_Lv1);

            _lv1.BackColor = orig_BackColor;
            _lv1.ForeColor = orig_FontColor;
            _lv2.BackColor = trans_BackColor;
            _lv2.ForeColor = trnas_FontColor;
            _lv3.BackColor = orig_BackColor;
            _lv3.ForeColor = orig_FontColor;

        }

        private void fpSub1_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            _Lv2 = fpSub1.Sheets[0].GetText(e.Row, "code").ToString();
          

            if (_Lv2 != "")
            {
                Sub2Find_DisplayData(_Lv2);
                _lv1.BackColor = orig_BackColor;
                _lv1.ForeColor = orig_FontColor;
                _lv2.BackColor = orig_BackColor;
                _lv2.ForeColor = orig_FontColor;
                _lv3.BackColor = trans_BackColor;
                _lv3.ForeColor = trnas_FontColor;
            }
            else
            {
                _lv1.BackColor = orig_BackColor;
                _lv1.ForeColor = orig_FontColor;
                _lv2.BackColor = trans_BackColor;
                _lv2.ForeColor = trnas_FontColor;
                _lv3.BackColor = orig_BackColor;
                _lv3.ForeColor = orig_FontColor;
            }
        }

        private void fpSub2_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            _lv1.BackColor = orig_BackColor;
            _lv1.ForeColor = orig_FontColor;
            _lv2.BackColor = orig_BackColor;
            _lv2.ForeColor = orig_FontColor;
            _lv3.BackColor = trans_BackColor;
            _lv3.ForeColor = trnas_FontColor;

        }
        #endregion

        #region 데이터 영역



        private void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                _Entity.code_type = "*";

                DataTable _DataTable = new Common_Code_Business().Common_Code_R20(_Entity);
                fpMain.Sheets[0].Rows.Count = 0;
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpMain.Sheets[0].SetText(i, "code_type".Trim(), _DataTable.Rows[i]["code_type".Trim()].ToString());
                        fpMain.Sheets[0].SetText(i, "code     ".Trim(), _DataTable.Rows[i]["code     ".Trim()].ToString());
                        fpMain.Sheets[0].SetText(i, "code_name".Trim(), _DataTable.Rows[i]["code_name".Trim()].ToString());
                    }

                    fpMain.Sheets[0].Visible = true;
                }
            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
                DisplayMessage("조회 오류가 발생했습니다.");
            }
            finally
            {
                DevExpressManager.SetCursor(this, Cursors.Default);
            }
        }

        private void Sub1Find_DisplayData(string code)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                _Entity.code_type = code;

                DataTable _DataTable = new Common_Code_Business().Common_Code_R10(_Entity);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpSub1.Sheets[0].Visible = false;
                    fpSub1.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpSub1.Sheets[0].SetText(i, "code_type".Trim(), _DataTable.Rows[i]["code_type".Trim()].ToString());
                        fpSub1.Sheets[0].SetText(i, "code     ".Trim(), _DataTable.Rows[i]["code     ".Trim()].ToString());
                        fpSub1.Sheets[0].SetText(i, "code_name".Trim(), _DataTable.Rows[i]["code_name".Trim()].ToString());
                    }

                    fpSub1.Sheets[0].Visible = true;
                }
                else
                {
                    fpSub1.Sheets[0].Rows.Count = 0;
                }
            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
                DisplayMessage("조회 오류가 발생했습니다.");
            }
            finally
            {
                DevExpressManager.SetCursor(this, Cursors.Default);
            }
        }

        private void Sub2Find_DisplayData(string code)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                _Entity.code_type = code;

                DataTable _DataTable = new Common_Code_Business().Common_Code_R10(_Entity);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpSub2.Sheets[0].Visible = false;
                    fpSub2.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        fpSub2.Sheets[0].SetText(i, "CODE_TYPE		      ".Trim(), _DataTable.Rows[i]["CODE_TYPE		      ".Trim()].ToString());
                        fpSub2.Sheets[0].SetText(i, "CODE		          ".Trim(), _DataTable.Rows[i]["CODE		          ".Trim()].ToString());
                        fpSub2.Sheets[0].SetText(i, "CODE_NAME		      ".Trim(), _DataTable.Rows[i]["CODE_NAME		      ".Trim()].ToString());
                        fpSub2.Sheets[0].SetText(i, "CODE_DESCRIPTION     ".Trim(), _DataTable.Rows[i]["CODE_DESCRIPTION     ".Trim()].ToString());
                        fpSub2.Sheets[0].SetText(i, "CODE_ETC1		      ".Trim(), _DataTable.Rows[i]["CODE_ETC1		      ".Trim()].ToString());
                        fpSub2.Sheets[0].SetText(i, "CODE_ETC2		      ".Trim(), _DataTable.Rows[i]["CODE_ETC2		      ".Trim()].ToString());
                        fpSub2.Sheets[0].SetText(i, "CODE_ETC3		      ".Trim(), _DataTable.Rows[i]["CODE_ETC3		      ".Trim()].ToString());
                        fpSub2.Sheets[0].SetText(i, "SORT		          ".Trim(), _DataTable.Rows[i]["SORT		          ".Trim()].ToString());                     
                    }                                	                                               	  

                    fpSub2.Sheets[0].Visible = true;
                }
                else
                {
                    fpSub2.Sheets[0].Rows.Count = 0;
                }
            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
                DisplayMessage("조회 오류가 발생했습니다.");
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

                bool _bool = false;
                if (_lv1.BackColor == trans_BackColor)
                {
                    DialogResult _DialogRestult = CustomMsg.ShowMessage("대분류 저장 하시겠습니까?", "Question", MessageBoxButtons.YesNo);

                    if (_DialogRestult == DialogResult.Yes)
                        _bool = new Common_Code_Business().Common_Code_A10(_Entity, ref fpMain);
                }

                if (_lv2.BackColor == trans_BackColor)
                {
                    DialogResult _DialogRestult = CustomMsg.ShowMessage("중분류 저장 하시겠습니까?", "Question", MessageBoxButtons.YesNo);

                    if (_DialogRestult == DialogResult.Yes)
                        _bool = new Common_Code_Business().Common_Code_A20(_Entity, ref fpSub1);
                }

                if (_lv3.BackColor == trans_BackColor)
                {
                    DialogResult _DialogRestult = CustomMsg.ShowMessage("소분류 저장 하시겠습니까?", "Question", MessageBoxButtons.YesNo);

                    if (_DialogRestult == DialogResult.Yes)
                        _bool = new Common_Code_Business().Common_Code_A30(_Entity, ref fpSub2);
                }

                if (!_bool)
                {
                    CustomMsg.ShowMessage("저장 되었습니다.");
                    InitializeControl();
                    MainFind_DisplayData();

                    int pRow = 0;
                    for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                    {
                        if (_Lv1 == fpMain.Sheets[0].GetText(i, "code"))
                        {
                            pRow = i;
                            break;
                        }
                    }

                    fpMain.Sheets[0].SetActiveCell(pRow, 6, true);
                    fpMain.ShowActiveCell(VerticalPosition.Center, HorizontalPosition.Center);
                    fpMain_CellClick(fpMain, new CellClickEventArgs(null, pRow, 0, 0, 0, System.Windows.Forms.MouseButtons.Left, false, false));

                }
                else
                    CustomMsg.ShowMessage("저장 중 오류가 발생했습니다.");
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

                bool _bool = false;

                if (_lv1.BackColor == trans_BackColor)
                {
     
                    if (fpMain.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text != "")
                        FpSpreadManager.SpreadRowRemove(fpMain, 0, fpMain.Sheets[0].ActiveRowIndex);
                    else
                    {
          
                        _Entity.code_type = fpMain.Sheets[0].GetText(fpMain.Sheets[0].ActiveRowIndex, "code_type");
                        _Entity.code = fpMain.Sheets[0].GetText(fpMain.Sheets[0].ActiveRowIndex, "code");
                        DialogResult _DialogRestult = CustomMsg.ShowMessage("대분류 [ 코드 : " + _Entity.code + " / 코드명 : " + fpMain.Sheets[0].GetText(fpMain.Sheets[0].ActiveRowIndex, "code_name") + " ] 삭제 하시겠습니까?", "Question", MessageBoxButtons.YesNo);

                        if (_DialogRestult == DialogResult.Yes)
                            _bool = new Common_Code_Business().Common_Code_A15(_Entity);
                    }
                }

                if (_lv2.BackColor == trans_BackColor)
                {
                    if (fpSub1.Sheets[0].RowHeader.Cells[fpSub1.Sheets[0].ActiveRowIndex, 0].Text != "")
                        FpSpreadManager.SpreadRowRemove(fpSub1, 0, fpSub1.Sheets[0].ActiveRowIndex);
                    else
                    {
                        _Entity.code_type = fpSub1.Sheets[0].GetText(fpSub1.Sheets[0].ActiveRowIndex, "code_type");
                        _Entity.code = fpSub1.Sheets[0].GetText(fpSub1.Sheets[0].ActiveRowIndex, "code");
                        DialogResult _DialogRestult = CustomMsg.ShowMessage("중분류 [ 코드 : " + _Entity.code + " / 코드명 : " + fpSub1.Sheets[0].GetText(fpSub1.Sheets[0].ActiveRowIndex, "code_name") + " ] 삭제 하시겠습니까?", "Question", MessageBoxButtons.YesNo);

                        if (_DialogRestult == DialogResult.Yes)
                            _bool = new Common_Code_Business().Common_Code_A15(_Entity);
                    }
                }

                if (_lv3.BackColor == trans_BackColor)
                {
                    if (fpSub2.Sheets[0].RowHeader.Cells[fpSub2.Sheets[0].ActiveRowIndex, 0].Text != "")
                        FpSpreadManager.SpreadRowRemove(fpSub2, 0, fpSub2.Sheets[0].ActiveRowIndex);
                    else
                    {
                        _Entity.code_type = fpSub2.Sheets[0].GetText(fpSub2.Sheets[0].ActiveRowIndex, "code_type");
                        _Entity.code = fpSub2.Sheets[0].GetText(fpSub2.Sheets[0].ActiveRowIndex, "code");
                        DialogResult _DialogRestult = CustomMsg.ShowMessage("소분류 [ 코드 : " + _Entity.code + " / 코드명 : " + fpSub2.Sheets[0].GetText(fpSub2.Sheets[0].ActiveRowIndex, "code_name") + " ] 삭제 하시겠습니까?", "Question", MessageBoxButtons.YesNo);

                        if (_DialogRestult == DialogResult.Yes)
                            _bool = new Common_Code_Business().Common_Code_A15(_Entity);
                        else
                            return;
                    }
                }

                if (!_bool)
                {
                    CustomMsg.ShowMessage("삭제 되었습니다.");
                    InitializeControl();
                    MainFind_DisplayData();
                }
                else
                    CustomMsg.ShowMessage("삭제 중 오류가 발생했습니다.");
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

        private void textEdit_KeyDown(object obj, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                MainFind_DisplayData();
        }


        #endregion

        private void lbl_Click(object sender, EventArgs e)
        {
            Label pCmd = sender as Label;
            string pCls = pCmd.Name;

            switch (pCls)
            {
                case "_lv1":
                    _lv1.BackColor = trans_BackColor;
                    _lv1.ForeColor = trnas_FontColor;
                    _lv2.BackColor = orig_BackColor;
                    _lv2.ForeColor = orig_FontColor;
                    _lv3.BackColor = orig_BackColor;
                    _lv3.ForeColor = orig_FontColor;
                    break;
                case "_lv2":
                    _lv1.BackColor = orig_BackColor;
                    _lv1.ForeColor = orig_FontColor;
                    _lv2.BackColor = trans_BackColor;
                    _lv2.ForeColor = trnas_FontColor;
                    _lv3.BackColor = orig_BackColor;
                    _lv3.ForeColor = orig_FontColor;
                    break;
                case "_lv3":
                    _lv1.BackColor = orig_BackColor;
                    _lv1.ForeColor = orig_FontColor;
                    _lv2.BackColor = orig_BackColor;
                    _lv2.ForeColor = orig_FontColor;
                    _lv3.BackColor = trans_BackColor;
                    _lv3.ForeColor = trnas_FontColor;
                    break;
            }
        }
    }
}
