using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles.Clases.BD.Factory.Consultas;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
using ReneUtiles.Clases.BD.Conexion;
namespace ReneUtiles.Clases.BD.Factory.Framewors
{
    public abstract class CodeBDFramework : ConsolaBasica
    {
        public FactoryFrameworkBD factory;
        public string Extencion;


        protected string getNombreClaseVariable(string nombre)
        {
            return Char.ToLower(nombre[0]) + subs(nombre, 1).ToLower();
        }

        public string getNombreVariableElemento(ElementoPorElQueBuscar t)
        {
            if (t is ModeloBD)
            {
                ModeloBD m = (ModeloBD)t;
                return getNombreStrIdkeyModelo(m);

            }
            else if (t is ColumnaDeModeloBD)
            {
                ColumnaDeModeloBD c = (ColumnaDeModeloBD)t;
                return getNombreStrColumnaModelo(c); 
            }
            return null;
        }

        public string getNombreStrElementoCapitalice(ElementoPorElQueBuscar t)
        {
            return Utiles.capitalize(getNombreVariableElemento(t));
        }


        public ModeloBD getModeloDeElemento(ElementoPorElQueBuscar t)
        {
            //			cwl("t="+t);
            if (t is ModeloBD)
            {
                //				cwl("retorno modelo");
                ModeloBD m = (ModeloBD)t;
                return m;

            }
            else if (t is ColumnaDeModeloBD)
            {
                ColumnaDeModeloBD c = (ColumnaDeModeloBD)t;
                //					cwl("c.Padre="+c.Padre);
                return c.Padre;
            }
            return null;
        }

        //static
        public string getNombreStrModelo(ModeloBD m)
        {
            //if (factory.conservarNombres) {
            //    return m.Nombre;
            //}
            //cwl("m="+m);
            //cwl("m.Nombre="+m.Nombre);
            string r = Utiles.getCapitalizeUnido(m.Nombre.Replace("TABLA_", ""));

            //cwl("capi uni="+r);
            r = Utiles.llevarASingular(r) + this.factory.sufijoModelos;
            //cwl("r="+r);
            return r;//"_MD";
        }

        public string getNombreStrIdkeyModelo(ModeloBD m)
        {
            string prefijo = "idkey_";
            string r = Utiles.llevarASingular(Utiles.getLowerPiso(m.Nombre
                                                              .Replace("TABLA_", prefijo)
            ));
            if (!r.StartsWith(prefijo))
            {
                r = prefijo + r;
            }
            return r;
        }
        public static string getNombreStrColumnaModelo(ModeloBD m, ColumnaDeModeloBD c)
        {
            string r = Utiles.llevarASingular(Utiles.getLowerPiso(c.Nombre
                                                              .Replace("COLUMNA_", "")
                                                              .Replace("ID_TABLA_", "idkey_")
                       ));
            if (c.EsReferencia)
            {
                if (!r.Contains("idkey"))
                {
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

        public static string getNombreStrColumnaModelo(ColumnaDeModeloBD c)
        {
            return getNombreStrColumnaModelo(c.Padre, c);
        }

    }
}
