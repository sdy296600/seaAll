using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CoFAS.NEW.MES.Core.Provider
{
    public class LoginProvider : EntityManager<LoginEntity>
    {
        public LoginProvider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public override LoginEntity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public DataTable Login_Info(LoginEntity _LoginEntity)
        {
            try
            {
                IDataParameter[] pIDataParameter = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "SQLServer":
                        pIDataParameter = new IDataParameter[]
                        {
                            new SqlParameter("@user_account", SqlDbType.VarChar, 50),
                            new SqlParameter("@user_password", SqlDbType.VarChar, 1000)
                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {
                            new MySqlParameter("@user_account", MySqlDbType.VarChar, 50),
                            new MySqlParameter("@user_password", MySqlDbType.VarChar, 1000)
                        };
                        break;
                }

                pIDataParameter[0].Value = _LoginEntity.user_account;
                pIDataParameter[1].Value = _LoginEntity.user_password;

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_LoginInfo_R10", pIDataParameter);
                return pDataTable;
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
                    "Login_Info(LoginEntity _LoginEntity)",
                    pException
                );
            }
        }

        public DataTable Login_CK(LoginEntity _LoginEntity)
        {
            try
            {
                IDataParameter[] pIDataParameter = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "SQLServer":
                        pIDataParameter = new IDataParameter[]
                        {
                          
                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {

                        };
                        break;
                }

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_LoginInfo_R10", pIDataParameter);

                return pDataTable;
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
                    "Login_Info(LoginEntity _LoginEntity)",
                    pException
                );
            }
        }

        public DataTable UserPWChg(LoginEntity _LoginEntity)
        {
            try
            {
                IDataParameter[] pIDataParameter = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "SQLServer":
                        pIDataParameter = new IDataParameter[]
                        {
                            new SqlParameter("@user_account", SqlDbType.VarChar, 50),
                            new SqlParameter("@user_newpassword", SqlDbType.VarChar, 1000)
                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {
                            new MySqlParameter("@user_account", MySqlDbType.VarChar, 50),
                            new MySqlParameter("@user_newpassword", MySqlDbType.VarChar, 1000)
                        };
                        break;
                }

                pIDataParameter[0].Value = _LoginEntity.user_account;
                pIDataParameter[1].Value = _LoginEntity.user_newpassword;

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_UserPWChg", pIDataParameter);
                return pDataTable;
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
                    "UserPWChg(LoginEntity _LoginEntity)",
                    pException
                );
            }
        }
    }
}
