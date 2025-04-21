using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ML;
using Newtonsoft.Json;

namespace PL_MVC.Controllers
{
    public class JavaScriptController : Controller
    {
        // GET: JavaScript
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoMaterno = "";
            usuario.ApellidoPaterno ="";
            usuario.Rol.IdRol = 0;
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Form(ML.Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.ImagenBase64))
            {
                usuario.Imagen = Convert.FromBase64String(usuario.ImagenBase64);
            }
            if (usuario.IdUsuario == 0)
            {
                ML.Result resultAdd = BL.Usuario.AddEF(usuario);
                return Json(resultAdd, JsonRequestBehavior.AllowGet);
            }
            ML.Result resultConDireccion = BL.Usuario.UsuarioDireccionUpdate(usuario);
            return Json(resultConDireccion, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.UsuarioDireccionDelete(IdUsuario);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllEstados()
        {
            ML.Result ddlEstados = BL.Estado.GetAllEF();
            return Json(ddlEstados, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetByIdEstado(int IdEstado)
        {
            ML.Result Jsonresult = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(Jsonresult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result JsonResult = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllRoles()
        {
            ML.Result JsonResult = BL.Rol.GetAllEF();
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        //public byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        //{
        //    System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
        //    byte[] data = reader.ReadBytes((int)Foto.ContentLength);
        //    return data;
        //}

        //public byte[] ConvertStringToByteArrayUsingCasting(string message)
        //{
        //    var byteArray = new byte[message.Length];
        //    for (int i = 0; i < message.Length; i++)
        //    {
        //        byteArray[i] = (byte)message[i];
        //    }
        //    return byteArray;
        //}
    }
}