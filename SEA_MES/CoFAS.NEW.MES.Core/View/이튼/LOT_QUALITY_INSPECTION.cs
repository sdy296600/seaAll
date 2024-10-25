using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class LOT_QUALITY_INSPECTION : DoubleBaseForm1
    {
        public LOT_QUALITY_INSPECTION()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);

          
        }

        private void DoubleBaseForm2_Load(object sender, EventArgs e)
        {
            splitContainer2.Orientation = Orientation.Vertical;

            splitContainer2.SplitterDistance = 500;
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

                int row = 0;

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {

                    if (fpMain.Sheets[0].GetValue(i, "ID").ToString() == _Mst_Id)
                    {
                        row = i;
                    }
                }

                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_ID      ".Trim(), fpMain.Sheets[0].GetValue(row, "STOCK_MST_ID      ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_OUT_CODE".Trim(), fpMain.Sheets[0].GetValue(row, "STOCK_MST_OUT_CODE".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "CHECK_DATE        ".Trim(), DateTime.Now.ToString("yyyy-MM-dd"));

            
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

      
    
    }
}
