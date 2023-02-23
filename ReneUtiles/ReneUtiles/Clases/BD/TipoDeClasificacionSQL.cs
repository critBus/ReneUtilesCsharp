/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 23/9/2021
 * Time: 18:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ReneUtiles.Clases.BD
{
	/// <summary>
	/// Description of TipoDeClasificacionSQL.
	/// </summary>
	public class TipoDeClasificacionSQL
	{
		public static readonly TipoDeClasificacionSQL 
			PRIMARY_KEY=new TipoDeClasificacionSQL("PRIMARY KEY"),
			PRIMARY_KEY_AUTOINCREMENT=new TipoDeClasificacionSQL("PRIMARY KEY AUTOINCREMENT"),
			UNIQUE=new TipoDeClasificacionSQL("UNIQUE"),
			NULLABLE=new TipoDeClasificacionSQL("NULLABLE"),
			NOT_NULL=new TipoDeClasificacionSQL("NOT NULL");
		public static readonly TipoDeClasificacionSQL[] VALUES={PRIMARY_KEY,PRIMARY_KEY_AUTOINCREMENT,UNIQUE,NULLABLE,NOT_NULL};
		public readonly string valor;
		public TipoDeClasificacionSQL(string valor)
		{
			this.valor=valor;
		}
		public string Valor{
			get{return this.valor;}
		}
		public string getValor(){
			return this.valor;
		}
		public  bool esLlave(){
        return this==PRIMARY_KEY||this==PRIMARY_KEY_AUTOINCREMENT;
	    }
	    public static bool esTipoDeClasificacionSQL(Object a){
			//return a.GetType()==PRIMARY_KEY.GetType();
			return a is TipoDeClasificacionSQL;
	    }
	}
}
