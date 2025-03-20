﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Estado
    {
        [DisplayName("Estado")]
        [Required(ErrorMessage = "El estado es obligatorio")]
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public List<object> Estados { get; set; }
        public ML.Municipio Municipio { get; set; }
    }
}
