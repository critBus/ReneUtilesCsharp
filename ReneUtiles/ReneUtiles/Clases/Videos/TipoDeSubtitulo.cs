/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 9/1/2022
 * Time: 13:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ReneUtiles.Clases.Tipos; 

namespace ReneUtiles.Clases.Videos
{
	/// <summary>
	/// Description of TipoDeSubtitulo.
	/// </summary>
	public class TipoDeSubtitulo: TipoDeExtencion
    {
       public static TipoDeSubtitulo
            SRT = new TipoDeSubtitulo(".srt", ".sr t"),
            ASS = new TipoDeSubtitulo(".ass", ".as s"),
            SSA = new TipoDeSubtitulo(".ssa", ".ss a"),
            SUB = new TipoDeSubtitulo(".sub", ".su b"),
            ES_LATSRT = new TipoDeSubtitulo(".es-latsrt", ".es-latsr t"),
            ES_ESSRT = new TipoDeSubtitulo(".es-essrt", ".es-essr t");
       public static TipoDeSubtitulo[] VALUES = { SRT, ASS, SSA, SUB, ES_LATSRT, ES_ESSRT};
        TipoDeSubtitulo(string extencion, string extencionDesactivada) : base(extencion, extencionDesactivada) { }
        
        public static bool esDelTipoDeSubtitulo(string  nombre){
        	return esDelTipoDeExtencion(VALUES,nombre);
        }
    }
}