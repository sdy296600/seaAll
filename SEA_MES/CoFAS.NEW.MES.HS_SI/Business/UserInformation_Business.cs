using FarPoint.Win.Spread;
using CoFAS.NEW.MES.HS_SI.Entity;
using CoFAS.NEW.MES.HS_SI.Provider;
using System;
using System.Data;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core;

namespace CoFAS.NEW.MES.HS_SI.Business
{
    public class UserInformation_Business
    {
        public DataTable UserInformation_R10(UserInformation_Entity _Entity)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    DataTable _DataTable = new UserInformation_Provider(_DBManager).UserInformation_R10(_Entity);
                    return _DataTable;
                }
            }
            catch(ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch(Exception _Exception)
            {
                throw new ExceptionManager(this, "DataTable UserInformation_R10(UserInformation_Entity _Entity)", _Exception);
            }

        }

        public bool UserInformation_A10(UserInformation_Entity _Entity, ref xFpSpread fpMain)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new UserInformation_Provider(_DBManager).UserInformation_A10(_Entity, ref fpMain);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "DataTable UserInformation_A10(UserInformation_Entity _Entity, ref FpSpread fpMain)", _Exception);
            }

        }

        public bool UserInformation_A20(UserInformation_Entity _Entity, ref xFpSpread fpMain)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new UserInformation_Provider(_DBManager).UserInformation_A20(_Entity, ref fpMain);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "DataTable UserInformation_A20(UserInformation_Entity _Entity, ref FpSpread fpMain)", _Exception);
            }

        }
    }
}
