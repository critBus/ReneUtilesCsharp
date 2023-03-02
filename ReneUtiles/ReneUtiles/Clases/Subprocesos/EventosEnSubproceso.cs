using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.Subprocesos
{
    public class EventosEnSubproceso
    {
        public Action antesDeComenzar;
        public Action alTerminar;
        public Action<Exception> siDaError;
        public Action alConcluirSiempre;//se ejecuta aun si ocurrio error:  1-siDaError/alTerminar 2-alConcluirSiempre
        


        public EventosEnSubproceso(
            Action alTerminar,
            Action<Exception> siDaError
        )
        {
            this.alTerminar = alTerminar;
            this.siDaError = siDaError;
        }

        public EventosEnSubproceso(Action antesDeComenzar, Action alTerminar, Action<Exception> siDaError, Action alConcluirSiempre)
        {
            this.antesDeComenzar = antesDeComenzar;
            this.alTerminar = alTerminar;
            this.siDaError = siDaError;
            this.alConcluirSiempre = alConcluirSiempre;
        }
    }
}
