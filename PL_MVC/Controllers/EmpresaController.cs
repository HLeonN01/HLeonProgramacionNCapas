using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class EmpresaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Empresa.GetAll();
            ML.Empresa empresa = new ML.Empresa();
            empresa.Empresas = result.Objects;
            return View(empresa);
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            if (IdEmpresa > 0)
            {
                ML.Result update = BL.Empresa.GetById(IdEmpresa.Value);
                empresa = (ML.Empresa)update.Object;
            }
            return View(empresa);
        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            if (empresa.IdEmpresa == 0)
            {
                ML.Result add = BL.Empresa.Add(empresa);
            }
            else
            {
                ML.Result update = BL.Empresa.Update(empresa);
            }
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Delete(int IdEmpresa)
        {
            ML.Result delete = BL.Empresa.Delete(IdEmpresa);
            return RedirectToAction("GetAll");
        }
    }
}