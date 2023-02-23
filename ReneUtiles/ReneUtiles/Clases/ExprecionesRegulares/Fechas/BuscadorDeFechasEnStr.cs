using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReneUtiles;
using ReneUtiles.Clases;
using System.Text.RegularExpressions;

namespace ReneUtiles.Clases.ExprecionesRegulares.Fechas
{
    public class BuscadorDeFechasEnStr:ExprecionesRegularesBasico
    {
        public RecursosDePatronesDeFecha refechas;

        public List<DatosDeFechaEnStr> fechasEnNombre;
        public MatchCollection mc;

        public Func<string, int, int,bool> predicate_esNombreNumericosCompletosMultiples;//nombre,numero,indiceInicialDelNumero
        public Func<int, int, bool> predicate_esNumeroParteDeNombre;//numero,indiceInicialDelNumero
        public Func<int, bool> predicate_esPalabraNumericaSimple;//numero,

        public BuscadorDeFechasEnStr(RecursosDePatronesDeFecha refechas) 
		{
            this.fechasEnNombre = new List<DatosDeFechaEnStr>();
            this.refechas = refechas;

            this.predicate_esNombreNumericosCompletosMultiples = (a, b, c) => false;
            this.predicate_esNumeroParteDeNombre = (a, b) => false;
            this.predicate_esPalabraNumericaSimple = (a) => false;
        }
        
        public List<DatosDeFechaEnStr> getFechasEnNombre()
        {
            return this.fechasEnNombre;
        }
        public List<DatosDeFechaEnStr> buscarFechas(string nombre,int i0)
        {
            //if (seBuscoCon(i0))
            //{
            //    return fechasEnNombre;
            //}
            //this.seBusco = true;
            //this.I0 = i0;

            Regex exprecion = refechas.Re_Fecha.SSfreSfS;


            this.mc = exprecion.Matches(nombre, i0);

            foreach (Match m in mc)
            {
                Group gF = refechas.getGrupoFecha(m);
                Group gND = refechas.getGrupoDia(m);
                Group gNM = refechas.getGrupoMes(m);
                Group gNA = refechas.getGrupoAño(m);
                Group gEI = refechas.getGrupoEnvolturaInicialFecha(m);
                if (gF.Success)
                {
                    if (gND.Success && gNA.Success && gNM.Success)
                    {
                        DatosDeNumeroEnteroEncontrado da = Matchs.getPrimerNumerEntero(nombre);
                        if (predicate_esNombreNumericosCompletosMultiples(nombre
                                                                                ,  da.numero
                                                                                ,  da.indiceInicial
                                                                               ))
                        {

                            continue;
                        }
                    }

                    DatosDeFechaEnStr d = new DatosDeFechaEnStr();
                    d.AñoStr = gNA.ToString();
                    d.indiceDeteccionDeAño = gNA.Index;

                    if ((!gND.Success) && (!gNM.Success)
                             && gNA.Success && (!gEI.Success))
                    {

                        if (predicate_esNumeroParteDeNombre(
                           (int)d.Año,
                             (int)d.indiceDeteccionDeAño
                        
                        ))
                        {
                            continue;
                        }

                        if (predicate_esPalabraNumericaSimple((int)d.Año))
                        {
                            continue;
                        }
                    }

                    if (gND.Success)
                    {
                        d.DiaStr = gND.ToString();
                        d.indiceDeteccionDeDia = gND.Index;
                    }
                    if (gNM.Success)
                    {
                        d.MesStr = gNM.ToString();
                        d.indiceDeteccionDeMes = gNM.Index;
                    }
                    d.fechaStr = gF.ToString();
                    d.indiceDeteccionDeFecha = gF.Index;

                    d.indiceAContinuacion = m.Index + m.Length;
                    this.fechasEnNombre.Add(d);
                }
            }

            return this.fechasEnNombre;
        }

        public bool estaDentroDeFecha(int indice)
        {

            foreach (DatosDeFechaEnStr d in fechasEnNombre)
            {
                if (d != null && d.estaDentroDeLosLimites(indice))
                {
                    return true;
                }
            }
            return false;
            //return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
        }

        public DatosDeIgnorarNumero estaDentroDeFecha_DatosDeIgnorarNumero(int indice)
        {

            foreach (DatosDeFechaEnStr d in fechasEnNombre)
            {
                DatosDeIgnorarNumero dd = null;
                if (d != null)
                {
                    dd = d.estaDentroDeLosLimites_DatosDeIgnorarNumero(indice);
                    if (dd != null)
                    {
                        return dd;
                    }

                }
            }
            return null;
            //return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
        }
    }
}
