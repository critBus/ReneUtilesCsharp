using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.ExprecionesRegulares
{
    public class DatosDeIgnorarNumero
    {
        private int indiceAContinuacion;
        public bool EsAleaterizacion { get; set; }
        public bool DentroDeFecha;
        public DatosDeIgnorarNumero(int indiceAContinuacion)
        {
            this.indiceAContinuacion = indiceAContinuacion;
            this.EsAleaterizacion = false;
            this.DentroDeFecha = false;
        }
        public int IndiceAContinuacion { get { return this.indiceAContinuacion; } set { this.indiceAContinuacion = value; } }
    }
}
