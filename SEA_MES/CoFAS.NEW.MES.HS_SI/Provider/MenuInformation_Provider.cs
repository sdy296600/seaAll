using FarPoint.Win.Spread;
using CoFAS.NEW.MES.HS_SI.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core;

namespace CoFAS.NEW.MES.HS_SI.Provider
{
    public class MenuInformation_Provider : EntityManager<MenuInformation_Entity>
    {
        public override MenuInformation_Entity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public MenuInformation_Provider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public DataTable MenuInformation_R10(MenuInformation_Entity _Entity)
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
                            new SqlParameter("@module", SqlDbType.VarChar, 10)
                        };
                        break;
                }

                pDataParameters[0].Value = _Entity._SearchModule == "" ? "%" : _Entity._SearchModule;

                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_MenuInformation_R10", pDataParameters);
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
                    "DataTable MenuInformation_R10(MenuInformation_Entity _Entity)",
                    _Exception
                );
            }
        }

        public DataTable MenuInformation_R20(string p_menu_id)
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
                            new SqlParameter("@p_menu_id", SqlDbType.VarChar, 10)
                        };
                        break;
                }

                pDataParameters[0].Value = p_menu_id;

                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_MenuInformation_R20", pDataParameters);
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
                    "DataTable MenuInformation_R20(string _module)",
                    _Exception
                );
            }
        }

        public bool MenuInformation_A10(MenuInformation_Entity _Entity, ref xFpSpread fpMain)
        {
            bool _Error = false;

            string cCRUD = string.Empty;
            string vCRUD = string.Empty;

            _pDBManager.BeginTransaction();

            try
            {
                IDataParameter[] pDataParameters = null;

                for (int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
                {
                    cCRUD = fpMain.Sheets[0].RowHeader.Cells[i, 0].Text;

                    switch (cCRUD)
                    {
                        case "수정":
                            vCRUD = "U";
                            break;
                        case "입력":
                            vCRUD = "C";
                            break;
                        case "삭제":
                            vCRUD = "D";
                            break;
                        default:
                            vCRUD = "";
                            break;
                    }

                    if (vCRUD != "")
                    {
                        switch (_pDBManager.DBManagerType.ToString())
                        {
                            case "MySql":
                                pDataParameters = new IDataParameter[] { };
                                break;
                            case "SQLServer":
                                pDataParameters = new IDataParameter[]
                                {
                                    new SqlParameter("@crud			".Trim(), SqlDbType.VarChar, 1),
                                    new SqlParameter("@user_account	".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@menu_id      ".Trim(), SqlDbType.Int),
                                    new SqlParameter("@menu_name	".Trim(), SqlDbType.VarChar, 100),
                                    new SqlParameter("@p_menu_id	".Trim(), SqlDbType.Int),
                                    new SqlParameter("@window_name	".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@module		".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@icon			".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@sort			".Trim(), SqlDbType.Int),
                                    new SqlParameter("@description	".Trim(), SqlDbType.VarChar, 2000),
                                    new SqlParameter("@use_yn		".Trim(), SqlDbType.VarChar, 1)
                                };
                                break;
                        }

                        pDataParameters[0].Value = vCRUD;
                        pDataParameters[1].Value = _Entity.user_account;
                        pDataParameters[2].Value = vCRUD == "C" ? "0" : fpMain.Sheets[0].GetText(i, "menu_id").ToString();
                        pDataParameters[3].Value = fpMain.Sheets[0].GetText(i, "menu_name   ".Trim()).ToString();
                        pDataParameters[4].Value = fpMain.Sheets[0].GetText(i, "p_menu_id   ".Trim()).ToString();
                        pDataParameters[5].Value = fpMain.Sheets[0].GetText(i, "window_name ".Trim()).ToString();
                        pDataParameters[6].Value = fpMain.Sheets[0].GetText(i, "module      ".Trim()).ToString();
                        pDataParameters[7].Value = fpMain.Sheets[0].GetText(i, "icon        ".Trim()).ToString();
                        pDataParameters[8].Value = fpMain.Sheets[0].GetText(i, "sort        ".Trim()).ToString();
                        pDataParameters[9].Value = fpMain.Sheets[0].GetText(i, "description ".Trim()).ToString();
                        pDataParameters[10].Value = fpMain.Sheets[0].GetText(i, "use_yn     ".Trim()).ToString() == "True" ? "Y" : "N";

                        DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_MenuInformation_A10", pDataParameters);
                        if (_DataTable != null && _DataTable.Rows.Count > 0)
                        {
                            if (_DataTable.Rows[0]["p_err_no"].ToString() != "00")
                            {
                                fpMain.Sheets[0].SetValue(i, 0, "True");
                                fpMain.Sheets[0].SetValue(i, 1, _DataTable.Rows[0][i].ToString());
                                _Error = true;
                            }
                        }
                        else
                        {
                            _Error = true;
                        }

                        pDataParameters = null;
                    }
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                _Error = true;
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                _Error = true;

                throw new ExceptionManager
                (
                    this,
                    "MenuInformation_A10(MenuInformation_Entity _Entity, ref FpSpread fpMain)",
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

        public DataTable MenuInformation_R30(string _module)
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
                            new SqlParameter("@module", SqlDbType.VarChar, 10)
                        };
                        break;
                }

                pDataParameters[0].Value = _module;

                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_MenuInformation_R30", pDataParameters);
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
                    "DataTable MenuInformation_R30(string _module)",
                    _Exception
                );
            }
        }

        public bool MenuInformation_A20(string _Module, string _ModuleName, string _Account)
        {
            bool _Error = false;
            _pDBManager.BeginTransaction();

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
                                    new SqlParameter("@Module		".Trim(), SqlDbType.VarChar, 100),
                                    new SqlParameter("@ModuleName	".Trim(), SqlDbType.VarChar, 100),
                                    new SqlParameter("@user_account	".Trim(), SqlDbType.VarChar, 50)
                        };
                        break;
                }

                pDataParameters[0].Value = _Module;
                pDataParameters[1].Value = _ModuleName;
                pDataParameters[2].Value = _Account;

                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_MenuInformation_A20", pDataParameters);
                if (_DataTable != null && _DataTable.Rows.Count > 0)
                {
                    if (_DataTable.Rows[0]["p_err_no"].ToString() != "00")
                    {
                        _Error = true;
                    }
                }
                else
                {
                    _Error = true;
                }
            }
            catch (ExceptionManager _ExceptionManager)
            {
                _Error = true;
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {
                _Error = true;

                throw new ExceptionManager
                (
                    this,
                    "bool MenuInformation_A20(string _Module, string _ModuleName, string _Account)",
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
