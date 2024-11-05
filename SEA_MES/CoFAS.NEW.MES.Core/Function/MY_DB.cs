using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace CoFAS.NEW.MES.Core.Function
{
    internal class MY_DB : IDataBase
    {
        public string connStr;
        MySqlConnection conn;
        public MY_DB(string connStr)
        {
            this.connStr = connStr;
            this.conn = new MySqlConnection(connStr);
        }

        public IDbTransaction BeginTransaction()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return conn.BeginTransaction();
        }

        public void Commit(IDbTransaction transaction)
        {
            if (transaction is MySqlTransaction sqlTransaction)
            {
                sqlTransaction.Commit();
            }
        }

        public void Connection()
        {
            if (connStr != null && connStr != string.Empty)
            {
                conn.Open();
            }
        }

        public void Disconnect()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public int ExecuteNonQuery(string query)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // 예외 처리 (예: 로깅)
                Console.WriteLine($"Error executing non-query: {ex.Message}");
                return -1; // 실패 시 -1 반환
            }
            finally
            {
                Disconnect(); // 작업 후 연결 해제
            }
        }

        public object ExecuteScalar(string query)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                // 예외 처리 (예: 로깅)
                Console.WriteLine($"Error executing scalar: {ex.Message}");
                return null; // 실패 시 null 반환
            }
            finally
            {
                Disconnect(); // 작업 후 연결 해제
            }
        }

        public DataSet GetDataSet(string query)
        {
            try
            {
                DataSet ds = new DataSet();
                MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool IsConnected()
        {
            return conn != null && conn.State == ConnectionState.Open;

        }

        public void Rollback(IDbTransaction transaction)
        {
            if (transaction is MySqlTransaction sqlTransaction)
            {
                sqlTransaction.Rollback();
            }
        }
    }
}
