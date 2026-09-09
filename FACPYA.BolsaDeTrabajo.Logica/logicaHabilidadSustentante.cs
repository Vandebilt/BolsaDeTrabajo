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
    public class logicaHabilidadSustentante
    {
        //Crear
        public static Tuple<string, string> Crear(bdHabilidadSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdHabilidad", parametro.IdHabilidad),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spHabilidadSustentante", parametros);

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

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spHabilidadSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdHabilidadSustentante> Leer(int pIdSustentante)
        {
            List<bdHabilidadSustentante> lista = new List<bdHabilidadSustentante>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spHabilidadSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdHabilidadSustentante
                         {
                             IdHabilidadSustentante = Convert.ToInt32(fila["Id"]),
                             IdHabilidad = fila["IdHabilidad"] != DBNull.Value ? Convert.ToInt32(fila["IdHabilidad"]) : 0

                         }).ToList();
            }

            return lista;
        }

        //Eliminar
        public static Tuple<string, string> Eliminar(bdHabilidadSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdHabilidadSustentante", parametro.IdHabilidadSustentante),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spHabilidadSustentante", parametros);

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
