using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class CitaController : Controller
    {
        // GET: Cita
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Cita cita = new ML.Cita();
            cita.Candidato = new ML.Candidato();
            cita.Candidato.Vacante = new ML.Vacante();
            cita.Piso = new ML.Piso();
            ML.Result DDLVacante = BL.Vacante.GetAll();
            if (DDLVacante.Correct)
            {
                cita.Candidato.Vacante.Vacantes = DDLVacante.Objects;
            }
            else
            {
                cita.Candidato.Vacante.Vacantes = new List<object>();
            }
            cita.Citas = new List<object>();
            return View(cita);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Cita IdVacante)
        {
            ML.Cita cita = new ML.Cita();
            cita.Candidato = new ML.Candidato();
            cita.Candidato.Vacante = new ML.Vacante();
            IdVacante.Candidato.Vacante.IdVacante = IdVacante.Candidato.Vacante.IdVacante == 0 ? 0 : IdVacante.Candidato.Vacante.IdVacante;
            ML.Result DDLVacante = BL.Vacante.GetAll();
            cita.Candidato.Vacante.Vacantes = DDLVacante.Objects;

            ML.Result result = BL.Cita.GetAll(IdVacante.Candidato.Vacante.IdVacante);
            cita.Citas = result.Objects;
            return View(cita);
        }

        [HttpGet]
        public ActionResult Form(int? IdCandidato)
        {
            ML.Cita cita = new ML.Cita();
            cita.Piso = new ML.Piso();
            cita.Candidato = new ML.Candidato();
            cita.Candidato.Vacante = new ML.Vacante();
            cita.Candidato.Vacante.EstatusVacante = new ML.EstatusVacante();
            if (IdCandidato != null)
            {
                ML.Result getBiId = BL.Cita.GetById(IdCandidato.Value);
                cita = (ML.Cita)getBiId.Object;
            }
            ML.Result ddlPisos = BL.Piso.GetAll();
            cita.Piso.Pisos = ddlPisos.Objects;
            return View(cita);
        }
    }
}