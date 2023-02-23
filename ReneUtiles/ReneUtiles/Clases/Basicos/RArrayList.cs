using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReneUtiles.Clases.Basicos
{
    public class RArrayList<E>:IEnumerable<E>,IEnumerator<E>
    {
        private E []elementos;
        private int cantidadDeElementos;




        public RArrayList() {
            clear();
        }
        public void add(E e) {
            int leng = elementos.Length;
            if (this.cantidadDeElementos== leng) {
                E[] elementosAmpliados=new E[(leng == 0?1:leng)*2];
                for (int i = 0; i < leng; i++)
                {
                    elementosAmpliados[i] = elementos[i];
                }
                elementos = elementosAmpliados;
            }
            elementos[this.cantidadDeElementos] = e;
            this.cantidadDeElementos++;
        }
        public void Add(E e)
        {
            add(e);
        }
        public void addRange(IEnumerable<E> elementosAagregar)
        {
            foreach (E e in elementosAagregar)
            {
                add(e);
            }
        }
        public void AddRange(IEnumerable<E> elementosAagregar)
        {
            addRange(elementosAagregar);
        }
        public E get(int i) {
            return this.elementos[i];
        }
        public E Get(int i)
        {
            return get(i);
        }
        public int size() {
            return this.cantidadDeElementos;
        }

        public void clear() {
            elementos = new E[0];
            cantidadDeElementos = 0;
            
        }
        public void Clear()
        {
            clear();
        }

        //------------------------------------------------------------
        //metodos para compaitibilidad

        public IEnumerator<E> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
        private int indiceActual = -1;

        public void Dispose()
        {
            this.indiceActual = -1;
            this.cantidadDeElementos = 0;
            this.elementos = null;
        }

        public bool MoveNext()
        {
            if (this.indiceActual<this.cantidadDeElementos-1) {
                this.indiceActual++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            clear();
            this.indiceActual = -1;
        }


        public E Current
        {
            get
            {
                return get(indiceActual);
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return get(indiceActual);
            }
        }
    }
}
