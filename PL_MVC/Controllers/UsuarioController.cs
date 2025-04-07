using Antlr.Runtime.Misc;
using BL;
using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

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

        // *********************************************************************************************************************
        /*
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
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        ML.Result resultDDL = BL.Rol.GetAllEF();
                        usuario.Rol.Roles = resultDDL.Objects;
                        usuario.Usuarios = usuarios.Objects;
                        return View(usuario);
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.MessageFalse = ex.Message;
                return PartialView("_Mensajes");
            }
        }
        */

        // *********************************************************************************************************************
        /*
        private ML.Result GetAllUsuarios(string xml)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            var xdoc = XDocument.Parse(xml);
            var objects = xdoc.Descendants("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}anyType");

            foreach (var elem in objects)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();

                usuario.ApellidoMaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);
                usuario.ApellidoPaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);

                usuario.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value) ?? string.Empty;

                usuario.Email = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value) ?? string.Empty;

                usuario.UserName = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value) ?? string.Empty;

                bool bandera = false;
                bool.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Estatus")?.Value, out bandera);
                usuario.Estatus = bandera;

                var direccion = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion");
                var colonia = direccion.Element("{http://schemas.datacontract.org/2004/07/ML}Colonia");
                var municipio = colonia.Element("{http://schemas.datacontract.org/2004/07/ML}Municipio");
                var rol = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol");

                usuario.Direccion.Calle = (string)(direccion.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty);
                usuario.Direccion.NumeroExterior = (string)(direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty);
                usuario.Direccion.NumeroInterior = (string)(direccion.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInterior")?.Value ?? string.Empty);
                usuario.Direccion.Colonia.Nombre = (string)(colonia.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                usuario.Direccion.Colonia.CodigoPostal = (string)(colonia.Element("{http://schemas.datacontract.org/2004/07/ML}CodigoPostal")?.Value ?? string.Empty);
                usuario.Direccion.Colonia.Municipio.Nombre = (string)(municipio.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                usuario.Rol.Nombre = (string)(rol.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                usuario.IdUsuario = int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out int idUsuario) ? idUsuario:0;
                result.Objects.Add(usuario);

            }
            return result;
        }
        */
        // *********************************************************************************************************************

        // *********************************************************************************************************************
        /*
        [NonAction]
        public string EscapeXml(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }
        */
        // *********************************************************************************************************************

        // *********************************************************************************************************************
        /*
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            string action = "";
            string url = "";
            string soapEnvelope = "";

            if (usuario.IdUsuario == 0)
            {

                //NEW USER
                action = "http://tempuri.org/IUserAdd/UsuarioAdd";
                url = "http://localhost:65169/UserAdd.svc";
                soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:UsuarioAdd>
        <tem:usuario>
            <ml:ApellidoMaterno>{EscapeXml(usuario.ApellidoMaterno)}</ml:ApellidoMaterno>
            <ml:ApellidoPaterno>{EscapeXml(usuario.ApellidoPaterno)}</ml:ApellidoPaterno>
            <ml:CURP>{EscapeXml(usuario.CURP)}</ml:CURP>
            <ml:Celular>{EscapeXml(usuario.Celular)}</ml:Celular>
            <ml:Direccion>
                <ml:Calle>{EscapeXml(usuario.Direccion.Calle)}</ml:Calle>
                <ml:Colonia>
                    <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                    <ml:Municipio>
                        <ml:Estado>
                            <ml:IdEstado>{usuario.Direccion.Colonia.Municipio.Estado.IdEstado}</ml:IdEstado>
                        </ml:Estado>
                        <ml:IdMunicipio>{usuario.Direccion.Colonia.Municipio.IdMunicipio}</ml:IdMunicipio>
                    </ml:Municipio>
                </ml:Colonia>
                <ml:NumeroExterior>{EscapeXml(usuario.Direccion.NumeroExterior)}</ml:NumeroExterior>
                <ml:NumeroInterior>{EscapeXml(usuario.Direccion.NumeroInterior)}</ml:NumeroInterior>
            </ml:Direccion>
           <ml:Email>{EscapeXml(usuario.Email)}</ml:Email>
           <ml:Estatus>{usuario.Estatus.ToString().ToLower()}</ml:Estatus>
           <ml:FechaNacimiento>{EscapeXml(usuario.FechaNacimiento)}</ml:FechaNacimiento>
           <ml:Imagen></ml:Imagen>
           <ml:Nombre>{EscapeXml(usuario.Nombre)}</ml:Nombre>
           <ml:Password>{EscapeXml(usuario.Password)}</ml:Password>
           <ml:Rol>
              <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
           </ml:Rol>
           <ml:Sexo>{EscapeXml(usuario.Sexo)}</ml:Sexo>
           <ml:Telefono>{EscapeXml(usuario.Telefono)}</ml:Telefono>
           <ml:UserName>{EscapeXml(usuario.UserName)}</ml:UserName>
        </tem:usuario>
      </tem:UsuarioAdd>
   </soapenv:Body>
</soapenv:Envelope>";
                ViewBag.MessageCorrect = "El usuario se arego correctamente";
            }
            else
            {
                //UPDATE USER
                DateTime fechaNacimiento = DateTime.ParseExact(usuario.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                action = "http://tempuri.org/IUserUpdate/UsuarioUpdate";
                url = "http://localhost:65169/UserUpdate.svc";
                soapEnvelope = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:UsuarioUpdate>
                         <tem:usuario>
                            <ml:ApellidoMaterno>{EscapeXml(usuario.ApellidoMaterno)}</ml:ApellidoMaterno>
                            <ml:ApellidoPaterno>{EscapeXml(usuario.ApellidoPaterno)}</ml:ApellidoPaterno>
                            <ml:CURP>{EscapeXml(usuario.CURP)}</ml:CURP>
                            <ml:Celular>{EscapeXml(usuario.Celular)}</ml:Celular>
                            <ml:Direccion>
                               <ml:Calle>{EscapeXml(usuario.Direccion.Calle)}</ml:Calle>
                               <ml:Colonia>
                                  <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                                  <ml:Municipio>
                                     <ml:Estado>
                                        <ml:IdEstado>{usuario.Direccion.Colonia.Municipio.Estado.IdEstado}</ml:IdEstado>
                                        <ml:Municipio/>
                                     </ml:Estado>
                                     <ml:IdMunicipio>{usuario.Direccion.Colonia.Municipio.IdMunicipio}</ml:IdMunicipio>
                                  </ml:Municipio>
                               </ml:Colonia>
                               <ml:NumeroExterior>{EscapeXml(usuario.Direccion.NumeroExterior)}</ml:NumeroExterior>
                               <ml:NumeroInterior>{EscapeXml(usuario.Direccion.NumeroInterior)}</ml:NumeroInterior>
                            </ml:Direccion>
                            <ml:Email>{EscapeXml(usuario.Email)}</ml:Email>
                            <ml:Estatus>{usuario.Estatus.ToString().ToLower()}</ml:Estatus>
                            <ml:FechaNacimiento>{EscapeXml(fechaNacimiento.ToString("yyyy-MM-dd"))}</ml:FechaNacimiento>
                            <ml:IdUsuario>{usuario.IdUsuario}</ml:IdUsuario>
                            <ml:Nombre>{EscapeXml(usuario.Nombre)}</ml:Nombre>
                            <ml:Password>{EscapeXml(usuario.Password)}</ml:Password>
                            <ml:Rol>
                               <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
                            </ml:Rol>
                            <ml:Sexo>{EscapeXml(usuario.Sexo)}</ml:Sexo>
                            <ml:Telefono>{EscapeXml(usuario.Telefono)}</ml:Telefono>
                            <ml:UserName>{EscapeXml(usuario.UserName)}</ml:UserName>
                         </tem:usuario>
                      </tem:UsuarioUpdate>
                   </soapenv:Body>
                </soapenv:Envelope>";
                ViewBag.MessageCorrect = "El usuario se actualizo correctamente";
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

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
                        return PartialView("_Mensajes");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ViewBag.MessageFalse = ex.Message;
            }
            return PartialView("_Mensajes");

        }
        */
        // *********************************************************************************************************************

        // *********************************************************************************************************************
        /*
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            string action = "http://tempuri.org/IGetById/UsuarioGetById";
            string url = "http://localhost:65169/GetById.svc";
            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
               <soapenv:Header/>
               <soapenv:Body>
                  <tem:UsuarioGetById>
                     <tem:IdUsuario>{IdUsuario}</tem:IdUsuario>
                  </tem:UsuarioGetById>
               </soapenv:Body>
            </soapenv:Envelope>";


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            // Enviar la solicitud
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
                        ML.Usuario usuario = new ML.Usuario();
                        string result = reader.ReadToEnd();
                        // Deserializar el usuario
                        ML.Result resultByID = GetById(result);
                        usuario = (ML.Usuario)resultByID.Object;

                        ML.Result resultDDL = BL.Rol.GetAllEF();
                        usuario.Rol.Roles = resultDDL.Objects;
                        ML.Result estadoDDL = BL.Estado.GetAllEF();
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = estadoDDL.Objects;
                        ML.Result resultDDLMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                        usuario.Direccion.Colonia.Municipio.Municipios = resultDDLMunicipio.Objects;
                        ML.Result resultDDLColonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                        usuario.Direccion.Colonia.Colonias = resultDDLColonias.Objects;

                        return View(usuario);
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ML.Usuario usuario1 = new ML.Usuario();
            usuario1.Rol = new ML.Rol();
            usuario1.Direccion = new ML.Direccion();
            usuario1.Direccion.Colonia = new ML.Colonia();
            usuario1.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario1.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            ML.Result resultDDL1 = BL.Rol.GetAllEF();
            usuario1.Rol.Roles = resultDDL1.Objects;
            ML.Result estadoDDL1 = BL.Estado.GetAllEF();
            usuario1.Direccion.Colonia.Municipio.Estado.Estados = estadoDDL1.Objects;
            return View(usuario1);
        }
        */
        // *********************************************************************************************************************

        // *********************************************************************************************************************
        /*
        private ML.Result GetById(string xml)
        {
            var xdoc = XDocument.Parse(xml);
            ML.Result result = new ML.Result();
            var usuarioElement = xdoc.Descendants().FirstOrDefault(e => e.Name.LocalName == "Object" && e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");

            if (usuarioElement != null)
            {
                ML.Usuario usuarioForm = new ML.Usuario();
                usuarioForm.Rol = new ML.Rol();
                usuarioForm.Direccion = new ML.Direccion();
                usuarioForm.Direccion.Colonia = new ML.Colonia();
                usuarioForm.Direccion.Colonia.Municipio = new ML.Municipio();
                usuarioForm.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                usuarioForm.ApellidoMaterno = (string)(usuarioElement.Element(XName.Get("ApellidoMaterno", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);
                usuarioForm.ApellidoPaterno = (string)(usuarioElement.Element(XName.Get("ApellidoPaterno", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);
                usuarioForm.CURP = (string)(usuarioElement.Element(XName.Get("CURP", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);
                usuarioForm.Celular = (string)(usuarioElement.Element(XName.Get("Celular", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);

                usuarioForm.Direccion.Calle = (string)(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("Calle", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);


                usuarioForm.Direccion.Colonia.IdColonia = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("Colonia", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("IdColonia", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idColonia)?idColonia : 0;


                usuarioForm.Direccion.Colonia.Municipio.Estado.IdEstado = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("Colonia", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("Municipio", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("Estado", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idEstado) ? idEstado : 0;

                usuarioForm.Direccion.Colonia.Municipio.IdMunicipio = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("Colonia", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("Municipio", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("IdMunicipio", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idMunicipio) ? idMunicipio : 0;

                usuarioForm.Direccion.IdDireccion = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("IdDireccion", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idDireccion)? idDireccion : 0;

                usuarioForm.Direccion.NumeroExterior = (string)(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("NumeroExterior", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);

                usuarioForm.Direccion.NumeroInterior = (string)(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("NumeroInterior", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);

                usuarioForm.Email = (string)(usuarioElement.Element(XName.Get("Email", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);
                usuarioForm.Estatus = bool.TryParse(usuarioElement.Element(XName.Get("Email", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out bool status) ? status:false;
                usuarioForm.FechaNacimiento = (string)(usuarioElement.Element(XName.Get("FechaNacimiento", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);
                usuarioForm.IdUsuario = int.TryParse(usuarioElement.Element(XName.Get("IdUsuario", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idUsuario) ? idUsuario : 0;
                usuarioForm.Nombre = (string)(usuarioElement.Element(XName.Get("Nombre", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);
                usuarioForm.Password = (string)(usuarioElement.Element(XName.Get("Password", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);
                usuarioForm.Rol.IdRol = int.TryParse(usuarioElement.Element(XName.Get("Rol", "http://schemas.datacontract.org/2004/07/ML")).Element(XName.Get("IdRol", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idRol) ? idRol : 0;
                usuarioForm.Sexo = (string)(usuarioElement.Element(XName.Get("Sexo", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);
                usuarioForm.Telefono = (string)(usuarioElement.Element(XName.Get("Telefono", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);
                usuarioForm.UserName = (string)(usuarioElement.Element(XName.Get("UserName", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);
                result.Object = usuarioForm;
            }
            return result;
        }
        */
        // *********************************************************************************************************************

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioREST"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);

                    var responseTask = cliente.GetAsync("GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<List<object>>();
                        readTask.Wait();                       

                        foreach (var resultItem in readTask.Result)
                        {
                            ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultUsuario);
                        }

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Usuarios = result.Objects;
                        ML.Result rolDDL = BL.Rol.GetAllEF();
                        usuario.Rol.Roles = rolDDL.Objects;
                        ML.Result estadoDDL = BL.Estado.GetAllEF();
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = estadoDDL.Objects;
                        usuario.Usuarios = result.Objects;
                        return View(usuario);
                    }
                }
            }
            catch(Exception ex) 
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Result result = new ML.Result();
            if (IdUsuario != null)
            {
                try
                {
                    using (HttpClient cliente = new HttpClient())
                    {
                        string endPoint = ConfigurationManager.AppSettings["UsuarioREST"].ToString();
                        cliente.BaseAddress = new Uri(endPoint);

                        var responseTask = cliente.GetAsync("GetById/" + IdUsuario );
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;
                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;
                            result.Correct = true;

                            ML.Result resultDDL = BL.Rol.GetAllEF();
                            resultItemList.Rol.Roles = resultDDL.Objects;
                            ML.Result estadoDDL = BL.Estado.GetAllEF();
                            resultItemList.Direccion.Colonia.Municipio.Estado.Estados = estadoDDL.Objects;
                            ML.Result resultDDLMunicipio = BL.Municipio.GetByIdEstado(resultItemList.Direccion.Colonia.Municipio.Estado.IdEstado);
                            resultItemList.Direccion.Colonia.Municipio.Municipios = resultDDLMunicipio.Objects;
                            ML.Result resultDDLColonias = BL.Colonia.GetByIdMunicipio(resultItemList.Direccion.Colonia.Municipio.IdMunicipio);
                            resultItemList.Direccion.Colonia.Colonias = resultDDLColonias.Objects;
                            return View(resultItemList);
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No hay ningun usuario con ese Id";
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                    result.Ex = ex;
                }
            }
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            ML.Result rolDDLUsuarioNuevo = BL.Rol.GetAllEF();
            usuario.Rol.Roles = rolDDLUsuarioNuevo.Objects;
            ML.Result estadoDDLUsuarioNuevo = BL.Estado.GetAllEF();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = estadoDDLUsuarioNuevo.Objects;
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
                Add(usuario);
            }
            else
            {
                Update(usuario);
            }
            return PartialView("_Mensajes");
        }

        [HttpPost]
        public ActionResult Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioREST"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);
                    var postTask = cliente.PostAsJsonAsync<ML.Usuario>("Add", usuario);
                    postTask.Wait();
                    var resultPost = postTask.Result;
                    if (resultPost.IsSuccessStatusCode)
                    {
                        return ViewBag.MessageCorrect = "El usuario se agrego correctamente";
                        
                    }
                    else
                    {
                        return ViewBag.MessageFalse = "El usuario no se agrego";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return PartialView("_Modal");
        }

        [HttpPost]
        public ActionResult Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result ();
            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioREST"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);
                    var postTask = cliente.PutAsJsonAsync<ML.Usuario>("Update", usuario);
                    postTask.Wait();
                    var resultPost = postTask.Result;
                    if (resultPost.IsSuccessStatusCode)
                    {
                        return ViewBag.MessageCorrect = "El usuario se agrego correctamente";
                        
                    }
                    else
                    {
                        return ViewBag.MessageFalse = "El usuario no se actualizo";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return PartialView("_Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioREST"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);
                    var postTask = cliente.GetAsync("Delete/" + idUsuario);
                    postTask.Wait();
                    var resultPost = postTask.Result;
                    if (resultPost.IsSuccessStatusCode)
                    {
                        ViewBag.MessageCorrect = "Usuario eliminado correctamente";
                        return PartialView("_Mensajes");
                    }
                    else
                    {
                        ViewBag.MessageFalse = "Usuario no se pudo eliminar";
                        return PartialView("_Mensajes");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return PartialView("_Model");
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
       
        /*public ActionResult Delete(int IdUsuario)
        {
            string action = "http://tempuri.org/IUserDelete/UsuarioDelete";
            string url = "http://localhost:65169/UserDelete.svc"; // Cambia a la URL del servicio
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            string soapEnvelope =
                $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                    <soapenv:Header/>
                        <soapenv:Body>
                        <tem:UsuarioDelete>
                        <!--Optional:-->
                        <tem:IdUsuario>{IdUsuario}</tem:IdUsuario>
                    </tem:UsuarioDelete>
                 </soapenv:Body>
                </soapenv:Envelope>";
            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())//cacha el soap
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);//almacena el xml
                stream.Write(content, 0, content.Length);//se envía 
            }

            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse()) //obtiene la respuesta
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))//lee la respuesta
                    {
                        string xml = reader.ReadToEnd();// se convierte a string
                        var xdoc = XDocument.Parse(xml);
                        // Acceder a GetUsuarioByIdResult usando el namespace correcto
                        var usuarioElement = xdoc.Descendants().FirstOrDefault(e =>
                            e.Name.LocalName == "Correct" &&
                            e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");
                        result.Correct = bool.Parse(usuarioElement.Value);
                        if (result.Correct)
                        {
                            ViewBag.MessageCorrect = "El registro se eliminó correctamente";
                            return PartialView("_Mensajes");
                        }
                        else
                        {
                            ViewBag.MessageFalse = "Hubo un error al eliminar el registro";
                            return View("_Mensajes");
                        }

                        // Asegúrate de que tu vista esté lista para recibir este objeto
                    }
                }
            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }

            return View(); // Devuelve la vista
        }
        */

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