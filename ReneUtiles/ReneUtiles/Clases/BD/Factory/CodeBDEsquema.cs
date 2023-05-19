using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;
namespace ReneUtiles.Clases.BD.Factory
{
    public abstract class CodeBDEsquema:ConsolaBasica
    {
        public abstract string getEsquemaStr(List<ModeloBD> modelos);

        public  string getNombreStrModeloLower(ModeloBD m)
        {
            return Utiles.getLowerPiso(m.Nombre.Replace("TABLA_", ""));
        }
        public  string getNombreStrModelo(ModeloBD m)
        {
            //if (factory.conservarNombres) {
            //    return m.Nombre;
            //}
            //cwl("m="+m);
            //cwl("m.Nombre="+m.Nombre);
            string r = Utiles.getCapitalizeUnido(m.Nombre.Replace("TABLA_", ""));

            //cwl("capi uni="+r);
            r = Utiles.llevarASingular(r);
            //cwl("r="+r);
            return r;//"_MD";
        }

    }
}

