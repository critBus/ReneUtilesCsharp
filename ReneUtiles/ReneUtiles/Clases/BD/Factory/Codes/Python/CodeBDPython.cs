/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 6/2/2022
 * Time: 20:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using ReneUtiles.Clases.BD;
using ReneUtiles.Clases.BD.Factory;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
using ReneUtiles.Clases.BD.Factory.Consultas;
namespace ReneUtiles.Clases.BD.Factory.Codes.Python
{
	/// <summary>
	/// Description of CodeBDPython.
	/// </summary>
	public class CodeBDPython:CodeBDLenguaje
	{
		
		public CodeBDPython(FactoryBD factory, DatosDeConexionFactoryBD datosConexionFactory)
            : base(factory, datosConexionFactory)
        {
			this.UsaUnArchivoParaTodosLosModelosJuntos = true;
			//this.factory = factory;
			//this.datosBDConect = new DatosDeBDConect();
			this.Extencion = ".py";
		}
		public override string getStrArchivoTodosLosModelosJuntos(int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string separacion1 = getSeparacionln(1, separacion0);
			string mr = separacion + "from RenePy.ClasesUtiles.BasesDeDatos.ModeloDeApiBD import ModeloDeApiBD";
			mr += separacion + "from RenePy.Utiles import *";
			for (int i = 0; i < factory.Esquema.getCantidadDeModelos(); i++) {
				ModeloBD m = factory.Esquema.getModelo(i);
				string codigo = getStrModelo(m, factory.Esquema, 0);
				mr += codigo;
			}
			return mr;
		}
		public override string getStrModelo(ModeloBD m, EsquemaBD E, int separacion0)
		{
			string nombreSuperClaseModelo = getNombreSuperclaseModelo();
			string separacion = getSeparacionln(0, separacion0);
			string separacion1 = getSeparacionln(1, separacion0);
			string mr = "";
			mr += separacion + "class " + this.getNombreStrModelo(m) + "(" + nombreSuperClaseModelo + "):";
			mr += separacion1 + m.Nombre + "=\"" + m.Nombre + "\"";
			for (int j = 0; j < m.Columnas.Count; j++) {
				ColumnaDeModeloBD c = m.Columnas[j];
				mr += separacion1 + c.Nombre + "=\"" + c.Nombre + "\"";
			}
			
			string[] columnasStr = new string[m.Columnas.Count];
			mr += separacion1 + "def __init__(self,apibd";
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				columnasStr[i] = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				//mr += (i != 0) ? "," : "";
				mr += ",";
				//mr += columnasStr[i];
				mr += columnasStr[i];
			}
			mr += ",idkey:int=None):";
			string separacion2 = getSeparacionln(2, separacion0);
			string separacion3 = getSeparacionln(3, separacion0);
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				string cc = columnasStr[i];
				if (c.EsReferencia) {
					mr += separacion2 + "if isinstance(" + cc + "," + this.getNombreStrModelo(c.ReferenciaID) + "):";
					mr += separacion3 + "self." + cc + "=" + cc + ".idkey";
					mr += separacion2 + "else:";
					mr += separacion3 + "self." + cc + "=" + cc;
				} else {
					mr += separacion2 + "self." + cc + "=" + cc;
				}
				
				
			}
			mr += separacion2 + "super().__init__(idkey,apibd)";
			
			ColumnaDeModeloBD[] referencias = m.getColumnasReferencia();
			for (int i = 0; i < referencias.Length; i++) {
				//string a="idkey_";//"id_tabla_";
				ColumnaDeModeloBD c = referencias[i];
				//mr += separacion1 + "def get"+Utiles.capitalize(FactoryBD.getNombreStrColumnaModelo(m,c).Replace(a,""))+"(self):";
				mr += separacion1 + "def " + CodeBDLenguaje.getNombreStrMetodoGetReferenciaColumnaModelo(m, c) + "(self):";
				mr += separacion2 + "return self.apibd." + getNombreMetodo_GetForID(c.ReferenciaID) + "(self." + CodeBDLenguaje.getNombreStrColumnaModelo(m, c) + ")";
			}
			List<string> nombreMetodosAgregados = new List<string>();
			//string separacion3 = getSeparacionln(3, separacion0);
			string separacion4 = getSeparacionln(4, separacion0);
			
			for (int i = 0; i < m.ListaOneToMany.Count; i++) {
				OneToMany o = m.ListaOneToMany[i];
				string nombreModeloActual = getNombreStrModelo(o.Many);//getStrMetodoGetAll_ForColumna
				mr += separacion1 + "def " + getNombreMetodoGetListaDe(o.Many) + "(self):";
				mr += separacion2 + "return self.apibd." + getNombreMetodoGetListaDe_OneToManyLinkInterno(o) + "(self.idkey)";
				
				
				string nombreMetodoAdd = getNombreMetodoAddMany_OneToMany(o);
				if (!nombreMetodosAgregados.Contains(nombreMetodoAdd)) {
					nombreMetodosAgregados.Add(nombreMetodoAdd);
					string nombreModeloLowerActual = getNombreStrModeloLower(o.Many);
					string nombreVariableColumnaLink = getNombreVariableElemento(o.LinkToOne);
					mr += separacion1 + "def " + nombreMetodoAdd + "(self," + nombreModeloLowerActual + "):";// + nombreModeloActual + " " 
					
					mr += separacion2 + "if self.idkey is None:";
					mr += separacion3 + "self.idkey=self.apibd." + getNombreMetodo_insertar(m) + "(self).idkey";
					mr += separacion3 + nombreModeloLowerActual + "." + nombreVariableColumnaLink + "=self.idkey";
					
					
					mr += separacion2 + "if " + nombreModeloLowerActual + ".idkey is None:";
					mr += separacion3 + nombreModeloLowerActual + "=self.apibd." + getNombreMetodo_insertar(o.Many) + "(" + nombreModeloLowerActual + ")";
					
					
					mr += separacion2 + "return " + nombreModeloLowerActual;
					
					
				}
				
			}
			
			for (int i = 0; i < m.ListaOneToManySort.Count; i++) {
				OneToManySort o = m.ListaOneToManySort[i];
				string nombreModeloActual = getNombreStrModelo(o.Many);//getStrMetodoGetAll_ForColumna
				mr += separacion1 + "def " + getNombreMetodoGetListaDe_OneToManyLinkInterno_Sort(o) + "(self):";
				mr += separacion2 + "return self.apibd." + getNombreMetodoGetAll_ForListaDeColumnas_Sort(o.Sort) + "(self.idkey)";
				
				string nombreModeloLowerActual = getNombreStrModeloLower(o.Many);
				string nombreVariableColumnaLink = getNombreVariableElemento(o.LinkToOne);
				string nombreMetodoAdd = getNombreMetodoAddMany_OneToManySort(o);
				if (!nombreMetodosAgregados.Contains(nombreMetodoAdd)) {
					nombreMetodosAgregados.Add(nombreMetodoAdd);
					mr += separacion1 + "def " + getNombreMetodoAddMany_OneToManySort(o) + "(self," + nombreModeloLowerActual + "):";// + nombreModeloActual + " "
					
					mr += separacion2 + "if self.idkey is None:";
					mr += separacion3 + "self.idkey=self.apibd." + getNombreMetodo_insertar(m) + "(self).idkey";
					mr += separacion3 + nombreModeloLowerActual + "." + nombreVariableColumnaLink + "=self.idkey";
					
					
					mr += separacion2 + "if " + nombreModeloLowerActual + ".idkey is None:";
					mr += separacion3 + nombreModeloLowerActual + "=self.apibd." + getNombreMetodo_insertar(o.Many) + "(" + nombreModeloLowerActual + ")";
					
					mr += separacion2 + "return " + nombreModeloLowerActual;
					
				}
			}
			
			
			for (int i = 0; i < m.ListaOneToMany_EnTablaExterna.Count; i++) {
				OneToMany_EnTablaExterna o = m.ListaOneToMany_EnTablaExterna[i];
				string nombreModeloActual = getNombreStrModelo(o.Many);
				string nombreModeloLowerActual = getNombreStrModeloLower(o.Many);
				string nombreModeloUnionActual = getNombreStrModelo(o.Union);
				string nombreModeloUnionLowerActual = getNombreStrModeloLower(o.Union);
				mr += separacion1 + "def " + getNombreMetodoGetListaDe_OneToManyEnTablaExterna(o) + "(self):";
				mr += separacion2 + "return self.apibd." + getNombreMetodoGetAll_InnerJoin_ForListaDeColumnas(o.Many, listE(o.One)) + "(self.idkey)";
				
				
				
				string nombreMetodoAdd = getNombreMetodoAddMany_OneToManyEnTablaExterna(o);
				if (!nombreMetodosAgregados.Contains(nombreMetodoAdd)) {
					nombreMetodosAgregados.Add(nombreMetodoAdd);
					
					mr += separacion1 + "def " + getNombreMetodoAddMany_OneToManyEnTablaExterna(o) + "(self," + nombreModeloLowerActual + "):";//+nombreModeloActual+" "
				
					mr += separacion2 + "if self.idkey is None:";
					mr += separacion3 + "self.idkey=self.apibd." + getNombreMetodo_insertar(m) + "(self).idkey";
				
					mr += separacion2 + "if " + nombreModeloLowerActual + ".idkey is None:";
					mr += separacion3 + nombreModeloLowerActual + "=self.apibd." + getNombreMetodo_insertar(o.Many) + "(" + nombreModeloLowerActual + ")";
					
					
					
				
					
					mr += separacion2 + "if not self.apibd." + getNombreMetodoExiste_OneToManyEnTablaExterna(o) + "(self.idkey," + nombreModeloLowerActual + ".idkey):";
					
					mr += separacion3 + nombreModeloUnionLowerActual + "=" + nombreModeloUnionActual + "(self.apibd,self," + nombreModeloLowerActual + ")";
					mr += separacion3 + "self.apibd." + getNombreMetodo_insertar(o.Union) + "(" + nombreModeloUnionLowerActual + ")";
					mr += separacion3 + "return " + nombreModeloLowerActual;
					
					
					//mr += separacion2 + "return "+getNombreMetodoGet_OneToManyEnTablaExterna(o)+"(self.idkey,"+nombreModeloLowerActual+".idkey)";
					mr += separacion2 + "return " + nombreModeloLowerActual;
					
				}
				
				
				
				
			}
			
			List<ManyToMany> lm = factory.Esquema.getListManyToMany(m);
			//cwl("lm.Count="+lm.Count+" !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			for (int i = 0; i < lm.Count; i++) {
				ManyToMany o = lm[i];
				ModeloBD mDestino = o.Many_1 == m ? o.Many_2 : o.Many_1;
				
				string nombreModeloActual = getNombreStrModelo(mDestino);
				mr += separacion1 + "def " + getNombreMetodoGetListaDe_ManyToMany(o, mDestino) + "(self):";
				mr += separacion2 + "return self.apibd." + getNombreMetodoGetAll_InnerJoin_ForListaDeColumnas(mDestino, listE(m)) + "(self.idkey)";
				
				
				
				string nombreModeloLowerActual = getNombreStrModeloLower(mDestino);
				string nombreModeloUnionActual = getNombreStrModelo(o.Union);
				string nombreModeloUnionLowerActual = getNombreStrModeloLower(o.Union);
				
				string nombreMetodoAdd = getNombreMetodoAddMany_ManyToMany(o, m);
				if (!nombreMetodosAgregados.Contains(nombreMetodoAdd)) {
					nombreMetodosAgregados.Add(nombreMetodoAdd);
					mr += separacion1 + "def " + getNombreMetodoAddMany_ManyToMany(o, m) + "(self," + nombreModeloLowerActual + "):";//+nombreModeloActual+" "
				
					mr += separacion2 + "if self.idkey is None:";
					mr += separacion3 + "self.idkey=self.apibd." + getNombreMetodo_insertar(m) + "(self).idkey";
				
					mr += separacion2 + "if " + nombreModeloLowerActual + ".idkey is None:";
					mr += separacion3 + nombreModeloLowerActual + "=self.apibd." + getNombreMetodo_insertar(mDestino) + "(" + nombreModeloLowerActual + ")";
					
					
					
					
					
					mr += separacion2 + "if not self.apibd." + getNombreMetodoExiste_ManyToMany(o) + "(self.idkey," + nombreModeloLowerActual + ".idkey):";
                    //getNombreMetodoAdd nombreModeloUnionActual + " " +
                    mr += separacion3 +  nombreModeloUnionLowerActual + "=" + nombreModeloUnionActual + "(self.apibd,self," + nombreModeloLowerActual + ")";
					mr += separacion3 + "self.apibd." + getNombreMetodo_insertar(o.Union) + "(" + nombreModeloUnionLowerActual + ")";
					mr += separacion3 + "return " + nombreModeloLowerActual;
					
					
					//mr += separacion2 + "return "+getNombreMetodoGet_EnBD_ManyToMany(o)+"(this.idkey,"+nombreModeloLowerActual+".idkey);";
					mr += separacion2 + "return " + nombreModeloLowerActual;
				}
				
				
				
			}
			
			//mr += separacion1 + "def getStr(self, textoInicial=\"\"):";
			mr += separacion1 + "def " + getNombreMetodoSave(m) + "(self):";
			mr += separacion2 + "if self.idkey is None:";
			mr += separacion3 + "return self.apibd." + getNombreMetodo_insertar(m) + "(self)";
			mr += separacion2 + "return self.apibd." + getNombreMetodoUpdate(m) + "(self)";
			
			mr += separacion1 + "def " + getNombreMetodoDelete_EnModelo(m) + "(self):";
			mr += separacion2 + "if self.idkey is not None:";
			if (E.necistaUnDeleteCascade(m)) {
				mr += separacion3 + "self.apibd." + getNombreMetodoDeleteForID_Cascade(m) + "(self.idkey)";
			} else {
				mr += separacion3 + "self.apibd." + getNombreMetodoDeleteForID(m) + "(self.idkey)";
			}
			
			
			
			mr += separacion1 + "def getStr(self, textoInicial=\"\"):";
			mr += separacion2 + "s = self";
			mr += separacion2 + "return strg(textoInicial,\"" + this.getNombreStrModelo(m) + ": idkey=\", s.idkey";
			//string separacion3 = getSeparacionln(3,separacion0);
			for (int i = 0; i < columnasStr.Length; i++) {
				string c = columnasStr[i];
				mr += separacion3 + ",\" " + c + "=\",s." + c;
			}
			mr += separacion2 + ")";
			
				
			return mr;
		
		}
		public override string getStrBD(int separacion0)
		{	
			string separacion = getSeparacionln(0, separacion0);
			string bd = "";
			bd += separacion + "from RenePy.Utiles import *";
			bd += separacion + "from RenePy.ClasesUtiles.BasesDeDatos.BDUpdates import *";
			bd += separacion + "from RenePy.ClasesUtiles.BasesDeDatos.BDSesionStorage import *";
			bd += separacion + "from RenePy.ClasesUtiles.BasesDeDatos.BasicoBD import *";
			bd += separacion + "from " + factory.DireccionPaquete + "." + factory.NombreArchivoQueContieneTodosLosModelos + " import *";
			EsquemaBD E = factory.Esquema;
//			for (int i = 0; i < E.getCantidadDeModelos(); i++) {
//				ModeloBD_ID mt=(ModeloBD_ID)E.getModelo(i);
//				bd +=separacion+ "from "+factory.DireccionPaquete+"."+this.getNombreStrModelo(mt)+" import *";
//			}
			
			bd += separacion + "class " + datosConexionFactory.NombreBDAdmin + "(BasicoBD):";
			//string separacion1="\n\t";
			string separacion1 = getSeparacionln(1, separacion0);
			

			bd += separacion1 + "def __init__(self,url=None):";
			string separacion2 = getSeparacionln(2, separacion0);
			string separacion3 = getSeparacionln(3, separacion0);
			if (getDBC().tipoDeConxion == TipoDeConexionBD.SQL_LITE) {
				bd += separacion2 + "if url is None:";
                //bd += separacion3 + "self.urlBD=\"" + factory.DireccionBDSqlite + "\"";
                bd += separacion3 + "self.urlBD=\"" + getDBC().url + "\"";
                bd += separacion2 + "else:";
				bd += separacion3 + "self.urlBD=url";
				bd += separacion2 + "self.BD: BDConexion = BDConexion." + getDSC().NombreMetodoGetConexionSQL_LITE + "(self.urlBD)";
			}
			bd += separacion2 + "self.__Upd: BDUpdates = BDUpdates(self.BD)";
			if (factory.Esquema.UsarSesionStorage) {
				bd += separacion2 + "self.__SesionStorage=BDSesionStorage(self.BD)";
			}
			bd += separacion2 + "self.usarUpdater=" + (factory.Esquema.UsarUpdate ? "True" : "False");
			
			bd += separacion1 + "def " + getNombreMetodoUrlBD() + "(self):";
			bd += separacion2 + "return self.urlBD";
			
			
			
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
				bool existeSoloEste = mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste.Count == 1;
				for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste.Count; j++) {
					List<ColumnaDeModeloBD> lc = mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste[j];
					bd += getStrMetodoExiste_ForListaDeColumnas(mt, lc, existeSoloEste, distancia); 
				}
				//CrearDeleteCascade Cr = factory.Esquema.listaCrearDeleteCascade[mt];
				if (factory.Esquema.necistaUnDeleteCascade(mt)) {
					bd += getStrMetodoDeleteForID_Cascade(mt, factory.Esquema, distancia);
				}
			}

            int separacionActual = 2;
            bd += getStrMetodoCrearTodasLasTablas(separacionActual);
            bd += getStrMetodoCrearTodasLasTablasSiNoExisten(separacionActual);

            //         bd += separacion1 + "def " + getNombreMetodoCrearTodasLasTablas() + "(self):";
            //for (int i = 0; i < E.getCantidadDeModelos(); i++) {
            //	ModeloBD mt = E.getModelo(i);
            //	bd += separacion2 + "self." + getNombreMetodoCrearTabla(mt) + "()";
            //}
            //if (factory.Esquema.UsarSesionStorage) {
            //	bd += separacion2 + "self.__SesionStorage." + getDSC().SesionStorage.NombreMetodoCrearTablaYBorrarSiExiste + "()";
            //}

            //bd += separacion1 + "def " + getNombreMetodoCrearTodasLasTablasSiNoExisten() + "(self):";
            //for (int i = 0; i < E.getCantidadDeModelos(); i++) {
            //	ModeloBD mt = E.getModelo(i);
            //	bd += separacion2 + "self." + getNombreMetodoCrearTablaSiNoExiste(mt) + "()";
            //}
            //if (factory.Esquema.UsarSesionStorage) {
            //	bd += separacion2 + "self.__SesionStorage." + getDSC().SesionStorage.NombreMetodoCrearTablaYBorrarSiExiste + "()";
            //}


            if (factory.Esquema.UsarSesionStorage) {
                //bd += separacion1 + "def getSesionStorage(self):";
                //bd += separacion2 + "return self.__SesionStorage";

                bd += getStrMetodoGetSesionStorage(separacionActual);

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
//					OneToMany o=mt.ListaOneToMany[j];
//					bd += getStrMetodoGetListaDe_OneToManyLinkInterno(o,distancia);
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
			
			
			//string separacion3 = getSeparacionln(3,separacion0);
			return bd;
		}


        public override string getStrMetodoGetSesionStorage(int separacion0)
        {
            string separacion1 = getSeparacionln(0, separacion0);
            string separacion2 = getSeparacionln(1, separacion0);
            string bd = "";
            bd += separacion1 + "def getSesionStorage(self):";
            bd += separacion2 + "return self.__SesionStorage";
            return bd;
        }
        public override string getStrMetodoCrearTodasLasTablasSiNoExisten(int separacion0)
        {
            EsquemaBD E = factory.Esquema;
            string separacion1 = getSeparacionln(0, separacion0);
            string separacion2 = getSeparacionln(1, separacion0);
            string bd = "";
            bd += separacion1 + "def " + getNombreMetodoCrearTodasLasTablasSiNoExisten() + "(self):";
            for (int i = 0; i < E.getCantidadDeModelos(); i++)
            {
                ModeloBD mt = E.getModelo(i);
                bd += separacion2 + "self." + getNombreMetodoCrearTablaSiNoExiste(mt) + "()";
            }
            if (factory.Esquema.UsarSesionStorage)
            {
                bd += separacion2 + "self.__SesionStorage." + getDSC().SesionStorage.NombreMetodoCrearTablaYBorrarSiExiste + "()";
            }
            return bd;
        }
        public override string getStrMetodoCrearTodasLasTablas(int separacion0)
        {
            EsquemaBD E = factory.Esquema;
            string separacion1 = getSeparacionln(0, separacion0);
            string separacion2 = getSeparacionln(1, separacion0);
            string bd = "";
            bd += separacion1 + "def " + getNombreMetodoCrearTodasLasTablas() + "(self):";
            for (int i = 0; i < E.getCantidadDeModelos(); i++)
            {
                ModeloBD mt = E.getModelo(i);
                bd += separacion2 + "self." + getNombreMetodoCrearTabla(mt) + "()";
            }
            if (factory.Esquema.UsarSesionStorage)
            {
                bd += separacion2 + "self.__SesionStorage." + getDSC().SesionStorage.NombreMetodoCrearTablaYBorrarSiExiste + "()";
            }

            return bd;
        }



        public override string getStrMetodoCrearTabla(ModeloBD m, int separacion0)
		{
			string separacion1 = getSeparacionln(0, separacion0);
			string mc = separacion1 + "def " + getNombreMetodoCrearTabla(m) + "(self):";
			string separacion2 = getSeparacionln(1, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			//mc += separacion2 + "self.BD.crearTablaYBorrarSiExiste("+nombreModelo+"." + m.Nombre;
			mc += separacion2 + "self.BD." + getDSC().NombreMetodoCrearTablaYBorrarSiExiste + "(" + nombreModelo + "." + m.Nombre;
			string separacion10 = getSeparacionln(5, separacion0);
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				//string nc = FactoryBD.getNombreStrColumnaModelo(m, c);
				string nc = c.Nombre;
				//mc += separacion10 + ",self." + nc;
				//mc += separacion10 + ",\"" + nc+"\"";
				mc += separacion10 + "," + nombreModelo + "." + nc;
				if (c.Tipo != null) {
					if (c.Tipo != TipoDeDatoSQL.VARCHAR) {
						mc += ",TipoDeDatoSQL." + c.Tipo.getValor();
					}
					if (c.Tipo == TipoDeDatoSQL.VARCHAR) {
						if (c.Tamaño != 256 && c.Tamaño > 0) {
							mc += "," + c.Tamaño;
						}
						
					}
				}
				if (c.Clasificaciones != null) {
					for (int j = 0; j < c.Clasificaciones.Length; j++) {
						mc += ",TipoDeClasificacionSQL." + c.Clasificaciones[j].getValor();
					}
				}
				
			}
			mc += separacion10 + ")";
			mc += separacion2 + "return self";
			return mc;
		}
		
		public override string getStrMetodoCrearTablaSiNoExiste(ModeloBD m, int separacion0)
		{
			string separacion1 = getSeparacionln(0, separacion0);
			string mc = separacion1 + "def " + getNombreMetodoCrearTablaSiNoExiste(m) + "(self):";
			string separacion2 = getSeparacionln(1, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			//mc += separacion2 + "self.BD.crearTablaYBorrarSiExiste("+nombreModelo+"." + m.Nombre;
			mc += separacion2 + "self.BD." + getDSC().NombreMetodoCrearTablaSiNoExiste + "(" + nombreModelo + "." + m.Nombre;
			string separacion10 = getSeparacionln(5, separacion0);
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				//string nc = FactoryBD.getNombreStrColumnaModelo(m, c);
				string nc = c.Nombre;
				//mc += separacion10 + ",self." + nc;
				//mc += separacion10 + ",\"" + nc+"\"";
				mc += separacion10 + "," + nombreModelo + "." + nc;
				if (c.Tipo != null) {
					if (c.Tipo != TipoDeDatoSQL.VARCHAR) {
						mc += ",TipoDeDatoSQL." + c.Tipo.getValor();
					}
					if (c.Tipo == TipoDeDatoSQL.VARCHAR) {
						if (c.Tamaño != 256 && c.Tamaño > 0) {
							mc += "," + c.Tamaño;
						}
						
					}
				}
				if (c.Clasificaciones != null) {
					for (int j = 0; j < c.Clasificaciones.Length; j++) {
						mc += ",TipoDeClasificacionSQL." + c.Clasificaciones[j].getValor();
					}
				}
				
			}
			mc += separacion10 + ")";
			mc += separacion2 + "return self";
			return mc;
		}
		
		
		public override string getStrMetodoGetArgs(ModeloBD m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			//string mr=separacion+"def get"+nombreModelo+"_Args(self, listaDeArgumentos):";
			string mr = separacion + "def " + getNombreMetodo_getArgs(m) + "(self, listaDeArgumentos):";
			string separacion1 = getSeparacionln(1, separacion0);
			mr += separacion1 + "return " + nombreModelo + "(";
			string separacionExtra = getSeparacionln(3, separacion0);
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				string nombreAtributo = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0) ? separacionExtra + "," : "";
				mr += nombreAtributo + "=" + getStrMetodoParse(c, "listaDeArgumentos[" + (i + 1) + "]");//"listaDeArgumentos["+(i+1)+"]";
			}
			mr += separacionExtra + ",idkey=listaDeArgumentos[0]";
			mr += separacionExtra + ",apibd=self";
			mr += separacionExtra + ")";
			return mr;
		}
		
		public override string getStrMetodoContentArgs(ModeloBD m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string mr = separacion + "def __content_" + nombreModelo + "(self, " + nombreModeloLower + ":" + nombreModelo + "):";
			string separacion1 = getSeparacionln(1, separacion0);
			mr += separacion1 + "lista = [";
			string separacionExtra = getSeparacionln(2, separacion0);
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				string nombreAtributo = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0) ? separacionExtra + "," : "";
				mr += "[" + nombreModelo + "." + c.Nombre + "," + nombreModeloLower + "." + nombreAtributo + "]";
			}
			mr += separacionExtra + "]";
			mr += separacion1 + "return lista";
			return mr;
		}
		
		public override string getStrMetodoGetForID(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			//string mr=separacion+"def get"+nombreModelo+"_id(self, id):";
			string mr = separacion + "def " + getNombreMetodo_GetForID(m) + "(self, id):";
			string separacion1 = getSeparacionln(1, separacion0);
			//mr+=separacion1+"O = self.BD.select_forID("+nombreModelo+"."+m.Nombre+", id)";
			mr += separacion1 + "O = self.BD." + getDSC().NombreMetodoGetForId + "(" + nombreModelo + "." + m.Nombre + ", id)";
			string separacion2 = getSeparacionln(2, separacion0);
			mr += separacion1 + "if O == None:";
			mr += separacion2 + "return None";
			mr += separacion1 + "return self." + getNombreMetodo_getArgs(m) + "(O)";
			return mr;
			
		}
		
		public override string getStrMetodoInsertar(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string mr = separacion + "def insertar" + nombreModelo + "(self," + nombreModeloLower + "):";
			string separacion1 = getSeparacionln(1, separacion0);
			mr += separacion1 + "if " + nombreModeloLower + ".idkey is None:";
			string separacion2 = getSeparacionln(2, separacion0);
            //mr += separacion2 + "id=self.BD." + getDSC().NombreMetodoInsertar + "(" + nombreModelo + "." + m.Nombre;
            mr += separacion2 + "id=self.BD." + getDSC().NombreMetodoInsertarConIdAutomatico + "(" + nombreModelo + "." + m.Nombre+",\"" + m.getPrimer_NombreColumnaKey() + "\",";
            string[] variables = new string[m.Columnas.Count];
			string separacionExtra = getSeparacionln(4, separacion0);

            mr += separacionExtra+",[";
            for (int i = 0; i < variables.Length; i++)
            {
                ColumnaDeModeloBD c = m.Columnas[i];

                //mr += (i!=0?",":"")+nombreModeloLower + "." +c.Nombre;
                mr += (i != 0 ? "," : "") + nombreModelo + "." + c.Nombre;
            }
            mr += "]";

            for (int i = 0; i < variables.Length; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				variables[i] = separacionExtra + "," + nombreModeloLower + "." + CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += variables[i];
			}
			mr += separacionExtra + ")";
			mr += separacion2 + "return self." + getNombreMetodo_GetForID(m) + "(id)";
			mr += separacion1 + "else:";
			mr += separacion2 + "id=self.BD." + getDSC().NombreMetodoInsertarSinIdAutomatico + "(" + nombreModelo + "." + m.Nombre + "," + nombreModeloLower + ".idkey";
			for (int i = 0; i < variables.Length; i++) {
				mr += variables[i];
			}
			mr += separacionExtra + ")";
			mr += separacion2 + "return self." + getNombreMetodo_GetForID(m) + "(" + nombreModeloLower + ".idkey)";
			return mr;
			
		
		}
		public override string getStrMetodoGetAll(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string mr = separacion + "def " + getNombreMetodoGetAll(m) + "(self):";
			string separacion2 = getSeparacionln(1, separacion0);
			mr += separacion2 + "O=self.BD." + getDSC().NombreMetodoSelectTodo + "(" + nombreModelo + "." + m.Nombre + ")";
			mr += separacion2 + "if O is not None:";
			string separacion3 = getSeparacionln(2, separacion0);
			mr += separacion3 + "return [ self." + getNombreMetodo_getArgs(m) + "(e) for e in O ]";
			mr += separacion2 + "return []";

			return mr;
		}
		public override string getStrMetodoUpdate(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string mr = separacion + "def " + getNombreMetodoUpdate(m) + "(self," + nombreModeloLower + "):";
			string separacion2 = getSeparacionln(1, separacion0);
			mr += separacion2 + "self.BD." + getDSC().NombreMetodoUpdateForId + "(" + nombreModelo + "." + m.Nombre + "," + nombreModeloLower + ".idkey";
			string separacionExtra = getSeparacionln(5, separacion0);
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				mr += separacionExtra + " , " + nombreModelo + "." + c.Nombre + " , " + nombreModeloLower + "." + CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				;
			}
			mr += ")";
			mr += separacion2 + "return self." + getNombreMetodo_GetForID(m) + "(" + nombreModeloLower + ".idkey)";
			
			return mr;
		}
		public override string getStrMetodoDeleteForID(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			
			string mr = separacion + "def " + getNombreMetodoDeleteForID(m) + "(self,id):";
			string separacion2 = getSeparacionln(1, separacion0);
			mr += separacion2 + "self.BD." + getDSC().NombreMetodoDeleteForId + "(" + nombreModelo + "." + m.Nombre + ",id)";
			
			return mr;
		}
		public override string getStrMetodoGetAll_ForColumna(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string mr = separacion + "def " + getNombreMetodoGetAll_ForColumna(m, c) + "(self," + nombreVariableColumna + "):";
			string separacion2 = getSeparacionln(1, separacion0);
			mr += separacion2 + "O=self.BD." + getDSC().NombreMetodoSelectWhere + "(" + nombreModelo + "." + m.Nombre + "," + nombreModelo + "." + c.Nombre + "," + nombreVariableColumna + ")";
			mr += separacion2 + "if O is not None:";
			string separacion3 = getSeparacionln(2, separacion0);
			mr += separacion3 + "return [ self." + getNombreMetodo_getArgs(m) + "(e) for e in O ]";
			mr += separacion2 + "return []";

			return mr;
		}
		public override string getStrMetodoGet_ForColumna(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string mr = separacion + "def " + getNombreMetodoGet_ForColumna(m, c) + "(self," + nombreVariableColumna + "):";
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			string separacion4 = getSeparacionln(3, separacion0);
			if (c.EsReferencia) {
				ModeloBD referencia = c.ReferenciaID;
				string nombreModeloColumnaReferencia = this.getNombreStrModelo(referencia);
				mr += separacion2 + "if isinstance(" + nombreVariableColumna + "," + nombreModeloColumnaReferencia + "):";
				mr += separacion3 + "O=self.BD." + getDSC().NombreMetodoSelectWhere + "(" + nombreModelo + "." + m.Nombre + "," + nombreModelo + "." + c.Nombre + "," + nombreVariableColumna + ".idkey)";
				mr += separacion2 + "else:";
				mr += separacion3 + "O=self.BD." + getDSC().NombreMetodoSelectWhere + "(" + nombreModelo + "." + m.Nombre + "," + nombreModelo + "." + c.Nombre + "," + nombreVariableColumna + ")";
				
				
				mr += separacion2 + "if O is None:";
				mr += separacion3 + "return None";
				mr += separacion2 + "return self." + getNombreMetodo_getArgs(m) + "(O)";
			} else {
			
				mr += separacion2 + "O=self.BD." + getDSC().NombreMetodoSelectWhere + "(" + nombreModelo + "." + m.Nombre + "," + nombreModelo + "." + c.Nombre + "," + nombreVariableColumna + ")";
				mr += separacion2 + "if O is None:";
				
				mr += separacion3 + "return None";
				mr += separacion2 + "return self." + getNombreMetodo_getArgs(m) + "(O)";
			
			}
			
			return mr;
		}
		public override string getStrMetodoDelete_ForColumna(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string mr = separacion + "def " + getNombreMetodoDelete_ForColumna(m, c) + "(self," + nombreVariableColumna + "):";
			string separacion2 = getSeparacionln(1, separacion0);
			if (c.EsReferencia) {
				ModeloBD referencia = c.ReferenciaID;
				string nombreModeloColumnaReferencia = this.getNombreStrModelo(referencia);
				mr += separacion2 + "if isinstance(" + nombreVariableColumna + "," + nombreModeloColumnaReferencia + "):";
				string separacion3 = getSeparacionln(2, separacion0);
				mr += separacion3 + "self.BD." + getDSC().NombreMetodoDelete + "(" + nombreModelo + "." + m.Nombre + "," + nombreModelo + "." + c.Nombre + "," + nombreVariableColumna + ".idkey)";
				mr += separacion2 + "else:";
				mr += separacion3 + "self.BD." + getDSC().NombreMetodoDelete + "(" + nombreModelo + "." + m.Nombre + "," + nombreModelo + "." + c.Nombre + "," + nombreVariableColumna + ")";
			} else {
				mr += separacion2 + "self.BD." + getDSC().NombreMetodoDelete + "(" + nombreModelo + "." + m.Nombre + "," + nombreModelo + "." + c.Nombre + "," + nombreVariableColumna + ")";
			}
//			if(c.EsReferencia){
//				ModeloBD referencia=c.ReferenciaID; 
//				string nombreVariableColumnaReferencia = CodeBDLenguaje.getNombreStrModeloLower(referencia);
//				string nombreModeloColumnaReferencia = this.getNombreStrModelo(referencia);
//				mr+= separacion + "public void " + getNombreMetodoDelete_ForColumna(m, c) + "(" + nombreModeloColumnaReferencia + " " + nombreVariableColumnaReferencia + "){";
//				mr+= separacion2 +getNombreMetodoDelete_ForColumna(m, c)+"("+nombreVariableColumnaReferencia+".idkey);";
//				mr += separacion + "}";
//			}
			
			return mr;
		}
		public override string getStrMetodoGetAll_ForListaDeColumnas(ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			
			
			
			string mr = separacion + "def " + getNombreMetodoGetAll_ForListaDeColumnas(m, C) + "(self,";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += ",";
				mr += nombreVariableColumna;
			}
			mr += "):";
			
			string separacion2 = getSeparacionln(1, separacion0);
			
			
			mr += separacion2 + "O=self.BD." + getDSC().NombreMetodoSelectWhere + "(" + nombreModelo + "." + m.Nombre;//+","+nombreModelo+"."+c.Nombre+","+nombreVariableColumna+");";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += ",";
				mr += nombreModelo + "." + c.Nombre + "," + nombreVariableColumna;
			}
			mr += ")";
			
			mr += separacion2 + "if O is None:";
			string separacion3 = getSeparacionln(2, separacion0);
			mr += separacion3 + "return []";
			mr += separacion2 + "return [ self." + getNombreMetodo_getArgs(m) + "(e) for e in O ]";
			

			return mr;
		}
		public override string getStrMetodoGet_ForListaDeColumnas(string nombreDelMetodo, ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			
			
			
			//string mr=separacion+"def "+getNombreMetodoGet_ForListaDeColumnas(m,C)+"(self,";
			string mr = separacion + "def " + nombreDelMetodo + "(self";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += ",";
				mr += nombreVariableColumna;
			}
			mr += "):";
			
			string separacion2 = getSeparacionln(1, separacion0);
			
			
			mr += separacion2 + "O=self.BD." + getDSC().NombreMetodoSelectWhereFirstRow + "(" + nombreModelo + "." + m.Nombre;//+","+nombreModelo+"."+c.Nombre+","+nombreVariableColumna+");";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += ",";
				mr += nombreModelo + "." + c.Nombre + "," + nombreVariableColumna;
			}
			mr += ")";
			
			mr += separacion2 + "if O is None:";
			string separacion3 = getSeparacionln(2, separacion0);
			mr += separacion3 + "return None";
			mr += separacion2 + "return self." + getNombreMetodo_getArgs(m) + "(O)";
			
			return mr;
		}
		public override string getStrMetodoDelete_ForListaDeColumnas(ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			
			
			
			string mr = separacion + "def " + getNombreMetodoDelete_ForListaDeColumnas(m, C) + "(self,";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += ",";
				mr += nombreVariableColumna;
			}
			mr += "):";
			
			string separacion2 = getSeparacionln(1, separacion0);
			
			mr += separacion2 + "self.BD." + getDSC().NombreMetodoDelete + "(" + nombreModelo + "." + m.Nombre;//+","+nombreModelo+"."+c.Nombre+","+nombreVariableColumna+");";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += ",";
				mr += nombreModelo + "." + c.Nombre + "," + nombreVariableColumna;
			}
			mr += ")";
			return mr;
		}
		public override string getStrMetodoGetAll_InnerJoin_ForListaDeColumnas(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			string mr = separacion + "def " + getNombreMetodoGetAll_InnerJoin_ForListaDeColumnas(m, elementosWhere) + "(self";
			for (int i = 0; i < elementosWhere.Count; i++) {
				ElementoPorElQueBuscar e = elementosWhere[i];
				string variableElemento = getNombreVariableElemento(e);
				//mr+=(i!=0?",":"");
				mr += ",";
				mr += variableElemento;
			}
			mr += "):";
			
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			string separacion4 = getSeparacionln(3, separacion0);
			string separacion5 = getSeparacionln(4, separacion0);
			
			
			mr += separacion2 + "O=" + __getStrInnerJoinAll(m, cadena, elementosWhere, 1);
			
			
			mr += separacion2 + "if O is None:";
			
			mr += separacion3 + "return []";
			mr += separacion2 + "return [ self." + getNombreMetodo_getArgs(m) + "(e) for e in O ]";
			return mr;
		}
		public override string getStrMetodoGet_InnerJoin_ForListaDeColumnas(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			string mr = separacion + "def " + getNombreMetodoGet_InnerJoin_ForListaDeColumnas(m, elementosWhere) + "(";
			for (int i = 0; i < elementosWhere.Count; i++) {
				ElementoPorElQueBuscar e = elementosWhere[i];
				string variableElemento = getNombreVariableElemento(e);
				mr += (i != 0 ? "," : "");
				mr += variableElemento;
			}
			mr += "):";
			
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			string separacion4 = getSeparacionln(3, separacion0);
			string separacion5 = getSeparacionln(4, separacion0);
			
			
			mr += separacion2 + "O=" + __getStrInnerJoinFirstRow(m, cadena, elementosWhere, 1);
			mr += separacion2 + "if O is None:";
			
			mr += separacion3 + "return None";
			mr += separacion2 + "return self." + getNombreMetodo_getArgs(m) + "(O)";
			
			return mr;
		}
		public override string getStrMetodoDelete_ForColumna_Cascade(ModeloBD_ID m, ColumnaDeModeloBD c, EsquemaBD E, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			//string nombreModeloLower=CodeBDLenguaje.getNombreStrModeloLower(m);
			string nombreVariableModelo = CodeBDLenguaje.getNombreStrModeloLower(m);//getNombreVariableElemento(m);
			string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string mr = separacion + "def " + getNombreMetodoDelete_ForColumna_Cascade(m, c) + "(self," + nombreVariableColumna + ",modeloQueLoLlamo=None):";
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			string separacion4 = getSeparacionln(3, separacion0);
			//mr+=__getStrDeletes_Cascade(m,listaCrearDeleteCascade,2);
			string separacionDeCascada = separacion2;
			string separacionDeCascada1 = separacion3;
			if (c.EsUnique) {
				mr += separacion2 + nombreVariableModelo + "=self." + getNombreMetodoGet_ForColumna(m, c) + "(" + nombreVariableColumna + ")";
			} else {
				mr += separacion2 + "l=self." + getNombreMetodoGetAll_ForColumna(m, c) + "(" + nombreVariableColumna + ")";
				mr += separacion2 + "for e in l:";
				mr += separacion3 + nombreVariableModelo + "=e";
				separacionDeCascada = separacion3;
				separacionDeCascada1 = separacion4;
			}
			List<ColumnaDeModeloBD> listaCascade = E.listaCrearDeleteCascade[m].ListaCascade;
			for (int i = 0; i < listaCascade.Count; i++) {
				ColumnaDeModeloBD cIneterna = listaCascade[i];
				ModeloBD mActual = cIneterna.Padre;
				string nombreModeloActual = this.getNombreStrModelo(mActual);
				string nombreModeloLowerActual = CodeBDLenguaje.getNombreStrModeloLower(mActual);
				//CrearDeleteCascade C=E.listaCrearDeleteCascade[mActual];
				
				if (E.necistaUnDeleteCascade(mActual)) {
					
					mr += separacionDeCascada + "if (not modeloQueLoLlamo is None) and isinstance(modeloQueLoLlamo," + nombreModeloActual + "):";
					mr += separacionDeCascada1 + "self." + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey)";
					mr += separacionDeCascada + "else:";
					mr += separacionDeCascada1 + "self." + getNombreMetodoDelete_ForColumna_Cascade(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey," + nombreVariableModelo + ")";//+"("+nombreVariableModelo+".idkey);";
					
					
					//mr+=separacionDeCascada+getNombreMetodoDelete_ForColumna_Cascade(mActual,cIneterna)+"("+nombreVariableModelo+".idkey)";
				} else {
					mr += separacionDeCascada + "self." + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey)";
				}
				
			}
			listaCascade = E.listaCrearDeleteCascadeInverso[m].ListaCascade;
			for (int i = 0; i < listaCascade.Count; i++) {
				ColumnaDeModeloBD cIneterna = listaCascade[i];
				ModeloBD mActual = cIneterna.ReferenciaID;
				string nombreModeloActual = this.getNombreStrModelo(mActual);
				string nombreModeloLowerActual = CodeBDLenguaje.getNombreStrModeloLower(mActual);

				string nombreVariableColumnaActual = getNombreVariableElemento(cIneterna);
				
				if (E.necistaUnDeleteCascade(mActual)) {
					
					mr += separacionDeCascada + "if (not modeloQueLoLlamo is None) and isinstance(modeloQueLoLlamo," + nombreModeloActual + "):";
					mr += separacionDeCascada1 + "self." + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ")";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada + "else:";
					mr += separacionDeCascada1 + "self." + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + "," + nombreVariableModelo + ")";//+"("+nombreVariableModelo+".idkey);";
					
					
					//mr += separacionDeCascada + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
				} else {
					mr += separacionDeCascada + "self." + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ")";//+"("+nombreVariableModelo+".idkey);";
				}
				
			}
			
			
			mr += separacion2 + "self." + getNombreMetodoDelete_ForColumna(m, c) + "(" + nombreVariableColumna + ")";
			//mr+=separacion2+"this.BD."+getDSC().NombreMetodoDelete+"("+nombreModelo+"."+m.Nombre+","+nombreModelo+"."+c.Nombre+","+nombreVariableColumna+");";
			
			
			
			//mr+=separacion+"}";
			return mr;
		}
		public override string getStrMetodoDeleteForID_Cascade(ModeloBD_ID m, EsquemaBD E, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreVariableModelo = CodeBDLenguaje.getNombreStrModeloLower(m);//getNombreVariableElemento(m);
			string nombreVariableColumna = getNombreVariableElemento(m);//CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string mr = separacion + "def " + getNombreMetodoDeleteForID_Cascade(m) + "(self," + nombreVariableColumna + ",modeloQueLoLlamo=None):";
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			string separacionDeCascada = separacion2;
			string separacionDeCascada1 = separacion3;
			
			mr += separacion2 + nombreVariableModelo + "=self." + getNombreMetodo_GetForID(m) + "(" + nombreVariableColumna + ")";
			List<ColumnaDeModeloBD> listaCascade = E.listaCrearDeleteCascade[m].ListaCascade;
			for (int i = 0; i < listaCascade.Count; i++) {
				ColumnaDeModeloBD cIneterna = listaCascade[i];
				ModeloBD mActual = cIneterna.Padre;
				string nombreModeloActual = this.getNombreStrModelo(mActual);
				string nombreModeloLowerActual = CodeBDLenguaje.getNombreStrModeloLower(mActual);
				//CrearDeleteCascade C=E.listaCrearDeleteCascade[mActual];
				
				if (E.necistaUnDeleteCascade(mActual)) {
					
					mr += separacionDeCascada + "if (not modeloQueLoLlamo is None) and isinstance(modeloQueLoLlamo," + nombreModeloActual + "):";
					mr += separacionDeCascada1 + "self." + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableColumna + ")";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada + "else:";
					mr += separacionDeCascada1 + "self." + getNombreMetodoDelete_ForColumna_Cascade(mActual, cIneterna) + "(" + nombreVariableColumna + "," + nombreVariableModelo + ")";//+"("+nombreVariableModelo+".idkey);";
					
					
					//mr+=separacionDeCascada+getNombreMetodoDelete_ForColumna_Cascade(mActual,cIneterna)+"("+nombreVariableColumna+")";//+"("+nombreVariableModelo+".idkey);";
				} else {
					mr += separacionDeCascada + "self." + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableColumna + ")";//+"("+nombreVariableModelo+".idkey);";
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
						
						mr += separacionDeCascada + "if (not modeloQueLoLlamo is None) and isinstance(modeloQueLoLlamo," + nombreModeloActual + "):";
						mr += separacionDeCascada1 + "self." + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ")";//+"("+nombreVariableModelo+".idkey);";
						mr += separacionDeCascada + "else:";
						mr += separacionDeCascada1 + "self." + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + "," + nombreVariableModelo + ")";//+"("+nombreVariableModelo+".idkey);";
						
						
						//mr += separacionDeCascada + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
					} else {
						mr += separacionDeCascada + "self." + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ")";//+"("+nombreVariableModelo+".idkey);";
					}
				
				}
			
			
			}
			
			
			mr += separacion2 + "self." + getNombreMetodoDeleteForID(m) + "(" + nombreVariableColumna + ")";
			
			return mr;
		}
		
		public override string getStrMetodoDelete_ForListaDeColumnas_Cascade(ModeloBD_ID m, List<ColumnaDeModeloBD> C, EsquemaBD E, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreVariableModelo = CodeBDLenguaje.getNombreStrModeloLower(m);//getNombreVariableElemento(m);
			
			string mr = separacion + "def " + getNombreMetodoDelete_ForListaDeColumnas_Cascade(m, C) + "(self,";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				//mr+=(i!=0?",":"");
				mr += ",";
				mr += nombreVariableColumna;
			}
			mr += ",modeloQueLoLlamo=None):";
			
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			
			string separacionDeCascada = separacion2;
			string separacionDeCascada1 = separacion3;
			
			mr += separacion2 + "l=" + "self." + getNombreMetodoGetAll_ForListaDeColumnas(m, C) + "(";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0 ? "," : "");
				mr += nombreVariableColumna;
			}
			mr += ")";
			mr += separacion2 + "for e in l:";
			mr += separacion3 + nombreModelo + " " + nombreVariableModelo + "=e";
			separacionDeCascada = separacion3;
			
			List<ColumnaDeModeloBD> listaCascade = E.listaCrearDeleteCascade[m].ListaCascade;
			for (int i = 0; i < listaCascade.Count; i++) {
				ColumnaDeModeloBD cIneterna = listaCascade[i];
				ModeloBD mActual = cIneterna.Padre;
				string nombreModeloActual = this.getNombreStrModelo(mActual);
				string nombreModeloLowerActual = CodeBDLenguaje.getNombreStrModeloLower(mActual);
				//CrearDeleteCascade Cc=E.listaCrearDeleteCascade[mActual];
				
				if (E.necistaUnDeleteCascade(mActual)) {
					
					mr += separacionDeCascada + "if (not modeloQueLoLlamo is None) and isinstance(modeloQueLoLlamo," + nombreModeloActual + "):";
					mr += separacionDeCascada1 + "self." + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey)";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada + "else:";
					mr += separacionDeCascada1 + "self." + getNombreMetodoDelete_ForColumna_Cascade(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey," + nombreVariableModelo + ")";//+"("+nombreVariableModelo+".idkey);";
					
					
					//mr+=separacionDeCascada+getNombreMetodoDelete_ForColumna_Cascade(mActual,cIneterna)+"("+nombreVariableModelo+".idkey)";
				} else {
					mr += separacionDeCascada + "self." + getNombreMetodoDelete_ForColumna(mActual, cIneterna) + "(" + nombreVariableModelo + ".idkey)";
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
					
					mr += separacionDeCascada + "if (not modeloQueLoLlamo is None) and isinstance(modeloQueLoLlamo," + nombreModeloActual + "):";
					mr += separacionDeCascada1 + "self." + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
					mr += separacionDeCascada + "else:";
					mr += separacionDeCascada1 + "self." + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + "," + nombreVariableModelo + ")";//+"("+nombreVariableModelo+".idkey);";
					
					
					
					//mr += separacionDeCascada + getNombreMetodoDeleteForID_Cascade(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ");";//+"("+nombreVariableModelo+".idkey);";
				} else {
					mr += separacionDeCascada + "self." + getNombreMetodoDeleteForID(mActual) + "(" + nombreVariableModelo + "." + nombreVariableColumnaActual + ")";//+"("+nombreVariableModelo+".idkey);";
				}
				
			}
			
			
			mr += separacion2 + getNombreMetodoDelete_ForListaDeColumnas(m, C) + "(";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += (i != 0 ? "," : "");
				mr += nombreVariableColumna;
			}
			mr += ")";
			
			return mr;
		}
		public override string getStrMetodoExiste(ModeloBD_ID m, ColumnaDeModeloBD c, bool soloHayEsteEnElModelo, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
			string mr = separacion + "def " + getNombreMetodoExiste(m, c, soloHayEsteEnElModelo) + "(self," + nombreVariableColumna + "):";
			string separacion2 = getSeparacionln(1, separacion0);
			
			mr += separacion2 + "return self.BD." + getDSC().NombreMetodoExiste + "(" + nombreModelo + "." + m.Nombre + "," + nombreModelo + "." + c.Nombre + "," + nombreVariableColumna + ")";
			
			
			
			
			return mr;
		}
		public override string getStrMetodoExiste_ForListaDeColumnas(string nombreMetodo, ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			string mr = separacion + "def " + nombreMetodo + "(self";// + getNombreTipoDeDato(c) + " " + nombreVariableColumna + ") throws Exception {";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				//mr += (i != 0 ? "," : "");
				mr += ",";
				mr += nombreVariableColumna;
			}
			mr += "):";
			
			string separacion2 = getSeparacionln(1, separacion0);
			
			string separacionEspecial = getSeparacionln(4, separacion0);
			mr += separacion2 + "return self.BD." + getDSC().NombreMetodoExiste + "(" + nombreModelo + "." + m.Nombre;// + "," + nombreModelo + "." + c.Nombre + "," + nombreVariableColumna + ");";
			for (int i = 0; i < C.Count; i++) {
				ColumnaDeModeloBD c = C[i];
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				//mr += (i != 0 ? "," : "");
				mr += separacionEspecial + ",";
				mr += nombreModelo + "." + c.Nombre + "," + nombreVariableColumna;
			}
			mr += ")";
			
			
			
			return mr;
		}
		
		public override string getStrMetodoExiste_ForID(ModeloBD_ID m, int separacion0)
		{
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			//string mr=separacion+"def get"+nombreModelo+"_id(self, id):";
			string mr = separacion + "def " + getNombreMetodo_GetForID(m) + "(self, id):";
			string separacion1 = getSeparacionln(1, separacion0);
			//mr+=separacion1+"O = self.BD.select_forID("+nombreModelo+"."+m.Nombre+", id)";
			mr += separacion1 + "O = self.BD." + getDSC().NombreMetodoGetForId + "(" + nombreModelo + "." + m.Nombre + ", id)";
			string separacion2 = getSeparacionln(2, separacion0);
			mr += separacion1 + "return O is not None";
			
			return mr;
		}
		
		public override string getStrMetodoGetAll_ForListaDeColumnas_Sort(SelectWhereSort s, int separacion0)
		{
			ModeloBD_ID m = s.Modelo;
			List<ColumnaDeModeloBD> C = s.ListaPorLasQueBuscar;
			string separacion = getSeparacionln(0, separacion0);
			string nombreModelo = this.getNombreStrModelo(m);
			string nombreModeloLower = CodeBDLenguaje.getNombreStrModeloLower(m);
			
			string mr = separacion + "def " + getNombreMetodoGetAll_ForListaDeColumnas_Sort(s) + "(self";//+getNombreTipoDeDato(c)+" "+nombreVariableColumna+"){";
			for (int i = 0; i < C.Count; i++) {
				//mr += (i != 0 ? "," : "");
				mr += ",";
				string nombreVariableColumna = null;

				mr += getNombreVariableElemento(C[i]);
				
			}
			mr += "):";
			
			string separacion2 = getSeparacionln(1, separacion0);
			string separacion3 = getSeparacionln(2, separacion0);
			string separacion4 = getSeparacionln(3, separacion0);
			
			//mr += separacion2 + "List<" + nombreModelo + "> lista=new ArrayList<>();";
			mr += separacion2 + "O=self.BD." + getDSC().NombreMetodoSelectWhereOrderBy + "(" + nombreModelo + "." + m.Nombre + ",";//+","+nombreModelo+"."+c.Nombre+","+nombreVariableColumna+");";
			mr += separacion3 + "[";
			for (int i = 0; i < C.Count; i++) {
				string nombreVariableColumna = getNombreVariableElemento(C[i]);
				string nombreColumna = null;
				ColumnaDeModeloBD c = C[i];//(ColumnaDeModeloBD)C[i];
					
				nombreColumna = c.Nombre;

				
				mr += separacion4 + (i != 0 ? "," : "");
				mr += nombreModelo + "." + nombreColumna + "," + nombreVariableColumna;
			}
			mr += separacion3 + "]";
			for (int i = 0; i < s.ListaPorLasQueOrdenar.Count; i++) {
				SelectWhereSort.ColumnaYOrden co = s.ListaPorLasQueOrdenar[i];
				ColumnaDeModeloBD c = co.Columna;
				string nombreVariableColumna = CodeBDLenguaje.getNombreStrColumnaModelo(m, c);
				mr += separacion4 + ",";
				mr += nombreModelo + "." + c.Nombre;//+ "," + nombreVariableColumna;
				if (co.Ordenamiento != null) {
					mr += "," + co.Ordenamiento.getValor();
				}
			}
			mr += ")";
			
			mr += separacion2 + "if O is None:";
			
			mr += separacion3 + "return []";
			mr += separacion2 + "return [ self." + getNombreMetodo_getArgs(m) + "(e) for e in O ]";
			return mr;
			
		}
		
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
			
			//mr+=separacion2+"this.BD."+getDSC().NombreMetodoSelectWhereInnerJoin+"("+nombreModelo+"."+m.Nombre;//+","+nombreModelo+"."+c.Nombre+","+nombreVariableColumna+");";
			mr += "self.BD." + nombreMetodoBD + "(" + nombreModelo + "." + m.Nombre;//+","+nombreModelo+"."+c.Nombre+","+nombreVariableColumna+");";
			mr += separacion3 + ",[";
			int pos = 0, j = 0;
			for (int i = 0; i < cadena.Count; i++) {
				ElementoPorElQueBuscar e = cadena[i];
				ModeloBD mEnInner = getModeloDeElemento(e);
				string nombreModeloEnInner = this.getNombreStrModelo(mEnInner);
				string nombreModeloLowerEnInner = CodeBDLenguaje.getNombreStrModeloLower(mEnInner);
				
				string nombreColumna = "";
				if (e is ColumnaDeModeloBD) {
					ColumnaDeModeloBD c = (ColumnaDeModeloBD)e;
					nombreColumna = "." + c.Nombre;
				}
				string par = "[" + nombreModeloEnInner + "." + mEnInner.Nombre + (nombreColumna.Length > 1 ? "," + nombreModeloEnInner + nombreColumna : "") + "]";
				
				if (i == 0 && cadena.Count == 1) {
					mr += par;
				}
				
				if (pos == 0) {
					mr += (j != 0 ? "," : "");
					mr += separacion4 + "[";
					mr += separacion5 + par;
				} else if (pos == 1) {
					mr += ",";
					mr += separacion5 + par;
					mr += separacion4 + "]";
					j++;
				}
				pos = (pos + 1) % 2;
			
			}
			mr += separacion3 + "]";
			
			mr += separacion3;
			pos = 0;
			j = 0;
			for (int i = 0; i < elementosWhere.Count; i++) {
				ElementoPorElQueBuscar e = elementosWhere[i];
				ModeloBD mEnInner = getModeloDeElemento(e);
				string nombreModeloEnInner = this.getNombreStrModelo(mEnInner);
				string nombreModeloLowerEnInner = CodeBDLenguaje.getNombreStrModeloLower(mEnInner);
				
				string nombreColumna = "";
				if (e is ColumnaDeModeloBD) {
					ColumnaDeModeloBD c = (ColumnaDeModeloBD)e;
					nombreColumna = "." + c.Nombre;
				}
				string par = nombreModeloEnInner + "." + mEnInner.Nombre + (nombreColumna.Length > 1 ? "," + nombreModeloEnInner + nombreColumna : "");
				
				
				mr += separacion3 + "," + par + "," + getNombreVariableElemento(e);
			}
			
			mr += ")";
			
			
			
			return mr;
		}
		//		protected override string __getStrDeletes_Cascade(ModeloBD m,Dictionary<ModeloBD,CrearDeleteCascade> listaCrearDeleteCascade,int separacion0){
		//			string separacion=getSeparacionln(0,separacion0);
		//			string mr="";
		//
		//			return mr;
		//		}
		
		
		
		public string getStrMetodoParse(ColumnaDeModeloBD c, string variable)
		{
			return getStrMetodoParse(c.Tipo, variable);
		}
		public string getStrMetodoParse(TipoDeDatoSQL tipo, string variable)
		{
			
			metodoCreador1<TipoDeDatoSQL,String> getNombreMetodoParse = t => {
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
				if (t == TipoDeDatoSQL.INTEGER) {
					return "toInt";
				} 
				if (t == TipoDeDatoSQL.POINT) {
					return "toPoint";
				} 
				if (t == TipoDeDatoSQL.REAL || t == TipoDeDatoSQL.DOUBLE_PRECISION) {
					return "toFloat";
				} 
//								if( t==TipoDeDatoSQL.VARCHAR){
//									return "";
//								}
				return null;
			};
			string nombreMetodo = getNombreMetodoParse(tipo);
			return nombreMetodo != null ? "self." + nombreMetodo + "(" + variable + ")" : variable;
		}
		
	}
}
