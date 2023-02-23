/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 5/2/2022
 * Time: 20:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ReneUtiles.Clases.BD
{
	/// <summary>
	/// Description of TipoDeConexionBD.
	/// </summary>
	public class TipoDeConexionBD
	{
		public static readonly TipoDeConexionBD
			SQL_LITE = new TipoDeConexionBD("sqlite", "org.sqlite.JDBC", ".db", ".s3db")
			, 
			MY_SQL = new TipoDeConexionBD("mysql", "com.mysql.jdbc.Driver")
			, 
			POSTGRES = new TipoDeConexionBD("postgresql", "org.postgresql.Driver")
			, 
			ORACLE = new TipoDeConexionBD("oracle", "oracle.jdbc.driver.OracleDrive");
		public static readonly TipoDeConexionBD[] VALUES = {
			SQL_LITE,
			MY_SQL,
			POSTGRES,
			ORACLE
		};
		public readonly string url, driver_java;
		public readonly string[] extencionDeArchivo;
		public TipoDeConexionBD(string url, string driver_java, params string[] extencionDeArchivo)
		{
			this.url = url;
			this.driver_java = driver_java;
			this.extencionDeArchivo = extencionDeArchivo;
		}
		public static bool esTipoDeConexionBD(Object e)
		{
			return e.GetType() == SQL_LITE.GetType();
		}
		
	}
}
