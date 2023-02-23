/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 22/8/2022
 * Hora: 11:34
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles;
using System.Collections.Generic;
using ReneUtiles.Clases;
using ReneUtiles.Clases.BD;
using ReneUtiles.Clases.BD.Factory;
using System.IO;
using ReneUtiles.Clases.BD.SesionEstorage.Modelos;
namespace ReneUtiles.Clases.BD.SesionEstorage
{
	/// <summary>
	/// Description of BDAdminSesionStorage.
	/// </summary>
	public class BDSesionStorage:ConsolaBasica
	{
		private BDAdminSesionStorage admin;
		
		public BDSesionStorage()
		{
			this.admin = new BDAdminSesionStorage();
			this.admin.crearTodasLasTablasSiNoExisten();
			
		}
		public bool contieneKey(string keySession, string key)
		{
			return this.admin.existePropiedadSesionStorage_MD(
				sesion: keySession, propiedad: key);
		}
		public BDSesionStorage put(string keySession, string key, object valor)
		{
			if (contieneKey(keySession, key)) {
				PropiedadSesionStorage_MD p = this.admin.getPropiedadSesionStorage_MD_For_Sesion_Propiedad(keySession, key);
				if (esLista(valor)) {
					this.admin.deleteDatoEnListaSesionStorage_MD_For_Idkey_propiedad_sesion_storage(p);
					recorrerLista(valor, v => {
						new DatoEnListaSesionStorage_MD(this.admin, p, v.ToString()).s();
					});
				} else {
					ValorSimpleSesionStorage_MD va = this.admin.getValorSimpleSesionStorage_MD_For_Idkey_propiedad_sesion_storage(p);
					va.valor = valor.ToString();
					va.s();
				}
			} else {
				bool esUnaLista = esLista(valor);
				cwl("esUnaLista=" + esUnaLista);
				PropiedadSesionStorage_MD p = new PropiedadSesionStorage_MD(this.admin, keySession, key, esUnaLista).s();
				if (esUnaLista) {
					recorrerLista(valor, v => {
						new DatoEnListaSesionStorage_MD(this.admin, p, v.ToString()).s();
					});
				} else {
					new ValorSimpleSesionStorage_MD(this.admin, p, valor.ToString()).s();
				}
			}
			return this;
		}
		
		public object get(string keySession, string key, object valorDefault = null)
		{
			if (contieneKey(keySession, key)) {
				PropiedadSesionStorage_MD p = this.admin.getPropiedadSesionStorage_MD_For_Sesion_Propiedad(keySession, key);
				
				if (p.es_lista) {
					List<object> r = new List<object>();
					
					List<DatoEnListaSesionStorage_MD> ld = this.admin.getDatoEnListaSesionStorage_MD_All_Idkey_propiedad_sesion_storage(p);
					foreach (DatoEnListaSesionStorage_MD d in ld) {
						r.Add(d.valor);
					}
					return r;
				} else {
					ValorSimpleSesionStorage_MD vs = this.admin.getValorSimpleSesionStorage_MD_For_Idkey_propiedad_sesion_storage(p);
					return vs.valor;
				}
			} else {
				if (valorDefault != null) {
					put(keySession, key, valorDefault);
				}
			}
			return valorDefault;
		}
		
		
		public int? getInt(string keySession, string nombre, int? valorDefault = null)
		{
			object r = get(keySession, nombre, valorDefault);
			return inT(r.ToString());
		}
		public double? getDouble(string keySession, string nombre, double? valorDefault = null)
		{
			object r = get(keySession, nombre, valorDefault);
			return dou(r.ToString());
		}
		public bool? getBool(string keySession, string nombre, bool? valorDefault = null)
		{
			object r = get(keySession, nombre, valorDefault);
			bool? b = Utiles.toBool(r);
			return b;
			//return b ?? false;
		}
		
		public string getStr(string keySession, string nombre, string valorDefault = null)
		{
			object r = get(keySession, nombre, valorDefault);
			return r != null ? r.ToString() : null;
			//return b ?? false;
		}
		
		public int[] getArrgInt(string keySession, string nombre, int[] valorDefault = null)
		{
			List<object> lo = (List<object>)get(keySession, nombre, valorDefault);
			int[] lr = new int[lo.Count];
			for (int i = 0; i < lo.Count; i++) {
				lr[i] = inT(lo[i]);
                
			}
			return lr;
                
		}
    

		public double[] getArrgDou(string keySession, string nombre, double[] valorDefault = null)
		{
			List<object> lo = (List<object>)get(keySession, nombre, valorDefault);
			double[] lr = new double[lo.Count];
			for (int i = 0; i < lo.Count; i++) {
				lr[i] = dou(lo[i]);
                
			}
			return lr;
                
		}
    

		public string[] getArrgStr(string keySession, string nombre, string[] valorDefault = null)
		{
			List<object> lo = (List<object>)get(keySession, nombre, valorDefault);
			string[] lr = new string[lo.Count];
			for (int i = 0; i < lo.Count; i++) {
				lr[i] = to_String(lo[i]);
                
			}
			return lr;
                
		}
    

		public bool[] getArrgBool(string keySession, string nombre, bool[] valorDefault = null)
		{
			List<object> lo = (List<object>)get(keySession, nombre, valorDefault);
			bool[] lr = new bool[lo.Count];
			for (int i = 0; i < lo.Count; i++) {
				lr[i] = (bool)toBool(lo[i]);
                
			}
			return lr;
                
		}
		
		
		public List<int> getListInt(string keySession, string nombre, List<int> valorDefault = null)
		{
			List<object> lo = (List<object>)get(keySession, nombre, valorDefault);
			List<int> lr = new List<int>();
			for (int i = 0; i < lo.Count; i++) {
				lr.Add(inT(lo[i]));
			}
			return lr;
                
		}
    

		public List<double> getListDou(string keySession, string nombre, List<double> valorDefault = null)
		{
			List<object> lo = (List<object>)get(keySession, nombre, valorDefault);
			List<double> lr = new List<double>();
			for (int i = 0; i < lo.Count; i++) {
				lr.Add(dou(lo[i]));
			}
			return lr;
                
		}
    

		public List<string> getListStr(string keySession, string nombre, List<string> valorDefault = null)
		{
			List<object> lo = (List<object>)get(keySession, nombre, valorDefault);
			List<string> lr = new List<string>();
			for (int i = 0; i < lo.Count; i++) {
				lr.Add(to_String(lo[i]));
			}
			return lr;
                
		}
    

		public List<bool> getListBool(string keySession, string nombre, List<bool> valorDefault = null)
		{
			List<object> lo = (List<object>)get(keySession, nombre, valorDefault);
			List<bool> lr = new List<bool>();
			for (int i = 0; i < lo.Count; i++) {
				lr.Add((bool)toBool(lo[i]));
			}
			return lr;
                
		}
		
		
		
		private bool esLista(object o)
		{
			return o is List<object> || o is object[];
		}
		
		private void recorrerLista(object o, Action<object> usarO)
		{
			if (o is List<object>) {
				List<object> l = (List<object>)o;
				for (int i = 0; i < l.Count; i++) {
					usarO(l[i]);
				}
				
			} else if (o is object[]) {
				object[] l = (object[])o;
				for (int i = 0; i < l.Length; i++) {
					usarO(l[i]);
				}
			}
		}
		//////
	}
}
