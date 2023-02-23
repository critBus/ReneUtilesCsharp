using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;

namespace ReneUtiles.Clases.ExprecionesRegulares.IdentificacionesNumericas
{
    public class IdentificacionNumericaEnStr:IdentificacionEnStr
    {
        protected int numero;
        
        

        public IdentificacionNumericaEnStr( int indiceDeRepresentacionStr, string representacionStr)
            :base(indiceDeRepresentacionStr, representacionStr)
        {
            this.numero = parse(representacionStr);
            
        }

        protected virtual int parse(string representacionStr) {
            //this.representacionStr = representacionStr;
            return Utiles.inT(representacionStr.Trim());
        }



        public int Numero
        {
            get
            {
                return this.numero;
            }
        }
        
    }
}
