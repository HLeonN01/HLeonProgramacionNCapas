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
    public class Candidato
    {
        [Column("Idcandidato")]
        public int IdCandidato { get; set; }
        [DisplayName("Nombre: ")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El nombre solo acepta letras")]
        public string Nombre { get; set; }
        [DisplayName("Apellido paterno: ")]
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El apellido paterno solo acepta letras")]
        public string ApellidoPaterno { get; set; }
        [DisplayName("Apellido materno: ")]
        [Required(ErrorMessage = "El apellido materno es obligatorio")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El apellido materno solo acepta letras")]
        public string ApellidoMaterno { get; set; }
        [DisplayName("Edad: ")]
        [Required(ErrorMessage = "La edad es obligatoria")]
        public string Edad { get; set; }
        [DisplayName("Correo: ")]
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electronico no tiene el formato correcto")]
        public string Correo { get; set; }
        [DisplayName("Teléfono: ")]
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "El teléfono no tiene el formato correcto")]
        public string Telefono { get; set; }
        [DisplayName("Dirección: ")]
        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Direccion { get; set; }
        public byte[] FotoArray { get; set; }
        public byte[] CurriculumArray { get; set; }
        public string Foto { get; set; }
        public string Curriculum { get; set; }
        public ML.Universidad Universidad { get; set; }
        public ML.Carrera Carrera { get; set; }
        public ML.BolsaTrabajo BolsaTrabajo { get; set; }
        //public ML.Vacante Vacante { get; set; }
        public List<object> Candidatos { get; set; }
    }
}
