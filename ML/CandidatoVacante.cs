using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class CandidatoVacante
    {
        public int IdCandidatoVacante { get; set; }
        public ML.Candidato Candidato { get; set; }
        public ML.Vacante Vacante { get; set; }
        public List<object> CandidatosVacantes { get; set; }
    }
}
