using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.POP.Function;
using FarPoint.Excel;
using MyUpDate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DBManager.PrimaryDBManagerType = DBManagerType.SQLServer;
            DBManager.PrimaryConnectionString = "Server=10.10.10.180;Database=HS_MES;UID=hansol_mes;PWD=Hansol123!@#;";
            utility.My_Settings_Start();
            utility.My_Settings_Get();

            세아_POP();
            //원재료간판_POP();

        }

        public static void 세아_POP()
        {

            string sqlcon = DBManager.PrimaryConnectionString;
            string updatetype = "세아POP";
            string runName = "CoFAS.NEW.MES.POP.exe";
            string name = "CoFAS.NEW.MES.Download.exe";

            string arguments = sqlcon + " " + updatetype + " " + runName;
            string dic = Application.StartupPath + "\\" + name;

            FileInfo di = new FileInfo(dic);

            if (di.Exists)
            {
                MyUpDate.MyUpDate date = new MyUpDate.MyUpDate();

                if (date.UpDate_Check(Application.StartupPath, updatetype, sqlcon))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new frmPOPLogin_세아());
                }
                else
                {
                    Process proc = new Process();
                    proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                    proc.StartInfo.FileName = $"{name}";
                    proc.StartInfo.Verb = "runas";
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.RedirectStandardOutput = false;
                    proc.StartInfo.Arguments = arguments;
                    proc.Start();
                    Application.Exit();
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmPOPLogin_세아());

            }
        }

        public static void 원재료간판_POP()
        {

            string sqlcon = DBManager.PrimaryConnectionString;
            string updatetype = "세아간판POP";
            string runName = "CoFAS.NEW.MES.POP.exe";
            string name = "CoFAS.NEW.MES.Download.exe";

            string arguments = sqlcon + " " + updatetype + " " + runName;
            string dic = Application.StartupPath + "\\" + name;

            FileInfo di = new FileInfo(dic);
            if (di.Exists)
            {

                //MyUpDate.MyUpDate date = new MyUpDate.MyUpDate();

                if (UpDate_Check(Application.StartupPath, updatetype, sqlcon))
                {

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new 원재료간판POP());
                }
                else
                {
                    Process proc = new Process();
                    proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                    proc.StartInfo.FileName = $"{name}";
                    proc.StartInfo.Verb = "runas";
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.RedirectStandardOutput = false;
                    proc.StartInfo.Arguments = arguments;
                    proc.Start();
                    Application.Exit();
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new 원재료간판POP());

            }

        }

        private static string sqlcon = "";
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
                    File.WriteAllText(startupPath + "\\STARTDATA.txt", DateTime.Parse("2022-01-01 00:00:00").ToString());
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

                        version = DateTime.Parse("2022-01-01 00:00:00");
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


            SqlConnection conn = new SqlConnection(sqlcon + "Connection Timeout=10;");
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
