using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Backup2.Utiles;
using System.Collections.Specialized;

namespace ReneUtiles.Clases.Copiador
{
    public class Direcciones_Y_Destino
    {
        public StringCollection sources { get; set; }

        public string destino { get; set; }

        public Direcciones_Y_Destino(string destino,params string[] archivosACopiar) {
            this.destino = destino;
            this.sources = getSC(archivosACopiar);
        }
        public Direcciones_Y_Destino(string destino, StringCollection sources)
        {
            this.destino = destino;
            this.sources = sources;
        }

        public static System.Collections.Specialized.StringCollection getSC(params string[] s)
        {
            System.Collections.Specialized.StringCollection sc = new System.Collections.Specialized.StringCollection();
            sc.AddRange(s);
            return sc;
        }
    }
}
