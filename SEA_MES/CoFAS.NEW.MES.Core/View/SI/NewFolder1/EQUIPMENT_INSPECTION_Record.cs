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
    public partial class EQUIPMENT_INSPECTION_RECORD : DoubleBaseForm1
    {
        public EQUIPMENT_INSPECTION_RECORD()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);

          
        }

        private void EQUIPMENT_INSPECTION_RECORD_Load(object sender, EventArgs e)
        {
            splitContainer2.Orientation = Orientation.Vertical;

            splitContainer2.SplitterDistance = 500;
        }

        public override void fpMain_CellClick(object sender, CellClickEventArgs e)
        {
            try
            {

                string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                if (pHeaderLabel != "입력" && pHeaderLabel != "합계")
                {
                    int id =  Convert.ToInt32(fpMain.Sheets[0].GetValue(e.Row, "ID"));
                    BaseMonthCalendarPopupBox baseMonthCalendarPopupBox = new BaseMonthCalendarPopupBox(id,this._pMenuSettingEntity.DESCRIPTION);
                    if (baseMonthCalendarPopupBox.ShowDialog() == DialogResult.OK)
                    {
                        if (baseMonthCalendarPopupBox._SelectionStart != string.Empty)
                        {
                            SubFind_DisplayData(id, baseMonthCalendarPopupBox._SelectionStart);
                        }
                    }
                }

            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }


        public void SubFind_DisplayData(int mst_id, string where)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;

                DataTable _DataTable = new SI_Business().EQUIPMENT_INSPECTION_RECORD_R10(mst_id,this._pMenuSettingEntity.DESCRIPTION,where,MainForm.UserEntity.user_account);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpSub.Sheets[0].Visible = false;
                    fpSub.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpSub.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName]);
                            if(item.ColumnName == "ID")
                            {
                                if (_DataTable.Rows[i][item.ColumnName].ToString() == "0")
                                {
                                    fpSub.Sheets[0].RowHeader.Cells[i, 0].Text = "입력";
                                }
                            }
                        }

                    }

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

        public override void SubSave_InputData()
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
                    //SubFind_DisplayData(_Mst_Id.Value);
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
    }
}
