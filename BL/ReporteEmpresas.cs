using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ReporteEmpresas
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = context.ReporteGetAll();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var empresas in query)
                        {
                            ML.CandidatoVacante candidato = new ML.CandidatoVacante();
                            candidato.Candidato = new ML.Candidato();
                            candidato.Vacante = new ML.Vacante();
                            candidato.Vacante.VacanteEmpresa = new ML.VacanteEmpresa();
                            candidato.Vacante.VacanteEmpresa.Empresa = new ML.Empresa();
                            
                            candidato.Candidato.IdCandidato = empresas.IdCandidato;
                            candidato.Candidato.Nombre = empresas.Nombre_Candidato;
                            candidato.Vacante.IdVacante = empresas.IdVacante;
                            candidato.Vacante.Nombre = empresas.Nombre_Vacante;
                            candidato.Vacante.VacanteEmpresa.Empresa.IdEmpresa = empresas.IdEmpresa;
                            candidato.Vacante.VacanteEmpresa.Empresa.Nombre = empresas.Nombre_Empresa;
                            result.Objects.Add(candidato);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }
            }
            catch(Exception ex) 
            {
                result.Correct = false; 
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
