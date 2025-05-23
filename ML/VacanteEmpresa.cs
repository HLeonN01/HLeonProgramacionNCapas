using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class VacanteEmpresa
    {
        public int IdVacanteEmpresa { get; set; }
        public ML.Vacante Vacante { get; set; }
        public ML.Empresa Empresa { get; set; }
        public List<object> VacantesEmpresas { get; set; }
    }
}
