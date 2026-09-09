using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaArea
    {
        //Crear
        public static Tuple<string, string> Crear(bdArea parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@Descripcion", parametro.Descripcion),
                    new SqlParameter("@UsuarioRegistro", parametro.UsuarioRegistro)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spArea", parametros);

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
        public static List<DataRow> Consultar(int? pIdEstatus, string pDescripcion)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdEstatus", pIdEstatus),
                new SqlParameter("@Descripcion", pDescripcion),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spArea", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdArea> Leer(int pIdArea)
        {
            List<bdArea> lista = new List<bdArea>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdAreaInteres", pIdArea)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spArea", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdArea
                         {
                             IdAreaInteres = Convert.ToInt32(fila["Id"]),
                             Area = fila["Area"].ToString(),
                             IdEstatus = Convert.ToInt32(fila["IdEstatus"]),

                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> Editar(bdArea parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdAreaInteres", parametro.IdAreaInteres),
                    new SqlParameter("@Descripcion", parametro.Descripcion),
                    new SqlParameter("@UsuarioModificacion", parametro.UsuarioModificacion)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spArea", parametros);

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
        public static Tuple<string, string> Eliminar(bdArea parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 5),
                    new SqlParameter("@IdAreaInteres", parametro.IdAreaInteres),
                    new SqlParameter("@UsuarioBaja", parametro.UsuarioBaja)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spArea", parametros);

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
        public static Tuple<string, string> Suspender(bdArea parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 6),
                    new SqlParameter("@IdAreaInteres", parametro.IdAreaInteres),
                    new SqlParameter("@UsuarioModificacion", parametro.UsuarioModificacion)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spArea", parametros);

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
        public static Tuple<string, string> Activar(bdArea parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 7),
                    new SqlParameter("@IdAreaInteres", parametro.IdAreaInteres),
                    new SqlParameter("@UsuarioModificacion", parametro.UsuarioModificacion)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spArea", parametros);

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
        public static DataTable ExportarExcel(int? pIdEstatus, string pDescripcion)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdEstatus", pIdEstatus),
                new SqlParameter("@Descripcion", pDescripcion)

            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spArea", parametros);

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
