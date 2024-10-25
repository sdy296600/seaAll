using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.Spreadsheet;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 수주관리 : DoubleBaseForm1
    {
        public 수주관리()
        {
            InitializeComponent();
        }

        public override void _AddItemButtonClicked(object sender, EventArgs e)
        {
            //NewMethod();
            //return;
            if (_Mst_Id == string.Empty)
            {
                Function.Core._AddItemButtonClicked(fpMain, MainForm.UserEntity.user_account);
            }
            else
            {
                Function.Core._AddItemButtonClicked(fpSub, MainForm.UserEntity.user_account);
                int row = 0;

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {
                    if (fpMain.Sheets[0].GetValue(i, "ID").ToString() == _Mst_Id)
                    {
                        row = i;
                    }
                }

                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.ORDER_MST_ID".Trim(), _Mst_Id);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.COMPANY_ID  ".Trim(), fpMain.Sheets[0].GetValue(row, "A.COMPANY_ID".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "B.NAME        ".Trim(), fpMain.Sheets[0].GetValue(row, "B.NAME      ".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "A.STOP_YN     ".Trim(), "N");
                fpSub.Sheets[0].SetText(fpSub.Sheets[0].ActiveRowIndex, "A.OUT_TYPE    ".Trim(), "미진행");




            }


        }
        public override void _ImportButtonClicked(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    수주관리_PopupBox 수주관리_PopupBox = new 수주관리_PopupBox(openFileDialog1.FileName);
                    수주관리_PopupBox._UserAccount = MainForm.UserEntity.user_account;
                    if (수주관리_PopupBox.ShowDialog() == DialogResult.OK)
                    {
                        MainFind_DisplayData();
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
                fpMain._EditorNotifyEventHandler -= new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                fpMain._EditorNotifyEventHandler += new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                fpSub._EditorNotifyEventHandler -= new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                fpSub._EditorNotifyEventHandler += new EditorNotifyEventHandler(pfpSpread_ButtonClicked);
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"SELECT A.ID				  AS 'ID'
                                      ,A.OUT_CODE		  AS 'A.OUT_CODE'
                                      ,A.NAME			  AS 'A.NAME'
                                      ,A.ORDER_DATE		  AS 'A.ORDER_DATE'
                                      ,A.COMPANY_ID  	  AS 'A.COMPANY_ID'
                                      ,B.NAME			  AS 'B.NAME'
                                      ,A.ORDER_TYPE		  AS 'A.ORDER_TYPE'
                                      ,A.CURRENCY_TYPE	  AS 'A.CURRENCY_TYPE'
                                      ,A.EXCHANGE_RATE	  AS 'A.EXCHANGE_RATE'
                                      ,A.MATERIAL_COST	  AS 'A.MATERIAL_COST'
                                      ,A.MANUFACTURE_COST AS 'A.MANUFACTURE_COST'
                                      ,A.ETC_COST		  AS 'A.ETC_COST'
                                      ,A.TOTAL_COST		  AS 'A.TOTAL_COST'
                                      ,A.COMMENT		  AS 'A.COMMENT'
                                      ,A.OUT_TYPE		  AS 'A.OUT_TYPE'
                                      ,A.ORDER_USER		  AS 'A.ORDER_USER'
                                      ,A.IN_ORDER_USER	  AS 'A.IN_ORDER_USER'
                                      ,A.STOCK_FLAG		  AS 'A.STOCK_FLAG'
                                      ,A.COMPLETE_YN	  AS 'A.COMPLETE_YN'
                                      ,A.USE_YN			  AS 'A.USE_YN'
                                      ,A.REG_USER		  AS 'A.REG_USER'
                                      ,A.REG_DATE		  AS 'A.REG_DATE'
                                      ,A.UP_USER		  AS 'A.UP_USER'
                                      ,A.UP_DATE 		  AS 'A.UP_DATE' 	                 
                                       FROM [dbo].[ORDER_MST] A
                                       INNER JOIN [dbo].[COMPANY] B ON A.COMPANY_ID = B.ID
                                       WHERE 1=1 ";
                StringBuilder sb = new StringBuilder();

                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString()
                    + this._pMenuSettingEntity.BASE_WHERE
                    + this._pMenuSettingEntity.BASE_ORDER;


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

                string sql = $@"SELECT A.ID					AS	'ID'					
                                      ,A.ORDER_MST_ID		AS	'A.ORDER_MST_ID'	
                                      ,A.STOCK_MST_ID		AS	'A.STOCK_MST_ID'		
	                                  ,A.SUPPLY_TYPE		AS	'A.SUPPLY_TYPE'
                                      ,A.STOCK_MST_PRICE    AS	'A.STOCK_MST_PRICE'
                                      ,C.OUT_CODE			AS	'C.OUT_CODE'
                                      ,C.NAME		    	AS	'C.NAME'	
                                      ,C.STANDARD			AS	'C.STANDARD'			
                                      ,C.TYPE				AS	'C.TYPE'				
                                      ,C.UNIT				AS	'C.UNIT'				
                                      ,C.PRICE				AS	'C.PRICE'				
                                      ,C.TYPE2				AS	'C.TYPE2'	
                                      ,C.QTY                AS  'C.QTY'        
                                      ,C.IN_SCHEDULE        AS  'C.IN_SCHEDULE' 
                                      ,C.OUT_SCHEDULE       AS  'C.OUT_SCHEDULE'
                                      ,A.CONVERSION_UNIT	AS	'A.CONVERSION_UNIT'	
                                      ,A.ORDER_QTY			AS	'A.ORDER_QTY'			
                                      ,A.ORDER_REMAIN_QTY	AS	'A.ORDER_REMAIN_QTY'	
                                      ,A.BOX_QTY			AS	'A.BOX_QTY'			
                                      ,A.OUT_QTY			AS	'A.OUT_QTY'			
                                      ,A.COST				AS	'A.COST'	
                                      ,A.COMPANY_ID			AS	'A.COMPANY_ID'			
	                                  ,B.NAME				AS	'B.NAME'				
                                      ,A.DEMAND_DATE		AS	'A.DEMAND_DATE'		
                                      ,A.OUT_TYPE			AS	'A.OUT_TYPE'			
                                      ,A.STOP_YN			AS	'A.STOP_YN'			
                                      ,A.COMMENT			AS	'A.COMMENT'			
                                      ,A.SUPPLY_PRICE_KRW	AS	'A.SUPPLY_PRICE_KRW'	
                                      ,A.FOREIGN_CURRENY	AS	'A.FOREIGN_CURRENY'	
                                      ,A.PUBLISH_DATE		AS	'A.PUBLISH_DATE'		
                                      ,A.DEADLINE			AS	'A.DEADLINE'			
                                      ,A.EXPORT_ID			AS	'A.EXPORT_ID'			
                                      ,A.INSPECTION_YN		AS	'A.INSPECTION_YN'		
                                      ,A.COMPLETE_YN		AS	'A.COMPLETE_YN'		
                                      ,A.USE_YN				AS	'A.USE_YN'				
                                      ,A.REG_USER			AS	'A.REG_USER'			
                                      ,A.REG_DATE			AS	'A.REG_DATE'			
                                      ,A.UP_USER			AS	'A.UP_USER'			
                                      ,A.UP_DATE			AS	'A.UP_DATE'			
                                 FROM [dbo].[ORDER_DETAIL] A 
                                 INNER JOIN [dbo].[COMPANY] B ON A.COMPANY_ID = B.ID 
                                 INNER JOIN [dbo].[STOCK_MST] C ON A.STOCK_MST_ID = C.ID
                                 WHERE 1=1 AND A.USE_YN = 'Y' AND A.ORDER_MST_ID = {_Mst_Id}";



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

                if (e.EditingControl.Text == "전체지시")
                {
                    DialogResult _DialogResult1 = CustomMsg.ShowMessage("해당 요청를 전체 출고지시 하시겠습니까?", "Question", MessageBoxButtons.YesNo);
                    if (_DialogResult1 == DialogResult.Yes)
                    {
                        string str = "";
                        if (pfpSpread.Sheets[0].GetValue(e.Row, "A.OUT_TYPE").ToString() == "CD20001")
                        {
                           str = $@"UPDATE [dbo].[ORDER_DETAIL]
                                            SET OUT_TYPE　='CD20002'
                                            where 1=1 
                                            AND ORDER_MST_ID = {pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()}
                                            AND OUT_TYPE = 'CD20001';
                                    UPDATE [dbo].[ORDER_MST]
                                            SET OUT_TYPE　='CD20002'
                                            where 1=1 
                                            AND ID = {pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()}
                                            AND OUT_TYPE = 'CD20001'";
                        }
                        else
                        {
                            str = $@"UPDATE [dbo].[ORDER_DETAIL]
                                            SET OUT_TYPE　='CD20001'
                                            where 1=1 
                                            AND ORDER_MST_ID = {pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()}
                                            AND OUT_TYPE = 'CD20002';
                                     UPDATE [dbo].[ORDER_MST]
                                            SET OUT_TYPE　='CD20001'
                                            where 1=1 
                                            AND ID = {pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()}
                                            AND OUT_TYPE = 'CD20002'";
                        }

                        DataTable _DataTable = new CoreBusiness().SELECT(str);

                        MainFind_DisplayData();
                        SubFind_DisplayData();
                    }

                }
                else if (e.EditingControl.Text == "출고지시")
                {
                    DialogResult _DialogResult1 = CustomMsg.ShowMessage("해당 요청를 출고지시 하시겠습니까?", "Question", MessageBoxButtons.YesNo);
                    if (_DialogResult1 == DialogResult.Yes)
                    {
                        string str = "";

                        if (pfpSpread.Sheets[0].GetValue(e.Row, "A.OUT_TYPE").ToString() == "CD20001")
                        {
                            str = $@"UPDATE [dbo].[ORDER_DETAIL]
                                           SET OUT_TYPE　='CD20002'
                                           where 1=1 
                                           AND ID = {pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()}
                                           AND OUT_TYPE = 'CD20001'";


                        }
                        else
                        {
                            str = $@"UPDATE [dbo].[ORDER_DETAIL]
                                           SET OUT_TYPE　='CD20001'
                                           where 1=1 
                                           AND ID = {pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()}
                                           AND OUT_TYPE = 'CD20002'";
                        }

                        DataTable _DataTable = new CoreBusiness().SELECT(str);
                        SubFind_DisplayData();
                    }
                }

                else if (e.EditingControl.Text == "클레임등록")
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
                                    클레임등록_PopupBox basePopupBox = new 클레임등록_PopupBox(
                                             pfpSpread.Sheets[0].GetValue(e.Row, "ID").ToString()
                                            , pfpSpread._user_account
                                            , pfpSpread.Sheets[0].GetValue(e.Row, "A.STOCK_MST_ID".Trim()).ToString()
                                            , pfpSpread.Sheets[0].GetValue(e.Row, "C.OUT_CODE    ".Trim()).ToString()
                                            , pfpSpread.Sheets[0].GetValue(e.Row, "C.NAME        ".Trim()).ToString()
                                            , pfpSpread.Sheets[0].GetValue(e.Row, "C.STANDARD    ".Trim()).ToString()
                                            , pfpSpread.Sheets[0].GetValue(e.Row, "C.TYPE        ".Trim()).ToString()
                                            , pfpSpread.Sheets[0].GetValue(e.Row, "B.NAME        ".Trim()).ToString()
                                            , fpMain.Sheets[0].GetValue(e.Row, "A.COMPANY_ID  ".Trim()).ToString()
                                            );


                                    if (basePopupBox.ShowDialog() == DialogResult.OK)
                                    {
                                        //pfpSpread.Sheets[0].SetValue(e.Row, e.Column, baseImagePopupBox._Image);
                                        //pfpSpread._ChangeEvent(pfpSpread, new ChangeEventArgs(null, e.Row, e.Column));
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
