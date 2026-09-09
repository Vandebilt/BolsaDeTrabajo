using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaListadoSustentante
    {

        public static List<bdValidacionSustentante> Leer(int pIdSustentante)
        {
            List<bdValidacionSustentante> lista = new List<bdValidacionSustentante>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 1),
                new SqlParameter("@IdSustentante", pIdSustentante)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spValidacionSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdValidacionSustentante
                         {
                             IdSustentante = Convert.ToInt32(fila["Id"]),
                             Nombre = fila["Nombre"].ToString(),
                         }).ToList();
            }

            return lista;
        }

        public static List<DataRow> Consultar(int? pIdTipoSustentante, string pNombre, string pFolio, int? pIdAreaExperiencia, string pAnios, int? pIdCarrera)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdTipoSustentante", pIdTipoSustentante),
                new SqlParameter("@Nombre", pNombre),
                new SqlParameter("@Folio", pFolio),
                new SqlParameter("@IdAreaExperiencia", pIdAreaExperiencia),
                new SqlParameter("@Anios", pAnios),
                new SqlParameter("@IdCarrera", pIdCarrera)

            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spListadoSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }
        //Exportar Microsoft Excel
        public static DataTable ExportarExcel(int? pIdTipoSustentante, string pNombre, string pFolio, int? pIdAreaExperiencia, string pAnios, int? pIdCarrera)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
               new SqlParameter("@Accion", 2),
                new SqlParameter("@IdTipoSustentante", pIdTipoSustentante),
                new SqlParameter("@Nombre", pNombre),
                new SqlParameter("@Folio", pFolio),
                new SqlParameter("@IdAreaExperiencia", pIdAreaExperiencia),
                new SqlParameter("@Anios", pAnios),
                new SqlParameter("@IdCarrera", pIdCarrera)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spListadoSustentante", parametros);

            // Columnas que no serán mostradas en el archivo de Excel
            var columnasAEliminar = new List<string>
            {
                "Id",
                "RutaImagenPerfil",
                "Folio1",
                "Estatus",
                "Carrera1",
                "Nombre",
                "PrimerApellido",
                "SegundoApellido",
                "Edad1",
                "Genero",
                "Municipio1",
                "Area",
                "Experiencia",
                "Sueldo Deseado1",
                "Horario Disponible",
                "Correo"
            };

            // Elimina las columnas de la lista
            columnasAEliminar.ForEach(columna => tabla.Columns.Remove(columna));

            return tabla;
        }

    }
}
