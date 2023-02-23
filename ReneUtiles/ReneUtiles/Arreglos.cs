/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 23/9/2021
 * Time: 17:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace ReneUtiles
{
	/// <summary>
	/// Description of Arreglos.
	/// </summary>
	public abstract class Arreglos
	{
		public static E[] unir<E>(params E[][] Arreglo){
			List<E> l=null;
			int end=Arreglo.Length;
			for (int i = 0; i < end; i++) {
				if (i==0) {
					l=new List<E>(Arreglo[i]);
					continue;
				}
				l.AddRange(Arreglo[i]);
				
			}
			return l.ToArray();
		}
		
		public static E[] fusionar<E>(params E[][] Arreglo){
			List<E> l=null;
			int end=Arreglo.Length;
			for (int i = 0; i < end; i++) {
				if (i==0) {
					l=new List<E>(Arreglo[i]);
					continue;
				}
				int lengInterno=Arreglo[i].Length;
				for (int j = 0; j < lengInterno; j++) {
					if (!l.Contains(Arreglo[i][j])) {
						l.Add(Arreglo[i][j]);
					}
				}
				
			}
			return l.ToArray();
		}
		
		public static  E[] colocarDeUltimoObject<E>(E []A, E a) {
			List<E> l=new List<E>(A);
			l.Add(a);
			return l.ToArray();
//        ArrayList U = new ArrayList(Arrays.asList(A));
//        U.add(a);
//        // A=(E[]) U.toArray(A);
//        return (E[]) U.toArray(A);
    	}
	}
}
