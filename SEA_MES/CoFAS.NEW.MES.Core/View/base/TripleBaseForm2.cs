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
    public partial class TripleBaseForm2 : frmBaseNone
    {

        #region ○ 변수선언


        public SystemLogEntity _SystemLogEntity = null;
        public bool _FirstYn = true;

        public string _Mst_Id = string.Empty;
        //public MenuSettingEntity _MenuSettingEntity = null;
        #endregion

        #region ○ 생성자

        public TripleBaseForm2()
        {
            
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);
        }

        #endregion

        #region ○ 폼 이벤트 영역

        public void Form_Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        public void Form_Activated(object sender, EventArgs e)
        {
            InitializeButton();
        }

        public virtual void Form_Load(object sender, EventArgs e)
        {
            try
            {
                if (this._pMenuSettingEntity != null)
                {
                    //string pBASE_TABLE1 = "";
                    //string pBASE_TABLE2 = "";
                    //string pBASE_TABLE3 = "";
                    //if (_pMenuSettingEntity.BASE_TABLE != "")
                    //{
                    //    pBASE_TABLE1 = this._pMenuSettingEntity.BASE_TABLE.Split('/')[0];
                    //    pBASE_TABLE2 = this._pMenuSettingEntity.BASE_TABLE.Split('/')[1];
                    //    pBASE_TABLE3 = this._pMenuSettingEntity.BASE_TABLE.Split('/')[2];
                    //}


                    //DataTable pDataTable1 =  new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME,fpMain,pBASE_TABLE1);
                    //DataTable pDataTable2 =  new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME,fpSub,pBASE_TABLE2);
                    //DataTable pDataTable3 =  new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME,fpSub2,pBASE_TABLE3);
                   
                    //if (pDataTable1 != null)
                    //{
                    //    InitializeControl(pDataTable1);
                    //    initializeSpread(pDataTable1, fpMain);
                    //}
                    //if (pDataTable3 != null)
                    //{
                    //    initializeSpread(pDataTable2, fpSub);
                    //}
                    //if (pDataTable3 != null)
                    //{
                    //    initializeSpread(pDataTable3, fpSub2);
                    //}

                }
              

                _SystemLogEntity = new SystemLogEntity();

                // 버튼이벤트 생성
                SearchButtonClicked -= new EventHandler(_SearchButtonClicked);
                PrintButtonClicked -= new EventHandler(_PrintButtonClicked);
                DeleteButtonClicked -= new EventHandler(_DeleteButtonClicked);
                SaveButtonClicked -= new EventHandler(_SaveButtonClicked);
                ImportButtonClicked -= new EventHandler(_ImportButtonClicked);
                ExportButtonClicked -= new EventHandler(_ExportButtonClicked);
                InitialButtonClicked -= new EventHandler(_InitialButtonClicked);
                AddItemButtonClicked -= new EventHandler(_AddItemButtonClicked);
                CloseButtonClicked -= new EventHandler(_CloseButtonClicked);

                SearchButtonClicked += new EventHandler(_SearchButtonClicked);
                PrintButtonClicked += new EventHandler(_PrintButtonClicked);
                DeleteButtonClicked += new EventHandler(_DeleteButtonClicked);
                SaveButtonClicked += new EventHandler(_SaveButtonClicked);
                ImportButtonClicked += new EventHandler(_ImportButtonClicked);
                ExportButtonClicked += new EventHandler(_ExportButtonClicked);
                InitialButtonClicked += new EventHandler(_InitialButtonClicked);
                AddItemButtonClicked += new EventHandler(_AddItemButtonClicked);
                CloseButtonClicked += new EventHandler(_CloseButtonClicked);

                fpMain._ChangeEventHandler += Change;
                fpSub._ChangeEventHandler += Change;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 버튼 이벤트 영역

        public virtual void _SearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                MainFind_DisplayData();
                fpSub.Sheets[0].Rows.Count = 0;
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public virtual void _PrintButtonClicked(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public virtual void _DeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public virtual void _SaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (Function.Core._SaveButtonClicked(fpMain))
                {
                    if (Function.Core._SaveButtonClicked(fpSub))
                    {
                        if (fpMain.Sheets[0].Rows.Count > 0)
                            MainSave_InputData();
                        if (fpSub.Sheets[0].Rows.Count > 0)
                            SubSave_InputData();
                
                        if (fpMain.Sheets[0].Rows.Count > 0 || fpSub.Sheets[0].Rows.Count > 0)
                        {
                            CustomMsg.ShowMessage("저장되었습니다.");
                            DisplayMessage("저장 되었습니다.");
                        }
                     
                    }
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public virtual void _ImportButtonClicked(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public virtual void _ExportButtonClicked(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public virtual void _InitialButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _Mst_Id = string.Empty;
                DataTable pDataTable = new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME, fpMain, this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);
                InitializeControl(pDataTable);
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public virtual void _AddItemButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (_Mst_Id == string.Empty)
                {
                    //MyCore._AddItemButtonClicked(fpMain, MainForm.UserEntity.user_account);
               
                }
                else
                {
                    Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);

                    string mst=  this._pMenuSettingEntity.BASE_TABLE.Split('/')[0] +"_ID";
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, mst, _Mst_Id);

                }
            }
            catch(Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public virtual void _CloseButtonClicked(object sender, EventArgs e)
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


        public void InitializeControl(DataTable dt)
        {
            try
            {
                if (_FirstYn)
                {

                    Function.Core.InitializeControl(dt, fpMain, this, _PAN_WHERE, this._pMenuSettingEntity);
                }


                fpMain.Sheets[0].Rows.Count = 0;
                fpSub.Sheets[0].Rows.Count = 0;
 
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

       

        public void Change(object sender, ChangeEventArgs e)
        {
            try
            {
                xFpSpread xFp = sender as xFpSpread;
 
  
                string pHeaderLabel = xFp.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                switch (pHeaderLabel)
                {
                    case "":
                        xFp.Sheets[0].RowHeader.Cells[e.Row, 0].Text = "수정";
                        break;
                    case "입력":
                        break;
                    case "수정":
                        break;
                    case "삭제":
                        break;
                }

                if (xFp.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(FarPoint.Win.Spread.CellType.NumberCellType))
                {
                    Function.Core._AddItemSUM(xFp);
                    xFp.ActiveSheet.SetActiveCell(e.Row, e.Column);
                    xFp.ShowActiveCell(FarPoint.Win.Spread.VerticalPosition.Center, FarPoint.Win.Spread.HorizontalPosition.Center);
                    //xFp.ShowActiveCell(e.Row, e.Column);
                    //xFp.ActiveSheet.Cells[e.Row, e.Column].ay();
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        #endregion

        #region ○ 데이터 영역

        public virtual void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                DataTable _DataTable = new CoreBusiness().BaseForm1_R10(_PAN_WHERE,this._pMenuSettingEntity);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpMain.Sheets[0].SetValue(i, item.ColumnName.ToUpper(), _DataTable.Rows[i][item.ColumnName]);

                            if (item.ColumnName == "COMPLETE_YN")
                            {
                                switch (_DataTable.Rows[i][item.ColumnName].ToString())
                                {
                                    case "Y":
                                        fpMain.Sheets[0].Rows[i].BackColor = Color.FromArgb(198, 239, 206);
                                        fpMain.Sheets[0].Rows[i].Locked = true;
                                        break;
                                    case "W":
                                        fpMain.Sheets[0].Rows[i].BackColor = Color.LightBlue;
                                        fpMain.Sheets[0].Rows[i].Locked = true;
                                        break;
                                }
                            }
                        }

                    }
                    Function.Core._AddItemSUM(fpMain);
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

        public virtual void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(this._pMenuSettingEntity,fpMain,this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);
                if (!_Error)
                {
                    fpMain.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
                   // SubFind_DisplayData(_Mst_Id);
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

        public virtual void SubSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(this._pMenuSettingEntity,fpSub,this._pMenuSettingEntity.BASE_TABLE.Split('/')[1]);
                if (!_Error)
                {
       

                    fpSub.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
                    SubFind_DisplayData(_Mst_Id);
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

        public virtual void MainDelete_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Focus();

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

        public virtual void SubFind_DisplayData(string mst_id)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;

                DataTable _DataTable = new CoreBusiness().DoubleBaseForm_R10(mst_id,this._pMenuSettingEntity.BASE_TABLE);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpSub.Sheets[0].Visible = false;
                    fpSub.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpSub.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName]);
                        }

                    }
                    Function.Core._AddItemSUM(fpSub);
                    fpSub.Sheets[0].Visible = true;


                }
                else
                {
                    fpSub.Sheets[0].Rows.Count = 0;
                    //CustomMsg.ShowMessage("조회 내역이 없습니다.");
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

      
      
        public virtual void fpMain_CellClick(object sender, CellClickEventArgs e)
        {
            try
            {

                string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                if (pHeaderLabel != "입력" && pHeaderLabel != "합계")
                {
                    _Mst_Id = fpMain.Sheets[0].GetValue(e.Row, "ID").ToString();
                    if (_Mst_Id != "" && _Mst_Id != null)
                    {
                        SubFind_DisplayData(_Mst_Id);
                    }
                }

            }
            catch (Exception _Exception)
            {
                //CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public virtual void fpSub_CellClick(object sender, CellClickEventArgs e)
        {
            try
            {

                

            }
            catch (Exception _Exception)
            {
                //CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void fpSub_Sheet1_CellChanged(object sender, SheetViewEventArgs e)
        {

        }
    }
}
