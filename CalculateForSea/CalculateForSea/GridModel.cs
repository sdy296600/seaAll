using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateForSea
{
    internal class GridModel
    {
        public string Date { get; set; }
        public string 설비No { get; set; }
        public string V1 { get; set; }
        public string V2 { get; set; }
        public string V3 { get; set; }
        public string V4 { get; set; }
        public string 가속위치 { get; set; }
        public string 감속위치 { get; set; }
        public string 메탈압력 { get; set; }
        public string 승압시간 { get; set; }
        public string 비스켓두께 { get; set; }
        public string 형체력 { get; set; }
        public string 형체력MN { get; set; }
        public string 사이클타임 { get; set; }
        public string 형체중자입시간 { get; set; }
        public string 주탕시간 { get; set; }
        public string 사출전진시간 { get; set; }
        public string 제품냉각시간 { get; set; }
        public string 형개중자후퇴시간 { get; set; }
        public string 압출시간 { get; set; }
        public string 취출시간 { get; set; }
        public string 스프레이시간 { get; set; }
        public string 금형내부 { get; set; }
        public string 오염도A { get; set; }
        public string 오염도B { get; set; }
        public string 탱크진공 { get; set; }
        
        [JsonIgnore]
        public string 트리거 { get; set; }


    }
}
