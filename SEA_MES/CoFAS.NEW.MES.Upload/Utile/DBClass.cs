
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.Upload
{
    public static class DBClass
    {

        //public static string sqlcon = $"Server={CoFAS.NEW.MES.Upload.Properties.Settings.Default.Server_IP};" +
        //                                        $"Database={CoFAS.NEW.MES.Upload.Properties.Settings.Default.Database_Name};" +
        //                                        $"uid={CoFAS.NEW.MES.Upload.Properties.Settings.Default.Uid};" +
        //                                        $"pwd={CoFAS.NEW.MES.Upload.Properties.Settings.Default.Pwd};";

        //public static string sqlcon = "Server=wms.tapex.co.kr;Database=Hansol_Auto_Update;UID=sa;PWD=coever1191!;";

        //대성금형
        //public static string sqlcon = "Server=211.221.27.249,1433;Database=Hansol_Auto_Update;UID=sa;PWD=coever1191!;";

        //이앤아이비
        //public static string sqlcon = "Server=222.113.146.82,11433;Database=Hansol_Auto_Update;UID=sa;PWD=coever1191!;";
        //이튼

        //public static string sqlcon = "Server = 172.22.4.11,60901; Database = HS_MES; UID = MesConnection; PWD=8$dJ@-!W3b-35;";
        public static string sqlcon = "Server=10.10.10.180;Database=HS_MES;UID=hansol_mes;PWD=Hansol123!@#;";
        //public static string sqlcon = "Server= 127.0.0.1;" +
        //                    "Database= HS_MES;" +
        //                    "UID= sa;" +
        //                    "PWD= coever1191!;";

        public static bool DB_Open_Check()
        {
            bool check = true;

            string str = sqlcon+ $"Connection Timeout=10;";

            SqlConnection conn = new SqlConnection(str);
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
        public static DataTable Get_Updateinto()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

               // DB 연결
                string strSql_Insert = @"SELECT [TYPE]
                                                             ,[UPDATETYPE]
                                                             ,[NAME]
                                                             ,[USE_YN]
                                                             ,[REGNT]
                                                     FROM [dbo].[t_updateinto] 
                                                  ORDER BY [USE_YN] DESC,[NAME]";

                //string strSql_Insert = @"SELECT [TYPE]
                //                                             ,[UPDATETYPE]
                //                                             ,[NAME]
                //                                             ,[USE_YN]
                //                                             ,[REGNT]
                //                                        FROM [dbo].[t_updateinto]  
                //                                        WHERE [UPDATETYPE] ='CoFAS.WMS'
                //                                        ORDER BY [USE_YN] DESC,[NAME]";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);



                conn.Open();

                System.Data.SqlClient.SqlDataReader dr;

                dt = new System.Data.DataTable();

                dr = cmd_Insert.ExecuteReader();

                dt.Load(dr);

                conn.Close();

                return dt;

            }
            catch (Exception err)
            {
                return dt;
            }
        }
        public static DataTable Get_Autoupdate(string updatetype)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@"SELECT 
                                                      [UPDATETYPE]
                                                     ,[UTYPE]
                                                     ,[VERSION]
                                                     ,[FILENAME]
                                                     ,[FILENAME2]
                                                     ,[FILEDATE]   
                                                     ,[TEXT]
                                                     ,[UPLOADDATE]
                                                     ,[CRC]
                                                     ,[FILESIZE]
                                                 FROM [dbo].[t_autoupdate]
                                                WHERE [UPDATETYPE] = '{updatetype}'
                                                ORDER BY [UPLOADDATE],[FILENAME] DESC ";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                //cmd_Insert.Parameters.Add("@NO", SqlDbType.Int).Value = int.Parse(textBox6.Text);

                conn.Open();

                System.Data.SqlClient.SqlDataReader dr;

                dt = new System.Data.DataTable();

                dr = cmd_Insert.ExecuteReader();

                dt.Load(dr);

                conn.Close();

                return dt;

            }
            catch (Exception err)
            {
                return dt;
            }
        }
        public static DataTable Get_Autoupdatehistory(string updatetype)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@"SELECT 
       [UPDATETYPE]
      ,[UTYPE]
      ,[VERSION]
      ,[FILENAME]
      ,[FILENAME2]
      ,[FILEDATE]   
      ,[TEXT]
      ,[UPLOADDATE]
      ,[CRC]
      ,[FILESIZE]
  FROM [dbo].[t_autoupdatehistory]
WHERE [UPDATETYPE] = '{updatetype}'
ORDER BY [UPLOADDATE],[FILENAME] DESC ";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                //cmd_Insert.Parameters.Add("@NO", SqlDbType.Int).Value = int.Parse(textBox6.Text);

                conn.Open();

                System.Data.SqlClient.SqlDataReader dr;

                dt = new System.Data.DataTable();

                dr = cmd_Insert.ExecuteReader();

                dt.Load(dr);

                conn.Close();

                return dt;

            }
            catch (Exception err)
            {
                return dt;
            }
        }
        public static DataTable Get_Crc_Autoupdate(string updatetype)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@" select CRC,FILENAME2
                                                       from [dbo].[t_autoupdate]
                                                     where UPDATETYPE = '{updatetype}'";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                conn.Open();

                System.Data.SqlClient.SqlDataReader dr;

                dt = new System.Data.DataTable();

                dr = cmd_Insert.ExecuteReader();

                dt.Load(dr);

                conn.Close();

                return dt;

            }
            catch (Exception err)
            {
                return dt;
            }
        }
        public static DataTable Get_Up_Load_Date_Autoupdate(string updatetype)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@" select UPLOADDATE
                                                       from [dbo].[t_autoupdate]
                                                     where UPDATETYPE = '{updatetype}'";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                conn.Open();

                System.Data.SqlClient.SqlDataReader dr;

                dt = new System.Data.DataTable();

                dr = cmd_Insert.ExecuteReader();

                dt.Load(dr);

                conn.Close();

                return dt;

            }
            catch (Exception err)
            {
                return dt;
            }
        }
        public static bool GetDateToCrc(string crc, string filename)
        {
            bool crcYn = true;

            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                string strSql_Insert = $@"select FILEIMAGE
                    from[dbo].[t_autoupdate]
                    where CRC = '{crc}'";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                conn.Open();

                SqlDataReader reader = cmd_Insert.ExecuteReader();

                byte[] bImage = null;

                while (reader.Read())
                {
                    bImage = (byte[])reader[0];
                }

                conn.Close();

                Crc32 crc32 = new Crc32();

                string tocrc = crc32.byteToCrc(bImage);

                if (crc != tocrc)
                {
                    crcYn = false;
                }
                else
                {
                    string path = "";

                    if (CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_TYPE == "MES")
                    {
                        path = Directory.GetCurrentDirectory() + $"\\{CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_Name}" + "\\" + "System" + "\\" + "PMS" + "\\" + filename;
                    }
                    else if (CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_TYPE == "DMS")
                    {
                        path = Directory.GetCurrentDirectory() + $"\\{CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_Name}" + "\\" + "System" + "\\" + "DMS" + "\\" + filename;
                    }
                    else if (CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_TYPE == "WMS")
                    {
                        path = Directory.GetCurrentDirectory().Substring(0,
                            Directory.GetCurrentDirectory().LastIndexOf("\\")) + "\\" + filename;               
                    }
                    else
                    {
                        path = Directory.GetCurrentDirectory() + $"\\{CoFAS.NEW.MES.Upload.Properties.Settings.Default.Program_Name}" + "\\" + filename;
                    }

                    string dic = Application.StartupPath + "\\Updata";

                    DirectoryInfo di = new DirectoryInfo(dic);

                    if (!di.Exists)
                    {
                        di.Create();
                    }

                    FileInfo info = new FileInfo(path);

                    if (info.Exists)
                    {
                        System.IO.File.Copy(path, dic + "\\" + filename, true);
                     }


                    FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                    fs.Write(bImage, 0, bImage.Length);

                    fs.Close();



                }

                return crcYn;
            }
            catch (Exception err)
            {
                return crcYn = false;
            }
        }
        public static void insert_Autoupdate(string updatetype, string version, string filename, byte[] flie)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@"
                   INSERT INTO [dbo].[t_autoupdate]
                 (
                  [UPDATETYPE]
                 ,[UTYPE]
                 ,[VERSION]
                 ,[FILENAME]
                 ,[FILENAME2]
                 ,[FILEDATE]
                 ,[FILEIMAGE]
                 ,[TEXT]
                 ,[UPLOADDATE]
                 ,[CRC]
                 ,[FILESIZE]
                 )
                   VALUES
                 (
                  @UPDATETYPE
                 ,@UTYPE
                 ,@VERSION
                 ,@FILENAME
                 ,@FILENAME2
                 ,@FILEDATE
                 ,@FILEIMAGE
                 ,@TEXT
                 ,@UPLOADDATE
                 ,@CRC
                 ,@FILESIZE
                )
                 INSERT INTO [dbo].[t_autoupdatehistory]
                  (
                  [UPDATETYPE]
                 ,[UTYPE]
                 ,[VERSION]
                 ,[FILENAME]
                 ,[FILENAME2]
                 ,[FILEDATE]
                 ,[FILEIMAGE]
                 ,[TEXT]
                 ,[UPLOADDATE]
                 ,[CRC]
                 ,[FILESIZE])
                  VALUES
                 (
                  @UPDATETYPE
                 ,@UTYPE
                 ,@VERSION
                 ,@FILENAME
                 ,@FILENAME2
                 ,@FILEDATE
                 ,@FILEIMAGE
                 ,@TEXT
                 ,@UPLOADDATE
                 ,@CRC
                 ,@FILESIZE
                 )";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff");

                Crc32 crc32 = new Crc32();

                cmd_Insert.Parameters.Add("@UPDATETYPE", SqlDbType.VarChar).Value = updatetype;
                cmd_Insert.Parameters.Add("@UTYPE", SqlDbType.VarChar).Value = "FILE";
                cmd_Insert.Parameters.Add("@VERSION", SqlDbType.VarChar).Value = version;
                cmd_Insert.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = filename.ToUpper();
                cmd_Insert.Parameters.Add("@FILENAME2", SqlDbType.VarChar).Value = filename;
                cmd_Insert.Parameters.Add("@FILEDATE", SqlDbType.DateTime).Value = time;
                cmd_Insert.Parameters.Add("@FILEIMAGE", SqlDbType.Image).Value = flie;
                cmd_Insert.Parameters.Add("@TEXT", SqlDbType.VarChar).Value = "";
                cmd_Insert.Parameters.Add("@UPLOADDATE", SqlDbType.DateTime).Value = time;
                cmd_Insert.Parameters.Add("@CRC", SqlDbType.VarChar).Value = crc32.byteToCrc(flie);
                cmd_Insert.Parameters.Add("@FILESIZE", SqlDbType.Int).Value = flie.Length;

                conn.Open();

                cmd_Insert.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public static void insert_UpDateInto(string updatetype, string name)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@"
                 INSERT INTO [dbo].[t_updateinto]
                 (
                  [TYPE]
                 ,[UPDATETYPE]
                 ,[NAME]
                 ,[IP]
                 ,[DB]
                 ,[ID]
                 ,[PASS]
                 ,[USE_YN]
                 ,[REGNT]
                 )
                 VALUES
                 ( 
                  @SQL
                 ,@UPDATETYPE
                 ,@NAME
                 ,@IP
                 ,@DB
                 ,@ID
                 ,@PASS
                 ,@USE_YN
                 ,@REGNT     
                );";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                string time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffff");


                cmd_Insert.Parameters.Add("@SQL", SqlDbType.VarChar).Value ="SQL";
                cmd_Insert.Parameters.Add("@UPDATETYPE", SqlDbType.VarChar).Value = updatetype;
                cmd_Insert.Parameters.Add("@NAME", SqlDbType.VarChar).Value = name;
                cmd_Insert.Parameters.Add("@IP", SqlDbType.VarChar).Value   = "";
                cmd_Insert.Parameters.Add("@DB", SqlDbType.VarChar).Value   = "";
                cmd_Insert.Parameters.Add("@ID", SqlDbType.VarChar).Value   = "";
                cmd_Insert.Parameters.Add("@PASS", SqlDbType.VarChar).Value = "";
                cmd_Insert.Parameters.Add("@USE_YN", SqlDbType.VarChar).Value = "Y";
                cmd_Insert.Parameters.Add("@REGNT", SqlDbType.DateTime).Value = time;


                conn.Open();

                cmd_Insert.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public static void Update_Autoupdate(string updatetype, string filename, byte[] flie,DateTime dateTime)
        {          
            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@"       
                INSERT INTO [dbo].[t_autoupdatehistory]        
                        SELECT * 
                         FROM [dbo].[t_autoupdate] 
                        WHERE UPDATETYPE ='{updatetype}' AND FILENAME = '{filename.ToUpper()}';

                 UPDATE [dbo].[t_autoupdate]
                 SET         
                  [FILEIMAGE] = @FILEIMAGE            
                 ,[UPLOADDATE] = @UPLOADDATE
                 ,[CRC] = @CRC
                 ,[FILESIZE] = @FILESIZE
                 WHERE UPDATETYPE ='{updatetype}' AND FILENAME = '{filename.ToUpper()}'";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                Crc32 crc32 = new Crc32();

                string crc1 = crc32.byteToCrc(flie);

                string crc2 = crc32.byteToCrc(flie);

                while (crc1 != crc2)
                {
                    crc1 = crc32.byteToCrc(flie);

                    crc2 = crc32.byteToCrc(flie);
                }


                cmd_Insert.Parameters.Add("@FILEIMAGE", SqlDbType.Image).Value = flie;
                cmd_Insert.Parameters.Add("@UPLOADDATE", SqlDbType.DateTime).Value = dateTime;
                cmd_Insert.Parameters.Add("@CRC", SqlDbType.VarChar).Value = crc1;
                cmd_Insert.Parameters.Add("@FILESIZE", SqlDbType.Int).Value = flie.Length;

                conn.Open();

                cmd_Insert.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public static void Update_Autoupdate(string updatetype, string version, string filename, byte[] flie, DateTime dateTime)
        {
            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@"       
                INSERT INTO [dbo].[t_autoupdatehistory]        
                        SELECT * 
                         FROM [dbo].[t_autoupdate] 
                        WHERE UPDATETYPE ='{updatetype}' AND FILENAME = '{filename.ToUpper()}';

                 UPDATE [dbo].[t_autoupdate]
                 SET         
                  [FILEIMAGE] = @FILEIMAGE            
                 ,[UPLOADDATE] = @UPLOADDATE
                 ,[CRC] = @CRC
                 ,[FILESIZE] = @FILESIZE
                 ,[VERSION] = @VERSION
                 WHERE UPDATETYPE ='{updatetype}' AND FILENAME = '{filename.ToUpper()}'";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                Crc32 crc32 = new Crc32();

                string crc1 = crc32.byteToCrc(flie);

                string crc2 = crc32.byteToCrc(flie);

                while (crc1 != crc2)
                {
                    crc1 = crc32.byteToCrc(flie);

                    crc2 = crc32.byteToCrc(flie);
                }


                cmd_Insert.Parameters.Add("@FILEIMAGE", SqlDbType.Image).Value = flie;
                cmd_Insert.Parameters.Add("@UPLOADDATE", SqlDbType.DateTime).Value = dateTime;
                cmd_Insert.Parameters.Add("@CRC", SqlDbType.VarChar).Value = crc1;
                cmd_Insert.Parameters.Add("@FILESIZE", SqlDbType.Int).Value = flie.Length;
                cmd_Insert.Parameters.Add("@VERSION", SqlDbType.VarChar).Value = version;

                conn.Open();

                cmd_Insert.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public static void Update_UpDateInto(string key, string updatetype, string name)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@"         
                 UPDATE [dbo].[t_updateinto]
                 SET                        
                 ,[UPDATETYPE] = @UPLOADDATE
                 ,[NAME] = @NAME
                 WHERE UPDATETYPE ='{key}'";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

             
                cmd_Insert.Parameters.Add("@UPLOADDATE", SqlDbType.VarChar).Value = updatetype;
                cmd_Insert.Parameters.Add("@NAME", SqlDbType.VarChar).Value = name;


                conn.Open();

                cmd_Insert.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        public static void Delete_Autoupdatehistor(DataRow row)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@"  
                 DELETE FROM [dbo].[t_autoupdatehistory]
                 WHERE UPDATETYPE ='{row["UPDATETYPE"].ToString()}' 
                      and FILENAME = '{row["FILENAME"].ToString()}'
                      and UPLOADDATE = '{Convert.ToDateTime(row["UPLOADDATE"]).ToString("yyyy-MM-dd HH:mm:ss.fff")}'; ";

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);

                conn.Open();

                cmd_Insert.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception err)
            {

            }
        }
        public static void Delete_Autoupdate(DataRow row)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@"  
                 INSERT INTO [dbo].[t_autoupdatehistory]        
                        SELECT * 
                         FROM [dbo].[t_autoupdate] 
                        WHERE UPDATETYPE ='{row["UPDATETYPE"].ToString()}' AND FILENAME = '{row["FILENAME"].ToString().ToUpper()}';

                 DELETE FROM [dbo].[t_autoupdate]
                 WHERE UPDATETYPE ='{row["UPDATETYPE"].ToString()}' and FILENAME = '{row["FILENAME"].ToString().ToUpper()}';"; 

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);



                conn.Open();

                cmd_Insert.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception err)
            {

            }
        }
        public static void Rollback_Autoupdatehistor(DataRow row)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@"  
                 DELETE FROM [dbo].[t_autoupdate]
                 WHERE UPDATETYPE ='{row["UPDATETYPE"].ToString()}' 
                     AND FILENAME = '{row["FILENAME"].ToString()}';

                   INSERT INTO [dbo].[t_autoupdate]        
                        SELECT * 
                         FROM [dbo].[t_autoupdatehistory] 
                        WHERE UPDATETYPE ='{row["UPDATETYPE"].ToString()}' 
                            AND FILENAME = '{row["FILENAME"].ToString().ToUpper()}'
                            AND UPLOADDATE = '{Convert.ToDateTime(row["UPLOADDATE"]).ToString("yyyy-MM-dd HH:mm:ss.fff")}'; ";
              

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);



                conn.Open();

                cmd_Insert.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception err)
            {

            }
        }
        public static void Delete_UpdateInto(string updatetype)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conn = new SqlConnection(sqlcon);

                // DB 연결
                string strSql_Insert = $@"         
                 UPDATE [dbo].[t_updateinto]
                       SET [USE_YN] = 'N'
                 WHERE UPDATETYPE ='{updatetype}'";           

                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, conn);



                conn.Open();

                cmd_Insert.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception err)
            {

            }
        }
    }
}
