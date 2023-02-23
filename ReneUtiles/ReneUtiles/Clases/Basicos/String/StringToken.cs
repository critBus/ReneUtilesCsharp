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
	/// Description of StringToken.
	/// </summary>
	public class StringToken:ConsolaBasica
	{
		private string token;
		private int indiceInicial;
		private int indiceAContinuacion;
		private bool terminaEnElLength;
		public StringToken(string token, int indiceInicial,int indiceAContinuacion,bool terminaEnElLength)
		{
			this.token=token;
			this.indiceInicial=indiceInicial;
			this.indiceAContinuacion=indiceAContinuacion;
			this.terminaEnElLength=terminaEnElLength;
		}
		public string Token{
			get{return this.token;}
		}
		public double TokenInt{
			
			get{//cwl("this.token"+this.token); 
				return Utiles.dou(this.token);}
		}
		public int IndiceInicial{
			get{return this.indiceInicial;}
		}
		public int IndiceAContinuacion{
			get{return this.indiceAContinuacion;}
		}
		public bool TerminaEnElLength{
			get{return this.terminaEnElLength;}
		}

        public override string ToString()
        {
            return this.token;
        }
    }
}
