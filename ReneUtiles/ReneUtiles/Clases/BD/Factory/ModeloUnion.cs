/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 27/3/2022
 * Hora: 19:07
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.BD.Factory
{
	/// <summary>
	/// Description of ModeloUnion.
	/// </summary>
	public class ModeloUnion:ModeloBD_ID
	{	public bool TieneUnNombreAutomatico;
		public ModeloUnion(string nombre,params ColumnaDeModeloBD[] columnas):base(nombre,columnas)
		{
			this.TieneUnNombreAutomatico=false;
		}
	}
}
