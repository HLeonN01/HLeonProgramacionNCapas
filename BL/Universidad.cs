using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Universidad
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities()) 
                {
                    var query = (from universidades in context.Universidads
                                 select universidades).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var universidades in query)
                        {
                            ML.Universidad universidad = new ML.Universidad();
                            universidad.IdUniversidad = universidades.IdUniversidad;
                            universidad.Nombre = universidades.Nombre;
                            result.Objects.Add(universidad);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay universidades";
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
