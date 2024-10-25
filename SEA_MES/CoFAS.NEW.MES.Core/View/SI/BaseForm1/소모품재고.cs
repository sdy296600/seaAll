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
    public partial class 소모품재고 : BaseForm1
    {

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT A.RESOURCE_NO AS 'A.RESOURCE_NO',
                                        A.재고수량 AS 'A.입고수량',
                               isnull(B.재고수량,0) AS 'A.사용수량' ,
                                A.재고수량 -isnull(B.재고수량,0) AS 'A.재고수량'
							
                                FROM 
                                (SELECT 
                                       A.RESOURCE_NO AS RESOURCE_NO
                                      ,A.[RESOURCE_TYPE]
                                	  ,sum([USE_QTY]) AS 재고수량 
									
                                  FROM [HS_MES].[dbo].[CONSUMABLE_MST]　A
								  INNER JOIN [dbo].[CONSUMABLE] B ON A.RESOURCE_NO = B.ID
                                  WHERE TYPE = 'CD19001'
								   AND A.USE_YN ='Y'
								  group by A.RESOURCE_NO, A.RESOURCE_TYPE
								  ) AS A
                                   left outer JOIN  
                                  (SELECT 
                                      RESOURCE_NO,
                                	  SUM([USE_QTY]) AS 재고수량      
                                  FROM [HS_MES].[dbo].[CONSUMABLE_MST]
                                  WHERE TYPE = 'CD19002'
								  AND USE_YN ='Y'
                                  GROUP BY RESOURCE_NO) AS B
                                  ON A.RESOURCE_NO = B.RESOURCE_NO
                                  WHERE 1=1
								
								";

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
