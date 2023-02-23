using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases
{
    public class ConjuntoDeEventos<E> //where E:Delegate
    {
        public List<E> listaDeMetodos = new List<E>();
        //public E evento;
        public void clear() {
            //listaDeMetodos.ForEach(metodo => {
            //    dynamic m = evento;
            //    m -= metodo;
            //});
            listaDeMetodos.Clear();

        }
        public void add(E metodo) {
            listaDeMetodos.Add(metodo);
            //if (evento == null)
            //{
            //    evento = metodo;
            //    return;
            //}
            
            //dynamic m = evento;
            //m += metodo;
        }
        public void setMetodoUnico(E metodo) {
            clear();
            add(metodo);
        }
        public void remove(E metodo) {
            //dynamic m = evento;
            //  m-= metodo;

            listaDeMetodos.Remove(metodo);
        }
        public void ejecutar(params object[] args)
        {
            listaDeMetodos.ForEach(v =>
            {
                //v.
                dynamic metodo = v;
                metodo.DynamicInvoke(args);
                //event Delegate a;

                //metodo(args);
            });
        }


        //public delegate void metodoExp1();

        //public void ejecu_exp1(params object[] args) {
        //    metodoExp1();
        //}

    }
}
