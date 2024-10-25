using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoFAS.NEW.MES.Core.Entity;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;

namespace CoFAS.NEW.MES.Core.Business
{
    public class SI_Business
    {
      

        public bool 수주관리_PopupBox(xFpSpread fpMain)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).수주관리_PopupBox(fpMain);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "DataTable UserInformation_A10(UserInformation_Entity _Entity, ref FpSpread fpMain)", _Exception);
            }

        }
        public DataTable EQUIPMENT_INSPECTION_RECORD_R10(int pID,string pTYPE,string pCHECK_DATE,string pUSER)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  SI_Provider(pDBManager).EQUIPMENT_INSPECTION_RECORD_R10(pID,pTYPE,pCHECK_DATE,pUSER);
                    return pDataTable;
                }
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
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public DataTable EQUIPMENT_INSPECTION_RECORD_R10(Panel _Panel, MenuSettingEntity _MenuSettingEntity)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  SI_Provider(pDBManager).EQUIPMENT_INSPECTION_RECORD_R10(_Panel,_MenuSettingEntity);
                    return pDataTable;
                }
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
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

        public DataTable USP_Production_Progress_Status_R10()
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  SI_Provider(pDBManager).USP_Production_Progress_Status_R10();
                    return pDataTable;
                }
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
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

        public DataTable WORKING_CONDITIONS_STATUS2_R10(DateTime pSTART_DATE, DateTime pEND_DATE)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  SI_Provider(pDBManager).WORKING_CONDITIONS_STATUS2_R10(pSTART_DATE,pEND_DATE);
                    return pDataTable;
                }
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
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public DataTable PROCESS_NG_STATUS_R10(string code)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  SI_Provider(pDBManager).PROCESS_NG_STATUS_R10(code);
                    return pDataTable;
                }
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
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

        public DataTable MENU_INFORMATION_R10(string _mst_id, string str)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  SI_Provider(pDBManager).MENU_INFORMATION_R10(_mst_id,str);
                    return pDataTable;
                }
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
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

        public bool MENU_INFORMATION_A10(MenuSettingEntity _Entity, xFpSpread fpMain, string BASE_TABLE)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).MENU_INFORMATION_A10(_Entity,fpMain,BASE_TABLE);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, "DataTable UserInformation_A10(UserInformation_Entity _Entity, ref FpSpread fpMain)", _Exception);
            }

        }

        public DataTable PRODUCTION_PLAN_R10(string _mst_id, string str)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable =  new  SI_Provider(pDBManager).PRODUCTION_PLAN_R10(_mst_id,str);
                    return pDataTable;
                }
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
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

        public DataTable PRODUCTION_ORDER_R10(Panel _Panel)
        {
            try
            {
                using (DBManager pDBManager = new DBManager())
                {
                    DataTable pDataTable = new SI_Provider(pDBManager).PRODUCTION_ORDER_R10(_Panel);
                    return pDataTable;
                }
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
                    "MenuSave_R10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

        public bool IN_STOCK_DETAIL_POPUPBOX_A10(xFpSpread fpMain, string user)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).IN_STOCK_DETAIL_POPUPBOX_A10(fpMain, user);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }
        public bool 외주작업지시_반출_A10(xFpSpread fpMain, string user, string ID)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).외주작업지시_반출_A10(fpMain, user, ID);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }
        public bool OUT_SOURCING_IN_STOCK_POPUPBOX_A10(xFpSpread fpMain, string user , string INSTRUCT_ID)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).OUT_SOURCING_IN_STOCK_POPUPBOX_A10(fpMain, user , INSTRUCT_ID);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }
        public bool INSPECTION_STOCK_REGISTER_RETURN_A10(xFpSpread fpMain, string user)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).INSPECTION_STOCK_REGISTER_RETURN_A10(fpMain, user);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }
        public bool INSPECTION_STOCK_REGISTER_RETURN_SUB_A10(xFpSpread fpMain, string user,string mst_id, string in_detail_id)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).INSPECTION_STOCK_REGISTER_RETURN_SUB_A10(fpMain, user, mst_id, in_detail_id);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }
        public bool INSTRUMENT_HISTORY_REGISTER_A10(xFpSpread fpMain, string user)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).INSTRUMENT_HISTORY_REGISTER_A10(fpMain, user);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }
        public bool INSTRUMENT_HISTORY_REGISTER_SUB_A10(xFpSpread fpMain, string user, string mst_id)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).INSTRUMENT_HISTORY_REGISTER_SUB_A10(fpMain, user, mst_id);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }
        public bool IN_STOCK_WAIT_A10(xFpSpread fpMain, string user)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).IN_STOCK_WAIT_A10(fpMain, user);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }

        public bool STOCK_ORDER_MST_DETAIL_A10(xFpSpread fpMain, string user , int row)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).STOCK_ORDER_MST_DETAIL_A10(fpMain, user, row);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }

        public bool STOCK_ORDER_MST_DETAIL_A20(xFpSpread fpMain, string user,int row)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).STOCK_ORDER_MST_DETAIL_A20(fpMain, user,row);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }

        public bool INSTRUCT_COMPLETE_Y_A10(xFpSpread fpMain, string user)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).INSTRUCT_COMPLETE_Y_A10(fpMain, user);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }

        public bool IN_STOCK_WAIT_Y_A10(xFpSpread fpMain, string ID)
        {
            try
            {
                using (DBManager _DBManager = new DBManager())
                {
                    bool _Error = new SI_Provider(_DBManager).IN_STOCK_WAIT_Y_A10(fpMain, ID);
                    return _Error;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager.Exception;
            }
            catch (Exception _Exception)
            {
                throw new ExceptionManager(this, _Exception.Message, _Exception);
            }

        }
    }


  

}
