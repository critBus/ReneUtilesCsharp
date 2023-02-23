/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 17/10/2021
 * Time: 12:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ReneUtiles
{
	/// <summary>
	/// Description of Operaciones.
	/// </summary>
	public  abstract class Operaciones
	{
		public static string getSeparadorDecimal()
		{
			string sd = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
			return sd;
		}
	}
}
