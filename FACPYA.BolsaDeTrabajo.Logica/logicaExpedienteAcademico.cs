using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaExpedienteAcademico
    {
        //Crear
        public static Tuple<string, string> Crear(bdExpedienteAcademico parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@IdTipoSustentante", parametro.IdTipoSustentante),
                    new SqlParameter("@Matricula", parametro.Matricula),
                    new SqlParameter("@IdTipoGrado", parametro.IdTipoGrado),
                    new SqlParameter("@IdCarrera", parametro.IdCarrera),
                    new SqlParameter("@IdPlanEstudio", parametro.IdPlanEstudio),
                    new SqlParameter("@IdModalidad", parametro.IdModalidad),
                    new SqlParameter("@IdSemestre", parametro.IdSemestre),
                    new SqlParameter("@IdTurnoEscolar", parametro.IdTurnoEscolar),
                    new SqlParameter("@AnioIngreso", parametro.AnioIngreso),
                    new SqlParameter("@AnioEgreso", parametro.AnioEgreso),
                    new SqlParameter("@IdEstatusAcademico", parametro.IdEstatusAcademico),
                    new SqlParameter("@IdEstatusTitulacion", parametro.IdEstatusTitulacion),
                    new SqlParameter("@HasServicioSocial", parametro.HasServicioSocial),
                    new SqlParameter("@HasPracticasProfesionales", parametro.HasPracticasProfesionales),
                    new SqlParameter("@Promedio", parametro.Promedio),
                    new SqlParameter("@OtraCarrera", parametro.OtraCarrera),

               };

                tabla = bdConexion.EjecutarStoredProcedure("spExpedienteAcademico", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        // Leer
        public static List<bdExpedienteAcademico> Leer(int pIdSustentante)
        {
            List<bdExpedienteAcademico> lista = new List<bdExpedienteAcademico>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdSustentante", pIdSustentante)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spExpedienteAcademico", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdExpedienteAcademico
                         {
                             IdExpedienteAcademico = Convert.ToInt32(fila["Id"]),
                             IdSustentante = fila["IdSustentante"] != DBNull.Value ? Convert.ToInt32(fila["IdSustentante"]) : 0,
                             IdTipoSustentante = fila["IdTipoSustentante"] != DBNull.Value ? Convert.ToInt32(fila["IdTipoSustentante"]) : 0,
                             Matricula = fila["Matricula"] != DBNull.Value ? fila["Matricula"].ToString() : string.Empty,
                             IdEstatusAcademico = fila["IdEstatusAcademico"] != DBNull.Value ? Convert.ToInt32(fila["IdEstatusAcademico"]) : 0,
                             IdTipoGrado = fila["IdTipoGrado"] != DBNull.Value ? Convert.ToInt32(fila["IdTipoGrado"]) : 0,
                             IdCarrera = fila["IdCarrera"] != DBNull.Value ? Convert.ToInt32(fila["IdCarrera"]) : 0,
                             IdPlanEstudio = fila["IdPlanEstudio"] != DBNull.Value ? Convert.ToInt32(fila["IdPlanEstudio"]) : 0,
                             IdModalidad = fila["IdModalidad"] != DBNull.Value ? Convert.ToInt32(fila["IdModalidad"]) : 0,
                             IdSemestre = fila["IdSemestre"] != DBNull.Value ? Convert.ToInt32(fila["IdSemestre"]) : 0,
                             IdTurnoEscolar = fila["IdTurnoEscolar"] != DBNull.Value ? Convert.ToInt32(fila["IdTurnoEscolar"]) : 0,
                             AnioIngreso = fila["AnioIngreso"] != DBNull.Value ? Convert.ToDateTime(fila["AnioIngreso"]) : (DateTime?)null,
                             AnioEgreso = fila["AnioEgreso"] != DBNull.Value ? Convert.ToDateTime(fila["AnioEgreso"]) : (DateTime?)null,
                             IdEstatusTitulacion = fila["IdEstatusTitulacion"] != DBNull.Value ? Convert.ToInt32(fila["IdEstatusTitulacion"]) : 0,
                             HasServicioSocial = fila["HasServicioSocial"] != DBNull.Value ? Convert.ToBoolean(fila["HasServicioSocial"]) : false,
                             HasPracticasProfesionales = fila["HasPracticasProfesionales"] != DBNull.Value ? Convert.ToBoolean(fila["HasPracticasProfesionales"]) : false,
                             Promedio = fila["Promedio"] != DBNull.Value ? Convert.ToDecimal(fila["Promedio"]) : 0,
                             OtraCarrera = fila["OtraCarrera"] != DBNull.Value ? fila["OtraCarrera"].ToString() : string.Empty
                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> Editar(bdExpedienteAcademico parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 3),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@IdTipoSustentante", parametro.IdTipoSustentante),
                    new SqlParameter("@Matricula", parametro.Matricula),
                    new SqlParameter("@IdEstatusAcademico", parametro.IdEstatusAcademico),
                    new SqlParameter("@IdTipoGrado", parametro.IdTipoGrado),
                    new SqlParameter("@IdCarrera", parametro.IdCarrera),
                    new SqlParameter("@IdPlanEstudio", parametro.IdPlanEstudio),
                    new SqlParameter("@IdModalidad", parametro.IdModalidad),
                    new SqlParameter("@IdSemestre", parametro.IdSemestre),
                    new SqlParameter("@IdTurnoEscolar", parametro.IdTurnoEscolar),
                    new SqlParameter("@AnioIngreso", parametro.AnioIngreso),
                    new SqlParameter("@AnioEgreso", parametro.AnioEgreso),
                    new SqlParameter("@IdEstatusTitulacion", parametro.IdEstatusTitulacion),
                    new SqlParameter("@HasServicioSocial", parametro.HasServicioSocial),
                    new SqlParameter("@HasPracticasProfesionales", parametro.HasPracticasProfesionales),
                    new SqlParameter("@Promedio", parametro.Promedio),
                    new SqlParameter("@OtraCarrera", parametro.OtraCarrera)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spExpedienteAcademico", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }
    }
}

