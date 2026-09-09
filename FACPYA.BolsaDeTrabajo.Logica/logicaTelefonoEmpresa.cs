using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaTelefonoEmpresa
    {
        //Crear
        public static Tuple<string, string> Crear(bdTelefonoEmpresa parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdEmpresa", parametro.IdEmpresa),
                    new SqlParameter("@Telefono", parametro.Telefono),
                    new SqlParameter("@Extension", parametro.Extension)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spTelefonoEmpresa", parametros);

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
        public static List<DataRow> Consultar(int? pIdEmpresa)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdEmpresa", pIdEmpresa),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spTelefonoEmpresa", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdTelefonoEmpresa> Leer(int pIdEmpresa)
        {
            List<bdTelefonoEmpresa> lista = new List<bdTelefonoEmpresa>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdEmpresa", pIdEmpresa),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spTelefonoEmpresa", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdTelefonoEmpresa
                         {
                             IdTelefonoEmpresa = Convert.ToInt32(fila["Id"]),
                             Telefono = fila["Telefono"].ToString(),
                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdTelefonoEmpresa parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdTelefonoEmpresa", parametro.IdTelefonoEmpresa),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spTelefonoEmpresa", parametros);

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
