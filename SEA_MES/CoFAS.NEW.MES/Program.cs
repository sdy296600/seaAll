
using MyUpDate;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CoFAS.NEW.MES
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Process[] procs = Process.GetProcessesByName("CoFAS.NEW.MES");
            // 두번 이상 실행되었을 때 처리할 내용을 작성합니다.
            //if (procs.Length > 1)
            //{
            //    MessageBox.Show("프로그램이 이미 실행되고 있습니다.\n다시 한번 확인해주시기 바랍니다.");
            //    Application.Exit();
            //    return;
            //}
            // MyUpDate.MyUpDate date = new MyUpDate.MyUpDate();
            // 실행을 업데이트 하고 체크해서 => 프로그램 실행 or 업데이트 후 실행
            //프로그램에서 업데이트 확인하고 => 실행 => 업데이트 프로그램에서 업데이후 재실행으로 바꿨어
            //
            //string sqlcon = "Server=222.113.146.82,11433;Database=Hansol_Auto_Update;UID=sa;PWD=coever1191!;";
            string sqlcon = "Server=10.10.10.180;Database=HS_MES;UID=hansol_mes;PWD=Hansol123!@#;";
            string updatetype = "세아";
            string runName  = "CoFAS.NEW.MES.exe";
            string name = "CoFAS.NEW.MES.Download.exe";

            string arguments = sqlcon + " " + updatetype + " " + runName;
            string dic = Application.StartupPath +"\\"+ name;

            FileInfo di = new FileInfo(dic);
            if (di.Exists)
            {
                MyUpDate.MyUpDate date = new MyUpDate.MyUpDate();

                if (date.UpDate_Check(Application.StartupPath, updatetype, sqlcon))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new frmLogin());
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
                Application.Run(new frmLogin());
            }
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmLogin());


        }
    }

}
