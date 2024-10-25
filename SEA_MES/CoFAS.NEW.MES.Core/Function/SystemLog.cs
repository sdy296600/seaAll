using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace CoFAS.NEW.MES.Core.Function
{
    public class SystemLog
    {  
        // ip Address 가져오기
        public static string Get_IpAddress()
        {
            string ip = "";
            IPHostEntry pIPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var item in pIPHostEntry.AddressList)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = item.ToString();
                }
            }

            return ip;

            // 권장하지 않는 방법이라함 .net 2.0버전
            //IPHostEntry host = Dns.GetHostByName(Dns.GetHostName());
            //string myip = host.AddressList[0].ToString();
            //return myip;
        }
        // Mac Address 가져오기
        public static string Get_MacAddress()
        {
            string MacAddress = "";

            try
            {

                MacAddress = NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();
                MacAddress = MacAddress.Substring(0, 2) + ":" + MacAddress.Substring(2, 2) + ":" + MacAddress.Substring(4, 2) + ":" + MacAddress.Substring(6, 2) + ":" + MacAddress.Substring(8, 2) + ":" + MacAddress.Substring(10, 2);

                return MacAddress;
            }
            catch(Exception err)
            {
                return MacAddress;
            }

        }
        // PC Name 가져오기
        public static string Get_PcName()
        {
            string PCName = "";
            PCName = Environment.MachineName;

            return PCName;
        }

        public class SimpleLogger
        {
            private static readonly object locker = new object();

            public void WriteToLog(string fileName, string text)
            {
                lock (locker)
                {
                    StreamWriter SW;

                    //해당 파일이 있을 때
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + fileName))
                    {
                        FileInfo flinfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + fileName);

                        //string strFileSize = GetFileSize(flinfo.Length);

                        //500MB를 기준으로 로그 파일 초기화 시킨다. 500MB = 52,428,800 byte
                        if (flinfo.Length > 52428800)
                        {
                            //초기화. 해당 파일을 지우고 새로 만든다.
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + fileName);

                            SW = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + fileName);
                            SW.WriteLine(text);
                            SW.Close();

                            SW.Dispose();
                        }
                        else
                        {
                            //덮어 씌우기
                            SW = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + fileName);
                            SW.WriteLine(text);
                            SW.Close();

                            SW.Dispose();
                        }
                    }
                    //해당 파일일 없을때
                    else
                    {
                        SW = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + fileName);
                        SW.WriteLine(text);
                        SW.Close();

                        SW.Dispose();
                    }
                }
            }
        }

    }


}
