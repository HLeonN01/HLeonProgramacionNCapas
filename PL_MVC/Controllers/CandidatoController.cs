using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class CandidatoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Candidato candidato = new ML.Candidato();
            candidato.Vacante = new ML.Vacante();
            ML.Result ddlVacante = BL.Vacante.GetAll();
            if (ddlVacante.Correct)
            {
                candidato.Vacante.Vacantes = ddlVacante.Objects;
            }
            else
            {
                candidato.Vacante.Vacantes = new List<object>();
            }
            candidato.Candidatos = new List<object>();
            return View(candidato);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Candidato IdVacante)
        {
            IdVacante.Vacante.IdVacante = IdVacante.Vacante.IdVacante == 0 ? 0 : IdVacante.Vacante.IdVacante;                          

            ML.Candidato candidato = new ML.Candidato();
            candidato.Vacante = new ML.Vacante();

            ML.Result result = BL.Candidato.GetAll(IdVacante.Vacante.IdVacante);
            candidato.Candidatos = result.Objects;
            
            ML.Result ddlVacante = BL.Vacante.GetAll();
            candidato.Vacante.Vacantes = ddlVacante.Objects;

            return View(candidato);
        }

        [HttpGet]
        public ActionResult Form(int? IdCandidato)
        {
            ML.Candidato candidato = new ML.Candidato();
            candidato.Universidad = new ML.Universidad();
            candidato.Carrera = new ML.Carrera();
            candidato.BolsaTrabajo = new ML.BolsaTrabajo();
            candidato.Vacante = new ML.Vacante();
            if (IdCandidato > 0)
            {
                ML.Result getByIdCandidato = BL.Candidato.GetById(IdCandidato.Value);         
                candidato = (ML.Candidato)getByIdCandidato.Object;
            }                       
            ML.Result ddlUniversidad = BL.Universidad.GetAll();
            ML.Result ddlCarrera = BL.Carrera.GetAll();
            ML.Result ddlBolsa = BL.BolsaTrabajo.GetAll();
            ML.Result ddlVacante = BL.Vacante.GetAll();
            candidato.Universidad.Universidades = ddlUniversidad.Objects;
            candidato.Carrera.Carreras = ddlCarrera.Objects;
            candidato.BolsaTrabajo.BolsasTrabajo = ddlBolsa.Objects;
            candidato.Vacante.Vacantes = ddlVacante.Objects;

            return View(candidato);
        }

        [HttpPost]
        public ActionResult Form(ML.Candidato candidato)
        {
            HttpPostedFileBase foto = Request.Files["FotoCont"];
            HttpPostedFileBase file = Request.Files["CurriculumCont"];

            if (foto != null && foto.ContentLength > 0)
            {
                candidato.FotoArray = ConvertirAArrayBytes(foto);
            }
            else if (!string.IsNullOrEmpty(Request["FotoBase64"]))
            {
                candidato.FotoArray = Convert.FromBase64String(Request["FotoBase64"]);
            }

            if (file != null && file.ContentLength > 0)
            {
                candidato.CurriculumArray = ConvertirAArrayBytes(file);
            }
            else if (!string.IsNullOrEmpty(Request["CurriculumBase64"]))
            {
                candidato.CurriculumArray = Convert.FromBase64String(Request["CurriculumBase64"]);
            }

            if (ModelState.IsValid)
            {
                if (candidato.IdCandidato == 0)
                { 
                    ML.Result addResult = BL.Candidato.Add(candidato);
                }
                else
                {
                    ML.Result updateResult = BL.Candidato.Update(candidato);
                }
            }
            else
            {
                ViewBag.Error = "Los datos proporcionados no son correctos, verifiquelos por favor!";
                candidato.Universidad = new ML.Universidad();
                candidato.Carrera = new ML.Carrera();
                candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                candidato.Vacante = new ML.Vacante();
                ML.Result ddlUniversidad = BL.Universidad.GetAll();
                ML.Result ddlCarrera = BL.Carrera.GetAll();
                ML.Result ddlBolsa = BL.BolsaTrabajo.GetAll();
                ML.Result ddlVacante = BL.Vacante.GetAll();
                candidato.Universidad.Universidades = ddlUniversidad.Objects;
                candidato.Carrera.Carreras = ddlCarrera.Objects;
                candidato.BolsaTrabajo.BolsasTrabajo = ddlBolsa.Objects;
                candidato.Vacante.Vacantes = ddlVacante.Objects;
                return View(candidato);
            }
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Delete(int IdCandidato)
        {
            ML.Result deleteResult = BL.Candidato.Delete(IdCandidato);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public FileResult DonwloadFile(int IdCandidato)
        {
            ML.Result result = BL.Candidato.GetById(IdCandidato);
            ML.Candidato candidato = new ML.Candidato();
            candidato = (ML.Candidato)result.Object;
            byte[] fileBytes = candidato.CurriculumArray;
            string fileName = candidato.Nombre + "_" + candidato.ApellidoPaterno + "_" + candidato.ApellidoMaterno + "_" + "CV.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;
        }

    }
}