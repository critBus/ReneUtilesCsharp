/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 26/9/2021
 * Time: 20:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using ReneUtiles.Clases;

namespace ReneUtiles.Clases.Basicos.String
{
	/// <summary>
	/// Description of StringTokenizer.
	/// </summary>
	public class StringTokenizer:ConsolaBasica
	{
		protected List<StringToken> listaDeTokens;
		protected List<StringToken> listaDeSaltos;
		protected string palabra;
		protected char[] caracteresDeSalto;
		protected int indiceActual;
		protected int LengPalabra;
		protected bool hayNextToken;
		protected bool esPrimerPaso;
		
		protected bool saltarContenedores;
		protected Predicate<char> condicionDeSalto;
		
		
		public StringTokenizer(string palabra,int i0,bool saltarContenedores, string caracteresDeSalto)
			: this(palabra,i0,saltarContenedores, caracteresDeSalto.ToCharArray())
		{
		
		}
//		public StringTokenizer(string palabra, bool saltarContenedores, string caracteresDeSalto)
//			: this(palabra,saltarContenedores, caracteresDeSalto.ToCharArray())
//		{
//		
//		}
		public StringTokenizer(string palabra, params char[] caracteresDeSalto)
			: this(palabra,0, false, caracteresDeSalto)
		{
		
		}
		public StringTokenizer(string Palabra,int I0, bool SaltarContenedores, params char[] CaracteresDeSalto)
		: this(palabra:Palabra,i0:I0,condicionDeSalto:null, saltarContenedores:SaltarContenedores, caracteresDeSalto:CaracteresDeSalto){
		}
		private StringTokenizer(string Palabra,int I0, bool SaltarContenedores,Predicate<char> CondicionDeSalto)
			: this(palabra:Palabra,i0:I0,condicionDeSalto:CondicionDeSalto, saltarContenedores:SaltarContenedores, caracteresDeSalto:new char[0])
		
		{
		
		}
		private StringTokenizer(string palabra,int i0,Predicate<char> condicionDeSalto, bool saltarContenedores,  char[] caracteresDeSalto)
		{
			this.saltarContenedores = saltarContenedores;
			this.palabra = palabra;
			if (isEmpty(caracteresDeSalto)) {
				caracteresDeSalto = new char[]{ ' ' };
			}
			this.caracteresDeSalto = caracteresDeSalto;
			indiceActual = i0;
			LengPalabra = palabra.Length;
			hayNextToken = false;
			esPrimerPaso = true;
			listaDeTokens = new List<StringToken>();
			listaDeSaltos = new List<StringToken>();
			if (condicionDeSalto==null) {
				this.condicionDeSalto=c=>or(c, caracteresDeSalto);	
			}else{
				this.condicionDeSalto=condicionDeSalto;
			}
			
		}
		public string Palabra {
			get{ return this.palabra; }
		}
		
		public int IndiceActual {
			get{ return this.indiceActual; }
		}
		public bool HayNextToken {
			get {
//				try{
				string salto = "";
				if (esPrimerPaso) {
//					cwl("palabra="+palabra);
//					cwl("LengPalabra="+LengPalabra);
					for (int i = indiceActual; i < LengPalabra; i++) {
//						cwl("i="+i+" indiceActual="+indiceActual);
						char c = palabra.ElementAt(i);
						if (saltarContenedores) {
							ParEnvoltura p = ParEnvoltura.esParEnvolturaInicial(c);
							if (p != null) {
								int indiceFinal = palabra.IndexOf(p.FinalChar, indiceActual + 1);
								if (indiceFinal != -1) {
									hayNextToken = false;
									saltaHasta(indiceFinal + 1);
									break;
								} else {
									salto += c;
									indiceActual++;
									continue;
								}
							}
							p = ParEnvoltura.esParEnvolturaFinal(c);
							if (p != null) {
								salto += c;
								indiceActual++;
								continue;
							}
						}
						
						if (!condicionDeSalto(c)) {
							listaDeSaltos.Add(new StringToken(salto, 0, i, i == LengPalabra - 1));
							hayNextToken = true;
							break;
						}
						salto += c;
						indiceActual++;
					}
					esPrimerPaso = false;
				}
				return hayNextToken;
//				}catch{
//					cwl("dio falso");
//					return false;}
			}
			
		}
		//<summary>
		// el nuevoIndiceActual tiene que ser superior al IndiceActual y menor al leng
		// y no lo incluye en el salto plq el IndiceActual=nuevoIndiceActual
		//</summary>
		public bool saltaHasta(int nuevoIndiceActual)
		{
            if (nuevoIndiceActual <= LengPalabra) {
                this.hayNextToken = false;
                return false;
            }
			if (nuevoIndiceActual > indiceActual&&nuevoIndiceActual <= LengPalabra  ) {
				if (nuevoIndiceActual == LengPalabra) {
					indiceActual=LengPalabra - 1;
				}
				
				bool vaHaTerminarEnElLength = nuevoIndiceActual == LengPalabra - 1;
				listaDeSaltos.Add(new StringToken(
					token: subs(palabra, indiceActual, nuevoIndiceActual),
					indiceInicial: indiceActual,
					indiceAContinuacion: nuevoIndiceActual,
					terminaEnElLength: vaHaTerminarEnElLength));
				indiceActual = nuevoIndiceActual;
				if (!vaHaTerminarEnElLength) {
					esPrimerPaso = true;
					return HayNextToken;
				} else {
					return this.hayNextToken = false;
				}
			}
			return HayNextToken;
		
		}
		
		public StringToken next()
		{
			string tk = "";
			string salto = "";
			bool encontroSalto = false;
			int indiceDetoken = indiceActual;
			int indiceAContinuacionTk = -1;
			
			for (int i = indiceActual; i < LengPalabra; i++) {
				hayNextToken = false;
				char c = palabra.ElementAt(i);
				bool saltarPorContenedor = false;
				if (saltarContenedores) {
					ParEnvoltura p = ParEnvoltura.esParEnvolturaInicial(c);
					if (p != null) {
						int indiceFinal = palabra.IndexOf(p.FinalChar, indiceActual + 1);
						if (indiceFinal != -1) {
							hayNextToken = false;
							saltaHasta(indiceFinal + 1);
							i = indiceActual - 1;
							//indiceActual++;
							continue;
						} else {
							saltarPorContenedor = true;
						}
					}
					p = ParEnvoltura.esParEnvolturaFinal(c);
					if (p != null) {
						saltarPorContenedor = true;
					}
				}
				
				
				if (condicionDeSalto(c) || saltarPorContenedor) {
					if (!encontroSalto) {
						encontroSalto = true;
						if (!isEmpty(tk)) {
							indiceAContinuacionTk = i;
						}
						
					}
					
					salto += c;
				} else {
					if (encontroSalto) {
						listaDeSaltos.Add(new StringToken(salto, indiceAContinuacionTk, i, i == LengPalabra - 1));
						//if (!isEmpty(listaDeTokens)) {
						if (!isEmpty(tk)) {
							hayNextToken = true;
							break;
						} else {
							encontroSalto = false;
							salto = "";
							indiceDetoken = i;
						}
						
					}
					tk += c;
				}
				indiceActual++;
			}
			indiceAContinuacionTk = indiceAContinuacionTk == -1 ? indiceActual : indiceAContinuacionTk;
			if (isEmpty(tk)) {
                //hayNextToken = false;//mio ahora
                return null;
			}
			//StringToken stk = new StringToken(tk, indiceDetoken, indiceAContinuacionTk, indiceActual == LengPalabra);
			StringToken stk = new StringToken(tk, indiceDetoken, indiceAContinuacionTk, indiceAContinuacionTk == LengPalabra);
			listaDeTokens.Add(stk);
			return stk;
		}
		
		public static StringTokenizer getTokenizerSeparaciones(string palabra){
			return	new StringTokenizer(palabra,0,false,c=>{return !Char.IsLetterOrDigit(c);});
		}
		
	}
}

//y lo incluye dentro del salto plq el IndiceActual=nuevoIndiceActual+1
