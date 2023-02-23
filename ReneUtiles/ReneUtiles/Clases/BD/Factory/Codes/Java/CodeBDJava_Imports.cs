/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 26/3/2022
 * Hora: 12:03
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.BD.Factory;
namespace ReneUtiles.Clases.BD.Factory.Codes.Java
{
	/// <summary>
	/// Description of CodeBDJava_Imports.
	/// </summary>
	public class CodeBDJava_Imports
	{
		public string ImportBDUpdates;
		public string ImportBDSesionStorage;
		public string ImportBDConexion;
		public string ImportTipoDeDatoSQL;
		public string ImportTipoDeClasificacionSQL;
		public string ImportTipoDeOrdenamientoSQL;
		public string ImportBasicoBD;
		public string ImportModeloDeApiBD;
		
		public string ImportList;
		public string ImportArrayList;
		public string ImportDate;
		public string ImportFile;
		public string ImportTime;
		
		public CodeBDJava_Imports()
		{
			this.ImportBDUpdates="Utiles.ClasesUtiles.BasesDeDatos.BDConexion";
			this.ImportBDSesionStorage="Utiles.ClasesUtiles.BasesDeDatos.BDSesionStorage";
			this.ImportBDConexion="Utiles.ClasesUtiles.BasesDeDatos.BDUpdates";
			this.ImportTipoDeDatoSQL="Utiles.ClasesUtiles.BasesDeDatos.TipoDeDatoSQL";
			this.ImportTipoDeClasificacionSQL="Utiles.ClasesUtiles.BasesDeDatos.TipoDeClasificacionSQL";
			this.ImportTipoDeOrdenamientoSQL="Utiles.ClasesUtiles.BasesDeDatos.TipoDeOrdenamientoSQL";
			this.ImportBasicoBD="Utiles.ClasesUtiles.BasesDeDatos.BasicoBD";
			this.ImportModeloDeApiBD="Utiles.ClasesUtiles.BasesDeDatos.ModeloDeApiBD";
			this.ImportList="java.util.List";
			this.ImportArrayList="java.util.ArrayList";
			this.ImportDate="java.util.Date";
			this.ImportFile="java.io.File";
			this.ImportTime="java.sql.Time";
		}
		
		protected string getImport(string a){
			return "\nimport "+a+";";
		}
		
		public virtual string getStr(){
			return getImport(ImportBDUpdates)
				+getImport(ImportBDSesionStorage)
				+getImport(ImportBDConexion)
				+getImport(ImportTipoDeDatoSQL)
				+getImport(ImportTipoDeClasificacionSQL)
				+getImport(ImportTipoDeOrdenamientoSQL)
				+getImport(ImportBasicoBD)
				+getImport(ImportModeloDeApiBD)
				
				+getImport(ImportList)
				+getImport(ImportArrayList)
				+getImport(ImportDate)
				+getImport(ImportFile)
				+getImport(ImportTime)
				
				;
		}
		
	}
}
