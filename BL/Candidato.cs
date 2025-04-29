using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL_EF;
using ML;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.IO;

namespace BL
{
    public class Candidato
    {
        public static ML.Result GetAll(int IdVacante)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    var query = context.CandidatoGetAll(IdVacante).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var candidatos in query)
                        {
                            ML.Candidato candidato = new ML.Candidato();
                            candidato.Universidad = new ML.Universidad();
                            candidato.Carrera = new ML.Carrera();
                            candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                            candidato.Vacante = new ML.Vacante();
                            candidato.IdCandidato = candidatos.IdCandidato;
                            candidato.Nombre = candidatos.NombreCandidato;
                            candidato.ApellidoPaterno = candidatos.ApellidoPaterno;
                            candidato.ApellidoMaterno = candidatos.ApellidoMaterno;
                            candidato.Edad = candidatos.Edad;
                            candidato.Correo = candidatos.Correo;
                            candidato.Telefono = candidatos.Telefono;
                            candidato.Direccion = candidatos.Direccion;
                            if (candidatos.Foto == null)
                            {
                                candidato.Foto = "";
                            }
                            else
                            {
                                candidato.Foto = Convert.ToBase64String(candidatos.Foto);
                            }
                            if (candidatos.Curriculum == null)
                            {
                                candidato.Curriculum = "";
                            }
                            else
                            {
                                candidato.Curriculum = Convert.ToBase64String(candidatos.Curriculum);
                            }
                            candidato.Universidad.IdUniversidad = candidatos.IdUniversidad;
                            candidato.Universidad.Nombre = candidatos.NombreUniversidad;
                            candidato.Carrera.IdCarrera = candidatos.IdCarrera;
                            candidato.Carrera.Nombre = candidatos.Nombre;
                            candidato.BolsaTrabajo.IdBolsaTrabajo = candidatos.IdBolsaTrabajo;
                            candidato.BolsaTrabajo.Nombre = candidatos.NombreBolsa;
                            candidato.Vacante.IdVacante = candidatos.IdVacante;
                            candidato.Vacante.Nombre = candidatos.NombreVacante;
                            result.Objects.Add(candidato);
                        }
                        result.Correct = true;
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
                    var candidatos = context.CandidatoGetById(IdCandidato).SingleOrDefault();
                    if (candidatos != null)
                    {
                        ML.Candidato candidato = new ML.Candidato();
                        candidato.Universidad = new ML.Universidad();
                        candidato.Carrera = new ML.Carrera();
                        candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                        candidato.Vacante = new ML.Vacante();
                        candidato.IdCandidato = candidatos.IdCandidato;
                        candidato.Nombre = candidatos.NombreCandidato;
                        candidato.ApellidoPaterno = candidatos.ApellidoPaterno;
                        candidato.ApellidoMaterno = candidatos.ApellidoMaterno;
                        candidato.Edad = candidatos.Edad;
                        candidato.Correo = candidatos.Correo;
                        candidato.Telefono = candidatos.Telefono;
                        candidato.Direccion = candidatos.Direccion;
                        if (candidatos.Foto == null)
                        {
                            candidato.Foto = "";
                        }
                        else
                        {
                            candidato.Foto = Convert.ToBase64String(candidatos.Foto);
                        }
                        if (candidatos.Foto == null)
                        {
                            candidato.FotoArray = new byte[0];
                        }
                        else
                        {
                            candidato.FotoArray = candidatos.Foto;
                        }
                        if (candidatos.Curriculum == null)
                        {
                            candidato.CurriculumArray = new byte[0];
                        }
                        else
                        {
                            candidato.CurriculumArray = candidatos.Curriculum ?? new byte[0];
                        }
                        if (candidatos.Curriculum == null)
                        {
                            candidato.Curriculum = "";
                        }
                        else
                        {
                            candidato.Curriculum = Convert.ToBase64String(candidatos.Curriculum);
                        }
                        candidato.Universidad.IdUniversidad = candidatos.IdUniversidad;
                        candidato.Universidad.Nombre = candidatos.NombreUniversidad;
                        candidato.Carrera.IdCarrera = candidatos.IdCarrera;
                        candidato.Carrera.Nombre = candidatos.Nombre;
                        candidato.BolsaTrabajo.IdBolsaTrabajo = candidatos.IdBolsaTrabajo;
                        candidato.BolsaTrabajo.Nombre = candidatos.NombreBolsa;
                        candidato.Vacante.IdVacante = candidatos.IdVacante;
                        candidato.Vacante.Nombre = candidatos.NombreVacante;
                        result.Object = candidato;
                        result.Correct = true;
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

        public static ML.Result Add(ML.Candidato candidatoDB)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    int rowAffects = context.CandidatoAdd(candidatoDB.Nombre, candidatoDB.ApellidoPaterno, candidatoDB.ApellidoMaterno, candidatoDB.Edad, candidatoDB.Correo, candidatoDB.Telefono, candidatoDB.Direccion, candidatoDB.FotoArray, candidatoDB.CurriculumArray,candidatoDB.Universidad.IdUniversidad, candidatoDB.Carrera.IdCarrera, candidatoDB.BolsaTrabajo.IdBolsaTrabajo, candidatoDB.Vacante.IdVacante);
                    if (rowAffects > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agregar al candidato";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Candidato candidatoDB)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new DL_EF.HLeonProgramacionEnCapasEntities())
                {
                    int rowAffects = context.CandidatoUpdate(candidatoDB.IdCandidato,candidatoDB.Nombre, candidatoDB.ApellidoPaterno, candidatoDB.ApellidoMaterno, candidatoDB.Edad, candidatoDB.Correo, candidatoDB.Telefono, candidatoDB.Direccion, candidatoDB.FotoArray, candidatoDB.CurriculumArray, candidatoDB.Universidad.IdUniversidad, candidatoDB.Carrera.IdCarrera, candidatoDB.BolsaTrabajo.IdBolsaTrabajo, candidatoDB.Vacante.IdVacante);
                    if (rowAffects > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar al candidato";
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

        public static ML.Result Delete(int IdCandidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.HLeonProgramacionEnCapasEntities context = new HLeonProgramacionEnCapasEntities())
                {
                    int rowAffect = context.CandidatoDelete(IdCandidato);
                    if (rowAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "NO se elimino el candidato";
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
