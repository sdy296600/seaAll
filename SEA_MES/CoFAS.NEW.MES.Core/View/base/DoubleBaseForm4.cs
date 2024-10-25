using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class DoubleBaseForm4 : DoubleBaseForm1
    {
        public DoubleBaseForm4()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);

          
        }

        private void DoubleBaseForm4_Load(object sender, EventArgs e)
        {
            splitContainer2.Orientation = Orientation.Vertical;

            splitContainer2.SplitterDistance = 1200; 
        }
        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            if (_Mst_Id == null)
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
        public override void MainSave_InputData()
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
    }
}
