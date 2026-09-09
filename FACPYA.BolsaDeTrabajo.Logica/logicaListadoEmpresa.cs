using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaListadoEmpresa
    {

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
                         }).ToList();
            }

            return lista;
        }

        public static List<DataRow> Consultar(string pFolio, string pNombre, int? pIdTipoEmpresa)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@Folio", pFolio),
                new SqlParameter("@Nombre", pNombre),
                new SqlParameter("@IdTipoEmpresa", pIdTipoEmpresa),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spListadoEmpresa", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        //Exportar Microsoft Excel
        public static DataTable ExportarExcel(string pFolio, string pNombre, int? pIdTipoEmpresa)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@Folio", pFolio),
                new SqlParameter("@Nombre", pNombre),
                new SqlParameter("@IdTipoEmpresa", pIdTipoEmpresa),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spListadoEmpresa", parametros);

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
