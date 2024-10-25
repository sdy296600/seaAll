using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.Spreadsheet;
using FarPoint.Win.Spread;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class ORDER_OUT_EXPORT : DoubleBaseForm1
    {
        public ORDER_OUT_EXPORT()
        {
            InitializeComponent();
            fpMain.CellClick += fpMain_CellClick;
            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);


        }

        private void DoubleBaseForm2_Load(object sender, EventArgs e)
        {

        }

        public override void fpMain_CellClick(object sender, CellClickEventArgs e)
        {
            try
            {

                string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                if (pHeaderLabel != "입력" && pHeaderLabel != "합계")
                {
                    _Mst_Id = fpMain.Sheets[0].GetValue(e.Row, "ID").ToString();

                    //SubFind_DisplayData();

                }

            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public override void SubFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);
                fpSub.Sheets[0].Rows.Count = 0;

                //       if (this.Text == "수주출고요청서출력")
                //       {
                //           string str = $@"select *
                //                     from [dbo].[ORDER_DETAIL] A
                //                     INNER JOIN [dbo].[COMPANY] B ON A.DEMAND_COMPANY = B.ID
                //                     INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                //                     INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
                //INNER JOIN [dbo].[Code_Mst] E ON D.TYPE = E.code
                //and E.code_type = 'SD04'
                //INNER JOIN [dbo].[Code_Mst] F ON D.UNIT = F.code
                //and F.code_type = 'CD04'
                //INNER JOIN [dbo].[Code_Mst] G ON A.CONVERSION_UNIT = G.code
                //and G.code_type = 'CD04'
                //INNER JOIN [dbo].[Code_Mst] H ON A.OUT_TYPE = H.code
                //and H.code_type = 'CD20'
                //INNER JOIN [dbo].[Code_Mst] I ON C.ORDER_TYPE = I.code
                //and I.code_type = 'SD09'
                //                     WHERE 1=1
                //                     AND A.USE_YN = 'Y'
                //                     AND C.ID = {_Mst_Id}";

                //           string where = @"AND A.COMPLETE_YN != 'Y'
                //                        AND A.OUT_TYPE = 'CD20002'";
                //           StringBuilder sb = new StringBuilder();
                //           //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                //           where += sb.ToString();

                //           string sql = str + where;
                //           DataTable _DataTable = new CoreBusiness().SELECT(sql);

                //           if (_DataTable != null && _DataTable.Rows.Count > 0)
                //           {
                //               fpSub.Sheets[0].Visible = false;
                //               fpSub.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                //               for (int i = 0; i < _DataTable.Rows.Count; i++)
                //               {
                //                   foreach (DataColumn item in _DataTable.Columns)
                //                   {
                //                       fpSub.Sheets[0].SetValue(i, item.ColumnName.ToUpper(), _DataTable.Rows[i][item.ColumnName]);


                //                   }

                //               }

                //               fpSub.Sheets[0].Visible = true;


                //           }
                //           else
                //           {
                //               fpSub.Sheets[0].Rows.Count = 0;
                //               CustomMsg.ShowMessage("조회 내역이 없습니다.");
                //           }
                //       }

                //       else
                //       {

                //       }

                //       if (this.Text == "수주출고요청서출력")
                //       {
                //           string str = $@"select *
                //                     from [dbo].[ORDER_DETAIL] A
                //                     INNER JOIN [dbo].[COMPANY] B ON A.DEMAND_COMPANY = B.ID
                //                     INNER JOIN [dbo].[ORDER_MST] C ON A.ORDER_MST_ID = C.ID
                //                     INNER JOIN [dbo].[STOCK_MST] D ON A.STOCK_MST_ID = D.ID
                //INNER JOIN [dbo].[Code_Mst] E ON D.TYPE = E.code
                //and E.code_type = 'SD04'
                //INNER JOIN [dbo].[Code_Mst] F ON D.UNIT = F.code
                //and F.code_type = 'CD04'
                //INNER JOIN [dbo].[Code_Mst] G ON A.CONVERSION_UNIT = G.code
                //and G.code_type = 'CD04'
                //INNER JOIN [dbo].[Code_Mst] H ON A.OUT_TYPE = H.code
                //and H.code_type = 'CD20'
                //INNER JOIN [dbo].[Code_Mst] I ON C.ORDER_TYPE = I.code
                //and I.code_type = 'SD09'
                //                     WHERE 1=1
                //                     AND A.USE_YN = 'Y'
                //                     AND C.ID = {_Mst_Id}";

                //           string where = @"AND A.COMPLETE_YN != 'Y'
                //                        AND A.OUT_TYPE = 'CD20002'";
                //           StringBuilder sb = new StringBuilder();
                //           Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                //           where += sb.ToString();

                //           string sql = str + where;
                //           DataTable _DataTable = new CoreBusiness().SELECT(sql);

                //           if (_DataTable != null && _DataTable.Rows.Count > 0)
                //           {
                //               fpSub.Sheets[0].Visible = false;
                //               fpSub.Sheets[0].Rows.Count = _DataTable.Rows.Count;

                //               for (int i = 0; i < _DataTable.Rows.Count; i++)
                //               {
                //                   foreach (DataColumn item in _DataTable.Columns)
                //                   {
                //                       fpSub.Sheets[0].SetValue(i, item.ColumnName.ToUpper(), _DataTable.Rows[i][item.ColumnName]);


                //                   }

                //               }

                //               fpSub.Sheets[0].Visible = true;


                //           }
                //           else
                //           {
                //               fpSub.Sheets[0].Rows.Count = 0;
                //               CustomMsg.ShowMessage("조회 내역이 없습니다.");
                //           }
                //       }

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
            //NewMethod();
            //return;
            if (_Mst_Id == string.Empty)
            {
                Function.Core._AddItemButtonClicked(fpMain, MainForm.UserEntity.user_account);
            }
            else
            {
                Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);
                int row = 0;

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "ID").ToString() == _Mst_Id)
                    {
                        row = i;
                    }
                }
                string mst = this._pMenuSettingEntity.BASE_TABLE.Split('/')[0] + "_ID";
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, mst, _Mst_Id);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "DEMAND_COMPANY".Trim(), fpMain.Sheets[0].GetValue(row, "ORDER_COMPANY".Trim()));
            }
        }

        public override void _ExportButtonClicked(object sender, EventArgs e)
        {
            try
            {
                //int ck_cnt = 0;
                //DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                //sc.LoadDocument(Application.StartupPath + $"\\업체명_출고요청서.xlsx", DocumentFormat.Xlsx);
                //Worksheet sheet = sc.Document.Worksheets[0];
                //for (int i = 0; i < fpSub.Sheets[0].Rows.Count; i++)
                //{
                //    if(fpSub.ActiveSheet.Cells[i, 0].Text == "True")
                //    {
                //        ck_cnt++;
                //        sheet.Cells[i+11,14].SetValueFromText(fpSub.ActiveSheet.Cells[i, 4].Text); //제품번호
                //        sheet.Cells[i+11,15].SetValueFromText(fpSub.ActiveSheet.Cells[i, 5].Text); //제품명
                //        sheet.Cells[i+11,16].SetValueFromText(fpSub.ActiveSheet.Cells[i, 6].Text); //규격
                //        sheet.Cells[i+11,17].SetValueFromText(fpSub.ActiveSheet.Cells[i, 8].Text); //단위
                //        sheet.Cells[i+11,18].SetValueFromText(fpSub.ActiveSheet.Cells[i, 10].Text); //요청량
                //        if (ck_cnt > 40)
                //        {
                //            return;
                //        }
                //    }
                //}
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }


    }
}
