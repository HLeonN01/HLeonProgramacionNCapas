using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Piso
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from pisos in context.Pisoes
                                 select pisos).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var pisos in query)
                        {
                            ML.Piso piso = new ML.Piso();
                            piso.IdPiso = pisos.IdPiso;
                            piso.Nombre = pisos.Nombre;
                            result.Objects.Add(piso);
                        }
                        return result;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay pisos";
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
