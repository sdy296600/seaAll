﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Download
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] strs2)
        {
            //string sqlcon = "Server=222.113.146.82,11433;Database=Hansol_Auto_Update;UID=sa;PWD=coever1191!;";
            //string updatetype = "NEW_MES";
            //string runName  = "CoFAS.NEW.MES.exe";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormDownload_Eaton from =  new FormDownload_Eaton();

            //DBManager.PrimaryConnectionString = "Server = 172.22.4.11,60901; Database = HS_MES; UID = MesConnection; PWD = 8$dJ@-!W3b-35;";
            //utility.My_Settings_Start();
            //utility.My_Settings_Get();

            string sqlcon =  "Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
            string updatetype = "작업표준서";
            string runName  = "CoFAS.NEW.MES.POP.exe";
            string name = "CoFAS.NEW.MES.Download.exe";

            string arguments = sqlcon + " " + updatetype + " " + runName;

            //string[] strs = arguments.Split(" ");

            from._sqlcon = sqlcon;
            from._updatetype = updatetype;
            from._runName = runName;

            //from._sqlcon     = strs[0];
            //from._updatetype = strs[1];
            //from._runName    = strs[2];
            //from._sqlcon     = "Server=222.113.146.82,11433;Database=Hansol_Auto_Update;UID=sa;PWD=coever1191!;";
            //from._updatetype = "NEW_MES";
            //from._runName    = "CoFAS.NEW.MES.exe";
            Application.Run(from);

            //Application.Run(new FormDownload(strs[0], strs[1], strs[2]));
            //if (strs.Length != 0)
            //{
            //    foreach (string item in strs)
            //    {
            //        MessageBox.Show(item);
            //    }

            //}
            //return;
            //string sqlcon = "Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
            ////string sqlcon = "Server= 127.0.0.1;" +
            ////                "Database= HS_MES;" +
            ////                "UID= sa;" +
            ////                "PWD= coever1191!;";
            //string updatetype = "이튼POP";
            //string runName = "CoFAS.NEW.MES.POP";

            //string sqlcon = "Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
            //이앤아이비
            //string sqlcon = "Server= 222.113.146.82,11433;" +
            //                "Database= Hansol_Auto_Update;" +
            //                "UID= sa;" +
            //                "PWD= coever1191!;";

            //string updatetype = "NEW_MES";

            //string runName = "CoFAS.NEW.MES";

            //대성금형
            //string sqlcon = "Server= 211.221.27.249,1433;" +
            //                 "Database= Hansol_Auto_Update;" +
            //                 "UID= sa;" +
            //                 "PWD= coever1191!;";

            // string updatetype = "대성금형POP";

            // string runName = "CoFAS_MES.POP";

            ////세아_MES
            //string sqlcon = "Server=10.10.10.180;Database=HS_MES;UID=hansol_mes;PWD=Hansol123!@#;";

            //string updatetype = "세아MES";

            //string runName = "CoFAS.NEW.MES";

            //세아_POP
            //string sqlcon = "Server=10.10.10.180;Database=HS_MES;UID=hansol_mes;PWD=Hansol123!@#;";

            //string updatetype = "세아간판POP";

            //string runName = "CoFAS.NEW.MES.POP";
            //Platform_Launcher(sqlcon, updatetype, runName);


        }

        private static void Platform_Launcher(string sql, string type, string _runName)
        {
            if (UpDate_Check(Application.StartupPath, type, sql))
            {
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                proc.StartInfo.FileName = $"{_runName}.exe";
                proc.StartInfo.Verb = "runas";
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.RedirectStandardOutput = false;
                proc.Start();

                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormDownload_세아_간판POP());
            }
        }

        private static string sqlcon ="";
        public static bool UpDate_Check(string startupPath, string type, string sql)
        {
            sqlcon = sql;
            string path = startupPath + "\\STARTDATA.txt";

            try
            {

                FileInfo info = new FileInfo(path);

                bool startYN = true;

                if (info.Exists)
                {
                    startYN = true;
                }
                else
                {
                    File.WriteAllText(startupPath + "\\STARTDATA.txt", DateTime.Parse("2022-01-01").ToString());
                    startYN = false;
                }
                // DB 연결 체크
                if (DB_Open_Check())
                {
                    DataTable dt = Get_Up_Load_Date_Autoupdate(type);

                    DateTime version = new DateTime();

                    string st = File.ReadAllText(path);

                    if (!DateTime.TryParse(st, out version))
                    {

                        version = DateTime.Parse("2022-01-01");
                    }

                    bool upcheck = true;

                    foreach (DataRow item in dt.Rows)
                    {
                        DateTime get_version = Convert.ToDateTime(item[0].ToString());

                        if (version < get_version)
                        {
                            File.WriteAllText(startupPath + "\\STARTDATA.txt", get_version.ToString("yyyy-MM-dd HH:mm:ss"));

                            upcheck = false;
                        }
                    }

                    if (upcheck && startYN)
                    {

                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception err)
            {
                // MessageBox.Show(err.Message);
                File.Delete(startupPath + "\\STARTDATA.txt");

                return false;
            }
        }
        private static bool DB_Open_Check()
        {
            bool check = true;


            SqlConnection conn = new SqlConnection(sqlcon+ "Connection Timeout=10;");
            try
            {
                // DB 연결          
                conn.Open();

                // 연결여부에 따라 다른 메시지를 보여준다
                if (conn.State != ConnectionState.Open)
                {
                    check = false;
                }

                conn.Close();

                return check;
            }
            catch (Exception err)
            {
                conn.Close();
                return check = false;
            }
        }

        private static DataTable Get_Up_Load_Date_Autoupdate(string type)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@" select UPLOADDATE
                                                       from [dbo].[t_autoupdate]
                                                     where UPDATETYPE = '{type}'";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                conn.Open();

                System.Data.SqlClient.SqlDataReader dr;

                dt = new System.Data.DataTable();

                dr = cmd_Insert.ExecuteReader();

                dt.Load(dr);

                conn.Close();

                return dt;

            }
            catch (Exception err)
            {
                return dt;
            }
        }
    }
}
