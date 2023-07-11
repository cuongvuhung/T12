using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace T12
{
    public class Utils
    {
        public Utils()
        {
        }

        public static string Hash(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = null;
            // SHA
            SHA256 sha = SHA256.Create();
            hashBytes = sha.ComputeHash(bytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
