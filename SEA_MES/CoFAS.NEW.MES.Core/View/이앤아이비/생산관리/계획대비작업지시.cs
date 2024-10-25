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
    public partial class 계획대비작업지시 : DoubleBaseForm1
    {
        public 계획대비작업지시()
        {
            InitializeComponent();
            fpMain._EditorNotifyEventHandler -= fpMain_ButtonClicked;
            fpMain._EditorNotifyEventHandler += fpMain_ButtonClicked;
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
        public override void _SaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                for (int i = fpSub.Sheets[0].RowCount -1; i >= 0; i--)

                {
                    if (fpSub.Sheets[0].GetValue(i, "CK").ToString() != "True")
                    {
                        FpSpreadManager.SpreadRowRemove(fpSub, 0, i);
                    }
                }

                if (Function.Core._SaveButtonClicked(fpSub))
                {
                    
                    if (fpSub.Sheets[0].Rows.Count > 0)
                        SubSave_InputData();

                    if (fpMain.Sheets[0].Rows.Count > 0 || fpSub.Sheets[0].Rows.Count > 0)
                    {
                        CustomMsg.ShowMessage("저장되었습니다.");
                        DisplayMessage("저장 되었습니다.");
                    }

                }
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowExceptionMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string select = $@"select
                                  B.OUT_CODE AS STOCK_MST_CODE
                                 ,B.ID AS STOCK_MST_ID
                                 ,A.ID  AS ID
                                 ,A.PLAN_DATE AS PLAN_DATE
                                 ,A.OUT_CODE AS PLAN_CODE
                                 ,B.NAME AS STOCK_MST_NAME
                                 ,B.STANDARD AS STOCK_MST_STANDARD
                                 ,B.TYPE AS STOCK_MST_TYPE
                                 ,B.PROCESS_ID AS PROCESS_ID
                                 ,A.PLAN_QTY AS PLAN_QTY
                                 ,ISNULL(C.INSTRUCT_QTY,0.00) AS INSTRUCT_QTY
                                 ,B.OUT_SCHEDULE
                                 ,B.IN_SCHEDULE
								 ,A.SORT AS SORT
								 ,A.COMMENT AS COMMENT
								 ,A.COMPLETE_YN AS COMPLETE_YN
								 ,A.USE_YN AS USE_YN
                                 ,B.QTY
                                 ,D.PRODUCTION_PLAN_ID
                                 from [dbo].[PRODUCTION_PLAN] A
                                 INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID
                                 LEFT JOIN 
                                 (
                                 SELECT SUM(INSTRUCT_QTY) AS INSTRUCT_QTY
                                 ,PRODUCTION_PLAN_ID
                                 ,STOCK_MST_ID
                                 FROM PRODUCTION_INSTRUCT
                                 GROUP BY PRODUCTION_PLAN_ID,
                                 		 STOCK_MST_ID
                                 ) C ON A.ID = C.PRODUCTION_PLAN_ID AND A.STOCK_MST_ID = C.STOCK_MST_ID
                                 LEFT JOIN 
                                 (
                                   SELECT PRODUCTION_PLAN_ID
                                     FROM PRODUCTION_INSTRUCT A
	                                 INNER JOIN PROCESS C ON A.PROCESS_ID = C.ID
	                                 WHERE 1=1           
                                     
	                                 GROUP BY PRODUCTION_PLAN_ID
                                     ) D ON A.ID = D.PRODUCTION_PLAN_ID 
                                 where 1=1 {this._pMenuSettingEntity.BASE_ORDER}   ";

                string where = @"AND A.USE_YN = 'Y'";

                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                where += sb.ToString();
               
                string sql = select + where;

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpMain);

                for (int i = 0; i < _DataTable.Rows.Count; i++)
                {
                    foreach (DataColumn item in _DataTable.Columns)
                    {                 
                        if (item.ColumnName.Contains("PRODUCTION_PLAN_ID"))
                        {
                            if (fpMain.Sheets[0].Rows[i].BackColor != Color.White)
                            {
                                if (_DataTable.Rows[i][item.ColumnName].ToString() != "")
                                {
                                    fpMain.Sheets[0].Rows[i].BackColor = Color.LightBlue;

                                }
                            }
                        }
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
        public override void SubFind_DisplayData()
        {
            try
            {
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
        public override void SubSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Focus();

                

                bool _Error = new CoreBusiness().BaseForm1_A10(this._pMenuSettingEntity,fpSub,"PRODUCTION_INSTRUCT");
                if (!_Error)
                {

                    
                    fpSub.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
                  
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


            }
            catch (Exception _Exception)
            {
                CustomMsg.ShowExceptionMessage(_Exception.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        private void fpMain_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            try
            {
                string where = string.Empty;

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "CK").ToString() == "True")
                    {
                        if (where == string.Empty)
                        {
                            where += "'" + fpMain.Sheets[0].GetValue(i, "ID").ToString() + "'";
                        }
                        else
                        {
                            where += ",'" + fpMain.Sheets[0].GetValue(i, "ID").ToString() + "'";
                        }
                    }
                }

                if (where == string.Empty)
                {
                    fpSub.Sheets[0].Rows.Count = 0;
                    return;
                }
                string sql = $@"WITH  bom_cte AS (
                                -- Anchor member: 시작점 (최상위 부모)
                                SELECT A.id
                               , A.STOCK_MST_ID
                               , A.SUB_STOCK_MST_ID
                               , A.PRODUCTION_QTY
                               , CAST(1 * B.PLAN_QTY AS decimal(18, 2)) AS CONSUME_QTY
                               , A.SEQ AS level
                               , B.ID as PRODUCTION_PLAN_ID
                                FROM bom A
                                INNER JOIN PRODUCTION_PLAN B ON A.STOCK_MST_ID = B.STOCK_MST_ID
                                WHERE 1=1
                                AND A.STOCK_MST_ID = A.SUB_STOCK_MST_ID
                                AND B.ID in ({where})
                            
                                UNION ALL
                            
                                -- Recursive member: 재귀적으로 하위 구성품을 조회
                                SELECT b.id
                               , b.STOCK_MST_ID
                               , b.SUB_STOCK_MST_ID
                               , b.PRODUCTION_QTY  
                               , CAST(a.CONSUME_QTY* b.CONSUME_QTY AS decimal(18, 2)) as CONSUME_QTY 
                               , a.level + 1
                               , CAST(PRODUCTION_PLAN_ID as bigint) as PRODUCTION_PLAN_ID
                                FROM bom_cte a
                                INNER JOIN bom b ON a.SUB_STOCK_MST_ID = b.STOCK_MST_ID 
                            where b.STOCK_MST_ID != b.SUB_STOCK_MST_ID
                            )
                            
                            -- CTE 결과를 출력
                              SELECT 
                                  0 AS ID
                                 ,A.PRODUCTION_PLAN_ID AS PRODUCTION_PLAN_ID
                                 ,GETDATE() AS INSTRUCT_DATE
                                 ,''        AS OUT_CODE
                                 ,a.SUB_STOCK_MST_ID AS STOCK_MST_ID
                                 ,b.OUT_CODE         AS STOCK_MST_OUT_CODE
                                 ,b.NAME             AS STOCK_MST_NAME
                                 ,b.STANDARD         AS STOCK_MST_STANDARD
                                 ,d.code_name        AS STOCK_MST_TYPE
                                 ,PROCESS_ID
                                 ,SUM(a.CONSUME_QTY) AS INSTRUCT_QTY
                                 ,ROW_NUMBER() OVER (ORDER BY [PROCESS_ID])  AS SORT
                                 ,'' AS COMMENT
                                 ,'N'AS COMPLETE_YN
                                 ,'' AS USE_YN
                                 ,'' AS REG_USER
                                 ,'' AS REG_DATE
                                 ,'' AS UP_USER
                                 ,'' AS UP_DATE
                                 ,b.QTY
                                 ,b.IN_SCHEDULE
                                 ,b.OUT_SCHEDULE
                                  FROM bom_cte a
                                  inner join STOCK_MST b on a.SUB_STOCK_MST_ID = b.id
                                  inner join PROCESS c on b.PROCESS_ID = c.id
                                  inner join Code_Mst d on d.code = b.type
                                  where 1=1 
                                    and b.TYPE like '%SD04%'
                                    {this._pMenuSettingEntity.BASE_WHERE}      
                                  GROUP BY 
                                   a.SUB_STOCK_MST_ID
                                  ,b.OUT_CODE
                                  ,b.TYPE
								  ,a.PRODUCTION_PLAN_ID
								  ,b.QTY
								  ,b.IN_SCHEDULE
								  ,b.OUT_SCHEDULE
                                  ,b.PROCESS_ID
                                  ,b.OUT_CODE 
                                  ,b.NAME
                                  ,b.STANDARD 
                                  ,d.code_name
                          ORDER BY PROCESS_ID";

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpSub);

                for (int x = 0; x < fpSub.Sheets[0].Rows.Count; x++)
                {
                    if (fpSub.Sheets[0].RowHeader.Cells[x, 0].Text != "합계")
                    {
                        fpSub.Sheets[0].RowHeader.Cells[x, 0].Text = "입력";
                        fpSub.Sheets[0].SetValue(x, "USE_YN", "Y");
                        fpSub.Sheets[0].SetValue(x, "UP_USER", MainForm.UserEntity.user_account);
                        fpSub.Sheets[0].SetValue(x, "UP_DATE", DateTime.Now);
                        fpSub.Sheets[0].SetValue(x, "REG_USER", MainForm.UserEntity.user_account);
                        fpSub.Sheets[0].SetValue(x, "REG_DATE", DateTime.Now);
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
    }
}
