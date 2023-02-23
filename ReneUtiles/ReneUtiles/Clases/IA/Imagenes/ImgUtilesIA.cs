/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 13/1/2022
 * Time: 11:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ReneUtiles.Clases;

namespace ReneUtiles.Clases.IA.Imagenes
{
	/// <summary>
	/// Description of MetodosComunes.
	/// </summary>
	public abstract class ImgUtilesIA:ConsolaBasica
	{
		public static int filtro1(int[] c1,int[] c0,int[] c_1){
			int s1=0,s0=0,s_1=0;
			for (int i = 0; i < 3; i++) {
				s1+=c1[i];
				s0+=c0[i];
				s_1+=c_1[i];
			}
			int res=s1-s_1;
			if(res<0){
				res=0;
			}
			return res;
		}
		
//		public class FiltroMxN{
//			public int[][] m;
//		}
//		public class PixelMxN{
//			public int[][] m;
//		}
		public static int filtro2(FiltroMxN f,PixelMxN p){
//			int [][]A=f.m;
//			int[][] traspuesta = new int[A[0].Length][];
//			for (int i = 0; i < traspuesta.Length; i++) {
//				traspuesta[i]=new int[A.Length];
//			}
//	        for (int f2 = 0; f2 < A.Length; f2++) {
//				for (int c = 0; c < A[0].Length; c++) {
//	                traspuesta[c][f2] = A[f2][c];
//	            }
//	        }
//			int res=0;
//			for (int i = 0; i < traspuesta.Length; i++) {
//				for (int j = 0; j < traspuesta[i].Length; j++) {
//					//cwl("f*="+(f.m.Length-j-1)+" f="+i+" c*="+(f.m.Length-i-1)+" c="+j);
//					//res+=f.m[f.m.Length-j-1][f.m.Length-i-1]*p.m[i][j];
//					res+=traspuesta[i][j]*p.m[i][j]; MxN-j-1
//				}
//			}
			
//			int res=0,MxN=f.m.Length;
//			for (int i = 0; i < MxN; i++) {
//				for (int j = 0; j < MxN; j++) {
//					int ii=j,ij=i;
////					cwl("f*="+(ii)+" f="+i+" c*="+(ij)+" c="+j+" "+f.m[ii][ij]);
//					res+=f.m[ii][ij]*p.m[i][j];
//				}
//			}
			
//			int res=0,MxN=f.m.Length;
//			for (int i = 0; i < MxN; i++) {
//				for (int j = 0; j < MxN; j++) {
//					int ii=MxN-j-1,ij=i;
////					cwl("f*="+(ii)+" f="+i+" c*="+(ij)+" c="+j+" "+f.m[ii][ij]);
//					res+=f.m[ii][ij]*p.m[i][j];
//				}
//			}
//			int res=0,MxN=f.m.Length;
//			for (int i = 0; i < MxN; i++) {
//				for (int j = 0; j < MxN; j++) {
//					int ii=MxN-j-1,ij=MxN-i-1;
////					cwl("f*="+(ii)+" f="+i+" c*="+(ij)+" c="+j+" "+f.m[ii][ij]);
//					res+=f.m[ii][ij]*p.m[i][j];
//				}
//			}
//			int res=0,MxN=f.m.Length;
//			for (int i = 0; i < MxN; i++) {
//				for (int j = 0; j < MxN; j++) {
//					int ii=MxN-i-1,ij=MxN-j-1;
////					cwl("f*="+(ii)+" f="+i+" c*="+(ij)+" c="+j+" "+f.m[ii][ij]);
//					res+=f.m[ii][ij]*p.m[i][j];
//				}
//			}

int res=0,MxN=f.m.Length;
			for (int i = 0; i < MxN; i++) {
				for (int j = 0; j < MxN; j++) {
					int ii=i,ij=j;
//					cwl("f*="+(ii)+" f="+i+" c*="+(ij)+" c="+j+" "+f.m[ii][ij]);
					res+=f.m[ii][ij]*p.m[i][j];
				}
			}
			//return res<0?0:res;
			return res;
		}
		
	}
}
