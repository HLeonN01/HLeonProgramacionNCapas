using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    public class CalculadoraController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Suma(int parametro1, int parametro2)
        {
            int suma = parametro1 + parametro2;
            return Content(HttpStatusCode.OK, suma);
        }

        [HttpPost]
        public IHttpActionResult Resta(int parametro3, int parametro4)
        {
            int resta = parametro3 - parametro4;
            return Content(HttpStatusCode.OK, resta);
        }

        [HttpPost]
        public IHttpActionResult Division(int parametro5, int parametro6)
        {
            int division = parametro5 / parametro6;
            return Content(HttpStatusCode.OK, division);
        }

        [HttpPost]
        public IHttpActionResult Multiplicacion(int parametro7, int parametro8)
        {
            int multiplicacion = parametro7 * parametro8;
            return Content(HttpStatusCode.OK, multiplicacion);
        }
    }
}
