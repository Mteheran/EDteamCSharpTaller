using System;
using System.Security.Cryptography;
using System.Text;

namespace CoWorkingApp.Data.Tools
{
    public static class EncryptData
    {
        public static string EncryptText(string text)
        {
            using(var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

                var has = BitConverter.ToString(hashBytes).Replace("-","").ToLower();

                return has;
            }
        }
    }
}