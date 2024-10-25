using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.Spreadsheet;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 제품출고등록 : DoubleBaseForm1
    {
        public 제품출고등록()
        {
            InitializeComponent();

            fpMain._EditorNotifyEventHandler -= new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
            fpMain._EditorNotifyEventHandler += new EditorNotifyEventHandler(pfpSpread_ButtonClicked);

           
        }
     
        public override void MainSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Focus();

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
        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            if (_Mst_Id == null)
            {

                return;
            }
            else
            {
                return;
            }

        }
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"select 
                                B.OUT_CODE		   AS	'B.OUT_CODE'
                               ,B.NAME			   AS	'B.NAME'
                               ,C.NAME			   AS	'C.NAME'
                               ,B.ORDER_DATE	   AS	'B.ORDER_DATE'
                               ,A.ID               AS   'ID'
                               ,A.OUT_CODE		   AS	'A.OUT_CODE'
                               ,A.ORDER_MST_ID	   AS	'ORDER_MST_ID'
                               ,A.OUT_STOCK_DATE  AS	'A.OUT_STOCK_DATE'
                               ,A.OUT_TYPE		   AS	'A.OUT_TYPE'
                               ,A.COMMENT		   AS	'A.COMMENT'
                               ,A.COMPLETE_YN	   AS	'A.COMPLETE_YN'
                               ,A.USE_YN		   AS	'A.USE_YN'
                               ,A.REG_USER		   AS	'A.REG_USER'
                               ,A.REG_DATE		   AS	'A.REG_DATE'
                               ,A.UP_USER         AS	'A.UP_USER'
                               ,A.UP_DATE         AS	'A.UP_DATE'
                               from [dbo].[OUT_STOCK_WAIT_MST] A
                               INNER JOIN [dbo].[ORDER_MST] B　ON A.ORDER_MST_ID = B.ID
                               INNER JOIN [dbo].[COMPANY] C ON B.COMPANY_ID = C.ID
                               WHERE 1=1
                               AND A.USE_YN = 'Y'" + this._pMenuSettingEntity.BASE_WHERE;
                        
                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString();

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpMain);



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
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;

                string sql = $@"SELECT 
                                A.ID                            AS 'ID'
                               ,A.OUT_STOCK_WAIT_MST_ID         AS 'A.OUT_STOCK_WAIT_MST_ID'
                               ,A.ORDER_DETAIL_ID               AS 'A.ORDER_DETAIL_ID'
                               ,B.ORDER_MST_ID                  AS 'B.ORDER_MST_ID'
                               ,B.STOCK_MST_ID                  AS 'B.STOCK_MST_ID'
                               ,C.OUT_CODE						AS 'C.OUT_CODE'
                               ,C.NAME							AS 'C.NAME'
							   ,E.code_name						AS 'E.CODE_NAME'
                               ,C.OUT_SCHEDULE					AS 'C.OUT_SCHEDULE'
                               ,C.IN_SCHEDULE					AS 'C.IN_SCHEDULE'
                               ,C.QTY							AS 'C.QTY'
                               ,B.ORDER_QTY						AS 'B.ORDER_QTY'
                               ,B.ORDER_REMAIN_QTY　　		 　 AS 'B.ORDER_REMAIN_QTY'
                               ,D.WAIT_QTY         　　　　　   AS 'WAIT_QTY'
                               ,A.OUT_QTY              　　　   AS 'A.OUT_QTY'  
                               ,A.COMMENT              　　　   AS 'A.COMMENT'  
                               ,A.COMPLETE_YN          　　　   AS 'A.COMPLETE_YN'
                               ,A.USE_YN               　　　   AS 'A.USE_YN'  
                               ,A.REG_USER             　　　   AS 'A.REG_USER'
                               ,A.REG_DATE             　　　   AS 'A.REG_DATE'
                               ,A.UP_USER              　　　   AS 'A.UP_USER'
                               ,A.UP_DATE              　　　   AS 'A.UP_DATE'
                               ,F.OUT_CODE                      AS 'F.OUT_CODE'
                               FROM OUT_STOCK_WAIT_DETAIL A
							   INNER JOIN ORDER_DETAIL B ON A.ORDER_DETAIL_ID = B.ID  AND B.OUT_TYPE != 'CD20001'
							   INNER JOIN STOCK_MST C ON B.STOCK_MST_ID = C.ID                           
                                LEFT JOIN 
                                (
                                 SELECT a.ORDER_DETAIL_ID,a.STOCK_MST_ID ,sum(a.OUT_QTY) as WAIT_QTY
                                 FROM OUT_STOCK_WAIT_DETAIL a
                                  WHERE USE_YN = 'Y'
                                  group by a.ORDER_DETAIL_ID,a.STOCK_MST_ID
                                )D ON B.ID = D.ORDER_DETAIL_ID AND B.STOCK_MST_ID = D.STOCK_MST_ID
                              INNER JOIN Code_Mst E ON C.TYPE = E.code
                              INNER JOIN IN_STOCK_DETAIL F ON A.IN_STOCK_DETAIL_ID = F.ID  
							  WHERE 1=1
                               AND B.STOP_YN = 'N'
                               AND B.USE_YN = 'Y'
							   AND A.USE_YN = 'Y'
							   AND A.OUT_STOCK_WAIT_MST_ID = {_Mst_Id};";

                DataTable _DataTable = new CoreBusiness().SELECT(sql);

                Function.Core.DisplayData_Set(_DataTable, fpSub);
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
        private void pfpSpread_ButtonClicked(object obj, EditorNotifyEventArgs e)
        {
            try
            {
                xFpSpread pfpSpread = obj as xFpSpread;

                if (e.EditingControl == null)
                {
                    return;
                }

                if (e.EditingControl.Text == "출고확정")
                {
                    if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0")
                    {
                        if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                        {
                            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                            {
                                string id = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();

                                if (id != "")
                                {
                                    string sql = $@"select COMPLETE_YN
                                                  from [dbo].[OUT_STOCK_WAIT_MST] 
                                                 WHERE ID = {id} AND USE_YN = 'Y'AND COMPLETE_YN = 'N'";

                                    DataTable _DataTable = new CoreBusiness().SELECT(sql);
                               
                                    if (_DataTable != null && _DataTable.Rows.Count == 1)
                                    {
                                        DialogResult pDialogResult = CustomMsg.ShowMessage("출고 확정 하시겠습니까?", "확인", MessageBoxButtons.YesNo);

                                        if (pDialogResult == DialogResult.Yes)
                                        {
                                             sql = $@"INSERT INTO [dbo].[OUT_STOCK_DETAIL]
                                                           ([OUT_CODE]
                                                           ,[OUT_STOCK_DATE]
                                                           ,[OUT_TYPE]
                                                           ,[ORDER_DETAIL_ID]
                                                           ,[PRODUCTION_INSTRUCT_ID]
                                                           ,[PRODUCTION_RESULT_ID]
                                                           ,[OUT_STOCK_WAIT_DETAIL_ID]
                                                           ,[IN_STOCK_DETAIL_ID]
                                                           ,[STOCK_MST_ID]
                                                           ,[OUT_QTY]
                                                           ,[COMMENT]
                                                           ,[COMPLETE_YN]
                                                           ,[USE_YN]
                                                           ,[REG_USER]
                                                           ,[REG_DATE]
                                                           ,[UP_USER]
                                                           ,[UP_DATE])
                                                        select
                                                         NULL
                                                        ,GETDATE()
                                                        ,'SD14003'
                                                        ,ORDER_DETAIL_ID
                                                        ,NULL
                                                        ,NULL                         
                                                        ,ID
                                                        ,IN_STOCK_DETAIL_ID
                                                        ,STOCK_MST_ID
                                                        ,OUT_QTY
                                                        ,''
                                                        ,'Y'
                                                        ,'Y'
                                                        ,'{pfpSpread._user_account}'
                                                        ,GETDATE()
                                                        ,'{pfpSpread._user_account}'
                                                        ,GETDATE()
                                                        from [dbo].[OUT_STOCK_WAIT_DETAIL]
                                                       where OUT_STOCK_WAIT_MST_ID = {pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()}
                                                         AND COMPLETE_YN = 'N'
                                                         AND USE_YN = 'Y'";

                                            new CoreBusiness().SELECT(sql);

                                            MainFind_DisplayData();
                                            SubFind_DisplayData();
                                        }
                                    }
                                    else
                                    {
                                        CustomMsg.ShowMessage("완료된 출고 지시입니다.");
                                        return;
                                    }
                                }
                            }
                        }
                    }


                }

            }
            catch (Exception err)
            {

            }
        }

    
    }
}
