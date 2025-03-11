using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]//DECORADORES
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoMaterno = "";
            usuario.ApellidoPaterno = "";
            usuario.Rol.IdRol = 0;
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            //result.Correct = false;
            //cambio
            
            if (result.Correct)
            {
                //result.objects
                usuario.Usuarios = result.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }
            //cambio
            ML.Result resultDDL = BL.Rol.GetAllEF();
            usuario.Rol.Roles = resultDDL.Objects;
            return View(usuario);
        }

        //GET ALL CON PARAMETROS
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            //Ternario            
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;
            usuario.Rol.IdRol = usuario.Rol.IdRol == 0 ? 0 : usuario.Rol.IdRol;

            //ML.Result result = BL.Usuario.GetAllEF(usuario);
            ML.Result result = BL.Usuario.GetAllEFView(usuario);
            usuario.Usuarios = result.Objects;

            ML.Result resultDDL = BL.Rol.GetAllEF();
            usuario.Rol.Roles = resultDDL.Objects;
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {                  
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            if (IdUsuario == null)
            {
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            }
            else
            {
                //Si no es vacio es actualizar
                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                usuario = (ML.Usuario)result.Object;
                ML.Result resultDDLMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultDDLMunicipio.Objects;
                ML.Result resultDDLColonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultDDLColonias.Objects;
            }
            ML.Result resultDDL = BL.Rol.GetAllEF();
            usuario.Rol.Roles = resultDDL.Objects;

            ML.Result estadoDDL = BL.Estado.GetAllEF();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = estadoDDL.Objects;
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            HttpPostedFileBase file = Request.Files["ImagenUsuario"];
            if (file != null)
            {
                usuario.Imagen = ConvertirAArrayBytes(file);
            }
            if (usuario.IdUsuario == 0)
            {
                BL.Usuario.AddEF(usuario);
                return RedirectToAction("GetAll");
            }
            else
            {
                //BL.Usuario.UpdateEF(usuario);
                if (usuario.Direccion.IdDireccion != 0)
                {
                    BL.Usuario.UsuarioDireccionUpdate(usuario);
                }
                else
                {
                    BL.Usuario.UusarioSinDireccionUpdate(usuario);
                }
                return RedirectToAction("GetAll");
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            //BL.Usuario.Delete(IdUsuario);
            BL.Usuario.UsuarioDireccionDelete(IdUsuario);
            return RedirectToAction("GetAll");

        }

        [HttpPost]
        public JsonResult CambioEstatus(int IdUsuario, bool Estatus)
        {
            ML.Result JsonResult = BL.Usuario.CambioEstatus(IdUsuario, Estatus);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
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

        public byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;
        }
    }
}