using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;
namespace ReneUtiles.Clases.Validacion.TipoDeValidaciones
{
    class TipoDeValidacionMaxLength : TipoDeValidacion
    {

        public TipoDeValidacionMaxLength(int lengthMax, Func<int, string> creardorMensaje)
            :base(v => STR_NO_EMPTY.esValido(v) && v.ToString().Trim().Length <= lengthMax
            , ()=>creardorMensaje(lengthMax))
            {
        }
    }
}
