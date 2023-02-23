/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 11/12/2021
 * Time: 20:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ReneUtiles.ReneUtiles.Clases.Tipos
{
	/// <summary>
	/// Description of TipoDeLenguaje.
	/// </summary>
	public class TipoDeLenguaje
	{	public static readonly TipoDeLenguaje
			CSHAR=new TipoDeLenguaje("C#"),
			JAVA=new TipoDeLenguaje("JAVA"),
			C_MAS_MAS=new TipoDeLenguaje("C_MAS_MAS"),
			C=new TipoDeLenguaje("C"),
			JAVASCRIPT=new TipoDeLenguaje("JAVASCRIPT"),
			PYTHON=new TipoDeLenguaje("PYTHON"),
			HTML=new TipoDeLenguaje("HTML"),
			XHTML=new TipoDeLenguaje("XHTML"),
			PHP=new TipoDeLenguaje("PHP"),
			TIPESCRIP=new TipoDeLenguaje("HTML"),
			CSS=new TipoDeLenguaje("CSS");
		public static readonly TipoDeLenguaje[] VALUES={CSHAR,JAVA,C,JAVASCRIPT,PYTHON,HTML,XHTML,PHP,TIPESCRIP,CSS};
		public readonly string Lenguaje;
		public TipoDeLenguaje(string lenguaje)
		{
			this.Lenguaje=lenguaje;
		}
		public static bool esTipoDeDatoSQl(Object e){
			return e.GetType()==CSS.GetType();
    }
	}
}
