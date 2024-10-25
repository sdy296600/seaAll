using FarPoint.Win.Spread;
using CoFAS.NEW.MES.HS_SI.Entity;
using CoFAS.NEW.MES.HS_SI.Provider;
using System;
using System.Data;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core;

namespace CoFAS.NEW.MES.HS_SI.Business
{
    public class MenuInformation_Business
    {
        public DataTable MenuInformation_R10(MenuInformation_Entity _Entity)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    DataTable _DataTable = new MenuInformation_Provider(_DBManager).MenuInformation_R10(_Entity);
                    return _DataTable;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "DataTable MenuInformation_R10(MenuInformation_Entity _Entity)", _Exception);
            }
        }

        public DataTable MenuInformation_R20(string p_menu_id)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    DataTable _DataTable = new MenuInformation_Provider(_DBManager).MenuInformation_R20(p_menu_id);
                    return _DataTable;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "DataTable MenuInformation_R20(string p_menu_id)", _Exception);
            }
        }
        public DataTable MenuInformation_R30(string _module)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    DataTable _DataTable = new MenuInformation_Provider(_DBManager).MenuInformation_R30(_module);
                    return _DataTable;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "DataTable MenuInformation_R30(string _module)", _Exception);
            }
        }


        public bool MenuInformation_A10(MenuInformation_Entity _Entity, ref xFpSpread fpMain)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new MenuInformation_Provider(_DBManager).MenuInformation_A10(_Entity, ref fpMain);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "MenuInformation_A10(MenuInformation_Entity _Entity, ref FpSpread fpMain)", _Exception);
            }
        }

        public bool MenuInformation_A20(string _Module, string _ModuleName, string _Account)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new MenuInformation_Provider(_DBManager).MenuInformation_A20(_Module, _ModuleName, _Account);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "MenuInformation_A20(string _Module, string _ModuleName)", _Exception);
            }
        }
    }
}