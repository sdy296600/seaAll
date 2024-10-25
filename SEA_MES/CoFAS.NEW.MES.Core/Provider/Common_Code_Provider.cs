
using CoFAS.NEW.MES.Core.Entity;
using CoFAS.NEW.MES.Core.Function;
using FarPoint.Win.Spread;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CoFAS.NEW.MES.Core.Business
{

        class Common_Code_Provider : EntityManager<Common_Code_Entity>
        {
            public override Common_Code_Entity GetEntity(DataRow pDataRow)
            {
                throw new NotImplementedException();
            }

            public Common_Code_Provider(DBManager pDBManager)
            {
                _pDBManager = pDBManager;
            }

            public DataTable Common_Code_R10(Common_Code_Entity _Entity)
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
                            new SqlParameter("@code_type", SqlDbType.VarChar, 50)

                            };
                            break;
                    }

                    pDataParameters[0].Value = _Entity.code_type;

                    DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_CommonCode_R10", pDataParameters);
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
                        "Common_Code_R10()",
                        _Exception
                    );
                }
            }

        public DataTable Common_Code_R20(Common_Code_Entity _Entity)
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
                            new SqlParameter("@code_type", SqlDbType.VarChar, 50)

                        };
                        break;
                }

                pDataParameters[0].Value = _Entity.code_type;

                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_CommonCode_R20", pDataParameters);
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
                    "Common_Code_R10()",
                    _Exception
                );
            }
        }



        public bool Common_Code_A10(Common_Code_Entity _Entity, ref xFpSpread fpMain)
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
                                    new SqlParameter("@user_account	    ".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@code_type		".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@code			    ".Trim(), SqlDbType.VarChar, 100),
                                    new SqlParameter("@code_name		".Trim(), SqlDbType.VarChar, 100),
                                    };
                                    break;
                            }

                            pDataParameters[0].Value = _Entity.user_account;
                            pDataParameters[1].Value = fpMain.Sheets[0].GetValue(i, "code_type".Trim()).ToString();
                            pDataParameters[2].Value = fpMain.Sheets[0].GetValue(i, "code     ".Trim()).ToString();
                            pDataParameters[3].Value = fpMain.Sheets[0].GetValue(i, "code_name".Trim()).ToString();


                            DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_CommonCode_A10", pDataParameters);
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
                        "Common_Code_A10(Common_Code_Entity _Entity, ref FpSpread fpMain)",
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

            //삭제용
            public bool Common_Code_A15(Common_Code_Entity _Entity)
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
                            new SqlParameter("@code_type", SqlDbType.VarChar, 50),
                            new SqlParameter("@code", SqlDbType.VarChar, 50),
                            new SqlParameter("@user_account", SqlDbType.VarChar, 50)
                            };
                            break;
                    }

                    pDataParameters[0].Value = _Entity.code_type;
                    pDataParameters[1].Value = _Entity.code;
                    pDataParameters[2].Value = _Entity.user_account;

                    DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_CommonCode_A15", pDataParameters);
                    if (_DataTable != null && _DataTable.Rows.Count > 0)
                    {
                        if (_DataTable.Rows[0]["p_err_no"].ToString() != "00")
                            _Error = true;
                    }
                    else
                    {
                        _Error = true;
                    }

                    return _Error;
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
                        "CommonCode_A15(CommonCode_Entity _Entity)",
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
            }
            public bool Common_Code_A20(Common_Code_Entity _Entity, FpSpread fpSub1)
            {
                bool _Error = false;
                string cCRUD = string.Empty;
                string vCRUD = string.Empty;

                _pDBManager.BeginTransaction();

                try
                {
                    IDataParameter[] pDataParameters = null;

                    for (int i = 0; i < fpSub1.Sheets[0].Rows.Count; i++)
                    {
                        cCRUD = fpSub1.Sheets[0].RowHeader.Cells[i, 0].Text;

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
                                new SqlParameter("@user_account		".Trim(), SqlDbType.VarChar, 50),
                                new SqlParameter("@code_type		".Trim(), SqlDbType.VarChar, 50),
                                new SqlParameter("@code     		".Trim(), SqlDbType.VarChar, 10),
                                new SqlParameter("@code_name		".Trim(), SqlDbType.VarChar, 100)
                                    };
                                    break;
                            }

                            pDataParameters[0].Value = _Entity.user_account;
                            pDataParameters[1].Value = fpSub1.Sheets[0].GetText(i, "code_type	".Trim()).ToString();
                            pDataParameters[2].Value = fpSub1.Sheets[0].GetText(i, "code		".Trim()).ToString();
                            pDataParameters[3].Value = fpSub1.Sheets[0].GetText(i, "code_name	".Trim()).ToString();

                            DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_CommonCode_A20", pDataParameters);
                            if (_DataTable != null && _DataTable.Rows.Count > 0)
                            {
                                if (_DataTable.Rows[0]["p_err_no"].ToString() != "00")
                                {
                                    fpSub1.Sheets[0].SetValue(i, 0, "True");
                                    fpSub1.Sheets[0].SetValue(i, 1, _DataTable.Rows[0][i].ToString());
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
                        "CommonCode_A20(CommonCode_Entity _Entity, ref FpSpread fpSub1)",
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

            public bool Common_Code_A30(Common_Code_Entity _Entity, FpSpread fpSub2)
            {
                bool _Error = false;
                string cCRUD = string.Empty;
                string vCRUD = string.Empty;

                _pDBManager.BeginTransaction();

                try
                {
                    IDataParameter[] pDataParameters = null;

                    for (int i = 0; i < fpSub2.Sheets[0].Rows.Count; i++)
                    {
                        cCRUD = fpSub2.Sheets[0].RowHeader.Cells[i, 0].Text;

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
                                new SqlParameter("@user_account	   ".Trim(), SqlDbType.VarChar, 50),
                                new SqlParameter("@code_type	   ".Trim(), SqlDbType.VarChar, 50),
                                new SqlParameter("@code			   ".Trim(), SqlDbType.VarChar, 10),
                                new SqlParameter("@code_name	   ".Trim(), SqlDbType.VarChar, 100),
                                new SqlParameter("@code_description".Trim(), SqlDbType.VarChar, 500),
                                new SqlParameter("@code_etc1	   ".Trim(), SqlDbType.VarChar, 500),
                                new SqlParameter("@code_etc2	   ".Trim(), SqlDbType.VarChar, 500),
                                new SqlParameter("@code_etc3	   ".Trim(), SqlDbType.VarChar, 500),
                                new SqlParameter("@description	   ".Trim(), SqlDbType.VarChar, 2000),
                                new SqlParameter("@sort			   ".Trim(), SqlDbType.Int)
                                    };
                                    break;
                            }

                            pDataParameters[0].Value = _Entity.user_account;
                            pDataParameters[1].Value = fpSub2.Sheets[0].GetText(i, "CODE_TYPE		".Trim()).ToString();
                            pDataParameters[2].Value = fpSub2.Sheets[0].GetText(i, "CODE		    ".Trim()).ToString();
                            pDataParameters[3].Value = fpSub2.Sheets[0].GetText(i, "CODE_NAME		".Trim()).ToString();
                            pDataParameters[4].Value = fpSub2.Sheets[0].GetText(i, "CODE_DESCRIPTION".Trim()).ToString();
                            pDataParameters[5].Value = fpSub2.Sheets[0].GetText(i, "CODE_ETC1		".Trim()).ToString();
                            pDataParameters[6].Value = fpSub2.Sheets[0].GetText(i, "CODE_ETC2		".Trim()).ToString();
                            pDataParameters[7].Value = fpSub2.Sheets[0].GetText(i, "CODE_ETC3		".Trim()).ToString();
                            pDataParameters[8].Value = fpSub2.Sheets[0].GetText(i, "DESCRIPTION		".Trim()).ToString();
                            pDataParameters[9].Value = fpSub2.Sheets[0].GetValue(i, "SORT			".Trim()) == null ? 0 : fpSub2.Sheets[0].GetValue(i, "SORT").ToString() == "" ? 0 : Convert.ToInt32(fpSub2.Sheets[0].GetValue(i, "SORT").ToString());

                            DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_CommonCode_A30", pDataParameters);
                            if (_DataTable != null && _DataTable.Rows.Count > 0)
                            {
                                if (_DataTable.Rows[0]["p_err_no"].ToString() != "00")
                                {
                                    fpSub2.Sheets[0].SetValue(i, 0, "True");
                                    fpSub2.Sheets[0].SetValue(i, 1, _DataTable.Rows[0][i].ToString());
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
                        "CommonCode_A30(CommonCode_Entity _Entity, ref FpSpread fpSub2)",
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
