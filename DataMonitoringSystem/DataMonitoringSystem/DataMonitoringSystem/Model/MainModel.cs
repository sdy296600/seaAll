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
    public class MainModel : INotifyPropertyChanged
    {
        private int _machineNo;
        public int MachineNo 
        {
            get { return _machineNo; }
            set { _machineNo = value; }
        }
        private string? _itemNo;
        public string? ItemNo
        {
            get { return _itemNo; }
            set { _itemNo = value;
                OnPropertyChanged();
            }
        }

        private string? _isRunning;
        public string? IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value;
                OnPropertyChanged();
            }
        }
        private string? _cycleTime;
        public string? CycleTime
        {
            get { return _cycleTime; }
            set { _cycleTime = value;
                OnPropertyChanged();
            }
        }
        private string? _warmUpCnt;
        public string? WarmUpCnt
        {
            get { return _warmUpCnt; }
            set { _warmUpCnt = value;
                OnPropertyChanged();
            }
        }
        private string? _okCnt;
        public string? OkCnt
        {
            get { return _okCnt; }
            set { _okCnt = value;
                OnPropertyChanged();
            }
        }
        private string? _errCnt;
        public string? ErrCnt
        {
            get { return _errCnt; }
            set { _errCnt = value;
                OnPropertyChanged();
            }
        }
        private string? _allCnt;
        public string? AllCnt
        {
            get { return _allCnt; }
            set { _allCnt = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"> 몇 호기 인지 넣을것</param>
        public MainModel(int i) 
        {
            MachineNo = i;
            MqttClient mqttClient = new MqttClient("10.10.10.216", 1883, false, null, null, 0);
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/PRODUCT_NAME_{MachineNo}" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/IS_WORKING_{MachineNo}" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/CYCLE_TIME_{MachineNo}" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/WORK_OKCNT_{MachineNo}" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/WORK_ERRCOUNT_{MachineNo}" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/WORK_WARMUPCNT_{MachineNo}" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.Subscribe(new string[] { $"/event/c/data_collection_digit/PROD_CNT_{MachineNo}" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived; ;
            mqttClient.Connect(Guid.NewGuid().ToString() + "_Message_Process");
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                string topic = e.Topic.Split('/')[4];
                string message = Encoding.UTF8.GetString(e.Message);

                if (topic.Contains("PRODUCT_NAME_")) 
                {
                    ItemNo = message;
                }
                if (topic.Contains("IS_WORKING_"))
                {
                    IsRunning = message;
                }
                if (topic.Contains("CYCLE_TIME_"))
                {
                    CycleTime = message;
                }
                if (topic.Contains("WORK_OKCNT_"))
                {
                    OkCnt = message;
                }
                if (topic.Contains("WORK_ERRCOUNT_"))
                {
                    ErrCnt = message;
                }
                if (topic.Contains("WORK_WARMUPCNT_"))
                {
                    WarmUpCnt = message;
                }
                if (topic.Contains("PROD_CNT_"))
                {
                    AllCnt = message;
                }
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
