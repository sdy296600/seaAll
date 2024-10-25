using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using uPLibrary.Networking.M2Mqtt;

namespace CalculateForSea
{
    public class DPSMqttClient : MqttClient
    {
        private short _UsedConnMethod = -1;

        private string _UserName;

        private string _Password;

        private bool _WillRetain;

        private ushort _KeepAlivePeriod;

        public DPSMqttClient(string brokerHostName)
            : base(brokerHostName)
        {
        }

        public DPSMqttClient(string brokerHostName, int brokerPort, bool secure, X509Certificate caCert, X509Certificate clientCert, MqttSslProtocols sslProtocol)
            : base(brokerHostName, brokerPort, secure, caCert, clientCert, sslProtocol)
        {
        }

        public DPSMqttClient(string brokerHostName, int brokerPort, bool secure, MqttSslProtocols sslProtocol, RemoteCertificateValidationCallback userCertificateValidationCallback, LocalCertificateSelectionCallback userCertificateSelectionCallback)
            : base(brokerHostName, brokerPort, secure, sslProtocol, userCertificateValidationCallback, userCertificateSelectionCallback)
        {
        }

        public DPSMqttClient(string brokerHostName, int brokerPort, bool secure, X509Certificate caCert, X509Certificate clientCert, MqttSslProtocols sslProtocol, RemoteCertificateValidationCallback userCertificateValidationCallback)
            : base(brokerHostName, brokerPort, secure, caCert, clientCert, sslProtocol, userCertificateValidationCallback)
        {
        }

        public DPSMqttClient(string brokerHostName, int brokerPort, bool secure, X509Certificate caCert, X509Certificate clientCert, MqttSslProtocols sslProtocol, RemoteCertificateValidationCallback userCertificateValidationCallback, LocalCertificateSelectionCallback userCertificateSelectionCallback)
            : base(brokerHostName, brokerPort, secure, caCert, clientCert, sslProtocol, userCertificateValidationCallback, userCertificateSelectionCallback)
        {
        }

        public new byte Connect(string clientId, string username, string password, bool willRetain, byte willQosLevel, bool willFlag, string willTopic, string willMessage, bool cleanSession, ushort keepAlivePeriod)
        {
            base.ConnectionClosed -= MqttConnectionClosed;
            byte result = base.Connect(clientId, username, password, willRetain, willQosLevel, willFlag, willTopic, willMessage, cleanSession, keepAlivePeriod);
            _UserName = username;
            _Password = password;
            _WillRetain = willRetain;
            _KeepAlivePeriod = keepAlivePeriod;
            _UsedConnMethod = 0;
            base.ConnectionClosed += MqttConnectionClosed;
            return result;
        }

        public new byte Connect(string clientId, string username, string password, bool cleanSession, ushort keepAlivePeriod)
        {
            base.ConnectionClosed -= MqttConnectionClosed;
            byte result = base.Connect(clientId, username, password, cleanSession, keepAlivePeriod);
            _UserName = username;
            _Password = password;
            _WillRetain = false;
            _KeepAlivePeriod = keepAlivePeriod;
            _UsedConnMethod = 1;
            base.ConnectionClosed += MqttConnectionClosed;
            return result;
        }

        public new byte Connect(string clientId, string username, string password)
        {
            base.ConnectionClosed -= MqttConnectionClosed;
            byte result = base.Connect(clientId, username, password);
            _UserName = username;
            _Password = password;
            _WillRetain = false;
            _KeepAlivePeriod = 0;
            _UsedConnMethod = 2;
            base.ConnectionClosed += MqttConnectionClosed;
            return result;
        }

        public new byte Connect(string clientId)
        {
            base.ConnectionClosed -= MqttConnectionClosed;
            byte result = base.Connect(clientId);
            _UserName = null;
            _Password = null;
            _WillRetain = false;
            _KeepAlivePeriod = 0;
            _UsedConnMethod = 3;
            base.ConnectionClosed += MqttConnectionClosed;
            return result;
        }

        public byte ConnectRandomId(string prefix, string username, string password, ushort keepAlivePeriod)
        {
            string clientId = (string.IsNullOrEmpty(prefix) ? Guid.NewGuid().ToString() : (prefix + "-" + Guid.NewGuid().ToString()));
            return Connect(clientId, username, password, cleanSession: true, keepAlivePeriod);
        }

        public byte ConnectRandomId(string prefix, string username, string password)
        {
            string clientId = (string.IsNullOrEmpty(prefix) ? Guid.NewGuid().ToString() : (prefix + "-" + Guid.NewGuid().ToString()));
            return Connect(clientId, username, password);
        }

        public byte ConnectRandomId(string prefix)
        {
            string clientId = (string.IsNullOrEmpty(prefix) ? Guid.NewGuid().ToString() : (prefix + "-" + Guid.NewGuid().ToString()));
            return Connect(clientId);
        }

        public new void Disconnect()
        {
            base.ConnectionClosed -= MqttConnectionClosed;
            base.Disconnect();
            _UserName = null;
            _Password = null;
            _WillRetain = false;
            _KeepAlivePeriod = 0;
            _UsedConnMethod = -1;
        }

        private void MqttConnectionClosed(object sender, EventArgs e)
        {
            MqttClient mqttClient = sender as MqttClient;
            bool isConnected = mqttClient.IsConnected;
            while (!isConnected)
            {
                try
                {
                    switch (_UsedConnMethod)
                    {
                        case 0:
                            mqttClient.Connect(base.ClientId, _UserName, _Password, _WillRetain, base.WillQosLevel, base.WillFlag, base.WillTopic, base.WillMessage, base.CleanSession, _KeepAlivePeriod);
                            break;
                        case 1:
                            mqttClient.Connect(base.ClientId, _UserName, _Password, base.CleanSession, _KeepAlivePeriod);
                            break;
                        case 2:
                            mqttClient.Connect(base.ClientId, _UserName, _Password);
                            break;
                        case 3:
                            mqttClient.Connect(base.ClientId);
                            break;
                    }
                }
                catch (Exception ex)
                {
                }

                isConnected = mqttClient.IsConnected;
            }
        }
    }
}
