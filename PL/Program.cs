using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int opcion = 0;
            int option = 0;
            /*do
            {
                Console.WriteLine("********************************************************");                
                Console.WriteLine("Seleccione la operacion para usuario");                
                Console.WriteLine("1. Agregar");
                Console.WriteLine("2. Eliminar");
                Console.WriteLine("3. Actualizar");
                Console.WriteLine("4. Mostrar un usuario por ID");
                Console.WriteLine("5. Mostrar todos los usuarios");
                Console.WriteLine("6. Salir");
                Console.WriteLine("********************************************************");
                opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Usuario.Add();
                        break;
                    case 2:
                        Usuario.Delete();
                        break;
                    case 3:
                        Usuario.Update();
                        break;
                    case 4:
                        Usuario.GetUsuarioById();
                        break;
                    case 5:
                        Usuario.GetAll();
                        break;
                }
            } while (opcion !=6);*/


            do
            {                
                Console.WriteLine("***********************************");
                Console.WriteLine("Seleccione su operacion");
                Console.WriteLine("1. Agregar");
                Console.WriteLine("2. Eliminar");
                Console.WriteLine("3. Actualizar");
                Console.WriteLine("4. Ver todos los usuarios");
                Console.WriteLine("5. Ver un solo usuario");
                Console.WriteLine("6. Salir");
                Console.WriteLine("***********************************");
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Rol.Add();
                        break;
                    case 2:
                        Rol.Delete();
                        break;
                    case 3:
                        Rol.Update();
                        break;
                    case 4:
                        Rol.GetAll();
                        break;
                    case 5:
                        Rol.GetById();
                        break;
                }
            } while (option != 6);

        }
    }
}
