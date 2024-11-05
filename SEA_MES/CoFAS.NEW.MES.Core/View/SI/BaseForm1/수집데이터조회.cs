using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.DataAccess.Native.Sql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core
{
    public partial class 수집데이터조회 : BaseForm1
    {
        DataBase_Class myDb;
        DataBase_Class msDb;

        public 수집데이터조회()
        {
            InitializeComponent();

        }


        public override void MainFind_DisplayData()
        {
            try
            {
                DevExpressManager.SetCursor(this, Cursors.WaitCursor);

                Base_textbox text = _PAN_WHERE.Controls[0] as Base_textbox;
                string machine_no = text.SearchText;
                Base_FromtoDateTime datetime = _PAN_WHERE.Controls[1] as Base_FromtoDateTime;
                string startTime = datetime.StartValue.ToString("yyyy-MM-dd HH:mm:ss");
                string endTime = datetime.EndValue.ToString("yyyy-MM-dd HH:mm:ss");
                //string machine_no = 
                string sql1 = $@"WITH LatestValues AS (
                                SELECT
                                    Category,
                                    VALUE,
                                    ROW_NUMBER() OVER (PARTITION BY Category ORDER BY ID DESC) AS rn,
                                    TIMESTAMP
                                FROM timeseriesdata
                                WHERE Timestamp >= '{startTime}' AND Timestamp < '{endTime}'  
                            )";

                string sql2 = "";
                switch (machine_no) 
                {
                    case "13호기":
                        sql2 = $@"SELECT 
                                '13호 주조기' AS MACHINE_NO,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6900_Ruled' THEN VALUE END), 0) AS V1,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6902_Ruled' THEN VALUE END), 0) AS V2,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6904_Ruled' THEN VALUE END), 0) AS V3,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6906_Ruled' THEN VALUE END), 0) AS V4,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6908' THEN VALUE END), 0) AS ACCELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6910' THEN VALUE END), 0) AS DECELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6912_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6914' THEN VALUE END), 0) AS SWAP_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6916' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6918' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6920_Ruled' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6936_Ruled' THEN VALUE END), 0) AS CYCLE_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6938_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6940_Ruled' THEN VALUE END), 0) AS BATH_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6942_Ruled' THEN VALUE END), 0) AS FORWARD_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6944_Ruled' THEN VALUE END), 0) AS FREEZING_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6946_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6948_Ruled' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6950_Ruled' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_13_TAG_D6952_Ruled' THEN VALUE END), 0) AS SPRAY_TIME,
                                0 AS CAVITY_CORE,
                                0 AS A_POLLUTION_DEGREE,
                                0 AS B_POLLUTION_DEGREE,
                                0 AS VACUUM,
                                MAX(TIMESTAMP) AS TIMESTAMP
                            FROM LatestValues
                            ";
                        break;

                    case "21호기":
                        sql2 = $@"SELECT 
                                '21호 주조기' AS MACHINE_NO,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6900_Ruled' THEN VALUE END), 0) AS V1,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6902_Ruled' THEN VALUE END), 0) AS V2,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6904_Ruled' THEN VALUE END), 0) AS V3,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6906_Ruled' THEN VALUE END), 0) AS V4,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6908' THEN VALUE END), 0) AS ACCELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6910' THEN VALUE END), 0) AS DECELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6912_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6914' THEN VALUE END), 0) AS SWAP_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6916' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6918' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6920_Ruled' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6936_Ruled' THEN VALUE END), 0) AS CYCLE_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6938_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6940_Ruled' THEN VALUE END), 0) AS BATH_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6942_Ruled' THEN VALUE END), 0) AS FORWARD_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6944_Ruled' THEN VALUE END), 0) AS FREEZING_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6946_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6948_Ruled' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6950_Ruled' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'DCM_21_TAG_D6952_Ruled' THEN VALUE END), 0) AS SPRAY_TIME,
                                COALESCE(MAX(CASE WHEN Category = 'LS_21_DW816' THEN VALUE END), 0) AS CAVITY_CORE,
                                COALESCE(MAX(CASE WHEN Category = 'LS_21_DW817' THEN VALUE END), 0) AS A_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = 'LS_21_DW818' THEN VALUE END), 0) AS B_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = 'LS_21_DW819' THEN VALUE END), 0) AS VACUUM,
                                MAX(TIMESTAMP) AS TIMESTAMP
                            FROM LatestValues
                            ";
                        break;
                    case "22호기":
                        sql2 = $@"SELECT 
                                  '22호 주조기' AS MACHINE_NO,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6900_Ruled' THEN VALUE END), 0) AS V1,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6902_Ruled' THEN VALUE END), 0) AS V2,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6904_Ruled' THEN VALUE END), 0) AS V3,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6906_Ruled' THEN VALUE END), 0) AS V4,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6908' THEN VALUE END), 0) AS ACCELERATION_POS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6910' THEN VALUE END), 0) AS DECELERATION_POS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6912_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6914' THEN VALUE END), 0) AS SWAP_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6916' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6918' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6920_Ruled' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6936_Ruled' THEN VALUE END), 0) AS CYCLE_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6938_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6940_Ruled' THEN VALUE END), 0) AS BATH_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6942_Ruled' THEN VALUE END), 0) AS FORWARD_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6944_Ruled' THEN VALUE END), 0) AS FREEZING_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6946_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6948_Ruled' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6950_Ruled' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_22_TAG_D6952_Ruled' THEN VALUE END), 0) AS SPRAY_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_22_DW816' THEN VALUE END), 0) AS CAVITY_CORE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_22_DW817' THEN VALUE END), 0) AS A_POLLUTION_DEGREE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_22_DW818' THEN VALUE END), 0) AS B_POLLUTION_DEGREE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_22_DW819' THEN VALUE END), 0) AS VACUUM,
                                  MAX(TIMESTAMP) AS TIMESTAMP
                              FROM LatestValues
                              ";
                        break;
                    case "23호기":
                        sql2 = $@"SELECT 
                                  '23호 주조기' AS MACHINE_NO,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6900_Ruled' THEN VALUE END), 0) AS V1,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6902_Ruled' THEN VALUE END), 0) AS V2,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6904_Ruled' THEN VALUE END), 0) AS V3,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6906_Ruled' THEN VALUE END), 0) AS V4,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6908' THEN VALUE END), 0) AS ACCELERATION_POS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6910' THEN VALUE END), 0) AS DECELERATION_POS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6912_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6914' THEN VALUE END), 0) AS SWAP_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6916' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6918' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6920_Ruled' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6936_Ruled' THEN VALUE END), 0) AS CYCLE_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6938_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6940_Ruled' THEN VALUE END), 0) AS BATH_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6942_Ruled' THEN VALUE END), 0) AS FORWARD_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6944_Ruled' THEN VALUE END), 0) AS FREEZING_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6946_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6948_Ruled' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6950_Ruled' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_23_TAG_D6952_Ruled' THEN VALUE END), 0) AS SPRAY_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_23_DW816' THEN VALUE END), 0) AS CAVITY_CORE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_23_DW817' THEN VALUE END), 0) AS A_POLLUTION_DEGREE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_23_DW818' THEN VALUE END), 0) AS B_POLLUTION_DEGREE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_23_DW819' THEN VALUE END), 0) AS VACUUM,
                                  MAX(TIMESTAMP) AS TIMESTAMP
                              FROM LatestValues
                              ";
                        break;
                    case "24호기":
                        sql2 = $@"SELECT 
                                  '24호 주조기' AS MACHINE_NO,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6900_Ruled' THEN VALUE END), 0) AS V1,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6902_Ruled' THEN VALUE END), 0) AS V2,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6904_Ruled' THEN VALUE END), 0) AS V3,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6906_Ruled' THEN VALUE END), 0) AS V4,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6908' THEN VALUE END), 0) AS ACCELERATION_POS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6910' THEN VALUE END), 0) AS DECELERATION_POS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6912_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6914' THEN VALUE END), 0) AS SWAP_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6916' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6918' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6920_Ruled' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6936_Ruled' THEN VALUE END), 0) AS CYCLE_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6938_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6940_Ruled' THEN VALUE END), 0) AS BATH_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6942_Ruled' THEN VALUE END), 0) AS FORWARD_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6944_Ruled' THEN VALUE END), 0) AS FREEZING_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6946_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6948_Ruled' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6950_Ruled' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_24_TAG_D6952_Ruled' THEN VALUE END), 0) AS SPRAY_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_24_DW816' THEN VALUE END), 0) AS CAVITY_CORE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_24_DW817' THEN VALUE END), 0) AS A_POLLUTION_DEGREE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_24_DW818' THEN VALUE END), 0) AS B_POLLUTION_DEGREE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_24_DW819' THEN VALUE END), 0) AS VACUUM,
                                  MAX(TIMESTAMP) AS TIMESTAMP
                              FROM LatestValues
                              ";
                        break;
                    case "25호기":
                        sql2 = $@"SELECT 
                                  '25호 주조기' AS MACHINE_NO,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6900_Ruled' THEN VALUE END), 0) AS V1,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6902_Ruled' THEN VALUE END), 0) AS V2,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6904_Ruled' THEN VALUE END), 0) AS V3,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6906_Ruled' THEN VALUE END), 0) AS V4,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6908' THEN VALUE END), 0) AS ACCELERATION_POS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6910' THEN VALUE END), 0) AS DECELERATION_POS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6912_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6914' THEN VALUE END), 0) AS SWAP_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6916' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6918' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6920_Ruled' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6936_Ruled' THEN VALUE END), 0) AS CYCLE_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6938_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6940_Ruled' THEN VALUE END), 0) AS BATH_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6942_Ruled' THEN VALUE END), 0) AS FORWARD_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6944_Ruled' THEN VALUE END), 0) AS FREEZING_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6946_Ruled' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6948_Ruled' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6950_Ruled' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'DCM_25_TAG_D6952_Ruled' THEN VALUE END), 0) AS SPRAY_TIME,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_25_DW186' THEN VALUE END), 0) AS CAVITY_CORE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_25_DW187' THEN VALUE END), 0) AS A_POLLUTION_DEGREE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_25_DW188' THEN VALUE END), 0) AS B_POLLUTION_DEGREE,
                                  COALESCE(MAX(CASE WHEN Category = 'LS_25_DW189' THEN VALUE END), 0) AS VACUUM,
                                  MAX(TIMESTAMP) AS TIMESTAMP
                              FROM LatestValues
                              ";
                        break;

                       
                }
               

                DataSet ds = myDb.GetAllData(sql1+sql2);
                DataTable dt = ds.Tables[0];

            
                Function.Core.DisplayData_Set(dt, fpMain);
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

        private void 수집데이터조회_Load(object sender, EventArgs e)
        {
            string mysqlconn = "Server=10.10.10.216;Database=hansoldms;UID=coever;PWD=coever119!";
            string mssqlconn = $"Server=10.10.10.180;Database=HS_MES;UID=hansol_mes;PWD=Hansol123!@#";

            myDb = new DataBase_Class(new MY_DB(mysqlconn));
        }
    }
}
