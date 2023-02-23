/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/5/2022
 * Hora: 15:26
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.Condicionales
{
	/// <summary>
	/// Description of IF_Condicional.
	/// </summary>
	public class IF_Condicional:Condicional
	{
		public Condicional Condicion; 
		public IF_Condicional(Condicional condicion)
		{
			this.Condicion=condicion;
		}
	}
}
