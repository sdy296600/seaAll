using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CoFAS.NEW.MES.Core.Function
{
    public class SHA256Encryption
    {
        // SHA 암호화는 복호화가 불가능
        public static string SHA256_EncryptToString(string phrase)
        {
            string pReturn = string.Empty;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(phrase));

                // Convert byte array to a string   
                StringBuilder pStringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    pStringBuilder.Append(bytes[i].ToString("x2"));
                }

                pReturn = pStringBuilder.ToString();
            }

            return pReturn;
        }

        public static string SHA384_EncryptToString(string phrase)
        {
            string pReturn = string.Empty;

            using (SHA384 sha384Hash = SHA384.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha384Hash.ComputeHash(Encoding.UTF8.GetBytes(phrase));

                // Convert byte array to a string   
                StringBuilder pStringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    pStringBuilder.Append(bytes[i].ToString("x2"));
                }

                pReturn = pStringBuilder.ToString();
            }

            return pReturn;
        }

        public static string SHA512_EncryptToString(string phrase)
        {
            string pReturn = string.Empty;

            using (SHA512 sha512Hash = SHA512.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(phrase));

                // Convert byte array to a string   
                StringBuilder pStringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    pStringBuilder.Append(bytes[i].ToString("x2"));
                }

                pReturn = pStringBuilder.ToString();
            }

            return pReturn;
        }
        private string SHA512Hash(string data)
        {
            SHA512 sha = new SHA512Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(data));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        public static string Encrypt(string data)
        {
            var str = data + "TEST";
            var result = new SHA512Managed().ComputeHash(Encoding.UTF8.GetBytes(str));
            return Convert.ToBase64String(result);
        }
    }

    
}
