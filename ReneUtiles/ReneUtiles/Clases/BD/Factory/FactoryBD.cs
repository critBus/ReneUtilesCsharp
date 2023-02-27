/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 12/12/2021
 * Time: 21:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Linq;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
//using System.Threading.Tasks;
//using System.IO;
using ReneUtiles.Clases.Tipos;
using System.Text.RegularExpressions;

using ReneUtiles.Clases;
using ReneUtiles.Clases.BD;
using ReneUtiles.Clases.BD.Factory.Consultas;
using ReneUtiles.Clases.BD.Factory.Codes.CSharp;
using ReneUtiles.Clases.BD.Factory.Codes.Python;
using ReneUtiles.Clases.BD.Factory.Codes.Java;
using ReneUtiles.Clases.BD.Factory.Codes.Java.Android;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;

using Delimon.Win32.IO;

using ReneUtiles.Clases.BD.Conexion;

namespace ReneUtiles.Clases.BD.Factory
{
	/// <summary>
	/// Description of FactoryBD.
	/// </summary>
	public class FactoryBD:BasicoFactory
	{
		//public string NombreBDAdmin;//nombre clase bd 
		//public TipoDeConexionBD TipoDeConexion;
		//public string DireccionBDSqlite;
		public string NombreArchivoQueContieneTodosLosModelos;
		
		
		public EsquemaBD Esquema;
		
		public string DireccionPaquete;
        public string NombreClaseBDPadre;

        public List<DatosDeConexionFactoryBD> listaDatosDeConexiones;


        public string sufijoModelos;


        public FactoryBD(EsquemaBD esquema)
		{
				this.Esquema=esquema;
            this.listaDatosDeConexiones = new List<DatosDeConexionFactoryBD>();
            this.NombreClaseBDPadre = "I_BDAdmin";

            this.sufijoModelos = "_MD";
            //this.NombreBDAdmin="BDAdmin";
            //this.NombreArchivoQueContieneTodosLosModelos="ModelosDeBD";
        }

        public DatosDeConexionFactoryBD addConexion_SQLite(
            string nombreBDAdmin
            ,string direccionBDSqlite) {

            DatosBDConect bdc = new DatosBDConect();
            TipoDeConexionBD tipoDeConexion = TipoDeConexionBD.SQL_LITE;
            bdc.controlador= tipoDeConexion.driver_java;
            bdc.url = new FileInfo(direccionBDSqlite);
            bdc.nombreBD = bdc.url.Name;
            bdc.tipoDeConxion = tipoDeConexion;

            DatosDeConexionFactoryBD dc = new DatosDeConexionFactoryBD(
                nombreBDAdmin: nombreBDAdmin
                //, tipoDeConexion: tipoDeConexion
                , datosDBConect: bdc
                );
            listaDatosDeConexiones.Add(dc);

            return dc;
        }
        public DatosDeConexionFactoryBD addConexion_POSTGRES(
            string nombreBDAdmin
            ,string nombreBD, string usuario = "postgres", string contrasenna = "postgres", string servidor = "localhost", string puerto = "5432")
        {
            TipoDeConexionBD tipoDeConexion = TipoDeConexionBD.POSTGRES;
            string controlador = tipoDeConexion.driver_java;
            DatosBDConect bdc = new DatosBDConect(controlador, null, usuario, contrasenna, servidor, nombreBD, puerto, "", tipoDeConexion, null, null, false);

            DatosDeConexionFactoryBD dc = new DatosDeConexionFactoryBD(
                nombreBDAdmin: nombreBDAdmin
                //, tipoDeConexion: tipoDeConexion
                , datosDBConect: bdc
                );
            listaDatosDeConexiones.Add(dc);

            return dc;
        }

        private CodeBDLenguaje comprobarSeguridad(Func<DatosDeConexionFactoryBD, CodeBDLenguaje> getC)
        {
            if (listaDatosDeConexiones.Count==0) {
                throw new Exception("Tienen que existir los datos de almenos una conexion");
            }
            CodeBDLenguaje c = getC(listaDatosDeConexiones[0]);
            if (c is CodeBDPython) {
                if (this.NombreArchivoQueContieneTodosLosModelos==null
                    ||this.NombreArchivoQueContieneTodosLosModelos.Length==0) {
                    throw new Exception("La configuracion tiene que estar completa:"
                        +"\nSi se usa python hay que expecificar el nombre del archivo que contendra los modelos");
                }
            }
            
            for (int i = 0; i < listaDatosDeConexiones.Count; i++)
            {
                DatosDeConexionFactoryBD d = listaDatosDeConexiones[i];

                string NombreBDAdmin = d.NombreBDAdmin;
                TipoDeConexionBD t = d.datosDBConect.tipoDeConxion;
                if (isNullOr(NombreBDAdmin, t, DireccionPaquete)//
               || isEmptyFullOr(NombreBDAdmin, DireccionPaquete)

               )
                {
                    throw new Exception("La configuracion tiene que estar completa");
                }

                string nombreAdminAComparar = d.NombreBDAdmin.ToLower();

                for (int j = i+1; j < listaDatosDeConexiones.Count; j++)
                {
                    DatosDeConexionFactoryBD d2 = listaDatosDeConexiones[j];
                    if (nombreAdminAComparar == d2.NombreBDAdmin.ToLower()) {
                        throw new Exception("No pueden aver dos nombre admin iguales:"
                            +"\nCaso: "+ nombreAdminAComparar);
                    }
                }

                if (c is CodeBDLenguaje_ConIAdmin)
                {
                    if (nombreAdminAComparar==this.NombreClaseBDPadre) {
                        throw new Exception("No pueden aver dos nombre admin iguales:"
                            + "\nCaso del la Interfas admin padre"
                            + "\nCaso: " + nombreAdminAComparar);
                    }
                }
                }
			
			Esquema.comprobarSeguridad();

            return c;
		}
		public List<FileInfo> crearCodigoCSharp(DirectoryInfo urlCarpeta)
		{
			return crearCodigo(urlCarpeta, d => new CodeBDCSharp(this,d));
		}
		public List<FileInfo> crearCodigoPython(DirectoryInfo urlCarpeta)
		{
			
			return crearCodigo(urlCarpeta, d => new CodeBDPython(this,d));
		}
		public List<FileInfo> crearCodigoJava(DirectoryInfo urlCarpeta)
		{
			return crearCodigo(urlCarpeta,d=>new CodeBDJava(this,d));
		}
		public List<FileInfo> crearCodigoAndroid(bool bdIneterna,DirectoryInfo urlCarpeta)
		{
			
			return crearCodigo(urlCarpeta, d => {
                CodeBDAndroid c = new CodeBDAndroid(this,d);
                c.EsBDInterna = bdIneterna;
                return c;
            });
		}
		
		private List<FileInfo> crearCodigo(DirectoryInfo urlCarpeta,Func<DatosDeConexionFactoryBD,CodeBDLenguaje> getC){
            

            Esquema.prepararAntesDeCrearCodigo();

            CodeBDLenguaje c = comprobarSeguridad(getC);

			List<FileInfo> archivosCreados=new List<FileInfo>();
			if(c.UsaUnArchivoParaTodosLosModelosJuntos){
					string codigo=c.getStrArchivoTodosLosModelosJuntos(0);
					FileInfo f=Archivos.crearTEXTO(carpeta:urlCarpeta,nombre:this.NombreArchivoQueContieneTodosLosModelos,extencion_Sustituye:c.Extencion,lineas:codigo);
					archivosCreados.Add(f);
			}else{
				for (int i = 0; i < Esquema.getCantidadDeModelos(); i++) {
					ModeloBD m=Esquema.getModelo(i);
					string codigo=c.getStrModelo(m,Esquema,0);
					FileInfo f=Archivos.crearTEXTO(carpeta:urlCarpeta,nombre:CodeBDLenguaje.getNombreStrModelo(m),extencion_Sustituye:c.Extencion,lineas:codigo);
					archivosCreados.Add(f);
				}
			}
            if (c is CodeBDLenguaje_ConIAdmin) {
                CodeBDLenguaje_ConIAdmin cle = (CodeBDLenguaje_ConIAdmin)c;
                FileInfo bd = Archivos.crearTEXTO(carpeta: urlCarpeta, nombre: this.NombreClaseBDPadre, extencion_Sustituye: c.Extencion, lineas: cle.getStrBD_IAdminPadre(0));
                archivosCreados.Add(bd);
            }
            for (int i = 0; i < listaDatosDeConexiones.Count; i++)
            {
                DatosDeConexionFactoryBD dcx = listaDatosDeConexiones[i];
                if (i!=0) {
                    c = getC(dcx);
                }
                FileInfo bd = Archivos.crearTEXTO(carpeta: urlCarpeta, nombre: dcx.NombreBDAdmin, extencion_Sustituye: c.Extencion, lineas: c.getStrBD(0));
                archivosCreados.Add(bd);
            }


                
			
			return archivosCreados;
		}
		
		
		
		
		
		
	}
}
