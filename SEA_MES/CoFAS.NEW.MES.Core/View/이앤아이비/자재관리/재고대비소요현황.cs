using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 재고대비소요현황 : BaseForm1
    {
        public 재고대비소요현황()
        {
            InitializeComponent();
           
        }

       
  
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string sql = $@"WITH  bom_cte AS (
                                -- Anchor member: 시작점 (최상위 부모)
                SELECT A.id
                                     , A.STOCK_MST_ID
                                     , A.STOCK_MST_ID AS SUB_STOCK_MST_ID
                                     , CAST(1 AS decimal(18, 2)) AS PRODUCTION_QTY
                                     , CAST(A.PLAN_QTY - B.QTY AS decimal(18, 2)) AS CONSUME_QTY
                                     , 0 AS level        
                                     ,CAST(A.ID AS int) as PRODUCTION_PLAN_ID
                                      FROM PRODUCTION_PLAN A
									  INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID
                                      WHERE 1=1 
									  AND (A.PLAN_QTY - B.QTY) > 0 
									  AND a.USE_YN = 'Y'  
									  AND a.COMPLETE_YN = 'N'
                            
                                UNION ALL
                            
                                -- Recursive member: 재귀적으로 하위 구성품을 조회
                                SELECT b.id
                               , b.STOCK_MST_ID
                               , b.SUB_STOCK_MST_ID
                               , b.PRODUCTION_QTY  
                               , CAST((a.CONSUME_QTY* b.CONSUME_QTY) - C.QTY AS decimal(18, 2)) as CONSUME_QTY 
                               , a.level + 1        
                               , CAST(0 AS int) AS  PRODUCTION_PLAN_ID
                                FROM bom_cte a
                                INNER JOIN bom b ON a.SUB_STOCK_MST_ID = b.STOCK_MST_ID
								INNER JOIN STOCK_MST C ON B.STOCK_MST_ID = C.ID
                            where 1=1 
							AND ((a.CONSUME_QTY * b.CONSUME_QTY) - C.QTY) > 0
							AND B.STOCK_MST_ID != B.SUB_STOCK_MST_ID 
							AND B.CONSUME_QTY !=0 
                            )

                            -- CTE 결과를 출력
                                 SELECT
                                 'false'                                     AS 'CK'                
                                 ,0                                          AS ID                  
                                 ,GETDATE()                                  AS INSTRUCT_DATE
                                 ,''                                         AS OUT_CODE
                                 ,A.SUB_STOCK_MST_ID                         AS 'STOCK_MST_ID'
                                 ,B.OUT_CODE                                 AS 'B.OUT_CODE'        
                                 ,B.NAME                                     AS 'B.NAME'            
                                 ,B.STANDARD                                 AS 'B.STANDARD'        
                                 ,B.TYPE                                     AS 'B.TYPE'
                                 ,B.PRICE                                    AS 'COST'
                                 ,B.PROCESS_ID                               AS 'B.PROCESS_ID'      
                                 ,A.CONSUME_QTY                              AS PLAN_QTY
                                 ,CASE
                                 WHEN (A.CONSUME_QTY  - B.QTY - IN_SCHEDULE) < 0 THEN 0
                                 ELSE (A.CONSUME_QTY  - B.QTY - IN_SCHEDULE)
                                 END AS INSTRUCT_QTY
                         ,ROW_NUMBER() OVER (ORDER BY [PROCESS_ID])  AS SORT
                                 ,''                                         AS COMMENT
                                 ,'N'                                        AS COMPLETE_YN
                                 ,'Y'                                        AS USE_YN
                                 ,'{ MainForm.UserEntity.user_account}'      AS REG_USER
                                 ,GETDATE()                                  AS REG_DATE
                                 ,'{ MainForm.UserEntity.user_account}'      AS UP_USER
                                 ,GETDATE()                                  AS UP_DATE
                                 ,B.QTY
                                 ,B.IN_SCHEDULE
                                 ,B.OUT_SCHEDULE                           
                                FROM 
								  (
								          SELECT SUB_STOCK_MST_ID
								         ,SUM(CONSUME_QTY) AS CONSUME_QTY						               
                                    FROM 
									(
									 SELECT DISTINCT *						               
                                    FROM bom_cte	
									) A
									GROUP BY SUB_STOCK_MST_ID
								  ) A
                                  inner join STOCK_MST B on A.SUB_STOCK_MST_ID = B.id                                                   
                                  where 1=1 
                          AND B.TYPE LIKE '%SD03%'
                      	
                          AND (A.CONSUME_QTY  - B.QTY - B.IN_SCHEDULE) > 0";

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                 sql += sb.ToString();

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpMain);

                for (int x = 0; x < fpMain.Sheets[0].Rows.Count; x++)
                {
                    if (fpMain.Sheets[0].RowHeader.Cells[x, 0].Text != "합계")
                    {
                        fpMain.Sheets[0].RowHeader.Cells[x, 0].Text = "입력";

                    }

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
        public override void _SaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                for (int i = fpMain.Sheets[0].RowCount - 1; i >= 0; i--)

                {
                    if (fpMain.Sheets[0].GetValue(i, "CK").ToString() != "True")
                    {
                        FpSpreadManager.SpreadRowRemove(fpMain, 0, i);
                    }
                }

                if (Function.Core._SaveButtonClicked(fpMain))
                {

                    if (fpMain.Sheets[0].Rows.Count > 0)
                        MainSave_InputData();
                }
                
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public override void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Focus();

                DataTable _DataTable = new DataTable();

                for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                {
                    if (fpMain.Sheets[0].RowHeader.Cells[i, 0].Text == "입력")
                    {
                        string str_select = $@"select 
                                    * from ORDER_MST
                                    where order_type = 'SD10001'
                                    and ORDER_DATE BETWEEN CONVERT(CHAR(10), GETDATE(), 23)
                                                      and  CONVERT(CHAR(10), GETDATE(), 23)
                                    and COMPANY_ID = '{fpMain.Sheets[0].GetValue(i, "COMPANY_ID").ToString()}'";

                        string sql_select = str_select;
                        _DataTable = new CoreBusiness().SELECT(sql_select);

                        if (_DataTable.Rows.Count > 0)
                        {
                            bool _Error_detail = new SI_Business().STOCK_ORDER_MST_DETAIL_A20(fpMain, MainForm.UserEntity.user_account,i);
                        }

                        else
                        {
                            bool _Error = new SI_Business().STOCK_ORDER_MST_DETAIL_A10(fpMain, MainForm.UserEntity.user_account,i);


                            if (!_Error)
                            {
                                bool _Error_detail = new SI_Business().STOCK_ORDER_MST_DETAIL_A20(fpMain, MainForm.UserEntity.user_account, i);

                                if (!_Error_detail)
                                {
                                    
                                }
                            }
                        }
                    }
                }

                CustomMsg.ShowMessage("저장되었습니다.");
                DisplayMessage("저장 되었습니다.");

                fpMain.Sheets[0].Rows.Count = 0;
                MainFind_DisplayData();
            }
            catch (Exception pExcption)
            {
                int start = pExcption.Message.IndexOf(" (")+1;
                int end = pExcption.Message.IndexOf(")", start)+1;
                string constraintName = pExcption.Message.Substring(start, end - start);
                CustomMsg.ShowExceptionMessage($"중복 값을 입력 하실수 없습니다. 중복값 {constraintName} 입니다.", "Error", MessageBoxButtons.OK);
            }
            finally
            {
                DevExpressManager.SetCursor(this, Cursors.Default);
            }
        }


    }
}
