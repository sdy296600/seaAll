using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 압출조건현황 : BaseForm1
    {
        public 압출조건현황()
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
                                        B.NAME             AS 'B.NAME'
                                        ,A.ID			   AS ID
										,EXT_ID			   
										,COUNT			   
										,PV_TEMP1
										,PV_TEMP2
										,PV_TEMP3
										,PV_TEMP4
										,PV_TEMP5
										,PV_TEMP6
										,PV_TEMP7
										,PV_TEMP8
										,PV_LOAD
										,PV_RPM
										,PV_SPEED
										,PV_STATUS
										,A.USE_YN		   AS USE_YN
										,A.UP_USER		   AS UP_USER
										,A.UP_DATE		   AS UP_DATE
										,A.REG_USER		   AS REG_USER
                                        ,A.REG_DATE        AS 'A.REG_DATE'
                                        FROM  CCS_EXTRUSION_SPC A
							            inner join EQUIPMENT B ON A.EXT_ID = B.COLUMN1
                                        WHERE 1=1 
							            and B.TYPE = 'CD14003'";

                StringBuilder sb = new StringBuilder();
                Function.Core.GET_WHERE(this._PAN_WHERE, sb);

                string sql = str + sb.ToString();

                DataTable _DataTable = new CoreBusiness().SELECT(sql);
                fpMain.DataSource = _DataTable;
                //Function.Core.DisplayData_Set(_DataTable, fpMain);

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
