using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaEmpresaArchivo
    {
        //Crear
        public static Tuple<string, string> Crear(bdEmpresaArchivo parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdTipoArchivo", parametro.IdTipoArchivo),
                    new SqlParameter("@IdEmpresa", parametro.IdEmpresa),
                    new SqlParameter("@NombreDocumento", parametro.NombreDocumento),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spArchivoEmpresa", parametros);

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

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spArchivoEmpresa", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdEmpresaArchivo> Leer(int pIdEmpresa)
        {
            List<bdEmpresaArchivo> lista = new List<bdEmpresaArchivo>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdArchivoEmpresa", pIdEmpresa),
                new SqlParameter("@IdEmpresa", pIdEmpresa)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spArchivoEmpresa", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdEmpresaArchivo
                         {
                             IdArchivoEmpresa = Convert.ToInt32(fila["Id"]),
                             NombreDocumento = fila["NombreDocumento"].ToString(),
                             IdTipoArchivo = Convert.ToInt32(fila["IdTipoArchivo"]),
                             IdEstatus = Convert.ToInt32(fila["IdEstatus"])

                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdEmpresaArchivo parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdArchivoEmpresa", parametro.IdArchivoEmpresa),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spArchivoEmpresa", parametros);

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
