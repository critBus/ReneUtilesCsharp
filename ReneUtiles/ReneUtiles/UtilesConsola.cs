using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace ReneUtiles
{
    public class UtilesConsola
    {
        //public static void sout(object o)
        //{
        //    Console.WriteLine(o.ToString());
        //}

        public static int getCantidadDeFilasConsola()
        {
            return Console.WindowHeight;
        }
        public static int getCantidadDeColumnasConsola()
        {
            return Console.WindowWidth;
        }
        public static void cwlFull(ConsoleColor c, char a)
        {

            string res = "";
            int total = getCantidadDeColumnasConsola();
            for (int i = 0; i < total; i++)
            {
                res += a;
            }
            cwl(c, res);
        }
        public static string getInputLine()
        {
            return Console.ReadLine();
        }
        public static char getInputChar()
        {
            return Console.ReadKey(true).KeyChar;
        }

        public static void clr()
        {
            Console.Clear();
        }
        public static void beepConsola()
        {
            Console.Beep();
        }
        public static void setColorDeConsola(ConsoleColor c)
        {
            Console.BackgroundColor = c;
        }
        public static void cw(ConsoleColor c, double a)
        {
            cw(c, a + "");
        }
        public static void cw(ConsoleColor c, string a)
        {
            setColorDeLetraConsola(c);
            cw(a);
        }
        public static void setColorDeLetraConsola(ConsoleColor c)
        {
            Console.ForegroundColor = c;
        }

        public static void cw(string a)
        {
            Console.Write(a);
        }
        public static void cwl(ConsoleColor c, char a)
        {
            cwl(c, a + "");
        }
        public static void cwl(ConsoleColor c, string a)
        {
            setColorDeLetraConsola(c);
            cw(a);
        }
        public static void cwl()
        {
            cwl("");
        }
        public static void cwl(object o)
        {
        	if(o==null){
        		Console.WriteLine("nullllllll");
        		return;
        	}
            string[] a={""};
            if(a.GetType()==o.GetType()){
                try
                {
                    Console.WriteLine(Utiles.str((string[])o));
                }
                catch (Exception ex) { Archivos.appenLogExeption(ex); }
                return;
            }
            Console.WriteLine(o.ToString());
        }
        public static void cwStringIndices(int tabulaciones,object a){
        	string b=a.ToString();
        	string r="";
        	string r2="";
        	for (int i = 0; i < b.Length; i++) {
        		char c=b.ElementAt(i);
        		string formato="{0,-"+i.ToString().Length+"} ";
        		
				r += String.Format(formato, c);
				r2 += String.Format(formato, i.ToString());
			}
			if (tabulaciones > 0) {
				r = Utiles.getTabulaciones(tabulaciones) + r;
				r2 = Utiles.getTabulaciones(tabulaciones) + r2;
			}
        	cwl(r);
        	cwl(r2);
        }
        public static void cwStringIndices(object a){
        	
        	cwStringIndices(-1,a);
        }
    }
}
