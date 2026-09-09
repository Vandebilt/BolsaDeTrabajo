using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaHabilidad
    {
        //Crear
        public static Tuple<string, string> Crear(bdHabilidad parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdTipoHabilidad", parametro.IdTipoHabilidad),
                    new SqlParameter("@Descripcion", parametro.Descripcion),
                    new SqlParameter("@UsuarioRegistro", parametro.UsuarioRegistro)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spHabilidad", parametros);

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
        public static List<DataRow> Consultar(int? pIdEstatus, int? pIdTipoHabilidad ,string pHabilidad)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdEstatus", pIdEstatus),
                new SqlParameter("@IdTipoHabilidad", pIdTipoHabilidad),
                new SqlParameter("@Descripcion", pHabilidad),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spHabilidad", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdHabilidad> Leer(int pIdHabilidad)
        {
            List<bdHabilidad> lista = new List<bdHabilidad>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdHabilidad", pIdHabilidad)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spHabilidad", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdHabilidad
                         {
                             IdHabilidad = Convert.ToInt32(fila["Id"]),
                             IdEstatus = Convert.ToInt32(fila["IdEstatus"]),
                             IdTipoHabilidad = Convert.ToInt32(fila["IdTipoHabilidad"]),
                             Habilidad = fila["Habilidad"].ToString(),
                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> Editar(bdHabilidad parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdHabilidad", parametro.IdHabilidad),
                    new SqlParameter("@IdTipoHabilidad", parametro.IdTipoHabilidad),
                    new SqlParameter("@Descripcion", parametro.Descripcion),
                    new SqlParameter("@UsuarioModificacion", parametro.UsuarioModificacion)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spHabilidad", parametros);

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
        public static Tuple<string, string> Eliminar(bdHabilidad parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 5),
                    new SqlParameter("@IdHabilidad", parametro.IdHabilidad),
                    new SqlParameter("@UsuarioBaja", parametro.UsuarioBaja)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spHabilidad", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        //Suspender
        public static Tuple<string, string> Suspender(bdHabilidad parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 6),
                    new SqlParameter("@IdHabilidad", parametro.IdHabilidad),
                    new SqlParameter("@UsuarioModificacion", parametro.UsuarioModificacion)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spHabilidad", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        //Activar
        public static Tuple<string, string> Activar(bdHabilidad parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 7),
                    new SqlParameter("@IdHabilidad", parametro.IdHabilidad),
                    new SqlParameter("@UsuarioModificacion", parametro.UsuarioModificacion)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spHabilidad", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        //Exportar Microsoft Excel
        public static DataTable ExportarExcel(int? pIdEstatus, int? pIdTipoHabilidad, string pDescripcion)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdEstatus", pIdEstatus),
                new SqlParameter("@IdTipoHabilidad", pIdTipoHabilidad),
                new SqlParameter("@Descripcion", pDescripcion)

            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spHabilidad", parametros);

            // Columnas que no serán mostradas en el archivo de Excel
            var columnasAEliminar = new List<string>
            {
                "Id",
                "IdEstatus"
            };

            // Elimina las columnas de la lista
            columnasAEliminar.ForEach(columna => tabla.Columns.Remove(columna));

            return tabla;
        }
    }
}

