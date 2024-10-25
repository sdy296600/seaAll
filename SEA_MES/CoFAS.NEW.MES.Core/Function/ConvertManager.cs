using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CoFAS.NEW.MES.Core.Function
{
    public class ConvertManager
    {
        /// <summary>
        /// 날짜 형식
        /// </summary>
        public enum enDateType
        {
            /// <summary>
            /// yyyy-MM-dd HH:mm:ss
            /// </summary>
            DateTime,
            /// <summary>
            /// MM-dd HH:mm:ss
            /// </summary>
            DateTimeShort,
            /// <summary>
            /// yyyy-MM-dd
            /// </summary>
            Date,
            /// <summary>
            /// HH:mm:ss
            /// </summary>
            Time,
            /// <summary>
            /// yyyy
            /// </summary>
            Year,
            /// <summary>
            /// MM
            /// </summary>
            Month,
            /// <summary>
            /// dd
            /// </summary>
            Day,
            /// <summary>
            /// HH
            /// </summary>
            Hour,
            /// <summary>
            /// mm
            /// </summary>
            Minute,
            /// <summary>
            /// ss
            /// </summary>
            Second,
            /// <summary>
            /// yyyyMMdd
            /// </summary>
            DBType,
            /// <summary>
            /// HHmm
            /// </summary>
            HourMinutes
        };

        /// <summary>
        /// 해당월의 첫번째, 마지막 날짜를 스트링으로 리턴
        /// </summary>
        /// <param name="dtToday">해당 월</param>
        /// <param name="strFirst">월 첫번째 날짜</param>
        /// <param name="strLast">월 마지막 날짜</param>
        public void FirstLastDayOfMonth(DateTime dtToday, ref string strFirst, ref string strLast)
        {

            //DateTime dtToday = DateTime.Today;
            DateTime dtFirstDay = dtToday.AddDays(1 - dtToday.Day);
            DateTime dtLastDay = dtToday.AddMonths(1).AddDays(0 - dtToday.Day);

            strFirst = dtFirstDay.ToString("yyyy.MM.dd");

            strLast = dtLastDay.ToString("yyyy.MM.dd");
        }

        /// <summary>
        /// right함수 구현
        /// </summary>
        /// <param name="str"></param>
        /// <param name="intLen"></param>
        /// <returns></returns>
        public static string Right(string str, int intLen)
        {
            return str.Substring(str.Length - intLen, intLen);
        }

        /// <summary>
        /// 시간을 문자열로 변경 하여 준다.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="enDType"></param>
        /// <returns></returns>
        public static string Date2String(DateTime dt, enDateType enDType)
        {
            string strRst = string.Empty;

            switch (enDType)
            {
                case enDateType.DateTime:
                    strRst = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    break;

                case enDateType.DateTimeShort:
                    strRst = dt.ToString("MM-dd HH:mm:ss");
                    break;

                case enDateType.Date:
                    strRst = dt.ToString("yyyy-MM-dd");
                    break;

                case enDateType.Time:
                    strRst = dt.ToString("HH:mm:ss");
                    break;

                case enDateType.Year:
                    strRst = dt.ToString("yyyy");
                    break;

                case enDateType.Month:
                    strRst = dt.ToString("MM");
                    break;

                case enDateType.Day:
                    strRst = dt.ToString("dd");
                    break;

                case enDateType.Hour:
                    strRst = dt.ToString("HH");
                    break;

                case enDateType.Minute:
                    strRst = dt.ToString("mm");
                    break;

                case enDateType.Second:
                    strRst = dt.ToString("ss");
                    break;

                case enDateType.DBType:
                    strRst = dt.ToString("yyyyMMdd");
                    break;

                case enDateType.HourMinutes:
                    strRst = dt.ToString("HHmm");
                    break;

                default:
                    strRst = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
            }

            return strRst;
        }

        /// <summary>
        /// string를 날짜로 변환 : 기준이 없는 형식 부분은 현재 시간 기준으로 변환
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="enDType"></param>
        /// <returns></returns>
        public static DateTime String2Date(string strDate, enDateType enDType)
        {
            DateTime drRst = DateTime.Now;

            int Y = drRst.Year;
            int M = drRst.Month;
            int D = drRst.Day;

            int H = drRst.Hour;
            int MI = drRst.Minute;
            int S = drRst.Second;

            strDate = strDate.Replace("-", "");

            if (strDate != null && strDate != "" && strDate.Length != 0) //strDate = drRst.ToString();
            {
                switch (enDType)
                {
                    case enDateType.DateTime:
                        Y = int.Parse(strDate.Substring(0, 4));
                        M = int.Parse(strDate.Substring(5, 2));
                        D = int.Parse(strDate.Substring(8, 2));

                        H = int.Parse(strDate.Substring(11, 2));
                        MI = int.Parse(strDate.Substring(14, 2));
                        S = int.Parse(strDate.Substring(17, 2));
                        break;

                    case enDateType.DateTimeShort:
                        M = int.Parse(strDate.Substring(0, 2));
                        D = int.Parse(strDate.Substring(3, 2));

                        H = int.Parse(strDate.Substring(6, 2));
                        MI = int.Parse(strDate.Substring(9, 2));
                        S = int.Parse(strDate.Substring(12, 2));

                        break;

                    case enDateType.Date:
                        Y = int.Parse(strDate.Substring(0, 4));
                        M = int.Parse(strDate.Substring(4, 2));
                        D = int.Parse(strDate.Substring(6, 2));
                        break;

                    case enDateType.Time:
                        H = int.Parse(strDate.Substring(0, 2));
                        MI = int.Parse(strDate.Substring(3, 2));
                        S = int.Parse(strDate.Substring(6, 2));
                        break;

                    case enDateType.Year:
                        Y = int.Parse(strDate);
                        break;

                    case enDateType.Month:
                        M = int.Parse(strDate);
                        break;

                    case enDateType.Day:
                        D = int.Parse(strDate);
                        break;

                    case enDateType.Hour:
                        H = int.Parse(strDate);
                        break;

                    case enDateType.Minute:
                        MI = int.Parse(strDate);
                        break;

                    case enDateType.Second:
                        S = int.Parse(strDate);
                        break;

                    case enDateType.DBType:
                        Y = int.Parse(strDate.Substring(0, 4));
                        M = int.Parse(strDate.Substring(4, 2));
                        D = int.Parse(strDate.Substring(6, 2));
                        break;

                    case enDateType.HourMinutes:
                        Y = DateTime.Now.Year;
                        M = int.Parse(strDate.Substring(0, 2));
                        D = int.Parse(strDate.Substring(2, 2));
                        break;

                    default:
                        Y = int.Parse(strDate.Substring(0, 4));
                        M = int.Parse(strDate.Substring(5, 2));
                        D = int.Parse(strDate.Substring(8, 2));

                        H = int.Parse(strDate.Substring(11, 2));
                        MI = int.Parse(strDate.Substring(14, 2));
                        S = int.Parse(strDate.Substring(17, 2));
                        break;
                }
            }

            return new DateTime(Y, M, D, H, MI, S);
        }

        /// <summary>
        /// 요일명 타입
        /// </summary>
        public enum enDayNameType { Kor, Kor_Long, Eng, Eng_Long, Han, Han_Long };

        /// <summary>
        /// 요일명을 구한다..
        /// </summary>
        /// <param name="DayNameType"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DayName_Get(enDayNameType DayNameType, DateTime dt)
        {
            string strFormat = string.Empty;
            string strCulture = Culture_Get(DayNameType);

            switch (DayNameType)
            {

                case enDayNameType.Eng_Long:
                case enDayNameType.Kor_Long:
                case enDayNameType.Han_Long:
                    strFormat = "dddddd";
                    break;

                default:
                    strFormat = "ddd";
                    break;

            }


            return dt.ToString(strFormat, new System.Globalization.CultureInfo(strCulture));


        }

        /// <summary>
        /// 달명을 구한다.  
        /// </summary>
        /// <param name="DayNameType"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string MonthName_Get(enDayNameType DayNameType, DateTime dt)
        {
            string strFormat = string.Empty;
            string strCulture = Culture_Get(DayNameType);

            switch (DayNameType)
            {

                case enDayNameType.Eng_Long:
                case enDayNameType.Kor_Long:
                case enDayNameType.Han_Long:
                    strFormat = "MMMMMM";
                    break;

                default:
                    strFormat = "MMM";
                    break;

            }


            return dt.ToString(strFormat, new System.Globalization.CultureInfo(strCulture));


        }

        private static string Culture_Get(enDayNameType DayNameType)
        {
            string strCulture = string.Empty;

            switch (DayNameType)
            {
                case enDayNameType.Eng:
                case enDayNameType.Eng_Long:
                    strCulture = "en-US";
                    break;


                case enDayNameType.Han_Long:
                case enDayNameType.Han:
                    strCulture = "ja-JP";
                    break;

                default:
                    strCulture = "ko-KR";
                    break;

            }


            return strCulture;
        }

        /// <summary>
        /// object값을 string으로 변환하여 준다.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string obj2String(object obj)
        {
            if (obj == null)
                return string.Empty;
            else
                return obj.ToString().Trim();
        }


        /// <summary>
        /// byte 배열을 string으로 변환하여 반환한다.
        /// </summary>
        /// <param name="byt"></param>
        /// <returns></returns>
        public static string Bytes2String(byte[] byt)
        {
            string str = string.Empty;

            foreach (byte bt in byt)
            {
                str += string.Format("{0:x2} ", bt);
            }

            str = str.Trim();

            return str;
        }

        /// <summary>
        /// object값을 int으로 변환하여 준다.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int obj2int(object obj)
        {
            if (obj == null || obj.ToString() == string.Empty)
                return 0;
            else
                return Convert.ToInt32(obj.ToString());
        }


        /// <summary>
        /// Byte 배열을 Hex 스트링으로 변환(Hex 스트링 사이에 구분문자 입력 처리 추가) 
        /// </summary>
        /// <param name="bytePacket"></param>
        /// <param name="cDelimiter"></param>
        /// <returns></returns>
        public static string ByteArray2HexString(Byte[] bytePacket, string strDelimiter)
        {
            string sReturn = "";
            try
            {
                int nCount = bytePacket.Length;

                for (int i = 0; i < nCount; i++)
                {
                    if (i == 0)
                        sReturn += String.Format("{0:X2}", bytePacket[i]);
                    else
                        sReturn += String.Format("{0}{1:X2}", strDelimiter, bytePacket[i]);
                }
            }
            catch
            {
                sReturn = "";
            }
            return sReturn;
        }


        public static double Hex2Double(string strHex)
        {
            Int64 int64Val = Convert.ToInt64(strHex, 16);
            double doubleValue = BitConverter.Int64BitsToDouble(int64Val);

            return doubleValue;
        }

        public static float Hex2Float(string strHex)
        {
            uint num = uint.Parse(strHex, System.Globalization.NumberStyles.AllowHexSpecifier);

            byte[] byteValue = BitConverter.GetBytes(num);
            float floatValue = BitConverter.ToSingle(byteValue, 0);

            return floatValue;
        }

        public static int Hex2Integer(string strHex)
        {
            int intValue = int.Parse(strHex, System.Globalization.NumberStyles.HexNumber);

            return intValue;
        }

        /// <summary>
        ///  object값을 long으로 변환하여 준다.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long obj2Long(object obj)
        {
            if (obj == null || obj.ToString() == string.Empty)
                return 0;
            else
                return Convert.ToInt64(obj);
        }


        /// <summary>
        ///  object값을 float으로 변환하여 준다.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float obj2Float(object obj)
        {
            if (obj == null || obj.ToString() == string.Empty)
                return 0;
            else
                return float.Parse(obj.ToString());
        }


        /// <summary>
        /// object값을 boolean으로 변환하여 준다.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool obj2Bool(object obj)
        {
            if (obj == null || obj.ToString() == string.Empty)
                return false;
            else
                return bool.Parse(obj.ToString());
        }

        /// <summary>
        /// hex string으로 Color를 만든다.
        /// </summary>
        /// <param name="strColorHex"></param>
        /// <returns></returns>
        public static Color String2Color(string strColorHex)
        {
            if (strColorHex.Length != 6) return Color.White;

            try
            {
                int intR = Convert.ToInt32(strColorHex.Substring(0, 2), 16);
                int intG = Convert.ToInt32(strColorHex.Substring(2, 2), 16);
                int intB = Convert.ToInt32(strColorHex.Substring(4, 2), 16);


                return Color.FromArgb(intR, intG, intB);
            }
            catch
            {
                return Color.White;
            }
        }

        /// <summary>
        /// hex string으로 Color를 만든다.
        /// </summary>
        /// <param name="strColorHex"></param>
        /// <param name="DefaultColor">에러 발생시 리턴 컬러</param>
        /// <returns></returns>
        public static Color String2Color(string strColorHex, Color DefaultColor)
        {
            if (strColorHex.Length != 6) return Color.White;

            try
            {
                int intR = Convert.ToInt32(strColorHex.Substring(0, 2), 16);
                int intG = Convert.ToInt32(strColorHex.Substring(2, 2), 16);
                int intB = Convert.ToInt32(strColorHex.Substring(4, 2), 16);


                return Color.FromArgb(intR, intG, intB);
            }
            catch
            {
                return DefaultColor;
            }
        }

        /// <summary>
        /// Json 데이터 Data set 으로 변경
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static DataSet Json2DataSet(string strJson)
        {
            DataSet ds = null;
            try
            {
                ds = JsonConvert.DeserializeObject<DataSet>(strJson);

                return ds;
            }
            catch
            {
                return ds;
            }
        }

        /// <summary>
        /// 문자열에 포함되어 있는 검색 문자 수를 리턴한다.
        /// </summary>
        /// <param name="strData">문자열</param>
        /// <param name="strFind">검색할 문자</param>
        /// <returns></returns>
        public static int inStr(string strData, string strFind)
        {
            int intCnt = 0;
            int intResult = 0;

            while (intResult >= 0 && (intResult + 1) < strData.Length)
            {
                intResult = strData.IndexOf(strFind, intResult + 1);
                if (intResult > 0) intCnt++;
            }

            return intCnt;
        }

        /// <summary>
        /// 문자열에 문자열을 더한다.
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="strAddData"></param>
        /// <param name="strSpreator"></param>
        /// <returns></returns>
        public static string StringAdd(string strData, string strAddData, string strSpreator)
        {
            if (strData == string.Empty)
                strData = strAddData;
            else
                strData += strSpreator + strAddData;

            return strData;
        }


        /// <summary>
        /// DateTime의 날짜 부분과 timespan의 시간 부분을 합친다.
        /// c1 input control의 datepicker와 timepicker 합칠 사용하면 유용.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTime DateTimeMergeTimeSpan(DateTime dt, TimeSpan ts)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day,
                                ts.Hours, ts.Minutes, ts.Seconds);

        }




        /// <summary>
        /// 리소스에서 Stream을 추출한다.
        /// </summary>
        /// <param name="thisExe">'System.Reflection.Assembly.GetExecutingAssembly()' 내용 그대로 넘길것..</param>
        /// <param name="strResourceName">네임스페이스를 포함한 이름 ex)네임스페이스.이름</param>
        /// <returns></returns>
        public static System.IO.Stream GetResource2Stream(System.Reflection.Assembly thisExe, string strResourceName)
        {
            //thisExe = System.Reflection.Assembly.GetExecutingAssembly();

            //string[] resources = thisExe.GetManifestResourceNames();

            return thisExe.GetManifestResourceStream(strResourceName);
        }

        /// <summary>
        /// 리소스에서 스트링 추출 한다
        /// </summary>
        /// <param name="thisExe">'System.Reflection.Assembly.GetExecutingAssembly()' 내용 그대로 넘길것..</param>
        /// <param name="strResourceName">네임스페이스를 포함한 이름 ex)네임스페이스.이름</param>
        /// <returns></returns>
        public static string GetResource2string(System.Reflection.Assembly thisExe, string strResourceName)
        {
            System.IO.Stream st = GetResource2Stream(thisExe, strResourceName);

            int intLength = Convert.ToInt32(st.Length);

            Byte[] byt = new byte[intLength];

            st.Read(byt, 0, intLength);

            return Encoding.Default.GetString(byt);

        }



        /// <summary>
        /// 2개에 바이트 배열을 합쳐준다.
        /// </summary>
        /// <param name="byt01"></param>
        /// <param name="byt02"></param>
        /// <returns></returns>
        public static byte[] BytesMerge(byte[] byt01, byte[] byt02)
        {
            //총 바이트 길이 확인
            int intLen = byt01.Length + byt02.Length;

            byte[] byt = new byte[intLen];

            Array.Copy(byt01, 0, byt, 0, byt01.Length);

            Array.Copy(byt02, 0, byt, byt01.Length, byt02.Length);

            return byt;

        }

        /// <summary>
        /// 신호에 Etx 추가 - 길이, 체크섬, etx
        /// </summary>
        /// <param name="byt"></param>
        /// <returns></returns>
        public static byte[] BytesSetEtx(byte[] byt)
        {
            int intLen = byt.Length;
            string strLen = string.Format("{0:x4}", intLen);
            byte[] bt = new byte[6];

            bt[0] = Convert.ToByte(strLen.Substring(0, 2), 16);
            bt[1] = Convert.ToByte(strLen.Substring(2, 2), 16);
            bt[2] = GetCheckSum(byt, 0, intLen);
            bt[3] = 0x03; //etx
            bt[4] = 0x0D; //CR
            bt[5] = 0x0A; //LF

            return BytesMerge(byt, bt);
        }

        /// <summary>
        /// 체크섬을 생성한다.
        /// </summary>
        /// <param name="byt"></param>
        /// <param name="intFromIndex">시작 인덱스</param>
        /// <param name="intLength">길이</param>
        /// <returns></returns>
        public static byte GetCheckSum(byte[] byt, int intFromIndex, int intLength)
        {

            long lngChk = 0;

            for (int i = intFromIndex; i < intFromIndex + intLength; i++)
            {
                lngChk += byt[i];
            }

            string strChk = string.Format("{0:x2}", lngChk);
            strChk = strChk.Substring(strChk.Length - 2, 2);
            return Convert.ToByte(strChk, 16);

        }

        /// <summary>
        /// byte배열을 string으로 반환 한다. 시작index =0, 길이 =0 으로 설정시 전체를 변환
        /// </summary>
        /// <param name="byt"></param>
        /// <param name="intFromIndex"></param>
        /// <param name="intLength"></param>
        /// <returns></returns>
        public static String Bytes2String(byte[] byt, int intFromIndex, int intLength)
        {
            if (intFromIndex == 0 && intLength == 0)
                intLength = byt.Length;

            return Encoding.Default.GetString(byt, intFromIndex, intLength);
        }

        /// <summary>
        /// byte배열을 int로 반환 한다. 시작index =0, 길이 =0 으로 설정시 전체를 변환
        /// </summary>
        /// <param name="byt"></param>
        /// <param name="intFromIndex"></param>
        /// <param name="intLength"></param>
        /// <returns></returns>
        public static int Bytes2Int(byte[] byt, int intFromIndex, int intLength)
        {
            if (intFromIndex == 0 && intLength == 0)
                intLength = byt.Length;

            string str = string.Empty;
            for (int i = intFromIndex; i < intFromIndex + intLength; i++)
            {
                str += string.Format("{0:x2}", byt[i]);
            }

            return Convert.ToInt32(str, 16);


        }


        /// <summary>
        /// string 배열을 byt배열로 만들어 준다.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isWithLength">byt앞에 길이 바이트 추가 여부.</param>
        /// <returns></returns>
        public static byte[] String2Byte(string[] str, bool isWithLength)
        {
            byte[] byt = new byte[0];

            foreach (string s in str)
            {
                if (s == null) break;

                byte[] bytData = Encoding.Default.GetBytes(s);

                if (isWithLength)
                {
                    byte[] bytLen = new byte[2];

                    int i = bytData.Length;
                    string sLen = string.Format("{0:x4}", i);

                    bytLen[0] = Convert.ToByte(sLen.Substring(0, 2), 16);
                    bytLen[1] = Convert.ToByte(sLen.Substring(2, 2), 16);

                    bytData = BytesMerge(bytLen, bytData);

                }

                byt = BytesMerge(byt, bytData);
            }

            return byt;
        }


        /// <summary>
        /// wav 파일을 재생한다.
        /// </summary>
        /// <param name="strFilePath"></param>
        public static void PlayWaveSound(string strFilePath)
        {
            try
            {
                System.Media.SoundPlayer p = new System.Media.SoundPlayer(strFilePath);

                p.PlaySync();
            }
            catch
            {
            }
        }






        /// <summary>
        /// 문자열이 숫자 인지 검사 한다
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool isNumeric(string str)
        {

            if (str == string.Empty) return false;

            foreach (char c in str.ToCharArray())
            {
                if (!Char.IsDigit(c))
                    return false;
            }

            return true;

        }




        /// <summary>
        /// 데이타 테이블 특정 자료 유무
        /// </summary>
        /// <param name="dt">데이터 테이블</param>
        /// <param name="expression">조건 입력</param>
        /// <param name="sortorder">정렬 방식</param>
        /// <returns></returns>
        public static bool DataTable_FindCount(DataTable dt, string expression, string sortorder)
        {
            bool isReturn = false;
            if (dt == null)
            {
                return isReturn;
            }

            if (dt.Select(expression, sortorder).Length > 0) isReturn = true;

            return isReturn;

        }

        /// <summary>
        /// 데이타 테이블 특정 자료 찾기
        /// </summary>
        /// <param name="dt">데이터 테이블</param>
        /// <param name="expression">조건 입력</param>
        /// <param name="sortorder">정렬 방식</param>
        /// <returns></returns>
        public static DataTable DataTable_FindValue(DataTable dt, string expression, string sortorder)
        {
            DataTable dtReturn = dt.Select(expression, sortorder).CopyToDataTable();

            return dtReturn;
        }





        /// <summary>
        /// 데이타 테이블 2개를 비교 하여 데이터가 다른지 여부 확인한다
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static bool DataTables_isEquals(DataTable dt1, DataTable dt2)
        {
            //null 확인
            if (dt1 == null || dt2 == null) return false;

            //row, col 수 같은지 비교
            if (dt1.Rows.Count != dt2.Rows.Count || dt1.Columns.Count != dt2.Columns.Count)
                return false;

            int intRows = dt1.Rows.Count;
            int intCols = dt1.Columns.Count;

            //각 field에 데이터 확인...
            for (int intRow = 0; intRow < intRows; intRow++)
            {
                for (int intCol = 0; intCol < intCols; intCol++)
                {
                    if (dt1.Rows[intRow][intCol].ToString() != dt2.Rows[intRow][intCol].ToString()) return false;
                }
            }


            return true;

        }

        /// <summary>
        /// enum을 string 배열로 변환하여 반화 한다.
        /// </summary>
        /// <param name="em">열거형을 new로 생성 하여 입력</param>
        /// <returns>열거형 값을 string 배열</returns>
        public static string[] EnumItems2Strings(Enum em)
        {
            Type type = em.GetType();
            Array arr = Enum.GetValues(type);
            string[] items = new string[arr.Length];

            int i = 0;
            foreach (object e in Enum.GetValues(type))   //Parity.GetType()))
            {
                items[i] = e.ToString();
                i++;
            }

            return items;
        }



        /// <summary>
        /// string 값을 enum item으로 변경한다.
        /// </summary>
        /// <param name="em">열거형을 new로 생성 하여 입력</param>
        /// <param name="strItem">item의 string 값</param>
        /// <returns>object 열거형으로 형변환 하여 사용</returns>
        public static object enumItem2Object(Enum em, string strItem)
        {
            Type type = em.GetType();

            object o = null;

            foreach (object e in Enum.GetValues(type))
            {
                if (strItem == e.ToString())
                {
                    o = e;
                    break;
                }
            }

            return o;

        }




        public static object String2Enum(Enum em, string strValue)
        {

            strValue = strValue.ToUpper();

            Type type = em.GetType();


            foreach (object e in Enum.GetValues(type))   //Parity.GetType()))
            {
                if (strValue == e.ToString().ToUpper())
                {
                    return e;
                }
            }

            return null;

        }


        /// <summary>
        /// 숫자를 올림 처리 한다.
        /// </summary>
        /// <param name="dValue"></param>
        /// <param name="intDigit"></param>
        /// <returns></returns>
        public static double dblToRoundUp(double dValue, int intDigit)
        {
            double dCoef = System.Math.Pow(10, intDigit);

            return dValue > 0 ? System.Math.Ceiling(dValue * dCoef) / dCoef : System.Math.Floor(dValue * dCoef) / dCoef;

        }


        /// <summary>
        /// alpha값이 있는 color를 string값으로 변환 : 형태 - a,r,g,b
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static String AColor2String(Color col)
        {
            return string.Format("{0},{1},{2},{3}", col.A, col.R, col.G, col.B);
        }


        /// <summary>
        /// string 값을 Alpha Color값으로 변환
        /// </summary>
        /// <param name="str">형태 - a,r,g,b</param>
        /// <returns></returns>
        public static Color String2AColor(string str)
        {
            string[] s = str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            return Color.FromArgb(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
        }

        /// <summary>
        /// Font를 string 값으로 변환 : 형태 - fontname, size, style
        /// </summary>
        /// <param name="fnt"></param>
        /// <returns></returns>
        public static string Font2String(Font fnt)
        {
            return string.Format("{0},{1},{2}", fnt.Name, fnt.Size, fnt.Style);
        }


        /// <summary>
        /// string값을 Font로 변환
        /// </summary>
        /// <param name="str">형태 - fontname, size, style</param>
        /// <returns></returns>
        public static Font String2Font(string str)
        {
            string[] s = str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            return new Font(s[0], float.Parse(s[1]), (FontStyle)String2Enum(new FontStyle(), s[2]));
        }

        /// <summary>
        /// string.empty값을 dbnull로 변환 하여 준다.
        /// </summary>
        /// <param name="param"></param>
        /// <param name="str"></param>
        public static object StringEmpty2DbNull(string str)
        {
            if (str == string.Empty)
            {
                return DBNull.Value;
            }
            else
            {
                return str;
            }
        }

        public static object StringEmpty2Value(string strOriginal, string strChangeValue)
        {
            if (strOriginal == string.Empty)
            {
                return strChangeValue;
            }
            else
            {
                return strOriginal;
            }
        }

        public static byte[] imageToByteArray(System.Drawing.Image image, string strImageFormat)
        {
            System.Drawing.Imaging.ImageFormat iFormat = null;

            switch (strImageFormat.ToUpper())
            {
                case "BMP":
                    iFormat = System.Drawing.Imaging.ImageFormat.Bmp;
                    break;
                case "EMF":
                    iFormat = System.Drawing.Imaging.ImageFormat.Emf;
                    break;
                case "EXIF":
                    iFormat = System.Drawing.Imaging.ImageFormat.Exif;
                    break;
                case "GIF":
                    iFormat = System.Drawing.Imaging.ImageFormat.Gif;
                    break;
                case "ICON":
                    iFormat = System.Drawing.Imaging.ImageFormat.Icon;
                    break;
                case "JPG":
                case "JPEG":
                    iFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                    break;
                case "MEMORYBMP":
                    iFormat = System.Drawing.Imaging.ImageFormat.MemoryBmp;
                    break;
                case "PNG":
                    iFormat = System.Drawing.Imaging.ImageFormat.Png;
                    break;
                case "TIFF":
                    iFormat = System.Drawing.Imaging.ImageFormat.Tiff;
                    break;
                case "WMF":
                    iFormat = System.Drawing.Imaging.ImageFormat.Wmf;
                    break;
                default:
                    iFormat = System.Drawing.Imaging.ImageFormat.Png;
                    break;

            }

            MemoryStream memstr = new MemoryStream();
            image.Save(memstr, iFormat);
            return memstr.ToArray();
        }

        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static byte[] GetFileData(string filename)
        {
            using (var sr = new StreamReader(filename))
            {
                return ASCIIEncoding.ASCII.GetBytes(sr.ReadToEnd());
            }
        }

        public static Image byteArrayToImage(byte[] bytesArr)
        {
            MemoryStream memstr = new MemoryStream(bytesArr);
            Image img = null;
            try
            {

                if (memstr.Length > 2)
                {
                    img = Image.FromStream(memstr);
                }


            }
            catch (Exception ex)
            {

            }
            return img;
        }
    }
}
