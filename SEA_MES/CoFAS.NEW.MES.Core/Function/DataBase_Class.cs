using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoFAS.NEW.MES.Core.Function
{
    public class DataBase_Class
    {
        private readonly IDataBase _database;
        public DataBase_Class(IDataBase database)
        {
            _database = database;
        }
        public DataSet GetAllData(string query)
        {
            return _database.GetDataSet(query);
        }

        // 데이터 삽입
        public int ExecuteData(string query)
        {
            return _database.ExecuteNonQuery(query);
        }
        public object ExecuteScalar(string query)
        {
            return _database.ExecuteScalar(query);
        }

        // 트랜잭션을 사용한 작업 예시
        public void ExecuteWithTransaction(string[] insertQueries)
        {
            using (var transaction = _database.BeginTransaction())
            {
                try
                {
                    foreach (var query in insertQueries)
                    {
                        _database.ExecuteNonQuery(query);
                    }
                    _database.Commit(transaction);
                }
                catch
                {
                    _database.Rollback(transaction);
                    throw; // 예외를 다시 던져서 호출자에게 알림
                }
            }
        }
        public IDbTransaction TranscationStart()
        {
            return _database.BeginTransaction();
        }
        public void CommitTransaction(IDbTransaction transaction)
        {
            _database.Commit(transaction); // 트랜잭션 커밋
        }

        public void RollbackTransaction(IDbTransaction transaction)
        {
            _database.Rollback(transaction); // 트랜잭션 롤백
        }
    }
}
