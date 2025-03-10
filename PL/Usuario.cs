using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Usuario
    {
        //-----------------------------------------------------------------------------------
        /*
        public static void Add()
        {
            //Inicializar el modelo
            ML.Usuario usuario = new ML.Usuario();
            //Solicitar que el usuario ingrese los parametros
            //Guardar lo que se ingresa en las propiedades del modelo
            Console.WriteLine("Ingrese el nombre del usuario");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese su correo");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese su contrasenia");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("Ingrese su telefono");
            usuario.Telfono = long.Parse(Console.ReadLine());

            //Una vez que se pasan los parametros a BL Llamar el metodo
            BL.Usuario.Add(usuario);
        }

        public static void Delete()
        {            
            Console.WriteLine("Ingrese el email del usuario que desea eliminar");
            int id = Convert.ToInt32(Console.ReadLine());

            BL.Usuario.Delete(id);
        }

        public static void Update()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Ingrese el id del usuario que desea modificar");
            usuario.Id = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el nuevo nombre");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el nuemo email");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese la nueva contrasenia");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo telefono");
            usuario.Telfono= long.Parse(Console.ReadLine());

            BL.Usuario.Update(usuario);
        }

        public static void GetAll()
        {
            ML.Result result = BL.Usuario.GetAll();
            if (result.Correct)
            {
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine();
                    Console.WriteLine("ID: " + usuario.Id);
                    Console.WriteLine("NOMBRE: " + usuario.Nombre);
                    Console.WriteLine("EMAIL: " + usuario.Email);
                    Console.WriteLine("PASSWORD: " + usuario.Password);
                    Console.WriteLine("TELEFONO: " + usuario.Telfono);
                }
            }
            else
            {
                Console.WriteLine("Hubo un error " + result.ErrorMessage);
            }
        }
        public static void GetUserById()
        {
            Console.WriteLine("Ingrese el ID del usuario que desea buscar");
            int id = Convert.ToInt32(Console.ReadLine());           
            ML.Result result = BL.Usuario.GetUserById(id);

            if (result.Correct)
            {
                ML.Usuario usuario =  (ML.Usuario)result.Object;
                Console.WriteLine("ID: " + usuario.Id);
                Console.WriteLine("NOMBRE: " + usuario.Nombre);
                Console.WriteLine("EMAIL: " + usuario.Email);
                Console.WriteLine("PASSWORD: " + usuario.Password);
                Console.WriteLine("TELEFONO: " + usuario.Telfono);
            }
            else
            {
                Console.WriteLine("Hubo un error " + result.ErrorMessage); 
            }
        }
        //------------------------------------------------------------------------------------
        */


        //**************************************************************************************
        
        public static void Add()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("INGRESE UN NUEVO USUARIO");
            Console.WriteLine("UserName: ");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("Nombre: ");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("ApellidoPaterno: ");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("ApellidoMaterno: ");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Email: ");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Password: ");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("FechaNacimiento: ");
            usuario.FechaNacimiento = Console.ReadLine();
            Console.WriteLine("Sexo: ");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("Telfono: ");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("Celular: ");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("Estatus: ");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("CURP: ");
            usuario.CURP = Console.ReadLine();
            Console.WriteLine("Imagen: ");
            string imagen = Console.ReadLine();
            usuario.Imagen = System.Text.Encoding.UTF8.GetBytes(imagen);
            Console.WriteLine("IdRol: ");
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());
            //ML.Result result = BL.Usuario.Add(usuario);
            //ML.Result result = BL.Usuario.AddEF(usuario);
            ML.Result result = BL.Usuario.AddLINQ(usuario);

            if (result.Correct)
            {
                Console.WriteLine("Se agrego correctamente al usuario");
            }
            else
            {
                Console.WriteLine("Error al guardar: " + result.ErrorMessage);
            }
        }

        public static void Update()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("INGRESE EL ID DEL USUARIO QUE DESEA MODIFICAR");
            Console.WriteLine("IdUsuario: ");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("UserName: ");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("Nombre: ");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("ApellidoPaterno: ");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("ApellidoMaterno: ");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Email: ");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Password: ");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("FechaNacimiento: ");
            usuario.FechaNacimiento = Console.ReadLine();
            Console.WriteLine("Sexo: ");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("Telfono: ");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("Celular: ");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("Estatus: ");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("CURP: ");
            usuario.CURP = Console.ReadLine();
            Console.WriteLine("Imagen: ");
            string imagen = Console.ReadLine();
            usuario.Imagen = System.Text.Encoding.UTF8.GetBytes(imagen);
            Console.WriteLine("IdRol: ");
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());
            //ML.Result result = BL.Usuario.Update(usuario);

            ML.Result result = BL.Usuario.UpdateLINQ(usuario);
            if (result.Correct)
            {
                Console.WriteLine("El usuario se actualizo correctamente");
            }
            else
            {
                Console.WriteLine("Error al actualizar " + result.ErrorMessage);
            }
        }

        public static void Delete()
        {
            Console.WriteLine("INGRESE EL ID DEL USUARIO QUE DESEA ELIMINAR");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            //ML.Result result = BL.Usuario.Delete(idUsuario);
            //ML.Result result = BL.Usuario.DeleteEF(IdUsuario);
            ML.Result result = BL.Usuario.DeleteLINQ(IdUsuario);
            if (result.Correct)
            {
                Console.WriteLine("El usuario se elimno correctamente");
            }
            else
            {
                Console.WriteLine("Error al eliminar " + result.ErrorMessage);
            }
        }

        public static void GetAll()
        {
            //ML.Result result = BL.Usuario.GetAllEF();
            //ML.Result result = BL.Usuario.GetAll();
            ML.Result result = BL.Usuario.GetAllLINQ();
            if (result.Correct)
            {
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine();
                    Console.WriteLine("Usuario: " + usuario.IdUsuario);
                    Console.WriteLine("Username: " + usuario.UserName);
                    Console.WriteLine("Nombre: " + usuario.Nombre);
                    Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
                    Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
                    Console.WriteLine("Email: " + usuario.Email);
                    Console.WriteLine("Password: " + usuario.Password);
                    Console.WriteLine("Fecha Nacimiento: " + usuario.FechaNacimiento);
                    Console.WriteLine("Sexo: " + usuario.Sexo);
                    Console.WriteLine("Telefono:  " + usuario.Telefono);
                    Console.WriteLine("Celular: " + usuario.Celular);
                    Console.WriteLine("Estatus: " + usuario.Estatus);
                    Console.WriteLine("Curp: " + usuario.CURP);
                    //Console.WriteLine("IMAGEN " + usuario.Imagen);
                    Console.WriteLine("IdRol: " + usuario.Rol.IdRol);
                }

            }
            else
            {
                Console.WriteLine("Error al obtener los usuario " + result.ErrorMessage);
            }
        }

        //UNBOXING
        public static void GetUsuarioById()
        {
            Console.WriteLine("Ingrese el ID del usuario que desea buscar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());
            //ML.Result result = BL.Usuario.GetById(IdUsuario);
            //ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            ML.Result result = BL.Usuario.GetByIdLINQ(IdUsuario);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                Console.WriteLine();
                Console.WriteLine("Usuario: " + usuario.IdUsuario);
                Console.WriteLine("Username: " + usuario.UserName);
                Console.WriteLine("Nombre: " + usuario.Nombre);
                Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
                Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
                Console.WriteLine("Email: " + usuario.Email);
                Console.WriteLine("Password: " + usuario.Password);
                Console.WriteLine("Fecha Nacimiento: " + usuario.FechaNacimiento);
                Console.WriteLine("Sexo: " + usuario.Sexo);
                Console.WriteLine("Telefono:  " + usuario.Telefono);
                Console.WriteLine("Celular: " + usuario.Celular);
                Console.WriteLine("Estatus: " + usuario.Estatus);
                Console.WriteLine("Curp: " + usuario.CURP);
                //Console.WriteLine("IMAGEN " + usuario.Imagen);
                Console.WriteLine("IdRol: " + usuario.Rol.IdRol);
            }
            else
            {
                Console.WriteLine("Error al encontrar el Usuario" + result.ErrorMessage);
            }
        }
    }
}
