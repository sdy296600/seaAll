using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core.Provider;
using System;
using System.Data;

namespace CoFAS.NEW.MES.Core.Business
{
    public class LoginBusiness
    {
        #region User Login

        public DataTable Login_Info(LoginEntity _LoginEntity)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable = new LoginProvider(pDBManager).Login_Info(_LoginEntity);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "Login_Info(LoginEntity _LoginEntity)", pException);
            }
        }
        public DataTable UserPWChg(LoginEntity _LoginEntity)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable = new LoginProvider(pDBManager).UserPWChg(_LoginEntity);
                    return pDataTable;
                }
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager(this, "UserPWChg(LoginEntity _LoginEntity)", pException);
            }
        }


        #endregion
    }
}
