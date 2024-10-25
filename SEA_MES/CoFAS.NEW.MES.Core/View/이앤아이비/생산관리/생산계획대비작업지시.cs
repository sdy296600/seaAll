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
    public partial class 생산계획대비작업지시 : BaseForm1
    {
        public 생산계획대비작업지시()
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
                                  0                                          AS ID
								 , PRODUCTION_PLAN_ID						 
								                                             AS PRODUCTION_PLAN_ID
                                 ,GETDATE()                                  AS INSTRUCT_DATE
                                 ,''                                         AS OUT_CODE
                                 ,A.SUB_STOCK_MST_ID                         AS 'A.STOCK_MST_ID'
                                 ,B.OUT_CODE                                 AS 'B.OUT_CODE'        
                                 ,B.NAME                                     AS 'B.NAME'            
                                 ,B.STANDARD                                 AS 'B.STANDARD'        
                                 ,B.TYPE                                     AS 'B.TYPE'       
                                 ,B.PROCESS_ID		                         AS 'B.PROCESS_ID'		
                                 ,A.CONSUME_QTY                              AS PLAN_QTY
                                 ,(A.CONSUME_QTY  - B.QTY - IN_SCHEDULE)     AS INSTRUCT_QTY
                                 ,GETDATE()                                  AS DEMAND_DATE
                                 ,''                                         AS MATERIAL
                                 ,''                                         AS COMPANY_ID
                                 ,''                                         AS COMPANY_NAME
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
                                 ,D.생산가능수량
                                  FROM 
								  (
								          SELECT SUB_STOCK_MST_ID
								         ,SUM(CONSUME_QTY) AS CONSUME_QTY
										 ,MAX(PRODUCTION_PLAN_ID) AS PRODUCTION_PLAN_ID						
                                    FROM 
									(
									 SELECT  *						               
                                    FROM bom_cte					
									) A
									GROUP BY SUB_STOCK_MST_ID,PRODUCTION_PLAN_ID
								  ) A
                                  inner join STOCK_MST B on A.SUB_STOCK_MST_ID = B.id AND B.TYPE LIKE '%SD04%'
                                  inner join PROCESS C on B.PROCESS_ID = C.id 
                                  inner join 
                                  (
                                  select CONVERT(INT,ISNULL(MIN(QTY/CONSUME_QTY),0)) AS 생산가능수량
                                  , A.STOCK_MST_ID
                                  from BOM A 
                                  INNER JOIN STOCK_MST B ON A.SUB_STOCK_MST_ID = B.ID
                                  where 1=1
                                  AND A.STOCK_MST_ID != A.SUB_STOCK_MST_ID
                                  GROUP BY A.STOCK_MST_ID
                                  ) D ON A.SUB_STOCK_MST_ID = D.STOCK_MST_ID
                                  where 1=1        
                                        AND(A.CONSUME_QTY - B.QTY - IN_SCHEDULE) > 0		
                                       {this._pMenuSettingEntity.BASE_WHERE}";
                //AND(A.CONSUME_QTY - B.QTY - IN_SCHEDULE) > 0
                //  AND B.TYPE like '%SD04%'AND (A.CONSUME_QTY  - B.QTY - IN_SCHEDULE) > 0
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

                //for (int i = fpMain.Sheets[0].RowCount - 1; i >= 0; i--)
                //{
                //    decimal 지시수량     = Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "INSTRUCT_QTY"));
                //    decimal 생산가능수량 = Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "생산가능수량"));
                //    if (지시수량 <= 0)
                //    {
                //        CustomMsg.ShowMessage("지시수량을 입력해주세요.");
                //        return;

                //    }
                //    if (생산가능수량 < 지시수량)
                //    {
                //        CustomMsg.ShowMessage("생산 가능수량이 부족합니다.");
                //        return;
                //    }
                //    //if (fpMain.Sheets[0].GetValue(i, "CK").ToString() != "True")
                //    //{
                //    //    FpSpreadManager.SpreadRowRemove(fpMain, 0, i);
                //    //}
                //    //else
                //    //{
                //    //    decimal 지시수량     = Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "INSTRUCT_QTY"));
                //    //    decimal 생산가능수량 = Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "생산가능수량"));
                //    //    if(지시수량  <= 0)
                //    //    {
                //    //        CustomMsg.ShowMessage("지시수량을 입력해주세요.");
                //    //        return;

                //    //    }
                //    //    if(생산가능수량 < 지시수량)
                //    //    {
                //    //        CustomMsg.ShowMessage("생산 가능수량이 부족합니다.");
                //    //        return;
                //    //    }
                //    //}
                //}

                fpMain.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(this._pMenuSettingEntity,fpMain,this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);
                if (!_Error)
                {
                    CustomMsg.ShowMessage("저장되었습니다.");
                    DisplayMessage("저장 되었습니다.");

                    fpMain.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
                }
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
