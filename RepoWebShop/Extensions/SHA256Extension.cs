using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class SHA256Extension
    {
        public static String FromString(this SHA256 sha, string value)
        {
            StringBuilder Sb = new StringBuilder();
            
            Encoding enc = Encoding.UTF8;
            Byte[] result = sha.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));

            return Sb.ToString();
        }
    }
}
