using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DataMonitoringSystem.Model
{
    public class ElecModel
    {

      

        private string? date;
        public string? Date
        {
            get { return date; }
            set { date = value;
                OnPropertyChanged();
            }
        }

        private string? machineNo;
        public string? MachineNo
        {
            get { return machineNo; }
            set { machineNo = value;
                OnPropertyChanged();
            }
        }

        private string? v1;
        public string? V1
        {
            get { return v1; }
            set { v1 = value;
                OnPropertyChanged();
            }
        }

        private string? v2;
        public string? V2
        {
            get { return v2; }
            set { v2 = value;
                OnPropertyChanged();
            }
        }

        private string? v3;
        public string? V3
        {
            get { return v3; }
            set { v3 = value;
                OnPropertyChanged();
            }
        }

        private string? v4;
        public string? V4
        {
            get { return v4; }
            set { v4 = value;
                OnPropertyChanged();
            }
        }

        // 가속위치
        private string? accelerationPos;
        public string? AccelerationPos
        {
            get { return accelerationPos; }
            set { accelerationPos = value;
                OnPropertyChanged();
            }
        }

        // 감속위치
        private string? decelerationPos;
        public string? DecelerationPos
        {
            get { return decelerationPos; }
            set { decelerationPos = value;
                OnPropertyChanged();
            }
        }

        // 메탈압력
        private string? metalPressure;
        public string? MetalPressure
        {
            get { return metalPressure; }
            set { metalPressure = value;
                OnPropertyChanged();
            }
        }

        // 승압시간
        private string? swapTime;
        public string? SwapTime
        {
            get { return swapTime; }
            set { swapTime = value;
                OnPropertyChanged();
            }
        }

        // 비스킷 타임
        private string? biskitThickness;
        public string? BiskitThickness
        {
            get { return biskitThickness; }
            set { biskitThickness = value;
                OnPropertyChanged();
            }
        }

        // 형체력 [%]
        private string? physicalStrengthPer;
        public string? PhysicalStrengthPer
        {
            get { return physicalStrengthPer; }
            set { physicalStrengthPer = value;
                OnPropertyChanged();
            }
        }

        // 형체력 MN
        private string? physicalStrengthMn;
        public string? PhysicalStrengthMn
        {
            get { return physicalStrengthMn; }
            set { physicalStrengthMn = value;
                OnPropertyChanged();
            }
        }

        // 사이클타임
        private string? cycleTime;
        public string? CycleTime
        {
            get { return cycleTime; }
            set { cycleTime = value;
                OnPropertyChanged();
            }
        }

        // 형체중자입시간
        private string? typeWeightEnrtyTime;
        public string? TypeWeightEnrtyTime
        {
            get { return typeWeightEnrtyTime; }
            set { typeWeightEnrtyTime = value;
                OnPropertyChanged();
            }
        }

        // 주탕시간
        private string? bathTime;
        public string? BathTime
        {
            get { return bathTime; }
            set { bathTime = value;
                OnPropertyChanged();
            }
        }

        // 사출전진시간
        private string? forwardTime;
        public string? ForwardTime
        {
            get { return forwardTime; }
            set { forwardTime = value;
                OnPropertyChanged();
            }
        }

        // 제품냉각시간
        private string? freezingTime;
        public string? FreezingTime
        {
            get { return freezingTime; }
            set { freezingTime = value;
                OnPropertyChanged();
            }
        }

        // 형개중자후퇴시간
        private string? typeWeightBackTime;
        public string? TypeWeightBackTime
        {
            get { return typeWeightBackTime; }
            set { typeWeightBackTime = value;
                OnPropertyChanged();
            }
        }

        // 압출시간
        private string? extrusionTime;
        public string? ExtrusionTime
        {
            get { return extrusionTime; }
            set { extrusionTime = value;
                OnPropertyChanged();
            }

        }

        // 취출시간
        private string? extractionTime;
        public string? ExtractionTime
        {
            get { return extractionTime; }
            set { extractionTime = value;
                OnPropertyChanged();
            }
        }

        // 스프레이시간
        private string? sprayTime;
        public string? SprayTime
        {
            get { return sprayTime; }
            set { sprayTime = value;
                OnPropertyChanged();
            }
        }

        // 금형내부
        private string? cavityCore;
        public string? CavityCore
        {
            get { return cavityCore; }
            set { cavityCore = value;
                OnPropertyChanged();
            }
        }

        // 오염도 A
        private string? a_PollutionDegree;
        public string? A_PollutionDegree
        {
            get { return a_PollutionDegree; }
            set { a_PollutionDegree = value;
                OnPropertyChanged();
            }
        }

        // 오염도 B
        private string? b_PollutionDegree;
        public string? B_PollutionDegree
        {
            get { return b_PollutionDegree; }
            set { b_PollutionDegree = value;
                OnPropertyChanged();
            }
        }

        // 탱크진공
        private string? vacuum;
        public string? Vacuum
        {
            get { return vacuum; }
            set { vacuum = value;
                OnPropertyChanged();
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"> 몇 호기 인지 넣을것</param>
        public ElecModel(int i)
        {
            
            MqttClient mqttClient = new MqttClient("10.10.10.216", 1883, false, null, null, 0);
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6900_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6902_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6904_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6906_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6908" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6910" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6912_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6914" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6916" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6918" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6936_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6938_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6940_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6942_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6944_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6948_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6950_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/DCM_{i}_TAG_D6952_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived; ;
            mqttClient.Connect(Guid.NewGuid().ToString() + "_Message_Process");

        }


        
        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {

                string topic = e.Topic.Split('/')[4];
                string message = Encoding.UTF8.GetString(e.Message);

                if (topic.Contains("_TAG_D6900_Ruled"))
                    V1 = message;

                if (topic.Contains("_TAG_D6902_Ruled"))
                    V2 = message;

                if (topic.Contains("_TAG_D6904_Ruled"))
                    V3 = message;

                if (topic.Contains("_TAG_D6906_Ruled"))
                    V4 = message;

                if (topic.Contains("_TAG_D6908"))
                    AccelerationPos = message;

                if (topic.Contains("_TAG_D6910"))
                    DecelerationPos = message;

                if (topic.Contains("_TAG_D6912_Ruled"))
                    MetalPressure = message;

                if (topic.Contains("_TAG_D6914"))
                    SwapTime = message;

                if (topic.Contains("_TAG_D6916"))
                    BiskitThickness = message;

                if (topic.Contains("_TAG_D6918"))
                    PhysicalStrengthPer = message;

                if (topic.Contains("_TAG_D6920_Ruled"))
                    PhysicalStrengthMn = message;

                if (topic.Contains("_TAG_D6936_Ruled"))
                    CycleTime = message;

                if (topic.Contains("_TAG_D6938_Ruled"))
                    TypeWeightEnrtyTime = message;

                if (topic.Contains("_TAG_D6940_Ruled"))
                    BathTime = message;

                if (topic.Contains("_TAG_D6942_Ruled"))
                    ForwardTime = message;

                if (topic.Contains("_TAG_D6944_Ruled"))
                    FreezingTime = message;

                if (topic.Contains("_TAG_D6946_Ruled"))
                    TypeWeightBackTime = message;

                if (topic.Contains("_TAG_D6948_Ruled"))
                    ExtrusionTime = message;

                if (topic.Contains("_TAG_D6950_Ruled"))
                    ExtractionTime = message;

                if (topic.Contains("_TAG_D6952_Ruled"))
                    SprayTime = message;

                //if (topic.Contains("_TAG_D3704"))
                //    getDtOkCnt = Convert.ToDouble(Encoding.UTF8.GetString(message));

                //if (topic.Contains("_TAG_D3705"))
                //    getDtErrCnt = Convert.ToDouble(Encoding.UTF8.GetString(message));

                //if (topic.Contains("_TAG_D3706"))
                //    getDtWarmCnt = Convert.ToDouble(Encoding.UTF8.GetString(message));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


       



    }
}
