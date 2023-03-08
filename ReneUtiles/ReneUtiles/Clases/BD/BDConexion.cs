/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 9/4/2022
 * Hora: 19:35
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

using ReneUtiles.Clases;
//using System.IO;
using System.Collections.Generic;
using System.Data;
using ReneUtiles.Clases.BD.Conexion;
using ReneUtiles;
using Delimon.Win32.IO;

namespace ReneUtiles.Clases.BD
{
	/// <summary>
	/// Description of BDConexion.
	/// </summary>
	public class BDConexion:ConsolaBasica
	{
	private GestorDeConexionImple gestorDeConexionImple;
    private DatosBDConect datosBDConect;
        

    private BDConexion(string usuario, string contraseña, string servidor, string nombreBD, string puerto, TipoDeConexionBD tipoDeConxion, FileInfo url) {
    	
 
        string controlador = tipoDeConxion.driver_java;
        datosBDConect = new DatosBDConect(controlador, null, usuario, contraseña, servidor, nombreBD, puerto, "", tipoDeConxion, url, null, false);
    }
        public SQLUtiles sq() {
            return this.gestorDeConexionImple.sqlUtiles;
        }

    public DatosBDConect getDatosBDConect() {
        return datosBDConect;
    }

    

    /**
     * activa la muestra de resultada en consola de forma automatica
     *
     * @return
     */
    public BDConexion cl() {
        // mostrarResultadoConsola = true;
        //datosBDConect.setMostrarResultadoConsola(true);
        datosBDConect.mostrarSQL=true;
        return this;
    }
        public BDConexion no_cl()
        {
            // mostrarResultadoConsola = true;
            //datosBDConect.setMostrarResultadoConsola(true);
            datosBDConect.mostrarSQL = false;
            return this;
        }
        public BDConexion cl_re()
        {
            // mostrarResultadoConsola = true;
            //datosBDConect.setMostrarResultadoConsola(true);
            datosBDConect.mostrarResultadoConsola = true;
            return this;
        }
        public GestorDeConexionImple getGestorDeConexionImple() {
//        if (gestorDeConexionImple == null) {
//            gestorDeConexionImple = new GestionadorDeConexion(datosBDConect);
//        }
        return gestorDeConexionImple;
    }

        public int getIdCorrespondiente(string nombreTabla,string id) {
            object o= getGestorDeConexionImple().ejecutarConsultaGetInt(sq().getMaximoID(nombreTabla,id));
            if (o==null) {
                //cwl("1-o==null");
                return 1;
            }
            if (o is int||o is double) {
                //cwl("n-o is int||o is double");
                return (int)o+1;
            }
            //if (o is string) {
            //    if (esNumero(o+"")) {
            //        //cwl("");
            //        return (int)dou(o + "");
            //    }
            //    return 1;
            //}
            //if (o is Object[][]) {
            //    Object[][] O = (Object[][])o;
            //    if (O == null || O.Length == 0)
            //    {
            //        return 1;
            //    }
            //    if (O[0]==null||O[0].Length==0) {
            //        return 1;
            //    }
            //    if (O[0][0]==null) {
            //        return 1;
            //    }
            //    return (int)O[0][0];
            //}
            return 1;
        }
        public int getIdCorrespondiente(string nombreTabla)
        {
            object o = getGestorDeConexionImple().ejecutarConsultaGetInt(sq().getMaximoID(nombreTabla));
            if (o == null)
            {
                //cwl("1-o==null");
                return 1;
            }
            if (o is int || o is double)
            {
                //cwl("n-o is int||o is double");
                return (int)o + 1;
            }
            //if (o is string) {
            //    if (esNumero(o+"")) {
            //        //cwl("");
            //        return (int)dou(o + "");
            //    }
            //    return 1;
            //}
            //if (o is Object[][]) {
            //    Object[][] O = (Object[][])o;
            //    if (O == null || O.Length == 0)
            //    {
            //        return 1;
            //    }
            //    if (O[0]==null||O[0].Length==0) {
            //        return 1;
            //    }
            //    if (O[0][0]==null) {
            //        return 1;
            //    }
            //    return (int)O[0][0];
            //}
            return 1;
        }

        public void setGestorDeConexionImple(GestorDeConexionImple gestorDeConexionImple) {
        this.gestorDeConexionImple = gestorDeConexionImple;
           
    }
    
    public bool existe(string nombreTabla,params Object[] paresColumnaValor){
        int cantidad=getCantidad_Where(nombreTabla, paresColumnaValor);
        return cantidad>0;
    }

    public Object[] select_forID(string nombreTabla,string idStr, int id) {
            Object[][] O = (Object[][])getGestorDeConexionImple()._execute(sq().select_Id(nombreTabla, idStr, id));
            if (O == null || O.Length == 0)
            {
                return null;
            }
            return O[0];
            //return ((Object[][]) getGestorDeConexionImple()._execute(sq().select_Id(nombreTabla, id)))[0];
        // return select_Where(nombreTabla,"id",id);
    }
        public Object[] select_forID(string nombreTabla,  int id)
        {
            Object[][] O = (Object[][])getGestorDeConexionImple()._execute(sq().select_Id(nombreTabla,  id));
            if (O == null || O.Length == 0)
            {
                return null;
            }
            return O[0];
            //return ((Object[][]) getGestorDeConexionImple()._execute(sq().select_Id(nombreTabla, id)))[0];
            // return select_Where(nombreTabla,"id",id);
        }

    public BDConexion delete_id(string nombreTabla, int id) {
        return delete_id(nombreTabla, id + "");
    }
        public BDConexion delete_id(string nombreTabla, string idStr, int id)
        {
            return delete_id(nombreTabla, idStr,id + "");
        }

    public BDConexion delete_id(string nombreTabla, string id) {
        getGestorDeConexionImple()._execute(sq().delete_id(nombreTabla, id));
        return this;
    }

    public BDConexion delete_id(string nombreTabla,string idStr, string id)
    {
        getGestorDeConexionImple()._execute(sq().delete_id(nombreTabla, idStr, id));
        return this;
    }

        public BDConexion delete(string nombreTabla,params  Object[]a) {
        getGestorDeConexionImple()._execute(sq().delete(nombreTabla, a));
        return this;
    }

    /**
     * (nombreTabla,id#,columna,setValor1,columna2,setValor2,... )
     *
     * @param nombreTabla
     * @param id
     * @param paresColumnaValor
     * @return
     */
    public BDConexion update_Id(string nombreTabla, int id,params  Object[]paresColumnaValor) {
         //cwl("salida: nombreTabla=" + nombreTabla + " id=" + id + " " + Arrays.toString(paresColumnaValor));
        string sql = sq().update_Id(nombreTabla, id + "", paresColumnaValor);
        getGestorDeConexionImple()._execute(sql);
        return this;
    }

    public BDConexion update_Id(string nombreTabla, string id,params  Object[]paresColumnaValor) {
        getGestorDeConexionImple()._execute(sq().update_Id(nombreTabla, id, paresColumnaValor));
        return this;
    }

    public BDConexion update(string nombreTabla, string columna, Object valor,params  Object[]a) {
        return update(nombreTabla, new Object[]{columna, valor}, a);
    }

    /**
     * (nombreTabla,[columna,setValor1,columna2,setValor2,...],
     * whereColumna1,whereValor1,whereColumna2,whereValor2,...)
     *
     * @param nombreTabla
     * @param paresColumnaValor
     * @param a
     * @return
     * @throws Exception
     */
    public BDConexion update(string nombreTabla, Object[] paresColumnaValor,params  Object[]a) {
        getGestorDeConexionImple()._execute(sq().update(nombreTabla, paresColumnaValor, a));
        return this;
    }

    public bool contiene(string nombreTabla,params  Object[] paresColumnaValor) {
        return getCantidad_Where(nombreTabla, paresColumnaValor) > 0;
    }

    public bool isEmpty(string nombreTabla, string columna) {
        return getCantidad(nombreTabla, columna) == 0;
    }

    public double  getSuma(string nombreTabla, string columna) {
        return (double ) getGestorDeConexionImple()._execute(sq().getSuma(nombreTabla, columna));
    }

    public int getCantidad_Where(string nombreTabla,params  Object[]paresColumnaValor) {
        return (int) ((double ) getGestorDeConexionImple()._execute(sq().getCantidad_Where(nombreTabla, paresColumnaValor)));
    }

    public int getCantidad(string nombreTabla, string columna) {
        return (int) ((double ) getGestorDeConexionImple()._execute(sq().getCantidad(nombreTabla, columna)));
    }

    public double  getValorPromedio(string nombreTabla, string columna) {
        return (double ) getGestorDeConexionImple()._execute(sq().getValorPromedio(nombreTabla, columna));
    }

    public double  getValorMinimo(string nombreTabla, string columna) {
        return (double ) getGestorDeConexionImple()._execute(sq().getValorMinimo(nombreTabla, columna));
    }

    public double  getValorMaximo(string nombreTabla, string columna) {
        return (double ) getGestorDeConexionImple()._execute(sq().getValorMaximo(nombreTabla, columna));
    }

    public Object[][] select_Distinct_Group_By_By_Having(string nombreTabla, string []columnas, string grupBy, string heavinColumna, string heavinValor) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Distinct_Group_By_By_Having(nombreTabla, columnas, grupBy, heavinColumna, heavinValor));

    }

    public Object[][] select_Group_By_Having(string nombreTabla, string []columnas, string grupBy, string heavinColumna, string heavinValor) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Group_By_Having(nombreTabla, columnas, grupBy, heavinColumna, heavinValor));
    }

    public Object[][] select_Distinct_Group_By(string nombreTabla, string []columnas, string grupBy) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Distinct_Group_By(nombreTabla, columnas, grupBy));
    }

    public Object[][] select_Group_By(string nombreTabla, string []columnas, string grupBy) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Group_By(nombreTabla, columnas, grupBy));
    }

//    public Object[][] select_Distinct_Where_Inner_Join(params Object[] a) {
//        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Distinct_Where_Inner_Join(a));
//    }
//    public Object[][] select_Where_Inner_Join(params Object[] a) {
//        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Where_Inner_Join(a));
//    }
//    public Object[][] select_Distinct_Inner_Join(params Object[] a) {
//        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Distinct_Inner_Join(a));
//    }
//    
//    public Object[][] select_Inner_Join(params Object[] a) {
//        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Inner_Join(a));
//    }
    public Object[][] select_Distinct_Where(string nombreTabla,params  Object[]a) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Distinct_Where(nombreTabla, a));
    }

    public Object[][] select_Distinct_Todo(string nombreTabla) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Distinct_Todo(nombreTabla));
    }

    public Object[][] select_Distinct(string nombreTabla, params String[] a) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Distinct(nombreTabla, a));
    }

    public Object[][] select_Distinct_ORDER_BY(string nombreTabla,params  Object[]a) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Distinct_ORDER_BY(nombreTabla, a));
    }// 

    public Object[][] select_Distinct_Where_ORDER_BY(string nombreTabla,params  Object[]a) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Distinct_Where_ORDER_BY(nombreTabla, a));
    }

    /**
     * (nombreTabla,[]columnas,where[pares .. Columna-Valor],columnas por los
     * que ordenar)<br/>
     * (nombreTabla,where[pares .. Columna-Valor],columnas por los que ordenar)
     *
     * @param nombreTabla
     * @param a
     * @return
     * @throws Exception
     */
    public Object[][] select_Where_ORDER_BY(string nombreTabla,params  Object[]a) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Where_ORDER_BY(nombreTabla, a));
    }
    
    public Object[] select_FirstRow_Where_ValorMaximo(string  nombreTabla,String columnaValorMaximo,params Object[] paresColumnaValor){
        Object[][] O = (Object[][]) getGestorDeConexionImple()._execute(sq().select_Where_ValorMaximo(nombreTabla, columnaValorMaximo, paresColumnaValor));
        if (O == null || O.Length  == 0) {
            return null;
        }
        return O[0];
    }
    /**
     * (nombreTabla,listaDe ELEMENTO RELACIONES ENTRE TABLAS,paresColumnaValor)
     * <br>
     * <br>(nombreTabla,[TABLA,COLUMNA_REFERENCIA_ID],paresColumnaValor)
     * <br>
     * <br>ELEMENTO RELACIONES ENTRE TABLAS (ON): siempre son [Pares]
     * <br>plq la lista de ellos es ejemplo:
     * <br>[ [1 [T],[T,CID],[T,CID],[T] ] , [2 [T,CID],[T] ] , [3
     * [T,CID],[T,CID] ] ]
     * <br>recordar que siempre la un T en uno de los i tiene que aparecer en el
     * siguiente(i+1) pq es un recorrido
     * <br>
     * <br>TABLA.CULUMNA_REFERENCIA_A_ID == a
     * <ul><li>
     * [T] ,[T,CID] a TABLA.COLUMNA_NOMBRE_PERSONALIZADO_ID</li>
     *
     * <li>[T,CID],[T] a TABLA.ID el default dado de automatico</li>
     *
     * <li>[T,CID],[T,CID]</li>
     * </ul>
     * <br>
     *
     *
     *
     * <br>PAR COLUMNA VALOR
     * <ul><li>[T,C],[T,C]
     *
     * </li><li>[T,C],V
     *
     * </li><li>C,[T,C]
     *
     * </li><li>C,V
     *
     * </li><li>[T],[T,C] la [T] es [T,"id"]
     *
     * </li><li>[T],V la [T] es [T,"id"]
     *
     * </li><li>C,[T] la [T] es [T,"id"]<li><ul>
     *
     * @param nombreTabla
     * @param a
     * @return
     * @throws Exception
     */
    
    
    public Object[][] select_Where_Inner_Join_TodoDeTabla(string nombreTabla, params Object[] a) {
        //
    
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Where_Inner_Join_TodoDeTabla(nombreTabla, a));
    }
    /**
     * (nombreTabla,listaDe ELEMENTO RELACIONES ENTRE TABLAS,paresColumnaValor)
     * <br>
     * <br>(nombreTabla,[TABLA,COLUMNA_REFERENCIA_ID],paresColumnaValor)
     * <br>
     * <br>ELEMENTO RELACIONES ENTRE TABLAS (ON): siempre son [Pares]
     * <br>plq la lista de ellos es ejemplo:
     * <br>[ [1 [T],[T,CID],[T,CID],[T] ] , [2 [T,CID],[T] ] , [3
     * [T,CID],[T,CID] ] ]
     * <br>recordar que siempre la un T en uno de los i tiene que aparecer en el
     * siguiente(i+1) pq es un recorrido
     * <br>
     * <br>TABLA.CULUMNA_REFERENCIA_A_ID == a
     * <ul><li>
     * [T] ,[T,CID] a TABLA.COLUMNA_NOMBRE_PERSONALIZADO_ID</li>
     *
     * <li>[T,CID],[T] a TABLA.ID el default dado de automatico</li>
     *
     * <li>[T,CID],[T,CID]</li>
     * </ul>
     * <br>
     *
     *
     *
     * <br>PAR COLUMNA VALOR
     * <ul><li>[T,C],[T,C]
     *
     * </li><li>[T,C],V
     *
     * </li><li>C,[T,C]
     *
     * </li><li>C,V
     *
     * </li><li>[T],[T,C] la [T] es [T,"id"]
     *
     * </li><li>[T],V la [T] es [T,"id"]
     *
     * </li><li>C,[T] la [T] es [T,"id"]<li><ul>
     *
     * @param nombreTabla
     * @param a
     * @return
     * @throws Exception
     */
    public Object[] select_Where_Inner_Join_TodoDeTabla_FirstRow(string nombreTabla,params  Object[]a) {
        Object[][] O = select_Where_Inner_Join_TodoDeTabla(nombreTabla, a);
        if (O == null || O.Length  == 0) {
            return null;
        }
        return O[0];
    }

    /**
     * (nombreTabla,listaDe ELEMENTO RELACIONES ENTRE TABLAS,lista
     * where[paresColumnaValor],columnasDeOrden, o+ ordenaminento)
     * <br>
     * <br>(nombreTabla,[TABLA,COLUMNA_REFERENCIA_ID],lista
     * where[paresColumnaValor],columnasDeOrden, o+ ordenaminento)
     * <br>
     * <br>
     * <br>Las listas son arreglos <b>Object[]</b>
     * <br>
     * <br>ELEMENTO RELACIONES ENTRE TABLAS (ON): siempre son [Pares]
     * <br>plq la lista de ellos es ejemplo:
     * <br>[ [1 [T],[T,CID],[T,CID],[T] ] , [2 [T,CID],[T] ] , [3
     * [T,CID],[T,CID] ] ]
     * <br>recordar que siempre la un T en uno de los i tiene que aparecer en el
     * siguiente(i+1) pq es un recorrido
     * <br>
     * <br>TABLA.CULUMNA_REFERENCIA_A_ID == a
     * <ul><li>[T] ,[T,CID] a TABLA.COLUMNA_NOMBRE_PERSONALIZADO_ID
     *
     * </li><li>[T,CID],[T] a TABLA.ID el default dado de automatico
     *
     * </li><li>[T,CID],[T,CID]</li></ul>
     * <br>
     *
     *
     *
     *
     * <br>PAR COLUMNA VALOR
     * <ul><li>[T,C],[T,C]
     *
     * </li><li>[T,C],V
     *
     * </li><li>C,[T,C]
     *
     * </li><li>C,V
     *
     * </li><li>[T],[T,C] la [T] es [T,"id"]
     *
     * </li><li>[T],V la [T] es [T,"id"]
     *
     * </li><li>C,[T] la [T] es [T,"id"]</li></ul>
     *
     * @param nombreTabla
     * @param a
     * @return
     * @throws Exception
     */
    public Object[][] select_Where_Inner_Join_ORDER_BY_TodoDeTabla(string nombreTabla,params  Object[]a) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Where_Inner_Join_ORDER_BY_TodoDeTabla(nombreTabla, a));
    }

    public Object[][] select_ORDER_BY(string nombreTabla,params  Object[]a) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_ORDER_BY(nombreTabla, a));
    }

    public Object[][] select_Where(string nombreTabla,params  Object[]a) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Where(nombreTabla, a)); 
    }

    public Object[] select_Where_FirstRow(string nombreTabla,params  Object[]a) {
        Object[][] O = select_Where(nombreTabla, a);
        if (O == null || O.Length  == 0) {
            return null;
        }
        return O[0];
    }

//    public <E> E select_Where_FirstResult(string nombreTabla, string columna,params  Object[]a) {
//        Object[] args = new Object[a.Length  + 1];
//        args[0] = new String[]{columna};
//        for (int i = 0; i < a.Length ; i++) {
//            args[i + 1] = a[i];
//        }
//        Object[][] O = select_Where(nombreTabla, args);
//        if (O.Length  > 0) {
//            return (E) O[0][0];
//        }
//        return null;
//    }
    public Object select_Where_FirstResult(string nombreTabla, string columna,params  Object[]a) {
        Object[] args = new Object[a.Length  + 1];
        args[0] = new String[]{columna};
        for (int i = 0; i < a.Length ; i++) {
            args[i + 1] = a[i];
        }
        Object[][] O = select_Where(nombreTabla, args);
        if (O.Length  > 0) {
            return O[0][0];
        }
        return null;
    }

    public Object[][] select_Todo(string nombreTabla) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select_Todo(nombreTabla));
    }

    public Object[][] select(string nombreTabla, params String[] columnas) {
        return (Object[][]) getGestorDeConexionImple()._execute(sq().select(nombreTabla, columnas));
    }

//    public <E> E[] select_Columna(string nombreTabla, string columna) {
//        Object[][] O = select(nombreTabla, columna);
//        if (O.Length  > 0) {
//            E e[] = (E[]) Array.newInstance(O[0][0].getClass(), O.Length );
//            for (int i = 0; i < e.Length ; i++) {
//                e[i] = (E) O[i][0];
//            }
//            return e;
//        }
//
//        return null;
//    }
    public String[] select_Distinct_Columna_Str(string nombreTabla, string columna) {
        Object[][] O = select_Distinct(nombreTabla, columna);
        string []f = new String[O.Length ];
        for (int i = 0; i < f.Length ; i++) {
            f[i] = O[i][0].ToString();
        }
        return f;
    }

    public String[] select_Columna_Str(string nombreTabla, string columna) {
        Object[][] O = select(nombreTabla, columna);
        string []f = new String[O.Length ];
        for (int i = 0; i < f.Length ; i++) {
            f[i] = O[i][0].ToString();
        }
        return f;
    }

    public BDConexion select_Columna_Recorrer(string nombreTabla, string columna, metodoUtilizar2<Object, int> recorrer) {
        Object[][] O = select(nombreTabla, columna);
        //Object []f  = new Object[O.Length ];
        for (int i = 0; i < O.Length ; i++) {
            //f[i] = O[i][0];
            recorrer(O[i][0], i);
        }
        return this;
    }

    public int[] select_Columna_Int(string nombreTabla, string columna) {
        Object[][] O = select(nombreTabla, columna);
        int []f  = new int[O.Length ];
        for (int i = 0; i < f.Length ; i++) {
            f[i] = ((int) O[i][0]);
        }
        return f;
    }

    public int[] select_Distinct_Columna_Int(string nombreTabla, string columna) {
        Object[][] O = select_Distinct(nombreTabla, columna);
        int []f  = new int[O.Length ];
        for (int i = 0; i < f.Length ; i++) {
            f[i] = ((int) O[i][0]);
        }
        return f;
    }

    public double [] select_Columna_Dou(string nombreTabla, string columna) {
        Object[][] O = select(nombreTabla, columna);
        double  []f  = new double [O.Length ];
        for (int i = 0; i < f.Length ; i++) {
            f[i] = ((double ) O[i][0]);
        }
        return f;
    }

    public double [] select_Distinct_Columna_Dou(string nombreTabla, string columna) {
        Object[][] O = select_Distinct(nombreTabla, columna);
        double  []f  = new double [O.Length ];
        for (int i = 0; i < f.Length ; i++) {
            f[i] = ((double ) O[i][0]);
        }
        return f;
    }

    public BDConexion copyEnTXT(string nombreTabla, string direccion) {
        getGestorDeConexionImple()._execute(sq().copyEnTXT(nombreTabla, direccion));
        return this;
    }

    public BDConexion dropTable(string nombreTabla) {
        getGestorDeConexionImple()._execute(sq().dropTable(nombreTabla));
        return this;
    }

    /**
     * (nombreTabla,valores una sola fila completa)
     * (nombreTabla,[]columnas,valores)<br/>
     * es nuestra responsabiladad asegurarnos de que almenos uno de los valores
     * sea una llave
     *
     * @param nombreTabla
     * @param a
     * @return
     * @throws Exception
     */
    public BDConexion insertar_SinIdAutomatico(string nombreTabla,params  Object[]a) {
        // cwl("por el correcto");
        getGestorDeConexionImple()._execute(sq().insertar_SinIdAutomatico(nombreTabla, a));
        return this;
    }

    public BDResultadoInsertar insertar_ConIdAutomatico(string nombreTabla, string id,params  Object[]a) {
            //  cwl("se confunde total");
            if (datosBDConect.tipoDeConxion==TipoDeConexionBD.POSTGRES
                &&(a.Length==0||!(a[0] is string[]))
                ) {
                throw new Exception("para insertar en postgres cuando el id es SERIAL hay que expesificar las columnas a agregar"
                    +"En este caso mediante un arreglo string con los nombres de las columnas");
            }
            SQLUtiles squ = sq();
            string sql = squ.insertar_idAutomatico(nombreTabla, id, a);
            GestorDeConexionImple gs = getGestorDeConexionImple();
            Object O = gs._execute(sql);

            if (O != null && O is BDResultadoInsertar)
            {
                BDResultadoInsertar res = (BDResultadoInsertar)O;
                return res;
            }

            return null;
        }

    public BDConexion insertar_Filas(string nombreTabla,params Object[][] A) {
        for (int i = 0; i < A.Length ; i++) {
            insertar(nombreTabla, A[i]);
        }
        return this;
    }

    public BDResultadoInsertar insertar(string nombreTabla,params  Object[]a) {
        // cwl("por aqui?");
        Object O=getGestorDeConexionImple()._execute(sq().insertar(nombreTabla, a));
        
        if(O!=null&& O  is  BDResultadoInsertar){
            BDResultadoInsertar res=(BDResultadoInsertar)O;
            return res;
        }
        
        return null;
    }

    public BDConexion crearTabla(string nombreTabla,params  Object[]NombreTipos) {
        getGestorDeConexionImple()._execute(sq().crearTabla(nombreTabla, NombreTipos));
        return this;
    }

    public BDConexion crearTablaSiNoExiste(string nombreTabla,params  Object[]NombreTipos) {
        getGestorDeConexionImple()._execute(sq().crearTablaSiNoExiste(nombreTabla, NombreTipos));
        return this;
    }

    public BDConexion crearTablaYBorrarSiExiste(string nombreTabla,params  Object[]NombreTipos) {

        drop_table_if_exist(nombreTabla);
        getGestorDeConexionImple()._execute(sq().crearTabla(nombreTabla, NombreTipos));
        return this;
    }

    public BDConexion drop_table_if_exist(string nombreTabla) {
        getGestorDeConexionImple()._execute(sq().drop_table_if_exist(nombreTabla));
        return this;
    }

    public static BDConexion getConexionSQL_LITE(string url) {
        return getConexionSQL_LITE(new FileInfo(url));
    }

    public static BDConexion getConexionSQL_LITE(FileInfo url) {
		BDConexion bd=new BDConexion("", "", "", "", "", TipoDeConexionBD.SQL_LITE, url);
		GestorDeConexionSQLite g=new GestorDeConexionSQLite(bd.datosBDConect);
		//GestorDeConexioSQLite g=new GestorDeConexioSQLite(url);
		//g.setDatosBDConect(bd.datosBDConect);
		bd.setGestorDeConexionImple(g);
		return bd;
    }

        public static BDConexion getConexion_POSTGRES(string nombreBD, string usuario="postgres",string contrasenna="postgres",string servidor="localhost",string puerto="5432")
        {
            BDConexion bd = new BDConexion(usuario:usuario
                ,contraseña:contrasenna,servidor:servidor,nombreBD: nombreBD
                ,puerto:puerto,tipoDeConxion:TipoDeConexionBD.POSTGRES
                ,url:null);
            GestorDeConexion_Postgresql g = new GestorDeConexion_Postgresql(bd.datosBDConect);
            //GestorDeConexioSQLite g=new GestorDeConexioSQLite(url);
            //g.setDatosBDConect(bd.datosBDConect);
            bd.setGestorDeConexionImple(g);
            return bd;
        }
        //
        //    public static BDConexion getPOSTGRESConexion(string usuario, string contraseña, string nombreBD, string servidor, string puerto) {
        //        return new BDConexion(usuario, contraseña, servidor, nombreBD, puerto, TipoDeConexionBD.POSTGRES, null);
        //    }

        //    public static BDConexion getPOSTGRESConexionLocal5432(string usuario, string contraseña, string nombreBD) {
        //        return getPOSTGRESConexion(usuario, contraseña, nombreBD, "localhost", "5432");
        //    }


    }
}
