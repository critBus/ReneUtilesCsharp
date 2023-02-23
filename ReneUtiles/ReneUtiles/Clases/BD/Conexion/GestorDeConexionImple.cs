/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/4/2022
 * Hora: 12:21
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases;
using System.IO;
using System.Collections.Generic;
using System.Data;

using System.Data.Common;
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
		
		
		public virtual Object  _execute(String sql){
			datosDeConexion.R=null;
			//DbConnection conn=crearConexion();
			datosDeConexion.Conn=crearConexion();
			//DbCommand sqlite_cmd;//SQLiteCommand
			//es crearTabla
			datosDeConexion.Cmd = datosDeConexion.Conn.CreateCommand();
			datosDeConexion.Cmd.CommandText = sql;
			cwl("sql="+sql);
			if(this.sqlUtiles.esSelect(sql)){
				//cwl("sql="+sql);
				List<object[]> lo=new List<object[]>();
				//DbDataReader sqlite_datareader;
				datosDeConexion.Dr = datosDeConexion.Cmd.ExecuteReader();
				int cantidadDeColumnas=0;
				while (datosDeConexion.Dr.Read())
		         {
					cantidadDeColumnas=datosDeConexion.Dr.FieldCount;
					object[] o=new object[cantidadDeColumnas];
					
					datosDeConexion.Dr.GetValues(o);
					lo.Add(o);
					//sqlite_datareader.GetDataTypeName()
		            //string myreader = sqlite_datareader.GetString(0);
		            //Console.WriteLine(myreader);
		         }
				if(this.sqlUtiles.esSelectValor(sql)){
					if(lo.Count>0&&((Object[])lo[0]).Length>0){
						datosDeConexion.R=lo[0][0];
						if(esNumero(datosDeConexion.R+"")){
							datosDeConexion.R=dou(datosDeConexion.R+"");
						}
					}
					
				}else{
					datosDeConexion.R=lo.ToArray();
				}
				//cwl("datosBDConect.mostrarResultadoConsola="+datosBDConect.mostrarResultadoConsola);
				if(datosBDConect.mostrarResultadoConsola){
					_mostrarResultadoConsola();
				}
				
			}else{
				
				if(this.sqlUtiles.esInsertar(sql)){
                    //algo obtener el id
                    //					sqlite_cmd = conn.CreateCommand();
                    //					sqlite_cmd.CommandText = "SELECT LAST_INSERT_ID() as lastid";
                    //					int id=(int)sqlite_cmd.ExecuteScalar();
                    //					
                    //					
                    //					cwl("id="+id);
                    datosDeConexion.R = ejecutarConsultaInsertar();//getResultadoDeInsertar();

                }
                else{
                    datosDeConexion.Cmd.ExecuteNonQuery();

                }
			}
			datosDeConexion.Conn.Close();
			
			return datosDeConexion.R;
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
    }
}
