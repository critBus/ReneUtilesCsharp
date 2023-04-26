/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 27/3/2022
 * Hora: 13:33
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
namespace ReneUtiles.Clases.BD.Factory.Consultas
{
	/// <summary>
	/// Description of OneToMany.
	/// </summary>
	public class OneToMany:BasicoFactory
{		
		public string Nombre;
		public bool TieneUnNombreAutomatico;
		public ModeloBD One;
		public ModeloBD Many;
		public ColumnaDeModeloBD LinkToOne;//lo tiene el many
        public ColumnaDeModeloBD LinkToMany;//lo tiene el many, generalmente el id, en ese caso null
        public OneToMany(ModeloBD one, ModeloBD many, ColumnaDeModeloBD linkToOne, string nombre)
            :this(one, many, linkToOne, nombre,null)
        {

        }

        public OneToMany(ModeloBD one,ModeloBD many
            ,ColumnaDeModeloBD linkToOne,string nombre
            , ColumnaDeModeloBD LinkToMany)
        {
			this.One=one;
			this.Many=many;
			this.LinkToOne=linkToOne;
			this.TieneUnNombreAutomatico=nombre==null;
			this.Nombre=nombre;
            this.LinkToMany = LinkToMany;

        }
		public OneToMany(ModeloBD one,ModeloBD many,ColumnaDeModeloBD linkToOne):this(one,many,linkToOne,null)
		{
			
		}
		
		public void comprobarSeguridad(){
			if(One==Many){
				throw new Exception("One no puede ser igual a Many");
			}
			if(!Many.contieneEstaColumna(LinkToOne)){
				throw new Exception("Many no contiene al link");
			}
		}
	}
}
