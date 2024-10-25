
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoFAS.NEW.MES.DPS
{
    class MY_Core
    {

        public void GET_FTP_LIST()
        {
            string ftpServer = "ftp://172.22.4.11";
            string ftpFolderPath = "/complete_N";
            string ftpUsername = "mesftp01";
            string ftpPassword = "KrpyuMES!615";

            //string ftpServer = "ftp://192.168.9.15:8500";
            //string ftpFolderPath = "/COEVER/complete_N";
            //string ftpUsername = "ftpadmin";
            //string ftpPassword = "coever119!QAZ";
            try
            {
                string[] fileList = GetFileList(ftpServer, ftpFolderPath, ftpUsername, ftpPassword);


                foreach (string fileName in fileList)
                {
                    //string qFTPPath, string qFileName, string qFTP_ID, string qFTP_PW, string qFTPLocalPath
                    //FTPDownLoad(ftpServer, ftpFolderPath, fileName, ftpUsername, ftpPassword, Directory.GetCurrentDirectory());
                    //Console.WriteLine(fileName);
                }
            }
            
             catch (Exception ex)
            {
                // Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static string[] GetFileList(string ftpServer, string folderPath, string username, string password)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create($"{ftpServer}{folderPath}");
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpRequest.Credentials = new NetworkCredential(username, password);

                using (FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(ftpResponse.GetResponseStream()))
                    {
                        string fileList = reader.ReadToEnd();
                        return fileList.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public void FTPDownLoad(string qFTPPath, string folderPath, string qFileName, string qFTP_ID, string qFTP_PW, string qFTPLocalPath)
        //{
        //    FileInfo f = new FileInfo(qFTPLocalPath + qFileName);
        //    if (f.Exists)
        //    {
        //        f.Delete();
        //    }

        //    FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(qFTPPath + folderPath +"/"+ qFileName);
        //    requestFileDownload.Credentials = new NetworkCredential(qFTP_ID, qFTP_PW);
        //    requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;

        //    FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();

        //    Stream responseStream = responseFileDownload.GetResponseStream();
        //    FileStream writeStream = new FileStream(qFTPLocalPath + "\\"+qFileName, FileMode.Create);

        //    int pLength = 102400;
        //    Byte[] buffer = new Byte[pLength];
        //    int bytesRead = responseStream.Read(buffer, 0, pLength);

        //    while (bytesRead > 0)
        //    {
        //        writeStream.Write(buffer, 0, bytesRead);
        //        bytesRead = responseStream.Read(buffer, 0, pLength);
        //    }

        //    responseStream.Close();
        //    writeStream.Close();

        //    if (qFileName.Contains("작업오더"))
        //    {
        //        DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
        //        sc.LoadDocument(qFTPLocalPath + "\\" + qFileName, DocumentFormat.Xlsx);

        //        Worksheet sheet = sc.Document.Worksheets[0];
        //        for (int i = 1; i < 10000; i++)
        //        {
        //            string[] strs = sheet.Rows[i][4].Value.ToString().Split('/');


        //            if (strs.Length == 3)
        //            {
        //                int su = 0;

        //                if (int.TryParse(sheet.Rows[i][3].Value.ToString().Trim(), out su))
        //                {
        //                    if (su > 0)
        //                    {
        //                        string @제품코드  = "'" + sheet.Rows[i][0].Value.ToString().Trim() + "'";
        //                        string @수량      = "'" + sheet.Rows[i][3].Value.ToString().Trim() + "'";
        //                        string @등록자    = "'MFG_PRO'";
        //                        string @일자      = $"'20{strs[2]}-{strs[0]}-{strs[1]}'";

        //                        string sql = $@"IF NOT EXISTS (select * from [dbo].[ORDER_MST] where FORMAT(ORDER_DATE, 'yyyy-MM-dd') = {@일자})
        //                                       BEGIN
        //                                       INSERT INTO[dbo].[ORDER_MST]
        //                                        ([OUT_CODE]
        //                                        ,[NAME]
        //                                        ,[ORDER_DATE]
        //                                        ,[ORDER_COMPANY]
        //                                        ,[ORDER_TYPE]
        //                                        ,[CURRENCY_TYPE]
        //                                        ,[EXCHANGE_RATE]
        //                                        ,[MATERIAL_COST]
        //                                        ,[MANUFACTURE_COST]
        //                                        ,[ETC_COST]
        //                                        ,[TOTAL_COST]
        //                                        ,[COMMENT]
        //                                        ,[COMPLETE_YN]
        //                                        ,[USE_YN]
        //                                        ,[REG_USER]
        //                                        ,[REG_DATE]
        //                                        ,[UP_USER]
        //                                        ,[UP_DATE])
        //                                  SELECT                                                
        //                                          {@일자} +'_' + {@제품코드}	
        //                                         ,{@일자} +'_' + {@제품코드}	
        //                                         ,{@일자}				  		
        //                                         ,21
        //                                         ,'SD09001'
        //                                         ,''
        //                                         ,''
        //                                         ,0
        //                                         ,0
        //                                         ,0
        //                                         ,0
        //                                         ,''
        //                                         ,'Y'
        //                                         ,'Y'
        //                                         ,{@등록자}				
        //                                         ,{@일자}			   
        //                                         ,{@등록자}				 
        //                                         ,{@일자}
        //                                           FROM STOCK_MST A                   
        //                                           WHERE A.OUT_CODE = {@제품코드};

        //                            END";
        //                        DataTable _DataTable = SELECT(sql);
        //                        sql = $@"INSERT INTO[dbo].[ORDER_DETAIL]
        //                                        ([ORDER_MST_ID]
        //                                        ,[STOCK_MST_ID]
        //                                        ,[STOCK_MST_OUT_CODE]
        //                                        ,[STOCK_MST_STANDARD]
        //                                        ,[STOCK_MST_TYPE]
        //                                        ,[SUPPLY_TYPE]
        //                                        ,[STOCK_MST_UNIT]
        //                                        ,[STOCK_MST_PRICE]
        //                                        ,[ORDER_QTY]
        //                                        ,[COST]
        //                                        ,[DEMAND_COMPANY]
        //                                        ,[DEMAND_DATE]
        //                                        ,[COMMENT]
        //                                        ,[INSPECTION_YN]
        //                                        ,[COMPLETE_YN]
        //                                        ,[USE_YN]
        //                                        ,[REG_USER]
        //                                        ,[REG_DATE]
        //                                        ,[UP_USER]
        //                                        ,[UP_DATE])
        //                                        SELECT top 1 D.ID
        //                                               ,A.ID
        //                                               ,A.ID
        //                                               ,A.ID
        //                                               ,A.ID
        //                                               ,''
        //                                               ,A.ID
        //                                               ,1000
        //                                               ,{@수량}					 						
        //                                               ,0
        //                            	   ,D.ORDER_COMPANY
        //                            	   ,{@일자}                           
        //                                               ,''
        //                                               ,'Y'
        //                            	   ,'Y'
        //                                               ,'Y'
        //                                               ,{@등록자}											
        //                                               ,{@일자}				   								
        //                                               ,{@등록자}				 							
        //                                               ,{@일자}
        //                                           FROM STOCK_MST A
        //                                           INNER JOIN[dbo].[ORDER_MST] D ON FORMAT(ORDER_DATE, 'yyyy-MM-dd') = {@일자}
        //                                           WHERE A.OUT_CODE = {@제품코드}";

        //                        _DataTable = SELECT(sql);
        //                    }
        //                }
        //            }




        //        }
        //    }
        //    else if (qFileName.Contains("실적처리"))
        //    {
        //        DevExpress.XtraSpreadsheet.SpreadsheetControl sc = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
        //        sc.LoadDocument(qFTPLocalPath + "\\" + qFileName, DocumentFormat.Xls);

        //        Worksheet sheet = sc.Document.Worksheets[0];
        //        for (int i = 1; i < 10000; i++)
        //        {

        //            int su = 0;

         
        //            if (int.TryParse(sheet.Rows[i][11].Value.ToString().Trim(), out su))
        //            {
        //                if (su > 0)
        //                {
        //                    string @제품코드  = "'" + sheet.Rows[i][14].Value.ToString().Trim() + "'";
        //                    string @수량      = "'" + sheet.Rows[i][11].Value.ToString().Trim() + "'";
        //                    string @등록자    = "'MFG_PRO'";
        //                    string @일자      = "'" + Convert.ToDateTime(sheet.Rows[i][24].Value.ToString().Trim()).ToString("yyyy-MM-dd") + "'";

        //                    string sql = $@"INSERT INTO [dbo].[IN_STOCK_DETAIL]
        //                    ([OUT_CODE]
        //                    ,[IN_STOCK_DATE]
        //                    ,[IN_TYPE]
        //                    ,[IN_STOCK_MST_ID]
        //                    ,[ORDER_DETAIL_ID]
        //                    ,[PRODUCTION_RESULT_ID]
        //                    ,[STOCK_MST_ID]
        //                    ,[STOCK_MST_OUT_CODE]
        //                    ,[STOCK_MST_STANDARD]
        //                    ,[STOCK_MST_TYPE]
        //                    ,[IN_QTY]
        //                    ,[USED_QTY]
        //                    ,[REMAIN_QTY]
        //                    ,[COMMENT]
        //                    ,[COMPLETE_YN]
        //                    ,[USE_YN]
        //                    ,[REG_USER]
        //                    ,[REG_DATE]
        //                    ,[UP_USER]
        //                    ,[UP_DATE])
        //                    select {@일자} +'_' + OUT_CODE
        //                         ,{@일자}
        //                         ,'SD13005'
        //                         ,0
        //                         ,0
        //                         ,0
        //                         ,ID
        //                         ,ID
        //                         ,ID
        //                         ,ID
        //                         ,{@수량}
        //                         ,{@수량}
        //                         ,0
        //                         ,''
        //                         ,'Y'
        //                         ,'Y'
        //                         ,{@등록자}
        //                         ,{@일자}
        //                         ,{@등록자}
        //                         ,{@일자}
        //                         from STOCK_MST
        //                        where OUT_CODE = {@제품코드};";
        //                    DataTable _DataTable = SELECT(sql);                       
        //                }
        //            }





        //        }
        //    }
        //    FtpWebRequest requestFileDelete = (FtpWebRequest)WebRequest.Create(qFTPPath + folderPath +"/"+ qFileName);
        //    requestFileDelete.Credentials = new NetworkCredential(qFTP_ID, qFTP_PW);
        //    requestFileDelete.Method = WebRequestMethods.Ftp.DeleteFile;

        //    FtpWebResponse responseFileDelete = (FtpWebResponse)requestFileDelete.GetResponse();

        //    folderPath = "/complete_Y";

        //    CoFAS_FTPUtilManager qFTPUtil = new CoFAS_FTPUtilManager(qFTPPath, qFTP_ID, qFTP_PW);
        //    if (qFTPUtil.IsFTPFileExsit(qFTPPath + folderPath + "/" + qFileName))
        //    {
        //        // 파일삭제하기
        //        FtpWebRequest requestFileDelete2 = (FtpWebRequest)WebRequest.Create(qFTPPath + folderPath +"/"+ qFileName);
        //        requestFileDelete2.Credentials = new NetworkCredential(qFTP_ID, qFTP_PW);
        //        requestFileDelete2.Method = WebRequestMethods.Ftp.DeleteFile;

        //        FtpWebResponse responseFileDelete2 = (FtpWebResponse)requestFileDelete2.GetResponse();
        //    }

        //    FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(qFTPPath + folderPath +"/"+ qFileName);
        //    requestFTPUploader.Credentials = new NetworkCredential(qFTP_ID, qFTP_PW);
        //    requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;

        //    FileInfo fileInfo = new FileInfo(qFTPLocalPath + "\\" + qFileName);
        //    FileStream fileStream = fileInfo.OpenRead();

        //    int bufferLength = 102400;
        //    buffer = new byte[bufferLength];

        //    Stream uploadStream = requestFTPUploader.GetRequestStream();
        //    int contentLength = fileStream.Read(buffer, 0, bufferLength);

        //    while (contentLength != 0)
        //    {
        //        uploadStream.Write(buffer, 0, contentLength);
        //        contentLength = fileStream.Read(buffer, 0, bufferLength);
        //    }

        //    uploadStream.Close();
        //    fileStream.Close();

        //    requestFTPUploader = null;
        //    //Process.Start(qFTPLocalPath);   //다운로드된 폴더 열기
        //}

        //public void FTPUpload(string pFileName, string qFullName, string qFTP_ID, string qFTP_PW, string qFTP_Path)
        //{
        //    try
        //    {
        //        // 파일존재확인
        //        CoFAS_FTPUtilManager qFTPUtil = new CoFAS_FTPUtilManager(qFTP_Path, qFTP_ID, qFTP_PW);
        //        if (qFTPUtil.IsFTPFileExsit(qFTP_Path + pFileName))
        //        {
        //            // 파일삭제하기
        //            FtpWebRequest requestFileDelete = (FtpWebRequest)WebRequest.Create("ftp://" + qFTP_Path + pFileName);
        //            requestFileDelete.Credentials = new NetworkCredential(qFTP_ID, qFTP_PW);
        //            requestFileDelete.Method = WebRequestMethods.Ftp.DeleteFile;

        //            FtpWebResponse responseFileDelete = (FtpWebResponse)requestFileDelete.GetResponse();
        //        }

        //        FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create("ftp://" + qFTP_Path + pFileName);
        //        requestFTPUploader.Credentials = new NetworkCredential(qFTP_ID, qFTP_PW);
        //        requestFTPUploader.Method = WebRequestMethods.Ftp.UploadFile;

        //        FileInfo fileInfo = new FileInfo(qFullName);
        //        FileStream fileStream = fileInfo.OpenRead();

        //        int bufferLength = 102400;
        //        byte[] buffer = new byte[bufferLength];

        //        Stream uploadStream = requestFTPUploader.GetRequestStream();
        //        int contentLength = fileStream.Read(buffer, 0, bufferLength);

        //        while (contentLength != 0)
        //        {
        //            uploadStream.Write(buffer, 0, contentLength);
        //            contentLength = fileStream.Read(buffer, 0, bufferLength);
        //        }

        //        uploadStream.Close();
        //        fileStream.Close();

        //        requestFTPUploader = null;
        //    }
        //    catch (Exception pException)
        //    {
        //        return;
        //    }
        //}
        public DataTable SELECT(string sql)
        {

            try
            {
                //string strcon = $"Server=172.22.4.11,60901;Database=HS_MES;UID=MesConnection;PWD=8$dJ@-!W3b-35;";
                string strcon = "Server = 127.0.0.1; Database = HS_MES;UID = sa;PWD = coever1191!";
                SqlConnection con = new SqlConnection(strcon);
                string strSql_Insert = sql;



                SqlCommand cmd_Insert = new SqlCommand(strSql_Insert, con);


                // DB처리
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd_Insert;
                DataTable ds = new System.Data.DataTable();
                con.Open();
                da.Fill(ds);
                con.Close();
                return ds;

            }
            catch (Exception err)
            {
                return null;
            }
        }

    }

    public class CoFAS_FTPUtilManager
    {
        #region 멤버 변수 & 프로퍼티

        /// <summary>
        ///
        /// </summary>
        private string host;

        /// <summary>
        /// FTP 서버 호스트명(IP)를 가져옵니다.
        /// </summary>
        public string Host
        {
            get { return host; }
            private set { host = value; }
        }

        private string userName;

        /// <summary>
        /// 사용자 명을 가져옵니다.
        /// </summary>
        public string UserName
        {
            get { return userName; }
            private set { userName = value; }
        }

        private string password;

        /// <summary>
        /// 비밀번호를 가져옵니다.
        /// </summary>
        public string Password
        {
            get { return password; }
            private set { password = value; }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// FXFtpUtil의 새 인스턴스를 생성합니다.
        /// </summary>
        /// <param name="host">FTP 서버 주소 입니다.</param>
        /// <param name="userName">FTP 사용자 아이디 입니다.</param>
        /// <param name="password">FTP 비밀번호 입니다.</param>
        public CoFAS_FTPUtilManager(string host, string userName, string password)
        {
            this.Host = host;
            this.UserName = userName;
            this.Password = password;
        }

        #endregion

        #region 메서드

        /// <summary>
        /// 파일을 FTP 서버에 업로드 합니다.
        /// </summary>
        /// <param name="localFileFullPath">로컬 파일의 전체 경로 입니다.</param>
        /// <param name="ftpFilePath"><para>파일을 업로드 할 FTP 전체 경로 입니다.</para><para>하위 디렉터리에 넣는 경우 /디렉터리명/파일명.확장자 형태 입니다.</para></param>
        /// <exception cref="FileNotFoundException">지정한 로컬 파일(localFileFullPath)이 없을 때 발생합니다.</exception>
        /// <returns>업로드 성공 여부입니다.</returns>
        public bool Upload(string localFileFullPath, string ftpFilePath)
        {
            LocalFileValidationCheck(localFileFullPath);

            FTPDirectioryCheck(GetDirectoryPath(ftpFilePath));
            if (IsFTPFileExsit(ftpFilePath))
            {
                //throw new ApplicationException(string.Format("{0}은 이미 존재하는 파일 입니다.", ftpFilePath));
            }
            string ftpFileFullPath = string.Format("ftp://{0}/{1}", this.Host, ftpFilePath);
            FtpWebRequest ftpWebRequest = WebRequest.Create(new Uri(ftpFileFullPath)) as FtpWebRequest;
            ftpWebRequest.Credentials = GetCredentials();
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.UsePassive = true;
            ftpWebRequest.Timeout = 10000;
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;

            FileInfo fileInfo = new FileInfo(localFileFullPath);
            FileStream fileStream = fileInfo.OpenRead();
            Stream stream = null;
            byte[] buf = new byte[2048];
            int currentOffset = 0;
            try
            {
                stream = ftpWebRequest.GetRequestStream();
                currentOffset = fileStream.Read(buf, 0, 2048);
                while (currentOffset != 0)
                {
                    stream.Write(buf, 0, currentOffset);
                    currentOffset = fileStream.Read(buf, 0, 2048);
                }

            }
            finally
            {
                fileStream.Dispose();
                if (stream != null)
                    stream.Dispose();
            }

            return true;
        }

        /// <summary>
        /// 해당 경로에 파일이 존재하는지 여부를 가져옵니다.
        /// </summary>
        /// <param name="ftpFilePath">파일 경로</param>
        /// <returns>존재시 참 </returns>
        public bool IsFTPFileExsit(string ftpFilePath)
        {
            string fileName = GetFileName(ftpFilePath);
            string ftpFileFullPath = string.Format("ftp:/{0}/%2F/{1}", GetDirectoryPath(ftpFilePath), fileName);
            FtpWebRequest ftpWebRequest = WebRequest.Create(new Uri(ftpFileFullPath)) as FtpWebRequest;
            ftpWebRequest.Credentials = new NetworkCredential(this.UserName, this.Password);
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.UsePassive = true;
            ftpWebRequest.Timeout = 10000;
            ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            FtpWebResponse response = null;
            string data = string.Empty;
            try
            {
                response = ftpWebRequest.GetResponse() as FtpWebResponse;
                if (response != null)
                {
                    StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                    data = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            string[] directorys = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (directorys.Length > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// FTP 풀 경로에서 Directory 경로만 가져옵니다.
        /// </summary>
        /// <param name="ftpFilePath">FTP 풀 경로</param>
        /// <returns>디렉터리 경로입니다.</returns>
        private string GetDirectoryPath(string ftpFilePath)
        {
            string[] datas = ftpFilePath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            string directoryPath = string.Empty;

            for (int i = 0; i < datas.Length - 1; i++)
            {
                directoryPath += string.Format("/{0}", datas[i]);
            }
            return directoryPath;
        }

        /// <summary>
        /// FTP 풀 경로에서 파일이름만 가져옵니다.
        /// </summary>
        /// <param name="ftpFilePath">FTP 풀 경로</param>
        /// <returns>파일명입니다.</returns>
        private string GetFileName(string ftpFilePath)
        {
            string[] datas = ftpFilePath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            return datas[datas.Length - 1];
        }

        /// <summary>
        /// FTP 경로의 디렉토리를 점검하고 없으면 생성
        /// </summary>
        /// <param name="directoryPath">디렉터리 경로 입니다.</param>
        public void FTPDirectioryCheck(string directoryPath)
        {
            string[] directoryPaths = directoryPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            string currentDirectory = string.Empty;
            foreach (string directory in directoryPaths)
            {
                currentDirectory += string.Format("/{0}", directory);
                if (!IsExtistDirectory(currentDirectory))
                {
                    MakeDirectory(currentDirectory);
                }
            }
        }

        /// <summary>
        /// FTP에 해당 디렉터리가 있는지 알아온다.
        /// </summary>
        /// <param name="currentDirectory">디렉터리 명</param>
        /// <returns>있으면 참</returns>
        private bool IsExtistDirectory(string currentDirectory)
        {
            string ftpFileFullPath = string.Format("ftp://{0}{1}", this.Host, GetParentDirectory(currentDirectory));
            FtpWebRequest ftpWebRequest = WebRequest.Create(new Uri(ftpFileFullPath)) as FtpWebRequest;
            ftpWebRequest.Credentials = new NetworkCredential(this.UserName, this.Password);
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.UsePassive = true;
            ftpWebRequest.Timeout = 10000;
            ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            FtpWebResponse response = null;
            string data = string.Empty;
            try
            {
                response = ftpWebRequest.GetResponse() as FtpWebResponse;
                if (response != null)
                {
                    StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default);

                    data = streamReader.ReadToEnd();
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            string[] directorys = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return (from directory in directorys
                    select directory.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    into directoryInfos
                    where directoryInfos[0][0] == 'd'
                    select directoryInfos[8]).Any(
                        name => name == (currentDirectory.Split('/')[currentDirectory.Split('/').Length - 1]).ToString());
        }

        /// <summary>
        /// FTP에 해당 디렉터리가 있는지 알아온다.
        /// </summary>
        /// <param name="currentDirectory">디렉터리 명</param>
        /// <param name="checkDirectory">확인 할 디렉터리 명</param>
        /// <returns>있으면 참</returns>
        public bool FtpDirectoryExists(string currentDirectory, string checkDirectory)
        {
            string ftpFileFullPath = string.Format("ftp://{0}", currentDirectory);
            //string ftpFileFullPath = string.Format("ftp://{0}", currentDirectory + "/%2F/" + checkDirectory); 
            FtpWebRequest ftpWebRequest = WebRequest.Create(new Uri(ftpFileFullPath)) as FtpWebRequest;
            ftpWebRequest.Credentials = new NetworkCredential(this.UserName, this.Password);
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.UsePassive = true;
            ftpWebRequest.Timeout = 10000;
            ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = null;

            bool isCheckDirectory = true;
            try
            {
                response = ftpWebRequest.GetResponse() as FtpWebResponse;
                if (response != null)
                {
                    StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default);

                    string directoryList = streamReader.ReadToEnd();

                    string[] directoryLists = directoryList.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    if (directoryLists == null || directoryLists.Length == 0)
                    {
                        isCheckDirectory = false;
                    }
                    else
                    {
                        for (int i = 0; i < directoryLists.Length; i++)
                        {
                            string name_Directory = directoryLists[i];
                            if (checkDirectory.Equals(name_Directory))
                            {
                                isCheckDirectory = true;
                                return isCheckDirectory;
                            }
                            else
                            {
                                isCheckDirectory = false;
                            }
                        }
                    }
                }
            }
            catch (Exception pException)
            {
                return false;
            }
            return isCheckDirectory;
        }

        /// <summary>
        /// 상위 디렉터리를 알아옵니다.
        /// </summary>
        /// <param name="currentDirectory"></param>
        /// <returns></returns>
        private string GetParentDirectory(string currentDirectory)
        {
            string[] directorys = currentDirectory.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            string parentDirectory = string.Empty;
            for (int i = 0; i < directorys.Length - 1; i++)
            {
                parentDirectory += "/" + directorys[i];
            }

            return parentDirectory;
        }

        /// <summary>
        /// 인증을 가져옵니다.
        /// </summary>
        /// <returns>인증</returns>
        private ICredentials GetCredentials()
        {
            return new NetworkCredential(this.UserName, this.Password);
        }

        private string GetStringResponse(FtpWebRequest ftp)
        {
            string result = "";
            using (FtpWebResponse response = (FtpWebResponse)ftp.GetResponse())
            {
                long size = response.ContentLength;
                using (Stream datastream = response.GetResponseStream())
                {
                    if (datastream != null)
                    {
                        using (StreamReader sr = new StreamReader(datastream))
                        {
                            result = sr.ReadToEnd();
                            sr.Close();
                        }

                        datastream.Close();
                    }
                }

                response.Close();
            }

            return result;
        }

        private FtpWebRequest GetRequest(string URI)
        {
            FtpWebRequest result = (FtpWebRequest)WebRequest.Create(URI);
            result.Credentials = GetCredentials();
            result.KeepAlive = false;
            return result;
        }

        /// <summary>
        /// FTP에 해당 디렉터리를 만든다.
        /// </summary>
        /// <param name="dirpath"></param>
        public bool MakeDirectory(string dirpath)
        {
            string URI = string.Format("ftp://{0}/{1}", this.Host, dirpath);
            System.Net.FtpWebRequest ftp = GetRequest(URI);
            ftp.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory;

            try
            {
                string str = GetStringResponse(ftp);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// FTP에 해당 디렉터리를 만든다.
        /// </summary>
        /// <param name="dirpath"></param>
        public void MakeDirectory_AfterChecking(string dirpath)
        {
            string URI = string.Format("ftp://{0}", dirpath);
            System.Net.FtpWebRequest ftp = GetRequest(URI);
            ftp.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory;

            try
            {
                string str = GetStringResponse(ftp);
            }
            catch (Exception pException)
            {
            }
        }

        /// <summary>
        /// 지정한 로컬 파일이 실제 존재하는지 확인합니다.
        /// </summary>
        /// <param name="localFileFullPath">로컬 파일의 전체 경로입니다.</param>
        private void LocalFileValidationCheck(string localFileFullPath)
        {
            if (!File.Exists(localFileFullPath))
            {
                throw new FileNotFoundException(string.Format("The specified file does not exist.\nLocalPath : {0}", localFileFullPath));
            }
        }
        #endregion
    }
}
