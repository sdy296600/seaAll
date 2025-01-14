using Code4Bugs.Utils.Intercomm.Sockets;
using EasyModbus;
using Microsoft.Data.SqlClient;
using System.Web;

namespace Elec
{
    public partial class Form1 : Form
    {
        CancellationTokenSource elecToken0 = new CancellationTokenSource();
        CancellationTokenSource elecToken1 = new CancellationTokenSource();
        CancellationTokenSource elecToken2 = new CancellationTokenSource();
        CancellationTokenSource elecToken3 = new CancellationTokenSource();
        CancellationTokenSource elecToken4 = new CancellationTokenSource();
        CancellationTokenSource elecToken5 = new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();

          
        }

        public void GetElec(ModbusClient client)
        {
            int machine_index = 0;
            try
            {
                string[] ips = client.IPAddress.Split('.');
                client.Connect();

                int ipLast = Convert.ToInt32(ips[ips.Length - 1]);

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
                dbSave(client.IPAddress, (mhours + khours).ToString());
                client.Disconnect();
            }
            catch (Exception ex)
            {
                WriteLog(client.IPAddress +" "+ex.Message);
            }
        }
        private void dbSave(string ip, string data) 
        {
            try
            {
                string sql = $@"
                   INSERT INTO [dbo].[ELEC_DATA_LOG]
                    (
                        [DATETIME],
                        [VALUE],
                        [IP]
                    )
                    VALUES
                    (
                        GETDATE(),
                        '{data}', 
                        '{ip}'    
                    );" ;
                using (SqlConnection sqlconn = new SqlConnection("Server=10.10.10.180; Database=HS_MES; User Id=hansol_mes; Password=Hansol123!@#;TrustServerCertificate=true;"))
                {
                    sqlconn.Open();
                    using (SqlCommand sqlcmd = new SqlCommand(sql, sqlconn))
                    {
                        using (SqlDataReader reader = sqlcmd.ExecuteReader())
                        {
                            string log = $"{ip} {data.ToString()}";
                            WriteLog(log);
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
        }
        private void WriteLog(string log)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    listBox1.Items.Insert(0, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : {log}");

                    if (listBox1.Items.Count > 100)
                    {
                        listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
                    }
                }));
            }
            else
            {
                listBox1.Items.Insert(0, $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} : {log}");

                if (listBox1.Items.Count > 100)
                {
                    listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
                }
            }
        }
        public async Task RunGetElec(int machine_no, CancellationToken token)
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
                        GetElec(client);
                        await Task.Delay(10000);
                    }
                    continue;
                }

                ip = $"172.1.100.15{machine_no}";
                clients.Add(new ModbusClient(ip, 502));
                ip = $"172.1.100.16{machine_no}";
                clients.Add(new ModbusClient(ip, 502));
                ip = $"172.1.100.17{machine_no}";
                clients.Add(new ModbusClient(ip, 502));
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string mn = machine_no == 0 ? "13" : $"2{machine_no}";

                foreach (ModbusClient client in clients)
                {
                    GetElec(client);
                }




                await Task.Delay(10000);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            elecToken0.Cancel();
            elecToken1.Cancel();
            elecToken2.Cancel();
            elecToken3.Cancel();
            elecToken4.Cancel();
            elecToken5.Cancel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
    }
}
