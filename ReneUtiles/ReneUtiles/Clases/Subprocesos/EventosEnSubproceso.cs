using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.Subprocesos
{
    public class EventosEnSubproceso
    {
        public Action alTerminar;
        public Action<Exception> siDaError;


        public EventosEnSubproceso(
            Action alTerminar,
            Action<Exception> siDaError
        )
        {
            this.alTerminar = alTerminar;
            this.siDaError = siDaError;
        }
    }
}
