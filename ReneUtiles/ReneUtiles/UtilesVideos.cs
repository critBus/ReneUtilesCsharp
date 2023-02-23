/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 3/10/2021
 * Time: 09:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
//using System.IO;
using Delimon.Win32.IO;
using ReneUtiles;
using ReneUtiles.Clases.Videos;

namespace ReneUtiles
{
	public  class DatosVideosConSubtitulos{
			public bool tieneVideos=false;
			public bool tieneSubtitulos=false;
			public bool tieneSubtitulosTodos=false;
			public bool tieneSubtitulosAlMenosUno=false;
			public int cantidadDeVideos=0;
			public int cantidadDeSubtitulos=0;
            public List<FileInfo> videos = new List<FileInfo>();
            public List<FileInfo> subtitulos = new List<FileInfo>();
    }
	
	
	/// <summary>
	/// Description of Videos.
	/// </summary>
	public abstract class UtilesVideos
	{
		
		public static DatosVideosConSubtitulos getDatosVideosConSubtitulosDe(DirectoryInfo c){
			DatosVideosConSubtitulos dv=new DatosVideosConSubtitulos();
			//Dictionary<string,bool> nombreVideosConSubtitulos=new Dictionary<string,bool>();
			HashSet<string> nombresSubtitulos=new HashSet<string>();
			HashSet<string> nombreVideos=new HashSet<string>();
			Archivos.recorrerArchivosExternos(c,f=>{
			                                  	if(esVideo(f)){
			                                  		dv.tieneVideos=true;
			                                  		dv.cantidadDeVideos++;
			                                  		nombreVideos.Add(Archivos.getNombre(f));
                                                    dv.videos.Add(f);
                                                    //nombreVideosConSubtitulos.Add(Archivos.getNombre(f),false);
                }
                else if (esSubtitulo(f)){
			                                  		dv.tieneVideos=true;
			                                  		dv.cantidadDeSubtitulos++;
			                                  		nombresSubtitulos.Add(Archivos.getNombre(f));
                                                    dv.subtitulos.Add(f);
			                                  	}
			                                  });
			if(dv.tieneVideos&&dv.tieneSubtitulos){
				foreach (string ns in nombresSubtitulos) {
					if(nombreVideos.Contains(ns)){
						dv.tieneSubtitulosAlMenosUno=true;
						continue;
					}
					return dv;
				}
				dv.tieneSubtitulosTodos=true;
			}
			return dv;
		}
		public static bool esVideo(FileInfo f){
			return esVideo(f.Name);
		}
		public static bool esVideo(string url){
			return TipoDeVideo.esDelTipoVideo(url);
		}
		
		public static bool esSubtitulo(FileInfo f){
			return esSubtitulo(f.Name);
		}
		public static bool esSubtitulo(string url){
			return TipoDeSubtitulo.esDelTipoDeSubtitulo(url);
		}
	}
}
