using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class WORKING_CONDITIONS_STATUS : BaseForm1
    {

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"select B.code_name as 공정
                                      ,A.code_name as 대분류
                                      ,A.code_etc1 as 소분류
                                      ,C.VALUE     as 값
                                      ,C.READ_DATE as 수집시간
                                  from [dbo].[Code_Mst] A
                                  INNER JOIN [dbo].[Code_Mst] B ON A.code_type = B.code
                                   LEFT JOIN 
                                   (
                                    SELECT A.*
                                                                                   FROM [dbo].[OPC_MST] A
                                                                                   INNER JOIN 
                                                                                   (
                                                                                   SELECT NAME ,MAX(READ_DATE) AS READ_DATE
                                                                                   FROM [dbo].[OPC_MST]
                                                                                  GROUP BY NAME 
                                                                                   ) B ON A.NAME = B.NAME AND  A.READ_DATE  = B.READ_DATE
                                   ) C ON  A.code_etc1 = C.NAME
                                  where 1=1
                                  and A.code_type like '%PR%'
                                  and A.code_type != 'PR00'
                                  ORDER BY A.code";
                DataTable _DataTable = new CoreBusiness().SELECT(str);

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
