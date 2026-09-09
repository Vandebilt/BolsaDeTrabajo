using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaGradoSustentante
    {
        //Crear
        public static Tuple<string, string> Crear(bdGradoSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@IdCarrera", parametro.IdCarrera),
                    new SqlParameter("@IdSemestre", parametro.IdSemestre),
                    new SqlParameter("@IdTurnoEscolar", parametro.IdTurnoEscolar),
                    new SqlParameter("@OtraCarrera", parametro.OtraCarrera),
                    new SqlParameter("@Matricula", parametro.Matricula),
                    new SqlParameter("@Promedio", parametro.Promedio),

               };

                tabla = bdConexion.EjecutarStoredProcedure("spGradoSustentante", parametros);

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
        public static List<bdGradoSustentante> Leer(int pIdSustentante)
        {
            List<bdGradoSustentante> lista = new List<bdGradoSustentante>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdSustentante", pIdSustentante)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spGradoSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdGradoSustentante
                         {
                             IdGradoSustentante = Convert.ToInt32(fila["Id"]),
                             IdCarrera = Convert.ToInt32(fila["IdCarrera"]),
                             IdSemestre = Convert.ToInt32(fila["IdSemestre"]),
                             IdTurnoEscolar = Convert.ToInt32(fila["IdTurnoEscolar"]),
                             OtraCarrera = fila["OtraCarrera"] != DBNull.Value ? fila["OtraCarrera"].ToString() : string.Empty,
                             Matricula = fila["Matricula"] != DBNull.Value ? fila["Matricula"].ToString() : string.Empty,
                             Promedio = fila["Promedio"] != DBNull.Value ? Convert.ToDecimal(fila["Promedio"]) : 0.0m

                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> Editar(bdGradoSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 3),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@IdCarrera", parametro.IdCarrera),
                    new SqlParameter("@IdSemestre", parametro.IdSemestre),
                    new SqlParameter("@IdTurnoEscolar", parametro.IdTurnoEscolar),
                    new SqlParameter("@OtraCarrera", parametro.OtraCarrera),
                    new SqlParameter("@Matricula", parametro.Matricula),
                    new SqlParameter("@Promedio", parametro.Promedio)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spGradoSustentante", parametros);

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
