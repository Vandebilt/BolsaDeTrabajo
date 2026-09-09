using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaSustentanteCorreo
    {
        //Crear
        public static Tuple<string, string> Crear(bdCorreoSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@IdTipoCorreo", parametro.IdTipoCorreo),
                    new SqlParameter("@Correo", parametro.Correo)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spCorreoSustentante", parametros);

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

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spCorreoSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdCorreoSustentante> Leer(int pIdSustentante)
        {
            List<bdCorreoSustentante> lista = new List<bdCorreoSustentante>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spCorreoSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdCorreoSustentante
                         {
                             IdCorreoSustentante = Convert.ToInt32(fila["Id"]),
                             IdSustentante = Convert.ToInt32(fila["IdSustentante"]),
                             IdTipoCorreo = Convert.ToInt32(fila["IdTipoCorreo"]),
                             Correo = fila["Correo"].ToString(),

                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdCorreoSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdCorreoSustentante", parametro.IdCorreoSustentante),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spCorreoSustentante", parametros);

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
