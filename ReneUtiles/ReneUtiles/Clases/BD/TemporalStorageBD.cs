/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 21/8/2022
 * Hora: 16:23
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using ReneUtiles;
using ReneUtiles.Clases;
using System.Text.RegularExpressions;
namespace ReneUtiles.Clases.BD
{
	
	/// <summary>
	/// Description of TemporalStorageBD.
	/// </summary>
	public class TemporalStorageBD:ConsolaBasica
	{
		class _DatosDeColumna
		{
			public object nombre;
			public TipoDeDatoSQL tipo;
			public object value;
			public _DatosDeColumna(
				object nombre,
				TipoDeDatoSQL tipo,
				object value
			)
			{
				this.nombre = nombre;
				this.tipo = tipo;
				this.value = value;
			}
		}
		private string _nombre;
		private string _nombreTablaTipos;
		private string _nombreTablaLugaresDeTipoLista;
		private string _direccion;
		private BDConexion _conet;
		private List<_DatosDeColumna> _columnas;
		
		private static TipoDeDatoSQL _TIPO_LISTA = new TipoDeDatoSQL("LISTA", null);
		
		public TemporalStorageBD(string nombre = "TablaTemporalStorage", string direccion = null)
		{
			this._nombre = nombre;
			this._nombreTablaTipos = nombre + "Tipos";
			this._nombreTablaLugaresDeTipoLista = this._nombreTablaTipos + "DireccionesTipoLista";
			this._columnas = new List<_DatosDeColumna>();
			if (direccion == null) {
				direccion = Directory.GetCurrentDirectory();
			}
			this._direccion = direccion + "/BDTemporalStorage.sqlite";
			this._conet = BDConexion.getConexionSQL_LITE(this._direccion)
				.crearTablaSiNoExiste(this._nombre)
				.crearTablaSiNoExiste(this._nombreTablaTipos)
				.crearTablaSiNoExiste(this._nombreTablaLugaresDeTipoLista, "nombreTabla", "nombrePropiedad", "tipo");
			_leerDatosDeBD();
				
		}
		
		private void _leerDatosDeBD()
		{
			this._columnas = new List<_DatosDeColumna>();
			object[][] datos = this._conet.select_Todo(this._nombre);
			object[][] datosTipos = this._conet.select_Todo(this._nombreTablaTipos);
			for (int i = 0; i < datos.Length; i++) {
				object[] valores = datos[i];
				for (int j = 1; j < valores.Length; j++) {
					this._columnas.Add(new _DatosDeColumna(datosTipos[1][j], TipoDeDatoSQL.get(datosTipos[0][j]), datos[i][j]));   
				}
			}
			object[][] datosNombreTablasListas = this._conet.select_Todo(this._nombreTablaLugaresDeTipoLista);
			for (int i = 0; i < datosNombreTablasListas.Length; i++) {
				object[] datosTabla = datosNombreTablasListas[i];
				datos = this._conet.select_Todo(datosTabla[1].ToString());
				List<object> valores = new List<object>();
				foreach (object[] j in datos) {
					valores.Add(j[1]);
				}
//				if datosTabla[3]==_TIPO_TUPLA:
//                	valores=tuple(valores)

//Aqui transformar del tipo "tipos bd" y revisar tambien por "_TIPO_LISTA"
//para transformarlo en TipoDeDatoSQL
				string tipoSupuesto = datosTabla[3].ToString();
				TipoDeDatoSQL t = TipoDeDatoSQL.get(tipoSupuesto);
				if (t == null) {
					if (_TIPO_LISTA.valor == tipoSupuesto) {
						t = _TIPO_LISTA;
					}
				}//(TipoDeDatoSQL)datosTabla[3]
				this._columnas.Add(new _DatosDeColumna(datosTabla[2], t, valores));
				
			}
			
		}
		
		private void _addColumnaDeSerNecesario(object nombre, object value)
		{
			TipoDeDatoSQL tipo = TipoDeDatoSQL.getTipoDeDatoSQL(value);
			if (tipo == null) {
				if (esLista(value)) {
					tipo = _TIPO_LISTA;
				}
//				elif esTupla(value):
//                	tipo = _TIPO_TUPLA
				
			}
			
			if (tipo != null) {
				bool existe = false;
				int indice = this._columnas.Count;
				for (int i = 0; i < indice; i++) {
					if (this._columnas[i].nombre.ToString() == nombre.ToString()) {
						indice = i;
						existe = true;
						break;
					}
				}
				if (!existe) {
					this._columnas.Add(new _DatosDeColumna(nombre, tipo, value));
				} else {
					this._columnas[indice].value = value;
					this._columnas[indice].tipo = tipo;
				}
			}
		}
		private void _updateTabla()
		{
			List<object> sql = new List<object>();
			List<object> insert = new List<object>();
			List<object> sqlTipos = new List<object>();
			List<object> insertTipos = new List<object>();
			List<object> insertNombres = new List<object>();
			
			List<int> indicesListasYTuplas = new List<int>();
			
			int indice = 0;
			foreach (_DatosDeColumna i in this._columnas) {
				if (i.tipo == _TIPO_LISTA) {
					indicesListasYTuplas.Add(indice);
					indice += 1;
					continue;
				}
				
				sql.Add(i.nombre);
				sql.Add(i.tipo);
				insert.Add(i.value);
				sqlTipos.Add(i.nombre);
				sqlTipos.Add(TipoDeDatoSQL.VARCHAR);
            	
				if (TipoDeDatoSQL.esTipoDeDatoSQL(i.tipo)) {
					insertTipos.Add(i.tipo.getValor());
				} else {
					insertTipos.Add("None");
				}
				insertNombres.Add(i.nombre);
				indice += 1;
			}
			foreach (object o in sql) {
				cwl("o=" + o);
			}
			object[] ob = sql.ToArray();
			this._conet.crearTablaYBorrarSiExiste(this._nombre, ob);
			if (insert.Count != 0) {
				this._conet.insertar(this._nombre, insert.ToArray());
			}
			this._conet.crearTablaYBorrarSiExiste(this._nombreTablaTipos, sqlTipos.ToArray());
			if (insert.Count != 0) {
				this._conet.insertar(this._nombreTablaTipos, insertTipos.ToArray());
				this._conet.insertar(this._nombreTablaTipos, insertNombres.ToArray());
			}
			this._conet.crearTablaYBorrarSiExiste(this._nombreTablaLugaresDeTipoLista, "nombreTabla", "nombrePropiedad", "tipo");
			foreach (int i in indicesListasYTuplas) {
				_DatosDeColumna col = this._columnas[i];
				string nombreTabla = this._nombre + col.nombre + "TablaList";
				this._conet.insertar(this._nombreTablaLugaresDeTipoLista, nombreTabla, col.nombre, col.tipo);
				TipoDeDatoSQL tipo = TipoDeDatoSQL.VARCHAR;
				List<object> lo = ((List<object>)col.value);
				//if(col.value!=null&&col.ToString().Length!=0){
				//if(col.value!=null&&(col.value is List<object>)&&((List<object>)col.value).Count!=0){
				if (lo.Count != 0) {
					tipo = TipoDeDatoSQL.getTipoDeDatoSQL(lo[0]);
					if (tipo == null) {
						tipo = TipoDeDatoSQL.VARCHAR;
					}
				}
				this._conet.crearTablaYBorrarSiExiste(nombreTabla, "valor", tipo);
				foreach (object j in lo) {
					this._conet.insertar(nombreTabla, j);
				}
			}
		}
		
		private bool esLista(object a)
		{
			return a is List<object>;
		}
		
		public TemporalStorageBD put(params object[] paresNombreValue)
		{
			if (paresNombreValue.Length % 2 == 0) {
				object nombre = null;
				int pos = 0;
				foreach (object i in paresNombreValue) {
					if (pos == 0) {
						nombre = i;
					} else if (pos == 1) {
						_addColumnaDeSerNecesario(nombre, i);
					}
					pos = (pos + 1) % 2;
				}
				_updateTabla();
			}
			return this;
		}
		
		
		public object get(string nombre, object valorDefault = null)
		{
			foreach (_DatosDeColumna i in this._columnas) {
				if (i.nombre.ToString() == nombre) {
					return i.value;
				}
			}
			if (valorDefault != null) {
				return valorDefault;
			}
			return valorDefault;
		}
		public int? getInt(string nombre, int? valorDefault = null)
		{
			object r = get(nombre, valorDefault);
			return inT(r.ToString());
		}
		public double? getDouble(string nombre, double? valorDefault = null)
		{
			object r = get(nombre, valorDefault);
			return dou(r.ToString());
		}
		public bool? getBool(string nombre, bool? valorDefault = null)
		{
			object r = get(nombre, valorDefault);
			bool? b = Utiles.toBool(r);
			return b;
			//return b ?? false;
		}
		
		public string getStr(string nombre, string valorDefault = null)
		{
			object r = get(nombre, valorDefault);
			return r != null ? r.ToString() : null;
			//return b ?? false;
		}
		
		
		
		public List<int> getListInt(string nombre, List<int> valorDefault = null)
		{
			List<object> lo = (List<object>)get(nombre, valorDefault);
			List<int> lr = new List<int>();
			for (int i = 0; i < lo.Count; i++) {
				lr.Add(inT(lo[i]));
			}
			return lr;
                
		}
    

		public int[] getArrgInt(string nombre, int[] valorDefault = null)
		{
			List<object> lo = (List<object>)get(nombre, valorDefault);
			int[] lr = new int[lo.Count];
			for (int i = 0; i < lo.Count; i++) {
				lr[i] = inT(lo[i]);
                
			}
			return lr;
                
		}
    

		public List<double> getListDou(string nombre, List<double> valorDefault = null)
		{
			List<object> lo = (List<object>)get(nombre, valorDefault);
			List<double> lr = new List<double>();
			for (int i = 0; i < lo.Count; i++) {
				lr.Add(dou(lo[i]));
			}
			return lr;
                
		}
    

		public double[] getArrgDou(string nombre, double[] valorDefault = null)
		{
			List<object> lo = (List<object>)get(nombre, valorDefault);
			double[] lr = new double[lo.Count];
			for (int i = 0; i < lo.Count; i++) {
				lr[i] = dou(lo[i]);
                
			}
			return lr;
                
		}
    

		public List<string> getListStr(string nombre, List<string> valorDefault = null)
		{
			List<object> lo = (List<object>)get(nombre, valorDefault);
			List<string> lr = new List<string>();
			for (int i = 0; i < lo.Count; i++) {
				lr.Add(to_String(lo[i]));
			}
			return lr;
                
		}
    

		public string[] getArrgStr(string nombre, string[] valorDefault = null)
		{
			List<object> lo = (List<object>)get(nombre, valorDefault);
			string[] lr = new string[lo.Count];
			for (int i = 0; i < lo.Count; i++) {
				lr[i] = to_String(lo[i]);
                
			}
			return lr;
                
		}
    

		public List<bool> getListBool(string nombre, List<bool> valorDefault = null)
		{
			List<object> lo = (List<object>)get(nombre, valorDefault);
			List<bool> lr = new List<bool>();
			for (int i = 0; i < lo.Count; i++) {
				lr.Add((bool)toBool(lo[i]));
			}
			return lr;
                
		}
    

		public bool[] getArrgBool(string nombre, bool[] valorDefault = null)
		{
			List<object> lo = (List<object>)get(nombre, valorDefault);
			bool[] lr = new bool[lo.Count];
			for (int i = 0; i < lo.Count; i++) {
				lr[i] = (bool)toBool(lo[i]);
                
			}
			return lr;
                
		}
		
		
		public bool contiene(string key)
		{
			foreach (_DatosDeColumna d in this._columnas) {
				if (d.nombre.ToString() == key) {
					return true;
				}
			}
			return false;
		}
		
		public TemporalStorageBD clear()
		{
			this._conet.crearTablaYBorrarSiExiste(this._nombre).crearTablaYBorrarSiExiste(this._nombreTablaTipos).crearTablaYBorrarSiExiste(this._nombreTablaLugaresDeTipoLista, "nombreTabla", "nombrePropiedad", "tipo");
			this._columnas.Clear();
			return this;
		}
	}
}
