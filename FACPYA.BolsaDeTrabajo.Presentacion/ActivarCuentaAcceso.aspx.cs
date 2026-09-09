using FACPYA.BolsaDeTrabajo.Logica;
using System;
using System.Web;
using System.Web.UI;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class ActivarCuentaAcceso : Page
    {
        #region Métodos
        private void MostrarMensaje(string mensaje, string tipo)
        {
            ClientScript.RegisterStartupScript(GetType(), "mostrarMensaje", "mostrarMensaje('" + mensaje + "', '" + tipo + "')", true);
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // Activación de cuenta por medio la verificación de token y código
        protected void btnActivarCuenta_Click(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            string codigo = txtCodigo.Text.Trim();

            // La acción sólo se dispara si existe un token
            // NOTA: problemas relacionados con el token no se le muestran al usuario
            if (!string.IsNullOrEmpty(token))
            {
                if (!string.IsNullOrEmpty(codigo))
                {
                    Tuple<string, string> control;
                    control = logicaCodigo.ActivarCuentaAcceso(token, codigo);

                    MostrarMensaje(control.Item1, control.Item2);

                    if (control.Item2 == "success")
                        // Hacer una pausa antes de redirigir
                        Response.Write("<script type='text/javascript'>setTimeout(function(){ window.location.href='Login.aspx'; }, 2000);</script>");


                }
                else
                {
                    MostrarMensaje("Alerta: Ingrese el código de verificación", "warning");
                }
            }
        }
    }
}