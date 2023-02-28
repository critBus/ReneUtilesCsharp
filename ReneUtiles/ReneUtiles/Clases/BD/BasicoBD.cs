/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 10/4/2022
 * Hora: 14:12
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles;
using ReneUtiles.Clases;
using System.Text.RegularExpressions;
namespace ReneUtiles.Clases.BD
{
	/// <summary>
	/// Description of BasicoBD.
	/// </summary>
	public class BasicoBD:ConsolaBasica 
	{
		public static readonly Regex PATRON_CONTIENE_TIME=new Regex(@"(\d\d[:]\d\d[:]\d\d)");
		public  int toInt(Object o) {
	        if (o == null || !esNumeroInt(o)) {
	            return -1;
	        }
	        return inT(o);
	    }
	    public  double toDou(Object o) {
	        if (o == null || !esNumeroDou(o)) {
	            return -1;
	        }
	        return dou(o);
	    }
	
	    public  string to_String(Object o) {
	        if (o == null) {
	            return "";
	        }
	        return o.ToString();
	    }
	
	    public  bool toBool(Object o) {
            if (o!=null&&o.ToString().Length==0) {
                return false;
            }
			return Utiles.toBool(o)??false;
//	        if (o == null) {
//	            return false;
//	        }
//	        string a = o.ToString().Trim().ToLower();
//	        if (!Utiles.esBool(a)) {
//	            return false;
//	        }
//	        
//	        return Boolean.Parse(a);
	    }
	
	    public  DateTime toDate(Object o) {
			
	        if (o == null) {
	            return NULL_DATE;
	        }
	
	        if (o is DateTime) {
	            return (DateTime) o;
	        }
	        string a=o.ToString().Trim();
	        if(a.Length==0){
	        	
	            return NULL_DATE;
	        }
	        return getDate(a);
	    }
	    public TimeSpan toTime(Object o) {
	        if(o==null){
	            return NULL_TIME;
	        }
			if (o is TimeSpan) {
	            return (TimeSpan) o;
	        }
			//cwl("t="+o);
			MatchCollection m=PATRON_CONTIENE_TIME.Matches(o+"");
			if(m.Count>0){
				return TimeSpan.Parse(m[0]+"");
			}
			return NULL_TIME;
	        //return TimeSpan.Parse(o+"");
	//        Date d=toDate(o);
	//        return new Time(d.getHours(),d.getMinutes(),d.getSeconds());
	    }
	    public  byte[] toBlob(Object o) {
	        if(o==null){
	            return new byte[0];
	        }
	        //algo para llevar a byte[]
	        return new byte[0];
	    }
	
	    public  string fromBlob(Object o) {
	        if(o is byte[]){
	            //algo
	        }
	        //algo
	        return "";
	    }
	
	
	    public  DateTime getDate(String date) {
			return DateTime.Parse(date);
	        //return SQLUtil.getDate(timestamp);
	    }
	
	
	
	    public  bool esNumeroInt(Object o) {
	        if (o is int) {
	            return true;
	        }
	        return Utiles.esNumeroAll(o.ToString());
	    }
	
	    public  int inT(Object o) {
	        return Utiles.inT(o.ToString());
	    }
	    public  double dou(Object o) {
	        return Utiles.dou(o.ToString());
	    }
	
	    public  bool esNumeroDou(Object o) {
	        if (o is int) {
	            return true;
	        }
	        if (o is float) {
	            return true;
	        }
	        if (o is double) {
	            return true;
	        }
	        return Utiles.esNumeroAll(o.ToString());
	    }
	
	public static bool esArregloString(Object a) {
        
       return a is String[];
    }

    public static bool esArreglo(Object a) {
    	return a is Object[];
    	
    }
		
		
	}
}
