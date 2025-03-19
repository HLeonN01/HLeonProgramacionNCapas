using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ResultExcel
    {
        public int NumeroRegistros { get; set; }
        public string ErrorMessage { get; set; }
        public List<object> Errores { get; set; } 
    }
}
