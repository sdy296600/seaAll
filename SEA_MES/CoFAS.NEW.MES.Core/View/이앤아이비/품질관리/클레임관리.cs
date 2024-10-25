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
    public partial class 클레임관리 : DoubleBaseForm1
    {
        public 클레임관리()
        {
            InitializeComponent();   
        }
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT 
	                                   A.ID						AS 'ID'						
                                      ,A.ORDER_DETAIL_ID		AS 'A.ORDER_DETAIL_ID'
									  ,A.STOCK_MST_ID			AS 'A.STOCK_MST_ID'
                                      ,D.NAME                   AS 'D.NAME'
									  ,E.NAME					AS 'E.NAME'
									  ,A.TYPE                   AS 'A.TYPE'			
                                      ,A.CLAIM_DATE				AS 'A.CLAIM_DATE'				
                                      ,A.COMMNET				AS 'A.COMMNET'
                                      ,A.IMAGE                  AS 'A.IMAGE'
                                      ,A.COMPLETE_YN			AS 'A.COMPLETE_YN'			
                                      ,A.USE_YN					AS 'A.USE_YN'					
                                      ,A.REG_USER				AS 'A.REG_USER'				
                                      ,A.REG_DATE				AS 'A.REG_DATE'				
                                      ,A.UP_USER				AS 'A.UP_USER'				
                                      ,A.UP_DATE				AS 'A.UP_DATE'				
	                                  ,C.OUT_CODE				AS 'C.OUT_CODE'				
	                                  ,C.NAME					AS 'C.NAME'					
	                                  ,C.STANDARD				AS 'C.STANDARD'				
	                                  ,C.TYPE					AS 'C.TYPE'	
									  ,D.NAME                   AS 'D.NAME'
                                      FROM CLAIM_MST A
                                      INNER JOIN ORDER_DETAIL B ON A.ORDER_DETAIL_ID = B.ID
									  INNER JOIN STOCK_MST C ON A.STOCK_MST_ID = C.ID
									  INNER JOIN COMPANY D ON B.COMPANY_ID = D.ID
									  INNER JOIN ORDER_MST E ON B.ORDER_MST_ID = E.ID
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

        public override void SubFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT 
	                                   A.ID						    AS 'ID'						
                                      ,A.CLAIM_MST_ID	            AS 'A.CLAIM_MST_ID'				
                                      ,A.START_DATE				    AS 'A.START_DATE'
                                      ,A.END_DATE				    AS 'A.END_DATE'
                                      ,A.STOCK_MST_ID				AS 'A.STOCK_MST_ID'
                                      ,A.RESULT				        AS 'A.RESULT'
                                      ,A.COMMENT				    AS 'A.COMMENT'
                                      ,A.EQUIPMENT				    AS 'A.EQUIPMENT'				
                                      ,A.LOCATION			        AS 'LOCATION'			
                                      ,A.FILE_NAME				    AS 'FILE_NAME'
                                      ,A.NCRFILE				    AS 'A.NCRFILE'		
                                      ,A.COMPLETE_YN			    AS 'A.COMPLETE_YN'			
                                      ,A.USE_YN					    AS 'A.USE_YN'					
                                      ,A.REG_USER				    AS 'A.REG_USER'				
                                      ,A.REG_DATE				    AS 'A.REG_DATE'				
                                      ,A.UP_USER				    AS 'A.UP_USER'				
                                      ,A.UP_DATE				    AS 'A.UP_DATE'				
                                      FROM CLAIM_DETAIL A
                                      INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID
                                      WHERE 1=1         
                                      AND A.USE_YN = 'Y'
                                      AND A.CLAIM_MST_ID = {_Mst_Id} ";

                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str /*+ sb.ToString()*/;

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
        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {

            if (_Mst_Id == string.Empty)
            {
             
            }
            else
            {

                int row = 0;

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "ID").ToString() == _Mst_Id)
                    {
                        row = i;
                    }
                }


                Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);

                string mst = this._pMenuSettingEntity.BASE_TABLE.Split('/')[0] + "_ID";
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.CLAIM_MST_ID", _Mst_Id);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.STOCK_MST_ID".Trim(), fpMain.Sheets[0].GetValue(row, "A.STOCK_MST_ID").ToString());
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "REG_USER ".Trim(), MainForm.UserEntity.user_account);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "REG_DATE ".Trim(), DateTime.Now);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "UP_USER  ".Trim(), MainForm.UserEntity.user_account);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "UP_DATE  ".Trim(), DateTime.Now);
            }
        }

        public override void _SaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (Function.Core._SaveButtonClicked(fpSub))
                {

                    int row = 0;
                    decimal qty = 0;

                    for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                    {
                        if (fpMain.Sheets[0].GetValue(i, "ID").ToString() == _Mst_Id)
                        {
                            row = i;
                        }
                    }

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

        public override void SubSave_InputData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpSub.Focus();
                bool _Error = new CoreBusiness().BaseForm1_A10(this._pMenuSettingEntity, fpSub, this._pMenuSettingEntity.BASE_TABLE.Split('/')[1]);
                if (!_Error)
                {


                    fpSub.Sheets[0].Rows.Count = 0;
                    MainFind_DisplayData();
                    SubFind_DisplayData();
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
