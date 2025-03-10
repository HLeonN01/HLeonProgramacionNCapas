using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Rol
    {
        public static void Add()
        {
            ML.Rol rol = new ML.Rol();
            Console.WriteLine("Ingrese el nombre del rol");
            rol.Nombre = Console.ReadLine();
            ML.Result result = BL.Rol.AddEF(rol);
            if (result.Correct)
            {
                Console.WriteLine("Se agrego el rol exitosamente");
            }
            else
            {
                Console.WriteLine("Error " + result.ErrorMessage);
            }
        }

        public static void Delete()
        {
            Console.WriteLine("Ingrese el ID del rol que desea eliminar");
            int IdRol = int.Parse(Console.ReadLine());
            ML.Result result = BL.Rol.DeleteEF(IdRol);
            if (result.Correct)
            {
                Console.WriteLine("Se elimino el rol");
            }
            else
            {
                Console.WriteLine("Error al eliminar " + result.ErrorMessage);
            }
        }

        public static void Update()
        {
            Console.WriteLine("Ingrese el ID del usuario que desea actualizar");
            int IdRol = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el nuevo nombre del rol");
            string Nombre = Console.ReadLine();
            ML.Result result = BL.Rol.UpdateEF(IdRol, Nombre);
            if (result.Correct)
            {
                Console.WriteLine("Se actualizo el rol exitosamente");
            }
            else
            {
                Console.WriteLine("Error al actualizar " + result.ErrorMessage);
            }
        }

        public static void GetAll()
        {
            ML.Result result = BL.Rol.GetAllEF();
            if (result.Correct)
            {
                foreach(ML.Rol rol in result.Objects)
                {
                    Console.WriteLine("ID: " + rol.IdRol);
                    Console.WriteLine("Nombre: " + rol.Nombre);
                }
            }
            else
            {
                Console.WriteLine("No hay registros" + result.ErrorMessage);
            }
        }

        public static void GetById()
        {
            Console.WriteLine("Ingrese el Id del rol que desea buscar");
            int IdRol = int.Parse(Console.ReadLine());
            ML.Result result = BL.Rol.GetById(IdRol);
            if (result.Correct)
            {
                ML.Rol rol = (ML.Rol)result.Object;
                Console.WriteLine("ID: " + rol.IdRol);
                Console.WriteLine("Nombre: " + rol.Nombre);
            }
            else
            {
                Console.WriteLine("Error al encontrar el Rol " + result.ErrorMessage);
            }
        }
    }
}