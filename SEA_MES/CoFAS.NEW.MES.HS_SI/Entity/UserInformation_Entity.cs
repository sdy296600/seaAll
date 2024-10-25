

namespace CoFAS.NEW.MES.HS_SI.Entity
{
    public class UserInformation_Entity
    {
        // 기본 마스터 정보
        public string user_account { get; set; }
        public string user_name { get; set; }
        public string dept_code { get; set; }
        public string dept_name { get; set; }
        public string user_authority { get; set; }

        // 조회조건
        public string _SearchDept { get; set; }
        public string _SearchUser { get; set; }
        public string _SearchFactory { get; set; }
        public string _SearchTitle { get; set; }

    }
}
