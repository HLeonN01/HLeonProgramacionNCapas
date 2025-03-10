using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        public bool Correct {  get; set; } //Para saber si el proceso se hizo correctamente o no 
        public string ErrorMessage { get; set; } //Para obtener el mensaje especifico del error
        public Exception Ex {  get; set; } //Traer el erro completo y especifico 
        public object Object { get; set; }//Para traer solo un objeto es decir un registro
        public List<object> Objects { get; set; } //Mostrar n objetos o registros


    }
}
