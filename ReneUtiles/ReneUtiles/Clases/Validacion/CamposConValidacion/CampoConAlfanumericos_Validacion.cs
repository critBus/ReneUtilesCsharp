using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.Validacion.CamposConValidacion
{
    class CampoConAlfanumericos_Validacion:CampoConValidacion<string>
    {
        public CampoConAlfanumericos_Validacion(string valorPorDefecto=null):base(valorPorDefecto) {
            addTipoDeValidacion(TipoDeValidaciones.TipoDeValidacion.STR_CON_ALFANUMERICOS);
        }
    }
}
