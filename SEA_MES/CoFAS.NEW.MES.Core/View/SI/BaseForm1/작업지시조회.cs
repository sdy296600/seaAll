using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 작업지시조회 : BaseForm1
    {

        public 작업지시조회()
        {
            InitializeComponent();
            Load += new EventHandler(Form_Load);
        }


        private void Form_Load(object sender, EventArgs e)
        {
            DevExpressManager.SetCursor(this, Cursors.WaitCursor);
            fpMain.Sheets[0].Rows.Count = 0;
            //MainFind_DisplayData();
        }

        public override void MainFind_DisplayData()
        {
            try
            {
                // MPL02818AC-DIE

                Base_FromtoDateTime datetime = _PAN_WHERE.Controls[0] as Base_FromtoDateTime;
                string strTime = datetime.StartValue.ToString("yyyy-MM-dd");
                string endTime = datetime.EndValue.ToString("yyyy-MM-dd");
                
                Base_textbox txtBox = _PAN_WHERE.Controls[1] as Base_textbox;
                string RESOURCE_NO = txtBox.SearchText;
                if (txtBox.SearchText == null)
                {
                   RESOURCE_NO = "";
                }

                string GET_DEMAND_MSTR =  $"  SELECT T1.ORDER_NO                                                           " +
                                          $"       , T1.LOT                                                                " +
                                          $"       , T1.ORDER_TYPE                                                         " +
	                                      $"       , T1.RESOURCE_NO                                                        " +
                                          $"       , T1.DEMAND_STATUS                                                      " +
	                                      $"       , T1.ORDER_DATE                                                         " +
                                          $"       , T1.ORDER_QTY                                                          " +
	                                      $"       , T1.DATE_SCHED_ORIG                                                    " +
                                          $"       , T1.DATE_SCHED_CURR                                                    " +
	                                      $"       , T1.LABEL_NO                                                           " +
	                                      $"       , T1.QTY_COMPLETE                                                       " +
	                                      $"       , (SELECT ISNULL(SUM(S1.P_QTY), 0)                                      " +
                                          $"            FROM HS_MES.DBO.PRODUCT_BARCODE S1                                 " +
                                          $"           WHERE S1.LOT_NO	    = T1.LOT                                       " +
                                          $"             AND S1.RESOURCE_NO = T1.RESOURCE_NO                               " +
                                          $"         ) AS COM_QTY                                                          " +
	                                      $"       , T1.QTY_SCRAPPED                                                       " +
	                                      $"       , T1.QTY_HELD                                                           " +
	                                      $"       , T1.INPROCESS_CODE                                                     " +
	                                      $"       , T1.CUSTOMER_NAME                                                      " +
	                                      $"       , T1.DATE_PLAN_START                                                    " +
	                                      $"       , T1.LEAD_TIME                                                          " +
	                                      $"       , T1.LEAD_TIME_UNIT                                                     " +
	                                      $"       , T1.LEAD_TIME_WHERE                                                    " +
	                                      $"       , T1.DPOSS_SRC                                                          " +
	                                      $"       , T1.DPOSS_PART                                                         " +
	                                      $"       , T1.SHIP_TO                                                            " +
	                                      $"       , T1.ORDER_SKEY                                                         " +
	                                      $"       , T1.ORDER_LINE_SKEY                                                    " +
	                                      $"       , T1.HOLD_CODE                                                          " +
	                                      $"       , T1.SCH_START_DATE                                                     " +
	                                      $"       , T1.SCH_END_DATE                                                       " +
	                                      $"       , T1.SCH_METHOD                                                         " +
	                                      $"       , T1.SCH_STATUS                                                         " +
	                                      $"       , T1.PRIORITY                                                           " +
	                                      $"       , T1.ROUTE_NUMBER                                                       " +
	                                      $"       , T1.ORDER_LINE_NO                                                      " +
	                                      $"       , T1.SHIFT                                                              " +
                                          $"    FROM SEA_MFG.DBO.DEMAND_MSTR    T1                                         " +
                                          $"   WHERE T1.ORDER_DATE  BETWEEN '{strTime}' AND '{endTime}'                    " +
                                          $"     AND T1.RESOURCE_NO LIKE '%{RESOURCE_NO}%'                               " +
                                          $"   GROUP BY T1.ORDER_NO                                                        " +
                                          $"       , T1.LOT                                                                " +
                                          $"       , T1.ORDER_TYPE                                                         " +
                                          $"       , T1.RESOURCE_NO                                                        " +
                                          $"       , T1.DEMAND_STATUS                                                      " +
                                          $"       , T1.ORDER_DATE                                                         " +
                                          $"       , T1.ORDER_QTY                                                          " +
                                          $"       , T1.DATE_SCHED_ORIG                                                    " +
                                          $"       , T1.DATE_SCHED_CURR                                                    " +
                                          $"       , T1.LABEL_NO                                                           " +
                                          $"       , T1.QTY_COMPLETE                                                       " +
                                          $"       , T1.QTY_SCRAPPED                                                       " +
                                          $"       , T1.QTY_HELD                                                           " +
                                          $"       , T1.INPROCESS_CODE                                                     " +
                                          $"       , T1.CUSTOMER_NAME                                                      " +
                                          $"       , T1.DATE_PLAN_START                                                    " +
                                          $"       , T1.LEAD_TIME                                                          " +
                                          $"       , T1.LEAD_TIME_UNIT                                                     " +
                                          $"       , T1.LEAD_TIME_WHERE                                                    " +
                                          $"       , T1.DPOSS_SRC                                                          " +
                                          $"       , T1.DPOSS_PART                                                         " +
                                          $"       , T1.SHIP_TO                                                            " +
                                          $"       , T1.ORDER_SKEY                                                         " +
                                          $"       , T1.ORDER_LINE_SKEY                                                    " +
                                          $"       , T1.HOLD_CODE                                                          " +
                                          $"       , T1.SCH_START_DATE                                                     " +
                                          $"       , T1.SCH_END_DATE                                                       " +
                                          $"       , T1.SCH_METHOD                                                         " +
                                          $"       , T1.SCH_STATUS                                                         " +
                                          $"       , T1.PRIORITY                                                           " +
                                          $"       , T1.ROUTE_NUMBER                                                       " +
                                          $"       , T1.ORDER_LINE_NO                                                      " +
                                          $"       , T1.SHIFT                                                              " +
                                          $"   ORDER BY T1.ORDER_DATE, T1.DATE_SCHED_ORIG, T1.DATE_SCHED_CURR              ";


                DataTable dtDemandMstr = new CoreBusiness().SELECT(GET_DEMAND_MSTR);
                fpMain.Sheets[0].Rows.Count = 0;

                if (dtDemandMstr != null || dtDemandMstr.Rows.Count > 0)
                {
                    fpMain.Sheets[0].Visible = false;
                    fpMain.Sheets[0].Rows.Count = dtDemandMstr.Rows.Count;

                    for (int i = 0; i < dtDemandMstr.Rows.Count; i++)
                    {
                        foreach (DataColumn item in dtDemandMstr.Columns)
                        {
                            fpMain.Sheets[0].SetValue(i, item.ColumnName, dtDemandMstr.Rows[i][item.ColumnName]);
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
        }
    }
}
