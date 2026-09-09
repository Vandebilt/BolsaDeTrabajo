using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaUsuario
    {
        //Crear
        public static Tuple<string, string> Crear(bdUsuario parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdRol", parametro.IdRol),
                    new SqlParameter("@Cuenta", parametro.Cuenta),
                    new SqlParameter("@Correo", parametro.Correo),
                    new SqlParameter("@Contrasenia", parametro.Contrasenia),
                    new SqlParameter("@UsuarioRegistro", parametro.UsuarioRegistro)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spUsuario", parametros);

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
        public static List<DataRow> Consultar(int? pIdUsuario, int? pIdRol, string pCuenta, string pCorreo)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdUsuario", pIdUsuario),
                new SqlParameter("@IdRol", pIdRol),
                new SqlParameter("@Cuenta", pCuenta),
                new SqlParameter("@Correo", pCorreo)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spUsuario", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdUsuario> Leer(int pIdUsuario)
        {
            List<bdUsuario> lista = new List<bdUsuario>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdUsuario", pIdUsuario)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spUsuario", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdUsuario
                         {
                             IdUsuario = Convert.ToInt32(fila["Id"]),
                             Correo = fila["Correo"] != DBNull.Value ? fila["Correo"].ToString() : string.Empty,

                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> Editar(bdUsuario parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdRol", parametro.IdRol),
                    new SqlParameter("@Correo", parametro.Correo),
                    new SqlParameter("@Contrasenia", parametro.Contrasenia),
                    new SqlParameter("@UsuarioModificacion", parametro.UsuarioModificacion)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spUsuario", parametros);

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
        public static Tuple<string, string> Eliminar(bdUsuario parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 5),
                    new SqlParameter("@IdUsuario", parametro.IdUsuario),
                    new SqlParameter("@UsuarioBaja", parametro.UsuarioBaja)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spUsuario", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        // Iniciar sesión
        public static List<bdUsuario> IniciarSesion(string pCredencial, string pContrasenia)
        {
            List<bdUsuario> lista = new List<bdUsuario>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 6),
                new SqlParameter("@Credencial", pCredencial),
                new SqlParameter("@Contrasenia", pContrasenia)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spUsuario", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdUsuario
                         {
                             IdUsuario = Convert.ToInt32(fila["Id"]),
                             IdEstatus = Convert.ToInt32(fila["IdEstatus"]),
                             IdRol = Convert.ToInt32(fila["IdRol"]),
                             IdSustentante = fila["IdSustentante"] != DBNull.Value ? Convert.ToInt32(fila["IdSustentante"]) : 0,
                             IdEmpresa = fila["IdEmpresa"] != DBNull.Value ? Convert.ToInt32(fila["IdEmpresa"]) : 0
                         }).ToList();
            }

            return lista;
        }

        //Exportar Microsoft Excel
        public static DataTable ExportarExcel(int? pIdRol, string pCorreo)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdRol", pIdRol),
                new SqlParameter("@Correo", pCorreo)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spUsuario", parametros);

            // Columnas que no serán mostradas en el archivo de Excel
            var columnasAEliminar = new List<string>
            {
                "Id"
            };

            // Elimina las columnas de la lista
            columnasAEliminar.ForEach(columna => tabla.Columns.Remove(columna));

            return tabla;
        }
    }
}
