/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 4/4/2022
 * Hora: 17:38
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using ReneUtiles.Clases.BD.Factory;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
namespace ReneUtiles.Clases.BD.Factory.Consultas
{
	/// <summary>
	/// Description of SelectWhereSort.
	/// </summary>
	public class SelectWhereSort:BasicoFactory
	{	public class ColumnaYOrden{public ColumnaDeModeloBD Columna;public TipoDeOrdenamientoSQL Ordenamiento;}
		public ModeloBD_ID Modelo;
		//public List<ElementoPorElQueBuscar> ListaPorLasQueBuscar;
		public List<ColumnaDeModeloBD> ListaPorLasQueBuscar;
		public List<ColumnaYOrden> ListaPorLasQueOrdenar;
		public SelectWhereSort(ModeloBD_ID modelo)
		{
			this.Modelo=modelo;
			this.ListaPorLasQueBuscar=new List<ColumnaDeModeloBD>();
			this.ListaPorLasQueOrdenar=new List<ColumnaYOrden>();
		}
	}
}
