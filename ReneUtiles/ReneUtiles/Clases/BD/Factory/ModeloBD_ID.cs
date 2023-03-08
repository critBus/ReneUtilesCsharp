/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 11/12/2021
 * Time: 20:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
//using System.Threading.Tasks;

namespace ReneUtiles.Clases.BD.Factory
{
	/// <summary>
	/// Description of ModeloBD_ID.
	/// </summary>
	public class ModeloBD_ID:ModeloBD
	{
        //private string idKeyDefault = "id";
        public bool tieneNombrePersonalizado_idKeyDefault = false;

        public ColumnaDeModeloBD columnaID;

        

        public ModeloBD_ID(string nombre,params ColumnaDeModeloBD[] columnas):this(nombre,false,columnas){
            inicializar();

        }
		public ModeloBD_ID(string nombre,bool suscritaAUpdates,params ColumnaDeModeloBD[] columnas):base(nombre,suscritaAUpdates,columnas)
		{
            //addC(nombre:"id",tipo:);
            inicializar();
        }

        public void inicializar() {
            this.columnaID = new ColumnaDeModeloBD(
                nombre:"id"
                ,tipo:TipoDeDatoSQL.INTEGER
                ,tamaño:-1
                ,clasificaciones: new TipoDeClasificacionSQL[] {
                    TipoDeClasificacionSQL.PRIMARY_KEY_AUTOINCREMENT
                }
                );
        }

        public string IdKeyDefault
        {
            get
            {
                return this.columnaID.Nombre;
            }

            set
            {
                
                tieneNombrePersonalizado_idKeyDefault = true;
                this.columnaID.Nombre = value;
            }
        }
    }
}
