using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.POP.Function;
using MyUpDate;
using System;
using System.Collections.Generic;
using System.Data;
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
            DBManager.PrimaryConnectionString = "Server=10.10.10.180;Database=HS_MES;UID=hansol_mes;PWD=Hansol123!@#";

            //===========================
            //DBManager.PrimaryConnectionString = "Server = 222.113.146.82,11433;Database=HS_MES;UID=sa;PWD=coever1191!";
            //DBManager.PrimaryConnectionString = "Server = 172.22.4.11,60901; Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
            //DBManager.PrimaryConnectionString = "Server =127.0.0.1;Database=HS_MES;UID=sa;PWD=coever1191!";

            utility.My_Settings_Start();
            utility.My_Settings_Get();

            세아_POP();
            //원재료간판_POP();

            //===========================
            //이튼_POP();
            //이튼_작업표준서();
        }

        public static void 세아_POP()
        {

            string sqlcon = DBManager.PrimaryConnectionString;
            string updatetype = "NEW_세아_POP";
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
            string updatetype = "NEW_세아원재료간판_POP";
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

        #region [타 업체]

        public static void 이튼_POP()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPOPLogin());
            //string sqlcon =  DBManager.PrimaryConnectionString;
            //string updatetype = "NEW_이앤아이비_POP";
            //string runName  = "CoFAS.NEW.MES.POP.exe";
            //string name = "CoFAS.NEW.MES.Download.exe";

            //string arguments = sqlcon + " " + updatetype + " " + runName;
            //string dic = Application.StartupPath +"\\"+ name;

            //FileInfo di = new FileInfo(dic);
            //if (di.Exists)
            //{
            //    MyUpDate.MyUpDate date = new MyUpDate.MyUpDate();

            //    if (date.UpDate_Check(Application.StartupPath, updatetype, sqlcon))
            //    {
            //        Application.EnableVisualStyles();
            //        Application.SetCompatibleTextRenderingDefault(false);
            //        Application.Run(new frmPOPLogin());
            //    }
            //    else
            //    {
            //        Process proc = new Process();
            //        proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            //        proc.StartInfo.FileName = $"{name}";
            //        proc.StartInfo.Verb = "runas";
            //        proc.StartInfo.UseShellExecute = true;
            //        proc.StartInfo.RedirectStandardOutput = false;
            //        proc.StartInfo.Arguments = arguments;
            //        proc.Start();
            //        Application.Exit();
            //    }
            //}
            //else
            //{
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new frmPOPLogin());
            //}
        }

        public static void 이튼_작업표준서()
        {


            string sqlcon = DBManager.PrimaryConnectionString;
            string updatetype = "작업표준서";
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
                    Application.Run(new 작업표준서());
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
                Application.Run(new 작업표준서());
            }
        }
        #endregion

    }
}
