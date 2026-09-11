using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaExperienciaSolicitud
    {
        //Crear
        public static Tuple<string, string> Crear(bdExperienciaSolicitud parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSolicitud", parametro.IdSolicitud),
                    new SqlParameter("@IdAreaInteres", parametro.IdAreaInteres),
                    new SqlParameter("@Anios", (object)parametro.Anios ?? DBNull.Value),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spExperienciaSolicitud", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        //Consultar
        public static List<DataRow> Consultar(int? pIdSolicitud)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdSolicitud", pIdSolicitud),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spExperienciaSolicitud", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdExperienciaSolicitud> Leer(int pIdSolicitud)
        {
            List<bdExperienciaSolicitud> lista = new List<bdExperienciaSolicitud>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSolicitud", pIdSolicitud),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spExperienciaSolicitud", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdExperienciaSolicitud
                         {
                             IdExperienciaSolicitud = Convert.ToInt32(fila["Id"]),
                             IdAreaInteres = fila["IdAreaInteres"] != DBNull.Value ? Convert.ToInt32(fila["IdAreaInteres"]) : 0,
                             Anios = fila["Anios"] != DBNull.Value ? (decimal?)Convert.ToDecimal(fila["Anios"]) : null,
                             IdSolicitud = fila["IdSolicitud"] != DBNull.Value ? Convert.ToInt32(fila["IdSolicitud"]) : 0

                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdExperienciaSolicitud parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdExperienciaSolicitud", parametro.IdExperienciaSolicitud),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spExperienciaSolicitud", parametros);

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
