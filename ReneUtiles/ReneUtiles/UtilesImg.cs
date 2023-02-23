using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReneUtiles.Clases.IMG;

namespace ReneUtiles
{
    abstract class UtilesImg
    {
        public static TipoDeImg getTipoDeImagen(string url)
        {
            string extencion = Archivos.getExtencion(url);
            return TipoDeImg.get(extencion);
        }
        public static bool esImagen(string url) {
            return getTipoDeImagen(url) != null;
        }
    }
}
