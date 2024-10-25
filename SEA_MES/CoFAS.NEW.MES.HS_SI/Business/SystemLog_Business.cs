using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.HS_SI.Entity;
using CoFAS.NEW.MES.HS_SI.Provider;
using System;
using System.Data;

namespace CoFAS.NEW.MES.HS_SI.Business
{
    public class SystemLog_Business
{
    public DataTable SystemLogStatus_R10()
    {
        try
        {
            using (DBManager _DBManager = new DBManager())
            {
                DataTable _DataTable = new SystemLog_Provider(_DBManager).SystemLogStatus_R10();
                return _DataTable;
            }
        }
        catch (ExceptionManager _ExceptionManager)
        {
            throw _ExceptionManager;
        }
        catch (Exception _Exception)
        {
            throw new ExceptionManager(this, "DataTable SystemLogStatus_R10())", _Exception);
        }
    }

    public DataTable SystemLogStatus_R20(SystemLog_Entity _Entity)
    {
        try
        {
            using (DBManager _DBManager = new DBManager())
            {
                DataTable _DataTable = new SystemLog_Provider(_DBManager).SystemLogStatus_R20(_Entity);
                return _DataTable;
            }
        }
        catch (ExceptionManager _ExceptionManager)
        {
            throw _ExceptionManager;
        }
        catch (Exception _Exception)
        {
            throw new ExceptionManager(this, "DataTable SystemLogStatus_R20(string _userAccount))", _Exception);
        }
    }
}
}
