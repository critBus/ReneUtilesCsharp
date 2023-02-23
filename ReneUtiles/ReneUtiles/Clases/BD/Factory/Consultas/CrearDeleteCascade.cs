/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/3/2022
 * Hora: 14:44
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
	/// Description of CrearDeleteCascade.
	/// </summary>
	public class CrearDeleteCascade
	{
//		public class ParModeloColumna{
//			public ModeloBD Modelo;
//			public ColumnaDeModeloBD Columna;
//			public ParModeloColumna(ModeloBD modelo,ColumnaDeModeloBD columna){
//					this.Modelo=modelo;
//					this.Columna=columna;
//			}
//		}
		public ModeloBD Modelo;
		public bool NecesitaDeleteCascade;
		public List<ColumnaDeModeloBD> ListaCascade;
		public CrearDeleteCascade(ModeloBD modelo)
		{
			this.Modelo=modelo;
			this.NecesitaDeleteCascade=false;
			this.ListaCascade=new List<ColumnaDeModeloBD>();
			
		}
	}
}
