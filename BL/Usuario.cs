using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BL
{
    public class Usuario
    {
        /*
        //-------------------------------------------------------------------------------------------------
        public static void Add(ML.Usuario usuario)//ML manda las propiedades al BL
        {
            try
            {
                //Using una vez que termina de ejecutarse elimina lo que se crea 
                //Se llama al metodo GetConnection de DL para crear la conexion a la base de datos
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    //Se guarda en query la sentencia 
                    string query = "INSERT INTO usuarios VALUES (@nombre, @email, @password, @telefono)";
                    SqlCommand cmd = new SqlCommand(); //Inicializa un objeto de tipo SqlCommand
                    cmd.CommandText = query; //El comando o sentencia que se va a ejecutar
                    cmd.Connection = context;//A donde se va a realizar el query es decir la base de datos


                    //Asignar los valores a los parametros de la consulta
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@password", usuario.Password);
                    cmd.Parameters.AddWithValue("@telefono", usuario.Telfono);

                    context.Open(); //Abrir la conexion a la base

                    //saber si se realizo la insercion
                    int numeroFilasAfectadas = cmd.ExecuteNonQuery();

                    if (numeroFilasAfectadas > 0)
                    {
                        Console.WriteLine("El registro se inserto correctamente");
                    }
                    else
                    {
                        Console.WriteLine("Error al insertar");
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error de conexion " + ex.ToString());
            }
        }

        public static void Delete(int id)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "DELETE FROM Usuarios WHERE id = @id";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@id", id);

                    context.Open();
                    
                    int numeroFilasAfectadas = cmd.ExecuteNonQuery();

                    if (numeroFilasAfectadas > 0)
                    {
                        Console.WriteLine("El registro se elimino correctamente");
                    }
                    else
                    {
                        Console.WriteLine("El registro no se pudo eliminar");
                    }
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error de conexion " + ex.ToString());
            }
        }

        public static void Update(ML.Usuario usuario)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UPDATE Usuarios SET nombre = @nombre, email = @email, password = @password, telefono = @telefono WHERE id = @id";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@id", usuario.Id);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);                                    
                    cmd.Parameters.AddWithValue("@email", usuario.Email);                                    
                    cmd.Parameters.AddWithValue("@password", usuario.Password);                                    
                    cmd.Parameters.AddWithValue("@telefono", usuario.Telfono);                                    

                    context.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("El registro se actualizo correctamente");
                    }
                    else
                    {
                        Console.WriteLine("Error al insertar el registro");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la conexion " + ex.ToString());
            }
        }

        
        public static ML.Result GetUserById(int id)
        {
            ML.Result result = new Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "SELECT * FROM Usuarios where id = @id";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        DataRow row = dataTable.Rows[0]; // Arreglo que me devuelve el registro  de la tabla
                        usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                        usuario.Nombre = row[1].ToString();
                        usuario.Email = row[2].ToString();
                        usuario.Password = row[3].ToString();
                        usuario.Telfono = row[4].ToString();

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No hay registros con ese ID";
                    }
                }
            }catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
            }
            return result;
        }


        public static ML.Result GetAll() //Regresa modelos de usuario
        {
            ML.Result result = new Result(); // Se instancia el objeto result
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))    
                {                    
                    string query = "SELECT * FROM Usuarios";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //Es el puente con la BD 
                    DataTable dataTable = new DataTable();
                    //Se instancia un data table
                    adapter.Fill(dataTable); // Adapter recive la informacion y llena el datatable con esa informacion

                    if (dataTable.Rows.Count > 0) //Si el numero de filas es mas que 0 
                    {
                        //Si hay registros
                        result.Objects = new List<object>();
                        //La propiedad de objetos se llena con la lista de registros

                        //Por cada fila que esta en la tabla(datatable) guardalo en la variable row
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            //Inicializa al objeto usuario
                            //Guarda la fila 0 en id
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                            //Guarda la fila 1 en Nombre
                            usuario.Nombre = (row[1].ToString());
                            //Guarda la fila 1 en Email
                            usuario.Email = (row[2].ToString());
                            //Guarda la fila 1 en Password 
                            usuario.Password = (row[3].ToString());
                            //Guarda la fila 1 en Telfono
                            usuario.Telfono = (row[4].ToString());
                            //Una vez llenos las variables guarda al usuario como un objeto
                            result.Objects.Add(usuario);
                        }
                        //Una vez que termina de iterar si el result.Correct es true quiere decir que fue exitosa la busqueda
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
                result.Correct=false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result; //Regresa result para saber si fue true o false
        }

        //---------------------------------------------------------------------------------------------------------------
        */

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioAdd";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure; //Indicarle que el query lo lea como Stored Procedure
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);

                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;                        
                    }
                    else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No se pudo insertar el registro";
                    }
                }
            }catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioUpdate";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                    cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.CommandType = CommandType.StoredProcedure;

                    context.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;                        
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el usuario";
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


        public static ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioDelete";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    context.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        result.Correct=true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar el usuario";
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
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection())) 
                {
                    string query = "UsuarioGetAll";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    context.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                            usuario.UserName = (row[1].ToString());
                            usuario.Nombre = (row[2].ToString());
                            usuario.ApellidoPaterno = (row[3].ToString());
                            usuario.ApellidoMaterno = (row[4].ToString());
                            usuario.Email = (row[5].ToString());
                            usuario.Password = (row[6].ToString());
                            usuario.FechaNacimiento = (row[7].ToString());
                            usuario.Sexo = (row[8].ToString());
                            usuario.Telefono = (row[9].ToString());
                            usuario.Celular = (row[10].ToString());
                            usuario.Estatus = Convert.ToBoolean(row[11].ToString());
                            usuario.CURP = (row[12].ToString());
                            //usuario.Imagen = System.Text.Encoding.UTF8.GetBytes(row[13].ToString());
                            usuario.Rol.IdRol = Convert.ToInt32(row[13].ToString());
                            result.Objects.Add(usuario);
                            
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la BD";
                    }
                }

            }catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        //BOXING
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection())) 
                {
                    string query = "UsuarioGetById";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    context.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        DataRow row = dataTable.Rows[0]; //Me da el primer regiatro de la tabla
                        usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                        usuario.UserName = (row[1].ToString());
                        usuario.Nombre = (row[2].ToString());
                        usuario.ApellidoPaterno = (row[3].ToString());
                        usuario.ApellidoMaterno = (row[4].ToString());
                        usuario.Email = (row[5].ToString());
                        usuario.Password = (row[6].ToString());
                        usuario.FechaNacimiento = (row[7].ToString());
                        usuario.Sexo = (row[8].ToString());
                        usuario.Telefono = (row[9].ToString());
                        usuario.Celular = (row[10].ToString());
                        usuario.Estatus = Convert.ToBoolean(row[11].ToString());
                        usuario.CURP = (row[12].ToString());
                        //usuario.Imagen = System.Text.Encoding.UTF8.GetBytes(row[13].ToString());
                        usuario.Rol.IdRol = Convert.ToInt32(row[13].ToString());
                        result.Object = usuario;
                        result.Correct = true;
                    } 
                    else 
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existe ningun usuario con ese ID";
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

        //public static ML.Result AddEF(ML.Usuario usuario)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities()) 
        //        {
        //            int rowsAffect = context.UsuarioAdd(usuario.UserName, usuario.Nombre,
        //                                usuario.ApellidoPaterno, 
        //                                usuario.ApellidoMaterno, 
        //                                usuario.Email, usuario.Password, 
        //                                DateTime.Parse(usuario.FechaNacimiento.ToString()), usuario.Sexo, 
        //                                usuario.Telefono,usuario.Celular,
        //                                usuario.Estatus, usuario.CURP, 
        //                                usuario.Imagen, usuario.Rol.IdRol);
        //            if (rowsAffect > 0)
        //            {
        //                result.Correct = true;
        //            }
        //            else
        //            {   
        //                result.Correct=false;
        //                result.ErrorMessage = "No se realizo la insercion";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}

        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    int rowsAffect = context.UsuarioDireccionAdd(usuario.UserName, usuario.Nombre,
                                        usuario.ApellidoPaterno,
                                        usuario.ApellidoMaterno,
                                        usuario.Email, usuario.Password,
                                        DateTime.Parse(usuario.FechaNacimiento.ToString()), usuario.Sexo,
                                        usuario.Telefono, usuario.Celular,
                                        usuario.Estatus, usuario.CURP,
                                        usuario.Imagen, usuario.Rol.IdRol, usuario.Direccion.Calle,
                                        usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior,
                                        usuario.Direccion.Colonia.IdColonia, usuario.IdUsuario);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se realizo la insercion";
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

        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    int rowsAffect = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre,
                                          usuario.ApellidoPaterno, usuario.ApellidoMaterno, 
                                          usuario.Email, usuario.Password, DateTime.Parse(usuario.FechaNacimiento.ToString()),
                                          usuario.Sexo, usuario.Telefono, usuario.Celular,
                                          usuario.Estatus, usuario.CURP, usuario.Imagen, usuario.Rol.IdRol);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar al usuario";
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

        public static ML.Result DeleteEF(int IdUsuario)
        {
            ML.Result result = new Result();

            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {

                    int rowsAffect = context.UsuarioDelete(IdUsuario);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else 
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el usuario";
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

        public static ML.Result GetAllEF(ML.Usuario usuarioObj)
        {
            ML.Result result = new ML.Result();            
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {                    
                    var cmd = context.UsuarioGetAll(usuarioObj.Nombre, usuarioObj.ApellidoPaterno, usuarioObj.ApellidoMaterno, usuarioObj.Rol.IdRol).ToList();
                    if (cmd.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var row in cmd)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.IdUsuario = row.idUsuario;
                            usuario.Nombre = row.NombreUsuario;
                            usuario.ApellidoPaterno = row.ApellidoPaterno;
                            usuario.ApellidoMaterno = row.ApellidoMaterno;
                            usuario.Email = row.Email;
                            usuario.Password = row.Password;
                            usuario.FechaNacimiento = row.FechaNacimiento;
                            usuario.Sexo = row.Sexo;
                            usuario.Telefono = row.Telefono;
                            usuario.Celular = row.Celular;
                            usuario.Estatus = row.Estatus;
                            usuario.CURP = row.CURP;  
                            usuario.UserName = row.UserName;
                            usuario.Rol.Nombre = row.NombreRol;
                            usuario.Direccion.Calle = row.Calle;
                            usuario.Direccion.NumeroInterior = row.NumeroInterior;
                            usuario.Direccion.NumeroExterior = row.NumeroExterior;
                            usuario.Direccion.Colonia.Nombre = row.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = row.CodigoPostal;
                            usuario.Imagen = row.Imagen;
                            usuario.Direccion.Colonia.Municipio.Nombre = row.NombreMunicipio;
                            /*
                            if (row.IdRol == null)
                            {
                                usuario.IdRol = 0;
                            }
                            else
                            {
                                usuario.IdRol = row.IdRol.Value;
                            }*/
                            result.Objects.Add(usuario);
                        }
                         
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la tabla";
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


        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var cmd = context.UsuarioGetById(IdUsuario).SingleOrDefault();

                    if (cmd != null)
                    {                        
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuario.IdUsuario = cmd.IdUsuario;
                        usuario.UserName = cmd.UserName;
                        usuario.Nombre = cmd.Nombre;
                        usuario.ApellidoPaterno = cmd.ApellidoPaterno;
                        usuario.ApellidoMaterno = cmd.ApellidoMaterno;
                        usuario.Email = cmd.Email;
                        usuario.Password = cmd.Password;
                        usuario.FechaNacimiento = cmd.FechaNacimiento;
                        usuario.Sexo = cmd.Sexo;
                        usuario.Telefono = cmd.Telefono;
                        usuario.Celular = cmd.Celular;
                        usuario.Estatus = cmd.Estatus;
                        usuario.CURP = cmd.CURP;
                        usuario.Direccion.Calle = cmd.Calle;
                        usuario.Direccion.NumeroExterior = cmd.NumeroExterior;
                        usuario.Direccion.NumeroInterior = cmd.NumeroInterior;
                        usuario.Direccion.Colonia.CodigoPostal = cmd.CodigoPostal;
                        usuario.Direccion.Colonia.Municipio.Nombre = cmd.NombreMunicipio;
                        usuario.Imagen = cmd.Imagen;
                        if (cmd.IdRol == null)
                        {
                            usuario.Rol.IdRol = 0;
                        }
                        else
                        {
                            usuario.Rol.IdRol = cmd.IdRol.Value;
                        }
                        if (cmd.IdDireccion == null)
                        {
                            usuario.Direccion.IdDireccion = 0;
                        }
                        else 
                        {
                            usuario.Direccion.IdDireccion = cmd.IdDireccion.Value;
                        }
                        if (cmd.IdColonia == null)
                        {
                            usuario.Direccion.Colonia.IdColonia = 0;
                        }
                        else
                        {
                            usuario.Direccion.Colonia.IdColonia = cmd.IdColonia.Value;
                        }
                        if (cmd.IdMunicipio == null)
                        {
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = 0;
                        }
                        else
                        {
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = cmd.IdMunicipio.Value;
                        }
                        if (cmd.IdEstado == null)
                        {
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = 0;
                        }
                        else
                        {
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = cmd.IdEstado.Value;
                        }
                       
                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el usuario";
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

        public static ML.Result AddLINQ(ML.Usuario usuario)
        {
            ML.Result result = new Result();

            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    //INSTANCIAR EL USUARIO DEL DL_EF
                    DL_EF.usuario usuarioDB = new DL_EF.usuario();                    
                    usuarioDB.UserName = usuario.UserName; //los parametros de ML.usuario se pasan a el objeto de DL_EF.usuario
                    usuarioDB.Nombre = usuario.Nombre;
                    usuarioDB.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioDB.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioDB.Email = usuario.Email;
                    usuarioDB.Password = usuario.Password;
                    usuarioDB.FechaNacimiento = DateTime.Parse(usuario.FechaNacimiento.ToString());
                    usuarioDB.Sexo = usuario.Sexo;
                    usuarioDB.Telefono = usuario.Telefono;
                    usuarioDB.Celular = usuario.Celular;
                    usuarioDB.Estatus = usuario.Estatus;
                    usuarioDB.CURP = usuario.CURP;
                    usuarioDB.Imagen = usuario.Imagen;
                    usuarioDB.IdRol = usuario.Rol.IdRol;

                    context.usuarios.Add(usuarioDB);

                    //guardar cambios
                    int filasAfectadas = context.SaveChanges();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo registrar al usuario";
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


        public static ML.Result DeleteLINQ(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {

                    //se coloca el tipo de dato var porque es algo mas general y no sabemos que tipo de dato estamos obteniendo
                    /*
                     * SINTAXIS
                     * 
                     * from alias in conexionBD.tabla
                     * where alias.Campo == parametro que recibimos(id o modelo)
                     * select(alias).SingleOrDefault()/ToList();
                     */

                    var delete = (from usuario in context.usuarios
                                  where usuario.idUsuario == IdUsuario
                                  select usuario).SingleOrDefault();


                    if (delete != null)
                    {
                        context.usuarios.Remove(delete);
                        int filasAfectadas = context.SaveChanges();

                        if (filasAfectadas > 0) 
                        {   
                            result.Correct=true;
                        }
                    } 
                    else 
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar al usuario";
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

        public static ML.Result UpdateLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

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

                    var update = (from usuarioDB in context.usuarios
                                  where usuarioDB.idUsuario == usuario.IdUsuario
                                  select usuarioDB).SingleOrDefault();
                   


                    if (update != null)
                    {
                        update.UserName = usuario.UserName;
                        update.Nombre = usuario.Nombre;
                        update.ApellidoPaterno = usuario.ApellidoPaterno;
                        update.ApellidoMaterno = usuario.ApellidoMaterno;
                        update.Email = usuario.Email;
                        update.Password = usuario.Password;
                        update.FechaNacimiento = DateTime.Parse(usuario.FechaNacimiento);
                        update.Sexo = usuario.Sexo;
                        update.Telefono = usuario.Telefono;
                        update.Celular = usuario.Celular;
                        update.Estatus = usuario.Estatus;
                        update.CURP = usuario.CURP;
                        update.Imagen = usuario.Imagen;
                        update.IdRol = usuario.Rol.IdRol;

                        int filasAfectadas = context.SaveChanges();
                        /*
                        context.usuarios.Attach(update);
                        context.Entry(update).State = System.Data.Entity.EntityState.Modified;
                        int filasAfectadas = context.SaveChanges();*/

                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo actualizar el reistro";
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

        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new HLeonProgramacionEnCapasEntities())
                {
                    /*
                     * SINTAXIS
                     * 
                     * from alias in conexionBD.tabla
                     * where alias.Campo == parametro que recibimos(id o modelo)
                     * select(alias).SingleOrDefault()/ToList();
                     */
                    var query = (from usuarioDB in context.usuarios
                                 select usuarioDB).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var getall in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = getall.idUsuario;
                            usuario.UserName = getall.UserName;
                            usuario.Nombre = getall.Nombre;
                            usuario.ApellidoPaterno = getall.ApellidoPaterno;
                            usuario.ApellidoMaterno = getall.ApellidoMaterno;
                            usuario.Email = getall.Email;
                            usuario.Password = getall.Password;
                            usuario.FechaNacimiento = getall.FechaNacimiento.ToString();
                            usuario.Sexo = getall.Sexo;
                            usuario.Telefono = getall.Telefono;
                            usuario.Celular = getall.Celular;
                            usuario.Estatus = getall.Estatus;
                            usuario.CURP = getall.CURP;
                            //usuario.Imagen = usuario.Imagen;
                            usuario.Rol.IdRol = getall.rol.IdRol;
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la BD";
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

        public static ML.Result GetByIdLINQ(int idUsuario)
        {
            ML.Result result = new ML.Result();
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
                    var query = (from usuarioDB in context.usuarios
                                 where usuarioDB.idUsuario == idUsuario
                                 select usuarioDB).SingleOrDefault();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.idUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString();
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.Estatus = query.Estatus;
                        usuario.CURP = query.CURP;
                        //usuario.Imagen = usuario.Imagen;
                        usuario.Rol.IdRol = (int)query.IdRol;
                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo encontrar el usuario";
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

        public static ML.Result CambioEstatus(int IdUsuario, bool Estatus)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new HLeonProgramacionEnCapasEntities())
                {
                    int filasAfectadas = context.CambiarEstatus(IdUsuario, Estatus);
                    if (filasAfectadas > 0)
                    {
                        result.Correct=true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "No se pudo cambiar el estatus";
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

        public static ML.Result UsuarioDireccionUpdate(ML.Usuario usuario)
        {
            ML.Result result = new Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new HLeonProgramacionEnCapasEntities()) 
                {
                    int query = context.UsuarioDireccionUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, DateTime.Parse(usuario.FechaNacimiento.ToString()), usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.CURP, usuario.Imagen, usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "no se pudo actualizar";
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

        public static ML.Result UusarioSinDireccionUpdate(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new HLeonProgramacionEnCapasEntities())
                {
                    int rowsAffect = context.UsuarioSinDireccionUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.Password, DateTime.Parse(usuario.FechaNacimiento.ToString()), usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.CURP, usuario.Imagen, usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "NO SE PUDO ACTUALIZAR EL USUARIO";
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

        public static ML.Result UsuarioDireccionDelete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new HLeonProgramacionEnCapasEntities())
                {
                    int rowAffect = context.UsuarioDireccionDelete(IdUsuario);

                    if (rowAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "EL USUARIO NO SE PUDO ELIMINAR";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetAllEFView(ML.Usuario usuarioOBJ)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities()) 
                {
                    var query = context.UsuarioGetAllViewSP(usuarioOBJ.Nombre, usuarioOBJ.ApellidoPaterno, usuarioOBJ.ApellidoMaterno,
                        usuarioOBJ.Rol.IdRol).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var usuarios in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.UserName = usuarios.UserName;
                            usuario.IdUsuario = usuarios.idUsuario;
                            usuario.Nombre = usuarios.NombreUsuario;
                            usuario.ApellidoPaterno = usuarios.ApellidoPaterno;
                            usuario.ApellidoMaterno = usuarios.ApellidoMaterno;
                            usuario.UserName = usuarios.UserName;
                            usuario.Email = usuarios.Email;
                            usuario.Password = usuarios.Password;
                            usuario.FechaNacimiento = usuarios.FechaNacimiento;
                            usuario.Sexo = usuarios.Sexo;
                            usuario.Telefono = usuarios.Telefono;
                            usuario.Celular = usuarios.Celular;
                            usuario.Estatus = usuarios.Estatus;
                            usuario.CURP = usuarios.CURP;
                            usuario.Imagen = usuarios.Imagen;
                            if (usuarios.IdRol == null)
                            {
                                usuario.Rol.IdRol = 0;
                            }
                            else
                            {
                                usuario.Rol.IdRol = usuarios.IdRol.Value;
                            }
                            usuario.Direccion.Calle = usuarios.Calle;
                            usuario.Direccion.NumeroInterior = usuarios.NumeroInterior;
                            usuario.Direccion.NumeroExterior = usuarios.NumeroExterior;
                            usuario.Direccion.Colonia.Nombre = usuarios.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = usuarios.CodigoPostal;
                            usuario.Direccion.Colonia.Municipio.Nombre = usuarios.NombreMunicipio;
                            result.Objects.Add(usuario);

                        }
                    }
                    result.Correct = true;
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