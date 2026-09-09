using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaSustentanteIdioma
    {
        //Crear
        public static Tuple<string, string> Crear(bdSustentanteIdioma parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdIdioma", parametro.IdIdioma),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@IdNivelIdioma", parametro.IdNivelIdioma)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spSustentanteIdioma", parametros);

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

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spSustentanteIdioma", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdSustentanteIdioma> Leer(int pIdSustentante)
        {
            List<bdSustentanteIdioma> lista = new List<bdSustentanteIdioma>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spSustentanteIdioma", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdSustentanteIdioma
                         {
                             IdSustentanteIdioma = Convert.ToInt32(fila["Id"]),
                             IdIdioma = fila["IdIdioma"] != DBNull.Value ? Convert.ToInt32(fila["IdIdioma"]) : 0,
                             IdSustentante = fila["IdSustentante"] != DBNull.Value ? Convert.ToInt32(fila["IdSustentante"]) : 0

                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdSustentanteIdioma parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdSustentanteIdioma", parametro.IdSustentanteIdioma),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spSustentanteIdioma", parametros);

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
