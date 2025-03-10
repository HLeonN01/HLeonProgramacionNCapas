using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = context.MunicipioGetByIdEstado(IdEstado).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var municipios in query) 
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.Estado = new ML.Estado();
                            municipio.IdMunicipio = municipios.IdMunicipio;
                            municipio.Nombre = municipios.Nombre;
                            if (municipios.IdEstado == null)
                            {
                                municipio.Estado.IdEstado = 0;
                            }
                            else
                            {
                                municipio.Estado.IdEstado = municipios.IdEstado.Value;
                            }
                            result.Objects.Add(municipio);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro al municipio";
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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = context.MunicipioGetAll().ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var municipios in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.Estado = new ML.Estado();
                            municipio.IdMunicipio = municipios.IdMunicipio;
                            municipio.Nombre = municipios.Nombre;
                            if (municipios.IdEstado == null)
                            {
                                municipio.Estado.IdEstado = 0;
                            }
                            else
                            {
                                municipio.Estado.IdEstado = municipios.IdEstado.Value;
                            }
                        }
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
