using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.Data.Mask;
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


        public override void _InitialButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DataTable pDataTable = new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME, fpMain, this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);

                Function.Core.InitializeControl(pDataTable, fpMain, this, _PAN_WHERE, _pMenuSettingEntity);
                수집데이터조회_Load(null, null);
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

                Base_ComboBox text = _PAN_WHERE.Controls[0] as Base_ComboBox;
                string machine_no = text.SearchText;
                Base_FromtoDateTime datetime = _PAN_WHERE.Controls[1] as Base_FromtoDateTime;
                string startTime = datetime.StartValue.ToString("yyyy-MM-dd");
                //string machine_no = 
                string category = "";
                string sql2 = "";
                List<string> list = new List<string>();
                StringBuilder sb = new StringBuilder();
                switch (machine_no)
                {
                    case "13호기":
                        list.Add("DCM_13_TAG_D6900_Ruled");
                        list.Add("DCM_13_TAG_D6902_Ruled");
                        list.Add("DCM_13_TAG_D6904_Ruled");
                        list.Add("DCM_13_TAG_D6906_Ruled");
                        list.Add("DCM_13_TAG_D6908");
                        list.Add("DCM_13_TAG_D6910");
                        list.Add("DCM_13_TAG_D6912_Ruled");
                        list.Add("DCM_13_TAG_D6914");
                        list.Add("DCM_13_TAG_D6916");
                        list.Add("DCM_13_TAG_D6918");
                        list.Add("DCM_13_TAG_D6920_Ruled");
                        list.Add("DCM_13_TAG_D6936_Ruled");
                        list.Add("DCM_13_TAG_D6938_Ruled");
                        list.Add("DCM_13_TAG_D6940_Ruled");
                        list.Add("DCM_13_TAG_D6942_Ruled");
                        list.Add("DCM_13_TAG_D6944_Ruled");
                        list.Add("DCM_13_TAG_D6946_Ruled");
                        list.Add("DCM_13_TAG_D6948_Ruled");
                        list.Add("DCM_13_TAG_D6950_Ruled");
                        list.Add("DCM_13_TAG_D6952_Ruled");
                        list.Add("LS_13_DW816");
                        list.Add("LS_13_DW817");
                        list.Add("LS_13_DW818");
                        list.Add("LS_13_DW819");
                        foreach (string item in list) 
                        {
                            sb.Append($"'{item}',");
                        }
                        sb.Remove(sb.Length - 1, 1);

                        sql2 = $@"SELECT 
                                '13호 주조기' AS MACHINE_NO,
                                COALESCE(MAX(CASE WHEN Category = '{list[0]}' THEN VALUE END), 0) AS V1,
                                COALESCE(MAX(CASE WHEN Category = '{list[1]}' THEN VALUE END), 0) AS V2,
                                COALESCE(MAX(CASE WHEN Category = '{list[2]}' THEN VALUE END), 0) AS V3,
                                COALESCE(MAX(CASE WHEN Category = '{list[3]}' THEN VALUE END), 0) AS V4,
                                COALESCE(MAX(CASE WHEN Category = '{list[4]}' THEN VALUE END), 0) AS ACCELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[5]}' THEN VALUE END), 0) AS DECELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[6]}_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                COALESCE(MAX(CASE WHEN Category = '{list[7]}' THEN VALUE END), 0) AS SWAP_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[8]}' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                COALESCE(MAX(CASE WHEN Category = '{list[9]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                COALESCE(MAX(CASE WHEN Category = '{list[10]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                COALESCE(MAX(CASE WHEN Category = '{list[11]}' THEN VALUE END), 0) AS CYCLE_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[12]}' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[13]}' THEN VALUE END), 0) AS BATH_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[14]}' THEN VALUE END), 0) AS FORWARD_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[15]}' THEN VALUE END), 0) AS FREEZING_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[16]}' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[17]}' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[18]}' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[19]}' THEN VALUE END), 0) AS SPRAY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[20]}' THEN VALUE END), 0) AS CAVITY_CORE,
                                COALESCE(MAX(CASE WHEN Category = '{list[21]}' THEN VALUE END), 0) AS A_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[22]}' THEN VALUE END), 0) AS B_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[23]}' THEN VALUE END), 0) AS VACUUM,
                                MAX(TIMESTAMP) AS TIMESTAMP
                            FROM 
                            ";
                        break;

                    case "21호기":
                        list.Add("DCM_21_TAG_D6900_Ruled");
                        list.Add("DCM_21_TAG_D6902_Ruled");
                        list.Add("DCM_21_TAG_D6904_Ruled");
                        list.Add("DCM_21_TAG_D6906_Ruled");
                        list.Add("DCM_21_TAG_D6908");
                        list.Add("DCM_21_TAG_D6910");
                        list.Add("DCM_21_TAG_D6912_Ruled");
                        list.Add("DCM_21_TAG_D6914");
                        list.Add("DCM_21_TAG_D6916");
                        list.Add("DCM_21_TAG_D6918");
                        list.Add("DCM_21_TAG_D6920_Ruled");
                        list.Add("DCM_21_TAG_D6936_Ruled");
                        list.Add("DCM_21_TAG_D6938_Ruled");
                        list.Add("DCM_21_TAG_D6940_Ruled");
                        list.Add("DCM_21_TAG_D6942_Ruled");
                        list.Add("DCM_21_TAG_D6944_Ruled");
                        list.Add("DCM_21_TAG_D6946_Ruled");
                        list.Add("DCM_21_TAG_D6948_Ruled");
                        list.Add("DCM_21_TAG_D6950_Ruled");
                        list.Add("DCM_21_TAG_D6952_Ruled");
                        list.Add("LS_21_DW816");
                        list.Add("LS_21_DW817");
                        list.Add("LS_21_DW818");
                        list.Add("LS_21_DW819");
                        foreach (string item in list)
                        {
                            sb.Append($"'{item}',");
                        }
                        sb.Remove(sb.Length - 1, 1);

                        sql2 = $@"SELECT 
                                '21호 주조기' AS MACHINE_NO,
                               COALESCE(MAX(CASE WHEN Category = '{list[0]}' THEN VALUE END), 0) AS V1,
                                COALESCE(MAX(CASE WHEN Category = '{list[1]}' THEN VALUE END), 0) AS V2,
                                COALESCE(MAX(CASE WHEN Category = '{list[2]}' THEN VALUE END), 0) AS V3,
                                COALESCE(MAX(CASE WHEN Category = '{list[3]}' THEN VALUE END), 0) AS V4,
                                COALESCE(MAX(CASE WHEN Category = '{list[4]}' THEN VALUE END), 0) AS ACCELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[5]}' THEN VALUE END), 0) AS DECELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[6]}_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                COALESCE(MAX(CASE WHEN Category = '{list[7]}' THEN VALUE END), 0) AS SWAP_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[8]}' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                COALESCE(MAX(CASE WHEN Category = '{list[9]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                COALESCE(MAX(CASE WHEN Category = '{list[10]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                COALESCE(MAX(CASE WHEN Category = '{list[11]}' THEN VALUE END), 0) AS CYCLE_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[12]}' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[13]}' THEN VALUE END), 0) AS BATH_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[14]}' THEN VALUE END), 0) AS FORWARD_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[15]}' THEN VALUE END), 0) AS FREEZING_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[16]}' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[17]}' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[18]}' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[19]}' THEN VALUE END), 0) AS SPRAY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[20]}' THEN VALUE END), 0) AS CAVITY_CORE,
                                COALESCE(MAX(CASE WHEN Category = '{list[21]}' THEN VALUE END), 0) AS A_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[22]}' THEN VALUE END), 0) AS B_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[23]}' THEN VALUE END), 0) AS VACUUM,
                                MAX(TIMESTAMP) AS TIMESTAMP
                            FROM 
                            ";
                        break;
                    case "22호기":
                        list.Add("DCM_22_TAG_D6900_Ruled");
                        list.Add("DCM_22_TAG_D6902_Ruled");
                        list.Add("DCM_22_TAG_D6904_Ruled");
                        list.Add("DCM_22_TAG_D6906_Ruled");
                        list.Add("DCM_22_TAG_D6908");
                        list.Add("DCM_22_TAG_D6910");
                        list.Add("DCM_22_TAG_D6912_Ruled");
                        list.Add("DCM_22_TAG_D6914");
                        list.Add("DCM_22_TAG_D6916");
                        list.Add("DCM_22_TAG_D6918");
                        list.Add("DCM_22_TAG_D6920_Ruled");
                        list.Add("DCM_22_TAG_D6936_Ruled");
                        list.Add("DCM_22_TAG_D6938_Ruled");
                        list.Add("DCM_22_TAG_D6940_Ruled");
                        list.Add("DCM_22_TAG_D6942_Ruled");
                        list.Add("DCM_22_TAG_D6944_Ruled");
                        list.Add("DCM_22_TAG_D6946_Ruled");
                        list.Add("DCM_22_TAG_D6948_Ruled");
                        list.Add("DCM_22_TAG_D6950_Ruled");
                        list.Add("DCM_22_TAG_D6952_Ruled");
                        list.Add("LS_22_DW816");
                        list.Add("LS_22_DW817");
                        list.Add("LS_22_DW818");
                        list.Add("LS_22_DW819");
                        foreach (string item in list)
                        {
                            sb.Append($"'{item}',");
                        }
                        sb.Remove(sb.Length - 1, 1);

                        sql2 = $@"SELECT 
                                '22호 주조기' AS MACHINE_NO,
                               COALESCE(MAX(CASE WHEN Category = '{list[0]}' THEN VALUE END), 0) AS V1,
                                COALESCE(MAX(CASE WHEN Category = '{list[1]}' THEN VALUE END), 0) AS V2,
                                COALESCE(MAX(CASE WHEN Category = '{list[2]}' THEN VALUE END), 0) AS V3,
                                COALESCE(MAX(CASE WHEN Category = '{list[3]}' THEN VALUE END), 0) AS V4,
                                COALESCE(MAX(CASE WHEN Category = '{list[4]}' THEN VALUE END), 0) AS ACCELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[5]}' THEN VALUE END), 0) AS DECELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[6]}_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                COALESCE(MAX(CASE WHEN Category = '{list[7]}' THEN VALUE END), 0) AS SWAP_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[8]}' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                COALESCE(MAX(CASE WHEN Category = '{list[9]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                COALESCE(MAX(CASE WHEN Category = '{list[10]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                COALESCE(MAX(CASE WHEN Category = '{list[11]}' THEN VALUE END), 0) AS CYCLE_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[12]}' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[13]}' THEN VALUE END), 0) AS BATH_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[14]}' THEN VALUE END), 0) AS FORWARD_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[15]}' THEN VALUE END), 0) AS FREEZING_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[16]}' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[17]}' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[18]}' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[19]}' THEN VALUE END), 0) AS SPRAY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[20]}' THEN VALUE END), 0) AS CAVITY_CORE,
                                COALESCE(MAX(CASE WHEN Category = '{list[21]}' THEN VALUE END), 0) AS A_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[22]}' THEN VALUE END), 0) AS B_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[23]}' THEN VALUE END), 0) AS VACUUM,
                                MAX(TIMESTAMP) AS TIMESTAMP
                            FROM 
                            ";
                        break;
                    case "23호기":
                        list.Add("DCM_23_TAG_D6900_Ruled");
                        list.Add("DCM_23_TAG_D6902_Ruled");
                        list.Add("DCM_23_TAG_D6904_Ruled");
                        list.Add("DCM_23_TAG_D6906_Ruled");
                        list.Add("DCM_23_TAG_D6908");
                        list.Add("DCM_23_TAG_D6910");
                        list.Add("DCM_23_TAG_D6912_Ruled");
                        list.Add("DCM_23_TAG_D6914");
                        list.Add("DCM_23_TAG_D6916");
                        list.Add("DCM_23_TAG_D6918");
                        list.Add("DCM_23_TAG_D6920_Ruled");
                        list.Add("DCM_23_TAG_D6936_Ruled");
                        list.Add("DCM_23_TAG_D6938_Ruled");
                        list.Add("DCM_23_TAG_D6940_Ruled");
                        list.Add("DCM_23_TAG_D6942_Ruled");
                        list.Add("DCM_23_TAG_D6944_Ruled");
                        list.Add("DCM_23_TAG_D6946_Ruled");
                        list.Add("DCM_23_TAG_D6948_Ruled");
                        list.Add("DCM_23_TAG_D6950_Ruled");
                        list.Add("DCM_23_TAG_D6952_Ruled");
                        list.Add("LS_23_DW816");
                        list.Add("LS_23_DW817");
                        list.Add("LS_23_DW818");
                        list.Add("LS_23_DW819");
                        foreach (string item in list)
                        {
                            sb.Append($"'{item}',");
                        }
                        sb.Remove(sb.Length - 1, 1);

                        sql2 = $@"SELECT 
                                '23호 주조기' AS MACHINE_NO,
                                COALESCE(MAX(CASE WHEN Category = '{list[0]}' THEN VALUE END), 0) AS V1,
                                COALESCE(MAX(CASE WHEN Category = '{list[1]}' THEN VALUE END), 0) AS V2,
                                COALESCE(MAX(CASE WHEN Category = '{list[2]}' THEN VALUE END), 0) AS V3,
                                COALESCE(MAX(CASE WHEN Category = '{list[3]}' THEN VALUE END), 0) AS V4,
                                COALESCE(MAX(CASE WHEN Category = '{list[4]}' THEN VALUE END), 0) AS ACCELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[5]}' THEN VALUE END), 0) AS DECELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[6]}_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                COALESCE(MAX(CASE WHEN Category = '{list[7]}' THEN VALUE END), 0) AS SWAP_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[8]}' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                COALESCE(MAX(CASE WHEN Category = '{list[9]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                COALESCE(MAX(CASE WHEN Category = '{list[10]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                COALESCE(MAX(CASE WHEN Category = '{list[11]}' THEN VALUE END), 0) AS CYCLE_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[12]}' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[13]}' THEN VALUE END), 0) AS BATH_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[14]}' THEN VALUE END), 0) AS FORWARD_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[15]}' THEN VALUE END), 0) AS FREEZING_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[16]}' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[17]}' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[18]}' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[19]}' THEN VALUE END), 0) AS SPRAY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[20]}' THEN VALUE END), 0) AS CAVITY_CORE,
                                COALESCE(MAX(CASE WHEN Category = '{list[21]}' THEN VALUE END), 0) AS A_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[22]}' THEN VALUE END), 0) AS B_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[23]}' THEN VALUE END), 0) AS VACUUM,
                                MAX(TIMESTAMP) AS TIMESTAMP
                            FROM 
                            ";
                        break;
                    case "24호기":
                        list.Add("DCM_24_TAG_D6900_Ruled");
                        list.Add("DCM_24_TAG_D6902_Ruled");
                        list.Add("DCM_24_TAG_D6904_Ruled");
                        list.Add("DCM_24_TAG_D6906_Ruled");
                        list.Add("DCM_24_TAG_D6908");
                        list.Add("DCM_24_TAG_D6910");
                        list.Add("DCM_24_TAG_D6912_Ruled");
                        list.Add("DCM_24_TAG_D6914");
                        list.Add("DCM_24_TAG_D6916");
                        list.Add("DCM_24_TAG_D6918");
                        list.Add("DCM_24_TAG_D6920_Ruled");
                        list.Add("DCM_24_TAG_D6936_Ruled");
                        list.Add("DCM_24_TAG_D6938_Ruled");
                        list.Add("DCM_24_TAG_D6940_Ruled");
                        list.Add("DCM_24_TAG_D6942_Ruled");
                        list.Add("DCM_24_TAG_D6944_Ruled");
                        list.Add("DCM_24_TAG_D6946_Ruled");
                        list.Add("DCM_24_TAG_D6948_Ruled");
                        list.Add("DCM_24_TAG_D6950_Ruled");
                        list.Add("DCM_24_TAG_D6952_Ruled");
                        list.Add("LS_24_DW816");
                        list.Add("LS_24_DW817");
                        list.Add("LS_24_DW818");
                        list.Add("LS_24_DW819");
                        foreach (string item in list)
                        {
                            sb.Append($"'{item}',");
                        }
                        sb.Remove(sb.Length - 1, 1);

                        sql2 = $@"SELECT 
                                '24호 주조기' AS MACHINE_NO,
                                COALESCE(MAX(CASE WHEN Category = '{list[0]}' THEN VALUE END), 0) AS V1,
                                COALESCE(MAX(CASE WHEN Category = '{list[1]}' THEN VALUE END), 0) AS V2,
                                COALESCE(MAX(CASE WHEN Category = '{list[2]}' THEN VALUE END), 0) AS V3,
                                COALESCE(MAX(CASE WHEN Category = '{list[3]}' THEN VALUE END), 0) AS V4,
                                COALESCE(MAX(CASE WHEN Category = '{list[4]}' THEN VALUE END), 0) AS ACCELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[5]}' THEN VALUE END), 0) AS DECELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[6]}_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                COALESCE(MAX(CASE WHEN Category = '{list[7]}' THEN VALUE END), 0) AS SWAP_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[8]}' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                COALESCE(MAX(CASE WHEN Category = '{list[9]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                COALESCE(MAX(CASE WHEN Category = '{list[10]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                COALESCE(MAX(CASE WHEN Category = '{list[11]}' THEN VALUE END), 0) AS CYCLE_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[12]}' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[13]}' THEN VALUE END), 0) AS BATH_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[14]}' THEN VALUE END), 0) AS FORWARD_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[15]}' THEN VALUE END), 0) AS FREEZING_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[16]}' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[17]}' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[18]}' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[19]}' THEN VALUE END), 0) AS SPRAY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[20]}' THEN VALUE END), 0) AS CAVITY_CORE,
                                COALESCE(MAX(CASE WHEN Category = '{list[21]}' THEN VALUE END), 0) AS A_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[22]}' THEN VALUE END), 0) AS B_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[23]}' THEN VALUE END), 0) AS VACUUM,
                                MAX(TIMESTAMP) AS TIMESTAMP
                            FROM 
                            ";
                        break;
                    case "25호기":
                        list.Add("DCM_25_TAG_D6900_Ruled");
                        list.Add("DCM_25_TAG_D6902_Ruled");
                        list.Add("DCM_25_TAG_D6904_Ruled");
                        list.Add("DCM_25_TAG_D6906_Ruled");
                        list.Add("DCM_25_TAG_D6908");
                        list.Add("DCM_25_TAG_D6910");
                        list.Add("DCM_25_TAG_D6912_Ruled");
                        list.Add("DCM_25_TAG_D6914");
                        list.Add("DCM_25_TAG_D6916");
                        list.Add("DCM_25_TAG_D6918");
                        list.Add("DCM_25_TAG_D6920_Ruled");
                        list.Add("DCM_25_TAG_D6936_Ruled");
                        list.Add("DCM_25_TAG_D6938_Ruled");
                        list.Add("DCM_25_TAG_D6940_Ruled");
                        list.Add("DCM_25_TAG_D6942_Ruled");
                        list.Add("DCM_25_TAG_D6944_Ruled");
                        list.Add("DCM_25_TAG_D6946_Ruled");
                        list.Add("DCM_25_TAG_D6948_Ruled");
                        list.Add("DCM_25_TAG_D6950_Ruled");
                        list.Add("DCM_25_TAG_D6952_Ruled");
                        list.Add("LS_25_DW186");
                        list.Add("LS_25_DW187");
                        list.Add("LS_25_DW188");
                        list.Add("LS_25_DW189");
                        foreach (string item in list)
                        {
                            sb.Append($"'{item}',");
                        }
                        sb.Remove(sb.Length - 1, 1);

                        sql2 = $@"SELECT 
                                '21호 주조기' AS MACHINE_NO,
                                COALESCE(MAX(CASE WHEN Category = '{list[0]}' THEN VALUE END), 0) AS V1,
                                COALESCE(MAX(CASE WHEN Category = '{list[1]}' THEN VALUE END), 0) AS V2,
                                COALESCE(MAX(CASE WHEN Category = '{list[2]}' THEN VALUE END), 0) AS V3,
                                COALESCE(MAX(CASE WHEN Category = '{list[3]}' THEN VALUE END), 0) AS V4,
                                COALESCE(MAX(CASE WHEN Category = '{list[4]}' THEN VALUE END), 0) AS ACCELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[5]}' THEN VALUE END), 0) AS DECELERATION_POS,
                                COALESCE(MAX(CASE WHEN Category = '{list[6]}_Ruled' THEN VALUE END), 0) AS METAL_PRESSURE,
                                COALESCE(MAX(CASE WHEN Category = '{list[7]}' THEN VALUE END), 0) AS SWAP_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[8]}' THEN VALUE END), 0) AS BISKIT_THICKNESS,
                                COALESCE(MAX(CASE WHEN Category = '{list[9]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_PER,
                                COALESCE(MAX(CASE WHEN Category = '{list[10]}' THEN VALUE END), 0) AS PHYSICAL_STRENGTH_MN,
                                COALESCE(MAX(CASE WHEN Category = '{list[11]}' THEN VALUE END), 0) AS CYCLE_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[12]}' THEN VALUE END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[13]}' THEN VALUE END), 0) AS BATH_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[14]}' THEN VALUE END), 0) AS FORWARD_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[15]}' THEN VALUE END), 0) AS FREEZING_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[16]}' THEN VALUE END), 0) AS TYPE_WEIGHT_BACK_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[17]}' THEN VALUE END), 0) AS EXTRUSION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[18]}' THEN VALUE END), 0) AS EXTRACTION_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[19]}' THEN VALUE END), 0) AS SPRAY_TIME,
                                COALESCE(MAX(CASE WHEN Category = '{list[20]}' THEN VALUE END), 0) AS CAVITY_CORE,
                                COALESCE(MAX(CASE WHEN Category = '{list[21]}' THEN VALUE END), 0) AS A_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[22]}' THEN VALUE END), 0) AS B_POLLUTION_DEGREE,
                                COALESCE(MAX(CASE WHEN Category = '{list[23]}' THEN VALUE END), 0) AS VACUUM,
                                TIMESTAMP AS TIMESTAMP
                            FROM 
                            ";
                        break;
                }
                category = sb.ToString();
                string sql1 = $@" timeseriesdata WHERE Timestamp >= '2024-11-01 00:00:00' AND Timestamp <= '2024-11-01 23:59:59' 
                                AND Category in ('DCM_21_TAG_D6900_Ruled','DCM_21_TAG_D6902_Ruled','DCM_21_TAG_D6904_Ruled','DCM_21_TAG_D6906_Ruled','DCM_21_TAG_D6908','DCM_21_TAG_D6910','DCM_21_TAG_D6912_Ruled','DCM_21_TAG_D6914','DCM_21_TAG_D6916','DCM_21_TAG_D6918','DCM_21_TAG_D6920_Ruled','DCM_21_TAG_D6936_Ruled','DCM_21_TAG_D6938_Ruled','DCM_21_TAG_D6940_Ruled','DCM_21_TAG_D6942_Ruled','DCM_21_TAG_D6944_Ruled','DCM_21_TAG_D6946_Ruled','DCM_21_TAG_D6948_Ruled','DCM_21_TAG_D6950_Ruled','DCM_21_TAG_D6952_Ruled','LS_21_DW816','LS_21_DW817','LS_21_DW818','LS_21_DW819')
                                GROUP BY TIMESTAMP
                                ORDER BY TIMESTAMP
                               ;";

            

                string mixSql = sql2 + sql1;

                DataSet ds = myDb.GetAllData(mixSql);
                
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
            Base_ComboBox text = _PAN_WHERE.Controls[0] as Base_ComboBox;
            Base_FromtoDateTime datetime = _PAN_WHERE.Controls[1] as Base_FromtoDateTime;
            datetime.OnlyUseOneBox();
            text._SearchCom.DeleteItemByIndex(0);
            text._SearchCom.ItemIndex = 0;

        }
    }
}
