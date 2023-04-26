

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace ReneUtiles.Clases.BD.SesionEstorage.Modelos{
public class DatoEnListaSesionStorage_MD:ModeloDeApiBD<BDAdminSesionStorage> {
		public static readonly string TABLA_DATO_EN_LISTA_SESION_STORAGE="TABLA_DATO_EN_LISTA_SESION_STORAGE";
		public static readonly string COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE="COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE";
		public static readonly string COLUMNA_VALOR="COLUMNA_VALOR";
		
		public int? idkey_propiedad_sesion_storage;
		public string valor;
		
		public DatoEnListaSesionStorage_MD(int? idkey_propiedad_sesion_storage,string valor,int? idkey,BDAdminSesionStorage apibd):base(idkey,apibd){
			this.idkey_propiedad_sesion_storage=idkey_propiedad_sesion_storage;
			this.valor=valor;
		}
		public DatoEnListaSesionStorage_MD(BDAdminSesionStorage apibd,int? idkey_propiedad_sesion_storage,string valor):this(idkey_propiedad_sesion_storage,valor,-1,apibd){
		}
		public DatoEnListaSesionStorage_MD(BDAdminSesionStorage apibd,PropiedadSesionStorage_MD propiedad_sesion_storage,string valor):this(propiedad_sesion_storage.idkey,valor,-1,apibd){
		}
		public PropiedadSesionStorage_MD getPropiedad_sesion_storage(){
			return this.apibd.getPropiedadSesionStorage_MD_id(this.idkey_propiedad_sesion_storage);
		}
		public DatoEnListaSesionStorage_MD s(){
			if (this.idkey==-1){
				return this.apibd.insertarDatoEnListaSesionStorage_MD(this);
			}
			return this.apibd.updateDatoEnListaSesionStorage_MD(this);
		}
		public void d(){
			if (this.idkey!=-1){
				this.apibd.deleteDatoEnListaSesionStorage_MD_ForId(this.idkey);
			}
		}
		public string getStr(String textoInicial){
			DatoEnListaSesionStorage_MD s = this;
			return textoInicial+"DatoEnListaSesionStorage_MD: idkey="+ s.idkey
				+" idkey_propiedad_sesion_storage="+s.idkey_propiedad_sesion_storage
				+" valor="+s.valor
			;
		}
		public string getStr(){ return getStr("");}
}
}
