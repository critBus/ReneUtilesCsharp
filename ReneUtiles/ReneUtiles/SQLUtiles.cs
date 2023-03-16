using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using ReneUtiles.Clases.BD;

using ReneUtiles.Clases.ExprecionesRegulares;
using ReneUtiles.Clases;

namespace ReneUtiles
{
	public class SQLUtiles
	{

		public const String CREATE = "CREATE",
			TABLE = "TABLE",
			CREATE_TABLE = CREATE + " " + TABLE,
			PRIMARY_KEY = "PRIMARY KEY",
			INSERT = "INSERT",
			INTO = "INTO",
			INSERT_INTO = INSERT + " " + INTO,
			VALUES = "VALUES",
			SELECT = "SELECT",
			MAX = "MAX",
			MIN = "MIN",
			AVG = "AVG",
			SELECT_MAX = SELECT + " " + MAX,
			SELECT_MIN = SELECT + " " + MIN,
			SELECT_AVG = SELECT + " " + AVG,
			FROM = "FROM",
			DROP = "DROP",
			DROP_TABLE = DROP + " " + TABLE,
			COPY = "COPY",
			WHERE = "WHERE",
			ORDER_BY = "ORDER BY",
			DISTINCT = "DISTINCT",
			SELECT_DISTINCT = SELECT + " " + DISTINCT,
			INNER_JOIN = "INNER JOIN",
			ON = "ON",
			GROUP_BY = "GROUP BY",
			HAVING = "HAVING",
			COUNT = "COUNT",
			SUM = "SUM",
			SELECT_COUNT = SELECT + " " + COUNT,
			SELECT_SUM = SELECT + " " + SUM,
			UPDATE = "UPDATE",
			SET = "SET",
			DELETE = "DELETE",
			IF_EXISTS = "IF EXISTS",
			DROP_TABLE_IF_EXISTS = DROP_TABLE + " " + IF_EXISTS,
			AUTOINCREMENT = "AUTOINCREMENT",
			IF_NOT_EXISTS = "IF NOT EXISTS",
			CREATE_TABLE_IF_NOT_EXISTS = CREATE_TABLE + " " + IF_NOT_EXISTS,
			DEFAULT = "DEFAULT",
			ALTER = "ALTER",
			ALTER_TABLE = ALTER + " " + TABLE,
			COLUMN = "COLUMN",
			DROP_COLUMN = DROP + " " + COLUMN,
			CASCADE = "CASCADE",
			RENAME = "RENAME",
			RENAME_COLUMN = RENAME + " " + COLUMN,
			TO = "TO",
			ADD = "ADD",
			ADD_COLUMN = ADD + " " + COLUMN,
			NOT_NULL = "NOT NULL";

        public string idKeyDefault = "Id";

        public virtual string getStrValor(object valor)
        {
            if (valor==null) {
                return "NULL";
            }
            return separadorComillas() + "" + valor.ToString().Replace(separadorComillas(), separadorComillasContrario()) + "" + separadorComillas();

        }
        public virtual string separadorComillas() {
            return "\"";
        }
        public virtual string separadorComillasContrario()
        {
            return "'";
        }

        public virtual string strC(object columna)
        {
            string c = columna+"";
            if (c!="*"&&!c.StartsWith(separadorComillas())) {
                c= separadorComillas() + c + separadorComillas();
            }
            return c;
        }
        public class Configuracion_De_Columnas
		{

			public Dictionary<string, HashSet<string>> columnasSeleccionadas;
            private SQLUtiles sqlUtil;

			public  Configuracion_De_Columnas(SQLUtiles sqlUtil)
			{
                this.sqlUtil = sqlUtil;

                this.columnasSeleccionadas = new Dictionary<string, HashSet<string>>();
			}

			public string getNombreDeTabla(int indice)
			{
				//Dictionary<string, HashSet<string>>.KeyCollection k;
//        	/k.ElementAt
				return this.columnasSeleccionadas.Keys.ElementAt(indice);
			}


			public HashSet<string> get(string nombreTabla)
			{
				return this.columnasSeleccionadas[nombreTabla];
			}

			public HashSet<string> add(string nombreTabla)
			{

				if (this.columnasSeleccionadas.ContainsKey(nombreTabla)) {
					return get(nombreTabla);
				}
				HashSet<string> l = new HashSet<string>();
				this.columnasSeleccionadas.Add(nombreTabla, l);
				return l;
			}

			public HashSet<string> addSiNoContiene(string nombreTabla, string columna)
			{

				if (this.columnasSeleccionadas.ContainsKey(nombreTabla)) {
					HashSet<string> l = get(nombreTabla);
					l.Add(columna);
					return l;
				}
//            System.out.println("Agrego nuevo");
				HashSet<string> l2 = new HashSet<string>();
				l2.Add(columna);
				this.columnasSeleccionadas.Add(nombreTabla, l2);
				return l2;
			}

			public string getParTC_siSeEncuentra(string columna)
			{
				string[] keys = this.columnasSeleccionadas.Keys.ToArray<string>();
				for (int i = 0; i < keys.Length; i++) {
					if (this.columnasSeleccionadas[keys[i]].Contains(columna)) {
						return keys[i] + "." + this.sqlUtil.strC(columna);
					}
				}
				return this.sqlUtil.strC(columna);
			}

            
            

			public string getC_T(Object[] lista)
			{
            
				string c = lista.Length == 1 ? this.sqlUtil.idKeyDefault : lista[1].ToString();
				string t = lista[0].ToString();
//            System.out.println("t="+t);
				addSiNoContiene(t, c);
				return t + "." + this.sqlUtil.strC(c);
			}
			//        public List<String> nombreTalas;
			//        public List<List<String>> nombreTalas;
		}

		public  string __getStr_ORDER_BY(Object[] a, int inicioDeColumnas)
		{
			a=adaptarTipos(a);
			string sql = "";

			for (int i = inicioDeColumnas; i < a.Length; i++) {
				bool esOrdenamiento = TipoDeOrdenamientoSQL.esTipoDeOrdenamientoSQL(a[i]);
				if (esOrdenamiento) {
					sql += " " + ((TipoDeOrdenamientoSQL)a[i]).getValor();
				} else {
					if (i != inicioDeColumnas) {
						sql += " , ";
					}
					String t = "", c = "";
					if (esArreglo(a[i])) {
						Object[] A = (Object[])a[i];
						t = A[0] + "";
						if (!(A.Length > 1)) {
							c = this.idKeyDefault;
						} else {
							c = A[1] + "";
							sql += t + "." + strC(c);
						}
					} else {
						sql += a[i];
					}
				}

			}
			if (sql.Length == 0) {
				return "";
			}
			return " " + ORDER_BY + " " + sql;
		}

		public  string __getStr_Where(Object[] a, int inicioDePares)
		{
			return __getStr_Where(a, new Configuracion_De_Columnas(this), inicioDePares);
		}

		public  string __getStr_Where(Object[] a)
		{
			return __getStr_Where(a, new Configuracion_De_Columnas(this), 0);
		}

		public  string __getStr_Where(Object[] a, Configuracion_De_Columnas cnf, int inicioDePares)
		{
			a=adaptarTipos(a);
			string sqlWhere = "";
			if (inicioDePares < 0) {
				inicioDePares = 0;
			}
			if (cnf == null) {
				cnf = new Configuracion_De_Columnas(this);
			}
			int pos = 0;
			for (int i = inicioDePares; i < a.Length; i++) {
				if ((i != inicioDePares) && (pos == 0)) {
					sqlWhere += " AND ";
				}
				if (pos == 0) {
					if (esArreglo(a[i])) {
						string t = "", c = "";
						Object[] A = (Object[])a[i];
						t = A[0] + "";
						if (!(A.Length > 1)) {
							c = this.idKeyDefault;
						} else {
							c = A[1] + "";
						}
						if (cnf != null) { //&&cnf.addSiNoContiene!=None:
							cnf.addSiNoContiene(t, strC(c));
						}
						sqlWhere += t + "." + strC(c);
					} else {
						if (cnf != null) { //and cnf.getParTC_siSeEncuentra!=None:
							sqlWhere += cnf.getParTC_siSeEncuentra(a[i] + "");
						} else {
							sqlWhere += strC(a[i]);
						}
					}
				} else if (pos == 1) {
					sqlWhere += " = ";
					if (esArreglo(a[i])) {
						string t = "", c = "";
						Object[] A = (Object[])a[i];
						t = A[0] + "";
						if (!(A.Length > 1)) {
							c = this.idKeyDefault;
						} else {
							c = A[1] + "";
						}
						if (cnf != null) {//and cnf.addSiNoContiene != None:
							cnf.addSiNoContiene(t, strC(c));
						}
						sqlWhere += t + "." + strC(c);
                    
					} else if (
						(a[i] + "").StartsWith("(")
						&& (a[i] + "").EndsWith(")")
						&& esSelect(Utiles.subs((a[i] + ""), 1, (a[i] + "").Length - 2))) {
						sqlWhere += a[i];
					} else {
						sqlWhere += "" + getStrValor(a[i]) + "";
					}

				}
				pos = (pos + 1) % 2;

			}
			if (!(sqlWhere.Length == 0)) {
				sqlWhere = " " + WHERE + " " + sqlWhere;
			}
			return sqlWhere;
		}

		public  string __getFrom_Inner_Join_Where_ORDER_BY(string select, Object[] args)
		{
			Configuracion_De_Columnas cnf = new Configuracion_De_Columnas(this);
			Object[] tw = (Object[])args[1];
			return __getFrom_Inner_Join(select, args, cnf) + __getStr_Where(tw, cnf, 0) + __getStr_ORDER_BY(args, 2);
		}

		public  string __getFrom_Inner_Join_Where(string select, Object[] args)
		{
			Configuracion_De_Columnas cnf = new Configuracion_De_Columnas(this);
			return __getFrom_Inner_Join(select, args, cnf) + __getStr_Where(args, cnf, 1);
		}

		public  string __getFrom_Inner_Join(string select, Object[] a)
		{
			return __getFrom_Inner_Join(select, a, new Configuracion_De_Columnas(this));
		}

    
		/// <summary>
		/// 
		///  ELEMENTO RELACIONES ENTRE TABLAS (ON): siempre son [Pares]
		/// 
		/// plq la lista de ellos es ejemplo:
		/// 
		/// [ [1 [T],[T,CID],[T,CID],[T] ] , [2 [T,CID],[T] ] , [3
		/// [T,CID],[T,CID] ] ]
		/// 
		/// recordar que siempre la un T en uno de los i tiene que aparecer en el
		/// siguiente(i+1) pq es un recorrido
		/// 
		/// 
		/// TABLA.CULUMNA_REFERENCIA_A_ID == a
		/// 
		/// [T] ,[T,CID] a TABLA.COLUMNA_NOMBRE_PERSONALIZADO_ID
		/// 
		/// 
		/// [T,CID],[T] a TABLA.ID el default dado de automatico
		/// 
		/// 
		/// [T,CID],[T,CID]
		/// 
		/// 
		/// 
		/// 
		/// 
		/// 
		/// PAR COLUMNA VALOR
		/// 
		/// [T,C],[T,C]
		/// 
		/// 
		/// [T,C],V
		/// 
		/// 
		/// C,[T,C]
		/// 
		/// 
		/// C,V
		/// 
		/// 
		/// [T],[T,C] la [T] es [T,this.idKeyDefault]
		/// 
		/// 
		/// [T],V la [T] es [T,this.idKeyDefault]
		/// 
		/// 
		/// C,[T] la [T] es [T,this.idKeyDefault]
		/// 
		/// 
		/// 
		/// 
		/// (listaDe ELEMENTO RELACIONES ENTRE TABLAS,paresColumnaValor)
		/// 
		/// 
		/// ([TABLA,COLUMNA_REFERENCIA_ID],paresColumnaValor)
		/// </summary>
		/// <param name="select"></param>
		/// <param name="a"></param>
		/// <param name="cnf"></param>
		/// <returns></returns>
		public  string __getFrom_Inner_Join(string select, Object[] a, Configuracion_De_Columnas cnf)
		{
			a=adaptarTipos(a);
			if (cnf == null) {
				cnf = new Configuracion_De_Columnas(this);
			}
			string sqlOn = "";
			Object[] A0 = (Object[])a[0];
			int leng = A0.Length;
			List<string> lSqlOn = new List<string>();

			if (leng > 0) {
				if (A0[0] is string) {
					//Nada por ahora
				} else {
					string tabla1inerjoin = null;  // la que se encuentra tambien en a[0][1]
					string tablaFrom = null;
					int pos = 0;
					int len2 = A0.Length; //len(a[0])
					for (int i = 0; i < len2; i++) {
						sqlOn = "";
						Object[] l = (Object[])A0[i];
						leng = l.Length;
						for (int j = 0; j < leng; j++) {
							if ((j != 0) && (pos == 0)) {
								sqlOn += " AND ";
							}
							if (pos == 0) {
//                            System.out.println("add 0 a "+l[j]);
								sqlOn += cnf.getC_T((Object[])l[j]);//(Object[][]), j
							}
							if (pos == 1) {
//                            System.out.println("add 1 a "+l[j]);
								sqlOn += " = " + cnf.getC_T((Object[])l[j]);//(Object[][]) , j
							}
							pos = (pos + 1) % 2;
							if ((i == 1) && (tabla1inerjoin == null)) {
								Object[] l_0 = (Object[])l[j];
								if (l_0[0] == cnf.getNombreDeTabla(1)) {
									tabla1inerjoin = cnf.getNombreDeTabla(1);
									tablaFrom = cnf.getNombreDeTabla(0);
								}
							}
                        

						}
						lSqlOn.Add(sqlOn);

					}
					if (tabla1inerjoin == null) {
						tabla1inerjoin = cnf.getNombreDeTabla(0);
						tablaFrom = cnf.getNombreDeTabla(1);
					}
					sqlOn = FROM + " " + tablaFrom + " " + INNER_JOIN + " " + tabla1inerjoin;
					len2 = lSqlOn.Count;
					for (int i = 0; i < len2; i++) {
						if (i != 0) {
							sqlOn += " " + INNER_JOIN + " " + cnf.getNombreDeTabla(i + 1);
						}
						sqlOn += " " + ON + " " + lSqlOn[i];
					}
				}
			}
			return select + sqlOn;
		}

		public  string addColumn(string nombreTabla, string Columna, TipoDeDatoSQL tipo, string defaultt, Object clasificacion)
		{
			clasificacion=getObjectCorrecto(clasificacion);
			if (tipo == null) {
				tipo = TipoDeDatoSQL.VARCHAR;
			}
			if (defaultt == null) {
				defaultt = tipo.getPorDefecto(); 
			}
			if (clasificacion == null) {
				clasificacion = "";
			} else if (TipoDeClasificacionSQL.esTipoDeClasificacionSQL(clasificacion)) {
				clasificacion = "  " + ((TipoDeClasificacionSQL)clasificacion).getValor();
			}
			return ALTER_TABLE + " " + nombreTabla + " " + ADD_COLUMN + " " + strC(Columna) + " " + tipo.getValor() + " " + DEFAULT + " " + defaultt + clasificacion;
		}

		public  string renombrarColumna(string nombreTabla, string Columna, string NuevoNombre)
		{
			return ALTER_TABLE + " " + nombreTabla + " " + RENAME_COLUMN + " " + strC(Columna) + " " + TO + " " + NuevoNombre;
		}

		public  string eliminarColumna(string nombreTabla, string Columna)
		{
			return ALTER_TABLE + " " + nombreTabla + " " + DROP_COLUMN + " " + Columna + " " + CASCADE;
		}

		public  bool esInsertar(string sql)
		{
			return sql.Trim().ToUpper().StartsWith(INSERT_INTO + " ");
		}
            
            
		//anterior !!!!!!!!!!!!!!!!!!!!!!!!!!!
		public  string select_Id(string nombreTabla, int id)
		{
			return select_Where(nombreTabla, strC(this.idKeyDefault), id);
		}
        public string select_Id(string nombreTabla, string idStr,int id)
        {
            return select_Where(nombreTabla, strC(idStr), id);
        }

        public  bool esDELETE(string sql)
		{

			return sql.Trim().ToUpper().StartsWith(DELETE + " ");
		}

		public  bool esUPDATE(string sql)
		{
			return sql.Trim().ToUpper().StartsWith(UPDATE + " ");
		}

		public  bool esINSERT(string sql)
		{
			return sql.Trim().ToUpper().StartsWith(INSERT + " ");
		}

		public  bool esDROP(string sql)
		{
			return sql.Trim().ToUpper().StartsWith(DROP + " ");
		}

		public  bool esCREATE(string sql)
		{
			return sql.Trim().ToUpper().StartsWith(CREATE + " ");
		}

		public virtual bool esSelect(string sql)
		{
			return sql.Trim().ToUpper().StartsWith(SELECT + " ");
		}

		public virtual bool  esSelectValor(string sql)
		{
			string sub = sql.Trim().ToUpper().Substring((SELECT + " ").Length);
			//System.out.println("sub="+sub);
			return (Utiles.startsWithOR(sub, MAX, MIN, AVG, COUNT, SUM));

			//.startsWith(CREATE)
		}
        
	
		public  string getCantidad_Where(String nombreTabla, params Object[] paresColumnaValor)
		{
			string columna = paresColumnaValor[0] + "";
			return getCantidad(nombreTabla, columna) + __getStr_Where(paresColumnaValor);
		}

		public  string getCantidad_Where_Inner_Join(string nombreTabla, string columna, params Object[] args)
		{
			args=adaptarTipos(args);
			return __getFrom_Inner_Join_Where(SELECT_COUNT + "(" + nombreTabla + "." + strC(columna) + ") ", args);
		}
	
    
		/// <summary>
		/// (nombre,.. nombreColumna,TipoDeDatoSQL,capacidad#,isKeyPrimary o
		/// tipoDeClasificacionSQL)
		/// 
		/// (nombre,.. nombreColumna,TipoDeDatoSQL,capacidad#)
		/// asumo que no es
		/// llave primaria (nombre,.. nombreColumna,capacidad#) asumo que es VARCHAR
		/// 
		/// 
		/// (nombre,.. nombreColumna) asumo que es VARCHAR(255)
		/// 
		/// si el siguiente a nombreColumna no es un TipoDeDatoSQL asumo que es
		/// VARCHAR(255) 
		/// 
		/// si el siguiente a TipoDeDatoSQL no es un # asumo que es VARCHAR(255)
		/// </summary>
		/// <param name="nombreTabla"></param>
		/// <param name="NombreTipos"></param>
		/// <returns></returns>
		public  string crearTablaSiNoExiste(string nombreTabla, params Object[] NombreTipos)
		{
			NombreTipos=adaptarTipos(NombreTipos);
			return Utiles.replaceFirst(crearTabla(nombreTabla, NombreTipos), CREATE_TABLE, CREATE_TABLE_IF_NOT_EXISTS);
		}

		public  string drop_table_if_exist(string nombreTabla)
		{
			return DROP_TABLE_IF_EXISTS + " " + nombreTabla;
		}

		public  string delete_id(string nombreTabla, string id)
		{
			return delete(nombreTabla, this.idKeyDefault, id);
		}

        public string delete_id(string nombreTabla,string idStr, string id)
        {
            return delete(nombreTabla, idStr, id);
        }

        public  string delete(string nombreTabla, params Object[] a)
		{
			a = adaptarTipos(a);
			string sqlWhere = "";
			int pos = 0;

			for (int i = 0; i < a.Length; i++) {
            
				if (pos == 0) {
					sqlWhere += (!Utiles.isEmpty(sqlWhere) ? " AND " : "");//Ahora aqui
					sqlWhere += strC(a[i]);
                    
				} else if (pos == 1) {
					sqlWhere += " = " + getStrValor(a[i]) + "";
				}

				pos = (++pos) % 2;
			}
			return DELETE + " " + FROM + " " + nombreTabla + " " + WHERE + " " + sqlWhere;
		}

    
		/// <summary>
		/// (nombreTabla,id#,columna,setValor1,columna2,setValor2,... )
		/// </summary>
		/// <param name="nombreTabla"></param>
		/// <param name=this.idKeyDefault></param>
		/// <param name="paresColumnaValor"></param>
		/// <returns></returns>
		public  string update_Id(string nombreTabla, string id, params Object[] paresColumnaValor)
		{
			return update(nombreTabla, paresColumnaValor, strC(this.idKeyDefault), id);
		}

    
		/// <summary>
		/// (nombreTabla,[columna,setValor1,columna2,setValor2,...
		/// ],whereColumna1,whereValor1,whereColumna2,whereValor2,...)
		/// </summary>
		/// <param name="nombreTabla"></param>
		/// <param name="paresColumnaValor"></param>
		/// <param name="a"></param>
		/// <returns></returns>
		public  string update(string nombreTabla, Object[] paresColumnaValor, params Object[] a)
		{
			a = adaptarTipos(a);
			paresColumnaValor = adaptarTipos(paresColumnaValor);
			string sqlSet = "", sqlWhere = "";
			int pos = 0;
			for (int i = 0; i < paresColumnaValor.Length; i++) {
				if (pos == 0) {
					sqlSet += (!Utiles.isEmpty(sqlSet) ? " , " : "") + strC(paresColumnaValor[i]);
				} else if (pos == 1) {
                    sqlSet += "=" + getStrValor(paresColumnaValor[i]); //separadorComillas() + "" + paresColumnaValor[i] + "" + separadorComillas() + "";
				}
				pos = (pos + 1) % 2;
			}
			pos = 0;
			for (int i = 0; i < a.Length; i++) {

				if (pos == 0) {
					sqlWhere += (!Utiles.isEmpty(sqlWhere) ? " AND " : "");
					sqlWhere += strC(a[i]);

				} else if (pos == 1) {
					sqlWhere += " = " + getStrValor(a[i]) + "";

				}

				pos = (++pos) % 2;
			}


			return UPDATE + " " + nombreTabla + " " + SET + " " + sqlSet + " " + WHERE + " " + sqlWhere;
		}

		public  string getSuma(string nombreTabla, string columna)
		{
			return SELECT_SUM + "(" + nombreTabla + "." + strC(columna) + ") " + FROM + " " + nombreTabla;
		}
		public  string getSuma_Where(string nombreTabla, params Object[] paresColumnaValor)
		{
			paresColumnaValor=adaptarTipos(paresColumnaValor);
			string columna = strC(paresColumnaValor[0]) + "";
			return getSuma(nombreTabla, columna) + __getStr_Where(paresColumnaValor);
		}

		public  string getSuma_Where_Inner_Join(string nombreTabla, string columna, params Object[] args)
		{
			args=adaptarTipos(args);
			return __getFrom_Inner_Join_Where(SELECT_SUM + "(" + nombreTabla + "." + strC(columna) + ") ", args);
		}
		public  string getCantidad(string nombreTabla, string columna)
		{
			return SELECT_COUNT + "(" + nombreTabla + "." + strC(columna) + ") " + FROM + " " + nombreTabla;
		}

		public  string select_Distinct_Group_By_By_Having(string nombreTabla, string[]columnas, String grupBy, String heavinColumna, String heavinValor)
		{
			return Utiles.replaceFirst(select_Group_By_Having(nombreTabla, columnas, grupBy, heavinColumna, heavinValor), SELECT + " ", SELECT_DISTINCT + " ");
		}

		public  string select_Group_By_Having(string nombreTabla, string[]columnas, string grupBy, string heavinColumna, string heavinValor)
		{
			heavinValor = getObjectCorrecto(heavinValor) + "";
			return select_Group_By(nombreTabla, columnas, grupBy) + " " + HAVING + " " + strC(heavinColumna) + "=" + heavinValor;
		}
	
		public  string getLastId(string nombreTabla)
		{
			return getValorMaximo(nombreTabla, strC(this.idKeyDefault));
		}

		public  string select_ConUltimoID(string nombreTabla)
		{
			return select_forID(nombreTabla, "(" + getLastId(nombreTabla) + ")");
		}

		public  string select_forID(string nombreTabla, string id)
		{
			return select_Where(nombreTabla, strC(this.idKeyDefault), id);
		}
    
    
    
		public  string select_Distinct_Group_By(string nombreTabla, string[]columnas, string grupBy)
		{
        
			return Utiles.replaceFirst(select_Group_By(nombreTabla, columnas, grupBy), SELECT + " ", SELECT_DISTINCT + " ");
			//return select_Group_By(nombreTabla, columnas, grupBy).replaceFirst(SELECT + " ", SELECT_DISTINCT + " ");
		}

		/**
     * (nombreTabla,[]columnas,grupBy)
     *
     * @param nombreTabla
     * @param columnas
     * @param grupBy
     * @return
     */
		public  string select_Group_By(string nombreTabla, string[] columnas, string grupBy)
		{
			return select(nombreTabla, columnas) + " " + GROUP_BY + " " + grupBy;

		}

    
    
		/// <summary>
		/// /// (nombreTabla,listaDe ELEMENTO RELACIONES ENTRE TABLAS,paresColumnaValor)
		/// 
		/// 
		/// (nombreTabla,[TABLA,COLUMNA_REFERENCIA_ID],paresColumnaValor)
		/// 
		/// 
		/// ELEMENTO RELACIONES ENTRE TABLAS (ON): siempre son [Pares]
		/// 
		/// plq la lista de ellos es ejemplo:
		/// 
		/// [ [1 [T],[T,CID],[T,CID],[T] ] , [2 [T,CID],[T] ] , [3
		/// [T,CID],[T,CID] ] ]
		/// 
		/// recordar que siempre la un T en uno de los i tiene que aparecer en el
		/// siguiente(i+1) pq es un recorrido
		/// 
		/// 
		/// TABLA.CULUMNA_REFERENCIA_A_ID == a
		/// 
		/// [T] ,[T,CID] a TABLA.COLUMNA_NOMBRE_PERSONALIZADO_ID
		/// 
		/// 
		/// [T,CID],[T] a TABLA.ID el default dado de automatico
		/// 
		/// 
		/// [T,CID],[T,CID]
		/// 
		/// 
		/// 
		/// 
		/// 
		/// 
		/// PAR COLUMNA VALOR
		/// 
		/// [T,C],[T,C]
		/// 
		/// 
		/// [T,C],V
		/// 
		/// 
		/// C,[T,C]
		/// 
		/// 
		/// C,V
		/// 
		/// 
		/// [T],[T,C] la [T] es [T,this.idKeyDefault]
		/// 
		/// 
		/// [T],V la [T] es [T,this.idKeyDefault]
		/// 
		/// 
		/// C,[T] la [T] es [T,this.idKeyDefault]
		/// </summary>
		/// <returns></returns>
		public  string select_Where_Inner_Join_TodoDeTabla(string nombreTabla, params Object[] args)
		{//_Todo
			args=adaptarTipos(args);
			return __getFrom_Inner_Join_Where(SELECT + " " + nombreTabla + ".* ", args);
		}

    
		/// <summary>
		/// (nombreTabla,listaDe ELEMENTO RELACIONES ENTRE TABLAS,paresColumnaValor)
		/// 
		/// 
		/// (nombreTabla,[TABLA,COLUMNA_REFERENCIA_ID],paresColumnaValor)
		/// 
		/// 
		/// ELEMENTO RELACIONES ENTRE TABLAS (ON): siempre son [Pares]
		/// 
		/// plq la lista de ellos es ejemplo:
		/// 
		/// [ [1 [T],[T,CID],[T,CID],[T] ] , [2 [T,CID],[T] ] , [3
		/// [T,CID],[T,CID] ] ]
		/// 
		/// recordar que siempre la un T en uno de los i tiene que aparecer en el
		/// siguiente(i+1) pq es un recorrido
		/// 
		/// 
		/// TABLA.CULUMNA_REFERENCIA_A_ID == a
		/// 
		/// [T] ,[T,CID] a TABLA.COLUMNA_NOMBRE_PERSONALIZADO_ID
		/// 
		/// 
		/// [T,CID],[T] a TABLA.ID el default dado de automatico
		/// 
		/// 
		/// [T,CID],[T,CID]
		/// 
		/// 
		/// 
		/// 
		/// 
		/// 
		/// PAR COLUMNA VALOR
		/// 
		/// [T,C],[T,C]
		/// 
		/// 
		/// [T,C],V
		/// 
		/// 
		/// C,[T,C]
		/// 
		/// 
		/// C,V
		/// 
		/// 
		/// [T],[T,C] la [T] es [T,this.idKeyDefault]
		/// 
		/// 
		/// [T],V la [T] es [T,this.idKeyDefault]
		/// 
		/// 
		/// C,[T] la [T] es [T,this.idKeyDefault]
		/// </summary>
		/// <returns></returns>
		public  string select_Distinct_Todo_Where_Inner_Join(string nombreTabla, params Object[] args)
		{
			args=adaptarTipos(args);
			return __getFrom_Inner_Join_Where(SELECT_DISTINCT + " " + nombreTabla + ".* ", args);
		}

   
		/// <summary>
		/// /// (nombreTabla,listaDe ELEMENTO RELACIONES ENTRE
		/// TABLAS,where[paresColumnaValor],columnasDeOrden, o+ ordenaminento)
		/// 
		/// (nombreTabla,[TABLA,COLUMNA_REFERENCIA_ID],where[paresColumnaValor],columnasDeOrden,
		/// o+ ordenaminento)
		/// 
		/// ELEMENTO RELACIONES ENTRE TABLAS (ON): siempre son [Pares]
		/// plq la lista de ellos es ejemplo:
		/// [ [1 [T],[T,CID],[T,CID],[T] ] , [2 [T,CID],[T] ] , [3
		/// [T,CID],[T,CID] ] ]
		/// recordar que siempre la un T en uno de los i tiene que aparecer en el
		/// siguiente(i+1) pq es un recorrido
		/// 
		/// TABLA.CULUMNA_REFERENCIA_A_ID == a
		/// [T] ,[T,CID] a TABLA.COLUMNA_NOMBRE_PERSONALIZADO_ID
		/// 
		/// [T,CID],[T] a TABLA.ID el default dado de automatico
		/// 
		/// [T,CID],[T,CID]
		/// 
		/// 
		/// 
		/// 
		/// 
		/// PAR COLUMNA VALOR
		/// [T,C],[T,C]
		/// 
		/// [T,C],V
		/// 
		/// C,[T,C]
		/// 
		/// C,V
		/// 
		/// [T],[T,C] la [T] es [T,this.idKeyDefault]
		/// 
		/// [T],V la [T] es [T,this.idKeyDefault]
		/// 
		/// C,[T] la [T] es [T,this.idKeyDefault]
		/// </summary>
		/// <returns></returns>
		public  string select_Where_Inner_Join_ORDER_BY_TodoDeTabla(string nombreTabla, params Object[] args)
		{//_Todo
			args=adaptarTipos(args);
			return __getFrom_Inner_Join_Where_ORDER_BY(SELECT + " " + nombreTabla + ".* ", args);
		}
		public  String select_Distinct_Where(String nombreTabla, params Object[] a)
		{
			a=adaptarTipos(a);
			return Utiles.replaceFirst(select_Where(nombreTabla, a), SELECT + " ", SELECT_DISTINCT + " ");
		}

		public  String select_Distinct_Todo(String nombreTabla)
		{
			return Utiles.replaceFirst(select_Todo(nombreTabla), SELECT + " ", SELECT_DISTINCT + " ");
		}

		public  String select_Distinct(String nombreTabla, params String[] a)
		{
			
			return Utiles.replaceFirst(select(nombreTabla, a), SELECT + " ", SELECT_DISTINCT + " ");
		}

		public  String select_Distinct_ORDER_BY(String nombreTabla, params Object[] a)
		{
			a=adaptarTipos(a);
			return Utiles.replaceFirst(select_ORDER_BY(nombreTabla, a), SELECT + " ", SELECT_DISTINCT + " ");
		}

		public  String select_Distinct_Where_ORDER_BY(String nombreTabla, params Object[] a)
		{
			a=adaptarTipos(a);
			return Utiles.replaceFirst(select_Where_ORDER_BY(nombreTabla, a), SELECT + " ", SELECT_DISTINCT + " ");
		}

		/**
     * (nombreTabla,columnas[],where[pares .. Columna-Valor],columnas por los
     * que ordenar)<br/>
     * (nombreTabla,where[pares .. Columna-Valor],columnas por los que ordenar)
     *
     * @param nombreTabla
     * @param a
     * @return
     */
		public  String select_Where_ORDER_BY(String nombreTabla, params Object[] a)
		{
			a = adaptarTipos(a);
			String sql = "";
			int inicioDeColumnas = 0;
			String sqlSelect = "";
			if (a.Length > 0) {
				if (esArreglo(a[0])) {
					inicioDeColumnas = 1;
					if (esArreglo(a[1])) {
						inicioDeColumnas = 2;
						sqlSelect = select_Where(nombreTabla, Arreglos.colocarDeUltimoObject((Object[])a[1], a[0]));
					} else {
						sqlSelect = select_Where(nombreTabla, (Object[])a[0]);

					}
				} else {
					sqlSelect = select_Todo(nombreTabla);
				}
//            for (int i = inicioDeColumnas; i < a.Length; i++) {
//                sql += (i != inicioDeColumnas ? " , " : "") + a[i];
//            }
			}
			//return sqlSelect + " " + ORDER_BY + " " + sql;
			return sqlSelect + " " + __getStr_ORDER_BY(a, inicioDeColumnas);

		}

		/**
     * (nombreTabla,columnas[],where[pares .. Columna-Valor],columnas por los
     * que ordenar)<br/>
     * (nombreTabla,columnas[],columnas por los que ordenar)<br/>
     * (nombreTabla,columnas por los que ordenar) selecciona todas las columnas
     *
     * @param nombreTabla
     * @param a
     * @return
     */
		public  String select_ORDER_BY(String nombreTabla, params Object[] a)
		{
			a=adaptarTipos(a);
			String sql = "";
			int inicioDeColumnas = 0;
			String sqlSelect = "";
			if (a.Length > 0) {
				if (esArregloString(a[0])) {
					inicioDeColumnas = 1;
					if (esArreglo(a[1])) {
						inicioDeColumnas = 2;
						sqlSelect = select_Where(nombreTabla, Arreglos.colocarDeUltimoObject((Object[])a[1], a[0]));
					} else {
						sqlSelect = select(nombreTabla, (String[])a[0]);
					}
				} else {
					sqlSelect = select_Todo(nombreTabla);
				}
				for (int i = inicioDeColumnas; i < a.Length; i++) {
					sql += (i != inicioDeColumnas ? " , " : "") + a[i];
				}
			}
			return sqlSelect + " " + ORDER_BY + " " + sql;

		}
		/// <summary>
		///  (nombreTabla,columnaValorMaximo,paresColumnaValor)
		/// la idea es crear un subSql que seleccione el maximo valor utlizando
		/// los pares columna valor tanto en el sql(todo) como en el subSql
		/// SubSql
		/// SELECT MAX(nombreTabla.columnaValorMaximo) FROM nombreTabla WHERE
		/// pares columna valor
		/// sql(todo)
		/// SELECT nombreTabla.columnaValorMaximo FROM nombreTabla WHERE pares
		/// columna valor
		/// AND columnaValorMaximo=(SubSql)
		/// 
		/// Ejemplo:
		/// SELECT nombreTabla.columnaValorMaximo FROM nombreTabla WHERE pares
		/// columna valor
		/// AND columnaValorMaximo = ( SELECT MAX(nombreTabla.columnaValorMaximo)
		/// FROM nombreTabla WHERE pares columna valor )
		/// </summary>
		/// <param name="nombreTabla"></param>
		/// <param name="columnaValorMaximo"></param>
		/// <param name="paresColumnaValor"></param>
		/// <returns></returns>
		public  string select_Where_ValorMaximo(string nombreTabla, string columnaValorMaximo, params Object[] paresColumnaValor)
		{
			paresColumnaValor=adaptarTipos(paresColumnaValor);
			List<Object> pares = new List<Object>(paresColumnaValor); //Arrays.asList(paresColumnaValor);

			pares.Add(columnaValorMaximo);
			pares.Add("(" + getValorMaximo_Where(nombreTabla, columnaValorMaximo, paresColumnaValor) + ")");
			return select_Where(nombreTabla, pares.ToArray());
			//return __select_Where(nombreTabla,pares.toArray(new Object[0]));
		}

    
		/// <summary>
		/// (nombreTabla,columnas[],pares .. Columna-Valor)<br/>
		/// (nombreTabla,pares .. Columna-Valor)
		/// </summary>
		/// <param name="nombreTabla"></param>
		/// <param name="a"></param>
		/// <returns></returns>
		public  string select_Where(string nombreTabla, params Object[] a)
		{
			a = adaptarTipos(a);

			string sql = "";
			int inicioDePares = 0;
			string sqlSelect = "";
			//String columnas[] = null;
			if (a.Length > 0) {
				if (esArregloString(a[0])) {
					sqlSelect = (((string[])a[0]).Length > 0) ? select(nombreTabla, (string[])a[0]) : select_Todo(nombreTabla);
					inicioDePares = 1;
				} else if (sqlSelect.Length == 0) {
					sqlSelect = select_Todo(nombreTabla);
				}


			}
			return sqlSelect + __getStr_Where(a, inicioDePares);
			//return sqlSelect + " " + WHERE + " " + sql;
		}
    
		public  string select_Todo(string nombreTabla)
		{
			return select(nombreTabla, "*");
		}

		public  string select(string nombreTabla, params string[] columnas)
		{
			string sql = "";
			for (int i = 0; i < columnas.Length; i++) {
				sql += (i != 0 ? " , " : "") + strC(columnas[i]);
			}
			return SELECT + " " + sql + " " + FROM + " " + nombreTabla;
		}

		public  string copyEnTXT(string nombreTabla, string direccion)
		{
			if (!Utiles.isEmpty(direccion)) {
				return COPY + " " + nombreTabla + " " + FROM + " " + ((direccion.EndsWith(".txt")) ? direccion : direccion + ".txt");
			}
			return null;
		}

		public  string getPoint(double X, double Y)
		{
			return "(" + X + "," + Y + ")";
		}

		public  string getDate(string año, string mes, string dia)
		{
			return año + "-" + mes + "-" + dia;
		}

		public  string dropTable(String nombreTabla)
		{
			return DROP_TABLE + " " + nombreTabla;
		}

		public  string getIdCorrespondiente(string nombreTabla)
		{
			return getIdCorrespondiente(nombreTabla, strC(this.idKeyDefault));
		}

		public  string getIdCorrespondiente(string nombreTabla, string id)
		{
            // RETURNING 
            string r = "(" + getValorMaximo(nombreTabla, id) + ")+1";
           // string r =  getValorMaximo(nombreTabla, id) ;
            return r;
		}
        public string getMaximoID(string nombreTabla)
        {
            return getMaximoID(nombreTabla, strC(this.idKeyDefault));
        }
        public string getMaximoID(string nombreTabla, string id)
        {
            string r = getValorMaximo(nombreTabla, id);
            return r;
        }


        public  string getValorPromedio(string nombreTabla, string columna)
		{
			return SELECT_AVG + "(" + nombreTabla + "." + strC(columna) + ") " + FROM + " " + nombreTabla;
		}
		public  string getValorPromedio_Where_Inner_Join(string nombreTabla, string columna, params Object[] args)
		{
			args = adaptarTipos(args);
			return __getFrom_Inner_Join_Where(SELECT_AVG + "(" + nombreTabla + "." + strC(columna) + ") ", args);
		}

		public  string getValorPromedio_Where(string nombreTabla, params Object[] paresColumnaValor)
		{
			paresColumnaValor=adaptarTipos(paresColumnaValor);
			string columna = strC(paresColumnaValor[0]) + "";
			return getValorPromedio(nombreTabla, columna) + __getStr_Where(paresColumnaValor);
		}

		public  string getValorMinimo(string nombreTabla, string columna)
		{
			return SELECT_MIN + "(" + nombreTabla + "." + strC(columna) + ") " + FROM + " " + nombreTabla;
		}
		public  string getValorMinimo_Where_Inner_Join(string nombreTabla, string columna, params Object[] args)
		{
			args=adaptarTipos(args);
			return __getFrom_Inner_Join_Where(SELECT_MIN + "(" + nombreTabla + "." + strC(columna) + ") ", args);
		}

		public  string getValorMinimo_Where(string nombreTabla, params Object[] paresColumnaValor)
		{
			paresColumnaValor=adaptarTipos(paresColumnaValor);
			string columna = strC(paresColumnaValor[0]) + "";
			return getValorMinimo(nombreTabla, columna) + __getStr_Where(paresColumnaValor);
		}
		public  string getValorMaximo(string nombreTabla, string columna)
		{
            string r = SELECT_MAX + "(" + nombreTabla + "." + strC(columna) + ") " + FROM + " " + nombreTabla;
            
            return r;
		}
    
		public  string getValorMaximo_Where_Inner_Join(string nombreTabla, string columna, params Object[] args)
		{
			args=adaptarTipos(args);
			return __getFrom_Inner_Join_Where(SELECT_MAX + "(" + nombreTabla + "." + strC(columna) + ") ", args);
		}

		public  string getValorMaximo_Where(string nombreTabla, params Object[] paresColumnaValor)
		{
			paresColumnaValor=adaptarTipos(paresColumnaValor);
			string columna = strC(paresColumnaValor[0]) + "";
			return getValorMaximo(nombreTabla, columna) + __getStr_Where(paresColumnaValor);
		}

    
		/// <summary>
		/// (nombreTabla,valores una sola fila completa)
		/// (nombreTabla,[]columnas,valores)<br/>
		/// es nuestra responsabiladad asegurarnos de que almenos uno de los valores
		/// sea una llave
		/// </summary>
		/// <param name="nombreTabla"></param>
		/// <param name="a"></param>
		/// <returns></returns>
		public  string insertar_SinIdAutomatico(string nombreTabla, params Object[] a)
		{
			a=adaptarTipos(a);
			bool idAutomatico = false;
			//System.out.println("1idAutomatico="+idAutomatico);
			return __insertar(nombreTabla, idAutomatico, "", a);
		}

    
		/// <summary>
		/// (nombreTabla,valores una sola fila completa)<br/>
		/// (nombreTabla,[]columnas,valores)<br/>
		/// si no lo tiene pone el id de forma automatica con el nombre del argumento
		/// </summary>
		/// <param name="nombreTabla"></param>
		/// <param name=this.idKeyDefault></param>
		/// <param name="a"></param>
		/// <returns></returns>
		public virtual  string insertar_idAutomatico(string nombreTabla, string id, Object[] a)
		{
			a=adaptarTipos(a);
			// System.out.println("pasa por el id auto");
			return __insertar(nombreTabla, true, id, a);
		}

   
		/// <summary>
		/// (nombreTabla,valores una sola fila completa)<br/>
		/// (nombreTabla,[]columnas,valores) si no lo tiene pone el id de forma
		/// automatica
		/// </summary>
		/// <param name="nombreTabla"></param>
		/// <param name="a"></param>
		/// <returns></returns>
		public  string insertar(string nombreTabla, params Object[] a)
		{
			a=adaptarTipos(a);
			return __insertar(nombreTabla, true, strC(this.idKeyDefault), a);
		}

   
        

		/// <summary>
		/// (nombreTabla,valores una sola fila completa)<br/>
		/// (nombreTabla,[]columnas,valores) si no lo tiene pone el id de forma automatica
		/// </summary>
		/// <param name="nombreTabla"></param>
		/// <param name="idAutomatico"></param>
		/// <param name="nombreId"></param>
		/// <param name="a"></param>
		/// <returns></returns>
		protected virtual string __insertar(string nombreTabla, bool idAutomatico, string nombreId, params Object[] a)
		{
			//System.out.println("00idAutomatico="+idAutomatico);
			a = adaptarTipos(a);
			//System.out.println("0idAutomatico="+idAutomatico);
			string sql = "", sqlColumns = "";
			int inicioDeValores = 0;
			string[] columnas = null;
			if (a.Length > 0) {
				if (esArregloString(a[0])) {
					columnas = (String[])a[0];
					for (int i = 0; i < columnas.Length; i++) {
						sqlColumns += (i != 0 ? " , " : "") + strC(columnas[i]);
					}
					inicioDeValores = 1;
				}
				for (int i = inicioDeValores; i < a.Length; i++) {
					//sql += (i != inicioDeValores ? " , " : "") + " " + getStrValor(a[i]) + " ";
					sql += (i != inicioDeValores ? " , " : "");
					if ((a[i] + "").StartsWith("b" + separadorComillas() + "") && (a[i] + "").EndsWith("" + separadorComillas() + "")) {
						sql += a[i];
					} else {
						if (esInt(a[i])) {
							sql += a[i] + " ";
						} else {
							sql += " " + getStrValor(a[i]) + " ";
						}
					}
				}
			}
//        System.out.println("0sql="+sql);
//        System.out.println("idAutomatico="+idAutomatico);
			if (idAutomatico) {
				if (Utiles.isEmpty(nombreId)) {
					nombreId = this.idKeyDefault;
				}
				sql = " NULL , " + sql;
				if (!Utiles.isEmpty(sqlColumns)) {
					sqlColumns = strC(nombreId) + " , " + sqlColumns;
				}
			}
//        System.out.println("sql="+sql);
			return INSERT_INTO + " " + nombreTabla + (!Utiles.isEmpty(sqlColumns) ? " ( " + sqlColumns + " ) " : " ") + VALUES + " ( " + sql + " ) ";
		}

		public  string insertar_Many_SinIdAutomatico(string nombreTabla, int cantidadDeColumnas)
		{
			return __insertar_Many(nombreTabla, false, "", cantidadDeColumnas);
		}

		public  string insertar_Many_idAutomatico(string nombreTabla, string id, int cantidadDeColumnas)
		{
			return __insertar_Many(nombreTabla, true, id, cantidadDeColumnas);
		}

		public  string insertar_Many(string nombreTabla, int cantidadDeColumnas)
		{
			return __insertar_Many(nombreTabla, true, strC(this.idKeyDefault), cantidadDeColumnas);
		}

		protected  string __insertar_Many(string nombreTabla, bool idAutomatico, string nombreId, int cantidadDeColumnas)
		{
			String sql = "";
			for (int i = 0; i < cantidadDeColumnas; i++) {
				sql += (i != 0 ? " , " : "") + "?";
			}
			if (idAutomatico) {
				sql = " NULL , " + sql;
			}
			return INSERT_INTO + " " + nombreTabla + " " + VALUES + " ( " + sql + " ) ";
		}

   
		/// <summary>
		///  (nombre,.. nombreColumna,TipoDeDatoSQL,capacidad#,isKeyPrimary o
		/// tipoDeClasificacionSQL)
		/// 
		/// (nombre,.. nombreColumna,TipoDeDatoSQL,capacidad#)
		/// asumo que no es
		/// llave primaria (nombre,.. nombreColumna,capacidad#) asumo que es VARCHAR
		/// 
		/// 
		/// (nombre,.. nombreColumna) asumo que es VARCHAR(255)
		/// 
		/// si el siguiente a nombreColumna no es un TipoDeDatoSQL asumo que es
		/// VARCHAR(255) 
		/// 
		/// si el siguiente a TipoDeDatoSQL no es un # asumo que es VARCHAR(255)
		/// </summary>
		/// <param name="nombreTabla"></param>
		/// <param name="NombreTipos"></param>
		/// <returns></returns>
		public virtual  string crearTabla(string nombreTabla, params Object[] NombreTipos)
		{
			NombreTipos=adaptarTipos(NombreTipos);
			string sql = "";
			TipoDeDatoSQL tipoAnterior = null;
			bool tieneClavePrimaria = false;
			int pos = 0;
			for (int i = 0; i < NombreTipos.Length; i++) {
				Object ielm = NombreTipos[i];
				if (pos == 3) {
					if (esBool(ielm)) {
						if ((bool)ielm) {
							//sql += " " + PRIMARY_KEY;
							sql += " " + NOT_NULL + " " + PRIMARY_KEY;
							tieneClavePrimaria = true;
						}
					} else if (TipoDeClasificacionSQL.esTipoDeClasificacionSQL(ielm)) {
						sql += " " + ((TipoDeClasificacionSQL)ielm).getValor();
						if (((TipoDeClasificacionSQL)ielm).esLlave()) {
							tieneClavePrimaria = true;
						}
					} else {
						pos = 0;
					}
					sql += " , ";
					tipoAnterior = null;
				}
				if (pos == 2) {
					if (esInt(ielm)) {
						sql += "(" + ielm + ")";
					} else {
						if (tipoAnterior == TipoDeDatoSQL.VARCHAR) {
							sql += "(255)";
						}
						if (esBool(ielm)) {
							if ((bool)ielm) {
								sql += " " + PRIMARY_KEY;
								tieneClavePrimaria = true;
							}
							sql += " , ";
							pos = 0;
							tipoAnterior = null;
							continue;
						} else if (TipoDeClasificacionSQL.esTipoDeClasificacionSQL(ielm)) {
							sql += " " + ((TipoDeClasificacionSQL)ielm).getValor();
							if (((TipoDeClasificacionSQL)ielm).esLlave()) {
								tieneClavePrimaria = true;
							}
							sql += " , ";
							pos = 0;
							tipoAnterior = null;
							continue;
						} else {
							pos = 0;
							sql += " , ";
						}

					}
				}

				if (pos == 1) {
					TipoDeDatoSQL tipo = null;
					if (TipoDeDatoSQL.esTipoDeDatoSQL(ielm)) {
						tipo = (TipoDeDatoSQL)ielm;
					}
					if (tipo != null) {
						sql += " " + tipo.getValor();
						tipoAnterior = tipo;
					} else if (esInt(ielm)) {
						tipo = TipoDeDatoSQL.VARCHAR;//sql+=" "+tipo.getValor()+"(255)";
						sql += " " + tipo.getValor() + "(" + ielm + ")";
						tipoAnterior = tipo;
						pos = 3;
						continue;
					} else {
						tipo = TipoDeDatoSQL.VARCHAR;
						sql += " " + tipo.getValor() + "(255)";
						tipoAnterior = tipo;
						if (esBool(ielm)) {
							if ((bool)ielm) {
								sql += " " + PRIMARY_KEY;
								tieneClavePrimaria = true;
							}
							sql += " , ";
							pos = 0;
							tipoAnterior = null;
							continue;
						} else if (TipoDeClasificacionSQL.esTipoDeClasificacionSQL(ielm)) {
							sql += " " + ((TipoDeClasificacionSQL)ielm).getValor();
							if (((TipoDeClasificacionSQL)ielm).esLlave()) {
								tieneClavePrimaria = true;
							}
							sql += " , ";
							pos = 0;
							tipoAnterior = null;
							continue;
						} else {
							pos = 0;
							sql += " , ";
						}
					}
				}
				if (pos == 0) {
					sql += " " + ielm;
				}
				pos = ++pos % 4;
			}

			if (pos == 1) {
				sql += " " + TipoDeDatoSQL.VARCHAR.getValor() + "(255)";
			} else if (pos == 2) {
				if (tipoAnterior == TipoDeDatoSQL.VARCHAR) {
					sql += "(255)";
				}
			}
			if (Utiles.endsWithOR(sql, ", ", ",")) {
				sql = Utiles.subs(sql, 0, sql.LastIndexOf(","));
			}
			if (!tieneClavePrimaria && !sql.Contains(" id ")) {
				string sqlFinal = " id " + TipoDeDatoSQL.INTEGER.getValor() + " " + PRIMARY_KEY + " " + AUTOINCREMENT ;
				if(NombreTipos.Length!=0&&sql.Length!=0){
					sqlFinal+=" , " + sql;
				}
				sql=sqlFinal;
			}
			return CREATE_TABLE + " " + nombreTabla + " ( " + sql + " ) ";
		}

		protected  bool esBool(Object a)
		{
			return a is bool;
			//return a.GetType()==true.GetType();
		}

		protected  bool esInt(Object a)
		{
			return a is int;
			//return a.GetType()==0.GetType();
		}

		protected  string strg(params Object[] O)
		{
			string res = "";
			int end = O.Length;
			for (int i = 0; i < end; i++) {
				res += O[i].ToString();
			}
        
			return res;
		}

		protected  bool esArregloString(Object a)
		{
			//return a instanceof String[];
			//string[] b={""};
			// return b.GetType()==a.GetType();
			return a is String[];
		}

		protected  bool esArreglo(Object a)
		{
			return a is Object[];
			//return a.GetType()==new Object[]{}.GetType();
			// return a.GetType().IsArray();
			//return a instanceof Object[];
			//String[] b={""};
			//return b.GetType()==a.GetType();
		}
        //

        protected Object[] adaptarTipos(params Object[] O)
		{
			for (int i = 0; i < O.Length; i++) {
				O[i]=getObjectCorrecto(O[i]);
				//string o = getObjectCorrecto(O[i]) + "";
            
				//Class c = o.getClass();
				//if (or(c, java.util.Date.class)) {
				//    O[i] = new java.sql.Timestamp(((java.util.Date) o).getTime());
				//}
			}
			return O;
		}

		protected  Object getObjectCorrecto(Object o)
		{
			if (o is DateTime) {
				DateTime d=(DateTime)o;
				if(d==null||d==Utiles.NULL_DATE||d==DateTime.MinValue){
					//cwl("fue null !!!!!!!!!!!!!!!!!!");
					//return "NULL";
					return null;
				}else{
					//cwl("o="+o);
					string r=d.ToString("yyyy-MM-dd HH:mm:ss.fff");
					//cwl("r="+r);
					return r;
				}
				
			}
			//Class c = o.getClass();
			//if (or(c, java.util.Date.class)) {
			//    return new java.sql.Timestamp(((java.util.Date) o).getTime());
			//}
			return o;
		}
    
		//public  Date getDate(String o){
		//return Date.from(java.sql.Timestamp.valueOf(o).toInstant());
		//}
		public  void cwl(object o)
        {
            string[] a={""};
            if(a.GetType()==o.GetType()){
                try
                {
                    Console.WriteLine(Utiles.str((string[])o));
                }
                catch (Exception ex) { Archivos.appenLogExeption(ex); }
                return;
            }
            Console.WriteLine(o.ToString());
        }
	}

    public class SQLUtiles_Postgres : SQLUtiles {
        public const String RETURNING = "RETURNING";
        private PatronRegex rgx_PATRON_RETURNING_al_Final=new PatronRegex(
            ConstantesExprecionesRegulares.espacios_UnoAlMenos+
            "RETURNING"+
            ConstantesExprecionesRegulares.espacios_UnoAlMenos +
            "[\"]?"+
            ConstantesExprecionesRegulares.letras+
            "[\"]?" +
            ConstantesExprecionesRegulares.espacios+
            ";"
            );
        public bool esRETURNING(string sql) {
            return rgx_PATRON_RETURNING_al_Final.ReFinal.IsMatch(sql);
        }

        /// <summary>
		/// (nombreTabla,valores una sola fila completa)<br/>
		/// (nombreTabla,[]columnas,valores)<br/>
		/// si no lo tiene pone el id de forma automatica con el nombre del argumento
        /// 
        /// manda a retornar el id
		/// </summary>
		/// <param name="nombreTabla"></param>
		/// <param name=this.idKeyDefault></param>
		/// <param name="a"></param>
		/// <returns></returns>
		public override string insertar_idAutomatico(string nombreTabla, string id, Object[] a)
        {
            if (Utiles.isEmpty(id))
            {
                id = this.idKeyDefault;
            }
            return base.insertar_idAutomatico(nombreTabla,id,a) + " RETURNING "+ strC(id) +";";
        }

        protected override string __insertar(string nombreTabla, bool idAutomatico, string nombreId, params Object[] a)
        {
            //System.out.println("00idAutomatico="+idAutomatico);
            a = adaptarTipos(a);
            //System.out.println("0idAutomatico="+idAutomatico);
            string sql = "", sqlColumns = "";
            int inicioDeValores = 0;
            string[] columnas = null;
            if (a.Length > 0)
            {
                if (esArregloString(a[0]))
                {
                    columnas = (String[])a[0];
                    for (int i = 0; i < columnas.Length; i++)
                    {
                        sqlColumns += (i != 0 ? " , " : "") + strC(columnas[i]);
                    }
                    inicioDeValores = 1;
                }
                for (int i = inicioDeValores; i < a.Length; i++)
                {
                    sql += (i != inicioDeValores ? " , " : "");
                    if ((a[i] + "").StartsWith("b" + separadorComillas() + "") && (a[i] + "").EndsWith("" + separadorComillas() + ""))
                    {
                        sql += a[i];
                    }
                    else
                    {
                        if (esInt(a[i]))
                        {
                            sql += a[i] + " ";
                        }
                        else
                        {
                            sql += " " + getStrValor(a[i]) + " ";
                        }
                    }
                }
            }
            //        System.out.println("0sql="+sql);
            //        System.out.println("idAutomatico="+idAutomatico);
            //if (idAutomatico)
            //{
            //    if (Utiles.isEmpty(nombreId))
            //    {
            //        nombreId = this.idKeyDefault;
            //    }
            //    sql = " NULL , " + sql;
            //    if (!Utiles.isEmpty(sqlColumns))
            //    {
            //        sqlColumns = nombreId + " , " + sqlColumns;
            //    }
            //}
            //        System.out.println("sql="+sql);
            return INSERT_INTO + " " + nombreTabla + (!Utiles.isEmpty(sqlColumns) ? " ( " + sqlColumns + " ) " : " ") + VALUES + " ( " + sql + " ) ";
        }


        public override string crearTabla(string nombreTabla, params Object[] NombreTipos)
        {
            NombreTipos = adaptarTipos(NombreTipos);
            string sql = "";
            TipoDeDatoSQL tipoAnterior = null;
            bool tieneClavePrimaria = false;
            int pos = 0;
            for (int i = 0; i < NombreTipos.Length; i++)
            {
                Object ielm = NombreTipos[i];
                if (pos == 3)
                {
                    if (esBool(ielm))
                    {
                        if ((bool)ielm)
                        {
                            //sql += " " + PRIMARY_KEY;
                            sql += " " + NOT_NULL + " " + PRIMARY_KEY;
                            tieneClavePrimaria = true;
                        }
                    }
                    else if (TipoDeClasificacionSQL.esTipoDeClasificacionSQL(ielm))
                    {
                        sql += " " + ((TipoDeClasificacionSQL)ielm).getValor();
                        if (((TipoDeClasificacionSQL)ielm).esLlave())
                        {
                            tieneClavePrimaria = true;
                        }
                    }
                    else
                    {
                        pos = 0;
                    }
                    sql += " , ";
                    tipoAnterior = null;
                }
                if (pos == 2)
                {
                    if (esInt(ielm))
                    {
                        sql += "(" + ielm + ")";
                    }
                    else
                    {
                        if (tipoAnterior == TipoDeDatoSQL.VARCHAR)
                        {
                            sql += "(255)";
                        }
                        if (esBool(ielm))
                        {
                            if ((bool)ielm)
                            {
                                sql += " " + PRIMARY_KEY;
                                tieneClavePrimaria = true;
                            }
                            sql += " , ";
                            pos = 0;
                            tipoAnterior = null;
                            continue;
                        }
                        else if (TipoDeClasificacionSQL.esTipoDeClasificacionSQL(ielm))
                        {
                            sql += " " + ((TipoDeClasificacionSQL)ielm).getValor();
                            if (((TipoDeClasificacionSQL)ielm).esLlave())
                            {
                                tieneClavePrimaria = true;
                            }
                            sql += " , ";
                            pos = 0;
                            tipoAnterior = null;
                            continue;
                        }
                        else
                        {
                            pos = 0;
                            sql += " , ";
                        }

                    }
                }

                if (pos == 1)
                {
                    TipoDeDatoSQL tipo = null;
                    if (TipoDeDatoSQL.esTipoDeDatoSQL(ielm))
                    {
                        tipo = (TipoDeDatoSQL)ielm;
                    }
                    if (tipo != null)
                    {
                        sql += " " + tipo.getValor();
                        tipoAnterior = tipo;
                    }
                    else if (esInt(ielm))
                    {
                        tipo = TipoDeDatoSQL.VARCHAR;//sql+=" "+tipo.getValor()+"(255)";
                        sql += " " + tipo.getValor() + "(" + ielm + ")";
                        tipoAnterior = tipo;
                        pos = 3;
                        continue;
                    }
                    else
                    {
                        tipo = TipoDeDatoSQL.VARCHAR;
                        sql += " " + tipo.getValor() + "(255)";
                        tipoAnterior = tipo;
                        if (esBool(ielm))
                        {
                            if ((bool)ielm)
                            {
                                sql += " " + PRIMARY_KEY;
                                tieneClavePrimaria = true;
                            }
                            sql += " , ";
                            pos = 0;
                            tipoAnterior = null;
                            continue;
                        }
                        else if (TipoDeClasificacionSQL.esTipoDeClasificacionSQL(ielm))
                        {
                            sql += " " + ((TipoDeClasificacionSQL)ielm).getValor();
                            if (((TipoDeClasificacionSQL)ielm).esLlave())
                            {
                                tieneClavePrimaria = true;
                            }
                            sql += " , ";
                            pos = 0;
                            tipoAnterior = null;
                            continue;
                        }
                        else
                        {
                            pos = 0;
                            sql += " , ";
                        }
                    }
                }
                if (pos == 0)
                {
                    sql += " " + ielm;
                }
                pos = ++pos % 4;
            }

            if (pos == 1)
            {
                sql += " " + TipoDeDatoSQL.VARCHAR.getValor() + "(255)";
            }
            else if (pos == 2)
            {
                if (tipoAnterior == TipoDeDatoSQL.VARCHAR)
                {
                    sql += "(255)";
                }
            }
            if (Utiles.endsWithOR(sql, ", ", ","))
            {
                sql = Utiles.subs(sql, 0, sql.LastIndexOf(","));
            }
            if (!tieneClavePrimaria && !sql.Contains(" id "))
            {
                //string sqlFinal = " id " + TipoDeDatoSQL.INTEGER.getValor() + " " + PRIMARY_KEY + " " + AUTOINCREMENT;
                string sqlFinal = " id " + TipoDeDatoSQL.SERIAL.getValor() + " " + PRIMARY_KEY ;
                if (NombreTipos.Length != 0 && sql.Length != 0)
                {
                    sqlFinal += " , " + sql;
                }
                sql = sqlFinal;
            }
            return CREATE_TABLE + " " + nombreTabla + " ( " + sql + " ) ";
        }
        public override bool esSelect(string sql)
        {
            
            return base.esSelect(sql) || Utiles.startsWithOR(sql,"(","((",SQLUtiles_Postgres.RETURNING);
        }

        public override bool esSelectValor(string sql)
        {
            return base.esSelectValor(sql) || Utiles.startsWithOR(sql, "(", "((", SQLUtiles_Postgres.RETURNING);
        }
        public override string separadorComillas()
        {
            return "'" ;
        }
        public override string separadorComillasContrario()
        {
            return "\"";
        }
        public override string strC(object columna)
        {
            string separador = separadorComillasContrario();
            string c = columna + "";
            if (c != "*" && !c.StartsWith(separador))
            {
                c = separador + c + separador;
            }
            return c;
        }
    }

    
    }
