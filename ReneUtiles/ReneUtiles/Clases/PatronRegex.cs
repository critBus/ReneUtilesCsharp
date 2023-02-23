/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 15/10/2021
 * Time: 19:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

//using ReneUtiles.Clases.Multimedia.Series.Contextos;
//using ReneUtiles.Clases.Multimedia.Series;
//using ReneUtiles.Clases.Basicos.String;
//using ReneUtiles.Clases.Multimedia;
//using ReneUtiles.Clases.Multimedia.Relacionador.Saltos;

using System.Text.RegularExpressions;
using ReneUtiles;

using System.IO;

namespace ReneUtiles.Clases
{
	/// <summary>
	/// Description of PatronRegex.
	/// </summary>
	public class PatronRegex
	{
		private string patron;
			
		private bool ignoreCase;
		//re patron
		//inicial ^
		//S separacion
		//Su separacionUnoAlMenos
		//Final &
		//Sf el siguiente no es letra ni numero
		private Regex re;
		private Regex reInicial;
		private Regex reInicialFinal;
		private Regex reInicialS;
		private Regex reInicialSu;
		private Regex reInicialSf;
		private Regex suReInicial;
		private Regex suRe;
		private Regex suReSf;
		private Regex reS;
		private Regex reSu;
		private Regex reSf;
		private Regex SreS;
		private Regex sreSfS;
		private Regex sSfre;
		private Regex sSfreS;
		private Regex sSfreSfS;
		private Regex reFinal;
		private Regex reSfFinal;
		private Regex reInicialS_SFinal;
			
		public string Patron {
			get{ return this.patron; }
		}
		public PatronRegex(string patron)
		{
			this.patron = patron;
			this.ignoreCase = true;
		}
		private bool estaVacio(string p){
			return p=="";
		}
		private Regex getRegex(string p)
		{
			if (ignoreCase) {
				p = "(?i:" + p + ")";
			}
			return new Regex(p);
		}
		public Regex Re {
			get { if(estaVacio(patron)){return new Regex("");}
				
				if (re == null) {
					re = getRegex(patron);
				}
				return re;
			}
		}
		public Regex ReFinal {
			get { if(estaVacio(patron)){return new Regex("");}
				if (reFinal == null) {
					reFinal = getRegex(patron + "$");
				}
				return reFinal;
			}
		}
		public Regex ReInicial {
			get { if(estaVacio(patron)){return new Regex("");}
				if (reInicial == null) {
					reInicial = getRegex("^" + patron);
				}
				return reInicial;
			}
		}
//reInicialFinal
		public Regex ReInicialFinal {
			get { if(estaVacio(patron)){return new Regex("");}
				if (reInicialFinal == null) {
					reInicialFinal = getRegex("^" + patron + "$");
				}
				return reInicialFinal;
			}
		}
		public Regex ReInicialS {
			get { if(estaVacio(patron)){return new Regex("");}
				if (reInicialS == null) {
					reInicialS = getRegex("^" + patron + ConstantesExprecionesRegulares.separaciones);
				}
				return reInicialS;
			}
		}
		public Regex ReInicialSu {
			get { if(estaVacio(patron)){return new Regex("");}
				if (reInicialSu == null) {
					reInicialSu = getRegex("^" + patron + ConstantesExprecionesRegulares.separaciones_UnoAlMenos);
				}
				return reInicialSu;
			}
		}
		public Regex ReInicialSf {
			get { if(estaVacio(patron)){return new Regex("");}
				if (reInicialSf == null) {
					reInicialSf = getRegex("^" + patron + ConstantesExprecionesRegulares.ElSiguienteNoEsLetraNiNumero);
				}
				return reInicialSf;
			}
		}
		public Regex ReSfFinal {
			get { if(estaVacio(patron)){return new Regex("");}
				if (reSfFinal == null) {
					reSfFinal = getRegex(ConstantesExprecionesRegulares.ElAnteriorNoEsLetraNiNumero+patron + "$");
				}
				return reSfFinal;
			}
		}
		public Regex ReS {
			get { if(estaVacio(patron)){return new Regex("");}
				if (reS == null) {
					reS = getRegex(patron + ConstantesExprecionesRegulares.separaciones);
				}
				return reS;
			}
		}
		public Regex ReSu {
			get { if(estaVacio(patron)){return new Regex("");}
				if (reSu == null) {
					reSu = getRegex(patron + ConstantesExprecionesRegulares.separaciones_UnoAlMenos);
				}
				return reSu;
			}
		}
		public Regex ReSf {
			get { if(estaVacio(patron)){return new Regex("");}
				if (reSf == null) {
					reSf = getRegex(patron + ConstantesExprecionesRegulares.ElSiguienteNoEsLetraNiNumero);
				}
				return reSf;
			}
		}
		public Regex SReS {
			get { if(estaVacio(patron)){return new Regex("");}
				if (SreS == null) {
					SreS = getRegex(ConstantesExprecionesRegulares.separaciones + patron + ConstantesExprecionesRegulares.separaciones);
				}
				return SreS;
			}
		}
			
		public Regex SuReInicial {
			get { if(estaVacio(patron)){return new Regex("");}
				if (suReInicial == null) {
					suReInicial = getRegex("^" + ConstantesExprecionesRegulares.separaciones_UnoAlMenos + patron);
				}
				return suReInicial;
			}
		}
		public Regex SuRe {
			get { if(estaVacio(patron)){return new Regex("");}
				if (suRe == null) {
					suRe = getRegex(ConstantesExprecionesRegulares.separaciones_UnoAlMenos + patron);
				}
				return suRe;
			}
		}
		public Regex SuReSf {
			get { if(estaVacio(patron)){return new Regex("");}
				if (suReSf == null) {
					suReSf = getRegex(ConstantesExprecionesRegulares.separaciones_UnoAlMenos + patron + ConstantesExprecionesRegulares.ElSiguienteNoEsLetraNiNumero);
				}
				return suReSf;
			}
		}
			
		public Regex SReSfS {
			get { if(estaVacio(patron)){return new Regex("");}
				if (sreSfS == null) {
					sreSfS = getRegex(ConstantesExprecionesRegulares.separaciones + @"(?:" + patron + ConstantesExprecionesRegulares.ElSiguienteNoEsLetraNiNumero + @")" + ConstantesExprecionesRegulares.separaciones);
				}
				return sreSfS;
			}
		}
//ssfre
		public Regex SSfRe {
			get { if(estaVacio(patron)){return new Regex("");}
				if (sSfre == null) {
					sSfre = getRegex(ConstantesExprecionesRegulares.separaciones + @"(?:" + ConstantesExprecionesRegulares.ElAnteriorNoEsLetraNiNumero + patron + @")");
				}
				return sSfre;
			}
		}
//ssfre
			
		public Regex SSfReS {
			get { if(estaVacio(patron)){return new Regex("");}
				if (sSfreS == null) {
					sSfreS = getRegex(ConstantesExprecionesRegulares.separaciones + @"(?:" + ConstantesExprecionesRegulares.ElAnteriorNoEsLetraNiNumero + patron + @")" + ConstantesExprecionesRegulares.separaciones);
				}
				return sSfreS;
			}
		}
//sSfreSfS
		public Regex SSfreSfS {
			get { if(estaVacio(patron)){return new Regex("");}
				if (sSfreSfS == null) {
					sSfreSfS = getRegex(ConstantesExprecionesRegulares.separaciones + @"(?:" + ConstantesExprecionesRegulares.ElAnteriorNoEsLetraNiNumero + patron + ConstantesExprecionesRegulares.ElSiguienteNoEsLetraNiNumero + @")" + ConstantesExprecionesRegulares.separaciones);
				}
				return sSfreSfS;
			}
		}
//sSfreSfS
			
		public Regex InicialSReSFinal {
			get { if(estaVacio(patron)){return new Regex("");}
				if (reInicialS_SFinal == null) {
					reInicialS_SFinal = getRegex("^" + ConstantesExprecionesRegulares.separaciones + @"(?:" + patron + @")" + ConstantesExprecionesRegulares.separaciones + "$");
				}
				return reInicialS_SFinal;
			}
		}
			
		//reInicialS_SFinal
		//			public Regex SuReSun {
		//				get { if(estaVacio(patron)){return new Regex("");}
		//					if (suRe == null) {
		//						suRe = new Regex(ConstantesExprecionesRegulares.separaciones_UnoAlMenos+ patron  );
		//					}
		//					return suRe;
		//				}
		//			}
				
	}
		
}
