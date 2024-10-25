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
    public partial class 생산계획등록_이튼 : DoubleBaseForm1
    {
        public 생산계획등록_이튼()
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
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"select A.ID				  AS   'ID'				
                                      ,B.OUT_CODE		  AS   'B.OUT_CODE'	
                                      ,A.STOCK_MST_ID	  AS   'STOCK_MST_ID'	
                                      ,C.OUT_CODE		  AS   'C.OUT_CODE'		
                                      ,C.NAME			  AS   'C.NAME'			
                                      ,C.STANDARD		  AS   'C.STANDARD'		
                                      ,D.code_name		  AS   'D.CODE_NAME'		
                                      ,A.SUPPLY_TYPE	  AS   'A.SUPPLY_TYPE'	
                                      ,E.code_name		  AS   'E.CODE_NAME'	
                                      ,A.STOCK_MST_PRICE  AS   'A.STOCK_MST_PRICE'
                                      ,A.ORDER_QTY		  AS   'A.ORDER_QTY'
                                      ,A.COST			  AS   'A.COST'			
                                      ,F.NAME			  AS   'F.NAME'			
                                      ,A.DEMAND_DATE	  AS   'A.DEMAND_DATE'	
                                      ,A.COMMENT		  AS   'A.COMMENT'		
                                      ,A.INSPECTION_YN	  AS   'A.INSPECTION_YN'	
                                      ,A.COMPLETE_YN	  AS   'A.COMPLETE_YN'	
                                      ,A.USE_YN			  AS   'A.USE_YN'			
                                      ,A.REG_USER		  AS   'A.REG_USER'		
                                      ,A.REG_DATE		  AS   'A.REG_DATE'		
                                      ,A.UP_USER 		  AS   'A.UP_USER' 	
                                      ,A.UP_DATE          AS   'A.UP_DATE'
                                  from ORDER_DETAIL A
                                  INNER JOIN ORDER_MST B ON A.ORDER_MST_ID = B.ID
                                  INNER JOIN STOCK_MST C ON A.STOCK_MST_ID = C.ID
                                  INNER JOIN Code_Mst D ON C.TYPE = D.code
                                  INNER JOIN Code_Mst E ON C.UNIT = E.code
                                  INNER JOIN COMPANY F ON A.DEMAND_COMPANY = F.ID
                                  WHERE 1=1   
                                  AND A.USE_YN = 'Y'
                                  AND A.COMPLETE_YN != 'Y'";
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


                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_ID              ".Trim(), fpMain.Sheets[0].GetValue(row, "STOCK_MST_ID ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_OUT_CODE        ".Trim(), fpMain.Sheets[0].GetValue(row, "STOCK_MST_ID ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_STANDARD        ".Trim(), fpMain.Sheets[0].GetValue(row, "STOCK_MST_ID ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_TYPE            ".Trim(), fpMain.Sheets[0].GetValue(row, "STOCK_MST_ID ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "OUT_CODE                  ".Trim(), fpMain.Sheets[0].GetValue(row, "B.OUT_CODE   ".Trim()));
                DateTime dateTime = DateTime.Now;
                int su = 0;

                switch (DateTime.Now.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        su = 5;
                        break;
                    case DayOfWeek.Monday:
                        su = 4;
                        break;
                    case DayOfWeek.Tuesday:
                        su = 3;
                        break;
                    case DayOfWeek.Wednesday:
                        su = 2;
                        break;
                    case DayOfWeek.Thursday:
                        su = 1;
                        break;
                    case DayOfWeek.Friday:
                        su = 0;
                        break;
                    case DayOfWeek.Saturday:
                        su = 6;
                        break;
                }

                dateTime = dateTime.AddDays(su);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "PLAN_END_DATE ".Trim(), dateTime);
               
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
        //public override void MainSave_InputData()
        //{
        //    try
        //    {
        //        DevExpressManager.SetCursor(this, Cursors.WaitCursor);

        //        fpMain.Focus();

        //    }
        //    catch (Exception _Exception)
        //    {
        //        CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
        //    }
        //    finally
        //    {
        //        DevExpressManager.SetCursor(this, Cursors.Default);
        //    }
        //}

        //public override void fpMain_CellClick(object sender, CellClickEventArgs e)
        //{
        //    try
        //    {

        //        string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
        //        if (pHeaderLabel != "입력")
        //        {
        //            _Mst_Id = fpMain.Sheets[0].GetValue(e.Row, "ID").ToString();

        //            SubFind_DisplayData(_Mst_Id);
        //        }

        //    }
        //    catch (Exception _Exception)
        //    {
        //        CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
        //    }
        //}
    }
}
