﻿/*
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
using ReneUtiles.Clases.BD.Factory;




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

        public static readonly string TABLE_NAME = "TABLE_NAME";


}
}



//public  List<string> getTablesNames() {
//    List<string> list = new List<string>();
//    DbConnection db =crearConexion();
//    try {


//        DataTable table_schema = db.GetSchema("Tables");

//        if (table_schema.Columns.Count > 0)
//        {
//            foreach (DataRow row in table_schema.Rows)
//            {
//                foreach (DataColumn col in table_schema.Columns)
//                {
//                    if (col.ColumnName == TABLE_NAME)
//                    {
//                        if (!row[col].ToString().Contains("sqlite_sequence"))
//                        {
//                            list.Add(row[col].ToString());
//                        }
//                    }
//                }
//            }
//        }
//    } catch (Exception ex) { } finally {
//        if (db!=null) {
//            db.Close();
//        }
//    }



//    return list;
//}
//public List<ColumnaDeModeloBD> getTableColumns(string table_name) {

//    List<ColumnaDeModeloBD> list = new List<ColumnaDeModeloBD>();

//    List<string> tables_name = getTablesNames();

//    DbConnection db = crearConexion();
//    DataTable table_schema = db.GetSchema("Tables");

//    return list;
//}
