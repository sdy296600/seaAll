using FarPoint.Win.Spread;
using CoFAS.NEW.MES.HS_SI.Entity;
using CoFAS.NEW.MES.HS_SI.Provider;
using System;
using System.Data;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core;

namespace CoFAS.NEW.MES.SW_SI.Business
{
    public class AuthInformation_Business
{
    public DataTable AuthInformation_R10(AuthInformation_Entity _Entity)
    {
        try
        {
            using (DBManager _DBManager = new DBManager())
            {
                DataTable _DataTable = new AuthInformation_Provider(_DBManager).AuthInformation_R10(_Entity);
                return _DataTable;
            }
        }
        catch (ExceptionManager _ExceptionManager)
        {
            throw _ExceptionManager;
        }
        catch (Exception _Exception)
        {
            throw new ExceptionManager(this, "DataTable AuthInformation_R10(AuthInformation_Entity _Entity)", _Exception);
        }
    }

    public DataTable AuthInformation_R20(string _UserAccount)
    {
        try
        {
            using (DBManager _DBManager = new DBManager())
            {
                DataTable _DataTable = new AuthInformation_Provider(_DBManager).AuthInformation_R20(_UserAccount);
                return _DataTable;
            }
        }
        catch (ExceptionManager _ExceptionManager)
        {
            throw _ExceptionManager;
        }
        catch (Exception _Exception)
        {
            throw new ExceptionManager(this, "DataTable AuthInformation_R20(string _UserAccount)", _Exception);
        }
    }

    public bool AuthInformation_A10(AuthInformation_Entity _Entity, string _UserAccount, ref xFpSpread fpMain)
    {
        try
        {
            using (DBManager _DBManager = new DBManager())
            {
                bool _bool = new AuthInformation_Provider(_DBManager).AuthInformation_A10(_Entity, _UserAccount, ref fpMain);
                return _bool;
            }
        }
        catch (ExceptionManager _ExceptionManager)
        {
            throw _ExceptionManager;
        }
        catch (Exception _Exception)
        {
            throw new ExceptionManager(this, "bool AuthInformation_A10(AuthInformation_Entity _Entity, string _UserAccount, ref FpSpread fpMain)", _Exception);
        }
    }

    public DataTable AuthInformation_R30()
    {
        try
        {
            using (DBManager _DBManager = new DBManager())
            {
                DataTable _DataTable = new AuthInformation_Provider(_DBManager).AuthInformation_R30();
                return _DataTable;
            }
        }
        catch (ExceptionManager _ExceptionManager)
        {
            throw _ExceptionManager;
        }
        catch (Exception _Exception)
        {
            throw new ExceptionManager(this, "DataTable AuthInformation_R30()", _Exception);
        }
    }

    public bool AuthInformation_A20(string _UserAccount, string _User1, ref xFpSpread fpRight)
    {
        try
        {
            using (DBManager _DBManager = new DBManager())
            {
                bool _bool = new AuthInformation_Provider(_DBManager).AuthInformation_A20(_UserAccount, _User1, ref fpRight);
                return _bool;
            }
        }
        catch (ExceptionManager _ExceptionManager)
        {
            throw _ExceptionManager;
        }
        catch (Exception _Exception)
        {
            throw new ExceptionManager(this, "bool AuthInformation_A20(string _UserAccount, string _User1, ref FpSpread fpRight)", _Exception);
        }
    }
}
}
