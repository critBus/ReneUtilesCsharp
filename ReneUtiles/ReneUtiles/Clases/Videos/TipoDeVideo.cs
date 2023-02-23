using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using ReneUtiles.Clases.Tipos;

namespace ReneUtiles.Clases.Videos
{
    public class TipoDeVideo : TipoDeExtencion
    {
       public readonly static TipoDeVideo
            MPG = new TipoDeVideo(".mpg", ".mp g"),
            MPEG = new TipoDeVideo(".mpeg", ".mpe"),
            MP4 = new TipoDeVideo(".mp4", ".mp"),
            RMVB = new TipoDeVideo(".rmvb", ".r"),
            GP3 = new TipoDeVideo(".3gp", ".3g"),
            AVI = new TipoDeVideo(".avi", ".av"),
            WMV = new TipoDeVideo(".wmv", ".w"),
            VOB = new TipoDeVideo(".vob", ".vo"),
            MOV = new TipoDeVideo(".mov", ".mo"),
            FLV = new TipoDeVideo(".flv", ".f"),
            RM = new TipoDeVideo(".rm", ".r m"),
            WEBM = new TipoDeVideo(".webm", ".web"),
            TS = new TipoDeVideo(".ts", ".t"),
            MKV = new TipoDeVideo(".mkv", ".m");
       public readonly static TipoDeVideo[] VALUES = { MPG, MPEG, MP4, RMVB, GP3, AVI, WMV, VOB, MOV, FLV, RM, WEBM, TS, MKV };
        TipoDeVideo(string extencion, string extencionDesactivada) : base(extencion, extencionDesactivada) { }
        
        public static bool esDelTipoVideo(string  nombre){
        	return esDelTipoDeExtencion(VALUES,nombre);
        }
        
        
        public static TipoDeVideo get(object tipo){
			if(tipo==null){
				return null;
			}
            string s = tipo.ToString().ToLower();

            foreach (TipoDeVideo t in VALUES) {
				if(t.extencion==s){ 
					return t;
				}
			}
			return null;
		}
    }
}
