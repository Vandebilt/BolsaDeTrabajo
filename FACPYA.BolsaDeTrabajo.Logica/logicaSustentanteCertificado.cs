using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaSustentanteCertificado
    {
        //Crear
        public static Tuple<string, string> Crear(bdSustentanteCertificado parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@Descripcion", parametro.Descripcion),
                    new SqlParameter("@InstitucionEmisora", parametro.InstitucionEmisora),
                    new SqlParameter("@FechaEmision", parametro.FechaEmision),
                    new SqlParameter("@NumeroCertificado", parametro.NumeroCertificado),
                    new SqlParameter("@UrlVerificacion", parametro.UrlVerificacion),
                    new SqlParameter("@ArchivoCertificado", parametro.ArchivoCertificado),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spCertificadoSustentante", parametros);

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
        // Cambia el tipo de retorno a DataTable
        public static DataTable Consultar(int? pIdSustentante)
        {
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            // Simplemente devuelve la tabla
            DataTable tabla = bdConexion.EjecutarStoredProcedure("spCertificadoSustentante", parametros);
            return tabla;
        }

        // Leer
        public static List<bdSustentanteCertificado> Leer(string pIdCertificadoSustentante)
        {
            List<bdSustentanteCertificado> lista = new List<bdSustentanteCertificado>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdCertificadoSustentante", pIdCertificadoSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spCertificadoSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdSustentanteCertificado
                         {
                             IdCertificadoSustentante = fila["Id"].ToString(),
                             Descripcion = fila["Descripcion"].ToString(),
                             InstitucionEmisora = fila["InstitucionEmisora"].ToString(),
                             FechaEmision = Convert.ToDateTime(fila["FechaEmision"]),
                             NumeroCertificado = fila["NumeroCertificado"].ToString(),
                             UrlVerificacion = fila["UrlVerificacion"].ToString(),
                             ArchivoCertificado = fila["ArchivoCertificado"].ToString()
                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(string pIdCertificadoSustentante)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdCertificadoSustentante", pIdCertificadoSustentante),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spCertificadoSustentante", parametros);

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
