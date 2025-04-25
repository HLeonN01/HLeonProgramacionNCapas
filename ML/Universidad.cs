using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ML
{
    public class Universidad
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public int IdUniversidad { get; set; }
        public string Nombre { get; set; }
        public List<object> Universidades { get; set; }
    }
}
