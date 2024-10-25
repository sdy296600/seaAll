using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Business;
using FarPoint.Win.Spread.CellType;

namespace CoFAS.NEW.MES.Core
{
    public partial class BaseForm2 : frmBaseNone
    {
        #region ○ 변수선언


        public SystemLogEntity _SystemLogEntity = null;
        private bool _FirstYn = true;
        //public MenuSettingEntity _MenuSettingEntity = null;
        #endregion

        #region ○ 생성자

        public BaseForm2()
        {
            //_MenuSettingEntity = pMenuSettingEntity;
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
                DataTable pDataTable =  new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME,fpMain,this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);
                if (pDataTable != null)
                {

                    Function.Core.initializeSpread(pDataTable, fpMain, this._pMenuSettingEntity.MENU_WINDOW_NAME, MainForm.UserEntity.user_account);
                    Function.Core.InitializeControl(pDataTable, fpMain, this, panel1, _pMenuSettingEntity);
                }
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

                fpMain._ChangeEventHandler += FpMain_Change;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 버튼 이벤트 영역

        private void _SearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                MainFind_DisplayData();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void _PrintButtonClicked(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void _DeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DialogResult _DialogResult1 = CustomMsg.ShowMessage("삭제하시겠습니까?", "Question", MessageBoxButtons.YesNo);
                if (_DialogResult1 == DialogResult.Yes)
                {
                    if (fpMain.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text == "입력")
                    {
                        FpSpreadManager.SpreadRowRemove(fpMain, 0, fpMain.Sheets[0].ActiveRowIndex);
                    }
                    else
                    {
                        fpMain.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text = "삭제";
                        fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "USE_YN", "N");

                        MainDelete_InputData();
                    }
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void _SaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (Function.Core._SaveButtonClicked(fpMain))
                {
                    if (fpMain.Sheets[0].Rows.Count > 0)
                        MainSave_InputData();
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void _ImportButtonClicked(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void _ExportButtonClicked(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void _InitialButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DataTable pDataTable = new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME, fpMain, this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);
                InitializeControl(pDataTable);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void _AddItemButtonClicked(object sender, EventArgs e)
        {
            try
            {
                FpSpreadManager.SpreadRowAdd(fpMain, 0);
                fpMain.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text = "입력";

                for (int i = 0; i < fpMain.Sheets[0].ColumnCount; i++)
                {
                    if (fpMain.Sheets[0].Columns[i].CellType.GetType() == typeof(ComboBoxCellType))
                    {
                        ComboBoxCellType pCellType = fpMain.Sheets[0].Columns[i].CellType as ComboBoxCellType;

                        fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, fpMain.Sheets[0].Columns[i].Tag.ToString(), pCellType.ItemData[0]);
                    }
                }

                fpMain.Sheets[0].SetText(fpMain.Sheets[0].ActiveRowIndex, "CODE_TYPE", this._pMenuSettingEntity.BASE_WHERE);
                fpMain.Sheets[0].SetText(fpMain.Sheets[0].ActiveRowIndex, "CODE", "");
                fpMain.Sheets[0].SetText(fpMain.Sheets[0].ActiveRowIndex, "CODE_NAME", "");
                fpMain.Sheets[0].SetText(fpMain.Sheets[0].ActiveRowIndex, "CODE_DESCRIPTION", "");
                fpMain.Sheets[0].SetText(fpMain.Sheets[0].ActiveRowIndex, "CODE_ETC1", "");
                fpMain.Sheets[0].SetText(fpMain.Sheets[0].ActiveRowIndex, "CODE_ETC2", "");
                fpMain.Sheets[0].SetText(fpMain.Sheets[0].ActiveRowIndex, "CODE_ETC3", "");
                fpMain.Sheets[0].SetText(fpMain.Sheets[0].ActiveRowIndex, "DESCRIPTION", "");
                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex,"SORT", fpMain.Sheets[0].Rows.Count);

                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex,"USE_YN", "Y");
                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex,"UP_USER", MainForm.UserEntity.user_account);
                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex,"UP_DATE", DateTime.Now);
                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex,"REG_USER", MainForm.UserEntity.user_account);
                fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex,"REG_DATE", DateTime.Now);



            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void _CloseButtonClicked(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void InitializeButton()
        {
            try
            {
                MainFormButtonSetting _MainFormButtonSetting = new MainFormButtonSetting();

                DataTable _DataTable = new MenuButton_Business().MenuButton_Select(_pMenuSettingEntity.MENU_NO, MainForm.UserEntity.user_authority);
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

        #region ○ 초기화 영역

        private void InitializeControl(DataTable dt)
        {
            try
            {
                if (_FirstYn)
                {
                    DataTable pDataTable =  new CoreBusiness().MENU_SEARCH_LIST_R10(this._pMenuSettingEntity);
                    SEARCH_PAN.Name = this._pMenuSettingEntity.BASE_TABLE;
                    SEARCH_PAN.Tag = $"and CODE_TYPE = '{this._pMenuSettingEntity.BASE_WHERE}'";
                    _pnHeader.Size = new Size(1500, 0);
                  


                }



                fpMain.Sheets[0].Rows.Count = 0;
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

        #region ○ 스프레드 영역

     

        private void FpMain_Change(object sender, ChangeEventArgs e)
        {
            try
            {
       
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
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 데이터 영역

        private void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                Common_Code_Entity _Entity = new Common_Code_Entity();

                _Entity.code_type = this._pMenuSettingEntity.BASE_WHERE;

                DataTable _DataTable = new Common_Code_Business().Common_Code_R10(_Entity);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpMain.Sheets[0].SetValue(i, item.ColumnName.ToUpper(), _DataTable.Rows[i][item.ColumnName.ToUpper()].ToString());
                        }

                    }
                    Function.Core._AddItemSUM(fpMain);
                    fpMain.Sheets[0].Visible = true;
                }
                else
                {
                    fpMain.Sheets[0].Rows.Count = 0;
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

                Common_Code_Entity _Entity = new Common_Code_Entity();

                _Entity.user_account = MainForm.UserEntity.user_account;
                bool _bool = new Common_Code_Business().Common_Code_A30(_Entity, ref fpMain);
                if (!_bool)
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
   
                //bool _Error = new CoreBusiness().BaseForm2_A10(_MenuSettingEntity,fpMain);
                //if (!_Error)
                //{
                //    CustomMsg.ShowMessage("삭제되었습니다.");
                //    DisplayMessage("삭제 되었습니다.");

                //    fpMain.Sheets[0].Rows.Count = 0;
                //    MainFind_DisplayData();
                //}
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
            if (e.KeyCode == Keys.Enter)
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
