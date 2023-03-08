using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;
using System.Collections.ObjectModel;

namespace ReneUtiles.Clases.ExprecionesRegulares.IdentificacionesNumericas
{
    public class ConjuntoDeIdentificacionesNumericas
    {
        private SortedSet<IdentificacionNumericaEnStr> ordenadosPorIndice;
        private SortedSet<IdentificacionNumericaEnStr> ordenadosPorNumero;

        public ConjuntoDeIdentificacionesNumericas() {
            ordenadosPorIndice = ComparableHash<IdentificacionNumericaEnStr>.getNewSortedSet_KeyComparable(v=>v.IndiceDeRepresentacionStr);
            ordenadosPorNumero = ComparableHash<IdentificacionNumericaEnStr>.getNewSortedSet_KeyComparable(v => v.Numero);
        }

        public void add(IdentificacionNumericaEnStr numero) {

        }

        public ReadOnlyCollection<IdentificacionNumericaEnStr> OrdenadosPorIndice {
            get { return new ReadOnlyCollection<IdentificacionNumericaEnStr>(this.ordenadosPorIndice.ToList()); }
        }
        public ReadOnlyCollection<IdentificacionNumericaEnStr> OrdenadosPorNumero
        {
            get { return new ReadOnlyCollection<IdentificacionNumericaEnStr>(this.ordenadosPorNumero.ToList()); }
        }
        public void clear() {
            this.ordenadosPorIndice.Clear();
            this.ordenadosPorNumero.Clear();
        }
       public int size() {
            return this.ordenadosPorIndice.Count();
        }

        public bool isEmpty() {
            return size() > 0;
        }

    }
}
