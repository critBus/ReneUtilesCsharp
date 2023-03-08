/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 26/3/2022
 * Hora: 12:09
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
//using ReneUtiles.Clases.BD.Factory.Python;
using ReneUtiles.Clases.BD.Factory.Consultas;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
using ReneUtiles.Clases.BD.Conexion;

namespace ReneUtiles.Clases.BD.Factory
{
	/// <summary>
	/// Description of CodeBDLenguaje.
	/// </summary>
	public abstract class CodeBDLenguaje:BasicoFactory
	{
		public FactoryBD factory;
		//public DatosDeBDConect datosBDConect;
		public string Extencion;
		public bool UsaUnArchivoParaTodosLosModelosJuntos=false;

        // public bool UtilizaInterfasAdmin;

        public DatosDeConexionFactoryBD datosConexionFactory;

        public CodeBDLenguaje(FactoryBD factory, DatosDeConexionFactoryBD datosConexionFactory) {
            this.factory = factory;
            this.datosConexionFactory = datosConexionFactory;
        }

        public DatosBDConect getDBC()
        {
            return this.datosConexionFactory.datosDBConect;
        }
        public DatosDeBDConect getDSC()
        {
            return this.datosConexionFactory.datosDelCodigoDelBDConector;
        }

        

        public virtual string getStrArchivoTodosLosModelosJuntos(int separacion0)
		{
			return "";
		}
		
		public abstract string getStrModelo(ModeloBD m, EsquemaBD E, int separacion0);
		public abstract string getStrBD(int separacion0);
		
		//Metodo getUrlBD
		public virtual string getNombreMetodoUrlBD()
		{
			return "getUrlBD";
		}

        //Metodo crearTabla
        public abstract string getStrMetodoGetSesionStorage(int separacion0);
        public abstract string getStrMetodoCrearTodasLasTablas( int separacion0);
        public abstract string getStrMetodoCrearTodasLasTablasSiNoExisten(int separacion0);

        public abstract  string getStrMetodoCrearTabla(ModeloBD m, int separacion0);
		public virtual  string getNombreMetodoCrearTabla(ModeloBD m)
		{
			return "crearTabla" + this.getNombreStrModelo(m);
		}
		
		
		//Metodo crearTablaSiNoExiste
		public abstract  string getStrMetodoCrearTablaSiNoExiste(ModeloBD m, int separacion0);
		public virtual  string getNombreMetodoCrearTablaSiNoExiste(ModeloBD m)
		{
			return "crearTabla" + this.getNombreStrModelo(m)+"SiNoExiste";
		}
		
		
		//Metodo GetArgs
		public abstract string getStrMetodoGetArgs(ModeloBD m, int separacion0);
		public virtual  string getNombreMetodo_getArgs(ModeloBD m)
		{
			string nombreModelo = this.getNombreStrModelo(m);
			return "get" + nombreModelo + "_Args";
		}
		
		//Metodo ContentArgs
		public abstract string getStrMetodoContentArgs(ModeloBD m, int separacion0);
		
		//Metodo GetForID
		public abstract string getStrMetodoGetForID(ModeloBD_ID m, int separacion0);
		public virtual  string getNombreMetodo_GetForID(ModeloBD m)
		{
			string nombreModelo = this.getNombreStrModelo(m);
			return "get" + nombreModelo + "_id";
		}
		
		//Metodo Insertar
		public abstract string getStrMetodoInsertar(ModeloBD_ID m, int separacion0);
		public virtual  string getNombreMetodo_insertar(ModeloBD m)
		{
			string nombreModelo = this.getNombreStrModelo(m);
			return "insertar" + nombreModelo;
		}
		
		//Metodo GetAll
		public abstract string getStrMetodoGetAll(ModeloBD_ID m, int separacion0);
		public virtual  string getNombreMetodoGetAll(ModeloBD m)
		{
			return "get" + this.getNombreStrModelo(m) + "_All";
		}
		
		//Metodo Update
		public abstract string getStrMetodoUpdate(ModeloBD_ID m, int separacion0);
		public virtual  string getNombreMetodoUpdate(ModeloBD m)
		{
			return "update" + this.getNombreStrModelo(m) + "";
		}
		
		//Metodo DeleteForID
		public abstract string getStrMetodoDeleteForID(ModeloBD_ID m, int separacion0);
		public virtual  string getNombreMetodoDeleteForID(ModeloBD m)
		{
			return "delete" + this.getNombreStrModelo(m) + "_ForId";
		}
		
		//Metodo GetAll_ForColumna
		public abstract string getStrMetodoGetAll_ForColumna(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0);
		public virtual  string getNombreMetodoGetAll_ForColumna(ModeloBD m, ColumnaDeModeloBD c)
		{
			return "get" + this.getNombreStrModelo(m) + "_All_" + CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m, c);
		}
		
		//Metodo Get_ForColumna
		public abstract string getStrMetodoGet_ForColumna(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0);
		public virtual  string getNombreMetodoGet_ForColumna(ModeloBD m, ColumnaDeModeloBD c)
		{
			return "get" + this.getNombreStrModelo(m) + "_For_" + CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m, c);
		}
		
		//Metodo Delete_ForColumna
		public abstract string getStrMetodoDelete_ForColumna(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0);
		public virtual  string getNombreMetodoDelete_ForColumna(ModeloBD m, ColumnaDeModeloBD c)
		{
			return "delete" + this.getNombreStrModelo(m) + "_For_" + CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m, c);
		}
		
		//Metodo GetAll_ForListaDeColumnas
		public abstract string getStrMetodoGetAll_ForListaDeColumnas(ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0);
		public virtual  string getNombreMetodoGetAll_ForListaDeColumnas(ModeloBD m, List<ColumnaDeModeloBD> C)
		{
			string r = "get" + this.getNombreStrModelo(m) + "_All";
			for (int i = 0; i < C.Count; i++) {
				r += "_" + CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m, C[i]);
			}
			return r;
		}
		
		//Metodo Get_ForListaDeColumnas
		public abstract string getStrMetodoGet_ForListaDeColumnas(string nombreDelMetodo, ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0);
		public virtual string getStrMetodoGet_ForListaDeColumnas(ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0)
		{
			return getStrMetodoGet_ForListaDeColumnas(getNombreMetodoGet_ForListaDeColumnas(m, C), m, C, separacion0);
		}
		public virtual  string getNombreMetodoGet_ForListaDeColumnas(ModeloBD m, List<ColumnaDeModeloBD> C)
		{
			string r = "get" + this.getNombreStrModelo(m) + "_For";
			for (int i = 0; i < C.Count; i++) {
				//r+="_"+CodeBDLenguaje.getNombreStrElementoCapitalice(C[i]);
				r += "_" + CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m, C[i]);
			}
			return r;
		}
		
		//Metodo Delete_ForListaDeColumnas
		public abstract string getStrMetodoDelete_ForListaDeColumnas(ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0);
		public virtual  string getNombreMetodoDelete_ForListaDeColumnas(ModeloBD m, List<ColumnaDeModeloBD> C)
		{
			string r = "delete" + this.getNombreStrModelo(m) + "_For";
			for (int i = 0; i < C.Count; i++) {
				r += "_" + CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m, C[i]);
			}
			return r;
		}
		
		//Metodo GetListaDe_OneToManyLinkInterno
		public virtual string getStrMetodoGetListaDe_OneToManyLinkInterno(OneToMany o, int separacion0)
		{
			ModeloBD m = o.Many;
			ColumnaDeModeloBD c = o.LinkToOne;
			return getStrMetodoGetAll_ForColumna((ModeloBD_ID)m, c, separacion0);
			
		}
		public virtual  string getNombreMetodoGetListaDe_OneToManyLinkInterno(OneToMany o)
		{
			if (o.TieneUnNombreAutomatico) {
				return getNombreMetodoGetAll_ForColumna(o.Many, o.LinkToOne);
			}
			
			return "get" +o.Nombre;
			//return "getListaDe_"+this.getNombreStrModelo(o.Many);
			
		}
		
		//Metodo GetAll_InnerJoin_ForListaDeColumnas
		public virtual string getStrMetodoGetAll_InnerJoin_ForListaDeColumnas(InnerJoin I, int separacion0)
		{
			return getStrMetodoGetAll_InnerJoin_ForListaDeColumnas(I.ModeloDestino, I.Cadena, I.ElementosWhere, separacion0);
		}
		public abstract string getStrMetodoGetAll_InnerJoin_ForListaDeColumnas(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0);
		public virtual string getStrMetodoGetAll_InnerJoin_ForListaDeColumnas_Link(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0)
		{
			return getStrMetodoGetAll_InnerJoin_ForListaDeColumnas(m, __getCadenaRealDeCadenaLink(cadena), elementosWhere, separacion0);
		}
		public virtual  string getNombreMetodoGetAll_InnerJoin_ForListaDeColumnas(ModeloBD m, List<ElementoPorElQueBuscar> C)
		{
			return getNombreMetodoGetAll_ForListaDeElementos(m, C);
		}
		
		//Metodo GetListaDe_OneToManyTablaExterna
		public virtual string getStrMetodoGetListaDe_OneToManyTablaExterna(OneToMany_EnTablaExterna o, int separacion0)
		{
			ModeloBD_ID m = (ModeloBD_ID)o.Many;
			List<ColumnaDeModeloBD> u = o.Union.Columnas;
			ModeloBD on = o.One;
			
			return getStrMetodoGetAll_InnerJoin_ForListaDeColumnas_Link(m, listE(u[0], u[1]), listE(o.One), separacion0);
			
			
		}
		public virtual  string getNombreMetodoGetListaDe_OneToManyEnTablaExterna(OneToMany_EnTablaExterna o)
		{
			if (o.Union.TieneUnNombreAutomatico) {
				return "get" + this.getNombreStrModelo(o.Many) + "_All";
			}
			
			return "get" + o.Union.Nombre;
		}
		
		//Metodos ManyToMany
		public virtual string getStrMetodosManyToMany(ManyToMany o, int separacion0)
		{
			ModeloBD_ID m1 = o.Many_1;
			ModeloBD_ID m2 = o.Many_2;
			List<ColumnaDeModeloBD> lc = o.Union.Columnas;
			
			metodoCreador2<ModeloBD_ID,ModeloBD_ID,string> getMetodoInner = (mReturn, mKey) =>
            {
                string r = "";
               // r += "\n// fue por aqui :" + mReturn.Nombre + " ---- " + mKey.Nombre;
                r += getStrMetodoGetAll_InnerJoin_ForListaDeColumnas_Link(mReturn, listE(lc[0], lc[1]), listE(mKey), separacion0);
               // r += "\n//-----------++++++++++++---------------";
                return r;
            };
			string mr = "";//"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!s";
			mr += getMetodoInner(m1, m2);
			mr += "\n";//"\n !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
			mr += getMetodoInner(m2, m1);
			return mr;
			
			
		}
		public virtual  string getNombreMetodoGetListaDe_ManyToMany(ManyToMany o, ModeloBD mDestino)
		{
			//return "getListaDe_"+this.getNombreStrModelo(o.Many);
			if (o.Union.TieneUnNombreAutomatico) {
				return "get" + this.getNombreStrModelo(mDestino) + "_All";
				//return getNombreMetodoGetAll_ForColumna(mDestino,o.Union.Columnas[0]);
			}
			
			//Aqui
			return "get" + o.Union.Nombre;
		}
		
		
		//Metodo Get_InnerJoin_ForListaDeColumnas
		public virtual string getStrMetodoGet_InnerJoin_ForListaDeColumnas(InnerJoin I, int separacion0)
		{
			return getStrMetodoGet_InnerJoin_ForListaDeColumnas(I.ModeloDestino, I.Cadena, I.ElementosWhere, separacion0);
		}
		public abstract string getStrMetodoGet_InnerJoin_ForListaDeColumnas(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0);
		public virtual  string getNombreMetodoGet_InnerJoin_ForListaDeColumnas(ModeloBD m, List<ElementoPorElQueBuscar> C)
		{
			return getNombreMetodoGet_ForListaDeElementos(m, C);
		}
		public virtual string getStrMetodoGet_InnerJoin_ForListaDeColumnas_Link(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0)
		{

			return getStrMetodoGet_InnerJoin_ForListaDeColumnas(m, __getCadenaRealDeCadenaLink(cadena), elementosWhere, separacion0);
		}
		
		//Metodo DeleteCascade DeleteForID
		public abstract string getStrMetodoDeleteForID_Cascade(ModeloBD_ID m, EsquemaBD E, int separacion0);
		public virtual  string getNombreMetodoDeleteForID_Cascade(ModeloBD m)
		{
			return getNombreMetodoDeleteForID(m) + "_CASCADE";
		
		}
		
		//Metodo DeleteCascade ForColumna
		public virtual  string getNombreMetodoDelete_ForColumna_Cascade(ModeloBD m, ColumnaDeModeloBD c)
		{
			return getNombreMetodoDelete_ForColumna(m, c) + "_CASCADE";
		}
		public abstract string getStrMetodoDelete_ForColumna_Cascade(ModeloBD_ID m, ColumnaDeModeloBD c, EsquemaBD E, int separacion0);
		
		//Metodo DeleteCascade ForListaDeColumnas
		public abstract string getStrMetodoDelete_ForListaDeColumnas_Cascade(ModeloBD_ID m, List<ColumnaDeModeloBD> C, EsquemaBD E, int separacion0);
		public virtual  string getNombreMetodoDelete_ForListaDeColumnas_Cascade(ModeloBD m, List<ColumnaDeModeloBD> C)
		{
			return getNombreMetodoDelete_ForListaDeColumnas(m, C) + "_CASCADE";
		}
		
		//Metodo Existe
		public virtual  string getNombreMetodoExiste(ModeloBD m, ColumnaDeModeloBD c, bool soloHayEsteEnElModelo)
		{
			return "existe" + this.getNombreStrModelo(m) + (soloHayEsteEnElModelo ? "" : "_For_" + CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m, c));
		}
		public abstract string getStrMetodoExiste(ModeloBD_ID m, ColumnaDeModeloBD c, bool soloHayEsteEnElModelo, int separacion0);
		
		//Metodo Existe ForListaDeColumnas
		public virtual  string getNombreMetodoExiste_ForListaDeColumnas(ModeloBD m, List<ColumnaDeModeloBD> C, bool soloHayEsteEnElModelo)
		{
			string r = "existe" + this.getNombreStrModelo(m);
			if (!soloHayEsteEnElModelo) {
				for (int i = 0; i < C.Count; i++) {
					r += "_" + CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m, C[i]);
				}
			}
			return r;
			//return +(soloHayEsteEnElModelo?"":"_For_"+CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m,c));
		}
		public abstract string getStrMetodoExiste_ForListaDeColumnas(string nombreMetodo, ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0);
		public virtual string getStrMetodoExiste_ForListaDeColumnas(ModeloBD_ID m, List<ColumnaDeModeloBD> C, bool soloHayEsteEnElModelo, int separacion0)
		{
			return getStrMetodoExiste_ForListaDeColumnas(getNombreMetodoExiste_ForListaDeColumnas(m, C, soloHayEsteEnElModelo), m, C, separacion0);
		}
		
		//Metodo AddMany Many dentro de modelo | OneToManyTablaExterna
		public virtual  string getNombreMetodoAddMany_OneToManyEnTablaExterna(OneToMany_EnTablaExterna o)
		{
			if (o.Union.TieneUnNombreAutomatico) {
				return "add" + this.getNombreStrModelo(o.Many);
			}
			
			return "add" + this.getNombreStrModelo(o.Union);
		}
		
		//Metodo AddMany OneToMany dentro de modelo | OneToMany
		public virtual  string getNombreMetodoAddMany_OneToMany(OneToMany o)
		{
			if (o.TieneUnNombreAutomatico) {
				return "add" + this.getNombreStrModelo(o.Many);
			}
			
			return "add" +o.Nombre;
		}
		
		//Metodo AddMany OneToMany dentro de modelo | OneToMany
		public virtual  string getNombreMetodoAddMany_OneToManySort(OneToManySort o)
		{
			if (o.TieneUnNombreAutomatico) {
				return "add" + this.getNombreStrModelo(o.Many);//+"_Sort"
			}
			
			return "add" +o.Nombre;//+"_Sort"
		}
		
		//Metodo Get_OneToManyTablaExterna
		//Get dentro del modelo
		public virtual  string getNombreMetodoGet_OneToManyEnTablaExterna(OneToMany_EnTablaExterna o)
		{
			if (o.Union.TieneUnNombreAutomatico) {
				return "get" + this.getNombreStrModelo(o.Many);
			}
			
			return "get" + this.getNombreStrModelo(o.Union);
		}
		//		public virtual string getStrMetodoGet_OneToManyTablaExterna(OneToMany_EnTablaExterna o, int separacion0)
		//		{
		//			ModeloBD_ID m = (ModeloBD_ID)o.Many;
		//			List<ColumnaDeModeloBD> ul = o.Union.Columnas;
		//			ModeloBD on = o.One;
		//
		//			return getStrMetodoGet_ForListaDeColumnas(getNombreMetodoGet_OneToManyEnTablaExterna(o), o.Union, ul, separacion0);
		//
		//
		//		}
		
		//Get EnBD por sus columnas OneToManyTablaExterna
		public virtual  string getNombreMetodoGet_EnBD_OneToManyEnTablaExterna(OneToMany_EnTablaExterna o)
		{
//			if (o.Union.TieneUnNombreAutomatico) {
//				return "get" + this.getNombreStrModelo(o.Many);
//			}
			
			return "get" + this.getNombreStrModelo(o.Union);
		}
		public virtual string getStrMetodoGet_EnBD_OneToManyTablaExterna(OneToMany_EnTablaExterna o, int separacion0)
		{
			ModeloBD_ID m = (ModeloBD_ID)o.Many;
			List<ColumnaDeModeloBD> ul = o.Union.Columnas;
			ModeloBD on = o.One;
			
			return getStrMetodoGet_ForListaDeColumnas(getNombreMetodoGet_EnBD_OneToManyEnTablaExterna(o), o.Union, ul, separacion0);
			
			
		}
		
		//Metodo Existe OneToManyTablaExterna
		public virtual  string getNombreMetodoExiste_OneToManyEnTablaExterna(OneToMany_EnTablaExterna o)
		{
			if (o.Union.TieneUnNombreAutomatico) {
				return "existe" + this.getNombreStrModelo(o.One) + "_And_" + this.getNombreStrModelo(o.Many);
			}
			
			return "existe" + this.getNombreStrModelo(o.Union);
		}
		public virtual string getStrMetodoExiste_OneToManyEnTablaExterna(OneToMany_EnTablaExterna o, int separacion0)
		{
			return getStrMetodoExiste_ForListaDeColumnas(getNombreMetodoExiste_OneToManyEnTablaExterna(o), o.Union, o.Union.Columnas, separacion0);
		}
		
		
		//Metodo AddManyToMany Many dentro de modelo | ManyToMany
		public virtual  string getNombreMetodoAddMany_ManyToMany(ManyToMany o, ModeloBD m)
		{
			if (o.Union.TieneUnNombreAutomatico) {
				return "add" + this.getNombreStrModelo(o.Many_1 == m ? o.Many_2 : o.Many_1);
			}
			
			return "add" + this.getNombreStrModelo(o.Union);
			;
		}
		
		//Metodo Get_ManyToMany
		//Get dentro del modelo
		public virtual  string getNombreMetodoGet_ManyToMany(ManyToMany o, ModeloBD m)
		{
			if (o.Union.TieneUnNombreAutomatico) {
				return "get" + this.getNombreStrModelo(o.Many_1 == m ? o.Many_2 : o.Many_1);
			}
			
			return "get" + this.getNombreStrModelo(o.Union);
			;
		}
		//		public virtual string getStrMetodoGet_ManyToMany(ManyToMany o, int separacion0)
		//		{
		//			//ModeloBD_ID m = (ModeloBD_ID)o.Many;
		//			List<ColumnaDeModeloBD> ul = o.Union.Columnas;
		//			//ModeloBD on = o.One;
		//
		//			return getStrMetodoGet_ForListaDeColumnas(getNombreMetodoGet_ManyToMany(o), o.Union, ul, separacion0);
		//
		//
		//		}
		
		
		//Metodo Existe ManyToMany
		public virtual  string getNombreMetodoExiste_ManyToMany(ManyToMany o)
		{
			if (o.Union.TieneUnNombreAutomatico) {
				return "existe" + this.getNombreStrModelo(o.Many_1) + "_And_" + this.getNombreStrModelo(o.Many_2);
			}
			
			return "existe" + this.getNombreStrModelo(o.Union);
			;
		}
		public virtual string getStrMetodoExiste_ManyToMany(ManyToMany o, int separacion0)
		{
			return getStrMetodoExiste_ForListaDeColumnas(getNombreMetodoExiste_ManyToMany(o), o.Union, o.Union.Columnas, separacion0);
		}
		
		//Get EnBD por sus columnas ManyToMany
		public virtual  string getNombreMetodoGet_EnBD_ManyToMany(ManyToMany o)
		{
//			if (o.Union.TieneUnNombreAutomatico) {
//				return "get" + this.getNombreStrModelo(o.Many);
//			}
			
			return "get" + this.getNombreStrModelo(o.Union);
			;
		}
		public virtual string getStrMetodoGet_EnBD_ManyToMany(ManyToMany o, int separacion0)
		{
			//ModeloBD_ID m = (ModeloBD_ID)o.Many;
			List<ColumnaDeModeloBD> ul = o.Union.Columnas;
			//ModeloBD on = o.One;
			
			return getStrMetodoGet_ForListaDeColumnas(getNombreMetodoGet_EnBD_ManyToMany(o), o.Union, ul, separacion0);
			
			
		}
		
		
		//save dentro de modelo
		public virtual  string getNombreMetodoSave(ModeloBD m)
		{
			string nombreModelo = this.getNombreStrModelo(m);
			return "s";
		}
		
		//Existe GetForID
		public abstract string getStrMetodoExiste_ForID(ModeloBD_ID m, int separacion0);
		public virtual  string getNombreMetodoExiste_ForID(ModeloBD m)
		{
			string nombreModelo = this.getNombreStrModelo(m);
			return "existe" + nombreModelo + "_id";
		}
		
		//Delete dentro de modelo
		public virtual  string getNombreMetodoDelete_EnModelo(ModeloBD m)
		{
			string nombreModelo = this.getNombreStrModelo(m);
			return "d";
		}
		
		
		//Metodo GetAll_ForListaDeColumnas_Sort
		public abstract string getStrMetodoGetAll_ForListaDeColumnas_Sort(SelectWhereSort s, int separacion0);
		public virtual  string getNombreMetodoGetAll_ForListaDeColumnas_Sort(SelectWhereSort s)
		{
			string r = "get" + this.getNombreStrModelo(s.Modelo) + "_All";
			for (int i = 0; i < s.ListaPorLasQueBuscar.Count; i++) {
				ElementoPorElQueBuscar e = s.ListaPorLasQueBuscar[i];
				
				if (e is ColumnaDeModeloBD) {
					r += "_" + CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(s.Modelo, (ColumnaDeModeloBD)e);
				} else {
					if (e is ModeloBD) {
						r += "_" + getNombreStrModelo((ModeloBD)e);
					} else {
						return null;
					}
				}
				
			}
			r += "_Sort";
			for (int i = 0; i < s.ListaPorLasQueOrdenar.Count; i++) {
				r += "_" + CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(s.Modelo, s.ListaPorLasQueOrdenar[i].Columna);
			}
			return r;
		}
		
		//Metodo GetListaDe_OneToManyLinkInterno_Sort
		public virtual string getStrMetodoGetListaDe_OneToManyLinkInterno_Sort(OneToManySort o, int separacion0)
		{
			
			return getStrMetodoGetAll_ForListaDeColumnas_Sort(o.Sort, separacion0);
			
		}
		public virtual  string getNombreMetodoGetListaDe_OneToManyLinkInterno_Sort(OneToManySort o)
		{
			if (o.Nombre == null) {
				string r = "get" + this.getNombreStrModelo(o.Many);
				r += "_All_Sort";
				for (int i = 0; i < o.Sort.ListaPorLasQueOrdenar.Count; i++) {
					r += "_" + CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(o.Sort.Modelo, o.Sort.ListaPorLasQueOrdenar[i].Columna);
				}
				return r;
			}
			return o.Nombre;
			//return "getListaDe_"+this.getNombreStrModelo(o.Many);
			//return getNombreMetodoGetAll_ForColumna(o.Many, o.LinkToOne);
		}
		
		
		//protected abstract string __getStrDeletes_Cascade(ModeloBD m,Dictionary<ModeloBD,CrearDeleteCascade> listaCrearDeleteCascade,int separacion0);

		//Nombres de Metodos ********************
		public virtual  string getNombreMetodoGetListaDe(ModeloBD m)
		{
			return "getListaDe_" + this.getNombreStrModelo(m);
		}
		public virtual  string getNombreMetodoGet_ForListaDeElementos(ModeloBD m, List<ElementoPorElQueBuscar> C)
		{
			string r = "get" + this.getNombreStrModelo(m) + "_For";
			for (int i = 0; i < C.Count; i++) {
				r += "_" + getNombreStrElementoCapitalice(C[i]);
				//r+="_"+CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m,C[i]);
			}
			return r;
		}
		public virtual  string getNombreMetodoGetAll_ForListaDeElementos(ModeloBD m, List<ElementoPorElQueBuscar> C)
		{
			string r = "get" + this.getNombreStrModelo(m) + "_All";
			for (int i = 0; i < C.Count; i++) {
				r += "_" + getNombreStrElementoCapitalice(C[i]);
				//r+="_"+CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m,C[i]);
			}
			return r;
		}
		public virtual  string getNombreMetodoCrearTodasLasTablas()
		{
			return "crearTodasLasTablas";
		}
		public virtual  string getNombreMetodoCrearTodasLasTablasSiNoExisten()
		{
			return "crearTodasLasTablasSiNoExisten";
		}



        
		
		protected abstract string __getStrInnerJoin(string nombreMetodoBD, ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0);
		protected virtual string __getStrInnerJoinFirstRow(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0)
		{
			return __getStrInnerJoin(getDSC().NombreMetodoSelectWhereInnerJoinFirstRow, m, cadena, elementosWhere, separacion0);
		}
		protected virtual string __getStrInnerJoinAll(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0)
		{
			return __getStrInnerJoin(getDSC().NombreMetodoSelectWhereInnerJoin, m, cadena, elementosWhere, separacion0);
		}
		
		
		protected List<ElementoPorElQueBuscar> __getCadenaRealDeCadenaLink(List<ElementoPorElQueBuscar> cadena)
		{
			List<ElementoPorElQueBuscar> cadenaReal = new List<ElementoPorElQueBuscar>();
			for (int i = 0; i < cadena.Count; i++) {
				ElementoPorElQueBuscar e = cadena[i];
				
				cadenaReal.Add(e);
				if (e is ColumnaDeModeloBD) {
					ColumnaDeModeloBD c = (ColumnaDeModeloBD)e;
					if (c.EsReferencia) {
						ModeloBD referencia = c.ReferenciaID;
						cadenaReal.Add(referencia);
					}
				}
			}
			return cadenaReal;
		}
		
		
		
		
		
		public virtual string getNombreSuperclaseModelo()
		{
			return "ModeloDeApiBD";
		}
		
		
		
		public virtual  string getSeparacionln(int indice, int separacion0)
		{
			return CodeBDLenguaje.getSeparacionlnDefualt(indice, separacion0);
//			string r = "\n";
//			for (int i = 0; i < indice + separacion0; i++) {
//				r += "\t";
//			}
//			return r;
		}
		public virtual  string getSeparacion(int indice)
		{
			string r = "";
			for (int i = 0; i < indice; i++) {
				r += "\t";
			}
			return r;
		}
		
		public static  string getSeparacionlnDefualt(int indice, int separacion0)
		{
			string r = "\n";
			for (int i = 0; i < indice + separacion0; i++) {
				r += "\t";
			}
			return r;
		}
		
		//otros
		public  string getNombreStrIdkeyModelo(ModeloBD m)
		{
            string prefijo = "idkey_";
            string r = Utiles.llevarASingular(Utiles.getLowerPiso(m.Nombre
                                                              .Replace("TABLA_", prefijo)
            ));
            if (!r.StartsWith(prefijo)) {
                //if (r.Contains("id")) {
                //    string nombreVariableModelo = CodeBDLenguaje.getNombreStrModeloLower(m);
                //    if (r== nombreVariableModelo + "id") {
                //        r = subs(r, 0, r.Length - 2);
                //    }
                //}
                r = prefijo + r;

                
                
            }
            return r;
		}
		public string getNombreVariableElemento(ElementoPorElQueBuscar t)
		{
			if (t is ModeloBD) {
				ModeloBD m = (ModeloBD)t;
				return getNombreStrIdkeyModelo(m);
					
			} else if (t is ColumnaDeModeloBD) {
				ColumnaDeModeloBD c = (ColumnaDeModeloBD)t;
				return getNombreStrColumnaModelo(c);
			}
			return null;
		}
		
		public  string getNombreStrElementoCapitalice(ElementoPorElQueBuscar t)
		{	
			return Utiles.capitalize(getNombreVariableElemento(t));
		}
		
		
		public ModeloBD getModeloDeElemento(ElementoPorElQueBuscar t)
		{
//			cwl("t="+t);
			if (t is ModeloBD) {
//				cwl("retorno modelo");
				ModeloBD m = (ModeloBD)t;
				return m;
					
			} else if (t is ColumnaDeModeloBD) {
				ColumnaDeModeloBD c = (ColumnaDeModeloBD)t;
//					cwl("c.Padre="+c.Padre);
				return c.Padre;
			}
			return null;
		}
		
		//static
		public  string getNombreStrModelo(ModeloBD m)
		{
            //if (factory.conservarNombres) {
            //    return m.Nombre;
            //}
            //cwl("m="+m);
            //cwl("m.Nombre="+m.Nombre);
            string r = Utiles.getCapitalizeUnido(m.Nombre.Replace("TABLA_", ""));

            //cwl("capi uni="+r);
             r= Utiles.llevarASingular(r) + this.factory.sufijoModelos;
            //cwl("r="+r);
            return r;//"_MD";
		}
		
		public static string getNombreStrColumnaModelo(ColumnaDeModeloBD c)
		{
			return getNombreStrColumnaModelo(c.Padre, c);
		}
		public static string getNombreStrColumnaModelo(ModeloBD m, ColumnaDeModeloBD c)
		{	
			string r = Utiles.llevarASingular(Utiles.getLowerPiso(c.Nombre
			                                                  .Replace("COLUMNA_", "")
			                                                  .Replace("ID_TABLA_", "idkey_")
			           ));
			if (c.EsReferencia) {
				if (!r.Contains("idkey")) {
                    //if (r.Contains("id")) {
                    //    string nombreVariableModelo = CodeBDLenguaje.getNombreStrModeloLower(m);
                    //    if (r== nombreVariableModelo+"id") {
                    //        r = subs(r,0,r.Length-2);
                    //    }
                    //}

					r = "idkey_" + r;
				}
			}
			return r;
		}
		public static string getNombreStrColumnaModeloCapitalice(ModeloBD m, ColumnaDeModeloBD c)
		{	
			return Utiles.capitalize(getNombreStrColumnaModelo(m, c));
		}
		public static string getNombreStrModeloLower(ModeloBD m)
		{
			return Utiles.getLowerPiso(m.Nombre.Replace("TABLA_", ""));
		}
		public static string getNombreStrMetodoGetReferenciaColumnaModelo(ModeloBD m, ColumnaDeModeloBD c)
		{
			string a = "idkey_";
			return "get" + Utiles.capitalize(CodeBDLenguaje.getNombreStrColumnaModelo(m, c).Replace(a, ""));
		}
		
		public static string getNombreStrColumnaModeloReferencia_ParaTipoModelo(ColumnaDeModeloBD c)
		{
			return CodeBDLenguaje.getNombreStrColumnaModelo(c.Padre, c).Replace("idkey_", "");
		}
        public string getStrStaticColumna(string nombreColumna)
        {
            string COLUMNA = "COLUMNA_";
            string r = nombreColumna.ToUpper();
            if (!r.StartsWith(COLUMNA))
            {
                r = COLUMNA + r;
            }
            return r;
        }
        public  string getStrStaticColumna(ColumnaDeModeloBD c) {
            
            return getStrStaticColumna(c.Nombre);
        }

        public string getStrStaticTabla(ModeloBD m)
        {
            string COLUMNA = "TABLA_";
            string r = m.Nombre.ToUpper();
            if (!r.StartsWith(COLUMNA))
            {
                r = COLUMNA + r;
            }
            return r;
        }

        public string getStrLlamadaACoumnaIdkeyDefault(ModeloBD_ID m) {
            string nombreModelo = this.getNombreStrModelo(m);
            string nombreC = factory.Esquema.idDeafult;
            if (m.tieneNombrePersonalizado_idKeyDefault) {
                nombreC = m.IdKeyDefault;
            }
            //return m.tieneNombrePersonalizado_idKeyDefault ? m.IdKeyDefault : factory.Esquema.idDeafult;
            return nombreModelo + "." + this.getStrStaticColumna(nombreC);//;
        }
    }
}
