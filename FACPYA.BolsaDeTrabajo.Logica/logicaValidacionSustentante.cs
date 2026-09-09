using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaValidacionSustentante
    {
        // ----- GESTION DE SUSTENTANTES -----
        // Leer
        public static List<bdValidacionSustentante> Leer(int pIdSustentante)
        {
            List<bdValidacionSustentante> lista = new List<bdValidacionSustentante>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 1),
                new SqlParameter("@IdSustentante", pIdSustentante)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spValidacionSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdValidacionSustentante
                         {
                             IdSustentante = Convert.ToInt32(fila["Id"]),
                             Nombre = fila["Nombre"].ToString(),
                             IdEstatus = Convert.ToInt32(fila["IdEstatus"]),

                         }).ToList();
            }

            return lista;
        }

        //Consultar Sustentantes
        public static List<DataRow> Consultar(int? pIdTipoSustentante, string pNombre, int? pIdTipoArchivo)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdTipoSustentante", pIdTipoSustentante),
                new SqlParameter("@Nombre", pNombre),
                new SqlParameter("@IdTipoArchivo", pIdTipoArchivo)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spValidacionSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        //Consultar
        public static List<DataRow> ConsultarDocumento(int? pIdSustentante, string pTipoArchivo)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spValidacionSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> UsuarioAsignacion(bdValidacionSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdUsuario", parametro.IdUsuario),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spValidacionSustentante", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        //Editar
        public static Tuple<string, string> UsuarioAsignacionNull(int pIdUsuario)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 5),
                    new SqlParameter("@IdUsuario", pIdUsuario),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spValidacionSustentante", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        // Leer
        public static List<bdValidacionSustentante> LeerUsuarioAsignacion(int pIdSustentante)
        {
            List<bdValidacionSustentante> lista = new List<bdValidacionSustentante>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 6),
                new SqlParameter("@IdSustentante", pIdSustentante)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spValidacionSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdValidacionSustentante
                         {
                             IdUsuarioAsignacion = fila["IdUsuarioAsignacion"] != DBNull.Value ? Convert.ToInt32(fila["IdUsuarioAsignacion"]) : 0,
                             IdArchivoSustentante = fila["IdArchivoSustentante"] != DBNull.Value ? Convert.ToInt32(fila["IdArchivoSustentante"]) : 0,

                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> ValidarDocumentos(bdValidacionSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 7),
                    new SqlParameter("@IdArchivoSustentante", parametro.IdArchivoSustentante),
                    new SqlParameter("@IdEstatus", parametro.IdEstatus),
                    new SqlParameter("@IdTipoRechazo", parametro.IdTipoRechazo),
                    new SqlParameter("@RetroAlimentacion", parametro.RetroAlimentacion),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spValidacionSustentante", parametros);

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
        public static DataTable ExportarExcel(int? pIdTipoSustentante, string pNombre, int? pIdTipoArchivo)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
               new SqlParameter("@Accion", 2),
                new SqlParameter("@IdTipoSustentante", pIdTipoSustentante),
                new SqlParameter("@Nombre", pNombre),
                new SqlParameter("@IdTipoArchivo", pIdTipoArchivo)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spValidacionSustentante", parametros);

            // Columnas que no serán mostradas en el archivo de Excel
            var columnasAEliminar = new List<string>
            {
                "Id",
                " "
            };

            // Elimina las columnas de la lista
            columnasAEliminar.ForEach(columna => tabla.Columns.Remove(columna));

            return tabla;
        }
    }
}
