using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.ExprecionesRegulares.IdentificacionesNumericas
{
    public class IdentificacionEnStr
    {
        protected int indiceDeRepresentacionStr;
        protected string representacionStr;

        public int IndiceDeRepresentacionStr
        {
            get
            {
                return indiceDeRepresentacionStr;
            }

            set
            {
                indiceDeRepresentacionStr = value;
            }
        }

        public string RepresentacionStr
        {
            get
            {
                return representacionStr;
            }

            set
            {
                representacionStr = value;
            }
        }

        public IdentificacionEnStr(int indiceDeRepresentacionStr, string representacionStr)
        {
            this.indiceDeRepresentacionStr = indiceDeRepresentacionStr;
            this.representacionStr = representacionStr;
        }
    }
}
