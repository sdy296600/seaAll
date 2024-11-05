﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CoFAS.NEW.MES.Core.Function
{
    public interface IDataBase
    {
        void Connection(); // 연결 설정
        void Disconnect(); // 연결 해제
        bool IsConnected(); // 연결 상태 확인
        DataSet GetDataSet(string query); // 특정 쿼리를 통해 DataTable 가져오기
        int ExecuteNonQuery(string query); // INSERT, UPDATE, DELETE 등의 명령어 실행
        object ExecuteScalar(string query); // 단일 값 반환 쿼리 실행
        IDbTransaction BeginTransaction(); // 트랜잭션 시작
        void Commit(IDbTransaction transaction); // 트랜잭션 커밋
        void Rollback(IDbTransaction transaction); // 트랜잭션 롤백
    }
}