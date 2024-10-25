using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class EQUIPMENT_OPERATION_STATUS : DoubleBaseForm1
    {
        public EQUIPMENT_OPERATION_STATUS()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
            Activated += new EventHandler(Form_Activated);
            FormClosing += new FormClosingEventHandler(Form_Closing);

          
        }

        private void DoubleBaseForm2_Load(object sender, EventArgs e)
        {
            splitContainer2.Orientation = Orientation.Vertical;

            splitContainer2.SplitterDistance = 500;
        }
        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            try
            {


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

        public void SubFind_DisplayData(int id, DateTime dateTime)
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;


                string str = $@"SET ANSI_WARNINGS OFF
　　　　　　　　　　　　　　　　SET ARITHIGNORE ON
　　　　　　　　　　　　　　　　SET ARITHABORT OFF
　　　　　　　　　　　　　　　　SELECT A.NAME 
　　　　　　　　　　　　　　　　,ISNULL(PASS,0) AS PASS
　　　　　　　　　　　　　　　　,ISNULL(FAIL,0) AS FAIL 
　　　　　　　　　　　　　　　　,ISNULL(PASS,0) + ISNULL(FAIL,0) AS TOTAL
　　　　　　　　　　　　　　　　,ISNULL((FAIL/(PASS + FAIL))*100,0) AS BAD
                                ,ISNULL(전체고장시간/고장건수,0)/60/60	 AS MTTR 
                                ,ISNULL((60*60*24)/고장건수,0)/60/60	  AS MTTF 
                                ,ISNULL(전체고장시간/고장건수,0)+ISNULL((60*60*24)/고장건수,0)	 AS MTBF 
　　　　　　　　　　　　　　　　FROM [dbo].[EQUIPMENT] A
　　　　　　　　　　　　　　　　LEFT JOIN 
                               (
                               select Convert(Decimal(18,2),COUNT(NAME)) AS PASS , NAME
                               from [dbo].[OPC_MST] 
                               where 1=1
                               AND READ_DATE BETWEEN '{dateTime.ToString("yyyy-MM-dd") + " 08:30"}' AND '{dateTime.AddDays(1).ToString("yyyy-MM-dd") + " 08:30"}'
                               AND NAME LIKE '%_Pass%'
                               GROUP by NAME
                               ) B ON A.OUT_CODE+'_Pass' = B.NAME
                               LEFT JOIN
                               (
                               select Convert(Decimal(18,2),COUNT(NAME)) AS Fail , NAME
                               from [dbo].[OPC_MST] 
                               where 1=1
                               AND READ_DATE BETWEEN '{dateTime.ToString("yyyy-MM-dd") + " 08:30"}' AND '{dateTime.AddDays(1).ToString("yyyy-MM-dd") + " 08:30"}'
                               AND NAME LIKE '%_Fail%'
                               GROUP by NAME
                               ) C ON A.OUT_CODE+'_Fail' = C.NAME  
                               LEFT JOIN
                               (
                                SELECT Convert(Decimal(18,2),SUM(STOP_TIME))AS 전체고장시간 
                                      ,Convert(Decimal(18,2),COUNT(ID)) 고장건수
                               	   ,EQUIPMENT_ID
                                FROM [dbo].[EQUIPMENT_STOP]
                                WHERE 1=1
                                AND USE_YN = 'Y'
                                AND EQUIPMENT_ID != 0
                                AND START_TIME BETWEEN '{dateTime.ToString("yyyy-MM-dd") + " 08:30"}' AND '{dateTime.AddDays(1).ToString("yyyy-MM-dd") + " 08:30"}'
                                GROUP BY EQUIPMENT_ID
                               ) D ON A.ID = D.EQUIPMENT_ID
　　　　　　　　　　　　　　　　WHERE 1=1 and A.ID = {id}";
                DataTable _DataTable = new CoreBusiness().SELECT(str);

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

        public override void fpMain_CellClick(object sender, CellClickEventArgs e)
        {
            try
            {

                string pHeaderLabel = fpMain.Sheets[0].RowHeader.Cells[e.Row, 0].Text;
                if (pHeaderLabel != "입력" && pHeaderLabel != "합계")
                {
                    int id =  Convert.ToInt32(fpMain.Sheets[0].GetValue(e.Row, "ID"));
                    BaseCalendarPopupBox baseMonthCalendarPopupBox = new BaseCalendarPopupBox();
                    if (baseMonthCalendarPopupBox.ShowDialog() == DialogResult.OK)
                    {
                        if (baseMonthCalendarPopupBox._SelectionStart != null)
                        {
                            SubFind_DisplayData(id, baseMonthCalendarPopupBox._SelectionStart.Value);
                        }
                    }
                }

            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
    }
}
