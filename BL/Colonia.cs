using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = context.ColoniaGetByIdMunicipio(IdMunicipio).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var colonias in query)
                        {
                            ML.Colonia colonia = new ML.Colonia(); 
                            colonia.Municipio = new ML.Municipio();
                            colonia.IdColonia = colonias.IdColonia;
                            colonia.Nombre = colonias.Nombre;
                            if (colonias.IdMunicipio == null)
                            {
                                colonia.Municipio.IdMunicipio = 0;
                            }
                            else
                            {
                                colonia.Municipio.IdMunicipio = colonias.IdMunicipio.Value;
                            }

                            result.Objects.Add(colonia);
                            result.Correct = true;
                        }
                    }
                    else
                    {
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

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = context.ColoniaGetAll().ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var colonias in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.Municipio = new ML.Municipio();
                            colonia.IdColonia = colonias.IdColonia;
                            colonia.Nombre = colonias.Nombre;
                            colonia.CodigoPostal = colonias.CodigoPostal;
                            if (colonias.IdMunicipio == null)
                            {
                                colonia.Municipio.IdMunicipio = 0;
                            }
                            else
                            {
                                colonia.Municipio.IdMunicipio = colonias.IdMunicipio.Value;
                            }
                            result.Objects.Add(colonia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "NO HAY REGISTROS";
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
