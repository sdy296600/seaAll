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
    public class AuthInformation_Provider : EntityManager<AuthInformation_Entity>
    {
        public override AuthInformation_Entity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public AuthInformation_Provider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public DataTable AuthInformation_R10(AuthInformation_Entity _Entity)
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
                            new SqlParameter("@Authority_name", SqlDbType.VarChar, 50),
  
                        };
                        break;
                }

                pDataParameters[0].Value =  _Entity._Authority_name;
     

                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_AuthInformation_R10", pDataParameters);
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
                    "DataTable AuthInformation_R10(AuthInformation_Entity _Entity)",
                    _Exception
                );
            }
        }

        public DataTable AuthInformation_R20(string _UserAccount)
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
                            new SqlParameter("@UserAccount", SqlDbType.VarChar, 50)
                        };
                        break;
                }

                pDataParameters[0].Value = _UserAccount;

                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_AuthInformation_R20", pDataParameters);
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
                    "DataTable AuthInformation_R20(string _UserAccount)",
                    _Exception
                );
            }
        }

        public bool AuthInformation_A10(AuthInformation_Entity _Entity, string _UserAccount, ref xFpSpread fpMain)
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

                    if (cCRUD != "")
                    {
                        switch (_pDBManager.DBManagerType.ToString())
                        {
                            case "MySql":
                                pDataParameters = new IDataParameter[] { };
                                break;
                            case "SQLServer":
                                pDataParameters = new IDataParameter[]
                                {
                                    new SqlParameter("@user_account		".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@menu_id			".Trim(), SqlDbType.Int),
                                    new SqlParameter("@menu_useyn		".Trim(), SqlDbType.VarChar, 1),
                                    new SqlParameter("@menu_search		".Trim(), SqlDbType.VarChar, 1),
                                    new SqlParameter("@menu_print		".Trim(), SqlDbType.VarChar, 1),
                                    new SqlParameter("@menu_delete		".Trim(), SqlDbType.VarChar, 1),
                                    new SqlParameter("@menu_save		".Trim(), SqlDbType.VarChar, 1),
                                    new SqlParameter("@menu_import		".Trim(), SqlDbType.VarChar, 1),
                                    new SqlParameter("@menu_export		".Trim(), SqlDbType.VarChar, 1),
                                    new SqlParameter("@menu_initialize	".Trim(), SqlDbType.VarChar, 1),
                                    new SqlParameter("@menu_newadd		".Trim(), SqlDbType.VarChar, 1),
                                    new SqlParameter("@reg_user			".Trim(), SqlDbType.VarChar, 50)
                                };
                                break;
                        }

                        pDataParameters[0].Value = _UserAccount;
                        pDataParameters[1].Value = fpMain.Sheets[0].GetValue(i, "menu_id         ".Trim()).ToString();
                        pDataParameters[2].Value = fpMain.Sheets[0].GetValue(i, "menu_useyn		".Trim()).ToString() == "True" ? "Y" : "N";
                        pDataParameters[3].Value = fpMain.Sheets[0].GetValue(i, "menu_search		".Trim()).ToString() == "True" ? "Y" : "N";
                        pDataParameters[4].Value = fpMain.Sheets[0].GetValue(i, "menu_print		".Trim()).ToString() == "True" ? "Y" : "N";
                        pDataParameters[5].Value = fpMain.Sheets[0].GetValue(i, "menu_delete		".Trim()).ToString() == "True" ? "Y" : "N";
                        pDataParameters[6].Value = fpMain.Sheets[0].GetValue(i, "menu_save		".Trim()).ToString() == "True" ? "Y" : "N";
                        pDataParameters[7].Value = fpMain.Sheets[0].GetValue(i, "menu_import		".Trim()).ToString() == "True" ? "Y" : "N";
                        pDataParameters[8].Value = fpMain.Sheets[0].GetValue(i, "menu_export		".Trim()).ToString() == "True" ? "Y" : "N";
                        pDataParameters[9].Value = fpMain.Sheets[0].GetValue(i, "menu_initialize	".Trim()).ToString() == "True" ? "Y" : "N";
                        pDataParameters[10].Value = fpMain.Sheets[0].GetValue(i, "menu_newadd    ".Trim()).ToString() == "True" ? "Y" : "N";
                        pDataParameters[11].Value = _Entity.user_account;

                        DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_AuthInformation_A10", pDataParameters);
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
                throw _ExceptionManager;
            }
            catch (Exception _Exception)
            {

                throw new ExceptionManager
                (
                    this,
                    "bool AuthInformation_A10(AuthInformation_Entity _Entity, ref FpSpread fpMain)",
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

        public DataTable AuthInformation_R30()
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
                        pDataParameters = new IDataParameter[] { };
                        break;
                }

                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_AuthInformation_R30", pDataParameters);
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
                    "DataTable AuthInformation_R30()",
                    _Exception
                );
            }
        }

        public bool AuthInformation_A20(string _UserAccount, string _User1, ref xFpSpread fpRight)
        {
            bool _Error = false;

            string cCRUD = string.Empty;
            string vCRUD = string.Empty;

            _pDBManager.BeginTransaction();
            try
            {
                IDataParameter[] pDataParameters = null;

                for (int i = 0; i < fpRight.Sheets[0].Rows.Count; i++)
                {
                    if (fpRight.Sheets[0].GetValue(i, "check").ToString() == "True")
                    {
                        // 기존권한이 존재하면 지우기
                        switch (_pDBManager.DBManagerType.ToString())
                        {
                            case "MySql":
                                pDataParameters = new IDataParameter[] { };
                                break;
                            case "SQLServer":
                                pDataParameters = new IDataParameter[]
                                {
                                    new SqlParameter("@user2    		".Trim(), SqlDbType.VarChar, 50)
                                };
                                break;
                        }

                        pDataParameters[0].Value = fpRight.Sheets[0].GetValue(i, "user_account    ".Trim()).ToString();

                        DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_AuthInformation_A30", pDataParameters);
                        if (_DataTable != null && _DataTable.Rows.Count > 0)
                        {
                            if (_DataTable.Rows[0]["p_err_no"].ToString() != "00")
                            {
                                _Error = true;
                            }
                            else
                            {
                                pDataParameters = null;
                                switch (_pDBManager.DBManagerType.ToString())
                                {
                                    case "MySql":
                                        pDataParameters = new IDataParameter[] { };
                                        break;
                                    case "SQLServer":
                                        pDataParameters = new IDataParameter[]
                                        {
                                    new SqlParameter("@user_account		".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@user1			".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@user2    		".Trim(), SqlDbType.VarChar, 50)
                                        };
                                        break;
                                }

                                pDataParameters[0].Value = _UserAccount;
                                pDataParameters[1].Value = _User1;
                                pDataParameters[2].Value = fpRight.Sheets[0].GetValue(i, "user_account    ".Trim()).ToString();

                                _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_AuthInformation_A20", pDataParameters);
                                if (_DataTable != null && _DataTable.Rows.Count > 0)
                                {
                                    if (_DataTable.Rows[0]["p_err_no"].ToString() != "00")
                                    {
                                        fpRight.Sheets[0].SetValue(i, 0, "True");
                                        fpRight.Sheets[0].SetValue(i, 1, _DataTable.Rows[0][i].ToString());
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
                        else
                        {
                            _Error = true;
                        }
                    }
                }
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
                    "bool AuthInformation_A20(string _UserAccount, string _User1, ref FpSpread fpRight)",
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
