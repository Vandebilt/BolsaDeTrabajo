using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaMunicipioSolicitud
    {
        //Crear
        public static Tuple<string, string> Crear(bdMunicipioSolicitud parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSolicitud", parametro.IdSolicitud),
                    new SqlParameter("@IdMunicipio", parametro.IdMunicipio),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spMunicipioSolicitud", parametros);

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

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spMunicipioSolicitud", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdMunicipioSolicitud> Leer(int pIdSolicitud)
        {
            List<bdMunicipioSolicitud> lista = new List<bdMunicipioSolicitud>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSolicitud", pIdSolicitud),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spMunicipioSolicitud", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdMunicipioSolicitud
                         {
                             IdMunicipioSolicitud = Convert.ToInt32(fila["Id"]),
                             IdMunicipio = fila["IdMunicipio"] != DBNull.Value ? Convert.ToInt32(fila["IdMunicipio"]) : 0,
                             IdSolicitud = fila["IdSolicitud"] != DBNull.Value ? Convert.ToInt32(fila["IdSolicitud"]) : 0

                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdMunicipioSolicitud parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdMunicipioSolicitud", parametro.IdMunicipioSolicitud),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spMunicipioSolicitud", parametros);

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
