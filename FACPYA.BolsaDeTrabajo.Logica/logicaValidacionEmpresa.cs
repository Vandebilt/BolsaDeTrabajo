using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaValidacionEmpresa
    {
        // ----- GESTION DE EMPRESAS -----
        // Leer
        public static List<bdValidacionEmpresa> Leer(int pIdEmpresa)
        {
            List<bdValidacionEmpresa> lista = new List<bdValidacionEmpresa>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 1),
                new SqlParameter("@IdEmpresa", pIdEmpresa)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spValidacionEmpresa", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdValidacionEmpresa
                         {
                             IdEmpresa = Convert.ToInt32(fila["Id"]),
                             Nombre = fila["Nombre"].ToString(),
                             IdEstatus = Convert.ToInt32(fila["IdEstatus"]),
                             Tipo = fila["Tipo"].ToString(),
                             Giro = fila["Giro"].ToString(),
                             Tamanio = fila["Tamanio"].ToString(),
                             Direccion = fila["Direccion"].ToString(),
                             Correo = fila["Correo"].ToString(),
                             PaginaWeb = fila["PaginaWeb"].ToString(),
                             ContactoNombre = fila["ContactoNombre"].ToString(),
                             ContactoPuesto = fila["ContactoPuesto"].ToString(),
                             Mision = fila["Mision"].ToString(),
                             Vision = fila["Vision"].ToString(),
                             RegimenGastosMedicos = fila["RegimenGastosMedicos"].ToString()

                         }).ToList();
            }

            return lista;
        }

        //Consultar Sustentantes
        public static List<DataRow> Consultar(string pNombre, int? pIdTipoArchivo)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@Nombre", pNombre),
                new SqlParameter("@IdTipoArchivo", pIdTipoArchivo)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spValidacionEmpresa", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        //Consultar
        public static List<DataRow> ConsultarDocumento(int? pIdEmpresa, string pTipoArchivo)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdEmpresa", pIdEmpresa),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spValidacionEmpresa", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> UsuarioAsignacion(bdValidacionEmpresa parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdUsuario", parametro.IdUsuario),
                    new SqlParameter("@IdEmpresa", parametro.IdEmpresa),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spValidacionEmpresa", parametros);

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

                tabla = bdConexion.EjecutarStoredProcedure("spValidacionEmpresa", parametros);

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
        public static List<bdValidacionEmpresa> LeerUsuarioAsignacion(int pIdEmpresa)
        {
            List<bdValidacionEmpresa> lista = new List<bdValidacionEmpresa>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 6),
                new SqlParameter("@IdEmpresa", pIdEmpresa)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spValidacionEmpresa", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdValidacionEmpresa
                         {
                             IdUsuarioAsignacion = fila["IdUsuarioAsignacion"] != DBNull.Value ? Convert.ToInt32(fila["IdUsuarioAsignacion"]) : 0,
                             IdArchivoEmpresa = fila["IdArchivoEmpresa"] != DBNull.Value ? Convert.ToInt32(fila["IdArchivoEmpresa"]) : 0,

                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> ValidarDocumentos(bdValidacionEmpresa parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 7),
                    new SqlParameter("@IdArchivoEmpresa", parametro.IdArchivoEmpresa),
                    new SqlParameter("@IdEstatus", parametro.IdEstatus),
                    new SqlParameter("@IdTipoRechazo", parametro.IdTipoRechazo),
                    new SqlParameter("@RetroAlimentacion", parametro.RetroAlimentacion),
                    new SqlParameter("@RetroPerfil", parametro.RetroPerfil),
                    new SqlParameter("@IdUsuario", parametro.UsuarioRevision)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spValidacionEmpresa", parametros);

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
        public static DataTable ExportarExcel(string pNombre, int? pIdTipoArchivo)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
               new SqlParameter("@Accion", 2),
                new SqlParameter("@Nombre", pNombre),
                new SqlParameter("@IdTipoArchivo", pIdTipoArchivo)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spValidacionEmpresa", parametros);

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
