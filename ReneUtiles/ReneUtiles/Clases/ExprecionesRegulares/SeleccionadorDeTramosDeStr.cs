using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.ExprecionesRegulares
{
    public class SeleccionadorDeTramosDeStr
    {
        public string nombreARecortar;
        public List<LimitesDeIndice> tramosAQuitar = new List<LimitesDeIndice>();
        public void addLimites(int i0, int i)
        {
            if (i0 != i && i0 >= 0 && i <= nombreARecortar.Length)
            {
                LimitesDeIndice l = new LimitesDeIndice();
                l.inicial = i0;
                l.final = i;
                tramosAQuitar.Add(l);
            }

        }
        public string getStrRecortado()
        {
            string r = "";
            for (int i = 0; i < nombreARecortar.Length; i++)
            {
                bool saltar = false;
                foreach (LimitesDeIndice lim in tramosAQuitar)
                {
                    if (i >= lim.inicial && i <= lim.final)
                    {
                        i = lim.final;
                        saltar = true;
                        break;
                    }
                }
                if (saltar)
                {
                    continue;
                }
                r += nombreARecortar.ElementAt(i);
            }
            return r;
        }

    }
}
