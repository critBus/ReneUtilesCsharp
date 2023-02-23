using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.Validacion.TipoDeValidaciones
{
    class TipoDeValidacionRangoPositivo : TipoDeValidacion
    {

        public TipoDeValidacionRangoPositivo(int min, int max, Func<int, int, string> creardorMensaje)
            :base(v => SOLO_FLOAT_POSITIVO_STR.esValido(v) && inT(v) >= min && inT(v) <= max
            , ()=>creardorMensaje(min, max))
            {
        }
    }
}
