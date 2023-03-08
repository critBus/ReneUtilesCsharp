/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 29/3/2022
 * Hora: 12:27
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
namespace ReneUtiles.Clases.BD.Factory.Consultas
{
	/// <summary>
	/// Description of ManyToMany.
	/// </summary>
	public class ManyToMany:BasicoFactory
	{	
		public ModeloBD_ID Many_1;
		public ModeloBD_ID Many_2;
		public ModeloUnion Union;
		public ManyToMany(ModeloBD_ID many_1, ModeloBD_ID many_2, ModeloUnion union)
		{
			this.Many_1 = many_1;
			this.Many_2 = many_2;
			this.Union = union;
		}
        public ManyToMany setIdStr(string idStr) {
            this.Union.IdKeyDefault = idStr;
            return this;
        }

        public void comprobarSeguridad(){
			if(Many_1==Many_2){
				throw new Exception("Los Many no pueden ser iguales");
			}
			if(Many_1==Union||Many_2==Union){
				throw new Exception("Los Many no pueden ser iguale a Union");
			}
		}

        public static ModeloUnion crearUnion(ModeloBD_ID many_1, ModeloBD_ID many_2, string nombre) {
            return ManyToMany.crearUnion(many_1,null,many_2,null,nombre);
        }

        public static ModeloUnion crearUnion(ModeloBD_ID many_1, string nombreColumnaMany_1, ModeloBD_ID many_2, string nombreColumnaMany_2, string nombre)
		{
			string nombreAutomatico=null;
			if (nombre == null) {
				metodoCreador1<String,String> modificarNombre = a => a.Replace("TABLA_","");
				 nombreAutomatico= "TABLA_" + modificarNombre(many_1.Nombre) + "_AND_" + modificarNombre(many_2.Nombre);
			}
			ModeloUnion union = new ModeloUnion(nombre ?? nombreAutomatico);
            if (nombreColumnaMany_1 == null)
            {
                union.addC_ID(many_1);
            }
            else {
                union.addC_ID(nombreColumnaMany_1,many_1);
            }

            if (nombreColumnaMany_2 == null)
            {
                union.addC_ID(many_2);
            }
            else
            {
                union.addC_ID(nombreColumnaMany_2, many_2);
            }

            //union.addC_ID(many_2);
			union.addBuscarListaPor(many_1);
			union.addBuscarListaPor(many_2);
			if (nombre == null) {
				union.TieneUnNombreAutomatico = true;
			}
			//one.addGetListaDe(union);
			return union;
		}
		public static ModeloUnion crearUnion(ModeloBD_ID many_1, ModeloBD_ID many_2)
		{
			return ManyToMany.crearUnion(many_1, many_2, null);
			
		}
	}
}
