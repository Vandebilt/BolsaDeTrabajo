using FACPYA.BolsaDeTrabajo.Datos;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaCodigo
    {
        // Código de autentificación de cuenta
        public static Tuple<string, string> EnviarCodigoAutentificacion(string pCorreo, string pUrlSitio)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@Correo", pCorreo),
                    new SqlParameter("@UrlSitio", pUrlSitio)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spCodigo", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        // Activación de cuenta de acceso
        public static Tuple<string, string> ActivarCuentaAcceso(string pToken, string pCodigo)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 2),
                    new SqlParameter("@Token", pToken),
                    new SqlParameter("@Codigo", pCodigo)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spCodigo", parametros);

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
