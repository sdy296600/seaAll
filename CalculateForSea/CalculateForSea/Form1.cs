using DevExpress.Utils.OAuth;
using EasyModbus;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using uPLibrary.Networking.M2Mqtt.Messages;
using VagabondK.Protocols.Channels;
using VagabondK.Protocols.LSElectric;
using VagabondK.Protocols.LSElectric.FEnet;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CalculateForSea
{
    public partial class Form1 : Form
    {

        #region 모델
        public class DataModel
        {
            public double NowShotKW { get; set; } = -1; // 현재 누적전력량
            public double Consumption_K { get; set; } = 0; // 누적 ESG (kWh)
            public double Consumption_M { get; set; } = 0; // 누적 ESG (MWh)
            public double ConsumptionRETI { get; set; } = 0; // 누적 RETI
            public double NowRETI { get; set; } = 0; // 현재 RETI
            public double NowESG { get; set; } = 0; // 현재 ESG
            public double Totalcnt { get; set; } = -1; // 현재 총 생산량 (사타수 + 배출수 + 양품수)
            public double PROD_CNT { get; set; } = 0; // 제품 생산수 ( 배출수 + 양품수)
            public double Active_Power { get; set; } = 0; // 유효전력
            public double ReActive_Power { get; set; } = 0; // 무효전력
            public double NowMotorHour { get; set; } = 0; // 현재 구동시간 - 모터구동 시작했을 때 부터의 시간입니다. (모터 가동 정지 시 초기화 됨)
            public double MotorLIFEDay { get; set; } = 0; // 총 모터 구동 일수
            public double MotorLIFEHour { get; set; } = 0; //총 모터 구동 시간 두개 데이터를 합쳐서 실제 사용한 구동시간을 산출합니다
            public string ID { get; set; } = "";
            public bool K_OK { get; set; } = false;
            public bool M_OK { get; set; } = false;
            public bool R_OK { get; set; } = false;
            public double All_Active_Power { get; set; } = 0; // 유효전력
            public DateTime? dt { get; set; } = null;
            public double getDtTotalCount { get; set; } = 0.001;

            public double getDtOkCnt { get; set; } = 0.001;
            public double getDtErrCnt { get; set; } = 0.001;
            public double getDtWarmCnt { get; set; } = 0.001;

            public bool is_Running = false;
            public bool is_First = true;
            public int Count = 0;

        }

        public class DataModel2
        {
            public double F_ESG_K { get; set; } = 0; // 누적 ESG (kWh)
            public double F_ESG_M { get; set; } = 0; // 누적 ESG (MWh)

            public double T_ESG_K { get; set; } = 0; // 누적 ESG (kWh)
            public double T_ESG_M { get; set; } = 0; // 누적 ESG (MWh)
            public double FnActive_Power { get; set; } = 0; // 유효전력
            public double tmActive_Power { get; set; } = 0; // 유효전력

            public double TRIMING_SHOT { get; set; } = 0; // 누적 ESG (MWh)
            public bool F_K_OK { get; set; } = false;
            public bool F_M_OK { get; set; } = false;
            public bool T_K_OK { get; set; } = false;
            public bool T_M_OK { get; set; } = false;
        }

        #endregion

        #region 전역 변수
        private System.Threading.Timer _tmr; // 데이터 조회 및 MQTT 전송 타이머
        private System.Threading.Timer _tmrFOrGrid; // 그리드 타이머
        private System.Threading.Timer _tmrSerData;
        string ConnectionString = "Server=10.10.10.216;Database=hansoldms;Uid=coever;Pwd=coever119!";
        DPSMqttClient _mqttClient;
        List<DataModel> models = new List<DataModel>();

        string[] machines = new string[6] { "13", "21", "22", "23", "24", "25" };
        DataModel model13 = new DataModel();
        DataModel model21 = new DataModel();
        DataModel model22 = new DataModel();
        DataModel model23 = new DataModel();
        DataModel model24 = new DataModel();
        DataModel model25 = new DataModel();

        List<DataModel2> models2 = new List<DataModel2>();

        DataModel2 model_13 = new DataModel2();
        DataModel2 model_21 = new DataModel2();
        DataModel2 model_22 = new DataModel2();
        DataModel2 model_23 = new DataModel2();
        DataModel2 model_24 = new DataModel2();
        DataModel2 model_25 = new DataModel2();

        List<List<GridModel>> gridModels = new List<List<GridModel>>();
        List<GridModel> list_13 = new List<GridModel>();
        List<GridModel> list_21 = new List<GridModel>();
        List<GridModel> list_22 = new List<GridModel>();
        List<GridModel> list_23 = new List<GridModel>();
        List<GridModel> list_24 = new List<GridModel>();
        List<GridModel> list_25 = new List<GridModel>();

        List<GridModel> gridModels_DCM = new List<GridModel>();
        GridModel list_13_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "WCI_D13", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        GridModel list_21_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "WCI_D21", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        GridModel list_22_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "WCI_D22", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        GridModel list_23_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "WCI_D23", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        GridModel list_24_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "WCI_D24", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        GridModel list_25_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "WCI_D25", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        List<string> IDS = new List<string>();
        CancellationTokenSource cts0 = new CancellationTokenSource();
        CancellationTokenSource cts1 = new CancellationTokenSource();
        CancellationTokenSource cts2 = new CancellationTokenSource();
        CancellationTokenSource cts3 = new CancellationTokenSource();
        CancellationTokenSource cts4 = new CancellationTokenSource();
        CancellationTokenSource cts5 = new CancellationTokenSource();
        CancellationTokenSource elecToken0 = new CancellationTokenSource();
        CancellationTokenSource elecToken1 = new CancellationTokenSource();
        CancellationTokenSource elecToken2 = new CancellationTokenSource();
        CancellationTokenSource elecToken3 = new CancellationTokenSource();
        CancellationTokenSource elecToken4 = new CancellationTokenSource();
        CancellationTokenSource elecToken5 = new CancellationTokenSource();

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Init();
            FEnetClient LSClient = new FEnetClient(new TcpChannel("172.1.100.141", 2004));
            FEnetClient LSClient2 = new FEnetClient(new TcpChannel("172.1.100.142", 2004));
            FEnetClient LSClient3 = new FEnetClient(new TcpChannel("172.1.100.143", 2004));
            FEnetClient LSClient4 = new FEnetClient(new TcpChannel("172.1.100.144", 2004));
            FEnetClient LSClient5 = new FEnetClient(new TcpChannel("172.1.100.145", 2004));

            StartPlcMonitoring(LSClient);
            StartPlcMonitoring(LSClient2);
            StartPlcMonitoring(LSClient3);
            StartPlcMonitoring(LSClient4);
            StartPlcMonitoring(LSClient5);
            
            List<Task> tasks = new List<Task>
            {
                Task.Run(async () => { await ThreadMethodAsync(0, 1, cts0.Token); }),
                Task.Run(async () => { await ThreadMethodAsync(1, 1, cts1.Token); }),
                Task.Run(async () => { await ThreadMethodAsync(2, 1, cts2.Token); }),
                Task.Run(async () => { await ThreadMethodAsync(3, 1, cts3.Token); }),
                Task.Run(async () => { await ThreadMethodAsync(5, 1, cts5.Token); }),
                Task.Run(async () => { await ThreadMethodAsync(4, 1, cts4.Token); })
            };
            List<Task> tasks2 = new List<Task>
            {
                Task.Run(async () => { await RunGetElec(0,elecToken0.Token); }),
                Task.Run(async () => { await RunGetElec(1,elecToken1.Token); }),
                Task.Run(async () => { await RunGetElec(2,elecToken2.Token); }),
                Task.Run(async () => { await RunGetElec(3,elecToken3.Token); }),
                Task.Run(async () => { await RunGetElec(4,elecToken4.Token); }),
                Task.Run(async () => { await RunGetElec(5,elecToken5.Token); })
            };

        }
        
        public async Task RunGetElec(int machine_no,CancellationToken token)
        {
            while (!token.IsCancellationRequested) 
            {

                string ip = "";
                double data = 0;
                List<ModbusClient> clients = new List<ModbusClient>();

                if (machine_no == 0)
                {
                    ip = "172.1.100.112";
                    clients.Add(new ModbusClient(ip, 502));
                    foreach (ModbusClient client in clients)
                    {
                        Get_Elec(client);
                    }
                    data = models[machine_no].Consumption_K + models[machine_no].Consumption_M + models[machine_no].ConsumptionRETI + models2[machine_no].F_ESG_K + models2[machine_no].F_ESG_M + models2[machine_no].T_ESG_M + models2[machine_no].T_ESG_K;
                    SaveWorkData($"UPDATE WORK_DATA SET WORK_POWER = '{data}'", machine_no);
                    SetElec(models[machine_no], models2[machine_no], machine_no);

                    WriteLog(machine_no + "호기 : " + data.ToString());

                    return;
                }

                ip = $"172.1.100.15{machine_no}";
                clients.Add(new ModbusClient(ip, 502));
                ip = $"172.1.100.16{machine_no}";
                clients.Add(new ModbusClient(ip, 502));
                ip = $"172.1.100.17{machine_no}";
                clients.Add(new ModbusClient(ip, 502));
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string mn = machine_no == 0 ? "13" : $"2{machine_no}";
                string mysqlString = $@"insert into trimming_elec(machine_no,value,date) values('{mn}','{models2[machine_no].T_ESG_M + models2[machine_no].T_ESG_K} M {models2[machine_no].T_ESG_M/1000} K {models2[machine_no].T_ESG_K}','{dateTime}');
                                        insert into furnace_elec(machine_no,value,date) values('{mn}','{models2[machine_no].F_ESG_M + models2[machine_no].F_ESG_K} M {models2[machine_no].F_ESG_M / 1000} K {models2[machine_no].F_ESG_K}','{dateTime}');
                                        insert into casting_elec(machine_no,value,date) values('{mn}','{models[machine_no].Consumption_M + models[machine_no].Consumption_K} M {models[machine_no].Consumption_M / 1000} K {models[machine_no].Consumption_K}','{dateTime}');";

                MySqlConnection conn2 = new MySqlConnection(ConnectionString);
                using (conn2)
                {
                    conn2.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = mysqlString;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn2;
                    cmd.ExecuteNonQuery();
                }

                foreach (ModbusClient client in clients)
                {
                    Get_Elec(client);
                }

                data = models[machine_no].Consumption_K + models[machine_no].Consumption_M + models[machine_no].ConsumptionRETI + models2[machine_no].F_ESG_K + models2[machine_no].F_ESG_M + models2[machine_no].T_ESG_M + models2[machine_no].T_ESG_K;
                SaveWorkData($"UPDATE WORK_DATA SET WORK_POWER = '{data}'", machine_no);
                SetElec(models[machine_no], models2[machine_no], machine_no);

                WriteLog(machine_no + "호기 : " + data.ToString());
                    
                await Task.Delay(1000);
            }
        
        }

        public void Get_Elec(ModbusClient client)
        {
            try
            {
                string[] ips = client.IPAddress.Split('.');
                client.Connect();
                
                int ipLast = Convert.ToInt32(ips[ips.Length - 1]);
                int machine_index = 0;
                int type = 0;
                if (ipLast - 170 > 0)
                {
                    machine_index = ipLast - 170;
                    type = 2; //트리밍
                }
                else if (ipLast - 160 > 0)
                {
                    machine_index = ipLast - 160;
                    type = 0; // 주조기
                }
                else if (ipLast - 150 > 0)
                {
                    machine_index = ipLast - 150;
                    type = 1; //용해로
                }

                int mhours = client.ReadHoldingRegisters(1311, 1)[0] * 1000;
                int khours = client.ReadHoldingRegisters(1330, 1)[0];
                DataModel model;
                DataModel2 model2;
                model = models[machine_index];
                model2 = models2[machine_index];


                switch (type)
                {
                    case 0:
                        {
                            model.Consumption_K = double.Parse(khours.ToString());
                            model.K_OK = true;
                            model.Consumption_M = double.Parse(mhours.ToString());
                            model.M_OK = true;
                        }
                        break;
                    case 1:
                        {
                            model2.F_ESG_K = double.Parse(khours.ToString());
                            model2.F_K_OK = true;
                            model2.F_ESG_M = double.Parse(mhours.ToString());
                            model2.F_M_OK = true;
                        }
                        break;
                    case 2:
                        {
                            model2.T_ESG_K = double.Parse(khours.ToString());
                            model2.T_K_OK = true;
                            model2.T_ESG_M = double.Parse(mhours.ToString());
                            model2.T_M_OK = true;
                        }
                        break;
                }

                client.Disconnect();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        public void GetPlcAsync(string address, FEnetClient LSClient)
        {
            TcpChannel ch = LSClient.Channel as TcpChannel;
            int i = Convert.ToInt16(ch.Host[ch.Host.Length - 1].ToString());
            try
            {
                List<string> datas = new List<string>();
                foreach (var item in LSClient.Read(address))
                {
                    datas.Add(item.Value.WordValue.ToString());
                }

                if (datas[0] == "1")
                {
                    var item = LSClient.Read("%DW816", 4);
                    foreach (int readItem in item.Cast(DataType.Word))
                    {
                        datas.Add(readItem.ToString());
                    }

                    lock (gridModels_DCM[i])
                    {
                        gridModels_DCM[i].금형내부 = datas[1];
                        gridModels_DCM[i].오염도A = datas[2];
                        gridModels_DCM[i].오염도B = datas[3];
                        gridModels_DCM[i].탱크진공 = datas[4];
                    }

                    int machine_id;
                    switch (i)
                    {
                        case 0:
                            machine_id = 13;
                            break;
                        case 1:
                            machine_id = 21;
                            break;
                        case 2:
                            machine_id = 22;

                            break;
                        case 3:
                            machine_id = 23;
                            break;
                        case 4:
                            machine_id = 24;
                            break;
                        case 5:
                            machine_id = 25;
                            break;
                        default:
                            return;
                    }

                    dm_alram_status_update(datas[1], $"LS_{machine_id}_DW816");
                    dm_alram_status_update(datas[2], $"LS_{machine_id}_DW817");
                    dm_alram_status_update(datas[3], $"LS_{machine_id}_DW818");
                    dm_alram_status_update(datas[4], $"LS_{machine_id}_DW819");
                }
            }
            catch (TimeoutException ex)
            {
                lock (gridModels_DCM[i])
                {
                    gridModels_DCM[i].금형내부 = "0";
                    gridModels_DCM[i].오염도A = "0";
                    gridModels_DCM[i].오염도B = "0";
                    gridModels_DCM[i].탱크진공 = "0";
                }

            }
            catch (Exception ex)
            {
                lock (gridModels_DCM[i])
                {
                    gridModels_DCM[i].금형내부 = "0";
                    gridModels_DCM[i].오염도A = "0";
                    gridModels_DCM[i].오염도B = "0";
                    gridModels_DCM[i].탱크진공 = "0";
                }
            }
        }

        public void StartPlcMonitoring(FEnetClient LSClient)
        {
            Task.Run(() =>
            {
                while (true) // 반복적으로 실행
                {
                    GetPlcAsync("%MX830", LSClient);
                    Task.Delay(10);
                }
            });
        }

        private void Init()
        {
            try
            {
                WriteLog("Program Start");
                _mqttClient = new DPSMqttClient("10.10.10.216", 1883, false, null, null, 0);
                _mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                
                #region [Casting]
                _mqttClient.Subscribe(new string[] { "DPS/Casting_161_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_161_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Casting_161_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Casting_161_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_161_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });// 현재  
                _mqttClient.Subscribe(new string[] { "DPS/Casting_161_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });// 구동 일수
                _mqttClient.Subscribe(new string[] { "DPS/Casting_161_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });// 구동 시간
                _mqttClient.Subscribe(new string[] { "DPS/Casting_162_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_162_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Casting_162_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Casting_162_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_162_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_162_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_162_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_162_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_163_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_163_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Casting_163_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Casting_163_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_163_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_163_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_163_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_164_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_164_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Casting_164_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Casting_164_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_164_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_164_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_164_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_164_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_165_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_165_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Casting_165_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Casting_165_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_165_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_165_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Casting_165_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                #endregion

                #region [Furnace]
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_151_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_151_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Furnace_151_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Furnace_151_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_151_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_151_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_151_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_152_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_152_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Furnace_152_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Furnace_152_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_152_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_152_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_152_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_152_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_153_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_153_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Furnace_153_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Furnace_153_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_153_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_153_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_153_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_154_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_154_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Furnace_154_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Furnace_154_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_154_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_154_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_154_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_154_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_155_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_155_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Furnace_155_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Furnace_155_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_155_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_155_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Furnace_155_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                #endregion

                #region [Trimming]
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_171_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_171_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Trimming_171_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Trimming_171_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_171_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_171_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_171_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_172_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_172_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Trimming_172_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Trimming_172_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_172_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_172_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_172_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_172_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_173_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_173_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Trimming_173_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Trimming_173_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_173_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_173_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_173_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_174_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_174_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Trimming_174_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Trimming_174_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_174_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_174_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_174_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_174_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_175_P_Active_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_175_P_ReActive_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Trimming_175_P_Active_Khours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                //_mqttClient.Subscribe(new string[] { "DPS/Trimming_175_P_Active_Mhours" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_175_Current_Motor_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_175_Motor_LIFE_Day" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/Trimming_175_Motor_LIFE_Hour" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                #endregion

                #region [LS]
                _mqttClient.Subscribe(new string[] { "DPS/LS_21_MX830" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_21_DW816" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_21_DW817" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_21_DW818" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_21_DW819" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_22_MX830" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_22_DW816" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_22_DW817" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_22_DW818" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_22_DW819" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_23_MX830" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_23_DW816" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_23_DW817" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_23_DW818" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_23_DW819" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_24_MX830" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_24_DW816" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_24_DW817" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_24_DW818" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_24_DW819" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_25_MX830" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_25_DW816" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_25_DW817" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_25_DW818" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/LS_25_DW819" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });

                #endregion

                #region [AC]
                _mqttClient.Subscribe(new string[] { "DPS/AC_21_D3704" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/AC_21_D3705" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/AC_21_D3706" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/AC_22_D3704" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/AC_22_D3705" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/AC_22_D3706" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/AC_23_D3704" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/AC_23_D3705" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/AC_23_D3706" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/AC_24_D3704" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/AC_24_D3705" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Subscribe(new string[] { "DPS/AC_24_D3706" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });

                #endregion

                #region [ACT]

                #endregion


                _mqttClient.Connect(Guid.NewGuid().ToString() + "_Message_Process");
                models2.AddRange(new[] { model_13, model_21, model_22, model_23, model_24, model_25 });
                models.AddRange(new[] { model13, model21, model22, model23, model24, model25 });
                gridModels.AddRange(new[] { list_13, list_21, list_22, list_23, list_24, list_25 });
                gridModels_DCM.AddRange(new[] { list_13_DCM, list_21_DCM, list_22_DCM, list_23_DCM, list_24_DCM, list_25_DCM });
                DataSet ds = new DataSet();

                MySqlConnection connnect = new MySqlConnection(ConnectionString);
                using (connnect)
                {
                    connnect.Open();
                    // work_performance조회 ( start_time = end_time 인것)
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "calculateForSEA_R";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connnect;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }
                int model_index = 0;
                foreach (DataModel model in models) 
                {
                    if (ds.Tables[model_index].Rows.Count > 0) 
                    {
                        models[model_index].ID = ds.Tables[model_index].Rows[0]["WORK_PERFORMANCE_ID"].ToString();
                    }
                    model_index++;
                }
                _tmr = new System.Threading.Timer(new TimerCallback(DataTimerCallback), null, 0, 1000);//3000
                _tmrFOrGrid = new System.Threading.Timer(new TimerCallback(GridTimerCallback), null, 0, 15000);//15000
            }
            catch (Exception)
            {
                WriteLog("Form1_Loads");
                throw;
            }
        }

        private async void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                ProcessData(e.Topic, e.Message);
                string topic = e.Topic.Split('/')[1];
                string message = Encoding.UTF8.GetString(e.Message);

                if (!topic.Contains("_TAG_") && !topic.Contains("_DW"))
                    _mqttClient.Publish($"/event/c/data_collection_digit/{topic}", Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        private void ProcessData(string topic, byte[] message)
        {

            DataModel model;
            DataModel2 model2;

            Int32.TryParse(topic.Split('_')[1], out int index);
            Int32.TryParse(topic.Split('_')[1], out int index2);

            if (topic.Contains("DCM_"))
            {
                GET_DCM(topic, message);
                return;
            }
            if (topic.Contains("LS"))
            {
                return;
            }

            if (topic.Contains("AC"))
            {
                GET_AC(topic, message);
                return;
            }

            if (topic.Contains("RTU_13") || topic.Contains("Casting_"))
            {
                index = index - 160 > 0 ? index - 160 : 0;
                model = models[index];
                model2 = models2[index];

                if (topic.Contains("P_Active_Khours"))
                {
                    model.Consumption_K = double.Parse(Encoding.UTF8.GetString(message));
                    model.K_OK = true;
                    double data = models[index].Consumption_K + models[index].Consumption_M + models[index].ConsumptionRETI + model2.F_ESG_K + model2.F_ESG_M + model2.T_ESG_M + model2.T_ESG_K;
                    SaveWorkData($"UPDATE WORK_DATA SET WORK_POWER = '{data}'", index);
                    SetElec(model, model2, index);
                }
                else if (topic.Contains("P_Active_Mhours"))
                {
                    model.Consumption_M = double.Parse(Encoding.UTF8.GetString(message)) * 1000;
                    model.M_OK = true;
                    double data = models[index].Consumption_K + models[index].Consumption_M + models[index].ConsumptionRETI + model2.F_ESG_K + model2.F_ESG_M + model2.T_ESG_M + model2.T_ESG_K;
                    SaveWorkData($"UPDATE WORK_DATA SET WORK_POWER = '{data}'", index);
                    SetElec(model, model2, index);
                }
                else if (topic.Contains("Load_Total_Power_Consumption"))
                {
                    model.ConsumptionRETI = double.Parse(Encoding.UTF8.GetString(message));
                    model.R_OK = true;
                    double data = models[index].Consumption_K + models[index].Consumption_M + models[index].ConsumptionRETI + model2.F_ESG_K + model2.F_ESG_M + model2.T_ESG_M + model2.T_ESG_K;
                    SaveWorkData($"UPDATE WORK_DATA SET WORK_POWER = '{data}'", index);
                    SetElec(model, model2, index);
                }
                else if (topic.Contains("P_Active_Ruled")) //유효전력 - Unit 0.01
                {
                    model.Active_Power = double.Parse(Encoding.UTF8.GetString(message));
                }
                else if (topic.Contains("P_ReActive_Ruled")) //무효전력 - Unit 0.01
                {
                    model.ReActive_Power = double.Parse(Encoding.UTF8.GetString(message));
                }
                return;
            }

            if (topic.Contains("Furnace"))
            {
                index2 = index2 - 150 > 0 ? index2 - 150 : 0;
                model = models[index2];

                model2 = models2[index2];

                if (topic.Contains("P_Active_Khours"))
                {
                    model2.F_ESG_K = double.Parse(Encoding.UTF8.GetString(message));
                    model2.F_K_OK = true;
                    double data = models[index2].Consumption_K + models[index2].Consumption_M + models[index2].ConsumptionRETI + model2.F_ESG_K + model2.F_ESG_M + model2.T_ESG_M + model2.T_ESG_K;
                    SaveWorkData($"UPDATE WORK_DATA SET WORK_POWER = '{data}'", index2);
                    SetElec(model, model2, index2);
                }
                else if (topic.Contains("P_Active_Mhours"))
                {
                    model2.F_ESG_M = double.Parse(Encoding.UTF8.GetString(message)) * 1000;
                    model2.F_M_OK = true;
                    double data = models[index2].Consumption_K + models[index2].Consumption_M + models[index2].ConsumptionRETI + model2.F_ESG_K + model2.F_ESG_M + model2.T_ESG_M + model2.T_ESG_K;
                    SaveWorkData($"UPDATE WORK_DATA SET WORK_POWER = '{data}'", index2);
                    SetElec(model, model2, index2);
                }
                else if (topic.Contains("P_Active_Ruled")) //유효전력 - Unit 0.01
                {
                    model2.FnActive_Power = double.Parse(Encoding.UTF8.GetString(message));
                }

                return;
            }

            if (topic.Contains("Trimming"))
            {
                index2 = index2 - 170 > 0 ? index2 - 170 : 0;

                model = models[index2];
                model2 = models2[index2];

                if (topic.Contains("P_Active_Khours"))
                {
                    model2.T_ESG_K = double.Parse(Encoding.UTF8.GetString(message));
                    model2.T_K_OK = true;
                    double data = models[index2].Consumption_K + models[index2].Consumption_M + models[index2].ConsumptionRETI + model2.F_ESG_K + model2.F_ESG_M + model2.T_ESG_M + model2.T_ESG_K;
                    SaveWorkData($"UPDATE WORK_DATA SET WORK_POWER = '{data}'", index2);
                    SetElec(model, model2, index2);
                }
                else if (topic.Contains("P_Active_Mhours"))
                {
                    model2.T_ESG_M = double.Parse(Encoding.UTF8.GetString(message)) * 1000;
                    model2.T_M_OK = true;
                    double data = models[index2].Consumption_K + models[index2].Consumption_M + models[index2].ConsumptionRETI + model2.F_ESG_K + model2.F_ESG_M + model2.T_ESG_M + model2.T_ESG_K;
                    SaveWorkData($"UPDATE WORK_DATA SET WORK_POWER = '{data}'", index2);
                    SetElec(model, model2, index2);
                }
                else if (topic.Contains("P_Active_Ruled")) //유효전력 - Unit 0.01
                {
                    model2.tmActive_Power = double.Parse(Encoding.UTF8.GetString(message));
                }

                return;
            }
        }

        
        private void GET_DCM(string topic, byte[] message)
        {
            int indexM = 0;

            switch (topic.Split('_')[1])
            {
                case "13":
                    indexM = 0;
                    break;
                case "21":
                    indexM = 1;
                    break;
                case "22":
                    indexM = 2;
                    break;
                case "23":
                    indexM = 3;
                    break;
                case "24":
                    indexM = 4;
                    break;
                case "25":
                    indexM = 5;
                    break;
            }

            if (topic.Contains("_TAG_D6900_Ruled"))
            {
                gridModels_DCM[indexM].V1 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].V1, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6902_Ruled"))
            {
                gridModels_DCM[indexM].V2 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].V2, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6904_Ruled"))
            {
                gridModels_DCM[indexM].V3 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].V3, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6906_Ruled"))
            {
                gridModels_DCM[indexM].V4 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].V4, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6908"))
            {
                gridModels_DCM[indexM].가속위치 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].가속위치, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6910"))
            {
                gridModels_DCM[indexM].감속위치 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].감속위치, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6912_Ruled"))
            {
                gridModels_DCM[indexM].메탈압력 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].메탈압력, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6914"))
            {
                gridModels_DCM[indexM].승압시간 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].승압시간, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6916"))
            {
                gridModels_DCM[indexM].비스켓두께 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].비스켓두께, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6918"))
            {
                gridModels_DCM[indexM].형체력 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].형체력, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6920_Ruled"))
            {
                gridModels_DCM[indexM].형체력MN = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].형체력MN, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6936_Ruled"))
            {
                gridModels_DCM[indexM].사이클타임 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].사이클타임, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6938_Ruled"))
            {
                gridModels_DCM[indexM].형체중자입시간 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].형체중자입시간, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6940_Ruled"))
            {
                gridModels_DCM[indexM].주탕시간 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].주탕시간, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6942_Ruled"))
            {
                gridModels_DCM[indexM].사출전진시간 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].사출전진시간, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6944_Ruled"))
            {
                gridModels_DCM[indexM].제품냉각시간 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].제품냉각시간, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6946_Ruled"))
            {
                gridModels_DCM[indexM].형개중자후퇴시간 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].형개중자후퇴시간, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6948_Ruled"))
            {
                gridModels_DCM[indexM].압출시간 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].압출시간, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6950_Ruled"))
            {
                gridModels_DCM[indexM].취출시간 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].취출시간, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D6952_Ruled"))
            {
                gridModels_DCM[indexM].스프레이시간 = Encoding.UTF8.GetString(message);
                dm_alram_status_update(gridModels_DCM[indexM].스프레이시간, topic.Split('/')[1]);
            }

            if (topic.Contains("_TAG_D3704"))
            {
                models[indexM].getDtOkCnt = Convert.ToDouble(Encoding.UTF8.GetString(message));
                checkDt(indexM);
            }

            if (topic.Contains("_TAG_D3705"))
            {
                models[indexM].getDtErrCnt = Convert.ToDouble(Encoding.UTF8.GetString(message));
                checkDt(indexM);
            }

            if (topic.Contains("_TAG_D3706"))
            {
                models[indexM].getDtWarmCnt = Convert.ToDouble(Encoding.UTF8.GetString(message));
                checkDt(indexM);
            }
        }

        private void checkDt(int i)
        {
            if (models[i].getDtOkCnt != 0.001 && models[i].getDtErrCnt != 0.001 && models[i].getDtWarmCnt != 0.001)
            {
                bool checkFirst = models[i].getDtTotalCount == 0.001;
                if (checkFirst)
                {
                    models[i].getDtTotalCount = models[i].getDtOkCnt + models[i].getDtErrCnt + models[i].getDtWarmCnt;
                    return;
                };
                if (models[i].getDtTotalCount < models[i].getDtOkCnt + models[i].getDtErrCnt + models[i].getDtWarmCnt)
                {
                    models[i].dt = DateTime.Now;
                }
                models[i].getDtTotalCount = models[i].getDtOkCnt + models[i].getDtErrCnt + models[i].getDtWarmCnt;

            }
        }

        private void SaveWorkData(string sql, int id)
        {
            string mysqlString = sql + $@" WHERE WORK_PERFORMANCE_ID = '{models[id].ID}'";
            MySqlConnection conn2 = new MySqlConnection(ConnectionString);
            using (conn2)
            {
                conn2.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = mysqlString;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn2;
                cmd.ExecuteNonQuery();
            }
        }
        private void GET_AC(string topic, byte[] message)
        {
            if (topic.Contains("3706"))
            {

                int trimingMI = 0;
                switch (topic.Split('_')[1])
                {
                    case "13":
                        trimingMI = 0;
                        break;
                    case "21":
                        trimingMI = 1;
                        break;
                    case "22":
                        trimingMI = 2;
                        break;
                    case "23":
                        trimingMI = 3;
                        break;
                    case "24":
                        trimingMI = 4;
                        break;
                    case "25":
                        trimingMI = 5;
                        break;
                    default:
                        return;
                }
                models2[trimingMI].TRIMING_SHOT = double.Parse(Encoding.UTF8.GetString(message));
            }
        }

        private void dm_alram_status_update(object data, string TAG)
        {
            try
            {
                string mysqlString = $"UPDATE dm_alarm_status " +
                                        $"SET collection_value = {data.ToString()} " +
                                     $" WHERE resource_code = '{TAG}'";

                MySqlConnection conn2 = new MySqlConnection(ConnectionString);
                using (conn2)
                {
                    conn2.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = mysqlString;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn2;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                WriteLog($"Error: {e.Message}");
            }

        }


        private void CalculateAndPublishPowerConsumption(DataModel model, int machineId)
        {

            int index = machineId != 0 ? machineId + 20 : 13;

            MySqlConnection conn = new MySqlConnection(ConnectionString);
            DataSet ds = new DataSet();

            using (conn)
            {
                conn.Open();
                // work_performance조회 ( start_time = end_time 인것)
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = $@"SELECT * FROM ELEC_DAY   WHERE DATETIME = '{DateTime.Now.ToString("yyyy-MM-dd")}' and MACHINE_ID ='{index}';
                                     SELECT * FROM ELEC_MONTH WHERE DATETIME = '{DateTime.Now.ToString("yyyy-MM")}'    and MACHINE_ID ='{index}';
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
            }

            double electricityRate = 153.7; // KRW per kWh (단가)
            double dailyPower = 0;
            double dailyAmount = 0;
            double monthConversion = 0;
            double monthlyAmount = 0;
            double unitAmount = 0;
            double unitPower = 0;


            if (ds.Tables[0].Rows.Count > 0)
            {
                double parsePower = 0;
                double.TryParse(ds.Tables[0].Rows[0]["VALUE"].ToString(), out parsePower);
                dailyPower = model.NowShotKW - parsePower;
                dailyAmount = dailyPower * electricityRate;
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                double parsePower = 0;
                double.TryParse(ds.Tables[0].Rows[0]["VALUE"].ToString(), out parsePower);
                dailyPower = model.NowShotKW - parsePower;
                dailyAmount = dailyPower * electricityRate;
            }

            unitPower = model.NowShotKW;
            unitAmount = model.NowShotKW * electricityRate;

            DataModel2 model2 = models2[machineId];
            DataSet ds2 = new DataSet();

            // MySQL 연결 및 데이터 조회
            using (MySqlConnection conn2 = new MySqlConnection(ConnectionString))
            {
                string sql = $@"SELECT START_POWER
                                     , WORK_POWER
                                  FROM WORK_DATA 
                                 WHERE WORK_PERFORMANCE_ID = '{model.ID}'; ";
                conn2.Open();

                using (MySqlCommand cmd = new MySqlCommand(sql, conn2))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds2); // 시계열 데이터를 데이터셋에 채움
                    }
                }
            }

            //유효적산전력 (누적전력량) 계산      * 유효 전력 적산 계산예시 - KWh 표현 ( 1(MWh) x 1000 + 300(KWh)) = 1300 KWh 
            double Cumulative_Power = 0;
            Cumulative_Power = 0;
            if (ds2.Tables[0].Rows.Count > 0)
            {
                double work_power = 0;
                double.TryParse(ds2.Tables[0].Rows[0]["WORK_POWER"].ToString(), out work_power);
                double start_power = 0;
                double.TryParse(ds2.Tables[0].Rows[0]["WORK_POWER"].ToString(), out start_power);
                Cumulative_Power = work_power - start_power;
            }

            if (machineId == 0)
            {
                // MQTT로 전송
                _mqttClient.Publish($"/event/c/data_collection_digit/RTU_13_01_Month_Power_Amount", Encoding.UTF8.GetBytes(monthlyAmount.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/RTU_13_01_Load_Power_Consumption_Today_Conversion", Encoding.UTF8.GetBytes(monthConversion.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/RTU_13_01_Daily_Power_Amount", Encoding.UTF8.GetBytes(dailyAmount.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/RTU_13_01_Daily_Power_Consumption", Encoding.UTF8.GetBytes(dailyPower.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/RTU_13_01_Unit_Power_Amount", Encoding.UTF8.GetBytes(unitAmount.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/RTU_13_01_Unit_Power_Consumption", Encoding.UTF8.GetBytes(unitPower.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);

                WriteLog($"13호기 데이터 MQTT 전송 완료");

            }
            else
            {
                // 누적 사용량  Casting_162_Cumulative_Power
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Cumulative_Power", Encoding.UTF8.GetBytes(Cumulative_Power.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_All_Active_Power", Encoding.UTF8.GetBytes(model.All_Active_Power.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Month_Power_Amount", Encoding.UTF8.GetBytes(monthlyAmount.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Load_Power_Consumption_Today_Conversion", Encoding.UTF8.GetBytes(monthConversion.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Daily_Power_Amount", Encoding.UTF8.GetBytes(dailyAmount.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Daily_Power_Consumption", Encoding.UTF8.GetBytes(dailyPower.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Unit_Power_Amount", Encoding.UTF8.GetBytes(unitAmount.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Unit_Power_Consumption", Encoding.UTF8.GetBytes(unitPower.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                WriteLog($"{machineId + 20}호기 데이터 MQTT 전송 완료");
            }

        }

        private async void DataTimerCallback(object state)
        {
            try
            {
                Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/SHOTKW_13", Encoding.UTF8.GetBytes((model13.NowShotKW).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/SHOTKW_21", Encoding.UTF8.GetBytes((model21.NowShotKW).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/SHOTKW_22", Encoding.UTF8.GetBytes((model22.NowShotKW).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/SHOTKW_23", Encoding.UTF8.GetBytes((model23.NowShotKW).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/SHOTKW_24", Encoding.UTF8.GetBytes((model24.NowShotKW).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/SHOTKW_25", Encoding.UTF8.GetBytes((model25.NowShotKW).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            }
            catch (Exception ex)
            {
                WriteLog($"Error : {ex.Message}");
            }
        }

        public async Task ThreadMethodAsync(int i, int timer, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    int machine_id;
                    switch (i)
                    {
                        case 0:
                            machine_id = 13;
                            break;
                        case 1:
                            machine_id = 21;

                            break;
                        case 2:
                            machine_id = 22;

                            break;
                        case 3:
                            machine_id = 23;

                            break;
                        case 4:
                            machine_id = 24;

                            break;
                        case 5:
                            machine_id = 25;

                            break;
                        default:
                            return;

                    }
                    GET_DCM gET = new GET_DCM(i);
                    await gET.GetPlcAsync(gridModels_DCM[i], models[i]);

                    DataSet ds = new DataSet();

                    MySqlConnection connnect = new MySqlConnection(ConnectionString);
                    using (connnect)
                    {
                        connnect.Open();
                        // work_performance조회 ( start_time = end_time 인것)
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.CommandText = $@"  SELECT * 
                          FROM work_performance 
                         WHERE start_time  = end_time 
                           AND machine_no  = 'WCI_D{machine_id}' 
                      ORDER BY id DESC 
                         LIMIT 1;";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connnect;
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(ds);
                    }

                    if (models[i].is_Running) return;
                    models[i].is_Running = true;
                    WriteLog("Data Received");

                    int nowPordCnt = 0;

                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        try
                        {
                            Get_DCM(i, gridModels_DCM[i]); //값 보내주기

                            int cavity = 1;
                            string cavitySql = $@"SELECT CAVITY 
                                                FROM SEA_MFG.DBO.MD_MST 
                                               WHERE CODE_MD = ( SELECT CODE_MD 
                                                                   FROM [sea_mfg].dbo.demand_mstr_ext 
                                                                  WHERE LOT='{ds.Tables[i].Rows[0]["LOT_NO"].ToString()}' 
                                                                    AND order_no ='{ds.Tables[i].Rows[0]["RESOURCE_NO"].ToString()}')";

                            using (SqlConnection sqlconn = new SqlConnection("Server=10.10.10.180; Database=HS_MES; User Id=hansol_mes; Password=Hansol123!@#;"))
                            {
                                sqlconn.Open();
                                using (SqlCommand sqlcmd = new SqlCommand(cavitySql, sqlconn))
                                {
                                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            cavity = 1; 
                                            Int32.TryParse(reader["CAVITY"].ToString(),out cavity);
                                        }
                                    }
                                }
                            }

                            string WORK_PERFORMANCE_ID = string.IsNullOrWhiteSpace(ds.Tables[i].Rows[0]["WORK_PERFORMANCE_ID"].ToString()) ? "" : ds.Tables[i].Rows[0]["WORK_PERFORMANCE_ID"].ToString();
                            string WORK_OKCNT = string.IsNullOrWhiteSpace(ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString()) ? "0" : ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString();
                            string WORK_WARMUPCNT = string.IsNullOrWhiteSpace(ds.Tables[i].Rows[0]["WORK_WARMUPCNT"].ToString()) ? "0" : ds.Tables[i].Rows[0]["WORK_WARMUPCNT"].ToString();
                            string WORK_ERRCOUNT = string.IsNullOrWhiteSpace(ds.Tables[i].Rows[0]["WORK_ERRCOUNT"].ToString()) ? "0" : ds.Tables[i].Rows[0]["WORK_ERRCOUNT"].ToString();
                            double power = models2[i].F_ESG_K + models2[i].F_ESG_M + models2[i].T_ESG_K + models2[i].T_ESG_M + models[i].Consumption_M + models[i].Consumption_K + models[i].ConsumptionRETI;
                            double okcnt = models[i].getDtOkCnt;
                            double errcnt = models[i].getDtErrCnt;
                            double warmcnt = models[i].getDtWarmCnt;
                            if (WORK_PERFORMANCE_ID != models[i].ID)
                            {
                                models[i] = new DataModel() { ID = WORK_PERFORMANCE_ID };
                                //double power = models2[i].F_ESG_K + models2[i].F_ESG_M + models2[i].T_ESG_K + models2[i].T_ESG_M + models[i].Consumption_M + models[i].Consumption_K + models[i].ConsumptionRETI;
                                //double okcnt = models[i].getDtOkCnt;
                                //double errcnt = models[i].getDtErrCnt;
                                //double warmcnt = models[i].getDtWarmCnt;

                                string sql = $@"  UPDATE WORK_DATA  
                                                SET 
                                                    START_POWER = {power},
                                                    WORK_POWER = {power},
                                                    LAST_POWER = {power},
                                                    START_OKCNT = {okcnt},
                                                    START_WARMUPCNT = {warmcnt},
                                                    START_ERRCOUNT = {errcnt},
                                                    WORK_OKCNT = {okcnt},
                                                    WORK_WARMUPCNT = {warmcnt},
                                                    WORK_ERRCOUNT = {errcnt}
                                            WHERE WORK_PERFORMANCE_ID = '{models[i].ID}';";
                                //여기 수정해야함
                                MySqlConnection conn4 = new MySqlConnection(ConnectionString);
                                using (conn4)
                                {
                                    conn4.Open();

                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.CommandText = sql;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Connection = conn4;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            int intwork_okcnt = 0;
                            int intwork_errcnt = 0;
                            int intwork_warmcnt = 0;
                            Int32.TryParse(WORK_OKCNT, out intwork_okcnt);
                            Int32.TryParse(WORK_WARMUPCNT, out intwork_warmcnt);
                            Int32.TryParse(WORK_ERRCOUNT, out intwork_errcnt);

                            int nowtotalcnt = (intwork_okcnt / cavity)
                                + intwork_warmcnt
                                + (intwork_errcnt / cavity);

                            nowPordCnt = intwork_okcnt
                                + intwork_errcnt;

                            if (models[i].Totalcnt != -1 && models[i].Totalcnt < nowtotalcnt)
                            {

                                string workSql = $@"   UPDATE work_performance
                                                      SET work_power = IFNULL( ( SELECT 
                                                                                    CASE 
                                                                                        WHEN WORK_POWER < LAST_POWER THEN (WORK_POWER + 65535) - LAST_POWER +1
                                                                                        ELSE WORK_POWER - LAST_POWER
                                                                                    END
                                                                                FROM WORK_DATA
                                                                                WHERE WORK_PERFORMANCE_ID = '{models[i].ID}')
                                                                             , 0)
                                                    WHERE WORK_PERFORMANCE_ID = '{models[i].ID}';

                                                   UPDATE WORK_DATA 
                                                      SET LAST_POWER = WORK_POWER
                                                    WHERE WORK_PERFORMANCE_ID = '{models[i].ID}'; ";

                                MySqlConnection conn4 = new MySqlConnection(ConnectionString);
                                using (conn4)
                                {
                                    conn4.Open();

                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.CommandText = workSql;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Connection = conn4;
                                    cmd.ExecuteNonQuery();
                                }

                                string WORK_POWERsql = $@"SELECT WORK_POWER 
                                                        FROM WORK_PERFORMANCE
                                                       WHERE WORK_PERFORMANCE_ID = '{models[i].ID}';
                                                     ";

                                DataSet ds2 = new DataSet();
                                MySqlConnection conn5 = new MySqlConnection(ConnectionString);

                                using (conn5)
                                {
                                    conn5.Open();

                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.CommandText = WORK_POWERsql;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Connection = conn5;
                                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                    adapter.Fill(ds2);
                                }

                                string WORK_POWER = string.IsNullOrWhiteSpace(ds2.Tables[0].Rows[0]["WORK_POWER"].ToString()) ? "0" : ds2.Tables[0].Rows[0]["WORK_POWER"].ToString();

                    

                                string mysqlString = $"INSERT INTO data_for_grid                                                                      " +
                                                     $"(                                                                                              " +
                                                     $"`date`,                                                                                        " +
                                                     $"machine_no,                                                                                    " +
                                                     $"V1,                                                                                            " +
                                                     $"V2,                                                                                            " +
                                                     $"V3,                                                                                            " +
                                                     $"V4,                                                                                            " +
                                                     $"acceleration_pos,                                                                              " +
                                                     $"deceleration_pos,                                                                              " +
                                                     $"metal_pressure,                                                                                " +
                                                     $"swap_time,                                                                                     " +
                                                     $"biskit_thickness,                                                                              " +
                                                     $"physical_strength_per,                                                                         " +
                                                     $"physical_strength_mn,                                                                          " +
                                                     $"cycle_time,                                                                                    " +
                                                     $"type_weight_enrty_time,                                                                        " +
                                                     $"bath_time,                                                                                     " +
                                                     $"forward_time,                                                                                  " +
                                                     $"freezing_time,                                                                                 " +
                                                     $"type_weight_back_time,                                                                         " +
                                                     $"extrusion_time,                                                                                " +
                                                     $"extraction_time,                                                                               " +
                                                     $"spray_time,                                                                                    " +
                                                     $"cavity_core,                                                                                   " +
                                                     $"A_Pollution_degree,                                                                            " +
                                                     $"B_Pollution_degree                                                                             " +
                                                     $", vacuum                                                                                         " +
                                                     $", SHOTCNT                                                                                       " +
                                                     $")                                                                                              " +
                                                     $"VALUES                                                                                         " +
                                                     $"(                                                                                              " +
                                                     $"now(),                                                                                         " +
                                                     $"'WCI_D{machine_id}',                                                                                     " +
                                                     $"'{gridModels_DCM[i].V1}', " +
                                                     $"'{gridModels_DCM[i].V2}', " +
                                                     $"'{gridModels_DCM[i].V3}', " +
                                                     $"'{gridModels_DCM[i].V4}', " +
                                                     $"'{gridModels_DCM[i].가속위치}',       " +
                                                     $"'{gridModels_DCM[i].감속위치}',       " +
                                                     $"'{gridModels_DCM[i].메탈압력}',       " +
                                                     $"'{gridModels_DCM[i].승압시간}',       " +
                                                     $"'{gridModels_DCM[i].비스켓두께}',       " +
                                                     $"'{gridModels_DCM[i].형체력}',       " +
                                                     $"'{gridModels_DCM[i].형체력MN}', " +
                                                     $"'{gridModels_DCM[i].사이클타임}', " +
                                                     $"'{gridModels_DCM[i].형체중자입시간}', " +
                                                     $"'{gridModels_DCM[i].주탕시간}', " +
                                                     $"'{gridModels_DCM[i].사출전진시간}', " +
                                                     $"'{gridModels_DCM[i].제품냉각시간}', " +
                                                     $"'{gridModels_DCM[i].형개중자후퇴시간}', " +
                                                     $"'{gridModels_DCM[i].압출시간}', " +
                                                     $"'{gridModels_DCM[i].취출시간}', " +
                                                     $"'{gridModels_DCM[i].스프레이시간}', " +
                                                     $"'{gridModels_DCM[i].금형내부}', " +
                                                     $"'{gridModels_DCM[i].오염도A}', " +
                                                     $"'{gridModels_DCM[i].오염도B}', " +
                                                     $"'{gridModels_DCM[i].탱크진공}', " +
                                                     $"'{nowtotalcnt}'" +
                                                     $");                                                                                             " +

                                                     $"INSERT INTO data_for_grid2                                                                      " +
                                                     $"(                                                                                              " +
                                                     $"`date`,                                                                                        " +
                                                     $"machine_no,                                                                                    " +
                                                     $"V1,                                                                                            " +
                                                     $"V2,                                                                                            " +
                                                     $"V3,                                                                                            " +
                                                     $"V4,                                                                                            " +
                                                     $"acceleration_pos,                                                                              " +
                                                     $"deceleration_pos,                                                                              " +
                                                     $"metal_pressure,                                                                                " +
                                                     $"swap_time,                                                                                     " +
                                                     $"biskit_thickness,                                                                              " +
                                                     $"physical_strength_per,                                                                         " +
                                                     $"physical_strength_mn,                                                                          " +
                                                     $"cycle_time,                                                                                    " +
                                                     $"type_weight_enrty_time,                                                                        " +
                                                     $"bath_time,                                                                                     " +
                                                     $"forward_time,                                                                                  " +
                                                     $"freezing_time,                                                                                 " +
                                                     $"type_weight_back_time,                                                                         " +
                                                     $"extrusion_time,                                                                                " +
                                                     $"extraction_time,                                                                               " +
                                                     $"spray_time,                                                                                    " +
                                                     $"cavity_core,                                                                                   " +
                                                     $"A_Pollution_degree,                                                                            " +
                                                     $"B_Pollution_degree                                                                             " +
                                                     $", vacuum                                                                                         " +
                                                     $", SHOTCNT                                                                                       " +
                                                     $")                                                                                              " +
                                                     $"VALUES ( now(),                                                                                         " +
                                                     $"'WCI_D{machine_id}',                                                                                     " +
                                                     $"'{gridModels_DCM[i].V1}', " +
                                                     $"'{gridModels_DCM[i].V2}', " +
                                                     $"'{gridModels_DCM[i].V3}', " +
                                                     $"'{gridModels_DCM[i].V4}', " +
                                                     $"'{gridModels_DCM[i].가속위치}',       " +
                                                     $"'{gridModels_DCM[i].감속위치}',       " +
                                                     $"'{gridModels_DCM[i].메탈압력}',       " +
                                                     $"'{gridModels_DCM[i].승압시간}',       " +
                                                     $"'{gridModels_DCM[i].비스켓두께}',       " +
                                                     $"'{gridModels_DCM[i].형체력}',       " +
                                                     $"'{gridModels_DCM[i].형체력MN}', " +
                                                     $"'{gridModels_DCM[i].사이클타임}', " +
                                                     $"'{gridModels_DCM[i].형체중자입시간}', " +
                                                     $"'{gridModels_DCM[i].주탕시간}', " +
                                                     $"'{gridModels_DCM[i].사출전진시간}', " +
                                                     $"'{gridModels_DCM[i].제품냉각시간}', " +
                                                     $"'{gridModels_DCM[i].형개중자후퇴시간}', " +
                                                     $"'{gridModels_DCM[i].압출시간}', " +
                                                     $"'{gridModels_DCM[i].취출시간}', " +
                                                     $"'{gridModels_DCM[i].스프레이시간}', " +
                                                     $"'{gridModels_DCM[i].금형내부}', " +
                                                     $"'{gridModels_DCM[i].오염도A}', " +
                                                     $"'{gridModels_DCM[i].오염도B}', " +
                                                     $"'{gridModels_DCM[i].탱크진공}', " +
                                                     $"'{nowtotalcnt}'" +
                                                     $");                                                                                             ";


                                MySqlConnection conn2 = new MySqlConnection(ConnectionString);
                                using (conn2)
                                {
                                    conn2.Open();

                                    MySqlCommand cmd = new MySqlCommand();
                                    cmd.CommandText = mysqlString;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Connection = conn2;
                                    cmd.ExecuteNonQuery();
                                }

                                double parseWorkPower = 0;
                                double.TryParse(WORK_POWER,out parseWorkPower);
                                models[i].All_Active_Power = parseWorkPower;
                                if ((models[i].Consumption_K + models[i].Consumption_M + models[i].ConsumptionRETI + models2[i].F_ESG_K + models2[i].F_ESG_M + models2[i].T_ESG_M + models2[i].T_ESG_K) - models[i].NowShotKW > 0)
                                {
                                    models[i].NowShotKW = models[i].Consumption_K + models[i].Consumption_M + models[i].ConsumptionRETI + models2[i].F_ESG_K + models2[i].F_ESG_M + models2[i].T_ESG_M + models2[i].T_ESG_K;
                                }

                                using (SqlConnection sqlconn = new SqlConnection("Server = 10.10.10.180; Database = HS_MES; User Id = hansol_mes; Password = Hansol123!@#;"))
                                {
                                    sqlconn.Open();
                                    using (SqlCommand sqlcmd = new SqlCommand())
                                    {
                                        // msSQL [ELEC_SHOT] - 작업지시가 내려져 있을때만 샷당 설비데이터 저장
                                        string dtValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        sqlcmd.Connection = sqlconn;
                                        sqlcmd.CommandType = CommandType.StoredProcedure;
                                        sqlcmd.CommandText = "USP_ELECTRIC_USE_DPS_A20";
                                        sqlcmd.Parameters.AddWithValue("@Date", dtValue);
                                        sqlcmd.Parameters.AddWithValue("@MACHINE_NO", gridModels_DCM[i].설비No);
                                        sqlcmd.Parameters.AddWithValue("@ORDER_NO", $"{ds.Tables[i].Rows[0]["ORDER_NO"]}");
                                        sqlcmd.Parameters.AddWithValue("@RESOURCE_NO", $"{ds.Tables[i].Rows[0]["RESOURCE_NO"]}");
                                        sqlcmd.Parameters.AddWithValue("@LOT_NO", $"{ds.Tables[i].Rows[0]["LOT_NO"]}");
                                        sqlcmd.Parameters.AddWithValue("@ELECTRICAL_ENERGY", (models[i].All_Active_Power).ToString("F2"));
                                        sqlcmd.Parameters.AddWithValue("@V1", gridModels_DCM[i].V1);
                                        sqlcmd.Parameters.AddWithValue("@V2", gridModels_DCM[i].V2);
                                        sqlcmd.Parameters.AddWithValue("@V3", gridModels_DCM[i].V3);
                                        sqlcmd.Parameters.AddWithValue("@V4", gridModels_DCM[i].V4);
                                        sqlcmd.Parameters.AddWithValue("@가속위치", gridModels_DCM[i].가속위치);
                                        sqlcmd.Parameters.AddWithValue("@감속위치", gridModels_DCM[i].감속위치);
                                        sqlcmd.Parameters.AddWithValue("@메탈압력", gridModels_DCM[i].메탈압력);
                                        sqlcmd.Parameters.AddWithValue("@승압시간", gridModels_DCM[i].승압시간);
                                        sqlcmd.Parameters.AddWithValue("@비스켓두께", gridModels_DCM[i].비스켓두께);
                                        sqlcmd.Parameters.AddWithValue("@형체력", gridModels_DCM[i].형체력);
                                        sqlcmd.Parameters.AddWithValue("@형체력MN", gridModels_DCM[i].형체력MN);
                                        sqlcmd.Parameters.AddWithValue("@사이클타임", gridModels_DCM[i].사이클타임);
                                        sqlcmd.Parameters.AddWithValue("@형체중자입시간", gridModels_DCM[i].형체중자입시간);
                                        sqlcmd.Parameters.AddWithValue("@주탕시간", gridModels_DCM[i].주탕시간);
                                        sqlcmd.Parameters.AddWithValue("@사출전진시간", gridModels_DCM[i].사출전진시간);
                                        sqlcmd.Parameters.AddWithValue("@제품냉각시간", gridModels_DCM[i].제품냉각시간);
                                        sqlcmd.Parameters.AddWithValue("@형개중자후퇴시간", gridModels_DCM[i].형개중자후퇴시간);
                                        sqlcmd.Parameters.AddWithValue("@압출시간", gridModels_DCM[i].압출시간);
                                        sqlcmd.Parameters.AddWithValue("@취출시간", gridModels_DCM[i].취출시간);
                                        sqlcmd.Parameters.AddWithValue("@스프레이시간", gridModels_DCM[i].스프레이시간);
                                        sqlcmd.Parameters.AddWithValue("@금형내부", gridModels_DCM[i].금형내부);
                                        sqlcmd.Parameters.AddWithValue("@오염도A", gridModels_DCM[i].오염도A);
                                        sqlcmd.Parameters.AddWithValue("@오염도B", gridModels_DCM[i].오염도B);
                                        sqlcmd.Parameters.AddWithValue("@탱크진공", gridModels_DCM[i].탱크진공);
                                        sqlcmd.Parameters.AddWithValue("@TotalCnt", nowtotalcnt);
                                        sqlcmd.ExecuteNonQuery();

                                        WriteLog("SHOT Data Processed");
                                    }

                                }

                                // MSSQL 전달
                                using (SqlConnection sqlconn = new SqlConnection("Server = 10.10.10.180; Database = HS_MES; User Id = hansol_mes; Password = Hansol123!@#;"))
                                {
                                    sqlconn.Open();
                                    using (SqlCommand sqlcmd = new SqlCommand())
                                    {
                                        sqlcmd.Connection = sqlconn;
                                        sqlcmd.CommandType = CommandType.StoredProcedure;
                                        sqlcmd.CommandText = "USP_ELECTRIC_USE_DPS_A10";
                                        sqlcmd.Parameters.AddWithValue("@RESOURCE_NO", ds.Tables[i].Rows[0]["RESOURCE_NO"].ToString());
                                        sqlcmd.Parameters.AddWithValue("@LOT_NO", ds.Tables[i].Rows[0]["LOT_NO"].ToString());
                                        sqlcmd.Parameters.AddWithValue("@ELEC_USE", (models[i].All_Active_Power).ToString("F2"));
                                        sqlcmd.ExecuteNonQuery();
                                    }
                                }

                            }
                            else
                            {
                                if (models[i].Totalcnt != -1)
                                {
                                    int machine_id;
                                    switch (i)
                                    {
                                        case 0:
                                            machine_id = 13;
                                            break;

                                        case 1:
                                            machine_id = 21;
                                            break;

                                        case 2:
                                            machine_id = 22;
                                            break;

                                        case 3:
                                            machine_id = 23;
                                            break;

                                        case 4:
                                            machine_id = 24;
                                            break;

                                        case 5:
                                            machine_id = 25;
                                            break;

                                        default:
                                            return;

                                    }

                                    string mysqlString = $@"CREATE TEMPORARY TABLE TempData AS
                                                        SELECT id
                                                          FROM data_for_grid2
                                                         WHERE machine_no = 'WCI_D{machine_id}'
                                                         ORDER BY id DESC LIMIT 3;

                                                        UPDATE data_for_grid2
                                                           SET date = now(),
                                                               cycle_time = '{gridModels_DCM[i].사이클타임}',
                                                               type_weight_enrty_time = '{gridModels_DCM[i].형체중자입시간}',
                                                               bath_time = '{gridModels_DCM[i].주탕시간}',
                                                               forward_time = '{gridModels_DCM[i].사출전진시간}',
                                                               freezing_time = '{gridModels_DCM[i].제품냉각시간}',
                                                               type_weight_back_time = '{gridModels_DCM[i].형개중자후퇴시간}',
                                                               extrusion_time = '{gridModels_DCM[i].압출시간}',
                                                               extraction_time = '{gridModels_DCM[i].취출시간}',
                                                               spray_time = '{gridModels_DCM[i].스프레이시간}'
                                                         WHERE id IN(SELECT id FROM TempData) AND SHOTCNT = '{nowtotalcnt}';

                                                          DROP TEMPORARY TABLE TempData;";

                                    MySqlConnection conn2 = new MySqlConnection(ConnectionString);
                                    using (conn2)
                                    {
                                        conn2.Open();

                                        MySqlCommand cmd = new MySqlCommand();
                                        cmd.CommandText = mysqlString;
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Connection = conn2;
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            models[i].Totalcnt = nowtotalcnt;
                            models[i].PROD_CNT = nowPordCnt;



                            string work_performanceSql = $@" UPDATE work_performance
                                                            SET work_okcnt = IFNULL((SELECT CASE WHEN WORK_OKCNT < START_OKCNT THEN ((WORK_OKCNT + 65535) - START_OKCNT)+1
                                                                                            ELSE WORK_OKCNT - START_OKCNT END 
                                                                                          - CASE WHEN WORK_ERRCOUNT < START_ERRCOUNT THEN ((WORK_ERRCOUNT + 65535) - START_ERRCOUNT)+1
                                                                                            ELSE WORK_ERRCOUNT - START_ERRCOUNT END
                                                                                       FROM WORK_DATA
                                                                                      WHERE WORK_PERFORMANCE_ID = '{models[i].ID}' )
                                                                                   , 0) * {cavity},
                                                                work_errcount = IFNULL(( SELECT CASE WHEN WORK_ERRCOUNT < START_ERRCOUNT THEN ((WORK_ERRCOUNT + 65535) - START_ERRCOUNT)+1
                                                                                                ELSE WORK_ERRCOUNT - START_ERRCOUNT END
                                                                                           FROM WORK_DATA
                                                                                          WHERE WORK_PERFORMANCE_ID = '{models[i].ID}' )
                                                                                      , 0) * {cavity},
                                                                work_warmupcnt = IFNULL(( SELECT CASE WHEN WORK_WARMUPCNT < START_WARMUPCNT THEN ((WORK_WARMUPCNT + 65535) - START_WARMUPCNT)+1
                                                                                                 ELSE WORK_WARMUPCNT - START_WARMUPCNT END
                                                                                            FROM WORK_DATA
                                                                                           WHERE WORK_PERFORMANCE_ID = '{models[i].ID}' )
                                                                                       , 0)
                                                          WHERE end_time = start_time
                                                            AND WORK_PERFORMANCE_ID = '{models[i].ID}'
                                                          ORDER BY ID DESC LIMIT 1; ";

                            MySqlConnection conn3 = new MySqlConnection(ConnectionString);
                            using (conn3)
                            {
                                conn3.Open();

                                MySqlCommand cmd = new MySqlCommand();
                                cmd.CommandText = work_performanceSql;
                                cmd.CommandType = CommandType.Text;
                                cmd.Connection = conn3;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            //여기서 입력문자열 예외 발생
                            WriteLog(ex.Message);
                        }
                    }
                    else
                    {
                        Get_DCM(i); //작업지시 없으면 0으로 초기화
                        WaitOrder();
                    }

                    switch (i)
                    {
                        case 0:
                            SendMQTT(ds, i, 13);
                            break;
                        case 1:
                            SendMQTT(ds, i, 21);

                            break;
                        case 2:
                            SendMQTT(ds, i, 23);

                            break;
                        case 3:
                            SendMQTT(ds, i, 23);

                            break;
                        case 4:
                            SendMQTT(ds, i, 24);

                            break;
                        case 5:
                            SendMQTT(ds, i, 25);

                            break;
                        default:
                            break;
                    }
                    CalculateAndPublishPowerConsumption(models[i], i);
                    models[i].is_Running = false;
                    await Task.Delay(timer * 1000);
                }
                catch (Exception e) 
                {
                    WriteLog(e.Message.ToString());
                }
                
            }

        }


        #region SEND_MQTT

        private void SendMQTT(DataSet ds, int i, int machine_no)
        {
            try
            {
                if (ds.Tables[i].Rows.Count > 0)
                {
                    int ok_cnt = 0;
                    int plan = 1;
                    Int32.TryParse(ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"].ToString().Split('.')[0], out plan);
                    Int32.TryParse(ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString(), out ok_cnt);

                    //퍼센트 계산 
                    // (양품개수 / 지시수량 )*100
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_{machine_no}", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["ORDER_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_{machine_no}", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["RESOURCE_NO"]}_{ds.Tables[i].Rows[0]["LOT_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_{machine_no}", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_POWER"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_{machine_no}", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_ERRCOUNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_{machine_no}", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_WARMUPCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_{machine_no}", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_{machine_no}", Encoding.UTF8.GetBytes(models[i].Totalcnt.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_{machine_no}", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_{machine_no}", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"]} / {ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_{machine_no}", Encoding.UTF8.GetBytes($"{(ok_cnt / plan * 10000).ToString("F2")}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_{machine_no}", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["IS_WORKING"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_{machine_no}", Encoding.UTF8.GetBytes(DateTime.Now.Subtract(Convert.ToDateTime(ds.Tables[i].Rows[0]["START_TIME"])).ToString(@"dd\.hh\:mm\:ss")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_{machine_no}", Encoding.UTF8.GetBytes((models[i].NowESG + models[i].NowRETI + (models2[i].F_ESG_K + models2[i].F_ESG_M) + (models2[i].T_ESG_K + models2[i].T_ESG_M)).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PROD_CNT_{machine_no}", Encoding.UTF8.GetBytes(models[i].PROD_CNT.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                else
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_{machine_no}", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_{machine_no}", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_{machine_no}", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_{machine_no}", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_{machine_no}", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_{machine_no}", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_{machine_no}", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_{machine_no}", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_{machine_no}", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_{machine_no}", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_{machine_no}", Encoding.UTF8.GetBytes("비가동"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_{machine_no}", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_{machine_no}", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PROD_CNT_{machine_no}", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                WriteLog("13호기 MQTT Send");
            }
            catch (Exception)
            {
                WriteLog("13호기 값 없음");
            }
        }

        #endregion

        private async void GridTimerCallback(object state)
        {
            try
            {
                //0; 0 < 5; 1++
                for (int i = 0; i < gridModels.Count - 1; i++)
                {
                    if (gridModels[i].Count > 0 && gridModels[i] != null)
                    {
                        switch (i.ToString())
                        {
                            case "0":
                                Task.Run(() => _mqttClient.Publish($"/event/c/DataGrid/A_13_DATAGRID", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModels[i])), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                                Thread.Sleep(100);
                                break;
                            case "1":
                                Task.Run(() => _mqttClient.Publish($"/event/c/DataGrid/A_21_DATAGRID", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModels[i])), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                                Thread.Sleep(100);
                                break;
                            case "2":
                                Task.Run(() => _mqttClient.Publish($"/event/c/DataGrid/A_22_DATAGRID", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModels[i])), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                                Thread.Sleep(100);
                                break;
                            case "3":
                                Task.Run(() => _mqttClient.Publish($"/event/c/DataGrid/A_23_DATAGRID", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModels[i])), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                                Thread.Sleep(100);
                                break;
                            case "4":
                                Task.Run(() => _mqttClient.Publish($"/event/c/DataGrid/A_24_DATAGRID", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModels[i])), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                                Thread.Sleep(100);
                                break;
                            case "5":
                                Task.Run(() => _mqttClient.Publish($"/event/c/DataGrid/A_25_DATAGRID", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModels[i])), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                                Thread.Sleep(100);
                                break;
                            default:
                                Thread.Sleep(100);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        /// <summary>
        /// 초기 세팅으로 돌아가기
        /// </summary>
        private void WaitOrder()
        {
            lbl_Status.Text = "Waiting For Order...";
            WriteLog("Waiting For Order...");
        }


        /// <summary>
        /// lb_logbox에 시간과 함께 로그 작성
        /// </summary>
        private void WriteLog(string log)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    lb_logbox.Items.Insert(0, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : {log}");

                    if (lb_logbox.Items.Count > 100)
                    {
                        lb_logbox.Items.RemoveAt(lb_logbox.Items.Count - 1);
                    }
                }));
            }
            else
            {
                lb_logbox.Items.Insert(0, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : {log}");

                if (lb_logbox.Items.Count > 100)
                {
                    lb_logbox.Items.RemoveAt(lb_logbox.Items.Count - 1);
                }
            }
        }

        private void Get_DCM(int i, GridModel gridModel)
        {
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6900_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.V1).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6902_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.V2).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6904_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.V3).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6906_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.V4).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6908", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.가속위치).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6910", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.감속위치).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6912_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.메탈압력).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6914", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.승압시간).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6916", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.비스켓두께).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6918", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.형체력).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6920_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.형체력MN).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6936_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.사이클타임).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6938_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.형체중자입시간).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6940_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.주탕시간).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6942_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.사출전진시간).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6944_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.제품냉각시간).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6946_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.형개중자후퇴시간).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6948_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.압출시간).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6950_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.취출시간).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6952_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.스프레이시간).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW816", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.금형내부).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW817", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.오염도A).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW818", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.오염도B).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW819", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.탱크진공).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
        }

        private void Get_DCM(int i)
        {
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6902_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6900_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6904_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6906_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6908", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6910", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6912_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6914", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6916", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6918", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6920_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6936_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6938_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6940_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6942_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6944_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6946_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6948_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6950_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/DCM_{machines[i]}_TAG_D6952_Ruled", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW816", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW817", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW818", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW819", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));

        }

        private void SetElec(DataModel model, DataModel2 model2, int index)
        {
            if (model.K_OK && model.M_OK && (index == 0 && model.R_OK) || (index != 0 && model2.T_K_OK && model2.T_M_OK && model2.F_K_OK && model2.F_M_OK))
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    DataSet ds = new DataSet();
                    conn.Open();
                    // 다중 SELECT 쿼리
                    string sql = @" SELECT Count(*) AS Count FROM elec_day WHERE DATETIME = @dateDay AND MACHINE_ID = @machineNo;
                                    SELECT Count(*) AS Count FROM elec_month WHERE DATETIME = @dateMonth AND MACHINE_ID = @machineNo;
                                ";
                    int machineid = index != 0 ? index + 20 : 13;

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {

                        // 매개변수 추가
                        cmd.Parameters.AddWithValue("@dateDay", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@dateMonth", DateTime.Now.ToString("yyyy-MM"));
                        cmd.Parameters.AddWithValue("@machineNo", machineid);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds); // 두 SELECT 결과를 DataSet에 채움
                        }
                    }

                    double data = model.Consumption_K + model.Consumption_M + model.ConsumptionRETI + model2.F_ESG_K + model2.F_ESG_M + model2.T_ESG_M + model2.T_ESG_K;

                    // `elec_day`에 데이터가 없으면 INSERT 실행
                    if (ds.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0]["Count"]) == 0)
                    {
                        string insertDaySql = @" INSERT INTO elec_day (DATETIME, VALUE, MACHINE_ID)
                                                 VALUES (@dateDay, @value, @machineNo)
                                               ";
                        using (MySqlCommand cmd = new MySqlCommand(insertDaySql, conn))
                        {
                            cmd.Parameters.AddWithValue("@dateDay", DateTime.Now.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@value", data);
                            cmd.Parameters.AddWithValue("@machineNo", machineid);
                            cmd.ExecuteNonQuery(); // INSERT 실행
                        }
                    }

                    // `elec_month`에 데이터가 없으면 INSERT 실행
                    if (ds.Tables[1].Rows.Count > 0 && Convert.ToInt32(ds.Tables[1].Rows[0]["Count"]) == 0)
                    {
                        string insertMonthSql = @" INSERT INTO elec_month (DATETIME, VALUE, MACHINE_ID)
                                                   VALUES (@dateMonth, @value, @machineNo)
                                                ";
                        using (MySqlCommand cmd = new MySqlCommand(insertMonthSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@dateMonth", DateTime.Now.ToString("yyyy-MM"));
                            cmd.Parameters.AddWithValue("@value", data);
                            cmd.Parameters.AddWithValue("@machineNo", machineid);
                            cmd.ExecuteNonQuery(); // INSERT 실행
                        }
                    }
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cts0.Cancel();
            cts1.Cancel();
            cts2.Cancel();
            cts3.Cancel();
            cts4.Cancel();
            cts5.Cancel();
            elecToken0.Cancel();
            elecToken1.Cancel();
            elecToken2.Cancel();
            elecToken3.Cancel();
            elecToken4.Cancel();
            elecToken5.Cancel();


        }
    }

}