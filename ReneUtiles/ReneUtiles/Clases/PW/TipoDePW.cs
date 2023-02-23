using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using ReneUtiles.Clases.Tipos;

namespace ReneUtiles.Clases.PW
{
    public class TipoDePW : TipoDeExtencion

    {
        public static TipoDePW
            HTML = new TipoDePW(".html", ".h___"),
            MHTML = new TipoDePW(".mhtml", ".mh___"),
            XHTML = new TipoDePW(".xhtml", ".xh___");
        public static TipoDePW[] VALUES = { HTML, MHTML, XHTML};
        TipoDePW(string extencion, string extencionDesactivada) : base(extencion, extencionDesactivada) { }
    }
}
