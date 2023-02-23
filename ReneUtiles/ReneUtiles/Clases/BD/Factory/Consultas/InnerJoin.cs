/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 29/3/2022
 * Hora: 16:08
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
namespace ReneUtiles.Clases.BD.Factory.Consultas
{
	/// <summary>
	/// Description of InnerJoin.
	/// </summary>
	public class InnerJoin:BasicoFactory
	{
		public ModeloBD_ID ModeloDestino;
		public List<ElementoPorElQueBuscar> Cadena;
		public List<ElementoPorElQueBuscar> ElementosWhere;
		public InnerJoin(ModeloBD_ID modeloDestino,List<ElementoPorElQueBuscar> cadena,List<ElementoPorElQueBuscar> elementosWhere)
		{
			this.ModeloDestino=modeloDestino;
			this.Cadena=cadena;
			this.ElementosWhere=elementosWhere;
		}
	}
}
