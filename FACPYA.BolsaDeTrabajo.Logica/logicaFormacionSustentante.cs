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
    public class logicaFormacionSustentante
    {
        //Crear
        public static Tuple<string, string> Crear(bdFormacionSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@IdTipoGrado", parametro.IdTipoGrado),
                    new SqlParameter("@IdCarrera", parametro.IdCarrera),
                    new SqlParameter("@IdEstatusAcademico", parametro.IdEstatusAcademico),
                    new SqlParameter("@IdEstatusTitulacion", parametro.IdEstatusTitulacion),
                    new SqlParameter("@OtraCarrera", parametro.OtraCarrera),
                    new SqlParameter("@AnioIngreso", parametro.AnioIngreso),
                    new SqlParameter("@AnioEgreso", parametro.AnioEgreso),
                    new SqlParameter("@Promedio", parametro.Promedio)
                    
               };

                tabla = bdConexion.EjecutarStoredProcedure("spFormacionSustentante", parametros);

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
        public static List<bdFormacionSustentante> Leer(int pIdSustentante)
        {
            List<bdFormacionSustentante> lista = new List<bdFormacionSustentante>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdSustentante", pIdSustentante)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spFormacionSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdFormacionSustentante
                         {
                             IdFormacionSustentante = Convert.ToInt32(fila["Id"]),
                             IdSustentante = Convert.ToInt32(fila["IdSustentante"]),
                             IdTipoGrado = Convert.ToInt32(fila["IdTipoGrado"]),
                             IdCarrera = Convert.ToInt32(fila["IdCarrera"]),
                             IdEstatusAcademico = Convert.ToInt32(fila["IdEstatusAcademico"]),
                             IdEstatusTitulacion = Convert.ToInt32(fila["IdEstatusTitulacion"]),
                             OtraCarrera = fila["OtraCarrera"].ToString(),
                             AnioIngreso = Convert.ToDateTime(fila["AnioIngreso"]),
                             AnioEgreso = Convert.ToDateTime(fila["AnioEgreso"]),
                             Promedio = Convert.ToDecimal(fila["Promedio"])
                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> Editar(bdFormacionSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 3),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@IdTipoGrado", parametro.IdTipoGrado),
                    new SqlParameter("@IdCarrera", parametro.IdCarrera),
                    new SqlParameter("@IdEstatusAcademico", parametro.IdEstatusAcademico),
                    new SqlParameter("@IdEstatusTitulacion", parametro.IdEstatusTitulacion),
                    new SqlParameter("@OtraCarrera", parametro.OtraCarrera),
                    new SqlParameter("@AnioIngreso", parametro.AnioIngreso),
                    new SqlParameter("@AnioEgreso", parametro.AnioEgreso),
                    new SqlParameter("@Promedio", parametro.Promedio)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spFormacionSustentante", parametros);

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
