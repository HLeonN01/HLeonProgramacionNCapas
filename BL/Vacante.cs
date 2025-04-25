using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Vacante
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from vacantes in context.Vacantes
                                 select vacantes).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var vacantes in query)
                        {
                            ML.Vacante vacante = new ML.Vacante();
                            vacante.EstatusVacante = new ML.EstatusVacante();
                            vacante.IdVacante = vacantes.IdVacante;
                            vacante.Nombre = vacantes.Nombre;
                            vacante.FechaPublicacion = DateTime.Parse(vacantes.FechaPublicacion.ToString()).ToString();
                            vacante.FechaLimite = DateTime.Parse(vacantes.FechaLimite.ToString()).ToString();
                            vacante.UrlVacante = vacantes.UrlVacante;
                            vacante.EstatusVacante.IdEstatusVacante = vacantes.IdEstatusVacante;
                            result.Objects.Add(vacante);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
