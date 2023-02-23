using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;

namespace ReneUtiles.Clases.Subprocesos
{
    public class EjecutorDeSubprosesos:ConsolaBasica
    {
        public Action accionSubproseso;
        public EventosEnSubproceso eventos;

        //private Action<Action> ejecutarAccion_EnVisual;

        public EjecutorDeSubprosesos(
        //EventosEnSubproceso eventos,
        //Action accionSubproseso

        )
        {
            //this.accionSubproseso = accionSubproseso;
            //this.eventos = eventos;
        }
        public virtual void ejecutar(EventosEnSubproceso eventos, Action accionSubproseso)
        {
           // cwl("Se ejecuta ---------------------------------");

         //   accionSubproseso();

            UtilesSubprocesos.subp(
                () =>
                {
                    try
                    {
                        accionSubproseso();
                        //ejecutarAccion_EnVisual(eventos.alTerminar);
                        eventos.alTerminar();
                    }
                    catch (Exception ex)
                    {
                        //ejecutarAccion_EnVisual(eventos.alTerminar);
                        //throw ex;
                        eventos.siDaError(ex);
                    }


                }

                        );
        }
    }

}
