using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;

namespace ReneUtiles.Clases.Copiador
{
    public abstract class MangerCopiador:ConsolaBasica
    {
        public abstract void addDirecciones(params Direcciones_Y_Destino[] direcciones);
    }
}
