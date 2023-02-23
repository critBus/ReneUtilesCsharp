using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles.Clases.Validacion.TipoDeValidaciones;

using ReneUtiles;
using ReneUtiles.Clases;

namespace ReneUtiles.Clases.Validacion.CamposConValidacion
{
    class CampoConValidacion<E>:ConsolaBasica
    {
        public E valorPorDefecto;
        public string mensaje="";
        public List<string> listaDeMensajes = new List<string>();
        public bool esValido=false;
        public E valor;
        public List<TipoDeValidacion> listaTiposDeValidacion=new List<TipoDeValidacion>();
        public Func<bool> condicionDeEvaluacion=()=>true;
        public string nombreDeVariable="";


        public Func<CampoConValidacion<E>,E,string> metodoGetNombre;
        public Func<CampoConValidacion<E>,string, E,string> metodoGetMensajeDeIdentifiacionDeCampoDefault;

        public bool detenerAlPrimerInvalido = true;

        public Func<CampoConValidacion<E>, string> metodoActualizarValor;//(this,nombreVariable)

        public CampoConValidacion(E valorPorDefecto):base()
        {
            this.valorPorDefecto = valorPorDefecto;

        }
        public CampoConValidacion()
        {
            this.metodoGetNombre = (campo, valor) =>{
                List<string> lista = Utiles.separadorDePalabrasEnTextoUnido(this.nombreDeVariable);
                lista = (from p in lista select Utiles.capitalize(p)).ToList();
                string nombre = "";
                foreach (string p in lista)
                {
                    nombre += (nombre.Length > 0 ? " " : "") + p;
                }
                return nombre;
            };

            this.metodoGetMensajeDeIdentifiacionDeCampoDefault = (campo, nombre, valor) => "El campo \"" + this.getNombre() + "\": ";
        }
        public string getNombre()
        {//string campo,E valor
            return this.metodoGetNombre(this,this.valor);
        }
        public string getMensajeDeIdentifiacionDeCampoDefault() {
            return metodoGetMensajeDeIdentifiacionDeCampoDefault(this,this.getNombre(), this.valor);
        }

        public CampoConValidacion<E> setNombre(Func<CampoConValidacion<E>, E, string> metodoGetNombre) {
            this.metodoGetNombre = metodoGetNombre;
            return this;
        }
        public CampoConValidacion<E> setNombre(string nombre)
        {
            return this.setNombre((campo, valor) =>nombre);
        }
        public bool comprovarValidacion()
        {
            this.listaDeMensajes.Clear();
            foreach (TipoDeValidacion t in this.listaTiposDeValidacion)
            {
                if (!t.esValido(this.valor)) {
                    
                    
                    this.listaDeMensajes.Add(t.getMensaje());
                    if (this.esValido)
                    {
                        this.mensaje = this.listaDeMensajes.Last();
                    }
                    this.esValido = false;

                    if (this.detenerAlPrimerInvalido) {
                        return false;
                    }
                    
                }
            }

            return this.esValido;
        }

        public CampoConValidacion<E> addTipoDeValidacion(params TipoDeValidacion[] tiposDeValidaciones) {
            foreach (TipoDeValidacion t in tiposDeValidaciones)
            {
                this.listaTiposDeValidacion.Add(t);
            }
            return this;
        }
        public CampoConValidacion<E> addTipoDeValidacion(Predicate<object> esValido, Func<string> getMensaje) {
            return addTipoDeValidacion(new TipoDeValidacion(esValido, getMensaje));
        }
        public CampoConValidacion<E> addTipoDeValidacion(Predicate<object> esValido, string mensaje)
        {
            return addTipoDeValidacion(esValido,()=> mensaje);
        }

        public CampoConValidacion<E> setMaxCar(int max)
        {
            TipoDeValidacion t = new TipoDeValidacionMaxLength(max, v => "Debe de tener como máximo " + v + " caracteres ");
            this.addTipoDeValidacion(t);
            return this;
        }

        public CampoConValidacion<E> setMinCar(int min)
        {
            TipoDeValidacion t = new TipoDeValidacionMinLength(min, v => "Debe de tener como mínimo " + v + " caracteres ");
            this.addTipoDeValidacion(t);
            return this;
        }



    }
}
