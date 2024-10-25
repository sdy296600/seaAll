using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class OUTSOURCING_INSTRUCT_REGISTER : DoubleBaseForm1
    {
        public OUTSOURCING_INSTRUCT_REGISTER()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);

          
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

                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "START_INSTRUCT_DATE", null);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "END_INSTRUCT_DATE", null);

                int row = 0;

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "ID").ToString() == _Mst_Id)
                    {
                        row = i;
                    }
                }

                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "INSTRUCT_DATE             ".Trim(), fpMain.Sheets[0].GetValue(row,"PLAN_DATE                 ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_ID              ".Trim(), fpMain.Sheets[0].GetValue(row,"STOCK_MST_ID              ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_OUT_CODE        ".Trim(), fpMain.Sheets[0].GetValue(row,"STOCK_MST_OUT_CODE        ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_STANDARD        ".Trim(), fpMain.Sheets[0].GetValue(row,"STOCK_MST_STANDARD        ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_TYPE            ".Trim(), fpMain.Sheets[0].GetValue(row,"STOCK_MST_TYPE            ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "OUT_CODE                  ".Trim(), fpMain.Sheets[0].GetValue(row,"OUT_CODE                  ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "INSTRUCT_QTY              ".Trim(), fpMain.Sheets[0].GetValue(row,"PLAN_QTY                  ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "SORT                      ".Trim(), fpMain.Sheets[0].GetValue(row,"SORT                      ".Trim()));
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

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"select 
                               A.ID        AS 'ID'
                              ,A.OUT_CODE       AS 'OUT_CODE'
                              ,A.ORDER_MST_ID             AS 'ORDER_MST_ID'
                              ,A.ORDER_DETAIL_ID      AS 'ORDER_DETAIL_ID'
							  ,A.EQUIPMENT_ID            AS 'EQUIPMENT_ID'
							  ,A.PLAN_DATE         AS 'PLAN_DATE'
							  ,A.PLAN_END_DATE             AS 'PLAN_END_DATE'
							  ,A.STOCK_MST_ID         AS 'STOCK_MST_ID'
							  ,A.STOCK_MST_OUT_CODE        AS 'STOCK_MST_OUT_CODE'
							  ,A.STOCK_MST_STANDARD        AS 'STOCK_MST_STANDARD'
							  ,A.STOCK_MST_TYPE        AS 'STOCK_MST_TYPE'
							  ,A.PROCESS_ID        AS 'PROCESS_ID'
							  ,A.PLAN_QTY        AS 'PLAN_QTY'
							  ,A.INSTRUCT_QTY        AS 'INSTRUCT_QTY'
                              ,A.REMAIN_QTY        AS 'REMAIN_QTY'
                              ,A.STOCK_MST_IN_SCHEDULE        AS 'STOCK_MST_IN_SCHEDULE'
                              ,A.STOCK_MST_OUT_SCHEDULE        AS 'STOCK_MST_OUT_SCHEDULE'
							  ,A.SORT AS 'SORT'
							  ,A.COMMENT  AS 'COMMENT'
							  ,A.COMPLETE_YN             AS 'COMPLETE_YN'
							  ,A.USE_YN           AS 'USE_YN'
                              ,B.INSPECTION_YN    AS 'INSPECTION_YN'
                              ,A.REG_DATE         AS 'REG_DATE'
							  ,A.REG_USER         AS 'REG_USER'
							  ,A.UP_USER          AS 'UP_USER'
							  ,A.UP_DATE          AS 'UP_DATE'
                              from [dbo].[PRODUCTION_PLAN] A
                              INNER JOIN [dbo].[STOCK_MST] B on A.STOCK_MST_ID = B.ID
                              INNER JOIN [dbo].[PROCESS] C on B.PROCESS_ID = C.ID
                              WHERE 1=1
                              AND A.USE_YN = 'Y'
                              AND C.TYPE = 'CD14002'";

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString();

                DataTable _DataTable = new CoreBusiness().SELECT(sql);
                fpMain.Sheets[0].Rows.Count = 0;
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpMain.Sheets[0].SetValue(i, item.ColumnName, _DataTable.Rows[i][item.ColumnName]);


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

        public override void SubFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;

                string str = $@"select 
                               A.ID        AS 'ID'
                              ,A.PRODUCTION_PLAN_ID       AS 'PRODUCTION_PLAN_ID'
                              ,A.OUT_CODE             AS 'OUT_CODE'
                              ,A.INSTRUCT_DATE      AS 'INSTRUCT_DATE'
							  ,A.STOCK_MST_ID            AS 'STOCK_MST_ID'
							  ,A.STOCK_MST_OUT_CODE         AS 'STOCK_MST_OUT_CODE'
							  ,A.STOCK_MST_STANDARD             AS 'STOCK_MST_STANDARD'
							  ,A.STOCK_MST_TYPE         AS 'STOCK_MST_TYPE'
							  ,A.PROCESS_ID        AS 'PROCESS_ID'
							  ,A.INSTRUCT_QTY        AS 'INSTRUCT_QTY'
							  ,A.START_INSTRUCT_DATE		  AS 'START_INSTRUCT_DATE'
							  ,A.END_INSTRUCT_DATE        AS 'END_INSTRUCT_DATE'
							  ,A.SORT AS 'SORT'
							  ,A.COMMENT  AS 'COMMENT'
							  ,A.COMPLETE_YN             AS 'COMPLETE_YN'
							  ,A.USE_YN           AS 'USE_YN'
                              ,A.REG_DATE         AS 'REG_DATE'
							  ,A.REG_USER         AS 'REG_USER'
							  ,A.UP_USER          AS 'UP_USER'
							  ,A.UP_DATE          AS 'UP_DATE'
                              from [dbo].[PRODUCTION_INSTRUCT] A
                              INNER JOIN [dbo].[PROCESS] B on A.PROCESS_ID = B.ID
                              WHERE 1=1
                              AND A.USE_YN = 'Y'
                              AND A.PRODUCTION_PLAN_ID = {_Mst_Id}
                              AND B.TYPE = 'CD14002'";

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str;

                DataTable _DataTable = new CoreBusiness().SELECT(sql);
                fpSub.Sheets[0].Rows.Count = 0;
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
    }
}
