using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoFAS.NEW.MES.Core.Entity;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;

namespace CoFAS.NEW.MES.Core.Business
{
    public class CoreBusiness
    {
        #region FpSpred ComboBox

        public DataTable Spread_ComboBox(string pServiceName, string pFirst, string pSecond)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable = new SpreadProvider(pDBManager).Spread_ComobBox(pServiceName, pFirst, pSecond);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "Spread_ComboBox(string pServiceName, string pFirst, string pSecond)", pException);
            }
        }

        #endregion

        public DataTable xComboBox(CodeSelect_Entity _Entity)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable _DataTable = new xComBoBoxProvider(pDBManager).xComboBox(_Entity);
                    return _DataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "xComboBox(CodeSelect_Entity _Entity)", pException);
            }
        }

        public DataTable ComboBoxSetting(string ServiceName, string parameter1, string parameter2, string parameter3)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable _DataTable = new xComBoBoxProvider(pDBManager).ComboBoxSetting(ServiceName, parameter1, parameter2, parameter3);
                    return _DataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "xComboBox(CodeSelect_Entity _Entity)", pException);
            }
        }

        public DataTable MENU_SEARCH_LIST_R10(MenuSettingEntity pMenuSettingEntity)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new MENU_SEARCH_LIST_Provider(pDBManager).MENU_SEARCH_LIST_R10(pMenuSettingEntity);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

        public DataTable BASE_MENU_SETTING_R10(string MENU_WINDOW_NAME, xFpSpread fpMain,string BASE_TABLE)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new MENU_SEARCH_LIST_Provider(pDBManager).BASE_MENU_SETTING_R10(MENU_WINDOW_NAME,fpMain,BASE_TABLE);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

        public DataTable BaseForm1_R10(Panel _Panel, MenuSettingEntity _MenuSettingEntity)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  BaseForm_Provider(pDBManager).BaseForm1_R10(_Panel,_MenuSettingEntity);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

        public DataTable SELECT(string sql)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  BaseForm_Provider(pDBManager).SELECT(sql);
                    return pDataTable;
                }
            }
           
            catch (Exception pException)
            {
                return null;
            }
        }
        
        public DataTable DoubleBaseForm_R10(string _mst_id,string str)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  BaseForm_Provider(pDBManager).DoubleBaseForm_R10(_mst_id,str);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

        public DataTable DoubleBaseForm_R20(string _mst_id, string str, string where)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  BaseForm_Provider(pDBManager).DoubleBaseForm_R20(_mst_id,str,where);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public DataTable DoubleBaseForm_R30(string from, string where1, string where2, string where3)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  BaseForm_Provider(pDBManager).DoubleBaseForm_R30(from,where1,where2,where3);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

        public DataTable DoubleBaseForm_R40(string from, string where1)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  BaseForm_Provider(pDBManager).DoubleBaseForm_R40(from,where1);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public bool BaseForm1_A10(MenuSettingEntity _Entity, xFpSpread fpMain, string BASE_TABLE)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new BaseForm_Provider(_DBManager).BaseForm1_A10(_Entity,fpMain,BASE_TABLE);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }
      

     

     
    }


    public class MenuSettingBusiness
    {
        #region 메뉴 조회하기 - Menu_List_Search(nUserID)
        /// <summary>
        /// 메뉴 조회 하기
        /// </summary>
        /// <param name="pCORP_CD"></param>
        /// <param name="pUserID"></param>
        /// <returns></returns>
        public DataTable MenuList_Search(string pUserID)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable = new MenuSettingProvider(pDBManager).MenuList_Search(pUserID);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "Menu_List_Search(string pUserID)",
                    pException
                );
            }
        }
        #endregion

        #region 개체 구하기 - GetEntity(pDataRow)

        /// <summary>
        /// 개체 구하기
        /// </summary>
        /// <param name="pDataRow">데이타 로우</param>
        /// <returns>메뉴 설정 개체</returns>
        public MenuSettingEntity GetEntity(DataRow pDataRow)
        {
            try
            {
                MenuSettingEntity pMenuSettingEntity = new MenuSettingProvider(null).GetEntity(pDataRow);
                return pMenuSettingEntity;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetEntity(pDataRow)",
                    pException
                );
            }
        }
        #endregion
    }

    public class SystemLogBusiness
    {
        public bool SystemLog_Info(SystemLogEntity _SystemLogEvent)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    bool _ErrorYN = new SystemLogProvider(pDBManager).SystemLog_Info(_SystemLogEvent);

                    //Log_API(_SystemLogEvent);

                    return _ErrorYN;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "SystemLog_Info(SystemLogEntity _SystemLogEvent)", pException);
            }
        }

        
    }
    
    public class MenuButton_Business
    {
        public DataTable MenuButton_Select(string menuID, string userID)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable = new MenuButton_Provider(pDBManager).MenuButton_Select(menuID, userID);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "MenuButton_Select(string menuID, string userID)",
                    pException
                );
            }
        }
    }

    public class MenuSave_Business
    {
        public DataTable MenuSave_R10(MenuSave_Entity _Entity)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new MenuSave_Provider(pDBManager).MenuSave_R10(_Entity);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public void MenuSave_A10(xFpSpread pfpSpread)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                   new MenuSave_Provider(pDBManager).MenuSave_A10(pfpSpread);      
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "MenuSave_A10(xFpSpread pfpSpread)",
                    pException
                );
            }
        }
    }

}
