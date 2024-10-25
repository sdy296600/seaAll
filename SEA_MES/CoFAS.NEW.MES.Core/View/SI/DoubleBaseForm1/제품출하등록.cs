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
    public partial class 제품출하등록 : DoubleBaseForm3
    {
        public 제품출하등록()
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

                string str = $@"select 
                                B.OUT_CODE		   AS	'B.OUT_CODE'
                               ,B.NAME			   AS	'B.NAME'
                               ,C.NAME			   AS	'C.NAME'
                               ,B.ORDER_DATE	   AS	'B.ORDER_DATE'
                               ,A.ID               AS   'ID'
                               ,A.OUT_CODE		   AS	'A.OUT_CODE'
                               ,A.ORDER_MST_ID	   AS	'A.ORDER_MST_ID'
                               ,A.OUT_STOCK_DATE  AS	'A.OUT_STOCK_DATE'
                               ,A.OUT_TYPE		   AS	'A.OUT_TYPE'
                               ,A.COMMENT		   AS	'A.COMMENT'
                               ,A.COMPLETE_YN	   AS	'A.COMPLETE_YN'
                               ,A.USE_YN		   AS	'A.USE_YN'
                               ,A.REG_USER		   AS	'A.REG_USER'
                               ,A.REG_DATE		   AS	'A.REG_DATE'
                               ,A.UP_USER         AS	'A.UP_USER'
                               ,A.UP_DATE         AS	'A.UP_DATE'
                               from [dbo].[OUT_STOCK_WAIT_MST] A
                               INNER JOIN [dbo].[ORDER_MST] B　ON A.ORDER_MST_ID = B.ID
                               INNER JOIN [dbo].[COMPANY] C ON B.COMPANY_ID = C.ID
                               WHERE 1=1
                               AND A.USE_YN = 'Y'
                               AND A.OUT_TYPE = 'SD14003'";
                        
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

                string sql = $@"SELECT 
                                A.ID                            AS 'ID'
                               ,A.OUT_STOCK_WAIT_MST_ID         AS 'A.OUT_STOCK_WAIT_MST_ID'
                               ,A.ORDER_DETAIL_ID               AS 'A.ORDER_DETAIL_ID'
                               ,B.ORDER_MST_ID                  AS 'B.ORDER_MST_ID'
                               ,B.STOCK_MST_ID                  AS 'B.STOCK_MST_ID'
                               ,C.OUT_CODE						AS 'C.OUT_CODE'
                               ,C.NAME							AS 'C.NAME'
							   ,E.code_name						AS 'E.CODE_NAME'
                               ,C.OUT_SCHEDULE					AS 'C.OUT_SCHEDULE'
                               ,C.IN_SCHEDULE					AS 'C.IN_SCHEDULE'
                               ,C.QTY							AS 'C.QTY'
                               ,B.ORDER_QTY						AS 'B.ORDER_QTY'
                               ,B.ORDER_REMAIN_QTY　　		 　 AS 'B.ORDER_REMAIN_QTY'
                               ,D.WAIT_QTY         　　　　　   AS 'WAIT_QTY'
                               ,A.OUT_QTY              　　　   AS 'A.OUT_QTY'  
                               ,A.COMMENT              　　　   AS 'A.COMMENT'  
                               ,A.COMPLETE_YN          　　　   AS 'A.COMPLETE_YN'
                               ,A.USE_YN               　　　   AS 'A.USE_YN'  
                               ,A.REG_USER             　　　   AS 'A.REG_USER'
                               ,A.REG_DATE             　　　   AS 'A.REG_DATE'
                               ,A.UP_USER              　　　   AS 'A.UP_USER'
                               ,A.UP_DATE              　　　   AS 'A.UP_DATE'
                               FROM OUT_STOCK_WAIT_DETAIL A
							   INNER JOIN ORDER_DETAIL B ON A.ORDER_DETAIL_ID = B.ID  AND B.OUT_TYPE != 'CD20001'
							   INNER JOIN STOCK_MST C ON B.STOCK_MST_ID = C.ID                           
                                LEFT JOIN 
                                (
                                 SELECT a.ORDER_DETAIL_ID,a.STOCK_MST_ID ,sum(a.OUT_QTY) as WAIT_QTY
                                 FROM OUT_STOCK_WAIT_DETAIL a
                                  WHERE USE_YN = 'Y'
                                  group by a.ORDER_DETAIL_ID,a.STOCK_MST_ID
                                )D ON B.ID = D.ORDER_DETAIL_ID AND B.STOCK_MST_ID = D.STOCK_MST_ID
                              INNER JOIN Code_Mst E ON C.TYPE = E.code
							  WHERE 1=1
                               AND B.STOP_YN = 'N'
                               AND B.USE_YN = 'Y'
							   AND A.USE_YN = 'Y'
							   AND A.OUT_STOCK_WAIT_MST_ID = {_Mst_Id};";

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

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
                    //CustomMsg.ShowMessage("조회 내역이 없습니다.");
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
