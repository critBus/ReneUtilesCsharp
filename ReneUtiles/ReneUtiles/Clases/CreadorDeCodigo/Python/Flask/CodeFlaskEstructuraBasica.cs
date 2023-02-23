/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 25/5/2022
 * Hora: 14:11
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.CreadorDeCodigo.Python.Flask
{
	/// <summary>
	/// Description of CodeFlaskEstructuraBasica.
	/// </summary>
	public class CodeFlaskEstructuraBasica
	{
		public FactoryEstructuraFlask Factory;
		public CodeFlaskEstructuraBasica(FactoryEstructuraFlask factory)
		{
			this.Factory=factory;
		}
		public string getContenidoArchivo_Ext(int separacion0){
			string separacion = getSeparacionln(0,separacion0);
			string separacion1 = getSeparacionln(1,separacion0);
			string mr ="";
			mr+=separacion+"from flask_marshmallow import Marshmallow";
			mr+=separacion+"from flask_migrate import Migrate";
			mr+=separacion+Factory.nb.getNombreVariableMarshmallow()+"=Marshmallow()";
			mr+=separacion+Factory.nb.getNombreVariableMigrate()+"=Migrate()";
			return mr;
		}
		public string getContenidoArchivo_DB(int separacion0){
			string separacion = getSeparacionln(0,separacion0);
			string separacion1 = getSeparacionln(1,separacion0);
			string separacion2 = getSeparacionln(2,separacion0);
			string mr ="";
			mr+=separacion+"from flask_sqlalchemy import SQLAlchemy";
			
			string db=Factory.nb.getNombreVariableDB();
			mr+=separacion+db+"=SQLAlchemy()";
			
			mr+=separacion+"class "+Factory.nb.getNombreClaseInterfasModelo()+":";
			mr+=separacion1+"def "+Factory.nb.getNombreMetodoSave()+"(self):";
			mr+=separacion2+db+".session.add(self)";
			mr+=separacion2+db+".session.commit()";
			mr+=separacion1+"def "+Factory.nb.getNombreMetodoDelete()+"(self):";
			mr+=separacion2+db+".session.delete(self)";
			mr+=separacion2+db+".session.commit()";
			
			mr+=separacion1+"@classmethod";
			mr+=separacion1+"def "+Factory.nb.getNombreMetodoGetAll()+"(cls):";
			mr+=separacion2+"return cls.query.all()";
			
			mr+=separacion1+"@classmethod";
			mr+=separacion1+"def "+Factory.nb.getNombreMetodoGetById()+"(cls,id):";
			mr+=separacion2+"return cls.query.get(id)";
			
			mr+=separacion1+"@classmethod";
			mr+=separacion1+"def "+Factory.nb.getNombreMetodoSimpleFilter()+"(cls,**kwards):";
			mr+=separacion2+"return cls.query.filter_by(**kwards).all()";
			
			return mr;
		}
		
		public string getContenidoArchivo_Models(int separacion0){
			string separacion = getSeparacionln(0,separacion0);
			string separacion1 = getSeparacionln(1,separacion0);
			string separacion2 = getSeparacionln(2,separacion0);
			string mr ="";
			mr+=separacion+getImportAll(Factory.nb.getNombreCarpetaApp(),Factory.nb.getNombreArchivoExt());
			
			return mr;
		}
		
		public string getContenidoArchivo_Shemas(int separacion0){
			string separacion = getSeparacionln(0,separacion0);
			string separacion1 = getSeparacionln(1,separacion0);
			string separacion2 = getSeparacionln(2,separacion0);
			string mr ="";
			mr+=separacion+"from marshmallow import fields";
			mr+=separacion+getImportAll(Factory.nb.getNombreCarpetaApp(),Factory.nb.getNombreArchivoExt());
			
			return mr;
		}
		
		public string getContenidoArchivo_Resources(int separacion0){
			string separacion = getSeparacionln(0,separacion0);
			string separacion1 = getSeparacionln(1,separacion0);
			string separacion2 = getSeparacionln(2,separacion0);
			string mr ="";
			mr+=separacion+"from flask import request,Blueprint";
			mr+=separacion+"from flask_restful import Api,Resource";
			mr+=separacion+getImportAll(Factory.nb.getNombreCarpetaApp(),Factory.nb.getNombreCarpetaModelos(),Factory.nb.getNombreArchivoModels());
			mr+=separacion+getImportAll(Factory.nb.getNombreCarpetaApp(),Factory.nb.getNombreCarpetaModelos(),Factory.nb.getNombreCarpetaApiV_No(1),Factory.nb.getNombreArchivoShemas());
			mr+=separacion+Factory.nb.getNombreVariableBlueprint()+"=Blueprint(\"NombreBluePrint\",__name__)";
			//aqui se instanciarian los Nombre Modelos Esquema
			mr+=separacion+Factory.nb.getNombreVariableApi()+"=Api("+Factory.nb.getNombreVariableBlueprint()+")";
			//aqui se crearian las urls
			
			return mr;
		}
		
		public string getContenidoArchivo_Default(int separacion0){
			string separacion = getSeparacionln(0,separacion0);
			string separacion1 = getSeparacionln(1,separacion0);
			string separacion2 = getSeparacionln(2,separacion0);
			string mr ="";
			mr+=separacion+"SECRET_KEY=\""+Utiles.getAleatoriedad(15)+"\"";
			mr+=separacion+"PROPAGATE_EXCEPTIONS=True";
			mr+=separacion+"SQLALCHEMY_DATABASE_URI=\"\"";
			mr+=separacion+"SQLALCHEMY_TRACK_MODIFICATIONS=False";
			mr+=separacion+"SHOW_SQLALCHEMY_LOG_MESSAGES=False";
			mr+=separacion+"ERROR_404_HELP=False";
			return mr;
		}
		
		public string getContenidoArchivo_InitApp(int separacion0){
			string separacion = getSeparacionln(0,separacion0);
			string separacion1 = getSeparacionln(1,separacion0);
			string separacion2 = getSeparacionln(2,separacion0);
			string mr ="";
			mr+=separacion+"from flask import Flask,jsonify";
			mr+=separacion+"from flask_restful import Api";
			mr+=separacion+getImportAll(Factory.nb.getNombreCarpetaApp(),Factory.nb.getNombreCarpetaCommon(),Factory.nb.getNombreArchivoErrorHanddling());
			mr+=separacion+getImportAll(Factory.nb.getNombreCarpetaApp(),Factory.nb.getNombreArchivoDB());
			mr+=separacion+getImportAll(Factory.nb.getNombreCarpetaApp(),Factory.nb.getNombreCarpetaModelos(),Factory.nb.getNombreCarpetaApiV_No(1),Factory.nb.getNombreArchivoResources());
			mr+=separacion+getImportAll(Factory.nb.getNombreCarpetaApp(),Factory.nb.getNombreArchivoExt());
			
			//mr+=separacion+"def create_app(setting_module):";
			mr+=separacion+"def create_app():";
			mr+=separacion1+Factory.nb.getNombreVariableFlask()+"=Flask(__name__)";
			//mr+=separacion1+Factory.nb.getNombreVariableFlask()+".config.from_object(setting_module)";
			mr+=separacion1+Factory.nb.getNombreVariableDB()+".init_app("+Factory.nb.getNombreVariableFlask()+")";
			mr+=separacion1+Factory.nb.getNombreVariableMarshmallow()+".init_app("+Factory.nb.getNombreVariableFlask()+")";
			mr+=separacion1+Factory.nb.getNombreVariableMigrate()+".init_app("+Factory.nb.getNombreVariableFlask()+","+Factory.nb.getNombreVariableDB()+")";
			mr+=separacion1+"Api("+Factory.nb.getNombreVariableFlask()+",catch_all_404s=True)";
			mr+=separacion1+Factory.nb.getNombreMetodoRegistrarManejadorDeErrores()+"("+Factory.nb.getNombreVariableFlask()+")";
			mr+=separacion1+"return "+Factory.nb.getNombreVariableFlask();
			
			mr+=separacion+"def "+Factory.nb.getNombreMetodoRegistrarManejadorDeErrores()+"("+Factory.nb.getNombreVariableFlask()+"):";
			mr+=separacion1+"@"+Factory.nb.getNombreVariableFlask()+".errorhandler(404)";
			mr+=separacion1+"def handler_404_error(e):";
			mr+=separacion2+"return jsonify({\"msg\":\"error de 404\"}),404";
			mr+=separacion1+"@"+Factory.nb.getNombreVariableFlask()+".errorhandler(500)";
			mr+=separacion1+"def handler_500_error(e):";
			mr+=separacion2+"return jsonify({\"msg\":\"error de 500\"}),500";
			
			
			
			return mr;
		}
		
		/// <summary>
		/// (modulo1,2,3,..., elementoAImportar)
		/// 
		/// -> from CP.modulo1.modulo2.modulo3 import elementoAImportar
		/// 
		/// </summary>
		/// <param name="modulos"></param>
		/// <returns></returns>
		public string getImport(params string[] modulos){
			string ele="from "+Factory.nb.getNombreCarpetaProyecto();
			for (int i = 0; i < modulos.Length; i++) {
				string e=modulos[i];
				if(i!=modulos.Length-1){
					ele+="."+e;
				}else{
					ele+=" import "+e;
				}
			}
			return ele;
		}
		/// <summary>
		/// 
		/// (modulo1,2,3,...,N, moduloN)
		/// 
		/// -> from CP.modulo1.modulo2.moduloN import moduloN
		/// </summary>
		/// <param name="modulos"></param>
		/// <returns></returns>
		public string getImportModulo(params string[] modulos){
			string[] m2=new string[modulos.Length+1];
			for (int i = 0; i < modulos.Length; i++) {
				m2[i]=modulos[i];
			}
			m2[modulos.Length]=modulos[modulos.Length-1];
			return getImport(m2);
		}
		/// <summary>
		/// (modulo1,2,3,...)
		/// 
		/// -> from CP.modulo1.modulo2.modulo3 import *
		/// </summary>
		/// <param name="modulos"></param>
		/// <returns></returns>
		public string getImportAll(params string[] modulos){
			string[] m2=new string[modulos.Length+1];
			for (int i = 0; i < modulos.Length; i++) {
				m2[i]=modulos[i];
			}
			m2[modulos.Length]="*";
			return getImport(m2);
		}
		
//		public string getContenidoArchivo_DB(int separacion0){
//			string separacion = getSeparacionln(0,separacion0);
//			string separacion1 = getSeparacionln(1,separacion0);
//			string separacion2 = getSeparacionln(2,separacion0);
//			string mr ="";
//			
//			
//			
//			return mr;
//		}
		public virtual  string getSeparacionln(int indice, int separacion0)
		{
			string r = "\n";
			for (int i = 0; i < indice + separacion0; i++) {
				r += "\t";
			}
			return r;
		}
		
	}
}
