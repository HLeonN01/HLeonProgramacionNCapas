using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    [RoutePrefix("api")]
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Usuario usuarioGetAll = new ML.Usuario();
            usuarioGetAll.Rol = new ML.Rol();
            usuarioGetAll.Nombre = "";
            usuarioGetAll.ApellidoPaterno = "";
            usuarioGetAll.ApellidoMaterno = "";
            usuarioGetAll.Rol.IdRol = 0;
            ML.Result result = BL.Usuario.GetAllEF(usuarioGetAll);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result.Objects);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.Correct);
            }
        }

        [HttpGet]
        [Route("GetById/{IdUsuario}")]
        public IHttpActionResult GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            if (result.Correct)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario = (ML.Usuario)result.Object;
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);

            }
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody]ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);                
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
            
        }
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update([FromBody]ML.Usuario usuarioUpdate)
        {
            ML.Result result = BL.Usuario.UsuarioDireccionUpdate(usuarioUpdate);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpGet]
        [Route("Delete/{idUsuario}")]
        public IHttpActionResult Delete(int idUsuario)
        {
            ML.Result result = BL.Usuario.UsuarioDireccionDelete(idUsuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}
