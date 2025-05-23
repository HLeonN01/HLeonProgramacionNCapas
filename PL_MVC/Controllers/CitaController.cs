using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class CitaController : Controller
    {
       [HttpGet]
        public ActionResult GetAll()
        {
            ML.Cita cita = new ML.Cita();
            cita.Candidato = new ML.Candidato();
            cita.CandidatoVacante = new ML.CandidatoVacante();
            cita.CandidatoVacante.Vacante = new ML.Vacante();
            cita.Piso = new ML.Piso();
            ML.Result DDLVacante = BL.Vacante.GetAll();
            if (DDLVacante.Correct)
            {
                cita.CandidatoVacante.Vacante.Vacantes = DDLVacante.Objects;
            }
            else
            {
                cita.CandidatoVacante.Vacante.Vacantes = new List<object>();
            }
            cita.Citas = new List<object>();
            return View(cita);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Cita IdVacante)
        {
            ML.Cita cita = new ML.Cita();
            cita.Candidato = new ML.Candidato();
            cita.CandidatoVacante = new ML.CandidatoVacante();
            cita.CandidatoVacante.Vacante = new ML.Vacante();
            IdVacante.CandidatoVacante.Vacante.IdVacante = IdVacante.CandidatoVacante.Vacante.IdVacante == 0 ? 0 : IdVacante.CandidatoVacante.Vacante.IdVacante;
            ML.Result DDLVacante = BL.Vacante.GetAll();
            cita.CandidatoVacante.Vacante.Vacantes = DDLVacante.Objects;

            ML.Result result = BL.Cita.GetAll(IdVacante.CandidatoVacante.Vacante.IdVacante);
            cita.Citas = result.Objects;
            return View(cita);
        }

        [HttpGet]
        public ActionResult Form(int? IdCandidato)
        {
            ML.Cita cita = new ML.Cita();
            cita.Piso = new ML.Piso();
            cita.EstatusCita = new ML.EstatusCita();
            cita.Candidato = new ML.Candidato();
            cita.CandidatoVacante = new ML.CandidatoVacante();
            cita.CandidatoVacante.Vacante = new ML.Vacante();
            cita.CandidatoVacante.Vacante.EstatusVacante = new ML.EstatusVacante();
            if (IdCandidato != null)
            {
                ML.Result getBiId = BL.Cita.GetById(IdCandidato.Value);
                cita = (ML.Cita)getBiId.Object;
            }
            ML.Result ddlPisos = BL.Piso.GetAll();
            cita.Piso.Pisos = ddlPisos.Objects;
            ML.Result resultEstatusCitas = BL.Cita.EstatusCitaGetAll();
            cita.EstatusCita.EstatusCitas = resultEstatusCitas.Objects;
            return View(cita);            
        }

        [HttpPost]
        public ActionResult Form(ML.Cita cita)
        {
            if (cita.IdCita == 0)
            {
                cita.EstatusCita = new ML.EstatusCita();
                cita.EstatusCita.IdEstatusCita = 1;
                ML.Result add = BL.Cita.Add(cita);
            }
            else
            {
                ML.Result update = BL.Cita.Update(cita);
            }
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Delete(int IdCita)
        {
            ML.Result delete = BL.Cita.Delete(IdCita);
            return RedirectToAction("GetAll");
        }
    }
}