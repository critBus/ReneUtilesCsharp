/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/4/2022
 * Hora: 12:28
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;
using ReneUtiles.Clases.BD;
using ReneUtiles;
namespace ReneUtiles.Clases.BD.Conexion
{
	/// <summary>
	/// Description of GestorDeConexioSQLite.
	/// </summary>
	public class GestorDeConexionSQLite:GestorDeConexionImple
	{

			public GestorDeConexionSQLite(DatosBDConect datosBDConect):base(datosBDConect,new SQLUtiles())
		{
			
			datosBDConect.url_basesDeDatos="Data Source="+datosBDConect.url+";Version=3;New=True;Compress=True;";
			datosBDConect.tipoDeConxion=TipoDeConexionBD.SQL_LITE;	
		}
		
		
		public override DbConnection crearConexion()
      {

         SQLiteConnection sqlite_conn;
         
         sqlite_conn = new SQLiteConnection(datosBDConect.url_basesDeDatos);
         
            sqlite_conn.Open();
         
         return sqlite_conn;
      }


        protected override BDResultadoInsertar ejecutarConsultaInsertar()
        {
            datosDeConexion.Cmd.ExecuteNonQuery();
            return getResultadoDeInsertar();
        }

        public BDResultadoInsertar getResultadoDeInsertar(){
            SQLiteConnection sqlite_conn=(SQLiteConnection)datosDeConexion.Conn;
			int id=(int)sqlite_conn.LastInsertRowId;
			BDResultadoInsertar r=new BDResultadoInsertar();
			r.add(TipoDeDatoSQL.INTEGER,id);
			return r;
		}
	}
}
