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
            try
            {
                string correo = ConfigurationManager.AppSettings["Correo"].ToString();
                string password = ConfigurationManager.AppSettings["Password"].ToString();

                string body = "";

                string path = Server.MapPath("~/Content/Correo/Plantilla.html");

                StreamReader leer = new StreamReader(path);

                body = leer.ReadToEnd();

                body = body.Replace("{{NombreUsuario}}", "Hugo");
                body = body.Replace("{{TipoEntrevista}}", "Presencial");
                body = body.Replace("{{Fecha}}", "08/05/2025");
                body = body.Replace("{{Piso}}", "9");

                var smptpClient = new SmtpClient("smpt.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(correo, password),
                    EnableSsl = true
                };

                var mensaje = new MailMessage
                {
                    From = new MailAddress(correo, "Hugo"),
                    Subject = "Entrevista agendada",
                    Body = body,
                    IsBodyHtml = true
                };

                
                mensaje.To.Add("hugoln01@outlook.com");
                smptpClient.Send(mensaje);
            }
            catch(Exception ex)
            {
            }
            return View();
        }
    }
}