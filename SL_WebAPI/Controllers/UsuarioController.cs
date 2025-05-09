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
            ML.Usuario usuarioGetAllGetAll = new ML.Usuario();
            usuarioGetAllGetAll.Rol = new ML.Rol();
            usuarioGetAllGetAll.Nombre = "";
            usuarioGetAllGetAll.ApellidoPaterno = "";
            usuarioGetAllGetAll.ApellidoMaterno = "";
            usuarioGetAllGetAll.Rol.IdRol = 0;
            ML.Result result = BL.Usuario.GetAllEF(usuarioGetAllGetAll);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result.Objects);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.Correct);
            }
        }

        [HttpPost]
        [Route("GetAll")]
        public IHttpActionResult GetAll([FromBody]ML.Usuario usuarioGetAll)
        {

            usuarioGetAll.Nombre = usuarioGetAll.Nombre == null ? "" : usuarioGetAll.Nombre;
            usuarioGetAll.ApellidoPaterno = usuarioGetAll.ApellidoPaterno == null ? "" : usuarioGetAll.ApellidoPaterno;
            usuarioGetAll.ApellidoMaterno = usuarioGetAll.ApellidoMaterno == null ? "" : usuarioGetAll.ApellidoMaterno;
            usuarioGetAll.Rol.IdRol = usuarioGetAll.Rol.IdRol == 0 ? 0 : usuarioGetAll.Rol.IdRol;
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
                ML.Usuario usuarioGetAll = new ML.Usuario();
                usuarioGetAll = (ML.Usuario)result.Object;
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);

            }
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody]ML.Usuario usuarioGetAll)
        {
            ML.Result result = BL.Usuario.AddEF(usuarioGetAll);
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
        public IHttpActionResult Update([FromBody]ML.Usuario usuarioGetAllUpdate)
        {
            ML.Result result = BL.Usuario.UsuarioDireccionUpdate(usuarioGetAllUpdate);
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
