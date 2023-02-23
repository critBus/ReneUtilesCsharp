/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 5/10/2021
 * Time: 20:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Text.RegularExpressions;

using ReneUtiles.Clases;

namespace ReneUtiles
{
	public class DatosDeNumeroEnteroEncontrado{
		public int numero;
		public int indiceInicial;
	}
	/// <summary>
	/// Description of Matchs.
	/// </summary>
	public abstract class Matchs:ConsolaBasica
	{
		public static readonly	PatronRegex R_N = new PatronRegex(ConstantesExprecionesRegulares.numeros_g);
		public static readonly	Regex Re_numerosEnterosDelFinal=new Regex(ConstantesExprecionesRegulares.numeros_NoNumeros_gn+"$");


        public static bool hayMatch(Regex re,string texto) {
            return re.IsMatch(texto);
        }

        /// <summary>
        /// i 0-n  plq si el grupo es 1 seria i=0
        /// </summary>
        /// <param name="m"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int inT_mg(Match m,int i){
			return inT(m.Groups[i+1].ToString());
		}
		public static string getTextoArreglado(string a)
		{
			a = Utiles.arreglarPalabra(a);
			int pLeng = a.Length;
			string pArreglada = "";
			for (int j = 0; j < pLeng; j++) {
				char c = a.ElementAt(j);
				if (!(Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c))) {
					if (Char.IsWhiteSpace(c)) {
						pArreglada += @"\s";
						continue;
					}
					pArreglada += "[" + c + "]";
					continue;
				}

				pArreglada += c;
			}
			a = pArreglada;
			return a;
		}
		public static string repetirGrupo(string key){
			return @"\k<"+key+@">";
		}
		
		public static string unoAlMenos(string expre){
			return "(?:"+expre+")+";
		}
		public static string orExpr(params  string[] Exprs){
			string r=@"(?:";
			for (int i = 0; i < Exprs.Length; i++) {
				r+=(i!=0?@"|":"")+Exprs[i];
			}
			return r+@")";
		}
		public static string grupoNombrado(string key,string expre){
			return ConstantesExprecionesRegulares.getGrupoNombrado(key,expre);
		}
		
		
		public static DatosDeNumeroEnteroEncontrado getPrimerNumerEntero(string a){
			DatosDeNumeroEnteroEncontrado d=new DatosDeNumeroEnteroEncontrado();
			bool atrapoAlNumero=false;
			string numeroStr="";
			for (int i = 0; i < a.Length; i++) {
				char c=a.ElementAt(i);	
				if(!Char.IsNumber(c)){
					if(atrapoAlNumero){
						break;
					}else{
						continue;
					}
					
				}
				if(!atrapoAlNumero){
					d.indiceInicial=i;
				}
				numeroStr+=c;
				atrapoAlNumero=true;
			}
			d.numero= inT(numeroStr);
			return d;
		}
		
		public static int getNumeroInicialEntero(string a){
			string numeroStr="";
			for (int i = 0; i < a.Length; i++) {
				char c=a.ElementAt(i);	
				//if(c=='.'||Char.IsPunctuation(c)||!Char.IsNumber(c)){
				if(!Char.IsNumber(c)){
					break;
				}
				numeroStr+=c;
			}
			return inT(numeroStr);
		}
//		public static int inT_Grp(Group g){
//			return inT(g.ToString());
//		}
//		
//		public class DatosDeGetLetras
//		{
//			public string Letras;
//			public int IndiceLetras;
//			public  DatosDeGetLetras()
//			{
//				Letras = null;
//				IndiceLetras = -1;
//			}
//			public  DatosDeGetLetras(DatosDeGetLetras d)
//			{
//				Letras = d.Letras;
//				IndiceLetras = d.IndiceLetras;
//			}
//			
//		}
//		public class DatosDeGetLetrasMatch:DatosDeGetLetras
//		{
//			public Match M{ get; set; }
//			public int IndiceLetrasFueraDeM{ get; set; }
//			public int I0{ get; set; }
////indice real del i0
//			public DatosDeGetLetrasMatch()
//				: base()
//			{
//			}
//			public DatosDeGetLetrasMatch(DatosDeGetLetras d)
//				: base(d)
//			{
//				if (d.GetType() == GetType()) {
//					DatosDeGetLetrasMatch dd = ((DatosDeGetLetrasMatch)d);
//					M = dd.M;
//					IndiceLetrasFueraDeM = dd.IndiceLetrasFueraDeM;
//				}
//			}
//			public DatosDeGetLetrasMatch(DatosDeGetLetras d, Match m, int i0)
//				: base(d)
//			{
//				this.M = m;
//				IndiceLetras = IndiceLetras + M.Length;
//				IndiceLetrasFueraDeM = IndiceLetras + i0;
//				I0 = i0;
//			}
//		}
//		
//		
//		
//		
//		public static DatosDeGetLetras getDatosLetrasInicial(string texto)
//		{
//			DatosDeGetLetras d = new DatosDeGetLetras();
//			string n = "";
//			int end = texto.Length;
//			for (int i = 0; i < end; i++) {
//				char c = texto.ElementAt(i);
//				if (Char.IsLetter(c)) {
//					n += c;
//				} else {
//					d.IndiceLetras = i - 1;
//					break;
//				}
//    			
//			}
//			if (d.IndiceLetras == -1) {
//				d.IndiceLetras = end - 1;
//			}
//			d.Letras = n;
//			return d;
//    	
//		}
//		public static DatosDeGetLetras getDatosLetrasFinal(string texto)
//		{
//			DatosDeGetLetras d = new DatosDeGetLetras();
//			string n = "";
//			int end = texto.Length;
//			for (int i = end - 1; i >= 0; i--) {
//				char c = texto.ElementAt(i);
//				if (Char.IsNumber(c)) {
//					
//					n = c + n;
//				} else {
//					d.IndiceLetras = i + 1; 
//					break;
//				}
//    			
//			}
//			if (d.IndiceLetras == -1) {
//				d.IndiceLetras = 0;
//			}
//			d.Letras = n;
//			return d;
//    	
//		}
//		public static DatosDeGetLetrasMatch getDatosLetrasFinal(Match textoDondeBuscar, Match patronEncontradoDentroDeltexto)
//		{
//			return new DatosDeGetLetrasMatch(getDatosLetrasFinal(patronEncontradoDentroDeltexto.ToString()), patronEncontradoDentroDeltexto, textoDondeBuscar.Index);
//			
//		}
//		
//		public static DatosDeGetNumeroMatch getDatosNumeroInicial_IntPositivo(Match m)
//		{
//			return new DatosDeGetNumeroMatch(getDatosNumeroInicial_IntPositivo(m.ToString()), m);
//		}
//		
//		//<summary>
//		//le aplica el patron al m.ToString, se supone que de un nuevo Match que comienze en numeros
//		//</summary>
//		public static DatosDeGetNumeroMatch getDatosNumeroInicial_IntPositivo(Match m, Regex patron)
//		{
//			Match mm = getMatch(m, patron);
//			return new DatosDeGetNumeroMatch(getDatosNumeroInicial_IntPositivo(mm.ToString()), mm, m.Index);
//			
//		}
//		
//		
//		public static int getNumeroInicial_IntPositivo(Match m, Regex patron)
//		{
//			return getNumeroInicial_IntPositivo(getMatch(m, patron));
//		}
//		
//		
//		public static int getNumeroInicial_IntPositivo(Match m)
//		{
//			return getNumeroInicial_IntPositivo(m.ToString());
//		}
//		public static int getNumeroInicial_IntPositivo(string texto)
//		{
//			return getDatosNumeroInicial_IntPositivo(texto).Numero;
//		}
//		
//		public static DatosDeGetNumero getDatosNumeroInicial_IntPositivo(string texto)
//		{
//			DatosDeGetNumero d = new DatosDeGetNumero();
//			string n = "";
//			int end = texto.Length;
//			for (int i = 0; i < end; i++) {
//				char c = texto.ElementAt(i);
//				if (Char.IsNumber(c)) {
//					n += c;
//				} else {
//					break;
//				}
//    			
//			}
//			
//			d.IndiceNumero=0;
//			d.Numero = inT(n);
//			return d;
//    	
//		}
//		public static DatosDeGetNumero getDatosNumeroInicial_IntPositivo(string texto,int I0)
//		{
//			DatosDeGetNumero d = new DatosDeGetNumero();
//			string n = "";
//			int end = texto.Length;
//			for (int i = I0; i < end; i++) {
//				char c = texto.ElementAt(i);
//				if (Char.IsNumber(c)) {
//					n += c;
//				} else {
//					d.IndiceNumero = i - 1;
//					break;
//				}
//    			
//			}
//			if (d.IndiceNumero == -1) {
//				d.IndiceNumero = end - 1;
//			}
//			d.Numero = inT(n);
//			return d;
//    	
//		}
//		
//		
//		
//		
//		public class DatosDeGetNumero
//		{
//			public int Numero{ get; set; }
//			public int IndiceNumero{ get; set; }
//			public  DatosDeGetNumero()
//			{
//				Numero = -1;
//				IndiceNumero = -1;
//			}
//			public  DatosDeGetNumero(DatosDeGetNumero d)
//			{
//				Numero = d.Numero;
//				IndiceNumero = d.IndiceNumero;
//			}
//			
//		}
//		public class DatosDeGetNumeroMatch:DatosDeGetNumero
//		{
//			public Match M{ get; set; }
//			public int IndiceNumeroFueraDeM{ get; set; }
//			public int I0{ get; set; }
////indice real del i0
//			public DatosDeGetNumeroMatch()
//				: base()
//			{
//			}
//			public DatosDeGetNumeroMatch(DatosDeGetNumero d)
//				: base(d)
//			{
//				if (d.GetType() == GetType()) {
//					DatosDeGetNumeroMatch dd = ((DatosDeGetNumeroMatch)d);
//					M = dd.M;
//					IndiceNumeroFueraDeM = dd.IndiceNumeroFueraDeM;
//				}
//			}
//			public DatosDeGetNumeroMatch(DatosDeGetNumero d, Match m, int i0)
//				: base(d)
//			{
//				this.M = m;
//				//IndiceNumero=IndiceNumero+M.Length;
//				IndiceNumero = IndiceNumero + M.Index;
//				IndiceNumeroFueraDeM = IndiceNumero + i0;
//				I0 = i0;
//			}
//			public DatosDeGetNumeroMatch(DatosDeGetNumero d, Match m)
//				: base(d)
//			{
//				this.M = m;
//				//IndiceNumero=IndiceNumero+M.Length;
//				I0 = m.Index;
//				IndiceNumeroFueraDeM = IndiceNumero + I0;
//				
//			}
//			//public int getIndiceReal(int i){}
//			//public DatosDeGetNumeroMatch(DatosDeGetNumeroMatch d):base(d){M=d.M;}
//		}
//		
//		public static DatosDeGetNumeroMatch getDatosNumeroFinal_IntPositivo(Match m)
//		{
//			return new DatosDeGetNumeroMatch(getDatosNumeroFinal_IntPositivo(m.ToString()), m);
//		}
//		
//		public static DatosDeGetNumeroMatch getDatosNumeroFinal_IntPositivo(Match m, Regex patron)
//		{
//			Match mm = getMatch(m, patron);
//				return new DatosDeGetNumeroMatch(getDatosNumeroFinal_IntPositivo(mm.ToString()), mm, m.Index);
//			
//		}
//		public static DatosDeGetNumeroMatch getDatosNumero_IntPositivo(Match m){
//			return new DatosDeGetNumeroMatch(getDatosNumero_IntPositivo(m.ToString()),m);
//		}
//		public static DatosDeGetNumero getDatosNumero_IntPositivo(string texto){
//			
//			Match m=getMatch(texto,Re_N.Re);
//			if (m!=null) {
//				DatosDeGetNumero d = new DatosDeGetNumero();
//				d.IndiceNumero=m.Index;
//				d.Numero=inT(m.ToString());
//				return d;
//			}
//			return null;
//		}
//
//		public static int getNumeroFinal_IntPositivo(Match m, Regex patron)
//		{
//			return getNumeroFinal_IntPositivo(getMatch(m, patron));
//		}
//		
//		public static int getNumeroFinal_IntPositivo(Match m)
//		{
//			return getNumeroFinal_IntPositivo(m.ToString());
//		}
//		
		public static int getNumeroFinal_IntPositivo(string texto)
		{
			Match m=Re_numerosEnterosDelFinal.Match(texto);
			if(m.Success){
				return inT(m.Groups[1].ToString());
			}
			return -1;
//			return getDatosNumeroFinal_IntPositivo(texto).Numero;
		}
//		public static DatosDeGetNumero getDatosNumeroFinal_IntPositivo(string texto)
//		{
//			DatosDeGetNumero d = new DatosDeGetNumero();
//			string n = "";
//			int end = texto.Length;
//			for (int i = end - 1; i >= 0; i--) {
//				char c = texto.ElementAt(i);
//				if (Char.IsNumber(c)) {
//					
//					n = c + n;
//				} else {
//					d.IndiceNumero = i + 1;
//					break;
//				}
//    			
//			}
//			if (d.IndiceNumero == -1) {
//				d.IndiceNumero = 0;
//			}
//			d.Numero = inT(n);
//			return d;
//    	
//		}
//		
//    
//		
//		public static DatosDeGetNumero getDatosNumeroFinal_IntPositivo(string texto,int I0)
//		{
//			DatosDeGetNumero d = new DatosDeGetNumero();
//			string n = "";
//			int end = texto.Length;
//			for (int i = I0; i >= 0; i--) {
//				char c = texto.ElementAt(i);
//				if (Char.IsNumber(c)) {
//					
//					n = c + n;
//				} else {
//					d.IndiceNumero = i + 1;
//					break;
//				}
//    			
//			}
//			if (d.IndiceNumero == -1) {
//				d.IndiceNumero = 0;
//			}
//			d.Numero = inT(n);
//			return d;
//    	
//		}
//    
		
//		public static Match getMatch(Match m, Regex re)
//		{
//			return getMatch(m.ToString(), re);
//		}
//		public static Match getMatch(string texto, Regex re)
//		{
//			MatchCollection l = re.Matches(texto);
//			foreach (Match e in l) {
//				if (e.Length > 0) {
//					return e;
//				}
//			}
//			return null;
//		}

//		public static Match getMatch(string texto, int i0, Regex re)
//		{
//			MatchCollection l = re.Matches(texto, i0);
//			foreach (Match e in l) {
//				if (e.Length > 0) {
//					return e;
//				}
//			}
//			return null;
//		}
//		public MatchCollection getMatchs(string texto, int i0, Regex re){
//		return re.Matches(texto, i0);
//		}
		
	}
}
