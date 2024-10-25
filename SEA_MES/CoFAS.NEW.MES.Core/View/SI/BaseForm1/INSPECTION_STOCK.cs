using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread.CellType;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class INSPECTION_STOCK : BaseForm1
    {
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"select
A.ID AS ID
,A.INSPECTION_CODE AS INSPECTION_CODE
,A.INSPECTION_TYPE AS INSPECTION_TYPE
,A.STOCK_MST_ID AS STOCK_MST_ID
,A.STOCK_MST_ID AS STOCK_MST_OUT_CODE
,A.STOCK_MST_ID AS STOCK_MST_TYPE
,A.STOCK_MST_ID AS STOCK_MST_STANDARD
,A.OK_QTY AS OK_QTY
,A.BAD_QTY AS BAD_QTY
,A.BAD_COMMENT AS BAD_COMMENT
,A.USE_YN AS USE_YN
,A.COMPLETE_YN AS COMPLETE_YN
,A.REG_USER AS REG_USER
,A.REG_DATE AS REG_DATE
,A.UP_USER AS UP_USER
,A.UP_DATE AS UP_DATE
from [dbo].[INSPECTION_STOCK] A
left join [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID";

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
    }
}
