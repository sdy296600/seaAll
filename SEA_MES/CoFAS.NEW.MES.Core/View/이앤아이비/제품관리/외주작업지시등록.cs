using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 외주작업지시등록 : BaseForm1
    {
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain._EditorNotifyEventHandler -= new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                fpMain._EditorNotifyEventHandler += new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                //fpMain.CellDoubleClick -= new CellClickEventHandler(pfpSpread_CellClick);
                //fpMain.CellDoubleClick += new CellClickEventHandler(pfpSpread_CellClick);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"select A.ID                         AS  'ID'                
                                      ,A.PRODUCTION_PLAN_ID         AS  'A.PRODUCTION_PLAN_ID' 
                                      ,A.OUT_CODE                   AS  'A.OUT_CODE'           
                                      ,A.INSTRUCT_DATE              AS  'A.INSTRUCT_DATE'      
                                      ,A.STOCK_MST_ID               AS  'A.STOCK_MST_ID'       
                                      ,B.OUT_CODE                   AS  'B.OUT_CODE'   
                                      ,B.NAME                       AS  'B.NAME'    
                                      ,B.STANDARD                   AS  'B.STANDARD'           
                                      ,B.TYPE                       AS  'B.TYPE'               
                                      ,C.NAME                       AS  'C.NAME'               
                                      ,A.INSTRUCT_QTY               AS  'A.INSTRUCT_QTY'
									  ,ISNULL(D.IN_QTY,0)           AS  'D.IN_QTY'
									  ,ISNULL(D.COMPLETE_QTY,0)     AS  'D.COMPLETE_QTY'                                 
                                      ,A.SORT                       AS  'A.SORT'               
                                      ,A.COMMENT                    AS  'A.COMMENT'       
                                      ,A.COMPANY_ID                 AS  'A.COMPANY_ID'
                                      ,A.COMPLETE_YN                AS  'A.COMPLETE_YN'        
                                      ,A.USE_YN                     AS  'A.USE_YN'             
                                      ,A.REG_USER                   AS  'A.REG_USER'           
                                      ,A.REG_DATE                   AS  'A.REG_DATE'           
                                      ,A.UP_USER                    AS  'A.UP_USER'            
                                      ,A.UP_DATE                    AS  'A.UP_DATE'
                                      ,A.PROCESS_ID                 AS  'A.PROCESS_ID'
                                 from [dbo].[PRODUCTION_INSTRUCT] A
                                 INNER JOIN [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID
                                 INNER JOIN [dbo].[PROCESS] C ON A.PROCESS_ID = C.ID
								 LEFT  JOIN [dbo].[IN_STOCK_WAIT_DETAIL] D ON A.ID = D.PRODUCTION_INSTRUCT_ID
                                 WHERE 1=1
                                 AND A.USE_YN = 'Y'
                                 AND A.PROCESS_ID = '6'";

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

        private void pfpSpread_ButtonClicked(object obj, EditorNotifyEventArgs e)
        {
            try
            {
                xFpSpread pfpSpread = obj as xFpSpread;

                if (e.EditingControl == null)
                {
                    return;
                }

                if (e.EditingControl.Text == "반출등록")
                {
                    if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0")
                    {
                        if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                        {
                            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                            {

                                string sql = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();
                                if (sql != "")
                                {
                                    외주작업지시_반출 basePopupBox = new 외주작업지시_반출( pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString(), pfpSpread._user_account , pfpSpread.Sheets[0].GetText(e.Row, "B.OUT_CODE").ToString() + " , " + pfpSpread.Sheets[0].GetText(e.Row, "B.NAME").ToString() , pfpSpread.Sheets[0].GetValue(e.Row, "A.INSTRUCT_QTY").ToString());

                                    if (basePopupBox.ShowDialog() == DialogResult.OK)
                                    {

                                    }
                                }

                            }
                            MainFind_DisplayData();
                        }
                    }
                }
                else if (e.EditingControl.Text == "반입등록")
                {
                    if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0")
                    {
                        if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                        {
                            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                            {

                                string sql = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();
                                if (sql != "")
                                {
                                    외주작업지시_반입 basePopupBox = new 외주작업지시_반입(
                                             pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()
                                            ,pfpSpread._user_account
                                            ,pfpSpread.Sheets[0].GetValue(e.Row, "A.STOCK_MST_ID".Trim()).ToString()
                                            ,pfpSpread.Sheets[0].GetValue(e.Row, "B.OUT_CODE    ".Trim()).ToString()
                                            ,pfpSpread.Sheets[0].GetValue(e.Row, "B.NAME        ".Trim()).ToString()
                                            ,pfpSpread.Sheets[0].GetValue(e.Row, "B.STANDARD    ".Trim()).ToString()
                                            ,pfpSpread.Sheets[0].GetValue(e.Row, "B.TYPE        ".Trim()).ToString()
                                            ,pfpSpread.Sheets[0].GetValue(e.Row, "A.INSTRUCT_QTY").ToString()
                                            );


                                    if (basePopupBox.ShowDialog() == DialogResult.OK)
                                    {
                                        //pfpSpread.Sheets[0].SetValue(e.Row, e.Column, baseImagePopupBox._Image);
                                        //pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
                                    }
                                }
                            }
                            MainFind_DisplayData();
                        }
                    }
                }

                else if (e.EditingControl.Text == "완료처리")
                {
                    if (pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString() != "0")
                    {
                        if (pfpSpread != null && pfpSpread.Sheets[0].Columns[e.Column].CellType != null)
                        {
                            if (pfpSpread.Sheets[0].Columns[e.Column].CellType.GetType() == typeof(ButtonCellType))
                            {

                                if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "완료처리")
                                {
                                    string sql = pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString();
                                    string text = pfpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text.ToString();
                                    if (sql != "")
                                    {
                                        if (CustomMsg.ShowExceptionMessage("작업지시를 완료처리하시겠습니까?", "작업지시 완료처리", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            pfpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text = "수정";
                                            bool _Error = new SI_Business().INSTRUCT_COMPLETE_Y_A10(pfpSpread, pfpSpread.Sheets[0].GetValue(e.Row, "REG_USER").ToString());
                                            if (_Error == false)
                                            {
                                                pfpSpread.Sheets[0].RowHeader.Cells[e.Row, 0].Text = text;
                                                CustomMsg.ShowMessage("저장되었습니다.");
                                                MainFind_DisplayData();
                                            }
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                                if (pfpSpread.Sheets[0].Columns[e.Column].Tag.ToString() == "")
                                {

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
