using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Logica;
using FACPYA.BolsaDeTrabajo.Logica.Validacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class PerfilEmpresa : System.Web.UI.Page
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

        public int IdEmpresaSesion()
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return 0; // Esto no se ejecutará, pero es necesario para que compile.
            }

            bdUsuario vSesion = (bdUsuario)Session["usuario"];
            return vSesion.IdEmpresa;
        }

        public int IdEstatusSesion()
        {
            int idEmpresa = IdEmpresaSesion();

            if (idEmpresa > 0)
            {
                var empresa = logicaEmpresa.Leer(idEmpresa);

                if (empresa != null)
                {
                    return empresa[0].IdEstatus;
                }
            }
            return 0;
        }

        private void RespaldarYRestaurarArchivos()
        {
            // ==========================================
            // RESPALDAR DOCUMENTO (Cédula Fiscal)
            // ==========================================
            if (fuDocumento.HasFile)
            {
                Session["CedulaBytes"] = fuDocumento.FileBytes;
                Session["CedulaExtension"] = System.IO.Path.GetExtension(fuDocumento.FileName);
                Session["CedulaFileName"] = fuDocumento.FileName;
            }

            // ==========================================
            // RESPALDAR LOGOTIPO (Imagen recortada)
            // ==========================================
            string base64Data = hfCroppedImage.Value;
            if (!string.IsNullOrWhiteSpace(base64Data))
            {
                Session["LogotipoBase64"] = base64Data;
            }
            else if (Session["LogotipoBase64"] != null)
            {
                base64Data = Session["LogotipoBase64"].ToString();
                hfCroppedImage.Value = base64Data;
            }

            // ==========================================
            // RESTAURAR ELEMENTOS VISUALMENTE TRAS EL POSTBACK
            // ==========================================

            // Restaurar visualmente el nombre del documento si ya estaba cargado
            if (Session["CedulaFileName"] != null)
            {
                txtNombreArchivoVisual.Text = Session["CedulaFileName"].ToString();
                pnlCargaNueva.Visible = false;
                pnlArchivoExistente.Visible = true;
            }

            // Restaurar visualmente la imagen del logotipo si ya estaba cargada
            if (!string.IsNullOrWhiteSpace(base64Data))
            {
                if (!base64Data.StartsWith("data:image", StringComparison.OrdinalIgnoreCase))
                {
                    previewImage.Src = "data:image/jpeg;base64," + base64Data;
                }
                else
                {
                    previewImage.Src = base64Data;
                }

                ToggleFotoUI(true, null);
            }
        }
        #endregion

        #region Retornar rutas

        public string RetornarRutaLogotipo()
        {
            List<bdRuta> ruta = logicaRuta.LeerRuta(4);
            return ruta.Count > 0 ? ruta[0].Ruta : string.Empty;
        }

        public string RetornarRutaDocumento()
        {
            List<bdRuta> ruta = logicaRuta.LeerRuta(5);
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
            master.CargarDropDownList(ddlIdTamanioEmpresa, 30, null);
            master.CargarDropDownList(ddlIdTipoEmpresa, 31, null);
        }

        protected void LeerEmpresa()
        {
            List<bdEmpresa> lista = logicaEmpresa.Leer(IdEmpresaSesion());
            
            txtNombre.Text = lista[0].Nombre.ToString();
            txtGiro.Text = lista[0].Giro.ToString();
            ddlIdTamanioEmpresa.SelectedValue = lista[0].IdTamanioEmpresa.ToString();
            ddlIdTipoEmpresa.SelectedValue = lista[0].IdTipoEmpresa.ToString();
            txtDireccion.Text = lista[0].Direccion.ToString();
            txtCorreo.Text = lista[0].Correo.ToString();
            txtPaginaWeb.Text = lista[0].PaginaWeb.ToString();
            txtNombreContacto.Text = lista[0].ContactoNombre.ToString();
            txtPuesto.Text = lista[0].ContactoPuesto.ToString();
            txtMision.Text = lista[0].Mision.ToString();
            txtVision.Text = lista[0].Vision.ToString();
            txtRegimenGastosMedicos.Text = lista[0].RegimenGastosMedicos.ToString();
            chkAvisoPrivacidad.Checked = lista[0].HasAvisoPrivacidad;
            if (chkAvisoPrivacidad.Checked)
            {
                chkAvisoPrivacidad.Disabled = true;
            }
            CargarLogo();
            GridViewTelefono();
            GridViewDocumento();

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
            List<bdEmpresa> empresa = logicaEmpresa.Leer(IdEmpresaSesion());

            // Mostrar Retroalimentacion del perfil
            if (!string.IsNullOrWhiteSpace(empresa[0].RetroPerfil.ToString()))
            {
                divMostrarRetroalimentacion.Visible = true;
                lblRetroalimentacion.Text = empresa[0].RetroPerfil.ToString();
            }
            else
            {
                divMostrarRetroalimentacion.Visible = false;
            }

            // Empresa recién creada
            if (IdEstatusSesion() == 1)
            {
                divAlertaCompletaDatos.Visible = true;
            }
            // Empresa con documentos incompletos (devueltos)
            else if (IdEstatusSesion() == 3)
            {
                List<bdEmpresaArchivo> lista = logicaEmpresaArchivo.Leer(IdEmpresaSesion());

                divDocumento.Visible = false;
                divEditarInfo.Visible = true;

                foreach (var item in lista)
                {
                    if (item.IdEstatus == 5 && item.IdTipoArchivo == 4)
                    {
                        divDocumento.Visible = true;
                        break; // Sale del ciclo en cuanto encuentra la primera coincidencia
                    }
                }

            }

            // Empresa con documentos aprobados
            else if (IdEstatusSesion() == 4)
            {
                divDocumento.Visible = false;
            }

            // Empresa con documentos pendientes de revisión
            else if (IdEstatusSesion() == 6)
            {
                // === DATOS DE LA EMPRESA ===
                btnBorrar.Visible = !estaEnModoLectura;
                txtNombre.ReadOnly = estaEnModoLectura;
                txtGiro.ReadOnly = estaEnModoLectura;
                ddlIdTamanioEmpresa.Enabled = !estaEnModoLectura;
                ddlIdTipoEmpresa.Enabled = !estaEnModoLectura;
                txtDireccion.ReadOnly = estaEnModoLectura;
                txtCorreo.ReadOnly = estaEnModoLectura;
                txtPaginaWeb.ReadOnly = estaEnModoLectura;
                lblTelefono.Visible = false;
                txtTelefono.Visible = false;
                lblExtension.Visible = false;
                txtExtension.Visible = false;
                lbtnAgregarTelefono.Visible = false;
                txtNombreContacto.ReadOnly = estaEnModoLectura;
                txtPuesto.ReadOnly = estaEnModoLectura;
                txtMision.ReadOnly = estaEnModoLectura;
                txtVision.ReadOnly = estaEnModoLectura;
                txtRegimenGastosMedicos.ReadOnly = estaEnModoLectura;
                gvConsultaTelefono.Columns[0].Visible = !estaEnModoLectura;
                gvConsultaDocumento.Columns[0].Visible = !estaEnModoLectura;
                divDocumento.Visible = false;
                btnGrabar.Visible = false;

                divAlertaRevision.Visible = true;
            }
        }

        private void ConfigurarAccesoYDatos()
        {
            // Validación de sesión y permisos
            int estatus = IdEstatusSesion();

            // Simplificado: Si es 6 o 4, activa modo lectura.
            if (estatus == 6 || estatus == 4 || estatus == 3)
            {
                if (estatus == 6) _modoLectura = true;
                EstablecerModoLectura(true);
            }
            else if (estatus == 1)
            {
                List<bdEmpresa> lista = logicaEmpresa.Leer(IdEmpresaSesion());
                txtCorreo.Text = lista[0].Correo.ToString();
            }

            // Cargar datos iniciales
            CargarDropDownLists();
            LeerEmpresa();
            Session.Remove("sesionFiltro");
            vpId = 0;
        }

        protected void CargarLogo()
        {
            var lista = logicaEmpresa.Leer(IdEmpresaSesion());

            if (lista == null || lista.Count == 0)
            {
                ToggleFotoUI(false, null);
                return;
            }

            if (lista[0].IdEstatusImg == 4)
            {
                btnBorrar.Visible = false;
            }

            string nombreArchivo = lista[0].Logotipo;
            int vpId = lista[0].IdArchivoEmpresa;
            lblEstatusImg.Text = lista[0].Estatus.ToString();
            hfVpId.Value = vpId.ToString();

            if (string.IsNullOrWhiteSpace(nombreArchivo))
            {
                ToggleFotoUI(false, null);
                return;
            }

            string rutaFisica = RetornarRutaLogotipo() + nombreArchivo;
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
        }

        #endregion

        #region Telefono
        protected List<DataRow> GridViewTelefono()
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaTelefonoEmpresa.Consultar(IdEmpresaSesion());

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

        protected void gvConsultaTelefono_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    //e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;

                    GridViewRow headerRow = gvConsultaTelefono.HeaderRow;
                    if (headerRow != null)
                    {
                        //headerRow.Cells[0].Visible = false;
                        headerRow.Cells[1].Visible = false;
                    }
                }
            }
        }
        #endregion

        #region Documento
        protected List<DataRow> GridViewDocumento()
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaEmpresaArchivo.Consultar(IdEmpresaSesion());

            if (lista.Count > 0)
            {
                master.CargarGridView(lista, gvConsultaDocumento);
            }
            else
            {
                gvConsultaDocumento.Visible = false;
            }

            return lista;
        }

        protected void gvConsultaDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;

                    GridViewRow headerRow = gvConsultaDocumento.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[0].Visible = false;
                        headerRow.Cells[1].Visible = false;
                    }
                }
            }
        }
        #endregion

        private bool _modoLectura = false; // Variable en la clase

        // Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfigurarAccesoYDatos();
                ManejarMensajesGlobales();
                //CargarDropDownLists();
                //LeerEmpresa();
            }
        }

        protected void lbtnAgregarTelefono_Click(object sender, EventArgs e)
        {
            // Validación de campos
            var diccionario = new Dictionary<object, string>
            {
                { "vTelefono", txtTelefono.Text.Trim() }
            };

            List<string> listaValidacion = validacionTelefonoEmpresa.Validar(diccionario);

            RespaldarYRestaurarArchivos();

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
                txtTelefono.Text = string.Empty;
                txtExtension.Text = string.Empty;
                return;
            }

            string telefono = (txtTelefono.Text ?? string.Empty).Trim();
            string extension = (txtExtension.Text ?? string.Empty).Trim();

            // Nota: Para guardar puedes mantener el texto original si lo deseas.
            string telefonoCmp = telefono.ToLowerInvariant();
            string extensionCmp = extension.ToLowerInvariant();

            // Obtener lista y prevenir null
            int idEmpresa = Convert.ToInt32(IdEmpresaSesion());
            List<bdTelefonoEmpresa> lista = logicaTelefonoEmpresa.Leer(idEmpresa) ?? new List<bdTelefonoEmpresa>();

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
            var parametro = new bdTelefonoEmpresa
            {
                IdEmpresa = idEmpresa,
                Telefono = telefonoCmp.Trim(),
                Extension = string.IsNullOrWhiteSpace(extensionCmp) ? null : extensionCmp.Trim()
            };

            var resultado = logicaTelefonoEmpresa.Crear(parametro);
            MostrarMensaje(resultado.Item1, resultado.Item2, null);

            // Recargar gvCorreo
            GridViewTelefono();

            // Limpiar controles Correo
            txtTelefono.Text = string.Empty;
            txtExtension.Text = string.Empty;
        }

        protected void gvConsultaTelefono_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int pId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Eliminar")
            {
                var control = logicaTelefonoEmpresa.Eliminar(new bdTelefonoEmpresa { IdTelefonoEmpresa = pId });
                MostrarMensaje(control.Item1, control.Item2, null);
                RespaldarYRestaurarArchivos();
                GridViewTelefono();
            }
        }


        protected void gvConsultaDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            // Consultar estado actual en BD ANTES de validar para saber qué exigir
            int idEmpresaActual = IdEmpresaSesion();
            List<bdEmpresaArchivo> listaArchivosActuales = logicaEmpresaArchivo.Leer(idEmpresaActual);

            // Determinamos si los archivos ya existen y están aprobados (Estatus 4)
            bool existeLogoAprobado = listaArchivosActuales.Any(x => x.IdTipoArchivo == 3 && x.IdEstatus == 4);
            bool existeCedulaAprobada = listaArchivosActuales.Any(x => x.IdTipoArchivo == 4 && x.IdEstatus == 4);

            // Validaciones de texto normales
            var diccionario = new Dictionary<object, string>
            {
                { "vNombre", txtNombre.Text },
                { "vGiro", txtGiro.Text },
                { "vIdTipoEmpresa", ddlIdTipoEmpresa.SelectedValue },
                { "vIdTamanioEmpresa", ddlIdTamanioEmpresa.SelectedValue },
                { "vDireccion", txtDireccion.Text },
                { "vPaginaWeb", txtPaginaWeb.Text },
                { "vCorreo", txtCorreo.Text },
                { "vContactoNombre", txtNombreContacto.Text },
                { "vContactoPuesto", txtPuesto.Text },
                { "vMision", txtMision.Text },
                { "vVision", txtVision.Text }
            };

            List<string> listaValidacion = validacionEmpresa.Validar(diccionario);
            if (listaValidacion == null) listaValidacion = new List<string>();

            int maxFileSize = 10485760; // 10 MB

            // Gestión de la Cédula Fiscal en Session (Prevenir pérdida por Postback)
            byte[] archivoBytes = null;
            string fileExtension = string.Empty;

            if (fuDocumento.HasFile)
            {
                archivoBytes = fuDocumento.FileBytes;
                fileExtension = System.IO.Path.GetExtension(fuDocumento.FileName);

                Session["CedulaBytes"] = archivoBytes;
                Session["CedulaExtension"] = fileExtension;
                Session["CedulaFileName"] = fuDocumento.FileName;
            }
            else if (Session["CedulaBytes"] != null)
            {
                archivoBytes = (byte[])Session["CedulaBytes"];
                fileExtension = Session["CedulaExtension"].ToString();
            }

            

            // Validaciones de Negocio Extra (Teléfono y Privacidad)
            var listaTelefonosBD = logicaTelefonoEmpresa.Leer(idEmpresaActual);
            if (listaTelefonosBD == null || listaTelefonosBD.Count == 0)
            {
                listaValidacion.Add("Alerta: Se espera ingresar al menos un Teléfono");
            }

           
            // Validación Cédula Fiscal
            if (!existeCedulaAprobada)
            {
                // Si NO está aprobada, es obligatorio que haya un archivo subido o en memoria
                if (archivoBytes == null || archivoBytes.Length == 0)
                {
                    listaValidacion.Add("Alerta: Es necesario Anexar Cedula Fiscal");
                }
                else
                {
                    if (!fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                        listaValidacion.Add("Alerta: Por favor, carga un archivo formato PDF");

                    if (archivoBytes.Length > maxFileSize)
                        listaValidacion.Add("Alerta: El archivo es demasiado grande. El tamaño máximo permitido es 10 MB.");
                }
            }

            // Gestión del Logotipo en Session
            string base64Data = hfCroppedImage.Value;
            if (!string.IsNullOrWhiteSpace(base64Data))
            {
                Session["LogotipoBase64"] = base64Data;
            }
            else if (Session["LogotipoBase64"] != null)
            {
                base64Data = Session["LogotipoBase64"].ToString();
                hfCroppedImage.Value = base64Data;
            }

            // Validación Logotipo
            if (!existeLogoAprobado)
            {
                if (string.IsNullOrWhiteSpace(base64Data))
                {
                    listaValidacion.Add("Alerta: Es necesario cargar un Logotipo de Empresa");
                }
            }

            if (!chkAvisoPrivacidad.Checked)
            {
                listaValidacion.Add("Alerta: Para continuar, debes aceptar el Aviso de Privacidad");
            }


            // Si hay errores, detener ejecución y restaurar la interfaz visual
            if (listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);

                // Restaurar estado visual del archivo PDF
                if (Session["CedulaFileName"] != null)
                {
                    txtNombreArchivoVisual.Text = Session["CedulaFileName"].ToString();
                    pnlCargaNueva.Visible = false;
                    pnlArchivoExistente.Visible = true;
                }

                // Restaurar estado visual del Logotipo
                if (!string.IsNullOrWhiteSpace(base64Data))
                {
                    hfCroppedImage.Value = base64Data;
                    if (!base64Data.StartsWith("data:image", StringComparison.OrdinalIgnoreCase))
                        previewImage.Src = "data:image/jpeg;base64," + base64Data;
                    else
                        previewImage.Src = base64Data;

                    ToggleFotoUI(true, null);
                }
                else
                {
                    CargarLogo();
                }

                return; // Salir del método
            }

            // ==========================================
            // GUARDAR INFORMACIÓN EN LA BASE DE DATOS
            // ==========================================
            var parametro = new bdEmpresa
            {
                IdEmpresa = idEmpresaActual,
                Nombre = txtNombre.Text.ToUpper(),
                Giro = txtGiro.Text.Trim().ToUpper(),
                IdTamanioEmpresa = int.Parse(ddlIdTamanioEmpresa.SelectedValue),
                IdTipoEmpresa = int.Parse(ddlIdTipoEmpresa.SelectedValue),
                Direccion = txtDireccion.Text.ToUpper(),
                Correo = txtCorreo.Text,
                PaginaWeb = txtPaginaWeb.Text,
                ContactoNombre = txtNombreContacto.Text.ToUpper(),
                ContactoPuesto = txtPuesto.Text.ToUpper(),
                Mision = txtMision.Text.ToUpper(),
                Vision = txtVision.Text.ToUpper(),
                RegimenGastosMedicos = string.IsNullOrWhiteSpace(txtRegimenGastosMedicos.Text) ? null : txtRegimenGastosMedicos.Text.ToUpper(),
                HasAvisoPrivacidad = chkAvisoPrivacidad.Checked,
                UsuarioModificacion = IdUsuarioSesion()
            };

            Tuple<string, string> control = logicaEmpresa.Editar(parametro);
            string NombreEmpresa = txtNombre.Text.ToUpper();

            // Guardar Logotipo (Solo si el usuario recortó/cargó uno en este proceso)
            if (!string.IsNullOrEmpty(base64Data))
            {
                string base64LimpioLogo = base64Data.Contains(",") ? base64Data.Substring(base64Data.IndexOf(",") + 1) : base64Data;
                byte[] bytesImagen = Convert.FromBase64String(base64LimpioLogo);

                string destinoCarpetaLogo = RetornarRutaLogotipo();
                if (!Directory.Exists(destinoCarpetaLogo))
                    Directory.CreateDirectory(destinoCarpetaLogo);

                string nombreLogotipo = $"Logotipo_{NombreEmpresa}_{idEmpresaActual}.jpg";
                string rutaCompletaLogo = Path.Combine(destinoCarpetaLogo, nombreLogotipo);
                File.WriteAllBytes(rutaCompletaLogo, bytesImagen);

                var parametroImagen = new bdEmpresaArchivo
                {
                    IdTipoArchivo = 3,
                    IdEmpresa = idEmpresaActual,
                    NombreDocumento = nombreLogotipo
                };
                logicaEmpresaArchivo.Crear(parametroImagen);
            }

            // Guardar Documento Cédula Fiscal (Solo si hay bytes cargados en memoria)
            if (archivoBytes != null && archivoBytes.Length > 0)
            {
                string destinoCarpetaDoc = RetornarRutaDocumento();
                if (!Directory.Exists(destinoCarpetaDoc))
                    Directory.CreateDirectory(destinoCarpetaDoc);

                string nombreDocumento = $"CedulaFiscal_{NombreEmpresa}_{idEmpresaActual}{fileExtension}";
                string rutaCompletaDoc = Path.Combine(destinoCarpetaDoc, nombreDocumento);
                File.WriteAllBytes(rutaCompletaDoc, archivoBytes);

                var parametroDocumento = new bdEmpresaArchivo
                {
                    IdTipoArchivo = 4,
                    IdEmpresa = idEmpresaActual,
                    NombreDocumento = nombreDocumento
                };
                logicaEmpresaArchivo.Crear(parametroDocumento);
            }

            // Limpieza general post-guardado
            Session.Remove("LogotipoBase64");
            Session.Remove("CedulaBytes");
            Session.Remove("CedulaExtension");
            Session.Remove("CedulaFileName");

            pnlCargaNueva.Visible = true;
            pnlArchivoExistente.Visible = false;
            hfCroppedImage.Value = "";
            divDocumento.Visible = false;

            // Mostrar mensaje final (Éxito)
            Session["MensajePendiente"] = control.Item1;
            Session["TipoMensaje"] = control.Item2;

            // Recargar página para aplicar cambios
            Response.Redirect(Request.RawUrl, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        //protected void btnGrabar_Click(object sender, EventArgs e)
        //{
        //    // 1. Validaciones de texto normales
        //    var diccionario = new Dictionary<object, string>
        //    {
        //        { "vNombre", txtNombre.Text },
        //        { "vGiro", txtGiro.Text },
        //        { "vIdTipoEmpresa", ddlIdTipoEmpresa.SelectedValue },
        //        { "vIdTamanioEmpresa", ddlIdTamanioEmpresa.SelectedValue },
        //        { "vDireccion", txtDireccion.Text },
        //        { "vCorreo", txtCorreo.Text },
        //        { "vContactoNombre", txtNombreContacto.Text },
        //        { "vContactoPuesto", txtPuesto.Text },
        //        { "vMision", txtMision.Text },
        //        { "vVision", txtVision.Text }
        //    };

        //    List<string> listaValidacion = validacionEmpresa.Validar(diccionario);
        //    if (listaValidacion == null) listaValidacion = new List<string>();

        //    int maxFileSize = 10485760; // 10 MB

        //    // 2. Gestión de la Cédula Fiscal en Session
        //    byte[] archivoBytes = null;
        //    string fileExtension = string.Empty;

        //    if (fuDocumento.HasFile)
        //    {
        //        archivoBytes = fuDocumento.FileBytes;
        //        fileExtension = System.IO.Path.GetExtension(fuDocumento.FileName);

        //        Session["CedulaBytes"] = archivoBytes;
        //        Session["CedulaExtension"] = fileExtension;
        //        Session["CedulaFileName"] = fuDocumento.FileName;
        //    }
        //    else if (Session["CedulaBytes"] != null)
        //    {
        //        archivoBytes = (byte[])Session["CedulaBytes"];
        //        fileExtension = Session["CedulaExtension"].ToString();
        //    }

        //    // 3. Validaciones de negocio adicionales
        //    // Consultamos directamente a la capa lógica para saber si realmente hay teléfonos registrados en la BD
        //    int idEmpresaActual = IdEmpresaSesion();
        //    var listaTelefonosBD = logicaTelefonoEmpresa.Leer(idEmpresaActual);

        //    if (listaTelefonosBD == null || listaTelefonosBD.Count == 0)
        //    {
        //        listaValidacion.Add("Alerta: Se espera ingresar al menos un Teléfono");
        //    }

        //    if (archivoBytes == null || archivoBytes.Length == 0)
        //    {
        //        if (divDocumento.Visible)
        //        {
        //            listaValidacion.Add("Alerta: Es necesario Anexar Cedula Fiscal");
        //        }
        //    }
        //    else
        //    {
        //        if (!fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
        //            listaValidacion.Add("Alerta: Por favor, carga un archivo formato PDF");

        //        if (archivoBytes.Length > maxFileSize)
        //            listaValidacion.Add("Alerta: El archivo es demasiado grande. El tamaño máximo permitido es 10 MB.");
        //    }

        //    // Logotipo
        //    string base64Data = hfCroppedImage.Value;
        //    if (!string.IsNullOrWhiteSpace(base64Data))
        //    {
        //        Session["LogotipoBase64"] = base64Data;
        //    }
        //    else if (Session["LogotipoBase64"] != null)
        //    {
        //        base64Data = Session["LogotipoBase64"].ToString();
        //        hfCroppedImage.Value = base64Data;
        //    }

        //    if (string.IsNullOrWhiteSpace(base64Data))
        //    {
        //        if (btnBorrar.Visible)
        //        {
        //            listaValidacion.Add("Alerta: Es necesario cargar un Logotipo de Empresa");
        //        }
        //    }

        //    if (!chkAvisoPrivacidad.Checked)
        //        listaValidacion.Add("Alerta: Para continuar, debes aceptar el Aviso de Privacidad");

        //    // 4. Si hay errores, detenemos la ejecución y transformamos el control visualmente
        //    if (listaValidacion.Count > 0)
        //    {
        //        MostrarMensaje(listaValidacion[0], "warning", null);

        //        // Ocultamos el FileUpload vacío y mostramos el TextBox con el nombre del archivo guardado (Cédula)
        //        if (Session["CedulaFileName"] != null)
        //        {
        //            txtNombreArchivoVisual.Text = Session["CedulaFileName"].ToString();
        //            pnlCargaNueva.Visible = false;
        //            pnlArchivoExistente.Visible = true;
        //        }

        //        // --- RESTAURAR EL LOGOTIPO VISUALMENTE TRAS EL ERROR ---
        //        if (!string.IsNullOrWhiteSpace(base64Data))
        //        {
        //            hfCroppedImage.Value = base64Data;

        //            // Asegurarnos de que tenga el formato de Data URI correcto para la etiqueta <img>
        //            if (!base64Data.StartsWith("data:image", StringComparison.OrdinalIgnoreCase))
        //            {
        //                previewImage.Src = "data:image/jpeg;base64," + base64Data;
        //            }
        //            else
        //            {
        //                previewImage.Src = base64Data;
        //            }

        //            ToggleFotoUI(true, null);
        //        }
        //        else
        //        {
        //            CargarLogo();
        //        }

        //        return;
        //    }

        //    // 5. Guardar Empresa
        //    var parametro = new bdEmpresa
        //    {
        //        IdEmpresa = IdEmpresaSesion(),
        //        Nombre = txtNombre.Text.ToUpper(),
        //        Giro = txtGiro.Text.Trim().ToUpper(),
        //        IdTamanioEmpresa = int.Parse(ddlIdTamanioEmpresa.SelectedValue),
        //        IdTipoEmpresa = int.Parse(ddlIdTipoEmpresa.SelectedValue),
        //        Direccion = txtDireccion.Text.ToUpper(),
        //        Correo = txtCorreo.Text,
        //        PaginaWeb = txtPaginaWeb.Text,
        //        ContactoNombre = txtNombreContacto.Text.ToUpper(),
        //        ContactoPuesto = txtPuesto.Text.ToUpper(),
        //        Mision = txtMision.Text.ToUpper(),
        //        Vision = txtVision.Text.ToUpper(),
        //        RegimenGastosMedicos = string.IsNullOrWhiteSpace(txtRegimenGastosMedicos.Text) ? null : txtRegimenGastosMedicos.Text.ToUpper(),
        //        HasAvisoPrivacidad = chkAvisoPrivacidad.Checked,
        //        UsuarioModificacion = IdUsuarioSesion()
        //    };

        //    Tuple<string, string> control = logicaEmpresa.Editar(parametro);

        //    string NombreEmpresa = txtNombre.Text.ToUpper();
        //    int idEmpresa = IdEmpresaSesion();


        //    List<bdEmpresaArchivo> lista = logicaEmpresaArchivo.Leer(IdEmpresaSesion());

        //    divDocumento.Visible = false;

        //    // Validar si ya existen de forma activa
        //    bool existeLogo = lista.Any(x => x.IdTipoArchivo == 3 && x.IdEstatus == 4);
        //    bool existeCedula = lista.Any(x => x.IdTipoArchivo == 4 && x.IdEstatus == 4);

        //    // 6. Guardar Logotipo solo si no existe y hay datos
        //    if (!existeLogo && !string.IsNullOrEmpty(base64Data))
        //    {
        //        string base64LimpioLogo = base64Data.Contains(",") ? base64Data.Substring(base64Data.IndexOf(",") + 1) : base64Data;
        //        byte[] bytesImagen = Convert.FromBase64String(base64LimpioLogo);

        //        string destinoCarpetaLogo = RetornarRutaLogotipo();
        //        if (!Directory.Exists(destinoCarpetaLogo))
        //            Directory.CreateDirectory(destinoCarpetaLogo);

        //        string nombreLogotipo = $"Logotipo_{NombreEmpresa}_{idEmpresa}.jpg";
        //        string rutaCompletaLogo = Path.Combine(destinoCarpetaLogo, nombreLogotipo);
        //        File.WriteAllBytes(rutaCompletaLogo, bytesImagen);

        //        var parametroImagen = new bdEmpresaArchivo
        //        {
        //            IdTipoArchivo = 3,
        //            IdEmpresa = idEmpresa,
        //            NombreDocumento = nombreLogotipo
        //        };
        //        logicaEmpresaArchivo.Crear(parametroImagen);
        //    }

        //    // 7. Guardar o actualizar Documento (Cédula Fiscal)
        //    bool seSubioArchivoNuevo = fuDocumento.HasFile;

        //    if ((!existeCedula || seSubioArchivoNuevo) && archivoBytes != null && archivoBytes.Length > 0)
        //    {
        //        string destinoCarpetaDoc = RetornarRutaDocumento();
        //        if (!Directory.Exists(destinoCarpetaDoc))
        //            Directory.CreateDirectory(destinoCarpetaDoc);

        //        string nombreDocumento = $"CedulaFiscal_{NombreEmpresa}_{idEmpresa}{fileExtension}";
        //        string rutaCompletaDoc = Path.Combine(destinoCarpetaDoc, nombreDocumento);
        //        File.WriteAllBytes(rutaCompletaDoc, archivoBytes);

        //        var parametroDocumento = new bdEmpresaArchivo
        //        {
        //            IdTipoArchivo = 4,
        //            IdEmpresa = idEmpresa,
        //            NombreDocumento = nombreDocumento
        //        };

        //        // Nota: Si tu capa lógica requiere actualizar en lugar de crear cuando ya existe, 
        //        // asegúrate de llamar al método de actualización correspondiente.
        //        logicaEmpresaArchivo.Crear(parametroDocumento);
        //    }

        //    // 8. Limpieza general al terminar con éxito (FUERA del ciclo)
        //    Session.Remove("LogotipoBase64");
        //    Session.Remove("CedulaBytes");
        //    Session.Remove("CedulaExtension");
        //    Session.Remove("CedulaFileName");

        //    pnlCargaNueva.Visible = true;
        //    pnlArchivoExistente.Visible = false;
        //    hfCroppedImage.Value = "";

        //    // Mostrar mensaje de resultado
        //    Session["MensajePendiente"] = control.Item1;
        //    Session["TipoMensaje"] = control.Item2;

        //    // Recargar la página para reflejar los cambios
        //    Response.Redirect(Request.RawUrl, false);
        //    Context.ApplicationInstance.CompleteRequest();
        //}

        protected void btnRemoverArchivo_Click(object sender, EventArgs e)
        {
            // Limpiamos la memoria
            Session.Remove("CedulaBytes");
            Session.Remove("CedulaExtension");
            Session.Remove("CedulaFileName");

            // Regresamos el control a su estado original (FileUpload vacío)
            txtNombreArchivoVisual.Text = "";
            pnlArchivoExistente.Visible = false;
            pnlCargaNueva.Visible = true;
        }
        protected void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Limpiamos siempre la memoria temporal (Session y HiddenField del cropper)
                Session.Remove("LogotipoBase64");
                hfCroppedImage.Value = string.Empty;

                // 2. Verificamos si ya estaba registrado en la Base de Datos (tiene un ID válido)
                int vpIdFromHidden = 0;
                if (int.TryParse(hfVpId.Value, out vpIdFromHidden) && vpIdFromHidden > 0)
                {
                    var parametro = new bdEmpresaArchivo
                    {
                        IdArchivoEmpresa = vpIdFromHidden
                    };

                    logicaEmpresaArchivo.Eliminar(parametro);
                    hfVpId.Value = string.Empty; // Limpiamos el ID de la BD
                }

                // 3. Forzamos el reseteo visual inmediato en la interfaz
                previewImage.Src = string.Empty;
                ToggleFotoUI(false, null);

                MostrarMensaje("Logotipo eliminado correctamente.", "success");

                // 4. Opcional: Recargamos el estado oficial por si quedó algo en la BD
                CargarLogo();
            }
            catch (Exception ex)
            {
                MostrarMensaje("No se pudo eliminar el Logotipo: " + ex.Message, "error");
            }
        }


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

            Session["MensajePendiente"] = null;
            Session["TipoMensaje"] = null;
        }
    }
}
