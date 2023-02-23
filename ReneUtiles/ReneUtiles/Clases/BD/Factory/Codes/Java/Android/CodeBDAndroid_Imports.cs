/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 4/4/2022
 * Hora: 12:46
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.BD.Factory.Codes.Java;
namespace ReneUtiles.Clases.BD.Factory.Codes.Java.Android
{
	/// <summary>
	/// Description of CodeBDAndroid_Imports.
	/// </summary>
	public class CodeBDAndroid_Imports:CodeBDJava_Imports 
	{
		public string ImportBDAndroid;//com.rene.android.reneandroid
		public string ImportContext;
		public string ImportJodaDateTime;
		public string ImportBasicoBDAndroid;
		public string ImportArchivoAndroid;
		public CodeBDAndroid_Imports():base()
		{
			string paqueteBD="com.rene.android.reneandroid.Utiles.ClasesUtiles.BasesDeDatos.";
			this.ImportBDUpdates=paqueteBD+"BDConexion";
			this.ImportBDSesionStorage=paqueteBD+"BDSesionStorage";
			this.ImportBDConexion=paqueteBD+"BDUpdates";
			this.ImportTipoDeDatoSQL=paqueteBD+"TipoDeDatoSQL";
			this.ImportTipoDeClasificacionSQL=paqueteBD+"TipoDeClasificacionSQL";
			this.ImportTipoDeOrdenamientoSQL=paqueteBD+"TipoDeOrdenamientoSQL";
			this.ImportBasicoBD=paqueteBD+"BasicoBD";
			this.ImportModeloDeApiBD=paqueteBD+"ModeloDeApiBD";
			this.ImportBDAndroid="com.rene.android.reneandroid.BDAndroid";
			this.ImportContext="android.content.Context";
			this.ImportJodaDateTime="org.joda.time.DateTime";
			this.ImportBasicoBDAndroid="com.rene.android.reneandroid.Clases.BD.BasicoBDAndroid";
			this.ImportJodaDateTime="org.joda.time.DateTime";
			this.ImportArchivoAndroid="com.rene.android.reneandroid.ArchivoAndroid";
		}
		public override string getStr(){
			return base.getStr()
				+getImport(this.ImportBDAndroid)
				+getImport(this.ImportContext)
				+getImport(this.ImportJodaDateTime)
				+getImport(this.ImportBasicoBDAndroid)
				+getImport(this.ImportArchivoAndroid)
				;
		}
	}
}
