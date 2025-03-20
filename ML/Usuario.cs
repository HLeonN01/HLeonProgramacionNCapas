using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [DisplayName("Nombre de usuario")]
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string UserName { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El nombre solo acepta letras")]
        public string Nombre { get; set; }

        [DisplayName("Apellido paterno")]
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El apellido paterno solo acepta letras")]
        public string ApellidoPaterno { get; set; }

        [DisplayName("Apellido materno")]
        [Required(ErrorMessage = "El apellido materno es obligatorio")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "El apellido materno solo acepta letras")]
        public string ApellidoMaterno { get; set; }

        [DisplayName("Correo")]
        [Required(ErrorMessage = "El correo electronico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electronico no tiene el formato correcto")]
        public string Email { get; set; }

        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
         ErrorMessage = "La contraseña debe tener mínimo 8 caracteres, una mayúscula y un número")]
        public string Password { get; set; }

        [DisplayName("Fecha de nacimiento")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public string FechaNacimiento { get; set; }

        [DisplayName("Genero")]
        [Required(ErrorMessage = "El genero es obligatorio")]
        public string Sexo { get; set; }

        [DisplayName("Número de Teléfono")]
        [Required(ErrorMessage = "El número de teléfono es obligatorio")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "El teléfono no permite letras")]
        public string Telefono { get; set; }

        [DisplayName("Número de celular")]
        [Required(ErrorMessage = "El número de celular es obligatorio")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "El celular no permite letras")]
        public string Celular { get; set; }

        public bool Estatus { get; set; }

        [DisplayName("CURP")]
        [Required(ErrorMessage = "El CURP es obligatorio")]
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$",
         ErrorMessage = "El CURP ingresado no es valido")]
        public string CURP { get; set; }

        [DisplayName("Imagen")]
        public byte[] Imagen { get; set; }

        public List<object> Usuarios { get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }
    }
}
