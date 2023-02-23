using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using ReneUtiles.Clases.Tipos;

namespace ReneUtiles.Clases.IMG
{
    public class TipoDeImg : TipoDeExtencion
    {
        public static TipoDeImg
            PNG = new TipoDeImg(".png", ".pn"),
            JPEG = new TipoDeImg(".jpeg", ".jep"),
            JPG = new TipoDeImg(".jpg", ".jp"),//webp
            WEPP = new TipoDeImg(".webp", ".we")
           ;
        public static TipoDeImg[] VALUES = { PNG, JPEG, JPG, WEPP };
        TipoDeImg(string extencion, string extencionDesactivada) : base(extencion, extencionDesactivada) { }

        public static TipoDeImg get(string extencionActiva)
        {
            extencionActiva = extencionActiva.ToLower();
            foreach (TipoDeImg t in VALUES)
            {
                if (extencionActiva == t.extencion)
                {
                    return t;
                }
            }
            return null;
        }

        public static bool pertenece(string extencionActiva)
        {
            return get(extencionActiva)!=null;
        }
    }
}
