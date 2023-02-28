using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles.Clases.BD.Factory.Consultas;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
using ReneUtiles.Clases.BD.Conexion; 
namespace ReneUtiles.Clases.BD.Factory
{
    public abstract class CodeBDLenguaje_ConIAdmin:CodeBDLenguaje
    {
        public CodeBDLenguaje_ConIAdmin(FactoryBD factory, DatosDeConexionFactoryBD datosConexionFactory)
            :base(factory,datosConexionFactory)
        {

        }

        public abstract string getStrBD_IAdminPadre(int separacion0);

        public abstract string getStrMetodoCrearTabla_Abstract(ModeloBD m, int separacion0);
        public abstract string getStrMetodoCrearTablaSiNoExiste_Abstract(ModeloBD m, int separacion0);

        public abstract string getStrMetodoGetArgs_Abstract(ModeloBD m, int separacion0);

        public abstract string getStrMetodoContentArgs_Abstract(ModeloBD m, int separacion0);

        public abstract string getStrMetodoGetForID_Abstract(ModeloBD_ID m, int separacion0);

        public abstract string getStrMetodoInsertar_Abstract(ModeloBD_ID m, int separacion0);
        public abstract string getStrMetodoGetAll_Abstract(ModeloBD_ID m, int separacion0);

        public abstract string getStrMetodoUpdate_Abstract(ModeloBD_ID m, int separacion0);

        public abstract string getStrMetodoDeleteForID_Abstract(ModeloBD_ID m, int separacion0);

        public abstract string getStrMetodoGetAll_ForColumna_Abstract(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0);

        public abstract string getStrMetodoGet_ForColumna_Abstract(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0);

        public abstract string getStrMetodoDelete_ForColumna_Abstract(ModeloBD_ID m, ColumnaDeModeloBD c, int separacion0);

        public abstract string getStrMetodoGetAll_ForListaDeColumnas_Abstract(ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0);

        public abstract string getStrMetodoGet_ForListaDeColumnas_Abstract(string nombreDelMetodo, ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0);

        public virtual string getStrMetodoGet_ForListaDeColumnas_Abstract(ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0)
        {
            return getStrMetodoGet_ForListaDeColumnas_Abstract(getNombreMetodoGet_ForListaDeColumnas(m, C), m, C, separacion0);
        }

        public abstract string getStrMetodoDelete_ForListaDeColumnas_Abstract(ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0);

        public virtual string getStrMetodoGetListaDe_OneToManyLinkInterno_Abstract(OneToMany o, int separacion0)
        {
            ModeloBD m = o.Many;
            ColumnaDeModeloBD c = o.LinkToOne;
            return getStrMetodoGetAll_ForColumna_Abstract((ModeloBD_ID)m, c, separacion0);

        }

        public virtual string getStrMetodoGetAll_InnerJoin_ForListaDeColumnas_Abstract(InnerJoin I, int separacion0)
        {
            return getStrMetodoGetAll_InnerJoin_ForListaDeColumnas_Abstract(I.ModeloDestino, I.Cadena, I.ElementosWhere, separacion0);
        }

        public abstract string getStrMetodoGetAll_InnerJoin_ForListaDeColumnas_Abstract(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0);

        public virtual string getStrMetodoGetAll_InnerJoin_ForListaDeColumnas_Link_Abstract(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0)
        {
            return getStrMetodoGetAll_InnerJoin_ForListaDeColumnas_Abstract(m, __getCadenaRealDeCadenaLink(cadena), elementosWhere, separacion0);
        }

        public virtual string getStrMetodoGetListaDe_OneToManyTablaExterna_Abstract(OneToMany_EnTablaExterna o, int separacion0)
        {
            ModeloBD_ID m = (ModeloBD_ID)o.Many;
            List<ColumnaDeModeloBD> u = o.Union.Columnas;
            ModeloBD on = o.One;

            return getStrMetodoGetAll_InnerJoin_ForListaDeColumnas_Link_Abstract(m, listE(u[0], u[1]), listE(o.One), separacion0);


        }


        public virtual string getStrMetodosManyToMany_Abstract(ManyToMany o, int separacion0)
        {
            ModeloBD_ID m1 = o.Many_1;
            ModeloBD_ID m2 = o.Many_2;
            List<ColumnaDeModeloBD> lc = o.Union.Columnas;

            metodoCreador2<ModeloBD_ID, ModeloBD_ID, string> getMetodoInner = (mReturn, mKey) => getStrMetodoGetAll_InnerJoin_ForListaDeColumnas_Link_Abstract(mReturn, listE(lc[0], lc[1]), listE(mKey), separacion0);
            string mr = "";//"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!s";
            mr += getMetodoInner(m1, m2);
            mr += "\n";//"\n !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!";
            mr += getMetodoInner(m2, m1);
            return mr;


        }

        public virtual string getStrMetodoGet_InnerJoin_ForListaDeColumnas_Abstract(InnerJoin I, int separacion0)
        {
            return getStrMetodoGet_InnerJoin_ForListaDeColumnas_Abstract(I.ModeloDestino, I.Cadena, I.ElementosWhere, separacion0);
        }


        public abstract string getStrMetodoGet_InnerJoin_ForListaDeColumnas_Abstract(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0);


        public virtual string getStrMetodoGet_InnerJoin_ForListaDeColumnas_Link_Abstract(ModeloBD_ID m, List<ElementoPorElQueBuscar> cadena, List<ElementoPorElQueBuscar> elementosWhere, int separacion0)
        {

            return getStrMetodoGet_InnerJoin_ForListaDeColumnas_Abstract(m, __getCadenaRealDeCadenaLink(cadena), elementosWhere, separacion0);
        }

        public abstract string getStrMetodoDeleteForID_Cascade_Abstract(ModeloBD_ID m, EsquemaBD E, int separacion0);

        public abstract string getStrMetodoDelete_ForColumna_Cascade_Abstract(ModeloBD_ID m, ColumnaDeModeloBD c, EsquemaBD E, int separacion0);


        public abstract string getStrMetodoDelete_ForListaDeColumnas_Cascade_Abstract(ModeloBD_ID m, List<ColumnaDeModeloBD> C, EsquemaBD E, int separacion0);


        public abstract string getStrMetodoExiste_Abstract(ModeloBD_ID m, ColumnaDeModeloBD c, bool soloHayEsteEnElModelo, int separacion0);


        //por aqui
        public abstract string getStrMetodoExiste_ForListaDeColumnas_Abstract(string nombreMetodo, ModeloBD_ID m, List<ColumnaDeModeloBD> C, int separacion0);
        

             
        public virtual string getStrMetodoExiste_ForListaDeColumnas_Abstract(ModeloBD_ID m, List<ColumnaDeModeloBD> C, bool soloHayEsteEnElModelo, int separacion0)
        {
			return getStrMetodoExiste_ForListaDeColumnas_Abstract(getNombreMetodoExiste_ForListaDeColumnas(m, C, soloHayEsteEnElModelo), m, C, separacion0);
		}





        public virtual string getStrMetodoGet_EnBD_OneToManyTablaExterna_Abstract(OneToMany_EnTablaExterna o, int separacion0)
        {
            ModeloBD_ID m = (ModeloBD_ID)o.Many;
            List<ColumnaDeModeloBD> ul = o.Union.Columnas;
            ModeloBD on = o.One;

            return getStrMetodoGet_ForListaDeColumnas_Abstract(getNombreMetodoGet_EnBD_OneToManyEnTablaExterna(o), o.Union, ul, separacion0);


        }

        public virtual string getStrMetodoExiste_OneToManyEnTablaExterna_Abstract(OneToMany_EnTablaExterna o, int separacion0)
        {
            return getStrMetodoExiste_ForListaDeColumnas_Abstract(getNombreMetodoExiste_OneToManyEnTablaExterna(o), o.Union, o.Union.Columnas, separacion0);
        }

        public virtual string getStrMetodoExiste_ManyToMany_Abstract(ManyToMany o, int separacion0)
        {
            return getStrMetodoExiste_ForListaDeColumnas_Abstract(getNombreMetodoExiste_ManyToMany(o), o.Union, o.Union.Columnas, separacion0);
        }

        public virtual string getStrMetodoGet_EnBD_ManyToMany_Abstract(ManyToMany o, int separacion0)
        {
            //ModeloBD_ID m = (ModeloBD_ID)o.Many;
            List<ColumnaDeModeloBD> ul = o.Union.Columnas;
            //ModeloBD on = o.One;

            return getStrMetodoGet_ForListaDeColumnas_Abstract(getNombreMetodoGet_EnBD_ManyToMany(o), o.Union, ul, separacion0);


        }

        public abstract string getStrMetodoExiste_ForID_Abstract(ModeloBD_ID m, int separacion0);

        public abstract string getStrMetodoGetAll_ForListaDeColumnas_Sort_Abstract(SelectWhereSort s, int separacion0);

        public virtual string getStrMetodoGetListaDe_OneToManyLinkInterno_Sort_Abstract(OneToManySort o, int separacion0)
        {

            return getStrMetodoGetAll_ForListaDeColumnas_Sort_Abstract(o.Sort, separacion0);

        }


        public abstract string getStrMetodoCrearTodasLasTablas_Abstract(int separacion0);
        public abstract string getStrMetodoCrearTodasLasTablasSiNoExisten_Abstract(int separacion0);
        public abstract string getStrMetodoGetSesionStorage_Abstract(int separacion0);

        protected virtual string __getStrMetodosEnAdmin(int separacion0) {
            int distancia = separacion0;
            EsquemaBD E = factory.Esquema;
            string bd = "";

            //bd += separacion1 + "public string " + getNombreMetodoUrlBD() + "(){";
            //bd += separacion2 + "return  this.urlBD;";
            //bd += separacion1 + "}";
            for (int i = 0; i < E.getCantidadDeModelos(); i++)
            {
                ModeloBD_ID mt = (ModeloBD_ID)E.getModelo(i);

                bd += getStrMetodoCrearTabla(mt, distancia);
                bd += getStrMetodoCrearTablaSiNoExiste(mt, distancia);
                bd += getStrMetodoGetArgs(mt, distancia);
                bd += getStrMetodoContentArgs(mt, distancia);
                bd += getStrMetodoGetForID(mt, distancia);
                bd += getStrMetodoInsertar(mt, distancia);
                bd += getStrMetodoGetAll(mt, distancia);
                bd += getStrMetodoUpdate(mt, distancia);
                bd += getStrMetodoDeleteForID(mt, distancia);
                bd += getStrMetodoExiste_ForID(mt, distancia);

                bool existeSoloEste = mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste.Count == 1
                    && mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste[0].Count > 1;
                for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste.Count; j++)
                {
                    List<ColumnaDeModeloBD> lc = mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste[j];
                    bd += getStrMetodoExiste_ForListaDeColumnas(mt, lc, existeSoloEste, distancia);
                }


                //cwl("i="+i+" "+mt.Nombre);
                //CrearDeleteCascade Cr = factory.Esquema.listaCrearDeleteCascade[mt];
                if (factory.Esquema.necistaUnDeleteCascade(mt))
                {
                    bd += getStrMetodoDeleteForID_Cascade(mt, factory.Esquema, distancia);
                }

            }
            int separacionActual = 2;

            bd += getStrMetodoCrearTodasLasTablas(separacionActual);
            bd += getStrMetodoCrearTodasLasTablasSiNoExisten(separacionActual);







            if (factory.Esquema.UsarSesionStorage)
            {
                bd += getStrMetodoGetSesionStorage(separacionActual);


            }



            for (int i = 0; i < E.getCantidadDeModelos(); i++)
            {
                ModeloBD_ID mt = (ModeloBD_ID)E.getModelo(i);


                List<ColumnaDeModeloBD> unicos = new List<ColumnaDeModeloBD>();
                for (int j = 0; j < mt.Columnas.Count; j++)
                {
                    ColumnaDeModeloBD cmc = mt.Columnas[j];
                    if (cmc.BuscarListaPorEstaColumna)
                    {
                        bd += getStrMetodoGetAll_ForColumna(mt, cmc, distancia);
                    }
                    if (cmc.BuscarModeloPorEstaColumna)
                    {
                        bd += getStrMetodoGet_ForColumna(mt, cmc, distancia);
                    }
                    if (cmc.EliminarPorEstaColumna)
                    {
                        bd += getStrMetodoDelete_ForColumna(mt, cmc, distancia);
                        //CrearDeleteCascade Cr = factory.Esquema.listaCrearDeleteCascade[mt];
                        if (factory.Esquema.necistaUnDeleteCascade(mt))
                        {
                            bd += getStrMetodoDelete_ForColumna_Cascade(mt, cmc, factory.Esquema, distancia);
                        }
                    }
                    if (cmc.EsUnique)
                    {
                        unicos.Add(cmc);
                    }

                }

                for (int j = 0; j < unicos.Count; j++)
                {
                    ColumnaDeModeloBD cmc = unicos[j];
                    bd += getStrMetodoExiste(mt, cmc, unicos.Count == 1, distancia);
                }



                for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos.Count; j++)
                {
                    List<ColumnaDeModeloBD> l = mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos[j];
                    bd += getStrMetodoGetAll_ForListaDeColumnas(mt, l, distancia);
                }

                for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueBuscarUnModelo.Count; j++)
                {
                    List<ColumnaDeModeloBD> l = mt.ListaDeConjuntoDeColumnasPorLasQueBuscarUnModelo[j];
                    bd += getStrMetodoGet_ForListaDeColumnas(mt, l, distancia);
                }

                for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueEliminar.Count; j++)
                {
                    List<ColumnaDeModeloBD> l = mt.ListaDeConjuntoDeColumnasPorLasQueEliminar[j];
                    bd += getStrMetodoDelete_ForListaDeColumnas(mt, l, distancia);

                    //CrearDeleteCascade Cr = factory.Esquema.listaCrearDeleteCascade[mt];
                    if (factory.Esquema.necistaUnDeleteCascade(mt))
                    {
                        bd += getStrMetodoDelete_ForListaDeColumnas_Cascade(mt, l, factory.Esquema, distancia);
                    }
                }

                //				for (int j = 0; j < mt.ListaOneToMany.Count; j++) {
                //					OneToMany o = mt.ListaOneToMany[j];
                //					bd += getStrMetodoGetListaDe_OneToManyLinkInterno(o, distancia);
                //					
                //				}
                for (int j = 0; j < mt.ListaOneToMany_EnTablaExterna.Count; j++)
                {
                    OneToMany_EnTablaExterna o = mt.ListaOneToMany_EnTablaExterna[j];
                    bd += getStrMetodoGetListaDe_OneToManyTablaExterna(o, distancia);
                    bd += getStrMetodoGet_EnBD_OneToManyTablaExterna(o, distancia);
                    bd += getStrMetodoExiste_OneToManyEnTablaExterna(o, distancia);
                }

                for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelosYOrdenar.Count; j++)
                {
                    SelectWhereSort o = mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelosYOrdenar[j];
                    bd += getStrMetodoGetAll_ForListaDeColumnas_Sort(o, distancia);

                }

            }

            List<ManyToMany> lmtm = factory.Esquema.ListaManyToMany;
            for (int i = 0; i < lmtm.Count; i++)
            {

                ManyToMany o = lmtm[i];
                bd += getStrMetodosManyToMany(o, 1);
                bd += getStrMetodoGet_EnBD_ManyToMany(o, distancia);
                bd += getStrMetodoExiste_ManyToMany(o, distancia);
            }

            List<InnerJoin> linj = factory.Esquema.ListaInnerJoinAll;
            for (int i = 0; i < linj.Count; i++)
            {
                InnerJoin I = linj[i];
                bd += getStrMetodoGetAll_InnerJoin_ForListaDeColumnas(I, 1);
            }

            linj = factory.Esquema.ListaInnerJoinOne;
            for (int i = 0; i < linj.Count; i++)
            {
                InnerJoin I = linj[i];
                bd += getStrMetodoGet_InnerJoin_ForListaDeColumnas(I, 1);
            }
            return bd;
        }

        protected virtual string __getStrMetodosEnAdmin_Abstract(int separacion0)
        {
            int distancia = separacion0;
            EsquemaBD E = factory.Esquema;
            string bd = "";

            for (int i = 0; i < E.getCantidadDeModelos(); i++)
            {
                ModeloBD_ID mt = (ModeloBD_ID)E.getModelo(i);

                bd += getStrMetodoCrearTabla_Abstract(mt, distancia);


                bd += getStrMetodoCrearTablaSiNoExiste_Abstract(mt, distancia);


                bd += getStrMetodoGetArgs_Abstract(mt, distancia);


                bd += getStrMetodoContentArgs_Abstract(mt, distancia);


                bd += getStrMetodoGetForID_Abstract(mt, distancia);


                bd += getStrMetodoInsertar_Abstract(mt, distancia);


                bd += getStrMetodoGetAll_Abstract(mt, distancia);


                bd += getStrMetodoUpdate_Abstract(mt, distancia);


                bd += getStrMetodoDeleteForID_Abstract(mt, distancia);


                bd += getStrMetodoExiste_ForID_Abstract(mt, distancia);

                bool existeSoloEste = mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste.Count == 1
                    && mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste[0].Count > 1;
                for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste.Count; j++)
                {
                    List<ColumnaDeModeloBD> lc = mt.ListaDeConjuntoDeColumnasPorLasVerSiExiste[j];
                    bd += getStrMetodoExiste_ForListaDeColumnas_Abstract(mt, lc, existeSoloEste, distancia);
                }


                //cwl("i="+i+" "+mt.Nombre);
                //CrearDeleteCascade Cr = factory.Esquema.listaCrearDeleteCascade[mt];
                if (factory.Esquema.necistaUnDeleteCascade(mt))
                {
                    bd += getStrMetodoDeleteForID_Cascade_Abstract(mt, factory.Esquema, distancia);
                }

            }

            int separacionActual = 2;
            bd += getStrMetodoCrearTodasLasTablas_Abstract(separacionActual);
            bd += getStrMetodoCrearTodasLasTablasSiNoExisten_Abstract(separacionActual);
            if (factory.Esquema.UsarSesionStorage)
            {
                bd += getStrMetodoGetSesionStorage_Abstract(separacionActual);

            }






            for (int i = 0; i < E.getCantidadDeModelos(); i++)
            {
                ModeloBD_ID mt = (ModeloBD_ID)E.getModelo(i);


                List<ColumnaDeModeloBD> unicos = new List<ColumnaDeModeloBD>();
                for (int j = 0; j < mt.Columnas.Count; j++)
                {
                    ColumnaDeModeloBD cmc = mt.Columnas[j];
                    if (cmc.BuscarListaPorEstaColumna)
                    {
                        bd += getStrMetodoGetAll_ForColumna_Abstract(mt, cmc, distancia);
                    }
                    if (cmc.BuscarModeloPorEstaColumna)
                    {
                        bd += getStrMetodoGet_ForColumna_Abstract(mt, cmc, distancia);
                    }
                    if (cmc.EliminarPorEstaColumna)
                    {
                        bd += getStrMetodoDelete_ForColumna_Abstract(mt, cmc, distancia);
                        //CrearDeleteCascade Cr = factory.Esquema.listaCrearDeleteCascade[mt];
                        if (factory.Esquema.necistaUnDeleteCascade(mt))
                        {
                            bd += getStrMetodoDelete_ForColumna_Cascade_Abstract(mt, cmc, factory.Esquema, distancia);
                        }
                    }
                    if (cmc.EsUnique)
                    {
                        unicos.Add(cmc);
                    }

                }

                for (int j = 0; j < unicos.Count; j++)
                {
                    ColumnaDeModeloBD cmc = unicos[j];
                    bd += getStrMetodoExiste_Abstract(mt, cmc, unicos.Count == 1, distancia);
                }



                for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos.Count; j++)
                {
                    List<ColumnaDeModeloBD> l = mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos[j];
                    bd += getStrMetodoGetAll_ForListaDeColumnas_Abstract(mt, l, distancia);
                }

                for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueBuscarUnModelo.Count; j++)
                {
                    List<ColumnaDeModeloBD> l = mt.ListaDeConjuntoDeColumnasPorLasQueBuscarUnModelo[j];
                    bd += getStrMetodoGet_ForListaDeColumnas_Abstract(mt, l, distancia);
                }

                for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueEliminar.Count; j++)
                {
                    List<ColumnaDeModeloBD> l = mt.ListaDeConjuntoDeColumnasPorLasQueEliminar[j];
                    bd += getStrMetodoDelete_ForListaDeColumnas_Abstract(mt, l, distancia);

                    //CrearDeleteCascade Cr = factory.Esquema.listaCrearDeleteCascade[mt];
                    if (factory.Esquema.necistaUnDeleteCascade(mt))
                    {
                        bd += getStrMetodoDelete_ForListaDeColumnas_Cascade_Abstract(mt, l, factory.Esquema, distancia);
                    }
                }


                for (int j = 0; j < mt.ListaOneToMany_EnTablaExterna.Count; j++)
                {
                    OneToMany_EnTablaExterna o = mt.ListaOneToMany_EnTablaExterna[j];
                    bd += getStrMetodoGetListaDe_OneToManyTablaExterna_Abstract(o, distancia);
                    bd += getStrMetodoGet_EnBD_OneToManyTablaExterna_Abstract(o, distancia);
                    bd += getStrMetodoExiste_OneToManyEnTablaExterna_Abstract(o, distancia);
                }

                for (int j = 0; j < mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelosYOrdenar.Count; j++)
                {
                    SelectWhereSort o = mt.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelosYOrdenar[j];
                    bd += getStrMetodoGetAll_ForListaDeColumnas_Sort_Abstract(o, distancia);

                }

            }

            List<ManyToMany> lmtm = factory.Esquema.ListaManyToMany;
            for (int i = 0; i < lmtm.Count; i++)
            {

                ManyToMany o = lmtm[i];
                bd += getStrMetodosManyToMany_Abstract(o, 1);
                bd += getStrMetodoGet_EnBD_ManyToMany_Abstract(o, distancia);
                bd += getStrMetodoExiste_ManyToMany_Abstract(o, distancia);
            }

            List<InnerJoin> linj = factory.Esquema.ListaInnerJoinAll;
            for (int i = 0; i < linj.Count; i++)
            {
                InnerJoin I = linj[i];
                bd += getStrMetodoGetAll_InnerJoin_ForListaDeColumnas_Abstract(I, 1);
            }

            linj = factory.Esquema.ListaInnerJoinOne;
            for (int i = 0; i < linj.Count; i++)
            {
                InnerJoin I = linj[i];
                bd += getStrMetodoGet_InnerJoin_ForListaDeColumnas_Abstract(I, 1);
            }
            return bd;
        }
    }
}
