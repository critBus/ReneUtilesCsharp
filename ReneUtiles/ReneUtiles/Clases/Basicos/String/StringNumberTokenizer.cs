/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 9/10/2021
 * Time: 16:15
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
	/// Description of StringNumberTokenizer.
	/// </summary>
	public class StringNumberTokenizer:StringTokenizer
	{
		public StringNumberTokenizer(string texto):base(texto)
		{
			init();
		}
		public StringNumberTokenizer(string texto,int i0, bool saltarContenedores):base(texto,i0,saltarContenedores)
		{
			init();
		}
		private void init(){
			//this.condicionDeSalto=Char.IsNumber;
			this.condicionDeSalto=c=>!Char.IsNumber(c);
		}
		
		
		
		
	}
}
