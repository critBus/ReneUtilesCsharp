using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles.Clases.Tipos;
using System.Text.RegularExpressions;

using ReneUtiles.Clases;
using ReneUtiles.Clases.BD;
using Delimon.Win32.IO;

using ReneUtiles.Clases.BD.Conexion;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
namespace ReneUtiles.Clases.BD.Factory.Framewors
{
    public class FactoryFrameworkBD: BasicoFactory
    {
        EsquemaBD esquema;
        public string sufijoModelos="";

    }
}
