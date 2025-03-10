using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {

        //STORED PROCEDURE
        public static ML.Result AddEF(ML.Rol rol) 
        { 
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities()) 
                {
                    int rowsAffect = context.RolAdd(rol.Nombre);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agregar el rol";
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

        public static ML.Result DeleteEF(int IdRol)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    int rowsAffect = context.RolDelete(IdRol);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el rol";
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

        public static ML.Result UpdateEF(int IdRol, string Nombre)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    int rowsAffect = context.RolUpdate(IdRol,Nombre);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el rol";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct= false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result() ;
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = context.RolGetAll().ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var all in query)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = all.IdRol;
                            rol.Nombre = all.NombreUsuario;
                            result.Objects.Add(rol);
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

        public static ML.Result GetById(int IdRol)
        {
            ML.Result result = new ML.Result() ;
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = context.RolGetById(IdRol).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Rol rol = new ML.Rol() ;
                        rol.IdRol = query.IdRol;
                        rol.Nombre = query.Nombre;
                        result.Object = rol;
                        result.Correct =true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro un usuario con ese Id";
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


        //LINQ
        public static ML.Result AddLINQ(ML.Rol rol)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    DL_EF.rol roles = new DL_EF.rol() ;
                    roles.IdRol = rol.IdRol;
                    roles.Nombre = rol.Nombre;
                    context.rols.Add(roles);
                    int filasAfectadas = context.SaveChanges();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agregar el registro";
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

        public static ML.Result UpdateLINQ(ML.Rol rol)
        {
            ML.Result result = new ML.Result() ;

            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    /*
                     * SINTAXIS
                     * 
                     * from alias in conexionBD.tabla
                     * where alias.Campo == parametro que recibimos(id o modelo)
                     * select(alias).SingleOrDefault()/ToList();
                     */

                    var query = (from roles in context.rols
                                 where roles.IdRol == rol.IdRol
                                 select roles).SingleOrDefault();

                    if (query != null)
                    {
                        query.IdRol = rol.IdRol;
                        query.Nombre = rol.Nombre;

                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo actualizar el rol";
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

        public static ML.Result DeleteLINQ(int IdRol)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from roles in context.rols
                                 where roles.IdRol == IdRol
                                 select roles).SingleOrDefault();
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

        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result ();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    /*
                     * SINTAXIS
                     * 
                     * from alias in conexionBD.tabla
                     * where alias.Campo == parametro que recibimos(id o modelo)
                     * select(alias).SingleOrDefault()/ToList();
                     */
                    var query = (from roles in context.rols
                                 select new
                                 {
                                     Id = roles.IdRol,
                                     Nombre = roles.Nombre

                                 }).ToList();

                    if (query.Count > 0)
                    { 
                        result.Objects = new List<object>();
                        foreach (var queries in query)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = queries.Id;
                            rol.Nombre = queries.Nombre;
                            result.Objects.Add(rol);
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

        public static ML.Result GetByIdLINQ(int IdRol)
        {
            ML.Result result = new ML.Result ();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from rol in context.rols
                                 where rol.IdRol == IdRol
                                 select new
                                 {
                                     Id = rol.IdRol,
                                     Nombre = rol.Nombre
                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Rol rol = new ML.Rol();
                        rol.IdRol = query.Id;
                        rol.Nombre = query.Nombre;
                        result.Object = rol;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro ese rol";
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
