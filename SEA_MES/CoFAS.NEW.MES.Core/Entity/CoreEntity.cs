using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CoFAS.NEW.MES.Core.Entity
{
    public class SpreadEntity
    {
        public string user_code { get; set; }

        public SpreadEntity() { }

        public SpreadEntity(SpreadEntity pSpreadEntity)
        {
            user_code = pSpreadEntity.user_code;
        }
    }
    public class UserEntity
    {
        #region Property

        //사용자 설정 엔티티
        public string user_account { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }
        public string user_authority { get; set; }
        public string user_dept { get; set; }
        public string dept_name { get; set; }
 
        public string ftp_ip { get; set; }
        public string ftp_port { get; set; }
        public string ftp_id { get; set; }
        public string ftp_pw { get; set; }
        public string user_title { get; set; }
        public string title_name { get; set; }


        public string FONT_TYPE { get; set; }
        public int FONT_SIZE { get; set; }


        public string V_NAME { get; set; }
        #endregion

        #region 생성자 - UserEntity()

        public UserEntity()
        {
        }

        #endregion

        #region 생성자 - UserEntity(pUserEntity)

        public UserEntity(UserEntity pUserEntity)
        {
            user_account = pUserEntity.user_account;
            user_name = pUserEntity.user_name;
            user_authority = pUserEntity.user_authority;
            user_dept = pUserEntity.user_dept;
            dept_name = pUserEntity.dept_name;
            user_title = pUserEntity.user_title;
            title_name = pUserEntity.title_name;

            ftp_ip = pUserEntity.ftp_ip;
            ftp_port = pUserEntity.ftp_port;
            ftp_id = pUserEntity.ftp_id;
            ftp_pw = pUserEntity.ftp_pw;


        }

        #endregion

    }

    public class MenuSettingEntity
    {
        #region Property

        ////메뉴설정 엔티티
        public string MENU_NO { get; set; }
        public string MENU_NAME { get; set; }
        public string MENU_MODULE { get; set; }
        public string MENU_WINDOW_NAME { get; set; }
        public string MENU_USEYN { get; set; }
        public string MENU_INIT { get; set; }
        public string MENU_SEARCH { get; set; }

        public string BASE_YN { get; set; }
        public string BASE_FORM_NAME { get; set; }
        public string BASE_TABLE { get; set; }

        public string BASE_WHERE { get; set; }

        public string BASE_ORDER { get; set; }

        public string DESCRIPTION { get; set; }
        public string MENU_SAVE { get; set; }
        public string MENU_ADDITEM { get; set; }
        public string MENU_DELETE { get; set; }
        public string MENU_PRINT { get; set; }
        public string MENU_IMPORT { get; set; }
        public string MENU_EXPORT { get; set; }
        public string MENU_ICONCSS { get; set; }

        #endregion

        #region 생성자 - MenuSettingEntity()

        public MenuSettingEntity()
        {
        }

        #endregion

        #region 생성자 - MenuSettingEntity(pUserEntity)

        public MenuSettingEntity(DataRowView pDataRowView)
        {
            MENU_NO = pDataRowView["menu_id"].ToString().Trim(); //menu_code , menu_no
            MENU_NAME = pDataRowView["menu_name"].ToString().Trim(); 
            MENU_WINDOW_NAME = pDataRowView["window_name"].ToString().Trim(); 
            MENU_MODULE = pDataRowView["module"].ToString().Trim(); 
            MENU_USEYN = pDataRowView["menu_useyn"].ToString().Trim(); 
            MENU_SEARCH = pDataRowView["menu_search"].ToString().Trim(); 
            MENU_PRINT = pDataRowView["menu_print"].ToString().Trim();
            MENU_DELETE = pDataRowView["menu_delete"].ToString().Trim();
            MENU_SAVE = pDataRowView["menu_save"].ToString().Trim();
            MENU_IMPORT = pDataRowView["menu_import"].ToString().Trim();
            MENU_EXPORT = pDataRowView["menu_export"].ToString().Trim();
            MENU_INIT = pDataRowView["menu_initialize"].ToString().Trim();
            MENU_ADDITEM = pDataRowView["menu_newadd"].ToString().Trim();
            MENU_ICONCSS = pDataRowView["icon"].ToString().Trim();
            BASE_YN = pDataRowView["BASE_YN"].ToString().Trim();
            BASE_FORM_NAME = pDataRowView["BASE_FORM_NAME"].ToString().Trim();
            BASE_TABLE = pDataRowView["BASE_TABLE"].ToString().Trim();
            DESCRIPTION = pDataRowView["DESCRIPTION"].ToString().Trim();
            BASE_TABLE = pDataRowView["BASE_TABLE"].ToString().Trim();
            BASE_WHERE = pDataRowView["BASE_WHERE"].ToString().Trim();
            BASE_ORDER = pDataRowView["BASE_ORDER"].ToString().Trim();

        }

        #endregion
    }

    public class SystemLogEntity
    {
        #region Property

        //사용자 설정 엔티티
        public string user_account { get; set; }
        public string user_ip { get; set; }
        public string user_mac { get; set; }
        public string user_pc { get; set; }
        public string event_type { get; set; }
        public string event_log { get; set; }
        public string menu_id { get; set; }
        #endregion

        #region 생성자 - SystemLogInfoEntity()

        public SystemLogEntity() { }

        #endregion

        #region 생성자 - SystemLogInfoEntity(pSystemLogInfoEntity)

        public SystemLogEntity(SystemLogEntity pSystemLogEntity)
        {
            user_account = pSystemLogEntity.user_account;
            user_ip = pSystemLogEntity.user_ip;
            user_mac = pSystemLogEntity.user_mac;
            user_pc = pSystemLogEntity.user_pc;
            event_type = pSystemLogEntity.event_type;
            event_log = pSystemLogEntity.event_log;
            menu_id = pSystemLogEntity.menu_id;
        }

        #endregion
    }

    public class CodeSelect_Entity
    {
        public string ServiceName { get; set; }
        public CodeSelect_Entity() { }
        public CodeSelect_Entity(CodeSelect_Entity _Entity)
        {
            if (_Entity == null)
                return;

            ServiceName = _Entity.ServiceName;
        }
    }

    public class MenuButton_Entity
    {
        public string menu_ID { get; set; }
        public string user_ID { get; set; }
    }

    public class PasswordEntity
    {
        public string user_account { get; set; }
        public string user_name { get; set; }
    }

    public class MenuSave_Entity
    {
        public string user_account { get; set; }
        public string menu_name { get; set; }
        public string spread_name { get; set; }
        public string column_tag { get; set; }
        public int width { get; set; }
        public string visible { get; set; }
        public string allowAutoFilter { get; set; }
        public int seq { get; set; } 

    }

    public class xFpSpread_Entity
    {

        public string HeaderName { get; set; }          // 헤더명
        public string Width  { get; set; }              // 헤더넓이
        public string Visible  { get; set; }            // 0:보임 1:숨김
        public string Locked { get; set; }              // 0:읽기전용 1:수정가능
        public string Align { get; set; }               // 0:왼쪽 1:중앙 2:오른쪽
        public string CellType  { get; set; }           // 0:None 1:Text 2:Number 3:ComboBox 4:Button 5:...................
        public string Length { get; set; }              // 전체자리수
        public string Point { get; set; }               // 소숫점자리수
        public string CodeType { get; set; }            // 조회유형 0:dbo.USP_CM000010RP_R10w 1:USP_CM000011RP_R10
        public string CodeName  { get; set; }           // 조회항목
        public string ColumnKey { get; set; }           // 컬럼Tag(Key)
        public string Seq { get; set; }                 // 순번(사용은 안하고 참고용으로만)


    }
    public class 이앤아이비수주
    {
        public string 구분 { get; set; }
        public DateTime 발주일자        { get; set; }
        public DateTime 납기일자       { get; set; }
        public string 고객사        { get; set; }
        public string 발주번호      { get; set; }
        public string 프로젝트명       { get; set; }
        //public string STOCK_MST_TYPE2 { get; set; }
        public string STOCK_MST_OUT_CODE { get; set; }
        //public string STOCK_MST_ID { get; set; }
        //public string STOCK_MST_STANDARD { get; set; }

        public string 단위 { get; set; }
        public decimal 수량 { get; set; }
        public decimal 단가 { get; set; }
        public decimal 합계 { get; set; }
        public string 진행상황 { get; set; }
        public decimal 공급가액1 { get; set; }
        public decimal 공급가액2 { get; set; }
    }

    public class CONSUMABLE_MST
    {
        public string TYPE { get; set; }     
        public string RESOURCE_NO { get; set; }
        public string RESOURCE_TYPE { get; set; }
        public decimal USE_QTY { get; set; }
        public string USE_LOCATION { get; set; }
        public DateTime USE_TIME { get; set; }
        public string COMMENT { get; set; }
    }

}
