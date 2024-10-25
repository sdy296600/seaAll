using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace CoFAS.NEW.MES.Core.Function
{
    public enum SystemType
    {
        Client,
        Server
    }

    public enum ApplicationType
    {
        ConsoleApplication,
        WindowsApplication,
        WebApplication
    }
    public class ConfigurationManager
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Static
        //////////////////////////////////////////////////////////////////////////////// Private

        #region Field

        /// <summary>
        /// 시스템 종류
        /// </summary>
        private static SystemType _pSystemType;

        /// <summary>
        /// 애플리케이션 종류
        /// </summary>
        private static ApplicationType _pApplicationType;

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Property
        ////////////////////////////////////////////////////////////////////////////////////////// Static
        //////////////////////////////////////////////////////////////////////////////// Public

        #region Property

        #region 시스템 종류 - SystemType

        /// <summary>
        /// 시스템 종류
        /// </summary>
        public static SystemType SystemType
        {
            get
            {
                return _pSystemType;
            }
        }

        #endregion

        #region 애플리케이션 종류 - ApplicationType

        /// <summary>
        /// 애플리케이션 종류
        /// </summary>
        public static ApplicationType ApplicationType
        {
            get
            {
                return _pApplicationType;
            }
        }

        #endregion

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Constructor
        ////////////////////////////////////////////////////////////////////////////////////////// Static

        #region 생성자 - ConfigurationManager()

        /// <summary>
        /// 생성자
        /// </summary>
        static ConfigurationManager()
        {
            try
            {
                string strSystemType = null;
                string strApplicationType = null;

                try
                {
                    strSystemType = System.Configuration.ConfigurationManager.AppSettings["SystemType"];
                    strApplicationType = System.Configuration.ConfigurationManager.AppSettings["ApplicationType"];
                }
                catch
                {
                    strSystemType = null;
                    strApplicationType = null;
                }

                if (strSystemType == null || strApplicationType == null)
                {
                    try
                    {
                        strSystemType = WebConfigurationManager.AppSettings["SystemType"];
                        strApplicationType = WebConfigurationManager.AppSettings["ApplicationType"];
                    }
                    catch
                    {
                        strSystemType = null;
                        strApplicationType = null;
                    }
                }

                switch (strSystemType)
                {
                    case "Client": _pSystemType = SystemType.Client; break;
                    default: _pSystemType = SystemType.Server; break;
                }

                switch (strApplicationType)
                {
                    case "ConsoleApplication": _pApplicationType = ApplicationType.ConsoleApplication; break;
                    case "WindowsApplication": _pApplicationType = ApplicationType.WindowsApplication; break;
                    default: _pApplicationType = ApplicationType.WebApplication; break;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(ConfigurationManager),
                    "ConfigurationManager()",
                    pException
                );
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////// Instance
        //////////////////////////////////////////////////////////////////////////////// Private

        #region 생성자 - ConfigurationManager()

        /// <summary>
        /// 생성자
        /// </summary>
        private ConfigurationManager()
        {
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Static
        //////////////////////////////////////////////////////////////////////////////// Public

        #region 어플리케이션 설정 구하기 - GetApplicationSetting(strApplicationSettingKey)

        /// <summary>
        /// 어플리케이션 설정 구하기
        /// </summary>
        /// <param name="strApplicationSettingKey">애플리케이션 설정 키</param>
        /// <returns>어플리케이션 설정</returns>
        public static string GetApplicationSetting(string strApplicationSettingKey)
        {
            try
            {
                switch (_pApplicationType)
                {
                    case ApplicationType.ConsoleApplication:
                    case ApplicationType.WindowsApplication: return System.Configuration.ConfigurationManager.AppSettings[strApplicationSettingKey];
                    default: return WebConfigurationManager.AppSettings[strApplicationSettingKey];
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(ConfigurationManager),
                    "GetApplicationSetting(strApplicationSettingKey)",
                    pException
                );
            }
        }

        #endregion

        #region 연결 문자열 구하기 - GetConnectionSetting(strConnectionName)

        /// <summary>
        /// 연결 문자열 구하기
        /// </summary>
        /// <param name="strConnectionName">연결명</param>
        /// <returns>연결 문자열</returns>
        public static string GetConnectionSetting(string strConnectionName)
        {
            try
            {
                switch (_pApplicationType)
                {
                    case ApplicationType.ConsoleApplication:
                    case ApplicationType.WindowsApplication: return System.Configuration.ConfigurationManager.ConnectionStrings[strConnectionName].ConnectionString;
                    default: return System.Web.Configuration.WebConfigurationManager.ConnectionStrings[strConnectionName].ConnectionString;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(ConfigurationManager),
                    "GetConnectionSetting(strConnectionName)",
                    pException
                );
            }
        }

        #endregion
    }
}
