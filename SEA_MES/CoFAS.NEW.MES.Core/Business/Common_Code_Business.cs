
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Data;

namespace CoFAS.NEW.MES.Core.Business
{
    public class Common_Code_Business
    {
        public DataTable Common_Code_R10(Common_Code_Entity _Entity)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    DataTable _DataTable = new Common_Code_Provider(_DBManager).Common_Code_R10(_Entity);
                    return _DataTable;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "Common_Code_R10(Common_Code_Entity _Entity)", _Exception);
            }
        }
        public DataTable Common_Code_R20(Common_Code_Entity _Entity)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    DataTable _DataTable = new Common_Code_Provider(_DBManager).Common_Code_R20(_Entity);
                    return _DataTable;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "Common_Code_R10(Common_Code_Entity _Entity)", _Exception);
            }
        }
        public bool Common_Code_A10(Common_Code_Entity _Entity, ref xFpSpread fpMain)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new Common_Code_Provider(_DBManager).Common_Code_A10(_Entity, ref fpMain);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "Common_Code_A10(Common_Code_Entity _Entity, ref FpSpread fpMain)", _Exception);
            }
        }

        //공통코드 삭제 프로시저
        public bool Common_Code_A15(Common_Code_Entity _Entity)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new Common_Code_Provider(_DBManager).Common_Code_A15(_Entity);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "Common_Code_A10(Common_Code_Entity _Entity, ref FpSpread fpMain)", _Exception);
            }
        }

        public bool Common_Code_A20(Common_Code_Entity _Entity, ref xFpSpread fpMain)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new Common_Code_Provider(_DBManager).Common_Code_A20(_Entity,  fpMain);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "Common_Code_A10(Common_Code_Entity _Entity, ref FpSpread fpMain)", _Exception);
            }
        }

        public bool Common_Code_A30(Common_Code_Entity _Entity, ref xFpSpread fpMain)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new Common_Code_Provider(_DBManager).Common_Code_A30(_Entity, fpMain);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "Common_Code_A10(Common_Code_Entity _Entity, ref FpSpread fpMain)", _Exception);
            }
        }
    }
}
