/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 5/4/2022
 * Hora: 09:36
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
	/// Description of OneToManySort.
	/// </summary>
	public class OneToManySort:OneToMany
	{
		public SelectWhereSort Sort;
		public OneToManySort(ModeloBD one,ModeloBD many,ColumnaDeModeloBD linkToOne,List<SelectWhereSort.ColumnaYOrden> listaPorLasQueOrdenar):base(one,many,linkToOne)
		{
			Sort=new SelectWhereSort((ModeloBD_ID)many);
			Sort.ListaPorLasQueOrdenar=listaPorLasQueOrdenar;
			Sort.ListaPorLasQueBuscar=list(linkToOne);
			
		}
	}
}
