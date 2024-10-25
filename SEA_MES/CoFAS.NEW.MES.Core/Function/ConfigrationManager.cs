using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoFAS.NEW.MES.Core.Function
{
    public class ConfigrationManager
    {
        #region Field

        private static string _strDatabaseServer = "";
        private static string _strDatabaseName = "";
        private static string _strDatabaseID = "";
        private static string _strDatabasePassword = "";
        private static string _strSkin;
        private static string _strAccount;
        private static string _strPassword;
        private static string _strHasToSave;
        private static string _strCORP_CD;
        private static string _strCORP_NM;
        private static string _strUSER_CORP;
        private static string _strEMP_NO;
        private static string _strEMP_NM;
        private static string _strConnectionString;

        #endregion

        #region 데이타베이스 서버 - DatabaseServer

        public static string DatabaseServer
        {
            get
            {
                if (_strDatabaseServer == null)
                {
                    _strDatabaseServer = "127.0.0.1";
                }
                return _strDatabaseServer;
            }
            set
            {
                _strDatabaseServer = value;
            }
        }

        #endregion

        #region 데이타베이스명 - DatabaseName

        /// <summary>
        /// 데이타베이스명
        /// </summary>
        public static string DatabaseName
        {
            get
            {
                if (_strDatabaseName == null)
                {
                    _strDatabaseName = "CoFAS_TowelStory";
                }
                return _strDatabaseName;
            }
            set
            {
                _strDatabaseName = value;
            }
        }

        #endregion

        #region 데이타베이스 ID - DatabaseID

        /// <summary>
        /// 데이타베이스 ID
        /// </summary>
        public static string DatabaseID
        {
            get
            {
                return _strDatabaseID;
            }
        }

        #endregion

        #region 데이타베이스 패스워드 - DatabasePassword

        /// <summary>
        /// 데이타베이스 패스워드
        /// </summary>
        /// 
        public static string DatabasePassword
        {
            get
            {
                if (_strDatabasePassword == null)
                    _strDatabasePassword = "";

                return _strDatabasePassword;
            }
            set
            {
                _strDatabasePassword = value;
            }
        }

        #endregion

        #region 스킨 - Skin

        /// <summary>
        /// 스킨
        /// </summary>
        public static string Skin
        {
            get
            {
                if (string.IsNullOrEmpty(_strSkin))
                {
                    _strSkin = "Office 2010 Black";
                }
                return _strSkin;
            }
            set
            {
                _strSkin = value;
            }
        }

        #endregion

        #region 계정 - Account

        /// <summary>
        /// 계정
        /// </summary>
        public static string Account
        {
            get
            {
                if (string.IsNullOrEmpty(_strAccount))
                {
                    _strAccount = string.Empty;
                }
                return _strAccount;
            }
            set
            {
                _strAccount = value;
            }
        }

        #endregion

        #region 패스워드 - Password

        /// <summary>
        /// 패스워드
        /// </summary>
        public static string Password
        {
            get
            {
                _strPassword = "123456";
                if (string.IsNullOrEmpty(_strPassword))
                {
                    _strPassword = string.Empty;
                }
                return _strPassword;
            }
            set
            {
                _strPassword = value;
            }
        }

        #endregion

        #region 저장 여부 - HasToSave

        /// <summary>
        /// 저장 여부
        /// </summary>
        /// <remarks>저장 : 1, 저장 안함 : 2</remarks>
        public static string HasToSave
        {
            get
            {
                if (string.IsNullOrEmpty(_strHasToSave))
                {
                    _strHasToSave = string.Empty;
                }
                return _strHasToSave;
            }
            set
            {
                _strHasToSave = value;
            }
        }

        #endregion

        #region 적용 회사

        /// <summary>
        /// 적용 회사
        /// </summary>
        /// <remarks></remarks>
        public static string CORP_CD
        {
            get
            {
                if (string.IsNullOrEmpty(_strCORP_CD))
                {
                    _strCORP_CD = string.Empty;
                }
                return _strCORP_CD;
            }
            set
            {
                _strCORP_CD = value;
            }
        }

        #endregion

        #region 적용 회사명

        /// <summary>
        /// 적용 회사명
        /// </summary>
        /// <remarks></remarks>
        public static string CORP_NM
        {
            get
            {
                if (string.IsNullOrEmpty(_strCORP_NM))
                {
                    _strCORP_NM = string.Empty;
                }
                return _strCORP_NM;
            }
            set
            {
                _strCORP_NM = value;
            }
        }

        #endregion

        #region 회사조회권한

        public static string USER_AUTH
        {
            get
            {
                if (string.IsNullOrEmpty(_strUSER_CORP))
                {
                    _strUSER_CORP = string.Empty;
                }
                return _strUSER_CORP;
            }

            set
            {
                _strUSER_CORP = value;
            }
        }

        #endregion

        #region 사원번호

        /// <summary>
        /// 사원번호
        /// </summary>
        /// <remarks></remarks>
        public static string EMP_NO
        {
            get
            {
                if (string.IsNullOrEmpty(_strEMP_NO))
                {
                    _strEMP_NO = string.Empty;
                }
                return _strEMP_NO;
            }
            set
            {
                _strEMP_NO = value;
            }
        }

        #endregion

        #region 사원이름

        /// <summary>
        /// 사원이름
        /// </summary>
        /// <remarks></remarks>
        public static string EMP_NM
        {
            get
            {
                if (string.IsNullOrEmpty(_strEMP_NM))
                {
                    _strEMP_NM = string.Empty;
                }
                return _strEMP_NM;
            }
            set
            {
                _strEMP_NM = value;
            }
        }

        #endregion

        #region 연결문자

        /// <summary>
        /// 연결 문자
        /// </summary>
        /// <remarks></remarks>
        public static string ConnectionString
        {
            get
            {
                _strConnectionString = string.Format
                (
                    "Server={0};Database={1};UID={2};PWD={3}", DatabaseServer, DatabaseName, DatabaseID, DatabasePassword
                );
                return _strConnectionString;
            }
        }

        #endregion
    }
}
