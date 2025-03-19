using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;

namespace BL
{
    public class CargaMasiva
    {
        public static ML.Result LeerExcel(string cadenaConecion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(cadenaConecion))
                {
                    string query = "SELECT * FROM[Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;

                        OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                        dataAdapter.SelectCommand = cmd;

                        DataTable tablaUsuario = new DataTable();
                        dataAdapter.Fill(tablaUsuario);
                        if (tablaUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Rol = new ML.Rol();
                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Colonia = new ML.Colonia();

                                usuario.UserName = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.FechaNacimiento = row[6].ToString();
                                usuario.Sexo = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.Estatus = row[10].ToString() == "1";
                                usuario.CURP = row[11].ToString();
                                //usuario.Imagen = null;
                                usuario.Rol.IdRol = Convert.ToInt16(row[12].ToString());
                                usuario.Direccion.Calle = row[13].ToString();
                                usuario.Direccion.NumeroInterior = row[14].ToString();
                                usuario.Direccion.NumeroExterior = row[15].ToString();
                                usuario.Direccion.Colonia.IdColonia = Convert.ToInt16(row[16].ToString());
                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
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


        public static ML.ResultExcel ValidarExcel(List<object> registros)
        {
            ML.ResultExcel result = new ML.ResultExcel();
            result.Errores = new List<object>();
            int contador = 1; //para el indice del registro
            foreach (ML.Usuario usuario in registros)
            {
                ML.ResultExcel errorRegistro = new ResultExcel();
                errorRegistro.ErrorMessage = "";
                errorRegistro.NumeroRegistros = contador;

                if (usuario.UserName.Length  > 50 || usuario.UserName == "" || usuario.UserName == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el username es mayor a 50 caracteres o es vacio ";
                }
                if (usuario.Nombre.Length  > 50 || usuario.UserName == "" || usuario.Nombre == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el nombre es mayor a 50 caracteres o es vacio";
                }
                if (usuario.ApellidoPaterno.Length > 50 || usuario.ApellidoPaterno == "" || usuario.ApellidoPaterno == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el apellido paterno es mayor a 50 caracteres o es vacio ";
                }
                if (usuario.ApellidoMaterno.Length > 50 || usuario.ApellidoMaterno == "" || usuario.ApellidoMaterno == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el apellido materno es mayor a 50 caracteres o es vacio ";
                }
                if (usuario.Email.Length > 30 || usuario.Email == "" || usuario.Email == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el email es mayor a 30 caracteres o es vacio ";
                }
                if (usuario.Password.Length > 50 || usuario.Password == "" || usuario.Password == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el password es mayor a 50 caracteres o es vacio ";
                }
                if (usuario.FechaNacimiento == "" || usuario.FechaNacimiento == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el formato de la fecha de nacimiento es incorrecta o es vacio ";
                }
                if (usuario.Sexo.Length > 1 || usuario.Sexo == "" || usuario.Sexo == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el sexo es mayor a un caracter o es vacio ";
                }
                if (usuario.Telefono.Length > 20 || usuario.Telefono == "" || usuario.Telefono == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el Telefono es mayor a 20 caracteres o es vacio ";
                }
                if (usuario.Celular.Length > 20 || usuario.Celular == "" || usuario.Celular == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el celular es mayor a 20 caracteres o es vacio ";
                }
                if (Convert.ToInt16(usuario.Estatus)  > 1  || usuario.Estatus.ToString() == "" || usuario.Estatus.ToString() == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el Estatus es mayor a 1 o es vacio ";
                }
                if (usuario.CURP.Length > 13 || usuario.CURP == "" || usuario.CURP == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el CURP es mayor a 13 caracteres o es vacio ";
                }
                if (usuario.Rol.IdRol > 3 || usuario.Rol.IdRol.ToString() == "" || usuario.Rol.IdRol.ToString() == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el IdRol es mayor a 3 o es vacio ";
                }
                if (usuario.Direccion.Calle.Length > 50 || usuario.Direccion.Calle == "" || usuario.Direccion.Calle == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} la calle es mayor a 50 caracteres o es vacio ";
                }
                if (usuario.Direccion.NumeroInterior.Length > 20 || usuario.Direccion.NumeroInterior == "" || usuario.Direccion.NumeroInterior == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el numero interior es mayor a 20 caracteres o es vacio ";
                }
                if (usuario.Direccion.NumeroExterior.Length > 20 || usuario.Direccion.NumeroExterior == "" || usuario.Direccion.NumeroExterior == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} el numero exterior es mayor a 20 caracteres o es vacio ";
                }
                if (usuario.Direccion.Colonia.IdColonia > 8390 || usuario.Direccion.Colonia.IdColonia.ToString() == "" || usuario.Direccion.Colonia.IdColonia.ToString() == null)
                {
                    errorRegistro.ErrorMessage += $"En el registro {contador} la IdColonia es mayor a 8390 o es vacio ";
                }

                //ERRORES
                if (errorRegistro.ErrorMessage != null && errorRegistro.ErrorMessage != "") 
                {
                    result.Errores.Add(errorRegistro);
                }
                contador++;
            }
            return result;
        }
    }
}
