<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="PerfilEmpresa.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.PerfilEmpresa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/cropperjs/1.5.13/cropper.min.css" />
    <script src="https://unpkg.com/cropperjs@1.6.2/dist/cropper.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid p-3">
        <div class="row justify-content-center">
            <div class="col-lg-10">
                <!-- Area de consulta información -->
                <div class="row p-2">
                    <div class="card h-90vh">
                        <div class="card-body contenido">
                            <!-- ===== Contenido de secciones ===== -->
                            <div class="tab-content" id="perfilTabsContent">
                                <!-- Contenido interno de los sub-tabs -->
                                <!-- Personal -->
                                <div class="tab-pane fade show active" id="pane-personal" role="tabpanel" aria-labelledby="tab-personal">
                                    <div class="row card-info justify-content-center">
                                        <!-- ===================== COLUMNA IZQUIERDA (FOTOGRAFÍA) ===================== -->
                                        <div class="col-md-3 pe-5" style="border-right: 1px solid #e0e0e0;">
                                            <h4 class="card-header d-flex align-items-center fw-bold py-3 mb-3">LOGOTIPO</h4>
                                            <div class="row g-1 card-image">
                                                <asp:Label ID="lblEstatusImg" runat="server" Visible="false" CssClass="label mb-0" Text=""></asp:Label>
                                                <div class="border-image p-3 text-center">
                                                    <div class="file-select" id="srcFile" runat="server" clientidmode="Static">
                                                        <input type="file" id="uploadImage" accept="image/*" onchange="uploader_handleFileChange(event)" />
                                                        <div class="file-select-content">
                                                            <span class="text-main">CARGAR ARCHIVO</span>
                                                        </div>
                                                    </div>
                                                    <div class="d-none w-100 mt-3" id="previewContainer" runat="server" clientidmode="Static">
                                                        <img id="previewImage" alt="Vista previa" class="img-fluid rounded" runat="server" clientidmode="Static" />
                                                    </div>
                                                    <small id="txtInstruccion" runat="server"></small>
                                                    <!-- Hidden fields -->
                                                    <asp:HiddenField ID="hfCroppedImage" runat="server" />
                                                    <asp:HiddenField ID="hfVpId" runat="server" />
                                                    <asp:LinkButton ID="btnBorrar" runat="server" CssClass="btn btnRed mt-3" Style="background-color: #f5f5f5; color: #333; display: none;" OnClick="btnBorrar_Click" OnClientClick="return confirmDeleteImage(this);"><i class="fa-solid fa-xmark"></i> Borrar
                                                    </asp:LinkButton>
                                                    <div id="divAlertaCompletaDatos" class="alert alert-info mt-5" runat="server" visible="false">
                                                        <strong>Completa tu perfil</strong>
                                                        <p class="mb-0">Ingresa los datos y documentos para finalizar el registro.</p>
                                                    </div>
                                                    <div id="divAlertaRevision" class="alert alert-info mt-5" runat="server" visible="false">
                                                        <strong>IMPORTANTE</strong>
                                                        <p class="mb-0">Su información está siendo revisada por el departamento de Bolsa de Trabajo.</p>
                                                    </div>
                                                    <div id="divEditarInfo" class="alert alert-warning mt-5" runat="server" visible="false">
                                                        <strong>¡Atención!</strong>
                                                        <p class="mb-0">Para continuar con la revisión de su trámite, es necesario que actualice o vuelva a cargar su información.</p>
                                                    </div>
                                                    <div id="divMostrarRetroalimentacion" class="alert alert-info mt-5" runat="server" visible="false">
                                                        <strong>Retroalimentación</strong>
                                                        <p class="mb-2">Comentarios registrados:</p>
                                                        <!-- Contenedor con fondo blanco para destacar el texto -->
                                                        <div class="p-3 bg-white border rounded text-dark">
                                                            <asp:Label ID="lblRetroalimentacion" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal fade" id="imageModal">
                                            <div class="modal-dialog modal-dialog-centered modal-lg">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        Recortar Imagen
                                                    </div>
                                                    <div class="modal-body text-center">
                                                        <img id="cropperImage" src="" alt="Imagen para Recortar" class="img-fluid" style="max-width: 100%;" />
                                                    </div>
                                                    <div class="modal-footer d-flex justify-content-center align-items-center">
                                                        <!-- Controles de edición -->
                                                        <div class="d-flex gap-2">
                                                            <button type="button" class="btn btn-outline-secondary" id="btnZoomIn" title="Acercar">
                                                                <i class="fa-solid fa-magnifying-glass-plus"></i>
                                                            </button>
                                                            <button type="button" class="btn btn-outline-secondary" id="btnZoomOut" title="Alejar">
                                                                <i class="fa-solid fa-magnifying-glass-minus"></i>
                                                            </button>
                                                            <button type="button" class="btn btn-outline-secondary" id="btnRotate" title="Rotar 90º">
                                                                <i class="fa-solid fa-rotate-right"></i>
                                                            </button>
                                                            <button type="button" class="btn btn-outline-secondary" id="btnReset" title="Reiniciar">
                                                                <i class="fa-solid fa-arrows-rotate"></i>
                                                            </button>
                                                        </div>
                                                        <!-- Botones principales -->
                                                        <div class="d-flex gap-2">
                                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                                                                Cerrar
                                                            </button>
                                                            <button type="button" class="btn btn-primary" id="cropButton">
                                                                Aplicar Recorte
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- ===================== COLUMNA DERECHA (INFORMACIÓN DE LA EMPRESA) ===================== -->
                                        <div class="col-lg-8 ps-5">
                                            <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 mb-3">INFORMACIÓN DE LA EMPRESA</h4>
                                            <div class="row g-3">
                                                <div class="col-md-7 form-floating mb-3">
                                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control textbox" MaxLength="155" placeholder="Nombre de la Empresa*"></asp:TextBox>
                                                    <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" CssClass="label" Text="Nombre de la Empresa*"></asp:Label>
                                                </div>
                                                <div class="col-md-5 form-floating mb-3">
                                                    <asp:TextBox ID="txtGiro" runat="server" CssClass="form-control textbox" MaxLength="155" placeholder="Giro*"></asp:TextBox>
                                                    <asp:Label ID="lblGiro" runat="server" AssociatedControlID="txtGiro" CssClass="label" Text="Giro*"></asp:Label>
                                                </div>
                                                <div class="col-md-6 form-floating mb-3">
                                                    <asp:DropDownList ID="ddlIdTamanioEmpresa" runat="server" CssClass="form-control textbox"></asp:DropDownList>
                                                    <asp:Label ID="lblIdTamanioEmpresa" runat="server" AssociatedControlID="ddlIdTamanioEmpresa" CssClass="label" Text="Tamaño de la Empresa*"></asp:Label>
                                                </div>
                                                <div class="col-md-6 form-floating mb-3">
                                                    <asp:DropDownList ID="ddlIdTipoEmpresa" runat="server" CssClass="form-control textbox"></asp:DropDownList>
                                                    <asp:Label ID="lblIdTipoEmpresa" runat="server" AssociatedControlID="ddlIdTipoEmpresa" CssClass="label" Text="Tipo de Empresa*"></asp:Label>
                                                </div>
                                                <div class="col-md-12 form-floating mb-3">
                                                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control textbox" MaxLength="255" placeholder="Dirección*"></asp:TextBox>
                                                    <asp:Label ID="lblDireccion" runat="server" AssociatedControlID="txtDireccion" CssClass="label" Text="Dirección*"></asp:Label>
                                                </div>
                                                <div class="col-md-6 mb-0">
                                                    <div class="form-floating mb-2">
                                                        <asp:TextBox ID="txtCorreo" runat="server" Enabled="false" CssClass="form-control textbox" MaxLength="155" placeholder="Correo*"></asp:TextBox>
                                                        <asp:Label ID="lblCorreo" runat="server" AssociatedControlID="txtCorreo" CssClass="label" Text="Correo*"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 form-floating mb-0">
                                                    <asp:TextBox ID="txtPaginaWeb" runat="server" CssClass="form-control textbox" MaxLength="255" placeholder="URL Página Web*"></asp:TextBox>
                                                    <asp:Label ID="lblPaginaWeb" runat="server" AssociatedControlID="txtPaginaWeb" CssClass="label" Text="URL Página Web*"></asp:Label>
                                                </div>
                                                <div class="col-md-7 form-floating mb-3">
                                                    <asp:TextBox ID="txtNombreContacto" runat="server" CssClass="form-control textbox" MaxLength="55" placeholder="Nombre del Contacto*"></asp:TextBox>
                                                    <asp:Label ID="lblNombreContacto" runat="server" AssociatedControlID="txtNombreContacto" CssClass="label" Text="Nombre del Contacto*"></asp:Label>
                                                </div>
                                                <div class="col-md-5 form-floating mb-3">
                                                    <asp:TextBox ID="txtPuesto" runat="server" CssClass="form-control textbox" MaxLength="155" placeholder="Puesto del Contacto**"></asp:TextBox>
                                                    <asp:Label ID="lblPuesto" runat="server" AssociatedControlID="txtPuesto" CssClass="label" Text="Puesto del Contacto*"></asp:Label>
                                                </div>
                                                <div class="col-md-12 form-floating mb-3">
                                                    <asp:TextBox ID="txtMision" runat="server" CssClass="form-control textbox" MaxLength="255" placeholder="Misión*"></asp:TextBox>
                                                    <asp:Label ID="lblMision" runat="server" AssociatedControlID="txtMision" CssClass="label" Text="Misión*"></asp:Label>
                                                </div>
                                                <div class="col-md-12 form-floating mb-3">
                                                    <asp:TextBox ID="txtVision" runat="server" CssClass="form-control textbox" MaxLength="255" placeholder="Visión*"></asp:TextBox>
                                                    <asp:Label ID="lblVision" runat="server" AssociatedControlID="txtVision" CssClass="label" Text="Visión*"></asp:Label>
                                                </div>
                                                <div class="col-md-12 form-floating mb-3">
                                                    <asp:TextBox ID="txtRegimenGastosMedicos" runat="server" CssClass="form-control textbox" MaxLength="155" placeholder="Regimen Gastos Médicos"></asp:TextBox>
                                                    <asp:Label ID="lblRegimenGastosMedicos" runat="server" AssociatedControlID="txtRegimenGastosMedicos" CssClass="label" Text="Regimen Gastos Médicos"></asp:Label>
                                                </div>
                                                <!-- Teléfono -->
                                                <div class="col-md-12 mb-3">
                                                    <fieldset class="border rounded-3 p-3">
                                                        <legend class="float-none w-auto px-3 fs-5 fw-bold text-dark">Teléfono</legend>
                                                        <!-- Contenedor del rectángulo -->
                                                        <div class="row">
                                                            <div class="col-md-6 mb-3 form-floating">
                                                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control textbox" MaxLength="10" onpaste="return false;" onkeypress="return AllowOnlyNumbers(event);" placeholder="Teléfono"></asp:TextBox>
                                                                <asp:Label ID="lblTelefono" runat="server" AssociatedControlID="txtTelefono" CssClass="label" Text="Teléfono*"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4 mb-3 form-floating">
                                                                <asp:TextBox ID="txtExtension" runat="server" CssClass="form-control textbox" MaxLength="5" onpaste="return false;" onkeypress="return AllowOnlyNumbers(event);" placeholder="Extensión"></asp:TextBox>
                                                                <asp:Label ID="lblExtension" runat="server" AssociatedControlID="txtExtension" CssClass="label" Text="Extensión"></asp:Label>
                                                            </div>
                                                            <div class="col-md-2 d-flex align-items-center mb-3">
                                                                <asp:LinkButton ID="lbtnAgregarTelefono" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Agregar" OnClick="lbtnAgregarTelefono_Click"><i class="fa-solid">Agregar</i></asp:LinkButton>
                                                            </div>
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="gvConsultaTelefono" runat="server" CssClass="table table-sm table-striped table-hover" OnRowDataBound="gvConsultaTelefono_RowDataBound" OnRowCommand="gvConsultaTelefono_RowCommand" AutoGenerateColumns="true" AllowPaging="true">
                                                                    <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                                    <PagerStyle CssClass="custom-pager" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnEliminarTelefono" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <!-- Fin Teléfono -->
                                                <!-- Documentos -->
                                                <div class="row mb-3">
                                                    <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 mb-3">DOCUMENTACIÓN</h4>

                                                    <div id="divDocumento" runat="server">
                                                        <h4 class="label d-flex align-items-center py-3 mb-0">ANEXAR CEDULA FISCAL*</h4>
                                                        <div class="col-md-12 mb-0">

                                                            <!-- Control real (Se usa la primera vez) -->
                                                            <asp:Panel ID="pnlCargaNueva" runat="server">
                                                                <div class="input-group">
                                                                    <asp:FileUpload ID="fuDocumento" runat="server" CssClass="form-control fileupload" accept=".pdf" />
                                                                </div>
                                                            </asp:Panel>

                                                            <!-- Control simulado (Aparece automáticamente si la validación falla, mostrando el nombre del archivo como si nunca se hubiera borrado) -->
                                                            <asp:Panel ID="pnlArchivoExistente" runat="server" Visible="false">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtNombreArchivoVisual" runat="server" CssClass="form-control" ReadOnly="true" BackColor="White"></asp:TextBox>
                                                                    <asp:Button ID="btnRemoverArchivo" runat="server" Text="Quitar" CssClass="btn btn-outline-danger" CausesValidation="false" OnClick="btnRemoverArchivo_Click" />
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                    <div class="mt-3 table-responsive">
                                                        <asp:GridView ID="gvConsultaDocumento" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="true" AllowPaging="true" OnRowDataBound="gvConsultaDocumento_RowDataBound" OnRowCommand="gvConsultaDocumento_RowCommand">
                                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                            <PagerStyle CssClass="custom-pager" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEliminarDocumento" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <!-- Fin Documentos -->

                                                <!-- Checkbox de Aviso de Privacidad -->
                                                <div class="col-md-12 mt-4">
                                                    <div class="form-check">
                                                        <input type="checkbox" class="form-check-input" id="chkAvisoPrivacidad" runat="server" />
                                                        <label class="form-check-label" for="chkAvisoPrivacidad">
                                                            He leído y acepto el
                                                                    <a href="#" data-bs-toggle="modal" data-bs-target="#modalAvisoPrivacidad" style="text-decoration: underline; color: #0d6efd;">Aviso de Privacidad
                                                                    </a>*
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Cierre de tu contenedor principal -->

                                            <!-- Estructura del Modal de Bootstrap -->
                                            <div class="modal fade" id="modalAvisoPrivacidad" tabindex="-1" aria-labelledby="modalAvisoPrivacidadLabel" aria-hidden="true">
                                                <!-- modal-dialog-scrollable permite que si el texto es muy largo, el modal tenga scroll interno -->
                                                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="modalAvisoPrivacidadLabel">Aviso de Privacidad</h5>
                                                            <!-- Botón para cerrar el modal (Sintaxis Bootstrap 5) -->
                                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <!-- Aquí colocas todo el texto legal de tu aviso de privacidad -->
                                                            <p>Autorizo a las personas responsables del Departamento de Bolsa de Trabajo de la FACPYA, al procesamiento informático de la información que les he facilitado, con el único objetivo de pertenecer a su Bolsa de Trabajo, así como proceder a la difusión necesaria e imprescindible para la atención de la demanda laboral de Estudiantes y egresados. Rechazo expresamente la autorización de su uso para cualquier otro fin deferente del indicado, excepto para su tratamiento estadístico en departamentos internos, pudiendo ejercitar los derechos de acceso, cancelación y rectificación de la información, así como de oposición.</p>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Fin Personal -->
                            <div class="card-footer mt-5">
                                <div class="footer-pane" data-pane="#pane-personal">
                                    <div class="row g-2">
                                        <div class="col-md-8"></div>
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="btnGrabar" runat="server"
                                                CssClass="btn btn-primary w-100" OnClick="btnGrabar_Click">
                                                <i class="fa-regular fa-floppy-disk"></i>&nbsp;Grabar
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        let cropper;

        function limpiarInput(btnLimpiar) {
            const input = btnLimpiar.parentElement.querySelector('input');
            if (input) {
                input.value = "";
                input.dispatchEvent(new Event('blur'));
                input.focus();
            }
        }

        function confirmDeleteImage(el) {
            var btn = el.closest ? el.closest('a') : el;

            Swal.fire({
                title: '¿Desea borrar la fotografía?',
                text: "La imagen se eliminará y deberá subir una nueva.",
                icon: 'warning',
                allowOutsideClick: false,
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#6c757d',
                confirmButtonText: 'Sí, Borrar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    const previewImage = document.getElementById('previewImage');
                    const previewContainer = document.getElementById('previewContainer');
                    const srcFile = document.getElementById('srcFile');
                    const hfCroppedImage = document.getElementById('<%= hfCroppedImage.ClientID %>');

                    if (previewImage) previewImage.src = '';
                    if (previewContainer) previewContainer.classList.add('d-none');
                    if (hfCroppedImage) hfCroppedImage.value = '';
                    if (srcFile) srcFile.classList.remove('d-none');

                    if (btn) {
                        btn.style.display = 'none';
                    }

                    // Lógica del postback
                    var href = btn.getAttribute('href') || '';
                    var m = href.match(/__doPostBack\('([^']+)'(?:,'([^']*)')?\)/);

                    if (m) {
                        __doPostBack(m[1], m[2] || '');
                    } else {
                        btn.removeAttribute('onclick');
                        btn.click();
                    }
                }
            });
            return false; // Prevenir el postback inmediato
        }

        function showImageInModalAndInitCropper(imageUrl) {
            const modalElement = document.getElementById('imageModal');
            const cropperImage = document.getElementById('cropperImage');
            const imageModal = bootstrap.Modal.getInstance(modalElement) || new bootstrap.Modal(modalElement);

            cropperImage.src = imageUrl;
            imageModal.show();

            if (cropper) {
                cropper.destroy();
            }

            modalElement.addEventListener('shown.bs.modal', function () {
                // Si ya existe, lo destruimos para reiniciar limpio
                if (cropper) cropper.destroy();

                cropper = new Cropper(cropperImage, {
                    viewMode: 1,
                    dragMode: 'move',
                    toggleDragModeOnDblclick: true,
                    aspectRatio: 1,
                    autoCropArea: 0.8,
                    restore: false,
                    guides: true,
                    center: true,
                    highlight: false,
                    cropBoxMovable: true,
                    cropBoxResizable: true,
                });
            });
        }

        function uploader_handleFileChange(event) {
            const file = event.target.files[0];

            // 1. Definimos el límite (10 MB en Bytes)
            const maxSizeInBytes = 10 * 1024 * 1024;

            if (file) {

                // --- A. VALIDACIÓN DE TAMAÑO (Primero, para ahorrar recursos) ---
                if (file.size > maxSizeInBytes) {
                    Swal.fire({
                        title: 'Archivo demasiado grande',
                        // Calculamos los MB para mostrarlo bonito al usuario
                        text: 'La imagen pesa ' + (file.size / (1024 * 1024)).toFixed(2) + ' MB. El límite es 10 MB.',
                        icon: 'warning',
                        allowOutsideClick: false,
                        confirmButtonText: 'Entendido',
                        confirmButtonColor: '#007bff'
                    }).then(() => {
                        // Limpiamos el input para que pueda intentar subir otra
                        event.target.value = null;
                    });
                    return; // DETENEMOS LA EJECUCIÓN AQUÍ
                }

                // --- B. VALIDACIÓN DE TIPO (Tu lógica original) ---
                const fileType = file.type;

                if (fileType === 'image/jpeg' || fileType === 'image/png') {
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        showImageInModalAndInitCropper(e.target.result);
                    };
                    reader.readAsDataURL(file);
                } else {
                    Swal.fire({
                        title: 'Formato no compatible',
                        text: 'Por favor, selecciona un archivo JPG o PNG.',
                        icon: 'error',
                        allowOutsideClick: false,
                        confirmButtonText: 'Aceptar',
                        confirmButtonColor: '#007bff'
                    }).then(() => {
                        event.target.value = null;
                    });
                }
            }
        }

        document.addEventListener("DOMContentLoaded", function () {

            // --- 1. Referencias a Elementos del MODAL ---
            const modalElement = document.getElementById('imageModal');
            const cropperImage = document.getElementById('cropperImage');
            const cropButton = document.getElementById('cropButton');
            const btnZoomIn = document.getElementById('btnZoomIn');
            const btnZoomOut = document.getElementById('btnZoomOut');
            const btnRotate = document.getElementById('btnRotate');
            const btnReset = document.getElementById('btnReset');

            // Validar que los elementos del modal existan
            if (!modalElement || !cropperImage || !cropButton || !btnZoomIn || !btnZoomOut || !btnRotate || !btnReset) {
                console.error("Error: Faltan elementos del MODAL en el HTML. Verifica todos los IDs.");
                return;
            }

            // --- 2. Listeners de Botones del MODAL (se enganchan 1 sola vez) ---
            cropButton.addEventListener('click', function () {
                if (cropper) {
                    const croppedCanvas = cropper.getCroppedCanvas({ width: 1500, height: 1500 });
                    const croppedImageUrl = croppedCanvas.toDataURL('image/jpeg');

                    // Elementos DENTRO del UpdatePanel (los buscamos en el momento)
                    const previewImage = document.getElementById('previewImage');
                    const previewContainer = document.getElementById('previewContainer');
                    const srcFile = document.getElementById('srcFile');
                    const btnBorrar = document.getElementById('<%= btnBorrar.ClientID %>');
                    const hfCroppedImage = document.getElementById('<%= hfCroppedImage.ClientID %>');

                    if (previewImage) previewImage.src = croppedImageUrl;
                    if (previewContainer) previewContainer.classList.remove('d-none');
                    if (srcFile) srcFile.classList.add('d-none');
                    if (btnBorrar) btnBorrar.style.display = 'inline-block';

                    if (hfCroppedImage) {
                        const base64Data = croppedImageUrl.split(',')[1];
                        hfCroppedImage.value = base64Data;
                    }

                    const imageModal = bootstrap.Modal.getInstance(modalElement);
                    if (imageModal) imageModal.hide();
                }
            });

            // Controles del Cropper
            btnZoomIn.addEventListener('click', function () { if (cropper) cropper.zoom(0.1); });
            btnZoomOut.addEventListener('click', function () { if (cropper) cropper.zoom(-0.1); });
            btnRotate.addEventListener('click', function () { if (cropper) cropper.rotate(90); });
            btnReset.addEventListener('click', function () { if (cropper) cropper.reset(); });

            // --- 3. Limpieza al Cerrar el Modal ---
            modalElement.addEventListener('hidden.bs.modal', function () {
                if (cropper) {
                    cropper.destroy(); ss
                    cropper = null;
                }
                // Limpiar el input para poder subir la misma imagen de nuevo
                const uploadInput = document.getElementById('uploadImage');
                if (uploadInput) {
                    uploadInput.value = '';
                }
            });
        });

    </script>
    <script type="text/javascript">
        // Muestra u oculta el botón dependiendo de si hay un archivo seleccionado
        function verificarArchivo() {
            var fileUpload = document.getElementById('<%= fuDocumento.ClientID %>');
            var btnLimpiar = document.getElementById('btnLimpiar');

            if (fileUpload && fileUpload.value !== '') {
                btnLimpiar.style.display = 'inline-block'; // Muestra el botón
            } else {
                btnLimpiar.style.display = 'none'; // Oculta el botón
            }
        }

        // Limpia el archivo y oculta el botón
        function limpiarArchivo() {
            var fileUpload = document.getElementById('<%= fuDocumento.ClientID %>');
            var btnLimpiar = document.getElementById('btnLimpiar');

            if (fileUpload) {
                fileUpload.value = ''; // Limpiamos el valor
            }

            if (btnLimpiar) {
                btnLimpiar.style.display = 'none'; // Ocultamos la 'X' nuevamente
            }
        }

        function AllowOnlyNumbers(e) {
            var charCode = (e.which) ? e.which : e.keyCode;


            if ((charCode >= 48 && charCode <= 57) || charCode == 8) {
                return true; // Permite el caracter
            }

            return false; // Bloquea todos los demás
        }
    </script>



</asp:Content>
