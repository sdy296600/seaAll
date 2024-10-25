using CoFAS.NEW.MES.Core.Function;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoFAS.NEW.MES.Core.Function
{
    /// <summary>
    /// 개체 관리자
    /// </summary>
    /// <typeparam name="T">개체 타입</typeparam>
    public abstract class EntityManager<T>
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Protected

        #region Field

        /// <summary>
        /// DB 관리자
        /// </summary>
        protected DBManager _pDBManager = null;

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Property
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 타입명 - TypeName

        /// <summary>
        /// 타입명
        /// </summary>
        public string TypeName
        {
            get
            {
                return GetType().Name;
            }
        }

        #endregion

        #region DB 관리자 - DBManager

        /// <summary>
        /// DB 관리자
        /// </summary>
        public DBManager DBManager
        {
            get
            {
                return _pDBManager;
            }
            set
            {
                _pDBManager = value;
            }
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Public     

        #region 컬렉션 구하기 - GetCollection(pDataTable)

        /// <summary>
        /// 컬렉션 구하기
        /// </summary>
        /// <param name="pDataTable">데이타 테이블</param>
        /// <returns>컬렉션</returns>
        public Collection<T> GetCollection(DataTable pDataTable)
        {
            try
            {
                Collection<T> pCollection = new Collection<T>();

                if (pDataTable != null)
                {
                    foreach (DataRow pDataRow in pDataTable.Rows)
                    {
                        pCollection.Add(GetEntity(pDataRow));
                    }
                }

                return pCollection;
            }
            catch (Exception pException)
            {
                throw new ExceptionManager
                (
                    this,
                    "GetCollection(pDataTable)",
                    pException
                );
            }
        }

        #endregion

        #region 개체 구하기 - GetEntity(pDataRow)

        /// <summary>
        /// 개체 구하기
        /// </summary>
        /// <param name="pDataRow">데이타 로우</param>
        /// <returns>개체</returns>
        public abstract T GetEntity(DataRow pDataRow);

        #endregion
    }
}
