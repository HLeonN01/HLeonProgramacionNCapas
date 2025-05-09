using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace PL_MVC.Controllers
{
    public class CorreoController : Controller
    {
        // GET: Correo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Enviar()
        {
            ML.Result result = new ML.Result();
            try
            {
                string correo = ConfigurationManager.AppSettings["Correo"].ToString();
                string password = ConfigurationManager.AppSettings["Password"].ToString();
                int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["PuertoSMTP"].ToString());
                bool defaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["DefaultCredentialsSMTP"].ToString());
                bool isHtmlBody = Convert.ToBoolean(ConfigurationManager.AppSettings["IsHtmlBodySMTP"].ToString());

                string body = "";

                string path = Server.MapPath("~/Content/Correo/Plantilla.html");
                string pathImagen = Server.MapPath("~/Content/LOGO.png");
                StreamReader leer = new StreamReader(path);
                body = leer.ReadToEnd();

                body = body.Replace("{{NombreUsuario}}", "Hugo");
                body = body.Replace("{{TipoEntrevista}}", "Presencial");
                body = body.Replace("{{Fecha}}", "08/05/2025");
                body = body.Replace("{{Piso}}", "9");

                var smptClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(correo, password),
                    EnableSsl = true
                };

                var mensaje = new MailMessage
                {
                    From = new MailAddress(correo, "Hola Hugo"),
                    Subject = "Entrevista agendada",
                    Body = body,
                    IsBodyHtml = true
                };

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                LinkedResource imagen = new LinkedResource(pathImagen)
                {
                    ContentId = "imagen",
                    ContentType = new System.Net.Mime.ContentType("image/png")
                };


                htmlView.LinkedResources.Add(imagen);
                mensaje.AlternateViews.Add(htmlView);
                mensaje.To.Add("hugoleonnegrete@gmail.com");
                smptClient.Send(mensaje);
                ViewBag.MessageCorrect = "El correo se envio de manera correcta";
            }
            catch(Exception ex)
            {
                result.Correct = false; 
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
                ViewBag.MessageFalse = "El correo se envio de manera";
            }

            ViewBag.result = result;
            return PartialView("_Correo");
        }
    }
}