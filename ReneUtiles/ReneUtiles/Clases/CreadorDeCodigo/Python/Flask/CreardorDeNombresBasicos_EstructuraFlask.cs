/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 25/5/2022
 * Hora: 12:40
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.CreadorDeCodigo.Python.Flask
{
	/// <summary>
	/// Description of CreardorDeNombresBasicos_EstructuraFlask.
	/// </summary>
	public class CreardorDeNombresBasicos_EstructuraFlask
	{	
		public FactoryEstructuraFlask Factory;
		public CreardorDeNombresBasicos_EstructuraFlask(FactoryEstructuraFlask factory)
		{
			this.Factory=factory;
		}
		public string getNombreCarpetaProyecto(){
			return "API_"+Factory.NombreDelProyecto; 
		}
		public string getNombreCarpetaApp(){
			return "App"; 
		}
		public string getNombreCarpetaCommon(){
			return "Common"; 
		}
		public string getNombreCarpetaModelos(){
			return "Modelos"; 
		}
		public string getNombreCarpetaApiV_No(int version){
			return "API_V"+version; 
		}
		public string getNombreCarpetaConfig(){
			return "Config"; 
		}
		
		
		
		public string getNombreArchivoEntryPoint(){
			return "entrypoint"; 
		}
		public string getNombreArchivoErrorHanddling(){
			return "error_handling"; 
		}
		public string getNombreArchivoResources(){
			return "resources"; 
		}
		public string getNombreArchivoShemas(){
			return "shemas"; 
		}
		public string getNombreArchivoModels(){
			return "models"; 
		}
		public string getNombreArchivoDB(){
			return "db"; 
		}
		public string getNombreArchivoExt(){
			return "ext"; 
		}
		public string getNombreArchivoDefault(){
			return "default"; 
		}
		
		
		public string getNombreVariableMarshmallow(){
			return "v_marshmallow"; 
		}
		public string getNombreVariableMigrate(){
			return "v_migrate"; 
		}
		public string getNombreVariableDB(){
			return "v_db"; 
		}
		public string getNombreVariableBlueprint(){
			return "v_blueprint"; 
		}
		public string getNombreVariableApi(){
			return "v_api"; 
		}
		public string getNombreVariableFlask(){
			return "v_flask"; 
		}
		
		
		public string getNombreClaseInterfasModelo(){
			return "InterfasModelo"; 
		}
		
		
		
		public string getNombreMetodoSave(){
			return "save"; 
		}
		public string getNombreMetodoDelete(){
			return "delete"; 
		}
		public string getNombreMetodoGetAll(){
			return "get_All"; 
		}
		public string getNombreMetodoGetById(){
			return "get_by_id"; 
		}
		public string getNombreMetodoSimpleFilter(){
			return "simple_filter"; 
		}
		public string getNombreMetodoRegistrarManejadorDeErrores(){
			return "registrarManejadorDeErrores"; 
		}
		
	}
}
