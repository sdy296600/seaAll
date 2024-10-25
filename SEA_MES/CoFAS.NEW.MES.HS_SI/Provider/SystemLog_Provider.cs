
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.HS_SI.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoFAS.NEW.MES.HS_SI.Provider
{
public class SystemLog_Provider: EntityManager<SystemLog_Provider>
    {

        public override SystemLog_Provider GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public SystemLog_Provider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public DataTable SystemLogStatus_R10()
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[] { };
                        break;

                    case "SQLServer":
                        pDataParameters = new IDataParameter[] { };
                        break;
                }

                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_SystemLogStatus_R10", pDataParameters);
                return _DataTable;
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {

                throw new ExceptionManager
                (
                    this,
                    "DataTable SystemLogStatus_R10()",
                    _Exception
                );
            }
        }

        public DataTable SystemLogStatus_R20(SystemLog_Entity _Entity)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[] { };
                        break;

                    case "SQLServer":
                        pDataParameters = new IDataParameter[]
                        {
                            new SqlParameter("@startDate    ".Trim(), SqlDbType.VarChar, 10),
                            new SqlParameter("@endDate      ".Trim(), SqlDbType.VarChar, 10),
                            new SqlParameter("@searchName   ".Trim(), SqlDbType.VarChar, 50),
                        };
                        break;
                }

                pDataParameters[0].Value = _Entity._SearchStart;
                pDataParameters[1].Value = _Entity._SearchEnd;
                pDataParameters[2].Value = _Entity._UserName;

                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_SystemLogStatus_R20", pDataParameters);
                return _DataTable;
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {

                throw new ExceptionManager
                (
                    this,
                    "DataTable SystemLogStatus_R10()",
                    _Exception
                );
            }
        }
    }
}
