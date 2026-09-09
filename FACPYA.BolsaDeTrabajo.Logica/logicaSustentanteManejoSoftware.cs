using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaSustentanteManejoSoftware
    {
        //Crear
        public static Tuple<string, string> Crear(bdSustentanteManejoSoftware parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@IdPaqueteSoftware", parametro.IdPaqueteSoftware),
                    new SqlParameter("@IdNivelSoftware", parametro.IdNivelSoftware),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spManejoSoftwareSustentante", parametros);

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

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spManejoSoftwareSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdSustentanteManejoSoftware> Leer(int pIdSustentante)
        {
            List<bdSustentanteManejoSoftware> lista = new List<bdSustentanteManejoSoftware>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spManejoSoftwareSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdSustentanteManejoSoftware
                         {
                             IdManejoSoftwareSustentante = Convert.ToInt32(fila["Id"]),
                             IdPaqueteSoftware = fila["IdPaqueteSoftware"] != DBNull.Value ? Convert.ToInt32(fila["IdPaqueteSoftware"]) : 0,
                             IdNivelSoftware = fila["IdNivelSoftware"] != DBNull.Value ? Convert.ToInt32(fila["IdNivelSoftware"]) : 0

                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdSustentanteManejoSoftware parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdManejoSoftwareSustentante", parametro.IdManejoSoftwareSustentante),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spManejoSoftwareSustentante", parametros);

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
