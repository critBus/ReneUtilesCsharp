using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using ReneUtiles;
using System.Text.RegularExpressions;

namespace ReneUtiles.Clases
{
	public  class ReneBasico
	{
		public readonly  DateTime NULL_DATE=Utiles.NULL_DATE;
		public readonly  TimeSpan NULL_TIME=Utiles.NULL_TIME;

        public static bool hayMatch(Regex re, string texto)
        {
            return Matchs.hayMatch(re, texto);
        }
        public static bool esDouble(string a)
        {
            return Utiles.esDouble(a);
        }


         public  string to_String(Object o) {
	        if (o == null) {
	            return "";
	        }
	        return o.ToString();
	    }
		public static bool? toBool(object a)
		{
            if (a!=null&&a.ToString().Length==0) {
                return false;
            }
			return Utiles.toBool(a); 
		}
		public static string[] split(string a,string separador){
			return Utiles.split(a,separador);
		}
		public static bool isEmptyFullOr(params string[] A)
		{
			return Utiles.isEmptyFullOr(A);
		}
		public static bool isNullOr(params object[] O){
			return Utiles.isNullOr(O);
		}
		public static List<T> list<T>(params T[] L){
			return Utiles.list<T>(L);
		}
		public static  int inT(object a)
		{
			return Utiles.inT(a); 
		}
		public static double dou(object a)
		{
			return Utiles.dou(a); 
		}
		public static string subs(object o, int i0){
			return Utiles.subs(o, i0);
		}
		public static string subs(object o, int i0, int iNoIncluida)
		{
			return Utiles.subs(o, i0, iNoIncluida);
		}
        
		public static bool isEmptyFull(string a)
		{
			return Utiles.isEmptyFull(a);
		}
		public static bool isEmpty(string a)
		{
			return Utiles.isEmpty(a);
		}
		public static string[] split(string a, params char[] separator)
		{
			return Utiles.split(a, separator);
		}
		public static bool startsWithOR(String a, params String[] A)
		{
			return Utiles.startsWithOR(a, A);
		}
		public static bool startsWithOR(String palabra, int i0DePalabra, params String[] A)
		{
			return Utiles.startsWithOR(palabra, i0DePalabra, A);
		}
		public static bool esNumero(String a)
		{
			return Utiles.esNumero(a);
		}
		public static bool esNumeroAll(params string[] A){
			return Utiles.esNumeroAll(A);
		}
		public static bool or<E>(E a, params E[] B)
		{
			return Utiles.or(a, B);
		}
		public static int orIndice<E>(E a, params E[] B)
		{
			return Utiles.orIndice(a, B);
		}
		public static bool isEmpty<E>(List<E> l)
		{
			return Utiles.isEmpty(l);
		}
		public static bool isEmpty<E>(E[] l)
		{
			return Utiles.isEmpty(l);
		}
        
//		public static string str<E>(params E[] a)
//		{
//			
////			if(a.Length==1&&a[0] is List<string>){
////				
////				return str(((List<string>)a[0]).ToArray());
////			}
//			string res = "[";
//			for (int i = 0; i < a.Length; i++) {
//				res += (i != 0 ? " , " : " ") + a[i];
//			}
//			return res += " ]";
//			//return Utiles.str(a);
//		}
	}
}
