

using ReneUtiles.Clases.BD;
using ReneUtiles;
using ReneUtiles.Clases;
using System;
using System.IO;
using System.Collections.Generic;
namespace ReneUtiles.Clases.BD.SesionEstorage.Modelos{
	
public class BDAdminSesionStorage:BasicoBD{
		private string urlBD;
		private BDConexion BD;
		private BDUpdates __Upd;
		private bool usarUpdater;
		public BDAdminSesionStorage():
				
	this(null)
		{
		}
		public BDAdminSesionStorage(string url){
		if (url==null){
			this.urlBD="bdSesionEstorage.sqlite";
		}else{
			this.urlBD=url;
		}
			
		this.BD =BDConexion.getConexionSQL_LITE(this.urlBD);
			this.__Upd =new BDUpdates(this.BD);
			this.usarUpdater=false;
		}
		public string getUrlBD(){
			return  this.urlBD;
		}
		public BDAdminSesionStorage crearTablaPropiedadSesionStorage_MD(){
			 this.BD.crearTablaYBorrarSiExiste(PropiedadSesionStorage_MD.TABLA_PROPIEDAD_SESION_STORAGE
							,PropiedadSesionStorage_MD.COLUMNA_SESION
							,PropiedadSesionStorage_MD.COLUMNA_PROPIEDAD
							,PropiedadSesionStorage_MD.COLUMNA_ES_LISTA,TipoDeDatoSQL.BOOLEAN
							);
			return this;
		}
		public BDAdminSesionStorage crearTablaPropiedadSesionStorage_MDSiNoExiste(){
			 this.BD.crearTablaSiNoExiste(PropiedadSesionStorage_MD.TABLA_PROPIEDAD_SESION_STORAGE
							,PropiedadSesionStorage_MD.COLUMNA_SESION
							,PropiedadSesionStorage_MD.COLUMNA_PROPIEDAD
							,PropiedadSesionStorage_MD.COLUMNA_ES_LISTA,TipoDeDatoSQL.BOOLEAN
							);
			return this;
		}
		public PropiedadSesionStorage_MD getPropiedadSesionStorage_MD_Args(Object[] listaDeArgumentos){
			return new PropiedadSesionStorage_MD(to_String(listaDeArgumentos[1])
					,to_String(listaDeArgumentos[2])
					,toBool(listaDeArgumentos[3])
					,toInt(listaDeArgumentos[0])
					,this
					);
			}
		public Object[] __content_PropiedadSesionStorage_MD(PropiedadSesionStorage_MD propiedad_sesion_storage){
			Object[] lista = {new Object[]{PropiedadSesionStorage_MD.COLUMNA_SESION,propiedad_sesion_storage.sesion}
				,new Object[]{PropiedadSesionStorage_MD.COLUMNA_PROPIEDAD,propiedad_sesion_storage.propiedad}
				,new Object[]{PropiedadSesionStorage_MD.COLUMNA_ES_LISTA,propiedad_sesion_storage.es_lista}
				};
			return lista;
			}
		public PropiedadSesionStorage_MD getPropiedadSesionStorage_MD_id(int? id){
			Object[] O = this.BD.select_forID(PropiedadSesionStorage_MD.TABLA_PROPIEDAD_SESION_STORAGE, id); 
			if (O == null){
				return null;}
			return this.getPropiedadSesionStorage_MD_Args(O);
			}
		public PropiedadSesionStorage_MD insertarPropiedadSesionStorage_MD(PropiedadSesionStorage_MD propiedad_sesion_storage){
			if (propiedad_sesion_storage.idkey==-1){
				int? id=this.BD.insertar(PropiedadSesionStorage_MD.TABLA_PROPIEDAD_SESION_STORAGE
						,propiedad_sesion_storage.sesion
						,propiedad_sesion_storage.propiedad
						,propiedad_sesion_storage.es_lista
						).id;
				return this.getPropiedadSesionStorage_MD_id(id);
			}else{
				this.BD.insertar_SinIdAutomatico(PropiedadSesionStorage_MD.TABLA_PROPIEDAD_SESION_STORAGE,propiedad_sesion_storage.idkey
						,propiedad_sesion_storage.sesion
						,propiedad_sesion_storage.propiedad
						,propiedad_sesion_storage.es_lista
						);
				return this.getPropiedadSesionStorage_MD_id(propiedad_sesion_storage.idkey);}
			}
		public List<PropiedadSesionStorage_MD> getPropiedadSesionStorage_MD_All(){
				List<PropiedadSesionStorage_MD> lista=new List<PropiedadSesionStorage_MD>();
				Object [][]O=this.BD.select_Todo(PropiedadSesionStorage_MD.TABLA_PROPIEDAD_SESION_STORAGE);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getPropiedadSesionStorage_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public PropiedadSesionStorage_MD updatePropiedadSesionStorage_MD(PropiedadSesionStorage_MD propiedad_sesion_storage){
				this.BD.update_Id(PropiedadSesionStorage_MD.TABLA_PROPIEDAD_SESION_STORAGE,propiedad_sesion_storage.idkey
							 , PropiedadSesionStorage_MD.COLUMNA_SESION , propiedad_sesion_storage.sesion
							 , PropiedadSesionStorage_MD.COLUMNA_PROPIEDAD , propiedad_sesion_storage.propiedad
							 , PropiedadSesionStorage_MD.COLUMNA_ES_LISTA , propiedad_sesion_storage.es_lista);
				return getPropiedadSesionStorage_MD_id(propiedad_sesion_storage.idkey);
		}
		public void deletePropiedadSesionStorage_MD_ForId(int? id){
				this.BD.delete_id(PropiedadSesionStorage_MD.TABLA_PROPIEDAD_SESION_STORAGE,id);
		}
		public void deletePropiedadSesionStorage_MD_ForId(PropiedadSesionStorage_MD propiedad_sesion_storage){
				deletePropiedadSesionStorage_MD_ForId(propiedad_sesion_storage.idkey);
		}
		public bool existePropiedadSesionStorage_MD(string sesion,string propiedad){
				return this.BD.existe(PropiedadSesionStorage_MD.TABLA_PROPIEDAD_SESION_STORAGE
						,PropiedadSesionStorage_MD.COLUMNA_SESION,sesion
						,PropiedadSesionStorage_MD.COLUMNA_PROPIEDAD,propiedad);
		}
		public void deletePropiedadSesionStorage_MD_ForId_CASCADE(int? idkey_propiedad_sesion_storage){
			deletePropiedadSesionStorage_MD_ForId_CASCADE(idkey_propiedad_sesion_storage,null);
		}
		public void deletePropiedadSesionStorage_MD_ForId_CASCADE(int? idkey_propiedad_sesion_storage,Object modeloQueLoLlamo){
			PropiedadSesionStorage_MD propiedad_sesion_storage=getPropiedadSesionStorage_MD_id(idkey_propiedad_sesion_storage);
			deleteValorSimpleSesionStorage_MD_For_Idkey_propiedad_sesion_storage(idkey_propiedad_sesion_storage);
			deleteDatoEnListaSesionStorage_MD_For_Idkey_propiedad_sesion_storage(idkey_propiedad_sesion_storage);
			deletePropiedadSesionStorage_MD_ForId(idkey_propiedad_sesion_storage);
		}
		public BDAdminSesionStorage crearTablaValorSimpleSesionStorage_MD(){
			 this.BD.crearTablaYBorrarSiExiste(ValorSimpleSesionStorage_MD.TABLA_VALOR_SIMPLE_SESION_STORAGE
							,ValorSimpleSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE,TipoDeDatoSQL.INTEGER
							,ValorSimpleSesionStorage_MD.COLUMNA_VALOR,500
							);
			return this;
		}
		public BDAdminSesionStorage crearTablaValorSimpleSesionStorage_MDSiNoExiste(){
			 this.BD.crearTablaSiNoExiste(ValorSimpleSesionStorage_MD.TABLA_VALOR_SIMPLE_SESION_STORAGE
							,ValorSimpleSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE,TipoDeDatoSQL.INTEGER
							,ValorSimpleSesionStorage_MD.COLUMNA_VALOR,500
							);
			return this;
		}
		public ValorSimpleSesionStorage_MD getValorSimpleSesionStorage_MD_Args(Object[] listaDeArgumentos){
			return new ValorSimpleSesionStorage_MD(toInt(listaDeArgumentos[1])
					,to_String(listaDeArgumentos[2])
					,toInt(listaDeArgumentos[0])
					,this
					);
			}
		public Object[] __content_ValorSimpleSesionStorage_MD(ValorSimpleSesionStorage_MD valor_simple_sesion_storage){
			Object[] lista = {new Object[]{ValorSimpleSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE,valor_simple_sesion_storage.idkey_propiedad_sesion_storage}
				,new Object[]{ValorSimpleSesionStorage_MD.COLUMNA_VALOR,valor_simple_sesion_storage.valor}
				};
			return lista;
			}
		public ValorSimpleSesionStorage_MD getValorSimpleSesionStorage_MD_id(int? id){
			Object[] O = this.BD.select_forID(ValorSimpleSesionStorage_MD.TABLA_VALOR_SIMPLE_SESION_STORAGE, id);
			if (O == null){
				return null;}
			return this.getValorSimpleSesionStorage_MD_Args(O);
			}
		public ValorSimpleSesionStorage_MD insertarValorSimpleSesionStorage_MD(ValorSimpleSesionStorage_MD valor_simple_sesion_storage){
			if (valor_simple_sesion_storage.idkey==-1){
				int? id=this.BD.insertar(ValorSimpleSesionStorage_MD.TABLA_VALOR_SIMPLE_SESION_STORAGE
						,valor_simple_sesion_storage.idkey_propiedad_sesion_storage
						,valor_simple_sesion_storage.valor
						).id;
				return this.getValorSimpleSesionStorage_MD_id(id);
			}else{
				this.BD.insertar_SinIdAutomatico(ValorSimpleSesionStorage_MD.TABLA_VALOR_SIMPLE_SESION_STORAGE,valor_simple_sesion_storage.idkey
						,valor_simple_sesion_storage.idkey_propiedad_sesion_storage
						,valor_simple_sesion_storage.valor
						);
				return this.getValorSimpleSesionStorage_MD_id(valor_simple_sesion_storage.idkey);}
			}
		public List<ValorSimpleSesionStorage_MD> getValorSimpleSesionStorage_MD_All(){
				List<ValorSimpleSesionStorage_MD> lista=new List<ValorSimpleSesionStorage_MD>();
				Object [][]O=this.BD.select_Todo(ValorSimpleSesionStorage_MD.TABLA_VALOR_SIMPLE_SESION_STORAGE);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getValorSimpleSesionStorage_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public ValorSimpleSesionStorage_MD updateValorSimpleSesionStorage_MD(ValorSimpleSesionStorage_MD valor_simple_sesion_storage){
				this.BD.update_Id(ValorSimpleSesionStorage_MD.TABLA_VALOR_SIMPLE_SESION_STORAGE,valor_simple_sesion_storage.idkey
							 , ValorSimpleSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE , valor_simple_sesion_storage.idkey_propiedad_sesion_storage
							 , ValorSimpleSesionStorage_MD.COLUMNA_VALOR , valor_simple_sesion_storage.valor);
				return getValorSimpleSesionStorage_MD_id(valor_simple_sesion_storage.idkey);
		}
		public void deleteValorSimpleSesionStorage_MD_ForId(int? id){
				this.BD.delete_id(ValorSimpleSesionStorage_MD.TABLA_VALOR_SIMPLE_SESION_STORAGE,id);
		}
		public void deleteValorSimpleSesionStorage_MD_ForId(ValorSimpleSesionStorage_MD valor_simple_sesion_storage){
				deleteValorSimpleSesionStorage_MD_ForId(valor_simple_sesion_storage.idkey);
		}
		public BDAdminSesionStorage crearTablaDatoEnListaSesionStorage_MD(){
			 this.BD.crearTablaYBorrarSiExiste(DatoEnListaSesionStorage_MD.TABLA_DATO_EN_LISTA_SESION_STORAGE
							,DatoEnListaSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE,TipoDeDatoSQL.INTEGER
							,DatoEnListaSesionStorage_MD.COLUMNA_VALOR,500
							);
			return this;
		}
		public BDAdminSesionStorage crearTablaDatoEnListaSesionStorage_MDSiNoExiste(){
			 this.BD.crearTablaSiNoExiste(DatoEnListaSesionStorage_MD.TABLA_DATO_EN_LISTA_SESION_STORAGE
							,DatoEnListaSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE,TipoDeDatoSQL.INTEGER
							,DatoEnListaSesionStorage_MD.COLUMNA_VALOR,500
							);
			return this;
		}
		public DatoEnListaSesionStorage_MD getDatoEnListaSesionStorage_MD_Args(Object[] listaDeArgumentos){
			return new DatoEnListaSesionStorage_MD(toInt(listaDeArgumentos[1])
					,to_String(listaDeArgumentos[2])
					,toInt(listaDeArgumentos[0])
					,this
					);
			}
		public Object[] __content_DatoEnListaSesionStorage_MD(DatoEnListaSesionStorage_MD dato_en_lista_sesion_storage){
			Object[] lista = {new Object[]{DatoEnListaSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE,dato_en_lista_sesion_storage.idkey_propiedad_sesion_storage}
				,new Object[]{DatoEnListaSesionStorage_MD.COLUMNA_VALOR,dato_en_lista_sesion_storage.valor}
				};
			return lista;
			}
		public DatoEnListaSesionStorage_MD getDatoEnListaSesionStorage_MD_id(int? id){
			Object[] O = this.BD.select_forID(DatoEnListaSesionStorage_MD.TABLA_DATO_EN_LISTA_SESION_STORAGE, id);
			if (O == null){
				return null;}
			return this.getDatoEnListaSesionStorage_MD_Args(O);
			}
		public DatoEnListaSesionStorage_MD insertarDatoEnListaSesionStorage_MD(DatoEnListaSesionStorage_MD dato_en_lista_sesion_storage){
			if (dato_en_lista_sesion_storage.idkey==-1){
				int? id=this.BD.insertar(DatoEnListaSesionStorage_MD.TABLA_DATO_EN_LISTA_SESION_STORAGE
						,dato_en_lista_sesion_storage.idkey_propiedad_sesion_storage
						,dato_en_lista_sesion_storage.valor
						).id;
				return this.getDatoEnListaSesionStorage_MD_id(id);
			}else{
				this.BD.insertar_SinIdAutomatico(DatoEnListaSesionStorage_MD.TABLA_DATO_EN_LISTA_SESION_STORAGE,dato_en_lista_sesion_storage.idkey
						,dato_en_lista_sesion_storage.idkey_propiedad_sesion_storage
						,dato_en_lista_sesion_storage.valor
						);
				return this.getDatoEnListaSesionStorage_MD_id(dato_en_lista_sesion_storage.idkey);}
			}
		public List<DatoEnListaSesionStorage_MD> getDatoEnListaSesionStorage_MD_All(){
				List<DatoEnListaSesionStorage_MD> lista=new List<DatoEnListaSesionStorage_MD>();
				Object [][]O=this.BD.select_Todo(DatoEnListaSesionStorage_MD.TABLA_DATO_EN_LISTA_SESION_STORAGE);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getDatoEnListaSesionStorage_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public DatoEnListaSesionStorage_MD updateDatoEnListaSesionStorage_MD(DatoEnListaSesionStorage_MD dato_en_lista_sesion_storage){
				this.BD.update_Id(DatoEnListaSesionStorage_MD.TABLA_DATO_EN_LISTA_SESION_STORAGE,dato_en_lista_sesion_storage.idkey
							 , DatoEnListaSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE , dato_en_lista_sesion_storage.idkey_propiedad_sesion_storage
							 , DatoEnListaSesionStorage_MD.COLUMNA_VALOR , dato_en_lista_sesion_storage.valor);
				return getDatoEnListaSesionStorage_MD_id(dato_en_lista_sesion_storage.idkey);
		}
		public void deleteDatoEnListaSesionStorage_MD_ForId(int? id){
				this.BD.delete_id(DatoEnListaSesionStorage_MD.TABLA_DATO_EN_LISTA_SESION_STORAGE,id);
		}
		public void deleteDatoEnListaSesionStorage_MD_ForId(DatoEnListaSesionStorage_MD dato_en_lista_sesion_storage){
				deleteDatoEnListaSesionStorage_MD_ForId(dato_en_lista_sesion_storage.idkey);
		}
		public void crearTodasLasTablas(){
			crearTablaPropiedadSesionStorage_MD();
			crearTablaValorSimpleSesionStorage_MD();
			crearTablaDatoEnListaSesionStorage_MD();
		}
		public void crearTodasLasTablasSiNoExisten(){
			crearTablaPropiedadSesionStorage_MDSiNoExiste();
			crearTablaValorSimpleSesionStorage_MDSiNoExiste();
			crearTablaDatoEnListaSesionStorage_MDSiNoExiste();
		}
		public PropiedadSesionStorage_MD getPropiedadSesionStorage_MD_For_Sesion_Propiedad(string sesion,string propiedad){
				List<PropiedadSesionStorage_MD> lista=new List<PropiedadSesionStorage_MD>();
				Object []O=this.BD.select_Where_FirstRow(PropiedadSesionStorage_MD.TABLA_PROPIEDAD_SESION_STORAGE,PropiedadSesionStorage_MD.COLUMNA_SESION,sesion,PropiedadSesionStorage_MD.COLUMNA_PROPIEDAD,propiedad);
				if (O!=null){
					return getPropiedadSesionStorage_MD_Args(O);
				}
				return null;
		}
		public List<ValorSimpleSesionStorage_MD> getValorSimpleSesionStorage_MD_All_Idkey_propiedad_sesion_storage(int? idkey_propiedad_sesion_storage){
				List<ValorSimpleSesionStorage_MD> lista=new List<ValorSimpleSesionStorage_MD>();
				Object [][]O=this.BD.select_Where(ValorSimpleSesionStorage_MD.TABLA_VALOR_SIMPLE_SESION_STORAGE,ValorSimpleSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE,idkey_propiedad_sesion_storage);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getValorSimpleSesionStorage_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public List<ValorSimpleSesionStorage_MD> getValorSimpleSesionStorage_MD_All_Idkey_propiedad_sesion_storage(PropiedadSesionStorage_MD propiedad_sesion_storage){
				return getValorSimpleSesionStorage_MD_All_Idkey_propiedad_sesion_storage(propiedad_sesion_storage.idkey);
		}
		public ValorSimpleSesionStorage_MD getValorSimpleSesionStorage_MD_For_Idkey_propiedad_sesion_storage(int? idkey_propiedad_sesion_storage){
				Object []O=this.BD.select_Where_FirstRow(ValorSimpleSesionStorage_MD.TABLA_VALOR_SIMPLE_SESION_STORAGE,ValorSimpleSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE,idkey_propiedad_sesion_storage);
				if (O!=null){
					return getValorSimpleSesionStorage_MD_Args(O);
				}
				return null;
		}
		public ValorSimpleSesionStorage_MD getValorSimpleSesionStorage_MD_For_Idkey_propiedad_sesion_storage(PropiedadSesionStorage_MD propiedad_sesion_storage){
				return getValorSimpleSesionStorage_MD_For_Idkey_propiedad_sesion_storage(propiedad_sesion_storage.idkey);
		}
		public void deleteValorSimpleSesionStorage_MD_For_Idkey_propiedad_sesion_storage(int? idkey_propiedad_sesion_storage){
				this.BD.delete(ValorSimpleSesionStorage_MD.TABLA_VALOR_SIMPLE_SESION_STORAGE,ValorSimpleSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE,idkey_propiedad_sesion_storage);
		}
		public void deleteValorSimpleSesionStorage_MD_For_Idkey_propiedad_sesion_storage(PropiedadSesionStorage_MD propiedad_sesion_storage){
				deleteValorSimpleSesionStorage_MD_For_Idkey_propiedad_sesion_storage(propiedad_sesion_storage.idkey);
		}
		public List<DatoEnListaSesionStorage_MD> getDatoEnListaSesionStorage_MD_All_Idkey_propiedad_sesion_storage(int? idkey_propiedad_sesion_storage){
				List<DatoEnListaSesionStorage_MD> lista=new List<DatoEnListaSesionStorage_MD>();
				Object [][]O=this.BD.select_Where(DatoEnListaSesionStorage_MD.TABLA_DATO_EN_LISTA_SESION_STORAGE,DatoEnListaSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE,idkey_propiedad_sesion_storage);
				if (O!=null){
					for(int i=0;i<O.Length;i++){
						lista.Add(getDatoEnListaSesionStorage_MD_Args(O[i]));
					}
				}
				return lista;
		}
		public List<DatoEnListaSesionStorage_MD> getDatoEnListaSesionStorage_MD_All_Idkey_propiedad_sesion_storage(PropiedadSesionStorage_MD propiedad_sesion_storage){
				return getDatoEnListaSesionStorage_MD_All_Idkey_propiedad_sesion_storage(propiedad_sesion_storage.idkey);
		}
		public void deleteDatoEnListaSesionStorage_MD_For_Idkey_propiedad_sesion_storage(int? idkey_propiedad_sesion_storage){
				this.BD.delete(DatoEnListaSesionStorage_MD.TABLA_DATO_EN_LISTA_SESION_STORAGE,DatoEnListaSesionStorage_MD.COLUMNA_ID_TABLA_PROPIEDAD_SESION_STORAGE,idkey_propiedad_sesion_storage);
		}
		public void deleteDatoEnListaSesionStorage_MD_For_Idkey_propiedad_sesion_storage(PropiedadSesionStorage_MD propiedad_sesion_storage){
				deleteDatoEnListaSesionStorage_MD_For_Idkey_propiedad_sesion_storage(propiedad_sesion_storage.idkey);
		}
	}
}
