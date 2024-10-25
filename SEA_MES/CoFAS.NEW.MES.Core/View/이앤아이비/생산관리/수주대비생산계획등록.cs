using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 수주대비생산계획등록 : DoubleBaseForm1
    {
        public 수주대비생산계획등록()
        {
            InitializeComponent();
            fpMain._EditorNotifyEventHandler -= fpMain_ButtonClicked;
            fpMain._EditorNotifyEventHandler += fpMain_ButtonClicked;
        }
        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            if (_Mst_Id != string.Empty)
            {

            }

        }
        public override void _DeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (fpSub.Sheets[0].ActiveRowIndex != -1)
                {
                    DialogResult _DialogResult1 = CustomMsg.ShowMessage("삭제하시겠습니까?", "Question", MessageBoxButtons.YesNo);
                    if (_DialogResult1 == DialogResult.Yes)
                    {
                        if (fpSub.Sheets[0].RowHeader.Cells[fpSub.Sheets[0].ActiveRowIndex, 0].Text == "입력")
                        {
                            FpSpreadManager.SpreadRowRemove(fpSub, 0, fpMain.Sheets[0].ActiveRowIndex);
                        }
                        else
                        {
                            fpSub.Sheets[0].RowHeader.Cells[fpMain.Sheets[0].ActiveRowIndex, 0].Text = "삭제";
                            fpSub.Sheets[0].SetValue(fpMain.Sheets[0].ActiveRowIndex, "USE_YN", "N");

                            MainDelete_InputData();
                        }
                    }
                }
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
                if (Function.Core._SaveButtonClicked(fpSub))
                {
                    
                    if (fpSub.Sheets[0].Rows.Count > 0)
                        SubSave_InputData();

                    if (fpSub.Sheets[0].Rows.Count > 0)
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

                string str = $@"select 'false'            AS   'CK'
                                      ,A.ID				  AS   'ID'				
                                      ,B.OUT_CODE		  AS   'B.OUT_CODE'	
                                      ,A.STOCK_MST_ID	  AS   'STOCK_MST_ID'	
                                      ,C.OUT_CODE		  AS   'C.OUT_CODE'		
                                      ,C.NAME			  AS   'C.NAME'			
                                      ,C.STANDARD		  AS   'C.STANDARD'		
                                      ,D.code_name		  AS   'D.CODE_NAME'		
                                      ,A.SUPPLY_TYPE	  AS   'A.SUPPLY_TYPE'	
                                      ,E.code_name		  AS   'E.CODE_NAME'	
                                      ,A.STOCK_MST_PRICE  AS   'A.STOCK_MST_PRICE'
                                      ,A.ORDER_QTY		  AS   'A.ORDER_QTY'
                                      ,A.ORDER_REMAIN_QTY AS   'A.ORDER_REMAIN_QTY'
                                      ,A.COST			  AS   'A.COST'			
                                      ,F.NAME			  AS   'F.NAME'			
                                      ,A.DEMAND_DATE	  AS   'A.DEMAND_DATE'	
                                      ,A.COMMENT		  AS   'A.COMMENT'		
                                      ,A.INSPECTION_YN	  AS   'A.INSPECTION_YN'	
                                      ,A.COMPLETE_YN	  AS   'A.COMPLETE_YN'	
                                      ,A.USE_YN			  AS   'A.USE_YN'			
                                      ,A.REG_USER		  AS   'A.REG_USER'		
                                      ,A.REG_DATE		  AS   'A.REG_DATE'		
                                      ,A.UP_USER 		  AS   'A.UP_USER' 	
                                      ,A.UP_DATE          AS   'A.UP_DATE'
                                  from ORDER_DETAIL A
                                  INNER JOIN ORDER_MST B ON A.ORDER_MST_ID = B.ID
                                  INNER JOIN STOCK_MST C ON A.STOCK_MST_ID = C.ID
                                  INNER JOIN Code_Mst D ON C.TYPE = D.code
                                  INNER JOIN Code_Mst E ON C.UNIT = E.code
                                  INNER JOIN COMPANY F ON A.COMPANY_ID = F.ID
                                  WHERE 1=1   
                                  AND A.USE_YN = 'Y'
                                  AND A.COMPLETE_YN != 'Y'
                                  AND B.ORDER_TYPE LIKE '%SD09%'
                                  AND A.OUT_TYPE != 'CD20001' 
                                  AND A.PRODUCTION_PLAN_ID IS NULL "; 

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString();

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpMain);

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    for (int x = 0; x < fpMain.Sheets[0].ColumnCount; x++)
                    {
                        if (fpMain.Sheets[0].Columns[x].CellType.GetType() != typeof(ButtonCellType) &&
                            fpMain.Sheets[0].Columns[x].CellType.GetType() != typeof(FileButtonCellType))
                        {
                            fpMain.Sheets[0].Cells[i, x].Locked = false;
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

                for (int i = 0; i < fpSub.Sheets[0].RowCount; i++)
                {
                    if (fpSub.Sheets[0].RowHeader.Cells[i, 0].Text != "합계")
                    {
                        string sql = $@"INSERT INTO [dbo].[PRODUCTION_PLAN]
                                              ([OUT_CODE]
                                              ,[ORDER_DETAIL_ID]
                                              ,[PLAN_DATE]
                                              ,[PLAN_END_DATE]
                                              ,[STOCK_MST_ID]
                                              ,[PLAN_QTY]
                                              ,[INSTRUCT_QTY]
                                              ,[REMAIN_QTY]
                                              ,[SORT]
                                              ,[COMMENT]
                                              ,[COMPLETE_YN]
                                              ,[USE_YN]
                                              ,[REG_USER]
                                              ,[REG_DATE]
                                              ,[UP_USER]
                                              ,[UP_DATE])
                                        VALUES
                                              ('{fpSub.Sheets[0].GetValue(i,"OUT_CODE       ".Trim())}'                                         
                                              ,NULL
                                              ,'{Convert.ToDateTime(fpSub.Sheets[0].GetValue(i,"PLAN_DATE      ".Trim())).ToString("yyyy-MM-dd HH:mm:ss")}'
                                              ,'{Convert.ToDateTime(fpSub.Sheets[0].GetValue(i,"PLAN_END_DATE  ".Trim())).ToString("yyyy-MM-dd HH:mm:ss")}'
                                              ,'{fpSub.Sheets[0].GetValue(i,"STOCK_MST_ID   ".Trim())}'
                                              ,'{fpSub.Sheets[0].GetValue(i,"PLAN_QTY       ".Trim())}'
                                              ,'{fpSub.Sheets[0].GetValue(i,"INSTRUCT_QTY   ".Trim())}'
                                              ,'{fpSub.Sheets[0].GetValue(i,"REMAIN_QTY     ".Trim())}'
                                              ,'{fpSub.Sheets[0].GetValue(i,"SORT           ".Trim())}'
                                              ,'{fpSub.Sheets[0].GetValue(i,"COMMENT        ".Trim())}'
                                              ,'{fpSub.Sheets[0].GetValue(i,"COMPLETE_YN    ".Trim())}'
                                              ,'{fpSub.Sheets[0].GetValue(i,"USE_YN         ".Trim())}'
                                              ,'{fpSub.Sheets[0].GetValue(i,"REG_USER       ".Trim())}'
                                              ,'{Convert.ToDateTime(fpSub.Sheets[0].GetValue(i,"REG_DATE       ".Trim())).ToString("yyyy-MM-dd HH:mm:ss")}'
                                              ,'{fpSub.Sheets[0].GetValue(i,"UP_USER        ".Trim())}'
                                              ,'{Convert.ToDateTime(fpSub.Sheets[0].GetValue(i,"UP_DATE        ".Trim())).ToString("yyyy-MM-dd HH:mm:ss")}');

                                              SELECT SCOPE_IDENTITY() AS NewUserID;";

                        DataTable _DataTable = new CoreBusiness().SELECT(sql);


                        for (int x = 0; x < fpMain.Sheets[0].RowCount; x++)
                        {
                        
                            if (fpMain.Sheets[0].GetValue(x, "CK").ToString() == "True")
                            {
                                if (fpMain.Sheets[0].GetValue(x, "STOCK_MST_ID       ".Trim()).ToString() ==
                                     fpSub.Sheets[0].GetValue(i, "STOCK_MST_ID       ".Trim()).ToString())
                                {
                                    sql = $@"UPDATE [dbo].[ORDER_DETAIL]
                                        SET [PRODUCTION_PLAN_ID] = {_DataTable.Rows[0][0].ToString()}
                                      WHERE ID = '{fpMain.Sheets[0].GetValue(x, "ID       ".Trim())}'";

                                    new CoreBusiness().SELECT(sql);
                                }
                            }
                        }

                    }
                }

                fpSub.Sheets[0].Rows.Count = 0;
                MainFind_DisplayData();

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
      
                string sql = $@"SELECT 
                                0                                           AS ID
                                ,''                                         AS OUT_CODE
                                ,NULL                                       AS ORDER_MST_ID
                                ,NULL                                       AS ORDER_DETAIL_ID
                                ,A.OUT_CODE	                                AS 'A.OUT_CODE'	
                                ,A.NAME		                                AS 'A.NAME'		
                                ,A.STANDARD	                                AS 'A.STANDARD'
                                ,A.TYPE		                                AS 'A.TYPE'		
                                ,A.PROCESS_ID	                            AS 'A.PROCESS_ID'
                                ,A.QTY                                      AS 'A.QTY'         
                                ,A.OUT_SCHEDULE                             AS 'A.OUT_SCHEDULE'
                                ,A.IN_SCHEDULE                              AS 'A.IN_SCHEDULE'
                                ,GETDATE()                                  AS PLAN_DATE
                                ,GETDATE()                                  AS PLAN_END_DATE
                                ,A.ID                                       AS STOCK_MST_ID
                                ,ORDER_QTY                                  AS PLAN_QTY
                                ,0                                          AS INSTRUCT_QTY
                                ,ORDER_QTY                                  AS REMAIN_QTY
                                ,ROW_NUMBER() OVER (ORDER BY [PROCESS_ID])  AS SORT
                                ,''                                         AS COMMENT
                                ,'N'                                        AS COMPLETE_YN
                                ,'Y'                                        AS USE_YN
                                ,''                                         AS REG_USER
                                ,''                                         AS REG_DATE
                                ,''                                         AS UP_USER
                                ,''                                         AS UP_DATE
                                FROM STOCK_MST A
                                INNER JOIN 
                                (
                                SELECT A.STOCK_MST_ID,SUM(ORDER_QTY) AS ORDER_QTY
                                FROM ORDER_DETAIL A
                                WHERE A.ID IN({where})
                                GROUP BY  A.STOCK_MST_ID
                                )B ON A.ID = B.STOCK_MST_ID";

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
