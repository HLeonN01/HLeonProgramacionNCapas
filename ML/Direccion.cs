using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        public string Calle { get; set; }
        [DisplayName("Número Exterior")]
        [Required(ErrorMessage = "El número exterior es obligatorio")]
        public string NumeroInterior { get; set; }
        [DisplayName("Número Interior")]
        [Required(ErrorMessage = "La número interior es obligatorio")]
        public string NumeroExterior { get; set; }
        public string CodigoPostal { get; set; }
        public ML.Colonia Colonia { get; set; }
    }
}
