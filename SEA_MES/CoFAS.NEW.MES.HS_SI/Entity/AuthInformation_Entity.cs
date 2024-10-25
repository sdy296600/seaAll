
namespace CoFAS.NEW.MES.HS_SI.Entity
{
    public class AuthInformation_Entity
    {
        // 기본정보
        public string user_account { get; set; }
        public string user_name { get; set; }
        public string dept_code { get; set; }
        public string dept_name { get; set; }
        public string user_authority { get; set; }

        // 조회조건
        public string _Authority_name { get; set; }

    }
}
