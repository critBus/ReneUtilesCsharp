/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/4/2022
 * Hora: 12:19
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.BD.Conexion
{
	/// <summary>
	/// Description of GestorDeConexion.
	/// </summary>
	public interface IGestorDeConexion
	{
		 Object _execute(String sql);
		 void _mostrarResultadoConsola();
	}
}
