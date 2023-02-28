/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/4/2022
 * Hora: 12:16
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Delimon.Win32.IO;
using System;
//using System.IO;

namespace ReneUtiles.Clases.BD.Conexion
{
	/// <summary>
	/// Description of DatosBDConect.
	/// </summary>
	public class DatosBDConect
	{
		public string controlador, url_basesDeDatos, usuario, contraseña, servidor, nombreBD, puerto, ultimuSQl;
	    public TipoDeConexionBD tipoDeConxion;
	    public FileInfo url;
	
	    public Object resultado;
	    public bool mostrarResultadoConsola = false;
        public bool mostrarSQL = true;
		public DatosBDConect()
		{
		}
		
		public DatosBDConect(string controlador, string url_basesDeDatos, string usuario, string contraseña, string servidor, string nombreBD, string puerto, string ultimuSQl, TipoDeConexionBD tipoDeConxion, FileInfo url, Object resultado, bool mostrarResultadoConsola) {
	        this.controlador = controlador;
	        this.url_basesDeDatos = url_basesDeDatos;
	        this.usuario = usuario;
	        this.contraseña = contraseña;
	        this.servidor = servidor;
	        this.nombreBD = nombreBD;
	        this.puerto = puerto;
	        this.ultimuSQl = ultimuSQl;
	        this.tipoDeConxion = tipoDeConxion;
	        this.url = url;
	        this.resultado = resultado;
	        this.mostrarResultadoConsola = mostrarResultadoConsola;
    	}
	}
}
