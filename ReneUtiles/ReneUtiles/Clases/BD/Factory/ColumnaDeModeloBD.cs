/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 11/12/2021
 * Time: 18:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using System.Linq;
using System.Security.AccessControl;
using System.Text;
//using System.Threading.Tasks;
using ReneUtiles.Clases.BD;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;

namespace ReneUtiles.Clases.BD.Factory
{
	/// <summary>
	/// Description of ColumnaDeModeloBD.
	/// </summary>
	public class ColumnaDeModeloBD:ElementoPorElQueBuscar,ElementoPorQueElOrdenar
	{	
		public string Nombre;
		public TipoDeDatoSQL Tipo;
		public int Tamaño;
		public TipoDeClasificacionSQL[] Clasificaciones;
		
		public bool BuscarModeloPorEstaColumna;
		public bool BuscarListaPorEstaColumna;
		public bool EliminarPorEstaColumna;
		
		public ModeloBD ReferenciaID;
		
		public object ValorDefault;
		
		public ModeloBD Padre;
        public ColumnaDeModeloBD() {
        }

        public ColumnaDeModeloBD(string nombre,TipoDeDatoSQL tipo,int tamaño,params TipoDeClasificacionSQL[] clasificaciones)
		{
			this.Nombre=nombre;
			if(tipo==null){
				tipo=TipoDeDatoSQL.VARCHAR;
			}
			this.Tipo=tipo;
			this.Tamaño=tamaño;
			this.Clasificaciones=clasificaciones;
			
		}

		public bool EsPrimaryKey{
			get { return Clasificaciones.Contains(TipoDeClasificacionSQL.PRIMARY_KEY)||Clasificaciones.Contains(TipoDeClasificacionSQL.PRIMARY_KEY_AUTOINCREMENT);}
		}
		
		public bool EsReferencia{
			get { return ReferenciaID!=null;}
		}
		
		public bool EsUnique{
			get { return Clasificaciones.Contains(TipoDeClasificacionSQL.UNIQUE);}
		}
	}
}
