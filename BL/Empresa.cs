using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from empresas in context.Empresa
                                 select empresas).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var empresas in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = empresas.IdEmpresa;
                            empresa.Nombre = empresas.Nombre;
                            empresa.Latitud = empresas.Latitud;
                            empresa.Longitud = empresas.Longitud;
                            result.Objects.Add(empresa);
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
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetById(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from empresas in context.Empresa
                                 where empresas.IdEmpresa == IdEmpresa
                                 select empresas).SingleOrDefault();
                    if (query != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();
                        empresa.IdEmpresa = query.IdEmpresa;
                        empresa.Nombre = query.Nombre;
                        empresa.Longitud = query.Longitud;
                        empresa.Latitud = query.Latitud;
                        result.Object = empresa;
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

        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    DL_EF.Empresa empresas = new DL_EF.Empresa();
                    empresas.IdEmpresa = empresa.IdEmpresa;
                    empresas.Nombre = empresa.Nombre; 
                    empresas.Longitud = empresa.Longitud;
                    empresas.Latitud = empresa.Latitud;
                    context.Empresa.Add(empresas);
                    int rowAffefct = context.SaveChanges();
                    if (rowAffefct > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo insertar";
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

        public static ML.Result Update(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from empresas in context.Empresa
                                 where empresas.IdEmpresa == empresa.IdEmpresa
                                 select empresas).SingleOrDefault();

                    if (query != null)
                    {
                        query.Nombre = empresa.Nombre;
                        query.Latitud = empresa.Latitud;
                        query.Longitud = empresa.Longitud;
                        int rowAffect = context.SaveChanges();
                        if (rowAffect > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo actualizar";
                        }
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

        public static ML.Result Delete(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from empresas in context.Empresa
                                 where empresas.IdEmpresa == IdEmpresa
                                 select empresas).SingleOrDefault();

                    if (query != null)
                    {
                        context.Empresa.Remove(query);
                        int rowAffect = context.SaveChanges();
                        if (rowAffect > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo eliminar  ";
                        }
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
