using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Linq;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.DPS // 변경 필요합니다.
{
    public class CoFAS_WebUtilManager
    {
        // API 설정
        // HttpResponseMessage response = null;
        string result = string.Empty;


        public static string _pKEY { get; set; }

        private static HttpClient client;
        private LoginInterface _user = new LoginInterface();
        public class LoginInterface
        {
            //public string account { get; set; } = DPS.Properties.Settings.Default.ID;
            //public string password { get; set; } = DPS.Properties.Settings.Default.PWD;
            //public string userAccount { get; set; } = DPS.Properties.Settings.Default.ID;
            //public string userPassword { get; set; } = DPS.Properties.Settings.Default.PWD;

            public string userAccount { get; set; } = "admin";
            public string userPassword { get; set; } = "1";
        }

        MultipartFormDataContent _pFILE = new MultipartFormDataContent();
        public CoFAS_WebUtilManager()
        {
            try
            {
                // ip 수정
                // string url = "http://" + DPS.Properties.Settings.Default.API_IP + ":" + DPS.Properties.Settings.Default.API_PORT;

                string url ="http://192.168.11.113:8080";

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //request.Method = "POST";
                //request.ContentType = "application/json";
                //request.Timeout = 3 * 1000;

                //byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_user));

                //using (Stream reqStream = request.GetRequestStream())
                //{
                //    reqStream.Write(bytes, 0, bytes.Length);
                //}


                HttpClient kclient = new HttpClient();
                //kclient.Timeout = TimeSpan.FromMinutes(3000);
                _pKEY = kclient.PostAsync(url.Trim() + "/Login", new StringContent(JsonConvert.SerializeObject(_user), Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;

                //_pKEY = ;
                client = new HttpClient();
                client.BaseAddress = new Uri(url.Trim());
            }
            catch (Exception err)
            {
                return;
            }
        }
        /// <summary>
        /// API 조회용 (GET)
        /// </summary>
        /// <param name="uri"> 서비스(토픽) + 조건식 정의 ex) userMst?usercode=1&userAccount=admin </param>
        public static HttpResponseMessage GETAsync(string uri)
        {
            if (!string.IsNullOrEmpty(_pKEY))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _pKEY);
            return client.GetAsync(uri.Replace("\u200B", "")).Result;
        }
        /// <summary>
        /// API 등록용 (POST)
        /// </summary>
        /// <param name="uri"> 서비스(토픽) ex) userMst </param>
        /// <param name="data"> 등록 데이터(Json Type) </param>
        public static HttpResponseMessage POSTAsync(string uri, string data)
        {
            if (!string.IsNullOrEmpty(_pKEY))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _pKEY);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            return client.PostAsync(uri.Replace("\u200B", ""), httpContent).Result;
        }
        /// <summary>
        /// API 수정용 (PUT)
        /// </summary>
        /// <param name="uri"> 서비스(토픽) ex) userMst </param>
        /// <param name="data"> 수정 데이터(Json Type) </param>
        public static HttpResponseMessage PUTAsync(string uri, string data)
        {
            if (!string.IsNullOrEmpty(_pKEY))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _pKEY);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            return client.PutAsync(uri.Replace("\u200B", ""), httpContent).Result;
        }
        /// <summary>
        /// API 삭제용 (DELETE)
        /// </summary>
        /// <param name="uri"> 서비스(토픽) ex) UseMst?account=1
        public static HttpResponseMessage DELETEAsync(string uri)
        {
            if (!string.IsNullOrEmpty(_pKEY))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _pKEY);
            return client.DeleteAsync(uri.Replace("\u200B", "")).Result;
        }
        /// <summary>
        /// API 파일 등록용 (POST)
        /// </summary>
        /// <param name="uri"> 서비스(토픽) ex) userMst </param>
        /// <param name="file"> 등록 데이터(Json Type) </param>
        public static HttpResponseMessage POSTFileAsync(string uri, MultipartFormDataContent file)
        {
            if (!string.IsNullOrEmpty(_pKEY))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _pKEY);
            return client.PostAsync("FileManager", file).Result;
        }
        /// <summary>
        /// API 파일 수정용 (PUT)
        /// </summary>
        /// <param name="uri"> 서비스(토픽) ex) userMst </param>
        /// <param name="file"> 수정 데이터(Json Type) </param>
        public static HttpResponseMessage PUTFileAsync(string uri, MultipartFormDataContent file)
        {
            if (!string.IsNullOrEmpty(_pKEY))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _pKEY);
            return client.PutAsync("FileManager", file).Result;
        }
    }
}
