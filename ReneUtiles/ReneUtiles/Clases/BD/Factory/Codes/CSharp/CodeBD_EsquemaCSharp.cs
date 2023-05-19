using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;
using ReneUtiles.Clases.BD;
using ReneUtiles.Clases.BD.SesionEstorage;
using ReneUtiles.Clases.BD.Factory;
namespace ReneUtiles.Clases.BD.Factory.Codes.CSharp
{
    public class CodeBD_EsquemaCSharp:CodeBDEsquema
    {
        public override string getEsquemaStr(List<ModeloBD> modelos)
        {
            StringBuilder codigo = new StringBuilder();

            foreach (var modelo in modelos)
            {
                string variableModelo = getNombreStrModelo(modelo);
                codigo.AppendLine($"ModeloBD_ID {variableModelo} = new ModeloBD_ID(\"{modelo.Nombre}\");");

                foreach (var columna in modelo.Columnas)
                {
                    if (columna.Nombre != "id")
                    {
                        if (columna.ReferenciaID != null)
                        {
                            string variableModeloReferencia = getNombreStrModelo(columna.ReferenciaID);
                            codigo.AppendLine($"{variableModelo}.addC_ID(\"{columna.Nombre}\", {variableModeloReferencia});");
                        }
                        else
                        {
                            codigo.Append($"{variableModelo}.addC(\"{columna.Nombre}\"");
                            if (columna.Tipo != TipoDeDatoSQL.VARCHAR
                                //|| columna.Tamaño != 256
                                )
                            {
                                codigo.Append($", TipoDeDatoSQL.{columna.Tipo.Valor.Replace(" ", "_")}");
                            }
                            if (or(columna.Tipo,
                                TipoDeDatoSQL.DOUBLE_PRECISION,
                                TipoDeDatoSQL.INTEGER,
                                TipoDeDatoSQL.REAL,
                                TipoDeDatoSQL.VARCHAR
                                )
                                &&(
                                    columna.Tipo== TipoDeDatoSQL.VARCHAR? columna.Tamaño!=256: true
                                )
                                ) {
                                
                                codigo.Append(","+columna.Tamaño);
                            }
                            foreach (var clasificacion in columna.Clasificaciones)
                            {
                                if (clasificacion==TipoDeClasificacionSQL.NULLABLE) {
                                    continue;
                                }
                                codigo.Append($",TipoDeClasificacionSQL.{clasificacion.Valor.Replace(" ", "_")}");
                            }
                            codigo.Append(");\n");
                            //codigo.AppendLine($"{variableModelo}.addC(\"{columna.Nombre}\", {columna.Tipo.Valor});");
                        }


                    }
                }


                codigo.AppendLine();
            }
            codigo.AppendLine("EsquemaBD e = new EsquemaBD();");
            codigo.Append($"e.addModelo(");//{variableModelo});
            for (int i = 0; i < modelos.Count; i++)
            {
                ModeloBD modelo = modelos[i];
                string variableModelo = getNombreStrModelo(modelo);
                codigo.Append((i != 0 ? "," : "") + variableModelo);
            }
            codigo.Append(");");

            return codigo.ToString();
        }


    }
}
