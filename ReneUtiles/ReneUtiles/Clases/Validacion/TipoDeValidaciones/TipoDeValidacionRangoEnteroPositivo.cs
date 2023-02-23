using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;
namespace ReneUtiles.Clases.Validacion.TipoDeValidaciones
{
    class TipoDeValidacionRangoEnteroPositivo : TipoDeValidacion
    {

        public TipoDeValidacionRangoEnteroPositivo(int min,int max, Func<int,int, string> creardorMensaje)
            :base(v => SOLO_INT_POSITIVO_STR.esValido(v) && inT(v) >= min && inT(v) <= max
            , ()=>creardorMensaje(min, max))
            {
        }
    }
}
