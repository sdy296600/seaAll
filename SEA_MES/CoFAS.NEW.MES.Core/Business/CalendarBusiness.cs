using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core.Provider;
using System;

using System.Data;


namespace CoFAS.NEW.MES.Core.Business
{
    public class CalendarBusiness
    {
        #region Calendar

        public DataTable Calendar_Info(CalendarEntity _CalendarEntity)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable = new CalendarProvider(pDBManager).Calendar_Info(_CalendarEntity);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "Calendar_Info(CalendarEntity _CalendarEntity)", pException);
            }
        }

        public DataTable Calendar_Info_R2(CalendarEntity _CalendarEntity)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable = new CalendarProvider(pDBManager).Calendar_Info_R2(_CalendarEntity);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "Calendar_Info_R2(CalendarEntity _CalendarEntity)", pException);
            }
        }

        public DataTable Calendar_Order_Info(string _Datetime)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable = new CalendarProvider(pDBManager).Calendar_Order_Info(_Datetime);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "Calendar_Info(CalendarEntity _CalendarEntity)", pException);
            }
        }

        #endregion
    }
}
