using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;
namespace ReneUtiles.Clases.BD.Factory
{
    public class SalidaCodeStr:ConsolaBasica
    {
        public string salida;
        public int separacion0;

        public SalidaCodeStr(int separacion0)
        {
            this.separacion0 = separacion0;
            this.salida = "";
        }
        public void s(string a)
        {
            salida+=a;
        }
        public void sp(string a)
        {
            sp(0,a);
        }
        public void sp(int separacion,string a)
        {
            salida += getSeparacionln(separacion,separacion0)+a;
        }
        protected string getSeparacionln(int indice, int separacion0)
        {
            string r = "\n";
            for (int i = 0; i < indice + separacion0; i++)
            {
                r += "\t";
            }
            return r;
        }

    }
}
