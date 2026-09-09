<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="Vacante.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.Vacante" %>

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
                            <div class="tab-content" id="vacanteTabsContent">
                                <!-- Contenido interno de los sub-tabs -->
                                <!-- Vacante -->
                                <div class="tab-pane fade show active" id="pane-Vacante" role="tabpanel" aria-labelledby="tab-vacante">
                                    <div class="row card-info justify-content-center">
                                        <!-- ===================== (INFORMACIÓN DE LA VACANTE) ===================== -->
                                        <div class="col-lg-12 ps-5 pe-5">
                                            <h2 class="card-header d-flex align-items-center text-dark fw-bold py-3 mb-3">EMPRESA</h2>
                                            <div class="row g-3 mb-5">
                                                <div class="col-md-3 form-floating mb-1">
                                                    <asp:TextBox ID="txtSolicitudNo" runat="server" CssClass="form-control textbox" placeholder="Solicitud No."></asp:TextBox>
                                                    <asp:Label ID="lblSolicitudNo" runat="server" AssociatedControlID="txtSolicitudNo" CssClass="label" Text="Solicitud No."></asp:Label>
                                                </div>
                                                <div class="col-md-12 form-floating mb-1">
                                                    <asp:TextBox ID="txtNombreEmpresa" runat="server" CssClass="form-control textbox" placeholder="Nombre de la Empresa"></asp:TextBox>
                                                    <asp:Label ID="lblNombreEmpresa" runat="server" AssociatedControlID="txtNombreEmpresa" CssClass="label" Text="Nombre de la Empresa"></asp:Label>
                                                </div>
                                                <div class="col-md-12 form-floating mb-1">
                                                    <asp:TextBox ID="txtNombreyPuestoContacto" runat="server" CssClass="form-control textbox" placeholder="Nombre y Puesto del Contacto"></asp:TextBox>
                                                    <asp:Label ID="lblNombreyPuestoContacto" runat="server" AssociatedControlID="txtNombreyPuestoContacto" CssClass="label" Text="Nombre y Puesto del Contacto"></asp:Label>
                                                </div>
                                            </div>
                                            <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 mb-3 mt-3">VACANTE</h4>
                                            <div class="row g-3 mb-5">
                                                <div class="col-md-12 form-floating mb-1">
                                                    <asp:TextBox ID="txtNombrePuesto" runat="server" CssClass="form-control textbox" MaxLength="255" placeholder="Nombre del Puesto"></asp:TextBox>
                                                    <asp:Label ID="lblNombrePuesto" runat="server" AssociatedControlID="txtNombrePuesto" CssClass="label" Text="Nombre del Puesto"></asp:Label>
                                                </div>
                                                <!-- Carrera -->
                                                <div class="col-md-12 mb-3">
                                                    <fieldset class="border rounded-3 p-3">
                                                        <legend class="float-none w-auto px-3 fs-5 fw-bold text-dark">Carrera</legend>

                                                        <!-- Contenedor del rectángulo -->
                                                        <div class="row">
                                                            <div class="col-md-8 mb-3 form-floating">
                                                                <asp:DropDownList ID="ddlIdCarrera" runat="server" CssClass="form-control textbox"></asp:DropDownList>
                                                                <asp:Label ID="lblIdCarrera" runat="server" AssociatedControlID="ddlIdCarrera" CssClass="label" Text="Carrera"></asp:Label>
                                                            </div>

                                                            <div class="col-md-4 d-flex align-items-center mb-3">
                                                                <asp:LinkButton ID="lbtnAgregarCarrera" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Agregar"><i class="">AGREGAR</i></asp:LinkButton>
                                                            </div>

                                                            <div class="table-responsive">
                                                                <asp:GridView ID="gvConsultaCarrera" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="true" AllowPaging="true">
                                                                    <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                                    <PagerStyle CssClass="custom-pager" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnEliminarCarrera" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <!-- Fin Carrera -->

                                                <div class="col-md-12 mt-4">
                                                    <div class="form-check">
                                                        <input type="checkbox" class="form-check-input" id="chkEstudiante" runat="server" />
                                                        <label class="label" for="chkEstudiante">
                                                            Estudiante
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 mt-4">
                                                    <div class="form-check">
                                                        <input type="checkbox" class="form-check-input" id="chkEgresado" runat="server" />
                                                        <label class="label" for="chkEgresado">
                                                            Egresado
                                                        </label>
                                                    </div>
                                                </div>


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
                                                CssClass="btn btn-primary w-100">
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

    </script>
</asp:Content>
