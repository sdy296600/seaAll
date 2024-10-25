using CoFAS.NEW.MES.Core.Function;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;


namespace CoFAS.NEW.MES.DPS
{
    public partial class Andon_dps : Form
    {

        public static CoFAS_Log _pCoFAS_Log = new CoFAS_Log(Directory.GetCurrentDirectory() + "//LOG", "", 30, true);

        public CoFAS_SocketServer _pCoFASSocketServer = null;
        private Socket SS; //소켓
        private Encoding _pEncoding = Encoding.ASCII;
        private bool isServerOpen = false;

        private string _pMessage = string.Empty; //메세지 처리
        private CoFAS_Log _pCoFASLog = new CoFAS_Log(Application.StartupPath + "\\LOG", "", 30, true);

        //고정 데이터 응답 코드
        byte[] byte_Alive = { 0x02, 0x02 , 0x00, 0x08, 0xC5, 0x01, 0x00, 0x2E }; //연결 alive 신호 들어올때마다 전달 
        byte[] byte_BellAnswer = { 0x02, 0x02, 0x00, 0x08, 0xC5, 0x05, 0x00, 0x2A }; //버튼 눌렀을때마다 응답신호 전달 
        public Andon_dps()
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

                _pCoFASSocketServer = new CoFAS_SocketServer(5000, 1000);
                _pCoFASSocketServer.evtClentConnect = new CoFAS_SocketServer.delClientConnect(CilentConnected);
                _pCoFASSocketServer.evtReceiveRequest = new CoFAS_SocketServer.delReceiveRequest(CilentSend);
                _pCoFASSocketServer.evtReceiveRequest = new CoFAS_SocketServer.delReceiveRequest(evtReceiveRequest);
                Server_Open();
            }
            catch (Exception err)
            {
                return;
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

        #region 서버 오픈
        private void Server_Open()
        {
            if (!isServerOpen)
            {	//서버시작
                try
                {
                    _pCoFASSocketServer.Start();
                    isServerOpen = true;
                    ControlManager.Invoke_PictureBox_Image(picBox01, Properties.Resources.Ok);
                }
                catch (Exception pException)
                {
                }
            }
            else
            {
                try
                {
                    _pCoFASSocketServer.Stop();
                    isServerOpen = false;
                    ControlManager.Invoke_PictureBox_Image(picBox01, Properties.Resources.Ng);
                }
                catch (Exception pException)
                {
                }
            }
        }
        #endregion

        #region 소켓통신 

        private void CilentConnected(Socket soc)
        {
            try
            {
                IPEndPoint ip = (IPEndPoint)soc.RemoteEndPoint;

                SS = soc;
            }
            catch (Exception pException)
            {
                Message_Log("[CilentConnected] : " + pException.ToString(), false, true);
            }
        }

        private void CilentSend(Socket soc, byte[] bytdata)
        {
            try
            {
                IPEndPoint ip = (IPEndPoint)soc.RemoteEndPoint;

                string str = _pEncoding.GetString(bytdata);
                Message_Log("Alive 신호 ", false, true);
                soc.Send(bytdata);
                
            }
            catch (Exception pException)
            {
                Message_Log("[CilentSend] : " + pException.ToString(),false,true);
            }
        }

        /// <summary>
        /// 소켓 에서 받은 데이터 처리 한다.
        /// </summary>
        /// <param name="soc"></param>
        /// <param name="byt"></param>
        private void evtReceiveRequest(Socket soc, byte[] byt)//안돈 이벤트
        {
            try
            {
                string station_num = "";
                string button_num = "";
                //들러온 데이터 바이트의 배열
                byte[] byteresult = byt;
                if (byteresult[5].ToString() == "1")
                {
                    CilentSend(SS, byte_Alive);
                    Add_Lv_PPC("수신기 Alive 신호 전달", "Alive 신호 전달");   //리스트 박스 바인딩
                }
                else if(byteresult[5].ToString() == "5")
                {
                    button_num = andon_button(byteresult[24].ToString());
                    //button_num = byteresult[24].ToString();
                    switch (byteresult[22].ToString())
                    {
                        case "108":
                            station_num = "ST4006";// 6번 스위치";
                           
                            break;
                        case "194":
                            station_num = "ST4005";// 5번 스위치";

                            break;
                        case "74":
                            station_num = "ST4004";// 4번 스위치";

                            break;
                        case "138":
                            station_num = "ST4003";// 3번 스위치";

                            break;
                        case "34":
                            station_num = "ST4002";// 2번 스위치";
                           
                            break;
                        case "92":
                            station_num = "ST4001";// 1번 스위치";

                            break;
                        case "2":
                            station_num = "ST4007";// 7번 스위치";

                            break;
                        case "66":
                            station_num = "ST4008";// 8번 스위치";
                     
                            break;
                        default:
                            station_num = "";
                            //button_num = andon_button(byteresult[24].ToString());
                            break;
                    }

                    CilentSend(SS, byte_BellAnswer);

                    if (station_num != "" && button_num != "")
                    {
                        Add_Lv_PPC(station_num, button_num);   //리스트 박스 바인딩

                        //station_num 리모콘번호
                        //button_num  버튼번호
                        // 수집시간
                        if (button_num == "10")
                        {
                            new CoFAS_DB_Manager().reset_Andon(station_num);
                        }
                        else
                        {
                            new CoFAS_DB_Manager().Set_Andon(station_num, button_num);
                        }
                    }

                }
            }
            catch (Exception pException)
            {
                Message_Log(pException.ToString() + ": 데이터 확인5", true, true);
            }
        }

     

        private void ReceiveData(byte[] data) //소켓 데이터 리시브
        {
            string strIpcamera = Encoding.Default.GetString(data);
            Message_Log(strIpcamera, true, true);
            if (strIpcamera.Length > 3)
            {
                string[] val = strIpcamera.Split('|');

                if (val[3].Substring(0, 2) == "OK")
                {
                    Message_Log(strIpcamera, true, true);
                    return;
                }
                else
                {
                    Message_Log(strIpcamera, true, true);
                    return;
                }
            }
        }

        #endregion
       

        // ■ 메세지 처리
        #region ○ 메세지 처리
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMessage">메세지 표시</param>
        /// <param name="isLog">로그 저장 유무</param>
        /// <param name="isView">화면 표시 유무</param>
        protected void Message_Log(string strMessage, bool isLog, bool isView)
        {
            DateTime dt = DateTime.Now;

            _pMessage = string.Format("[{0}:{1}:{2}] {3}\r\n", dt.ToString("HH"), dt.ToString("mm"), dt.ToString("ss"), strMessage);

            if (isView)
            {
                ControlManager.InvokeIfNeeded(_lciTCP_DATA, () => _lciTCP_DATA.Text = _pMessage);
            }

            if (isLog)
            {
                _pCoFASLog.WLog(_pMessage);
            }
        }
        #endregion

        #region ○ 안돈 버튼 처리
        public string andon_button(string strButton)
        {
            string str = "";
            switch (strButton)
            {
                case "4":
                    str = strButton;//"제품불량";
                    break;
                case "5":
                    str = strButton;//"재료부족";
                    break;
                case "6":
                    str = strButton;//"로봇에러";
                    break;
                case "8":
                    str = strButton;//"품질확인";
                    break;
                case "9":
                    str = strButton;//"원재료입고";
                    break;
                case "10":
                    str = strButton;// "리셋";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }
        #endregion

        // ■ 리스트 뷰 추가
        #region ○ 리스트 뷰 추가
        public void Add_Lv_PPC(string strMsg1, string strLine)
        {
            try
            {
                string strDate = ConvertManager.Date2String(DateTime.Now, ConvertManager.enDateType.DateTimeShort);

                string[] stritem = new string[] { strDate, strMsg1, strLine };

                ControlManager.Invoke_ListView_AddItemString(AndonList, false, stritem, true, 33);

                Message_Log("RFID -> IPC : " + strMsg1, true, true);
            }
            catch (Exception ex)
            {
                _pCoFASLog.WLog_Exception("Add_Lv_PPC", ex);
            }
        }



        #endregion

    }




}

