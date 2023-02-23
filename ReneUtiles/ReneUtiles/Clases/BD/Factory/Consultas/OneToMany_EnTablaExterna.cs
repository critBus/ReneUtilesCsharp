/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 27/3/2022
 * Hora: 18:02
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles;

namespace ReneUtiles.Clases.BD.Factory.Consultas
{
	/// <summary>
	/// Description of OneToMany_EnTablaExterna.
	/// </summary>
	public class OneToMany_EnTablaExterna
	{
		public ModeloBD One;
		public ModeloBD Many;
		public ModeloUnion Union;
		public OneToMany_EnTablaExterna(ModeloBD one, ModeloBD many, ModeloUnion union)
		{
			this.One = one;
			this.Many = many;
			this.Union = union;
		}
		public void comprobarSeguridad(){
			if(One==Many){
				throw new Exception("One no puede ser igual a Many");
			}
			if(One==Union||Many==Union){
				throw new Exception(" Many y One no pueden ser iguale a Union");
			}
		}
		public static ModeloUnion crearUnion(ModeloBD_ID one, ModeloBD_ID many, string nombre)
		{
			string nombreAutomatico=null;
			if (nombre == null) {
				metodoCreador1<String,String> modificarNombre = a => a.Replace("TABLA_","");
				 nombreAutomatico= "TABLA_ONE_" + modificarNombre(one.Nombre) + "_MANY_" + modificarNombre(many.Nombre);
			}
			ModeloUnion union = new ModeloUnion(nombre ?? nombreAutomatico);
			union.addC_ID(one);
			union.addC_ID(many);
			union.addBuscarListaPor(one);
			if (nombre == null) {
				union.TieneUnNombreAutomatico = true;
			}
			//one.addGetListaDe(union);
			return union;
		}
		
		public static ModeloUnion crearUnion(ModeloBD_ID one, ModeloBD_ID many)
		{
			return OneToMany_EnTablaExterna.crearUnion(one, many, null);
			
		}
	}
}
