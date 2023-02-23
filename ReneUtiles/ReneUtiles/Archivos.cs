using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

//using System.IO;
using Delimon.Win32.IO;


using System.Runtime.Serialization.Formatters.Binary;
using ReneUtiles.Clases;
using ReneUtiles.Clases.Almacenamiento;
//379

namespace ReneUtiles
{
	public abstract class Archivos :ConsolaBasica
	{
		public static readonly char[] caracteresNoPermitidosNombre = {  '*', '?', '\"', '<', '>', '|', '\\', '/', ':' };

        public static EspacioEnAlmacenamiento getTipoDeEspacioDisponible(string url) {
            
            string discoDestino = Utiles.subs(url, 0, 3);
            System.IO.DriveInfo[] allDrives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo d in allDrives)
            {
                if (d.Name == discoDestino)
                {

                    return new EspacioEnAlmacenamiento(d.AvailableFreeSpace);
                }
            }
            return null;
        }


        public static double getEspacioQueOcupa(DirectoryInfo d) {
            return new System.IO.DirectoryInfo(d.ToString()).GetFiles("*", System.IO.SearchOption.AllDirectories).Sum(f => f.Length);
        }
        public static double getEspacioQueOcupa(FileInfo d)
        {
            return d.Length;
        }
        public static FileSystemInfo getFileSystemCorrecto(string url) {
            if (esArchivo(url)) {
                return new FileInfo(url);
            }
            return new DirectoryInfo(url);
        }

        public static void sustituirLineasEnArchivo(FileInfo f,  Func<string[],string,int,string> leerLinea,Func<List<string>,List<string>> modificarLuegoDeSustituir=null){
			string []lineas=leer(f);
			lineas=recorrerYSustituirTEXTO(f,leerLinea);
            System.IO.File.CreateText(f.ToString()).Close();
			lineas=modificarLuegoDeSustituir(new List<string>(lineas)).ToArray();
            System.IO.File.WriteAllLines(f.ToString(), lineas);
			
		}
		
		public static string[] recorrerYSustituirTEXTO(FileInfo f, Func<string[],string,int,string> leerLinea)
		{
			
			string[] lineas = leer(f);
			for (int i = 0; i < lineas.Length; i++) {
				lineas[i]=leerLinea(lineas,lineas[i], i);
			}
			return lineas;
		}
		
		public static FileInfo crearTEXTO(DirectoryInfo carpeta, string nombre, string extencion_Sustituye, params string[] lineas)
		{
			if (carpeta.Exists) {
				string url = setExtencionStr(carpeta, nombre, extencion_Sustituye);
                bool tieneCaracteresNoPermitidosAqui = tieneCarateresNoPermitidos(url);
                char? c = getCaraterNoPermitidoQueTiene(url);

                //File.CreateText(url); //.Close();
				FileInfo f = new FileInfo(url);
				if (lineas.Length > 0) {
					System.IO.File.WriteAllLines(f.ToString(), lineas);
				}
				return f;
			}
			return null;
		}
		
		
		public static void recorrerTEXTO(FileInfo f, Action<string,int> leerLinea)
		{
			
			string[] lineas = leer(f);
			for (int i = 0; i < lineas.Length; i++) {
				leerLinea(lineas[i], i);
			}
			
		}
		
		public static void recorrerTXT(FileInfo f, Action<string,int> leerLinea)
		{
			if (esTXT(f)) {
				recorrerTEXTO(f,leerLinea);
//				string[] lineas = leer(f);
//				for (int i = 0; i < lineas.Length; i++) {
//					leerLinea(lineas[i], i);
//				}
			}
		}
		
		
		public static string[] getUrlsParents(string urls){
			
			string a=urls.Replace("/",@"\");
			string separador=@"\";
			List<string> l=new List<string>();
			int indiceAnterior=0;
			int indiceActual=-1;
			while((indiceActual=a.IndexOf(separador,indiceAnterior))!=-1){
				//l.Add(subs(a,indiceAnterior,indiceActual));
				l.Add(subs(a,0,indiceActual));
				
				indiceAnterior=indiceActual+separador.Length;
			}
			return l.ToArray();
		}
		
		public static DirectoryInfo unirUrls_Direc(DirectoryInfo urlParent,string urlRelativa){
			return new DirectoryInfo(unirUrls(urlParent.ToString(),urlRelativa));
		}
		public static FileInfo unirUrls_File(DirectoryInfo urlParent,string urlRelativa){
			return new FileInfo(unirUrls(urlParent.ToString(),urlRelativa));
		}
		public static string unirUrls(string urlParent,string urlRelativa){
			return urlParent+@"\"+urlRelativa;
		}
		
		public static string eliminarCarateresNoPermitidos(string a){
			foreach (char c in caracteresNoPermitidosNombre) {
				a=a.Replace(c,' ');
			}
			return a;
		}

        public static bool tieneCarateresNoPermitidos(string a)
        {
            foreach (char c in caracteresNoPermitidosNombre)
            {
                if (a.Contains(c)) {
                    return true;
                }
            }
            return false;
        }

        public static char? getCaraterNoPermitidoQueTiene(string a)
        {
            foreach (char c in caracteresNoPermitidosNombre)
            {
                if (a.Contains(c))
                {
                    return c;
                }
            }
            return null;
        }

        public static void recorrerCarpeta_BoolEntrarSubCarpeta(
			DirectoryInfo d,
			Func<DirectoryInfo,int,int,bool> metodoUtilizarCarpeta,
			Action<FileInfo,int,int> metodoUtilizarArchivo,
			int profundidad = 0,int indice=0)
		{
			//metodoUtilizarCarpeta  (carpeta,#profundidad,#i)=>bool si entrar en esta carpeta
			//metodoUtilizarArchivo  (f,#profundidad,#i)=>{}
			if (metodoUtilizarCarpeta != null) {
				if (!metodoUtilizarCarpeta(d, profundidad,indice)) {
					return;
				}
				
			}
			
			//FileInfo[] F = d.GetFiles();
			
			FileInfo[] F = getArchivos_DireccionCompleta(d);
			int total = F.Length;
			for (int i = 0; i < total; i++) {
				//string ff = F[i].ToString();
				metodoUtilizarArchivo(F[i], profundidad + 1,i);
			}
			DirectoryInfo[] D = getCarpetas_DireccionCompleta(d);
			int indiceElemento=total;
			total = D.Length;
			
			for (int i = 0; i < total; i++) {
				recorrerCarpeta_BoolEntrarSubCarpeta(D[i], metodoUtilizarCarpeta, metodoUtilizarArchivo, profundidad + 1,indiceElemento);
				indiceElemento++;
			}
			

		}
		
		
		
		
		public static bool esTXT(FileInfo f)
		{
			return getExtencion(f).ToLower() == ".txt";
		}
		public static void recorrerCarpeta_UtilizarCarpetaAlFinal(DirectoryInfo d, bool recorrerCarpetasInternas, Predicate<DirectoryInfo> metodoUtilizarCarpeta, metodoUtilizar<FileInfo> metodoUtilizarArchivo)
		{
			
			
			//FileInfo[] F = d.GetFiles();
			FileInfo[] F = getArchivos_DireccionCompleta(d);
			int total = F.Length;
			for (int i = 0; i < total; i++) {
				//string ff = F[i].ToString();
				//metodo(new FileInfo(d.ToString() + "/" + F[i].ToString()));
				metodoUtilizarArchivo(F[i]);
			}
			if (recorrerCarpetasInternas) {
				//DirectoryInfo[] D = d.GetDirectories();
				DirectoryInfo[] D = getCarpetas_DireccionCompleta(d);
				total = D.Length;

				for (int i = 0; i < total; i++) {
					//recorrerCarpeta(new DirectoryInfo(d.ToString() + "/" + D[i].ToString()), recorrerCarpetasInternas, metodoUtilizarArchivo);
					recorrerCarpeta_UtilizarCarpetaAlFinal(D[i], recorrerCarpetasInternas, metodoUtilizarCarpeta, metodoUtilizarArchivo);
				}
			}
			
			if (metodoUtilizarCarpeta != null) {
				if (!metodoUtilizarCarpeta(d)) {
					return;
				}
				
			}

		}

		/**<summary>
     * deubuleve el indice donde comieza la extencion solo dentro del nombre o
     * -1 sino tiene
     *</summary>
     * 
     */
		public static int getIndiceDeExtencion(FileInfo f)
		{

			return getIndiceDeExtencion(f.Name);
		}
		/**<summary>
     * deubuleve el indice donde comieza la extencion solo dentro del nombre o
     * -1 sino tiene
     *</summary>
     * 
     */
		public static int getIndiceDeExtencion(String nombre)
		{
			int i = nombre.LastIndexOf(".");
			// int i = contieneStringContrario(nombre, ".");
			int lenNombre = nombre.Length;
			bool retornarInvalido = false;
			if (i != -1 && i < lenNombre - 1) {
				char c = nombre.ElementAt(i + 1);
				if (!Char.IsLetter(c)) {
					if (i < lenNombre - 2) {
						char c2 = nombre.ElementAt(i + 2);
						retornarInvalido = c != '3' && c2 != 'g';
       			
					} else {
						retornarInvalido = true;
					}
       		
				}
//       	else{
//       		retornarInvalido=false;
//       	}
			}
       
			if (retornarInvalido) {
				return	-1;
			}
       
//        if (i != -1 && i < nombre.Length - 1 && ((!esCharLetra(nombre.charAt(i + 1))) ? (i < nombre.length() - 2 ? (nombre.charAt(i + 1) != '3' && nombre.charAt(i + 2) != 'g') : true) : (false))) {
//            return -1;
//        }
			return i;
		}
		
	
		
		public static string  getNombre(FileInfo f)
		{
			string extencion = getExtencion(f);
			string nombreConExtencion = f.Name;
			int iPunto = nombreConExtencion.LastIndexOf(".");
			int iExtencion = iPunto != -1 ? iPunto : nombreConExtencion.Length;
			string nombre = subs(nombreConExtencion, 0, iExtencion);
			return nombre;
		}
		public static string  getUrl(FileSystemInfo f, string nombre)
		{
			string url = f.ToString() + @"\" + nombre;
			return url.Replace("\\", @"\" );

        }
		public static DirectoryInfo crearCarpeta_SiNoExiste(DirectoryInfo f, string nombre)
		{
			if (f.Exists) {
				string url = getUrl(f, nombre);
				if (!Directory.Exists(url)) {
                    System.IO.Directory.CreateDirectory(url);
				}
				return new DirectoryInfo(url);
	    		
			}
			return null;
    		
		}
		public static DirectoryInfo crearCarpeta(DirectoryInfo f, string nombre)
		{
			if (f.Exists) {
				string url = getUrl(f, nombre);
                System.IO.Directory.CreateDirectory(url);
				return new DirectoryInfo(url);
	    		
			}
			return null;
    		
		}
		
		//		public static FileInfo crearTEXTO(DirectoryInfo carpeta, string nombre, string extencion_Sustituye)
		//		{
		//
		//		}
		
		public static FileInfo crearArchivo(DirectoryInfo f, string nombre)
		{
			if (f.Exists) {
				string url = getUrl(f, nombre);
                System.IO.File.Create(url).Close();
				return new FileInfo(url);
			}
			return null;
		}
		public static FileInfo crearArchivo_SiNoExiste(DirectoryInfo f, string nombre)
		{
			if (f.Exists) {
				string url = getUrl(f, nombre);
				if (!File.Exists(url)) {
                    System.IO.File.Create(url).Close();
				}
				return new FileInfo(url);
			}
			return null;
		}
		//    	public static FileInfo crearTXT(DirectoryInfo f,string nombre){
		//    		return crearTEXTO(f,nombre,".txt");
		//    	}
    	
		public static FileInfo crearTXT(DirectoryInfo f, string nombre, params string[] lineas)
		{
			FileInfo txt = crearTEXTO(f, nombre, ".txt", lineas);
			//if (lineas.Length > 0) {
			//	File.WriteAllLines(txt.ToString(), lineas);
			//}
			return txt;
		}
		//<summary>
		//la carpetaDestino tiene que existir pq lo que se va a hacer es crear una carpeta nueva
		//dentro de carpetaDestino con el mismo nombre de la carpetaOriginal y ahí su la info de
		//su contenido
		//</summary>
		public static DirectoryInfo copiarInfDeCarpeta(DirectoryInfo carpetaOriginal, int nivelesCarpetasInternas, DirectoryInfo carpetaDestino)
		{
			return copiarInfDeCarpeta(carpetaOriginal, null, nivelesCarpetasInternas, carpetaDestino);
		}
		
		
		//public static DirectoryInfo
		//<summary>
		//la carpetaDestino tiene que existir pq lo que se va a hacer es crear una carpeta nueva
		//dentro de carpetaDestino con el mismo nombre de la carpetaOriginal y ahí su la info de
		//su contenido
		//</summary>
		public static DirectoryInfo copiarInfDeCarpeta(DirectoryInfo carpetaOriginal, Predicate<DirectoryInfo> copiarEstaCarpeta, int nivelesCarpetasInternas, DirectoryInfo carpetaDestino)
		{
			if (nivelesCarpetasInternas > 0 && carpetaOriginal.Exists && carpetaDestino.Exists) {
				if (copiarEstaCarpeta != null && !copiarEstaCarpeta(carpetaOriginal)) {
					return null;
				}
				
				string nombreCarpeta = carpetaOriginal.Name;
				carpetaDestino = crearCarpeta_SiNoExiste(carpetaDestino, nombreCarpeta);
				//crearTXT(carpetaDestino,nombreCarpeta,getArchivosListStr(carpeta));
				DateTime dt = DateTime.Now;
				crearTXT(carpetaDestino, "CREADO " + dt.Day + "-" + dt.Month + "-" + dt.Year + " (" + dt.Hour + "-" + dt.Minute + ")");
				
				string[] nombresArchivos = getArchivos_SoloNombreStr(carpetaOriginal);
				int leng = nombresArchivos.Length;
				for (int i = 0; i < leng; i++) {
					crearArchivo_SiNoExiste(carpetaDestino, nombresArchivos[i]);
				}
    			
				DirectoryInfo[] carpetasInternas = getCarpetas_DireccionCompleta(carpetaOriginal);
				leng = carpetasInternas.Length;
				for (int i = 0; i < leng; i++) {
					copiarInfDeCarpeta(carpetasInternas[i], copiarEstaCarpeta, nivelesCarpetasInternas - 1, carpetaDestino);
				}
				return carpetaDestino;
	
			}
			return null;
    		
    		
//    		if (carpetaOriginal.Exists&&carpetaDestino.Exists&&nivelesDeProfundidad>=0) {
//    			DirectoryInfo d=crearCarpeta(carpetaDestino,carpetaDestino.Name);
//    			DateTime dt=DateTime.Now;
//    			crearTXT(d,"CREADO d"+dt.Day+" m"+dt.Month+" y"+dt.Year+" - h"+dt.Hour+" m"+dt.Minute);
//    			
//    			
//    		}
    	
		}
    	
		public static string[] getArchivos_DireccionCompletaStr(DirectoryInfo carpeta)
		{
			return Directory.GetFiles(carpeta.ToString());
		}
		
		public static FileInfo[] getArchivos_DireccionCompleta(DirectoryInfo carpeta)
		{
			string[] urls = getArchivos_DireccionCompletaStr(carpeta);
			FileInfo[] F = new FileInfo[urls.Length];
			for (int i = 0; i < urls.Length; i++) {
				F[i] = new FileInfo(urls[i]);
			}
			return F;
		}
		
		public static string[] getArchivos_DireccionCompletaStr(string carpeta)
		{
			return Directory.GetFiles(carpeta);
		}
		public static string[] getArchivos_SoloNombreStr(string carpeta)
		{
			return getArchivos_SoloNombreStr(new DirectoryInfo(carpeta));
		}
		public static string[] getArchivos_SoloNombreStr(DirectoryInfo carpeta)
		{
			FileInfo[] F = carpeta.GetFiles();
			int leng = F.Length;
			string[] res = new string[leng];
			for (int i = 0; i < leng; i++) {
				res[i] = F[i].ToString();//pq el getFiles() de DirectoryInfo solo devuelve los nombres, no la direccion completa
			}
			return res;
		}
		public static string[] getArchivos_SoloNombreSinExtencionStr(DirectoryInfo carpeta)
		{
			FileInfo[] F = carpeta.GetFiles();
			int leng = F.Length;
			string[] res = new string[leng];
			for (int i = 0; i < leng; i++) {
				res[i] = getNombre(F[i]);//pq el getFiles() de DirectoryInfo solo devuelve los nombres, no la direccion completa
			}
			return res;
		}
		public static string[] getCarpetas_SoloNombreStr(DirectoryInfo carpeta)
		{
			DirectoryInfo[] F = carpeta.GetDirectories();
			int leng = F.Length;
			string[] res = new string[leng];
			for (int i = 0; i < leng; i++) {
				res[i] = F[i].ToString();//pq el GetDirectories() de DirectoryInfo solo devuelve los nombres, no la direccion completa
			}
			return res;
		}
		public static DirectoryInfo[] getCarpetas_DireccionCompleta(string url)
		{
			string[] direcciones = Directory.GetDirectories(url);
			int leng = direcciones.Length;
			DirectoryInfo[] res = new DirectoryInfo[leng];
			for (int i = 0; i < leng; i++) {
				res[i] = new DirectoryInfo(direcciones[i]);
			}
			return res;
    	
		}
		public static DirectoryInfo[] getCarpetas_DireccionCompleta(DirectoryInfo carpeta)
		{
			return getCarpetas_DireccionCompleta(carpeta.ToString());
		}
    	
		public static void crearTXTContenidoDeCarpetaYCarpetas(DirectoryInfo carpeta, int nivelesCarpetasInternas, DirectoryInfo carpetaDestino)
		{
	
			if (nivelesCarpetasInternas > 0 && carpeta.Exists && carpetaDestino.Exists) {
				string nombreCarpeta = carpeta.Name;
				carpetaDestino = crearCarpeta_SiNoExiste(carpetaDestino, nombreCarpeta);
				crearTXT(carpetaDestino, nombreCarpeta, getArchivos_SoloNombreStr(carpeta));
				DirectoryInfo[] carpetasInternas = getCarpetas_DireccionCompleta(carpeta);
				int leng = carpetasInternas.Length;
				for (int i = 0; i < leng; i++) {
					crearTXTContenidoDeCarpetaYCarpetas(carpetasInternas[i], nivelesCarpetasInternas - 1, carpetaDestino);
				}
	            
	
			}
		}
		public static bool existeArchivo(FileInfo f)
		{
			return existeArchivo(f.ToString());
		}
		public static bool existeArchivo(string url)
		{
			return esArchivo(url);
		}
		public static bool esArchivo(string url)
		{
			return System.IO.File.Exists(url);
		}
		public static bool existeCarpeta(string url)
		{
			return esCarpeta(url);
		}
		public static bool esCarpeta(string url)
		{
			return System.IO.Directory.Exists(url);
		}

		public static void escribir(string url, string contenido)
		{

            System.IO.StreamWriter sw = File.AppendText(url);
			string[] lineas = Utiles.split(contenido, '\n');
			int length = lineas.Length;
			for (int i = 0; i < length; i++) {
				sw.WriteLine(lineas[i]);
			}
			sw.Close();
		}

		public static void appenLogLn(string mensaje)
		{
			string url = System.IO.Directory.GetCurrentDirectory().ToString() + "/log.txt";
			if (!File.Exists(url)) {
                System.IO.File.CreateText(url).Close();
			}
			string cotenido = mensaje + "\n";
			escribir(url, cotenido);
			// File.AppendAllText(url, exe);
			//Console.WriteLine(cotenido);
		}
		public static void appenLogExeption(Exception ex)
		{
			string url = System.IO.Directory.GetCurrentDirectory().ToString() + "/logErrors.txt";
			if (!File.Exists(url)) {
                System.IO.File.CreateText(url).Close();
			}
			string cotenido = "------------------------------\n" + DateTime.Now + "\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace;
			escribir(url, cotenido);
			//File.AppendAllText(url,exe);
			Console.WriteLine(cotenido);

		}
		
		public static void saveObject_ExtencionForzada(string urlCarpeta, string nombre, string extencionForsada, object ob)
		{
			saveObject_ExtencionForzada(urlCarpeta + "/" + nombre, extencionForsada, ob);
		}
		public static void saveObject_ExtencionForzada(string url, string extencionForsada, object ob)
		{
			saveObject(setExtencionStr(url, extencionForsada), ob);
		}
		public static void saveObject(string url, object ob)
		{
            System.IO.FileStream fs = new System.IO.FileStream(url, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
			new BinaryFormatter().Serialize(fs, ob);
			fs.Close();
		}
		public static object readObject(string url)
		{
            System.IO.FileStream fs = new System.IO.FileStream(url, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			Object res = new BinaryFormatter().Deserialize(fs);
			fs.Close();
			return res;
		}
		public static string[] leer(FileInfo f)
		{
			return leer(f.ToString());
		}
		public static string[] leer(String url)
		{
			return System.IO.File.ReadAllLines(url);
		}

		private static void recorrerCarpeta(DirectoryInfo d, bool recorrerCarpetasInternas, Predicate<DirectoryInfo> metodoUtilizarCarpeta, metodoUtilizar<FileInfo> metodoUtilizarArchivo)
		{
			if (metodoUtilizarCarpeta != null) {
				if (!metodoUtilizarCarpeta(d)) {
					return;
				}
				
			}
			
			//FileInfo[] F = d.GetFiles();
			FileInfo[] F = getArchivos_DireccionCompleta(d);
			int total = F.Length;
			for (int i = 0; i < total; i++) {
				//string ff = F[i].ToString();
				//metodo(new FileInfo(d.ToString() + "/" + F[i].ToString()));
				metodoUtilizarArchivo(F[i]);
			}
			if (recorrerCarpetasInternas) {
				//DirectoryInfo[] D = d.GetDirectories();
				DirectoryInfo[] D = getCarpetas_DireccionCompleta(d);
				total = D.Length;

				for (int i = 0; i < total; i++) {
					//recorrerCarpeta(new DirectoryInfo(d.ToString() + "/" + D[i].ToString()), recorrerCarpetasInternas, metodoUtilizarArchivo);
					recorrerCarpeta(D[i], recorrerCarpetasInternas, metodoUtilizarCarpeta, metodoUtilizarArchivo);
				}
			}

		}
		
		public static void recorrerCarpeta(DirectoryInfo d
		                                   , bool recorrerCarpetasInternas
		                                   , metodoUtilizar<FileInfo> metodo)
		{ 
			
			recorrerCarpeta(d, recorrerCarpetasInternas, null, metodo);

		}
		public static void recorrerCarpeta(DirectoryInfo d, Predicate<DirectoryInfo> metodoUtilizarCarpeta, metodoUtilizar<FileInfo> metodoUtilizarArchivo)
		{
			recorrerCarpeta(d, true, metodoUtilizarCarpeta, metodoUtilizarArchivo);
		}
		
		public static void recorrerCarpeta(DirectoryInfo d, metodoUtilizar<DirectoryInfo> metodoUtilizarCarpeta, metodoUtilizar<FileInfo> metodoUtilizarArchivo)
		{
			//metodoUtilizar<FileInfo> me=new metodoUtilizar<FileInfo>(dd =>{ metodoUtilizarCarpeta(dd);});
			//Predicate<DirectoryInfo> ddd=r=>true;
			recorrerCarpeta(d, true, r => {
				metodoUtilizarCarpeta(r);
				return true;
			}, metodoUtilizarArchivo);
		}
		public static void recorrerArchivosExternos(DirectoryInfo d, metodoUtilizar<FileInfo> metodo)
		{ 
			FileInfo[] F = getArchivos_DireccionCompleta(d);
			int total = F.Length;
			for (int i = 0; i < total; i++) {
				metodo(F[i]);
			}
		}
		//public static string getNombre(string url)
		// {
		//     string carpeta = "", nombre = url;
		//     if (Utiles.containsOR(url, "/", "\\"))
		//     {
		//         int indice = Utiles.indexOF_OR(url, "/", "\\");
		//         nombre = url.Substring(indice);
		//         carpeta = url.Substring(0, indice);
		//     }
		//     return nombre;
		// }
		public static string getExtencion(string url)
		{
			return getExtencion(new FileInfo(url));
		}
		public static string getExtencion(FileSystemInfo f)
		{
			string nombre = f.Name;
			if (nombre.Contains(".")) {
				int indice = nombre.LastIndexOf(".");
				return nombre.Substring(indice, nombre.Length - indice);//-1
			}
			return "";
			//return nombre.Contains(".") ? nombre.Substring(nombre.LastIndexOf("."), nombre.Length) : "";
		}
		public static FileInfo setExtencion(string url, string extencionNueva)
		{
			return setExtencion(new FileInfo(url), extencionNueva);
		}
		public static string setExtencionStr(DirectoryInfo parent, string nombre, string extencionNueva)
		{
			string url = setExtencionStr(getUrl(parent, nombre), extencionNueva);
			return url;
		}
    
		public static string setExtencionStr(string url, string extencionNueva)
		{
			if (url.EndsWith(extencionNueva)) {
				return url;
			}
        	
			string extencion = getExtencion(url);
			string nuevaUrl = (extencion.Length != 0 ? url.Substring(0, url.LastIndexOf(extencion)) : url) + extencionNueva;

			return nuevaUrl;
			// return setExtencion(new FileInfo(url), extencionNueva).ToString();
		}
		public static FileInfo setExtencion(FileInfo f, string extencionNueva)
		{
			string url = f.ToString();
			string nuevaUrl = setExtencionStr(url, extencionNueva);
            System.IO.Directory.Move(url, nuevaUrl);
			return new FileInfo(nuevaUrl);
       
		}

		public static string getParentStr(FileInfo f)
		{
			DirectoryInfo parent = Directory.GetParent(f.ToString());
			string urlParent = parent == null ? "" : parent.ToString();
			return urlParent;
		}
		public static FileInfo renombrarStr(FileInfo f, string nuevoNombre)
		{
			nuevoNombre = nuevoNombre.Replace("?", "");
			if (f == null && nuevoNombre.Trim().Length == 0) {//String.IsNullOrWhiteSpace(nuevoNombre)
				return null;
			}
			//cwl("f="+f.ToString());
			//cwl("existe="+Archivos.existeArchivo(f));
			//string extencion = getExtencion(f);
			string newDirection = getParentStr(f) + "/" + nuevoNombre;
			//File.Move(f.ToString(), newDirection);
			//cwl("newDirection="+newDirection);
			return new FileInfo(newDirection);
		}
		public static FileInfo renombrar(FileInfo f, string nuevoNombre)
		{
			if (f == null && nuevoNombre.Trim().Length == 0) {//String.IsNullOrWhiteSpace(nuevoNombre)
				return null;
			}
			//cwl("f="+f.ToString());
			//cwl("existe="+Archivos.existeArchivo(f));
			//string extencion = getExtencion(f);
			string newDirection = getParentStr(f) + "/" + nuevoNombre;
            System.IO.File.Move(f.ToString(), newDirection);
			return new FileInfo(newDirection);

		}
		public static FileInfo renombrar_SinExtencion(FileInfo f, string nuevoNombre)
		{
			if (f == null && nuevoNombre.Trim().Length == 0) {
				return null;
			}
			string extencion = getExtencion(f);
			string newDirection = getParentStr(f) + "/" + nuevoNombre + extencion;
            System.IO.File.Move(f.ToString(), newDirection);
			return new FileInfo(newDirection);

		}

        
	}
}


//public static EspacioEnAlmacenamiento getTipoDeEspacioDisponible(string url, double _cantidadDeBytesDeUrl = -1)
//{
//    double cantidadDeBytesDeUrl = _cantidadDeBytesDeUrl;
//    if (cantidadDeBytesDeUrl == -1)
//    {
//        if (Archivos.esArchivo(url))
//        {
//            cantidadDeBytesDeUrl = getEspacioQueOcupa(new FileInfo(url));
//        }
//        else
//        {
//            cantidadDeBytesDeUrl = getEspacioQueOcupa(new DirectoryInfo(url));
//        }

//    }

//    string discoDestino = Utiles.subs(url, 0, 3);
//    System.IO.DriveInfo[] allDrives = System.IO.DriveInfo.GetDrives();
//    foreach (System.IO.DriveInfo d in allDrives)
//    {
//        if (d.Name == discoDestino)
//        {

//            if (cantidadDeBytesDeUrl < d.AvailableFreeSpace)
//            {
//                return true;
//            }
//            this.showDlg_Info("No tiene espacio disponible en la unidad " + d.Name
//                + "\n Espacio disponible");
//            return false;
//        }
//    }
//}