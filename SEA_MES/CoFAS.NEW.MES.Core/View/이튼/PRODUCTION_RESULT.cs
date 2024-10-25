using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class PRODUCTION_RESULT : DoubleBaseForm1
    {
        public PRODUCTION_RESULT()
        {
            InitializeComponent();



          
        }

        private void DoubleBaseForm2_Load(object sender, EventArgs e)
        {
            //splitContainer2.Orientation = Orientation.Vertical;

            //splitContainer2.SplitterDistance = 500; 
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

              
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_ID              ".Trim(), fpMain.Sheets[0].GetValue(row, "STOCK_MST_ID              ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_OUT_CODE        ".Trim(), fpMain.Sheets[0].GetValue(row, "STOCK_MST_ID        ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_STANDARD        ".Trim(), fpMain.Sheets[0].GetValue(row, "STOCK_MST_ID        ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_TYPE            ".Trim(), fpMain.Sheets[0].GetValue(row, "STOCK_MST_ID            ".Trim()));            
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

        public override void MainFind_DisplayData()
        {
            try
            {
                //fpMain._EditorNotifyEventHandler -= new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                //fpMain._EditorNotifyEventHandler += new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                //fpSub._EditorNotifyEventHandler -= new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                //fpSub._EditorNotifyEventHandler += new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT
                              A.ID 
                             ,A.PRODUCTION_PLAN_ID            AS 'A.PRODUCTION_PLAN_ID'
                             ,A.INSTRUCT_DATE                 AS 'A.INSTRUCT_DATE'
                             ,A.OUT_CODE                      AS 'A.OUT_CODE'
                             ,A.STOCK_MST_ID                  AS 'STOCK_MST_ID'
                             ,B.OUT_CODE                      AS 'B.OUT_CODE'
                             ,B.NAME                          AS 'B.NAME'
                             ,B.STANDARD                      AS 'B.STANDARD'
                             ,B.TYPE                          AS 'B.TYPE'
							 ,A.WORK_CAPA_STD_OPERATOR	      AS 'A.WORK_CAPA_STD_OPERATOR'
					    	 ,A.WORK_CAPA_WORKING_HR_SHIFT	  AS 'A.WORK_CAPA_WORKING_HR_SHIFT'
                             ,A.INSTRUCT_QTY                  AS 'A.INSTRUCT_QTY'
                             ,A.SORT                          AS 'A.SORT'
                             ,A.COMMENT                       AS 'A.COMMENT'
                             ,A.COMPLETE_YN                   AS 'A.COMPLETE_YN'
                             ,A.USE_YN                        AS 'A.USE_YN'
                             ,A.REG_USER                      AS 'A.REG_USER'
                             ,A.REG_DATE                      AS 'A.REG_DATE'
                             ,A.UP_USER                       AS 'A.UP_USER'
                             ,A.UP_DATE                       AS 'A.UP_DATE'		
                              FROM [dbo].[PRODUCTION_INSTRUCT] A
                              inner join STOCK_MST b on A.STOCK_MST_ID = b.id
                            WHERE 1=1";
                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString()
                    + this._pMenuSettingEntity.BASE_WHERE
                    + this._pMenuSettingEntity.BASE_ORDER;


                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpMain);
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
