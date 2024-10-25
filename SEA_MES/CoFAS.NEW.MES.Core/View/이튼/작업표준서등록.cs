using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 작업표준서등록 : DoubleBaseForm1
    {

       
        public 작업표준서등록()
        {
            InitializeComponent();


          
        }

        private void DoubleBaseForm2_Load(object sender, EventArgs e)
        {
            splitContainer2.Orientation = Orientation.Vertical;

            splitContainer2.SplitterDistance = 500;
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

        public override void SubFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;

                string str = $@"
                                  select ISNULL(C.ID,0)                                             AS 'ID'
                                        ,B.ID                                                       AS 'STOCK_MST_ID'
                                        ,A.ID                                                       AS 'EQUIPMENT_OUT_CODE'
                                        ,A.ID                                                       AS 'EQUIPMENT_NAME'
                                        ,A.ID                                                       AS 'EQUIPMENT_TYPE'
                                        ,C.IMAGE                                                    AS 'IMAGE'
                                        ,ISNULL(C.USE_YN,'Y')                                       AS 'USE_YN'
                                        ,ISNULL(C.UP_USER, '{MainForm.UserEntity.user_account}' )   AS 'UP_USER'
                                        ,ISNULL(C.UP_DATE, GETDATE())                               AS 'UP_DATE'
                                        ,ISNULL(C.REG_USER,'{MainForm.UserEntity.user_account}' )   AS 'REG_USER'
                                        ,ISNULL(C.REG_DATE,GETDATE())                               AS 'REG_DATE'
                                  from EQUIPMENT A
                                  INNER JOIN STOCK_MST B ON A.TYPE = B.LINE
                                   LEFT JOIN WORK_STANDARDS C ON B.ID = C.STOCK_MST_ID AND C.EQUIPMENT_OUT_CODE = A.ID
                                  WHERE B.ID = {_Mst_Id}";

                DataTable _DataTable = new CoreBusiness().SELECT(str);

                Function.Core.DisplayData_Set(_DataTable, fpSub);

                for (int i = 0; i < _DataTable.Rows.Count; i++)
                {
                    if (_DataTable.Rows[i]["ID"].ToString() == "0")
                    {
                        fpSub.Sheets[0].RowHeader.Cells[i, 0].Text = "입력";
                    }

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
