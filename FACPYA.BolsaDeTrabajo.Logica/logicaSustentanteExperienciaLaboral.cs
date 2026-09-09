using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaSustentanteExperienciaLaboral
    {
        //Crear
        public static Tuple<string, string> Crear(bdExperienciaLaboral parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@Puesto", parametro.Puesto),
                    new SqlParameter("@Empresa", parametro.Empresa),
                    new SqlParameter("@TipoTrabajo", parametro.TipoTrabajo),
                    new SqlParameter("@FechaInicio", parametro.FechaInicio),
                    new SqlParameter("@FechaFin", parametro.FechaFin),
                    new SqlParameter("@Descripcion", parametro.Descripcion),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spExperienciaLaboral", parametros);

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
            DataTable tabla = bdConexion.EjecutarStoredProcedure("spExperienciaLaboral", parametros);
            return tabla;
        }

        // Leer
        public static List<bdExperienciaLaboral> Leer(int pIdExperienciaLaboral)
        {
            List<bdExperienciaLaboral> lista = new List<bdExperienciaLaboral>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdExperienciaLaboral", pIdExperienciaLaboral),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spExperienciaLaboral", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdExperienciaLaboral
                         {
                             IdExperienciaLaboral = Convert.ToInt32(fila["Id"]),
                             Puesto = fila["Puesto"] != DBNull.Value ? fila["Puesto"].ToString() : string.Empty,
                             Empresa = fila["Empresa"] != DBNull.Value ? fila["Empresa"].ToString() : string.Empty,
                             TipoTrabajo = fila["TipoTrabajo"] != DBNull.Value ? fila["TipoTrabajo"].ToString() : string.Empty,
                             FechaInicio = fila["FechaInicio"] != DBNull.Value ? Convert.ToDateTime(fila["FechaInicio"]) : DateTime.MinValue,
                             FechaFin = fila["FechaFin"] != DBNull.Value ? Convert.ToDateTime(fila["FechaFin"]) : (DateTime?)null,
                             Descripcion = fila["Descripcion"] != DBNull.Value ? fila["Descripcion"].ToString() : string.Empty

                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> Editar(bdExperienciaLaboral parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdExperienciaLaboral", parametro.IdExperienciaLaboral),
                    new SqlParameter("@Puesto", parametro.Puesto),
                    new SqlParameter("@Empresa", parametro.Empresa),
                    new SqlParameter("@TipoTrabajo", parametro.TipoTrabajo),
                    new SqlParameter("@FechaInicio", parametro.FechaInicio),
                    new SqlParameter("@FechaFin", parametro.FechaFin),
                    new SqlParameter("@Descripcion", parametro.Descripcion),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spExperienciaLaboral", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdExperienciaLaboral parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 5),
                    new SqlParameter("@IdExperienciaLaboral", parametro.IdExperienciaLaboral),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spExperienciaLaboral", parametros);

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
