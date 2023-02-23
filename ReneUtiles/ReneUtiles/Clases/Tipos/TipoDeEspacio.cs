using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.Tipos
{
    public class TipoDeEspacio
    {
        public static readonly TipoDeEspacio
            BIT = new TipoDeEspacio("BIT", "BI", 1.25),
            BYTES = new TipoDeEspacio("BYTES", "BY", 1),
            KILOBYTE = new TipoDeEspacio("KILOBYTE","KB", 1024),
            MEGABYTE = new TipoDeEspacio("MEGABYTE","MG", (double)1024 * 1024),
            GIGABYTE = new TipoDeEspacio("GIGABYTE","GB", (double)1024 * 1024*1024),
            TERABYTE = new TipoDeEspacio("TERABYTE","TB", (double)1024 * 1024 * 1024 * 1024);
        public static readonly TipoDeEspacio[] VALUES = { BIT, BYTES, KILOBYTE, MEGABYTE, GIGABYTE, TERABYTE };
        public readonly string medida;
        public readonly double cantidadDeBYTES;
        public readonly string medida_mini_Str2;
        public TipoDeEspacio(string medida, string medida_mini_Str2, double cantidadDeBYTES)
        {
            this.medida = medida;
            this.cantidadDeBYTES = cantidadDeBYTES;
            this.medida_mini_Str2 = medida_mini_Str2;
        }

        public override string ToString()
        {
            return this.medida;
        }
        

        public double getCantidadDeBYTES() {
            return this.cantidadDeBYTES;
        }

        public static TipoDeEspacio get(string medida)
        {
            medida = medida.ToLower();
            foreach (TipoDeEspacio t in VALUES)
            {
                if (medida == t.medida)
                {
                    return t;
                }
            }
            return null;
        }

        public static bool pertenece(string extencionActiva)
        {
            return get(extencionActiva) != null;
        }
    }
}
