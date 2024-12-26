using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DataMonitoringSystem.Model
{
    public class ElecModel2
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

        /// <summary>
        /// 월간전력사용량
        /// </summary>
        private string? _monthElec;
        public string? MonthElec
        {
            get { return _monthElec; }
            set { _monthElec = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 일간 전력 사용량
        /// </summary>
        private string? _dayElec;
        public string? DayElec
        {
            get { return _dayElec; }
            set { _dayElec = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 단위 전력 사용량
        /// </summary>
        private string? _unitElec;
        public string? UnitElec
        {
            get { return _unitElec; }
            set { _unitElec = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 월간 전기료
        /// </summary>
        private string? _monthAmount;
        public string? MonthAmount
        {
            get { return _monthAmount; }
            set { _monthAmount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 일간 전기료
        /// </summary>
        private string? _dayAmount;
        public string? DayAmount
        {
            get { return _dayAmount; }
            set { _dayAmount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 단위 전기료
        /// </summary>
        private string? _unitAmount;
        public string? UnitAmount
        {
            get { return _unitAmount; }
            set { _unitAmount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 사용 전력
        /// </summary>
        private string? _usingElec;
        public string? UsingElec
        {
            get { return _usingElec; }
            set { _usingElec = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// R 전류
        /// </summary>
        private string? _rElecCurrent;
        public string? RElecCurrent
        {
            get { return _rElecCurrent; }
            set { _rElecCurrent = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// S 전류
        /// </summary>
        private string? _sElecCurrent;
        public string? SElecCurrent
        {
            get { return _sElecCurrent; }
            set { _sElecCurrent = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// T 전류
        /// </summary>
        private string? _tElecCurrent;
        public string? TElecCurrent
        {
            get { return _tElecCurrent; }
            set { _tElecCurrent = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 누적전력량 
        /// </summary>
        private string? _cumulativePower;
        public string? CumulativePower
        {
            get { return _cumulativePower; }
            set {
                _cumulativePower = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 역률
        /// </summary>
        private string? _factor;
        public string? Factor
        {
            get { return _factor; }
            set {
                _factor = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"> 몇 호기 인지 넣을것</param>
        public ElecModel2(int i)
        {
            MqttClient mqttClient = new MqttClient("10.10.10.216", 1883, false, null, null, 0);
            if (i == 13)
            {
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_Load_Power_Consumption_Today_Conversion" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_Daily_Power_Consumption" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_Unit_Power_Consumption" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_Month_Power_Amount" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_Daily_Power_Amount" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_Unit_Power_Amount" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_Load_Active_Power" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_R_Phase_Voltage" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_S_Phase_Voltage" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_T_Phase_Voltage" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_Load_Power_Consumption_Conversion" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/RTU_13_01_Load_Total_Power_Consumption" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            }
            else 
            {
                int castingNo = i + 140;
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_Load_Power_Consumption_Today_Conversion" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_Daily_Power_Consumption" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_Unit_Power_Consumption" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_Month_Power_Amount" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_Daily_Power_Amount" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_Unit_Power_Amount" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_All_Active_Power" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_IL1_Current_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_IL2_Current_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_IL3_Current_Ruled" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_Cumulative_Power" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
                mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/Casting_{castingNo}_P_Factor" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            }
            
            mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived; ;
            mqttClient.Connect(Guid.NewGuid().ToString() + "_Message_Process");

        }


        
        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                string topic = e.Topic.Split('/')[4];
                string message = Encoding.UTF8.GetString(e.Message);

                if (topic.Contains("_Load_Power_Consumption_Today_Conversion"))
                    MonthElec = message;

                if (topic.Contains("_Daily_Power_Consumption"))
                    DayElec = message;

                if (topic.Contains("_Unit_Power_Consumption"))
                    UnitElec = message;

                if (topic.Contains("_Month_Power_Amount"))
                    MonthAmount = message;

                if (topic.Contains("_Daily_Power_Amount"))
                    DayAmount = message;

                if (topic.Contains("_Unit_Power_Amount"))
                    UnitAmount = message;

                if (topic.Contains("_All_Active_Power") || topic.Contains("_Load_Active_Power"))
                    UsingElec = message;

                if (topic.Contains("_R_Phase_Voltage") || topic.Contains("_IL1_Current_Ruled"))
                    RElecCurrent = message;
                
                if (topic.Contains("_S_Phase_Voltage") || topic.Contains("_IL2_Current_Ruled"))
                    SElecCurrent = message;
                
                if (topic.Contains("_T_Phase_Voltage") || topic.Contains("_IL3_Current_Ruled"))
                    TElecCurrent = message;
                
                if (topic.Contains("_Load_Power_Consumption_Conversion") || topic.Contains("_Cumulative_Power"))
                    _cumulativePower = message;

                if (topic.Contains("_P_Factor") || topic.Contains("_Load_Total_Power_Consumption"))
                    Factor = message;
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
