using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class TPC실적현황 : BaseForm1
    {

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;
                string s1 = "";
                string s2 = "";
                for (int i = 0; i < _PAN_WHERE.Controls.Count; i++)
                {
                   
                    switch (_PAN_WHERE.Controls[i].GetType().Name)
                    {
     
                        case "Base_FromtoDateTime":

                            Base_FromtoDateTime _FromtoDateTime = _PAN_WHERE.Controls[i] as Base_FromtoDateTime;


                            s1 = _FromtoDateTime.StartValue.ToString("yyyy-MM-dd HH:mm");
                            s2 = _FromtoDateTime.EndValue.ToString("yyyy-MM-dd HH:mm");
                            break;

                    }



                }

            
                string str = $@"DECLARE @cols AS NVARCHAR(MAX),
        @query AS NVARCHAR(MAX),
        @startDate AS DATE,
        @endDate AS DATE,
        @opcType AS VARCHAR(100);

-- 날짜 및 OPC_TYPE 조건 설정
SET @startDate = '{s1}';
SET @endDate = '{s2}';
SET @opcType = 'MES_FSt040_Data_Save'; -- 예를 들어 'Type1'으로 설정

-- 피벗할 열 이름을 동적으로 생성
SELECT @cols = STUFF((
    SELECT DISTINCT ',' + QUOTENAME(NAME)
    FROM [dbo].[OPC_MST]
    WHERE READ_DATE BETWEEN @startDate AND @endDate
      AND OPC_TYPE = @opcType
	 
    FOR XML PATH(''), TYPE
).value('.', 'NVARCHAR(MAX)'), 1, 1, '');

-- 생성된 열 목록 확인
PRINT 'Columns: ' + @cols;

-- 동적 SQL 쿼리 생성
SET @query = '
SELECT READ_DATE, ' + @cols + '
FROM 
(
    SELECT 
        NAME,
        READ_DATE,
        VALUE
    FROM [dbo].[OPC_MST]
    WHERE READ_DATE BETWEEN @startDate AND @endDate
      AND OPC_TYPE = @opcType 
) AS SourceData
PIVOT
(
    MAX(VALUE)
    FOR NAME IN (' + @cols + ')
) AS PivotTable
ORDER BY READ_DATE;';

-- 생성된 쿼리 확인
PRINT 'Query: ' + @query;

-- 동적 SQL 실행
EXEC sp_executesql @query, N'@startDate DATE, @endDate DATE, @opcType VARCHAR(100)', @startDate, @endDate, @opcType;";

                //StringBuilder sb = new StringBuilder();
                //Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                //string sql = str + sb.ToString();

                DataTable _DataTable = new CoreBusiness().SELECT(str);
                fpMain.Sheets[0].Rows.Count = 0;
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
