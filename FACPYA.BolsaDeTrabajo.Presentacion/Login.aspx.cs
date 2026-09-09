using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Logica;
using FACPYA.BolsaDeTrabajo.Logica.Validacion;
using System;
using System.Collections.Generic;
using System.Web;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class Login : System.Web.UI.Page
    {
        #region Funciones
        protected void LimpiarFormularioRegistro()
        {
            txtCorreoRegistro.Text = string.Empty;
            txtContraseniaRegistro.Text = string.Empty;
            txtConfirmarContraseniaRegistro.Text = string.Empty;
            divIniciarSesion.Visible = true;
            divRegistroCuenta.Visible = false;
            lnkbtnRegistroSustentante.Text = "Registro sustentante";
            lnkbtnRecuperarContrasenia.Visible = true;
        }
        #endregion
        private void MostrarMensaje(string mensaje, string tipo, string modal)
        {
            ClientScript.RegisterStartupScript(GetType(), "Mensaje", "alerta('" + mensaje + "', '" + tipo + "', '" + modal + "')", true);
        }

        private void LimpiarFormulario()
        {
            txtCuenta.Text = string.Empty;
            txtContrasenia.Text = string.Empty;
            txtCorreo.Text = string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkbtnRegistroSustentante_Click(object sender, EventArgs e)
        {
            if (divIniciarSesion.Visible)
            {
                // Mostrar el formulario de registro
                divIniciarSesion.Visible = false;
                divRegistroCuenta.Visible = true;
                lnkbtnManualCandidato.Visible = false;
                lnkbtnRecuperarContrasenia.Visible = false;
                lnkbtnRegistroSustentante.Text = "Ya tengo una cuenta";
            }
            else
            {
                // Mostrar el formulario de inicio de sesión
                divIniciarSesion.Visible = true;
                divRegistroCuenta.Visible = false;
                lnkbtnManualCandidato.Visible = true;
                lnkbtnRecuperarContrasenia.Visible = true;
                lnkbtnRegistroSustentante.Text = "Registro Candidato";
            }

        }


        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string credencial = txtCuenta.Text.Trim();
            string contrasenia = txtContrasenia.Text.Trim();

            List<bdUsuario> lista = logicaUsuario.IniciarSesion(credencial, contrasenia);

            if (lista.Count == 0)
                MostrarMensaje("Las credenciales de acceso no son válidas", "error", null);
            else if (lista[0].IdEstatus == 2)
                MostrarMensaje("La cuenta fue suspendida por el administrador del sistema", "warning", null);
            else
            {
                // Limpiar cualquier sesión anterior activa
                Session.Clear();

                // Obtener parametros
                bdUsuario usuario = new bdUsuario
                {
                    IdUsuario = lista[0].IdUsuario,
                    IdEstatus = lista[0].IdEstatus,
                    IdRol = lista[0].IdRol,
                    IdSustentante = lista[0].IdSustentante,
                    IdEmpresa = lista[0].IdEmpresa
                };
                Session["usuario"] = usuario;

                // Redireccionar a pantalla principal

                // Administrador
                if (lista[0].IdRol == 1)
                {
                    Response.Redirect("ValidacionCandidato.aspx");
                }
                // Candidato
                else if (lista[0].IdRol == 2)
                {
                    Response.Redirect("PerfilCandidato.aspx");
                }
                // Subadministrador (administrador pero sin permiso de manipular información, pura vista)
                if (lista[0].IdRol == 3)
                {
                    Response.Redirect("ListadoCandidato.aspx");
                }
                // Empresa
                if (lista[0].IdRol == 4)
                {
                    Response.Redirect("PerfilEmpresa.aspx");
                }
            }
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
                    IdRol = 2, // Rol Sustentante
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

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            //Validacion de campos y formatos de entrada
            Dictionary<object, string> diccionario = new Dictionary<object, string>();

            diccionario.Add("vCorreo", txtCorreo.Text);

            List<string> listaValidacion = validacionRecuperarContrasena.Validar(diccionario);

            if (listaValidacion.Count == 0)
            {
                bdRecuperarcontasena parametro = new bdRecuperarcontasena();

                parametro.Correo = txtCorreo.Text;

                //Evalua si se trata de un nuevo registro o edicion por medio de la variable publica(vpId)
                Tuple<string, string> control;

                // Envio de ticket al correo
                control = logicaEnviarCorreo.RecuperarContrasena(parametro.Correo);

                MostrarMensaje(control.Item1, control.Item2, null);
                txtCorreo.Text = string.Empty;
            }
            else
            {
                MostrarMensaje(listaValidacion[0], "warning", "#miModal");
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            txtCorreo.Text = string.Empty;
        }
    }

}