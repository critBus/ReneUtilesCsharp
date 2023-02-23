using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.ExprecionesRegulares.IdentificacionesNumericas
{
    public class IdentificacionNumeroRomanoEnStr:IdentificacionNumericaEnStr
    {
        public IdentificacionNumeroRomanoEnStr(int indiceDeRepresentacionStr, string representacionStr) : base(indiceDeRepresentacionStr, representacionStr)
        {
        }

        protected override int parse(string representacionStr)
        {
            //this.representacionStr = representacionStr;
            return Utiles.getNumeroDeNumeroRomano(representacionStr.Trim());
        }
    }
}
