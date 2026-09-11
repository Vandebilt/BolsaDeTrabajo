using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaSemestreSolicitud
    {
        //Crear
        public static Tuple<string, string> Crear(bdSemestreSolicitud parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSolicitud", parametro.IdSolicitud),
                    new SqlParameter("@IdSemestre", parametro.IdSemestre),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spSemestreSolicitud", parametros);

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

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spSemestreSolicitud", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdSemestreSolicitud> Leer(int pIdSolicitud)
        {
            List<bdSemestreSolicitud> lista = new List<bdSemestreSolicitud>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSolicitud", pIdSolicitud),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spSemestreSolicitud", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdSemestreSolicitud
                         {
                             IdSemestreSolicitud = Convert.ToInt32(fila["Id"]),
                             IdSemestre = fila["IdSemestre"] != DBNull.Value ? Convert.ToInt32(fila["IdSemestre"]) : 0,
                             IdSolicitud = fila["IdSolicitud"] != DBNull.Value ? Convert.ToInt32(fila["IdSolicitud"]) : 0

                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdSemestreSolicitud parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdSemestreSolicitud", parametro.IdSemestreSolicitud),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spSemestreSolicitud", parametros);

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
