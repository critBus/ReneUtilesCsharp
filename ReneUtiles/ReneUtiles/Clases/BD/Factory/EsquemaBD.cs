/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 29/3/2022
 * Hora: 13:16
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

using System.Collections.Generic;
using ReneUtiles.Clases.BD.Factory.Consultas;
using ReneUtiles.Clases.BD.Factory.UtilesFactory;

namespace ReneUtiles.Clases.BD.Factory
{
	/// <summary>
	/// Description of EsquemaBD.
	/// </summary>
	public class EsquemaBD:BasicoFactory
	{
		
		public bool UsarSesionStorage;
		public bool UsarUpdate;
		private List<ModeloBD> modelos;
		public List<ManyToMany> ListaManyToMany;
		public List<InnerJoin> ListaInnerJoinAll;
		public List<InnerJoin> ListaInnerJoinOne;
		public Dictionary<ModeloBD,CrearDeleteCascade> listaCrearDeleteCascade;
		public Dictionary<ModeloBD,CrearDeleteCascade> listaCrearDeleteCascadeInverso;

        private string idDeafult;

        

        public EsquemaBD()
		{
			this.modelos = new List<ModeloBD>();
			this.ListaManyToMany = new List<ManyToMany>();
			this.ListaInnerJoinAll = new List<InnerJoin>();
			this.ListaInnerJoinOne = new List<InnerJoin>();
			this.listaCrearDeleteCascade = new Dictionary<ModeloBD,CrearDeleteCascade>();
			this.listaCrearDeleteCascadeInverso = new Dictionary<ModeloBD,CrearDeleteCascade>();

            this.idDeafult = "id";
        }

        public string IdDeafult
        {
            get
            {
                return idDeafult;
            }

            set
            {
                idDeafult = value;
                for (int i = 0; i < modelos.Count; i++)
                {
                    ModeloBD m = modelos[i];
                    if (m is ModeloBD_ID) {
                        ModeloBD_ID mi = (ModeloBD_ID)m;
                        if (!mi.tieneNombrePersonalizado_idKeyDefault) {
                            mi.columnaID.Nombre = value;
                        }
                    }
                }
                }
        }

        public bool necistaUnDeleteCascade(ModeloBD m){
			return listaCrearDeleteCascade[m].NecesitaDeleteCascade||listaCrearDeleteCascadeInverso[m].NecesitaDeleteCascade;
		}
		//Despues de comprobar la seguridad
		public void prepararAntesDeCrearCodigo(){
			for (int i = 0; i < modelos.Count; i++) {
				ModeloBD mi=modelos[i];
				CrearDeleteCascade cr=new CrearDeleteCascade(mi);
				
				listaCrearDeleteCascade.Add(mi,cr);
				
				CrearDeleteCascade cr_Inverso=new CrearDeleteCascade(mi);
				listaCrearDeleteCascadeInverso.Add(mi,cr_Inverso);
				for (int j = 0; j < modelos.Count; j++) {
					if(i!=j){
						ModeloBD mj=modelos[j];
						for (int k = 0; k < mj.Columnas.Count; k++) {
							ColumnaDeModeloBD c=mj.Columnas[k];
							if(c.EsReferencia&&c.ReferenciaID==mi){
								cr.NecesitaDeleteCascade=true;
								cr.ListaCascade.Add(c);
								
								c.EliminarPorEstaColumna=true;
								if(!c.EsUnique){
									c.BuscarListaPorEstaColumna=true;
								}
								//goto continueFor1;
								break;
							}
							
						}
						for (int k = 0; k < mj.ListaBorrarJuntoA.Count; k++) {
							ColumnaDeModeloBD c=mj.ListaBorrarJuntoA[k];
//							cwl("pasa por "+c.Nombre);
//							cwl("en "+mi.Nombre);
//							cwl("re="+c.ReferenciaID.Nombre);
							if(c.Padre==mi){
								//cwl("add I="+c.Nombre);
								cr_Inverso.NecesitaDeleteCascade=true;
								cr_Inverso.ListaCascade.Add(c);
								
								c.EliminarPorEstaColumna=true;
								if(!c.EsUnique){
									c.BuscarListaPorEstaColumna=true;
								}
								
								
							
							}
						}
//						continueFor1:;
					}
				}
				
				if(cr.NecesitaDeleteCascade){
					for (int j = 0; j < mi.ListaDeConjuntoDeColumnasPorLasQueEliminar.Count; j++) {
						mi.addBuscarListaPor( mi.ListaDeConjuntoDeColumnasPorLasQueEliminar[j]);
					}
					
				}
				for (int j = 0; j < mi.Columnas.Count; j++) {
					ColumnaDeModeloBD c=mi.Columnas[j];
					if(c.EsUnique){
						c.BuscarModeloPorEstaColumna=true;
						c.EliminarPorEstaColumna=true;
					}
				}
				
				for (int j = 0; j < mi.ListaOneToManySort.Count; j++) {
					mi.ListaDeConjuntoDeColumnasPorLasQueBuscaAllModelosYOrdenar.Add(mi.ListaOneToManySort[j].Sort);
				}
				
			}
		}
		public void comprobarSeguridad(){
			metodoCreador2<ModeloBD,ModeloBD,bool> paresModelosIguales=(m1,m2)=>{
				if(m1==m2){return true;}
				if(m1.Nombre==m2.Nombre){return true;}
				return false;
			};
			for (int i = 0; i < modelos.Count; i++) {
				modelos[i].comprobarSeguridad();
				//hay que comprobar los sort y junto los onemanysort (sus Sorts internos, con los externos del modelo)
				for (int j = 0; j < modelos.Count; j++) {
					if(i!=j){
						//if(modelos[i]==modelos[j]||modelos[i].Nombre==modelos[j].Nombre){
						if(paresModelosIguales(modelos[i],modelos[j])){
							throw new Exception("No puede existir modelos con el mismo nombre"
							               +": "+modelos[i].Nombre+" y "+modelos[j].Nombre);
						}
					}
				}
			}
			
			metodoCreador4<ModeloBD,ModeloBD,ModeloBD,ModeloBD,bool> paresModelosIguales4=(m1,m2,m3,m4)=>{
				if(paresModelosIguales(m1,m2)&&paresModelosIguales(m2,m1)){return true;}
				return false;
			};
			for (int i = 0; i < ListaManyToMany.Count; i++) {
				ManyToMany mI=ListaManyToMany[i];
				mI.comprobarSeguridad();
				for (int j = 0; j < ListaManyToMany.Count; j++) {
					if(i!=j){
						
						ManyToMany mJ=ListaManyToMany[j];
						if(mI==mJ){
							throw new Exception("No puede existir ManyToMany iguales"
							               +": "+mI.Union.Nombre);
						}
						
						if(
							(paresModelosIguales4(mI.Many_1,mJ.Many_1,mI.Many_2,mJ.Many_2)
						    ||paresModelosIguales4(mI.Many_2,mJ.Many_1,mI.Many_1,mJ.Many_2))
							&&paresModelosIguales(mI.Union,mJ.Union)
						){
							throw new Exception("No puede existir ManyToMany iguales"
							               +": "+mI.Union.Nombre);
						}
//						if((mI.Many_1==mJ.Many_1&&mI.Many_2==mJ.Many_2)
//						  ||(mI.Many_1==mJ.Many_2&&mI.Many_2==mJ.Many_1)
//						 &&mI.Union==mJ.Union){
//						
//						}
					}
				}
			}
			
			metodoCreador1<List<InnerJoin>,bool> innerjoinIguales=l=>{
			for (int i = 0; i < l.Count; i++) {
				for (int j = 0; j < l.Count; j++) {
						if(i!=j){
							InnerJoin Ii=l[i];
							InnerJoin Ij=l[j];
							if(Ii==Ij){
								return true;
								
							}
						}
					}
				}
				return false;
			};
			
			if(innerjoinIguales(ListaInnerJoinAll)||innerjoinIguales(ListaInnerJoinOne)){
				throw new Exception("No puede existir InnerJoin iguales");
			}
		}
		
		public int getCantidadDeModelos(){
			return modelos.Count;
		}
		public ModeloBD getModelo(int indice){
			return modelos[indice];
		}
		
		public EsquemaBD addModelo(params ModeloBD[] M)
		{
			for (int i = 0; i < M.Length; i++) {
				ModeloBD m = M[i];
				modelos.Add(m);
				for (int j = 0; j < m.ListaOneToMany_EnTablaExterna.Count; j++) {
					OneToMany_EnTablaExterna o = m.ListaOneToMany_EnTablaExterna[j];
					modelos.Add(o.Union);
				}
			}
			
			
			return this;
		}
		
		public EsquemaBD addManyToMany(ManyToMany m)
		{
			addModelo(m.Union);
			ListaManyToMany.Add(m);
			return this;
		}
		
		public ManyToMany addManyToMany(ModeloBD_ID m1, ModeloBD_ID m2, string nombre)
		{
			ManyToMany m = new ManyToMany(m1, m2, ManyToMany.crearUnion(m1, m2, nombre));
			addManyToMany(m);
			return m;
		}
        public ManyToMany addManyToMany(ModeloBD_ID m1, string m1_nombre, ModeloBD_ID m2, string m2_nombre, string nombre)
        {
            ManyToMany m = new ManyToMany(m1, m2, ManyToMany.crearUnion(m1, m1_nombre, m2, m2_nombre, nombre));
            addManyToMany(m);
            return m;
        }
        public ManyToMany addManyToMany(ModeloBD_ID m1, ModeloBD_ID m2)
		{
			return addManyToMany(m1, m2, null);
		}
		
		
		public List<ManyToMany> getListManyToMany(ModeloBD m){
			List<ManyToMany> l=new List<ManyToMany>();
			for (int i = 0; i < ListaManyToMany.Count; i++) {
				ManyToMany ma=ListaManyToMany[i];
				if(ma.Many_1==m||ma.Many_2==m){
					l.Add(ma);
				}
			}
			return l;
		}
		
		
		public bool isEmptyListaManyToMany(){
			return ListaManyToMany.Count==0;
		}
		
		public ManyToMany getManyToMany(int indice){
			return ListaManyToMany[indice];
		}
		
		public EsquemaBD addInnerJoinAll(InnerJoin I){
			ListaInnerJoinAll.Add(I);
			return this;
		}
		public InnerJoin addInnerJoinAll(ModeloBD_ID m,List<ElementoPorElQueBuscar> cadena,List<ElementoPorElQueBuscar> elementosWhere){
			InnerJoin I=new InnerJoin(m,cadena,elementosWhere);
			addInnerJoinAll(I);
			return I;
		}
		
		
		public EsquemaBD addInnerJoinOne(InnerJoin I){
			ListaInnerJoinOne.Add(I);
			return this;
		}
		public InnerJoin addInnerJoinOne(ModeloBD_ID m,List<ElementoPorElQueBuscar> cadena,List<ElementoPorElQueBuscar> elementosWhere){
			InnerJoin I=new InnerJoin(m,cadena,elementosWhere);
			addInnerJoinOne(I);
			return I;
		}
		
		
		
	}
}
