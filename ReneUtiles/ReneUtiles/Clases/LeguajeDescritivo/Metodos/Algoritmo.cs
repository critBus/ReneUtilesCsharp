/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/5/2022
 * Hora: 16:38
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ReneUtiles.Clases.LeguajeDescritivo.Metodos
{
	/// <summary>
	/// Description of Algoritmo.
	/// </summary>
	public abstract class Algoritmo
	{
		public List<Algoritmo> Antes;
		//public Algoritmo Actual;
		public List<Algoritmo> Despues;
		
		protected Algoritmo(){
			this.Antes=new List<Algoritmo>();
			this.Despues=new List<Algoritmo>();
		}
	}
}
