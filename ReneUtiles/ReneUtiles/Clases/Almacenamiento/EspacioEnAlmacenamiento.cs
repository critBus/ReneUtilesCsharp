using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;
using ReneUtiles.Clases.Tipos;

namespace ReneUtiles.Clases.Almacenamiento
{
    public class EspacioEnAlmacenamiento
    {
        public TipoDeEspacio tipoDeEspacio;
        public double cantidadDeEsteTipoDeEspacio;
        public double cantidadDeBytes;
        public string cantidadDeEsteTipoDeEspacio_numeroStr;
        public EspacioEnAlmacenamiento(double cantidadDeBytes) {
            this.cantidadDeBytes = cantidadDeBytes;
            calularTipoDeEspacio();
        }

        private void calularTipoDeEspacio() {
            for (int i = 1; i < TipoDeEspacio.VALUES.Length; i++)
            {
                
                if (this.cantidadDeBytes < TipoDeEspacio.VALUES[i].cantidadDeBYTES)
                {
                    this.tipoDeEspacio = TipoDeEspacio.VALUES[i - 1];
                    darValoresSecundarios();
                    return;
                }
            }
            this.tipoDeEspacio = TipoDeEspacio.VALUES[TipoDeEspacio.VALUES.Length - 1];
            darValoresSecundarios();
        }

        private void darValoresSecundarios()
        {
            this.cantidadDeEsteTipoDeEspacio = this.cantidadDeBytes / this.tipoDeEspacio.cantidadDeBYTES;
            double decimas = this.cantidadDeEsteTipoDeEspacio - ((int)this.cantidadDeEsteTipoDeEspacio);
            this.cantidadDeEsteTipoDeEspacio_numeroStr = decimas>0?String.Format("{0:0.00}", this.cantidadDeEsteTipoDeEspacio): this.cantidadDeEsteTipoDeEspacio+"";
        }
    }
    
}
