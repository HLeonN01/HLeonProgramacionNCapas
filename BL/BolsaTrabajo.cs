using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BolsaTrabajo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from bolsas in context.BolsaTrabajo
                                 select bolsas).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var bolsas in query)
                        {
                            ML.BolsaTrabajo bolsa = new ML.BolsaTrabajo();
                            bolsa.IdBolsaTrabajo = bolsas.IdBolsaTrabajo;
                            bolsa.Nombre = bolsas.Nombre;
                            result.Objects.Add(bolsa);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay bolsas de trabajo";
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
