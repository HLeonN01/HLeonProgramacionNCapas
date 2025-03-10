using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        //sirve para llenar el dropdown list
        public List<object> Roles { get; set; }
    }
}
