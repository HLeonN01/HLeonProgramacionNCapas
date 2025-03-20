using ML;
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

            ML.Result result = BL.Usuario.GetAllEF(usuario);
            //ML.Result result = BL.Usuario.GetAllEFView(usuario);
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
            HttpPostedFileBase file = Request.Files["Imagen"];
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
            ML.Result result = BL.Usuario.UsuarioDireccionDelete(IdUsuario);
            if (result.Correct)
            {
                ViewBag.MessageCorrect = "Se elimino el usuario correctamente";
                return PartialView("_Mensajes");
            }
            else
            {
                ViewBag.MessageFalse = "No se pudo eliminar el usuario";
                return PartialView("_Mensajes");
            }
            //return RedirectToAction("GetAll");

        }

        [HttpPost]
        public ActionResult CargaMasivaExcel()
        {
            if (ModelState.IsValid)
            {
                if (Session["RutaExcel"] == null)
                {
                    HttpPostedFileBase excelUsuario = Request.Files["Excel"];
                    string extensionPermitida = ".xlsx";

                    if (excelUsuario.ContentLength > 0)
                    {
                        string extensionObtenida = Path.GetExtension(excelUsuario.FileName);
                        if (extensionObtenida == extensionPermitida)
                        {
                            string ruta = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(excelUsuario.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyHmmssff") + ".xlsx";

                            if (!System.IO.File.Exists(ruta))
                            {
                                excelUsuario.SaveAs(ruta);
                                string cadenaConecion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + ruta;
                                ML.Result resultExcel = BL.CargaMasiva.LeerExcel(cadenaConecion);

                                if (resultExcel.Objects.Count > 0)
                                {
                                    ML.ResultExcel resultValidacion = BL.CargaMasiva.ValidarExcel(resultExcel.Objects);
                                    if (resultValidacion.Errores.Count > 0)
                                    {
                                        //ERRORES
                                        ViewBag.ErroresExcel = resultValidacion.Errores;
                                        return PartialView("_Modal");
                                    }
                                    else
                                    {
                                        Session["RutaExcel"] = ruta;
                                        ViewBag.ErroresExcel = "El archivo que proporciono se valido con exito";
                                        return PartialView("_Modal");
                                    }
                                }
                            }
                            else
                            {
                                //VISTA PARCIAL
                                //vUELVE A CARGAR EL ARCHIVO
                                ViewBag.ErroresExcel = "Por favor vuelva a proporcionar su archivo";
                                return PartialView("_Modal");
                            }
                        }
                        else
                        {
                            //VISYTA PARCIAL 
                            //EL ARCHIVO NO ES UN EXCEL
                            ViewBag.ErroresExcel = "El archivo que proporciono no es un excel, intente con otro";
                            return PartialView("_Modal");
                        }
                    }
                    else
                    {
                        //VISTA PARCIAL 
                        //NO ME DISTE UN ARCHIVO
                        ViewBag.ErroresExcel = "No se proporciono ningun archivo excel";
                        return PartialView("_Modal");
                    }

                }
                else
                {
                    //ya lei y valide el excel
                    string cadenaConecion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + Session["RutaExcel"].ToString();
                    ML.Result resultLeer = BL.CargaMasiva.LeerExcel(cadenaConecion);
                    if (resultLeer.Objects.Count > 0)
                    {
                        //guardar el numero de fallidos y correctos
                        int fallidos = 0;
                        int correctos = 0;
                        //lista de usuarios fallidos y correctos
                        List<ML.Usuario> usuariosFallidos = new List<ML.Usuario>();
                        List<ML.Usuario> usuariosCorrectos = new List<ML.Usuario>();
                        //Lo leyo bien
                        foreach (ML.Usuario usuario in resultLeer.Objects)
                        {
                            ML.Result resultInsertar = BL.Usuario.AddEF(usuario);
                            if (!resultInsertar.Correct)
                            {
                                //Mostrar error
                                fallidos++;
                                usuariosFallidos.Add(usuario);
                            }
                            else
                            {
                                correctos++;
                                usuariosCorrectos.Add(usuario);
                            }
                        }
                        ViewBag.usuariosFallidos = usuariosFallidos;
                        ViewBag.usuariosCorrectos = usuariosCorrectos;
                        ViewBag.fallidos = fallidos;
                        ViewBag.correctos = correctos;

                        if (fallidos > 0)
                        {
                            ViewBag.ErroresExcel = "Se intento registrar " + resultLeer.Objects.Count + ", usuarios de los cuales fallaron " + fallidos + ", y se registraron " + correctos;

                            Session["RutaExcel"] = null;
                            return PartialView(viewName: "_Modal");
                        }
                        else
                        {
                            ViewBag.ErroresExcel = "Todos los registros se insertaron correctamente";

                            Session["RutaExcel"] = null;
                            return PartialView("_Modal");
                        }
                        //MOSTRAR ERRORES E INSERCIONES
                    }
                    else
                    {
                        ViewBag.ErroresExcel = "No se pudo insertar el archivo";

                        Session["RutaExcel"] = null;
                        return PartialView("_Modal");
                        //error
                    }
                }
            }
            else
            {

                ViewBag.ErroresExcel = "El archivo proporcionado contiene errores, reviselo";
                return PartialView("_Modal");
            }

            Session["RutaExcel"] = null;
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