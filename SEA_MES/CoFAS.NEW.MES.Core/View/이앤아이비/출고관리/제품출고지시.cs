using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System;
using System.Data;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 제품출고지시 : DoubleBaseForm1
    {
        public 제품출고지시()
        {
            InitializeComponent();
            fpSub._EditorNotifyEventHandler -= new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
            fpSub._EditorNotifyEventHandler += new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
        }


        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            if (_Mst_Id == string.Empty)
            {
                CustomMsg.ShowMessage("선택된 정보가 없습니다.");
                return;
            }
            else
            {
                Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);

                string mst=  this._pMenuSettingEntity.BASE_TABLE.Split('/')[0] +"_ID";
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, mst, _Mst_Id);
            }
           
        }

        private void pfpSpread_ButtonClicked(object obj, EditorNotifyEventArgs e)
        {
            try
            {
                xFpSpread pfpSpread = obj as xFpSpread;

                if (e.EditingControl == null)
                {
                    return;
                }


                if (e.EditingControl.Text == "포장등록")
                {
                    if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0")
                    {
                        if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                        {
                            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                            {
                                string id = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();

                                if (id != "")
                                {
                                    string sql = $@"select COMPLETE_YN
                                                  from [dbo].[OUT_STOCK_WAIT_MST] 
                                                 WHERE ID = {id} AND USE_YN = 'Y'AND COMPLETE_YN = 'N'";

                                    DataTable _DataTable = new CoreBusiness().SELECT(sql);

                                    if (_DataTable != null && _DataTable.Rows.Count == 1)
                                    {
                                        포장등록_PopupBox basePopupBox = new 포장등록_PopupBox(pfpSpread.Sheets[0].GetValue(e.Row, "ORDER_MST_ID").ToString()
                                                                                             , pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()
                                                                                             , pfpSpread._user_account);
                                        if (basePopupBox.ShowDialog() == DialogResult.OK)
                                        {

                                        }
                                    }
                                    else
                                    {
                                        CustomMsg.ShowMessage("완료된 출고 지시입니다.");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {

            }
        }
    }
}
