using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        public int IdUsuario { get; set; }
        [DisplayName("Tipo de rol")]
        [Required(ErrorMessage = "El tipo de tol obligatorio")]
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        //sirve para llenar el dropdown list
        public List<object> Roles { get; set; }
    }
}
