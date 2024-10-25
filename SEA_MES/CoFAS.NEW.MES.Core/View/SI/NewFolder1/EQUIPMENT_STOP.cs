using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class EQUIPMENT_STOP : DoubleBaseForm1
    {
        public EQUIPMENT_STOP()
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

                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "EQUIPMENT_ID          ".Trim(), fpMain.Sheets[0].GetValue(row, "ID".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "EQUIPMENT_NAME        ".Trim(), fpMain.Sheets[0].GetValue(row, "ID".Trim()));
              
            }


        }
        public override void _DeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (fpSub.Sheets[0].ActiveRowIndex != -1)
                {
                    DialogResult _DialogResult1 = CustomMsg.ShowMessage("삭제하시겠습니까?", "Question", MessageBoxButtons.YesNo);
                    if (_DialogResult1 == DialogResult.Yes)
                    {
                        if (fpSub.Sheets[0].RowHeader.Cells[fpSub.Sheets[0].ActiveRowIndex, 0].Text == "입력")
                        {
                            FpSpreadManager.SpreadRowRemove(fpSub, 0, fpMain.Sheets[0].ActiveRowIndex);
                        }
                        else
                        {
                            fpSub.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text = "삭제";
                            fpSub.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "USE_YN", "N");

                            MainDelete_InputData();
                        }
                    }
                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

       

    }
}
