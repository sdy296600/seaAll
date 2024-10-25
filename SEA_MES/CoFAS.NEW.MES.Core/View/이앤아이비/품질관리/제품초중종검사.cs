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
    public partial class 제품초중종검사 : BaseForm1
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
                                      ,A.STOCK_MST_ID               AS  'A.STOCK_MST_ID'       
                                      ,B.OUT_CODE                   AS  'B.OUT_CODE'   
                                      ,B.NAME                       AS  'B.NAME'    
                                      ,B.STANDARD                   AS  'B.STANDARD'           
                                      ,B.TYPE                       AS  'B.TYPE'                                       
                                      ,A.TYPE                       AS  'A.TYPE' 
                                      ,A.COMMENT                    AS  'A.COMMENT'           
                                      ,A.USE_YN                     AS  'A.USE_YN'             
                                      ,A.REG_USER                   AS  'A.REG_USER'           
                                      ,A.REG_DATE                   AS  'A.REG_DATE'           
                                      ,A.UP_USER                    AS  'A.UP_USER'            
                                      ,A.UP_DATE                    AS  'A.UP_DATE'
                                 from [dbo].[SELF_INSPECTION_MST] A
                                 INNER JOIN [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID
                                 WHERE 1=1
                                 AND A.USE_YN = 'Y'";

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

                else if (e.EditingControl.Text == "검사결과등록")
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
                                    초중종검사결과등록_PopupBox basePopupBox = new 초중종검사결과등록_PopupBox(
                                             pfpSpread.Sheets[0].GetValue(e.Row, "A.STOCK_MST_ID".Trim()).ToString()
                                            , pfpSpread._user_account
                                            , pfpSpread.Sheets[0].GetValue(e.Row, "ID".Trim()).ToString()
                                            , pfpSpread.Sheets[0].GetValue(e.Row, "A.TYPE".Trim()).ToString()
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

            }
            catch (Exception err)
            {

            }
        }
    }
}
