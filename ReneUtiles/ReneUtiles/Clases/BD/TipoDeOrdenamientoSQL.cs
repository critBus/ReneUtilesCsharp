/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 3/4/2022
 * Hora: 19:01
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
namespace ReneUtiles.Clases.BD
{
	/// <summary>
	/// Description of TipoDeOrdenamientoSQL.
	/// </summary>
	public class TipoDeOrdenamientoSQL:ElementoPorQueElOrdenar
	{	
		public static readonly TipoDeOrdenamientoSQL 
			ASC=new TipoDeOrdenamientoSQL("ASC"),
		DESC=new TipoDeOrdenamientoSQL("DESC");
		public static readonly TipoDeOrdenamientoSQL[] VALUES={ASC,DESC};
		
		public readonly string valor;
		public TipoDeOrdenamientoSQL(string valor)
		{
			this.valor=valor;
		}
		public string Valor{
			get{return this.valor;}
		}
		public string getValor(){
			return this.valor;
		}
		
	    public static bool esTipoDeOrdenamientoSQL(Object a){
			return a is TipoDeOrdenamientoSQL;
	    }
	}
}
