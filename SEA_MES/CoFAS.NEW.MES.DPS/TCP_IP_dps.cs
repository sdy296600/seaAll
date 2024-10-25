using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.DPS
{
    public partial class TCP_IP_dps : Form
    {

        public static CoFAS_Log _pCoFAS_Log = new CoFAS_Log(Directory.GetCurrentDirectory() + "//LOG", "", 30, true);

        public CoFAS_SocketServer _pSocketServer = null;
        public TCP_IP_dps()
        {
            InitializeComponent();
       

        }



        private void Form1_Load(object sender, EventArgs e)
        {

            Tray_IconSetting();

            TCP_SERVER_ON();



        }
        #region ○ TCP

        private void TCP_SERVER_ON()
        {
            try
            {

                Text = "DPS_TCP_SERVER";
          
                _pSocketServer = new CoFAS_SocketServer(20, 1000);
                _pSocketServer.evtReceiveRequest += new CoFAS_SocketServer.delReceiveRequest(receiverequest);

                _pSocketServer.Start();



            }
            catch (Exception err)
            {
              
                return;
            }
        }

        private void receiverequest(Socket soc, byte[] bytData)
        {

            try
            {
             

                //드러온 데이터 바이트의 배열
                byte[] byteresult = bytData;

                //드러온 데이터 sstring 인코딩
                string strSTR = System.Text.Encoding.Default.GetString(bytData).TrimEnd();



                if (strSTR == "") return;
                //데이터 Split 문자 받기
                //char Split = Convert.ToChar(DPS.Properties.Settings.Default.SPLIT);

                ////데이터 Split
                //string[] str = strSTR.Split(Split);


                //데이터 Split 이 안되는 경우 리턴
                //if (str.Length == 1)
                //{
                // 
                //}

          
             
                //TCP_Packetdata(str);

            }
            catch (Exception err)
            {
             

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

                if (MessageBox.Show("TCP_IP_dps 설비 데이터 연동을 종료 하시겠습니까?", "데이터 연동 종료확인", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
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
    



}

