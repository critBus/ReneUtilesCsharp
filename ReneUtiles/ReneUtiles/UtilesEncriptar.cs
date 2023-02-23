using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ReneUtiles
{
    public abstract class UtilesEncriptar
    {
        public static string getSHA256(string texto) {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(texto));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}",stream[i]);
            }
            return sb.ToString();
        }
    }
}
