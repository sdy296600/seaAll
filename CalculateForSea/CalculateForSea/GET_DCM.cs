using DevExpress.Xpo;
using System;
using System.Threading.Tasks;
using System.Threading;
using MCProtocol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySqlConnector;
using DevExpress.Internal;
using static CalculateForSea.Form1;

namespace CalculateForSea
{
    public class GET_DCM {
        int i = 1;
        public async Task GetPlcAsync(GridModel model,DataModel model2)
        {
            Mitsubishi.McProtocolTcp mcProtocolTcp = new Mitsubishi.McProtocolTcp();
            int cnt = 0;
            int errCnt = 0;
            int warmCnt = 0;
            int no = i;
            try
            {
                var cts = new CancellationTokenSource();
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(2), cts.Token);

                if (no != 0)
                {
                    mcProtocolTcp.HostName = $"172.1.100.18{no}";
                }
                else 
                {
                    mcProtocolTcp.HostName = $"172.1.100.102";
                }

                mcProtocolTcp.PortNumber = 5001;
                mcProtocolTcp.CommandFrame = Mitsubishi.McFrame.MC3E;
                // Open 작업을 별도 실행
                var openTask = Task.Run(async () => await mcProtocolTcp.Open(), cts.Token);

                //Open과 타임아웃을 동시에 실행
                var completedTask = await Task.WhenAny(openTask, timeoutTask);
                if (completedTask == timeoutTask)
                {
                    cts.Cancel(); // 타임아웃 발생 시 작업 취소
                    throw new TimeoutException("PLC 연결이 2초 내에 완료되지 않았습니다.");
                }

                cts.Cancel(); // Open이 완료되었으면 타임아웃 취소

                if (mcProtocolTcp.Connected)
                {
                    // 비동기 메서드 호출
                    byte[] results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 3704, 1);
                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        cnt = BitConverter.ToUInt16(results, 0);
                        model2.getDtOkCnt = cnt;
                        SaveWorkData($"UPDATE WORK_DATA SET WORK_OKCNT = '{cnt}'", model2);
                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 3705, 1);
                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        errCnt = BitConverter.ToUInt16(results, 0);
                        model2.getDtErrCnt = errCnt;

                        SaveWorkData($"UPDATE WORK_DATA SET WORK_ERRCOUNT = '{errCnt}'", model2);
                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 3706, 1);
                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        warmCnt = BitConverter.ToUInt16(results, 0);
                        model2.getDtWarmCnt = warmCnt;
                        SaveWorkData($"UPDATE WORK_DATA SET WORK_WARMUPCNT = '{warmCnt}'", model2);
                    }

                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6900, 1);
                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.V1 = (BitConverter.ToUInt16(results, 0)/100.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6902, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.V2 = (BitConverter.ToUInt16(results, 0) / 100.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6904, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.V3 = (BitConverter.ToUInt16(results, 0) / 100.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6906, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.V4 = (BitConverter.ToUInt16(results, 0) / 100.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6908, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.가속위치 = BitConverter.ToUInt16(results, 0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6910, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.감속위치 = BitConverter.ToUInt16(results, 0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6912, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.메탈압력 = (BitConverter.ToUInt16(results, 0) / 10.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6914, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.승압시간 = BitConverter.ToUInt16(results, 0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6916, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.비스켓두께 = BitConverter.ToUInt16(results, 0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6918, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.형체력 = BitConverter.ToUInt16(results, 0).ToString();
                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6920, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.형체력MN = (BitConverter.ToUInt16(results, 0) / 100.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6936, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.사이클타임 = (BitConverter.ToUInt16(results, 0) / 10.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6938, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.형체중자입시간 = (BitConverter.ToUInt16(results, 0) / 10.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6940, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.주탕시간 = (BitConverter.ToUInt16(results, 0) / 10.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6942, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.사출전진시간 = (BitConverter.ToUInt16(results, 0) / 10.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6944, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.제품냉각시간 = (BitConverter.ToUInt16(results, 0) / 10.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6946, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.형개중자후퇴시간 = (BitConverter.ToUInt16(results, 0) / 10.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6948, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.압출시간 = (BitConverter.ToUInt16(results, 0) / 10.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6950, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.취출시간 = (BitConverter.ToUInt16(results, 0) / 10.0).ToString();

                    }
                    results = await mcProtocolTcp.ReadDeviceBlock(Mitsubishi.PlcDeviceType.D, 6952, 1);

                    if (results != null && results.Length >= 2) // 워드 값은 2바이트
                    {
                        model.스프레이시간 = (BitConverter.ToUInt16(results, 0) / 10.0).ToString();

                    }
                }
            }
            catch (TimeoutException ex)
            {
                //UpdateUI("타임아웃 발생: " + ex.Message, true);
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (mcProtocolTcp.Connected)
                {
                    mcProtocolTcp.Close();
                }
            }
       
        }
        public GET_DCM() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        public GET_DCM(int i) : base()
        {
            this.i = i;
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
       
        public void SaveWorkData(string sql ,DataModel model)
        {

            string mysqlString = sql + $@" WHERE WORK_PERFORMANCE_ID = '{model.ID}'";
            MySqlConnection conn2 = new MySqlConnection("Server=10.10.10.216;Database=hansoldms;Uid=coever;Pwd=coever119!");
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
}