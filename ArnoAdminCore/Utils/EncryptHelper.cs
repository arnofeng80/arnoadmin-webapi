using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ArnoAdminCore.Utils
{
    public class EncryptHelper
    {
        private static readonly String Salt = "ASDF1234&^%$";
        public static string MD5Encoding(string rawPass)
        {
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(rawPass);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string HMACMD5Encoding(string md5Pass)
        {
            return HMACMD5Encoding(md5Pass, Salt);
        }

        public static string HMACMD5Encoding(string md5Pass, string saltStr)
        {
            var hmacMD5 = new HMACMD5(System.Text.Encoding.UTF8.GetBytes(saltStr));
            var saltedHash = hmacMD5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(md5Pass));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in saltedHash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string PasswordEncoding(string rawPass, string saltStr)
        {
            return HMACMD5Encoding(MD5Encoding(rawPass), saltStr);
        }

        public static string PasswordEncoding(string rawPass)
        {
            return HMACMD5Encoding(MD5Encoding(rawPass), Salt);
        }
    }
}
