

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace ReneUtiles.Clases.BD.SesionEstorage.Modelos{
public class PropiedadSesionStorage_MD:ModeloDeApiBD<BDAdminSesionStorage> {
		public static readonly string TABLA_PROPIEDAD_SESION_STORAGE="TABLA_PROPIEDAD_SESION_STORAGE";
		public static readonly string COLUMNA_SESION="COLUMNA_SESION";
		public static readonly string COLUMNA_PROPIEDAD="COLUMNA_PROPIEDAD";
		public static readonly string COLUMNA_ES_LISTA="COLUMNA_ES_LISTA";
		
		public string sesion;
		public string propiedad;
		public bool es_lista;
		
		public PropiedadSesionStorage_MD(string sesion,string propiedad,bool es_lista,int idkey,BDAdminSesionStorage apibd):base(idkey,apibd){
			this.sesion=sesion;
			this.propiedad=propiedad;
			this.es_lista=es_lista;
		}
		public PropiedadSesionStorage_MD(BDAdminSesionStorage apibd,string sesion,string propiedad,bool es_lista):this(sesion,propiedad,es_lista,-1,apibd){
		}
		public PropiedadSesionStorage_MD s(){
			if (this.idkey==-1){
				return this.apibd.insertarPropiedadSesionStorage_MD(this);
			}
			return this.apibd.updatePropiedadSesionStorage_MD(this);
		}
		public void d(){
			if (this.idkey!=-1){
				this.apibd.deletePropiedadSesionStorage_MD_ForId_CASCADE(this.idkey);
			}
		}
		public string getStr(String textoInicial){
			PropiedadSesionStorage_MD s = this;
			return textoInicial+"PropiedadSesionStorage_MD: idkey="+ s.idkey
				+" sesion="+s.sesion
				+" propiedad="+s.propiedad
				+" es_lista="+s.es_lista
			;
		}
		public string getStr(){ return getStr("");}
}
}
