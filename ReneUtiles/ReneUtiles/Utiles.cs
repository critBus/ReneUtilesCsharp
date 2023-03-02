using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Text.RegularExpressions;

using ReneUtiles.Clases;

using ReneUtiles.Clases;
using System.Diagnostics;

namespace ReneUtiles
{
	public delegate E metodoCreador<out E>();
	public delegate R metodoCreador1<A, out R>(A a);
	public delegate R metodoCreador2<A, B, out R>(A a, B b);
	public delegate R metodoCreadorRef2<A, B, out R>(ref A a, ref B b);
	public delegate R metodoCreador3<A, B, C, out R>(A a, B b, C c);
	public delegate R metodoCreador4<A, B, C, D, out R>(A a, B b, C c, D d);
	public delegate void metodoUtilizar<A>(A a);
	public delegate void metodoUtilizarRef<A>(ref A a);
	public delegate void metodoUtilizar2<A, B>(A a, B b);
	public delegate void metodoUtilizarRef2<A, B>(ref A a, ref B b);
	public delegate void metodoUtilizar3<A, B, C>(A a, B b, C c);
	public delegate void metodoUtilizarRef3<A, B, C>(ref A a, ref B b, ref C c);
	public delegate void metodoRealizar();



    public abstract class Utiles
    {
        public readonly static DateTime NULL_DATE = DateTime.MinValue;
        public readonly static TimeSpan NULL_TIME = TimeSpan.Zero;
        public readonly static Dictionary<string, int> NumerosRomanos = getDiccionarioNumerosRomanos();

        //private readonly static Func<string> SEGURIDAD_R_A = () =>
        //{
        //    string []tokens = {
        //        "79b18a7294a2241ec6d6accea10b3be8b5f25f9eea067ac1d0c4526e03af7243"//mio
        //        ,"6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b"
        //        ,"8f61feeb2f616a7769fcfa782ee34f9bb8c49f203b6fb40573f1d2a974ead913"
        //    };
        //    string placaActual= UtilesEncriptar.getSHA256(UtilesHardware.GetMotherBoardID());
        //    foreach (string item in tokens)
        //    {
        //        if (item== placaActual) {
        //            return "";
        //        }
        //    }

        //    throw new Exception("usuario sin autorizacion");

        //};
        //private readonly static string APLICAR_SEGURIDAD = SEGURIDAD_R_A();

        public static string join<T>(IEnumerable<T> a,string j=",") {
            T []A = a.ToArray();
            string r = "";
            for (int i = 0; i < A.Length; i++)
            {
                r += (i != 0 ? j : "") + A[i];
            }
            return r;
        }
        public static int getCantidadDeCaracteresIgualesEnMismaPosicion(string a,string b) {
            int c = 0;

            for (int i = 0; i < a.Length&& i < b.Length; i++)
            {
                if (a[i]==b[i]) {
                    c+=1;
                }
            }
            return c;
        }
        public static void ejecutarCMD(string urlExe,string comando) {

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.UseShellExecute = false;
            psi.Arguments = comando;//"\"" + @"D:\_Cosas\Temporal\manga\APARI3NCIAS (Temporada 1) [08 Cap.] [1080p] [Dual Audio] FDT\S01E02.mkv" + "\""; //"-jar -XX:+UseConcMarkSweepGC -Xmx1024M -Xms1024M START.jar";
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.FileName = urlExe;// @"C:\Program Files\DAUM\PotPlayer\PotPlayerMini64.exe";//"jre8\\bin\\javaw.exe";
            Process.Start(psi);

            

        }

        public static void ejecutarCMD_Visible(string urlExe, string comando=null)
        {

            ProcessStartInfo psi = new ProcessStartInfo();
            
            psi.UseShellExecute = true;
            if (comando!=null) {
                psi.Arguments = comando;//"\"" + @"D:\_Cosas\Temporal\manga\APARI3NCIAS (Temporada 1) [08 Cap.] [1080p] [Dual Audio] FDT\S01E02.mkv" + "\""; //"-jar -XX:+UseConcMarkSweepGC -Xmx1024M -Xms1024M START.jar";
            }
            
            psi.CreateNoWindow = false;
            psi.WindowStyle = ProcessWindowStyle.Normal;
            psi.FileName = urlExe;// @"C:\Program Files\DAUM\PotPlayer\PotPlayerMini64.exe";//"jre8\\bin\\javaw.exe";
            Process.Start(psi);

            //Console.WriteLine(comando);
            //System.Diagnostics.Process.Start(comando);

            //using (Process process = new Process())
            //{
            //    //process.StartInfo.FileName = "cmd.exe";
            //    //process.StartInfo.Arguments = comando;//@"cscript \"C:\Program Files\Microsoft Office\Office16\OSPP.VBS\" /dstatus";
            //    //process.StartInfo.UseShellExecute = false;
            //    //process.StartInfo.RedirectStandardOutput = true;
            //    //process.Start();

            //    //StreamReader reader = process.StandardOutput;
            //    //string output = reader.ReadToEnd();
            //}

        }

        public static string identacion(int indice)
        {
            string r = "";
            for (int i = 0; i < indice; i++)
            {
                r += "\t";
            }
            return r;
        }

        public static string identacionLn(int indice, int separacion0)
        {
            string r = "\n";
            for (int i = 0; i < indice + separacion0; i++)
            {
                r += "\t";
            }
            return r;
        }


        public static string primerCharMinuscula(string a) {
            return Char.ToLower(a[0]) + subs(a, 1);
        }
        public static string primerCharMayuscula(string a)
        {
            return Char.ToUpper(a[0]) + subs(a, 1);
        }
        public static string eliminarAlInicio(string a, char characterARemplazar) {
            string r = "";
            bool seguirRemplazando = true;
            foreach (char c in a)
            {
                if (c!=characterARemplazar) {
                    seguirRemplazando = false;
                }
                if (seguirRemplazando&&c==characterARemplazar) {
                    continue;
                }
                r += c;
            }
            return r;
        }
        public static bool allIn<T>(IEnumerable<T> L,Predicate<T> condicion) {
            foreach (T item in L)
            {
                if (!condicion(item)) {
                    return false;
                }
            }
            return true;
        }
        public static List<string> separadorDePalabrasEnTextoUnido(string palabra) {
            List<string> listaDePalabras = new List<string>();
            List<string> p=new string[] {""}.ToList();
            Action<char> appenC = c => p[0]+= c;
            Action agregarPalabra = () => {
                if (p[0].Length>0) {
                    listaDePalabras.Add(p[0]);
                    p[0] = "";
                }
            };
            bool elAnteriorFueNumero = false;
            foreach (char c in palabra)
            {
                if (Char.IsDigit(c))
                {
                    if (!elAnteriorFueNumero)
                    {
                        agregarPalabra();
                    }
                    appenC(c);
                    elAnteriorFueNumero = true;
                    continue;
                }
                else {
                    elAnteriorFueNumero = false;
                }
                if (Char.IsUpper(c)) {
                    agregarPalabra();
                    appenC(c);
                } else if (c == '_' || Char.IsWhiteSpace(c)) {
                    agregarPalabra();
                } else if (Char.IsLetter(c)) {
                    appenC(c);
                }
            }
            agregarPalabra();
            return listaDePalabras;
        }

        public static bool esDouble(string a)
        {
            int end = a.Length;
            bool hayPunto = false;
            for (int i = 0; i < end; i++)
            {
                char c = a.ElementAt(i);
                if (c=='.') {
                    if (hayPunto)
                    {
                        return false;
                    }
                    else {
                        hayPunto = true;
                    }
                } else if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
            //try{int b=inT(a);return true;}catch{return false;}
        }

        public static bool? toBool(object o){
			if(o==null){
				return null;
			}
			if(o is bool){
				return (bool)o;
			}
			if(o is bool?){
				return (bool?)o;
			}
			return Boolean.Parse(o.ToString().ToLower());
		}
		
		public static int minimo(params int?[] args){
			int? menor=null;
			foreach (int? e in args) {
				if(e!=null&&(menor==null||e<menor)){
					menor=e;
				}
			}
			return menor??-1;
		}
		public static int maximo(params int?[] args){
			int? mayor=null;
			foreach (int? e in args) {
				if(e!=null&&(mayor==null||e>mayor)){
					mayor=e;
				}
			}
			return mayor??-1;
		}
		
		public static string recorrerLineasYModificar(string texto,Func<string,int,string> modificarLinea){
			string r="";
//			int indiceAnterior=0;
//			int ultimoIndice=0;
//			int contador=0;
//			int cantidadSaltos=0;
//			cantidadSaltos=texto.Sum(c=>(cantidadSaltos=(c=='\n'?cantidadSaltos+1:cantidadSaltos)));
//			Console.WriteLine(contador);
//			while(ultimoIndice<texto.Length
//			      &&(ultimoIndice=texto.IndexOf('\n',ultimoIndice))!=-1){
//				
//				r+=modificarLinea(subs((ultimoIndice-indiceAnterior==1)?"":texto,indiceAnterior,ultimoIndice),contador)+(contador+1<cantidadSaltos?"\n":"");
//			}
			string []T=split(texto,'\n');
			for (int i = 0; i < T.Length; i++) {
				r+=modificarLinea(T[i],i)+(i+1<T.Length?"\n":"");
			}
			//Console.WriteLine(r);
			return r;
		}
		
		private static Dictionary<string,int> getDiccionarioNumerosRomanos()
		{
			Dictionary<string,int> d = new Dictionary<string, int>();
			d.Add("I", 1);
			d.Add("V", 5);
			d.Add("X", 10);
			d.Add("L", 50);
			d.Add("C", 100);
			d.Add("D", 500);
			d.Add("M", 1000);
			d.Add("IV", 4);
			d.Add("IX", 9);
			d.Add("XL", 40);
			d.Add("XC", 90);
			d.Add("CD", 400);
			d.Add("CM", 900);
			return d;
		}
		
		public static string remplazarAll<T>(string a,string nuevo,params T[] olds){
			for (int i = 0; i < olds.Length; i++) {
				a=a.Replace(olds[i]+"",nuevo);
			}
			return a;
		}
		
		public static bool esBool(Object o){
			return o is bool;
		}
		public static bool isEmptyFullOr(params string[] A)
		{
			for (int i = 0; i < A.Length; i++) {
				if (isEmptyFull(A[i])) {
					return true;
				}
			}
			return false;
		}
		public static bool isNullOr(params object[] O)
		{
			for (int i = 0; i < O.Length; i++) {
				if (O[i] == null) {
					return true;
				}
			}
			return false;
		}
		public static List<T> list<T>(params T[] L)
		{
			return new List<T>(L);
		}
		
		public static string llevarASingular(string a)
		{
			a = remplazarEnLosfinales(a, "iones", "ion");
			
			a = remplazarEnLosfinales(a, "ios", "io");
			
			
			return a;
			//return a.Replace("iones","ion").Replace("ios","io");
		}
		
		public static string remplazarEnLosfinales(string a, string old, string nuevo)
		{
			if (a.EndsWith(old)) {
				a = subs(a, 0, a.LastIndexOf(old)) + nuevo;
			}
			a = a.Replace(old + "_", nuevo + "_");
			return a;
		}
        public static string remplazarAlfinal(string a, string nuevo,params string[] olds)
        {
            //if (a.EndsWith(old))
            //{
            //    a = subs(a, 0, a.LastIndexOf(old)) + nuevo;
            //}
            //a = a.Replace(old + "_", nuevo + "_");
            string b = a;
            foreach (string old in olds)
            {
                b = remplazarEnLosfinales(a,old,nuevo);
            }
            return b;
        }
        public static string getLowerPiso(string a)
		{
			return a.Replace(" ", "_").ToLower();
		}
		public static string getCapitalizeUnido(string a)
		{
			string r = "";
			
			if (ConstantesExprecionesRegulares.PATRON_MAYUSCULAS_GUION_BAJO.IsMatch(a)) {
				MatchCollection l = ConstantesExprecionesRegulares.PATRON_MAYUSCULAS.Matches(a);
				foreach (Match e in l) {
					r += Utiles.capitalize(e.ToString());
					
				}
			}
            if (r.Length==0) {
                return capitalize(a);
            }
			return r;
		}
		public static string capitalize(string a)
		{
			
			string r = a.ElementAt(0) + "";//seguir!!!!!!!!!
			r = r.ToUpper();
			if (a.Length > 1) {
				r += a.Substring(1).ToLower();
			}
			return r;
		}
		public static string getAleatoriedad(int max)
		{
			Random r = new Random();
			string res = "";
			metodoCreador<bool> getBool = () => r.Next(2) == 0;
			for (int i = 0; i < max; i++) {
				bool esletra = getBool();
				if (esletra) {
					bool minuscula = getBool();
					string letra = ((char)r.Next(97, 123)) + "";
					if (!minuscula) {
						letra = letra.ToUpper();
					} 
					res += letra;
				} else {
					res += r.Next(10);
				}
				//cwl(esLetra());
				//cwl(((char)i)+" i="+i);
			}
			return res;
		}
		public static int getNumeroDeNumeroRomano(string s)
		{
			
			Dictionary<string,int> d = NumerosRomanos;
			int num = 0;
			int lenght = s.Length;
			
			for (int i = 0; i < lenght; i++) {
				
				if (i + 1 < lenght && d.ContainsKey(s.Substring(i, 2))) {
					num += d[s.Substring(i++, 2)];
				} else {
					//ConsolaBasica.cwl("subs(s, i, 1)="+subs(s, i, 1));
					num += d[s.Substring(i, 1)];
				}
			}
			return num;
			//string [][] a={{""},{""}};
			return -1;
		}
		public static string subs(object o, int i0){
			return subs(o,i0,o.ToString().Length);
		}
		public static string subs(object o, int i0, int iNoIncluida)
		{
			//Console.WriteLine("iNoIncluida="+iNoIncluida);
			string a = o.ToString();
			//Console.WriteLine("a="+a);
			int leng = a.Length;
			//Console.WriteLine("leng=" + leng);
			//Console.WriteLine("String.IsNullOrWhiteSpace(a)=" + String.IsNullOrWhiteSpace(a));
			//Console.WriteLine("leng>1=" + (leng > 1));
			//Console.WriteLine("entre(0,true,i0,true,leng-1)=" + entre(0, true, i0, true, leng - 1));
			//Console.WriteLine("entre(1, true, iNoIncluida, true, leng)=" + entre(1, true, iNoIncluida, true, leng));
//			if ((!String.IsNullOrWhiteSpace(a)) && leng > 1
//			    && entre(0, true, i0, true, leng - 1)
//			    && entre(1, true, iNoIncluida, true, leng)) {
			//ConsolaBasica.cwl("i0="+i0+" iNoIncluida="+iNoIncluida+" iNoIncluida - i0="+(iNoIncluida - i0));
			return a.Substring(i0, iNoIncluida - i0);
//			}
//			return "";
		}
		public static int posicionFinalDe(String a, String comienzo, String terminacion, int indiceInicialDeComienzo)
		{
			int contadorDeComienzo = 0;
			int aLeng = a.Length;
			int comienzoLeng = comienzo.Length;
			int terminacionLeng = terminacion.Length;
			// for (int i = indice + 1; i < aLeng;) {
			for (int i = indiceInicialDeComienzo + comienzoLeng; i < aLeng;) {
				//char c=a.ElementAt(i);
				if (startsWithOR(a, i, comienzo)) {
					contadorDeComienzo++;
					i += comienzoLeng;
					continue;
				}
				if (startsWithOR(a, i, terminacion)) {
					if (contadorDeComienzo == 0) {
						return i;
					}
					contadorDeComienzo--;
					i += terminacionLeng;
					continue;
				}
        	
				i++;
			}
			// return a.length();
			return -1;

		}

		
		public static string eliminarContenidoDeEnvolturas(string a)
		{
			
			int end = ParEnvoltura.BASICOS.Length;
			//int indiceActual = 0;
			int aLeng = a.Length;
			for (int i = 0; i < end; i++) {
				ParEnvoltura pe = ParEnvoltura.BASICOS[i];
				//UtilesConsola.cwl("i="+i+" pe.Inicial="+pe.Inicial+" pe.Final="+pe.Final);
				bool continuar;
				do {
					continuar = false;
					int indiceInicial = a.IndexOf(pe.Inicial);
					//UtilesConsola.cwl("indiceInicial="+indiceInicial);
					if (indiceInicial != -1) {
						//indiceActual = indiceInicial;
						int indiceFinal = posicionFinalDe(a, pe.Inicial, pe.Final, indiceInicial);
						//UtilesConsola.cwl("indiceFinal="+indiceFinal);
						if (indiceFinal != -1) {
							indiceFinal += pe.Final.Length;
							string subInicial = indiceInicial > 0 ? subs(a, 0, indiceInicial) : "";
							//UtilesConsola.cwl("subInicial="+subInicial);
							string subfinal = indiceFinal <= aLeng - 1 ? subs(a, indiceFinal, aLeng) : "";
							//UtilesConsola.cwl("subfinal="+subfinal);
							a = subInicial + subfinal;
							aLeng = a.Length;
							continuar = true;
							//UtilesConsola.cwl("a="+a);
							//UtilesConsola.cwStringIndices(a);
							//continue;
						}
					}
				} while(continuar);
			}
			return a;
		}
		public static bool or<E,A>(E a, metodoCreador1<A,E> c, A[] B)
		{
			int length = B.Length;
			for (int i = 0; i < length; i++) {
				if (a.Equals(c(B[i]))) {
					return true;
				}
			}
       
			return false;
		}
		
		public static string getTabulaciones(int cantidad)
		{
			string res = "";
			for (int i = 0; i < cantidad; i++) {
				res += "\t";
			}
			return res;
		}
		public static string[] split(string a,string separador){
			List<string> l=new List<string>();
			int indiceAnterior=0;
			int indiceActual=-1;
			while((indiceActual=a.IndexOf(separador,indiceAnterior))!=-1){
				l.Add(subs(a,indiceAnterior,indiceActual));
				indiceAnterior=indiceActual+separador.Length;
			}
			if(indiceAnterior<a.Length-1){
				l.Add(a.Substring(indiceAnterior));
			}
			return l.ToArray();
		}
    	
		public static string[] split(string a, params char[] separator)
		{
			return a.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		}
		public static string[] split(string a, params string[] separator)
		{
			return a.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		}
//		public static string str<E>(E[] a)
//		{
//			string res = "[";
//			for (int i = 0; i < a.Length; i++) {
//				res += (i != 0 ? " , " : " ") + a[i];
//			}
//			return res += " ]";
//		}
		public static string str<E>(IEnumerable<E> a)
		{
			string res = "[";
			int i=0;
			foreach (E e in a) {
				res +=(i != 0 ? " , " : " ") + e;
				i++;
			}
			
			return res += " ]";
		}
		public static string str_ln<E>(IEnumerable<E> a)
		{
			string res = "[";
			int i=0;
			foreach (E e in a) {
				res +="\n"+ (i != 0 ? " , " : " ") + e;
				i++;
			}
			
			return res += " ]";
		}
		public static bool entre(double min, double numero, double max)
		{
			return entre(min, false, numero, false, max);
		}
		public static bool entre(double min, bool igualMin, double numero, bool igualMax, double max)
		{
			return (igualMin ? min <= numero : min < numero) && (igualMax ? max >= numero : max > numero);
		}

		
		public static bool isEmpty(string a)
		{
            
			return a==null||a.Length == 0;
		}
		public static bool isEmptyFull(string a)
		{
			return a.Trim().Length==0;//String.IsNullOrWhiteSpace(a);
			// return isEmpty(a.Replace(" ", ""));
		}
		public static bool iguales(string a, string b, bool ignoreCase)
		{
			return String.Compare(a, b, ignoreCase) == 0;
		}
		public static bool containsOR(string palabra, params string[] A)
		{
			// ISet<bool> i;
			int total = A.Length;
			for (int i = 0; i < total; i++) {
				if (palabra.Contains(A[i])) {
					return true;
				}
			}
			return false;
		}
        public static bool containsOR_AcontP_arreglarA(string palabra, params string[] A)
        {
            // ISet<bool> i;
            int total = A.Length;
            for (int i = 0; i < total; i++)
            {
                if (A[i]!=null) {
                    string t = arreglarPalabra(A[i].Trim().ToLower());
                    if (t.Contains(palabra))
                    {
                        return true;
                    }
                }
                
            }
            return false;
        }

        public static bool containsOR_AcontP_arreglarA(IEnumerable<string> A,string palabra )
        {
            // ISet<bool> i;
            
            foreach (string item in A)
            {
                string t = arreglarPalabra(item.Trim().ToLower());
                if (t.Contains(palabra))
                {
                    return true;
                }
            }
            
            return false;
        }
        public static int indexOF_OR(string palabra, params string[] A)
		{
			int total = A.Length;
			for (int i = 0; i < total; i++) {
				if (palabra.Contains(A[i])) {
					return palabra.IndexOf(A[i]);
				}
			}
			return -1;
		}

		public static int inT(object a)
		{
			
			if(a is int|| a is double){
				return (int)a;
			}
			return Convert.ToInt32(a.ToString().Trim());
		}
		public static double dou(object a)
		{
			if(a is int|| a is double){
				return (double)a;
			}
			return Convert.ToDouble(a.ToString().Trim());
		}
		//        public static bool startsWithOR(String a, params String[] A)
		//        {
		//            return startsWithOR(a, A);
		//        }

		public static bool startsWith(String a, params char[] A)
		{
			for (int i = 0; i < A.Length; i++) {

				if (!isEmpty(a) && a.ElementAt(0) == A[i]) {
					return true;
				}

			}
			return false;
		}

		public static bool startsWithOR(String a, params String[] A)
		{
			for (int i = 0; i < A.Length; i++) {

				if (a.StartsWith(A[i])) {
					return true;
				}

			}
			return false;
		}
		public static int startsWith_Indice(String palabra, params String[] A)
		{
			return startsWith_Indice(palabra, 0, A);
		}
		public static int startsWith_Indice(String palabra, int i0DePalabra, params String[] A)
		{
			return startsWith_Indice(palabra, i0DePalabra, false, A);
		}
		public static int startsWith_Indice(String palabra, int i0DePalabra, bool ignoreCase, params String[] A)
		{
			//int end = A.Length;
			if (i0DePalabra > 0) {
				palabra = palabra.Substring(i0DePalabra);	
			}
			if (ignoreCase) {
				palabra = palabra.ToLower();
			} 
			for (int i = 0; i < A.Length; i++) {
				string p = ignoreCase ? A[i].ToLower() : A[i];
				if (palabra.StartsWith(p)) {
					return i;
				}
			}
			return -1;
		}
		public static bool startsWithOR(String palabra, int i0DePalabra, params String[] A)
		{
			return startsWithOR(palabra, i0DePalabra, false, A);
		}
		public static bool startsWithOR(String palabra, int i0DePalabra, bool ignoreCase, params String[] A)
		{
			int end = A.Length;
			if (i0DePalabra > 0) {
				palabra = palabra.Substring(i0DePalabra);	
			}
			if (ignoreCase) {
				palabra = palabra.ToLower();
			}
			for (int i = 0; i < end; i++) {
				string p = ignoreCase ? A[i].ToLower() : A[i];
				if (palabra.StartsWith(p)) {
					return true;
				}
			}
			return false;
		}

		public static string replaceFirst(string a, string original, string remplazo)
		{
			if (a.Contains(original)) {
				int i = a.IndexOf(original);
				string res = i == 0 ? "" : subs(a, 0, i);
				//res += a.Substring(i, original.Length)+;
				res += remplazo;
				int iContinuacion = i + original.Length;
				res += iContinuacion == a.Length ? "" : a.Substring(iContinuacion);
				return res;
			}
                
			return a;
		}
		/**
     * Si a es igual a algún elemento del arreglo devuelve true
     *
     * @param <E> Cualquier tipo de objeto
     * @param a Cualquier tipo de objeto
     * @param B Un arrglo de objetos del mismo tipo que "a"
     * @return
     */
		public static bool or<E>(E a, params E[] B)
		{
			int length = B.Length;
			for (int i = 0; i < length; i++) {
				if (a.Equals(B[i])) {
					return true;
				}
			}
       
			return false;
		}
    
		public static int orIndice<E>(E a, params E[] B)
		{
			
			for (int i = 0; i < B.Length; i++) {
				if (a.Equals(B[i])) {
					return i;
				}
            
			}

			return -1;
		}
		public static int endsWith_Indice(String palabra, params String[] A)
		{
			return endsWith_Indice(palabra, 0, A);
		}
		public static int endsWith_Indice(String palabra, int iFinalDePalabra, params String[] A)
		{
			int end = A.Length;
			if (iFinalDePalabra > 0 && iFinalDePalabra < palabra.Length) {
				//palabra = palabra.Substring(i0DePalabra);	
				palabra = subs(palabra, 0, iFinalDePalabra);
				//UtilesConsola.cwl("sub="+palabra);
			}
			for (int i = 0; i < end; i++) {
				//UtilesConsola.cwl("palabra="+palabra+" A[i]="+A[i]);
				if (palabra.EndsWith(A[i])) {
					return i;
				}
			}
			return -1;
		}
		public static bool endsWithOR(String a, params String[] A)
		{
			int end = A.Length;
			for (int i = 0; i < end; i++) {
				if (a.EndsWith(A[i])) {
					return true;
				}
			}
        
			return false;
		}
    
		public static bool charIgualEnAll(int indice, string a, params  String[] B)
		{
    	
			int end = B.Length;
			char ca = a.ElementAt(indice);
			for (int i = 0; i < end; i++) {
				if (ca != B[i].ElementAt(indice)) {
					return false;
				}
			}
        
			return true;
		}
    
		public static bool esCharDesconocidoOR(int indice, params  String[] B)
		{
			int end = B.Length;
			for (int i = 0; i < end; i++) {
				if (B[i].ElementAt(indice) == '�') {
					return true;
				}
			}
    	
			return false;
		}
		public static bool esCharDesconocidoOR(params  char[] B)
		{
			int end = B.Length;
			for (int i = 0; i < end; i++) {
				if (B[i] == '�') {
					return true;
				}
			}
    	
			return false;
		}
    
		public static string eliminarEspaciosEnBlanco(string a)
		{
			string[] s = split(a.Trim(), ' ');
			string res = "";
			int end = s.Length;
			for (int i = 0; i < end; i++) {
				res += s[i];
			}
			return res;
		}
    
		public static bool esNumero(string a)
		{
			int end = a.Length;
            if (end==0) {
                return false;
            }
			for (int i = 0; i < end; i++) {
				char c = a.ElementAt(i);
				if (!Char.IsNumber(c)) {
					return false;
				}
			}
			return true;
			//try{int b=inT(a);return true;}catch{return false;}
		}
		public static bool esNumeroAll(params string[] A){
            if (A.Length==0) {
                return false;
            }
			for (int i = 0; i < A.Length; i++) {
				if(!esNumero(A[i])){
					return false;
				}
			}
			return true;
		}
    
		public static bool isEmpty<E>(List<E> l)
		{
			return l.Count == 0;
		}
		public static bool isEmpty<E>(E[] l)
		{
			return l.Length == 0;
		}
    
		public static string getEspaciosDelAchoDelToStringDe(Object o)
		{
			int leng = o.ToString().Length;
			string r = "";
			for (int i = 0; i < leng; i++) {
				r += " ";
			}
			return r;
		}
		public static bool esAleaterizacion(String a)
		{
			return esAleaterizacion(a, false);
		}
		public static bool esAleaterizacion(String a, bool recorrerInverso)
		{
			int lengA = a.Length;
			if (lengA < 7) {
				return false;
			}
			int minMayusculas = 4, minMinusculas = 4, minNumeros = 0;
			if (lengA < 12) {
				minMayusculas = 3;
				minMinusculas = 2;
				minNumeros = 3;
			}
			int cantidadDeMayusculas = 0, cantidadDeMinusculas = 0, cantidadDeNumeros = 0;
			//bool tieneMinusculas = false;
			
			 
			for (int i = (recorrerInverso ? lengA - 1 : 0); (recorrerInverso ? i >= 0 : i < lengA); i = (recorrerInverso ? i - 1 : i + 1)) {
				if (cantidadDeMayusculas > minMayusculas && cantidadDeMinusculas > minMinusculas && cantidadDeNumeros > minNumeros) {
					return true;
				}
				char c = a.ElementAt(i);
				if (Char.IsUpper(c)) {
					cantidadDeMayusculas++;
					continue;
				}
				if (Char.IsLower(c)) {
					cantidadDeMinusculas++;
					//tieneMinusculas = true;
					continue;
				}
				if (Char.IsNumber(c)) {
					cantidadDeNumeros++;
					continue;
				}

			}
			//if ((!tieneMinusculas) && cantidadDeMayusculas > minMayusculas && cantidadDeNumeros > minNumeros) {
			if (cantidadDeMinusculas==0 && cantidadDeMayusculas > minMayusculas && cantidadDeNumeros > minNumeros) {
				return true;
			}
			if(cantidadDeMayusculas==0&&cantidadDeNumeros>minNumeros&&cantidadDeMinusculas>minMinusculas){
				return true;
			}
			return false;
		}
    
		
		
		public static String arreglarPalabra(String a)
		{
			String b = a;
			char[] malos = {
				'ä',
				'á',
				'é',
				'í',
				'ó',
				'ú',
				'Á',
				'É',
				'Í',
				'Ú',
				'Ó',
				'Ñ',
				'ñ',
				'â',
				'å',
				'à',
				'ä',
				'ê',
				'ë',
				'è',
				'ï',
				'î',
				'ì',
				'Ä',
				'Å',
				'É',
				'ô',
				'ò',
				'ö',
				'û',
				'ù',
				'ÿ',
				'Ö',
				'Ü',
				'Á',
				'Â',
				'À',
				'ã',
				'Ã',
				'ð',
				'Ð',
				'Ê',
				'Ë',
				'È',
				'Í',
				'Ï',
				'Ï',
				'Ì',
				'Ó',
				'Ô',
				'Ô',
				'Ò',
				'õ',
				'Õ',
				'Ú',
				'Û',
				'Ù',
				'ý',
				'Ý'
			};
			char[] buenos = {
				'a',
				'a',
				'e',
				'i',
				'o',
				'u',
				'A',
				'E',
				'I',
				'U',
				'O',
				'N',
				'n',
				'a',
				'a',
				'a',
				'a',
				'e',
				'e',
				'e',
				'i',
				'i',
				'i',
				'A',
				'A',
				'E',
				'o',
				'o',
				'o',
				'u',
				'u',
				'y',
				'O',
				'U',
				'A',
				'A',
				'A',
				'a',
				'A',
				'o',
				'D',
				'E',
				'E',
				'E',
				'I',
				'I',
				'I',
				'I',
				'O',
				'O',
				'O',
				'O',
				'o',
				'O',
				'U',
				'U',
				'U',
				'y',
				'y'
			};
			for (int i = 0; i < malos.Length; i++) {
				b = b.Replace(malos[i], buenos[i]);
			}
			return b;
		}
	}
    
   
}
    
    
    
