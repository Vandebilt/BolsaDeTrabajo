using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaCertificacionIdioma
    {
        //Crear
        public static Tuple<string, string> Crear(bdCertificacionIdioma parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@Descripcion", parametro.Descripcion),
                    new SqlParameter("@IdIdioma", parametro.IdIdioma),
                    new SqlParameter("@UsuarioRegistro", parametro.UsuarioRegistro)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spCertificacionIdioma", parametros);

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
        public static List<DataRow> Consultar(int? pIdEstatus, string pCertificacion, int? pIdIdioma)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdEstatus", pIdEstatus),
                new SqlParameter("@Descripcion", pCertificacion),
                new SqlParameter("@IdIdioma", pIdIdioma),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spCertificacionIdioma", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdCertificacionIdioma> Leer(int pIdCertificacionIdioma)
        {
            List<bdCertificacionIdioma> lista = new List<bdCertificacionIdioma>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdCertificacionIdioma", pIdCertificacionIdioma)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spCertificacionIdioma", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdCertificacionIdioma
                         {
                             IdCertificacionIdioma = Convert.ToInt32(fila["Id"]),
                             IdEstatus = Convert.ToInt32(fila["IdEstatus"]),
                             Descripcion = fila["Certificacion"].ToString(),
                             IdIdioma = Convert.ToInt32(fila["IdIdioma"]),

                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> Editar(bdCertificacionIdioma parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdCertificacionIdioma", parametro.IdCertificacionIdioma),
                    new SqlParameter("@Descripcion", parametro.Descripcion),
                    new SqlParameter("@IdIdioma", parametro.IdIdioma),
                    new SqlParameter("@UsuarioModificacion", parametro.UsuarioModificacion)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spCertificacionIdioma", parametros);

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
        public static Tuple<string, string> Eliminar(bdCertificacionIdioma parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 5),
                    new SqlParameter("@IdCertificacionIdioma", parametro.IdCertificacionIdioma),
                    new SqlParameter("@UsuarioBaja", parametro.UsuarioBaja)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spCertificacionIdioma", parametros);

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
        public static Tuple<string, string> Suspender(bdCertificacionIdioma parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 6),
                    new SqlParameter("@IdCertificacionIdioma", parametro.IdCertificacionIdioma),
                    new SqlParameter("@UsuarioModificacion", parametro.UsuarioModificacion)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spCertificacionIdioma", parametros);

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
        public static Tuple<string, string> Activar(bdCertificacionIdioma parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 7),
                    new SqlParameter("@IdCertificacionIdioma", parametro.IdCertificacionIdioma),
                    new SqlParameter("@UsuarioModificacion", parametro.UsuarioModificacion)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spCertificacionIdioma", parametros);

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
        public static DataTable ExportarExcel(int? pIdEstatus, string pCertificacion, int? pIdIdioma)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdEstatus", pIdEstatus),
                new SqlParameter("@Descripcion", pCertificacion),
                new SqlParameter("@IdIdioma", pIdIdioma),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spCertificacionIdioma", parametros);

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
