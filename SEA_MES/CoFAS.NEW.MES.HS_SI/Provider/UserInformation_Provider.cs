using FarPoint.Win.Spread;
using CoFAS.NEW.MES.HS_SI.Entity;
using System;
using System.Data;
using System.Data.SqlClient;
using CoFAS.NEW.MES.Core.Function;
using CoFAS.NEW.MES.Core;

namespace CoFAS.NEW.MES.HS_SI.Provider
{
    public class UserInformation_Provider : EntityManager<UserInformation_Entity>
    {
        public override UserInformation_Entity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public UserInformation_Provider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }

        public DataTable UserInformation_R10(UserInformation_Entity _Entity)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        break;
                    case "SQLServer":
                        pDataParameters = new IDataParameter[]
                        {
                            new SqlParameter("@Dept", SqlDbType.VarChar, 10),
                            new SqlParameter("@User", SqlDbType.VarChar, 100),
                            //new SqlParameter("@Factory", SqlDbType.VarChar, 10),
                            //new SqlParameter("@Title", SqlDbType.VarChar, 10)
                        };
                        break;
                }

                pDataParameters[0].Value = _Entity._SearchDept;
                pDataParameters[1].Value = _Entity._SearchUser;
                //pDataParameters[2].Value = _Entity._SearchFactory;
                //pDataParameters[3].Value = _Entity._SearchTitle;

                DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_UserInformation_R10", pDataParameters);
                return _DataTable;
            }
            catch(ExceptionManager _ExceptionManager)
            {
                throw _ExceptionManager;
            }
            catch(Exception _Exception)
            {
                throw new ExceptionManager
                (
                    this,
                    "DataTable UserInformation_R10(UserInformation_Entity _Entity)",
                    _Exception
                );
            }
        }

        public bool UserInformation_A10(UserInformation_Entity _Entity, ref xFpSpread fpMain)
        {
            bool _Error = false;

            string cCRUD = string.Empty;
            string vCRUD = string.Empty;

            _pDBManager.BeginTransaction();

            try
            {
                IDataParameter[] pDataParameters = null;

                for(int i = 0; i < fpMain.Sheets[0].Rows.Count; i++)
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

                    if(vCRUD != "")
                    {
                        switch (_pDBManager.DBManagerType.ToString())
                        {
                            case "MySql":
                                break;
                            case "SQLServer":
                                pDataParameters = new IDataParameter[]
                                {
                                    new SqlParameter("@CRUD			 ".Trim(),  SqlDbType.VarChar, 1),
                                    new SqlParameter("@user_account  ".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@user_password ".Trim(), SqlDbType.VarChar, 1000),
                                    new SqlParameter("@user_name     ".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@user_authority".Trim(), SqlDbType.VarChar, 10),
                                    new SqlParameter("@user_factory  ".Trim(), SqlDbType.VarChar, 10),
                                    new SqlParameter("@user_dept     ".Trim(), SqlDbType.VarChar, 10),
                                    new SqlParameter("@user_title    ".Trim(), SqlDbType.VarChar, 10),
                                    new SqlParameter("@user_mail     ".Trim(), SqlDbType.VarChar, 1000),
                                    new SqlParameter("@user_phone    ".Trim(), SqlDbType.VarChar, 15),
                                    new SqlParameter("@user_fax      ".Trim(), SqlDbType.VarChar, 15),
                                    new SqlParameter("@user_in_tel   ".Trim(), SqlDbType.VarChar, 15),
                                    new SqlParameter("@user_entry    ".Trim(), SqlDbType.VarChar, 15),
                                    new SqlParameter("@user_leave    ".Trim(), SqlDbType.VarChar, 15),
                                    new SqlParameter("@user_emp      ".Trim(), SqlDbType.VarChar, 20),
                                    new SqlParameter("@description   ".Trim(), SqlDbType.VarChar, 2000),
                                    new SqlParameter("@reg_user      ".Trim(), SqlDbType.VarChar, 50)
                                };
                                break;
                        }

                        pDataParameters[0].Value = vCRUD;
                        pDataParameters[1].Value  = fpMain.Sheets[0].GetValue(i, "user_account  ".Trim()).ToString();
                        pDataParameters[2].Value  = SHA256Encryption.Encrypt("123!@#QW");
                        pDataParameters[3].Value  = fpMain.Sheets[0].GetValue(i, "user_name     ".Trim()).ToString();
                        pDataParameters[4].Value  = fpMain.Sheets[0].GetValue(i, "user_authority".Trim()).ToString();
                        pDataParameters[5].Value  = fpMain.Sheets[0].GetValue(i, "user_factory  ".Trim()).ToString();
                        pDataParameters[6].Value  = fpMain.Sheets[0].GetValue(i, "user_dept     ".Trim()).ToString();
                        pDataParameters[7].Value  = fpMain.Sheets[0].GetValue(i, "user_title    ".Trim()).ToString();
                        pDataParameters[8].Value  = fpMain.Sheets[0].GetValue(i, "user_mail     ".Trim()).ToString();
                        pDataParameters[9].Value  = fpMain.Sheets[0].GetValue(i, "user_phone    ".Trim()).ToString();
                        pDataParameters[10].Value = fpMain.Sheets[0].GetValue(i, "user_fax      ".Trim()).ToString();
                        pDataParameters[11].Value = fpMain.Sheets[0].GetValue(i, "user_in_tel   ".Trim()).ToString();
                        pDataParameters[12].Value = fpMain.Sheets[0].GetValue(i, "user_entry    ".Trim()).ToString();
                        pDataParameters[13].Value = fpMain.Sheets[0].GetValue(i, "user_leave    ".Trim()).ToString();
                        pDataParameters[14].Value = fpMain.Sheets[0].GetValue(i, "user_emp      ".Trim()).ToString();
                        pDataParameters[15].Value = fpMain.Sheets[0].GetValue(i, "description   ".Trim()).ToString();
                        pDataParameters[16].Value = _Entity.user_account;
       

                        DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_UserInformation_A10", pDataParameters);
                        if(_DataTable != null && _DataTable.Rows.Count > 0)
                        {
                            if (_DataTable.Rows[0]["err_no"].ToString() != "00")
                            {
                                fpMain.Sheets[0].SetValue(i, "error     ".Trim(), true);
                                fpMain.Sheets[0].SetValue(i, "error_name".Trim(), _DataTable.Rows[0][i].ToString());
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

        public bool UserInformation_A20(UserInformation_Entity _Entity, ref xFpSpread fpMain)
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
                    if (fpMain.Sheets[0].GetValue(i, "check     ".Trim()).ToString() == "True")
                    {
                        switch (_pDBManager.DBManagerType.ToString())
                        {
                            case "MySql":
                                break;
                            case "SQLServer":
                                pDataParameters = new IDataParameter[]
                                {
                                    new SqlParameter("@reg_account      ".Trim(), SqlDbType.VarChar, 50),
                                    new SqlParameter("@user_account     ".Trim(), SqlDbType.VarChar, 50),
                                 
                                };
                                break;
                        }


                        pDataParameters[0].Value = _Entity.user_account;
                        pDataParameters[1].Value = fpMain.Sheets[0].GetValue(i, "user_account").ToString();
       

                        string sql = $@"declare
	                                  @p_err_no			    varchar(100)	= '00'
	                                 ,@p_err_msg			varchar(100)	= ''
	                                 ,@p_rtn_key			varchar(100)	= ''
	                                 ,@p_rtn_other		    varchar(100)	= ''
	                                 
	                                 update	 User_Mst
	                                 set		 user_leave		= convert(varchar, getdate(), 112)
	                                 		,use_yn			= 'N'
	                                 		,up_date		= getdate()
	                                 		,up_user		= @reg_account
	                                 where	 user_account	= @user_account
                                     
	                                 if @@rowcount = 0
	                                 begin
	                                 	set @p_err_msg	= '삭제오류'
	                                 	set @p_err_no	= '10'
	                                 end
                                     
	                                 select	 @p_err_msg	as err_msg
	                                 		,@p_err_no	as err_no
	                                 		,@p_rtn_key	as rtn_key";




                        DataTable _DataTable = _pDBManager.GetDataTable(CommandType.Text,sql , pDataParameters);
                        if (_DataTable != null && _DataTable.Rows.Count > 0)
                        {
                            if (_DataTable.Rows[0]["err_no"].ToString() != "00")
                            {
                                fpMain.Sheets[0].SetValue(i, "error     ".Trim(), true);
                                fpMain.Sheets[0].SetValue(i, "error_name".Trim(), _DataTable.Rows[0][i].ToString());
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
                    "DataTable UserInformation_A20(UserInformation_Entity _Entity, ref FpSpread fpMain)",
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

