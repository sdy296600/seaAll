﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Media.Media3D;
using System.Data.SqlTypes;

namespace CalculateForSea
{
    public partial class Form1 : Form
    {
        #region 모델
        public class DataModel
        {
            public double NowShotKW { get; set; } = 0; // 현재 누적전력량
            public double Consumption_K { get; set; } = 0; // 누적 ESG (kWh)
            public double Consumption_M { get; set; } = 0; // 누적 ESG (MWh)
            public double ConsumptionRETI { get; set; } = 0; // 누적 RETI
            public double NowRETI { get; set; } = 0; // 현재 RETI
            public double NowESG { get; set; } = 0; // 현재 ESG
            public double Totalcnt { get; set; } = 0; // 현재 총 생산량 (사타수 + 배출수 + 양품수)
            public double PORD_CNT { get; set; } = 0; // 제품 생산수 ( 배출수 + 양품수)
            public double Active_Power { get; set; } = 0; // 유효전력
            public double ReActive_Power { get; set; } = 0; // 무효전력
            public double NowMotorHour { get; set; } = 0; // 현재 구동시간 - 모터구동 시작했을 때 부터의 시간입니다. (모터 가동 정지 시 초기화 됨)
            public double MotorLIFEDay { get; set; } = 0; // 총 모터 구동 일수
            public double MotorLIFEHour { get; set; } = 0; //총 모터 구동 시간 두개 데이터를 합쳐서 실제 사용한 구동시간을 산출합니다
        }

        public class DataModel2
        {
            public double ESG_K { get; set; } = 0; // 누적 ESG (kWh)
            public double ESG_M { get; set; } = 0; // 누적 ESG (MWh)

            public double TRIMING_SHOT { get; set; } = 0; // 누적 ESG (MWh)
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
        GridModel list_13_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "0", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        GridModel list_21_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "0", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        GridModel list_22_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "0", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        GridModel list_23_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "0", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        GridModel list_24_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "0", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        GridModel list_25_DCM = new GridModel() { 트리거 = "0", 탱크진공 = "0", V1 = "0", V2 = "0", V3 = "0", V4 = "0", 형체중자입시간 = "0", 형체력MN = "0", 형체력 = "0", 형개중자후퇴시간 = "0", 오염도B = "0", 가속위치 = "0", 감속위치 = "0", 메탈압력 = "0", 비스켓두께 = "0", 사이클타임 = "0", Date = "0", 사출전진시간 = "0", 설비No = "0", 스프레이시간 = "0", 승압시간 = "0", 압출시간 = "0", 금형내부 = "0", 제품냉각시간 = "0", 주탕시간 = "0", 오염도A = "0", 취출시간 = "0" };
        List<string> IDS = new List<string>();

        #endregion
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            try
            {
                WriteLog("Program Start");
                _mqttClient = new DPSMqttClient("10.10.10.216", 1883, false, null, null, 0);

                _mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                _mqttClient.Subscribe(new string[] { "DPS/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                _mqttClient.Connect(Guid.NewGuid().ToString() + "_Message_Process");
                models2.AddRange(new[] { model_13, model_21, model_22, model_23, model_24, model_25 });
                models.AddRange(new[] { model13, model21, model22, model23, model24, model25 });
                gridModels.AddRange(new[] { list_13, list_21, list_22, list_23, list_24, list_25 });
                gridModels_DCM.AddRange(new[] { list_13_DCM, list_21_DCM, list_22_DCM, list_23_DCM, list_24_DCM, list_25_DCM });
                _tmr = new System.Threading.Timer(new TimerCallback(DataTimerCallback), null, 0, 3000);//3000
                _tmrFOrGrid = new System.Threading.Timer(new TimerCallback(GridTimerCallback), null, 0, 15000);//15000
                _tmrSerData = new System.Threading.Timer(new TimerCallback(SerDataTimerCallback), null, 0, 3000);//3000
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
                {
                    _mqttClient.Publish($"/event/c/data_collection_digit/{topic}", Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                }

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        private void ProcessData(string topic, byte[] message)
        {
            if (topic.Contains("DCM_"))
            {
                GET_DCM(topic, message);
                return;
            }
            if (topic.Contains("LS"))
            {
                GET_LS(topic, message);
                return;
            }
            if (topic.Contains("AC"))
            {
                GET_AC(topic, message);
                return;
            }
            if (topic.Contains("Furnace"))
            {
                DataModel2 model2;
                Int32.TryParse(topic.Split('_')[1], out int index2);
                index2 = index2 - 150 > 0 ? index2 - 150 : 0;
                model2 = models2[index2];

                if (topic.Contains("P_Active_Khours"))
                {
                    model2.ESG_K = double.Parse(Encoding.UTF8.GetString(message));
                }
                else if (topic.Contains("P_Active_Mhours"))
                {
                    //model2.ESG_M = double.Parse(Encoding.UTF8.GetString(message)) * 1000;
                    model2.ESG_M = double.Parse(Encoding.UTF8.GetString(message));
                }
                return;
            }
            if (topic.Contains("RTU_13") || topic.Contains("Casting_"))
            {
                Get_MQTT(topic, message);
                return;
            }
        }
        private void GET_LS(string topic, byte[] message)
        {
            int index2 = 0;
            switch (topic.Split('_')[1])
            {
                case "13":
                    index2 = 0;
                    break;
                case "21":
                    index2 = 1;
                    break;
                case "22":
                    index2 = 2;
                    break;
                case "23":
                    index2 = 3;
                    break;
                case "24":
                    index2 = 4;
                    break;
                case "25":
                    index2 = 5;
                    break;
                default:
                    return;
            }

            if (topic.Contains("MX"))
            {
                gridModels_DCM[index2].트리거 = Encoding.UTF8.GetString(message);
            }
            else if (topic.Contains("DW"))
            {
                if (gridModels_DCM[index2].트리거 == "1")
                {

                    if (topic.Contains("DW816") || topic.Contains("DW186"))
                    {
                        gridModels_DCM[index2].금형내부 = Encoding.UTF8.GetString(message);
                        dm_alram_status_update(gridModels_DCM[index2].금형내부, topic.Split('/')[1]);

                    }
                    if (topic.Contains("DW817") || topic.Contains("DW187"))
                    {
                        gridModels_DCM[index2].오염도A = Encoding.UTF8.GetString(message);
                        dm_alram_status_update(gridModels_DCM[index2].오염도A, topic.Split('/')[1]);

                    }
                    if (topic.Contains("DW818") || topic.Contains("DW188"))
                    {
                        gridModels_DCM[index2].오염도B = Encoding.UTF8.GetString(message);
                        dm_alram_status_update(gridModels_DCM[index2].오염도B, topic.Split('/')[1]);

                    }
                    if (topic.Contains("DW819") || topic.Contains("DW189"))
                    {
                        gridModels_DCM[index2].탱크진공 = Encoding.UTF8.GetString(message);
                        dm_alram_status_update(gridModels_DCM[index2].탱크진공, topic.Split('/')[1]);

                    }
                }
                else
                {
                    //if (topic.Contains("DW816") || topic.Contains("DW186"))
                    //{
                    //    gridModels_DCM[index2].금형내부 = "0";
                    //    dm_alram_status_update(gridModels_DCM[index2].금형내부, topic.Split('/')[1]);

                    //}
                    //if (topic.Contains("DW817") || topic.Contains("DW187"))
                    //{
                    //    gridModels_DCM[index2].오염도A = "0";
                    //    dm_alram_status_update(gridModels_DCM[index2].오염도A, topic.Split('/')[1]);

                    //}
                    //if (topic.Contains("DW818") || topic.Contains("DW188"))
                    //{
                    //    gridModels_DCM[index2].오염도B = "0";
                    //    dm_alram_status_update(gridModels_DCM[index2].오염도B, topic.Split('/')[1]);

                    //}
                    //if (topic.Contains("DW819") || topic.Contains("DW189"))
                    //{
                    //    gridModels_DCM[index2].탱크진공 = "0";
                    //    dm_alram_status_update(gridModels_DCM[index2].탱크진공, topic.Split('/')[1]);

                    //}
                }
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
        }
        private void dm_alram_status_update(object data ,string TAG) 
        {
            try
            {
                string mysqlString = $"UPDATE dm_alarm_status set collection_value = {data.ToString()} where resource_code = '{TAG}'";
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

            } catch (Exception e)
            {
                WriteLog($"Error: {e.Message}");
            }
            
        }

        private void Get_MQTT(string topic, byte[] message)
        {
            DataModel model;
            Int32.TryParse(topic.Split('_')[1], out int index);
            index = index - 160 > 0 ? index - 160 : 0;
            model = models[index];
            if (topic.Contains("P_Active_Khours"))
            {
                model.Consumption_K = double.Parse(Encoding.UTF8.GetString(message));
            }
            else if (topic.Contains("P_Active_Mhours"))
            {
                //model.Consumption_M = double.Parse(Encoding.UTF8.GetString(message)) * 1000;
                model.Consumption_M = double.Parse(Encoding.UTF8.GetString(message));
            }
            else if (topic.Contains("Load_Total_Power_Consumption"))
            {
                //model.ConsumptionRETI = double.Parse(Encoding.UTF8.GetString(message)) / 1000;
                model.ConsumptionRETI = double.Parse(Encoding.UTF8.GetString(message));
            }
            else if (topic.Contains("P_Active")) //유효전력
            {
                model.Active_Power = double.Parse(Encoding.UTF8.GetString(message)); //Unit값 곱하면 현재 측정값
            }
            else if (topic.Contains("P_ReActive")) //무효전력
            {
                model.ReActive_Power = double.Parse(Encoding.UTF8.GetString(message)); //Unit값 곱하면 현재 측정값
            }
            //else if (topic.Contains("P_Active") && !topic.Contains("Ruled")) //유효전력
            //{
            //    model.Active_Power = double.Parse(Encoding.UTF8.GetString(message)) * 0.01; //Unit값 곱하면 현재 측정값
            //}
            //else if (topic.Contains("P_ReActive") && !topic.Contains("Ruled")) //무효전력
            //{
            //    model.ReActive_Power = double.Parse(Encoding.UTF8.GetString(message)) * 0.01; //Unit값 곱하면 현재 측정값
            //}
            else if (topic.Contains("Current_Motor_Hour")) // 현재 구동시간
            {
                model.NowMotorHour = double.Parse(Encoding.UTF8.GetString(message));
            }
            else if (topic.Contains("Motor_LIFE_Day")) // 구동 일수
            {
                model.MotorLIFEDay = double.Parse(Encoding.UTF8.GetString(message));
            }
            else if (topic.Contains("Motor_LIFE_Hour")) // 구동 시간
            {
                model.MotorLIFEHour = double.Parse(Encoding.UTF8.GetString(message));

            }
            else if (topic.Contains("RTU_13_01_Load_THD_Phase_Voltage") || topic.Contains("RTU_13_01_Load_THD_Phase_Current"))
            {
                _mqttClient.Publish($"/event/c/data_collection_digit/{topic.Split('/')[1]}", Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(message)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
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
        private void CalculateAndPublishPowerConsumption(DataModel model, int machineId)
        {

            //누적전력량 계산   *단위 -  0.01KW / 0.01KVAR
            double P = model.Active_Power;
            double Q = model.ReActive_Power;


            // 피상전력(S)을 계산합니다.
            double S = Math.Sqrt(Math.Pow(P, 2) + Math.Pow(Q, 2));

            // 역률(PF)을 계산합니다.
            double powerFactor = P / S;

            // 역률(PF)을 계산합니다.
            double MH = model.NowMotorHour;

            // 적산 유효전력을 계산합니다.
            double Cumulative_Power = P * MH;

            WriteLog($"{model.NowShotKW}");

            // 사용량 계산
            double monthConversion = model.Consumption_K;
            var dailyPower = monthConversion / DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            double unitPower = model.Totalcnt != 0 ? model.NowShotKW / model.Totalcnt : 0;
            // 전기요금 계산 (한국전력기준 24년 산업용 전기 단가 153.7원)
            double electricityRate = 153.7; // KRW per kWh 
            double dailyAmount = dailyPower * electricityRate;
            double monthlyAmount = monthConversion * electricityRate;
            double unitAmount = unitPower * electricityRate;
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
                // 피상전력 Casting_162_APPARENT_POWER
                // 역률  Casting_162_P_Factor_2
                // 누적 사용량  Casting_162_Cumulative_Power
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Cumulative_Power", Encoding.UTF8.GetBytes(Cumulative_Power.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_P_Factor_2", Encoding.UTF8.GetBytes(powerFactor.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_APPARENT_POWER", Encoding.UTF8.GetBytes(S.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);


                // MQTT로 전송
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Month_Power_Amount", Encoding.UTF8.GetBytes(monthlyAmount.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Load_Power_Consumption_Today_Conversion", Encoding.UTF8.GetBytes(monthConversion.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Daily_Power_Amount", Encoding.UTF8.GetBytes(dailyAmount.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Daily_Power_Consumption", Encoding.UTF8.GetBytes(dailyPower.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Unit_Power_Amount", Encoding.UTF8.GetBytes(unitAmount.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                _mqttClient.Publish($"/event/c/data_collection_digit/Casting_{160 + machineId}_Unit_Power_Consumption", Encoding.UTF8.GetBytes(unitPower.ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
                WriteLog($"{machineId + 20}호기 데이터 MQTT 전송 완료");
            }

        }

        private void SerDataTimerCallback(object state)
        {
            try
            {
                DataSet ds = new DataSet();

                // MySQL 연결 및 데이터 조회
                using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SelectTimeSeriesData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds); // 시계열 데이터를 데이터셋에 채움
                        }
                    }
                }

                // 데이터가 없는 경우 처리 종료
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    WriteLog("No data found");
                    WaitOrder();
                    return;
                }

                // 데이터 처리 루프
                foreach (DataRow data in ds.Tables[0].Rows)
                {
                    string id = data["ID"].ToString();
                    string category = data["Category"].ToString();
                    string value = data["Value"].ToString();
                    string timestamp = DateTime.Parse(data["Timestamp"].ToString()).ToString("yyyy-MM-dd HH:mm:ss.fff");

                    IDS.RemoveAll(item => (Convert.ToInt32(id) - 2001) - Convert.ToInt32(item) <= 0);
                    // 이미 처리된 ID는 생략
                    if (IDS.Contains(id)) continue;

                    // 기계 ID 확인
                    string machineId = "";
                    foreach (DataRow machineData in ds.Tables[1].Rows)
                    {
                        string currentMachineId = machineData["MACHINE_NO"].ToString();
                        if (category.Split('_')[1].IndexOf(currentMachineId.Substring(currentMachineId.Length - 2, 2)) > -1 &&
                            DateTime.Parse(timestamp) >= DateTime.Parse(machineData["START_TIME"].ToString()))
                        {
                            machineId = currentMachineId;
                            break; // 기계 ID가 확인되면 루프 탈출
                        }
                    }

                    // 유효한 기계 ID가 없으면 다음 데이터로 넘어감
                    if (string.IsNullOrEmpty(machineId)) continue;

                    // MSSQL 데이터 저장
                    try
                    {
                        using (SqlConnection sqlconn = new SqlConnection("Server=10.10.10.180;Database=HS_MES;User Id=hansol_mes;Password=Hansol123!@#;"))
                        {
                            sqlconn.Open();
                            using (SqlCommand sqlcmd = new SqlCommand("PUT_TIMESERIESDATA", sqlconn))
                            {
                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                sqlcmd.Parameters.AddWithValue("@A_OUT_CODE", machineId);
                                sqlcmd.Parameters.AddWithValue("@A_CATEGORY", category);
                                sqlcmd.Parameters.AddWithValue("@A_TIMESTAMP", timestamp);
                                sqlcmd.Parameters.AddWithValue("@A_VALUE", value);
                                sqlcmd.ExecuteNonQuery();
                            }
                        }
                        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                        {
                            conn.Open();
                            using (MySqlCommand cmd = new MySqlCommand("DELETE FROM timeseriesdata WHERE id = @id", conn))
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.AddWithValue("@id", id); // 파라미터화된 쿼리 사용
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // 처리된 ID 목록에 추가
                        IDS.Add(id);
                    }
                    catch (Exception ex)
                    {
                        WriteLog($"Error storing data: {ex.Message}");
                    }
                }

                WriteLog("Data processing completed");
            }
            catch (Exception ex)
            {
                WriteLog($"Error: {ex.Message}");
            }
        }

        private async void DataTimerCallback(object state)
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet dsTSD = new DataSet(); //SelectTimeSeriesData
                MySqlConnection conn = new MySqlConnection(ConnectionString);
                using (conn)
                {
                    conn.Open();
                    // work_performance조회 ( start_time = end_time 인것)
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "calculateForSEA_R";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(ds);

                }
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    WriteLog("Data Received");

                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        try
                        {
                            
                            Get_DCM(i, gridModels_DCM[i]); //값 보내주기
                            int cavity = 1;
                            string cavitySql = $"SELECT CAVITY FROM SEA_MFG.DBO.MD_MST WHERE CODE_MD =  (select CODE_MD from [sea_mfg].dbo.demand_mstr_ext WHERE LOT='{ds.Tables[i].Rows[0]["LOT_NO"].ToString()}' AND order_no ='{ds.Tables[i].Rows[0]["RESOURCE_NO"].ToString()}')";

                            using (SqlConnection sqlconn = new SqlConnection("Server=10.10.10.180; Database=HS_MES; User Id=hansol_mes; Password=Hansol123!@#;"))
                            {
                                sqlconn.Open();
                                using (SqlCommand sqlcmd = new SqlCommand(cavitySql, sqlconn))
                                {
                                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            cavity = Convert.ToInt32(reader["CAVITY"].ToString());
                                        }
                                    }
                                }
                            }

                            string WORK_OKCNT = string.IsNullOrWhiteSpace(ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString()) ? "0" : ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString();
                            string WORK_WARMUPCNT = string.IsNullOrWhiteSpace(ds.Tables[i].Rows[0]["WORK_WARMUPCNT"].ToString()) ? "0" : ds.Tables[i].Rows[0]["WORK_WARMUPCNT"].ToString();
                            string WORK_ERRCOUNT = string.IsNullOrWhiteSpace(ds.Tables[i].Rows[0]["WORK_ERRCOUNT"].ToString()) ? "0" : ds.Tables[i].Rows[0]["WORK_ERRCOUNT"].ToString();
                            string WORK_POWER = string.IsNullOrWhiteSpace(ds.Tables[i].Rows[0]["WORK_POWER"].ToString()) ? "0" : ds.Tables[i].Rows[0]["WORK_POWER"].ToString();

                            int nowtotalcnt = (Convert.ToInt32(WORK_OKCNT)/cavity)
                                + Convert.ToInt32(WORK_WARMUPCNT)
                                + (Convert.ToInt32(WORK_ERRCOUNT)/cavity);

                            int nowPordCnt = Convert.ToInt32(WORK_OKCNT)
                                + Convert.ToInt32(WORK_ERRCOUNT);

                            if (models[i].Totalcnt < nowtotalcnt)
                            {
                                if (models[i].ConsumptionRETI + models[i].Consumption_K + models[i].Consumption_M - models[i].NowShotKW > 0)
                                {
                                    using (SqlConnection sqlconn = new SqlConnection("Server = 10.10.10.180; Database = HS_MES; User Id = hansol_mes; Password = Hansol123!@#;"))
                                    {
                                        sqlconn.Open();
                                        using (SqlCommand sqlcmd = new SqlCommand())
                                        {

                                            sqlcmd.Connection = sqlconn;
                                            sqlcmd.CommandType = CommandType.StoredProcedure;
                                            sqlcmd.CommandText = "USP_ELECTRIC_USE_DPS_A20";
                                            sqlcmd.Parameters.AddWithValue("@MACHINE_NO", gridModels[i][0].설비No);
                                            sqlcmd.Parameters.AddWithValue("@ORDER_NO", $"{ds.Tables[i].Rows[0]["ORDER_NO"]}");
                                            sqlcmd.Parameters.AddWithValue("@RESOURCE_NO", $"{ds.Tables[i].Rows[0]["RESOURCE_NO"]}");
                                            sqlcmd.Parameters.AddWithValue("@LOT_NO", $"{ds.Tables[i].Rows[0]["LOT_NO"]}");
                                            sqlcmd.Parameters.AddWithValue("@ELECTRICAL_ENERGY", (models[i].Consumption_K + models[i].Consumption_M + models[i].ConsumptionRETI - models[i].NowShotKW).ToString("F2"));
                                            sqlcmd.Parameters.AddWithValue("@V1", gridModels[i][0].V1);
                                            sqlcmd.Parameters.AddWithValue("@V2", gridModels[i][0].V2);
                                            sqlcmd.Parameters.AddWithValue("@V3", gridModels[i][0].V3);
                                            sqlcmd.Parameters.AddWithValue("@V4", gridModels[i][0].V4);
                                            sqlcmd.Parameters.AddWithValue("@가속위치", gridModels[i][0].가속위치);
                                            sqlcmd.Parameters.AddWithValue("@감속위치", gridModels[i][0].감속위치);
                                            sqlcmd.Parameters.AddWithValue("@메탈압력", gridModels[i][0].메탈압력);
                                            sqlcmd.Parameters.AddWithValue("@승압시간", gridModels[i][0].승압시간);
                                            sqlcmd.Parameters.AddWithValue("@비스켓두께", gridModels[i][0].비스켓두께);
                                            sqlcmd.Parameters.AddWithValue("@형체력", gridModels[i][0].형체력);
                                            sqlcmd.Parameters.AddWithValue("@형체력MN", gridModels[i][0].형체력MN);
                                            sqlcmd.Parameters.AddWithValue("@사이클타임", gridModels[i][0].사이클타임);
                                            sqlcmd.Parameters.AddWithValue("@형체중자입시간", gridModels[i][0].형체중자입시간);
                                            sqlcmd.Parameters.AddWithValue("@주탕시간", gridModels[i][0].주탕시간);
                                            sqlcmd.Parameters.AddWithValue("@사출전진시간", gridModels[i][0].사출전진시간);
                                            sqlcmd.Parameters.AddWithValue("@제품냉각시간", gridModels[i][0].제품냉각시간);
                                            sqlcmd.Parameters.AddWithValue("@형개중자후퇴시간", gridModels[i][0].형개중자후퇴시간);
                                            sqlcmd.Parameters.AddWithValue("@압출시간", gridModels[i][0].압출시간);
                                            sqlcmd.Parameters.AddWithValue("@취출시간", gridModels[i][0].스프레이시간);
                                            sqlcmd.Parameters.AddWithValue("@스프레이시간", gridModels[i][0].스프레이시간);
                                            sqlcmd.Parameters.AddWithValue("@금형내부", gridModels[i][0].금형내부);
                                            sqlcmd.Parameters.AddWithValue("@오염도A", gridModels[i][0].오염도A);
                                            sqlcmd.Parameters.AddWithValue("@오염도B", gridModels[i][0].오염도B);
                                            sqlcmd.Parameters.AddWithValue("@탱크진공", gridModels[i][0].탱크진공);

                                            sqlcmd.ExecuteNonQuery();


                                            WriteLog("SHOT Data Processed");
                                        }
                                     
                                    }
                                }
                                int machine_id;
                                //여기에 
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
                                string mysqlString =
                                                               $"INSERT INTO data_for_grid                                                                      " +
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
                                                               $", vacuum                                                                                       " +
                                                               $")                                                                                              " +
                                                               $"VALUES                                                                                         " +
                                                               $"(                                                                                              " +
                                                               $"now(),                                                                                         " +
                                                               $"'WCI_D{machine_id}',                                                                                     " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6900_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6902_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6904_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6906_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6908'),       " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6910'),       " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6912_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6914'),       " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6916'),       " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6918'),       " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6920_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6936_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6938_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6940_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6942_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6944_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6946_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6948_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6950_Ruled'), " +
                                                               $"(select collection_value from dm_alarm_status where resource_code = 'DCM_{machine_id}_TAG_D6952_Ruled'), " +
                                                               (machine_id == 13 ? "0," : $"(select collection_value from dm_alarm_status where resource_code = 'LS_{machine_id}_DW816'),") +
                                                               (machine_id == 13 ? "0," : $"(select collection_value from dm_alarm_status where resource_code = 'LS_{machine_id}_DW817'),") +
                                                               (machine_id == 13 ? "0," : $"(select collection_value from dm_alarm_status where resource_code = 'LS_{machine_id}_DW818'),") +
                                                               (machine_id == 13 ? "0," : $"(select collection_value from dm_alarm_status where resource_code = 'LS_{machine_id}_DW819')") +
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
                            }

                            models[i].NowShotKW = models[i].Consumption_K + models[i].ConsumptionRETI + models[i].Consumption_M;
                            models[i].Totalcnt = nowtotalcnt;
                            models[i].PORD_CNT = nowPordCnt;

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
                                    sqlcmd.Parameters.AddWithValue("@ELEC_USE", ds.Tables[i].Rows[0]["WORK_POWER"].ToString());
                                    sqlcmd.ExecuteNonQuery();
                                }
                            }
                            WriteLog("Data MSSQL Processed");

                            string work_performanceSql;


                            if (i == 0)
                            {
                                work_performanceSql =
                                $"UPDATE work_performance                                                                                                          " +
                                $"set                                                                                                                              " +
                                $"work_power = IFNULL((SELECT IFNULL((MAX(CAST(VL AS INT)) - MIN(CAST(VL AS INT))) / 1000, 0) AS 생산수량                          " +
                                $"FROM data_collection a                                                                                                           " +
                                $"WHERE CD = 'RTU_13_01_Load_Total_Power_Consumption'                                                               " +
                                $"                AND Tm > (SELECT start_time FROM work_performance WHERE start_time = end_time ORDER BY id DESC limit 1)          " +
                                $"                AND CAST(VL AS INT) > 0)                                                                                         " +
                                $"                +                                                                                                                " +
                                $"                (SELECT IFNULL((MAX(CAST(VL AS INT)) - MIN(CAST(VL AS INT))), 0) AS 생산수량                                     " +
                                $"                FROM data_collection a                                                                                           " +
                                $"                WHERE CD = 'ESG_P_Active_Khours'                                                                    " +
                                $"                AND Tm > (SELECT start_time FROM work_performance WHERE start_time = end_time ORDER BY id DESC limit 1)          " +
                                $"                AND CAST(VL AS INT) > 0)                                                                                         " +
                                $"                        +                                                                                                        " +
                                $"                        (SELECT IFNULL((MAX(CAST(VL AS INT)) - MIN(CAST(VL AS INT))) * 1000, 0) AS 생산수량                      " +
                                $"                FROM data_collection a                                                                                           " +
                                $"                WHERE CD = 'ESG_P_Active_Mhours'                                                                      " +
                                $"                AND Tm > (SELECT start_time FROM work_performance WHERE start_time = end_time ORDER BY id DESC limit 1)          " +
                                $"                AND CAST(VL AS INT) > 0),0)                                                                                     "+
                                $"                        ,                                                                                                        " +
                                $"work_errcount = IFNULL((SELECT IFNULL((MAX(CAST(VL AS INT)) - MIN(CAST(VL AS INT))), 0) AS 생산수량                              " +
                                $"                FROM data_collection a                                                                                           " +
                                $"                WHERE CD = 'DCM_13_TAG_D3705'                                                                     "+
                                $"                AND Tm > (SELECT start_time FROM work_performance WHERE start_time = end_time ORDER BY id DESC limit 1)          " +
                                $"                AND CAST(VL AS INT) > 0),0) * {cavity},                                                                              " +
                                $"work_okcnt = IFNULL((SELECT IFNULL((MAX(VL) - MIN(VL)), 0) AS 생산수량                                                           " +
                                $"                FROM data_collection a                                                                                           " +
                                $"                WHERE CD = 'DCM_13_TAG_D3704'                                                                     " +
                                $"                AND Tm > (SELECT start_time FROM work_performance WHERE start_time = end_time ORDER BY id DESC limit 1)          " +
                                $"                AND CAST(VL AS INT) > 0),0) * {cavity},                                                                              " +
                                $"work_warmupcnt = IFNULL((SELECT IFNULL((MAX(VL) - MIN(VL)), 0) AS 생산수량                                                       " +
                                $"                FROM data_collection a                                                                                           " +
                                $"                WHERE CD = 'DCM_13_TAG_D3706                                                                       " +
                                $"                AND Tm > (SELECT start_time FROM work_performance WHERE start_time = end_time ORDER BY id DESC limit 1)          " +
                                $"                AND CAST(VL AS INT) > 0),0)                                                                                      " +
                                $"where end_time = start_time                                                                                                      " +
                                $"    AND MACHINE_NO = 'WCI_D13'                                                                                                   " +
                                $"ORDER BY ID DESC LIMIT 1;                                                                                                        "
                                ;
                            }
                            else 
                            {
                                work_performanceSql=
                                $"UPDATE work_performance                                                                         "+
                                $"SET work_power = IFNULL((                                                                       "+
                                $"                         SELECT  MAX(CAST(A.VL AS DOUBLE)) - MIN(CAST(A.VL AS DOUBLE))          "+
                                $"                           FROM data_collection       A                                         "+
                                $"                              , work_performance      B                                         "+
                                $"                          WHERE A.Tm > (SELECT START_TIME FROM work_performance                 "+
                                $"                                                                                                "+
                                $"                                WHERE                                                           "+
                                $"                                                                                                "+
                                $"                                          start_time = end_time                                 "+
                                $"                                                                                                "+
                                $"                                AND MACHINE_NO = 'WCI_D{20+i}'                                  "+
                                $"                                                                                                "+
                                $"                                          ORDER BY ID DESC LIMIT 1                              "+
                                $"                                      )                                                         "+
                                $"                            AND B.MACHINE_NO = 'WCI_D{20+i}'                                    "+
                                $"                            AND A.CD = 'Casting_1{60+i}_P_Active_Ruled'                             "+
                                $"                      ),0)                                                                      "+
                                $"                                                                                                "+
                                $", work_okcnt = IFNULL((                                                             " +
                                $"                                                                                                "+
                                $"                        SELECT((SELECT  MAX(CAST(A.VL AS DOUBLE)) - MIN(CAST(A.VL AS DOUBLE))   "+
                                $"                                                                                                "+
                                $"                            FROM data_collection       A                                        "+
                                $"                                , work_performance      B                                       "+
                                $"                                                                                                "+
                                $"                            WHERE A.Tm > (SELECT START_TIME                                     "+
                                $"                                                                                                "+
                                $"                                                            FROM work_performance               "+
                                $"                                                                                                "+
                                $"                                                WHERE start_time = end_time                     "+
                                $"                                                                                                "+
                                $"                                                    AND MACHINE_NO = 'WCI_D{20+i}'              "+
                                $"                                                                                                "+
                                $"                                                        ORDER BY ID DESC LIMIT 1                "+
                                $"                                                        )                                       "+
                                $"                                                                                                "+
                                $"                            AND A.VL > 0                                                        "+
                                $"                                                                                                "+
                                $"                            AND A.CD = 'DCM_{20+i}_TAG_D3704'                                   "+
                                $"                                                                                                "+
                                $"                            AND B.MACHINE_NO = 'WCI_D{20+i}')                                   "+
                                $"                        - (SELECT  IFNULL(MAX(CAST(A.VL AS DOUBLE) ) - MIN(CAST(A.VL AS DOUBLE)),0)       " +
                                $"                                                                                                "+
                                $"                            FROM data_collection       A                                        "+
                                $"                                , work_performance B                                            "+
                                $"                                                                                                "+
                                $"                            WHERE A.Tm > (SELECT START_TIME                                     "+
                                $"                                                            FROM work_performance               "+
                                $"                                                WHERE start_time = end_time                     "+
                                $"                                                                                                "+
                                $"                                                    AND MACHINE_NO = 'WCI_D{20+i}'              "+
                                $"                                                                                                "+
                                $"                                                        ORDER BY ID DESC LIMIT 1                "+
                                $"			  				                        )                                             "+
                                $"                            AND A.VL > 0                                                        "+
                                $"                                                                                                "+
                                $"                            AND A.CD = 'DCM_{20+i}_TAG_D3705'                                   "+
                                $"                                                                                                "+
                                $"                            AND B.MACHINE_NO = 'WCI_D{20+i}')) AS OKCNT                         "+
                                $"                                                                                                "+
                                $"                            FROM DUAL                                                           "+
                                $"                                                                                                "+
                                $"                        ),0)*{cavity}                                                           "+
                                $"                                                                                                "+
                                $", work_errcount = IFNULL((                                                         "+
                                $"                        SELECT  MAX(CAST(A.VL AS DOUBLE)) - MIN(CAST(A.VL AS DOUBLE))           "+
                                $"                            FROM data_collection       A                                        "+
                                $"                                , work_performance      B                                       "+
                                $"                            WHERE A.Tm > (SELECT START_TIME                                     "+
                                $"                                                                                                "+
                                $"                                                            FROM work_performance               "+
                                $"                                                                                                "+
                                $"                                                WHERE start_time = end_time                     "+
                                $"                                                                                                "+
                                $"                                    AND MACHINE_NO = 'WCI_D{20+i}'                              "+
                                $"                                                                                                "+
                                $"                                                ORDER BY ID DESC LIMIT 1                        "+
                                $"                                    )                                                           "+
                                $"                            AND A.VL > 0                                                        "+
                                $"                            AND A.CD = 'DCM_{20+i}_TAG_D3705'                                   "+
                                $"                            AND B.MACHINE_NO = 'WCI_D{20+i}'                                    "+
                                $"                        ),0)*{cavity}                                                           "+
                                $"                                                                                                "+
                                $", work_warmupcnt = IFNULL((                                                       " +
                                $"                        SELECT  MAX(CAST(A.VL AS DOUBLE)) - MIN(CAST(A.VL AS DOUBLE))           "+
                                $"                            FROM data_collection       A                                        "+
                                $"                                , work_performance      B                                       "+
                                $"                            WHERE A.Tm > (SELECT START_TIME FROM work_performance               "+
                                $"                                                                                                "+
                                $"                            WHERE                                                               "+
                                $"                                                                                                "+
                                $"                                        start_time = end_time                                   "+
                                $"                                                                                                "+
                                $"                            AND MACHINE_NO = 'WCI_D{20+i}'                                      "+
                                $"                                                                                                "+
                                $"                                        ORDER BY ID DESC LIMIT 1                                "+
                                $"                                    )                                                           "+
                                $"                            AND B.MACHINE_NO = 'WCI_D{20+i}'                                    "+
                                $"                            AND A.CD = 'DCM_{20+i}_TAG_D3706'                                   "+
                                $"                        ),0)                                                                    "+
                                $"where end_time = start_time                                                                    "+                                 
                                $"    AND MACHINE_NO = 'WCI_D{20+i}'                                                                  "+                                
                                $"ORDER BY ID DESC LIMIT 1;                                                                       "
                                ;
                            }
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
                            SendMQTT_13(ds, i);
                            break;
                        case 1:
                            SendMQTT_21(ds, i);
                            break;
                        case 2:
                            SendMQTT_22(ds, i);
                            break;
                        case 3:
                            SendMQTT_23(ds, i);
                            break;
                        case 4:
                            SendMQTT_24(ds, i);
                            break;
                        case 5:
                            SendMQTT_25(ds, i);
                            break;
                        default:
                            break;
                    }
                    CalculateAndPublishPowerConsumption(models[i], i);
                }
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

        #region SEND_MQTT

        private void SendMQTT_13(DataSet ds, int i)
        {
            try
            {
                if (ds.Tables[i].Rows.Count > 0)
                {
                    //퍼센트 계산 
                    // (양품개수 / 지시수량 )*100
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_13", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["ORDER_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_13", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["RESOURCE_NO"]}_{ds.Tables[i].Rows[0]["LOT_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_13", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_POWER"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_13", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_ERRCOUNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_13", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_WARMUPCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_13", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_13", Encoding.UTF8.GetBytes(models[i].Totalcnt.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_13", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_13", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"]} / {ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_13", Encoding.UTF8.GetBytes($"{(Convert.ToDouble(ds.Tables[i].Rows[0]["WORK_OKCNT"]) / Convert.ToInt32(ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"].ToString().Split('.')[0]) * 10000).ToString("F2")}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_13", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["IS_WORKING"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_13", Encoding.UTF8.GetBytes(DateTime.Now.Subtract(Convert.ToDateTime(ds.Tables[i].Rows[0]["START_TIME"])).ToString(@"dd\.hh\:mm\:ss")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_13", Encoding.UTF8.GetBytes((models[i].NowESG + models[i].NowRETI + (models2[i].ESG_K + models2[i].ESG_M)).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_13", Encoding.UTF8.GetBytes(models[i].PORD_CNT.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                else
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_13", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_13", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_13", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_13", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_13", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_13", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_13", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_13", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_13", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_13", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_13", Encoding.UTF8.GetBytes("비가동"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_13", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_13", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_13", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                WriteLog("13호기 MQTT Send");
            }
            catch (Exception)
            {
                WriteLog("13호기 값 없음");
            }
        }

        private void SendMQTT_21(DataSet ds, int i)
        {
            try
            {
                if (ds.Tables[i].Rows.Count > 0)
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_21", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["ORDER_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_21", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["RESOURCE_NO"]}_{ds.Tables[i].Rows[0]["LOT_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_21", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_POWER"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_21", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_ERRCOUNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_21", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_WARMUPCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_21", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_21", Encoding.UTF8.GetBytes(models[i].Totalcnt.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_21", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_21", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"]} / {ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_21", Encoding.UTF8.GetBytes($"{(Convert.ToDouble(ds.Tables[i].Rows[0]["WORK_OKCNT"]) / Convert.ToInt32(ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"].ToString().Split('.')[0]) * 10000).ToString("F2")}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_21", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["IS_WORKING"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_21", Encoding.UTF8.GetBytes(DateTime.Now.Subtract(Convert.ToDateTime(ds.Tables[i].Rows[0]["START_TIME"])).ToString(@"dd\.hh\:mm\:ss")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_21", Encoding.UTF8.GetBytes((models[i].NowESG + models[i].NowRETI + (models2[i].ESG_K + models2[i].ESG_M)).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_21", Encoding.UTF8.GetBytes(models[i].PORD_CNT.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                else
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_21", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_21", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_21", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_21", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_21", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_21", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_21", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_21", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_21", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_21", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_21", Encoding.UTF8.GetBytes("비가동"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_21", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_21", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_21", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                WriteLog("21호기 MQTT Send");
            }
            catch (Exception)
            {
                WriteLog("21호기 값 없음");
            }
        }

        private void SendMQTT_22(DataSet ds, int i)
        {
            try
            {
                if (ds.Tables[i].Rows.Count > 0)
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_22", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["ORDER_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_22", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["RESOURCE_NO"]}_{ds.Tables[i].Rows[0]["LOT_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_22", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_POWER"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_22", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_ERRCOUNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_22", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_WARMUPCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_22", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_22", Encoding.UTF8.GetBytes(models[i].Totalcnt.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_22", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_22", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"]} / {ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_22", Encoding.UTF8.GetBytes($"{(Convert.ToDouble(ds.Tables[i].Rows[0]["WORK_OKCNT"]) / Convert.ToInt32(ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"].ToString().Split('.')[0]) * 10000).ToString("F2")}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_22", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["IS_WORKING"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_22", Encoding.UTF8.GetBytes(DateTime.Now.Subtract(Convert.ToDateTime(ds.Tables[i].Rows[0]["START_TIME"])).ToString(@"dd\.hh\:mm\:ss")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_22", Encoding.UTF8.GetBytes((models[i].NowESG + models[i].NowRETI + (models2[i].ESG_K + models2[i].ESG_M)).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_22", Encoding.UTF8.GetBytes(models[i].PORD_CNT.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                else
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_22", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_22", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_22", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_22", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_22", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_22", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_22", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_22", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_22", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_22", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_22", Encoding.UTF8.GetBytes("비가동"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_22", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_22", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_22", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                WriteLog("22호기 MQTT Send");
            }
            catch (Exception)
            {
                WriteLog("22호기 값 없음");
            }
        }

        private void SendMQTT_23(DataSet ds, int i)
        {
            try
            {
                if (ds.Tables[i].Rows.Count > 0)
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_23", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["ORDER_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_23", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["RESOURCE_NO"]}_{ds.Tables[i].Rows[0]["LOT_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_23", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_POWER"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_23", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_ERRCOUNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_23", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_WARMUPCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_23", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_23", Encoding.UTF8.GetBytes(models[i].Totalcnt.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_23", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_23", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"]} / {ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_23", Encoding.UTF8.GetBytes($"{(Convert.ToDouble(ds.Tables[i].Rows[0]["WORK_OKCNT"]) / Convert.ToInt32(ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"].ToString().Split('.')[0]) * 10000).ToString("F2")}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_23", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["IS_WORKING"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_23", Encoding.UTF8.GetBytes(DateTime.Now.Subtract(Convert.ToDateTime(ds.Tables[i].Rows[0]["START_TIME"])).ToString(@"dd\.hh\:mm\:ss")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_23", Encoding.UTF8.GetBytes((models[i].NowESG + models[i].NowRETI + (models2[i].ESG_K + models2[i].ESG_M)).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_23", Encoding.UTF8.GetBytes(models[i].PORD_CNT.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                else
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_23", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_23", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_23", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_23", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_23", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_23", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_23", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_23", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_23", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_23", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_23", Encoding.UTF8.GetBytes("비가동"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_23", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_23", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_23", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                WriteLog("23호기 MQTT Send");
            }
            catch (Exception)
            {
                WriteLog("23호기 값 없음");
            }
        }

        private void SendMQTT_24(DataSet ds, int i)
        {
            try
            {
                if (ds.Tables[i].Rows.Count > 0)
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_24", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["ORDER_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_24", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["RESOURCE_NO"]}_{ds.Tables[i].Rows[0]["LOT_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_24", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_POWER"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_24", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_ERRCOUNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_24", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_WARMUPCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_24", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_24", Encoding.UTF8.GetBytes(models[i].Totalcnt.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_24", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_24", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"]} / {ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_24", Encoding.UTF8.GetBytes($"{(Convert.ToDouble(ds.Tables[i].Rows[0]["WORK_OKCNT"]) / Convert.ToInt32(ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"].ToString().Split('.')[0]) * 10000).ToString("F2")}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_24", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["IS_WORKING"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_24", Encoding.UTF8.GetBytes(DateTime.Now.Subtract(Convert.ToDateTime(ds.Tables[i].Rows[0]["START_TIME"])).ToString(@"dd\.hh\:mm\:ss")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_24", Encoding.UTF8.GetBytes((models[i].NowESG + models[i].NowRETI + (models2[i].ESG_K + models2[i].ESG_M)).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_24", Encoding.UTF8.GetBytes(models[i].PORD_CNT.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                else
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_24", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_24", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_24", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_24", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_24", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_24", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_24", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_24", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_24", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_24", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_24", Encoding.UTF8.GetBytes("비가동"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_24", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_24", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_24", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                
                }
                WriteLog("24호기 MQTT Send");
            }
            catch (Exception)
            {
                WriteLog("24호기 값 없음");
            }
        }

        private void SendMQTT_25(DataSet ds, int i)
        {
            try
            {
                if (ds.Tables[i].Rows.Count > 0)
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_25", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["ORDER_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_25", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["RESOURCE_NO"]}_{ds.Tables[i].Rows[0]["LOT_NO"]}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_25", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_POWER"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_25", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_ERRCOUNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_25", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_WARMUPCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_25", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["WORK_OKCNT"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_25", Encoding.UTF8.GetBytes(models[i].Totalcnt.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_25", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_25", Encoding.UTF8.GetBytes($"{ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"]} / {ds.Tables[i].Rows[0]["WORK_OKCNT"]}".ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_25", Encoding.UTF8.GetBytes($"{(Convert.ToDouble(ds.Tables[i].Rows[0]["WORK_OKCNT"]) / Convert.ToInt32(ds.Tables[i].Rows[0]["PLAN_PERFORMANCE"].ToString().Split('.')[0]) * 10000).ToString("F2")}"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_25", Encoding.UTF8.GetBytes(ds.Tables[i].Rows[0]["IS_WORKING"].ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_25", Encoding.UTF8.GetBytes(DateTime.Now.Subtract(Convert.ToDateTime(ds.Tables[i].Rows[0]["START_TIME"])).ToString(@"dd\.hh\:mm\:ss")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_25", Encoding.UTF8.GetBytes((models[i].NowESG + models[i].NowRETI + (models2[i].ESG_K + models2[i].ESG_M)).ToString("F2")), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_25", Encoding.UTF8.GetBytes(models[i].PORD_CNT.ToString()), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                else
                {
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/ORDER_NO_25", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PRODUCT_NAME_25", Encoding.UTF8.GetBytes($"-"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_POWER_25", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_ERRCOUNT_25", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_WARMUPCNT_25", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/WORK_OKCNT_25", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/TOTAL_CNT_25", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_PERFORMANCE_25", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PLAN_PERFORMANCE_25", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PER_PERFORMANCE_25", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/IS_WORKING_25", Encoding.UTF8.GetBytes("비가동"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/CYCLE_TIME_25", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/NOW_KW_25", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                    Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/PORD_CNT_25", Encoding.UTF8.GetBytes("0"), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
                }
                WriteLog("25호기 MQTT Send");
            }
            catch (Exception)
            {
                WriteLog("25호기 값 없음");
            }
        }

        #endregion

        private async void GridTimerCallback(object state)
        {
            try
            {
                DataSet ds = new DataSet();
                MySqlConnection conn = new MySqlConnection(ConnectionString);
                using (conn)
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "SelectGridHistory";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(ds);
                }

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    gridModels[i] = new List<GridModel>();
                    if (ds.Tables[i].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                        {
                            gridModels[i].Add(new GridModel()
                            {
                                Date = ds.Tables[i].Rows[j]["date"].ToString(),
                                설비No = ds.Tables[i].Rows[j]["machine_no"].ToString(),
                                V1 = ds.Tables[i].Rows[j]["V1"].ToString(),
                                V2 = ds.Tables[i].Rows[j]["V2"].ToString(),
                                V3 = ds.Tables[i].Rows[j]["V3"].ToString(),
                                V4 = ds.Tables[i].Rows[j]["V4"].ToString(),
                                가속위치 = ds.Tables[i].Rows[j]["acceleration_pos"].ToString(),
                                감속위치 = ds.Tables[i].Rows[j]["deceleration_pos"].ToString(),
                                메탈압력 = ds.Tables[i].Rows[j]["metal_pressure"].ToString(),
                                승압시간 = ds.Tables[i].Rows[j]["swap_time"].ToString(),
                                비스켓두께 = ds.Tables[i].Rows[j]["biskit_thickness"].ToString(),
                                형체력 = ds.Tables[i].Rows[j]["physical_strength_per"].ToString(),
                                형체력MN = ds.Tables[i].Rows[j]["physical_strength_mn"].ToString(),
                                사이클타임 = ds.Tables[i].Rows[j]["cycle_time"].ToString(),
                                형체중자입시간 = ds.Tables[i].Rows[j]["type_weight_enrty_time"].ToString(),
                                주탕시간 = ds.Tables[i].Rows[j]["bath_time"].ToString(),
                                사출전진시간 = ds.Tables[i].Rows[j]["forward_time"].ToString(),
                                제품냉각시간 = ds.Tables[i].Rows[j]["freezing_time"].ToString(),
                                형개중자후퇴시간 = ds.Tables[i].Rows[j]["type_weight_back_time"].ToString(),
                                압출시간 = ds.Tables[i].Rows[j]["extrusion_time"].ToString(),
                                취출시간 = ds.Tables[i].Rows[j]["extraction_time"].ToString(),
                                스프레이시간 = ds.Tables[i].Rows[j]["spray_time"].ToString(),
                                금형내부 = ds.Tables[i].Rows[j]["cavity_core"].ToString(),
                                오염도A = ds.Tables[i].Rows[j]["A_Pollution_degree"].ToString(),
                                오염도B = ds.Tables[i].Rows[j]["B_Pollution_degree"].ToString(),
                                탱크진공 = ds.Tables[i].Rows[j]["vacuum"].ToString(),
                            });
                        }
                    }
                }
                for (int i = 0; i < gridModels.Count; i++)
                {
                    if (gridModels[i].Count > 0)
                    {
                        try
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
                        catch (Exception ex)
                        {
                            WriteLog(ex.Message);
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
            lb_logbox.Invoke(new Action(() =>
            {
                lb_logbox.Items.Insert(0, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : {log}");

                if (lb_logbox.Items.Count > 100)
                {
                    lb_logbox.Items.RemoveAt(lb_logbox.Items.Count - 1);
                }
            }));
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
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW186", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.금형내부).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW187", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.오염도A).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW188", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.오염도B).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW189", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(gridModel.탱크진공).TrimEnd('"').TrimStart('"')), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            //Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/AC_{machines[i]}_D3706", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(models2[i].TRIMING_SHOT)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
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
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW186", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW187", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW188", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/LS_{machines[i]}_DW189", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));
            //Task.Run(() => _mqttClient.Publish($"/event/c/data_collection_digit/AC_{machines[i]}_D3706", Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(0)), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false));

        }
    }
}