﻿using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Function;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Data.Mask;
using DevExpress.DataAccess.Native.Sql;
using DevExpress.XtraRichEdit.Import.Html;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CoFAS.NEW.MES.Core.Entity;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarPoint.Excel;

namespace CoFAS.NEW.MES.Core
{
    public partial class 수집데이터조회 : BaseForm1
    {
        DataBase_Class myDb;
        DataBase_Class msDb;

        public 수집데이터조회()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            DevExpressManager.SetCursor(this, Cursors.WaitCursor);
            fpMain.Sheets[0].Rows.Count = 0;

            string mysqlconn = $"Server=10.10.10.216;Database=hansoldms;UID=coever;PWD=coever119!";
            myDb = new DataBase_Class(new MY_DB(mysqlconn));

        }

        public override void _InitialButtonClicked(object sender, EventArgs e)
        {
            try
            {
                DataTable pDataTable = new CoreBusiness().BASE_MENU_SETTING_R10(this._pMenuSettingEntity.MENU_WINDOW_NAME, fpMain, this._pMenuSettingEntity.BASE_TABLE.Split('/')[0]);

                Function.Core.InitializeControl(pDataTable, fpMain, this, _PAN_WHERE, _pMenuSettingEntity);
                Form_Load(null, null);
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
                string machine_id = text.SearchText;
                //여기에 
                switch (machine_id)
                {
                    case "13호기":
                        machine_id = "'WCI_D13'";

                        break;
                    case "21호기":
                        machine_id = "'WCI_D21'";

                        break;
                    case "22호기":
                        machine_id = "'WCI_D22'";

                        break;
                    case "23호기":
                        machine_id = "'WCI_D23'";

                        break;
                    case "24호기":
                        machine_id = "'WCI_D24'";

                        break;

                    case "25호기":
                        machine_id = "'WCI_D25'";

                        break;
                    default : machine_id = "NULL";
                        break;

                }

                Base_FromtoDateTime datetime = _PAN_WHERE.Controls[1] as Base_FromtoDateTime;
                string strTime = datetime.StartValue.ToString("yyyy-MM-dd");
                string endTime = datetime.EndValue.ToString("yyyy-MM-dd");

                string getMySqlDataGrid =   $" SELECT T2.ID                                                    " +
                                            $"      , T2.ORDER_NO                                              " +
                                            $"      , T2.START_TIME                                            " +
                                            $"      , T2.END_TIME                                              " +
                                            $"      , T1.*                                                     " +
                                            $"   FROM data_for_grid   T1                                       " +
                                            $"      , WORK_PERFORMANCE T2                                                     " +
                                            $"  WHERE T1.machine_no = T2.MACHINE_NO                            " +
                                            $"    AND T1.DATE BETWEEN T2.START_TIME AND T2.END_TIME                                   " +
                                            $"    AND T1.DATE BETWEEN '{strTime}' AND '{endTime}'                                    " +
                                            $"    AND T1.MACHINE_NO = IFNULL({machine_id}, T1.MACHINE_NO)                                   " +
                                            $"  ORDER BY T2.ORDER_NO, T1.DATE " ;

                DataSet ds = myDb.GetAllData(getMySqlDataGrid);
                DataTable dt = ds.Tables[0];
                Function.Core.DisplayData_Set(dt, fpMain);

                #region [수정전]

                //string category = "";
                //string sql2 = "";
                //List<string> list = new List<string>();
                //StringBuilder sb = new StringBuilder();
                //switch (machine_no)
                //{
                //    case "13호기":
                //        list.Add("DCM_13_TAG_D6900_Ruled");
                //        list.Add("DCM_13_TAG_D6902_Ruled");
                //        list.Add("DCM_13_TAG_D6904_Ruled");
                //        list.Add("DCM_13_TAG_D6906_Ruled");
                //        list.Add("DCM_13_TAG_D6908");
                //        list.Add("DCM_13_TAG_D6910");
                //        list.Add("DCM_13_TAG_D6912_Ruled");
                //        list.Add("DCM_13_TAG_D6914");
                //        list.Add("DCM_13_TAG_D6916");
                //        list.Add("DCM_13_TAG_D6918");
                //        list.Add("DCM_13_TAG_D6920_Ruled");
                //        list.Add("DCM_13_TAG_D6936_Ruled");
                //        list.Add("DCM_13_TAG_D6938_Ruled");
                //        list.Add("DCM_13_TAG_D6940_Ruled");
                //        list.Add("DCM_13_TAG_D6942_Ruled");
                //        list.Add("DCM_13_TAG_D6944_Ruled");
                //        list.Add("DCM_13_TAG_D6946_Ruled");
                //        list.Add("DCM_13_TAG_D6948_Ruled");
                //        list.Add("DCM_13_TAG_D6950_Ruled");
                //        list.Add("DCM_13_TAG_D6952_Ruled");
                //        list.Add("LS_13_DW816");
                //        list.Add("LS_13_DW817");
                //        list.Add("LS_13_DW818");
                //        list.Add("LS_13_DW819");
                //        foreach (string item in list) 
                //        {
                //            sb.Append($"'{item}',");
                //        }
                //        sb.Remove(sb.Length - 1, 1);

                //        sql2 = $@"SELECT 
                //                '13호 주조기' AS MACHINE_NO,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[0]}' THEN Vl END), 0) AS V1,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[1]}' THEN Vl END), 0) AS V2,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[2]}' THEN Vl END), 0) AS V3,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[3]}' THEN Vl END), 0) AS V4,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[4]}' THEN Vl END), 0) AS ACCELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[5]}' THEN Vl END), 0) AS DECELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[6]}' THEN Vl END), 0) AS METAL_PRESSURE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[7]}' THEN Vl END), 0) AS SWAP_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[8]}' THEN Vl END), 0) AS BISKIT_THICKNESS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[9]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_PER,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[10]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_MN,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[11]}' THEN Vl END), 0) AS CYCLE_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[12]}' THEN Vl END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[13]}' THEN Vl END), 0) AS BATH_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[14]}' THEN Vl END), 0) AS FORWARD_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[15]}' THEN Vl END), 0) AS FREEZING_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[16]}' THEN Vl END), 0) AS TYPE_WEIGHT_BACK_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[17]}' THEN Vl END), 0) AS EXTRUSION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[18]}' THEN Vl END), 0) AS EXTRACTION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[19]}' THEN Vl END), 0) AS SPRAY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[20]}' THEN Vl END), 0) AS CAVITY_CORE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[21]}' THEN Vl END), 0) AS A_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[22]}' THEN Vl END), 0) AS B_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[23]}' THEN Vl END), 0) AS VACUUM,
                //                DATE_FORMAT(Tm, '%Y-%m-%d %H:%i:%s') AS TIMESTAMP
                //            FROM 
                //            ";
                //        break;

                //    case "21호기":
                //        list.Add("DCM_21_TAG_D6900_Ruled");
                //        list.Add("DCM_21_TAG_D6902_Ruled");
                //        list.Add("DCM_21_TAG_D6904_Ruled");
                //        list.Add("DCM_21_TAG_D6906_Ruled");
                //        list.Add("DCM_21_TAG_D6908");
                //        list.Add("DCM_21_TAG_D6910");
                //        list.Add("DCM_21_TAG_D6912_Ruled");
                //        list.Add("DCM_21_TAG_D6914");
                //        list.Add("DCM_21_TAG_D6916");
                //        list.Add("DCM_21_TAG_D6918");
                //        list.Add("DCM_21_TAG_D6920_Ruled");
                //        list.Add("DCM_21_TAG_D6936_Ruled");
                //        list.Add("DCM_21_TAG_D6938_Ruled");
                //        list.Add("DCM_21_TAG_D6940_Ruled");
                //        list.Add("DCM_21_TAG_D6942_Ruled");
                //        list.Add("DCM_21_TAG_D6944_Ruled");
                //        list.Add("DCM_21_TAG_D6946_Ruled");
                //        list.Add("DCM_21_TAG_D6948_Ruled");
                //        list.Add("DCM_21_TAG_D6950_Ruled");
                //        list.Add("DCM_21_TAG_D6952_Ruled");
                //        list.Add("LS_21_DW816");
                //        list.Add("LS_21_DW817");
                //        list.Add("LS_21_DW818");
                //        list.Add("LS_21_DW819");
                //        foreach (string item in list)
                //        {
                //            sb.Append($"'{item}',");
                //        }
                //        sb.Remove(sb.Length - 1, 1);

                //        sql2 = $@"SELECT 
                //                '21호 주조기' AS MACHINE_NO,
                //               COALESCE(MAX(CASE WHEN Cd = '{list[0]}' THEN Vl END), 0) AS V1,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[1]}' THEN Vl END), 0) AS V2,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[2]}' THEN Vl END), 0) AS V3,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[3]}' THEN Vl END), 0) AS V4,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[4]}' THEN Vl END), 0) AS ACCELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[5]}' THEN Vl END), 0) AS DECELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[6]}' THEN Vl END), 0) AS METAL_PRESSURE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[7]}' THEN Vl END), 0) AS SWAP_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[8]}' THEN Vl END), 0) AS BISKIT_THICKNESS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[9]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_PER,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[10]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_MN,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[11]}' THEN Vl END), 0) AS CYCLE_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[12]}' THEN Vl END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[13]}' THEN Vl END), 0) AS BATH_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[14]}' THEN Vl END), 0) AS FORWARD_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[15]}' THEN Vl END), 0) AS FREEZING_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[16]}' THEN Vl END), 0) AS TYPE_WEIGHT_BACK_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[17]}' THEN Vl END), 0) AS EXTRUSION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[18]}' THEN Vl END), 0) AS EXTRACTION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[19]}' THEN Vl END), 0) AS SPRAY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[20]}' THEN Vl END), 0) AS CAVITY_CORE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[21]}' THEN Vl END), 0) AS A_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[22]}' THEN Vl END), 0) AS B_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[23]}' THEN Vl END), 0) AS VACUUM,
                //                DATE_FORMAT(Tm, '%Y-%m-%d %H:%i:%s') AS TIMESTAMP
                //            FROM 
                //            ";
                //        break;
                //    case "22호기":
                //        list.Add("DCM_22_TAG_D6900_Ruled");
                //        list.Add("DCM_22_TAG_D6902_Ruled");
                //        list.Add("DCM_22_TAG_D6904_Ruled");
                //        list.Add("DCM_22_TAG_D6906_Ruled");
                //        list.Add("DCM_22_TAG_D6908");
                //        list.Add("DCM_22_TAG_D6910");
                //        list.Add("DCM_22_TAG_D6912_Ruled");
                //        list.Add("DCM_22_TAG_D6914");
                //        list.Add("DCM_22_TAG_D6916");
                //        list.Add("DCM_22_TAG_D6918");
                //        list.Add("DCM_22_TAG_D6920_Ruled");
                //        list.Add("DCM_22_TAG_D6936_Ruled");
                //        list.Add("DCM_22_TAG_D6938_Ruled");
                //        list.Add("DCM_22_TAG_D6940_Ruled");
                //        list.Add("DCM_22_TAG_D6942_Ruled");
                //        list.Add("DCM_22_TAG_D6944_Ruled");
                //        list.Add("DCM_22_TAG_D6946_Ruled");
                //        list.Add("DCM_22_TAG_D6948_Ruled");
                //        list.Add("DCM_22_TAG_D6950_Ruled");
                //        list.Add("DCM_22_TAG_D6952_Ruled");
                //        list.Add("LS_22_DW816");
                //        list.Add("LS_22_DW817");
                //        list.Add("LS_22_DW818");
                //        list.Add("LS_22_DW819");
                //        foreach (string item in list)
                //        {
                //            sb.Append($"'{item}',");
                //        }
                //        sb.Remove(sb.Length - 1, 1);

                //        sql2 = $@"SELECT 
                //                '22호 주조기' AS MACHINE_NO,
                //               COALESCE(MAX(CASE WHEN Cd = '{list[0]}' THEN Vl END), 0) AS V1,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[1]}' THEN Vl END), 0) AS V2,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[2]}' THEN Vl END), 0) AS V3,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[3]}' THEN Vl END), 0) AS V4,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[4]}' THEN Vl END), 0) AS ACCELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[5]}' THEN Vl END), 0) AS DECELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[6]}' THEN Vl END), 0) AS METAL_PRESSURE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[7]}' THEN Vl END), 0) AS SWAP_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[8]}' THEN Vl END), 0) AS BISKIT_THICKNESS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[9]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_PER,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[10]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_MN,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[11]}' THEN Vl END), 0) AS CYCLE_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[12]}' THEN Vl END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[13]}' THEN Vl END), 0) AS BATH_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[14]}' THEN Vl END), 0) AS FORWARD_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[15]}' THEN Vl END), 0) AS FREEZING_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[16]}' THEN Vl END), 0) AS TYPE_WEIGHT_BACK_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[17]}' THEN Vl END), 0) AS EXTRUSION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[18]}' THEN Vl END), 0) AS EXTRACTION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[19]}' THEN Vl END), 0) AS SPRAY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[20]}' THEN Vl END), 0) AS CAVITY_CORE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[21]}' THEN Vl END), 0) AS A_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[22]}' THEN Vl END), 0) AS B_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[23]}' THEN Vl END), 0) AS VACUUM,
                //                DATE_FORMAT(Tm, '%Y-%m-%d %H:%i:%s') AS TIMESTAMP
                //            FROM 
                //            ";
                //        break;
                //    case "23호기":
                //        list.Add("DCM_23_TAG_D6900_Ruled");
                //        list.Add("DCM_23_TAG_D6902_Ruled");
                //        list.Add("DCM_23_TAG_D6904_Ruled");
                //        list.Add("DCM_23_TAG_D6906_Ruled");
                //        list.Add("DCM_23_TAG_D6908");
                //        list.Add("DCM_23_TAG_D6910");
                //        list.Add("DCM_23_TAG_D6912_Ruled");
                //        list.Add("DCM_23_TAG_D6914");
                //        list.Add("DCM_23_TAG_D6916");
                //        list.Add("DCM_23_TAG_D6918");
                //        list.Add("DCM_23_TAG_D6920_Ruled");
                //        list.Add("DCM_23_TAG_D6936_Ruled");
                //        list.Add("DCM_23_TAG_D6938_Ruled");
                //        list.Add("DCM_23_TAG_D6940_Ruled");
                //        list.Add("DCM_23_TAG_D6942_Ruled");
                //        list.Add("DCM_23_TAG_D6944_Ruled");
                //        list.Add("DCM_23_TAG_D6946_Ruled");
                //        list.Add("DCM_23_TAG_D6948_Ruled");
                //        list.Add("DCM_23_TAG_D6950_Ruled");
                //        list.Add("DCM_23_TAG_D6952_Ruled");
                //        list.Add("LS_23_DW816");
                //        list.Add("LS_23_DW817");
                //        list.Add("LS_23_DW818");
                //        list.Add("LS_23_DW819");
                //        foreach (string item in list)
                //        {
                //            sb.Append($"'{item}',");
                //        }
                //        sb.Remove(sb.Length - 1, 1);

                //        sql2 = $@"SELECT 
                //                '23호 주조기' AS MACHINE_NO,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[0]}' THEN Vl END), 0) AS V1,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[1]}' THEN Vl END), 0) AS V2,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[2]}' THEN Vl END), 0) AS V3,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[3]}' THEN Vl END), 0) AS V4,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[4]}' THEN Vl END), 0) AS ACCELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[5]}' THEN Vl END), 0) AS DECELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[6]}' THEN Vl END), 0) AS METAL_PRESSURE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[7]}' THEN Vl END), 0) AS SWAP_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[8]}' THEN Vl END), 0) AS BISKIT_THICKNESS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[9]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_PER,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[10]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_MN,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[11]}' THEN Vl END), 0) AS CYCLE_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[12]}' THEN Vl END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[13]}' THEN Vl END), 0) AS BATH_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[14]}' THEN Vl END), 0) AS FORWARD_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[15]}' THEN Vl END), 0) AS FREEZING_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[16]}' THEN Vl END), 0) AS TYPE_WEIGHT_BACK_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[17]}' THEN Vl END), 0) AS EXTRUSION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[18]}' THEN Vl END), 0) AS EXTRACTION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[19]}' THEN Vl END), 0) AS SPRAY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[20]}' THEN Vl END), 0) AS CAVITY_CORE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[21]}' THEN Vl END), 0) AS A_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[22]}' THEN Vl END), 0) AS B_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[23]}' THEN Vl END), 0) AS VACUUM,
                //                DATE_FORMAT(Tm, '%Y-%m-%d %H:%i:%s') AS TIMESTAMP
                //            FROM 
                //            ";
                //        break;
                //    case "24호기":
                //        list.Add("DCM_24_TAG_D6900_Ruled");
                //        list.Add("DCM_24_TAG_D6902_Ruled");
                //        list.Add("DCM_24_TAG_D6904_Ruled");
                //        list.Add("DCM_24_TAG_D6906_Ruled");
                //        list.Add("DCM_24_TAG_D6908");
                //        list.Add("DCM_24_TAG_D6910");
                //        list.Add("DCM_24_TAG_D6912_Ruled");
                //        list.Add("DCM_24_TAG_D6914");
                //        list.Add("DCM_24_TAG_D6916");
                //        list.Add("DCM_24_TAG_D6918");
                //        list.Add("DCM_24_TAG_D6920_Ruled");
                //        list.Add("DCM_24_TAG_D6936_Ruled");
                //        list.Add("DCM_24_TAG_D6938_Ruled");
                //        list.Add("DCM_24_TAG_D6940_Ruled");
                //        list.Add("DCM_24_TAG_D6942_Ruled");
                //        list.Add("DCM_24_TAG_D6944_Ruled");
                //        list.Add("DCM_24_TAG_D6946_Ruled");
                //        list.Add("DCM_24_TAG_D6948_Ruled");
                //        list.Add("DCM_24_TAG_D6950_Ruled");
                //        list.Add("DCM_24_TAG_D6952_Ruled");
                //        list.Add("LS_24_DW816");
                //        list.Add("LS_24_DW817");
                //        list.Add("LS_24_DW818");
                //        list.Add("LS_24_DW819");
                //        foreach (string item in list)
                //        {
                //            sb.Append($"'{item}',");
                //        }
                //        sb.Remove(sb.Length - 1, 1);

                //        sql2 = $@"SELECT 
                //                '24호 주조기' AS MACHINE_NO,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[0]}' THEN Vl END), 0) AS V1,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[1]}' THEN Vl END), 0) AS V2,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[2]}' THEN Vl END), 0) AS V3,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[3]}' THEN Vl END), 0) AS V4,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[4]}' THEN Vl END), 0) AS ACCELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[5]}' THEN Vl END), 0) AS DECELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[6]}' THEN Vl END), 0) AS METAL_PRESSURE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[7]}' THEN Vl END), 0) AS SWAP_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[8]}' THEN Vl END), 0) AS BISKIT_THICKNESS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[9]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_PER,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[10]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_MN,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[11]}' THEN Vl END), 0) AS CYCLE_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[12]}' THEN Vl END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[13]}' THEN Vl END), 0) AS BATH_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[14]}' THEN Vl END), 0) AS FORWARD_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[15]}' THEN Vl END), 0) AS FREEZING_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[16]}' THEN Vl END), 0) AS TYPE_WEIGHT_BACK_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[17]}' THEN Vl END), 0) AS EXTRUSION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[18]}' THEN Vl END), 0) AS EXTRACTION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[19]}' THEN Vl END), 0) AS SPRAY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[20]}' THEN Vl END), 0) AS CAVITY_CORE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[21]}' THEN Vl END), 0) AS A_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[22]}' THEN Vl END), 0) AS B_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[23]}' THEN Vl END), 0) AS VACUUM,
                //                DATE_FORMAT(Tm, '%Y-%m-%d %H:%i:%s') AS TIMESTAMP
                //            FROM 
                //            ";
                //        break;
                //    case "25호기":
                //        list.Add("DCM_25_TAG_D6900_Ruled");
                //        list.Add("DCM_25_TAG_D6902_Ruled");
                //        list.Add("DCM_25_TAG_D6904_Ruled");
                //        list.Add("DCM_25_TAG_D6906_Ruled");
                //        list.Add("DCM_25_TAG_D6908");
                //        list.Add("DCM_25_TAG_D6910");
                //        list.Add("DCM_25_TAG_D6912_Ruled");
                //        list.Add("DCM_25_TAG_D6914");
                //        list.Add("DCM_25_TAG_D6916");
                //        list.Add("DCM_25_TAG_D6918");
                //        list.Add("DCM_25_TAG_D6920_Ruled");
                //        list.Add("DCM_25_TAG_D6936_Ruled");
                //        list.Add("DCM_25_TAG_D6938_Ruled");
                //        list.Add("DCM_25_TAG_D6940_Ruled");
                //        list.Add("DCM_25_TAG_D6942_Ruled");
                //        list.Add("DCM_25_TAG_D6944_Ruled");
                //        list.Add("DCM_25_TAG_D6946_Ruled");
                //        list.Add("DCM_25_TAG_D6948_Ruled");
                //        list.Add("DCM_25_TAG_D6950_Ruled");
                //        list.Add("DCM_25_TAG_D6952_Ruled");
                //        list.Add("LS_25_DW186");
                //        list.Add("LS_25_DW187");
                //        list.Add("LS_25_DW188");
                //        list.Add("LS_25_DW189");
                //        foreach (string item in list)
                //        {
                //            sb.Append($"'{item}',");
                //        }
                //        sb.Remove(sb.Length - 1, 1);

                //        sql2 = $@"SELECT 
                //                '25호 주조기' AS MACHINE_NO,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[0]}' THEN Vl END), 0) AS V1,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[1]}' THEN Vl END), 0) AS V2,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[2]}' THEN Vl END), 0) AS V3,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[3]}' THEN Vl END), 0) AS V4,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[4]}' THEN Vl END), 0) AS ACCELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[5]}' THEN Vl END), 0) AS DECELERATION_POS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[6]}' THEN Vl END), 0) AS METAL_PRESSURE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[7]}' THEN Vl END), 0) AS SWAP_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[8]}' THEN Vl END), 0) AS BISKIT_THICKNESS,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[9]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_PER,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[10]}' THEN Vl END), 0) AS PHYSICAL_STRENGTH_MN,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[11]}' THEN Vl END), 0) AS CYCLE_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[12]}' THEN Vl END), 0) AS TYPE_WEIGHT_ENTRY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[13]}' THEN Vl END), 0) AS BATH_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[14]}' THEN Vl END), 0) AS FORWARD_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[15]}' THEN Vl END), 0) AS FREEZING_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[16]}' THEN Vl END), 0) AS TYPE_WEIGHT_BACK_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[17]}' THEN Vl END), 0) AS EXTRUSION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[18]}' THEN Vl END), 0) AS EXTRACTION_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[19]}' THEN Vl END), 0) AS SPRAY_TIME,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[20]}' THEN Vl END), 0) AS CAVITY_CORE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[21]}' THEN Vl END), 0) AS A_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[22]}' THEN Vl END), 0) AS B_POLLUTION_DEGREE,
                //                COALESCE(MAX(CASE WHEN Cd = '{list[23]}' THEN Vl END), 0) AS VACUUM,
                //                DATE_FORMAT(Tm, '%Y-%m-%d %H:%i:%s') AS TIMESTAMP
                //            FROM 
                //            ";
                //        break;
                //}
                //category = sb.ToString();
                //string sql1 = $@" data_collection WHERE Tm >= '{startTime} 00:00:00' AND Tm <= '{startTime} 23:59:59' 
                //                AND Cd in ({category})
                //                GROUP BY DATE_FORMAT(Tm, '%Y-%m-%d %H:%i:%s')
                //                ORDER BY TIMESTAMP
                //               ;";



                //string mixSql = sql2 + sql1;



                //DataSet ds = myDb.GetAllData(mixSql);
                #endregion


            
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
