/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 26/3/2022
 * Hora: 12:03
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.BD.Factory;
using System.Collections.Generic;
using ReneUtiles;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
using ReneUtiles.Clases.BD.Factory.Consultas;
namespace ReneUtiles.Clases.BD.Factory.Codes.Java
{
	/// <summary>
	/// Description of CodeBDJava.
	/// </summary>
	public class CodeBDJava:CodeBDLenguaje
	{
		//private FactoryBD factory;
		protected CodeBDJava_Imports importsJava;
		public string NombreClaseUtilidadesBD;
		public CodeBDJava(FactoryBD factory, DatosDeConexionFactoryBD datosConexionFactory)
            : base(factory, datosConexionFactory)
        {
			this.importsJava = new CodeBDJava_Imports();
			//this.factory = factory;
			//this.datosBDConect = new CodeBDJava_DatosDeBDConect();
			this.Extencion = ".java";
			this.NombreClaseUtilidadesBD="BasicoBD";
		}
		public CodeBDJava_DatosDeBDConect getCodeJava(){
			return (CodeBDJava_DatosDeBDConect)getDSC();
		}
		public void setImports(CodeBDJava_Imports c)
		{
			this.importsJava = c;
		}
		public override string getStrBD(int separacion0)
		{	
			string separacion = getSeparacionln(0, separacion0);
			string bd = "package " + factory.DireccionPaquete + ";";
			bd += "\n" + importsJava.getStr();
			string nombreClaseBD = datosConexionFactory.NombreBDAdmin;
			bd += separacion + "\npublic class " + nombreClaseBD + " extends "+this.NombreClaseUtilidadesBD+"{";
			//string separacion1="\n\t";
			string separacion1 = getSeparacionln(1, separacion0);
			bd += separacion1 + "private String urlBD;";
			bd += separacion1 + "private BDConexion BD;";
			bd += separacion1 + "private BDUpdates __Upd;";
			if (factory.Esquema.UsarSesionStorage) {
				bd += separacion1 + "private BDSesionStorage __SesionStorage;";
			}
			bd += separacion1 + "private boolean usarUpdater;";
			bd += separacion1 +"public " + nombreClaseBD + "("+__getStrArgumentos_DelConstructorBD_SinUrl(this,1)+") throws Exception {";
			bd += __getStrLlamadaThis_DelConstructorBD_SinUrl(this,1);
			bd += separacion1 +"}";
			
			bd += separacion1 + "public " + nombreClaseBD + "("+__getStrArgumentos_DelConstructorBD(this,1)+") throws Exception {";
			string separacion2 = getSeparacionln(2, separacion0);
			string separacion3 = getSeparacionln(3, separacion0);
			if (getDBC().tipoDeConxion == TipoDeConexionBD.SQL_LITE) {
				//bd += separacion2 + "this.urlBD=\"" + factory.DireccionBDSqlite + "\";";//NombreMetodoGetConexionSQL_LITE
				
				bd+=separacion1+"if (url==null){";
				bd+=separacion2+"this.urlBD=\"" + getDBC().url + "\";";//NombreMetodoGetConexionSQL_LITE
				bd+=separacion1+"}else{";
				bd+=separacion2+"this.urlBD=url;";
				bd+=separacion1+"}";
				//bd += separacion2 + "this.BD = BDConexion." + getDSC().NombreMetodoGetConexionSQL_LITE + "(this.urlBD);";
				bd += separacion2 + ""+__getStrNewBDConexion_DelConstructorBD(this,2);
			}
			bd += separacion2 + "this.__Upd =new BDUpdates(this.BD);";
			if (factory.Esquema.UsarSesionStorage) {
				bd += separacion2 + "this.__SesionStorage=new BDSesionStorage(this.BD);";
			}
			bd += separacion2 + "this.usarUpdater=" + (factory.Esquema.UsarUpdate ? "true" : "false") + ";";
			bd += separacion1 + "}";
			EsquemaBD E = factory.Esquema;
			
			bd += separacion1 + "public String "+getNombreMetodoUrlBD()+"(){";
			bd += separacion2 + "return  this.urlBD;";
			bd += separacion1 + "}";
			for (int i = 0; i < E.getCantidadDeModelos(); i++) {
				ModeloBD_ID mt = (ModeloBD_ID)E.getModelo(i);
				int distancia = 1;
				bd += getStrMetodoCrearTabla(mt, distancia);
				bd += getStrMetodoCrearTablaSiNoExiste(mt, distancia);
				bd += getStrMetodoGetArgs(mt, distancia);
				bd += getStrMetodoContentArgs(mt, distancia);
				bd += getStrMetodoGetForID(mt, distancia);
				bd += getStrMetodoInsertar(mt, distancia);
				bd += getStrMetodoGetAll(mt, distancia);
				bd += getStrMetodoUpdate(mt, distancia);
				bd += getStrMetodoDeleteForID(mt, distancia);
				bd += getStrMetodoExiste_ForID(mt,distancia);
				
				bool existeSoloEste=mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste.Count==1;
				for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste.Count; j++) {
					List<ColumnaDeModeloBD> lc=mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste[j];
					bd +=getStrMetodoExiste_ForListaDeColumnas(mt,lc,existeSoloEste,distancia); 
				}
				//cwl("i="+i+" "+mt.Nombre);
				//CrearDeleteCascade Cr = factory.Esquema.listaCrearDeleteCascade[mt];
				if (factory.Esquema.necistaUnDeleteCascade(mt)) {
					bd += getStrMetodoDeleteForID_Cascade(mt, factory.Esquema, distancia);
				}
				
			}
			
			
			
			bd += separacion1 + "public void " + getNombreMetodoCrearTodasLasTablas() + "() throws Exception {";
			for (int i = 0; i < E.getCantidadDeModelos(); i++) {
				ModeloBD mt = E.getModelo(i);
				bd += separacion2 + getNombreMetodoCrearTabla(mt) + "();";
			}
			if (factory.Esquema.UsarSesionStorage) {
				bd += separacion2 + "this.__SesionStorage." + getDSC().SesionStorage.NombreMetodoCrearTablaYBorrarSiExiste + "();";
			}
			bd += separacion1 + "}";
			
			bd += separacion1 + "public void " + getNombreMetodoCrearTodasLasTablasSiNoExisten() + "() throws Exception {";
			for (int i = 0; i < E.getCantidadDeModelos(); i++) {
				ModeloBD mt = E.getModelo(i);
				bd += separacion2 + getNombreMetodoCrearTablaSiNoExiste(mt) + "();";
			}
			if (factory.Esquema.UsarSesionStorage) {
				bd += separacion2 + "this.__SesionStorage." + getDSC().SesionStorage.NombreMetodoCrearTablaYBorrarSiExiste + "();";
			}
			bd += separacion1 + "}";
			
			if (factory.Esquema.UsarSesionStorage) {
				bd += separacion1 + "public SesionStorage getSesionStorage() throws Exception {";
				bd += separacion2 + "return this.__SesionStorage";
				bd += separacion1 + "}";
			}
			
			
			
			for (int i = 0; i < E.getCantidadDeModelos(); i++) {
				ModeloBD_ID mt = (ModeloBD_ID)E.getModelo(i);
				int distancia = 1;
				
				List<ColumnaDeModeloBD> unicos = new List<ColumnaDeModeloBD>();
				for (int j = 0; j < mt.Columnas.Count; j++) {
					ColumnaDeModeloBD cmc = mt.Columnas[j];
					if (cmc.BuscarListaPorEstaColumna) {
						bd += getStrMetodoGetAll_ForColumna(mt, cmc, distancia);
					}
					if (cmc.BuscarModeloPorEstaColumna) {
						bd += getStrMetodoGet_ForColumna(mt, cmc, distancia);
					}
					if (cmc.EliminarPorEstaColumna) {
						bd += getStrMetodoDelete_ForColumna(mt, cmc, distancia);
						//CrearDeleteCascade Cr = factory.Esquema.listaCrearDeleteCascade[mt];
						if (factory.Esquema.necistaUnDeleteCascade(mt)) {
							bd += getStrMetodoDelete_ForColumna_Cascade(mt, cmc, factory.Esquema, distancia);
						}
					}
					if (cmc.EsUnique) {
						unicos.Add(cmc);
					}
					
				}
				
				for (int j = 0; j < unicos.Count; j++) {
					ColumnaDeModeloBD cmc = unicos[j];
					bd += getStrMetodoExiste(mt, cmc, unicos.Count == 1, distancia);
				}
				
				
				
				for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos.Count; j++) {
					List<ColumnaDeModeloBD> l = mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos[j];
					bd += getStrMetodoGetAll_ForListaDeColumnas(mt, l, distancia);
				}
				
				for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueBuscarUnModelo.Count; j++) {
					List<ColumnaDeModeloBD> l = mt.ListaDeConjuntoDeColumnasPorLasQueBuscarUnModelo[j];
					bd += getStrMetodoGet_ForListaDeColumnas(mt, l, distancia);
				}
				
				for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueEliminar.Count; j++) {
					List<ColumnaDeModeloBD> l = mt.ListaDeConjuntoDeColumnasPorLasQueEliminar[j];
					bd += getStrMetodoDelete_ForListaDeColumnas(mt, l, distancia);
					
					//CrearDeleteCascade Cr = factory.Esquema.listaCrearDeleteCascade[mt];
					if (factory.Esquema.necistaUnDeleteCascade(mt)) {
						bd += getStrMetodoDelete_ForListaDeColumnas_Cascade(mt, l, factory.Esquema, distancia);
					}
				}
				
//				for (int j = 0; j < mt.ListaOneToMany.Count; j++) {
//					OneToMany o = mt.ListaOneToMany[j];
//					bd += getStrMetodoGetListaDe_OneToManyLinkInterno(o, distancia);
//					
//				}
				for (int j = 0; j < mt.ListaOneToMany_EnTablaExterna.Count; j++) {
					OneToMany_EnTablaExterna o = mt.ListaOneToMany_EnTablaExterna[j];
					bd += getStrMetodoGetListaDe_OneToManyTablaExterna(o, distancia);
					bd += getStrMetodoGet_EnBD_OneToManyTablaExterna(o, distancia);
					bd += getStrMetodoExiste_OneToManyEnTablaExterna(o, distancia);
				}
				
				for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelosYOrdenar.Count; j++) {
					SelectWhereSort o = mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelosYOrdenar[j];
					bd += getStrMetodoGetAll_ForListaDeColumnas_Sort(o, distancia);
					
				}
			
			}
			
			List<ManyToMany> lmtm = factory.Esquema.ListaManyToMany;
			for (int i = 0; i < lmtm.Count; i++) {
				int distancia = 1;
				ManyToMany o = lmtm[i];
				bd += getStrMetodosManyToMany(o, 1);
				bd += getStrMetodoGet_EnBD_ManyToMany(o, distancia);
				bd += getStrMetodoExiste_ManyToMany(o, distancia);
			}
			
			List<InnerJoin> linj = factory.Esquema.ListaInnerJoinAll;
			for (int i = 0; i < linj.Count; i++) {
				InnerJoin I = linj[i];
				bd += getStrMetodoGetAll_InnerJoin_ForListaDeColumnas(I, 1);
			}
			
			linj = factory.Esquema.ListaInnerJoinOne;
			for (int i = 0; i < linj.Count; i++) {
				InnerJoin I = linj[i];
				bd += getStrMetodoGet_InnerJoin_ForListaDeColumnas(I, 1);
			}
			
//			for (int i = 0; i < E.getCantidadDeModelos(); i++) {
//				ModeloBD_ID mt = (ModeloBD_ID)E.getModelo(i);
//				int distancia = 1;
//				CrearDeleteCascade Cr = factory.Esquema.listaCrearDeleteCascade[mt];
//				if (Cr.NecesitaDeleteCascade) {
//					bd += getStrMetodoGet_InnerJoin_ForListaDeColumnas(I, 1);
//				}
//			
//			}
			
			bd += separacion + "}";
			
			return bd;
		}
		
		
		public override string getStrModelo(ModeloBD m,EsquemaBD E, int separacion0)
		{
			string nombreSuperClaseModelo = getNombreSuperclaseModelo();
			string nombreTipoApiBD = getNombreClaseBDImplementada();
			string separacion1 = getSeparacionln(1, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			List<string> nombreMetodosAgregados=new List<string>();
			
			string mr = "package " + factory.DireccionPaquete + ";";
			mr += "\n" + importsJava.getStr();
			mr += "\npublic class " + nombreModelo + " extends " + nombreSuperClaseModelo + "<" + nombreTipoApiBD + "> {";
			mr += separacion1 + "public static final String " + this.getStrStaticTabla(m) + "=\"" + m.Nombre + "\";";
			
			string[] columnasStr = new string[m.Columnas.Count];
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				columnasStr[i] = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				
				mr += separacion1 + "public static final String " + this.getStrStaticColumna(c) + "=\"" + c.Nombre + "\";";
			}
            if (m is ModeloBD_ID)
            {
                ModeloBD_ID mID = (ModeloBD_ID)m;
                ColumnaDeModeloBD c = mID.columnaID;
                mr += separacion1 + "public static final String " + this.getStrStaticColumna(c) + "=\"" + c.Nombre + "\";";
            }

            mr += separacion1;
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				
				
				mr += separacion1 + "public " + getNombreTipoDeDato(c) + " " + columnasStr[i] + ";";
			}
			
			
			mr += separacion1;
			mr += separacion1 + "public " + nombreModelo + "(";
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				
				mr += (i != 0) ? "," : "";
				mr += getNombreTipoDeDato(c) + " " + columnasStr[i];
			}
			mr += "," + getNombreTipoDeDato(m.getTipoDeDatoID())+" idkey," + nombreTipoApiBD + " apibd){";
			string separacion2 = getSeparacionln(2, separacion0);
			mr += separacion2 + "super(idkey,apibd);";
			for (int i = 0; i < columnasStr.Length; i++) {
				string c = columnasStr[i];
				mr += separacion2 + "this." + c + "=" + c + ";";
			}
			mr += separacion1 + "}";
			
			
			
			mr += separacion1 + "public " + nombreModelo + "(" + nombreTipoApiBD + " apibd";
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				
				//mr += (i != 0) ? "," : "";
				mr += ",";
				mr += getNombreTipoDeDato(c) + " " + columnasStr[i];
			}
			mr += "){";
			mr += separacion2 + "this(";
			for (int i = 0; i < columnasStr.Length; i++) {
				string c = columnasStr[i];
				mr += (i != 0) ? "," : "";
				mr += c;
			}
			mr += ",-1,apibd);";
			mr += separacion1 + "}";
			
			if (m.tieneAlgunaColumnaReferencia()) {
			
				mr += separacion1 + "public " + nombreModelo + "(" + nombreTipoApiBD + " apibd";
				for (int i = 0; i < m.Columnas.Count; i++) {
					ColumnaDeModeloBD c = m.Columnas[i];
					mr += ",";
					if (c.EsReferencia) {
						mr += this.getNombreStrModelo(c.ReferenciaID) + " " + CodeBDLenguaje.getNombreStrColumnaModeloReferencia_ParaTipoModelo(c);
					} else {
						mr += getNombreTipoDeDato(c) + " " + columnasStr[i];
					}
					//mr += (i != 0) ? "," : "";
				
				
				}
				mr += "){";
				mr += separacion2 + "this(";
				for (int i = 0; i < m.Columnas.Count; i++) {
					ColumnaDeModeloBD c = m.Columnas[i];
				
					mr += (i != 0) ? "," : "";
					if (c.EsReferencia) {
						mr += CodeBDLenguaje.getNombreStrColumnaModeloReferencia_ParaTipoModelo(c) + ".idkey";
					} else {
						string cs = columnasStr[i];
						mr += cs;
				
					}
				}
				mr += ",-1,apibd);";
				mr += separacion1 + "}";
			
			}

            string variableModelo = "v";
            mr += separacion1 + "public " + nombreModelo + "(" + nombreModelo + " " + variableModelo + ")";
            mr += "){";
            mr += separacion2 + "this(";
            for (int i = 0; i < columnasStr.Length; i++)
            {
                string c = columnasStr[i];
                mr += (i != 0) ? "," : "";
                mr += variableModelo + "." + c;
            }
            mr += "," + variableModelo + ".idkey,"
                + variableModelo + ".apibd);";
            mr += separacion1 + "}";

            ColumnaDeModeloBD[] referencias = m.getColumnasReferencia();
			for (int i = 0; i < referencias.Length; i++) {
				//"id_tabla_";
				ColumnaDeModeloBD c = referencias[i];
				mr += separacion1 + "public " + this.getNombreStrModelo(c.ReferenciaID) + " " + CodeBDLenguaje.getNombreStrMetodoGetReferenciaColumnaModelo(m, c) + "() throws Exception {";
				mr += separacion2 + "return this.apibd." + getNombreMetodo_GetForID(c.ReferenciaID) + "(this." + CodeBDLenguaje.getNombreStrColumnaModelo(m, c) + ");";
				mr += separacion1 + "}";
			}
			string separacion3 = getSeparacionln(3, separacion0);
			string separacion4 = getSeparacionln(4, separacion0);
			
			for (int i = 0; i < m.ListaOneToMany.Count; i++) {
				OneToMany o = m.ListaOneToMany[i];
				string nombreModeloActual = getNombreStrModelo(o.Many);//getStrMetodoGetAll_ForColumna
				string nombreModeloLowerActual = getNombreStrModeloLower(o.Many);
				string nombreVariableColumnaLink=getNombreVariableElemento(o.LinkToOne);
				mr += separacion1 + "public List<" + nombreModeloActual + "> " + getNombreMetodoGetListaDe(o.Many) + "() throws Exception {";
				mr += separacion2 + "return this.apibd." + getNombreMetodoGetListaDe_OneToManyLinkInterno(o) + "(this.idkey);";
				mr += separacion1 + "}";
				string nombreMetodoAdd=getNombreMetodoAddMany_OneToMany(o);
				if(!nombreMetodosAgregados.Contains(nombreMetodoAdd)){
					nombreMetodosAgregados.Add(nombreMetodoAdd);
					mr += separacion1 + "public " + nombreModeloActual + " " + nombreMetodoAdd + "(" + nombreModeloActual + " " + nombreModeloLowerActual + ") throws Exception {";
					
					mr += separacion2 + "if (this.idkey==-1){";
					mr += separacion3 + "this.idkey=this.apibd." + getNombreMetodo_insertar(m) + "(this).idkey;";
					mr += separacion3 + nombreModeloLowerActual+"."+nombreVariableColumnaLink+"=this.idkey;";
					mr += separacion2 + "}";
					
					mr += separacion2 + "if (" + nombreModeloLowerActual + ".idkey==-1){";
					mr += separacion3 + nombreModeloLowerActual + "=this.apibd." + getNombreMetodo_insertar(o.Many) + "(" + nombreModeloLowerActual + ");";
					mr += separacion2 + "}";
					
					
					mr += separacion2 + "return " + nombreModeloLowerActual + ";";
					
					mr += separacion1 + "}";
				}
				
			}
			for (int i = 0; i < m.ListaOneToManySort.Count; i++) {
				OneToManySort o = m.ListaOneToManySort[i];
				string nombreModeloActual = getNombreStrModelo(o.Many);//getStrMetodoGetAll_ForColumna
				string nombreModeloLowerActual = getNombreStrModeloLower(o.Many);
				string nombreVariableColumnaLink=getNombreVariableElemento(o.LinkToOne);
				mr += separacion1 + "public List<" + nombreModeloActual + "> " + getNombreMetodoGetListaDe_OneToManyLinkInterno_Sort(o) + "() throws Exception {";
				mr += separacion2 + "return this.apibd." + getNombreMetodoGetAll_ForListaDeColumnas_Sort(o.Sort) + "(this.idkey);";
				mr += separacion1 + "}";
				
				string nombreMetodoAdd=getNombreMetodoAddMany_OneToManySort(o);
				if(!nombreMetodosAgregados.Contains(nombreMetodoAdd)){
					nombreMetodosAgregados.Add(nombreMetodoAdd);
					mr += separacion1 + "public " + nombreModeloActual + " " + getNombreMetodoAddMany_OneToManySort(o) + "(" + nombreModeloActual + " " + nombreModeloLowerActual + ") throws Exception {";
					
					mr += separacion2 + "if (this.idkey==-1){";
					mr += separacion3 + "this.idkey=this.apibd." + getNombreMetodo_insertar(m) + "(this).idkey;";
					mr += separacion3 + nombreModeloLowerActual+"."+nombreVariableColumnaLink+"=this.idkey;";
					mr += separacion2 + "}";
					
					mr += separacion2 + "if (" + nombreModeloLowerActual + ".idkey==-1){";
					mr += separacion3 + nombreModeloLowerActual + "=this.apibd." + getNombreMetodo_insertar(o.Many) + "(" + nombreModeloLowerActual + ");";
					mr += separacion2 + "}";
					
					
					mr += separacion2 + "return " + nombreModeloLowerActual + ";";
					
					mr += separacion1 + "}";
				}
				
			}
			
			for (int i = 0; i < m.ListaOneToMany_EnTablaExterna.Count; i++) {
				OneToMany_EnTablaExterna o = m.ListaOneToMany_EnTablaExterna[i];
				string nombreModeloActual = getNombreStrModelo(o.Many);
				string nombreModeloLowerActual = getNombreStrModeloLower(o.Many);
				string nombreModeloUnionActual = getNombreStrModelo(o.Union);
				string nombreModeloUnionLowerActual = getNombreStrModeloLower(o.Union);
				mr += separacion1 + "public List<" + nombreModeloActual + "> " + getNombreMetodoGetListaDe_OneToManyEnTablaExterna(o) + "() throws Exception {";
				mr += separacion2 + "return this.apibd." + getNombreMetodoGetAll_InnerJoin_ForListaDeColumnas(o.Many, listE(o.One)) + "(this.idkey);";
				mr += separacion1 + "}";
				
				string nombreMetodoAdd=getNombreMetodoAddMany_OneToManyEnTablaExterna(o);
				if(!nombreMetodosAgregados.Contains(nombreMetodoAdd)){
					nombreMetodosAgregados.Add(nombreMetodoAdd);
					
					
					mr += separacion1 + "public " + nombreModeloActual + " " + getNombreMetodoAddMany_OneToManyEnTablaExterna(o) + "(" + nombreModeloActual + " " + nombreModeloLowerActual + ") throws Exception {";
				
					mr += separacion2 + "if (this.idkey==-1){";
					mr += separacion3 + "this.idkey=this.apibd." + getNombreMetodo_insertar(m) + "(this).idkey;";
					mr += separacion2 + "}";
				
					mr += separacion2 + "if (" + nombreModeloLowerActual + ".idkey==-1){";
					mr += separacion3 + nombreModeloLowerActual + "=this.apibd." + getNombreMetodo_insertar(o.Many) + "(" + nombreModeloLowerActual + ");";
					mr += separacion2 + "}";
					
					
					
					mr += separacion2 + "if (!this.apibd." + getNombreMetodoExiste_OneToManyEnTablaExterna(o) + "(this.idkey," + nombreModeloLowerActual + ".idkey)){";
					
					mr += separacion3 + nombreModeloUnionActual + " " + nombreModeloUnionLowerActual + "=new " + nombreModeloUnionActual + "(this.apibd,this," + nombreModeloLowerActual + ");";
					mr += separacion3 + "this.apibd." + getNombreMetodo_insertar(o.Union) + "(" + nombreModeloUnionLowerActual + ");";
					mr += separacion3 + "return " + nombreModeloLowerActual + ";";
					mr += separacion2 + "}";
					
					//mr += separacion2 + "return this.apibd."+getNombreMetodoGet_EnBD_OneToManyEnTablaExterna(o)+"(this.idkey,"+nombreModeloLowerActual+".idkey)."+nombreModeloActual+";";
					mr += separacion2 + "return " + nombreModeloLowerActual + ";";
					
					mr += separacion1 + "}";
				}
				
				
			}
			
			List<ManyToMany> lm = factory.Esquema.getListManyToMany(m);
			//cwl("lm.Count="+lm.Count+" !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			for (int i = 0; i < lm.Count; i++) {
				ManyToMany o = lm[i];
				ModeloBD mDestino = o.Many_1 == m ? o.Many_2 : o.Many_1;
				
				string nombreModeloActual = getNombreStrModelo(mDestino);
				mr += separacion1 + "public List<" + nombreModeloActual + "> " + getNombreMetodoGetListaDe_ManyToMany(o, mDestino) + "() throws Exception {";
				mr += separacion2 + "return this.apibd." + getNombreMetodoGetAll_InnerJoin_ForListaDeColumnas(mDestino, listE(m)) + "(this.idkey);";
				mr += separacion1 + "}";
				
				
				
				
				string nombreModeloLowerActual = getNombreStrModeloLower(mDestino);
				string nombreModeloUnionActual = getNombreStrModelo(o.Union);
				string nombreModeloUnionLowerActual = getNombreStrModeloLower(o.Union);

                string cabezalMtM = "";
                if (mDestino == o.Many_1)
                {
                    cabezalMtM = "(" + nombreModeloLowerActual + ".idkey,this.idkey)";
                }
                else
                {
                    cabezalMtM = "(this.idkey," + nombreModeloLowerActual + ".idkey)";
                }

                string nombreMetodoAdd=getNombreMetodoAddMany_ManyToMany(o,m);
				if(!nombreMetodosAgregados.Contains(nombreMetodoAdd)){
					nombreMetodosAgregados.Add(nombreMetodoAdd);
					mr += separacion1 + "public " + nombreModeloActual + " " + getNombreMetodoAddMany_ManyToMany(o, m) + "(" + nombreModeloActual + " " + nombreModeloLowerActual + ") throws Exception {";
				
				
					mr += separacion2 + "if (this.idkey==-1){";
					mr += separacion3 + "this.idkey=this.apibd." + getNombreMetodo_insertar(m) + "(this).idkey;";
					mr += separacion2 + "}";
					
					mr += separacion2 + "if (" + nombreModeloLowerActual + ".idkey==-1){";
					mr += separacion3 + nombreModeloLowerActual + "=this.apibd." + getNombreMetodo_insertar(mDestino) + "(" + nombreModeloLowerActual + ");";
					mr += separacion2 + "}";
					
					
					 
					mr += separacion2 + "if (!this.apibd." + getNombreMetodoExiste_ManyToMany(o) + cabezalMtM+"){";

                    //mr += separacion3 + nombreModeloUnionActual + " " + nombreModeloUnionLowerActual + "=new " + nombreModeloUnionActual + "(this.apibd,this," + nombreModeloLowerActual + ");";

                    mr += separacion3 + nombreModeloUnionActual + " " + nombreModeloUnionLowerActual + "=new " + nombreModeloUnionActual;

                    if (mDestino == o.Many_1)
                    {
                        mr += "(this.apibd," + nombreModeloLowerActual + ",this);";
                    }
                    else
                    {
                        mr += "(this.apibd,this," + nombreModeloLowerActual + ");";
                    }

                    mr += separacion3 + "this.apibd." + getNombreMetodo_insertar(o.Union) + "(" + nombreModeloUnionLowerActual + ");";
					mr += separacion3 + "return " + nombreModeloLowerActual + ";";
					mr += separacion2 + "}";
					
					//mr += separacion2 + "return "+getNombreMetodoGet_EnBD_ManyToMany(o)+"(this.idkey,"+nombreModeloLowerActual+".idkey);";
					mr += separacion2 + "return " + nombreModeloLowerActual + ";";
					
					mr += separacion1 + "}";
				}


                string nombreMetodoRemove = getNombreMetodoRemoveMany_ManyToMany(o, m);
                if (!nombreMetodosAgregados.Contains(nombreMetodoRemove))
                {
                    nombreMetodosAgregados.Add(nombreMetodoRemove);



                    mr += separacion1 + "public void " + nombreMetodoRemove + "(" + nombreModeloActual + " " + nombreModeloLowerActual + ")throws Exception {";
                    //mr += separacion2 + "if (this.idkey!=-1&&"+ nombreModeloLowerActual + ".idkey!=-1&&this.apibd."+ getNombreMetodoExiste_ManyToMany(o) + "(this.idkey," + nombreModeloLowerActual + ".idkey)){";
                    mr += separacion2 + "if (this.idkey!=-1&&" + nombreModeloLowerActual + ".idkey!=-1&&this.apibd." + getNombreMetodoExiste_ManyToMany(o) + cabezalMtM + "){";

                    mr += separacion3 + nombreModeloUnionActual + " " + nombreModeloUnionLowerActual + "=this.apibd." + getNombreMetodoGet_EnBD_ManyToMany(o) + cabezalMtM + ";"; //"(this.idkey," + nombreModeloLowerActual + ".idkey);";
                    mr += separacion3 + nombreModeloUnionLowerActual + ".d();";
                    mr += separacion2 + "}";
                    mr += separacion1 + "}";
                }


            }
			
			mr += separacion1 + "public " + nombreModelo + " " + getNombreMetodoSave(m) + "()throws Exception {";
			
			mr += separacion2 + "if (this.idkey==-1){";
			mr += separacion3 + "return this.apibd." + getNombreMetodo_insertar(m) + "(this);";
			mr += separacion2 + "}";
			
			mr += separacion2 + "return this.apibd." + getNombreMetodoUpdate(m) + "(this);";
			mr += separacion1 + "}";

            //save con id-1 en una nueva BD
            string variableBDNew = "bd";
            mr += separacion1 + "public " + nombreModelo + " " + getNombreMetodoSaveNewBD(m) + "(" + factory.NombreClaseBDPadre + " " + variableBDNew + ")throws Exception {";
            string variableIDAnteiror = "idAnterior";
            mr += separacion2 + getNombreTipoDeDato(m.getTipoDeDatoID()) + " " + variableIDAnteiror + "=this.idkey;";
            mr += separacion2 + "this.idkey=-1;";
            string variableBDAnterior = "bdAnterior";
            mr += separacion2 + factory.NombreClaseBDPadre + " " + variableBDAnterior + "=this.apibd;";
            mr += separacion2 + "this.apibd=" + variableBDNew + ";";
            string variableModeloNuevo = "n";
            mr += separacion2 + nombreModelo + " " + variableModeloNuevo + "=" + getNombreMetodoSave(m) + "();";
            mr += separacion2 + "this.idkey=" + variableIDAnteiror + ";";
            mr += separacion2 + "this.apibd=" + variableBDAnterior + ";";
            mr += separacion2 + "return " + variableModeloNuevo + ";";
            mr += separacion1 + "}";

            //metodo insertar con un ID 
            mr += separacion1 + "public " + nombreModelo + " " + getNombreMetodoSaveConID(m) + "() throws Exception {";
            mr += separacion2 + "if (this.apibd." + getNombreMetodoExiste_ForID(m) + "(this.idkey)){";
            mr += separacion3 + "return this.apibd." + getNombreMetodoUpdate(m) + "(this);";
            mr += separacion2 + "}";
            mr += separacion2 + "return this.apibd." + getNombreMetodo_insertar(m) + "(this);";
            mr += separacion1 + "}";

            mr += separacion1 + "public void " + getNombreMetodoDelete_EnModelo(m) + "()throws Exception {";
			mr += separacion2 + "if (this.idkey!=-1){";
			if(E.necistaUnDeleteCascade(m)){
			mr += separacion3 + "this.apibd." + getNombreMetodoDeleteForID_Cascade(m) + "(this.idkey);";
			}else{
			mr += separacion3 + "this.apibd." + getNombreMetodoDeleteForID(m) + "(this.idkey);";
			}
			mr += separacion2 + "}";
			mr += separacion1 + "}";
			
			mr += separacion1 + "public String getStr(String textoInicial){";
			mr += separacion2 + nombreModelo + " s = this;";
			mr += separacion2 + "return textoInicial+\"" + this.getNombreStrModelo(m) + ": idkey=\"+ s.idkey";
			//string separacion3 = getSeparacionln(3, separacion0);
			for (int i = 0; i < columnasStr.Length; i++) {
				string c = columnasStr[i];
				mr += separacion3 + "+\" " + c + "=\"+s." + c;
			}
			mr += separacion2 + ";";
			mr += separacion1 + "}";
			mr += separacion1 + "public String getStr(){ return getStr(\"\");}";
			mr += "\n}";
			return mr;
		
		}

        private string getPublicOverrideMetodo(int separacion0)
        {
            string separacion1 = getSeparacionln(0, separacion0);
            return "public ";
        }
        private string getPublicAbstractMetodo()
        {
            return "public abstract ";
        }
        //public override string getStrMetodoMetodoDesactivarConsola_Abstract(int separacion0)
        //{
        //    string separacion1 = getSeparacionln(0, separacion0);
        //    string separacion2 = getSeparacionln(0, separacion0);
        //    string mr = "";
        //    mr += separacion1 + getPublicAbstractMetodo() + " " + factory.NombreClaseBDPadre +" "+getNombreMetodoDesactivarConsola()+ "()throws Exception;";
        //    return mr;
        //}
        public override string getStrMetodoMetodoDesactivarConsola(int separacion0)
        {
            string separacion1 = getSeparacionln(0, separacion0);
            string separacion2 = getSeparacionln(1, separacion0);
            string mr = "";
            mr += separacion1 + getPublicOverrideMetodo(separacion0) + " " + factory.NombreClaseBDPadre+" " + getNombreMetodoDesactivarConsola() + "()throws Exception{";
            mr += separacion2 + "this.BD." + getDSC().NombreMetodoDesactivarConsola + "();";
            mr += separacion2 + "return this;";
            mr += separacion1 + "}";
            return mr;
        }


        public override string getStrMetodoGetSesionStorage(int separacion0)
        {
            string separacion1 = getSeparacionln(0, separacion0);
            string separacion2 = getSeparacionln(1, separacion0);
            string bd = "";
            bd += separacion1 + "public SesionStorage getSesionStorage(){";
            bd += separacion2 + "return this.__SesionStorage";
            bd += separacion1 + "}";
            return bd;
        }
        public override string getStrMetodoCrearTodasLasTablasSiNoExisten(int separacion0)
        {
            EsquemaBD E = factory.Esquema;
            string separacion1 = getSeparacionln(0, separacion0);
            string separacion2 = getSeparacionln(1, separacion0);
            string bd = "";
            bd += separacion1 + "public void " + getNombreMetodoCrearTodasLasTablasSiNoExisten() + "(){";
            for (int i = 0; i < E.getCantidadDeModelos(); i++)
            {
                ModeloBD mt = E.getModelo(i);
                bd += separacion2 + getNombreMetodoCrearTablaSiNoExiste(mt) + "();";
            }
            if (factory.Esquema.UsarSesionStorage)
            {
                bd += separacion2 + "this.__SesionStorage." + getDSC().SesionStorage.NombreMetodoCrearTablaYBorrarSiExiste + "();";
            }
            bd += separacion1 + "}";
            return bd;
        }
        public override string getStrMetodoCrearTodasLasTablas(int separacion0)
        {
            EsquemaBD E = factory.Esquema;
            string separacion1 = getSeparacionln(0, separacion0);
            string separacion2 = getSeparacionln(1, separacion0);
            string bd = "";
            bd += separacion1 + "public void " + getNombreMetodoCrearTodasLasTablas() + "(){";
            for (int i = 0; i < E.getCantidadDeModelos(); i++)
            {
                ModeloBD mt = E.getModelo(i);
                bd += separacion2 + getNombreMetodoCrearTabla(mt) + "();";
            }
            if (factory.Esquema.UsarSesionStorage)
            {
                bd += separacion2 + "this.__SesionStorage." + getDSC().SesionStorage.NombreMetodoCrearTablaYBorrarSiExiste + "();";
            }
            bd += separacion1 + "}";

            return bd;
        }


        public override string getStrMetodoCrearTabla(ModeloBD m, int separacion0)
		{
			string nombreModelo = this.getNombreStrModelo(m);
			string tipoARetornar = getNombreClaseBDImplementada();
			string separacion1 = getSeparacionln(0, separacion0);
			string mc = separacion1 + "public " + tipoARetornar + " " + getNombreMetodoCrearTabla(m) + "() throws Exception {";
			string separacion2 = getSeparacionln(1, separacion0);
			mc += separacion2 + " this.BD." + getDSC().NombreMetodoCrearTablaYBorrarSiExiste + "(";
			
			string separacion10 = getSeparacionln(5, separacion0);
			mc += nombreModelo + "." + this.getStrStaticTabla(m);
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				string nc = this.getStrStaticColumna(c);
				mc += separacion10 + "," + nombreModelo + "." + nc;//(i != 0 ? "," : "")
				if (c.Tipo != null) {
					if (c.Tipo != TipoDeDatoSQL.VARCHAR) {
						mc += ",TipoDeDatoSQL." + c.Tipo.getValor().Replace(" ", "_");
					}
					if (c.Tipo == TipoDeDatoSQL.VARCHAR) {
						if (c.Tamaño != 256 && c.Tamaño > 0) {
							mc += "," + c.Tamaño;
						}
						
					}
				}
				if (c.Clasificaciones != null) {
					for (int j = 0; j < c.Clasificaciones.Length; j++) {
						mc += ",TipoDeClasificacionSQL." + c.Clasificaciones[j].getValor().Replace(" ", "_");
					}
				}
				
			}
			mc += separacion10 + ");";
			mc += separacion2 + "return this;";
			mc += separacion1 + "}";
			return mc;
		
		}
		
		public override string getStrMetodoCrearTablaSiNoExiste(ModeloBD m, int separacion0)
		{
			string nombreModelo = this.getNombreStrModelo(m);
			string tipoARetornar = getNombreClaseBDImplementada();
			string separacion1 = getSeparacionln(0, separacion0);
			string mc = separacion1 + "public " + tipoARetornar + " " + getNombreMetodoCrearTablaSiNoExiste(m) + "() throws Exception {";
			string separacion2 = getSeparacionln(1, separacion0);
			mc += separacion2 + " this.BD." + getDSC().NombreMetodoCrearTablaSiNoExiste + "(";
			
			string separacion10 = getSeparacionln(5, separacion0);
			mc += nombreModelo + "." + this.getStrStaticTabla(m);
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				string nc = this.getStrStaticColumna(c);
				mc += separacion10 + "," + nombreModelo + "." + nc;//(i != 0 ? "," : "")
				if (c.Tipo != null) {
					if (c.Tipo != TipoDeDatoSQL.VARCHAR) {
						mc += ",TipoDeDatoSQL." + c.Tipo.getValor().Replace(" ", "_");
					}
					if (c.Tipo == TipoDeDatoSQL.VARCHAR) {
						if (c.Tamaño != 256 && c.Tamaño > 0) {
							mc += "," + c.Tamaño;
						}
						
					}
				}
				if (c.Clasificaciones != null) {
					for (int j = 0; j < c.Clasificaciones.Length; j++) {
						mc += ",TipoDeClasificacionSQL." + c.Clasificaciones[j].getValor().Replace(" ", "_");
					}
				}
				
			}
			mc += separacion10 + ");";
			mc += separacion2 + "return this;";
			mc += separacion1 + "}";
			return mc;
		
		}
		
		public override string getStrMetodoGetArgs(ModeloBD m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			//string mr=separacion+"def get"+nombreModelo+"_Args(self, listaDeArgumentos):";
			string mr = separacion + "public " + nombreModelo + " " + getNombreMetodo_getArgs(m) + "(Object[] listaDeArgumentos) throws Exception {";
			string separacion1 = getSeparacionln(1, separacion0);
			mr += separacion1 + "return new " + nombreModelo + "(";
			string separacionExtra = getSeparacionln(3, separacion0);
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				//string nombreAtributo=CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0) ? separacionExtra + "," : "";
				//mr+=nombreAtributo+"=listaDeArgumentos["+(i+1)+"]";
				mr += getStrMetodoParse(c, "listaDeArgumentos[" + (i + 1) + "]");
			}
			mr += separacionExtra + "," + getStrMetodoParse(TipoDeDatoSQL.INTEGER, "listaDeArgumentos[0]");//
			mr += separacionExtra + ",this";
			mr += separacionExtra + ");";
			mr += separacion1 + "}";
			return mr;
		}
		public override string getStrMetodoContentArgs(ModeloBD m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string mr = separacion + "public Object[] __content_" + nombreModelo + "(" + nombreModelo + " " + nombreModeloLower + ") throws Exception {";
			string separacion1 = getSeparacionln(1, separacion0);
			mr += separacion1 + "Object[] lista = {";
			string separacionExtra = getSeparacionln(2, separacion0);
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				string nombreAtributo = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0) ? separacionExtra + "," : "";
				mr += "new Object[]{" + nombreModelo + "." + this.getStrStaticColumna(c) + "," + nombreModeloLower + "." + nombreAtributo + "}";
			}
			mr += separacionExtra + "};";
			mr += separacion1 + "return lista;";
			mr += separacion1 + "}";
			return mr;
		}
		public override string getStrMetodoGetForID(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			//string mr=separacion+"def get"+nombreModelo+"_id(self, id):";
			string mr = separacion + "public " + nombreModelo + " " + getNombreMetodo_GetForID(m) + "("+getNombreTipoDeDato(m.getTipoDeDatoID())+" id) throws Exception {";
			string separacion1 = getSeparacionln(1, separacion0);
			mr += separacion1 + "Object[] O = this.BD." + getDSC().NombreMetodoGetForId + "(" + nombreModelo + "." + this.getStrStaticTabla(m) + "," + getStrLlamadaACoumnaIdkeyDefault(m) + ", id);";
			string separacion2 = getSeparacionln(2, separacion0);
			mr += separacion1 + "if (O == null){";
			mr += separacion2 + "return null;}";
			mr += separacion1 + "return this." + getNombreMetodo_getArgs(m) + "(O);";
			mr += separacion1 + "}";
			return mr;
		}
		public override string getStrMetodoInsertar(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string mr = separacion + "public " + nombreModelo + " " + getNombreMetodo_insertar(m) + "(" + nombreModelo + " " + nombreModeloLower + ") throws Exception {";
			string separacion1 = getSeparacionln(1, separacion0);
			mr += separacion1 + "if (" + nombreModeloLower + ".idkey==-1){";
			string separacion2 = getSeparacionln(2, separacion0);
            //string idKey = m.getPrimer_NombreColumnaKey() ?? getStrLlamadaACoumnaIdkeyDefault(m); //"+getIdkey(m)+"
            //mr += separacion2 + "int id=this.BD." + getDSC().NombreMetodoInsertar + "(" + nombreModelo + "." + this.getStrStaticTabla(m);
            mr += separacion2 + "int id=this.BD." + getDSC().NombreMetodoInsertarConIdAutomatico + "(" + nombreModelo + "." + this.getStrStaticTabla(m) + "," + getStrLlamadaACoumnaIdkeyDefault(m) + ",";
            string[] variables = new string[m.Columnas.Count];
			string separacionExtra = getSeparacionln(4, separacion0);

            mr += separacionExtra + ",new String[]{";
            for (int i = 0; i < variables.Length; i++)
            {
                ColumnaDeModeloBD c = m.Columnas[i];

                mr += (i != 0 ? "," : "") + nombreModelo + "." + this.getStrStaticColumna(c);
            }
            mr += "}";

            for (int i = 0; i < variables.Length; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				variables[i] = separacionExtra + "," + nombreModeloLower + "." + CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += variables[i];
			}
			mr += separacionExtra + ").id;";
			mr += separacion2 + "return this." + getNombreMetodo_GetForID(m) + "(id);";
			mr += separacion1 + "}else{";
			mr += separacion2 + "this.BD." + getDSC().NombreMetodoInsertarSinIdAutomatico + "(" + nombreModelo + "." + this.getStrStaticTabla(m) + "," + nombreModeloLower + ".idkey";
			for (int i = 0; i < variables.Length; i++) {
				mr += variables[i];
			}
			mr += separacionExtra + ");";
			mr += separacion2 + "return this." + getNombreMetodo_GetForID(m) + "(" + nombreModeloLower + ".idkey);}";
			mr += separacion1 + "}";
			return mr;
		}
		
		public override string getStrMetodoGetAll(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string mr = separacion + "public List<" + nombreModelo + "> " + getNombreMetodoGetAll(m) + "() throws Exception {";
			string separacion2 = getSeparacionln(2, separacion0);
			mr += separacion2 + "List<" + nombreModelo + "> lista=new ArrayList<>();";
			mr += separacion2 + "Object [][]O=this.BD." + getDSC().NombreMetodoSelectTodo + "(" + nombreModelo + "." + this.getStrStaticTabla(m) + ");";
			mr += separacion2 + "if (O!=null){";
			string separacion3 = getSeparacionln(3, separacion0);
			mr += separacion3 + "for(int i=0;i<O.length;i++){";
			string separacion4 = getSeparacionln(4, separacion0);
			mr += separacion4 + "lista.add(" + getNombreMetodo_getArgs(m) + "(O[i]));";
			mr += separacion3 + "}";
			mr += separacion2 + "}";
			mr += separacion2 + "return lista;";
			mr += separacion + "}";
			return mr;
		}
		
		public override string getStrMetodoUpdate(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string mr = separacion + "public " + nombreModelo + " " + getNombreMetodoUpdate(m) + "(" + nombreModelo + " " + nombreModeloLower + ") throws Exception {";
			string separacion2 = getSeparacionln(2, separacion0);
			mr += separacion2 + "this.BD." + getDSC().NombreMetodoUpdateForId + "(" + nombreModelo + "." + this.getStrStaticTabla(m) + "," + nombreModeloLower + ".idkey";
			string separacionExtra = getSeparacionln(5, separacion0);
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				mr += separacionExtra + " , " + nombreModelo + "." + this.getStrStaticColumna(c) + " , " + nombreModeloLower + "." + CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			}
			mr += ");";
			mr += separacion2 + "return " + getNombreMetodo_GetForID(m) + "(" + nombreModeloLower + ".idkey);";
			mr += separacion + "}";
			return mr;
		}
		
		public override string getStrMetodoDeleteForID(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			
			string mr = separacion + "public void " + getNombreMetodoDeleteForID(m) + "("+getNombreTipoDeDato(m.getTipoDeDatoID())+" id) throws Exception {";
			string separacion2 = getSeparacionln(2, separacion0);
			mr += separacion2 + "this.BD." + getDSC().NombreMetodoDeleteForId + "(" + nombreModelo + "." + this.getStrStaticTabla(m) + "," + getStrLlamadaACoumnaIdkeyDefault(m) + ",id);";
			mr += separacion + "}";
			mr += separacion + "public void " + getNombreMetodoDeleteForID(m) + "(" + nombreModelo + " " + nombreModeloLower + ") throws Exception {";
			mr += separacion2 + getNombreMetodoDeleteForID(m) + "(" + nombreModeloLower + ".idkey);";
			mr += separacion + "}";
			return mr;
		}
		
		public override string getStrMetodoGetAll_ForColumna(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string mr = separacion + "public List<" + nombreModelo + "> " + getNombreMetodoGetAll_ForColumna(m, c) + "(" + getNombreTipoDeDato(c) + " " + nombreVariableColumna + ") throws Exception {";
			string separacion2 = getSeparacionln(2, separacion0);
			mr += separacion2 + "List<" + nombreModelo + "> lista=new ArrayList<>();";
			mr += separacion2 + "Object [][]O=this.BD." + getDSC().NombreMetodoSelectWhere + "(" + nombreModelo + "." + this.getStrStaticTabla(m) + "," + nombreModelo + "." + this.getStrStaticColumna(c) + "," + nombreVariableColumna + ");";
			
			
			mr += separacion2 + "if (O!=null){";
			string separacion3 = getSeparacionln(3, separacion0);
			mr += separacion3 + "for(int i=0;i<O.length;i++){";
			string separacion4 = getSeparacionln(4, separacion0);
			mr += separacion4 + "lista.add(" + getNombreMetodo_getArgs(m) + "(O[i]));";
			mr += separacion3 + "}";
			mr += separacion2 + "}";
			mr += separacion2 + "return lista;";
			mr += separacion + "}";
			return mr;
		}
		public override string getStrMetodoGet_ForColumna(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string mr = separacion + "public " + nombreModelo + " " + getNombreMetodoGet_ForColumna(m, c) + "(" + getNombreTipoDeDato(c) + " " + nombreVariableColumna + ") throws Exception {";
			string separacion2 = getSeparacionln(2, separacion0);
			
			mr += separacion2 + "Object []O=this.BD." + getDSC().NombreMetodoSelectWhereFirstRow + "(" + nombreModelo + "." + this.getStrStaticTabla(m) + "," + nombreModelo + "." + this.getStrStaticColumna(c) + "," + nombreVariableColumna + ");";
			
			
			mr += separacion2 + "if (O!=null){";
			string separacion3 = getSeparacionln(3, separacion0);
			
			mr += separacion3 + "return " + getNombreMetodo_getArgs(m) + "(O);";
			
			mr += separacion2 + "}";
			mr += separacion2 + "return null;";
			mr += separacion + "}";
			
			if (c.EsReferencia) {
				ModeloBD referencia = c.ReferenciaID; 
				string nombreVariableColumnaReferencia = CodeBDLenguaje.getNombreStrModeloLower(referencia);
				string nombreModeloColumnaReferencia = this.getNombreStrModelo(referencia);
				mr += separacion + "public " + nombreModelo + " " + getNombreMetodoGet_ForColumna(m, c) +  "(" + nombreModeloColumnaReferencia + " " + nombreVariableColumnaReferencia + ") throws Exception {";
				mr += separacion2 +"return "+ getNombreMetodoGet_ForColumna(m, c) + "(" + nombreVariableColumnaReferencia + ".idkey);";
				mr += separacion + "}";
			}
			return mr;
		}
		
		public override string getStrMetodoDelete_ForColumna(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string mr = separacion + "public void " + getNombreMetodoDelete_ForColumna(m, c) + "(" + getNombreTipoDeDato(c) + " " + nombreVariableColumna + ") throws Exception {";
			string separacion2 = getSeparacionln(2, separacion0);
			
			mr += separacion2 + "this.BD." + getDSC().NombreMetodoDelete + "(" + nombreModelo + "." + this.getStrStaticTabla(m) + "," + nombreModelo + "." + this.getStrStaticColumna(c) + "," + nombreVariableColumna + ");";
			
			
			
			mr += separacion + "}";
			
			if(c.EsReferencia){
				ModeloBD referencia=c.ReferenciaID; 
				string nombreVariableColumnaReferencia = CodeBDLenguaje.getNombreStrModeloLower(referencia);
				string nombreModeloColumnaReferencia = this.getNombreStrModelo(referencia);
				mr+= separacion + "public void " + getNombreMetodoDelete_ForColumna(m, c) + "(" + nombreModeloColumnaReferencia + " " + nombreVariableColumnaReferencia + ") throws Exception {";
				mr+= separacion2 +getNombreMetodoDelete_ForColumna(m, c)+"("+nombreVariableColumnaReferencia+".idkey);";
				mr += separacion + "}";
			}
			
			return mr;
		}
		
		public override string getStrMetodoGetAll_ForListaDeColumnas(ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			string mr = separacion + "public List<" + nombreModelo + "> " + getNombreMetodoGetAll_ForListaDeColumnas(m, C) + "(";//+getNombreTipoDeDato(c)+" "+nombreVariableColumna+"){";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0 ? "," : "");
				mr += getNombreTipoDeDato(c) + " " + nombreVariableColumna;
			}
			mr += ") throws Exception {";
			
			string separacion2 = getSeparacionln(2, separacion0);
			mr += separacion2 + "List<" + nombreModelo + "> lista=new ArrayList<>();";
			mr += separacion2 + "Object [][]O=this.BD." + getDSC().NombreMetodoSelectWhere + "(" + nombreModelo + "." + this.getStrStaticTabla(m);//+","+nombreModelo+"."+this.getStrStaticColumna(c)+","+nombreVariableColumna+");";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += ",";
				mr += nombreModelo + "." + this.getStrStaticColumna(c) + "," + nombreVariableColumna;
			}
			mr += ");";
			
			mr += separacion2 + "if (O!=null){";
			string separacion3 = getSeparacionln(3, separacion0);
			mr += separacion3 + "for(int i=0;i<O.length;i++){";
			string separacion4 = getSeparacionln(4, separacion0);
			mr += separacion4 + "lista.add(" + getNombreMetodo_getArgs(m) + "(O[i]));";
			mr += separacion3 + "}";
			mr += separacion2 + "}";
			mr += separacion2 + "return lista;";
			mr += separacion + "}";
			return mr;
		}
		
		public override string getStrMetodoGet_ForListaDeColumnas(string nombreDelMetodo, ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			//string mr = separacion + "public " + nombreModelo + " " + getNombreMetodoGet_ForListaDeColumnas(m, C) + "(";
			string mr = separacion + "public " + nombreModelo + " " + nombreDelMetodo + "(";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0 ? "," : "");
				mr += getNombreTipoDeDato(c) + " " + nombreVariableColumna;
			}
			mr += ") throws Exception {";
			
			string separacion2 = getSeparacionln(2, separacion0);
			mr += separacion2 + "List<" + nombreModelo + "> lista=new ArrayList<>();";
			mr += separacion2 + "Object []O=this.BD." + getDSC().NombreMetodoSelectWhereFirstRow + "(" + nombreModelo + "." + this.getStrStaticTabla(m);//+","+nombreModelo+"."+this.getStrStaticColumna(c)+","+nombreVariableColumna+");";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += ",";
				mr += nombreModelo + "." + this.getStrStaticColumna(c) + "," + nombreVariableColumna;
			}
			mr += ");";
			
			mr += separacion2 + "if (O!=null){";
			string separacion3 = getSeparacionln(3, separacion0);
			
			mr += separacion3 + "return " + getNombreMetodo_getArgs(m) + "(O);";
			
			mr += separacion2 + "}";
			mr += separacion2 + "return null;";
			mr += separacion + "}";
			return mr;
		}
		
		public override string getStrMetodoDelete_ForListaDeColumnas(ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			string mr = separacion + "public void " + getNombreMetodoDelete_ForListaDeColumnas(m, C) + "(";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0 ? "," : "");
				mr += getNombreTipoDeDato(c) + " " + nombreVariableColumna;
			}
			mr += ") throws Exception {";
			
			string separacion2 = getSeparacionln(2, separacion0);
			
			
			mr += separacion2 + "this.BD." + getDSC().NombreMetodoDelete + "(" + nombreModelo + "." + this.getStrStaticTabla(m);//+","+nombreModelo+"."+this.getStrStaticColumna(c)+","+nombreVariableColumna+");";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += ",";
				mr += nombreModelo + "." + this.getStrStaticColumna(c) + "," + nombreVariableColumna;
			}
			mr += ");";
			
			mr += separacion + "}";
			return mr;
		}
		
		
		public override string getStrMetodoGetAll_InnerJoin_ForListaDeColumnas(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			string mr = separacion + "public List<" + nombreModelo + "> " + getNombreMetodoGetAll_InnerJoin_ForListaDeColumnas(m, elementosWhere) + "(";
			for (int i = 0; i < elementosWhere.Count; i++) {
				ElementoPorElQueBuscar e = elementosWhere[i];
				string variableElemento = getNombreVariableElemento(e);
				mr += (i != 0 ? "," : "");
				mr += getNombreTipoDeDato(e) + " " + variableElemento;
			}
			mr += ") throws Exception {";
			
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			string separacion4 = getSeparacionln(3, separacion0);
			string separacion5 = getSeparacionln(4, separacion0);
			
			
			mr += separacion2 + "Object [][]O=" + __getStrInnerJoinAll(m, cadena, elementosWhere, 1);
			
			
			mr += separacion2 + "List<" + nombreModelo + "> lista=new ArrayList<>();";
			mr += separacion2 + "if (O!=null){";
			mr += separacion3 + "for(int i=0;i<O.length;i++){";
			mr += separacion4 + "lista.add(" + getNombreMetodo_getArgs(m) + "(O[i]));";
			mr += separacion3 + "}";
			mr += separacion2 + "}";
			mr += separacion2 + "return lista;";
			mr += separacion + "}";
			return mr;
		}
		
		
		public override string getStrMetodoGet_InnerJoin_ForListaDeColumnas(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			string mr = separacion + "public " + nombreModelo + " " + getNombreMetodoGet_InnerJoin_ForListaDeColumnas(m, elementosWhere) + "(";
			for (int i = 0; i < elementosWhere.Count; i++) {
				ElementoPorElQueBuscar e = elementosWhere[i];
				string variableElemento = getNombreVariableElemento(e);
				mr += (i != 0 ? "," : "");
				mr += getNombreTipoDeDato(e) + " " + variableElemento;
			}
			mr += ") throws Exception {";
			
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			string separacion4 = getSeparacionln(3, separacion0);
			string separacion5 = getSeparacionln(4, separacion0);
			
			
			mr += separacion2 + "Object []O=" + __getStrInnerJoinFirstRow(m, cadena, elementosWhere, 1);
			
			
			
			mr += separacion2 + "if (O!=null){";
			
			mr += separacion3 + "return " + getNombreMetodo_getArgs(m) + "(O);";
			
			mr += separacion2 + "}";
			mr += separacion2 + "return null;";
			mr += separacion + "}";
			return mr;
		}
		
		
		public override string getStrMetodoDelete_ForColumna_Cascade(ModeloBD_ID m, ColumnaDeModeloBD c, EsquemaBD E, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			//string nombreModeloLower=CodeBDLenguaje.getNombreStrModeloLower(m);
			string nombreVariableModelo = CodeBDLenguaje.getNombreStrModeloLower(m);//getNombreVariableElemento(m);
			string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			string separacion4 = getSeparacionln(3, separacion0);
			
			string mr = separacion + "public void " + getNombreMetodoDelete_ForColumna_Cascade(m,c) + "(" + getNombreTipoDeDato(c) + " " + nombreVariableColumna + ") throws Exception {";
			mr+= separacion2 +getNombreMetodoDelete_ForColumna_Cascade(m,c)+"("+nombreVariableColumna+",null);";
			mr+= separacion+"}";
			
			mr += separacion + "public void " + getNombreMetodoDelete_ForColumna_Cascade(m, c) + "(" + getNombreTipoDeDato(c) + " " + nombreVariableColumna + ",Object modeloQueLoLlamo) throws Exception {";
			
			//mr+=__getStrDeletes_Cascade(m,listaCrearDeleteCascade,2);
			string separacionDeCascada = separacion2;
			string separacionDeCascada1 = separacion3;
			if (c.EsUnique) {
				mr += separacion2 + nombreModelo + " " + nombreVariableModelo + "=" + getNombreMetodoGet_ForColumna(m, c) + "(" + nombreVariableColumna + ");";
			} else {
				mr += separacion2 + "List<" + nombreModelo + "> l=" + getNombreMetodoGetAll_ForColumna(m, c) + "(" + nombreVariableColumna + ");";
				mr += separacion2 + "for(int i=0;i<l.size();i++){";
				mr += separacion3 + nombreModelo + " " + nombreVariableModelo + "=l.get(i);";
				separacionDeCascada = separacion3;
				separacionDeCascada1 = separacion4;
				
			}
			List<ColumnaDeModeloBD> listaCascade = E.listaCrearDeleteCascade[m].ListaCascade;
			
			for (int i = 0; i < listaCascade.Count; i++) {
				ColumnaDeModeloBD cIneterna = listaCascade[i];
				ModeloBD mActual = cIneterna.Padre;
				string nombreModeloActual = this.getNombreStrModelo(mActual);
				string nombreModeloLowerActual = CodeBDLenguaje.getNombreStrModeloLower(mActual);
//				CrearDeleteCascade C = E.listaCrearDeleteCascade[mActual];
//				CrearDeleteCascade CI = E.listaCrearDeleteCascadeInverso[mActual];
				
				if (E.necistaUnDeleteCascade(mActual)) {
					
					mr += separacionDeCascada +"if(modeloQueLoLlamo!=null&& modeloQueLoLlamo instanceof "+nombreModeloActual+"){";
					mr += separacionDeCascada1 + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey);";
					mr += separacionDeCascada +"}else{";
					mr += separacionDeCascada1 + getNombreMetodoDelete_ForColumna_Cascade(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey,"+nombreVariableModelo+ ");";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada +"}";
					
					//mr += separacionDeCascada + getNombreMetodoDelete_ForColumna_Cascade(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey);";
				} else {
					mr += separacionDeCascada + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey);";
				}
				
			}
			
			listaCascade = E.listaCrearDeleteCascadeInverso[m].ListaCascade;
			for (int i = 0; i < listaCascade.Count; i++) {
				ColumnaDeModeloBD cIneterna = listaCascade[i];
				ModeloBD mActual = cIneterna.ReferenciaID;
				string nombreModeloActual = this.getNombreStrModelo(mActual);
				string nombreModeloLowerActual = CodeBDLenguaje.getNombreStrModeloLower(mActual);
//				CrearDeleteCascade C = E.listaCrearDeleteCascade[mActual];
//				CrearDeleteCascade CI = E.listaCrearDeleteCascadeInverso[mActual];
				string nombreVariableColumnaActual = getNombreVariableElemento(cIneterna);
				
				if (E.necistaUnDeleteCascade(mActual)) {
					
					mr += separacionDeCascada +"if(modeloQueLoLlamo!=null&& modeloQueLoLlamo instanceof "+nombreModeloActual+"){";
					mr += separacionDeCascada1 + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada +"}else{";
					mr += separacionDeCascada1 + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual +","+nombreVariableModelo+ ");";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada +"}";
					
					//mr += separacionDeCascada + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
				} else {
					mr += separacionDeCascada + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
				}
				
			}
			if (!c.EsUnique) {
				mr += separacion2 + "}";
			}
			
			mr += separacion2 + getNombreMetodoDelete_ForColumna(m, c) + "(" + nombreVariableColumna + ");";
			//mr+=separacion2+"this.BD."+getDSC().NombreMetodoDelete+"("+nombreModelo+"."+this.getStrStaticTabla(m)+","+nombreModelo+"."+this.getStrStaticColumna(c)+","+nombreVariableColumna+");";
			
			
			
			mr += separacion + "}";
			return mr;
		}
		public override string getStrMetodoDeleteForID_Cascade(ModeloBD_ID m, EsquemaBD E, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			//string nombreModeloLower=CodeBDLenguaje.getNombreStrModeloLower(m);
			string nombreVariableModelo = CodeBDLenguaje.getNombreStrModeloLower(m);//getNombreVariableElemento(m);
			string nombreVariableColumna = getNombreVariableElemento(m);//CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			string mr = separacion + "public void " + getNombreMetodoDeleteForID_Cascade(m) + "(" + getNombreTipoDeDato(m.getTipoDeDatoID()) + " " + nombreVariableColumna + ") throws Exception {";
			mr+= separacion2 +getNombreMetodoDeleteForID_Cascade(m)+"("+nombreVariableColumna+",null);";
			mr+= separacion+"}";
			
			mr+= separacion + "public void " + getNombreMetodoDeleteForID_Cascade(m) + "(" + getNombreTipoDeDato(m.getTipoDeDatoID()) + " " + nombreVariableColumna + ",Object modeloQueLoLlamo) throws Exception {";
			
			//mr+=__getStrDeletes_Cascade(m,listaCrearDeleteCascade,2);
			string separacionDeCascada = separacion2;
			string separacionDeCascada1 = separacion3;
			mr+=separacion2+nombreModelo+" "+nombreVariableModelo+"="+getNombreMetodo_GetForID(m)+"("+nombreVariableColumna+");";
			
			List<ColumnaDeModeloBD> listaCascade = E.listaCrearDeleteCascade[m].ListaCascade;
			
			for (int i = 0; i < listaCascade.Count; i++) {
				ColumnaDeModeloBD cIneterna = listaCascade[i];
				ModeloBD mActual = cIneterna.Padre;
				string nombreModeloActual = this.getNombreStrModelo(mActual);
				string nombreModeloLowerActual = CodeBDLenguaje.getNombreStrModeloLower(mActual);
//				CrearDeleteCascade C = E.listaCrearDeleteCascade[mActual];
//				CrearDeleteCascade CI = E.listaCrearDeleteCascadeInverso[mActual];
				
				if (E.necistaUnDeleteCascade(mActual)) {
					mr += separacionDeCascada +"if(modeloQueLoLlamo!=null&& modeloQueLoLlamo instanceof "+nombreModeloActual+"){";
					mr += separacionDeCascada1 + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableColumna + ");";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada +"}else{";
					mr += separacionDeCascada1 + getNombreMetodoDelete_ForColumna_Cascade(mActual, cIneterna) + "(" + nombreVariableColumna + ","+nombreVariableModelo+");";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada +"}";
				} else {
					mr += separacionDeCascada + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableColumna + ");";//+"("+nombreVariableModelo+".idkey);";
				}
				
			}
			
			
			listaCascade = E.listaCrearDeleteCascadeInverso[m].ListaCascade;
			if (listaCascade.Count > 0) {
				//mr += separacion2 + nombreModelo + " " + nombreVariableModelo + "=" + getNombreMetodo_GetForID(m) + "(" + nombreVariableColumna + ");";
			
				for (int i = 0; i < listaCascade.Count; i++) {
					ColumnaDeModeloBD cIneterna = listaCascade[i];
					ModeloBD mActual = cIneterna.ReferenciaID;
					string nombreModeloActual = this.getNombreStrModelo(mActual);
					string nombreModeloLowerActual = CodeBDLenguaje.getNombreStrModeloLower(mActual);
//					CrearDeleteCascade C = E.listaCrearDeleteCascade[mActual];
//					CrearDeleteCascade CI = E.listaCrearDeleteCascadeInverso[mActual];
					string nombreVariableColumnaActual = getNombreVariableElemento(cIneterna);
				
					if (E.necistaUnDeleteCascade(mActual)) {
						
						mr += separacionDeCascada +"if(modeloQueLoLlamo!=null&& modeloQueLoLlamo instanceof "+nombreModeloActual+"){";
						mr += separacionDeCascada1 + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
						mr += separacionDeCascada +"}else{";
						mr += separacionDeCascada1 + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual  + ","+nombreVariableModelo+");";//+"("+nombreVariableModelo+".idkey);";
						mr += separacionDeCascada +"}";
						
						//mr += separacionDeCascada + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
					} else {
						mr += separacionDeCascada + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
					}
				
				}
			
			
			}
			
			
			mr += separacion2 + getNombreMetodoDeleteForID(m) + "(" + nombreVariableColumna + ");";
			//mr+=separacion2+"this.BD."+getDSC().NombreMetodoDelete+"("+nombreModelo+"."+this.getStrStaticTabla(m)+","+nombreModelo+"."+this.getStrStaticColumna(c)+","+nombreVariableColumna+");";
			
			
			
			mr += separacion + "}";
			return mr;
		}
		
		public override string getStrMetodoDelete_ForListaDeColumnas_Cascade(ModeloBD_ID m, List<ColumnaDeModeloBD> C, EsquemaBD E, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreVariableModelo = CodeBDLenguaje.getNombreStrModeloLower(m);//getNombreVariableElemento(m);
			//string nombreVariableColumna=CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			
			string mr = separacion + "public void " + getNombreMetodoDelete_ForListaDeColumnas_Cascade(m,C) + "(";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0 ? "," : "");
				mr += getNombreTipoDeDato(c) + " " + nombreVariableColumna;
			}
			mr += ") throws Exception {";
			mr+= separacion2 +getNombreMetodoDelete_ForListaDeColumnas_Cascade(m,C)+"(";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0 ? "," : "");
				mr += nombreVariableColumna;
			}
			mr += ",null);";
			mr+= separacion+"}";
			
			
			
			
			
			 mr += separacion + "public void " + getNombreMetodoDelete_ForListaDeColumnas_Cascade(m, C) + "(";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0 ? "," : "");
				mr += getNombreTipoDeDato(c) + " " + nombreVariableColumna;
			}
			mr += ",Object modeloQueLoLlamo) throws Exception {";
			
			//string mr=separacion+"public "+nombreModelo+" "+getStrMetodoDelete_ForListaDeColumnas_Cascade(m,C)+"("+getNombreTipoDeDato(c)+" "+nombreVariableColumna+") throws Exception {";
			
			
			string separacionDeCascada = separacion2;
			string separacionDeCascada1 = separacion3;
			
			mr += separacion2 + "List<" + nombreModelo + "> l=" + getNombreMetodoGetAll_ForListaDeColumnas(m, C) + "(";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0 ? "," : "");
				mr += nombreVariableColumna;
			}
			mr += ");";
			mr += separacion2 + "for(int i=0;i<l.size();i++){";
			mr += separacion3 + nombreModelo + " " + nombreVariableModelo + "=l.get(i);";
			separacionDeCascada = separacion3;
			
			List<ColumnaDeModeloBD> listaCascade = E.listaCrearDeleteCascade[m].ListaCascade;
			
			for (int i = 0; i < listaCascade.Count; i++) {
				ColumnaDeModeloBD cIneterna = listaCascade[i];
				ModeloBD mActual = cIneterna.Padre;
				string nombreModeloActual = this.getNombreStrModelo(mActual);
				string nombreModeloLowerActual = CodeBDLenguaje.getNombreStrModeloLower(mActual);
//				CrearDeleteCascade C0 = E.listaCrearDeleteCascade[mActual];
//				CrearDeleteCascade CI = E.listaCrearDeleteCascadeInverso[mActual];
				
				if (E.necistaUnDeleteCascade(mActual)) {
					
					mr += separacionDeCascada +"if(modeloQueLoLlamo!=null&& modeloQueLoLlamo instanceof "+nombreModeloActual+"){";
					mr += separacionDeCascada1 + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey);";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada +"}else{";
					mr += separacionDeCascada1 + getNombreMetodoDelete_ForColumna_Cascade(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey,"+nombreVariableModelo+");";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada +"}";
					
					//mr += separacionDeCascada + getNombreMetodoDelete_ForColumna_Cascade(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey);";//+"("+nombreVariableModelo+".idkey);";
				} else {
					mr += separacionDeCascada + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey);";//+"("+nombreVariableModelo+".idkey);";
				}
				
			}
			
			listaCascade = E.listaCrearDeleteCascadeInverso[m].ListaCascade;
			for (int i = 0; i < listaCascade.Count; i++) {
				ColumnaDeModeloBD cIneterna = listaCascade[i];
				ModeloBD mActual = cIneterna.ReferenciaID;
				string nombreModeloActual = this.getNombreStrModelo(mActual);
				string nombreModeloLowerActual = CodeBDLenguaje.getNombreStrModeloLower(mActual);
//				CrearDeleteCascade C0 = E.listaCrearDeleteCascade[mActual];
//				CrearDeleteCascade CI = E.listaCrearDeleteCascadeInverso[mActual];
				string nombreVariableColumnaActual = getNombreVariableElemento(cIneterna);
				
				if (E.necistaUnDeleteCascade(mActual)) {
					
					mr += separacionDeCascada +"if(modeloQueLoLlamo!=null&& modeloQueLoLlamo instanceof "+nombreModeloActual+"){";
					mr += separacionDeCascada1 + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada +"}else{";
					mr += separacionDeCascada1 + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ","+nombreVariableModelo+");";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada +"}";
					
					
					//mr += separacionDeCascada + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
				} else {
					mr += separacionDeCascada + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
				}
				
			}
			
			
			mr += separacion2 + getNombreMetodoDelete_ForListaDeColumnas(m, C) + "(";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0 ? "," : "");
				mr += nombreVariableColumna;
			}
			mr += ");";
			//mr+=separacion2+"this.BD."+getDSC().NombreMetodoDelete+"("+nombreModelo+"."+this.getStrStaticTabla(m)+","+nombreModelo+"."+this.getStrStaticColumna(c)+","+nombreVariableColumna+");";
			
			
			
			mr += separacion + "}";
			return mr;
		}
		
		
		public override string getStrMetodoExiste(ModeloBD_ID m, ColumnaDeModeloBD c, bool soloHayEsteEnElModelo, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string mr = separacion + "public boolean " + getNombreMetodoExiste(m, c, soloHayEsteEnElModelo) + "(" + getNombreTipoDeDato(c) + " " + nombreVariableColumna + ") throws Exception {";
			string separacion2 = getSeparacionln(2, separacion0);
			
			mr += separacion2 + "return this.BD." + getDSC().NombreMetodoExiste + "(" + nombreModelo + "." + this.getStrStaticTabla(m) + "," + nombreModelo + "." + this.getStrStaticColumna(c) + "," + nombreVariableColumna + ");";
			
			
			
			mr += separacion + "}";
			return mr;
		}
		
		
		public override string getStrMetodoExiste_ForListaDeColumnas(string nombreMetodo, ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			string mr = separacion + "public boolean " + nombreMetodo + "(";// + getNombreTipoDeDato(c) + " " + nombreVariableColumna + ") throws Exception {";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0 ? "," : "");
				mr += getNombreTipoDeDato(c) + " " + nombreVariableColumna;
			}
			mr += ") throws Exception {";
			
			string separacion2 = getSeparacionln(2, separacion0);
			
			string separacionEspecial = getSeparacionln(4, separacion0);
			mr += separacion2 + "return this.BD." + getDSC().NombreMetodoExiste + "(" + nombreModelo + "." + this.getStrStaticTabla(m);// + "," + nombreModelo + "." + this.getStrStaticColumna(c) + "," + nombreVariableColumna + ");";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				//mr += (i != 0 ? "," : "");
				mr += separacionEspecial + ",";
				mr += nombreModelo + "." + this.getStrStaticColumna(c) + "," + nombreVariableColumna;
			}
			mr += ");";
			
			
			mr += separacion + "}";
			return mr;
		}
		
		public override string getStrMetodoExiste_ForID(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			//string mr=separacion+"def get"+nombreModelo+"_id(self, id):";
			string mr = separacion + "public boolean " + getNombreMetodoExiste_ForID(m) + "("+getNombreTipoDeDato(m.getTipoDeDatoID())+" id) throws Exception {";
			string separacion1 = getSeparacionln(1, separacion0);
			mr += separacion1 + "Object[] O = this.BD." + getDSC().NombreMetodoGetForId + "(" + nombreModelo + "." + this.getStrStaticTabla(m) + "," + getStrLlamadaACoumnaIdkeyDefault(m) + ", id);";
			string separacion2 = getSeparacionln(2, separacion0);
			mr += separacion1 + "return O != null;";
			
			mr += separacion1 + "}";
			return mr;
		}
		

		public override string getStrMetodoGetAll_ForListaDeColumnas_Sort(SelectWhereSort s, int separacion0)
		{
			ModeloBD_ID m=s.Modelo;
			List<ColumnaDeModeloBD> C=s.ListaPorLasQueBuscar;
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			string mr = separacion + "public List<" + nombreModelo + "> " + getNombreMetodoGetAll_ForListaDeColumnas_Sort(s) + "(";//+getNombreTipoDeDato(c)+" "+nombreVariableColumna+"){";
			for (int i = 0; i < C.Count; i++) {
				mr += (i != 0 ? "," : "");
				string nombreVariableColumna =null;
//				if(C[i] is ColumnaDeModeloBD){
//					ColumnaDeModeloBD c = (ColumnaDeModeloBD)C[i];
//					nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
//					mr += getNombreTipoDeDato(c) + " " + nombreVariableColumna;
//				}else{
//					if(C[i] is ModeloBD){
//						nombreVariableColumna = this.getNombreStrModelo((ModeloBD)C[i]);
//						mr += "int " + nombreVariableColumna;
//					}
//				}
				
				mr += getNombreTipoDeDato(C[i]) + " " + getNombreVariableElemento(C[i]);
				
			}
			mr += ") throws Exception {";
			
			string separacion2 = getSeparacionln(2, separacion0);
			string separacion3 = getSeparacionln(3, separacion0);
			string separacion4 = getSeparacionln(4, separacion0);
			
			mr += separacion2 + "List<" + nombreModelo + "> lista=new ArrayList<>();";
			mr += separacion2 + "Object [][]O=this.BD." + getDSC().NombreMetodoSelectWhereOrderBy + "(" + nombreModelo + "." + this.getStrStaticTabla(m)+",";//+","+nombreModelo+"."+this.getStrStaticColumna(c)+","+nombreVariableColumna+");";
			mr += separacion3 +"new Object []{";
			for (int i = 0; i < C.Count; i++) {
				//ElementoPorElQueBuscar e=C[i];
				string nombreVariableColumna = getNombreVariableElemento(C[i]);
				string nombreColumna=null;
				//if(e is ColumnaDeModeloBD){
				ColumnaDeModeloBD c = C[i];//(ColumnaDeModeloBD)C[i];
					
					nombreColumna=this.getStrStaticColumna(c);
//				}else{
//					if(e is ModeloBD){
//						nombreColumna="id";
//					}
//				}
				
				mr +=separacion4+ (i != 0 ? "," : "");
				mr += nombreModelo + "." + nombreColumna + "," + nombreVariableColumna;
			}
			mr += separacion3+"}";
			for (int i = 0; i < s.ListaPorLasQueOrdenar.Count; i++) {
				SelectWhereSort.ColumnaYOrden co=s.ListaPorLasQueOrdenar[i];
				ColumnaDeModeloBD c = co.Columna;
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr +=separacion4+  "," ;
				mr += nombreModelo + "." + this.getStrStaticColumna(c) ;//+ "," + nombreVariableColumna;
				if(co.Ordenamiento!=null){
					mr += ","+co.Ordenamiento.getValor();
				}
			}
			mr += ");";
			
			mr += separacion2 + "if (O!=null){";
			
			mr += separacion3 + "for(int i=0;i<O.length;i++){";
			
			mr += separacion4 + "lista.add(" + getNombreMetodo_getArgs(m) + "(O[i]));";
			mr += separacion3 + "}";
			mr += separacion2 + "}";
			mr += separacion2 + "return lista;";
			mr += separacion + "}";
			return mr;
		}


//		public override string getStrMetodoDelete_ForListaDeColumnas(ModeloBD_ID m,List<ColumnaDeModeloBD> C,int separacion0)
		
		
		//		public  string __getStrMetodoGetListaDe_OneToManyLinkInterno_MetodoDeModelo(OneToMany o,int separacion0){
		//
		//		}
		
		
		//		protected override string __getStrInnerJoinFirstRow(ModeloBD_ID m,List<ElementoPorElQueBuscar> cadena,List<ElementoPorElQueBuscar> elementosWhere,int separacion0){
		//			return __getStrInnerJoin(getDSC().NombreMetodoSelectWhereInnerJoinFirstRow,m,cadena,elementosWhere,separacion0);
		//		}
		//		protected override string __getStrInnerJoinAll(ModeloBD_ID m,List<ElementoPorElQueBuscar> cadena,List<ElementoPorElQueBuscar> elementosWhere,int separacion0){
		//			//getDSC().NombreMetodoSelectWhereInnerJoin
		//			return __getStrInnerJoin(getDSC().NombreMetodoSelectWhereInnerJoin,m,cadena,elementosWhere,separacion0);
		//		}
		
		
		
		protected override string __getStrInnerJoin(string nombreMetodoBD, ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0)
		{
			//string separacion=getSeparacionln(0,separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			string mr = "";
			
			
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			string separacion4 = getSeparacionln(3, separacion0);
			string separacion5 = getSeparacionln(4, separacion0);
			
			//mr+=separacion2+"this.BD."+getDSC().NombreMetodoSelectWhereInnerJoin+"("+nombreModelo+"."+this.getStrStaticTabla(m);//+","+nombreModelo+"."+this.getStrStaticColumna(c)+","+nombreVariableColumna+");";
			mr += "this.BD." + nombreMetodoBD + "(" + nombreModelo + "." + this.getStrStaticTabla(m);//+","+nombreModelo+"."+this.getStrStaticColumna(c)+","+nombreVariableColumna+");";
			mr += separacion3 + ",new Object[]{";
			int pos = 0, j = 0;
			for (int i = 0; i < cadena.Count; i++) {
				ElementoPorElQueBuscar e = cadena[i];
				ModeloBD mEnInner = getModeloDeElemento(e);
				string nombreModeloEnInner = this.getNombreStrModelo(mEnInner);
				string nombreModeloLowerEnInner = CodeBDLenguaje.getNombreStrModeloLower(mEnInner);
				
				string nombreColumna = "";
				if (e is ColumnaDeModeloBD) {
					ColumnaDeModeloBD c = (ColumnaDeModeloBD)e;
					nombreColumna = "." + this.getStrStaticColumna(c);
					
				}
				string par = "new Object[]{" + nombreModeloEnInner + "." + this.getStrStaticTabla(mEnInner) + (nombreColumna.Length > 1 ? "," + nombreModeloEnInner + nombreColumna : "") + "}";
				
				
				if (i == 0 && cadena.Count == 1) {
					mr += par;
				}
				
				if (pos == 0) {
					mr += (j != 0 ? "," : "");
					mr += separacion4 + "new Object[]{";
					mr += separacion5 + par;
				} else if (pos == 1) {
					mr += ",";
					mr += separacion5 + par;
					mr += separacion4 + "}";
					j++;
				}
				pos = (pos + 1) % 2;
			
			}
			mr += separacion3 + "}";
			
			mr += separacion3;
			pos = 0;
			j = 0;
			for (int i = 0; i < elementosWhere.Count; i++) {
				ElementoPorElQueBuscar e = elementosWhere[i];
				ModeloBD mEnInner = getModeloDeElemento(e);
				string nombreModeloEnInner = this.getNombreStrModelo(mEnInner);
				string nombreModeloLowerEnInner = CodeBDLenguaje.getNombreStrModeloLower(mEnInner);
				
				string nombreColumna = "";
				string par ="";
				if (e is ColumnaDeModeloBD) {
					ColumnaDeModeloBD c = (ColumnaDeModeloBD)e;
					nombreColumna = "." + this.getStrStaticColumna(c);
					par = "new Object[]{" + 
                        nombreModeloEnInner + "." + this.getStrStaticTabla(mEnInner) + (nombreColumna.Length > 1 ? "," + nombreModeloEnInner + nombreColumna : "")
                        + "}";
				}else{
					par = "new Object[]{" + nombreModeloEnInner + "." + this.getStrStaticTabla(mEnInner) +  "}";//,\"id\"
				}
				
				
				
				mr += separacion3 + "," + par + "," + getNombreVariableElemento(e);
			}
			
			mr += ");";
			
			
			
			return mr;
		}
		
		//		protected override string __getStrDeletes_Cascade(ModeloBD m,Dictionary<ModeloBD,CrearDeleteCascade> listaCrearDeleteCascade,int separacion0){
		//			string separacion=getSeparacionln(0,separacion0);
		//			string mr="";
		//			List<ColumnaDeModeloBD> listaCascade=listaCrearDeleteCascade[m].ListaCascade;
		//			for (int i = 0; i < listaCascade.Count; i++) {
		//				ColumnaDeModeloBD c=listaCascade[i];
		//				ModeloBD mActual=c.Padre;
		//				string nombreModeloActual=this.getNombreStrModelo(m);
		//				string nombreModeloLowerActual=CodeBDLenguaje.getNombreStrModeloLower(m);
		//				CrearDeleteCascade C=listaCrearDeleteCascade[mActual];
		//				if(c.EsUnique){
		//
		//				}
		//				if(C.NecesitaDeleteCascade){
		//					mr+=separacion+getNombreMetodoDelete_ForColumna_Cascade(mActual,c)+"("+getNombreVariableElemento(m)+".idkey);";
		//				}else{
		//					mr+=separacion+getNombreMetodoDelete_ForColumna(mActual,c)+"("+getNombreVariableElemento(m)+".idkey);";
		//				}
		//
		//			}
		//			return mr;
		//		}
		
		public string getNombreClaseBDImplementada()
		{
			return datosConexionFactory.NombreBDAdmin;
		}
		
		public string getNombreTipoDeDato(ElementoPorElQueBuscar c)
		{
			if (c is ModeloBD) {
				return getNombreTipoDeDato(TipoDeDatoSQL.INTEGER);
			} else if (c is ColumnaDeModeloBD) {
				return getNombreTipoDeDato((ColumnaDeModeloBD)c);
			}
			return null;
		}
		public string getNombreTipoDeDato(ColumnaDeModeloBD c)
		{
			
			return getNombreTipoDeDato(c.Tipo);
		}
		public virtual string getNombreTipoDeDato(TipoDeDatoSQL t)
		{
			
			if (t == TipoDeDatoSQL.BOOLEAN) {
				return "boolean";
			} 
			if (t == TipoDeDatoSQL.BLOB) {
				return "byte[]";
			} 
			if (t == TipoDeDatoSQL.DATE) {
				return "Date";
			} 
			if (t == TipoDeDatoSQL.TIME) {
				return "Time";
			}
			if (t == TipoDeDatoSQL.INTEGER||t==TipoDeDatoSQL.SERIAL) {
				return "int";
			} 
			if (t == TipoDeDatoSQL.POINT) {
				return "Point";
			} 
			if (t == TipoDeDatoSQL.REAL || t == TipoDeDatoSQL.DOUBLE_PRECISION) {
				return "double";
			} 
			if (t == TipoDeDatoSQL.VARCHAR||t==TipoDeDatoSQL.TEXT) {
				return "String";
			}
			return null;
			
		}
		
		
		
		
		public string getStrMetodoParse(ColumnaDeModeloBD c, string variable)
		{
			return getStrMetodoParse(c.Tipo, variable);
		}
		public virtual string getStrMetodoParse(TipoDeDatoSQL tipo, string variable)
		{
			
//			metodoCreador1<TipoDeDatoSQL,String> getNombreMetodoParse = t => {
//				if (t == TipoDeDatoSQL.BOOLEAN) {
//					return "toBool";
//				} 
//				if (t == TipoDeDatoSQL.BLOB) {
//					return "toBlob";
//				} 
//				if (t == TipoDeDatoSQL.DATE) {
//					return "toDate";
//				} 
//				if (t == TipoDeDatoSQL.TIME) {
//					return "toTime";
//				} 
//				if (t == TipoDeDatoSQL.INTEGER) {
//					return "toInt";
//				} 
//				if (t == TipoDeDatoSQL.POINT) {
//					return "toPoint";
//				} 
//				if (t == TipoDeDatoSQL.REAL || t == TipoDeDatoSQL.DOUBLE_PRECISION) {
//					return "toDouble";
//				} //DOUBLE_PRECISION
//				if (t == TipoDeDatoSQL.VARCHAR) {
//					return "to_String";
//				}
//				return null;
//			};
			string nombreMetodo = getNombreMetodoParse(tipo);
			return nombreMetodo != null ? nombreMetodo + "(" + variable + ")" : variable;
		}
		
		protected virtual string getNombreMetodoParse(TipoDeDatoSQL t){
			if (t == TipoDeDatoSQL.BOOLEAN) {
					return "toBool";
				} 
				if (t == TipoDeDatoSQL.BLOB) {
					return "toBlob";
				} 
				if (t == TipoDeDatoSQL.DATE) {
					return "toDate";
				} 
				if (t == TipoDeDatoSQL.TIME) {
					return "toTime";
				} 
				if (t == TipoDeDatoSQL.INTEGER||t==TipoDeDatoSQL.SERIAL) {
					return "toInt";
				} 
				if (t == TipoDeDatoSQL.POINT) {
					return "toPoint";
				} 
				if (t == TipoDeDatoSQL.REAL || t == TipoDeDatoSQL.DOUBLE_PRECISION) {
					return "toDouble";
				} //DOUBLE_PRECISION
				if (t == TipoDeDatoSQL.VARCHAR||t==TipoDeDatoSQL.TEXT) {
					return "to_String";
				}
				return null;
			
		}
		
		protected virtual string __getStrLlamadaThis_DelConstructorBD_SinUrl(CodeBDJava code,int separacion0){
		string separacion = getSeparacionln(0, separacion0);
		return separacion+"this(null);";
		}
		protected virtual string __getStrArgumentos_DelConstructorBD_SinUrl(CodeBDJava code,int separacion0){
			return "";
		}
		protected virtual string __getStrArgumentos_DelConstructorBD(CodeBDJava code,int separacion0){
			return "String url";
		}
		
		protected virtual string __getStrNewBDConexion_DelConstructorBD(CodeBDJava code,int separacion0){
			string separacion = getSeparacionln(0, separacion0);
//			string separacion1 = getSeparacionln(1, separacion0);
//			string mr="";
//			mr+=separacion+"if (url==null){";
//			mr+=separacion1+"this.BD =BDConexion." + code.getDSC().NombreMetodoGetConexionSQL_LITE + "(this.urlBD);";
//			mr+=separacion+"}else{";
//			mr+=separacion1+"this.BD ="+;
			return separacion+"this.BD =BDConexion." + code.getDSC().NombreMetodoGetConexionSQL_LITE + "(this.urlBD);";;
		}
	}
}
