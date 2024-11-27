using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Download
{
    public partial class FormDownload_세아_간판POP : Form
    {

        public string _sqlcon = "";
        public string _updatetype = "";
        public string _runName = "";

        public FormDownload_세아_간판POP(string sqlcon, string updatetype, string runName)
        {
            _sqlcon = sqlcon;
            _updatetype = updatetype;
            _runName = "CoFAS.NEW.MES.POP.exe";
            InitializeComponent();

        }


        private void FormDownload_Hansol_Load(object sender, EventArgs e)
        {
            try
            {
                Thread thread1 = new Thread(new ThreadStart(ProgValueSetting));

                thread1.Start();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public void ProgValueSetting()
        {
            try
            {
                DataTable dt = Get_Crc_Autoupdate(_updatetype);

                if (dt.Rows.Count == 0)
                {
                    //MessageBox.Show("프로그램이 존재하지 않습니다.");
                    Thread.Sleep(2000);
                    BeginInvoke(new MethodInvoker(delegate ()
                    {
                        Process proc = new Process();
                        proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        proc.StartInfo.FileName = $"{_runName}";
                        proc.StartInfo.Verb = "runas";
                        proc.StartInfo.UseShellExecute = true;
                        proc.StartInfo.RedirectStandardOutput = false;
                        proc.Start();

                        Application.Exit();

                    }));
                    return;
                }

                BeginInvoke(new MethodInvoker(delegate ()
                {
                    label3.Text = dt.Rows.Count.ToString();
                    progressBar1.Value = 0;
                    progressBar1.Maximum = dt.Rows.Count;
                }));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string crc = dt.Rows[i][0].ToString();

                    string filename = dt.Rows[i][1].ToString();

                    bool crcYn = false;

                    while (!crcYn)
                    {
                        crcYn = GetDateToCrc(crc, filename);
                    }

                    BeginInvoke(new MethodInvoker(delegate ()
                    {
                        label4.Text = filename;
                        label1.Text = i.ToString();
                        progressBar1.Value++;

                    }));
                }

                Thread.Sleep(3000);

                BeginInvoke(new MethodInvoker(delegate ()
                {
                    Process proc = new Process();
                    proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                    proc.StartInfo.FileName = $"{_runName}.exe";
                    proc.StartInfo.Verb = "runas";
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.RedirectStandardOutput = false;
                    proc.Start();

                    Application.Exit();
                }));
            }
            catch (Exception err)
            {
                Application.Exit();
            }
        }

        private DataTable Get_Crc_Autoupdate(string updatetype)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlcon))
                {
                    // DB 연결
                    string strSql_Insert = $@" select CRC,FILENAME2
                                                       from [dbo].[t_autoupdate]
                                                     where UPDATETYPE = '{updatetype}'";

                    SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                    conn.Open();

                    System.Data.SqlClient.SqlDataReader dr;

                    dt = new System.Data.DataTable();

                    dr = cmd_Insert.ExecuteReader();

                    dt.Load(dr);

                    conn.Close();

                    return dt;
                }
            }
            catch (Exception err)
            {
                return dt;
            }
        }
        private bool GetDateToCrc(string crc, string filename)
        {
            bool crcYn = true;

            try
            {
                using (SqlConnection conn = new SqlConnection(_sqlcon))
                {
                    string strSql_Insert = $@"select FILEIMAGE
                                                      from[dbo].[t_autoupdate]
                                                    where CRC = '{crc}' and FILENAME ='{filename}'";

                    SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                    conn.Open();

                    SqlDataReader reader = cmd_Insert.ExecuteReader();

                    byte[] bImage = null;

                    while (reader.Read())
                    {
                        bImage = (byte[])reader[0];
                    }

                    conn.Close();

                    Crc32 crc32 = new Crc32();

                    string tocrc = crc32.byteToCrc(bImage);

                    if (crc != tocrc)
                    {
                        crcYn = false;
                    }
                    else
                    {
                        if (_runName == "Platform_Launcher")
                        {
                            string path = Directory.GetCurrentDirectory() + $"\\" + "System" + "\\" + "PMS" + "\\" + filename;

                            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                            fs.Write(bImage, 0, bImage.Length);

                            fs.Close();
                        }
                        else
                        {
                            string path = Directory.GetCurrentDirectory() + "\\" + filename;

                            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                            fs.Write(bImage, 0, bImage.Length);

                            fs.Close();
                        }
                    }
                }
                return crcYn;
            }
            catch (Exception err)
            {
                return crcYn = false;
            }
        }

    }
}
