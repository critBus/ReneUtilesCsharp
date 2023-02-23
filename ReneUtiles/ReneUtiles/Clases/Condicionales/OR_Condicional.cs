/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/5/2022
 * Hora: 15:32
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.Condicionales
{
	/// <summary>
	/// Description of OR_Condicional.
	/// </summary>
	public class OR_Condicional:Condicional
	{
		public Condicional Condicion1,Condicion2; 
		public OR_Condicional(Condicional condicion1,Condicional condicion2)
		{
			this.Condicion1=condicion1;
			this.Condicion2=condicion2;
		}
		
	}
}
