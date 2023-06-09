using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace TADA.Utilities
{
    public class HashPassword
    {
        public static string Hash(string text)
        {
            try
            {
                MD5 md5 = MD5.Create();
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder hashSb = new StringBuilder();
                foreach (byte b in hash)
                {
                    hashSb.Append(b.ToString("X2"));
                }
                return hashSb.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
