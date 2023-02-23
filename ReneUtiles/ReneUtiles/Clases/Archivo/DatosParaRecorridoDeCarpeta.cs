using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;

namespace ReneUtiles.Clases.Archivo
{
   public class DatosParaRecorridoDeCarpeta
    {
        DirectoryInfo di;
        bool recorrerCarpetasInternas;
        public DatosParaRecorridoDeCarpeta() { }
        public DatosParaRecorridoDeCarpeta(DirectoryInfo di, bool recorrerCarpetasInternas)
        {
            actualizar(di, recorrerCarpetasInternas);
            //this.di = di;
            //this.recorrerCarpetasInternas = recorrerCarpetasInternas;
        }

        public void actualizar(DirectoryInfo di, bool recorrerCarpetasInternas)
        {
            this.di = di;
            this.recorrerCarpetasInternas = recorrerCarpetasInternas;
        }

        public bool RecorrerCarpetasInternas
        {
            get { return recorrerCarpetasInternas; }
            set { recorrerCarpetasInternas = value; }
        }
        public DirectoryInfo Carpeta
        {
            get { return di; }
            set { di = value; }
        }
    }
}
