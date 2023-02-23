using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases
{
    public class ComparableHash<E> : IComparable<ComparableHash<E>>
    {
        public virtual int CompareTo(ComparableHash<E> other)
        {
            return getKeyComparador().CompareTo(other.getKeyComparador());
        }
        protected virtual string getKeyComparador()
        {
            return getKeyHash();
        }

        protected virtual string getKeyHash()
        {
            return this.ToString();
        }

        public static HashSet<E> getNewHashSet()
        {
            return new HashSet<E>(new Comparador<E>());
        }
        public static HashSet<E> getNewHashSet(IEnumerable<E> anterior)
        {
            return new HashSet<E>(anterior, new Comparador<E>());
        }
        public static SortedSet<E> getNewSortedSet()
        {
            return new SortedSet<E>(new Comparador<E>());
        }
        public static SortedSet<E> getNewSortedSet(IEnumerable<E> anterior)
        {
            return new SortedSet<E>(anterior, new Comparador<E>());
        }
        public static SortedSet<E> getNewSortedSet(params E[] anterior)
        {
            return new SortedSet<E>(anterior, new Comparador<E>());
        }

        public static Dictionary<E, Z> getNewDictionary<Z>()
        {
            return new Dictionary<E, Z>(new Comparador<E>());
        }


        private class Comparador<T> : IEqualityComparer<T>, IComparer<T>
        {
            //private T comparable;



            public int Compare(T x, T y)
            {
                dynamic xi = x;
                dynamic yi = y;
                return xi.CompareTo(yi);
            }

            public bool Equals(T x, T y)
            {
                return Compare(x, y) == 0;
            }

            public int GetHashCode(T obj)
            {
                dynamic obji = obj;
                return obji.getKeyHash().GetHashCode();
            }
        }

        public static SortedSet<E> getNewSortedSet_KeyComparable<EKeyComparable>(Func<E, EKeyComparable> metodoGetKeyComparable) where EKeyComparable : IComparable<EKeyComparable>
        {
            return new SortedSet<E>(ComparableHash<E>.getComparador_getKeyComparable(metodoGetKeyComparable));
        }

        private static ComparadorIndependiente<T> getComparador_getKeyComparable<T, EKeyComparable>(Func<T, EKeyComparable> metodoGetKeyComparable) where EKeyComparable : IComparable<EKeyComparable>
        {
            Func<T, int> metodoGetHash = t => t.ToString().GetHashCode();
            Func<T, T, int> metodoComparar = (x, y) =>
            {
                EKeyComparable a = metodoGetKeyComparable(x);
                EKeyComparable b = metodoGetKeyComparable(y);
                return a.CompareTo(b);
            };//getComparador_getKeyComparable(x).Compare(getComparador_getKeyComparable(y));

            return new ComparadorIndependiente<T>(metodoComparar, metodoGetHash);
        }


        //private class ComparadorIndependiente<T> where T: IEqualityComparer<T>, IComparer<T>
        private class ComparadorIndependiente<T> : IEqualityComparer<T>, IComparer<T>
        {
            //private T comparable;



            private Func<T, T, int> metodoComparar;
            private Func<T, int> metodoGetHash;



            public ComparadorIndependiente(Func<T, T, int> metodoComparar, Func<T, int> metodoGetHash)
            {
                this.metodoComparar = metodoComparar;
                this.metodoGetHash = metodoGetHash;
            }

            public int Compare(T x, T y)
            {
                //dynamic xi = x;
                //dynamic yi = y;
                //return xi.CompareTo(yi);
                return this.metodoComparar(x, y);
            }

            public bool Equals(T x, T y)
            {
                return Compare(x, y) == 0;
            }

            public int GetHashCode(T obj)
            {
                //dynamic obji = obj;
                //return obji.getKeyHash().GetHashCode();
                return this.metodoGetHash(obj);
            }
        }

    }
}
