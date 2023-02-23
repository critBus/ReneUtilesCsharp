/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/5/2022
 * Hora: 15:30
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.Condicionales 
{
	/// <summary>
	/// Description of NOT_Condicional.
	/// </summary>
	public class NOT_Condicional:Condicional
	{
		public Condicional Condicion;  
		public NOT_Condicional(Condicional condicion)
		{
			this.Condicion=condicion;
		}
		
	}
}
