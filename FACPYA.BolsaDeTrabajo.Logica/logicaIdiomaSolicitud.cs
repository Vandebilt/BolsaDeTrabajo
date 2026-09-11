using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaIdiomaSolicitud
    {
        //Crear
        public static Tuple<string, string> Crear(bdIdiomaSolicitud parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSolicitud", parametro.IdSolicitud),
                    new SqlParameter("@IdIdioma", parametro.IdIdioma),
                    new SqlParameter("@IdNivelIdioma", parametro.IdNivelIdioma)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spIdiomaSolicitud", parametros);

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

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spIdiomaSolicitud", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdIdiomaSolicitud> Leer(int pIdSolicitud)
        {
            List<bdIdiomaSolicitud> lista = new List<bdIdiomaSolicitud>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSolicitud", pIdSolicitud),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spIdiomaSolicitud", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdIdiomaSolicitud
                         {
                             IdIdiomaSolicitud = Convert.ToInt32(fila["Id"]),
                             IdIdioma = fila["IdIdioma"] != DBNull.Value ? Convert.ToInt32(fila["IdIdioma"]) : 0,
                             IdNivelIdioma = fila["IdNivelIdioma"] != DBNull.Value ? Convert.ToInt32(fila["IdNivelIdioma"]) : 0,
                             IdSolicitud = fila["IdSolicitud"] != DBNull.Value ? Convert.ToInt32(fila["IdSolicitud"]) : 0

                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdIdiomaSolicitud parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdIdiomaSolicitud", parametro.IdIdiomaSolicitud),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spIdiomaSolicitud", parametros);

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
