using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReneUtiles.Clases;
using ReneUtiles.Clases.BD;
using ReneUtiles.Clases.BD.Conexion;

using ReneUtiles;
namespace ReneUtiles.Clases.BD.Factory
{
    public class DatosDeConexionFactoryBD
    {
        public string NombreBDAdmin;//nombre clase bd 
        //public TipoDeConexionBD TipoDeConexion;
        public DatosBDConect datosDBConect;

        public DatosDeBDConect datosDelCodigoDelBDConector;

        public DatosDeConexionFactoryBD(string nombreBDAdmin, DatosBDConect datosDBConect)
        {
            NombreBDAdmin = nombreBDAdmin;
            //TipoDeConexion = tipoDeConexion;
            this.datosDBConect = datosDBConect;

            this.datosDelCodigoDelBDConector = new DatosDeBDConect();
        }
    }
}
