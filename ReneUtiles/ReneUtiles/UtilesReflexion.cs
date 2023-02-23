using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReneUtiles
{
    public abstract class UtilesReflexion
    {
        public static E getValuePropiedad<T,E>(T elemento,string nombrePropiedad) {
            PropertyInfo[] properties = elemento.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                if (p.Name== nombrePropiedad) { return (E)p.GetValue(elemento); }

            }
            return default(E);
        }
        public static void setValuePropiedad<T,E>(T elemento, string nombrePropiedad,E valor)
        {
            PropertyInfo[] properties = elemento.GetType().GetProperties();
            foreach (PropertyInfo p in properties)
            {
                if (p.Name == nombrePropiedad) { p.SetValue(elemento,valor); break;  }

            }
           
        }
    }
}
