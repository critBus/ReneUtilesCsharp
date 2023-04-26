/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 10/4/2022
 * Hora: 16:38
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.BD
{
	/// <summary>
	/// Description of ModeloDeApiBD.
	/// </summary>
	public class ModeloDeApiBD<E> {
    public int? idkey;
    public E apibd;

    public ModeloDeApiBD(int? idkey, E apibd) {
        this.idkey = idkey;
        this.apibd = apibd;
    }

    public int? getIdkey() {
        return idkey;
    }

    public void setIdkey(int idkey) {
        this.idkey = idkey;
    }

    public E getApibd() {
        return apibd;
    }

    public void setApibd(E apibd) {
        this.apibd = apibd;
    }
}
}
