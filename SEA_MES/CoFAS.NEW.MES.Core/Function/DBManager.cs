using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

using System.Data.OleDb;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core.Function
{
    public enum DBManagerType
    {
        SQLServer,
        Oracle,
        MySql,
        OleDB
    }

    public class DBManager : IDisposable
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Static
        //////////////////////////////////////////////////////////////////////////////// Private

        #region Field

        /// <summary>
        /// 기본 데이타베이스 관리자 종류
        /// </summary>
        private static DBManagerType _pPrimaryDBManagerType;

        /// <summary>
        /// 기본 연결 문자열
        /// </summary>
        private static string _pPrimaryConnectionString;

        /// <summary>
        /// SQL 서버 매개 변수 형식
        /// </summary>
        private static string _strSQLServerParameterFormat = "@P{0}";

        /// <summary>
        /// MY SQL 매개 변수 형식
        /// </summary>
        private static string _strMySQLParameterFormat = "@P{0}";

        /// <summary>
        /// 오라클 매개 변수 형식
        /// </summary>
        private static string _strOracleParameterFormat = ":P{0}";

        /// <summary>
        /// OLE DB 매개 변수 형식
        /// </summary>
        private static string _strOLEDBParameterFormat = "@P{0}";

        private static string _strDatabaseServer = "127.0.0.1";
        private static string _strDatabaseName = "Coever";
        private static string _strDatabaseID = "sa";
        private static string _strDatabasePass = "";

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////// Instance
        //////////////////////////////////////////////////////////////////////////////// Private

        #region Field

        /// <summary>
        /// 데이타베이스 관리자 종류
        /// </summary>
        private DBManagerType _pDBManagerType;

        /// <summary>
        /// 연결 문자열
        /// </summary>
        private string _pConnectionString;

        /// <summary>
        /// 데이타베이스 연결
        /// </summary>
        private DbConnection _pDbConnection;

        /// <summary>
        /// 데이타베이스 트랜잭션
        /// </summary>
        private DbTransaction _pDbTransaction;

        /// <summary>
        /// 명령 시간 제한
        /// </summary>
        private int _nCommandTimeout;

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Proiperty
        ////////////////////////////////////////////////////////////////////////////////////////// Static
        //////////////////////////////////////////////////////////////////////////////// Public

        #region 기본 데이타베이스 관리자 종류 - PrimaryDBManagerType

        /// <summary>
        /// 기본 데이타베이스 관리자 종류
        /// </summary>
        public static DBManagerType PrimaryDBManagerType
        {
            get
            {
                return _pPrimaryDBManagerType;
            }
            set
            {
                _pPrimaryDBManagerType = value;
            }
        }

        #endregion

        #region 기본 연결 문자열 - PrimaryConnectionString

        /// <summary>
        /// 기본 연결 문자열
        /// </summary>
        public static string PrimaryConnectionString
        {
            get
            {
                return _pPrimaryConnectionString;
            }
            set
            {
                _pPrimaryConnectionString = value;
            }
        }





        #endregion

        #region SQL 서버 파라미터 형식 - SQLServerParameterFormat

        /// <summary>
        /// SQL 서버 파라미터 형식
        /// </summary>
        public static string SQLServerParameterFormat
        {
            get
            {
                return _strSQLServerParameterFormat;
            }
            set
            {
                _strSQLServerParameterFormat = value;
            }
        }

        #endregion

        #region 오라클 파라미터 형식 - OracleParameterFormat

        /// <summary>
        /// 오라클 파라미터 형식
        /// </summary>
        public static string OracleParameterFormat
        {
            get
            {
                return _strOracleParameterFormat;
            }
            set
            {
                _strOracleParameterFormat = value;
            }
        }

        #endregion

        #region MySQL 파라미터 형식 - MySQLParameterFormat

        /// <summary>
        /// SQL 서버 파라미터 형식
        /// </summary>
        public static string MySQLParameterFormat
        {
            get
            {
                return _strMySQLParameterFormat;
            }
            set
            {
                _strMySQLParameterFormat = value;
            }
        }

        #endregion

        #region OLE DB 파라미터 형식 - OLEDBParameterFormat

        /// <summary>
        /// OLE DB 파라미터 형식
        /// </summary>
        public static string OLEDBParameterFormat
        {
            get
            {
                return _strOLEDBParameterFormat;
            }
            set
            {
                _strOLEDBParameterFormat = value;
            }
        }

        #endregion


        public static string InitDatabaseServer
        {
            get
            {
                return _strDatabaseServer;
            }
            set
            {
                _strDatabaseServer = value;
            }
        }

        public static string InitDatabaseName
        {
            get
            {
                return _strDatabaseName;
            }
            set
            {
                _strDatabaseName = value;
            }
        }

        public static string InitDatabaseID
        {
            get
            {
                return _strDatabaseID;
            }
            set
            {
                _strDatabaseID = value;
            }
        }

        public static string InitDatabasePass
        {
            get
            {
                return _strDatabasePass;
            }
            set
            {
                _strDatabasePass = value;
            }
        }



        ////////////////////////////////////////////////////////////////////////////////////////// Instance
        //////////////////////////////////////////////////////////////////////////////// Public

        #region 데이타베이스 관리자 종류 - DBManagerType

        /// <summary>
        /// 데이타베이스 관리자 종류
        /// </summary>
        public DBManagerType DBManagerType
        {
            get
            {
                return _pDBManagerType;
            }
        }

        #endregion

        #region 연결 문자열 - ConnectionString

        /// <summary>
        /// 연결 문자열
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _pConnectionString;
            }
        }

        #endregion

        #region 데이타베이스 연결 - DbConnection

        /// <summary>
        /// 데이타베이스 연결
        /// </summary>
        public DbConnection DbConnection
        {
            get
            {
                return _pDbConnection;
            }
        }

        #endregion

        #region 데이타베이스 트랜잭션 - DbTransaction

        /// <summary>
        /// 데이타베이스 트랜잭션
        /// </summary>
        public DbTransaction DbTransaction
        {
            get
            {
                return _pDbTransaction;
            }
        }

        #endregion

        #region 명령 시간 제한 - CommandTimeout

        /// <summary>
        /// 명령 시간 제한
        /// </summary>
        public int CommandTimeout
        {
            get
            {
                return _nCommandTimeout;
            }
            set
            {
                _nCommandTimeout = value;
            }
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Constructor
        ////////////////////////////////////////////////////////////////////////////////////////// Static

        #region 생성자 - DBManager()

        /// <summary>
        /// 생성자
        /// </summary>
        static DBManager()
        {
            try
            {
                string strPrimaryDBManagerType = ConfigurationManager.GetApplicationSetting("DBManagerType");

                switch (strPrimaryDBManagerType)
                {
                    case "SQLServer": _pPrimaryDBManagerType = DBManagerType.SQLServer; break;
                    case "Oracle": _pPrimaryDBManagerType = DBManagerType.Oracle; break;
                    case "MySql": _pPrimaryDBManagerType = DBManagerType.MySql; break;
                    default: _pPrimaryDBManagerType = DBManagerType.OleDB; break;
                }

                _pPrimaryConnectionString = ConfigurationManager.GetApplicationSetting("ConnectionString");
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    typeof(DBManager),
                    "DBManager()",
                    pException
                );
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 생성자 - DBManager(pDBManagerType, pConnectionString)

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="pDBManagerType">데이타베이스 관리자 종류</param>
        /// <param name="pConnectionString">연결 문자열</param>
        public DBManager(DBManagerType pDBManagerType, string pConnectionString)
        {
            try
            {
                _pDBManagerType = pDBManagerType;
                _pConnectionString = pConnectionString;
                _pDbConnection = GetDbConnection(pDBManagerType, pConnectionString);
                _pDbTransaction = null;
                _nCommandTimeout = 300;

                if (_pDbConnection.State == ConnectionState.Open)
                {
                    return;
                }

                _pDbConnection.Open();
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
                    "DBManager(pDBManagerType, pConnectionString)",
                    pException
                );
            }
        }

        #endregion

        #region 생성자 - DBManager()

        /// <summary>
        /// 생성자
        /// </summary>
        public DBManager()
            : this(_pPrimaryDBManagerType, _pPrimaryConnectionString)
        {
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Destructor

        #region 소멸자 - ~DBManager()

        /// <summary>
        /// 소멸자
        /// </summary>
        ~DBManager()
        {
            try
            {
                Dispose();
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
                    "~DBManager()",
                    pException
                );
            }
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 트랜잭션 시작하기 - BeginTransaction(pIsolationLevel)

        /// <summary>
        /// 트랜잭션 시작하기
        /// </summary>
        /// <param name="pIsolationLevel">격리 수준</param>
        public void BeginTransaction(IsolationLevel pIsolationLevel)
        {
            try
            {
                _pDbTransaction = _pDbConnection.BeginTransaction(pIsolationLevel);
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "BeginTransaction(pIsolationLevel)",
                    pException
                );
            }
        }

        #endregion

        #region 트랜잭션 시작하기 - BeginTransaction()

        /// <summary>
        /// 트랜잭션 시작하기
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                BeginTransaction(IsolationLevel.Serializable);//.ReadCommitted);
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
                    "BeginTransaction()",
                    pException
                );
            }
        }

        #endregion

        #region 실행하기 - Execute(pCommandType, strCommandText, pDataParameters)

        /// <summary>
        /// 실행하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pDataParameters">데이타 매개 변수 목록</param>
        /// <returns>적용 행 수</returns>
        public int Execute(CommandType pCommandType, string strCommandText, IDataParameter[] pDataParameters)
        {
            try
            {
                return GetDbCommand(_pDBManagerType, _nCommandTimeout, pCommandType, strCommandText, pDataParameters).ExecuteNonQuery();
            }
            catch (ExceptionManager pExceptionManager)
            {
                CustomMsg.ShowExceptionMessage(pExceptionManager.Message.ToString(), "Error", MessageBoxButtons.OK);
                return 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Check for specific error number for duplicate key violation
                {
                    CustomMsg.ShowExceptionMessage(ex.Message.ToString(), "Error", MessageBoxButtons.OK);
                    throw new ExceptionManager
                 (
                     this,
                     ex.Message,
                     ex
                 );
                }
                else
                {
                    CustomMsg.ShowExceptionMessage(ex.Message.ToString(), "Error", MessageBoxButtons.OK);
                }
                return 0;
            }           
            catch (Exception pException)
            {
                CustomMsg.ShowExceptionMessage(pException.Message.ToString(), "Error", MessageBoxButtons.OK);
                return 0;
            }
        
        }

        #endregion

        #region 실행하기 - Execute2(pCommandType, strCommandText, pDataParameters)

        /// <summary>
        /// 실행하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pDataParameters">데이타 매개 변수 목록</param>
        /// <returns>적용 행 수</returns>
        public int Execute2(CommandType pCommandType, string strCommandText)
        {
            try
            {
                return GetDbCommand(_pDBManagerType, _nCommandTimeout, pCommandType, strCommandText).ExecuteNonQuery();
            }
            catch (ExceptionManager pExceptionManager)
            {
                throw pExceptionManager;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Check for specific error number for duplicate key violation
                {
                    throw new ExceptionManager
                 (
                     this,
                     ex.Message,
                     ex
                 );
                }
                else
                {
                    // Handle other SQL exceptions
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                return 0;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "Execute(pCommandType, strCommandText, pDataParameters)",
                    pException
                );
            }
        }

        #endregion

        #region 실행하기 - Execute(pCommandType, strCommandText, ...)

        /// <summary>
        /// 실행하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pArguments">인자 목록</param>
        /// <returns>적용 행 수</returns>
        public int Execute(CommandType pCommandType, string strCommandText, params object[] pArguments)
        {
            try
            {
                return Execute(pCommandType, strCommandText, GetDataParameters(_pDBManagerType, pArguments));
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
                    "Execute(pCommandType, strCommandText, ...)",
                    pException
                );
            }
        }

        #endregion

        #region 스칼라 구하기 - GetScalar(pCommandType, strCommandText, pDataParameters)

        /// <summary>
        /// 스칼라 구하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pDataParameters">데이타 매개 변수 목록</param>
        /// <returns>스칼라</returns>
        public object GetScalar(CommandType pCommandType, string strCommandText, IDataParameter[] pDataParameters)
        {
            try
            {
                return GetDbCommand(_pDBManagerType, _nCommandTimeout, pCommandType, strCommandText, pDataParameters).ExecuteScalar();
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
                    "GetScalar(pCommandType, strCommandText, pDataParameters)",
                    pException
                );
            }
        }

        #endregion

        #region 스칼라 구하기 - GetScalar(pCommandType, strCommandText, ...)

        /// <summary>
        /// 스칼라 구하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pArguments">인자 목록</param>
        /// <returns>스칼라</returns>
        public object GetScalar(CommandType pCommandType, string strCommandText, params object[] pArguments)
        {
            try
            {
                return GetScalar(pCommandType, strCommandText, GetDataParameters(_pDBManagerType, pArguments));
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
                    "GetScalar(pCommandType, strCommandText, ...)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타베이스 데이타 리더기 구하기 - GetDbDataReader(pCommandType, strCommandText, pDataParameters)

        /// <summary>
        /// 데이타베이스 데이타 리더기 구하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pDataParameters">데이타 매개 변수 목록</param>
        /// <returns>데이타베이스 데이타 리더기</returns>
        public DbDataReader GetDbDataReader(CommandType pCommandType, string strCommandText, IDataParameter[] pDataParameters)
        {
            try
            {
                return GetDbCommand(_pDBManagerType, _nCommandTimeout, pCommandType, strCommandText, pDataParameters).ExecuteReader();
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
                    "GetDbDataReader(pCommandType, strCommandText, pDataParameters)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타베이스 데이타 리더기 구하기 - GetDbDataReader(pCommandType, strCommandText, ...)

        /// <summary>
        /// 데이타베이스 데이타 리더기 구하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pArguments">인자 목록</param>
        /// <returns>데이타베이스 데이타 리더기</returns>
        public DbDataReader GetDbDataReader(CommandType pCommandType, string strCommandText, params object[] pArguments)
        {
            try
            {
                return GetDbDataReader(pCommandType, strCommandText, GetDataParameters(_pDBManagerType, pArguments));
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
                    "GetDbDataReader(pCommandType, strCommandText, ...)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타 테이블 구하기 - GetDataTable(pCommandType, strCommandText)

        /// <summary>
        /// 데이타 테이블 구하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <returns>데이타 셋</returns>
        public DataTable GetDataTable(CommandType pCommandType, string strCommandText)
        {
            try
            {
                DbDataAdapter pDbDataAdapter = GetDbDataAdapter(_pDBManagerType);
                pDbDataAdapter.SelectCommand = GetDbCommand(_pDBManagerType, _nCommandTimeout, pCommandType, strCommandText);
                DataTable pDataTable = new DataTable();
                pDbDataAdapter.Fill(pDataTable);
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
                    "GetDataTable(pCommandType, strCommandText)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타 테이블 구하기 - GetDataTable(pCommandType, strCommandText, pDataParameters)

        /// <summary>
        /// 데이타 테이블 구하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pDataParameters">데이타 매개 변수 목록</param>
        /// <returns>데이타 셋</returns>
        public DataTable GetDataTable(CommandType pCommandType, string strCommandText, IDataParameter[] pDataParameters)
        {
            try
            {
                DbDataAdapter pDbDataAdapter = GetDbDataAdapter(_pDBManagerType);
                pDbDataAdapter.SelectCommand = GetDbCommand(_pDBManagerType, _nCommandTimeout, pCommandType, strCommandText, pDataParameters);
                DataTable pDataTable = new DataTable();

                pDbDataAdapter.Fill(pDataTable);
                return pDataTable;
            }
            catch (Exception pException)            
            {
                throw new ExceptionManager
                (
                    this,
                    "GetDataTable(pCommandType, strCommandText, pDataParameters)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타 테이블 구하기 - GetDataTable(pCommandType, strCommandText, ...)

        /// <summary>
        /// 데이타 테이블 구하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pArguments">인자 목록</param>
        /// <returns>데이타 테이블</returns>
        public DataTable GetDataTable(CommandType pCommandType, string strCommandText, params object[] pArguments)
        {
            try
            {
                return GetDataTable(pCommandType, strCommandText, GetDataParameters(_pDBManagerType, pArguments));
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
                    "GetDataTable(pCommandType, strCommandText, ...)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타 셋 구하기 - GetDataSet(pCommandType, strCommandText, pDataParameters)

        /// <summary>
        /// 데이타 셋 구하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pDataParameters">데이타 매개 변수 목록</param>
        /// <returns>데이타 셋</returns>
        public DataSet GetDataSet(CommandType pCommandType, string strCommandText, IDataParameter[] pDataParameters)
        {
            try
            {
                DbDataAdapter pDbDataAdapter = GetDbDataAdapter(_pDBManagerType);

                pDbDataAdapter.SelectCommand = GetDbCommand(_pDBManagerType, _nCommandTimeout, pCommandType, strCommandText, pDataParameters);

                DataSet pDataSet = new DataSet();

                pDbDataAdapter.Fill(pDataSet);

                return pDataSet;
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
                    "GetDataSet(pCommandType, strCommandText, pDataParameters)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타 셋 구하기 - GetDataSet(pCommandType, strCommandText, ...)

        /// <summary>
        /// 데이타 셋 구하기
        /// </summary>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pArguments">인자 목록</param>
        /// <returns>데이타 셋</returns>
        public DataSet GetDataSet(CommandType pCommandType, string strCommandText, params object[] pArguments)
        {
            try
            {
                return GetDataSet(pCommandType, strCommandText, GetDataParameters(_pDBManagerType, pArguments));
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
                    "GetDataSet(pCommandType, strCommandText, ...)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타 셋 채우기 - FillDataSet(pDataSet, pCommandType, strCommandText, pDataParameters)

        /// <summary>
        /// 데이타 셋 채우기
        /// </summary>
        /// <param name="pDataSet">데이타 셋</param>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pDataParameters">데이타 매개 변수 목록</param>
        public void FillDataSet(DataSet pDataSet, CommandType pCommandType, string strCommandText, IDataParameter[] pDataParameters)
        {
            try
            {
                DbDataAdapter pDbDataAdapter = GetDbDataAdapter(_pDBManagerType);

                pDbDataAdapter.SelectCommand = GetDbCommand(_pDBManagerType, _nCommandTimeout, pCommandType, strCommandText, pDataParameters);

                pDbDataAdapter.Fill(pDataSet);
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
                    "FillDataSet(pDataSet, pCommandType, strCommandText, pDataParameters)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타 셋 채우기 - FillDataSet(pDataSet, pCommandType, strCommandText, ...)

        /// <summary>
        /// 데이타 셋 채우기
        /// </summary>
        /// <param name="pDataSet">데이타 셋</param>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pArguments">인자 목록</param>
        public void FillDataSet(DataSet pDataSet, CommandType pCommandType, string strCommandText, params object[] pArguments)
        {
            try
            {
                FillDataSet(pDataSet, pCommandType, strCommandText, GetDataParameters(_pDBManagerType, pArguments));
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
                    "FillDataSet(pDataSet, pCommandType, strCommandText, ...)",
                    pException
                );
            }
        }

        #endregion

        #region 트랜잭션 완료하기 - CommitTransaction()

        /// <summary>
        /// 트랜잭션 완료하기
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                _pDbTransaction.Commit();
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "CommitTransaction()",
                    pException
                );
            }
        }

        #endregion

        #region 트랜잭션 취소하기 - RollbackTransaction()

        /// <summary>
        /// 트랜잭션 취소하기
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                _pDbTransaction.Rollback();
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "RollbackTransaction()",
                    pException
                );
            }
        }

        #endregion

        #region 닫기 - Close()

        /// <summary>
        /// 닫기
        /// </summary>
        public void Close()
        {
            try
            {
                if (_pDbTransaction != null)
                {
                    _pDbTransaction.Dispose();

                    _pDbTransaction = null;
                }

                if (_pDbConnection != null && _pDbConnection.State != ConnectionState.Closed)
                {
                    _pDbConnection.Close();
                    _pDbConnection.Dispose();

                    _pDbConnection = null;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "Close()",
                    pException
                );
            }
        }

        #endregion

        #region 해제하기 - Dipose()

        /// <summary>
        /// 해제하기
        /// </summary>
        public void Dispose()
        {
            try
            {
                Close();
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
                    "Close()",
                    pException
                );
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////// Private

        #region 데이타베이스 연결 구하기 - GetDbConnection(pDBManagerType, pConnectionString)

        /// <summary>
        /// 데이타베이스 연결 구하기
        /// </summary>
        /// <param name="pDBManagerType">데이타베이스 관리자 종류</param>
        /// <param name="pConnectionString">연결 문자열</param>
        /// <returns>데이타베이스 연결</returns>
        private DbConnection GetDbConnection(DBManagerType pDBManagerType, string pConnectionString)
        {
            try
            {
                switch (pDBManagerType)
                {
                    case DBManagerType.SQLServer: return new SqlConnection(pConnectionString);
                   // case DBManagerType.Oracle: return new OracleConnection(pConnectionString);
                    case DBManagerType.MySql: return new MySqlConnection(pConnectionString);
                    default: return new OleDbConnection(pConnectionString);
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetDbConnection(pDBManagerType, pConnectionString)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타베이스 매개 변수 구하기 - GetDbParameter(pDBManagerType)

        /// <summary>
        /// 데이타베이스 매개 변수 구하기
        /// </summary>
        /// <param name="pDBManagerType">데이타베이스 관리자 종류</param>
        /// <returns>데이타베이스 매개 변수</returns>
        private DbParameter GetDbParameter(DBManagerType pDBManagerType)
        {
            try
            {
                switch (pDBManagerType)
                {
                    case DBManagerType.SQLServer: return new SqlParameter();
                    //case DBManagerType.Oracle: return new OracleParameter();
                    case DBManagerType.MySql: return new MySqlParameter();
                    case DBManagerType.OleDB: return new OleDbParameter();
                    default: return null;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetDbParameter(pDBManagerType)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타베이스 매개 변수 구하기 - GetDbParameter(pDBManagerType, strParameterName, pParameterDirection, pDataType, nDataSize, byDataScale)

        /// <summary>
        /// 데이타베이스 매개 변수 구하기
        /// </summary>
        /// <param name="pDBManagerType">데이타베이스 관리자 종류</param>
        /// <param name="strParameterName">매개 변수명</param>
        /// <param name="pParameterDirection">매개 변수 방향</param>
        /// <param name="pDataType">데이타 종류</param>
        /// <param name="nDataSize">데이타 크기</param>
        /// <param name="byDataScale">데이타 정밀도</param>
        /// <returns>데이타베이스 매개 변수</returns>
        private DbParameter GetDbParameter(DBManagerType pDBManagerType, string strParameterName, ParameterDirection pParameterDirection, DbType pDataType, int nDataSize, byte byDataScale)
        {
            try
            {
                DbParameter pDbParameter;

                switch (pDBManagerType)
                {
                    case DBManagerType.SQLServer: pDbParameter = new SqlParameter(); break;
                    //case DBManagerType.Oracle: pDbParameter = new OracleParameter(); break;
                    case DBManagerType.MySql: pDbParameter = new MySqlParameter(); break;
                    case DBManagerType.OleDB: pDbParameter = new OleDbParameter(); break;
                    default: return null;
                }

                pDbParameter.ParameterName = strParameterName;
                pDbParameter.Direction = pParameterDirection;
                pDbParameter.DbType = pDataType;
                pDbParameter.Size = nDataSize;

                if (pDbParameter.GetType() == typeof(SqlParameter))
                {
                    (pDbParameter as SqlParameter).Scale = byDataScale;
                }

                return pDbParameter;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetDbParameter(strParameterName, pParameterDirection, pDataType, nDataSize, byDataScale)",
                    pException
                );
            }
        }

        #endregion

        #region 반환 데이타베이스 매개 변수 구하기 - GetReturnDbParameter(pDBManagerType, pDataType, nDataSize, byDataScale)

        /// <summary>
        /// 반환 데이타베이스 매개 변수 구하기
        /// </summary>
        /// <param name="pDBManagerType">데이타베이스 관리자 종류</param>
        /// <param name="pDataType">데이타 종류</param>
        /// <param name="nDataSize">데이타 크기</param>
        /// <param name="byDataScale">데이타 정밀도</param>
        /// <returns>데이타베이스 매개 변수</returns>
        private DbParameter GetReturnDbParameter(DBManagerType pDBManagerType, DbType pDataType, int nDataSize, byte byDataScale)
        {
            try
            {
                return GetDbParameter(pDBManagerType, "ReturnValue", ParameterDirection.ReturnValue, pDataType, nDataSize, byDataScale);
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
                    "GetReturnDbParameter(pDBManagerType, pDataType, nDataSize, byDataScale)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타 매개 변수 목록 설정하기 - GetDataParameters(pDBManagerType, pArguments)

        /// <summary>
        /// 데이타 매개 변수 목록 설정하기
        /// </summary>
        /// <param name="pDBManagerType">데이타베이스 관리자 종류</param>
        /// <param name="pArguments">인자 목록</param>
        /// <returns>데이타 매개 변수 목록</returns>
        private IDataParameter[] GetDataParameters(DBManagerType pDBManagerType, object[] pArguments)
        {
            try
            {
                IDataParameter[] pDataParameters;

                switch (pDBManagerType)
                {
                    case DBManagerType.SQLServer: pDataParameters = new SqlParameter[pArguments.Length]; break;
                    //case DBManagerType.Oracle: pDataParameters = new OracleParameter[pArguments.Length]; break;
                    case DBManagerType.MySql: pDataParameters = new MySqlParameter[pArguments.Length]; break;
                    case DBManagerType.OleDB: pDataParameters = new OleDbParameter[pArguments.Length]; break;
                    default: return null;
                }

                for (int i = 0; i < pArguments.Length; i++)
                {
                    pDataParameters[i] = GetDbParameter(pDBManagerType);

                    switch (pDBManagerType)
                    {
                        case DBManagerType.SQLServer: pDataParameters[i].ParameterName = string.Format(_strSQLServerParameterFormat, i); break;
                        case DBManagerType.Oracle: pDataParameters[i].ParameterName = string.Format(_strOracleParameterFormat, i); break;
                        case DBManagerType.MySql: pDataParameters[i].ParameterName = string.Format(_strMySQLParameterFormat, i); break;
                        case DBManagerType.OleDB: pDataParameters[i].ParameterName = string.Format(_strOLEDBParameterFormat, i); break;
                        default: pDataParameters[i].ParameterName = string.Format(_strSQLServerParameterFormat, i); break;
                    }

                    pDataParameters[i].Value = pArguments[i];
                }

                return pDataParameters;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetDataParameterArray(pDBManagerType, pArguments)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타베이스 명령 구하기 - GetDbCommand(pDBManagerType, nCommandTimeout, pCommandType, strCommandText)

        /// <summary>
        /// 데이타베이스 명령 구하기
        /// </summary>
        /// <param name="pDBManagerType">데이타베이스 관리자 종류</param>
        /// <param name="nCommandTimeout">명령 시간 제한</param>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <returns>데이타베이스 명령</returns>
        private DbCommand GetDbCommand(DBManagerType pDBManagerType, int nCommandTimeout, CommandType pCommandType, string strCommandText)
        {
            try
            {
                DbCommand pDbCommand;
                switch (pDBManagerType)
                {
                    case DBManagerType.SQLServer: pDbCommand = new SqlCommand(); break;
                   // case DBManagerType.Oracle: pDbCommand = new OracleCommand(); break;
                    case DBManagerType.MySql: pDbCommand = new MySqlCommand(); break;
                    case DBManagerType.OleDB: pDbCommand = new OleDbCommand(); break;
                    default: return null;
                }

                pDbCommand.Connection = _pDbConnection;
                pDbCommand.CommandType = pCommandType;
                pDbCommand.CommandText = strCommandText;
                pDbCommand.CommandTimeout = nCommandTimeout;

                if (_pDbTransaction != null)
                {
                    pDbCommand.Transaction = _pDbTransaction;
                }
                return pDbCommand;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetDbCommand(pDBManagerType, nCommandTimeout, pCommandType, strCommandText, pDataParameters)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타베이스 명령 구하기 - GetDbCommand(pDBManagerType, nCommandTimeout, pCommandType, strCommandText, pDataParameters)

        /// <summary>
        /// 데이타베이스 명령 구하기
        /// </summary>
        /// <param name="pDBManagerType">데이타베이스 관리자 종류</param>
        /// <param name="nCommandTimeout">명령 시간 제한</param>
        /// <param name="pCommandType">명령 종류</param>
        /// <param name="strCommandText">명령문</param>
        /// <param name="pDataParameters">데이타 매개 변수 목록</param>
        /// <returns>데이타베이스 명령</returns>
        private DbCommand GetDbCommand(DBManagerType pDBManagerType, int nCommandTimeout, CommandType pCommandType, string strCommandText, IDataParameter[] pDataParameters)
        {
            try
            {
                DbCommand pDbCommand;

                switch (pDBManagerType)
                {
                    case DBManagerType.SQLServer: pDbCommand = new SqlCommand(); break;
                    //case DBManagerType.Oracle: pDbCommand = new OracleCommand(); break;
                    case DBManagerType.MySql: pDbCommand = new MySqlCommand(); break;
                    case DBManagerType.OleDB: pDbCommand = new OleDbCommand(); break;
                    default: return null;
                }

                pDbCommand.Connection = _pDbConnection;
                pDbCommand.CommandType = pCommandType;
                pDbCommand.CommandText = strCommandText;
                pDbCommand.CommandTimeout = nCommandTimeout;

                if (pDataParameters != null)
                {
                    foreach (DbParameter pDbParameter in pDataParameters)
                    {
                        pDbCommand.Parameters.Add(pDbParameter);
                    }
                }

                if (_pDbTransaction != null)
                {
                    pDbCommand.Transaction = _pDbTransaction;
                }

                return pDbCommand;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetDbCommand(pDBManagerType, nCommandTimeout, pCommandType, strCommandText, pDataParameters)",
                    pException
                );
            }
        }

        #endregion

        #region 데이타베이스 데이타 어댑터 구하기 - GetDbDataAdapter(pDBManagerType)

        /// <summary>
        /// 데이타베이스 데이타 어댑터 구하기
        /// </summary>
        /// <param name="pDBManagerType">데이타베이스 관리자 종류</param>
        /// <returns>데이타베이스 데이타 어댑터</returns>
        private DbDataAdapter GetDbDataAdapter(DBManagerType pDBManagerType)
        {
            try
            {
                switch (pDBManagerType)
                {
                    case DBManagerType.SQLServer: return new SqlDataAdapter();
                   // case DBManagerType.Oracle: return new OracleDataAdapter();
                    case DBManagerType.MySql: return new MySqlDataAdapter();
                    case DBManagerType.OleDB: return new OleDbDataAdapter();
                    default: return null;
                }
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetDbDataAdapter(pDBManagerType)",
                    pException
                );
            }
        }

        #endregion
    }
}
