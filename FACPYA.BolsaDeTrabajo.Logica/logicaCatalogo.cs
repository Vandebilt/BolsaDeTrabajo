using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.ddlEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaCatalogo
    {
        // DropDownList
        public static List<ddlCatalogo> ddlCargaCatalogo(int pAccion, int? pIdFiltro)
        {
            List<ddlCatalogo> lista = new List<ddlCatalogo>();
            DataTable tabla = new DataTable();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", pAccion),
                new SqlParameter("@IdFiltro", pIdFiltro)
            };

            tabla = bdConexion.EjecutarStoredProcedure("spCatalogo", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new ddlCatalogo
                         {
                             IdCatalogo = Convert.ToInt32(fila["IdCatalogo"]),
                             Descripcion = fila["Descripcion"].ToString()
                         }).ToList();
            }

            return lista;
        }
    }
}
