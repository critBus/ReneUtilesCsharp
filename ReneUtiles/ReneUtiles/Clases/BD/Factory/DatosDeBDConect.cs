/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 26/3/2022
 * Hora: 12:39
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.BD.Factory
{
	/// <summary>
	/// Description of DatosDeBDConect.
	/// </summary>
	public class DatosDeBDConect
	{	
		public class DatosDeSesionStorage{
			public string NombreMetodoCrearTablaYBorrarSiExiste;
			public DatosDeSesionStorage(){
				this.NombreMetodoCrearTablaYBorrarSiExiste="createTablas";
			}
		}

        //public string NombreMetodoInsertar;
        public string NombreMetodoDesactivarConsola;
        public string NombreMetodoGetIdCorrespondiente;

        public string NombreMetodoInsertarConIdAutomatico;


        public string NombreMetodoInsertarSinIdAutomatico;
		public string NombreMetodoCrearTablaYBorrarSiExiste;
		public string NombreMetodoCrearTablaSiNoExiste;
		public string NombreMetodoGetForId;
		public string NombreMetodoSelectTodo;
		public string NombreMetodoUpdateForId;
		public string NombreMetodoDeleteForId;
		public string NombreMetodoSelectWhere;
		public string NombreMetodoSelectWhereFirstRow;
		public string NombreMetodoDelete;
		public string NombreMetodoSelectWhereInnerJoin;
		public string NombreMetodoSelectWhereInnerJoinFirstRow;
		public string NombreMetodoGetConexionSQL_LITE;
        public string NombreMetodoGetConexionPOSTGRES;
        public string NombreMetodoExiste;
		public string NombreMetodoSelectWhereOrderBy;
		
		public DatosDeBDConect.DatosDeSesionStorage SesionStorage;
		
		public DatosDeBDConect()
		{
            this.NombreMetodoDesactivarConsola = "no_cl";
            this.NombreMetodoGetIdCorrespondiente = "getIdCorrespondiente";
            this.NombreMetodoGetConexionPOSTGRES = "getConexion_POSTGRES";
            this.NombreMetodoInsertarConIdAutomatico = "insertar_ConIdAutomatico";
            //this.NombreMetodoInsertar="insertar";
            this.NombreMetodoInsertarSinIdAutomatico="insertar_SinIdAutomatico";
			this.NombreMetodoCrearTablaYBorrarSiExiste="crearTablaYBorrarSiExiste";
			this.NombreMetodoCrearTablaSiNoExiste="crearTablaSiNoExiste";
			this.NombreMetodoGetForId="select_forID";
			this.NombreMetodoSelectTodo="select_Todo";
			this.NombreMetodoUpdateForId="update_Id";
			this.NombreMetodoDeleteForId="delete_id";
			this.NombreMetodoSelectWhere="select_Where";
			this.NombreMetodoSelectWhereFirstRow="select_Where_FirstRow";
			this.NombreMetodoDelete="delete";
			this.NombreMetodoSelectWhereInnerJoin="select_Where_Inner_Join_TodoDeTabla";
			this.NombreMetodoSelectWhereInnerJoinFirstRow="select_Where_Inner_Join_TodoDeTabla_FirstRow";
			this.NombreMetodoGetConexionSQL_LITE="getConexionSQL_LITE";
			this.NombreMetodoExiste="existe";
			this.NombreMetodoSelectWhereOrderBy="select_Where_ORDER_BY";
			
			this.SesionStorage= new DatosDeSesionStorage();
													   
		}
		
		
		
		
	}
}
