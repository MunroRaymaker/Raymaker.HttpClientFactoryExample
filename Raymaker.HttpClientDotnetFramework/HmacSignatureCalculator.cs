using System.Security.Cryptography;
using System.Text;

namespace Raymaker.HttpClientDotnetFramework
{
    public static class HmacSignatureCalculator
    {
        public static string Signature(string secret, string message)
        {
            var hmac = ToHMAC(message, secret);
            return hmac;
        }

        // ReSharper disable once InconsistentNaming
        private static string ToHMAC(string message, string key)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(key);
            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return ByteToString(hashmessage);
        }

        private static string ByteToString(byte[] buf)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buf.Length; i++)
            {
                sb.Append(buf[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
