/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 3/10/2021
 * Time: 13:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ReneUtiles.Clases
{
	/// <summary>
	/// Description of TerminacionNumerica.
	/// </summary>
	public class TerminacionNumerica
	{
		public static readonly TerminacionNumerica[] TerminacionesNumericasBasicas={
			new TerminacionNumerica(1,"ra", "er","th"),
			new TerminacionNumerica(2,"da", "do", "nd","th"),
			new TerminacionNumerica(3,"ra", "ro", "rd","th"),
			new TerminacionNumerica(4,"ta", "to","th"),
			new TerminacionNumerica(5,"ta", "to","th"),
			new TerminacionNumerica(6,"ta", "to","th"),
			new TerminacionNumerica(7,"ta", "tima", "to", "timo","th"),
			new TerminacionNumerica(8,"ba", "va", "bo", "vo","th"),
			new TerminacionNumerica(9,"na", "no","th"),
			//new TerminacionNumerica(,),
			
		};
		
		public int Numero{get;set;}
		public string[] Terminaciones{get;set;}
		public TerminacionNumerica(int numero,params string[] terminaciones)
		{
			Numero=numero;
			Terminaciones=terminaciones;
		}
		
		
	}
}
