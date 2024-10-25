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
    public partial class ORDER_PLAN_REGISTER : DoubleBaseForm1
    {

        #region ○ 변수선언

        public string _UserAccount = string.Empty;
        public string qty = "0";

        #endregion

        public ORDER_PLAN_REGISTER()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            fpMain.Change += fpMain_Change;
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);

          
        }

        private void DoubleBaseForm2_Load(object sender, EventArgs e)
        {
            //splitContainer2.Orientation = Orientation.Vertical;
            //splitContainer2.SplitterDistance = 300;
            _UserAccount = MainForm.UserEntity.user_account;
        }
        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (_Mst_Id != string.Empty)
                {
                    Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);

                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_OUT_CODE", _Mst_Id);
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_ID", _Mst_Id);
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_STANDARD", _Mst_Id);
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_TYPE", _Mst_Id);


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

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string select = @"select
                                  A.STOCK_MST_ID
                                 ,B.OUT_CODE
                                 ,B.NAME
                                 ,B.STANDARD
                                 ,B.TYPE
                                 ,SUM(A.ORDER_QTY) AS ORDER_QTY
                                 ,B.OUT_SCHEDULE
                                 ,B.IN_SCHEDULE
                                 ,B.QTY
                                 from [dbo].[ORDER_DETAIL] A
                                 INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID
                                 where 1=1";

                string where = @"AND A.COMPLETE_YN != 'Y'
                                 AND A.USE_YN = 'Y'";
                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                where += sb.ToString();
                string group = @" GROUP BY 
                                 A.STOCK_MST_ID
                                ,B.OUT_CODE
                                ,B.NAME
                                ,B.STANDARD
                                ,B.TYPE
                                ,B.OUT_SCHEDULE
                                ,B.IN_SCHEDULE
                                ,B.QTY";


                string sql = select + where + group;
                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                    for (int i = 0; i < _DataTable.Rows.Count; i++)
                    {
                        foreach (DataColumn item in _DataTable.Columns)
                        {
                            fpMain.Sheets[0].SetValue(i, item.ColumnName.ToUpper(), _DataTable.Rows[i][item.ColumnName]);


                        }

                    }

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

                DataTable _DataTable = new SI_Business().PRODUCTION_PLAN_R10(_Mst_Id, this._pMenuSettingEntity.BASE_TABLE);

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

                //string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                //if (pHeaderLabel != "입력" && pHeaderLabel != "합계")
                //{
                //    _Mst_Id  =  fpMain.Sheets[0].GetValue(e.Row, "STOCK_MST_ID").ToString();

                //    SubFind_DisplayData(_Mst_Id);
                   
                //}

            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        private void fpMain_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            //MessageBox.Show(fpSpread1.ActiveSheet.Cells[e.Row, e.Column].Text);
            //for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
            //{
                if (/*fpMain.ActiveSheet.Cells[i, 0].Text == "True"*/ fpMain.Sheets[0].GetValue(e.Row, "CK").ToString() == "True")
                {
                Function.Core._AddItemButtonClicked(fpSub, _UserAccount);
                    _Mst_Id = fpMain.Sheets[0].GetValue(e.Row, "STOCK_MST_ID").ToString();
                    qty = fpMain.Sheets[0].GetValue(e.Row, "PLAN_QTY").ToString();
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_OUT_CODE", _Mst_Id);
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_ID", _Mst_Id);
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_STANDARD", _Mst_Id);
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_TYPE", _Mst_Id);
                    fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "INSTRUCT_QTY", qty);
                }
                else
                {

                    //if (fpSub.Sheets[0].Rows.Count > 1)
                    //{
                        //for (int x = 0; x < fpSub.Sheets[0].Rows.Count; x++)
                        //{
                            //if (fpSub.Sheets[0].GetText(x, 3).ToString() != fpMain.Sheets[0].GetValue(e.Row, "STOCK_MST_CODE").ToString())
                            //{
                            //    YDSCore._AddItemButtonClicked(fpSub, _UserAccount);
                            //    _Mst_Id = fpMain.Sheets[0].GetValue(e.Row, "STOCK_MST_ID").ToString();
                            //    qty = fpMain.Sheets[0].GetValue(e.Row, "PLAN_QTY").ToString();
                            //    fpSub.Sheets[0].SetValue(x + 1, "STOCK_MST_OUT_CODE", _Mst_Id);
                            //    fpSub.Sheets[0].SetValue(x + 1, "STOCK_MST_ID", _Mst_Id);
                            //    fpSub.Sheets[0].SetValue(x + 1, "STOCK_MST_STANDARD", _Mst_Id);
                            //    fpSub.Sheets[0].SetValue(x + 1, "STOCK_MST_TYPE", _Mst_Id);
                            //    fpSub.Sheets[0].SetValue(x + 1, "INSTRUCT_QTY", qty);
                            //}
                            

                            //else
                            //{
                            //    qty = fpMain.Sheets[0].GetValue(e.Row, "PLAN_QTY").ToString();
                            //    string sum = Convert.ToString(Convert.ToInt64(fpSub.ActiveSheet.Cells[x, 7].Text.ToString()) + Convert.ToDouble(qty));
                            //    fpSub.Sheets[0].SetValue(x, 7, sum);
                            //}
                        //}
                    //}
                }
            //}
        }
    }
}
