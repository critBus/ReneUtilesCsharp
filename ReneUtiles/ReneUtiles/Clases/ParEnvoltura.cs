/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 8/10/2021
 * Time: 18:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ReneUtiles.Clases
{
	/// <summary>
	/// Description of ParEnvoltura.
	/// </summary>
	public class ParEnvoltura
	{
		public static readonly ParEnvoltura 
			PARENTESIS=new ParEnvoltura("(",")"),
			CORCHETES=new ParEnvoltura("[","]"),
			LLAVES=new ParEnvoltura("{","}") ;
		public static readonly ParEnvoltura[] BASICOS={PARENTESIS,CORCHETES,LLAVES};
		
		private string inicial;
		private string final;
		private char inicialChar;
		private char finalChar;
		
		public ParEnvoltura(string inicial,string final)
		{
			this.Inicial=inicial;
			this.Final=final;
		}
		
		
		public static ParEnvoltura esParEnvolturaInicial(char c){
			int end=BASICOS.Length;
			for (int i = 0; i < end; i++) {
				if (BASICOS[i].InicialChar==c) {
					return BASICOS[i];
				}
			}
			return null;
		}
		public static ParEnvoltura esParEnvolturaFinal(char c){
			int end=BASICOS.Length;
			for (int i = 0; i < end; i++) {
				if (BASICOS[i].FinalChar==c) {
					return BASICOS[i];
				}
			}
			return null;
		}
		
		public string Inicial{
			get{return this.inicial;}
			set{
				this.inicial=value;
				this.inicialChar=value.ElementAt(0);
			}
		}
		public string Final{
			get{return this.final;}
			set{
				this.final=value;
				this.finalChar=value.ElementAt(0);
			}
		}
		public char InicialChar{
			get{return this.inicialChar;}
			set{
				this.inicialChar=value;
				this.inicial=value+"";
			}
		}
		public char FinalChar{
			get{return this.finalChar;}
			set{
				this.finalChar=value;
				this.final=value+"";
			}
		}
	}
}
