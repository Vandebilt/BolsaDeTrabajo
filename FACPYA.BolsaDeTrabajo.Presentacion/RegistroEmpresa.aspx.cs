using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Logica;
using FACPYA.BolsaDeTrabajo.Logica.Validacion;
using System;
using System.Collections.Generic;
using System.Web;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class RegistroEmpresa : System.Web.UI.Page
    {
        #region Funciones
        protected void LimpiarFormularioRegistro()
        {
            txtCorreoRegistro.Text = string.Empty;
            txtContraseniaRegistro.Text = string.Empty;
            txtConfirmarContraseniaRegistro.Text = string.Empty;
        }
        #endregion
        private void MostrarMensaje(string mensaje, string tipo, string modal)
        {
            ClientScript.RegisterStartupScript(GetType(), "Mensaje", "alerta('" + mensaje + "', '" + tipo + "', '" + modal + "')", true);
        }

        private void LimpiarFormulario()
        {
            txtCorreo.Text = string.Empty;
            txtContraseniaRegistro.Text = string.Empty;
            txtConfirmarContraseniaRegistro.Text = string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            // Validación de campos y formatos de entrada
            Dictionary<object, string> diccionario = new Dictionary<object, string>
            {
                { "vCorreo", txtCorreoRegistro.Text.Trim() },
                { "vContrasenia", txtContraseniaRegistro.Text.Trim() },
                { "vConfirmarContrasenia", txtConfirmarContraseniaRegistro.Text.Trim() }
            };

            List<string> listaValidacion = validacionUsuario.Validar(diccionario);

            if (listaValidacion.Count == 0)
            {
                bdUsuario parametro = new bdUsuario
                {
                    IdRol = 4, // Rol Empresa
                    Correo = txtCorreoRegistro.Text.Trim().ToLower(),
                    Contrasenia = txtContraseniaRegistro.Text.Trim()
                };

                Tuple<string, string> control;
                control = logicaUsuario.Crear(parametro);

                // Válida que se haya realizado el registro correctamente
                if (control.Item2 == "success")
                {
                    // Obtiene la URL del sitio y la envia como parámetro para enviarla para la autentificación del código
                    string urlSitio = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/BolsaDeTrabajoPruebas" + "/ActivarCuentaAcceso.aspx";

                    // Envia el código de autentificación al correo electrónico
                    control = logicaCodigo.EnviarCodigoAutentificacion(parametro.Correo, urlSitio);
                }

                MostrarMensaje(control.Item1, control.Item2, null);

                // Regresa al formulario de inicio de sesión, la autentificación se va directo en otra pantalla
                LimpiarFormularioRegistro();
            }
            else
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
            }

        }
    }

}