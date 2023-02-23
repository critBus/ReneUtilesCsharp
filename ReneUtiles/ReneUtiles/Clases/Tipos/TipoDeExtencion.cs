using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using ReneUtiles;

namespace ReneUtiles.Clases.Tipos
{
	public class TipoDeExtencion
	{
		protected string extencion, extencionDesactivada;
		public TipoDeExtencion(string extencion, string extencionDesactivada)
		{
			this.extencion = extencion;
			this.extencionDesactivada = extencionDesactivada;
		}
		public string Extencion {
			get { return this.extencion; }
			set { this.extencion = value; }
		}
		public string ExtencionDesactivada {
			get { return this.extencionDesactivada; }
			set { this.extencionDesactivada = value; }
		}
        public override string ToString()
        {
            return this.extencion;
        }

        protected static bool esDelTipoDeExtencion(TipoDeExtencion[] T, string nombre)
		{
			string extencionActual=Archivos.getExtencion(nombre);
			if (!(extencionActual.Trim().Length==0)) {
				if (Utiles.or(extencionActual,t=>{return t.Extencion;},T)) {
					return true;
				} 
			}
			return false;
		}
	}
}
