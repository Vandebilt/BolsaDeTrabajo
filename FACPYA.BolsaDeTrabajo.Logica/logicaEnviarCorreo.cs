using FACPYA.BolsaDeTrabajo.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaEnviarCorreo
    {
        // Recuperar contraseña
        public static Tuple<string, string> RecuperarContrasena(string pCorreo)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@Correo", pCorreo)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spEnviarCorreo", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        // Revisión de documentos
        public static Tuple<string, string> RevisionDocumentos(int? idEntidad, string pHtmlDetalle, int idRol)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();

                SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                // Se eliminó new SqlParameter("@v_IdRol", idRol) porque no existe en el SP
                new SqlParameter("@v_IdSustentante", (idRol == 2) ? (object)idEntidad ?? DBNull.Value : DBNull.Value),
                new SqlParameter("@v_IdEmpresa",     (idRol == 4) ? (object)idEntidad ?? DBNull.Value : DBNull.Value),
                new SqlParameter("@DetalleHtml", pHtmlDetalle)
            };

                tabla = bdConexion.EjecutarStoredProcedure("spEnviarCorreo", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.Message; // Preferible usar e.Message en lugar de e.ToString() para no saturar el UI
                tipoMensaje = "error";
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }
    }
}
