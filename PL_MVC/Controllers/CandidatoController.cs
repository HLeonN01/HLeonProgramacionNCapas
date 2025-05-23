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

            ML.CandidatoVacante candidato = new ML.CandidatoVacante();
            candidato.Candidato = new ML.Candidato();
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
            candidato.Candidato.Candidatos = new List<object>();
            return View(candidato);
        }
        [HttpPost]
        public ActionResult GetAll(ML.CandidatoVacante IdVacante)
        {
            IdVacante.Vacante.IdVacante = IdVacante.Vacante.IdVacante == 0 ? 0 : IdVacante.Vacante.IdVacante;                          

            ML.CandidatoVacante candidato = new ML.CandidatoVacante();
            candidato.Vacante = new ML.Vacante();
            candidato.Candidato = new ML.Candidato();
            ML.Result result = BL.Candidato.GetAll(IdVacante.Vacante.IdVacante);
            candidato.Candidato.Candidatos = result.Objects;
            
            ML.Result ddlVacante = BL.Vacante.GetAll();
            candidato.Vacante.Vacantes = ddlVacante.Objects;

            return View(candidato);
        }

        [HttpGet]
        public ActionResult Form(int? IdCandidato)
        {
            ML.CandidatoVacante candidato = new ML.CandidatoVacante();
            candidato.Candidato = new ML.Candidato();
            candidato.Candidato.Universidad = new ML.Universidad();
            candidato.Candidato.Carrera = new ML.Carrera();
            candidato.Candidato.BolsaTrabajo = new ML.BolsaTrabajo();
            candidato.Vacante = new ML.Vacante();
            if (IdCandidato > 0)
            {
                ML.Result getByIdCandidato = BL.Candidato.GetById(IdCandidato.Value);         
                candidato = (ML.CandidatoVacante)getByIdCandidato.Object;
            }                       
            ML.Result ddlUniversidad = BL.Universidad.GetAll();
            ML.Result ddlCarrera = BL.Carrera.GetAll();
            ML.Result ddlBolsa = BL.BolsaTrabajo.GetAll();
            ML.Result ddlVacante = BL.Vacante.GetAll();
            candidato.Candidato.Universidad.Universidades = ddlUniversidad.Objects;
            candidato.Candidato.Carrera.Carreras = ddlCarrera.Objects;
            candidato.Candidato.BolsaTrabajo.BolsasTrabajo = ddlBolsa.Objects;
            candidato.Vacante.Vacantes = ddlVacante.Objects;

            return View(candidato);
        }

        [HttpPost]
        public ActionResult Form(ML.CandidatoVacante candidatoVacante)
        {
            
            HttpPostedFileBase foto = Request.Files["FotoCont"];
            HttpPostedFileBase file = Request.Files["CurriculumCont"];            
            if (foto != null && foto.ContentLength > 0)
            {
                candidatoVacante.Candidato.FotoArray = ConvertirAArrayBytes(foto);
            }
            else if (!string.IsNullOrEmpty(Request["FotoBase64"]))
            {
                candidatoVacante.Candidato.FotoArray = Convert.FromBase64String(Request["FotoBase64"]);
            }

            if (file != null && file.ContentLength > 0)
            {
                candidatoVacante.Candidato.CurriculumArray = ConvertirAArrayBytes(file);
            }
            else if (!string.IsNullOrEmpty(Request["CurriculumBase64"]))
            {
                candidatoVacante.Candidato.CurriculumArray = Convert.FromBase64String(Request["CurriculumBase64"]);
            }

            if (ModelState.IsValid)
            {
                if (candidatoVacante.Candidato.IdCandidato == 0)
                { 
                    ML.Result addResult = BL.Candidato.Add(candidatoVacante);
                }
                else
                {
                    ML.Result updateResult = BL.Candidato.Update(candidatoVacante);
                }
            }
            else
            {
                ViewBag.Error = "Los datos proporcionados no son correctos, verifiquelos por favor!";
                candidatoVacante.Candidato.Universidad = new ML.Universidad();
                candidatoVacante.Candidato.Carrera = new ML.Carrera();
                candidatoVacante.Candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                candidatoVacante.Vacante = new ML.Vacante();
                ML.Result ddlUniversidad = BL.Universidad.GetAll();
                ML.Result ddlCarrera = BL.Carrera.GetAll();
                ML.Result ddlBolsa = BL.BolsaTrabajo.GetAll();
                ML.Result ddlVacante = BL.Vacante.GetAll();
                candidatoVacante.Candidato.Universidad.Universidades = ddlUniversidad.Objects;
                candidatoVacante.Candidato.Carrera.Carreras = ddlCarrera.Objects;
                candidatoVacante.Candidato.BolsaTrabajo.BolsasTrabajo = ddlBolsa.Objects;
                candidatoVacante.Vacante.Vacantes = ddlVacante.Objects;
                return View(candidatoVacante);
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