using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using ReneUtiles.Clases.BD;
using ReneUtiles;
using Npgsql;
using System.Data.Common;

namespace ReneUtiles.Clases.BD.Conexion
{
    public class GestorDeConexion_Postgresql : GestorDeConexionImple
    {
        public GestorDeConexion_Postgresql(DatosBDConect datosBDConect):base(datosBDConect,new SQLUtiles_Postgres())
		{
            datosBDConect.url_basesDeDatos = "Server="+ datosBDConect.servidor+ ";Port="+ datosBDConect.puerto+ "; User Id="+ datosBDConect.usuario+ ";Password="+ datosBDConect.contraseña+ ";Database ="+ datosBDConect.nombreBD;
            
            datosBDConect.tipoDeConxion = TipoDeConexionBD.POSTGRES;
        }

        public override DbConnection crearConexion()
        {

            NpgsqlConnection conn = new NpgsqlConnection(datosBDConect.url_basesDeDatos);
            conn.Open();

            return conn;
        }

        protected override BDResultadoInsertar ejecutarConsultaInsertar()
        {
            SQLUtiles_Postgres sqlu = (SQLUtiles_Postgres)this.sqlUtiles;
            string sql = datosDeConexion.Cmd.CommandText;
            if (sqlu.esRETURNING(sql))
            {
                datosDeConexion.R = datosDeConexion.Cmd.ExecuteScalar();
                BDResultadoInsertar r = new BDResultadoInsertar();
                r.add(TipoDeDatoSQL.INTEGER, datosDeConexion.R);

                return r;
            }
            else {
                datosDeConexion.Cmd.ExecuteNonQuery();
            }
            
            return null;
        }
    }
}
