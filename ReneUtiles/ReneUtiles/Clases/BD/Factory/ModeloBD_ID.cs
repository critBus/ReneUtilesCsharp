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
		public ModeloBD_ID(string nombre,params ColumnaDeModeloBD[] columnas):this(nombre,false,columnas){
			
		}
		public ModeloBD_ID(string nombre,bool suscritaAUpdates,params ColumnaDeModeloBD[] columnas):base(nombre,suscritaAUpdates,columnas)
		{
			//addC(nombre:"id",tipo:);
		}
	}
}
