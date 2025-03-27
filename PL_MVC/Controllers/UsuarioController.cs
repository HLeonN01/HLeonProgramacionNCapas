using BL;
using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {


        // *********************************************************************************************************************
        /*
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
            //ML.Result result = BL.Usuario.GetAllEF(usuario);
            //result.Correct = false;
            //cambio

            //if (result.Correct)
            //{
            //    //result.objects
            //    usuario.Usuarios = result.Objects;
            //}
            //else
            //{
            //    usuario.Usuarios = new List<object>();
            //}
            //cambio
            //return View(usuario);

            GetAllService.GetAllClient objeto = new GetAllService.GetAllClient();
            var usuarios = objeto.UsuarioGetAll(usuario);
            ML.Result resultDDL = BL.Rol.GetAllEF();
            usuario.Rol.Roles = resultDDL.Objects;

            if (usuarios.Correct)
            {
                usuario.Usuarios = usuarios.Objects.ToList();
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }
            //usuario.Rol.Roles = usuarios.Objects.ToList();
            return View(usuario);

        }
        */
        // *********************************************************************************************************************
        // *********************************************************************************************************************
        /*
        //GET ALL CON PARAMETROS
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            //Ternario            
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;
            usuario.Rol.IdRol = usuario.Rol.IdRol == 0 ? 0 : usuario.Rol.IdRol;

            GetAllService.GetAllClient objeto = new GetAllService.GetAllClient();
            var usuarios = objeto.UsuarioGetAll(usuario);
            //ML.Result result = BL.Usuario.GetAllEF(usuario);
            //ML.Result result = BL.Usuario.GetAllEFView(usuario);
            //usuario.Usuarios = result.Objects;
            ML.Result resultDDL = BL.Rol.GetAllEF();
            usuario.Rol.Roles = resultDDL.Objects;
            return View(usuario);
        }
        */
        // *********************************************************************************************************************
        // *********************************************************************************************************************
        /*
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
                GetByIdService.GetByIdClient objeto = new GetByIdService.GetByIdClient();
                var usuarios = objeto.UsuarioGetById(IdUsuario.Value);
                usuario = (ML.Usuario)usuarios.Object;
                ML.Result resultDDLMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultDDLMunicipio.Objects;
                ML.Result resultDDLColonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultDDLColonias.Objects;

                //ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                //usuario = (ML.Usuario)result.Object;
                //ML.Result resultDDLMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                //usuario.Direccion.Colonia.Municipio.Municipios = resultDDLMunicipio.Objects;
                //ML.Result resultDDLColonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                //usuario.Direccion.Colonia.Colonias = resultDDLColonias.Objects;
            }
            ML.Result resultDDL = BL.Rol.GetAllEF();
            usuario.Rol.Roles = resultDDL.Objects;

            ML.Result estadoDDL = BL.Estado.GetAllEF();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = estadoDDL.Objects;
            return View(usuario);
        }
        */
        // *********************************************************************************************************************
        // *********************************************************************************************************************
        /*
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            //HttpPostedFileBase file = Request.Files["Imagen"];
            //if (file != null)
            //{
            //    usuario.Imagen = ConvertirAArrayBytes(file);
            //}
            if (usuario.IdUsuario == 0)
            {
                //BL.Usuario.AddEF(usuario);
                UserAddService.UserAddClient objeto = new UserAddService.UserAddClient();
                var usuarioAdd = objeto.UsuarioAdd(usuario);
                if (usuarioAdd.Correct)
                {
                    ViewBag.MessageCorrect = "Se agrego el usuario correctamente";
                    return PartialView("_Mensajes");
                }
                else
                {
                    ViewBag.MessageCorrect = "No se agrego el usuario";
                    return PartialView("_Mensajes");
                }
                //return RedirectToAction("GetAll");
            }
            else
            {
                //BL.Usuario.UpdateEF(usuario);
                if (usuario.Direccion.IdDireccion != 0)
                {
                    //BL.Usuario.UsuarioDireccionUpdate(usuario);
                    UserUpdateService.UserUpdateClient objeto = new UserUpdateService.UserUpdateClient();
                    var usuarioUpdate = objeto.UsuarioUpdate(usuario);
                    if (usuarioUpdate.Correct)
                    {
                        ViewBag.MessageCorrect = "Se actualizo el usuario correctamente";
                        return PartialView("_Mensajes");
                    }
                    else
                    {
                        ViewBag.MessageCorrect = "No se actualizo el usuario";
                        return PartialView("_Mensajes");
                    }
                }
                else
                {
                    //BL.Usuario.UusarioSinDireccionUpdate(usuario);
                    UserUpdateService.UserUpdateClient objeto = new UserUpdateService.UserUpdateClient();
                    var usuarioUpdate = objeto.UsuarioUpdate(usuario);
                    if (usuarioUpdate.Correct)
                    {
                        ViewBag.MessageCorrect = "Se actualizo el usuario correctamente";
                        return PartialView("_Mensajes");
                    }
                    else
                    {
                        ViewBag.MessageCorrect = "No se actualizo el usuario";
                        return PartialView("_Mensajes");

                    }
                    //return RedirectToAction("GetAll");
                }
            }
        }
        */
        // *********************************************************************************************************************
        // *********************************************************************************************************************
        /*
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            //BL.Usuario.Delete(IdUsuario);
            //ML.Result result = BL.Usuario.UsuarioDireccionDelete(IdUsuario);
            UserDeleteService.UserDeleteClient objecto = new UserDeleteService.UserDeleteClient();
            var usuarioDelete = objecto.UsuarioDelete(IdUsuario);
            if (usuarioDelete.Correct)
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
        */
        // *********************************************************************************************************************

        [HttpGet]
        public ActionResult GetAll()
        {
            string action = "http://tempuri.org/IGetAll/UsuarioGetAll";
            string url = "http://localhost:65169/GetAll.svc";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
               <soapenv:Header/>
               <soapenv:Body>
                  <tem:UsuarioGetAll>
                    <tem:usuario>
                        <ml:ApellidoMaterno></ml:ApellidoMaterno>
                        <ml:ApellidoPaterno></ml:ApellidoPaterno>
                        <ml:Nombre></ml:Nombre>

                        <ml:Rol>
                           <ml:IdRol>0</ml:IdRol>
                        </ml:Rol>
           
                     </tem:usuario>

                  </tem:UsuarioGetAll>
               </soapenv:Body>
               </soapenv:Envelope>";

            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();

                        var usuarios = GetAllUsuarios(result); // Captura el objeto completo
                        
                        return View(usuarios);
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.MessageCorrect = ex.Message;
                return PartialView("_Mensajes");
            }
        }

        private ML.Usuario GetAllUsuarios(string xml)
        {
            var usuarioForm = new ML.Usuario();
            ML.Result result = new ML.Result();
            var xdoc = XDocument.Parse(xml);
            var objects = xdoc.Descendants("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}anyType");

            result.Objects = new List<object>();
            foreach (var elem in objects)
            {
                var usuario = new ML.Usuario();
                usuario.Direccion = new ML.Direccion();
                usuario.Rol = new ML.Rol();
                int idRol = 0;
                usuario.ApellidoMaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);                

                usuario.ApellidoPaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);

                usuario.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value) ?? string.Empty;
                var direccion = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion");
                var rol = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol");
                if (direccion != null)
                {
                    usuario.Direccion.Calle = (string)(direccion.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty);
                }
                if (direccion != null)
                {
                    usuario.Direccion.NumeroExterior = (string)(direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty);
                }

                if (direccion != null)
                {
                    usuario.Direccion.NumeroInterior = (string)(direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value ?? string.Empty);    
                }

                if (rol != null)
                {
                    int.TryParse(direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value, out idRol);
                    usuario.Rol.IdRol = idRol;
                }
                else
                {
                    usuario.Rol.IdRol = 0;
                }

                result.Objects.Add(usuario);

            }
            return usuarioForm;
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