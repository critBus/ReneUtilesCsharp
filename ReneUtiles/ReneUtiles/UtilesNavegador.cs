using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;

namespace ReneUtiles
{
    public abstract class UtilesNavegador
    {
        private static string URL_EXPLORER=@"C:\Windows\explorer.exe";
        public static void abrirRutaConArchivoSeleccionado(string urlArchivo) {
            Utiles.ejecutarCMD(URL_EXPLORER, "/select,\"" + urlArchivo + "\"");
        }
        public static void abrirArchivo(string urlArchivo)
        {
            Utiles.ejecutarCMD(URL_EXPLORER, "/e,/root,\"" + urlArchivo + "\"");
        }
        public static void abrirCarpeta(string urlCarpeta)
        {
            Utiles.ejecutarCMD(URL_EXPLORER, "\"" + urlCarpeta + "\"");
        }
    }
}
