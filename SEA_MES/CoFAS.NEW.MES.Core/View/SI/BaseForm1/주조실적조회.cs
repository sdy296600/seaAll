using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.DataAccess.Native.Sql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 주조실적조회 : BaseForm1
    {
        DataBase_Class myDb;
        DataBase_Class msDb;

        public 주조실적조회()
        {
            InitializeComponent();

        }


        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                DataTable _DataTable = new CoreBusiness().BaseForm1_R10(_PAN_WHERE, this._pMenuSettingEntity);
        
                string sql = "SELECT * FROM WORK_PERFORMANCE";

                DataSet ds = myDb.GetAllData(sql);
                DataTable dt = ds.Tables[0];

                sql = "SELECT * FROM BAD_PERFORMANCE";
                DataSet ds2 = msDb.GetAllData(sql);
                DataTable dt2 = ds2.Tables[0];
                DataTable resultTable = new DataTable();
               

                // dt1의 모든 열을 resultTable에 추가
                foreach (DataColumn column in _DataTable.Columns)
                {
                    resultTable.Columns.Add(column.ColumnName, column.DataType);
                }
                resultTable.Columns.Add("WORK_ERRCOUNT", typeof(string));
                resultTable.Columns.Add("BAD_QTY", typeof(string));



                // LINQ로 조인 수행하여 모든 열 가져오기
                var joinedData = from row1 in _DataTable.AsEnumerable()
                                 join row2 in dt.AsEnumerable()
                                 on row1.Field<long?>("ID") equals row2.Field<long?>("WORK_PERFORMANCE_ID") into gj2 // 'into'를 사용하여 그룹을 생성
                                 from subRow2 in gj2.DefaultIfEmpty() // row2가 null인 경우에 대비
                                 join row3 in dt2.AsEnumerable()
                                 on row1.Field<long?>("ID") equals row3.Field<long?>("WORK_PERFORMANCE_ID") into gj3 // 'into'를 사용하여 그룹을 생성
                                 from subRow3 in gj3.DefaultIfEmpty() // row3가 null인 경우에 대비
                                                                      // 여기를 통해 row1의 LOT_NO 및 RESOURCE_NO를 subRow2 및 subRow3와 비교
                                 where (subRow2 == null ||
                                        (row1.Field<string>("LOT_NO") == subRow2.Field<string>("LOT_NO") &&
                                         row1.Field<string>("RESOURCE_NO") == subRow2.Field<string>("RESOURCE_NO"))) &&
                                       (subRow3 == null ||
                                        (row1.Field<string>("LOT_NO") == subRow3.Field<string>("LOT_NO") &&
                                         row1.Field<string>("RESOURCE_NO") == subRow3.Field<string>("RESOURCE_NO")))
                                 group new { subRow2, subRow3 } by new
                                 {
                                     Row1Data = row1.ItemArray,
                                     Column2 = subRow2 != null ? subRow2.Field<string>("WORK_ERRCOUNT") : null // null 처리
                                 } into groupedData
                                 select new
                                 {
                                     Row1Data = groupedData.Key.Row1Data,
                                     Column2 = groupedData.Key.Column2,
                                     SumColumnFromRow3 = groupedData.Sum(x => x.subRow3 != null ? x.subRow3.Field<decimal>("BAD_QTY") : 0) // null 처리
                                 };


                // 결과를 resultTable에 추가
                foreach (var item in joinedData)
                {
                    var newRow = resultTable.NewRow();

                    // row1의 모든 열 추가
                    for (int i = 0; i < item.Row1Data.Length; i++)
                    {
                        newRow[i] = item.Row1Data[i];
                    }

                    // row2 및 row3의 특정 열 추가
                    newRow["WORK_ERRCOUNT"] = item.Column2;
                    newRow["BAD_QTY"] = item.SumColumnFromRow3; // 합계 추가

                    resultTable.Rows.Add(newRow);
                }
                Function.Core.DisplayData_Set(resultTable, fpMain);
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

        private void 주조실적조회_Load(object sender, EventArgs e)
        {
            string mysqlconn = "Server=10.10.10.216;Database=hansoldms;UID=coever;PWD=coever119!";
            string mssqlconn = $"Server=10.10.10.180;Database=HS_MES;UID=hansol_mes;PWD=Hansol123!@#";

            myDb = new DataBase_Class(new MY_DB(mysqlconn));
            msDb = new DataBase_Class(new MS_DB(mssqlconn));
        }
    }
}
