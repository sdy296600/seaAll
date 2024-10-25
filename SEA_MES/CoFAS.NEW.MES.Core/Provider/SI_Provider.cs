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
using CoFAS.NEW.MES.Core.Business;

namespace CoFAS.NEW.MES.Core.Provider
{


    public class SI_Provider : EntityManager<MenuSave_Entity>
    {
        public SI_Provider(DBManager pDBManager)
        {
            _pDBManager = pDBManager;
        }


        public override MenuSave_Entity GetEntity(DataRow pDataRow)
        {
            throw new NotImplementedException();
        }

        public bool 수주관리_PopupBox(xFpSpread fpMain)
        {
            bool _Error = false;
            try
            {
                _pDBManager.BeginTransaction();
                IDataParameter[] pDataParameters = null;

                for (int i = 0; i < fpMain.Sheets[0].RowCount; i++)
                {

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
                                   new SqlParameter("@OUT_CODE   	".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@NAME   	    ".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@ORDER_DATE   	".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@COMPANY_ID ".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@STOCK_MST_ID  ".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@DEMAND_DATE   ".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@QTY           ".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@REG_USER   	".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@REG_DATE   	".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@UP_USER   	".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@UP_DATE       ".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@ORDER_TYPE        ".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@STOP_YN           ".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@STOCK_MST_PRICE  	".Trim(), SqlDbType.VarChar, 50),
                                   new SqlParameter("@CONVERSION_UNIT  	".Trim(), SqlDbType.VarChar, 50),
                            };
                            break;
                    }


                    pDataParameters[0].Value = fpMain.Sheets[0].GetValue(i, "발주번호               ".Trim()).ToString();
                    pDataParameters[1].Value = fpMain.Sheets[0].GetValue(i, "프로젝트명             ".Trim()).ToString();
                    pDataParameters[2].Value = Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "발주일자	            ".Trim())).ToString("yyy-MM-dd");
                    pDataParameters[3].Value = fpMain.Sheets[0].GetValue(i, "고객사	                ".Trim()).ToString();
                    pDataParameters[4].Value = fpMain.Sheets[0].GetValue(i, "STOCK_MST_OUT_CODE	    ".Trim()).ToString();
                    pDataParameters[5].Value = Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "납기일자		        ".Trim())).ToString("yyy-MM-dd");
                    pDataParameters[6].Value = fpMain.Sheets[0].GetValue(i, "수량                   ".Trim()).ToString();
                    pDataParameters[7].Value = fpMain.Sheets[0].GetValue(i, "REG_USER               ".Trim()).ToString();
                    pDataParameters[8].Value = Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "REG_DATE               ".Trim())).ToString("yyy-MM-dd HH:mm:ss");
                    pDataParameters[9].Value = fpMain.Sheets[0].GetValue(i, "UP_USER                ".Trim()).ToString();
                    pDataParameters[10].Value = Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "UP_DATE                ".Trim())).ToString("yyy-MM-dd HH:mm:ss");
                    pDataParameters[11].Value = fpMain.Sheets[0].GetValue(i, "구분                   ".Trim()).ToString();
                    pDataParameters[12].Value = fpMain.Sheets[0].GetValue(i, "진행상황               ".Trim()).ToString();
                    pDataParameters[13].Value = fpMain.Sheets[0].GetValue(i, "단가                   ".Trim()).ToString();
                    pDataParameters[14].Value = fpMain.Sheets[0].GetValue(i, "단위                   ".Trim()).ToString();
                    int Execute =_pDBManager.Execute(CommandType.StoredProcedure, "Order_Mst_PopupBox", pDataParameters);

                    if (Execute == 0)
                    {
                        _Error = true;
                    }
                    else
                    {
                        _Error = false;
                    }

                    pDataParameters = null;

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
        public DataTable EQUIPMENT_INSPECTION_RECORD_R10(int pID, string pTYPE, string pCHECK_DATE, string pUSER)
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
                            new SqlParameter("@ID                ".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@TYPE              ".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@CHECK_DATE        ".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@USER              ".Trim(), SqlDbType.VarChar, 50)
                        };
                        break;
                }

                pDataParameters[0].Value = pID;
                pDataParameters[1].Value = pTYPE;
                pDataParameters[2].Value = pCHECK_DATE;
                pDataParameters[3].Value = pUSER;


                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "[dbo].[USP_EQUIPMENT_INSPECTION_RECORD_R10]", pDataParameters);
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
        public DataTable EQUIPMENT_INSPECTION_RECORD_R10(Panel _Panel, MenuSettingEntity _MenuSettingEntity)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                StringBuilder sql = new StringBuilder();

                sql.Append($@"SELECT 
                                 TYPE
                                ,EQUIPMENT_ID
                                ,EQUIPMENT_NAME
                                ,CHECK_DATE
                                FROM [dbo].[EQUIP_INSPECTION_LIST]        
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


                sql.Append("GROUP BY TYPE ,EQUIPMENT_ID,EQUIPMENT_NAME,CHECK_DATE");
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
        public DataTable USP_Production_Progress_Status_R10()
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

                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {

                        };
                        break;
                    default:
                        break;
                }

                string str = @"select 
  D.OUT_CODE AS 제품코드
 ,D.NAME     AS 제품명
 ,A.작업시간
 --,A.NOW_END
 ,CONVERT(decimal(18, 2), (DATEDIFF(SECOND,
 CASE WHEN A.NOW_START > C.START_INSTRUCT_DATE
 THEN A.NOW_START
 ELSE C.START_INSTRUCT_DATE
 END
 , CASE WHEN C.END_INSTRUCT_DATE IS NULL
 THEN
 CASE  WHEN A.NOW_END > GETDATE()
 THEN GETDATE()
 ELSE A.NOW_END
 END
 ELSE
 CASE
 WHEN A.NOW_END < C.END_INSTRUCT_DATE
 THEN A.NOW_END
 ELSE C.END_INSTRUCT_DATE
 END
 END) / E.CYCLE_TIME)*FRACTION_DEFECTIVE) AS 계획
 ,COUNT(B.ID)+ISNULL(F.TOTAL_QTY, 0) as 실적
 ,SUM(ISNULL(COUNT(B.ID), 0) + ISNULL(F.TOTAL_QTY, 0)) OVER(ORDER BY  A.sort) AS 누적
 FROM(
 SELECT a.code, a.code_etc1 + ' ~ ' + a.code_etc2 + '[' + a.code_etc3 + '분]' AS 작업시간
 , DATEADD(MINUTE, Convert(int, a.code_description),
 (SELECT  CASE
 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'
 THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
 ELSE FORMAT(DATEADD(DAY, -1, GETDATE()), 'yyyy-MM-dd')
 END)) as 'NOW_START'
,DATEADD(MINUTE, (Convert(int, a.code_description) + Convert(int, a.code_etc3)),
 (SELECT  CASE
 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'
 THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
 ELSE FORMAT(DATEADD(DAY, -1, GETDATE()), 'yyyy-MM-dd')
 END)) as 'NOW_END'
 ,A.SORT
 FROM [dbo].[Code_Mst] a
  WHERE 1 = 1
   AND a.code_type = 'CD15'
   ) A
 INNER JOIN[dbo].[PRODUCTION_INSTRUCT] C ON FORMAT(C.INSTRUCT_DATE, 'yyyy-MM-dd') = FORMAT(GETDATE(), 'yyyy-MM-dd')
 AND C.USE_YN = 'Y'
 AND C.START_INSTRUCT_DATE IS NOT NULL
 AND(A.NOW_START BETWEEN C.START_INSTRUCT_DATE AND ISNULL(C.END_INSTRUCT_DATE, GETDATE()) OR
      A.NOW_END BETWEEN C.START_INSTRUCT_DATE AND ISNULL(C.END_INSTRUCT_DATE, GETDATE()))
 LEFT JOIN
 (
    SELECT A.STOCK_MST_ID, A.ID, B.READ_DATE
   FROM [dbo].[PRODUCTION_INSTRUCT] A
   LEFT JOIN[dbo].[OPC_MST_OK] B ON B.READ_DATE BETWEEN A.START_INSTRUCT_DATE AND ISNULL(A.END_INSTRUCT_DATE, GETDATE())
   WHERE 1 = 1
   AND A.USE_YN = 'Y'
   AND FORMAT(A.INSTRUCT_DATE,'yyyy-MM-dd')  = (SELECT  CASE
 WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'
 THEN FORMAT(GETDATE(), 'yyyy-MM-dd') 
 ELSE FORMAT(DATEADD(DAY,-1, GETDATE()), 'yyyy-MM-dd') 
 END )
 ) B ON B.ID = C.ID AND B.READ_DATE BETWEEN A.NOW_START AND A.NOW_END
 LEFT JOIN[dbo].[STOCK_MST] D ON C.STOCK_MST_ID = D.ID
 LEFT JOIN [dbo].[WORK_CAPA] E ON C.WORK_CAPA_WORKING_HR_SHIFT = E.ID
 LEFT JOIN
 (
 SELECT PRODUCTION_INSTRUCT_ID, SUM(TOTAL_QTY) AS TOTAL_QTY
 FROM [dbo].[PRODUCTION_RESULT]
 GROUP BY PRODUCTION_INSTRUCT_ID
 ) F ON B.ID = F.PRODUCTION_INSTRUCT_ID
   GROUP BY A.code,A.작업시간,A.NOW_START,A.NOW_END, A.sort,D.NAME,D.OUT_CODE,E.CYCLE_TIME,C.END_INSTRUCT_DATE,C.START_INSTRUCT_DATE,F.TOTAL_QTY,FRACTION_DEFECTIVE
   ORDER BY A.sort";


                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.Text, str, pIDataParameter);
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
        public DataTable WORKING_CONDITIONS_STATUS2_R10(DateTime pSTART_DATE, DateTime pEND_DATE)
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
                            new SqlParameter("@START_DATE".Trim(), SqlDbType.VarChar, 50),
                            new SqlParameter("@END_DATE  ".Trim(), SqlDbType.VarChar, 50)

                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {

                        };
                        break;
                    default:
                        break;
                }
                pIDataParameter[0].Value = pSTART_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                pIDataParameter[1].Value = pEND_DATE.ToString("yyyy-MM-dd HH:mm:ss");

                string sql =  $@"SELECT *
                                 FROM[dbo].[OPC_MST]
                                 WHERE READ_DATE BETWEEN @START_DATE AND @END_DATE;";
                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.Text, sql, pIDataParameter);
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
        public DataTable PROCESS_NG_STATUS_R10(string code)
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
                            new SqlParameter("@CODE".Trim(), SqlDbType.VarChar, 50),
                        };
                        break;
                    case "MySql":
                        pIDataParameter = new IDataParameter[]
                        {

                        };
                        break;
                    default:
                        break;
                }
                pIDataParameter[0].Value = code;


                DataTable pDataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_PROCESS_NG_STATUS_R10", pIDataParameter);
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
        public DataTable MENU_INFORMATION_R10(string _mst_id, string str)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                StringBuilder sql = new StringBuilder();

                string[] strs = str.Split('/');

                sql.Append($@"select *
                                 from {strs[1]}
                                 where 1 = 1 
                                  and P_MENU_ID = {_mst_id}");


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
                sql.Append($"ORDER BY SORT");
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
        public bool MENU_INFORMATION_A10(MenuSettingEntity _Entity, xFpSpread fpMain, string BASE_TABLE)
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
                            sql.Append($@"UPDATE {BASE_TABLE} SET ");
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
                                        string tag = fpMain.Sheets[0].Columns[x].Tag.ToString();
                                        if (tag != "MENU_ID" && tag != "UP_DATE" && tag != "UP_USER")
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
                                                    sql3.SqlValue = fpMain.Sheets[0].GetValue(i, x);
                                                    break;
                                                case "ButtonCellType":

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
                                                    sql3 = new SqlParameter($"@{tag}".Trim(), SqlDbType.VarChar, 500);
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
                                    sql.Append($"WHERE MENU_ID = {fpMain.Sheets[0].Rows[i].GetValue("MENU_ID").ToString()} ");

                                }
                                if (cCRUD == "입력")
                                {
                                    StringBuilder into = new StringBuilder();
                                    into.Append($"(");

                                    for (int x = 0; x < fpMain.Sheets[0].ColumnCount; x++)
                                    {
                                        string tag = fpMain.Sheets[0].Columns[x].Tag.ToString();
                                        if (tag != "ID" && tag != "MENU_ID")
                                        {
                                            if (fpMain.Sheets[0].Columns[x].CellType.ToString() != "ButtonCellType" &&
                                                fpMain.Sheets[0].Columns[x].CellType.ToString().Contains("Display") == false)
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
                                    into.Append($") VALUES ");

                                    StringBuilder values = new StringBuilder();

                                    values.Append($"(");
                                    for (int x = 0; x < fpMain.Sheets[0].ColumnCount; x++)
                                    {
                                        string tag = fpMain.Sheets[0].Columns[x].Tag.ToString();
                                        if (tag != "MENU_ID")
                                        {

                                            //if (values.Length == 1)
                                            //{
                                            //    values.Append($"@{tag} ");
                                            //}
                                            //else
                                            //{
                                            //    values.Append($",@{tag} ");
                                            //}
                                            SqlParameter sql1 = null ;

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
                                                    sql1.SqlValue = fpMain.Sheets[0].GetValue(i, x);
                                                    break;
                                                case "ButtonCellType":

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
                                                    sql1 = new SqlParameter($"@{tag}".Trim(), SqlDbType.VarChar, 500);
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


                                    values.Append($")");

                                    sql.Append(into);
                                    sql.Append(values);
                                }
                                if (cCRUD == "삭제")
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

                                    sql.Append($"WHERE MENU_ID = {fpMain.Sheets[0].Rows[i].GetValue("MENU_ID").ToString()} ");

                                    break;
                                }
                                break;
                        }

                        pDataParameters = new IDataParameter[list.Count];

                        for (int x = 0; x < pDataParameters.Length; x++)
                        {
                            pDataParameters[x] = list[x];
                        }


                        int Execute =_pDBManager.Execute(CommandType.Text, sql.ToString(), pDataParameters);

                        // DataTable _DataTable = _pDBManager.GetDataTable(CommandType.StoredProcedure, "USP_UserInformation_A10", pDataParameters);
                        if (Execute == 0)
                        {
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
        public DataTable PRODUCTION_PLAN_R10(string _mst_id, string str)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                StringBuilder sql = new StringBuilder();

                string[] strs = str.Split('/');

                sql.Append($@"select *
                                 from {strs[1]}
                                 where 1 = 1 
                                  and STOCK_MST_ID = {_mst_id}");


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
                sql.Append(" and COMPLETE_YN != 'Y'");
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
        public DataTable PRODUCTION_ORDER_R10(Panel _Panel)
        {
            try
            {
                IDataParameter[] pDataParameters = null;

                StringBuilder sql = new StringBuilder();

                //string[] strs = str.Split('/');

                //where A.RESOURCE_NO = { resource_no}
                //and A.order_date BETWEEN { date_from}
                //AND { date_to};

                sql.Append($@"select *
                                 from [sea_mfg].[dbo].[demand_mstr] A
INNER JOIN [HS_MES].[dbo].[WORK_RECYCLE] B ON A.RESOURCE_NO = B.RESOURCE_NO
AND A.LOT = B.LOT_NO
");


                switch (_pDBManager.DBManagerType.ToString())
                {
                    case "MySql":
                        pDataParameters = new IDataParameter[]
                        {

                        };
                        break;

                    case "SQLServer":
                        List<SqlParameter> list = new List<SqlParameter>();

                        for (int i = 0; i < _Panel.Controls.Count; i++)
                        {
                            string _par = _Panel.Controls[i].Name;

                            switch (_Panel.Controls[i].GetType().Name)
                            {
                                case "Base_textbox":
                                    Base_textbox base_Textbox = _Panel.Controls[i] as Base_textbox;
                                    if (base_Textbox.SearchText.Length > 0)
                                    {
                                        sql.Append($" and A.{_par} like @{_par} ");
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

                                    sql.Append($" and A.{_par} BETWEEN  @{_par + "1"} and @{_par + "2"} ");
                                    SqlParameter sql3 = new SqlParameter($"@{_par + "1"}".Trim(), SqlDbType.VarChar, 50);
                                    SqlParameter sql4 = new SqlParameter($"@{_par + "2"}".Trim(), SqlDbType.VarChar, 50);

                                    sql3.SqlValue = _FromtoDateTime.StartValue.ToString("yyyy-MM-dd HH:mm");
                                    sql4.SqlValue = _FromtoDateTime.EndValue.ToString("yyyy-MM-dd HH:mm");
                                    list.Add(sql3);
                                    list.Add(sql4);
                                    //sql1 = new SqlParameter($"@{_par}".Trim(), SqlDbType.VarChar, 50);
                                    //sql1.SqlValue = Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, x)).ToString("yyyy-MM-dd HH:mm:ss");
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

                //sql.Append(" and USE_YN = 'Y'");
                //sql.Append(" and COMPLETE_YN != 'Y'");
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

        public bool IN_STOCK_DETAIL_POPUPBOX_A10(xFpSpread fpMain, string user)
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

                    if (cCRUD != "")
                    {

                        if (cCRUD == "수정")
                        {
                            string str = $@"INSERT 
                        INTO OUT_STOCK_DETAIL
                        (
                           OUT_CODE
                          ,OUT_STOCK_DATE
                          ,OUT_TYPE
                          ,ORDER_DETAIL_ID
                          ,OUT_STOCK_WAIT_DETAIL_ID
                          ,STOCK_MST_ID
                          ,STOCK_MST_OUT_CODE
                          ,STOCK_MST_STANDARD
                          ,STOCK_MST_TYPE
                          ,OUT_QTY
                          ,COMPLETE_YN
                          ,USE_YN
                          ,IN_STOCK_DETAIL_ID
                          ,REG_USER
                          ,REG_DATE
                          ,UP_USER
                          ,UP_DATE
                        )
                        values
                        (
                            '{fpMain.Sheets[0].GetValue(i, "OUT_CODE").ToString()}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'{"SD14003"}'
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "ORDER_DETAIL_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "OUT_STOCK_WAIT_DETAIL_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "OUT_QTY").ToString())}
                           ,'N'
                           ,'Y'
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "ID").ToString())}
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        )";

                            //list.Add(str);
                            sql.Append(str);
                        }

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
        public bool 외주작업지시_반출_A10(xFpSpread fpMain, string user, string ID)
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

                    string type = fpMain.Sheets[0].GetValue(i, "TYPE").ToString();

                    if (cCRUD != "")
                    {

                        if (cCRUD == "입력")
                        {
                            string str = $@"INSERT 
                        INTO OUT_STOCK_DETAIL
                        (
                           OUT_CODE
                          ,OUT_STOCK_DATE
                          ,OUT_TYPE
                          ,STOCK_MST_ID
                          ,STOCK_MST_OUT_CODE
                          ,STOCK_MST_STANDARD
                          ,STOCK_MST_TYPE
                          ,OUT_QTY
                          ,COMPLETE_YN
                          ,USE_YN
                          ,IN_STOCK_DETAIL_ID
                          ,INSTRUCT_ID
                          ,REG_USER
                          ,REG_DATE
                          ,UP_USER
                          ,UP_DATE
                        )
                        values
                        (
                            '{fpMain.Sheets[0].GetValue(i, "OUT_CODE").ToString()}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'SD14002'
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "SUB_STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "SUB_STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "SUB_STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "SUB_STOCK_MST_ID").ToString())}
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "OUT_QTY").ToString())}
                           ,'N'
                           ,'Y'
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "IN_STOCK_DETAIL_ID").ToString())}
                           ,{Convert.ToInt32(ID)}
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        )";

                            //list.Add(str);
                            sql.Append(str);
                        }

                        else if (cCRUD == "수정")
                        {
                            string str = $@"UPDATE 
                        OUT_STOCK_DETAIL 
                        set 
                        OUT_STOCK_DATE = '{DateTime.Now.ToString("yyyy-MM-dd")}'
                        ,OUT_QTY = {Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "OUT_QTY").ToString())}
                        ,UP_USER = '{user}'
                        ,UP_DATE = '{DateTime.Now.ToString("yyyy-MM-dd")}'
                        where ID = '{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "OUT_STOCK_DETAIL_ID").ToString())}'
                        ";

                            //list.Add(str);
                            sql.Append(str);
                        }

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
        public bool OUT_SOURCING_IN_STOCK_POPUPBOX_A10(xFpSpread fpMain, string user, string INSTRUCT_ID)
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

                    if (cCRUD != "")
                    {

                        if (cCRUD == "입력")
                        {
                            if (fpMain.Sheets[0].GetValue(i, "INSPECTION_YN").ToString() == "N")
                            {
                                string str = $@"INSERT 
                        INTO IN_STOCK_WAIT_DETAIL
                        (
                          STOCK_MST_ID
                          ,IN_QTY
                          ,COMPLETE_QTY
                          ,COMMENT
                          ,COMPLETE_YN
                          ,INSTRUCT_ID
                          ,USE_YN
                          ,REG_USER
                          ,REG_DATE
                          ,UP_USER
                          ,UP_DATE
                        )
                        values
                        (
                           {Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "IN_QTY").ToString())}
                           ,{Convert.ToDecimal(0)}
                           ,'{fpMain.Sheets[0].GetValue(i, "COMMENT").ToString()}'
                           ,'N'
                           ,'Y'
                           ,{Convert.ToInt32(INSTRUCT_ID)}
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        )";
                            }

                            else if (fpMain.Sheets[0].GetValue(i, "INSPECTION_YN").ToString() == "Y")
                            {
                                string str_test = $@"INSERT 
                        INTO INSPECTION_STOCK
                        (
                           INSPECTION_DATE
                          ,INSPECTION_CODE
                          ,INSPECTION_TYPE
                          ,STOCK_MST_ID
                          ,STOCK_MST_OUT_CODE
                          ,STOCK_MST_TYPE
                          ,STOCK_MST_STANDARD
                          ,INSPECTION_QTY
                          ,OK_YN
                          ,INSTRUCT_ID
                          ,COMPLETE_YN
                          ,REG_USER
                          ,REG_DATE
                          ,UP_USER
                          ,UP_DATE
                        )
                        values
                        (
                           '{DateTime.Now.ToString("yyyy-MM-dd")}'
                            ,'{"외주검사"}'
                           ,'SD22001'
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(i, "IN_QTY").ToString())}
                           ,'SD26001'
                           ,{Convert.ToInt32(INSTRUCT_ID)}
                           ,'N'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        )";

                                //list.Add(str);
                                sql.Append(str_test);
                            }
                        }

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
        public bool INSPECTION_STOCK_REGISTER_RETURN_A10(xFpSpread fpMain, string user)
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

                    if (cCRUD != "")
                    {

                        if (cCRUD == "입력")
                        {
                            string str = $@"INSERT 
                        INTO INSPECTION_STOCK
                        (
                          INSPECTION_DATE
                          ,INSPECTION_CODE
                          ,INSPECTION_TYPE
                          ,STOCK_MST_ID
                          ,STOCK_MST_OUT_CODE
                          ,STOCK_MST_TYPE
                          ,STOCK_MST_STANDARD
                          ,USE_YN
                          ,COMPLETE_YN
                          ,IN_STOCK_DETAIL_ID
                          ,REG_USER
                          ,REG_DATE
                          ,UP_USER
                          ,UP_DATE
                        )
                        values
                        (
                          '{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "INSPECTION_DATE").ToString()).ToString("yyyy-MM-dd")}'
                           ,'{fpMain.Sheets[0].GetValue(i, "INSPECTION_CODE").ToString()}'
                           ,'SD22002'
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "STOCK_MST_ID").ToString())}
                           ,'Y'
                           ,'N'
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "IN_STOCK_DETAIL_ID").ToString())}
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        )";

                            //list.Add(str);
                            sql.Append(str);
                        }

                        else if (cCRUD == "수정")
                        {
                            string str = $@"UPDATE 
                        INSPECTION_STOCK 
                        set 
                        INSPECTION_DATE = '{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "INSPECTION_DATE").ToString())}'
                        ,INSPECTION_CODE = '{fpMain.Sheets[0].GetValue(i, "INSPECTION_CODE").ToString()}'
                        ,UP_USER = '{user}'
                        ,UP_DATE = '{DateTime.Now.ToString("yyyy-MM-dd")}'
                        where ID = '{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "ID").ToString())}'
                        ";

                            //list.Add(str);
                            sql.Append(str);
                        }

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
        public bool INSPECTION_STOCK_REGISTER_RETURN_SUB_A10(xFpSpread fpMain, string user, string mst_id, string in_detail_id)
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

                    cCRUD = fpMain.Sheets[0].RowHeader.Cells[0, 0].Text;

                    if (cCRUD != "")
                    {

                        if (cCRUD == "입력")
                        {
                            string str = $@"INSERT 
                        INTO INSPECTION_STOCK_DETAIL
                        (
                          INSPECTION_STOCK_ID
                          ,OK_QTY
                          ,BAD_QTY
                          ,BAD_COMMENT
                          ,USE_YN
                          ,COMPLETE_YN
                          ,IN_STOCK_DETAIL_ID
                          ,REG_USER
                          ,REG_DATE
                          ,UP_USER
                          ,UP_DATE
                        )
                        values
                        (
                           '{Convert.ToInt32(mst_id)}'
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(0, "OK_QTY").ToString())}
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(0, "BAD_QTY").ToString())}
                           ,'{fpMain.Sheets[0].GetValue(0, "BAD_COMMENT").ToString()}'
                           ,'Y'
                           ,'N'
                           ,'{Convert.ToInt32(in_detail_id)}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        )";

                            //list.Add(str);
                            sql.Append(str);
                        }

                        else if (cCRUD == "수정")
                        {
                            string str = $@"UPDATE 
                        INSPECTION_STOCK 
                        set 
                        INSPECTION_DATE = '{Convert.ToDateTime(fpMain.Sheets[0].GetValue(0, "INSPECTION_DATE").ToString())}'
                        ,INSPECTION_CODE = '{fpMain.Sheets[0].GetValue(0, "INSPECTION_CODE").ToString()}'
                        ,UP_USER = '{user}'
                        ,UP_DATE = '{DateTime.Now.ToString("yyyy-MM-dd")}'
                        where ID = '{Convert.ToInt32(mst_id)}'
                        ";

                            //list.Add(str);
                            sql.Append(str);
                        }

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
        public bool INSTRUMENT_HISTORY_REGISTER_A10(xFpSpread fpMain, string user)
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
                    byte[] bImage;

                    List<SqlParameter> list = new List<SqlParameter>();

                    cCRUD = fpMain.Sheets[0].RowHeader.Cells[i, 0].Text;

                    if (cCRUD != "")
                    {
                        MemoryStream ms = new MemoryStream();

                        Image ima = fpMain.Sheets[0].GetValue(i, "IMAGE") as Image;

                        if (ima != null)
                        {
                            ima.Save(ms, ImageFormat.Bmp);
                        }

                        bImage = ms.ToArray();


                        if (cCRUD == "입력")
                        {
                            string str = $@"INSERT 
                        INTO INSTRUMENT_MST
                        (
                          DATE
                          ,OUT_CODE
                          ,NAME
                          ,STANDARD
                          ,MODEL_NO
                          ,SERIAL_NO
                          ,MAKER
                          ,RECENT_DATE
                          ,NEXT_DATE
                          ,CALIBRATION_CYCLE
                          ,CALIBRATION_CORP
                          ,DEPARTMENT
                          ,USE_LOCATION
                          ,MANAGEMENT_MAIN
                          ,MANAGEMENT_SUB
                          ,CALIBRATION_CHECK
                          ,DISPOSE_DATE
                          ,DISPOSE
                          ,IMAGE
                          ,COMMENT
                          ,USE_YN
                          ,REG_USER
                          ,REG_DATE
                          ,UP_USER
                          ,UP_DATE
                        )
                        values
                        (
                          '{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "DATE").ToString()).ToString("yyyy-MM-dd")}'
                           ,'{fpMain.Sheets[0].GetValue(i, "OUT_CODE").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "NAME").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "STANDARD").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "MODEL_NO").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "SERIAL_NO").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "MAKER").ToString()}'
                           ,'{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "RECENT_DATE").ToString()).ToString("yyyy-MM-dd")}'
                           ,'{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "NEXT_DATE").ToString()).ToString("yyyy-MM-dd")}'
                           ,'{fpMain.Sheets[0].GetValue(i, "CALIBRATION_CYCLE").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "CALIBRATION_CORP").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "DEPARTMENT").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "USE_LOCATION").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "MANAGEMENT_MAIN").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "MANAGEMENT_SUB").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "CALIBRATION_CHECK").ToString()}'
                           ,'{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "DISPOSE_DATE").ToString()).ToString("yyyy-MM-dd")}'
                           ,'{fpMain.Sheets[0].GetValue(i, "DISPOSE").ToString()}'
                           ,{ms.ToArray()}
                           ,'{fpMain.Sheets[0].GetValue(i, "COMMENT").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "USE_YN").ToString()}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        )";

                            //list.Add(str);
                            sql.Append(str);
                        }


                        else if (cCRUD == "수정")
                        {
                            string str = $@"UPDATE 
                        INSTRUMENT_MST 
                        set 
                        DATE = '{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "DATE").ToString()).ToString("yyyy-MM-dd")}'
                        ,OUT_CODE = '{fpMain.Sheets[0].GetValue(i, "OUT_CODE").ToString()}'
                        ,NAME = '{fpMain.Sheets[0].GetValue(i, "NAME").ToString()}'
                        ,STANDARD = '{fpMain.Sheets[0].GetValue(i, "STANDARD").ToString()}'
                        ,MODEL_NO = '{fpMain.Sheets[0].GetValue(i, "MODEL_NO").ToString()}'
                        ,SERIAL_NO = '{fpMain.Sheets[0].GetValue(i, "SERIAL_NO").ToString()}'
                        ,MAKER = '{fpMain.Sheets[0].GetValue(i, "MAKER").ToString()}'
                        ,RECENT_DATE = '{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "RECENT_DATE").ToString()).ToString("yyyy-MM-dd")}'
                        ,NEXT_DATE = '{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "NEXT_DATE").ToString()).ToString("yyyy-MM-dd")}'
                        ,CALIBRATION_CYCLE = '{fpMain.Sheets[0].GetValue(i, "CALIBRATION_CYCLE").ToString()}'
                        ,CALIBRATION_CORP = '{fpMain.Sheets[0].GetValue(i, "CALIBRATION_CORP").ToString()}'
                        ,DEPARTMENT = '{fpMain.Sheets[0].GetValue(i, "DEPARTMENT").ToString()}'
                        ,USE_LOCATION = '{fpMain.Sheets[0].GetValue(i, "USE_LOCATION").ToString()}'
                        ,MANAGEMENT_MAIN = '{fpMain.Sheets[0].GetValue(i, "MANAGEMENT_MAIN").ToString()}'
                        ,MANAGEMENT_SUB = '{fpMain.Sheets[0].GetValue(i, "MANAGEMENT_SUB").ToString()}'
                        ,CALIBRATION_CHECK = '{fpMain.Sheets[0].GetValue(i, "CALIBRATION_CHECK").ToString()}'
                        ,DISPOSE_DATE = '{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "NEXT_DATE").ToString()).ToString("yyyy-MM-dd")}'
                        ,DISPOSE = '{fpMain.Sheets[0].GetValue(i, "DISPOSE").ToString()}'
                        ,IMAGE = '{bImage}'
                        ,COMMENT = '{fpMain.Sheets[0].GetValue(i, "COMMENT").ToString()}'
                        ,USE_YN = '{fpMain.Sheets[0].GetValue(i, "USE_YN").ToString()}'
                        ,UP_USER = '{user}'
                        ,UP_DATE = '{DateTime.Now.ToString("yyyy-MM-dd")}'
                        where ID = '{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "ID").ToString())}'
                        ";

                            //list.Add(str);
                            sql.Append(str);
                        }

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
        public bool INSTRUMENT_HISTORY_REGISTER_SUB_A10(xFpSpread fpMain, string user, string mst_id)
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

                    if (cCRUD != "")
                    {

                        if (cCRUD == "입력")
                        {
                            string str = $@"INSERT 
                        INTO INSTRUMENT_DETAIL
                        (
                          INSTRUMENT_MST_ID
                          ,DATE
                          ,MST_OUT_CODE
                          ,MST_NAME
                          ,MST_STANDARD
                          ,HISTORY
                          ,COMMENT
                          ,REG_USER
                          ,REG_DATE
                          ,UP_USER
                          ,UP_DATE
                        )
                        values
                        (
                           '{Convert.ToInt32(mst_id)}'
                           ,'{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "DATE").ToString()).ToString("yyyy-MM-dd")}'
                           ,'{fpMain.Sheets[0].GetValue(i, "MST_OUT_CODE").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "MST_NAME").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "MST_STANDARD").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "HISTORY").ToString()}'
                           ,'{fpMain.Sheets[0].GetValue(i, "COMMENT").ToString()}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                           ,'{user}'
                           ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        )";

                            //list.Add(str);
                            sql.Append(str);
                        }

                        else if (cCRUD == "수정")
                        {
                            string str = $@"UPDATE 
                        INSTRUMENT_DETAIL 
                        set
                        DATE = '{Convert.ToDateTime(fpMain.Sheets[0].GetValue(i, "DATE").ToString()).ToString("yyyy-MM-dd")}'
                        ,HISTORY = '{fpMain.Sheets[0].GetValue(i, "HISTORY").ToString()}'
                        ,COMMENT = '{fpMain.Sheets[0].GetValue(i, "COMMENT").ToString()}'
                        ,UP_USER = '{user}'
                        ,UP_DATE = '{DateTime.Now.ToString("yyyy-MM-dd")}'
                        where ID = '{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "ID").ToString())}'
                        ";

                            //list.Add(str);
                            sql.Append(str);
                        }

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
                        if (fpMain.Sheets[0].GetValue(i, "OUT_CODE").ToString().Contains("외주") == true)
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

        public bool STOCK_ORDER_MST_DETAIL_A10(xFpSpread fpMain, string user ,int row)
        {
            bool _Error = false;

            string cCRUD = string.Empty;

            _pDBManager.BeginTransaction();

            try
            {
                IDataParameter[] pDataParameters = null;

                StringBuilder sql = new StringBuilder();

                cCRUD = fpMain.Sheets[0].RowHeader.Cells[row, 0].Text;

                if (cCRUD == "입력")
                {
                    string str = $@"INSERT 
                                            INTO ORDER_MST
                                            (
                                              OUT_CODE
                                              ,NAME
                                              ,ORDER_DATE
                                              ,COMPANY_ID
                                              ,ORDER_TYPE
                                              ,MATERIAL_COST
                                              ,MANUFACTURE_COST
                                              ,ETC_COST
                                              ,TOTAL_COST
                                              ,COMMENT
                                              ,STOCK_FLAG
                                              ,COMPLETE_YN
                                              ,USE_YN
                                              ,REG_USER
                                              ,REG_DATE
                                              ,UP_USER
                                              ,UP_DATE
                                            )
                                            values
                                            (
                                               '{fpMain.Sheets[0].GetValue(row, "OUT_CODE").ToString()}'
                                               ,'{fpMain.Sheets[0].GetValue(row, "OUT_CODE").ToString()}'
                                               ,'{Convert.ToDateTime(fpMain.Sheets[0].GetValue(row, "INSTRUCT_DATE").ToString()).ToString("yyyy-MM-dd")}'
                                               ,'{fpMain.Sheets[0].GetValue(row, "COMPANY_ID").ToString()}'
                                               ,'SD10001'
                                               ,{Convert.ToDecimal(0)}
                                               ,{Convert.ToDecimal(0)}
                                               ,{Convert.ToDecimal(0)}
                                               ,{Convert.ToDecimal(0)}
                                               ,'{fpMain.Sheets[0].GetValue(row, "COMMENT").ToString()}'
                                               ,'N'
                                               ,'N'
                                               ,'Y'
                                               ,'{user}'
                                               ,GETDATE()
                                               ,'{user}'
                                               ,GETDATE()
                                            )";
                    sql.Append(str);

                    if (_pDBManager.DbConnection.State != ConnectionState.Open)
                    {
                        _pDBManager.DbConnection.Open();
                    }

                    int Execute = _pDBManager.Execute2(CommandType.Text, sql.ToString());
                    _Error = false;
                    pDataParameters = null;
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

        public bool STOCK_ORDER_MST_DETAIL_A20(xFpSpread fpMain, string user, int row)
        {
            bool _Error = false;

            string cCRUD = string.Empty;

            _pDBManager.BeginTransaction();

            try
            {
                IDataParameter[] pDataParameters = null;

                string str_select = $@"select 
* from ORDER_MST
where order_type = 'SD10001'
and ORDER_DATE BETWEEN CONVERT(CHAR(10), GETDATE(), 23) and  CONVERT(CHAR(10), GETDATE(), 23)
and COMPANY_ID = '{fpMain.Sheets[0].GetValue(row, "COMPANY_ID").ToString()}'
";

                string sql_select = str_select;
                DataTable _DataTable = new CoreBusiness().SELECT(sql_select);

                //for(int i = 0; i < _DataTable.Rows.Count; i++)
                //{
                    //for (int x = 0; x < fpMain.Sheets[0].Rows.Count; x++)
                    //{
                        StringBuilder sql = new StringBuilder();

                        string order_mst_id = _DataTable.Rows[0]["ID"].ToString();

                        if (fpMain.Sheets[0].GetValue(row, "COMPANY_ID").ToString() == _DataTable.Rows[0]["COMPANY_ID"].ToString())
                        {
                            string str = $@"INSERT 
                        INTO ORDER_DETAIL
                        (
                          ORDER_MST_ID
                          ,STOCK_MST_ID
                          ,SUPPLY_TYPE
                          ,STOCK_MST_PRICE
                          ,ORDER_QTY
                          ,ORDER_REMAIN_QTY
                          ,BOX_QTY
                          ,OUT_QTY
                          ,COST
                          ,COMPANY_ID
                          ,DEMAND_DATE
                          ,OUT_TYPE
                          ,COMMENT
                          ,INSPECTION_YN
                          ,COMPLETE_YN
                          ,USE_YN
                          ,REG_USER
                          ,REG_DATE
                          ,UP_USER
                          ,UP_DATE
                        )
                        values
                        (
                           {Convert.ToInt32(order_mst_id)}
                           ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(row, "STOCK_MST_ID").ToString())}
                           ,' '
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(row, "COST").ToString())}
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(row, "INSTRUCT_QTY").ToString())}
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(row, "INSTRUCT_QTY").ToString())}
                           ,{Convert.ToDecimal(0)}
                           ,{Convert.ToDecimal(0)}
                           ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(row, "COST").ToString()) * Convert.ToDecimal(fpMain.Sheets[0].GetValue(row, "INSTRUCT_QTY").ToString())}
                           ,'{Convert.ToInt32(fpMain.Sheets[0].GetValue(row, "COMPANY_ID").ToString())}'
                           ,'{Convert.ToDateTime(fpMain.Sheets[0].GetValue(row, "INSTRUCT_DATE").ToString()).ToString("yyyy-MM-dd")}'
                           ,'CD20001'
                           ,'{fpMain.Sheets[0].GetValue(row, "COMMENT").ToString()}'
                           ,'N'
                           ,'N'
                           ,'Y'
                           ,'{user}'
                           ,GETDATE()
                           ,'{user}'
                           ,GETDATE()
                        )";

                            sql.Append(str);

                            int Execute = _pDBManager.Execute2(CommandType.Text, sql.ToString());
                        }
                        //string str_sub = $@"INSERT 
                        //    INTO ORDER_DETAIL
                        //    (
                        //      ORDER_MST_ID
                        //      ,STOCK_MST_ID
                        //      ,SUPPLY_TYPE
                        //      ,STOCK_MST_PRICE
                        //      ,ORDER_QTY
                        //      ,ORDER_REMAIN_QTY
                        //      ,BOX_QTY
                        //      ,OUT_QTY
                        //      ,COST
                        //      ,COMPANY_ID
                        //      ,DEMAND_DATE
                        //      ,OUT_TYPE
                        //      ,COMMENT
                        //      ,INSPECTION_YN
                        //      ,COMPLETE_YN
                        //      ,USE_YN
                        //      ,REG_USER
                        //      ,REG_DATE
                        //      ,UP_USER
                        //      ,UP_DATE
                        //    )
                        //    values
                        //    (
                        //       {Convert.ToInt32(order_mst_id)}
                        //       ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(x, "STOCK_MST_ID").ToString())}
                        //       ,' '
                        //       ,{Convert.ToInt32(fpMain.Sheets[0].GetValue(x, "COST").ToString())}
                        //       ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(x, "INSTRUCT_QTY").ToString())}
                        //       ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(x, "INSTRUCT_QTY").ToString())}
                        //       ,{Convert.ToDecimal(0)}
                        //       ,{Convert.ToDecimal(0)}
                        //       ,{Convert.ToDecimal(fpMain.Sheets[0].GetValue(x, "COST").ToString()) * Convert.ToDecimal(fpMain.Sheets[0].GetValue(x, "INSTRUCT_QTY").ToString())}
                        //       ,'{fpMain.Sheets[0].GetValue(x, "ORDER_COMPANY").ToString()}'
                        //       ,'{Convert.ToDateTime(fpMain.Sheets[0].GetValue(x, "INSTRUCT_DATE").ToString()).ToString("yyyy-MM-dd")}'
                        //       ,'CD20001'
                        //       ,'{fpMain.Sheets[0].GetValue(x, "COMMENT").ToString()}'
                        //       ,'N'
                        //       ,'N'
                        //       ,'Y'
                        //       ,'{user}'
                        //       ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        //       ,'{user}'
                        //       ,'{DateTime.Now.ToString("yyyy-MM-dd")}'
                        //    )";

                        //sql.Append(str_sub);

                        //int Execute_sub = _pDBManager.Execute2(CommandType.Text, sql.ToString());
                    //}
                //}
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

        public bool INSTRUCT_COMPLETE_Y_A10(xFpSpread fpMain, string user)
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

                    if (cCRUD == "수정")
                    {
                       string str = $@"UPDATE 
                        PRODUCTION_INSTRUCT 
                        SET
                        COMPLETE_YN = 'Y'
                        where ID = '{Convert.ToInt32(fpMain.Sheets[0].GetValue(i, "ID").ToString())}'";

                        sql.Append(str);

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

        public bool IN_STOCK_WAIT_Y_A10(xFpSpread fpMain, string ID)
        {
            bool _Error = false;

            string cCRUD = string.Empty;

            _pDBManager.BeginTransaction();

            try
            {
                IDataParameter[] pDataParameters = null;

                StringBuilder sql = new StringBuilder();

                List<SqlParameter> list = new List<SqlParameter>();

                string str = $@"INSERT INTO [dbo].[IN_STOCK_DETAIL]
                                                               ([IN_STOCK_WAIT_DETAIL_ID]
		                                                       ,[OUT_CODE]
                                                               ,[ORDER_DETAIL_ID]
                                                               ,[IN_STOCK_DATE]
                                                               ,[IN_TYPE]         
                                                               ,[STOCK_MST_ID]
                                                               ,[IN_QTY]
                                                               ,[USED_QTY]
                                                               ,[REMAIN_QTY]
                                                               ,[COMMENT]
                                                               ,[COMPLETE_YN]
                                                               ,[USE_YN]      
                                                               ,[REG_USER]
                                                               ,[REG_DATE]
                                                               ,[UP_USER]
                                                               ,[UP_DATE])
                                                         select [ID]  
                                                               ,[OUT_CODE]
                                                               ,[ORDER_DETAIL_ID]
                                                         	   ,[IN_DATE] 
                                                               ,[IN_TYPE]
                                                               ,[STOCK_MST_ID]
                                                               ,[COMPLETE_QTY]
                                                               ,0
                                                         	   ,[COMPLETE_QTY]
                                                               ,[COMMENT]
                                                         	   ,'N'
                                                               ,[USE_YN]
                                                               ,[REG_USER]
                                                               ,[REG_DATE]
                                                               ,[UP_USER]
                                                               ,[UP_DATE]
                                                         from [dbo].[IN_STOCK_WAIT_DETAIL]
                                                         where ID ={ID}";

                sql.Append(str);

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

