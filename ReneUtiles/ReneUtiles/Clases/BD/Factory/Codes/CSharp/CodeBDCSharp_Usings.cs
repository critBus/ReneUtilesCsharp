/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 10/4/2022
 * Hora: 12:38
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.BD.Factory.Codes.CSharp
{
	/// <summary>
	/// Description of CodeBDCSharp_Usings.
	/// </summary>
	public class CodeBDCSharp_Usings
	{	public string UsingBD;
		public string UsingReneUtiles;
		public string UsingReneUtilesClases;
		
		public string UsingSystem;
		public string UsingIO;
		public string UsingGeneric;
		public CodeBDCSharp_Usings()
		{
			this.UsingBD="ReneUtiles.Clases.BD";
			this.UsingReneUtiles="ReneUtiles";
			this.UsingReneUtilesClases="ReneUtiles.Clases";
			
			this.UsingSystem="System";
			this.UsingIO="System.IO";
			this.UsingGeneric="System.Collections.Generic";
		}
		
		protected string getUsing(string a){
			return "\nusing "+a+";";
		}
		public virtual string getStr(){
			return getUsing(UsingBD)
				+getUsing(UsingReneUtiles)
				+getUsing(UsingReneUtilesClases)
				+getUsing(UsingSystem)
				+getUsing(UsingIO)
				+getUsing(UsingGeneric)
				
				;
		}
	}
}
