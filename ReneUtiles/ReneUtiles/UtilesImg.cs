using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReneUtiles.Clases.IMG;
using Delimon.Win32.IO;

namespace ReneUtiles
{
    public abstract class UtilesImg
    {
        public static TipoDeImg getTipoDeImagen(string url)
        {
            string extencion = Archivos.getExtencion(url);
            return TipoDeImg.get(extencion);
        }
        public static bool esImagen(string url) {
            return getTipoDeImagen(url) != null;
        }

        public static int getCantidadDeImagenesExternasEnCarpeta(
            DirectoryInfo carpeta
            ,params TipoDeImg[] formatosPermitidos
            ) {
            int cantidad = 0;
            Archivos.recorrerArchivosExternos(carpeta
               , f =>
               {
                   
                   string url = f.ToString();
                   if (UtilesImg.esImagen(url))
                   {
                       TipoDeImg formato = UtilesImg.getTipoDeImagen(url);
                       if (Utiles.or(formato,
                           formatosPermitidos
                           ))
                       {
                           cantidad++;
                       }
                   }
               });
            return cantidad;
                       }
    }
}
