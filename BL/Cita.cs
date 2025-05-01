using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cita
    {
        public static ML.Result GetAll(int IdVacante)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from candidatosbd in context.Candidatoes
                                 join cita in context.Citas on candidatosbd.IdCandidato equals cita.IdCandidato into JoinCitaCandidato
                                 from citaCandidatos in JoinCitaCandidato.DefaultIfEmpty()
                                 join vacante in context.Vacantes on candidatosbd.Vacante.IdVacante equals vacante.IdVacante into JoinVacanteCandidato
                                 from vacanteCandidato in JoinVacanteCandidato.DefaultIfEmpty()
                                 join piso in context.Pisoes on citaCandidatos.IdPiso equals piso.IdPiso into JoinPisoCita
                                 from pisoCita in JoinPisoCita.DefaultIfEmpty()
                                 where vacanteCandidato.IdVacante == IdVacante
                                 select new
                                 {
                                     Vacantes = vacanteCandidato,
                                     Candidatos = candidatosbd,
                                     Citas = citaCandidatos,
                                     Pisos = pisoCita
                                 }).ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var citas in query)
                        {
                            ML.Cita Citabd = new ML.Cita();
                            Citabd.Candidato = new ML.Candidato();
                            Citabd.Candidato.Vacante = new ML.Vacante();
                            Citabd.Piso = new ML.Piso();
                            
                            if (citas.Citas != null && citas.Citas.IdCita > 0)
                            {
                                Citabd.IdCita = citas.Citas.IdCita;
                            }
                            else
                            {
                                Citabd.IdCita = 0;
                            }
                            if (citas.Candidatos != null && citas.Candidatos.Foto != null)
                            {
                                Citabd.Candidato.Foto = Convert.ToBase64String(citas.Candidatos.Foto);
                            }
                            else
                            {
                                Citabd.Candidato.Foto = "";
                            }
                            Citabd.Candidato.IdCandidato = citas.Candidatos.IdCandidato;
                            Citabd.Candidato.Nombre = citas.Candidatos.Nombre;
                            Citabd.Candidato.ApellidoPaterno = citas.Candidatos.ApellidoPaterno;
                            Citabd.Candidato.ApellidoMaterno = citas.Candidatos.ApellidoMaterno;
                            Citabd.Candidato.Edad = citas.Candidatos.Edad;
                            Citabd.Candidato.Correo = citas.Candidatos.Correo;
                            Citabd.Candidato.Telefono = citas.Candidatos.Telefono;
                            Citabd.Candidato.Vacante.Nombre = citas.Candidatos.Vacante.Nombre;
                            result.Objects.Add(Citabd);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay citas";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetById(int IdCandidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = (from candidatosbd in context.Candidatoes
                                 join cita in context.Citas on candidatosbd.IdCandidato equals cita.IdCandidato into JoinCitaCandidato
                                 from citaCandidatos in JoinCitaCandidato.DefaultIfEmpty()
                                 join vacante in context.Vacantes on candidatosbd.Vacante.IdVacante equals vacante.IdVacante into JoinVacanteCandidato
                                 from vacanteCandidato in JoinVacanteCandidato.DefaultIfEmpty()
                                 join piso in context.Pisoes on citaCandidatos.IdPiso equals piso.IdPiso into JoinPisoCita
                                 from pisoCita in JoinPisoCita.DefaultIfEmpty()
                                 join estatus in context.EstatusCitas on citaCandidatos.IdEstatusCita equals estatus.IdEstatusCita into JoinEstatusCita
                                 from estatusCitas in JoinEstatusCita.DefaultIfEmpty()
                                 where candidatosbd.IdCandidato == IdCandidato
                                 select new
                                 {
                                     Vacantes = vacanteCandidato,
                                     Candidatos = candidatosbd,
                                     Citas = citaCandidatos,
                                     Pisos = pisoCita,
                                     Estatus = estatusCitas
                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Cita cita = new ML.Cita();
                        cita.Candidato = new ML.Candidato();
                        cita.Candidato.Vacante = new ML.Vacante();
                        cita.Candidato.Vacante.EstatusVacante = new ML.EstatusVacante();
                        cita.EstatusCita = new ML.EstatusCita();
                        cita.Piso = new ML.Piso();

                        if (query.Citas != null && query.Citas.IdCita > 0)
                        {
                            cita.IdCita = query.Citas.IdCita;
                        }
                        else
                        {
                            cita.IdCita = 0;
                        }
                        if (query.Citas != null && query.Citas.FechaHora != null)
                        {
                            cita.FechaHora = query.Citas.FechaHora.ToString();
                        }
                        else
                        {
                            cita.FechaHora = "";
                        }
                        if (query.Pisos != null && query.Pisos.IdPiso > 0)
                        {
                            cita.Piso.IdPiso = query.Pisos.IdPiso;
                        }
                        else
                        {
                            cita.Piso.IdPiso = 0;
                        }
                        cita.Candidato.IdCandidato = query.Candidatos.IdCandidato;
                        if (query.Pisos != null && query.Pisos.Nombre != null)
                        {
                            cita.Piso.Nombre = query.Pisos.Nombre.ToString();
                        }
                        else
                        {
                            cita.Piso.Nombre = "";
                        }
                        if (query.Vacantes != null && query.Vacantes.UrlVacante != "")
                        {
                            cita.Candidato.Vacante.UrlVacante = query.Vacantes.UrlVacante;
                        }
                        else
                        {
                            cita.Candidato.Vacante.UrlVacante = "";
                        }
                        if (query.Estatus != null && query.Estatus.IdEstatusCita > 0)
                        {
                            cita.EstatusCita.IdEstatusCita = query.Estatus.IdEstatusCita;
                        }
                        else
                        {
                            cita.EstatusCita.IdEstatusCita = 0;
                        }
                        if (cita.EstatusCita != null && cita.EstatusCita.Nombre != null)
                        {
                            cita.EstatusCita.Nombre = query.Estatus.Nombre;
                        }
                        else
                        {
                            cita.EstatusCita.Nombre = "";
                        }
                        cita.Candidato.Nombre = query.Candidatos.Nombre;
                        cita.Candidato.ApellidoPaterno = query.Candidatos.ApellidoPaterno;
                        cita.Candidato.ApellidoMaterno = query.Candidatos.ApellidoMaterno; 
                        cita.Candidato.Correo = query.Candidatos.Correo;
                        cita.Candidato.Telefono = query.Candidatos.Telefono;
                        cita.Candidato.Vacante.IdVacante = query.Vacantes.IdVacante;
                        cita.Candidato.Vacante.Nombre = query.Vacantes.Nombre;
                        if (query.Candidatos != null && query.Candidatos.Foto != null)
                        {
                            cita.Candidato.Foto = Convert.ToBase64String(query.Candidatos.Foto);
                        }
                        else
                        {
                            cita.Candidato.Foto = "";
                        }
                        result.Object = cita;
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
