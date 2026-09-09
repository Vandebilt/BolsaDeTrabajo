using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaSustentanteTelefono
    {
        //Crear
        public static Tuple<string, string> Crear(bdTelefonoSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@IdTipoTelefono", parametro.IdTipoTelefono),
                    new SqlParameter("@Telefono", parametro.Telefono),
                    new SqlParameter("@Extension", parametro.Extension)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spTelefonoSustentante", parametros);

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
        public static List<DataRow> Consultar(int? pIdSustentante)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spTelefonoSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdTelefonoSustentante> Leer(int pIdSustentante)
        {
            List<bdTelefonoSustentante> lista = new List<bdTelefonoSustentante>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spTelefonoSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdTelefonoSustentante
                         {
                             IdTelefonoSustentante = Convert.ToInt32(fila["Id"]),
                             Telefono = fila["Telefono"].ToString(),
                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdTelefonoSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdTelefonoSustentante", parametro.IdTelefonoSustentante),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spTelefonoSustentante", parametros);

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
