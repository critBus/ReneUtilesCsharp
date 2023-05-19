/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/4/2022
 * Hora: 12:21
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Linq;
using ReneUtiles.Clases;
using System.IO;
using System.Collections.Generic;
using System.Data;

using System.Data.Common;
using ReneUtiles.Clases.BD.Factory;
namespace ReneUtiles.Clases.BD.Conexion
{
	/// <summary>
	/// Description of GestorDeConexionImple.
	/// </summary>
	public abstract class GestorDeConexionImple:ConsolaBasica, IGestorDeConexion
	{
		protected class DatosDeConexion{
			public DbConnection Conn;
			public DbCommand Cmd;
			public DbDataReader Dr;
			public Object R;
			public DatosDeConexion(){}
		}
		
		protected DatosBDConect datosBDConect;
		protected DatosDeConexion datosDeConexion;
        public SQLUtiles sqlUtiles;

		public GestorDeConexionImple(DatosBDConect datosBDConect, SQLUtiles sqlUtiles)
		{
			this.datosBDConect = datosBDConect;
			this.datosDeConexion=new DatosDeConexion();
            this.sqlUtiles = sqlUtiles;
		}

		public DatosBDConect getDatosBDConect()
		{
			return datosBDConect;
		}

		public void setDatosBDConect(DatosBDConect datosBDConect)
		{
			this.datosBDConect = datosBDConect;
		}

        private readonly object lockObj = new object();

        public virtual Object  _execute(String sql){
            lock (lockObj)
            {
                // Código del método _execute aquí

                datosDeConexion.R = null;
                //DbConnection conn=crearConexion();
                datosDeConexion.Conn = crearConexion();

                //if (sql== "SELECT * FROM TABLA_DIRECCION_DE_PAQUETE WHERE \"COLUMNA_SECCION\" = \"ANIME\"") {
                //    cwl("vamos a ver");
                //}

                try
                {
                    //DbCommand sqlite_cmd;//SQLiteCommand
                    //es crearTabla
                    datosDeConexion.Cmd = datosDeConexion.Conn.CreateCommand();
                    datosDeConexion.Cmd.CommandText = sql;
                    if (this.datosBDConect.mostrarSQL)
                    {
                        cwl("sql=" + sql);
                    }

                    if (this.sqlUtiles.esSelect(sql))
                    {
                        //cwl("sql="+sql);
                        List<object[]> lo = new List<object[]>();
                        //DbDataReader sqlite_datareader;
                        try
                        {
                            datosDeConexion.Dr = datosDeConexion.Cmd.ExecuteReader();
                            int cantidadDeColumnas = 0;
                            while (datosDeConexion.Dr.Read())
                            {
                                cantidadDeColumnas = datosDeConexion.Dr.FieldCount;
                                object[] o = new object[cantidadDeColumnas];

                                try
                                {
                                    datosDeConexion.Dr.GetValues(o);
                                }
                                catch (System.InvalidOperationException ex)
                                {
                                    cwl("error exepcion 2");
                                    throw ex;
                                }
                                lo.Add(o);
                                //sqlite_datareader.GetDataTypeName()
                                //string myreader = sqlite_datareader.GetString(0);
                                //Console.WriteLine(myreader);
                            }

                        }
                        catch (System.InvalidOperationException ex)
                        {
                            cwl("error exepcion");
                            throw ex;
                        }

                        if (this.sqlUtiles.esSelectValor(sql))
                        {
                            if (lo.Count > 0 && ((Object[])lo[0]).Length > 0)
                            {
                                datosDeConexion.R = lo[0][0];
                                if (esNumero(datosDeConexion.R + ""))
                                {
                                    datosDeConexion.R = dou(datosDeConexion.R + "");
                                }
                            }

                        }
                        else
                        {
                            datosDeConexion.R = lo.ToArray();
                        }
                        //cwl("datosBDConect.mostrarResultadoConsola="+datosBDConect.mostrarResultadoConsola);
                        if (datosBDConect.mostrarResultadoConsola)
                        {
                            _mostrarResultadoConsola();
                        }

                    }
                    else
                    {

                        if (this.sqlUtiles.esInsertar(sql))
                        {
                            //algo obtener el id
                            //					sqlite_cmd = conn.CreateCommand();
                            //					sqlite_cmd.CommandText = "SELECT LAST_INSERT_ID() as lastid";
                            //					int id=(int)sqlite_cmd.ExecuteScalar();
                            //					
                            //					
                            //					cwl("id="+id);
                            datosDeConexion.R = ejecutarConsultaInsertar();//getResultadoDeInsertar();

                        }
                        else
                        {
                            datosDeConexion.Cmd.ExecuteNonQuery();

                        }
                    }
                    datosDeConexion.Conn.Close();

                }
                catch (Exception ex)
                {
                    datosDeConexion.Conn.Close();
                    throw ex;
                }



                return datosDeConexion.R;


            }


            
		}
		public virtual void _mostrarResultadoConsola(){
			Object o=datosDeConexion.R;
			if(o!=null){
				if(o is Object[][]){
					Object[][] O=(Object[][])o;
					cwl("La matris resultante es:");
					for (int i = 0; i < O.Length; i++) {
						Object[]  f=O[i];
						string r="[";
						for (int j = 0; j < f.Length; j++) {
							r+=" "+f[j];
						}
						r+=" ]";
						cwl(r);
					}
				}else{
					cwl("el resultado es: "+o.ToString());
				}
			}
		}
		
		
		public abstract DbConnection crearConexion();
        //public abstract BDResultadoInsertar getResultadoDeInsertar();
        protected abstract BDResultadoInsertar ejecutarConsultaInsertar();

        public int ejecutarConsultaGetInt(string sql) {
            datosDeConexion.R = null;
            int r = 1;
            //DbConnection conn=crearConexion();
            datosDeConexion.Conn = crearConexion();
            try {
                //DbCommand sqlite_cmd;//SQLiteCommand
                //es crearTabla
                datosDeConexion.Cmd = datosDeConexion.Conn.CreateCommand();
                datosDeConexion.Cmd.CommandText = sql;
                if (this.datosBDConect.mostrarSQL)
                {
                    cwl("sql=" + sql);
                }
                 r = (int)datosDeConexion.Cmd.ExecuteScalar();
                datosDeConexion.R = r;
                datosDeConexion.Conn.Close();

            }
            catch (Exception ex)
            {
                datosDeConexion.Conn.Close();
                throw ex;
            }
            
            return r;

        }

        private bool tieneAlgunaColumna(
            DataTable c
            ,DataRow column
            , params string[] columnas) {
            foreach (var nombreColumna in columnas)
            {
                if (c.Columns.Contains(nombreColumna)
                    &&(bool) column[nombreColumna]
                    ) {
                    return true;
                }
            }
            return false;
        }

        public List<ModeloBD> ObtenerTablasYColumnas()
        {
            

            List<ModeloBD> modelos = new List<ModeloBD>();
            DbConnection db = null;
            try {
                db = crearConexion();
                // Obtener información sobre las tablas de la base de datos
                DataTable tables = db.GetSchema("Tables");
                
                // Recorrer las tablas
                foreach (DataRow table in tables.Rows)
                {
                    // Obtener el nombre de la tabla
                    string tableName = (string)table["TABLE_NAME"];
                    if (tableName == "sqlite_sequence") continue;

                    // Crear una instancia de ModeloBD para representar la tabla
                    ModeloBD tabla = new ModeloBD(tableName);
                    

                    // Obtener información sobre las columnas de la tabla
                    DataTable columns = db.GetSchema("Columns"
                        , new string[] { null, null, tableName });

                    // Recorrer las columnas
                    foreach (DataRow column in columns.Rows)
                    {
                        
                        // Obtener el nombre y tipo de la columna
                        string columnName = (string)column["COLUMN_NAME"];
                        string dataTypeName = (string)column["DATA_TYPE"];
                        int? size = column["CHARACTER_MAXIMUM_LENGTH"] as int?;

                        //cwl("dataTypeName="+ dataTypeName);

                        // Crear una instancia de TipoDeDatoSQL para representar el tipo de la columna
                        TipoDeDatoSQL dataType = TipoDeDatoSQL.get(dataTypeName.ToUpper())?? TipoDeDatoSQL.VARCHAR;
                        

                        // Crear una instancia de ColumnaDeModeloBD para representar la columna
                        ColumnaDeModeloBD columna = new ColumnaDeModeloBD();
                        columna.Nombre = columnName;
                        columna.Tipo = dataType;
                        columna.Tamaño = size.HasValue ? size.Value : 0;

                        // Obtener las clasificaciones de la columna
                        List<TipoDeClasificacionSQL> clasificaciones = new List<TipoDeClasificacionSQL>();
                        
                        if (tieneAlgunaColumna(columns, column, "IS_NULLABLE"))
                        {
                            clasificaciones.Add(TipoDeClasificacionSQL.NULLABLE);
                        }
                        else
                        {
                            clasificaciones.Add(TipoDeClasificacionSQL.NOT_NULL);
                        }
                        if (tieneAlgunaColumna(columns, column, "PRIMARY_KEY"))
                        {
                            if (
                                tieneAlgunaColumna(columns, column
                                , "IS_AUTOINCREMENT"
                                ,"AUTOINCREMENT"
                                ,"AUTO_INCREMENT")
                                
                                )
                            {
                                clasificaciones.Add(TipoDeClasificacionSQL.PRIMARY_KEY_AUTOINCREMENT);
                            }
                            else
                            {
                                clasificaciones.Add(TipoDeClasificacionSQL.PRIMARY_KEY);
                            }
                        }
                        
                        if (tieneAlgunaColumna(columns, column, "UNIQUE"))
                        {
                            clasificaciones.Add(TipoDeClasificacionSQL.UNIQUE);
                        }
                        columna.Clasificaciones = clasificaciones.ToArray();

                        // Agregar la columna a la lista de columnas de la tabla
                        tabla.Columnas.Add(columna);
                    }
                    modelos.Add(tabla);



                }


                foreach(ModeloBD modelo in modelos)
        {
                    foreach (ColumnaDeModeloBD columna in modelo.Columnas)
                    {
                        string columnName = columna.Nombre;
                        // Buscar referencias a otras tablas
                        if (columnName.StartsWith("COLUMNA_ID_"))
                        {
                            // Obtener el nombre dela tabla referenciada
                            string referencedTableName = columnName.Substring(11);

                            // Buscar el modelo de base de datos correspondiente
                            ModeloBD referencedTable = modelos.FirstOrDefault(m => m.Nombre == referencedTableName);

                            // Si se encontró el modelo, establecer la referencia
                            if (referencedTable != null)
                            {
                                columna.ReferenciaID = referencedTable;
                            }
                        }
                        //--------
                        //if (columna.ReferenciaID != null)
                        //{
                        //    columna.ReferenciaID = modelos.FirstOrDefault(m => m.Nombre == columna.ReferenciaID.Nombre);
                        //    if (columna.ReferenciaID!=null) {
                        //        cwl("hay referencias");
                        //    }
                        //}
                        //-----------
                    }


                    // Hacer lo que necesites con la tabla
                    Console.WriteLine("Tabla: {0}", modelo.Nombre);
                    foreach (ColumnaDeModeloBD columna in modelo.Columnas)
                    {
                        Console.WriteLine("Columna: {0} ({1})", columna.Nombre, columna.Tipo.Valor);
                        Console.WriteLine("  Tamaño: {0}", columna.Tamaño);
                        foreach (TipoDeClasificacionSQL clasificacion in columna.Clasificaciones)
                        {
                            Console.WriteLine("  Clasificación: {0}", clasificacion.Valor);
                        }
                        if (columna.EsReferencia) {
                            Console.WriteLine("  Clasificación: "+ columna.ReferenciaID.Nombre);
                        }
                    }
                    cwl("*******************************************");
                }

            } finally {
                if (db!=null) {
                    db.Close();
                }
            }//catch (Exception ex) { throw ex; }

            

            

            return modelos;
        }


    }




}
