using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FACPYA.BolsaDeTrabajo.Datos
{
    // Solo cadena de conexion, si se necesita ver detalles, editar o agregar una cadena de conexion nueva, ir al archivo
    // Web.config de FACPYA.Inventario.Presentacion

    // Nombre del ConnectionStrings a libre eleccion, si se desea cambiar afectar tambien al archivo web.config de Presentacion

    public class bdConexion
    {
        static private readonly string appConexion = ConfigurationManager.ConnectionStrings["bdConexion"].ConnectionString;

        // Se ejecuta stored con paramaetros
        public static DataTable EjecutarStoredProcedure(string storedProcedure, SqlParameter[] parametroSQL)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var conn = new SqlConnection(appConexion))
                {
                    conn.Open();
                    using (var comando = new SqlCommand(storedProcedure, conn))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddRange(parametroSQL);
                        using (var adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Se ejecuta stored sin parametros
        public static DataTable EjecutarStoredProcedure(string storedProcedure)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var conn = new SqlConnection(appConexion))
                {
                    conn.Open();
                    using (var comando = new SqlCommand(storedProcedure, conn))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        using (var adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(dt);
                        }
                    }
                }
                return dt;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
