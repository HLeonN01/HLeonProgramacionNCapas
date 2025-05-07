using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ML
{
    public class Cita
    {
        public int IdCita { get; set; }
        [Required(ErrorMessage="La fecha es obligatoria")]
        public string FechaHora { get; set; }
        public ML.Piso Piso { get; set; }
        public ML.Candidato Candidato { get; set; } 
        [Required(ErrorMessage="El estatus de la cita es obligatorio")]
        public ML.EstatusCita EstatusCita { get; set; }
        public string Url { get; set; }
        public List<object> Citas { get; set; }
    }
}
