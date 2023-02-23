using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using ReneUtiles;

namespace ReneUtiles.Clases
{
    public class ConsolaBasica:ReneBasico
    {
        public static void cwl(object o)
        {
            UtilesConsola.cwl(o);
        }
        public static void cwl(){
        	UtilesConsola.cwl();
        }

        public static void endC() {
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}
