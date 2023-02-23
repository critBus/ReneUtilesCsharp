/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 9/4/2022
 * Hora: 19:05
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.BD;
using System.Collections.Generic;
using ReneUtiles;
namespace ReneUtiles.Clases.BD.Conexion
{
	/// <summary>
	/// Description of BDResultadoInsertar.
	/// </summary>
	public class BDResultadoInsertar:ReneBasico
	{
		public class BDDatosColumnaInsertada {

        public TipoDeDatoSQL tipoDeColumna;  
        public Object valor;

        public BDDatosColumnaInsertada(TipoDeDatoSQL tipoDeColumna, Object valor) {
            this.tipoDeColumna = tipoDeColumna;
            this.valor = valor;
        }

        public TipoDeDatoSQL getTipoDeColumna() {
            return tipoDeColumna;
        }

        public Object getValor() {
            return valor;
        }

    }
    public bool esID_Numero_Autoincrementado;
    public int id;
    public String nombreColumnaID;

    private List<BDDatosColumnaInsertada> columnasInsertadas;

    public BDResultadoInsertar() {
        columnasInsertadas = new List<BDDatosColumnaInsertada>();
        this.esID_Numero_Autoincrementado = false;
    }

    public BDDatosColumnaInsertada add(TipoDeDatoSQL tipoDeColumna, Object valor) {
        BDDatosColumnaInsertada d = new BDDatosColumnaInsertada(tipoDeColumna, valor);
        columnasInsertadas.Add(d);
        comprobarSiEsIDAutomatico();
        return d;
    }

    private void comprobarSiEsIDAutomatico() {
        if (columnasInsertadas.Count == 1) {
    		BDDatosColumnaInsertada d = columnasInsertadas[0];
            if (d.tipoDeColumna == TipoDeDatoSQL.INTEGER) {
                setID_Autoincrementado(d.getValor());
                return;
            }

        }
        this.esID_Numero_Autoincrementado = false;

    }

    public void setID_Autoincrementado(Object o) {
        if (esNumeroAll(o + "")) {
            this.id = inT(o + "");
            this.nombreColumnaID = "id";
            this.esID_Numero_Autoincrementado = true;
        }
    }

//    public bool esID_Numero_Autoincrementado() {
//        return esID_Numero_Autoincrementado;
//    }

    public int getId() {
        return id;
    }

    public String getNombreColumnaID() {
        return nombreColumnaID;
    }

    public int getCantidadDeColumnasKey() {
        return columnasInsertadas.Count;
    }

    public BDResultadoInsertar.BDDatosColumnaInsertada get(int i) {
	return columnasInsertadas[i];
    }

    public bool isEmpty() {
        return columnasInsertadas.Count==0;
    }

}

}
