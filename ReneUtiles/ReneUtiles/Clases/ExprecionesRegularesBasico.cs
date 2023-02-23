/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 25/7/2022
 * Hora: 09:58
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
//using ReneUtiles.Clases;
using System.Text.RegularExpressions;
using ReneUtiles;
namespace ReneUtiles.Clases
{
	/// <summary>
	/// Description of ExprecionesRegularesBasico.
	/// </summary>
	public class ExprecionesRegularesBasico:ConsolaBasica
	{
		public string grupoNombrado(string key,string expre){
			return ConstantesExprecionesRegulares.getGrupoNombrado(key,expre);
		}
		public  int inT_Grp(Group g){
			return inT(g.ToString());
		}
		public  int inT_Cap(Capture g){
			return inT(g.ToString());
		}
		
		public string repetirGrupo(string key){
			//return @"\k<"+key+@">";
			return Matchs.repetirGrupo(key);
		}
		
		public string unoAlMenos(string expre){
			//return "(?:"+expre+")+";
			return Matchs.unoAlMenos(expre);
		}
		public string orExpr(params  string[] Exprs){
			return Matchs.orExpr(Exprs);
//			string r=@"(?:";
//			for (int i = 0; i < Exprs.Length; i++) {
//				r+=(i!=0?@"|":"")+Exprs[i];
//			}
//			return r+@")";
		}
		public Regex[] arrgR(params Regex[] Rs){
			return Rs;
		}
		
		
	}
}
