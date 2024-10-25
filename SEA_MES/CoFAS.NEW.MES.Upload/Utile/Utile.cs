using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Upload
{
    public static class Utile
    {

        public static void File_Start()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory() + $"\\{CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_Name}";
                proc.StartInfo.FileName = $"{CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_Name}.exe";
                proc.StartInfo.Verb = "runas";
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.RedirectStandardOutput = false;
                proc.Start();

                Application.Exit();
            }
            catch (Exception err)
            {
                My_Exception();
            }
        }
        public static void Tapex_WMS_File_Start()
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory =Directory.GetCurrentDirectory().Substring(0,
                            Directory.GetCurrentDirectory().LastIndexOf("\\"));
                proc.StartInfo.FileName = $"{CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_Name}.exe";
                proc.StartInfo.Verb = "runas";
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.RedirectStandardOutput = false;
                proc.StartInfo.Arguments = CoFAS.NEW.MES.Upload.Properties.Settings.Default.UPDATETYPE;
                proc.Start();

                Application.Exit();
            }
            catch (Exception err)
            {
                My_Exception();
            }
        }

        public static void My_Exception()
        {
            try
            {
                File.Delete(Application.StartupPath + "\\STARTDATA.txt");

                FolderBrowserDialog op = new FolderBrowserDialog();

                op.SelectedPath = Application.StartupPath + "\\Updata";

                foreach (string str in Directory.GetFiles(op.SelectedPath))
                {
                    string path = "";

                    string filename = str.Substring(str.LastIndexOf("\\") + 1);

                    if (CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_TYPE == "MES")
                    {
                        path = Directory.GetCurrentDirectory() + $"\\{CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_Name}" + "\\" + "System" + "\\" + "PMS" + "\\" + filename;
                    }
                    else if (CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_TYPE == "DMS")
                    {
                        path = Directory.GetCurrentDirectory() + $"\\{CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_Name}" + "\\" + "System" + "\\" + "DMS" + "\\" + filename;
                    }
                    else
                    {
                        path = Directory.GetCurrentDirectory() + $"\\{CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_Name}" + "\\" + filename;
                    }
  

                    File.Copy(str, path, true);
                }

                File_Start();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }
    }
}
