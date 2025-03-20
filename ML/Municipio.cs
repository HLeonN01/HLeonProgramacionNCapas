using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Municipio
    {
        [DisplayName("Municipio")]
        [Required(ErrorMessage = "El municipio es obligatorio")]
        public int IdMunicipio{ get;set; }
        public string Nombre { get; set; }
        public ML.Estado Estado { get; set; }
        public List<object> Municipios { get; set; }
    }
}
