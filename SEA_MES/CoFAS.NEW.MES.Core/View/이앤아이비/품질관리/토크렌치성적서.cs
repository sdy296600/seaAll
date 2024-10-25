using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 토크렌치성적서 : DoubleBaseForm1
    {
 
        public 토크렌치성적서()
        {
            Load += new EventHandler(DoubleBaseForm1_Load);
            InitializeComponent();
        }

        private void DoubleBaseForm1_Load(object sender, EventArgs e)
        {

        }

        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                string str = $@"select *
                              from [dbo].[TOQLENCH_MST]
                              WHERE 1=1";

                string where = @"AND USE_YN = 'Y'";

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                where += sb.ToString();

                string sql = str + where;

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

                string str = $@"select *
                              ,ISNULL(DATEDIFF(DAY, DATE, CYCLE),0) AS RESULT
                              ,DATEADD(DAY, 180, DATE)              AS DATE_RESULT
                              from [dbo].[TOQLENCH_DETAIL]
                              WHERE 1=1
                              AND TOQLENCH_MST_ID = {_Mst_Id}
                              AND USE_YN = 'Y'";

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str;

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
                string mst = this._pMenuSettingEntity.BASE_TABLE.Split('/')[0] + "_ID";
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, mst, _Mst_Id);
                //_Mst_Id = fpMain.Sheets[0].GetValue(row, "ID ".Trim()).ToString();
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "TOQLENCH_MST_ID              ".Trim(), fpMain.Sheets[0].GetValue(row, "ID ".Trim()));
                //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "MST_OUT_CODE ".Trim(), fpMain.Sheets[0].GetValue(row, "OUT_CODE".Trim()));
                //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "MST_NAME ".Trim(), fpMain.Sheets[0].GetValue(row, "NAME".Trim()));
                //fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "MST_STANDARD ".Trim(), fpMain.Sheets[0].GetValue(row, "STANDARD".Trim()));
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "REG_USER ".Trim(), MainForm.UserEntity.user_account);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "REG_DATE ".Trim(), DateTime.Now);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "UP_USER  ".Trim(), MainForm.UserEntity.user_account);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "UP_DATE  ".Trim(), DateTime.Now);
                fpSub.Sheets[0].SetValue(fpSub.Sheets[0].ActiveRowIndex, "CYCLE  ".Trim(), DateTime.Now.AddDays(180));
            }
        }

    
    }
}
