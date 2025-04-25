using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Carrera
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from carrerasList in context.Carreras
                                 select carrerasList).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var carreras in query)
                        {
                            ML.Carrera carrera = new ML.Carrera();
                            carrera.IdCarrera = carreras.IdCarrera;
                            carrera.Nombre = carreras.Nombre;
                            result.Objects.Add(carrera);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay carreras";
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
