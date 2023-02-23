using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace ReneUtiles.Clases.Basicos
{
    public class RArchivo : RFile
    {
        public RArchivo(string carpeta, string nombre) : base(carpeta, nombre) { }
        public RArchivo(string url) : base(url) { }
        public string getExtencion(){
        return Nombre.Contains(".")?Nombre.Substring(Nombre.IndexOf("."),Nombre.Length):"";
        }
    }
}
