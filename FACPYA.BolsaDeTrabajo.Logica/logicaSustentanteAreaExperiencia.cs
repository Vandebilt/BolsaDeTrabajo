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
    public class logicaSustentanteAreaExperiencia
    {
        //Crear
        public static Tuple<string, string> Crear(bdSustentanteAreaExperiencia parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdAreaInteres", parametro.IdAreaInteres),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@Anios", parametro.Anios),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spSustentanteAreaExperiencia", parametros);

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

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spSustentanteAreaExperiencia", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdSustentanteAreaExperiencia> Leer(int pIdSustentante)
        {
            List<bdSustentanteAreaExperiencia> lista = new List<bdSustentanteAreaExperiencia>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spSustentanteAreaExperiencia", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdSustentanteAreaExperiencia
                         {
                             IdSustentanteAreaExperiencia = Convert.ToInt32(fila["Id"]),
                             IdAreaInteres = fila["IdAreaInteres"] != DBNull.Value ? Convert.ToInt32(fila["IdAreaInteres"]) : 0,
                             IdSustentante = fila["IdSustentante"] != DBNull.Value ? Convert.ToInt32(fila["IdSustentante"]) : 0,
                             Anios = Convert.ToDecimal(fila["Anios"]),


                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdSustentanteAreaExperiencia parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdSustentanteAreaExperiencia", parametro.IdSustentanteAreaExperiencia),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spSustentanteAreaExperiencia", parametros);

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
