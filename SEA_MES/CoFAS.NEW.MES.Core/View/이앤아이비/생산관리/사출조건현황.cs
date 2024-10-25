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
    public partial class 사출조건현황 : BaseForm1
    {
        public 사출조건현황()
        {
            InitializeComponent();
        }
        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                fpMain.Sheets[0].Rows.Count = 0;

                //string str = $@"SELECT 
                //                        B.NAME             AS 'B.NAME'
                //                        ,*
                //                        FROM  CCS_INJ_SPC_DATA A
                //   inner join EQUIPMENT B ON RTRIM(A.INJ_NO) = B.COLUMN1
                //   WHERE B.TYPE = 'CD14001'";

                //string str = $@"SELECT 
                //                        B.NAME             AS 'B.NAME'
                //                        ,*
                //                        FROM  CCS_INJ_SPC_DATA A
                //   inner join EQUIPMENT B ON A.INJ_NO = B.COLUMN1
                //   WHERE B.TYPE = 'CD14001'";

                string str = $@"SELECT 
                                        B.NAME             AS 'B.NAME'
										,UNIQ_NO
										,INJ_ID
										,INJ_NO
										,SPC_DATETIME
										,INJ_CONTROL
										,PROC_QTY
										,CV_001
										,CV_002
										,CV_003
										,CV_004
										,CV_005
										,CV_006
										,CV_007
										,CV_008
										,CV_009
										,CV_010
										,CV_011
										,CV_012
										,CV_013
										,CV_014
										,CV_015
										,CV_016
										,CV_017
										,CV_018
										,CV_019
										,CV_020
										,CV_021
										,CV_022
										,CV_023
										,CV_024
										,CV_025
										,CV_026
										,CV_027
										,CV_028
										,PROC_DATETIME
										,DAY_COUNT
										,SV_001
										,SV_002
										,SV_003
										,SV_004
										,SV_005
										,SV_006
										,SV_007
										,SV_008
										,SV_009
										,SV_010
										,SV_011
										,SV_012
										,SV_013
										,SV_014
										,SV_015
										,SV_016
										,SV_017
										,SV_018
										,SV_019
										,SV_020
										,SV_021
										,SV_022
										,SV_023
										,SV_024
										,SV_025
										,SV_026
										,SV_027
										,SV_028
										,SV_029
										,SV_030
										,SV_031
										,SV_032
										,SV_033
										,SV_034
										,SV_035
										,SV_036
										,SV_037
										,SV_038
										,SV_039
										,SV_040
										,SV_041
										,SV_042
										,SV_043
										,SV_044
										,SV_045
										,SV_046
										,SV_047
										,SV_048
										,SV_049
										,SV_050
										,SV_051
										,SV_052
										,SV_053
										,SV_054
										,SV_055
										,SV_056
										,SV_057
										,SV_058
										,SV_059
										,SV_060
										,SV_061
										,SV_062
										,SV_063
										,SV_064
										,MOLD_CAVITY
										,MOLD_NAME
										,A.USE_YN		   AS USE_YN
										,A.UP_USER	       AS UP_USER
										,A.UP_DATE		   AS UP_DATE
										,A.REG_USER		   AS REG_USER
                                        ,A.REG_DATE        AS REG_DATE

                                        FROM  CCS_INJ_SPC_DATA A
							            inner join EQUIPMENT B ON A.INJ_NO = B.COLUMN1
							            WHERE B.TYPE = 'CD14001'";

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
