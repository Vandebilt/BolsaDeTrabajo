using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaRuta
    {
        // Leer Ruta
        public static List<bdRuta> LeerRuta(int pAccion)
        {
            List<bdRuta> lista = new List<bdRuta>();
            DataTable tabla = new DataTable();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", pAccion)
            };
            tabla = bdConexion.EjecutarStoredProcedure("spRuta", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdRuta
                         {
                             Ruta = fila["Ruta"].ToString()
                         }).ToList();
            }
            return lista;
        }
    }
}
