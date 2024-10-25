using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CoFAS.NEW.MES.Core.Provider
{
    public class CalendarProvider : EntityManager<CalendarEntity>
    {
        public CalendarProvider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public override CalendarEntity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public DataTable Calendar_Info(CalendarEntity _CalendarEntity)
        {
            try
            {
                IDataParameter[] pIDataParameter = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "SQLServer":
                        pIDataParameter = new IDataParameter[]
                        {
                            new SqlParameter("@crud		    ".Trim(), SqlDbType.VarChar, 10),
                            new SqlParameter("@id		    ".Trim(), SqlDbType.Int),
                            new SqlParameter("@title		".Trim(), SqlDbType.VarChar,50),
                            new SqlParameter("@content	    ".Trim(), SqlDbType.Text),
                            new SqlParameter("@start_date	".Trim(), SqlDbType.VarChar,50),
                            new SqlParameter("@end_date	    ".Trim(), SqlDbType.VarChar,50),
                            new SqlParameter("@reg_date	    ".Trim(), SqlDbType.VarChar,50),
                            new SqlParameter("@reg_user	    ".Trim(), SqlDbType.VarChar,50),
                            new SqlParameter("@up_date	    ".Trim(), SqlDbType.VarChar,50),
                            new SqlParameter("@up_user	    ".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@color_R	    ".Trim(), SqlDbType.Int),
                            new SqlParameter("@color_G	    ".Trim(), SqlDbType.Int),
                            new SqlParameter("@color_B	    ".Trim(), SqlDbType.Int),
                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {
                            new MySqlParameter("@crud		    ".Trim(), MySqlDbType.VarChar, 50),
                            new MySqlParameter("@id		        ".Trim(), MySqlDbType.VarChar, 1000),
                            new MySqlParameter("@title		    ".Trim(), MySqlDbType.VarChar, 50),
                            new MySqlParameter("@content	    ".Trim(), MySqlDbType.VarChar, 1000),
                            new MySqlParameter("@start_date 	".Trim(), MySqlDbType.DateTime),
                            new MySqlParameter("@end_date	    ".Trim(), MySqlDbType.DateTime),
                            new MySqlParameter("@reg_date	    ".Trim(), MySqlDbType.DateTime),
                            new MySqlParameter("@reg_user	    ".Trim(), MySqlDbType.VarChar, 1000),
                            new MySqlParameter("@up_date	    ".Trim(), MySqlDbType.DateTime),
                            new MySqlParameter("@up_user	    ".Trim(), MySqlDbType.VarChar, 1000),
                            new MySqlParameter("@color_R	    ".Trim(), MySqlDbType.VarChar, 50),
                            new MySqlParameter("@color_G	    ".Trim(), MySqlDbType.VarChar, 1000),
                            new MySqlParameter("@color_B	    ".Trim(), MySqlDbType.VarChar, 50),      
                        };
                        break;
                }

                pIDataParameter[0].Value  = _CalendarEntity.CRUD;
                pIDataParameter[1].Value  = _CalendarEntity.id; 
                pIDataParameter[2].Value  = _CalendarEntity.title;
                pIDataParameter[3].Value  = _CalendarEntity.content;
                pIDataParameter[4].Value  = _CalendarEntity.start_date.ToString("yyyy-MM-dd HH:mm:ss:ff");
                pIDataParameter[5].Value  = _CalendarEntity.end_date.ToString("yyyy-MM-dd HH:mm:ss:ff"); 
                pIDataParameter[6].Value  = _CalendarEntity.reg_date.ToString("yyyy-MM-dd HH:mm:ss:ff"); 
                pIDataParameter[7].Value  = _CalendarEntity.reg_user;
                pIDataParameter[8].Value  = _CalendarEntity.up_date.ToString("yyyy-MM-dd HH:mm:ss:ff"); 
                pIDataParameter[9].Value  = _CalendarEntity.up_user;
                pIDataParameter[10].Value = _CalendarEntity.color_R;
                pIDataParameter[11].Value = _CalendarEntity.color_G;
                pIDataParameter[12].Value = _CalendarEntity.color_B;

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_CalendarInfo_R10", pIDataParameter);
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
                    "Calendar_Info(CalendarEntity _CalendarEntity)",
                    pException
                );
            }
        }

        public DataTable Calendar_Info_R2(CalendarEntity _CalendarEntity)
        {
            try
            {
                IDataParameter[] pIDataParameter = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "SQLServer":
                        pIDataParameter = new IDataParameter[]
                        {

                            new SqlParameter("@id		    ".Trim(), SqlDbType.Int),

                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {
                            new MySqlParameter("@id		        ".Trim(), MySqlDbType.VarChar, 1000),                         
                        };
                        break;
                }

                pIDataParameter[0].Value = _CalendarEntity.id;
   

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_CalendarInfo_R20", pIDataParameter);
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
                    "Calendar_Info(CalendarEntity _CalendarEntity)",
                    pException
                );
            }
        }

        public DataTable Calendar_Order_Info(string  _Datetime)
        {
            try
            {
                IDataParameter[] pIDataParameter = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "SQLServer":
                        pIDataParameter = new IDataParameter[]
                        {
              
                            new SqlParameter("@Datetime	    ".Trim(), SqlDbType.VarChar,50),
               
                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {
                            new MySqlParameter("@Datetime		    ".Trim(), MySqlDbType.VarChar, 50),
                            
                        };
                        break;
                }

                pIDataParameter[0].Value = _Datetime;


                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_Calendar_Order_Info_R10", pIDataParameter);
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
                    "Calendar_Info(CalendarEntity _CalendarEntity)",
                    pException
                );
            }
        }
    }
}
