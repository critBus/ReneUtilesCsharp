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
    public class RecursosDePatronesDeFecha:ExprecionesRegularesBasico
    {
        public static readonly string KEY_NUMERO_DIA = "dia";
        public static readonly string KEY_NUMERO_AÑO = "anno";
        public static readonly string KEY_NUMERO_MES = "mes";
        public static readonly string KEY_FECHA = "fecha";
        public static readonly string KEY_SEPARACION_FECHA = "separacion_fecha";
        public static readonly string KEY_ENVOLTURA_INICIAL_FECHA = "envol_ini_fecha";
        public static readonly string KEY_ENVOLTURA_FINAL_FECHA = "envol_ini_fecha";

        public PatronRegex Re_Fecha;
        string patron_Fecha;

        public RecursosDePatronesDeFecha() {

            string dia = @"(?<" + KEY_NUMERO_DIA + @">(?:[0-2]?[0-9])|(?:3[0-1]))";
            string diaAlto = @"(?<" + KEY_NUMERO_DIA + @">(?:1[3-9])|(?:2[0-9])|(?:3[0-1]))";
            string nNormal = @"(?:0?[0-9])|(?:1[0-2])";
            string mes = @"(?<" + KEY_NUMERO_MES + @">" + nNormal + ")";
            string gSeparacion = @"(?<" + KEY_SEPARACION_FECHA + @">[-_. ])";
            string año = @"(?<" + KEY_NUMERO_AÑO + @">(?:19[5-9][0-9])|(?:20[0-9][0-9]))";
            string kSeparacion = @"\k<" + KEY_SEPARACION_FECHA + @">";
            string envolturaInicial = @"(?<" + KEY_ENVOLTURA_INICIAL_FECHA + @">[[]|[(]|[{])";
            string envolturaFinal = @"(?<" + KEY_ENVOLTURA_FINAL_FECHA + @">[]]|[)]|[}])";

            patron_Fecha = @"(?<" + KEY_FECHA + @">(?:" + diaAlto + gSeparacion + mes + kSeparacion + año + @")|(?:" + mes + gSeparacion + diaAlto + kSeparacion + año + @")|(?:" + dia + gSeparacion + mes + kSeparacion + año + @")|(?:" + envolturaInicial + año + envolturaFinal + @")|(?:" + año + @"))";
            Re_Fecha = new PatronRegex(patron_Fecha);
        }

        public Group getGrupoAño(Match m)
        {
            return m.Groups[KEY_NUMERO_AÑO];
        }
        public Group getGrupoMes(Match m)
        {
            return m.Groups[KEY_NUMERO_MES];
        }
        public Group getGrupoDia(Match m)
        {
            return m.Groups[KEY_NUMERO_DIA];
        }
        public Group getGrupoFecha(Match m)
        {
            return m.Groups[KEY_FECHA];
        }
        public Group getGrupoSeparacionFecha(Match m)
        {
            return m.Groups[KEY_SEPARACION_FECHA];
        }
        public Group getGrupoEnvolturaInicialFecha(Match m)
        {
            return m.Groups[KEY_ENVOLTURA_INICIAL_FECHA];
        }
        public Group getGrupoEnvolturaFinalFecha(Match m)
        {
            return m.Groups[KEY_ENVOLTURA_FINAL_FECHA];
        }
    }
}
