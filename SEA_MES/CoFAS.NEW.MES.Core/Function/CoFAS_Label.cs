using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace CoFAS.NEW.MES.Core.Function
{
    public class CoFAS_Label
    {

        public Bitmap WebImageView(string URL)
        {
            // 웹이미지 가져 오기
            try
            {
                WebClient Downloader = new WebClient();
                string url = "https://api.labelary.com/v1/printers/12dpmm/labels/5x2/0/" + URL;
                Stream ImageStream = Downloader.OpenRead(url); //url stream 받기
                Bitmap DownloadImage = Bitmap.FromStream(ImageStream) as Bitmap; // 이미지로 변환
                return DownloadImage;
            }

            catch (Exception)
            {
                return null;

            }

        }
    }
}


