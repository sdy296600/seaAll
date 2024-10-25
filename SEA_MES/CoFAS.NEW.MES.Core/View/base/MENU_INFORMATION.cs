using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class MENU_INFORMATION : DoubleBaseForm1
    {
        public MENU_INFORMATION()
        {
            InitializeComponent();

           
        }
        private void MENU_INFORMATION_Load(object sender, EventArgs e)
        {
            splitContainer2.Orientation = Orientation.Vertical;

            splitContainer2.SplitterDistance = 200;
        }
        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (_Mst_Id != string.Empty)
                {
                    Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);

                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "P_MENU_ID", _Mst_Id);
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "MENU_ID", "0");
                }
                else
                {
                    Function.Core._AddItemButtonClicked(fpMain, MainForm.UserEntity.user_account);
                    fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "P_MENU_ID","-1");
                    fpMain.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "MENU_ID", "0");
                }

            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }

        }
        public override void _DeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
               
                
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public override void SubFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;

                DataTable _DataTable = new SI_Business().MENU_INFORMATION_R10(_Mst_Id,this._pMenuSettingEntity.BASE_TABLE);

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

        public override void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Focus();
                bool _Error = new SI_Business().MENU_INFORMATION_A10(this._pMenuSettingEntity,fpMain,this._pMenuSettingEntity.BASE_TABLE.Split('/')[1]);
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
        public override void SubSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);


                fpSub.Focus(); 

                bool _Error = new SI_Business().MENU_INFORMATION_A10(this._pMenuSettingEntity,fpSub,this._pMenuSettingEntity.BASE_TABLE.Split('/')[1]);
                if (!_Error)
                {


                    fpSub.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
                    SubFind_DisplayData();
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
        public override void fpMain_CellClick(object sender, CellClickEventArgs e)
        {
            try
            {

                string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                if (pHeaderLabel != "입력" && pHeaderLabel != "합계")
                {
                    _Mst_Id  =  fpMain.Sheets[0].GetValue(e.Row, "MENU_ID").ToString();

                    SubFind_DisplayData();
                   
                }

            }
            catch (Exception _Exception)
            {
                //CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

  
    }
}
