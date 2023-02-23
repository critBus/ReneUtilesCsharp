using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;

namespace ReneUtiles.Clases.Basicos
{
    public class RCarpeta : RFile
    {
        public RCarpeta(string carpeta, string nombre) : base(carpeta, nombre) { }
        public RCarpeta(string url) : base(url) { }
        //public RArchivo[] getArchivos() {
        //    string []files = Directory.GetFiles(getURL());
        //}
    }
}
