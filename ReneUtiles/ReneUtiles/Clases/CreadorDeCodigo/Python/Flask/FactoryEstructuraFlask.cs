/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 25/5/2022
 * Hora: 12:32
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
//using System.Threading.Tasks;
//using System.IO;

using ReneUtiles.Clases.Tipos;
using System.Text.RegularExpressions;

using Delimon.Win32.IO;

using ReneUtiles.Clases;
namespace ReneUtiles.Clases.CreadorDeCodigo.Python.Flask
{
	/// <summary>
	/// Description of FactoryEstructuraFlask.
	/// </summary>
	public class FactoryEstructuraFlask:ConsolaBasica
	{
		public string NombreDelProyecto;
		public CreardorDeNombresBasicos_EstructuraFlask nb;
		public CodeFlaskEstructuraBasica code;
		public FactoryEstructuraFlask()
		{
			this.nb=new CreardorDeNombresBasicos_EstructuraFlask(this);
			this.code=new CodeFlaskEstructuraBasica(this);
		}
		public void crearEstructura(string carpeta){
			crearEstructura(new DirectoryInfo(carpeta));
		}
		public void crearEstructura(DirectoryInfo carpeta){
			if(this.NombreDelProyecto==null){
				throw new Exception("Hay que tener un nombre de proyecto");
			}
			if(carpeta.Exists){
				
				DirectoryInfo carpeta_Proyecto=cc(carpeta,nb.getNombreCarpetaProyecto(),code.getContenidoArchivo_InitApp(0));
				{
					DirectoryInfo carpeta_App=cc(carpeta_Proyecto,nb.getNombreCarpetaApp());
					{
						DirectoryInfo carpeta_Common=cc(carpeta_App,nb.getNombreCarpetaCommon());
						{
							FileInfo archivo_error_handling=ca(carpeta_Common,nb.getNombreArchivoErrorHanddling());
						}
						DirectoryInfo carpeta_Modelos=cc(carpeta_App,nb.getNombreCarpetaModelos());
						{
							DirectoryInfo carpetaApiVersion=cc(carpeta_Modelos,nb.getNombreCarpetaApiV_No(1));
							{
								FileInfo archivo_resources=ca(carpetaApiVersion,nb.getNombreArchivoResources(),code.getContenidoArchivo_Resources(0));
								FileInfo archivo_shemas=ca(carpetaApiVersion,nb.getNombreArchivoShemas(),code.getContenidoArchivo_Shemas(0));
								
							}
							FileInfo archivo_models=ca(carpeta_Modelos,nb.getNombreArchivoModels(),code.getContenidoArchivo_Models(0));
							
						}
						FileInfo archivo_db=ca(carpeta_App,nb.getNombreArchivoDB(),code.getContenidoArchivo_DB(0));
						FileInfo archivo_ext=ca(carpeta_App,nb.getNombreArchivoExt(),code.getContenidoArchivo_Ext(0));
						
					}
					DirectoryInfo carpeta_Config=cc(carpeta_Proyecto,nb.getNombreCarpetaConfig());
					{
						FileInfo archivo_default=ca(carpeta_Config,nb.getNombreArchivoDefault(),code.getContenidoArchivo_Default(0));
						
					}
				}
				
				
				FileInfo archivo_entryPoint=ca(carpeta_Proyecto,nb.getNombreArchivoEntryPoint());
				
			}
		}
		
		private FileInfo ca(DirectoryInfo carpeta,string nombre,string contenido){
			FileInfo archivo=ca(carpeta,nombre);
			ea(archivo,contenido);
			return archivo;
		}
		private FileInfo ca(DirectoryInfo carpeta,string nombre){
			return Archivos.crearArchivo_SiNoExiste(carpeta,nombre+".py");
		}
		private DirectoryInfo cc(DirectoryInfo carpeta,string nombre,string contenidoInit=null){
			DirectoryInfo carpetaNueva=Archivos.crearCarpeta_SiNoExiste(carpeta,nombre);
			FileInfo archivo_init=ca(carpetaNueva,"__init__");
			if(contenidoInit!=null){
				ea(archivo_init,contenidoInit);
			}
			
			return carpetaNueva;
		}
		private void ea(FileInfo archivo,string contenido){
			Archivos.escribir(archivo.ToString(),contenido);
		}
	}
}
