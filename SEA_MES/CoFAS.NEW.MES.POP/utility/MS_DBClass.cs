using CoFAS.NEW.MES.Core;
using CoFAS.NEW.MES.Core.Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.POP.Function
{
    class MS_DBClass
    {

        //string ConnectionString = $"Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
        string ConnectionString ="Server=10.10.10.180;Database=HS_MES;UID=hansol_mes;PWD=Hansol123!@#";
        public MS_DBClass(My_Settings my_Settings)
        {
            //ConnectionString = $"Server = 127.0.0.1;" +
            //    $"             Database = {my_Settings.DB_NAME};" +
            //    $"                  UID = sa;" +
            //    $"                  PWD = coever1191!;";
            ConnectionString = DBManager.PrimaryConnectionString;
            DBManager.PrimaryDBManagerType = DBManagerType.SQLServer;
            //DBManager.PrimaryConnectionString = "Server = 127.0.0.1;Database=HS_MES;UID=sa;PWD=coever1191!";
            //DBManager.PrimaryConnectionString = "Server=10.10.10.180;Database=HS_MES;UID=hansol_mes;PWD=Hansol123!@#";
            //DBManager.PrimaryConnectionString = "Server = 172.22.4.11,60901; Database = HS_MES; UID = MesConnection; PWD=8$dJ@-!W3b-35;";
        }

        public bool DB_Open_Check()
        {
            bool check = true;


            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConnectionString + ";Connection Timeout=10;");
            try
            {
                // DB 연결          
                conn.Open();

                // 연결여부에 따라 다른 메시지를 보여준다
                if (conn.State != ConnectionState.Open)
                {
                    check = false;
                }

                conn.Close();

                return check;
            }
            catch (Exception err)
            {
                conn.Close();
                return check = false;
            }
        }

        public DataTable Login_Info(LoginEntity _LoginEntity)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "USP_LoginInfo_R10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@user_account", SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@user_password", SqlDbType.VarChar, 1000));




                cmd.Parameters["@user_account  ".Trim()].Value = _LoginEntity.user_account;
                cmd.Parameters["@user_password ".Trim()].Value = _LoginEntity.user_password;
 

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;
            
            }
            catch (Exception err)
            {
                return null;
            }
        }
        public bool SystemLog_Info(SystemLogEntity _SystemLogEntity)
        {
            bool pErrorYN = false;    
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "USP_SystemEventLog_A10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@user_account   ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@event_ip       ".Trim(), SqlDbType.VarChar, 15));
                cmd.Parameters.Add(new SqlParameter("@event_mac      ".Trim(), SqlDbType.VarChar, 20));
                cmd.Parameters.Add(new SqlParameter("@event_name     ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@event_type     ".Trim(), SqlDbType.VarChar, 20));
                cmd.Parameters.Add(new SqlParameter("@event_log      ".Trim(), SqlDbType.VarChar, 500));


                cmd.Parameters["@user_account   ".Trim()].Value =  _SystemLogEntity.user_account;
                cmd.Parameters["@event_ip       ".Trim()].Value =  _SystemLogEntity.user_ip;
                cmd.Parameters["@event_mac      ".Trim()].Value =  _SystemLogEntity.user_mac;
                cmd.Parameters["@event_name     ".Trim()].Value =  _SystemLogEntity.user_pc;
                cmd.Parameters["@event_type     ".Trim()].Value =  _SystemLogEntity.event_type;
                cmd.Parameters["@event_log      ".Trim()].Value =  _SystemLogEntity.event_log;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
             
                if (dt.Rows[0]["err_no"].ToString() != "00")
                    pErrorYN = true;

            }
            catch (Exception pException)
            {
                pErrorYN = true;
            }
    
            return pErrorYN;
        }

        public DataTable instruct_Setting(DateTime dateTime1, DateTime dateTime2,string line)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"SELECT A.ID
                                    ,FORMAT(A.INSTRUCT_DATE,'yyyy-MM-dd') AS INSTRUCT_DATE
	                                ,B.OUT_CODE AS STOCK_OUT_CODE
	                                ,B.NAME                                
	                                ,C.code_name
	                                ,A.INSTRUCT_QTY
	                                ,A.SORT
	                                ,A.COMPLETE_YN
	                                ,A.START_INSTRUCT_DATE
                                    ,A.END_INSTRUCT_DATE
                                    ,A.OUT_CODE
                                  FROM [dbo].[PRODUCTION_INSTRUCT] A
                                  INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID
                                  INNER JOIN Code_Mst C ON B.TYPE = C.code							
                                  INNER JOIN PRODUCTION_PLAN F ON A.PRODUCTION_PLAN_ID = F.ID
                                  WHERE 1=1 
                                    AND A.INSTRUCT_DATE BETWEEN '{dateTime1.ToString("yyyy-MM-dd")}' 
                                                            AND '{dateTime2.ToString("yyyy-MM-dd")}'
                                    AND F.LINE LIKE '%{line}%'
                                    AND A.USE_YN = 'Y'
                               ORDER BY INSTRUCT_DATE ,SORT";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }

        public DataSet CallBack(int id)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
              

                cmd.CommandText = $@"SELECT C.*
				                       FROM [dbo].[PRODUCTION_INSTRUCT] A				                       
				                       INNER JOIN [dbo].[OPC_MST_OK] C ON 
				                        C.READ_DATE BETWEEN A.START_INSTRUCT_DATE AND ISNULL(A.END_INSTRUCT_DATE,GETDATE())
									   inner join [dbo].[STOCK_MST] B ON A.STOCK_MST_ID = B.ID  AND C.VALUE = B.COLUMN10
                                       WHERE A.ID = {id};

                                   select top 1 READ_DATE,VALUE
								  from [dbo].[OPC_MST_OK]
								 
							      ORDER BY READ_DATE desc;

                               select top 1 READ_DATE
								  from [dbo].[OPC_MST]
								 
							      ORDER BY READ_DATE desc;";


                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet dt = new System.Data.DataSet();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataSet GET_QTY(int id,DateTime end_date)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
    
                cmd.CommandText = $@"SELECT COUNT(*)
				   FROM [dbo].[PRODUCTION_INSTRUCT] A	 
				   INNER JOIN [dbo].[OPC_MST_OK] C ON 
				   C.READ_DATE BETWEEN A.START_INSTRUCT_DATE AND '{end_date.ToString("yyyy-MM-dd HH:mm:ss")}'
                   where a.id = {id};";


                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet dt = new System.Data.DataSet();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable work_Setting()
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"select code,code_name
                                       from [dbo].[Code_Mst]
                                      where 1=1 
                                        and code_type = 'CD14'
                                        and use_yn = 'Y'
                                   ORDER BY SORT";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable instruct_Get(int id)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"SELECT A.ID                                  
	                                ,B.OUT_CODE AS STOCK_OUT_CODE
	                                ,B.NAME
									,B.STANDARD
									,CAST(ROUND(A.INSTRUCT_QTY,0)as DECIMAL(18,0)) as INSTRUCT_QTY
									,CAST(ROUND(ISNULL(E.OK_QTY,0),0) as DECIMAL(18,0)) as OK_QTY
									,CAST(ROUND(ISNULL(E.NG_QTY,0),0) as DECIMAL(18,0)) as NG_QTY
									,CAST(ROUND(ISNULL(E.TOTAL_QTY,0),0)as DECIMAL(18,0)) as TOTAL_QTY
									,B.COLUMN10 AS OUT_CODE
									,FORMAT(A.INSTRUCT_DATE,'yyyy-MM-dd') AS INSTRUCT_DATE                     
                                    ,[START_INSTRUCT_DATE]
                                    ,[END_INSTRUCT_DATE]
                                    ,A.COMMENT
                                    ,A.STOCK_MST_ID
									,G.code_name AS LINE
                                    ,F.LINE AS LINE_CODE
                                  FROM [dbo].[PRODUCTION_INSTRUCT] A
                                  INNER JOIN STOCK_MST B ON A.STOCK_MST_ID = B.ID        
								  LEFT JOIN 
								  (
								   SELECT PRODUCTION_INSTRUCT_ID
								        ,ISNULL(SUM(OK_QTY),0) AS OK_QTY
										,ISNULL(SUM(NG_QTY),0) AS NG_QTY
										,ISNULL(SUM(TOTAL_QTY),0) AS TOTAL_QTY
								    FROM [dbo].[PRODUCTION_RESULT]
									WHERE USE_YN = 'Y'
									GROUP BY PRODUCTION_INSTRUCT_ID
								  ) E ON A.ID = E.PRODUCTION_INSTRUCT_ID
								  INNER JOIN PRODUCTION_PLAN F ON A.PRODUCTION_PLAN_ID = F.ID
								  INNER JOIN Code_Mst G ON F.LINE = G.code
            
                                  WHERE 1=1 
								  AND A.USE_YN = 'Y'          
                                  AND A.ID = {id};";

								
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;




                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }

        public DataTable Fixed_Stop(int id)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"select A.ID,B.*,C.TYPE
                                       from [dbo].[PRODUCTION_INSTRUCT] A
                                       LEFT JOIN
                                       (SELECT CONVERT(FLOAT,code_etc2) as code_etc2
                                        , DATEADD(MINUTE ,CONVERT(INT,code_etc1)
                                        ,(SELECT  CASE 
                                        WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                        THEN CONVERT(DATETIME,FORMAT(GETDATE(), 'yyyy-MM-dd'))
                                        ELSE CONVERT(DATETIME,FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd'))　END)) AS code_etc1
                                        , code_etc3
                                        , code
                                        FROM [dbo].[Code_Mst] 
                                        where 1=1 
                                         AND code_type = 'CD12'
                                         AND code_description = '고정비가동'
                                         ) B ON   FORMAT(B.code_etc1,'yyyy-MM-dd HH:mm') >= FORMAT(A.START_INSTRUCT_DATE,'yyyy-MM-dd HH:mm')
                                              AND FORMAT(B.code_etc1,'yyyy-MM-dd HH:mm') < FORMAT(ISNULL(A.END_INSTRUCT_DATE,GETDATE()),'yyyy-MM-dd HH:mm')
                                         LEFT JOIN [dbo].[EQUIPMENT_STOP] C ON C.PRODUCTION_INSTRUCT_ID = A.ID AND B.code = C.TYPE
                                         where A.id = {id}";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;




                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable btn_start_work_Click(int id,string dateTime)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"UPDATE [dbo].[PRODUCTION_INSTRUCT]
                                        SET 
                                            [START_INSTRUCT_DATE] = '{dateTime}'
                                           ,[END_INSTRUCT_DATE] = null
                                      
                                      WHERE id = {id}";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;




                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable btn_end_Click(int id)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"UPDATE [dbo].[PRODUCTION_INSTRUCT]
                                        SET 
                                            [START_INSTRUCT_DATE] = null
                                           ,[END_INSTRUCT_DATE] = null                                     
                                      WHERE id = {id}";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;




                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }

        public DataTable btn_end_Click(int id, DateTime endtime)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"UPDATE [dbo].[PRODUCTION_INSTRUCT]
                                        SET                                        
                                           [END_INSTRUCT_DATE] = '{endtime.ToString("yyyy-MM-dd HH:mm:ss")}'    
                                           ,[UP_DATE] = GETDATE()
                                      WHERE id = {id}";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;




                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable btn_coment_Click(int id, string text)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"UPDATE [dbo].[PRODUCTION_INSTRUCT]
                                        SET 
                                           COMMENT = '{text}'    
                                      WHERE id = {id}";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;




                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable EQUIPMENT_Get()
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"SELECT ID,NAME
                                       FROM [dbo].[EQUIPMENT] A
                                      WHERE USE_YN  ='Y'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;




                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }

        public DataTable Image_Get()
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"SELECT ID,NAME
                                       FROM [dbo].[EQUIPMENT] A
                                      WHERE USE_YN  ='Y'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;




                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable Spread_ComobBox(string pServieceName, string pFirst, string pSecond)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "USP_xSpreadComboBox_R10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@ServiceName ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@First       ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@Second      ".Trim(), SqlDbType.VarChar, 50));



                cmd.Parameters["@ServiceName ".Trim()].Value = pServieceName;
                cmd.Parameters["@First       ".Trim()].Value = pFirst;
                cmd.Parameters["@Second      ".Trim()].Value = pSecond;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
    
        public void PRODUCTION_RESULT(List<PRODUCTION_RESULT> list)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();

                foreach (PRODUCTION_RESULT item in list)
                {
                    cmd.CommandText = $@"INSERT INTO [dbo].[PRODUCTION_RESULT]
                                                ([LOT_NO]
                                                ,[PRODUCTION_INSTRUCT_ID]
                                                ,[STOCK_MST_ID]
                                                ,[STOCK_MST_OUT_CODE]
                                                ,[STOCK_MST_STANDARD]
                                                ,[STOCK_MST_TYPE]                                              
                                                ,[RESULT_TYPE]
                                                ,[OK_QTY]
                                                ,[NG_QTY]
                                                ,[TOTAL_QTY]
                                                ,[START_DATE]
                                                ,[END_DATE]
                                                ,[COMMENT]
                                                ,[USE_YN]
                                                ,[REG_USER]
                                                ,[REG_DATE]
                                                ,[UP_USER]
                                                ,[UP_DATE])
                                          VALUES
                                                ( 
                                                 '{item.LOT_NO}'
                                                ,'{item.PRODUCTION_INSTRUCT_ID}'
                                                ,'{item.STOCK_MST_ID}'
                                                ,'{item.STOCK_MST_OUT_CODE}'
                                                ,'{item.STOCK_MST_STANDARD}'
                                                ,'{item.STOCK_MST_TYPE}'
                                                ,'{item.RESULT_TYPE}'
                                                ,'{item.OK_QTY}'
                                                ,'{item.NG_QTY}'
                                                ,'{item.TOTAL_QTY}'
                                                ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                ,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                ,'{item.COMMENT}'
                                                ,'{item.USE_YN}'
                                                ,'{item.REG_USER}'
                                                ,'{item.REG_DATE.ToString("yyyy-MM-dd HH:mm:ss")}'
                                                ,'{item.UP_USER}'
                                                ,'{item.UP_DATE.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    cmd.ExecuteNonQuery();
                }


                con.Close();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public DataTable Equipment_Setting()
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"select ID,NAME
                                       from [dbo].[EQUIPMENT]
                                      where 1=1                                      
                                        and use_yn = 'Y'
                                      ORDER BY NAME";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable EquipmentCheck_Setting(int id,string type)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"SELECT 
                                            A.ID
                                           ,A.TYPE
                                           ,EQUIP_INSPECTION_ID
                                           ,A.EQUIPMENT_ID   
                                           ,A.EQUIPMENT_NAME   
                                           ,CHECK_DATE
                                           ,B.code_name            AS CATEGORY1
                                           ,C.code_name　          AS CATEGORY2 
                                           ,C.code_description     AS CATEGORY3 


                                           ,ISNULL(D.CHECK_USER_NAME,'') AS CHECK_USER_NAME
                                           ,ISNULL(D.CHECK_YN ,'')       AS CHECK_YN
                                           ,ISNULL(D.USER_NAME,'')       AS USER_NAME
                                           ,ISNULL(D.CATEGORY4,'')       AS CATEGORY4
                                           ,ISNULL(D.CATEGORY5,'')       AS CATEGORY5 
                                           ,ISNULL(D.CATEGORY6,'')       AS CATEGORY6 
                                           ,ISNULL(D.CATEGORY7,'')       AS CATEGORY7 
                                           ,D.COMMENT
                                           ,USE_YN
                                           ,UP_USER
                                           ,UP_DATE
                                           ,REG_USER
                                           ,REG_DATE

                              
                                         
                                      FROM [dbo].[EQUIP_INSPECTION] A
                                     inner join [dbo].[Code_Mst] B ON A.Category１ = B.code
                                     inner join [dbo].[Code_Mst] C ON A.Category２ = C.code
                                      left join [dbo].[EQUIP_INSPECTION_LIST] D ON A.EQUIPMENT_ID = D.EQUIPMENT_ID and A.ID = D.EQUIP_INSPECTION_ID AND D.CHECK_DATE = FORMAT(GETDATE(), 'yyyy-MM-dd')
                                     WHERE A.EQUIPMENT_ID = {id} AND A.TYPE = '{type}'
                                     ORDER BY B.code_name";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }

        public void EQUIP_INSPECTION_LIST(List<EQUIP_INSPECTION_LIST> list)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
               
                foreach (EQUIP_INSPECTION_LIST item in list)
                {
                    cmd.CommandText = $@"MERGE INTO[dbo].[EQUIP_INSPECTION_LIST]  AS A
                                             USING(SELECT
                                     
                                               '{item.EQUIP_INSPECTION_ID}'  AS EQUIP_INSPECTION_ID
                                             , '{item.CHECK_DATE}'           AS CHECK_DATE
                                             , '{item.TYPE}'                 AS TYPE) AS B
                                               ON(A.EQUIP_INSPECTION_ID = B.EQUIP_INSPECTION_ID
                                     
                                               AND A.CHECK_DATE = B.CHECK_DATE
                                     
                                               AND A.TYPE = B.TYPE)
                                     WHEN MATCHED THEN
                                      UPDATE
                                     
                                         SET[Category1]         = '{item.CATEGORY1}'
                                     		 ,[Category2]       = '{item.CATEGORY2}'
                                     		 ,[Category3]       = '{item.CATEGORY3}'
                                     		 ,[CHECK_YN]        = '{item.CHECK_YN}'
                                     		 ,[USER_NAME]       = '{item.USER_NAME}'
                                             ,[CHECK_USER_NAME] = '{item.CHECK_USER_NAME}'
                                     		 ,[COMMENT]         = '{item.COMMENT}'
                                     		 ,[USE_YN]          = '{item.USE_YN}'
                                     		 ,[UP_USER]         = '{item.REG_USER}'
                                     		 ,[UP_DATE]         = '{item.REG_DATE}'
                                     		 ,[REG_USER]        = '{item.UP_USER}'
                                     		 ,[REG_DATE]        = '{item.UP_DATE}'
                                     WHEN NOT MATCHED THEN
                                     INSERT
                                            (                                   
                                               [TYPE]
                                              ,[EQUIP_INSPECTION_ID]
                                              ,[EQUIPMENT_ID]
                                              ,[EQUIPMENT_NAME]
                                              ,[CHECK_DATE]
                                              ,[Category1]
                                              ,[Category2]
                                              ,[Category3]
                                              ,[CHECK_YN]
                                              ,[USER_NAME]
                                              ,[CHECK_USER_NAME] 
                                              ,[COMMENT]
                                              ,[USE_YN]
                                              ,[UP_USER]
                                              ,[UP_DATE]
                                              ,[REG_USER]
                                              ,[REG_DATE])
                                     		    VALUES
                                                (
                                                  '{item.TYPE}'
                                                , '{item.EQUIP_INSPECTION_ID}'
                                                , '{item.EQUIPMENT_ID}'
                                                , '{item.EQUIPMENT_NAME}'
                                                , '{item.CHECK_DATE}'
                                                , '{item.CATEGORY1}'
                                                , '{item.CATEGORY2}'
                                                , '{item.CATEGORY3}'
                                                , '{item.CHECK_YN}'
                                                , '{item.USER_NAME}'
                                                , '{item.CHECK_USER_NAME}'
                                                , '{item.COMMENT}'
                                                , '{item.USE_YN}'
                                                , '{item.REG_USER}'
                                                , '{item.REG_DATE}'
                                                , '{item.UP_USER}'
                                                , '{item.UP_DATE}'
                                                ); ";



                    cmd.ExecuteNonQuery();
                }


                con.Close();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public void EQUIPMENT_STOP_INSERT(EQUIPMENT_STOP sTOP)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
               
                cmd.CommandText = $@"INSERT INTO [dbo].[EQUIPMENT_STOP]
                                            ([TYPE] 
                                            ,[PRODUCTION_INSTRUCT_ID]
                                            ,[EQUIPMENT_ID]
                                            ,[EQUIPMENT_NAME]
                                            ,[START_TIME]
                                            ,[END_TIME]
                                            ,[COMMENT]
                                            ,[USE_YN]
                                            ,[UP_USER]
                                            ,[UP_DATE]
                                            ,[REG_USER]
                                            ,[REG_DATE])
                                      VALUES
                                            ('{sTOP.TYPE}'
                                            ,'{sTOP.PRODUCTION_INSTRUCT_ID}'
                                            ,'{sTOP.EQUIPMENT_ID}'
                                            ,'{sTOP.EQUIPMENT_NAME}'
                                            ,'{sTOP.START_TIME.ToString("yyyy-MM-dd HH:mm:ss")}'
                                            ,'{sTOP.END_TIME.ToString("yyyy-MM-dd HH:mm:ss")}'
                                            ,'{sTOP.COMMENT}'
                                            ,'{sTOP.USE_YN}'
                                            ,'{sTOP.UP_USER}'
                                            ,'{sTOP.UP_DATE.ToString("yyyy-MM-dd HH:mm:ss")}'
                                            ,'{sTOP.REG_USER}'
                                            ,'{sTOP.REG_DATE.ToString("yyyy-MM-dd HH:mm:ss")}')";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public void EQUIPMENT_STOP_INSERT1(EQUIPMENT_STOP sTOP)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.CommandText = $@"INSERT INTO [dbo].[EQUIPMENT_STOP]
                                            ([TYPE] 
                                            ,[PRODUCTION_INSTRUCT_ID]
                                            ,[EQUIPMENT_ID]
                                            ,[EQUIPMENT_NAME]
                                            ,[START_TIME]                                         
                                            ,[COMMENT]
                                            ,[USE_YN]
                                            ,[UP_USER]
                                            ,[UP_DATE]
                                            ,[REG_USER]
                                            ,[REG_DATE])
                                      VALUES
                                            ('{sTOP.TYPE}'
                                            ,'{sTOP.PRODUCTION_INSTRUCT_ID}'
                                            ,'{sTOP.EQUIPMENT_ID}'
                                            ,'{sTOP.EQUIPMENT_NAME}'
                                            ,'{sTOP.START_TIME.ToString("yyyy-MM-dd HH:mm:ss")}'
                                            ,'{sTOP.COMMENT}'
                                            ,'{sTOP.USE_YN}'
                                            ,'{sTOP.UP_USER}'
                                            ,'{sTOP.UP_DATE.ToString("yyyy-MM-dd HH:mm:ss")}'
                                            ,'{sTOP.REG_USER}'
                                            ,'{sTOP.REG_DATE.ToString("yyyy-MM-dd HH:mm:ss")}')";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public void EQUIPMENT_STOP_UPDATE(string id,DateTime end_time)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.CommandText = $@"update[dbo].[EQUIPMENT_STOP]
                                        set END_TIME = '{end_time.ToString("yyyy-MM-dd HH:mm:ss")}'
                                        where 1 = 1
                                        AND ID = {id}
                                        AND END_TIME IS NULL";




                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public void EQUIPMENT_STOP_DELETE(string id)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.CommandText = $@"update[dbo].[EQUIPMENT_STOP]
                                        set USE_YN = 'N'
                                        where 1 = 1
                                        AND ID = {id}";




                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception pExcption)
            {
                CustomMsg.ShowMessage(pExcption.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
        public DataTable USP_Production_Progress_Status_R10()
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
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
 END) / E.CYCLE_TIME))*FRACTION_DEFECTIVE AS 계획
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
                cmd.CommandText = str;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable EQUIPMENT_STOP(string id)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"SELECT C.code_name AS 비가동유형
                                            ,B.NAME     AS 설비명
                                            --,FORMAT(A.START_TIME, 'yyyy-MM-dd HH:mm:ss') AS 시작시간
                                            --,FORMAT(A.END_TIME, 'yyyy-MM-dd HH:mm:ss') AS 종료시간
                                            ,A.START_TIME AS 시작시간
                                            ,A.END_TIME AS 종료시간
                                            ,A.COMMENT AS 비고
                                            ,A.ID
                                            ,'' as '*'
                               FROM [dbo].[EQUIPMENT_STOP] A
                                LEFT JOIN[dbo].[EQUIPMENT] B ON A.EQUIPMENT_ID = B.ID
                               INNER JOIN [dbo].[Code_Mst] C ON A.TYPE = C.code
                               WHERE 1 = 1
                               AND A.USE_YN = 'Y'                            
                               AND A.PRODUCTION_INSTRUCT_ID = {id}";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }

        public DataTable START_CHECK(int id)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = $@"SELECT COUNT(1) 
                                      FROM [dbo].[EQUIP_INSPECTION] A
                                      inner join [dbo].[EQUIP_INSPECTION_LIST] D ON A.EQUIPMENT_ID = D.EQUIPMENT_ID and A.ID = D.EQUIP_INSPECTION_ID AND D.CHECK_DATE = 
									   (
                                         SELECT  CASE 
                                      WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                      THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                      ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END 
                                      )      				  
                                      WHERE 1=1
                                      AND A.USE_YN = 'Y'
                                      union all
                                      SELECT COUNT(1)  
                                      FROM [dbo].[EQUIP_INSPECTION] A
                                      left join [dbo].[EQUIP_INSPECTION_LIST] D ON A.EQUIPMENT_ID = D.EQUIPMENT_ID and A.ID = D.EQUIP_INSPECTION_ID AND D.CHECK_DATE = 
                                      (
                                        SELECT  CASE 
                                      WHEN FORMAT(GETDATE(), 'HH:mm') > '08:30'   
                                      THEN FORMAT(GETDATE(), 'yyyy-MM-dd')
                                      ELSE FORMAT(DATEADD(DAY ,-1, GETDATE()), 'yyyy-MM-dd')　END 
                                      )                 
                                      WHERE 1=1
                                      AND A.USE_YN = 'Y'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }

        public DataTable SELECT(string sql )
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable SELECT2(string sql)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(DBManager.PrimaryConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable USP_MaterialBarcode_A10
            (
              string ResourceNo
             ,string Vendor_No
             ,string ResourceWeight
             ,string SplitQty
             ,string Lot_No
             ,string Comment
            )
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(DBManager.PrimaryConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "USP_MaterialBarcode_A10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@ResourceNo       ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@ResourceType     ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@Vendor_No        ".Trim(), SqlDbType.VarChar, 20));
                cmd.Parameters.Add(new SqlParameter("@ResourceWeight   ".Trim(), SqlDbType.Decimal));
                cmd.Parameters.Add(new SqlParameter("@SplitQty         ".Trim(), SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Comment          ".Trim(), SqlDbType.VarChar, 100));
                cmd.Parameters.Add(new SqlParameter("@RegUser          ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@UpUser           ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@Lot_No           ".Trim(), SqlDbType.VarChar, 50));
       
                cmd.Parameters["@ResourceNo      ".Trim()].Value = ResourceNo;
                cmd.Parameters["@ResourceType    ".Trim()].Value = "";
                cmd.Parameters["@Vendor_No       ".Trim()].Value = Vendor_No;
                cmd.Parameters["@ResourceWeight  ".Trim()].Value = ResourceWeight;
                cmd.Parameters["@SplitQty        ".Trim()].Value = SplitQty;
                cmd.Parameters["@Comment         ".Trim()].Value = Comment;
                cmd.Parameters["@RegUser         ".Trim()].Value = "";
                cmd.Parameters["@UpUser          ".Trim()].Value = "";
                cmd.Parameters["@Lot_No          ".Trim()].Value = Lot_No;
                //   @ResourceNo     VARCHAR(50),
                //   @ResourceType   VARCHAR(50) = '',
                //   @Vendor_No      VARCHAR(50),
                //   @ResourceWeight DECIMAL(18, 0),
                //   @SplitQty       INT,
                //   @Comment        VARCHAR(100) = '',
                //   @RegUser        VARCHAR(50) = '',
                //   @UpUser         VARCHAR(50) = ''
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable USP_ProductBarcode_A10
         (
            string ResourceNo     
          , string VendorNo       
          , string LotNo          
          , string BarcodeStatus  
          , string BarcodeDate    
          , string MoveDate       
          , string InventoryRecord
          , string PQty               
          , string Comment        
          , string USER
          , string WORKPERFORMANCE_ID

         )
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(DBManager.PrimaryConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "USP_ProductBarcode_A10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@ResourceNo     ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@VendorNo       ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@LotNo          ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@BarcodeStatus  ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@BarcodeDate    ".Trim(), SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@MoveDate       ".Trim(), SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@InventoryRecord".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@PQty           ".Trim(), SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@Comment        ".Trim(), SqlDbType.VarChar, 250));
                cmd.Parameters.Add(new SqlParameter("@USER           ".Trim(), SqlDbType.VarChar, 250));
                cmd.Parameters.Add(new SqlParameter("@WORKPERFORMANCE_ID      ".Trim(), SqlDbType.Int));



                cmd.Parameters["@ResourceNo              ".Trim()].Value = ResourceNo;
                cmd.Parameters["@VendorNo                ".Trim()].Value = VendorNo;
                cmd.Parameters["@LotNo                   ".Trim()].Value = LotNo;
                cmd.Parameters["@BarcodeStatus           ".Trim()].Value = BarcodeStatus;   // 재고기록표
                cmd.Parameters["@BarcodeDate             ".Trim()].Value = BarcodeDate;     // 오늘날짜
                cmd.Parameters["@MoveDate                ".Trim()].Value = MoveDate;        // 오늘날짜 => 업데이트
                cmd.Parameters["@InventoryRecord         ".Trim()].Value = InventoryRecord; // ID
                cmd.Parameters["@PQty                    ".Trim()].Value = PQty;            // 입력수량
                cmd.Parameters["@WORKPERFORMANCE_ID      ".Trim()].Value = WORKPERFORMANCE_ID;       // 제품코드-LOT-수량-재고기록표(ID) 
                cmd.Parameters["@Comment                 ".Trim()].Value = Comment;         //
                cmd.Parameters["@USER                    ".Trim()].Value = USER;            //

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public DataTable USP_WorkPerformance_A10
           (
             string MACHINE_NO
            ,string ORDER_DATE 
            , string RESOURCE_NO	
            , string LOT_NO 
            , string SHIFT 
            , string WORK_CODE 
            , decimal QTY_COMPLETE 
            , string WORK_TIME 
            , string START_TIME 
            , string END_TIME 
            , string REG_USER 
            , string REG_DATE 
            , string UP_USER
            , string UP_DATE 
            , string IN_PER
           )
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(DBManager.PrimaryConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "USP_WorkPerformance_A10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;


                //   @ORDER_DATE
                //@RESOURCE_NO 
                //@LOT_NO
                //   @SHIFT
                //   @WORK_CODE 
                //   @QTY_COMPLETE 
                //   @WORK_TIME 
                //   @START_TIME 
                //   @END_TIME
                //   @REG_USER 
                //   @REG_DATE
                //   @UP_USER 
                //   @UP_DATE 

                cmd.Parameters.Add(new SqlParameter("@MACHINE_NO  ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@ORDER_DATE  ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@RESOURCE_NO ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@LOT_NO      ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@SHIFT       ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@WORK_CODE   ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@QTY_COMPLETE".Trim(), SqlDbType.Decimal));
                cmd.Parameters.Add(new SqlParameter("@WORK_TIME   ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@START_TIME  ".Trim(), SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@END_TIME    ".Trim(), SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@REG_USER    ".Trim(), SqlDbType.VarChar, 50));
                //cmd.Parameters.Add(new SqlParameter("@REG_DATE    ".Trim(), SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@UP_USER     ".Trim(), SqlDbType.VarChar, 50));
                cmd.Parameters.Add(new SqlParameter("@IN_PER      ".Trim(), SqlDbType.VarChar, 50));


                cmd.Parameters["@MACHINE_NO  ".Trim()].Value = MACHINE_NO  ;
                cmd.Parameters["@ORDER_DATE  ".Trim()].Value = ORDER_DATE  ;
                cmd.Parameters["@RESOURCE_NO ".Trim()].Value = RESOURCE_NO ;
                cmd.Parameters["@LOT_NO      ".Trim()].Value = LOT_NO      ;
                cmd.Parameters["@SHIFT       ".Trim()].Value = SHIFT       ;
                cmd.Parameters["@WORK_CODE   ".Trim()].Value = WORK_CODE   ;
                cmd.Parameters["@QTY_COMPLETE".Trim()].Value = QTY_COMPLETE;
                cmd.Parameters["@WORK_TIME   ".Trim()].Value = WORK_TIME   ;
                cmd.Parameters["@START_TIME  ".Trim()].Value = START_TIME  ;
                cmd.Parameters["@END_TIME    ".Trim()].Value = END_TIME    ;
                cmd.Parameters["@REG_USER    ".Trim()].Value = REG_USER    ;
 
                cmd.Parameters["@UP_USER     ".Trim()].Value = UP_USER     ;
                cmd.Parameters["@IN_PER     ".Trim()].Value = IN_PER;
                //cmd.Parameters["@UP_DATE     ".Trim()].Value = UP_DATE     ;



                //   @ResourceNo     VARCHAR(50),
                //   @ResourceType   VARCHAR(50) = '',
                //   @Vendor_No      VARCHAR(50),
                //   @ResourceWeight DECIMAL(18, 0),
                //   @SplitQty       INT,
                //   @Comment        VARCHAR(100) = '',
                //   @RegUser        VARCHAR(50) = '',
                //   @UpUser         VARCHAR(50) = ''
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }


        

            public DataTable USP_BadPerformance_A10
           (
              string BAD_DATE 
            , string RESOURCE_NO 
            , string BAD_TYPE
            , string BAD_QTY 
            , string BAD_FLAG 
            , string ORDER_NO 
            , string LOT_NO 
            , string REG_USER 
            , string UP_USER 

           )
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(DBManager.PrimaryConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "USP_BadPerformance_A10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;


                //   @ORDER_DATE
                //@RESOURCE_NO 
                //@LOT_NO
                //   @SHIFT
                //   @WORK_CODE 
                //   @QTY_COMPLETE 
                //   @WORK_TIME 
                //   @START_TIME 
                //   @END_TIME
                //   @REG_USER 
                //   @REG_DATE
                //   @UP_USER 
                //   @UP_DATE 


                cmd.Parameters.Add(new SqlParameter("@BAD_DATE   ".Trim(), SqlDbType.VarChar, 8));  //VARCHAR(8),
                cmd.Parameters.Add(new SqlParameter("@RESOURCE_NO".Trim(), SqlDbType.VarChar, 50));  //VARCHAR(50)
                cmd.Parameters.Add(new SqlParameter("@BAD_TYPE   ".Trim(), SqlDbType.VarChar, 50));  //VARCHAR(50)
                cmd.Parameters.Add(new SqlParameter("@BAD_QTY    ".Trim(), SqlDbType.Int));  //INT,
                cmd.Parameters.Add(new SqlParameter("@BAD_FLAG   ".Trim(), SqlDbType.VarChar, 50));  //VARCHAR(1),
                cmd.Parameters.Add(new SqlParameter("@ORDER_NO   ".Trim(), SqlDbType.VarChar, 50));     //VARCHAR(50)
                cmd.Parameters.Add(new SqlParameter("@LOT_NO     ".Trim(), SqlDbType.VarChar, 50));  //VARCHAR(50)
                cmd.Parameters.Add(new SqlParameter("@REG_USER   ".Trim(), SqlDbType.VarChar, 50));  //VARCHAR(50)
                cmd.Parameters.Add(new SqlParameter("@UP_USER    ".Trim(), SqlDbType.VarChar, 50));     //VARCHAR(50)



                cmd.Parameters["@BAD_DATE   ".Trim()].Value = BAD_DATE   ;
                cmd.Parameters["@RESOURCE_NO".Trim()].Value = RESOURCE_NO;
                cmd.Parameters["@BAD_TYPE   ".Trim()].Value = BAD_TYPE   ;
                cmd.Parameters["@BAD_QTY    ".Trim()].Value = BAD_QTY    ;
                cmd.Parameters["@BAD_FLAG   ".Trim()].Value = BAD_FLAG   ;
                cmd.Parameters["@ORDER_NO   ".Trim()].Value = ORDER_NO   ;
                cmd.Parameters["@LOT_NO     ".Trim()].Value = LOT_NO     ;
                cmd.Parameters["@REG_USER   ".Trim()].Value = REG_USER   ;
                cmd.Parameters["@UP_USER    ".Trim()].Value = UP_USER;




       
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
        public void PACK_PROD(List<PACK_PROD> list)
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(this.ConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();

                foreach (PACK_PROD item in list)
                {


                      cmd.CommandText = $@"INSERT INTO [dbo].[PACK_PROD]
                                                ( [WORK_PERFORMANCE_ID]
                                                , [MACHINE_NO]
                                                , [ORDER_NO]
                                                , [RESOURCE_NO]
                                                , [LOT_NO]
                                                , [COMPLETE_QTY]
                                                , [QTY]
                                                , [START_TIME]
                                                , [END_TIME]
                                                , [REG_USER]
                                                , [REG_DATE]
                                                , [UP_USER]
                                                , [UP_DATE])
                                           VALUES
                                                ( @WORK_PERFORMANCE_ID
                                                , @MACHINE_NO
                                                , @ORDER_NO
                                                , @RESOURCE_NO
                                                , @LOT_NO
                                                , @COMPLETE_QTY
                                                , @QTY
                                                , GETDATE()
                                                , @END_TIME
                                                , @REG_USER
                                                , GETDATE()
                                                , @UP_USER
                                                , GETDATE())";
                    //( 
                    // '{item.WORK_PERFORMANCE_ID}'
                    //,'{item.MACHINE_NO}'
                    //,'{item.ORDER_NO}'
                    //,'{item.RESOURCE_NO}'
                    //,'{item.COMPLETE_QTY}'
                    //,'{item.QTY}'
                    //,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                    //,'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'
                    //,'{item.REG_USER}'
                    //,'{item.REG_DATE.ToString("yyyy-MM-dd HH:mm:ss")}'
                    //,'{item.UP_USER}'
                    //,'{item.UP_DATE.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    cmd.ExecuteNonQuery();
                }


                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public DataTable USP_WORK_RECYCLE_POP_A10
        (
           string  pRESOURCE_NO    
         , string  pLOT_NO         
         , string  pORDER_DATE     
         , string  pRE_SCRAP_STATUS
         , string  pRE_GROSS_STATUS
         , string  pRE_GATE_STATUS 
         , string  pREG_USER       
         , string  pUP_USER

        )
        {
            try
            {
                SqlConnection con;
                con = new SqlConnection(DBManager.PrimaryConnectionString);
                SqlCommand cmd;
                cmd = new SqlCommand();
                cmd.CommandText = "USP_WORK_RECYCLE_POP_A10";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;


                cmd.Parameters.Add(new SqlParameter("@RESOURCE_NO    ".Trim(), SqlDbType.VarChar, 50));  //VARCHAR(8),
                cmd.Parameters.Add(new SqlParameter("@LOT_NO         ".Trim(), SqlDbType.VarChar, 50));  //VARCHAR(50)
                cmd.Parameters.Add(new SqlParameter("@ORDER_DATE     ".Trim(), SqlDbType.DateTime));  //VARCHAR(50)
                cmd.Parameters.Add(new SqlParameter("@RE_SCRAP_STATUS".Trim(), SqlDbType.VarChar, 1));   //INT,
                cmd.Parameters.Add(new SqlParameter("@RE_GROSS_STATUS".Trim(), SqlDbType.VarChar, 1));  //VARCHAR(1),
                cmd.Parameters.Add(new SqlParameter("@RE_GATE_STATUS ".Trim(), SqlDbType.VarChar, 1));     //VARCHAR(50)
                cmd.Parameters.Add(new SqlParameter("@REG_USER       ".Trim(), SqlDbType.VarChar, 50));  //VARCHAR(50)
                cmd.Parameters.Add(new SqlParameter("@UP_USER        ".Trim(), SqlDbType.VarChar, 50));  //VARCHAR(50)
       



                cmd.Parameters["@RESOURCE_NO    ".Trim()].Value = pRESOURCE_NO;
                cmd.Parameters["@LOT_NO         ".Trim()].Value = pLOT_NO;
                cmd.Parameters["@ORDER_DATE     ".Trim()].Value = pORDER_DATE;
                cmd.Parameters["@RE_SCRAP_STATUS".Trim()].Value = pRE_SCRAP_STATUS == "False" ? "N" : "Y";
                cmd.Parameters["@RE_GROSS_STATUS".Trim()].Value = pRE_GROSS_STATUS == "False" ? "N" : "Y";
                cmd.Parameters["@RE_GATE_STATUS ".Trim()].Value = pRE_GATE_STATUS  == "False" ? "N" : "Y"; 
                cmd.Parameters["@REG_USER       ".Trim()].Value = pREG_USER;
                cmd.Parameters["@UP_USER        ".Trim()].Value = pUP_USER;
   





                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new System.Data.DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception err)
            {
                return null;
            }
        }
    }

}
