using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.DPS
{
    public partial class Check_API_DPS : Form
    {

        public static CoFAS_Log _pCoFAS_Log = new CoFAS_Log(Directory.GetCurrentDirectory() + "//LOG", "", 30, true);
        public System.Threading.Timer _timer;
        public Check_API_DPS()
        {
            InitializeComponent();


        }



        private void Form1_Load(object sender, EventArgs e)
        {

            Tray_IconSetting();

            Check_API_SERVER_ON();



        }
        #region ○ TCP

        private void Check_API_SERVER_ON()
        {
            try
            {

                Text = "DPS_API_SERVER";

                _timer = new System.Threading.Timer(CallBack);

                _timer.Change(0, 3600 * 1000);

                this.FormClosing += _FormClosing;

             
            }
            catch (Exception err)
            {

                return;
            }
        }

        public void CallBack(Object state)
        {
            try
            {
                //BeginInvoke(new MethodInvoker(delegate ()

                Invoke(new MethodInvoker(delegate ()
                {
                    HttpClient _client = new HttpClient()
                    {
                        BaseAddress = new Uri("https://log.smart-factory.kr")
                    };

                    DataTable dt = Get_SMRTFTRLOG();
                    foreach (DataRow item in dt.Rows)
                    {
                        SMRTFTRLOG_V1_1Entity entity = new SMRTFTRLOG_V1_1Entity();

                        entity.crtfcKey = "$5$API$pYt7jJ5SGHjfK7ZiX342f/yfcS4042.9QQgYjWdw5AD";
                        entity.logDt = Convert.ToDateTime(item["수집시간"]).ToString("yyyy-MM-dd HH:mm:ss.fff");
                        entity.useSe = "평균상전압"+ "=" + item["평균상전압"].ToString();

                        entity.sysUser = "DPS";
                        entity.conectIp = "127.0.0.1";
                        entity.dataUsgqty = "0";


                        var aa = _client.GetAsync($"/apisvc/sendLogDataJSON.do?logData={JsonConvert.SerializeObject(entity)}").Result;

                    }
                    
                }));
            }
            catch (Exception pExcption)
            {

            }

        }

        //string _sqlcon = "Server=127.0.0.1;Database=CoPlatform_OPCUA;UID=sa;PWD=coever1191!;";
        public DataTable Get_SMRTFTRLOG()
        {
            DataTable dt = new DataTable();
            try
            {

                //string strcon = "Server = 183.111.74.179,1433;Database = DAMAYO_MES;UID = Coever;PWD = dmy1234!@";
                string strcon = "Server = 183.111.74.179,1433; Database = Hansol_DPS;UID = Coever;PWD = dmy1234!@";
                SqlConnection conn = new SqlConnection(strcon);

        
                //string strSql_Insert = $@"SELECT [PART_ID]
                //                                ,[LOT_NO]
                //                                ,[EDIT_QTY]
                //                                ,[EDIT_DATE]
                //                                ,[EDIT_NO]
                //                                ,[REMARK]
                //                                ,[CREATEDATE]
                //                                ,[CREATEUSER]
                //                                ,[ROW_STAMP]
                //                            FROM [DAMAYO_MES].[dbo].[TB_PRODUCT_TRANS_EDIT_LOG]
                //                            where [CREATEDATE] > DATEADD(HOUR, -1, GETDATE() ) 
                //                             ORDER BY CREATEDATE DESC";


                string strSql_Insert = $@"select *
                                            from [dbo].[data_collection]
                                           where 수집시간 > DATEADD(HOUR, -1, GETDATE() ) 
                                        ORDER BY 수집시간 DESC;";
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





        #endregion




        #region ○ 공통

        private void Tray_IconSetting()
        {
            TrayIcon.MouseDoubleClick += new MouseEventHandler(delegate
            {
                this.ShowInTaskbar = true;
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            });

            showToolStripMenuItem.Click += new EventHandler(delegate
            {
                this.ShowInTaskbar = true;
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            });

            exitToolStripMenuItem.Click += new EventHandler(delegate
            {

                if (MessageBox.Show("Check_API_DPS 설비 데이터 연동을 종료 하시겠습니까?", "데이터 연동 종료확인", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    this.Dispose();
                }
            });

            TrayIcon.ContextMenuStrip = contextMenuStrip1;
        }
        public bool _Bool = true;
        private void _FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.WindowsShutDown && _Bool)
            {
                this.ShowInTaskbar = false;
                this.Visible = false;
                e.Cancel = true;
            }

        }
        #endregion



    }

    public class SMRTFTRLOG_V1_1_MSTEntity
    {
        public UInt64 autoIncreaseKey { get; set; } // DB의 Primary Key
        public SMRTFTRLOG_V1_1Entity vaLue { get; set; }
    }

    public class SMRTFTRLOG_V1_1Entity
    {
        public string crtfcKey { get; set; }
        public string logDt { get; set; }
        public string useSe { get; set; }
        public string sysUser { get; set; }
        public string conectIp { get; set; }
        public string dataUsgqty { get; set; }

    }
    public class SMRTFTRLOG_V1_1_ResultEntity
    {
        public UInt64 autoIncreaseKey { get; set; } // DB의 Primary Key
        public string recptnDt { get; set; }
        public string recptnRsltCd { get; set; } // 오류코드
        public string recptnRslt { get; set; } // 오류코드 내용
        public string recptnRsltDtl { get; set; }

    }
    public class SMRTFTRLOG_V1_1_ResponeEntity
    {
        public SMRTFTRLOG_V1_1_ResultEntity result { get; set; }
    }


}

