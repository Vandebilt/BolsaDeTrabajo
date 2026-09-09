using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Logica;
using FACPYA.BolsaDeTrabajo.Logica.Validacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class PerfilCandidato : System.Web.UI.Page
    {
        // Variable global para determinar la acción a realizar en el botón "Grabar"
        private int vpId
        {
            get
            {
                // Si existe en el ViewState, lo leemos
                if (ViewState["vpId"] != null)
                    return (int)ViewState["vpId"];

                return 0; // Valor por defecto si no existe
            }
            set
            {
                // Lo guardamos en el ViewState para que sobreviva a los PostBacks
                ViewState["vpId"] = value;
            }
        }

        #region Id de sesión
        public int IdUsuarioSesion()
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return 0; // Esto no se ejecutará, pero es necesario para que compile.
            }

            bdUsuario vSesion = (bdUsuario)Session["usuario"];
            return vSesion.IdUsuario;
        }

        public int IdSustentanteSesion()
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return 0; // Esto no se ejecutará, pero es necesario para que compile.
            }

            bdUsuario vSesion = (bdUsuario)Session["usuario"];
            return vSesion.IdSustentante;
        }

        public int IdEstatusSesion()
        {
            int idSustentante = IdSustentanteSesion();

            if (idSustentante > 0)
            {
                var sustentante = logicaSustentante.Leer(idSustentante);

                if (sustentante != null)
                {
                    return sustentante[0].IdEstatus;
                }
            }
            return 0;
        }
        #endregion

        #region Retornar rutas
        //RETORNA RUTA DE FOTOGRAFIAS
        public string RetornarRutaFotografia()
        {
            List<bdRuta> ruta = logicaRuta.LeerRuta(1);
            return ruta.Count > 0 ? ruta[0].Ruta : string.Empty;
        }

        //RETORNA RUTA DE DOCUMENTOS
        public string RetornarRutaDocumento()
        {
            List<bdRuta> ruta = logicaRuta.LeerRuta(2);
            return ruta.Count > 0 ? ruta[0].Ruta : string.Empty;
        }

        //RETORNA RUTA DE CERTIFICADO

        public string RetornarRutaCertificado()
        {
            List<bdRuta> ruta = logicaRuta.LeerRuta(3);
            return ruta.Count > 0 ? ruta[0].Ruta : string.Empty;
        }

        //Mostrar mensaje
        private void MostrarMensaje(string mensaje, string tipo, string url = null)
        {
            string script = url != null
                ? $"mostrarMensaje('{mensaje}', '{tipo}', '{url}')"
                : $"mostrarMensaje('{mensaje}', '{tipo}')";

            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", script, true);
        }
        #endregion

        #region Invocar funciones públicas
        // Instancia de la pantantalla maestra para el uso de funciones públicas
        Inicio master = new Inicio();

        // Abrir modal
        private void AbrirModal(string ventana)
        {
            string script = @"<script type='text/javascript'>$(document).ready(function() {abrirModal('" + ventana + @"');});</script>";
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", script, false);
        }
        protected void CargarDropDownLists()
        {
            // Información Sustentante
            master.CargarDropDownList(ddlIdGenero, 1, null);
            master.CargarDropDownList(ddlIdEstadoCivil, 2, null);
            master.CargarDropDownList(ddlIdMunicipio, 3, null);
            master.CargarDropDownList(ddlIdTipoCorreo, 4, null);
            master.CargarDropDownList(ddlIdTipoTelefono, 5, null);
            master.CargarDropDownList(ddlIdAreaExperiencia, 6, null);
            master.CargarDropDownList(ddlIdAreaInteres, 6, null);
            master.CargarDropDownList(ddlIdIdioma, 7, null);
            master.CargarDropDownList(ddlIdNivelIdioma, 8, null);
            master.CargarDropDownList(ddlIdPaqueteSoftware, 9, null);
            master.CargarDropDownList(ddlIdNivelSoftware, 10, null);
            master.CargarDropDownList(ddlIdHabilidad, 11, null);
            master.CargarDropDownList(ddlIdTipoSustentante, 15, null);
            master.CargarDropDownList(ddlIdTipoArchivo, 16, null);
            master.CargarDropDownList(ddlIdCarrera, 12, null);
            master.CargarDropDownList(ddlIdSemestre, 13, null);
            master.CargarDropDownList(ddlIdTurnoEscolar, 14, null);
            master.CargarDropDownList(ddlIdTipoGrado, 17, null);
            master.CargarDropDownList(ddlIdEstatusAcademico, 18, null);
            master.CargarDropDownList(ddlIdEstatusTitulacion, 19, null);
            master.CargarDropDownList(ddlIdTipoHabilidad, 20, null);
            master.CargarDropDownList(ddlIdTiempoDisponible, 26, null);
            master.CargarDropDownList(ddlIdIdiomaCertificacion, 29, null);
        }

  
        private void ToggleFotoUI(bool hayFoto, string mensajeError)
        {
            if (hayFoto)
            {
                // MODO: Mostrar Imagen
                previewContainer.Attributes["class"] = "w-100 mt-3";
                btnBorrar.Style["display"] = "inline-block";
                srcFile.Attributes["class"] = "file-select d-none";
            }
            else
            {
                // MODO: Ocultar Imagen 
                previewContainer.Attributes["class"] = "d-none w-100 mt-3";
                btnBorrar.Style["display"] = "none";
                srcFile.Attributes["class"] = "file-select";
                previewImage.Src = "";
            }

            // Manejo del mensaje (usa el control 'txtInstruccion' que tenías)
            if (!string.IsNullOrEmpty(mensajeError))
            {
                txtInstruccion.InnerText = mensajeError;
                txtInstruccion.Style["color"] = "red";
            }
            else
            {

            }
        }

        private string ResolverRutaFisica(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return null;

            valor = valor.Trim();

            // 1) Ruta física absoluta (C:\..., \\server\...)
            if (Path.IsPathRooted(valor)) return valor;

            // 2) Ruta virtual ~/...
            if (valor.StartsWith("~/") || valor.StartsWith("~/\\"))
                return Server.MapPath(valor);

            // 3) Ruta relativa al sitio /Uploads/... 
            if (valor.StartsWith("/") || valor.StartsWith("\\"))
                return Server.MapPath("~" + valor.Replace("\\", "/"));

            return null; // O ajusta el caso 4
        }

        private string MimePorExtension(string ext)
        {
            ext = (ext ?? "").ToLowerInvariant();
            switch (ext)
            {
                case ".png": return "image/png";
                case ".gif": return "image/gif";
                case ".webp": return "image/webp";
                case ".bmp": return "image/bmp";
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                default: return "application/octet-stream"; // Importante para la validación
            }
        }

        private string FisicaAUrl(string rutaFisica)
        {
            string raiz = Server.MapPath("~").TrimEnd('\\') + "\\";
            if (string.IsNullOrEmpty(rutaFisica)) return null;
            if (!rutaFisica.StartsWith(raiz, StringComparison.OrdinalIgnoreCase)) return null;

            string relativo = rutaFisica.Substring(raiz.Length).Replace("\\", "/");
            return "~/" + relativo; // p.ej. ~/Uploads/Sustentantes/Foto.jpg
        }

        public void EstablecerModoLectura(bool estaEnModoLectura)
        {
            if (IdEstatusSesion() == 6)
            {
                // === TAB: PERSONAL ===
                txtNombre.ReadOnly = estaEnModoLectura;
                txtPrimerApellido.ReadOnly = estaEnModoLectura;
                txtSegundoApellido.ReadOnly = estaEnModoLectura;
                txtFechaNacimiento.ReadOnly = estaEnModoLectura;
                ddlIdGenero.Enabled = !estaEnModoLectura;
                ddlIdEstadoCivil.Enabled = !estaEnModoLectura;
                ddlIdMunicipio.Enabled = !estaEnModoLectura;
                chkTrabaja.Enabled = !estaEnModoLectura;
                txtNacionalidad.ReadOnly = estaEnModoLectura;
                txtColonia.ReadOnly = estaEnModoLectura;
                txtCalle.ReadOnly = estaEnModoLectura;
                txtNumerodeCasa.ReadOnly = estaEnModoLectura;
                ddlIdTiempoDisponible.Enabled = !estaEnModoLectura;
                txtSueldoDeseado.ReadOnly = estaEnModoLectura;
                txtBiografia.ReadOnly = estaEnModoLectura;
                txtLinkedinUrl.ReadOnly = estaEnModoLectura;
                srcFile.Visible = !estaEnModoLectura;
                btnBorrar.Visible = !estaEnModoLectura;
                btnGrabarPersonal.Visible = !estaEnModoLectura;
                divAlertaFotografía.Visible = false;

                // === TAB: DATOS DE CONTACTO ===
                lblIdTipoCorreo.Visible = !estaEnModoLectura;
                ddlIdTipoCorreo.Visible = !estaEnModoLectura;
                lblCorreo.Visible = !estaEnModoLectura;
                txtCorreo.Visible = !estaEnModoLectura;
                btnAgregarCorreo.Visible = !estaEnModoLectura;
                gvConsultaCorreo.Columns[0].Visible = !estaEnModoLectura;
                lblIdTipoTelefono.Visible = !estaEnModoLectura;
                ddlIdTipoTelefono.Visible = !estaEnModoLectura;
                lblTelefono.Visible = !estaEnModoLectura;
                txtTelefono.Visible = !estaEnModoLectura;
                lblExtension.Visible = !estaEnModoLectura;
                txtExtension.Visible = !estaEnModoLectura;
                lbtnAgregarTelefono.Visible = !estaEnModoLectura;
                gvConsultaTelefono.Columns[0].Visible = !estaEnModoLectura;

                // === TAB: HABILIDADES Y COMPETENCIAS ===
                lblIdAreaExperiencia.Visible = !estaEnModoLectura;
                ddlIdAreaExperiencia.Visible = !estaEnModoLectura;
                lblAniosExperiencia.Visible = !estaEnModoLectura;
                txtAniosExperiencia.Visible = !estaEnModoLectura;
                lblMesesExperiencia.Visible = !estaEnModoLectura;
                txtMesesExperiencia.Visible = !estaEnModoLectura;
                lbtnAgregarAreaExperiencia.Visible = !estaEnModoLectura;
                gvConsultaAreaExperiencia.Columns[0].Visible = !estaEnModoLectura;
                lblIdAreaInteres.Visible = !estaEnModoLectura;
                ddlIdAreaInteres.Visible = !estaEnModoLectura;
                lbtnAgregarAreaInteres.Visible = !estaEnModoLectura;
                gvConsultaAreaInteres.Columns[0].Visible = !estaEnModoLectura;
                lblIdTipoHabilidad.Visible = !estaEnModoLectura;
                ddlIdTipoHabilidad.Visible = !estaEnModoLectura;
                lblIdHabilidad.Visible = !estaEnModoLectura;
                ddlIdHabilidad.Visible = !estaEnModoLectura;
                lbtnAgregarHabilidad.Visible = !estaEnModoLectura;
                gvConsultaHabilidad.Columns[0].Visible = !estaEnModoLectura;
                lblIdPaqueteSoftware.Visible = !estaEnModoLectura;
                ddlIdPaqueteSoftware.Visible = !estaEnModoLectura;
                lblIdNivelSoftware.Visible = !estaEnModoLectura;
                ddlIdNivelSoftware.Visible = !estaEnModoLectura;
                lbtnAgregarPaqueteSoftware.Visible = !estaEnModoLectura;
                gvConsultaSoftware.Columns[0].Visible = !estaEnModoLectura;
                lblIdIdioma.Visible = !estaEnModoLectura;
                ddlIdIdioma.Visible = !estaEnModoLectura;
                lblIdNivelIdioma.Visible = !estaEnModoLectura;
                ddlIdNivelIdioma.Visible = !estaEnModoLectura;
                lbtnAgregarIdioma.Visible = !estaEnModoLectura;
                gvConsultaIdioma.Columns[0].Visible = !estaEnModoLectura;

                // === TAB: ACADEMICA ===
                ddlIdTipoSustentante.Enabled = !estaEnModoLectura;
                txtMatricula.ReadOnly = estaEnModoLectura;
                ddlIdTipoGrado.Enabled = !estaEnModoLectura;
                ddlIdCarrera.Enabled = !estaEnModoLectura;
                ddlIdPlanEstudios.Enabled = !estaEnModoLectura;
                ddlIdModalidad.Enabled = !estaEnModoLectura;
                ddlIdSemestre.Enabled = !estaEnModoLectura;
                ddlIdTurnoEscolar.Enabled = !estaEnModoLectura;
                ddlAnioIngreso.Enabled = !estaEnModoLectura;
                ddlAnioEgreso.Enabled = !estaEnModoLectura;
                ddlIdEstatusAcademico.Enabled = !estaEnModoLectura;
                ddlIdEstatusTitulacion.Enabled = !estaEnModoLectura;
                rbServicioSi.Enabled = !estaEnModoLectura;
                rbServicioNo.Enabled = !estaEnModoLectura;
                rbPracticasSi.Enabled = !estaEnModoLectura;
                rbPracticasNo.Enabled = !estaEnModoLectura;
                txtPromedio.ReadOnly = estaEnModoLectura;
                txtOtraCarrera.ReadOnly = estaEnModoLectura;
                btnGrabarAcademico.Visible = !estaEnModoLectura;

                // === TAB: DOCUMENTOS ===
                lblIdTipoArchivo.Visible = !estaEnModoLectura;
                ddlIdTipoArchivo.Visible = !estaEnModoLectura;
                fuDocumento.Visible = !estaEnModoLectura;
                lbtnAgregarArchivo.Visible = !estaEnModoLectura;
                gvConsultaDocumento.Columns[0].Visible = !estaEnModoLectura;
                btnAgregarReporte.Visible = !estaEnModoLectura;
                btnAgregarCertificado.Visible = !estaEnModoLectura;
                divAlertaRevision.Visible = true;
                divAlertaCompletaDatos.Visible = false;
                divPerfilCompleto.Visible = false;
            }
            else if (IdEstatusSesion() == 4)
            {
                txtNombre.ReadOnly = estaEnModoLectura;
                txtPrimerApellido.ReadOnly = estaEnModoLectura;
                txtSegundoApellido.ReadOnly = estaEnModoLectura;
                txtFechaNacimiento.ReadOnly = estaEnModoLectura;
                ddlIdGenero.Enabled = !estaEnModoLectura;
                srcFile.Visible = !estaEnModoLectura;
                btnBorrar.Visible = !estaEnModoLectura;
                lblIdTipoArchivo.Visible = !estaEnModoLectura;
                ddlIdTipoArchivo.Visible = !estaEnModoLectura;
                fuDocumento.Visible = !estaEnModoLectura;
                lbtnAgregarArchivo.Visible = !estaEnModoLectura;
                gvConsultaDocumento.Columns[0].Visible = !estaEnModoLectura;
                divAlertaFotografía.Visible = false;
                divAlertaCompletaDatos.Visible = false;
                divPerfilCompleto.Visible = true;
            }
        }
        #endregion

        #region Leer
        private void CargarAnios()
        {
            int anioActual = DateTime.Now.Year;
            int anioInicio = anioActual - 50;

            // Limpiar para evitar duplicados
            ddlAnioIngreso.Items.Clear();
            ddlAnioEgreso.Items.Clear();

            // Opción inicial
            ddlAnioIngreso.Items.Add(new ListItem("Seleccionar", "0"));
            ddlAnioEgreso.Items.Add(new ListItem("Seleccionar", "0"));

            // Cargar los años
            for (int anio = anioActual; anio >= anioInicio; anio--)
            {
                ddlAnioIngreso.Items.Add(new ListItem(anio.ToString(), anio.ToString()));
                ddlAnioEgreso.Items.Add(new ListItem(anio.ToString(), anio.ToString()));
            }
        }

        protected void CargarFotografia()
        {
            var lista = logicaSustentante.Leer(IdSustentanteSesion());
            if (lista == null || lista.Count == 0)
            {
                ToggleFotoUI(false, null);
                return;
            }

            if (lista[0].IdEstatusImg == 4)
            {
                btnBorrar.Visible = false;
            }

            string nombreArchivo = lista[0].Fotografia;
            int vpId = lista[0].IdArchivoSustentante;
            lblEstatusImg.Text = lista[0].Estatus.ToString();
            hfVpId.Value = vpId.ToString();

            if (string.IsNullOrWhiteSpace(nombreArchivo))
            {
                ToggleFotoUI(false, null);
                return;
            }

            string rutaFisica = RetornarRutaFotografia() + nombreArchivo;
            string srcParaImagen = null; // Variable para guardar el resultado

            if (string.IsNullOrEmpty(rutaFisica) || !File.Exists(rutaFisica))
            {
                ToggleFotoUI(false, "No se encontró el archivo de imagen.");
                return;
            }

            // <-- CAMBIO: Usamos el helper para ver si es accesible vía web
            string urlWeb = FisicaAUrl(rutaFisica);

            if (!string.IsNullOrEmpty(urlWeb))
            {
                srcParaImagen = ResolveUrl(urlWeb) + "?v=" + File.GetLastWriteTime(rutaFisica).Ticks;
            }
            else
            {
                string ext = Path.GetExtension(rutaFisica);
                string mime = MimePorExtension(ext);

                if (mime.StartsWith("image/"))
                {
                    byte[] bytes = File.ReadAllBytes(rutaFisica);
                    string base64 = Convert.ToBase64String(bytes);
                    srcParaImagen = $"data:{mime};base64,{base64}";
                }
                else
                {
                    // El archivo existe pero no es una imagen (ej. un .zip)
                    ToggleFotoUI(false, "El archivo encontrado no es una imagen válida."); // <- CAMBIO
                    return;
                }
            }

            previewImage.Src = srcParaImagen;
            ToggleFotoUI(true, null);
        }
        private void CargarAcademica()
        {
            List<bdExpedienteAcademico> lista = logicaExpedienteAcademico.Leer(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                ddlIdTipoSustentante.SelectedValue = lista[0].IdTipoSustentante.ToString();
                txtMatricula.Text = lista[0].Matricula.ToString();
                ddlIdTipoGrado.SelectedValue = lista[0].IdTipoGrado.ToString();
                ddlIdCarrera.SelectedValue = lista[0].IdCarrera.ToString();
                txtPromedio.Text = (lista[0].Promedio == 0) ? "" : lista[0].Promedio.ToString();
                txtOtraCarrera.Text = (lista[0].OtraCarrera ?? "").ToUpper();
                // Estudiante
                ddlIdSemestre.SelectedValue = lista[0].IdSemestre.ToString();
                ddlIdTurnoEscolar.SelectedValue = lista[0].IdTurnoEscolar.ToString();
                rbServicioSi.Checked = lista[0].HasServicioSocial;
                rbServicioNo.Checked = !lista[0].HasServicioSocial;
                rbPracticasSi.Checked = lista[0].HasPracticasProfesionales;
                rbPracticasNo.Checked = !lista[0].HasPracticasProfesionales;

                if (ddlIdTipoSustentante.SelectedValue == "1")
                {
                    ddlIdPlanEstudios.Enabled = true;
                    ddlIdModalidad.Enabled = true;

                    int idCarreraBD = int.Parse(lista[0].IdCarrera.ToString());
                    int idPlanBD = int.Parse(lista[0].IdPlanEstudio.ToString());
                    int idModalidadBD = int.Parse(lista[0].IdModalidad.ToString());
                    int idPlanCombinado = (idCarreraBD * 100000) + idPlanBD;

                    master.CargarDropDownList(ddlIdPlanEstudios, 24, idCarreraBD);
                    ddlIdPlanEstudios.SelectedValue = idPlanCombinado.ToString();

                    master.CargarDropDownList(ddlIdModalidad, 25, idPlanCombinado);
                    ddlIdModalidad.SelectedValue = idModalidadBD.ToString();

                    if (IdEstatusSesion() == 6 || IdEstatusSesion() == 4)
                    {
                        ddlIdCarrera.Enabled = !_modoLectura;
                        ddlIdPlanEstudios.Enabled = !_modoLectura;
                        ddlIdModalidad.Enabled = !_modoLectura;
                    }
                }

                string valorIngreso = "0";

                if (lista[0].AnioIngreso.HasValue)
                {
                    int anio = lista[0].AnioIngreso.Value.Year;
                    if (anio > 1)
                        valorIngreso = anio.ToString();
                }

                SeleccionarValorSeguro(ddlAnioIngreso, valorIngreso);

                string valorEgreso = "0";
                if (lista[0].AnioEgreso.HasValue)
                {
                    int anio = lista[0].AnioEgreso.Value.Year;
                    if (anio > 1)
                        valorEgreso = anio.ToString();
                }

                SeleccionarValorSeguro(ddlAnioEgreso, valorEgreso);
                ddlIdEstatusAcademico.SelectedValue = lista[0].IdEstatusAcademico.ToString();
                ddlIdEstatusTitulacion.SelectedValue = lista[0].IdEstatusTitulacion.ToString();

                if (lista[0].IdTipoSustentante == 1)
                {
                    phEstudiante.Visible = true;
                    ddlAnioIngreso.ClearSelection();
                    ddlAnioEgreso.ClearSelection();
                    ddlIdEstatusAcademico.ClearSelection();
                    ddlIdEstatusTitulacion.ClearSelection();

                }
                else if (lista[0].IdTipoSustentante == 2)
                {
                    ddlIdPlanEstudios.Enabled = true;
                    master.CargarDropDownList(ddlIdPlanEstudios, 24, int.Parse(ddlIdCarrera.SelectedValue));

                    phEgresado.Visible = true;
                    ddlIdSemestre.ClearSelection();
                    ddlIdTurnoEscolar.ClearSelection();
                    ddlIdPlanEstudios.ClearSelection();
                    ddlIdModalidad.ClearSelection();
                    rbServicioSi.Checked = false;
                    rbServicioNo.Checked = false;
                    rbPracticasSi.Checked = false;
                    rbPracticasNo.Checked = false;
                }
            }

        }
        private void CargarExperiencia()
        {
            DataTable datosExperiencia = logicaSustentanteExperienciaLaboral.Consultar(IdSustentanteSesion());
            lvExperiencia.DataSource = datosExperiencia;
            lvExperiencia.DataBind();
        }
        private void CargarCertificado()
        {
            DataTable datosCertificado = logicaSustentanteCertificado.Consultar(IdSustentanteSesion());
            lvCertificado.DataSource = datosCertificado;
            lvCertificado.DataBind();
        }
        protected void LeerSustentante()
        {
            List<bdSustentante> lista = logicaSustentante.Leer(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                txtNombre.Text = lista[0].Nombre.ToString();
                txtPrimerApellido.Text = lista[0].PrimerApellido.ToString();
                txtSegundoApellido.Text = lista[0].SegundoApellido.ToString();

                if (lista[0].FechaNacimiento.HasValue) // .HasValue es lo mismo que != null
                {
                    txtFechaNacimiento.Text = lista[0].FechaNacimiento.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtFechaNacimiento.Text = ""; // Asigna un valor por defecto si es nulo
                }

                ddlIdGenero.SelectedValue = lista[0].IdGenero.ToString();
                ddlIdEstadoCivil.SelectedValue = lista[0].IdEstadoCivil.ToString();
                txtNacionalidad.Text = lista[0].Nacionalidad.ToString();
                ddlIdMunicipio.SelectedValue = lista[0].IdMunicipio.ToString();
                txtColonia.Text = lista[0].Colonia.ToString();
                txtCalle.Text = lista[0].Calle.ToString();
                txtNumerodeCasa.Text = lista[0].NumeroCasa.ToString();
                txtSueldoDeseado.Text = (lista[0].SueldoDeseado == 0) ? "" : lista[0].SueldoDeseado.ToString();
                ddlIdTiempoDisponible.SelectedValue = lista[0].IdTiempoDisponible.ToString();
                chkTrabaja.Checked = Convert.ToBoolean(lista[0].TrabajaActualmente);
                txtLinkedinUrl.Text = lista[0].LinkedinUrl.ToString();
                txtBiografia.Text = lista[0].Biografia.ToString();

                CargarAnios();

                string horarioGuardado = lista[0].HorarioDisponible;

                CargarExperiencia();
                CargarAcademica();
                CargarCertificado();
                GridViewCorreo();
                GridViewTelefono();
                GridViewAreaExperiencia();
                GridViewAreaInteres();
                GridViewHabilidad();
                GridViewSoftware();
                GridViewIdioma();
                GridViewCertificacionIdioma();
                GridViewDocumento();
                CargarFotografia();
            }
        }
        #endregion

        private bool _modoLectura = false; // Variable en la clase

        // Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Configuración Inicial (Permisos y Cargas)
                ConfigurarAccesoYDatos();

                // Configuración de Scripts de Cliente (DropDowns)
                ConfigurarScriptsDePostback();

                // Manejo de Pestañas (Tabs) y lógica específica
                ManejarPestanaActiva();

                // Manejo de Notificaciones (Toasts/Alertas)
                ManejarMensajesGlobales();
            }
        }

        #region Información personal
        // Borrar Fotografía
        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            // Validación de existencia de ID en el campo oculto
            try
            {
                int vpIdFromHidden = int.Parse(hfVpId.Value);
                if (vpIdFromHidden > 0)
                {
                    var parametro = new bdSustentanteArchivo
                    {
                        IdArchivoSustentante = vpIdFromHidden
                    };
                    logicaSustentanteArchivo.Eliminar(parametro);
                    MostrarMensaje("Fotografía eliminada correctamente.", "success");
                    updImg.Update();
                    CargarFotografia();

                }

            }
            catch (Exception ex)
            {
                MostrarMensaje("No se pudo eliminar la fotografía: " + ex.Message, "error");
            }
        }

        // Botón grabar información personal
        protected void btnGrabarPersonal_Click(object sender, EventArgs e)
        {
            // Validación de campos y formatos de entrada
            Dictionary<object, string> diccionario = new Dictionary<object, string>
                {
                    { "vNombre", txtNombre.Text },
                    { "vPrimerApellido", txtPrimerApellido.Text },
                    { "vFechaNacimiento",txtFechaNacimiento.Text },
                    { "vGenero", ddlIdGenero.SelectedValue },
                    { "vEstadoCivil", ddlIdEstadoCivil.SelectedValue },
                    { "vNacionalidad", txtNacionalidad.Text },
                    { "vMunicipio", ddlIdMunicipio.SelectedValue },
                    { "vColonia", txtColonia.Text },
                    { "vCalle", txtCalle.Text },
                    { "vNumeroDeCasa", txtNumerodeCasa.Text },
                    { "vTiempoDisponible", ddlIdTiempoDisponible.SelectedValue },
                    { "vSueldoDeseado", txtSueldoDeseado.Text },
                    { "vBiografia", txtBiografia.Text },
                };

            List<string> listaValidacion = validacionSustentante.Validar(diccionario);

            if (listaValidacion.Count == 0)
            {
                // IMAGEN SUSTANTE
                string base64Data = hfCroppedImage.Value;
                if (!string.IsNullOrWhiteSpace(base64Data))
                {
                    byte[] archivoBytes;
                    try
                    {
                        archivoBytes = Convert.FromBase64String(base64Data);
                    }
                    catch (FormatException)
                    {
                        MostrarMensaje("La imagen recibida es inválida.", "error", "#ModalNuevo");
                        return;
                    }

                    var lista = logicaSustentante.Leer(IdSustentanteSesion());
                    string NombreSustentante = lista[0].Nombre + lista[0].PrimerApellido;

                    // Guardar en disco
                    string destinoCarpeta = RetornarRutaFotografia(); // debe devolver una ruta física válida
                    if (!Directory.Exists(destinoCarpeta))
                        Directory.CreateDirectory(destinoCarpeta);

                    // Generar nombre único para la imagen
                    string nuevoNombre = $"FotoSustentante_{NombreSustentante}_{IdSustentanteSesion()}.jpg";
                    string rutaCompleta = Path.Combine(destinoCarpeta, nuevoNombre);

                    // Guardar el archivo en disco
                    try
                    {
                        File.WriteAllBytes(rutaCompleta, archivoBytes);
                    }
                    catch (Exception ex)
                    {
                        MostrarMensaje("Error al guardar la imagen: " + ex.Message, "error", "#ModalNuevo");
                        return;
                    }

                    bdSustentanteArchivo parametroArchivo = new bdSustentanteArchivo
                    {
                        IdTipoArchivo = 2,
                        IdSustentante = IdSustentanteSesion(),
                        NombreDocumento = nuevoNombre,
                    };

                    logicaSustentanteArchivo.Crear(parametroArchivo);
                }

                // Guardar información personal del sustentante (Candidato)
                bdSustentante parametro = new bdSustentante
                {
                    IdSustentante = IdSustentanteSesion(),
                    Nombre = txtNombre.Text.ToUpper(),
                    PrimerApellido = txtPrimerApellido.Text.Trim().ToUpper(),
                    SegundoApellido = txtSegundoApellido.Text.Trim().ToUpper(),
                    FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text.Trim()),
                    IdGenero = int.Parse(ddlIdGenero.SelectedValue),
                    IdEstadoCivil = int.Parse(ddlIdEstadoCivil.SelectedValue),
                    Nacionalidad = txtNacionalidad.Text.ToUpper(),
                    IdMunicipio = int.Parse(ddlIdMunicipio.SelectedValue),
                    IdTiempoDisponible = int.Parse(ddlIdTiempoDisponible.SelectedValue),
                    Colonia = txtColonia.Text.ToUpper(),
                    Calle = txtCalle.Text.ToUpper(),
                    NumeroCasa = txtNumerodeCasa.Text,
                    SueldoDeseado = int.Parse(txtSueldoDeseado.Text),
                    TrabajaActualmente = chkTrabaja.Checked,
                    LinkedinUrl = txtLinkedinUrl.Text,
                    Biografia = txtBiografia.Text.ToUpper(),
                };

                // Invocar la lógica de negocio para editar el sustentante y obtener el resultado
                Tuple<string, string> control;
                control = logicaSustentante.Editar(parametro);

                // Mostrar mensaje de resultado
                Session["MensajePendiente"] = control.Item1;
                Session["TipoMensaje"] = control.Item2;

                // Recargar la página para reflejar los cambios
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
            }
        }
        #endregion

        #region Datos de Contacto
        #region Correo
        // Cargar GvCorreo
        protected List<DataRow> GridViewCorreo()
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaSustentanteCorreo.Consultar(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaCorreo.Visible = true;
                master.CargarGridView(lista, gvConsultaCorreo);
                FuncionDeshabilitarControles(gvConsultaCorreo);

            }
            else
            {
                gvConsultaCorreo.Visible = false;
            }

            return lista;
        }

        // Oculta columnas de GridView
        protected void gvConsultaCorreo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    //e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;

                    GridViewRow headerRow = gvConsultaCorreo.HeaderRow;
                    if (headerRow != null)
                    {
                        //headerRow.Cells[0].Visible = false;
                        headerRow.Cells[1].Visible = false;
                        headerRow.Cells[2].Visible = false;
                    }
                }
            }
        }

        public void FuncionDeshabilitarControles(GridView pGridView)
        {
            // Recorre el GridView y desactiva controles
            for (int i = 0; i < pGridView.Rows.Count; i++)
            {
                // Editar
                if (pGridView.Rows[i].Cells[3].Text == "PRINCIPAL")
                {
                    //Eliminar
                    LinkButton btnEliminar = pGridView.Rows[i].FindControl("btnEliminarCorreo") as LinkButton;
                    if (btnEliminar != null)
                    {
                        btnEliminar.Enabled = false;
                        btnEliminar.ToolTip = "Opción Invalida";
                        btnEliminar.CssClass = "btn btn-danger btnDesactivar";
                        btnEliminar.OnClientClick = ""; // Anulamos el JS por si acaso

                    }
                }
            }
        }

        // Agregar Correo
        protected void btnAgregarCorreo_Click(object sender, EventArgs e)
        {
            // 1) Validación de campos
            var diccionario = new Dictionary<object, string>
            {
                { "vIdTipoCorreo", ddlIdTipoCorreo.SelectedValue },
                { "vCorreo", txtCorreo.Text.Trim() }
            };

            List<string> listaValidacion = validacionSustentanteCorreo.Validar(diccionario);

            if (listaValidacion != null && listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            // Validación de límite de correos
            if (gvConsultaCorreo.Rows.Count == 5)
            {
                MostrarMensaje("Sólo se permiten registrar 5 correos", "warning", null);
                ddlIdTipoCorreo.ClearSelection();
                txtCorreo.Text = string.Empty;
                return;
            }

            // Validación de duplicados (correo y tipo principal)
            int idTipoCorreo = Convert.ToInt32(ddlIdTipoCorreo.SelectedValue);
            string correoInput = (txtCorreo.Text ?? string.Empty).Trim();

            // (Opcional) normalización mínima de email: trim + lower para comparar
            // Nota: Para guardar puedes mantener el texto original si lo deseas.
            string correoCmp = correoInput.ToLowerInvariant();

            // Obtener lista y prevenir null
            int idSustentante = Convert.ToInt32(IdSustentanteSesion());
            List<bdCorreoSustentante> lista = logicaSustentanteCorreo.Leer(idSustentante) ?? new List<bdCorreoSustentante>();

            // Normalizar datos existentes al comparar (trim + lower), cuidando nulos
            bool correoDuplicado = lista.Any(c =>
                !string.IsNullOrWhiteSpace(c?.Correo) &&
                string.Equals(c.Correo.Trim(), correoInput, StringComparison.OrdinalIgnoreCase));

            // Validación de correo duplicado
            if (correoDuplicado)
            {
                MostrarMensaje("El correo que trata de registrar ya se encuentra en la lista", "warning", null);
                return;
            }

            // Validación de tipo de correo principal (solo uno permitido)
            if (idTipoCorreo == 1)
            {
                bool tipoCorreoDuplicado = lista.Any(c => c?.IdTipoCorreo == 1);
                if (tipoCorreoDuplicado)
                {
                    MostrarMensaje("Solo se permite un correo principal", "warning", null);
                    return;
                }
            }

            // Crear registro
            var parametro = new bdCorreoSustentante
            {
                IdSustentante = idSustentante,
                IdTipoCorreo = idTipoCorreo,
                Correo = correoInput, // guarda como lo escribió el usuario (legible)
            };

            var resultado = logicaSustentanteCorreo.Crear(parametro);
            MostrarMensaje(resultado.Item1, resultado.Item2, null);

            // Recargar gvCorreo
            GridViewCorreo();

            // Limpiar controles Correo
            ddlIdTipoCorreo.ClearSelection();
            txtCorreo.Text = string.Empty;
        }

        // Eliminar Correo
        protected void gvConsultaCorreo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int pId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                var control = logicaSustentanteCorreo.Eliminar(new bdCorreoSustentante { IdCorreoSustentante = pId });
                MostrarMensaje(control.Item1, control.Item2, null);
                GridViewCorreo();

            }
        }
        #endregion

        #region Telefono
        // Cargar GvTelefono
        protected List<DataRow> GridViewTelefono()
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaSustentanteTelefono.Consultar(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaTelefono.Visible = true;
                master.CargarGridView(lista, gvConsultaTelefono);
            }
            else
            {
                gvConsultaTelefono.Visible = false;
            }

            return lista;
        }

        // Oculta Columnas GridView
        protected void gvConsultaTelefono_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    //e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;

                    GridViewRow headerRow = gvConsultaTelefono.HeaderRow;
                    if (headerRow != null)
                    {
                        //headerRow.Cells[0].Visible = false;
                        headerRow.Cells[1].Visible = false;
                        headerRow.Cells[2].Visible = false;
                    }
                }
            }
        }


        // Agregar Teléfono
        protected void lbtnAgregarTelefono_Click(object sender, EventArgs e)
        {
            // Validación de campos
            var diccionario = new Dictionary<object, string>
            {
                { "vIdTipoTelefono", ddlIdTipoTelefono.SelectedValue },
                { "vTelefono", txtTelefono.Text.Trim() }
            };

            List<string> listaValidacion = validacionSustentanteTelefono.Validar(diccionario);

            // Validación de campos y mensajes
            if (listaValidacion != null && listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            // Validación de longitud del teléfono (mínimo 10 dígitos)
            if (txtTelefono.Text.Trim().Length < 10)
            {
                MostrarMensaje("El teléfono debe tener 10 dígitos", "warning", null);
                return;
            }

            // Validación de límite de teléfonos (máximo 5)
            if (gvConsultaTelefono.Rows.Count == 5)
            {
                MostrarMensaje("Sólo se permiten registrar 5 teléfonos", "warning", null);
                ddlIdTipoTelefono.ClearSelection();
                txtTelefono.Text = string.Empty;
                txtExtension.Text = string.Empty;
                return;
            }

            int idTipoTelefono = Convert.ToInt32(ddlIdTipoTelefono.SelectedValue);
            string telefono = (txtTelefono.Text ?? string.Empty).Trim();
            string extension = (txtExtension.Text ?? string.Empty).Trim();

            // Nota: Para guardar puedes mantener el texto original si lo deseas.
            string telefonoCmp = telefono.ToLowerInvariant();
            string extensionCmp = extension.ToLowerInvariant();

            // Obtener lista y prevenir null
            int idSustentante = Convert.ToInt32(IdSustentanteSesion());
            List<bdTelefonoSustentante> lista = logicaSustentanteTelefono.Leer(idSustentante) ?? new List<bdTelefonoSustentante>();

            bool telefonoDuplicado = lista.Any(c =>
                !string.IsNullOrWhiteSpace(c.Telefono) &&
                string.Equals(c.Telefono.Trim(), telefono.Trim(), StringComparison.OrdinalIgnoreCase));


            // Validación de teléfono duplicado
            if (telefonoDuplicado)
            {
                MostrarMensaje("El telefono que trata de registrar ya se encuentra en la lista", "warning", null);
                return;
            }

            // Crear registro
            var parametro = new bdTelefonoSustentante
            {
                IdSustentante = idSustentante,
                IdTipoTelefono = idTipoTelefono,
                Telefono = telefonoCmp.Trim(),
                Extension = extensionCmp.Trim()
            };

            var resultado = logicaSustentanteTelefono.Crear(parametro);
            MostrarMensaje(resultado.Item1, resultado.Item2, null);

            // Recargar gvCorreo
            GridViewTelefono();

            // Limpiar controles Correo
            ddlIdTipoTelefono.ClearSelection();
            txtTelefono.Text = string.Empty;
            txtExtension.Text = string.Empty;
        }

        // Eliminar Teléfono
        protected void gvConsultaTelefono_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int pId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                var control = logicaSustentanteTelefono.Eliminar(new bdTelefonoSustentante { IdTelefonoSustentante = pId });
                MostrarMensaje(control.Item1, control.Item2, null);
                GridViewTelefono();

            }
        }
        #endregion
        #endregion

        #region Habilidades y Competencias
        // Cargar habilidades según el tipo seleccionado
        protected void ddlIdTipoHabilidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pidTipoHabilidad = Convert.ToInt32(ddlIdTipoHabilidad.SelectedItem.Value);
            master.CargarDropDownList(ddlIdHabilidad, 11, pidTipoHabilidad);
        }
        #endregion

        #region Area de Experiencia
        // Cargar GvAreaExperiencia
        protected List<DataRow> GridViewAreaExperiencia()
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaSustentanteAreaExperiencia.Consultar(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaAreaExperiencia.Visible = true;
                master.CargarGridView(lista, gvConsultaAreaExperiencia);
            }
            else
            {
                gvConsultaAreaExperiencia.Visible = false;
            }

            return lista;
        }

        // Oculta columnas de GridView
        protected void gvConsultaAreaExperiencia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    //e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;

                    GridViewRow headerRow = gvConsultaAreaExperiencia.HeaderRow;
                    if (headerRow != null)
                    {
                        //headerRow.Cells[0].Visible = false;
                        headerRow.Cells[1].Visible = false;
                        headerRow.Cells[2].Visible = false;
                    }
                }
            }
        }

        // Agregar Area de Experiencia
        protected void lbtnAgregarAreaExperiencia_Click(object sender, EventArgs e)
        {
            // Validación de campos
            var diccionario = new Dictionary<object, string>
            {
                { "vIdArea", ddlIdAreaExperiencia.SelectedValue },
                { "vAnios", txtAniosExperiencia.Text },
                { "vMeses", txtMesesExperiencia.Text },
            };

            List<string> listaValidacion = validacionSustentanteAreaExperiencia.Validar(diccionario);

            if (listaValidacion != null && listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            // Declaracion de variables
            int idAreaExperiencia = Convert.ToInt32(ddlIdAreaExperiencia.SelectedValue);
            decimal Anios = decimal.Parse(txtAniosExperiencia.Text + '.' + txtMesesExperiencia.Text);
            int idSustentante = Convert.ToInt32(IdSustentanteSesion());

            List<bdSustentanteAreaExperiencia> lista = logicaSustentanteAreaExperiencia.Leer(idSustentante) ?? new List<bdSustentanteAreaExperiencia>();

            // Validación de duplicados
            bool duplicado = lista.Any(c => c.IdAreaInteres == idAreaExperiencia);
            if (duplicado)
            {
                MostrarMensaje("El área que intenta registrar ya existe en la lista", "warning", null);
                return;
            }

            // Validación de tiempo válido (años o meses deben ser mayores a 0)
            if (txtAniosExperiencia.Text.Trim() == "0" && txtMesesExperiencia.Text.Trim() == "0")
            {
                MostrarMensaje("Debe ingresar un tiempo válido (años o meses deben ser mayores a 0)", "warning", null);
                txtAniosExperiencia.Text = string.Empty;
                txtMesesExperiencia.Text = string.Empty;

                return;
            }

            // Crear registro
            var parametro = new bdSustentanteAreaExperiencia
            {
                IdSustentante = idSustentante,
                IdAreaInteres = idAreaExperiencia,
                Anios = Anios
            };

            // Invocar la lógica de negocio para crear el registro y obtener el resultado
            var resultado = logicaSustentanteAreaExperiencia.Crear(parametro);
            MostrarMensaje(resultado.Item1, resultado.Item2, null);

            // Recargar gvCorreo
            GridViewAreaExperiencia();

            // Limpiar controles Correo
            ddlIdAreaExperiencia.ClearSelection();
            txtAniosExperiencia.Text = string.Empty;
            txtMesesExperiencia.Text = string.Empty;
        }

        // Eliminar Area de Experiencia
        protected void gvConsultaAreaExperiencia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtener el ID del registro a eliminar desde el CommandArgument
            int pId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                // Invocar la lógica de negocio para eliminar el registro y obtener el resultado
                var control = logicaSustentanteAreaExperiencia.Eliminar(new bdSustentanteAreaExperiencia { IdSustentanteAreaExperiencia = pId });
                MostrarMensaje(control.Item1, control.Item2, null);
                GridViewAreaExperiencia();

            }
        }
        #endregion

        #region Area de Interés
        // Cargar GvAreaInteres
        protected List<DataRow> GridViewAreaInteres()
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaSustentanteAreaInteres.Consultar(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaAreaInteres.Visible = true;
                master.CargarGridView(lista, gvConsultaAreaInteres);
            }
            else
            {
                gvConsultaAreaInteres.Visible = false;
            }

            return lista;
        }

        // Oculta columnas de GridView
        protected void gvConsultaAreaInteres_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    //e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;

                    GridViewRow headerRow = gvConsultaAreaInteres.HeaderRow;
                    if (headerRow != null)
                    {
                        //headerRow.Cells[0].Visible = false;
                        headerRow.Cells[1].Visible = false;
                        headerRow.Cells[2].Visible = false;
                    }
                }
            }
        }

        // Agregar Area de Interés
        protected void lbtnAgregarAreaInteres_Click(object sender, EventArgs e)
        {
            // Validación de campos
            var diccionario = new Dictionary<object, string>
            {
                { "vIdAreaInteres", ddlIdAreaInteres.SelectedValue }
            };

            List<string> listaValidacion = validacionSustentanteAreaInteres.Validar(diccionario);

            if (listaValidacion != null && listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            // Declaracion de variables
            int idAreaInteres = Convert.ToInt32(ddlIdAreaInteres.SelectedValue);
            int idSustentante = Convert.ToInt32(IdSustentanteSesion());

            List<bdSustentanteAreaInteres> lista = logicaSustentanteAreaInteres.Leer(idSustentante) ?? new List<bdSustentanteAreaInteres>();

            // Validación de duplicados
            bool duplicado = lista.Any(c => c.IdAreaInteres == idAreaInteres);
            if (duplicado)
            {
                MostrarMensaje("El área de interés que intenta registrar ya existe en la lista", "warning", null);
                // Limpiar controles
                ddlIdAreaInteres.ClearSelection();
                return;
            }

            // Crear registro
            var parametro = new bdSustentanteAreaInteres
            {
                IdSustentante = idSustentante,
                IdAreaInteres = idAreaInteres
            };

            // Invocar la lógica de negocio para crear el registro y obtener el resultado
            var resultado = logicaSustentanteAreaInteres.Crear(parametro);
            MostrarMensaje(resultado.Item1, resultado.Item2, null);

            // Recargar gv
            GridViewAreaInteres();

            // Limpiar controles
            ddlIdAreaInteres.ClearSelection();
        }

        // Eliminar Area de Interés
        protected void gvConsultaAreaInteres_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int pId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                var control = logicaSustentanteAreaInteres.Eliminar(new bdSustentanteAreaInteres { IdSustentanteAreaInteres = pId });
                MostrarMensaje(control.Item1, control.Item2, null);
                GridViewAreaInteres();

            }
        }
        #endregion

        #region Habilidades
        // Cargar GvHabilidad
        protected List<DataRow> GridViewHabilidad()
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaSustentanteHabilidad.Consultar(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaHabilidad.Visible = true;
                master.CargarGridView(lista, gvConsultaHabilidad);
            }
            else
            {
                gvConsultaHabilidad.Visible = false;
            }

            return lista;
        }

        // Oculta columnas de GridView
        protected void gvConsultaHabilidad_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    e.Row.Cells[1].Visible = false;

                    GridViewRow headerRow = gvConsultaHabilidad.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[1].Visible = false;
                    }
                }
            }
        }

        // Agregar Habilidad
        protected void lbtnAgregarHabilidad_Click(object sender, EventArgs e)
        {
            // Validación de campos
            var diccionario = new Dictionary<object, string>
            {
                { "vIdTipoHabilidad", ddlIdTipoHabilidad.SelectedValue },
                { "vIdHabilidad", ddlIdHabilidad.SelectedValue }
            };

            List<string> listaValidacion = validacionSustentanteHabilidad.Validar(diccionario);

            if (listaValidacion != null && listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            // Declaracion de variables
            int idHabilidad = Convert.ToInt32(ddlIdHabilidad.SelectedValue);
            int idSustentante = Convert.ToInt32(IdSustentanteSesion());

            List<bdSustentanteHabilidad> lista = logicaSustentanteHabilidad.Leer(idSustentante) ?? new List<bdSustentanteHabilidad>();

            // Validación de duplicados
            bool habilidadDuplicado = lista.Any(c => c.IdHabilidad == idHabilidad);
            if (habilidadDuplicado)
            {
                MostrarMensaje("La habilidad que intenta registrar ya existe en la lista", "warning", null);
                // Limpiar controles
                ddlIdTipoHabilidad.ClearSelection();
                ddlIdHabilidad.ClearSelection();
                return;
            }

            // Crear registro
            var parametro = new bdSustentanteHabilidad
            {
                IdSustentante = idSustentante,
                IdHabilidad = idHabilidad
            };

            // Invocar la lógica de negocio para crear el registro y obtener el resultado
            var resultado = logicaSustentanteHabilidad.Crear(parametro);
            MostrarMensaje(resultado.Item1, resultado.Item2, null);

            // Recargar gv
            GridViewHabilidad();

            // Limpiar controles
            ddlIdTipoHabilidad.ClearSelection();
            ddlIdHabilidad.ClearSelection();
        }

        // Eliminar Habilidad
        protected void gvConsultaHabilidad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtener el ID del registro a eliminar desde el CommandArgument
            int pId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                // Invocar la lógica de negocio para eliminar el registro y obtener el resultado
                var control = logicaSustentanteHabilidad.Eliminar(new bdSustentanteHabilidad { IdSustentanteHabilidad = pId });
                MostrarMensaje(control.Item1, control.Item2, null);
                GridViewHabilidad();

            }
        }
        #endregion

        #region Software
        // Cargar GvSoftware
        protected List<DataRow> GridViewSoftware()
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaSustentanteManejoSoftware.Consultar(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaSoftware.Visible = true;
                master.CargarGridView(lista, gvConsultaSoftware);
            }
            else
            {
                gvConsultaSoftware.Visible = false;
            }

            return lista;
        }

        // Oculta columnas de GridView
        protected void gvConsultaSoftware_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    e.Row.Cells[1].Visible = false;

                    GridViewRow headerRow = gvConsultaSoftware.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[1].Visible = false;
                    }
                }
            }
        }

        // Agregar Paquete Software
        protected void lbtnAgregarPaqueteSoftware_Click(object sender, EventArgs e)
        {
            // Validación de campos
            var diccionario = new Dictionary<object, string>
            {
                { "vIdSoftware", ddlIdPaqueteSoftware.SelectedValue },
                { "vIdNivelSoftware", ddlIdNivelSoftware.SelectedValue }
            };

            List<string> listaValidacion = validacionSustentanteManejoSoftware.Validar(diccionario);

            if (listaValidacion != null && listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            // Declaracion de variables
            int idPaqueteSoftware = Convert.ToInt32(ddlIdPaqueteSoftware.SelectedValue);
            int idNivelSoftware = Convert.ToInt32(ddlIdNivelSoftware.SelectedValue);
            int idSustentante = Convert.ToInt32(IdSustentanteSesion());

            List<bdSustentanteManejoSoftware> lista = logicaSustentanteManejoSoftware.Leer(idSustentante) ?? new List<bdSustentanteManejoSoftware>();

            // Validación de duplicados
            bool softwareDuplicado = lista.Any(c => c.IdPaqueteSoftware == idPaqueteSoftware);
            if (softwareDuplicado)
            {
                MostrarMensaje("El paquete de software que intenta registrar ya existe en la lista", "warning", null);
                // Limpiar controles
                ddlIdPaqueteSoftware.ClearSelection();
                ddlIdNivelSoftware.ClearSelection();
                return;
            }

            // Crear registro
            var parametro = new bdSustentanteManejoSoftware
            {
                IdSustentante = idSustentante,
                IdPaqueteSoftware = idPaqueteSoftware,
                IdNivelSoftware = idNivelSoftware
            };

            // Invocar la lógica de negocio para crear el registro y obtener el resultado
            var resultado = logicaSustentanteManejoSoftware.Crear(parametro);
            MostrarMensaje(resultado.Item1, resultado.Item2, null);

            // Recargar gv
            GridViewSoftware();

            // Limpiar controles
            ddlIdPaqueteSoftware.ClearSelection();
            ddlIdNivelSoftware.ClearSelection();
        }

        // Eliminar Paquete Software
        protected void gvConsultaSoftware_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtener el ID del registro a eliminar desde el CommandArgument
            int pId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                // Invocar la lógica de negocio para eliminar el registro y obtener el resultado
                var control = logicaSustentanteManejoSoftware.Eliminar(new bdSustentanteManejoSoftware { IdManejoSoftwareSustentante = pId });
                MostrarMensaje(control.Item1, control.Item2, null);
                GridViewSoftware();

            }
        }
        #endregion

        #region Idioma
        // Cargar GvIdioma
        protected List<DataRow> GridViewIdioma()
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaSustentanteIdioma.Consultar(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaIdioma.Visible = true;
                master.CargarGridView(lista, gvConsultaIdioma);
            }
            else
            {
                gvConsultaIdioma.Visible = false;
            }

            return lista;
        }

        // Oculta columnas de GridView
        protected void gvConsultaIdioma_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;

                    GridViewRow headerRow = gvConsultaIdioma.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[1].Visible = false;
                        headerRow.Cells[2].Visible = false;
                    }
                }
            }
        }

        // Agregar Idioma
        protected void lbtnAgregarIdioma_Click(object sender, EventArgs e)
        {
            // Validación de campos
            var diccionario = new Dictionary<object, string>
            {
                { "vIdIdioma", ddlIdIdioma.SelectedValue },
                { "vIdNivelIdioma", ddlIdNivelIdioma.SelectedValue }
            };

            List<string> listaValidacion = validacionSustentanteIdioma.Validar(diccionario);

            if (listaValidacion != null && listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            // Declaracion de variables
            int idIdioma = Convert.ToInt32(ddlIdIdioma.SelectedValue);
            int idNivelIdioma = Convert.ToInt32(ddlIdNivelIdioma.SelectedValue);
            int idSustentante = Convert.ToInt32(IdSustentanteSesion());

            List<bdSustentanteIdioma> lista = logicaSustentanteIdioma.Leer(idSustentante) ?? new List<bdSustentanteIdioma>();

            // Validación de duplicados
            bool idiomaDuplicado = lista.Any(c => c.IdIdioma == idIdioma);
            if (idiomaDuplicado)
            {
                MostrarMensaje("El idioma que intenta registrar ya existe en la lista", "warning", null);
                // Limpiar controles
                ddlIdIdioma.ClearSelection();
                ddlIdNivelIdioma.ClearSelection();
                return;
            }

            // Crear registro
            var parametro = new bdSustentanteIdioma
            {
                IdSustentante = idSustentante,
                IdIdioma = idIdioma,
                IdNivelIdioma = idNivelIdioma
            };

            // Invocar la lógica de negocio para crear el registro y obtener el resultado
            var resultado = logicaSustentanteIdioma.Crear(parametro);
            MostrarMensaje(resultado.Item1, resultado.Item2, null);

            // Recargar Gridview
            GridViewIdioma();

            // Limpiar controles
            ddlIdIdioma.ClearSelection();
            ddlIdNivelIdioma.ClearSelection();
        }

        // Eliminar Idioma
        protected void gvConsultaIdioma_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtener el ID del registro a eliminar desde el CommandArgument
            int pId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                // Invocar la lógica de negocio para eliminar el registro y obtener el resultado
                var control = logicaSustentanteIdioma.Eliminar(new bdSustentanteIdioma { IdSustentanteIdioma = pId });
                MostrarMensaje(control.Item1, control.Item2, null);
                GridViewIdioma();

            }
        }
        #endregion

        #region Certificacion Idioma
        // Cargar GvCertificacionIdioma
        protected List<DataRow> GridViewCertificacionIdioma()
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaSustentanteCertificacionIdioma.Consultar(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaCertificacionIdioma.Visible = true;
                master.CargarGridView(lista, gvConsultaCertificacionIdioma);
            }
            else
            {
                gvConsultaCertificacionIdioma.Visible = false;
            }

            return lista;
        }

        // Oculta columnas de GridView
        protected void gvConsultaCertificacionIdioma_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    e.Row.Cells[1].Visible = false;

                    GridViewRow headerRow = gvConsultaCertificacionIdioma.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[1].Visible = false;
                    }
                }
            }
        }

        // Cargar DropDown dependiendo del Idioma Seleccionado
        protected void ddlIdIdiomaCertificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pidIdiomaCertificacion = Convert.ToInt32(ddlIdIdiomaCertificacion.SelectedItem.Value);
            master.CargarDropDownList(ddlIdCertificacionIdioma, 28, pidIdiomaCertificacion);
        }

        // Agregar CertificacionIdioma
        protected void lbtnAgregarCertificacionIdioma_Click(object sender, EventArgs e)
        {
            // Validación de campos
            var diccionario = new Dictionary<object, string>
            {
                { "vIdIdioma", ddlIdIdiomaCertificacion.SelectedValue },
                { "vIdCertificacionIdioma", ddlIdCertificacionIdioma.SelectedValue },

            };

            List<string> listaValidacion = validacionSustentanteCertificacionIdioma.Validar(diccionario);

            if (listaValidacion != null && listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            // Declaracion de variables
            int idCertificacionIdioma = Convert.ToInt32(ddlIdCertificacionIdioma.SelectedValue);
            int idSustentante = Convert.ToInt32(IdSustentanteSesion());

            List<bdSustentanteCertificacionIdioma> lista = logicaSustentanteCertificacionIdioma.Leer(idSustentante) ?? new List<bdSustentanteCertificacionIdioma>();

            // Validación de duplicados
            bool idiomaDuplicado = lista.Any(c => c.IdCertificacionIdioma == idCertificacionIdioma);
            if (idiomaDuplicado)
            {
                MostrarMensaje("La Certificacion de Idioma que intenta registrar ya existe en la lista", "warning", null);
                // Limpiar controles
                ddlIdIdiomaCertificacion.ClearSelection();
                ddlIdCertificacionIdioma.ClearSelection();
                return;
            }

            // Crear registro
            var parametro = new bdSustentanteCertificacionIdioma
            {
                IdSustentante = idSustentante,
                IdCertificacionIdioma = idCertificacionIdioma
            };

            // Invocar la lógica de negocio para crear el registro y obtener el resultado
            var resultado = logicaSustentanteCertificacionIdioma.Crear(parametro);
            MostrarMensaje(resultado.Item1, resultado.Item2, null);

            // Recargar Gridview
            GridViewCertificacionIdioma();

            // Limpiar controles
            ddlIdIdiomaCertificacion.ClearSelection();
            ddlIdCertificacionIdioma.ClearSelection();
        }

        // Eliminar CertificacionIdioma
        protected void gvConsultaCertificacionIdioma_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtener el ID del registro a eliminar desde el CommandArgument
            int pId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                // Invocar la lógica de negocio para eliminar el registro y obtener el resultado
                var control = logicaSustentanteCertificacionIdioma.Eliminar(new bdSustentanteCertificacionIdioma { IdSustentanteCertificacionIdioma = pId });
                MostrarMensaje(control.Item1, control.Item2, null);
                GridViewCertificacionIdioma();

            }
        }
        #endregion

        #region Documento
        // Cargar GvDocumento
        protected List<DataRow> GridViewDocumento()
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaDocumento.Consultar(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaDocumento.Visible = true;
                master.CargarGridView(lista, gvConsultaDocumento);
            }
            else
            {
                gvConsultaDocumento.Visible = false;
            }

            return lista;
        }

        // Oculta columnas de GridView
        protected void gvConsultaDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // 1. Asegurarse de que es una fila de datos
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    e.Row.Cells[1].Visible = false; // Ocultas Celda 1
                    GridViewRow headerRow = gvConsultaDocumento.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[1].Visible = false;
                    }
                }

                string tipo = e.Row.Cells[2].Text.Trim();
                string estatus = e.Row.Cells[3].Text.Trim();

                // Tu nueva condición combinada
                if (tipo.Equals("FOTOGRAFIA", StringComparison.OrdinalIgnoreCase) ||
                    estatus.Equals("APROBADO", StringComparison.OrdinalIgnoreCase))
                {
                    // Buscamos el LinkButton
                    LinkButton lbtn = (LinkButton)e.Row.FindControl("btnEliminarDocumento");

                    if (lbtn != null)
                    {
                        // Lo deshabilitamos
                        lbtn.Visible = false;
                        lbtn.CssClass = "btn btn-danger disabled";
                        lbtn.OnClientClick = ""; // Anulamos el JS por si acaso
                    }
                }
            }
        }
        #endregion

        #region Información Académica

        // Seleccionar valor seguro en DropDownList
        private void SeleccionarValorSeguro(DropDownList ddl, string valor)
        {
            // Si el valor es nulo, vacío o "0", selecciona "0" y retorna
            if (string.IsNullOrEmpty(valor) || valor == "0")
            {
                ddl.SelectedValue = "0";
                return;
            }

            // Verifica si el valor existe en la lista de elementos del DropDownList
            if (ddl.Items.FindByValue(valor) != null)
            {
                ddl.SelectedValue = valor;
            }
            else
            {
                // Si no existe (es un año antiguo), lo agrega
                ddl.Items.Add(new ListItem(valor, valor));
                ddl.SelectedValue = valor;
            }
        }

        // Cargar los valores de los DropDownList según la información del sustentante
        protected void ddlIdCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIdTipoSustentante.SelectedValue == "1")
            {
                // Si el tipo de sustentante es "Estudiante", habilita el ddlIdPlanEstudios y carga los planes de estudio correspondientes a la carrera seleccionada
                ddlIdPlanEstudios.Enabled = true;
                master.CargarDropDownList(ddlIdPlanEstudios, 24, int.Parse(ddlIdCarrera.SelectedValue));
            }
        }

        // Cargar los valores de los DropDownList según la información del sustentante
        protected void ddlIdPlanEstudios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIdPlanEstudios.SelectedValue != "0")
            {
                // Si se selecciona un plan de estudios, habilita el ddlIdModalidad y carga las modalidades correspondientes al plan de estudios seleccionado
                ddlIdModalidad.Enabled = true;
                master.CargarDropDownList(ddlIdModalidad, 25, int.Parse(ddlIdPlanEstudios.SelectedValue));
            }
        }

        // Cargar los valores de los DropDownList según la información del sustentante
        protected void ddlIdTipoSustentante_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<bdSustentante> lista = logicaSustentante.Leer(IdSustentanteSesion());
            if (ddlIdTipoSustentante.SelectedValue == "1")
            {
                // Si el tipo de sustentante es "Estudiante", habilita el ddlIdPlanEstudios y carga los planes de estudio correspondientes a la carrera seleccionada
                if (lista[0].IdTipoSustentante != 1)
                {
                    ddlIdPlanEstudios.Enabled = true;
                    ddlIdModalidad.ClearSelection();
                    ddlIdModalidad.Enabled = false;
                    master.CargarDropDownList(ddlIdPlanEstudios, 24, int.Parse(ddlIdCarrera.SelectedValue));
                }

                // Si el tipo de sustentante es "Estudiante", muestra los campos correspondientes a estudiante y oculta los de egresado
                phEgresado.Visible = false;
                phEstudiante.Visible = true;
                lblPromedio.Visible = true;
                txtPromedio.Visible = true;
                lblOtraCarrera.Visible = true;
                txtOtraCarrera.Visible = true;
            }
            // Si el tipo de sustentante es "Egresado", muestra los campos correspondientes a egresado y oculta los de estudiante
            else if (ddlIdTipoSustentante.SelectedValue == "2")
            {
                phEgresado.Visible = true;
                phEstudiante.Visible = false;
                lblPromedio.Visible = true;
                txtPromedio.Visible = true;
                lblOtraCarrera.Visible = true;
                txtOtraCarrera.Visible = true;

            }
            // Si el tipo de sustentante es "Otro", oculta los campos correspondientes a estudiante y egresado
            else
            {
                phEgresado.Visible = false;
                phEstudiante.Visible = false;
                lblPromedio.Visible = false;
                txtPromedio.Visible = false;
                lblOtraCarrera.Visible = false;
                txtOtraCarrera.Visible = false;
            }
        }
        protected void btnGrabarTipoSustentante_Click(object sender, EventArgs e)
        {
            bdSustentante parametro = new bdSustentante
            {
                IdSustentante = IdSustentanteSesion(),
                IdTipoSustentante = int.Parse(ddlIdTipoSustentante.SelectedValue)
            };

            // Evalúa si se trata de un nuevo registro o edición por medio de la variable pública (vpId)
            Tuple<string, string> control;

            // Si el IdSustentanteSesion() es mayor a 0, se trata de una edición
            control = logicaSustentante.EditarTipoSustentante(parametro);
            MostrarMensaje(control.Item1, control.Item2, null);
            LeerSustentante();
        }

        // Guardar Información Académica
        protected void btnGrabarAcademico_Click(object sender, EventArgs e)
        {
            List<bdExpedienteAcademico> listaExpediente = logicaExpedienteAcademico.Leer(IdSustentanteSesion());

            // Si no hay registros, se trata de un nuevo registro, de lo contrario es una edición
            string valorServicio = rbServicioSi.Checked ? "1" : (rbServicioNo.Checked ? "0" : "");
            string valorPracticas = rbPracticasSi.Checked ? "1" : (rbPracticasNo.Checked ? "0" : "");

            // Validación de campos y formatos de entrada
            Dictionary<object, string> diccionario = new Dictionary<object, string>
                {
                    { "vTipoSustentante", ddlIdTipoSustentante.SelectedValue},
                    { "vMatricula", txtMatricula.Text },
                    { "vTipoGrado", ddlIdTipoGrado.SelectedValue },
                    { "vCarrera", ddlIdCarrera.SelectedValue },
                    { "vPlan", ddlIdPlanEstudios.SelectedValue },
                    { "vModalidad", ddlIdModalidad.SelectedValue },
                    { "vSemestre", ddlIdSemestre.SelectedValue },
                    { "vTurnoEscolar", ddlIdTurnoEscolar.SelectedValue },
                    { "vAnioIngreso", ddlAnioIngreso.SelectedValue },
                    { "vAnioEgreso", ddlAnioEgreso.SelectedValue },
                    { "vEstatusAcademico", ddlIdEstatusAcademico.SelectedValue },
                    { "vEstatusTitulacion", ddlIdEstatusTitulacion.SelectedValue },
                    { "vTieneServicio", valorServicio },
                    { "vTienePracticas", valorPracticas }
                };

            List<string> listaValidacion = validacionExpedienteAcademico.Validar(diccionario);

            if (listaValidacion.Count != 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            // Declaración de variables
            int? idSemestre = null;
            int? idTurnoEscolar = null;
            int anioIngresoInt = 0;
            int anioEgresoInt = 0;
            int? idEstatusAcademico = null;
            int? idEstatusTitulacion = null;
            int? idPlanSucio = null;
            int? idModalidad = null;
            decimal valorTemporal;
            int tempValor;
            decimal? promedioParaGuardar = null;

            // Validación y conversión del promedio
            if (!string.IsNullOrWhiteSpace(txtPromedio.Text) &&
                decimal.TryParse(txtPromedio.Text, out valorTemporal))
            {
                // Si no está vacío y SÍ se pudo convertir, asigna el valor
                promedioParaGuardar = valorTemporal;
            }

            // Validación y conversión de los valores seleccionados en los DropDownList
            if (int.TryParse(ddlIdSemestre.SelectedValue, out tempValor) && tempValor > 0)
            {
                idSemestre = tempValor;
            }

            if (int.TryParse(ddlIdTurnoEscolar.SelectedValue, out tempValor) && tempValor > 0)
            {
                idTurnoEscolar = tempValor;
            }

            if (int.TryParse(ddlAnioIngreso.SelectedValue, out tempValor) && tempValor > 0)
            {
                anioIngresoInt = tempValor;
            }

            if (int.TryParse(ddlAnioEgreso.SelectedValue, out tempValor) && tempValor > 0)
            {
                anioEgresoInt = tempValor;
            }

            if (int.TryParse(ddlIdEstatusAcademico.SelectedValue, out tempValor) && tempValor > 0)
            {
                idEstatusAcademico = tempValor;
            }

            if (int.TryParse(ddlIdEstatusTitulacion.SelectedValue, out tempValor) && tempValor > 0)
            {
                idEstatusTitulacion = tempValor;
            }

            DateTime? fechaIngreso = null;
            DateTime? fechaEgreso = null;

            // Asigna solo si el año es válido
            if (anioIngresoInt > 0)
            {
                fechaIngreso = new DateTime(anioIngresoInt, 1, 1);
            }

            if (anioEgresoInt > 0)
            {
                fechaEgreso = new DateTime(anioEgresoInt, 1, 1);
            }

            if (int.TryParse(ddlIdPlanEstudios.SelectedValue, out int tempVal))
            {
                idPlanSucio = tempVal;
            }

            int? idPlanLimpio = idPlanSucio % 100000;

            if (int.TryParse(ddlIdModalidad.SelectedValue, out tempValor) && tempValor > 0)
            {
                idModalidad = tempValor;
            }

            bdExpedienteAcademico parametro = new bdExpedienteAcademico
            {
                IdSustentante = IdSustentanteSesion(),
                IdTipoSustentante = int.Parse(ddlIdTipoSustentante.SelectedValue),
                Matricula = txtMatricula.Text.ToUpper(),
                IdTipoGrado = int.Parse(ddlIdTipoGrado.SelectedValue),
                IdCarrera = int.Parse(ddlIdCarrera.SelectedValue),
                IdPlanEstudio = idPlanLimpio,
                IdModalidad = idModalidad,
                IdSemestre = idSemestre,
                IdTurnoEscolar = idTurnoEscolar,
                AnioIngreso = fechaIngreso,
                AnioEgreso = fechaEgreso,
                IdEstatusAcademico = idEstatusAcademico,
                IdEstatusTitulacion = idEstatusTitulacion,
                HasServicioSocial = (valorServicio == "1"),
                HasPracticasProfesionales = (valorPracticas == "1"),
                OtraCarrera = string.IsNullOrWhiteSpace(txtOtraCarrera.Text) ? null : txtOtraCarrera.Text.ToUpper(),
                Promedio = promedioParaGuardar
            };

            Tuple<string, string> control;

            if (listaExpediente.Count == 0)
                // Si no hay registros, se trata de un nuevo registro
                control = logicaExpedienteAcademico.Crear(parametro);
            else
                // Si hay registros, se trata de una edición
                control = logicaExpedienteAcademico.Editar(parametro);

            string valorFinal = (parametro.OtraCarrera ?? "").ToUpper();

            // Generamos un script para forzar el valor en el TextBox txtOtraCarrera después de la actualización
            string script = $@"
                var txt = document.getElementById('{txtOtraCarrera.ClientID}');
                if(txt) {{
                    txt.value = '{valorFinal}';
                    // Opcional: Agregar clase visual para que se note que cambió
                }}
            ";

            // Ejecutamos este script inmediatamente al terminar la carga AJAX
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ForceUpperText", script, true);

            // Limpiar controles según el tipo de sustentante
            if (ddlIdTipoSustentante.SelectedValue == "1")
            {
                // Si el tipo de sustentante es "Estudiante", muestra los campos correspondientes a estudiante y oculta los de egresado
                phEstudiante.Visible = true;
                ddlAnioIngreso.ClearSelection();
                ddlAnioEgreso.ClearSelection();
                ddlIdEstatusAcademico.ClearSelection();
                ddlIdEstatusTitulacion.ClearSelection();

            }
            else if (ddlIdTipoSustentante.SelectedValue == "2")
            {
                // Si el tipo de sustentante es "Egresado", muestra los campos correspondientes a egresado y oculta los de estudiante
                phEgresado.Visible = true;
                ddlIdSemestre.ClearSelection();
                ddlIdTurnoEscolar.ClearSelection();
                rbServicioSi.Checked = false;
                rbServicioNo.Checked = false;
                rbPracticasSi.Checked = false;
                rbPracticasNo.Checked = false;
            }

            // Mostrar mensaje
            MostrarMensaje(control.Item1, control.Item2, null);
        }

        #endregion

        #region Experiencia Laboral

        // Cargar gvListaLaboral
        protected void gvListaLaboral_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Asignar el nuevo índice de página
            gvListaLaboral.PageIndex = e.NewPageIndex;

            // Recuperar la lista del ViewState con el tipo correcto
            if (ViewState["Actividades"] != null)
            {
                // CORRECCIÓN AQUÍ: Usar List<ActividadTemporal> en lugar de List<DataRow>
                List<ActividadTemporal> listaActividades = (List<ActividadTemporal>)ViewState["Actividades"];

                // Volver a enlazar la tabla con la nueva página
                gvListaLaboral.DataSource = listaActividades;
                gvListaLaboral.DataBind();
            }

            // Mantener el modal abierto si es necesario (opcional, depende de tu flujo)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#ModalNuevo').modal('show');", true);
        }
        [Serializable]
        // Clase para almacenar temporalmente las actividades laborales
        public class ActividadTemporal
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
        }

        // Agregar actividad laboral a la lista temporal
        protected void btnAgregarActividad_Click(object sender, EventArgs e)
        {
            // Validación de que la descripción no esté vacía
            if (string.IsNullOrWhiteSpace(txtDescripcionLaboral.Text))
            {
                return;
            }

            // Recuperar la lista de actividades del ViewState
            List<ActividadTemporal> listaActividades;

            // Crea una nueva lista si no existe en el ViewState, de lo contrario, recupera la existente
            if (ViewState["Actividades"] != null)
            {
                listaActividades = (List<ActividadTemporal>)ViewState["Actividades"];
            }
            else
            {
                listaActividades = new List<ActividadTemporal>();
            }

            // Crear un nuevo objeto ActividadTemporal y asignarle un ID único basado en la cantidad de elementos en la lista
            ActividadTemporal nuevaActividad = new ActividadTemporal();

            // Asignar un ID único basado en la cantidad de elementos en la lista
            nuevaActividad.Id = listaActividades.Count + 1;
            nuevaActividad.Descripcion = txtDescripcionLaboral.Text;

            // Agregar la nueva actividad a la lista
            listaActividades.Add(nuevaActividad);

            // Guardar la lista actualizada en el ViewState
            ViewState["Actividades"] = listaActividades;
            gvListaLaboral.DataSource = listaActividades;
            gvListaLaboral.DataBind();

            // Limpiar el TextBox de descripción después de agregar la actividad
            txtDescripcionLaboral.Text = string.Empty;

            // Mantener el modal abierto después de agregar la actividad
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop",
             "$('#ModalNuevo').modal('show');", true);
        }

        // Cerrar Modal Laboral
        protected void btnCerrarLaboral_Click(object sender, EventArgs e)
        {
            // Limpiar los campos del modal y el GridView
            txtPuesto.Text = string.Empty;
            txtEmpresa.Text = string.Empty;
            txtFechaInicioLaboral.Text = string.Empty;
            txtFechaFinLaboral.Text = string.Empty;
            txtDescripcionLaboral.Text = string.Empty;
            txtTipoTrabajo.Text = string.Empty;

            // Limpiar el GridView y el ViewState
            gvListaLaboral.DataSource = null;
            gvListaLaboral.DataBind();
            ViewState["Actividades"] = null;

            // Cerrar el modal y eliminar el backdrop
            string script = @"
                $('#ModalNuevo').modal('hide');
                $('.modal-backdrop').remove();
                $('body').removeClass('modal-open');
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CerrarModalScript", script, true);
        }

        // Ocultar columnas gvlistalaboral
        protected void gvListaLaboral_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtener la actividad temporal de la fila actual
                ActividadTemporal datosFila = (ActividadTemporal)e.Row.DataItem;
                Literal litResp = (Literal)e.Row.FindControl("litResponsabilidades");

                // Asignar la descripción de la actividad al Literal correspondiente
                if (litResp != null)
                {
                    litResp.Text = datosFila.Descripcion;
                }

                // Ocultar la columna de ID en las filas de datos y en el encabezado
                if (e.Row.Cells.Count > 1)
                {
                    e.Row.Cells[1].Visible = false;

                    GridViewRow headerRow = gvListaLaboral.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[0].Visible = false;
                        headerRow.Cells[1].Visible = false;
                        headerRow.Cells[2].Visible = false;
                    }
                }

            }
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Label lblPageInfo = (Label)e.Row.FindControl("lblPageInfo");
                if (lblPageInfo != null)
                {
                    int paginaActual = gvListaLaboral.PageIndex + 1;
                    int totalPaginas = gvListaLaboral.PageCount;
                    lblPageInfo.Text = $"Página {paginaActual} de {totalPaginas}";
                }

                LinkButton btnPrev = (LinkButton)e.Row.FindControl("btnPrevPage");
                LinkButton btnFirst = (LinkButton)e.Row.FindControl("btnFirstPage");
                LinkButton btnNext = (LinkButton)e.Row.FindControl("btnNextPage");
                LinkButton btnLast = (LinkButton)e.Row.FindControl("btnLastPage");

                if (btnPrev != null && btnFirst != null)
                {
                    bool esPrimeraPagina = gvListaLaboral.PageIndex == 0;
                    btnPrev.Enabled = !esPrimeraPagina;
                    btnFirst.Enabled = !esPrimeraPagina;

                    if (esPrimeraPagina)
                    {
                        btnPrev.CssClass += " disabled";
                        btnFirst.CssClass += " disabled";
                    }
                }

                if (btnNext != null && btnLast != null)
                {
                    bool esUltimaPagina = gvListaLaboral.PageIndex == (gvListaLaboral.PageCount - 1);
                    btnNext.Enabled = !esUltimaPagina;
                    btnLast.Enabled = !esUltimaPagina;

                    if (esUltimaPagina)
                    {
                        btnNext.CssClass += " disabled";
                        btnLast.CssClass += " disabled";
                    }
                }
            }
        }

        // Eliminar lista actividad laboral
        protected void gvListaLaboral_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                // Obtener el ID de la actividad temporal a eliminar desde el CommandArgument
                int idTemporal = Convert.ToInt32(e.CommandArgument);

                if (ViewState["Actividades"] != null)
                {
                    // Recuperar la lista de actividades del ViewState
                    List<ActividadTemporal> listaActividades = (List<ActividadTemporal>)ViewState["Actividades"];

                    // Buscar y eliminar la actividad temporal correspondiente al ID
                    ActividadTemporal itemParaEliminar = listaActividades.FirstOrDefault(act => act.Id == idTemporal);
                    if (itemParaEliminar != null)
                    {
                        listaActividades.Remove(itemParaEliminar);
                    }

                    // Actualizar el ViewState y volver a enlazar el GridView
                    ViewState["Actividades"] = listaActividades;
                    gvListaLaboral.DataSource = listaActividades;
                    gvListaLaboral.DataBind();
                }
            }
        }

        // Guardar experiencia laboral
        protected void btnGrabarLaboral_Click(object sender, EventArgs e)
        {
            // Validación de campos y formatos de entrada
            Dictionary<object, string> diccionario = new Dictionary<object, string>
            {
                { "vPuesto", txtPuesto.Text },
                { "vEmpresa", txtEmpresa.Text },
                { "vFechaInicio", txtFechaInicioLaboral.Text },
                { "vTipoTrabajo", txtTipoTrabajo.Text },

            };

            List<string> listaValidacion = validacionSustentanteExperienciaLaboral.Validar(diccionario);

            // Validación de que al menos una actividad haya sido agregada
            if (listaValidacion.Count != 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", "#ModalNuevo");
                return;
            }
            else if (gvListaLaboral.Rows.Count == 0)
            {
                MostrarMensaje("Debe incluir al menos 1 actividad", "warning", null);
                return;
            }

            // Recuperar la lista de actividades del ViewState
            List<ActividadTemporal> listaActividades = new List<ActividadTemporal>();
            if (ViewState["Actividades"] != null)
            {
                listaActividades = (List<ActividadTemporal>)ViewState["Actividades"];
            }

            List<string> descripciones = listaActividades.Select(act => act.Descripcion).ToList();

            // Convertir la lista de descripciones a un objeto JSON
            var objetoParaJson = new
            {
                responsibilities = descripciones
            };

            // Serializar el objeto a JSON
            string jsonDescripcion = JsonConvert.SerializeObject(objetoParaJson);

            // Validar y convertir las fechas de inicio y fin laboral
            DateTime.TryParse(txtFechaInicioLaboral.Text, out DateTime fechaInicioVal);
            DateTime? fechaFinVal = null;
            if (DateTime.TryParse(txtFechaFinLaboral.Text, out DateTime tempFechaFin))
            {
                fechaFinVal = tempFechaFin;
            }

            // Recuperar el ID de experiencia laboral de la sesión, si existe
            int vpId = 0;
            if (Session["vpId"] != null)
            {
                vpId = int.Parse(Session["vpId"].ToString());
            }

            bdExperienciaLaboral parametro = new bdExperienciaLaboral
            {
                IdExperienciaLaboral = vpId,
                IdSustentante = IdSustentanteSesion(),
                Puesto = txtPuesto.Text.ToUpper(),
                Empresa = txtEmpresa.Text.ToUpper(),
                TipoTrabajo = txtTipoTrabajo.Text.ToUpper(),
                FechaInicio = DateTime.Parse(txtFechaInicioLaboral.Text),
                FechaFin = fechaFinVal,
                Descripcion = jsonDescripcion.ToUpper()
            };


            Tuple<string, string> control;
            if (vpId == 0)
                // Si el ID es 0, se trata de un nuevo registro
                control = logicaSustentanteExperienciaLaboral.Crear(parametro);
            else
                // Si el ID es mayor a 0, se trata de una edición
                control = logicaSustentanteExperienciaLaboral.Editar(parametro);

            // Mostrar mensaje de resultado
            MostrarMensaje(control.Item1, control.Item2, null);

            // Limpiar controles
            if (control.Item2 == "success")
            {
                txtPuesto.Text = string.Empty;
                txtEmpresa.Text = string.Empty;
                txtFechaInicioLaboral.Text = string.Empty;
                txtFechaFinLaboral.Text = string.Empty;
                txtDescripcionLaboral.Text = string.Empty;
                txtTipoTrabajo.Text = string.Empty;
                ViewState["Actividades"] = null;
                Session["vpId"] = null;
                gvListaLaboral.DataBind();
                vpId = 0;
            }

            // Cerrar el modal y actualizar la lista de experiencia laboral
            ScriptManager.RegisterStartupScript(updModal, updModal.GetType(), "HideModalScript", "cerrarModalLaboral();", true);
            CargarExperiencia();
            updListaExperiencia.Update();
        }

        // Editar experiencia laboral
        protected void lnkEditarExperiencia_Click(object sender, EventArgs e)
        {
            // Recuperar el ID de experiencia laboral desde el CommandArgument del LinkButton
            LinkButton lnk = (LinkButton)sender;
            int pId = Convert.ToInt32(lnk.CommandArgument);

            // Recuperar la experiencia laboral correspondiente al ID
            List<bdExperienciaLaboral> lista = logicaSustentanteExperienciaLaboral.Leer(pId);

            string commandName = lnk.CommandName;

            if (commandName == "EditarExperiencia")
            {
                // Guardar el ID de experiencia laboral en la sesión para su uso posterior
                Session["vpId"] = pId;

                // Cargar los datos de la experiencia laboral en los controles del modal
                txtPuesto.Text = lista[0].Puesto.ToString();
                txtEmpresa.Text = lista[0].Empresa.ToString();
                txtFechaInicioLaboral.Text = lista[0].FechaInicio.ToString("yyyy-MM-dd");
                if (lista[0].FechaFin != null)
                {
                    txtFechaFinLaboral.Text = lista[0].FechaFin.Value.ToString("yyyy-MM-dd");
                }
                txtTipoTrabajo.Text = lista[0].TipoTrabajo.ToString();

                // Recuperar la descripción de la experiencia laboral y convertirla en una lista de actividades
                List<ActividadTemporal> listaActividades = new List<ActividadTemporal>();
                string jsonDescripcion = lista[0].Descripcion.ToString();

                if (!string.IsNullOrEmpty(jsonDescripcion))
                {
                    // Intentar parsear el JSON y convertirlo en una lista de actividades
                    try
                    {
                        var jsonObj = Newtonsoft.Json.Linq.JObject.Parse(jsonDescripcion);
                        var arrayItems = jsonObj["RESPONSIBILITIES"].ToObject<List<string>>();

                        int contadorId = 1;
                        foreach (var item in arrayItems)
                        {
                            listaActividades.Add(new ActividadTemporal
                            {
                                Id = contadorId,
                                Descripcion = item
                            });
                            contadorId++;
                        }
                    }
                    catch
                    {
                        listaActividades.Add(new ActividadTemporal { Id = 1, Descripcion = jsonDescripcion });
                    }
                }

                // Guardar la lista de actividades en el ViewState y enlazarla al GridView
                ViewState["Actividades"] = listaActividades;
                gvListaLaboral.DataSource = listaActividades;
                gvListaLaboral.DataBind();
                updModal.Update();
                AbrirModal("#ModalNuevo");

            }

        }

        // Eliminar experiencia laboral
        protected void lnkEliminarExperiencia_Click(object sender, EventArgs e)
        {
            // Recuperar el ID de experiencia laboral desde el CommandArgument del LinkButton
            LinkButton lnk = (LinkButton)sender;

            int pId = Convert.ToInt32(lnk.CommandArgument);
            string commandName = lnk.CommandName;

            if (commandName == "EliminarExperiencia")
            {
                // Llamar al método de eliminación de experiencia laboral
                var control = logicaSustentanteExperienciaLaboral.Eliminar(new bdExperienciaLaboral { IdExperienciaLaboral = pId });
                MostrarMensaje(control.Item1, control.Item2, null);
                CargarExperiencia();
            }
        }

        // Ocultar botones de edición y eliminación en modo lectura
        protected void lvExperiencia_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (_modoLectura && e.Item.ItemType == ListViewItemType.DataItem)
            {
                // Buscar el botón de eliminar dentro del item
                LinkButton lnkEliminar = (LinkButton)e.Item.FindControl("lnkEliminarExperiencia");
                LinkButton lnkEditar = (LinkButton)e.Item.FindControl("lnkEditarExperiencia");
                if (lnkEliminar != null)
                {
                    lnkEliminar.Text = ""; // Ocultarlo
                    lnkEliminar.Enabled = false;
                    lnkEliminar.Visible = false;

                }
                if (lnkEditar != null)
                {
                    lnkEditar.Text = ""; // Ocultarlo
                    lnkEditar.Enabled = false;
                    lnkEditar.Visible = false;

                }
            }
        }

        // Abrir modal para agregar nueva experiencia laboral
        protected void btnAgregarReporte_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(btnAgregarReporte, btnAgregarReporte.GetType(), "ShowModalScript", "abrirModalLaboral();", true);
        }

        /// <summary>
        /// Toma el string JSON de la descripción y lo convierte en una lista HTML (<ul>).
        /// Debe ser 'protected' o 'public' para ser visible desde el ASPX.
        /// </summary>
        protected string FormatearDescripcionComoLista(object descripcionObj)
        {
            string json = descripcionObj as string;

            // Si la descripción está vacía o no es un JSON, no hagas nada.
            if (string.IsNullOrEmpty(json) || !json.Trim().StartsWith("{"))
            {
                // Devuelve el texto original (pero "codificado" para HTML por seguridad)
                return HttpUtility.HtmlEncode(json);
            }

            try
            {
                // 1. Usamos el deserializador de JSON que viene con ASP.NET
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var data = serializer.Deserialize<Dictionary<string, object>>(json);

                // 2. Buscamos la llave "responsibilities"
                if (data != null && data.ContainsKey("RESPONSIBILITIES"))
                {
                    var responsabilidades = data["RESPONSIBILITIES"] as System.Collections.ArrayList;

                    if (responsabilidades != null && responsabilidades.Count > 0)
                    {
                        // 3. Construimos el HTML de la lista (<ul>)
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<ul class='mb-0'>"); // mb-0 quita el margen inferior

                        foreach (object item in responsabilidades)
                        {
                            // Usamos HtmlEncode por seguridad, por si el texto tiene < o >
                            sb.AppendFormat("<li>{0}</li>", HttpUtility.HtmlEncode(item.ToString()));
                        }
                        sb.Append("</ul>");

                        return sb.ToString();
                    }
                }

                // Si el JSON no tiene el formato esperado, devuelve el JSON tal cual
                return HttpUtility.HtmlEncode(json);
            }
            catch (Exception)
            {
                // Si falla el parsing, solo devuelve el texto original
                return HttpUtility.HtmlEncode(json);
            }
        }
        #endregion

        #region Certificados
        protected void btnAgregarCertificado_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(btnAgregarCertificado, btnAgregarCertificado.GetType(), "ShowModalScript", "abrirModalCertificado();", true);
        }

        protected void btnGrabarCertificado_Click(object sender, EventArgs e)
        {
            // Validación de campos y formatos de entrada
            Dictionary<object, string> diccionario = new Dictionary<object, string>
            {
                { "vDescripcion", txtDescripcion.Text },
                { "vInstitucionEmisora", txtInstiucionEmisora.Text },
                { "vFechaEmision", txtFechaEmision.Text },
            };

            List<string> listaValidacion = validacionSustentanteCertificado.Validar(diccionario);

            // 1. Guardamos los datos actuales en una Session TEMPORAL
            var datosTemp = new bdSustentanteCertificado
            {
                Descripcion = txtDescripcion.Text,
                InstitucionEmisora = txtInstiucionEmisora.Text,
                // Opción B: Mínimo valor posible (Cuidado con SQL Server antiguo)
                // Al Guardar (antes del Redirect):
                FechaEmision = string.IsNullOrEmpty(txtFechaEmision.Text)
               ? DateTime.MinValue  // Esto genera el 01/01/0001 interno
               : DateTime.Parse(txtFechaEmision.Text),
                NumeroCertificado = txtNumeroCertificado.Text,
                UrlVerificacion = txtUrlVerificacion.Text
            };

            Session["DatosTempCertificado"] = datosTemp; // Nombre diferente a tu lista principal

            if (listaValidacion.Count != 0)
            {
                Session["MensajePendiente"] = listaValidacion[0];
                Session["TipoMensaje"] = "warning";
                Session["TabActiva"] = "tab-certificados";

                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            else if (gvListaCertificado.Rows.Count == 0 && txtUrlVerificacion.Text == "")
            {
                Session["MensajePendiente"] = "Debe incluir al menos 1 referencia de certificado (URL ó Documento)";
                Session["TipoMensaje"] = "warning";
                Session["TabActiva"] = "tab-certificados";

                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // --- Lógica de Certificados (esto no cambia) ---
            List<CertificadoTemporal> listaCertificados = new List<CertificadoTemporal>();
            if (ViewState["Certificados"] != null)
            {
                listaCertificados = (List<CertificadoTemporal>)ViewState["Certificados"];
            }

            string rutaCertificadoParaDB = null;

            byte[] archivoBytes = Session["ArchivoBytesTemp"] as byte[];

            if (archivoBytes != null)
            {
                string destinoCarpeta = RetornarRutaCertificado();
                if (!Directory.Exists(destinoCarpeta))
                {
                    Directory.CreateDirectory(destinoCarpeta);
                }

                var listasus = logicaSustentante.Leer(IdSustentanteSesion());
                string NombreSustentante = listasus[0].Nombre + listasus[0].PrimerApellido;

                string nombreUsuario = txtInstiucionEmisora.Text;

                // Reemplazamos los caracteres prohibidos por nada ("") o por un guion bajo ("_")
                string nombreLimpio = nombreUsuario.Replace("/", "")
                                                   .Replace("\\", "")
                                                   .Replace("-", "_"); // Opcional: cambiar guion por guion bajo

                string nuevoNombre = $"CertificadoSustentante_{NombreSustentante}_{nombreLimpio}_{IdSustentanteSesion()}.pdf";
                string rutaCompleta = Path.Combine(destinoCarpeta, nuevoNombre);

                try
                {
                    // Guardar el archivo en el servidor
                    File.WriteAllBytes(rutaCompleta, archivoBytes);

                    // SI SE GUARDÓ CON ÉXITO, asignamos el nombre/ruta a nuestra variable
                    rutaCertificadoParaDB = nuevoNombre; // O 'rutaCompleta' si prefieres guardar la ruta total
                    LimpiarSession();

                }
                catch (Exception ex)
                {
                    // Si falló el guardado del archivo, SÍ debemos detenernos.
                    MostrarMensaje("Error CRÍTICO al guardar el archivo: " + ex.Message, "error", "#ModalNuevo");
                    LimpiarSession();

                    return;
                }
            }

            bdSustentanteCertificado parametro = new bdSustentanteCertificado
            {
                IdSustentante = IdSustentanteSesion(),
                Descripcion = txtDescripcion.Text.ToUpper(),
                InstitucionEmisora = txtInstiucionEmisora.Text.ToUpper(),
                FechaEmision = DateTime.Parse(txtFechaEmision.Text),
                NumeroCertificado = txtNumeroCertificado.Text.ToUpper(),
                UrlVerificacion = string.IsNullOrWhiteSpace(txtUrlVerificacion.Text) ? null : txtUrlVerificacion.Text,
                ArchivoCertificado = rutaCertificadoParaDB
            };

            Tuple<string, string> control;
            control = logicaSustentanteCertificado.Crear(parametro);

            MostrarMensaje(control.Item1, control.Item2, null);

            // Limpiar controles
            if (control.Item2 == "success")
            {
                txtDescripcion.Text = string.Empty;
                txtInstiucionEmisora.Text = string.Empty;
                txtFechaEmision.Text = string.Empty;
                txtNumeroCertificado.Text = string.Empty;
                txtUrlVerificacion.Text = string.Empty;
                gvListaLaboral.DataBind();
            }

            Session["MensajePendiente"] = control.Item1;
            Session["TipoMensaje"] = control.Item2;
            Session["DatosTempCertificado"] = null;
            Session["TabActiva"] = "tab-certificados";
            Session["ListaCertificados"] = null;

            Response.Redirect(Request.RawUrl, false);
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        protected void btnCerrarCertificado_Click(object sender, EventArgs e)
        {
            LimpiarSession();
            txtDescripcion.Text = string.Empty;
            txtInstiucionEmisora.Text = string.Empty;
            txtFechaEmision.Text = string.Empty;
            txtNumeroCertificado.Text = string.Empty;
            txtUrlVerificacion.Text = string.Empty;
            gvListaCertificado.DataSource = null;
            gvListaCertificado.DataBind();

            Session["TabActiva"] = "tab-certificados";

            Response.Redirect(Request.RawUrl, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void gvListaCertificado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Oculta columnas de GridView
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    e.Row.Cells[1].Visible = false;

                    GridViewRow headerRow = gvListaCertificado.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[0].Visible = false;
                        headerRow.Cells[1].Visible = false;
                        headerRow.Cells[2].Visible = false;
                    }
                }

            }
        }

        protected void gvListaCertificado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idTemporal = Convert.ToInt32(e.CommandArgument);

                if (Session["ListaCertificados"] != null)
                {
                    List<CertificadoTemporal> listaCertificado = (List<CertificadoTemporal>)Session["ListaCertificados"];

                    CertificadoTemporal itemParaEliminar = listaCertificado.FirstOrDefault(act => act.Id == idTemporal);
                    if (itemParaEliminar != null)
                    {
                        listaCertificado.Remove(itemParaEliminar);
                    }

                    Session["ListaCertificados"] = listaCertificado;
                    gvListaCertificado.DataSource = listaCertificado;
                    gvListaCertificado.DataBind();
                    AbrirModal("#ModalNuevo2");
                }
            }
        }

        protected void lnkEliminarCertificado_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            string commandName = lnk.CommandName;

            if (commandName == "EliminarCertificado")
            {
                string pId = lnk.CommandArgument;
                var control = logicaSustentanteCertificado.Eliminar(pId);

                MostrarMensaje(control.Item1, control.Item2, null);
                CargarCertificado();
            }
        }

        [Serializable]
        public class CertificadoTemporal
        {
            public int Id { get; set; }
            public string ArchivoCertificado { get; set; }
            public byte[] ArchivoBytes { get; set; }
        }

        protected void btnAgregarPdfCertificado_Click(object sender, EventArgs e)
        {
            // 1. Guardamos los datos actuales en una Session TEMPORAL
            var datosTemp = new bdSustentanteCertificado
            {
                Descripcion = txtDescripcion.Text,
                InstitucionEmisora = txtInstiucionEmisora.Text,
                FechaEmision = string.IsNullOrEmpty(txtFechaEmision.Text)
               ? DateTime.MinValue  // Esto genera el 01/01/0001 interno
               : DateTime.Parse(txtFechaEmision.Text),
                NumeroCertificado = txtNumeroCertificado.Text,
                UrlVerificacion = txtUrlVerificacion.Text
            };

            Session["DatosTempCertificado"] = datosTemp; // Nombre diferente a tu lista principal

            if (gvListaCertificado.Rows.Count > 0)
            {
                // El usuario no ha subido un archivo
                Session["MensajePendiente"] = "Ya existe un documento cargado";
                Session["TipoMensaje"] = "warning";
                Session["TabActiva"] = "tab-certificados";
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!fuCertificado.HasFile)
            {
                // El usuario no ha subido un archivo
                Session["MensajePendiente"] = "Por favor, carga un archivo formato PDF";
                Session["TipoMensaje"] = "warning";
                Session["TabActiva"] = "tab-certificados";
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            string fileExtension = System.IO.Path.GetExtension(fuCertificado.FileName);

            if (!fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                Session["MensajePendiente"] = "Tipo de archivo incorrecto. Solo se permiten archivos PDF";
                Session["TipoMensaje"] = "error";
                Session["TabActiva"] = "tab-certificados";
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            byte[] archivoBytes = fuCertificado.FileBytes;
            int maxFileSize = 10485760; // 10 MB

            if (archivoBytes.Length > maxFileSize)
            {
                Session["MensajePendiente"] = "El archivo es demasiado grande. El tamaño máximo permitido es 10 MB.";
                Session["TipoMensaje"] = "error";
                Session["TabActiva"] = "tab-certificados";
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            List<CertificadoTemporal> listaCertificado;

            if (Session["ListaCertificados"] != null)
            {
                listaCertificado = (List<CertificadoTemporal>)Session["ListaCertificados"];
            }
            else
            {
                listaCertificado = new List<CertificadoTemporal>();
            }

            // Creamos el nuevo objeto
            CertificadoTemporal nuevoArchivo = new CertificadoTemporal();
            nuevoArchivo.Id = listaCertificado.Count + 1;
            nuevoArchivo.ArchivoCertificado = fuCertificado.FileName;
            nuevoArchivo.ArchivoBytes = fuCertificado.FileBytes; // Guardamos bytes directo aquí

            listaCertificado.Add(nuevoArchivo);

            Session["ListaCertificados"] = listaCertificado;
            Session["ArchivoBytesTemp"] = nuevoArchivo.ArchivoBytes;
            Session["TabActiva"] = "tab-certificados";
            Response.Redirect(Request.RawUrl, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void lvCertificado_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (_modoLectura && e.Item.ItemType == ListViewItemType.DataItem)
            {
                // Buscar el botón de eliminar dentro del item
                LinkButton lnkEliminar = (LinkButton)e.Item.FindControl("lnkEliminarCertificado");
                if (lnkEliminar != null)
                {
                    lnkEliminar.Text = ""; // Ocultarlo
                    lnkEliminar.Enabled = false;
                }
            }
        }
        #endregion

        #region Documentos
        // Agregar documento
        protected void lbtnAgregarArchivo_Click(object sender, EventArgs e)
        {
            // Validación de que no exista un KARDEX aprobado
            foreach (GridViewRow row in gvConsultaDocumento.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string tipoArchivo = HttpUtility.HtmlDecode(row.Cells[2].Text).Trim();
                    string estatus = HttpUtility.HtmlDecode(row.Cells[3].Text).Trim();

                    if (tipoArchivo == "KARDEX" && estatus == "APROBADO")
                    {
                        MostrarMensaje("Ya cuentas con KARDEX aprobado", "warning", null);
                        ddlIdTipoArchivo.ClearSelection();
                        LimpiarSession(); // Asumo que este método limpia las variables temporales
                        return;
                    }
                }
            }
            // Tu primera validación (campos pendientes)
            List<bdSustentante> lista = logicaSustentante.ValidarSustentante(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                // Mostrar mensaje de error y redirigir
                Session["MensajePendiente"] = "Campos pendientes";
                Session["TipoMensaje"] = "error";
                Session["TabActiva"] = "tab-documentos";

                // Redirigir a la misma página para mostrar el mensaje
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            else
            {
                // Si no hay campos pendientes, ocultar la alerta
                divAlertaCampos.Visible = false;
            }

            // Guardar el archivo en la sesión temporal
            Session["ArchivoBytesTemp"] = fuDocumento.FileBytes;
            string fileExtension = System.IO.Path.GetExtension(fuDocumento.FileName);
            byte[] archivoBytes = (byte[])Session["ArchivoBytesTemp"];

            // Validación de que se haya seleccionado un tipo de archivo
            if (ddlIdTipoArchivo.SelectedValue == "0")
            {
                Session["MensajePendiente"] = "Por favor, selecciona un tipo de archivo";
                Session["TipoMensaje"] = "warning";
                Session["TabActiva"] = "tab-documentos";

                // Redirigir a la misma página para mostrar el mensaje
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Validación de que se haya subido un archivo
            if (!fuDocumento.HasFile)
            {
                // El usuario no ha subido un archivo
                Session["MensajePendiente"] = "Por favor, carga un archivo formato PDF";
                Session["TipoMensaje"] = "warning";
                Session["TabActiva"] = "tab-documentos";
                Session["TipoArchivo"] = ddlIdTipoArchivo.SelectedValue;

                // Redirigir a la misma página para mostrar el mensaje
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Validación de que el archivo sea PDF
            if (!fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                // El usuario ha subido un archivo que no es PDF
                Session["MensajePendiente"] = "Tipo de archivo incorrecto. Solo se permiten archivos PDF";
                Session["TipoMensaje"] = "error";
                Session["TabActiva"] = "tab-documentos";
                Session["TipoArchivo"] = ddlIdTipoArchivo.SelectedValue;

                // Redirigir a la misma página para mostrar el mensaje
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Validación de tamaño de archivo (10 MB)
            int maxFileSize = 10485760; // 10 MB

            // Validación de tamaño de archivo (10 MB)
            if (archivoBytes.Length > maxFileSize)
            {
                // El usuario ha subido un archivo que excede el tamaño máximo permitido
                Session["MensajePendiente"] = "El archivo es demasiado grande. El tamaño máximo permitido es 10 MB.";
                Session["TipoMensaje"] = "error";
                Session["TabActiva"] = "tab-documentos";

                // Redirigir a la misma página para mostrar el mensaje
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            bdSustentanteArchivo parametro = new bdSustentanteArchivo
            {
                IdTipoArchivo = int.Parse(ddlIdTipoArchivo.SelectedValue),
                IdSustentante = IdSustentanteSesion()
            };

            // Guardar el archivo en la carpeta correspondiente
            string destinoCarpeta = RetornarRutaDocumento();
            string nuevoNombre = $"Documento_{ddlIdTipoArchivo.SelectedItem}_{IdSustentanteSesion()}.pdf";
            string rutaCompleta = Path.Combine(destinoCarpeta, nuevoNombre);

            // Validación de existencia de carpeta
            try
            {
                File.WriteAllBytes(rutaCompleta, archivoBytes);
            }
            catch (Exception ex)
            {
                // Error al guardar
                MostrarMensaje($"Error al guardar el archivo: {ex.Message.Replace("'", "\\'")}", "error", null);
                return;
            }

            // Guardamos el nombre del documento en el objeto parametro
            parametro.NombreDocumento = nuevoNombre;
            var control = logicaSustentanteArchivo.Crear(parametro);

            // Actualizar la vista del GridView y limpiar selección
            LeerSustentante(); // Recargamos el GridView
            ddlIdTipoArchivo.ClearSelection();

            // Mostrar mensaje de resultado
            Session["MensajePendiente"] = control.Item1;
            Session["TipoMensaje"] = control.Item2;
            Session["TabActiva"] = "tab-personal";

            // Redirigir a la misma página para mostrar el mensaje
            Response.Redirect(Request.RawUrl, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        // Eliminar documento
        protected void gvConsultaDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Recuperar el ID del documento desde el CommandArgument del GridView
            int pId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                // Llamar al método de eliminación de documento
                var control = logicaSustentanteArchivo.Eliminar(new bdSustentanteArchivo { IdArchivoSustentante = pId });
                MostrarMensaje(control.Item1, control.Item2, null);
                LeerSustentante();

                if (control.Item2 == "success")
                {
                    divAlertaRevision.Visible = false;  // Muestra alerta de "En Revisión"
                }
            }
        }
        #endregion

        #region Métodos Auxiliares
        // Configuración inicial de acceso y carga de datos
        private void ConfigurarAccesoYDatos()
        {
            // Validación de sesión y permisos
            int estatus = IdEstatusSesion();

            // Simplificado: Si es 6 o 4, activa modo lectura.
            if (estatus == 6 || estatus == 4)
            {
                if (estatus == 6) _modoLectura = true;
                EstablecerModoLectura(true);
            }

            // Cargar datos iniciales
            CargarDropDownLists();
            LeerSustentante();
            Session.Remove("sesionFiltro");
            vpId = 0;
        }

        private void ConfigurarScriptsDePostback()
        {
            // Script para Tipo Sustentante
            string scriptSustentante = Page.ClientScript.GetPostBackEventReference(ddlIdTipoSustentante, "");
            string fullScriptSustentante = "sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');" + scriptSustentante;
            ddlIdTipoSustentante.Attributes.Add("onchange", fullScriptSustentante);

            // Script para Tipo Habilidad
            string scriptHabilidad = Page.ClientScript.GetPostBackEventReference(ddlIdTipoHabilidad, "");
            string fullScriptHabilidad = "sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');" + scriptHabilidad;
            ddlIdTipoHabilidad.Attributes.Add("onchange", fullScriptHabilidad);

            // Script para Carrera
            string scriptCarrera = Page.ClientScript.GetPostBackEventReference(ddlIdCarrera, "");
            string fullScriptCarrera = "sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');" + scriptCarrera;
            ddlIdCarrera.Attributes.Add("onchange", fullScriptCarrera);

            // Script para Plan de Estudios
            string scriptPlanEstudios = Page.ClientScript.GetPostBackEventReference(ddlIdPlanEstudios, "");
            string fullscriptPlanEstudios = "sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');" + scriptPlanEstudios;
            ddlIdPlanEstudios.Attributes.Add("onchange", fullscriptPlanEstudios);

            // Script para Certificacion de Idiomas
            string scriptIdiomaCertificacion = Page.ClientScript.GetPostBackEventReference(ddlIdIdiomaCertificacion, "");
            string fullscriptIdiomaCertificacion = "sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');" + scriptIdiomaCertificacion;
            ddlIdIdiomaCertificacion.Attributes.Add("onchange", fullscriptIdiomaCertificacion);
        }

        private void ManejarPestanaActiva()
        {
            // Recuperar la pestaña activa de la sesión y activar el Tab correspondiente
            if (Session["TabActiva"] == null) return;

            // Recuperar el ID del Tab activo de la sesión
            string tabId = Session["TabActiva"].ToString();

            // Recuperar lista de certificados si existe
            List<CertificadoTemporal> listaCertificado = Session["ListaCertificados"] as List<CertificadoTemporal> ?? new List<CertificadoTemporal>();

            // Activar el Tab visualmente en el frontend
            RegistrarScriptActivacionTab(tabId);

            // Lógica específica por Tab
            switch (tabId)
            {
                // Dependiendo del Tab activo, se ejecutan diferentes métodos
                case "tab-documentos":
                    ProcesarTabDocumentos();
                    break;
                case "tab-certificados":
                    ProcesarTabCertificados(listaCertificado);
                    break;
            }
        }

        private void ProcesarTabDocumentos()
        {
            if (Session["MensajePendiente"] as string == "Campos pendientes")
            {
                var lista = logicaSustentante.ValidarSustentante(IdSustentanteSesion());
                divAlertaCampos.Visible = true;

                // Construcción de lista HTML
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append($"<strong>¡Atención! Tienes {lista.Count} apartado(s) pendiente:</strong><ul class='mb-0 mt-2'>");

                foreach (var item in lista)
                {
                    // Ponemos el Título de la categoría
                    sb.Append($"<li><strong>{System.Web.HttpUtility.HtmlEncode(item.Titulo)}</strong>");

                    // Dividimos los campos faltantes usando la coma como separador
                    string[] campos = item.Faltantes.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                    // Creamos la sub-lista para los campos
                    sb.Append("<ul>");
                    foreach (var campo in campos)
                    {
                        sb.Append($"<li>{System.Web.HttpUtility.HtmlEncode(campo)}</li>");
                    }
                    sb.Append("</ul>"); // Cerramos la sub-lista

                    sb.Append("</li>"); // Cerramos el elemento principal
                }

                sb.Append("</ul>"); // Cerramos la lista principal

                ltlCamposFaltantes.Text = sb.ToString();
                ddlIdTipoArchivo.ClearSelection();
            }
            else if (Session["TipoArchivo"] != null)
            {
                ddlIdTipoArchivo.SelectedValue = Session["TipoArchivo"].ToString();
            }

            LimpiarSession();
        }

        private void ProcesarTabCertificados(List<CertificadoTemporal> listaCertificado)
        {
            if (Session["DatosTempCertificado"] != null)
            {
                // Si hay datos temporales de certificado, llenamos el formulario con ellos
                var datos = (bdSustentanteCertificado)Session["DatosTempCertificado"];
                LlenarFormularioCertificado(datos);
                Session["DatosTempCertificado"] = null; // Limpiar inmediata
            }

            // Si hay una lista de certificados en la sesión, la usamos para llenar el GridView
            ViewState["ListaCertificados"] = listaCertificado;
            gvListaCertificado.DataSource = listaCertificado;
            gvListaCertificado.DataBind();

            // Lógica para reabrir el modal si hay datos o errores
            bool hayMensajeError = Session["MensajePendiente"] != null && (string)Session["TipoMensaje"] != "success";
            bool hayDatos = listaCertificado != null && listaCertificado.Count > 0;

            // Si hay datos o un mensaje de error, abrimos el modal
            if (hayDatos || hayMensajeError)
            {
                AbrirModal("#ModalNuevo2");
            }
        }

        // Método para llenar el formulario de certificado con datos existentes
        private void LlenarFormularioCertificado(bdSustentanteCertificado datos)
        {
            txtDescripcion.Text = datos.Descripcion;
            txtInstiucionEmisora.Text = datos.InstitucionEmisora;
            txtFechaEmision.Text = (datos.FechaEmision == DateTime.MinValue) ? string.Empty : datos.FechaEmision.ToString("yyyy-MM-dd");
            txtNumeroCertificado.Text = datos.NumeroCertificado;
            txtUrlVerificacion.Text = datos.UrlVerificacion;
        }

        // Método para abrir un modal específico usando JavaScript
        private void ManejarMensajesGlobales()
        {
            // Si no hay mensaje pendiente, no hacemos nada
            if (Session["MensajePendiente"] == null) return;

            // Recuperamos el mensaje y el tipo de mensaje de la sesión
            string mensaje = Session["MensajePendiente"].ToString();
            string tipo = Session["TipoMensaje"]?.ToString() ?? "info";

            // Construimos el script para mostrar el mensaje usando la función mostrarMensaje
            string script = $@"
            setTimeout(function() {{
                if (typeof mostrarMensaje === 'function') {{
                    mostrarMensaje('{System.Web.HttpUtility.JavaScriptStringEncode(mensaje)}', '{System.Web.HttpUtility.JavaScriptStringEncode(tipo)}', null); 
                }} else {{
                    console.error('La función mostrarMensaje no está definida.');
                }}
            }}, 200);";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "AlertaPostRedirect", script, true);

            // Caso especial: Salir sin borrar sesión ni mostrar alerta si el mensaje es este específico
            if (mensaje == "Ya existe un documento cargado")
            {
                Session["MensajePendiente"] = null;
                Session["TipoMensaje"] = null;
                return;
            }
            if (tipo == "success")
            {
                LimpiarSession();
            }
            Session["MensajePendiente"] = null;
            Session["TipoMensaje"] = null;
        }

        // Método para registrar un script que active un Tab específico al cargar la página
        private void RegistrarScriptActivacionTab(string tabId)
        {
            string scriptTab = $@"
            document.addEventListener('DOMContentLoaded', function() {{
            var triggerEl = document.getElementById('{tabId}');
            if(triggerEl) {{
                var tab = new bootstrap.Tab(triggerEl);
                tab.show();
            }}
            }});";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ActivarTabScript", scriptTab, true);
        }

        #endregion

        // Limpiar todas las variables de sesión relacionadas con la carga de archivos y certificados
        protected void LimpiarSession()
        {
            Session["UploadError"] = null;
            Session["ArchivoBytesTemp"] = null;
            Session["ArchivoNombreTemp"] = null;
            Session["Extension"] = null;
            Session["TipoArchivo"] = null;
            Session["TabActiva"] = null;
            Session["ListaCertificados"] = null;
            Session["DatosTempCertificado"] = null;
        }

        // Error
        protected string ValidarDestino(object urlObj)
        {
            string url = Convert.ToString(urlObj);

            // Si está vacío, devolvemos vacío (para ocultar el botón después)
            if (string.IsNullOrEmpty(url)) return "";

            // CASO EXTERNO (http/https)
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                // Solo verificamos que la URL tenga formato válido
                bool esUrlValida = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (!esUrlValida)
                {
                    // El texto no es un link válido -> Mandar a tu 404
                    return ResolveUrl("~/404.aspx");
                }

                // Si el formato es válido, lo dejamos pasar.
                // NOTA: Si la página externa está caída, el navegador mostrará el error de esa página, no el tuyo.
                return url;
            }

            // CASO LOCAL (Archivos en tu servidor)
            else
            {
                try
                {
                    // Verificamos si existe físicamente
                    string rutaFisica = Server.MapPath(url);
                    if (!System.IO.File.Exists(rutaFisica))
                    {
                        // Archivo local borrado o no encontrado -> Mandar a tu 404
                        return ResolveUrl("~/404.aspx");
                    }
                    return url;
                }
                catch
                {
                    // Si la ruta local tiene caracteres ilegales -> Mandar a tu 404
                    return ResolveUrl("~/404.aspx");
                }
            }
        }
    }
}
