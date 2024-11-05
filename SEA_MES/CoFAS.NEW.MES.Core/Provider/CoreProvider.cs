using CoFAS.NEW.MES.Core.Business;
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Core.Provider
{
    public class SpreadProvider : EntityManager<SpreadEntity>
    {
        public SpreadProvider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public override SpreadEntity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public DataTable Spread_ComobBox(string pServieceName, string pFirst, string pSecond)
        {
            try
            {
                
                IDataParameter[] pIDataParameter = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "SQLServer":
                        pIDataParameter = new IDataParameter[]
                        {
                            new SqlParameter("@ServiceName ".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@First       ".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@Second      ".Trim(), SqlDbType.VarChar, 50),
                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {
                            new MySqlParameter("@ServieceName".Trim(), MySqlDbType.VarChar, 50),
                            new MySqlParameter("@First       ".Trim(), MySqlDbType.VarChar, 50),
                            new MySqlParameter("@Second      ".Trim(), MySqlDbType.VarChar, 50),
                        };
                        break;
                }

                pIDataParameter[0].Value = pServieceName;
                pIDataParameter[1].Value = pFirst;
                pIDataParameter[2].Value = pSecond;

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_xSpreadComboBox_R10", pIDataParameter);
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
                    "Spread_ComobBox(string pServieceName, string pFirst, string pSecond)",
                    pException
                );
            }
        }
    }

    public class xComBoBoxProvider : EntityManager<CodeSelect_Entity>
    {
        public xComBoBoxProvider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public override CodeSelect_Entity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public DataTable xComboBox(CodeSelect_Entity _Entity)
        {
            try
            {
                IDataParameter[] pIDataParameter = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "SQLServer":
                        pIDataParameter = new IDataParameter[]
                        {
                            new SqlParameter("@ServiceName", SqlDbType.VarChar, 50)
                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {
                            new MySqlParameter("@ServiceName", MySqlDbType.VarChar, 50)
                        };
                        break;
                    default:
                        break;
                }

                pIDataParameter[0].Value = _Entity.ServiceName;

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_xComboBox_R10", pIDataParameter);
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
                    "xComboBox(CodeSelect_Entity _Entity)",
                    pException
                );
            }
        }

        public DataTable ComboBoxSetting(string ServiceName, string parameter1, string parameter2, string parameter3)
        {
            try
            {
                IDataParameter[] pIDataParameter = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "SQLServer":
                        pIDataParameter = new IDataParameter[]
                        {
                              new SqlParameter("@serviceName", SqlDbType.VarChar,50),
                              new SqlParameter("@parameter1", SqlDbType.VarChar,50),
                              new SqlParameter("@parameter2", SqlDbType.VarChar,50),
                              new SqlParameter("@parameter3", SqlDbType.VarChar,50)
                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {
                              new MySqlParameter("@E_DVReqNo   ", MySqlDbType.Int32),
                              new MySqlParameter("@E_DVReqSeq  ", MySqlDbType.Int32),
                              new MySqlParameter("@E_DVReqSerl ", MySqlDbType.VarChar, 50)
                        };
                        break;
                    default:
                        break;
                }
                pIDataParameter[0].Value = ServiceName;
                pIDataParameter[1].Value = parameter1;
                pIDataParameter[2].Value = parameter2;
                pIDataParameter[3].Value = parameter3;

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "[USP_xComboBox_R40]", pIDataParameter);
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
                    "xComboBox(CodeSelect_Entity _Entity)",
                    pException
                );
            }
        }
    }

    public class _PasswordProvider : EntityManager<PasswordEntity>
    {
        public override PasswordEntity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public _PasswordProvider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public DataTable ConfirmPassword_R10(string user_id, string user_password)
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
                            new SqlParameter("@user_password", SqlDbType.VarChar, 100)
                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {
                            new MySqlParameter("@user_account", MySqlDbType.VarChar, 50),
                            new MySqlParameter("@user_password", MySqlDbType.VarChar, 100)
                        };
                        break;
                    default:
                        break;
                }

                pIDataParameter[0].Value = user_id;
                pIDataParameter[1].Value = user_password;

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_ConfirmPassword_R10", pIDataParameter);
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
                    "ConfirmPassword_R10(string userid, string password)",
                    pException
                );
            }
        }

        public DataTable PasswordChange_A10(string user_id, string user_password)
        {
            bool _Error = false;

            _pDBManager.BeginTransaction();

            try
            {
                IDataParameter[] pIDataParameter = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "SQLServer":
                        pIDataParameter = new IDataParameter[]
                        {
                            new SqlParameter("@user_account", SqlDbType.VarChar, 50),
                            new SqlParameter("@user_password", SqlDbType.VarChar, 100)
                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {
                            new MySqlParameter("@user_account", MySqlDbType.VarChar, 50),
                            new MySqlParameter("@user_password", MySqlDbType.VarChar, 100)
                        };
                        break;
                    default:
                        break;
                }

                pIDataParameter[0].Value = user_id;
                pIDataParameter[1].Value = user_password;

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_PasswordChange_A10", pIDataParameter);
                return pDataTable;
            }
            catch (ExceptionManager pExceptionManager)
            {
                _Error = true;

                throw pExceptionManager;
            }
            catch (Exception pException)
            {
                _Error = true;
                throw new ExceptionManager
                (
                    this,
                    "PasswordChagne_A10(string user_account, string user_password)",
                    pException
                );
            }
            finally
            {
                if (_Error)
                    _pDBManager.RollbackTransaction();
                else
                    _pDBManager.CommitTransaction();
            }
        }
    }

    public class SystemLogProvider : EntityManager<SystemLogEntity>
    {
        #region 생성자 - SystemLogInfoProvider(pDBManager)
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="pDBManager">DB 관리자</param>
        public SystemLogProvider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public override SystemLogEntity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 시스템 이벤트 정보 저장 하기 - SystemEventLog_Info_Save(SystemLogInfoEntity pSystemLogInfoEntity)

        /// <summary>
        /// 조회하기
        /// </summary>
        /// <param name="pCORP_CD">회사코드</param>
        /// <param name="pWINDOW_NAME">화면 아이디</param>
        /// <param name="pDEV_GRID_NAME">그리드 아이디</param>
        /// <returns>조회 목록</returns>
        public bool SystemLog_Info(SystemLogEntity _SystemLogEntity)
        {
            bool pErrorYN = false;

            _pDBManager.BeginTransaction();

            try
            {
                IDataParameter[] pDataParameters = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {
                                new MySqlParameter("@user_account   ".Trim(), MySqlDbType.VarChar, 50),
                                new MySqlParameter("@event_ip       ".Trim(), MySqlDbType.VarChar, 15),
                                new MySqlParameter("@event_mac      ".Trim(), MySqlDbType.VarChar, 20),
                                new MySqlParameter("@event_name     ".Trim(), MySqlDbType.VarChar, 50),
                                new MySqlParameter("@event_type     ".Trim(), MySqlDbType.VarChar, 20),
                                new MySqlParameter("@event_log      ".Trim(), MySqlDbType.VarChar, 500)
                        };
                        break;

                    case "SQLServer":
                        pDataParameters = new IDataParameter[]
                        {
                                new SqlParameter("@user_account   ".Trim(), SqlDbType.VarChar, 50),
                                new SqlParameter("@event_ip       ".Trim(), SqlDbType.VarChar, 15),
                                new SqlParameter("@event_mac      ".Trim(), SqlDbType.VarChar, 20),
                                new SqlParameter("@event_name     ".Trim(), SqlDbType.VarChar, 50),
                                new SqlParameter("@event_type     ".Trim(), SqlDbType.VarChar, 20),
                                new SqlParameter("@event_log      ".Trim(), SqlDbType.VarChar, 500)
                        };
                        break;
                }

                pDataParameters[0].Value = _SystemLogEntity.user_account;
                pDataParameters[1].Value = _SystemLogEntity.user_ip;
                pDataParameters[2].Value = _SystemLogEntity.user_mac;
                pDataParameters[3].Value = _SystemLogEntity.user_pc;
                pDataParameters[4].Value = _SystemLogEntity.event_type;
                pDataParameters[5].Value = _SystemLogEntity.event_log;

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_SystemEventLog_A10", pDataParameters);

                if (pDataTable.Rows[0]["err_no"].ToString() != "00")
                    pErrorYN = true;

            }
            catch (ExceptionManager pExceptionManager)
            {
                pErrorYN = true;

                //throw pExceptionManager;
            }
            catch (Exception pException)
            {
                pErrorYN = true;

                throw new ExceptionManager
                (
                    this,
                    "SystemLog_Info(SystemLogEntity _SystemLogEntity)",
                    pException
                );
            }
            finally
            {
                if (pErrorYN)
                    _pDBManager.RollbackTransaction();  // 저장 실패 Rollback Transaction 
                else
                    _pDBManager.CommitTransaction();  // 저장 성공  Commit Transaction
            }
            return pErrorYN;
        }

        #endregion

    }

    public class xPopup_Provider : EntityManager<SystemLogEntity>
    {
        #region 생성자 - xPopup_Provider(pDBManager)
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="pDBManager">DB 관리자</param>
        public xPopup_Provider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public override SystemLogEntity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }
        #endregion

        public DataTable xPopup(string serviceName, string Parameter1, string Parameter2, string Parameter3, string USE_YN)
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
                            new SqlParameter("@ServiceName             ".Trim(), SqlDbType.VarChar, 20),
                            new SqlParameter("@Parameter1              ".Trim(), SqlDbType.VarChar, 20),
                            new SqlParameter("@Parameter2              ".Trim(), SqlDbType.VarChar, 20),
                            new SqlParameter("@Parameter3              ".Trim(), SqlDbType.VarChar, 20),
                            new SqlParameter("@USE_YN                  ".Trim(), SqlDbType.VarChar, 20),

                        };
                        break;
                }

                pDataParameters[0].Value = serviceName;
                pDataParameters[1].Value = Parameter1;
                pDataParameters[2].Value = Parameter2;
                pDataParameters[3].Value = Parameter3;
                pDataParameters[4].Value = USE_YN;


                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "[USP_xPOPUP_R20]", pDataParameters);
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
                    "LocationManagement_R20(string _Module)",
                    _Exception
                );
            }
        }


    }

    public class MenuSettingProvider : EntityManager<MenuSettingEntity>
    {
        #region 생성자 - MenuSettingProvider(pDBManager)
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="pDBManager">DB 관리자</param>
        public MenuSettingProvider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public override MenuSettingEntity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 메뉴 조회하기 - Menu_List_Search(string pUserID)

        /// <summary>
        /// 조회하기
        /// </summary>
        /// <param name="pCORP_CD">회사코드</param>
        /// <param name="pUserID">사용자아이디</param>
        /// <returns>조회 목록</returns>
        public DataTable MenuList_Search(string pUserID)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {
                            new MySqlParameter("@user_account", MySqlDbType.VarChar,50)
                        };
                        break;

                    case "SQLServer":
                        pDataParameters = new IDataParameter[]
                        {
                            new SqlParameter("@user_account", SqlDbType.NVarChar, 50)
                        };
                        break;
                }

                pDataParameters[0].Value = pUserID;

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_MainMenu_R10", pDataParameters);
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
                    "MenuList_Search(string pUserID)",
                    pException
                );
            }
        }

        #endregion


    }

    public class MenuButton_Provider : EntityManager<MenuButton_Entity>
    {
        public MenuButton_Provider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public override MenuButton_Entity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public DataTable MenuButton_Select(string menuID, string userAccount)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {
                            new MySqlParameter("@menuID", MySqlDbType.Int32),
                            new MySqlParameter("@userAccount", MySqlDbType.VarChar, 50)
                        };
                        break;

                    case "SQLServer":
                        pDataParameters = new IDataParameter[]
                        {
                            new SqlParameter("@menuID", SqlDbType.Int),
                            new SqlParameter("@userAccount", SqlDbType.VarChar, 50)
                        };
                        break;
                }

                pDataParameters[0].Value = menuID;
                pDataParameters[1].Value = userAccount;

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_MenuButton_R10", pDataParameters);
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
                    "MenuList_Search(string pUserID)",
                    pException
                );
            }
        }
    }
    public class MenuSave_Provider : EntityManager<MenuSave_Entity>
    {
        public MenuSave_Provider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public override MenuSave_Entity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }
        public DataTable MenuSave_R10(MenuSave_Entity _Entity)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {
                            new MySqlParameter("@menuID", MySqlDbType.Int32),
                            new MySqlParameter("@userAccount", MySqlDbType.VarChar, 50)
                        };
                        break;

                    case "SQLServer":
                        pDataParameters = new IDataParameter[]
                        {
                            new SqlParameter("@user_account   ".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@menu_name      ".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@spread_name    ".Trim(), SqlDbType.VarChar, 50),
                        };
                        break;
                }

                pDataParameters[0].Value = _Entity.user_account;
                pDataParameters[1].Value = _Entity.menu_name;
                pDataParameters[2].Value = _Entity.spread_name;


                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "MenuSave_R10", pDataParameters);
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
                    "MenuSave_A10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public void MenuSave_A10(xFpSpread pfpSpread)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                for (int i = 0; i < pfpSpread.Sheets[0].ColumnCount; i++)
                {
                    if (pfpSpread.Sheets[0].Columns[i].Tag.ToString() != "")
                    {
                        switch (_pDBManager.DBManagerType.ToString())
                        {
                            case "MySql":
                                pDataParameters = new IDataParameter[]
                                {
                                   new MySqlParameter("@menuID", MySqlDbType.Int32),
                                   new MySqlParameter("@userAccount", MySqlDbType.VarChar, 50)
                                };
                                break;

                            case "SQLServer":
                                pDataParameters = new IDataParameter[]
                                {
                                   //new SqlParameter("@user_account".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@menu_name   ".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@spread_name ".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@ColumnKey   ".Trim(), SqlDbType.VarChar, 50),
                                   //new SqlParameter("@HeaderName  ".Trim(), SqlDbType.VarChar, 50),
                                   //new SqlParameter("@Width       ".Trim(), SqlDbType.VarChar, 50),
                                   //new SqlParameter("@Visible     ".Trim(), SqlDbType.VarChar, 50),
                                   //new SqlParameter("@Locked      ".Trim(), SqlDbType.VarChar, 50),
                                   //new SqlParameter("@Align       ".Trim(), SqlDbType.VarChar, 50),
                                   //new SqlParameter("@CellType    ".Trim(), SqlDbType.VarChar, 50),
                                   //new SqlParameter("@Length      ".Trim(), SqlDbType.VarChar, 50),
                                   //new SqlParameter("@Point       ".Trim(), SqlDbType.VarChar, 50),
                                   //new SqlParameter("@CodeType    ".Trim(), SqlDbType.VarChar, 50),
                                   //new SqlParameter("@CodeName    ".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@Seq         ".Trim(), SqlDbType.VarChar, 50),
                                };
                                break;
                        }

                        //pDataParameters[0].Value = pfpSpread._user_account;
                        pDataParameters[0].Value = pfpSpread._menu_name;
                        pDataParameters[1].Value = pfpSpread.Name;
                        pDataParameters[2].Value = pfpSpread.Sheets[0].Columns[i].Tag.ToString();
                        //pDataParameters[4].Value = pfpSpread.Sheets[0].Columns[i].Label.ToString(); //@HeaderName  
                        //pDataParameters[5].Value = pfpSpread.Sheets[0].Columns[i].Width.ToString();  //@Width       
                        //pDataParameters[6].Value = pfpSpread.Sheets[0].Columns[i].Visible.ToString();
                        //pDataParameters[7].Value = pfpSpread.Sheets[0].Columns[i].Locked.ToString();
                        //pDataParameters[8].Value = pfpSpread.Sheets[0].Columns[i].HorizontalAlignment.ToString();
                        //pDataParameters[9].Value = pfpSpread.Sheets[0].Columns[i].CellType.ToString();
                        //pDataParameters[10].Value =   //@Length      
                        //pDataParameters[11].Value =   //@Point       
                        //pDataParameters[12].Value =   //@CodeType    
                        //pDataParameters[13].Value =   //@CodeName    
                        pDataParameters[3].Value = i.ToString();//@Seq         

                        //Convert.ToInt32(pfpSpread.Sheets[0].Columns[i].Width);
                        //pfpSpread.Sheets[0].Columns[i].Visible == true ? "Y" : "N"; ;
                        //pfpSpread.Sheets[0].Columns[i].AllowAutoFilter == true ? "Y" : "N";
                        //i;
                        //pfpSpread._user_account;
                        //pfpSpread._menu_name;
                        //pfpSpread.Name;
                        //pfpSpread.Sheets[0].Columns[i].Tag.ToString();
                        //Convert.ToInt32(pfpSpread.Sheets[0].Columns[i].Width);
                        //pfpSpread.Sheets[0].Columns[i].Visible == true ? "Y" : "N"; ;
                        //pfpSpread.Sheets[0].Columns[i].AllowAutoFilter == true ? "Y" : "N";

                        
                        
                        
                        
                        
                        
                        
                        
                        DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "MenuSave_A10", pDataParameters);
                        pDataParameters = null;
                    }
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
                    "MenuSave_A10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
    }
    public class MENU_SEARCH_LIST_Provider : EntityManager<MenuSave_Entity>
    {
        public MENU_SEARCH_LIST_Provider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public override MenuSave_Entity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }
        public DataTable MENU_SEARCH_LIST_R10(MenuSettingEntity _Entity)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {

                        };
                        break;

                    case "SQLServer":
                        pDataParameters = new IDataParameter[]
                        {
                            new SqlParameter("@WINDOW_NAME   ".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@TABLE_NAME      ".Trim(), SqlDbType.VarChar, 50),

                        };
                        break;
                }

                pDataParameters[0].Value = _Entity.MENU_WINDOW_NAME;
                pDataParameters[1].Value = _Entity.BASE_TABLE;



                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "[dbo].[MENU_SEARCH_LIST_R10]", pDataParameters);
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
                    "MenuSave_A10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public DataTable BASE_MENU_SETTING_R10(string MENU_WINDOW_NAME, xFpSpread fpMain, string BASE_TABLE)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {

                        };
                        break;

                    case "SQLServer":
                        pDataParameters = new IDataParameter[]
                        {
                            new SqlParameter("@WINDOW_NAME     ".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@SPREAD_NAME     ".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@TABLE_NAME      ".Trim(), SqlDbType.VarChar, 50),
                        };
                        break;
                }

                pDataParameters[0].Value = MENU_WINDOW_NAME;
                pDataParameters[1].Value = fpMain.Name;
                pDataParameters[2].Value = BASE_TABLE;



                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "BASE_MENU_SETTING_R10", pDataParameters);
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
                    "MenuSave_A10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }

    }

    public class BaseForm_Provider : EntityManager<MenuSave_Entity>
    {
        public BaseForm_Provider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }


        public override MenuSave_Entity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public DataTable SELECT(string sql)
        {
            try
            {
                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.Text, sql,null);
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
                    "MenuSave_A10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public DataTable BaseForm1_R10(Panel _Panel, MenuSettingEntity _MenuSettingEntity)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                StringBuilder sql = new StringBuilder();

                sql.Append($@"select *
                                 from {_Panel.Name} 
                                 where 1 = 1 ");

                sql.Append(_Panel.Tag.ToString());

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {

                        };
                        break;

                    case "SQLServer":
                        List<SqlParameter> list = new  List<SqlParameter>();

                        for (int i = 0; i < _Panel.Controls.Count; i++)
                        {
                            string _par = _Panel.Controls[i].Name;


                            switch (_Panel.Controls[i].GetType().Name)
                            {
                                case "Base_textbox":
                                    Base_textbox base_Textbox = _Panel.Controls[i] as Base_textbox;
                                    if (base_Textbox.SearchText.Length > 0)
                                    {
                                        sql.Append($" and {_par} like @{_par} ");
                                        SqlParameter sql1 = new SqlParameter($"@{_par}".Trim(), SqlDbType.VarChar, 50);
                                        //sql1.SqlValue = $"%{base_Textbox.SearchText}%";
                                        sql1.SqlValue = $"%{base_Textbox.SearchText}%";
                                        list.Add(sql1);
                                    }
                                    break;
                                case "Base_ComboBox":

                                    Base_ComboBox base_ComboBox = _Panel.Controls[i] as Base_ComboBox;
                                    if (base_ComboBox.SearchText.Length > 0)
                                    {
                                        sql.Append($" and {_par} = @{_par} ");
                                        SqlParameter sql2 = new SqlParameter($"@{_par}".Trim(), SqlDbType.VarChar, 50);
                                        sql2.SqlValue = base_ComboBox.SearchText;
                                        list.Add(sql2);
                                    }
                                    break;

                                case "Base_FromtoDateTime":

                                    Base_FromtoDateTime _FromtoDateTime = _Panel.Controls[i] as Base_FromtoDateTime;

                                    sql.Append($" and {_par} BETWEEN  @{_par + "1"} and @{_par + "2"} ");
                                    SqlParameter sql3 = new SqlParameter($"@{_par+"1"}".Trim(), SqlDbType.VarChar, 50);
                                    SqlParameter sql4 = new SqlParameter($"@{_par+"2"}".Trim(), SqlDbType.VarChar, 50);

                                    sql3.SqlValue = _FromtoDateTime.StartValue.ToString("yyyy-MM-dd HH:mm");
                                    sql4.SqlValue = _FromtoDateTime.EndValue.ToString("yyyy-MM-dd HH:mm");
                                    list.Add(sql3);
                                    list.Add(sql4);
                                    //sql1 = new SqlParameter($"@{_par}".Trim(), SqlDbType.VarChar, 50);
                                    //sql1.SqlValue = Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, x)).ToString("yyyy-MM-dd HH:mm:ss");
                                    break;
                                case "Base_Searchbox":

                                    Base_Searchbox base_Searchbox = _Panel.Controls[i] as Base_Searchbox;
                                    if (base_Searchbox.SearchText.Length > 0)
                                    {
                                        sql.Append($" and {_par} = @{_par} ");
                                        SqlParameter sql2 = new SqlParameter($"@{_par}".Trim(), SqlDbType.VarChar, 50);
                                        sql2.SqlValue = base_Searchbox.SearchText;
                                        list.Add(sql2);
                                    }
                                    break;
                                case "삭제":
                                    break;
                            }



                        }

                        pDataParameters = new IDataParameter[list.Count];

                        for (int x = 0; x < pDataParameters.Length; x++)
                        {
                            pDataParameters[x] = list[x];
                        }

                        break;
                }


                //pDataParameters[0].Value = _Entity.MENU_WINDOW_NAME;
                //pDataParameters[1].Value = "fpMain";
                //pDataParameters[2].Value = _Entity.BASE_TABLE;

                sql.Append(_MenuSettingEntity.BASE_ORDER);

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.Text, sql.ToString(), pDataParameters);
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
                    "MenuSave_A10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public DataTable DoubleBaseForm_R10(string _mst_id, string str)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                StringBuilder sql = new StringBuilder();

                string[] strs = str.Split('/');

                sql.Append($@"select *
                                 from {strs[1]}
                                 where 1 = 1 
                                  and {strs[0] + "_ID"} = {_mst_id}");


                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {

                        };
                        break;

                    case "SQLServer":





                        break;
                }

                //pDataParameters[0].Value = _Entity.MENU_WINDOW_NAME;
                //pDataParameters[1].Value = "fpMain";
                //pDataParameters[2].Value = _Entity.BASE_TABLE;

                sql.Append(" and USE_YN = 'Y'");

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.Text, sql.ToString(), pDataParameters);
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
                    "MenuSave_A10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public DataTable DoubleBaseForm_R20(string _mst_id, string str, string where)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                StringBuilder sql = new StringBuilder();

                string[] strs = str.Split('/');

                sql.Append($@"select *
                                 from {strs[1]}
                                 where 1 = 1 
                                  and {strs[0] + "_ID"} = {_mst_id} {where} ");

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {

                        };
                        break;

                    case "SQLServer":





                        break;
                }

                //pDataParameters[0].Value = _Entity.MENU_WINDOW_NAME;
                //pDataParameters[1].Value = "fpMain";
                //pDataParameters[2].Value = _Entity.BASE_TABLE;

                sql.Append(" and USE_YN = 'Y'");

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.Text, sql.ToString(), pDataParameters);
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
                    "MenuSave_A10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public DataTable DoubleBaseForm_R30(string from, string where1, string where2, string where3)
        {
            try
            {
                IDataParameter[] pDataParameters = null;


                StringBuilder sql = new StringBuilder();
                string[] strs = from.Split('/');

                sql.Append($@"select *
                                  from {strs[1]}
                                 where 1 = 1 
                                 {where1} 
                                 {where2}
                                 {where3}");

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {

                        };
                        break;

                    case "SQLServer":





                        break;
                }

                //pDataParameters[0].Value = _Entity.MENU_WINDOW_NAME;
                //pDataParameters[1].Value = "fpMain";
                //pDataParameters[2].Value = _Entity.BASE_TABLE;

                sql.Append(" and USE_YN = 'Y'");

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.Text, sql.ToString(), pDataParameters);
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
                    "MenuSave_A10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public DataTable DoubleBaseForm_R40(string from, string where1)
        {
            try
            {
                IDataParameter[] pDataParameters = null;


                StringBuilder sql = new StringBuilder();
                string[] strs = from.Split('/');

                sql.Append($@"select *
                                  from {strs[1]}
                                 where 1 = 1 
                                 {where1}");

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {

                        };
                        break;

                    case "SQLServer":





                        break;
                }

                //pDataParameters[0].Value = _Entity.MENU_WINDOW_NAME;
                //pDataParameters[1].Value = "fpMain";
                //pDataParameters[2].Value = _Entity.BASE_TABLE;

                sql.Append(" and USE_YN = 'Y'");

                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.Text, sql.ToString(), pDataParameters);
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
                    "MenuSave_A10(MenuSave_Entity _Entity)",
                    pException
                );
            }
        }
        public bool BaseForm1_A10(MenuSettingEntity _Entity, xFpSpread fpMain, string BASE_TABLE)
        
        {
            bool _Error = false;

            string cCRUD = string.Empty;

            _pDBManager.BeginTransaction();

            try
            {
                IDataParameter[] pDataParameters = null;

                
                for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                {
                 
                    StringBuilder sql = new StringBuilder();

                    cCRUD = fpMain.Sheets[0].RowHeader.Cells[i, 0].Text;

                    switch (cCRUD)
                    {
                        case "수정":
                            sql.Append($@"UPDATE {BASE_TABLE} SET ");
                         
                            break;
                        case "입력":
                            sql.Append($@"INSERT INTO {BASE_TABLE}");
                           
                            break;
                        case "삭제":
                            if (BASE_TABLE == "BASE_MENU_SETTING")
                            {
                                sql.Append($@" DELETE FROM {BASE_TABLE} ");
                            }
                            else
                            {
                                sql.Append($@"UPDATE {BASE_TABLE} SET ");
                            }
                         
                            break;
                        case "합계":
                            cCRUD = "";
                            break;
                        default:

                            break;
                    }

                    if (cCRUD != "")
                    {
                        List<SqlParameter> list = new  List<SqlParameter>();

                        switch (_pDBManager.DBManagerType.ToString())
                        {
                            case "MySql":
                                break;
                            case "SQLServer":

                                if (cCRUD == "수정")
                                {
                                    sql.Append($"UP_DATE = @UP_DATE ");
                                    SqlParameter sql1 = new SqlParameter($"@UP_DATE".Trim(), SqlDbType.VarChar, 50);
                                    sql1.SqlValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    list.Add(sql1);

                                    sql.Append($",UP_USER = @UP_USER ");
                                    SqlParameter sql2 = new SqlParameter($"@UP_USER".Trim(), SqlDbType.VarChar, 50);
                                    sql2.SqlValue = fpMain._user_account;
                                    list.Add(sql2);

                                    for (int x = 0; x < fpMain.Sheets[0].ColumnCount; x++)
                                    {
                                        if (fpMain.Sheets[0].Columns[x].ParentStyleName == "Y")
                                        {
                                            string tag = fpMain.Sheets[0].Columns[x].Tag.ToString();

                                            if (tag.Split('.').Length == 2)
                                            {
                                                tag = tag.Split('.')[1];
                                            }

                                            if (tag != "ID" && tag != "UP_DATE" && tag != "UP_USER" && tag != "MENU_ID")
                                            {
                                                SqlParameter sql3 = null;

                                                switch (fpMain.Sheets[0].Columns[x].CellType.ToString())
                                                {
                                                    case "DateTimeCellType":
                                                        sql.Append($",{tag} = @{tag} ");
                                                        sql3 = new SqlParameter($"@{tag}".Trim(), SqlDbType.DateTime);

                                                        if (fpMain.Sheets[0].GetValue(i, x) == null || fpMain.Sheets[0].GetValue(i, x).ToString() == "")
                                                        {
                                                            sql3.SqlValue = DBNull.Value;
                                                        }
                                                        else
                                                        {
                                                            sql3.SqlValue = Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, x)).ToString("yyyy-MM-dd HH:mm:ss");
                                                        }

                                                        break;
                                                    case "NumberCellType":
                                                        
                                                        sql.Append($",{tag} = @{tag} ");
                                                        sql3 = new SqlParameter($"@{tag}".Trim(), SqlDbType.Decimal);

                                                        if (fpMain.Sheets[0].GetValue(i, x) == null)
                                                        {
                                                            sql3.SqlValue = 0;
                                                        }
                                                        else
                                                        {
                                                            sql3.SqlValue = fpMain.Sheets[0].GetValue(i, x);
                                                        }

                                                       

                                                        

                                                        break;
                                                    case "ButtonCellType":
                                                    case "DisplayTextCellType":
                                                    case "DisplayNumberCellType":
                                                    case "DisplayCheckBoxCellType":
                                                    case "TextButtonCellType":
                                                        //sql.Append($",{tag} = @{tag} ");
                                                        //sql3 = new SqlParameter($"@{tag}".Trim(), SqlDbType.VarChar, 50);
                                                        //sql3.SqlValue = fpMain.Sheets[0].GetValue(i, x);
                                                        break;

                                                    case "MYComboBoxCellType":
                                                        sql.Append($",{tag} = @{tag} ");
                                                        int su =0;
                                                        if (int.TryParse(fpMain.Sheets[0].GetValue(i, x).ToString(), out su))
                                                        {
                                                            sql3 = new SqlParameter($"@{tag}".Trim(), SqlDbType.BigInt);
                                                            sql3.SqlValue = Convert.ToInt32(fpMain.Sheets[0].GetValue(i, x));

                                                        }
                                                        else
                                                        {
                                                            string str =fpMain.Sheets[0].GetValue(i, x).ToString();
                                                            sql3 = new SqlParameter($"@{tag}".Trim(), SqlDbType.VarChar, str.Length);
                                                            sql3.SqlValue = str;
                                                        }
                                                        break;
                                                    case "ImageCellType":
                                                        using (MemoryStream ms = new MemoryStream())
                                                        {
                                                            bool ok = true;
                                                            try
                                                            {
                                                                Image ima = fpMain.Sheets[0].GetValue(i, x) as Image;
                                                                if (ima != null)
                                                                {
                                                                    ima.Save(ms, ImageFormat.Bmp);
                                                                }
                                                                else
                                                                {
                                                                    ok = false;
                                                                }
                                                            }
                                                            catch (Exception)
                                                            {
                                                                ok = false;
                                                            }
                                                            if (ok)
                                                            {
                                                                sql.Append($",{tag} = @{tag} ");
                                                                sql3 = new SqlParameter($"@{tag}".Trim(), SqlDbType.Image);
                                                                sql3.SqlValue = ms.ToArray();
                                                            }
                                                        }

                                                        break;
                                                    case "FileButtonCellType":
                                                        byte[] file = fpMain.Sheets[0].GetValue(i, x) as byte[];
                                                        if (file != null)
                                                        {
                                                            sql.Append($",{tag} = @{tag} ");
                                                            sql3 = new SqlParameter($"@{tag}".Trim(), SqlDbType.Image);
                                                            sql3.SqlValue = file;
                                                        } 
                                                        break;
                                                    case "CheckBoxCellType":
                                                        sql.Append($",{tag} = @{tag} ");

                                                        sql3 = new SqlParameter($"@{tag}".Trim(), SqlDbType.VarChar, 1);


                                                        if (Convert.ToBoolean(fpMain.Sheets[0].GetValue(i, x)))
                                                        {
                                                            sql3.SqlValue = "Y";
                                                        }
                                                        else
                                                        {
                                                            sql3.SqlValue = "N";
                                                        }



                                                        break;

                                                    default:
                                                        sql.Append($",{tag} = @{tag} ");
                                                        sql3 = new SqlParameter($"@{tag}".Trim(), SqlDbType.VarChar, 100);
                                                        if (fpMain.Sheets[0].GetValue(i, x) == null)
                                                        {
                                                            sql3.SqlValue = "";
                                                        }
                                                        else
                                                        {
                                                            sql3.SqlValue = fpMain.Sheets[0].GetValue(i, x).ToString();
                                                        }
                                                        break;
                                                }
                                                if (sql3 != null)
                                                {
                                                    list.Add(sql3);
                                                }



                                            }
                                        }
                                    }

                                    sql.Append($"WHERE ID = {fpMain.Sheets[0].Rows[i].GetValue("ID").ToString()} ");
                                    //for (int a = 0; a < fpMain.Sheets[0].ColumnCount; a++)
                                    //{
                                    //    if(fpMain.Sheets[0].Columns[i].Tag.ToString().Contains("UP_DATE"))
                                    //    {
                                    //        sql.Append($"and FORMAT(UP_DATE,'yyyy-MM-dd HH:mm:ss') = '{Convert.ToDateTime(fpMain.Sheets[0].Rows[i].GetValue(fpMain.Sheets[0].Columns[i].Tag.ToString())).ToString("yyyy-MM-dd HH:mm:ss")}' ");
                                    //    }
                                    //}                            
                                }
                                if (cCRUD == "입력")
                                {
                                    StringBuilder into = new StringBuilder();
                                    into.Append($"(");

                                    for (int x = 0; x < fpMain.Sheets[0].ColumnCount; x++)
                                    {
                                        if (fpMain.Sheets[0].Columns[x].ParentStyleName == "Y")
                                        {
                                            string tag = fpMain.Sheets[0].Columns[x].Tag.ToString();
                                            if (tag.Split('.').Length == 2)
                                            {
                                                tag = tag.Split('.')[1];
                                            }
                                            if (tag != "ID" && tag != "MENU_ID")
                                            {
                                                if (fpMain.Sheets[0].Columns[x].CellType != null)
                                                {
                                                    if (fpMain.Sheets[0].Columns[x].CellType.ToString() != "ButtonCellType" &&
                                                        fpMain.Sheets[0].Columns[x].CellType.ToString().Contains("Display") == false &&
                                                        fpMain.Sheets[0].Columns[x].CellType.ToString() != "TextButtonCellType")
                                                    {
                                                        if (into.Length == 1)
                                                        {
                                                            into.Append($"{tag}");
                                                        }
                                                        else
                                                        {
                                                            into.Append($",{tag}");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    into.Append($") VALUES ");

                                    StringBuilder values = new StringBuilder();

                                    values.Append($"(");
                                    for (int x = 0; x < fpMain.Sheets[0].ColumnCount; x++)
                                    {
                                        if (fpMain.Sheets[0].Columns[x].ParentStyleName == "Y")
                                        {
                                            string tag = fpMain.Sheets[0].Columns[x].Tag.ToString();
                                            if (tag.Split('.').Length == 2)
                                            {
                                                tag = tag.Split('.')[1];
                                            }
                                            if (tag != "ID" && tag != "MENU_ID")
                                            {

                                                if (fpMain.Sheets[0].Columns[x].CellType != null)
                                                {
                                                    SqlParameter sql1 = null;

                                                    switch (fpMain.Sheets[0].Columns[x].CellType.ToString())
                                                    {
                                                        case "DateTimeCellType":
                                                            sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.DateTime);
                                                            if (fpMain.Sheets[0].GetValue(i, x) == null)
                                                            {
                                                                sql1.SqlValue = DBNull.Value;
                                                            }
                                                            else
                                                            {
                                                                sql1.SqlValue = Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, x)).ToString("yyyy-MM-dd HH:mm:ss");
                                                            }
                                                            break;
                                                        case "NumberCellType":
                                                            sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.Decimal);

                                                            if (fpMain.Sheets[0].GetValue(i, x) == null)
                                                            {
                                                                sql1.SqlValue = 0;
                                                            }
                                                            else
                                                            {
                                                                sql1.SqlValue = fpMain.Sheets[0].GetValue(i, x);
                                                            }
                                                          
                                                            break;
                                                        case "ButtonCellType":
                                                        case "DisplayTextCellType":
                                                        case "DisplayNumberCellType":
                                                        case "DisplayCheckBoxCellType":
                                                        case "TextButtonCellType":
                                                            break;
                                                        case "MYComboBoxCellType":

                                                            int su =0;
                                                            if (int.TryParse(fpMain.Sheets[0].GetValue(i, x).ToString(), out su))
                                                            {
                                                                sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.BigInt);
                                                                sql1.SqlValue = Convert.ToInt32(fpMain.Sheets[0].GetValue(i, x));

                                                            }
                                                            else
                                                            {
                                                                string str =fpMain.Sheets[0].GetValue(i, x).ToString();
                                                                sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.VarChar, str.Length);
                                                                sql1.SqlValue = str;
                                                            }
                                                            break;

                                                        case "ImageCellType":
                                                            sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.Image);
                                                            using (MemoryStream ms = new MemoryStream())
                                                            {
                                                                Image ima = fpMain.Sheets[0].GetValue(i, x) as Image;
                                                                if (ima != null)
                                                                {
                                                                    ima.Save(ms, ImageFormat.Bmp);

                                                                    sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.Image);
                                                                    sql1.SqlValue = ms.ToArray();
                                                                }
                                                                else
                                                                {
                                                                    sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.Image);
                                                                    sql1.SqlValue = DBNull.Value;
                                                                }
                                                            }
                                                            break;
                                                        case "FileButtonCellType":
                                                            sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.Image);
                                                           
                                                            byte[] file = fpMain.Sheets[0].GetValue(i, x) as byte[];

                                                            if (file != null)
                                                            { 
                                                                sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.Image);
                                                                sql1.SqlValue = file;
                                                            }
                                                            else
                                                            {
                                                                sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.Image);
                                                                sql1.SqlValue = DBNull.Value;
                                                            }

                                                            break;
                                                        case "CheckBoxCellType":

                                                            sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.VarChar, 1);

                                                            if (Convert.ToBoolean(fpMain.Sheets[0].GetValue(i, x)))
                                                            {
                                                                sql1.SqlValue = "Y";
                                                            }
                                                            else
                                                            {
                                                                sql1.SqlValue = "N";
                                                            }
                                                            break;
                                                        default:
                                                            sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.VarChar, 50);
                                                            if (fpMain.Sheets[0].GetValue(i, x) == null)
                                                            {
                                                                sql1.SqlValue = "";
                                                            }
                                                            else
                                                            {
                                                                sql1.SqlValue = fpMain.Sheets[0].GetValue(i, x).ToString();
                                                            }



                                                            break;
                                                    }

                                                    if (sql1 != null)
                                                    {
                                                        if (values.Length == 1)
                                                        {
                                                            values.Append($"@{tag} ");
                                                        }
                                                        else
                                                        {
                                                            values.Append($",@{tag} ");
                                                        }
                                                        list.Add(sql1);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    values.Append($")");

                                    sql.Append(into);
                                    sql.Append(values);
                                }
                                if (cCRUD == "삭제")
                                {
                                    if (BASE_TABLE == "BASE_MENU_SETTING")
                                    {
                                        sql.Append($"WHERE ID = {fpMain.Sheets[0].Rows[i].GetValue("ID").ToString()} ");
                                    }
                                    else
                                    {
                                        sql.Append($"UP_DATE = @UP_DATE ");
                                        SqlParameter sql1 = new SqlParameter($"@UP_DATE".Trim(), SqlDbType.VarChar, 50);
                                        sql1.SqlValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        list.Add(sql1);

                                        sql.Append($",UP_USER = @UP_USER ");
                                        SqlParameter sql2 = new SqlParameter($"UP_USER".Trim(), SqlDbType.VarChar, 50);
                                        sql2.SqlValue = fpMain._user_account;
                                        list.Add(sql2);

                                        sql.Append($",USE_YN = 'N' ");

                                        sql.Append($"WHERE ID = {fpMain.Sheets[0].Rows[i].GetValue("ID").ToString()} ");
                                    }
                                    break;
                                }
                                break;
                        }

                        pDataParameters = new IDataParameter[list.Count];

                        for (int x = 0; x < pDataParameters.Length; x++)
                        {
                            pDataParameters[x] = list[x];
                        }


                        if (_pDBManager.DbConnection.State != ConnectionState.Open)
                        {
                            _pDBManager.DbConnection.Open();
                        }

                        string sq = sql.ToString();
                        //CustomMsg.ShowMessageLink(sq, "저장 오류");
                        int Execute =_pDBManager.Execute(CommandType.Text, sq, pDataParameters);

                        // DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_UserInformation_A10", pDataParameters);
                        if (Execute == 0)
                        {
                            //CustomMsg.ShowMessageLink(sq, "저장 오류");
                            _Error = true;
                        }
                        else
                        {
                            _Error = false;
                        }

                        pDataParameters = null;
                    }
                }

            }
            catch (ExceptionManager _ExceptionManager)
            {
                _Error = true;
                throw new ExceptionManager
               (
                   this,
                   _ExceptionManager.Exception.Message,
                   _ExceptionManager.Exception
               );
            }
            catch (Exception _Exception)
            {
                _Error = true;
                throw new ExceptionManager
                (
                    this,
                    "DataTable UserInformation_A10(UserInformation_Entity _Entity, ref FpSpread fpMain)",
                    _Exception
                );
            }
            finally
            {
                if (_Error)
                    _pDBManager.RollbackTransaction();
                else
                    _pDBManager.CommitTransaction();
            }

            return _Error;
        }

        public bool IN_STOCK_WAIT_A10(xFpSpread fpMain, string user)
        {
            bool _Error = false;

            string cCRUD = string.Empty;

            _pDBManager.BeginTransaction();

            try
            {
                IDataParameter[] pDataParameters = null;

                for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                {
                    StringBuilder sql = new StringBuilder();

                    List<SqlParameter> list = new List<SqlParameter>();

                    cCRUD = fpMain.Sheets[0].RowHeader.Cells[i, 0].Text;

                        if (cCRUD == "입력")
                        {
                            if(fpMain.Sheets[0].GetValue(i, "OUT_CODE").ToString().Contains("외주") == true)
                            {
                            string str = $@"INSERT 
                        INTO IN_STOCK_DETAIL
                        (
                          OUT_CODE
                          ,IN_STOCK_DATE
                          ,IN_TYPE
                          ,STOCK_MST_ID
                          ,STOCK_MST_OUT_CODE
                          ,STOCK_MST_STANDARD
                          ,STOCK_MST_TYPE
                          ,IN_QTY
                          ,COMPLETE_YN
                          ,USE_YN
                          ,INSPECTION_DETAIL_ID
                          ,REG_USER
                          ,REG_DATE
                          ,UP_USER
                          ,UP_DATE
                        )
                        values
                        (
                          '{fpMain.Sheets[0].GetValue(i, "OUT_CODE").ToString()}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'SD13006'
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(0, "IN_QTY").ToString())}
                           ,'Y'
                           ,'Y'
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "INSPECTION_DETAIL_ID").ToString())}
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        )";
                            sql.Append(str);
                        }

                            else
                            {
                            string str = $@"INSERT 
                        INTO IN_STOCK_DETAIL
                        (
                          OUT_CODE
                          ,IN_STOCK_DATE
                          ,IN_TYPE
                          ,STOCK_MST_ID
                          ,STOCK_MST_OUT_CODE
                          ,STOCK_MST_STANDARD
                          ,STOCK_MST_TYPE
                          ,IN_QTY
                          ,COMPLETE_YN
                          ,USE_YN
                          ,INSPECTION_DETAIL_ID
                          ,REG_USER
                          ,REG_DATE
                          ,UP_USER
                          ,UP_DATE
                        )
                        values
                        (
                          '{fpMain.Sheets[0].GetValue(i, "OUT_CODE").ToString()}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'SD13005'
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(0, "IN_QTY").ToString())}
                           ,'Y'
                           ,'Y'
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "INSPECTION_DETAIL_ID").ToString())}
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        )";
                            sql.Append(str);
                        }

                            //list.Add(str);

                            if (_pDBManager.DbConnection.State != ConnectionState.Open)
                            {
                                _pDBManager.DbConnection.Open();
                            }

                            int Execute = _pDBManager.Execute2(CommandType.Text, sql.ToString());

                            // DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_UserInformation_A10", pDataParameters);
                            _Error = false;
                            //if (Execute == 0)
                            //{
                            //    _Error = true;
                            //}
                            //else
                            //{
                            //    _Error = false;
                            //}

                            pDataParameters = null;
                    }
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                _Error = true;
                throw new ExceptionManager
               (
                   this,
                   _ExceptionManager.Exception.Message,
                   _ExceptionManager.Exception
               );
            }
            catch (Exception _Exception)
            {
                _Error = true;
                throw new ExceptionManager
                (
                    this,
                    "DataTable UserInformation_A10(UserInformation_Entity _Entity, ref FpSpread fpMain)",
                    _Exception
                );
            }
            finally
            {
                if (_Error)
                    _pDBManager.RollbackTransaction();
                else
                    _pDBManager.CommitTransaction();
            }

            return _Error;
        }
    }
}

