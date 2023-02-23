/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 23/9/2021
 * Time: 19:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ReneUtiles.Clases.BD
{
	/// <summary>
	/// Description of TipoDeDatoSQL.
	/// </summary>
	public class TipoDeDatoSQL
	{
		public static readonly TipoDeDatoSQL
			INTEGER = new TipoDeDatoSQL("INTEGER", "0"),
			VARCHAR = new TipoDeDatoSQL("VARCHAR", ""),
			REAL = new TipoDeDatoSQL("REAL", "0"),
			DATE = new TipoDeDatoSQL("DATE", null),
			TIME = new TipoDeDatoSQL("TIME", null),
			POINT = new TipoDeDatoSQL("POINT", "(0,0)"),
			BLOB = new TipoDeDatoSQL("BLOB", null),
			BOOLEAN = new TipoDeDatoSQL("BOOLEAN", "false"),
			DOUBLE_PRECISION = new TipoDeDatoSQL("DOUBLE PRECISION", "0")
            , SERIAL = new TipoDeDatoSQL("SERIAL", null)
            ;
		public static readonly TipoDeDatoSQL[] VALUES = {
			INTEGER,
			VARCHAR,
			REAL,
			DATE,
			POINT,
			BLOB,
			BOOLEAN,
            SERIAL
		};
		public readonly string valor;
		public readonly string porDefecto;
		public TipoDeDatoSQL(string valor, string porDefecto)
		{
			this.valor = valor;
			this.porDefecto = porDefecto;
		}
		public string Valor {
			get{ return this.valor; }
		}
		public string getValor()
		{
			return this.valor;
		}
		public string getPorDefecto()
		{
			return this.porDefecto;
		}
		public override string ToString()
		{
			return this.valor;
		}

		//public string toStr
		public static bool esTipoDeDatoSQL(object e)
		{
			//return e.GetType()==INTEGER.GetType();
			return e is TipoDeDatoSQL;
		}
		
		public static TipoDeDatoSQL get(object tipo){
			if(tipo==null){
				return null;
			}
			foreach (TipoDeDatoSQL t in VALUES) {
				if(t.valor==tipo.ToString()){
					return t;
				}
			}
			return null;
		}
		
		public static TipoDeDatoSQL getTipoDeDatoSQL(object a){
			if(a is string){
				return TipoDeDatoSQL.VARCHAR;
			}
			if(a is double){
				return TipoDeDatoSQL.DOUBLE_PRECISION;
			}
			if(a is int){
				return TipoDeDatoSQL.INTEGER;
			}
			if(a is bool){
				return TipoDeDatoSQL.BOOLEAN;
			}
			
			if(a is DateTime||a is DateTime?){
				return TipoDeDatoSQL.DATE;
			}
			return null;
		}
	}
}
