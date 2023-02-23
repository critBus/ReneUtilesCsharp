/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 4/4/2022
 * Hora: 13:16
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.BD.Factory.Codes.Java; 
namespace ReneUtiles.Clases.BD.Factory.Codes.Java.Android
{
	/// <summary>
	/// Description of CodeBDAndroid.
	/// </summary>
	public class CodeBDAndroid:CodeBDJava 
	{
		public bool EsBDInterna;
		public CodeBDAndroid(FactoryBD factory, DatosDeConexionFactoryBD datosConexionFactory)
            : base(factory, datosConexionFactory)
        {
			this.EsBDInterna = true;
			this.importsJava = new CodeBDAndroid_Imports();
			//this.datosBDConect = new CodeBDAndroid_DatosDeBDConect();
			this.NombreClaseUtilidadesBD="BasicoBDAndroid";
		}
		
		
		protected override string __getStrLlamadaThis_DelConstructorBD_SinUrl(CodeBDJava code,int separacion0){
		string separacion = getSeparacionln(0, separacion0);
		return separacion+"this(context,null);";
		}
		protected override string __getStrArgumentos_DelConstructorBD_SinUrl(CodeBDJava code,int separacion0){
			return "Context context";
		}
		protected override string __getStrArgumentos_DelConstructorBD(CodeBDJava code, int separacion0)
		{
			if (code is CodeBDAndroid) {
				return "Context context,String url";
			}
			return base.__getStrArgumentos_DelConstructorBD(code, separacion0);
		}
		
		protected override string __getStrNewBDConexion_DelConstructorBD(CodeBDJava code, int separacion0)
		{
			if (code is CodeBDAndroid) {
				string separacion = getSeparacionln(0, separacion0);
				string separacion1 = getSeparacionln(1, separacion0);
				if (EsBDInterna) {
					return separacion + "this.BD =BDAndroid.getDB_SQLite(context,this.urlBD);";
				}
				string r = separacion + "File f= ArchivoAndroid.getFileInterno(this.urlBD);";
				r += separacion + "if(!f.exists()){";
				r += separacion1 + "BDAndroid.crearArchivoSQLite(context,f);";
				r += separacion + "}";
				r += separacion + "this.BD =BDAndroid.getDB_SQLite(context,f);";
				return r;
			}
			return base.__getStrNewBDConexion_DelConstructorBD(code, separacion0);
		}
		
		protected override string getNombreMetodoParse(TipoDeDatoSQL t)
		{
			if (t == TipoDeDatoSQL.DATE) {
				return "toJodaDateTime";
			}
			return base.getNombreMetodoParse(t);
		}
		public override string getNombreTipoDeDato(TipoDeDatoSQL t)
		{
			if (t == TipoDeDatoSQL.DATE) {
				return "DateTime";
			}
			return base.getNombreTipoDeDato(t);
		}
	}
}
