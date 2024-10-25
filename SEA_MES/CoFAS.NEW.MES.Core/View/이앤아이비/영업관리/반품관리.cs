using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 반품관리 : DoubleBaseForm1
    {
        public 반품관리()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);

          
        }

        private void DoubleBaseForm2_Load(object sender, EventArgs e)
        {

        }
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT A.ID					   AS  'ID'
                                      ,B.OUT_CODE			   AS  'B.OUT_CODE'
	                                  ,B.NAME				   AS  'B.NAME'
                                      ,C.ID		               AS  'C.ID'	
	                                  ,C.NAME		           AS  'C.NAME'	
                                      ,C.OUT_CODE	           AS  'C.OUT_CODE'
                                      ,C.STANDARD	           AS  'C.STANDARD'
                                      ,C.TYPE		           AS  'C.TYPE'
                                      ,A.SUPPLY_TYPE		   AS  'A.SUPPLY_TYPE'
                                      ,E.code_name	           AS  'E.CODE_NAME'
                                      ,A.STOCK_MST_PRICE	   AS  'A.STOCK_MST_PRICE'
	                                  ,C.QTY                   AS  'C.QTY'
                                      ,A.ORDER_QTY			   AS  'A.ORDER_QTY'
                                      ,A.ORDER_REMAIN_QTY	   AS  'A.ORDER_REMAIN_QTY'
                                      ,A.COST				   AS  'A.COST'
                                      ,D.NAME          		   AS  'D.NAME'
                                      ,D.COLUMN4               AS  'D.COLUMN4'
                                      ,A.DEMAND_DATE		   AS  'A.DEMAND_DATE'
                                      ,A.COMMENT			   AS  'A.COMMENT'
                                      ,A.INSPECTION_YN		   AS  'A.INSPECTION_YN'
                                      ,A.COMPLETE_YN		   AS  'A.COMPLETE_YN'
                                      ,A.USE_YN				   AS  'A.USE_YN'
                                      ,A.REG_USER			   AS  'A.REG_USER'
                                      ,A.REG_DATE			   AS  'A.REG_DATE'
                                      ,A.UP_USER			   AS  'A.UP_USER'
                                      ,A.UP_DATE			   AS  'A.UP_DATE'
                                      FROM [dbo].[ORDER_DETAIL] A
                                      INNER JOIN [dbo].[ORDER_MST] B ON A.ORDER_MST_ID = B.ID
                                      INNER JOIN [dbo].[STOCK_MST] C ON A.STOCK_MST_ID = C.ID
                                      INNER JOIN [dbo].[COMPANY] D ON A.COMPANY_ID = D.ID
                                      INNER JOIN [dbo].[Code_Mst] E ON C.UNIT = E.code 
                                      WHERE 1=1
                                      AND A.USE_YN = 'Y'
                                      AND B.ORDER_TYPE LIKE '%SD09%'";
                        
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
                            
                            if (item.ColumnName.Contains("A.COMPLETE_YN"))
                            {
                                switch (_DataTable.Rows[i][item.ColumnName].ToString())
                                {
                                    case "Y":
                                        fpMain.Sheets[0].Rows[i].BackColor = Color.FromArgb(198, 239, 206);
                                        fpMain.Sheets[0].Rows[i].Locked = true;
                                        break;
                                    case "W":
                                        fpMain.Sheets[0].Rows[i].BackColor = Color.LightBlue;
                                        fpMain.Sheets[0].Rows[i].Locked = true;
                                        break;
                                }
                            }

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
                                 A.ID						AS 'ID'
                                ,A.IN_DATE					AS 'IN_DATE'
                                ,A.OUT_CODE					AS 'OUT_CODE'
                                ,A.IN_TYPE					AS 'IN_TYPE'
                                ,A.ORDER_DETAIL_ID			AS 'ORDER_DETAIL_ID'
                                ,A.PRODUCTION_INSTRUCT_ID	AS 'PRODUCTION_INSTRUCT_ID'
                                ,A.STOCK_MST_ID				AS 'STOCK_MST_ID'
                                ,B.OUT_CODE					AS 'STOCK_MST_OUT_CODE'
                                ,B.NAME						AS 'STOCK_MST_NAME'
                                ,B.STANDARD					AS 'STOCK_MST_STANDARD'
                                ,B.TYPE						AS 'STOCK_MST_TYPE'
                                ,A.IN_QTY                   AS 'IN_QTY'
                                ,A.COMPLETE_QTY				AS 'COMPLETE_QTY'
                                ,A.COMMENT					AS 'COMMENT'
                                ,A.COMPLETE_YN				AS 'COMPLETE_YN'
                                ,A.USE_YN					AS 'USE_YN'
                                ,A.REG_USER					AS 'REG_USER'
                                ,A.REG_DATE					AS 'REG_DATE'
                                ,A.UP_USER					AS 'UP_USER'
                                ,A.UP_DATE					AS 'UP_DATE'
                                ,A.INSPECTION_YN			AS 'INSPECTION_YN'

                                from [dbo].[IN_STOCK_WAIT_DETAIL] A
                                inner join [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID
                                WHERE 1=1
                                AND A.ORDER_DETAIL_ID = {_Mst_Id}";

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str;

                DataTable _DataTable = new CoreBusiness().SELECT(sql);
                Function.Core.DisplayData_Set(_DataTable, fpSub);
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

            if (_Mst_Id　!=  string.Empty)
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
                string mst=  this._pMenuSettingEntity.BASE_TABLE.Split('/')[0] +"_ID";
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, mst, _Mst_Id);

                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "IN_QTY                    ".Trim(), fpMain.Sheets[0].GetValue(row, "A.ORDER_QTY ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_ID              ".Trim(), fpMain.Sheets[0].GetValue(row, "C.ID ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_OUT_CODE        ".Trim(), fpMain.Sheets[0].GetValue(row, "C.OUT_CODE ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_NAME            ".Trim(), fpMain.Sheets[0].GetValue(row, "C.NAME ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_STANDARD        ".Trim(), fpMain.Sheets[0].GetValue(row, "C.STANDARD ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "STOCK_MST_TYPE            ".Trim(), fpMain.Sheets[0].GetValue(row, "C.TYPE ".Trim()));

            }


        }
    


       
    }
}
