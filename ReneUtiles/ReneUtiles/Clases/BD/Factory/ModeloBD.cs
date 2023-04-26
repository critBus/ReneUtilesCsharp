/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 11/12/2021
 * Time: 18:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
//using System.Threading.Tasks;
using ReneUtiles.Clases;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;
using ReneUtiles.Clases.BD.Factory.Consultas;
namespace ReneUtiles.Clases.BD.Factory
{
	/// <summary>
	/// Description of ModeloBD.
	/// </summary>
	public class ModeloBD:ConsolaBasica,ElementoPorElQueBuscar
	{
		public string Nombre;
		public List<ColumnaDeModeloBD> Columnas;
		
		public bool SuscritaAUpdates;
		
		
		public List<List<ColumnaDeModeloBD>> ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos;
		public List<List<ColumnaDeModeloBD>> ListaDeConjuntoDeColumnasPorLasQueBuscarUnModelo;
		public List<List<ColumnaDeModeloBD>> ListaDeConjuntoDeColumnasPorLasQueEliminar;
		public List<OneToMany> ListaOneToMany;
		public List<OneToManySort> ListaOneToManySort;
		public List<OneToMany_EnTablaExterna> ListaOneToMany_EnTablaExterna;
		
		public List<SelectWhereSort> ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelosYOrdenar;
		
		public List<ColumnaDeModeloBD> ListaBorrarJuntoA;
		
		
		public List<List<ColumnaDeModeloBD>> ListaDeConjuntoDeColumnasPorLasVerSiExiste;
		
		public ModeloBD(string nombre, params ColumnaDeModeloBD[] columnas)
			: this(nombre, false, columnas)
		{
			
		}
		public ModeloBD(string nombre, bool suscritaAUpdates, params ColumnaDeModeloBD[] columnas)
		{
			this.Nombre = nombre;
			this.Columnas = new List<ColumnaDeModeloBD>(columnas);
			this.SuscritaAUpdates = suscritaAUpdates;
			
			this.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos = new List<List<ColumnaDeModeloBD>>();
			this.ListaDeConjuntoDeColumnasPorLasQueBuscarUnModelo = new List<List<ColumnaDeModeloBD>>();
			this.ListaDeConjuntoDeColumnasPorLasQueEliminar = new List<List<ColumnaDeModeloBD>>();
			this.ListaOneToMany = new List<OneToMany>();
			this.ListaOneToManySort = new List<OneToManySort>();
			this.ListaOneToMany_EnTablaExterna = new List<OneToMany_EnTablaExterna>();
			this.ListaBorrarJuntoA = new List<ColumnaDeModeloBD>();
			this.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelosYOrdenar = new List<SelectWhereSort>();
			
			
			this.ListaDeConjuntoDeColumnasPorLasVerSiExiste = new List<List<ColumnaDeModeloBD>>();
		}
		
		public bool contieneEstaColumna(ColumnaDeModeloBD c)
		{
			return Columnas.IndexOf(c) != -1;
		}
		
		
		public ModeloBD addBorrarJuntoA(ModeloBD m){
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c=m.Columnas[i];
				if(c.EsReferencia&&c.ReferenciaID==this){
					 addBorrarJuntoA(c);
					// cwl("se agrego="+c.Nombre);
					
				}
			}
			return this;
		}
		public ModeloBD addBorrarJuntoA(ColumnaDeModeloBD c){
			this.ListaBorrarJuntoA.Add(c);
			return this;
		}
		public void comprobarSeguridad()
		{
			if (Columnas.Count == 0) {
				throw new Exception("Tiene que tener columnas el modelo" + this.Nombre);
			}
			metodoCreador1<List<ColumnaDeModeloBD>,bool> tieneListaColumnasIguales = l => {
				for (int i = 0; i < l.Count; i++) {
					for (int j = 0; j < l.Count; j++) {
						if (i != j) {
							ColumnaDeModeloBD ci = l[i];
							ColumnaDeModeloBD cj = l[j];
							string ni=ci.Nombre;
							string nj=cj.Nombre;
							if (ci == cj || ci.Nombre == cj.Nombre) {
								return true;
							}
						}
					}
				}
				return false;
			};
			
			if (tieneListaColumnasIguales(Columnas)) {
				throw new Exception("No puede existir columnas con el mismo nombre"
				+ ": en " + this.Nombre);
			}
			
			metodoCreador1<List<List<ColumnaDeModeloBD>>,bool> tieneMatrisOListasColumnasIguales = l => {
				for (int i = 0; i < l.Count; i++) {
					if (tieneListaColumnasIguales(l[i])) {
						//cwl("true en 0");
						return true;
					}
					for (int j = 0; j < l.Count; j++) {
						if (i != j) {
							if (l[i] == l[j]) {
								//cwl("true en 1");
								return true;
							}
						}
					}
				}
				return false;
			};
			if (tieneMatrisOListasColumnasIguales(ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos)
			    || tieneMatrisOListasColumnasIguales(ListaDeConjuntoDeColumnasPorLasQueBuscarUnModelo)
			    || tieneMatrisOListasColumnasIguales(ListaDeConjuntoDeColumnasPorLasQueEliminar)) {
				throw new Exception("No puede existir columnas ni listas iguales "
				+ ": en " + this.Nombre);
			}
			
			for (int i = 0; i < ListaOneToMany.Count; i++) {
				OneToMany oi = ListaOneToMany[i];
				oi.comprobarSeguridad();
				for (int j = 0; j < ListaOneToMany.Count; j++) {
					if (i != j) {
						
						OneToMany oj = ListaOneToMany[j];
						if (oi == oj) {
							throw new Exception("No puede existir OneToMany iguales "
							+ ": en " + this.Nombre);
						}
						if (oi.One == oi.Many) {
							throw new Exception("No puede existir One y Many iguales "
							+ ": en " + this.Nombre);
						}
						if (oi.One == oj.One && oi.Many == oj.Many && (oi.LinkToOne == oj.LinkToOne || oi.LinkToOne.Nombre == oj.LinkToOne.Nombre)) {
							throw new Exception("No puede existir OneToMany iguales "
							+ ": en " + this.Nombre);
						}
					}
				}
			}
			
			
			for (int i = 0; i < ListaOneToMany_EnTablaExterna.Count; i++) {
				OneToMany_EnTablaExterna oi = ListaOneToMany_EnTablaExterna[i];
				oi.comprobarSeguridad();
				for (int j = 0; j < ListaOneToMany.Count; j++) {
					if (i != j) {
						
						OneToMany_EnTablaExterna oj = ListaOneToMany_EnTablaExterna[j];
						if (oi == oj) {
							throw new Exception("No puede existir OneToMany_EnTablaExterna iguales "
							+ ": en " + this.Nombre);
						}
						if (oi.One == oi.Many) {
							throw new Exception("No puede existir One y Many iguales "
							+ ": en " + this.Nombre);
						}
						if (oi.One == oj.One && oi.Many == oj.Many && oi.Union == oj.Union) {
							throw new Exception("No puede existir OneToMany_EnTablaExterna iguales "
							+ ": en " + this.Nombre);
						}
					}
				}
			}
		}
		//		public OneToMany addGet
		
		
		public OneToMany addGetListaDe(ModeloBD m)
		{
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				if (c.EsReferencia && c.ReferenciaID == this) {
					OneToMany o = addGetListaDe(m, c,null);
					return o;
				}
			}
			return null;
		}
        public OneToMany addGetListaDe(ModeloBD m, ColumnaDeModeloBD c, string nombre) {
            return addGetListaDe(null, m, c, nombre);
        }
        /// <summary>
        /// cLink es generalmente el id, plq se puede dejar en null
        /// esta columna es la que el modelo Many va ha tener como referencia
        /// aunque esta referencia es mas bien un valor por el que va ha identificar
        /// a su modelo One, se recomienda que sea una columna de
        /// valor unico 
        /// </summary>
        /// <param name="cPorLaQueBuscar"></param>
        /// <param name="m"></param>
        /// <param name="c"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public OneToMany addGetListaDe(ColumnaDeModeloBD cLink, ModeloBD m, ColumnaDeModeloBD c,string nombre)
		{
			OneToMany o = new OneToMany(one: this, many: m
                , linkToOne: c,nombre:nombre
                ,LinkToMany: cLink
                );
			ListaOneToMany.Add(o);

            if (!c.EsReferencia) {
                m.addBuscarListaPor(c);
            }

			return o;
		}
		
		public OneToManySort addGetListaSortDe_ref(ModeloBD m,params ElementoPorQueElOrdenar[] parametrosParaOrdenar){
			for (int i = 0; i < m.Columnas.Count; i++) {
				ColumnaDeModeloBD c = m.Columnas[i];
				if (c.EsReferencia && c.ReferenciaID == this) {
					OneToManySort o = addGetListaSortDe(m, c,parametrosParaOrdenar);
					return o;
				}
			}
			return null;
		}
		public OneToManySort addGetListaSortDe(ModeloBD modelo, ColumnaDeModeloBD columna,params ElementoPorQueElOrdenar[] parametrosParaOrdenar)
		{
			List<SelectWhereSort.ColumnaYOrden> listaSort=new List<SelectWhereSort.ColumnaYOrden>();
			for (int i = 0; i < parametrosParaOrdenar.Length; i++) {
				ElementoPorQueElOrdenar e=parametrosParaOrdenar[i];
				if(e is ColumnaDeModeloBD){
					ColumnaDeModeloBD c=(ColumnaDeModeloBD)e;
					TipoDeOrdenamientoSQL t=null;
					if(i!=parametrosParaOrdenar.Length-1){
						ElementoPorQueElOrdenar ej=parametrosParaOrdenar[i+1];
						if(ej is TipoDeOrdenamientoSQL){
							t=(TipoDeOrdenamientoSQL)ej;
							i++;
						}
					}
					listaSort.Add(new SelectWhereSort.ColumnaYOrden{Columna=c,Ordenamiento=t});
				}
			}
			
			OneToManySort o = new OneToManySort(one: this, many: modelo, linkToOne: columna,listaPorLasQueOrdenar:listaSort);
			ListaOneToManySort.Add(o);
			return o;
		}
		
		public OneToMany_EnTablaExterna addGetListaDe_enTablaExterna(ModeloBD_ID m)
		{
			return addGetListaDe_enTablaExterna(m, null);
		}
		public OneToMany_EnTablaExterna addGetListaDe_enTablaExterna(ModeloBD_ID m, string nombre)
		{
			ModeloUnion union = OneToMany_EnTablaExterna.crearUnion(one: (ModeloBD_ID)this, many: m, nombre: nombre);
			OneToMany_EnTablaExterna o = new OneToMany_EnTablaExterna(one: this, many: m, union: union);
			ListaOneToMany_EnTablaExterna.Add(o);
			return o;
		}
		
		public ModeloBD addBuscarListaSortPor(List<ColumnaDeModeloBD> listaWhere,params ElementoPorQueElOrdenar[] listaSort){
			SelectWhereSort s=new SelectWhereSort((ModeloBD_ID)this);
			s.ListaPorLasQueBuscar=listaWhere;
			for (int i = 0; i < listaSort.Length; i++) {
				ElementoPorQueElOrdenar e=listaSort[i];
				if(e is ColumnaDeModeloBD){
					ColumnaDeModeloBD c=(ColumnaDeModeloBD)e;
					TipoDeOrdenamientoSQL t=null;
					if(i!=listaSort.Length-1){
						ElementoPorQueElOrdenar ej=listaSort[i+1];
						if(ej is TipoDeOrdenamientoSQL){
							t=(TipoDeOrdenamientoSQL)ej;
							i++;
						}
					}
					s.ListaPorLasQueOrdenar.Add(new SelectWhereSort.ColumnaYOrden{Columna=c,Ordenamiento=t});
				}
			}
			
			this.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelosYOrdenar.Add(s);
			return this;
		}
		
		public ModeloBD addBuscarListaPor(List<ColumnaDeModeloBD> l)
		{
			if (l.Count == 1) {
				ColumnaDeModeloBD c = l[0];
				if (c != null) {
					c.BuscarListaPorEstaColumna = true;
				}
				
				
				
			} else {
				
				for (int i = 0; i < ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos.Count; i++) {
					List<ColumnaDeModeloBD> t = ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos[i];
					if (t.Count == l.Count) {
						for (int j = 0; j < t.Count; j++) {
							if (t[j] != l[j]) {
								goto asd;
							}
						}
						return this;
						asd:
						;
					}
					
				}
				
				ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos.Add(l);
			}
			return this;
		}
		
		public  ModeloBD addBuscarListaPor<T>(params T[] C) where T:ElementoPorElQueBuscar
		{	
			metodoCreador1<ElementoPorElQueBuscar,ColumnaDeModeloBD> intentarParseAColumna = t => {
				ColumnaDeModeloBD c = null;
				if (t is ModeloBD) {
					ModeloBD m = (ModeloBD)t;
					ColumnaDeModeloBD[] referencias = m.getColumnasReferencia();
					for (int i = 0; i < referencias.Length; i++) {
						if (referencias[i].ReferenciaID == this) {
							c = referencias[i];
							break;
						}
					}
					
				} else if (C[0] is ColumnaDeModeloBD) {
					c = (ColumnaDeModeloBD)t;
				}
				
				return c;
			};
			
			if (C.Length == 1) {
				ColumnaDeModeloBD c = intentarParseAColumna(C[0]);
				if (c != null) {
					c.BuscarListaPorEstaColumna = true;
				}
				
				
				
			} else {
				List<ColumnaDeModeloBD> l = new List<ColumnaDeModeloBD>();
				for (int i = 0; i < C.Length; i++) {
					ColumnaDeModeloBD c = intentarParseAColumna(C[i]);
					if (c != null) {
						l.Add(c);
					}
				}
				if (l.Count > 0) {
					this.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelos.Add(l);
				}
				
			}
			return this;
		}
		
		public ModeloBD addBuscarPor(params ColumnaDeModeloBD[] C)
		{
			if (C.Length == 1) {
				C[0].BuscarModeloPorEstaColumna = true;
			} else {
				List<ColumnaDeModeloBD> l = new List<ColumnaDeModeloBD>(C);
				this.ListaDeConjuntoDeColumnasPorLasQueBuscarUnModelo.Add(l);
			}
			return this;
		}
		public ModeloBD addDeletePor(params ColumnaDeModeloBD[] C)
		{
			if (C.Length == 1) {
				C[0].EliminarPorEstaColumna = true;
			} else {
				List<ColumnaDeModeloBD> l = new List<ColumnaDeModeloBD>(C);
				this.ListaDeConjuntoDeColumnasPorLasQueEliminar.Add(l);
			}
			return this;
		}
		
		public ColumnaDeModeloBD addC(string nombre, TipoDeDatoSQL tipo, int tamaño, params TipoDeClasificacionSQL[] clasificaciones)
		{
			ColumnaDeModeloBD c = new ColumnaDeModeloBD(nombre: nombre, tipo: tipo, tamaño: tamaño, clasificaciones: clasificaciones);
			this.Columnas.Add(c);
			c.Padre = this;
			if (c.EsUnique) {
				c.BuscarModeloPorEstaColumna = true;
				c.EliminarPorEstaColumna = true;
			}
//			cwl("0 c.Padre="+c.Padre);
//			cwl("0 c.Padre.Nombre="+c.Padre.Nombre);
			return c;
		}
		public ColumnaDeModeloBD addC(string nombre, TipoDeDatoSQL tipo, params TipoDeClasificacionSQL[] clasificaciones)
		{
			return addC(nombre: nombre, tipo: tipo, tamaño: -1, clasificaciones: clasificaciones);
		}
		public ColumnaDeModeloBD addC(string nombre, int tamaño, params TipoDeClasificacionSQL[] clasificaciones)
		{
			return addC(nombre: nombre, tipo: null, tamaño: tamaño, clasificaciones: clasificaciones);
		}
		public ColumnaDeModeloBD addC(string nombre, params TipoDeClasificacionSQL[] clasificaciones)
		{
			return addC(nombre: nombre, tipo: null, tamaño: -1, clasificaciones: clasificaciones);
		}
		public ColumnaDeModeloBD addC_ID(string nombre, ModeloBD_ID modelo, params TipoDeClasificacionSQL[] clasificaciones)
		{
			ColumnaDeModeloBD c = addC(nombre: nombre, clasificaciones: clasificaciones);
			c.ReferenciaID = modelo;
			c.Tipo = TipoDeDatoSQL.INTEGER;
			return c;
		}
		public ColumnaDeModeloBD addC_ID(ModeloBD_ID modelo, params TipoDeClasificacionSQL[] clasificaciones)
		{
			//return addC_ID(nombre: "COLUMNA_ID_" + modelo.Nombre, modelo: modelo, clasificaciones: clasificaciones);
			ColumnaDeModeloBD c = addC_ID(nombre: "COLUMNA_ID_" + modelo.Nombre, modelo: modelo, clasificaciones: clasificaciones);
			//c.ReferenciaID = modelo;
			return c;
		}
		
		public ColumnaDeModeloBD[] getColumnasReferencia()
		{
			List<ColumnaDeModeloBD> lc = new List<ColumnaDeModeloBD>();
			for (int i = 0; i < Columnas.Count; i++) {
				ColumnaDeModeloBD c = Columnas.ElementAt(i);
				//cwl(c.Nombre);
				if (c.EsReferencia) {
					lc.Add(c);
					//cwl(lc.Count);
					
				}
			}
			return lc.ToArray();
		}
		
		public bool tieneAlgunaColumnaReferencia(){
		for (int i = 0; i < Columnas.Count; i++) {
				ColumnaDeModeloBD c = Columnas.ElementAt(i);
				//cwl(c.Nombre);
				if (c.EsReferencia) {
					return true;
					//cwl(lc.Count);
					
				}
			}
			return false;
		}
		public bool TienePrimaryKey {
			get {
				for (int i = 0; i < Columnas.Count; i++) {
					if (Columnas.ElementAt(i).EsPrimaryKey) {
						return true;
					}
				}
				return false;
			}
		}
		
		public TipoDeDatoSQL getTipoDeDatoID()
		{
			return TipoDeDatoSQL.INTEGER;
		}
		
		public ModeloBD addExiste(params ColumnaDeModeloBD[] C){
			List<ColumnaDeModeloBD> l = new List<ColumnaDeModeloBD>(C);
			this.ListaDeConjuntoDeColumnasPorLasVerSiExiste.Add(l);
			return this;
			
		}

        public string getPrimer_NombreColumnaKey() {
            for (int i = 0; i < Columnas.Count; i++)
            {
                ColumnaDeModeloBD c = Columnas.ElementAt(i);
                if (c.EsPrimaryKey)
                {
                    return c.Nombre;
                }
            }
            //return  "id";
            return null;
        }

	}
}
