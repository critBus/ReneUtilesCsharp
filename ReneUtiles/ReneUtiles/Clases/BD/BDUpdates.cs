/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 10/4/2022
 * Hora: 15:21
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using ReneUtiles;
using ReneUtiles.Clases;
using System.Text.RegularExpressions;

namespace ReneUtiles.Clases.BD
{
	/// <summary>
	/// Description of BDUpdates.
	/// </summary>
	public class BDUpdates:BasicoBD 
	{
		public static readonly Regex PATRON_BDUPDATE_COLUMNA_INFORMACION =new Regex("[<][{](.*)[}][>]=[<][{](.*)");
	    public static readonly string TIPO_DE_MOTIVO_NO_ESPECIFICADO = "TIPO_DE_MOTIVO_NO_ESPECIFICADO",
	            TIPO_DE_MOTIVO_CREADO = "TIPO_DE_MOTIVO_CREADO";
	
	    
	    public static readonly string TABLA_UPDATES = "TABLA_UPDATES",
	            COLUMNA_ID_TABLA = "COLUMNA_ID_TABLA",
	            COLUMNA_NOMBRE_TABLA = "COLUMNA_NOMBRE_TABLA",
	            COLUMNA_CONTENIDO = "COLUMNA_CONTENIDO",
	            COLUMNA_MOTIVO = "COLUMNA_MOTIVO",
	            COLUMNA_FECHA = "COLUMNA_FECHA";
	    private BDConexion __BD;
	
	    public BDUpdates(BDConexion BD) {
	        this.__BD = BD;
	    }
	
	    public BDUpdates createTabla(){
	        this.__BD.crearTabla(TABLA_UPDATES,
	                COLUMNA_ID_TABLA, TipoDeClasificacionSQL.NOT_NULL,
	                COLUMNA_NOMBRE_TABLA, 50, TipoDeClasificacionSQL.NOT_NULL,
	                COLUMNA_CONTENIDO, TipoDeDatoSQL.BLOB, TipoDeClasificacionSQL.NOT_NULL,
	                COLUMNA_MOTIVO, 50, TipoDeClasificacionSQL.NOT_NULL,
	                COLUMNA_FECHA, TipoDeClasificacionSQL.NOT_NULL);
	        return this;
	    }
	
	    private BDUpdate_Model __get_Args(Object[] listaArgumentos) {
	        return new BDUpdate_Model(
	                toInt(listaArgumentos[1]),
	                to_String(listaArgumentos[2]),
	                toBlob(listaArgumentos[3]),
	                to_String(listaArgumentos[4]),
	                toDate(listaArgumentos[5]),
	                toInt(listaArgumentos[0])
	        );
	    }
	
	    public BDUpdate_Model get(string nombreTabla, int idTabla, DateTime  fecha){
	        return this.__get_Args(this.__BD.select_Where_FirstRow(TABLA_UPDATES,
	                COLUMNA_NOMBRE_TABLA, nombreTabla,
	                COLUMNA_ID_TABLA, idTabla,
	                COLUMNA_FECHA, fecha));
	    }
	
	    public BDUpdate_Model addM(int idKey_Tabla, string nombreTabla, Object contenido){
	        return addM(idKey_Tabla,nombreTabla,contenido,null,NULL_DATE);
	    }
	
	    public BDUpdate_Model addM(int idKey_Tabla, string nombreTabla, Object contenido, string motivo, DateTime  fecha){
	        if (motivo == null) {
	            motivo = TIPO_DE_MOTIVO_NO_ESPECIFICADO;
	        }
	        if (esArreglo(contenido)) {
	            string c = "";
	            Object []l = (Object[]) contenido;
	            for (int i = 0; i < l.Length; i++) {
	                c += "<{" + l[0] + "}>=<{" + l[1] + "}> ";
	            }
	
	            contenido = c;
	        }
	        contenido = toBlob(contenido);
	        return this.__add(new BDUpdate_Model(idKey_Tabla, nombreTabla, (byte[]) contenido, motivo, fecha));
	    }
	
	    public BDUpdate_Model __add(BDUpdate_Model update){
	        if (update.idKey != -1) {
	            this.__BD.insertar(TABLA_UPDATES, update.idKey_Tabla,
	                    update.nombreTabla, update.contenido,
	                    update.motivo, update.fecha);
	        } else {
	            this.__BD.insertar(TABLA_UPDATES, update.idKey, update.idKey_Tabla,
	                    update.nombreTabla, update.contenido,
	                    update.motivo, update.fecha);
	        }
	        return this.get(update.nombreTabla, update.idKey_Tabla, update.fecha);
	    }
	
	    public List<BDUpdate_Model> getAll(){
	        Object [][]O = this.__BD.select_Todo(TABLA_UPDATES);
	        List<BDUpdate_Model> l=new List<BDUpdate_Model>();
	        for (int i = 0; i < O.Length; i++) {
	            l.Add(__get_Args(O[i]));
	        }
	        return l;
	    }
	
	    public List<E> getInstanciasDesc_id<E>(string nombreTabla,int id,metodoCreador1<Object[],E> creador_Args){
	        Object [][]O=this.__BD.select_Where_ORDER_BY(TABLA_UPDATES
	                ,new Object[]{COLUMNA_NOMBRE_TABLA,nombreTabla
	                        ,COLUMNA_ID_TABLA,id}
	                ,COLUMNA_FECHA,TipoDeOrdenamientoSQL.DESC);
	        List<E> l=new List<E>();
	        for (int i = 0; i < O.Length; i++) {
	            l.Add(creador_Args(this.__get_Args(O[i]).getRowObj()));
	
	        }
	        return l;
	
	    }
	
	    public BDUpdate_Model getLastUpdate(string nombreTabla,int id){
	        Object []O=this.__BD.select_FirstRow_Where_ValorMaximo(TABLA_UPDATES,COLUMNA_FECHA
	                ,COLUMNA_NOMBRE_TABLA,nombreTabla
	                ,COLUMNA_ID_TABLA,id);
	        return this.__get_Args(O);}
	    public E getLastInstancia<E>(string nombreTabla,int id,metodoCreador1<Object[],E> creador_Args){
	        return creador_Args(this.getLastUpdate(nombreTabla,id).getRowObj());
	    }
	    public E getInstancia<E>(int idUpdate,metodoCreador1<Object[],E> creador_Args)where E:ModeloDeApiBD<Object>{
	        Object[] O=this.__BD.select_forID(TABLA_UPDATES,idUpdate);
	        if (O==null){
	            return null;} 
	        return creador_Args(this.__get_Args(O).getRowObj());
	    }
	}
	public class BDUpdate_Model:BasicoBD {
	
	        public int idKey_Tabla;
	        public string nombreTabla;
	        public byte[] contenido;
	        private string __contenidoStr;
	        private Dictionary< string, string> __contenidoDic;
	        public string motivo;
	        public DateTime fecha;
	        public int idKey;
	        private Object[] __rowObj;
	
	        public BDUpdate_Model(int idKey_Tabla, string nombreTabla, byte[] contenido, string motivo, DateTime  fecha):this(idKey_Tabla, nombreTabla, contenido, motivo, fecha, -1) {
	            
	        }
	
	        public BDUpdate_Model(int idKey_Tabla, string nombreTabla, byte[] contenido, string motivo, DateTime  fecha, int idKey) {
	            if (motivo == null || motivo.Trim().Length==0) {
	                motivo = BDUpdates.TIPO_DE_MOTIVO_NO_ESPECIFICADO;
	            }
	            if (fecha == null) {
	                fecha = new DateTime();
	            }
	
	            this.idKey_Tabla = idKey_Tabla;
	            this.nombreTabla = nombreTabla;
	            this.contenido = contenido;
	            this.motivo = motivo;
	            this.fecha = fecha;
	            this.idKey = idKey;
	        }
	
	        public string getContenidoStr() {
	            if (this.__contenidoStr == null) {
	                this.__contenidoStr = fromBlob(this.contenido);
	            }
	            return this.__contenidoStr;
	        }
	
	        public string getValorEnColumna(string nombreColumna) {
	            if (this.__contenidoDic == null) {
	                this.getRowObj();
	            }
	        	return this.__contenidoDic[nombreColumna];
	        }
	
	        public Object[] getRowObj() {
	            if (this.__rowObj == null) {
	                List<Object> l = new List<Object>();
	                if (this.idKey_Tabla != -1) {
	                    l.Add(this.idKey_Tabla);
	                }
	                this.__contenidoDic = new Dictionary<string, string>();
	                
	                string []lista =Utiles.split(this.getContenidoStr(),"}> ");
	                for (int i = 0; i < lista.Length; i++) {
	                	MatchCollection ml= BDUpdates.PATRON_BDUPDATE_COLUMNA_INFORMACION.Matches(lista[i]);
	                	if(ml.Count>0){
	                		this.__contenidoDic.Add(ml[0].ToString(),ml[1].ToString());
	                		l.Add(ml[1].ToString());
	                	}
	                    
	                }
	
	                this.__rowObj = l.ToArray();
	            }
	            return this.__rowObj;
	        }
	
	        public void print() {
	            BDUpdate_Model u = this;
	            cwl("BDUpdate_Model: idKey=" + u.idKey + " idKey_Tabla=" + u.idKey_Tabla + "\ncontenido=" + u.getContenidoStr() + "\nmotivo=" + u.motivo + " fecha=" + u.fecha); 
	        }
	
	    }
	
}
