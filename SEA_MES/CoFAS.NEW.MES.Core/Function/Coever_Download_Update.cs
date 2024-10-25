using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoFAS.NEW.MES.Core.Function
{
    class Coever_Download_Update
    {
        public string sqlcon = $"Server = db3.coever.co.kr;" +
                               $"Database= Hansol_Auto_Update;" +
                               $"uid = sa;" +
                               $"pwd = Codb89897788@$^;";

        string _key = "Download";

        string _startupPath = "";

        public Coever_Download_Update(string startupPath)
        {
            _startupPath = startupPath;

            string path = _startupPath + "\\UpDate";

            DirectoryInfo di = new DirectoryInfo(path);

            if (!di.Exists)
            {
                di.Create();
            }

        }
        public void UpDate_Check()
        {
            string path = _startupPath + "\\STARTDATA.txt";

            try
            {

                FileInfo info = new FileInfo(path);

                bool startYN = true;

                if (info.Exists)
                {
                    startYN = true;
                }
                else
                {
                    File.WriteAllText(_startupPath + "\\STARTDATA.txt", DateTime.Parse("2022-01-01").ToString());
                    startYN = false;
                }

                // DB 연결 체크
                if (DB_Open_Check())
                {
                    DataTable dt = Get_Up_Load_Date_Autoupdate(_key);

                    DateTime version = new DateTime();

                    string st = File.ReadAllText(path);

                    if (!DateTime.TryParse(st, out version))
                    {
                        version = DateTime.Parse("2022-01-01");
                    }

                    bool upcheck = true;

                    foreach (DataRow item in dt.Rows)
                    {
                        DateTime get_version = Convert.ToDateTime(item[0].ToString());

                        if (version < get_version)
                        {
                            File.WriteAllText(_startupPath + "\\STARTDATA.txt", get_version.ToString("yyyy-MM-dd HH:mm:ss"));

                            upcheck = false;
                        }
                    }

                    if (upcheck && startYN)
                    {
                        return;
                    }
                    else
                    {
                        ProgValueSetting();
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception err)
            {             
                File.Delete(_startupPath + "\\STARTDATA.txt");
            }
        }
        private bool DB_Open_Check()
        {
            bool check = true;


            SqlConnection conn = new SqlConnection(sqlcon + "Connection Timeout=1;");
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
                return  false;
            }
        }
        private DataTable Get_Up_Load_Date_Autoupdate(string updatetype)
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

        private void ProgValueSetting()
        {
            try
            {
                DataTable dt = Get_Crc_Autoupdate(_key);

                if (dt.Rows.Count == 0)
                {
                    return;

                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string crc = dt.Rows[i][0].ToString();

                    string filename = dt.Rows[i][1].ToString();

                    bool crcYn = false;

                    while (!crcYn)
                    {
                        crcYn = GetDateToCrc(crc, filename);
                    }
                }

            }
            catch (Exception err)
            {
                File.Delete(_startupPath + "\\STARTDATA.txt");
            }
        }

        public  DataTable Get_Crc_Autoupdate(string updatetype)
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

        public  bool GetDateToCrc(string crc, string filename)
        {         
            try
            {
                bool crcYn = true;

                SqlConnection conn = new SqlConnection(sqlcon);

                string strSql_Insert = $@"select FILEIMAGE
                                            from [dbo].[t_autoupdate]
                                           where CRC = '{crc}' and FILEIMAGE = '{filename}'";

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
                    string path = _startupPath + "\\UpDate" +"\\" + filename;   
                  
                    FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                    fs.Write(bImage, 0, bImage.Length);

                    fs.Close();
                }

                return crcYn;
            }
            catch (Exception err)
            {
                return  false;
            }
        }
    }

    public sealed class Crc32 : HashAlgorithm
    {
        public const UInt32 DefaultPolynomial = 0xedb88320u;
        public const UInt32 DefaultSeed = 0xffffffffu;

        static UInt32[] defaultTable;

        readonly UInt32 seed;
        readonly UInt32[] table;
        UInt32 hash;

        public Crc32()
            : this(DefaultPolynomial, DefaultSeed)
        {
        }

        public Crc32(UInt32 polynomial, UInt32 seed)
        {
            if (!BitConverter.IsLittleEndian)
                throw new PlatformNotSupportedException("Not supported on Big Endian processors");

            table = InitializeTable(polynomial);
            this.seed = hash = seed;
        }

        public override void Initialize()
        {
            hash = seed;
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            hash = CalculateHash(table, hash, array, ibStart, cbSize);
        }

        protected override byte[] HashFinal()
        {
            var hashBuffer = UInt32ToBigEndianBytes(~hash);
            HashValue = hashBuffer;
            return hashBuffer;
        }

        public override int HashSize { get { return 32; } }

        public byte[] GetPhoto(string filePath)
        {
            FileStream stream = new FileStream(
                filePath, FileMode.Open, FileAccess.Read);

            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }
        public String byteToCrc(byte[] by)
        {
            String hash = String.Empty;
            try
            {
                Crc32 crc32 = new Crc32();


                foreach (byte b in crc32.ComputeHash(by))
                {
                    hash += b.ToString("x2").ToLower();
                }

                return hash;
            }
            catch (Exception err)
            {
                return hash;
            }
        }
        public static UInt32 Compute(byte[] buffer)
        {
            return Compute(DefaultSeed, buffer);
        }

        public static UInt32 Compute(UInt32 seed, byte[] buffer)
        {
            return Compute(DefaultPolynomial, seed, buffer);
        }

        public static UInt32 Compute(UInt32 polynomial, UInt32 seed, byte[] buffer)
        {
            return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
        }

        static UInt32[] InitializeTable(UInt32 polynomial)
        {
            if (polynomial == DefaultPolynomial && defaultTable != null)
                return defaultTable;

            var createTable = new UInt32[256];
            for (var i = 0; i < 256; i++)
            {
                var entry = (UInt32)i;
                for (var j = 0; j < 8; j++)
                    if ((entry & 1) == 1)
                        entry = (entry >> 1) ^ polynomial;
                    else
                        entry >>= 1;
                createTable[i] = entry;
            }

            if (polynomial == DefaultPolynomial)
                defaultTable = createTable;

            return createTable;
        }

        static UInt32 CalculateHash(UInt32[] table, UInt32 seed, IList<byte> buffer, int start, int size)
        {
            var hash = seed;
            for (var i = start; i < start + size; i++)
                hash = (hash >> 8) ^ table[buffer[i] ^ hash & 0xff];
            return hash;
        }

        static byte[] UInt32ToBigEndianBytes(UInt32 uint32)
        {
            var result = BitConverter.GetBytes(uint32);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }
    }
}
