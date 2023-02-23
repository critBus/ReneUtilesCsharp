using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReneUtiles;
using ReneUtiles.Clases;
using ReneUtiles.Clases.IMG;

namespace ReneUtiles.Clases.Validacion.TipoDeValidaciones
{
    class TipoDeValidacion:ConsolaBasica
    {
        public Predicate<object> esValido;
        public Func<string> getMensaje;

        public static readonly TipoDeValidacion NO_NULL = new TipoDeValidacion(
            v=>v!=null
            , "No puede estar vacío "
            );
        public static readonly TipoDeValidacion STR_NO_EMPTY = new TipoDeValidacion(
            v => NO_NULL.esValido(v.ToString()) && !String.IsNullOrWhiteSpace(v.ToString())
            , "No puede estar vacío "
            );

        public static readonly TipoDeValidacion STR_CON_ALFANUMERICOS = new TipoDeValidacion(
            v => STR_NO_EMPTY.esValido(v) && Matchs.hayMatch(ConstantesExprecionesRegulares.PATRON_CONTIENE_LETRAS,v.ToString())
            , "Debe de contener letras"
            );

        public static readonly TipoDeValidacion SOLO_INT_POSITIVO_STR = new TipoDeValidacion(
            v => STR_NO_EMPTY.esValido(v.ToString()) && esNumero(v.ToString())
            , "Debe ser un numero entero positivo"
            );

        public static readonly TipoDeValidacion SOLO_FLOAT_POSITIVO_STR = new TipoDeValidacion(
            v => STR_NO_EMPTY.esValido(v) && esDouble(v.ToString())
            , "Debe ser un numero positivo cuyo indicador decimal sea un ‘.’"
            );

        public static readonly TipoDeValidacion STR_CORREO = new TipoDeValidacion(
            v => STR_NO_EMPTY.esValido(v)
                                            && Matchs.hayMatch(ConstantesExprecionesRegulares.PATRON_CORREO, v.ToString())
            , "Correo Incorrecto"
            );

        public static readonly TipoDeValidacion STR_SOLO_LETRAS = new TipoDeValidacion(
            v => STR_NO_EMPTY.esValido(v)
                                            && Matchs.hayMatch(ConstantesExprecionesRegulares.PATRON_SOLO_LETRAS, v.ToString())
            , "Solo debe de contener letras y espacios "
            );

        public static readonly TipoDeValidacion STR_SOLO_LETRAS_Y_NUMEROS = new TipoDeValidacion(
            v => STR_NO_EMPTY.esValido(v)
                                            && Matchs.hayMatch(ConstantesExprecionesRegulares.PATRON_SOLO_LETRAS_Y_NUMEROS, v.ToString())
            , "Solo debe de contener letras, numeros y espacios "
            );

        public static readonly TipoDeValidacion STR_SOLO_ALFANUMERICOS = new TipoDeValidacion(
            v => STR_NO_EMPTY.esValido(v)
                                            && Matchs.hayMatch(ConstantesExprecionesRegulares.PATRON_SOLO_ALFANUMERICOS, v.ToString())
            , "Solo debe de contener letras, numeros, espacios y '_'"
            );
        public static readonly TipoDeValidacion STR_SEGURIDAD_MINIMA_CONTRASEÑA = new TipoDeValidacion(
            v => (STR_NO_EMPTY.esValido(v.ToString()) && v.ToString().Length > 7) ? (hayMatch(ConstantesExprecionesRegulares.PATRON_CONTIENE_LETRAS, v.ToString()) && hayMatch(ConstantesExprecionesRegulares.PATRON_TIENE_NUMEROS, v.ToString())):false
             , "Debe de contener letras, numeros, y al menos 8 caracteres"
            );

       
        public static readonly TipoDeValidacion STR_ES_DIRECCION_FORMATO_IMAGEN = new TipoDeValidacion(
            v => UtilesImg.esImagen(v.ToString())
            , "Debe de seleccionar algun archivo de tipo imagen"
            );

        public static readonly TipoDeValidacion STR_ES_DIRECCION_FORMATO_IMAGEN_JPG_JPEG_PNG = new TipoDeValidacion(
            v => STR_ES_DIRECCION_FORMATO_IMAGEN.esValido(v)&& (UtilesImg.getTipoDeImagen(v.ToString()) == TipoDeImg.JPG
                                                                                || UtilesImg.getTipoDeImagen(v.ToString()) == TipoDeImg.JPEG
                                                                                || UtilesImg.getTipoDeImagen(v.ToString()) == TipoDeImg.PNG)
            , "Debe de seleccionar algun archivo de tipo imagen con formato .jpg .jpeg .png"
            );


        public TipoDeValidacion(Predicate<object> esValido, Func<string> getMensaje)
        {
            this.esValido = esValido;
            this.getMensaje = getMensaje;
        }
        public TipoDeValidacion(Predicate<object> esValido, string mensaje)
        {
            this.esValido = esValido;
            setMensaje(mensaje);
        }
        public TipoDeValidacion setMensaje(string mensaje) {
            this.getMensaje = () =>mensaje;
            return this;
        }
    }
}
