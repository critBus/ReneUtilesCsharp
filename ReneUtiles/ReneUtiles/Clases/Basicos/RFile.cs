using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using ReneUtiles;

namespace ReneUtiles.Clases.Basicos
{
    public class RFile
    {
        string carpeta, nombre;
        public RFile(string url) {
            string carpeta="", nombre=url;
            if (Utiles.containsOR(url, "/","\\")) {
                int indice = Utiles.indexOF_OR(url, "/","\\");
                nombre=url.Substring(indice);
                carpeta = url.Substring(0, indice);
            }
            init(carpeta, nombre);
        }
        public RFile(string carpeta, string nombre) {
            init(carpeta, nombre);
        }
        private void init(string carpeta, string nombre)
        {
            this.carpeta = carpeta;
            this.nombre = nombre;
        }
        public string getURL(){
            return carpeta.Length!=0? carpeta + "/" + nombre:nombre;
        }

        public string Carpeta {
            get { return carpeta; }
            set { this.carpeta = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { this.nombre = value; }
        }

        

        
    }
}
