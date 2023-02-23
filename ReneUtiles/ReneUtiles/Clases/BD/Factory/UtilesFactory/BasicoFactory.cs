/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 29/3/2022
 * Hora: 11:29
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

using ReneUtiles.Clases;
using System.Collections.Generic;

namespace ReneUtiles.Clases.BD.Factory.UtilesFactory
{
	/// <summary>
	/// Description of BasicoFactory.
	/// </summary>
	public class BasicoFactory:ConsolaBasica
	{
		public static List<ElementoPorElQueBuscar> listE(params ElementoPorElQueBuscar[] L){
			return list<ElementoPorElQueBuscar>(L);
		}
		public static List<ColumnaDeModeloBD> listC(params ColumnaDeModeloBD[] L){
			return list<ColumnaDeModeloBD>(L);
		}
	}
}
