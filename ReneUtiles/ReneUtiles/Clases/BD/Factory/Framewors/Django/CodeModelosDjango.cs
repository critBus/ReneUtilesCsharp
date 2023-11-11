using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;
using ReneUtiles.Clases.BD.Factory.Framewors;

namespace ReneUtiles.Clases.BD.Factory.Framewors.Django
{
    public class CodeModelosDjango : CodeBDFramework
    {
        public string getCreate(ModeloBD m, int separacion0) {
            SalidaCodeStr _ = new SalidaCodeStr(separacion0);
            string nombreTipo = getNombreStrModelo(m);
            _.sp(0,"def save_"+ nombreTipo + "(self");
            for (int i = 0; i < m.Columnas.Count; i++)
            {
                ColumnaDeModeloBD c = m.Columnas[i];
                _.s(","
                    +CodeBDLenguaje.getNombreStrColumnaModelo(m, c));
            }
            _.s("):");
            string variableM = "m";
            _.sp(1, variableM+" = " + nombreTipo + ".objects.create(");
            for (int i = 0; i < m.Columnas.Count; i++)
            {
                ColumnaDeModeloBD c = m.Columnas[i];
                _.sp(2, CodeBDLenguaje.getNombreStrColumnaModeloCapitalice(m, c)
                + "=" + CodeBDLenguaje.getNombreStrColumnaModelo(m, c));
            }
            _.sp(2, ")");
            _.sp(1, variableM + ".save()");
            _.sp(1, "return " + variableM );


            return _.salida;
        }
    }
}
