/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 27/9/2021
 * Time: 20:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace ReneUtiles.Clases
{
	/// <summary>
	/// Description of ConstantesExprecionesRegulares.
	/// </summary>
	public class ConstantesExprecionesRegulares
	{
        public static readonly string numeros_conComa = @"(?:\d+)(?:[.,]\d+)?";
        public static readonly string numeros = @"(?:\d+)";
		//public static readonly string numeros_3max = @"(?:\d{1,3})";
		public static readonly string numeros_4max = @"(?:\d{1,4})";
        public static readonly string numeros_2max = @"(?:\d{1,2})";

        public static readonly string numeros_g = @"(\d+)";
		
		public static readonly string noNumeros = @"(?:\D*)";
		
		public static readonly string numeros_NoNumeros_gn = @"(?:"+numeros_g+noNumeros+")";
		
		public static readonly string espacios = @"(?:\s*)";
		public static readonly string espacios_UnoAlMenos = @"(?:\s+)";
		public static readonly string separaciones = @"(?:(?:[^\w]|_)*)";
		public static readonly string separaciones_UnoAlMenos = @"(?:(?:[^\w]|_)+)";
		public static readonly string patronNxN = @"(?:\d{1,3})(?:x|X)(?:\d{1,3})";
		public static readonly string patronNxN_PosiblesEspaciosInternos = @"(?:\d+)" + espacios + "(?:x|X)" + espacios + @"(?:\d+)";
		public static readonly string patronNxEN = @"(?:\d{1,3})(?:x|X)[Ee](?:\d{1,3})";
		public static readonly string patronSNxEN = @"(?:[Ss]\d{1,3})(?:x|X)[Ee](?:\d{1,3})";
		//public static readonly string patronSNEN = @"([Ss]\d{1,3})[Ee](\d{1,3})";
		public static readonly string patronSNEN = @"(?:[Ss]\d{1,3})"+separaciones+@"[Ee](?:\d{1,3})";
		public static readonly string caracteresEspeciales = @"(?:[^\w\n\s]*)";
		public static readonly string numerosYespacios_UnoAlMenos = numeros + espacios_UnoAlMenos;
		//public static readonly Regex Re_numerosYespacios_UnoAlMenos=new Regex(numerosYespacios_UnoAlMenos = numeros + espacios_UnoAlMenos);
		public static readonly string cualquieras = @"(?:.*)";
		public static readonly string numerosYLetras = @"(?:(?:(?![_])\w)+)";//?:
		public static readonly string letra = @"(?:(?![\d_])\w)";
		public static readonly string letras = @"(?:" + letra + "+)";
		public static readonly string envolturaInicial = @"[({[]";
		public static readonly string envolturaFinal = @"[])}]";
//@"([)}]|[]])";
		public static readonly string envolturaFinal_ONada = @"(?:" + envolturaFinal + "?)";
		public static readonly string envolturaInicial_ONada = @"(?:" + envolturaInicial + "?)";
		public static readonly string mesN = @"(?:(?:1[0-2])|(?:(?:0?)[1-9]))";
		public static readonly string diaN = @"(?:(?:3[10])|[12][0-9]|(?:(?:0?)[1-9]))";
		public static readonly string añoModerno = @"(?:(?:(?:20[0-2])|(?:19[5-9]))\d)";
		public static readonly string fecha = @"(?:(?:" + diaN + separaciones_UnoAlMenos + diaN + separaciones_UnoAlMenos + añoModerno + ")"
		                                     + "|(?:"
		                                     + mesN + separaciones_UnoAlMenos + mesN + separaciones_UnoAlMenos + añoModerno + ")|(?:"
		                                     + añoModerno + separaciones_UnoAlMenos + diaN + separaciones_UnoAlMenos + mesN + ")|(?:"
		                                     + añoModerno + separaciones_UnoAlMenos + mesN + separaciones_UnoAlMenos + diaN + "))";
		
		
		public static readonly string dominioWeb = @"\A[w]{3}(?:\.)[a-z0-9]+(?:\.)(?:com|net|info|org)\Z";
		public static readonly string email = @"\A(?:\w+\.?\w*\@\w+\.)(?:com)\Z";
		public static readonly string emailTo = @"\A(?:mailto:)?(?:\w{3,})(?:\.\w{3,})?@(?:\w{2,}\.){1,}(?:\w{2,4})\Z";
		public static readonly string NumerosRomanos = "(?:M{0,4}(?:CM|CD|D?C{0,3})(?:XC|XL|L?X{0,3})(?:IX|IV|V?I{0,3}))";
		public static readonly string NumerosRomanosPrimeros = "(?:((?:XC|XL|L?X{0,3})(?:IX|IV|V?I{1,3}))|(?:(?:XC|XL|L?X{1,3})(?:IX|IV|V?I{0,3})))";
		public static readonly string NumerosRomanos0_9 = "(?:(?:IX|IV|V?I{1,3}))";
		public static readonly string ElSiguienteNoEsLetraNiNumero = @"(?!(?:(?![_])\w))";
		public static readonly string ElSiguienteNoEsLetra = @"(?!(?:(?![\d_])\w))";
		
		public static readonly string ElAnteriorNoEsLetraNiNumero = @"(?<!(?:(?![_])\w))";
		//		public static readonly string Nm=@"(\d+(" + Operaciones.getSeparadorDecimal() + @")?\d*)";//numero en el contexto matematico osea incluye decimales y negativos
		//		public static readonly string signoNm=@"(([+-])?)";
		//		public static readonly string parteRealNm = signoNm + Nm;
		//		public static readonly string parteImaginariaNm = "((" + signoNm + "(" + Nm + ")?" + "[i])?)";
		//		public static readonly string numeroiImaginario = "(" + parteRealNm + parteImaginariaNm + ")|("+parteImaginariaNm+parteRealNm+")";
		//
		
		public static readonly Regex PATRON_MAYUSCULAS_GUION_BAJO = new Regex("^(?:(?:[A-Z]+_?)+)$");
		public static readonly Regex PATRON_MAYUSCULAS = new Regex("(?:[A-Z]+)");


        public static readonly string __MEDIO_PATRON_LETRAS = "(?:\\w|[ÑñáéíóúÁÉÍÓÚÀÈÌÒÙàèìòù])";

        public static readonly Regex PATRON_CONTIENE_LETRAS = new Regex("(?:\\d*)((?![\\d_])" + __MEDIO_PATRON_LETRAS + "+(?<![\\d_]))(?:\\d*)");
        public static readonly Regex PATRON_CORREO = new Regex("^([a-z0-9_]+(?:(?:[.]?[a-z0-9_])*)[@][a-z0-9_]+\\.)(com)$");
        public static readonly Regex PATRON_SOLO_LETRAS = new Regex("^(?:(?:[ ]*)(?:(?![\\d_])" + __MEDIO_PATRON_LETRAS + "(?<![\\d_]))+(?:[ ]*))+$");
        public static readonly Regex PATRON_SOLO_LETRAS_Y_NUMEROS = new Regex("^(?:(?:[ ]*)(?:(?![_])" + __MEDIO_PATRON_LETRAS + "(?<![_]))+(?:[ ]*))+$");
        public static readonly Regex PATRON_SOLO_ALFANUMERICOS = new Regex("^(?:(?:[ ]*)" + __MEDIO_PATRON_LETRAS + "+(?:[ ]*))+$");
        public static readonly Regex PATRON_TIENE_NUMEROS = new Regex("[0-9]+");




        public ConstantesExprecionesRegulares()
		{
		}
		
		public static  string getGrupoNombrado(string key,string exprecion){
			return @"(?<"+key+@">"+exprecion+@")";
		}
		
		public static string[] getSoloConSeparaciones(params string[] palabras)
		{
			int end = palabras.Length;
			string[] res = new string[end];
			for (int i = 0; i < end; i++) {
				res[i] = "^" + separaciones + palabras[i] + separaciones + "$";
			}
			return res;
		}
		
		public static string getPatronPalabrasOR(bool ignoreCase, params string[] palabras)
		{
			string r = "("+(ignoreCase?"?i:(?:":"");
			int end = palabras.Length;
			for (int i = 0; i < end; i++) {
				string p = palabras[i];
				
				p=Utiles.arreglarPalabra(p);//nuevo !!!!!!!!!!!!!!
				
				int pLeng = p.Length;
				string pArreglada = "";
				for (int j = 0; j < pLeng; j++) {
					char c = p.ElementAt(j);
					if (!(Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c))) {
						if (Char.IsWhiteSpace(c)) {
							pArreglada += @"\s";
							continue;
						}
						pArreglada += "[" + c + "]";
						continue;
					}
//					else {
//						if (primeraLatraMayusYminus && Char.IsLetter(c)) {
//							pArreglada += "[" + Char.ToUpper(c) + "" + Char.ToLower(c) + "]";
//							continue;
//						}
//					}
					pArreglada += c;
				}
				p = pArreglada;
//				if (ignoreCase) {
//					p = "(?i:" + p + ")";
//				}
//				if (primeraLatraMayusYminus) {
//					char c=p.ElementAt(0);
//					if (Char.IsLetter(c)) {
//						p="["+Char.ToUpper(c)+""+Char.ToLower(c)+"]"+(p.Length>1?p.Substring(1):"");
//					}
//					
//				}
				r += (i != 0 ? "|" : "") + "(?:" + p + ")";
			}
			return r + "))";
			
		}
		
		public static string getPatronTerminacionNumerica(TerminacionNumerica[] T,string key_grupo_numero=null)
		{
			string r = "(?:";
			//r+="\n";
			for (int i = 0; i < T.Length; i++) {
				TerminacionNumerica t = T[i];
				r += i != 0 ? "|" : "";
				//r+="\n";
				r+="(?:";
				if(key_grupo_numero!=null){
					r+="(?<"+key_grupo_numero+">";
				}
				r += t.Numero;
				if(key_grupo_numero!=null){
					r+=")";
				}
				r += "(?:";
				for (int j = 0; j < t.Terminaciones.Length; j++) {
					r += j != 0 ? "|" : "";
					//r+="(";
					r += t.Terminaciones[j];
					//r+=")";
				}
				r += ")";
				r += ")";
				//r+="\n";
			}
			return r + ")";
		}
		public static string getPatronTerminacionNumerica()
		{
			return getPatronTerminacionNumerica(TerminacionNumerica.TerminacionesNumericasBasicas);
		
		}
	}
}
