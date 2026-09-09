<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="PerfilCandidato.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.PerfilCandidato" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/cropperjs/1.5.13/cropper.min.css" />
    <script src="https://unpkg.com/cropperjs@1.6.2/dist/cropper.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid p-3">
        <div class="row justify-content-center">
            <div class="col-lg-3">
                <div class="row p-2">
                    <div class="card h-28vh">
                        <div class="card-body contenido ">
                            <!-- ===== Nav de secciones ===== -->
                            <ul class="nav nav-pills d-flex flex-column gap-2 mb-4" id="perfilTabs" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link active" id="tab-personal" data-bs-toggle="tab" data-bs-target="#pane-personal" type="button" role="tab"><i class="fa-solid fa-circle-info me-2"></i>Información Personal</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-contacto" data-bs-toggle="tab" data-bs-target="#pane-contacto" type="button" role="tab"><i class="fa-solid fa-address-card me-2"></i>Datos de Contacto</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-habycomp" data-bs-toggle="tab" data-bs-target="#pane-habycomp" type="button" role="tab"><i class="fa-solid fa-gears me-2"></i>Habilidades y Competencias</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-academico" data-bs-toggle="tab" data-bs-target="#pane-academico" type="button" role="tab"><i class="fa-solid fa-school-flag me-2"></i>Información Académica</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-laboral" data-bs-toggle="tab" data-bs-target="#pane-laboral" type="button" role="tab"><i class="fa-solid fa-briefcase me-2"></i>Experiencia Laboral</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-certificados" data-bs-toggle="tab" data-bs-target="#pane-certificados" type="button" role="tab"><i class="fa-solid fa-certificate me-2"></i>Certificados</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-documentos" data-bs-toggle="tab" data-bs-target="#pane-documentos" type="button" role="tab"><i class="fa-solid fa-file me-2"></i>Documentos</button>
                                </li>
                            </ul>
                            <div id="divAlertaCompletaDatos" class="alert alert-info" runat="server" visible="true">
                                <strong>Completa tu perfil</strong>
                                <p class="mb-0">Ingresa tus datos y documentos para finalizar el registro.</p>
                            </div>
                            <div id="divAlertaRevision" class="alert alert-info" runat="server" visible="false">
                                <strong>En revisión</strong>
                                <p class="mb-0">Tus datos han sido recibidos y están en proceso de revisión para ser aprobados.</p>
                            </div>
                            <div id="divPerfilCompleto" class="alert alert-info" runat="server" visible="false">
                                <strong>¡Perfil Completo!</strong>
                                <p class="mb-0">Tu documentación ha sido revisada y aprobada.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-8">
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
                                        <div class="col-md-3">
                                            <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-3">FOTOGRAFÍA</h4>
                                            <asp:UpdatePanel ID="updImg" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row g-1 card-image">
                                                        <asp:Label ID="lblEstatusImg" runat="server" CssClass="label mb-0" Text=""></asp:Label>
                                                        <div class="border-image p-3 text-center">
                                                            <div class="file-select" id="srcFile" runat="server" clientidmode="Static">
                                                                Subir Imagen
                                                            <input type="file" id="uploadImage" accept="image/*" />
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
                                                            <div id="divAlertaFotografía" class="alert alert-warning mt-3" runat="server" visible="true">
                                                                <strong>IMPORTANTE</strong>
                                                                <p class="mb-0">La imagen debe ser nítida, de frente y con vestimenta formal. Evita el uso de filtros. Tamaño máximo 10 MB.</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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
                                        <!-- ===================== COLUMNA DERECHA (DATOS PERSONALES) ===================== -->
                                        <div class="col-md-9">
                                            <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-3">DATOS PERSONALES</h4>
                                            <asp:UpdatePanel ID="updPersonal" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row g-3">
                                                        <div class="col-md-4 form-floating">
                                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control textbox" MaxLength="25" onpaste="return false;" onkeypress="return AllowOnlyLetters(event);" placeholder="Nombre(s)"></asp:TextBox>
                                                            <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" CssClass="label" Text="Nombre(s)*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4 form-floating">
                                                            <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="form-control textbox" MaxLength="25" onpaste="return false;" onkeypress="return AllowOnlyLetters(event);" placeholder="Apellido Paterno"></asp:TextBox>
                                                            <asp:Label ID="lblPrimerApellido" runat="server" AssociatedControlID="txtPrimerApellido" CssClass="label" Text="Apellido Paterno*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4 form-floating">
                                                            <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="form-control textbox" MaxLength="25" onpaste="return false;" onkeypress="return AllowOnlyLetters(event);" placeholder="Apellido Materno"></asp:TextBox>
                                                            <asp:Label ID="lblSegundoApellido" runat="server" AssociatedControlID="txtSegundoApellido" CssClass="label" Text="Apellido Materno"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4 form-floating">
                                                            <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control textbox" type="date" placeholder="Fecha de Nacimiento" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                                                            <asp:Label ID="lblFechaNacimiento" runat="server" AssociatedControlID="txtFechaNacimiento" CssClass="label" Text="Fecha de Nacimiento*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4 form-floating">
                                                            <asp:DropDownList ID="ddlIdGenero" runat="server" CssClass="form-control textbox"></asp:DropDownList>
                                                            <asp:Label ID="lblGenero" runat="server" AssociatedControlID="ddlIdGenero" CssClass="label" Text="Sexo*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4 form-floating">
                                                            <asp:DropDownList ID="ddlIdEstadoCivil" runat="server" CssClass="form-control textbox"></asp:DropDownList>
                                                            <asp:Label ID="lblIdEstadoCivil" runat="server" AssociatedControlID="ddlIdEstadoCivil" CssClass="label" Text="Estado Civil*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 form-floating">
                                                            <asp:TextBox ID="txtNacionalidad" runat="server" CssClass="form-control textbox" MaxLength="150" onpaste="return false;" onkeypress="return AllowOnlyLetters(event);" placeholder="Nacionalidad"></asp:TextBox>
                                                            <asp:Label ID="lblNacionalidad" runat="server" AssociatedControlID="txtNacionalidad" CssClass="label" Text="Nacionalidad*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4 form-floating">
                                                            <asp:DropDownList ID="ddlIdMunicipio" runat="server" CssClass="form-control textbox select2-custom"></asp:DropDownList>
                                                            <asp:Label ID="lblIdMunicipio" runat="server" AssociatedControlID="ddlIdMunicipio" CssClass="label" Text="Municipio*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-5 form-floating">
                                                            <asp:TextBox ID="txtColonia" runat="server" CssClass="form-control textbox" MaxLength="250" onpaste="return false;" onkeypress="return AllowAlphanumeric(event);" placeholder="Colonia"></asp:TextBox>
                                                            <asp:Label ID="lblColonia" runat="server" AssociatedControlID="txtColonia" CssClass="label" Text="Colonia*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-5 form-floating">
                                                            <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control textbox" MaxLength="250" onpaste="return false;" onkeypress="return AllowAlphanumeric(event);" placeholder="Calle"></asp:TextBox>
                                                            <asp:Label ID="lblCalle" runat="server" AssociatedControlID="txtCalle" CssClass="label" Text="Calle*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 form-floating">
                                                            <asp:TextBox ID="txtNumerodeCasa" runat="server" CssClass="form-control textbox" MaxLength="10" onpaste="return false;" onkeypress="return AllowAlphanumeric(event);" placeholder="Número de Casa"></asp:TextBox>
                                                            <asp:Label ID="lblNumerodeCasa" runat="server" AssociatedControlID="txtNumerodeCasa" CssClass="label" Text="Número de Casa*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4 d-flex align-items-center justify-content-center">
                                                            <asp:CheckBox ID="chkTrabaja" runat="server" CssClass="me-2" />
                                                            <asp:Label ID="lblchkTrabaja" runat="server" AssociatedControlID="chkTrabaja" CssClass="label" Text="Trabaja Actualmente"></asp:Label>
                                                        </div>
                                                        <div class="col-md-7 form-floating">
                                                            <asp:DropDownList ID="ddlIdTiempoDisponible" runat="server" CssClass="form-control textbox select2-custom"></asp:DropDownList>
                                                            <asp:Label ID="lblTiempoDisponible" runat="server" AssociatedControlID="ddlIdTiempoDisponible" CssClass="label" Text="Horario Disponible*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-5 form-floating position-relative">
                                                            <span class="simbolo-peso">$</span>
                                                            <asp:TextBox ID="txtSueldoDeseado" runat="server" CssClass="form-control textbox ps-4" MaxLength="7" onpaste="return false;" onkeypress="return AllowOnlyNumbers(event);" oninput="this.value = this.value.replace(/^0+/, '');" placeholder="Sueldo Deseado*"></asp:TextBox>
                                                            <asp:Label ID="lblSueldoDeseado" runat="server" AssociatedControlID="txtSueldoDeseado" CssClass="label" Text="Sueldo Deseado*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12 form-floating mb-4">
                                                            <asp:TextBox ID="txtBiografia" runat="server" CssClass="form-control textbox" placeholder="Biografía"></asp:TextBox>
                                                            <asp:Label ID="lblBiografia" runat="server" AssociatedControlID="txtBiografia" CssClass="label" Text="Biografía*"></asp:Label>
                                                        </div>
                                                        <div class="col-md-12 form-floating">
                                                            <asp:TextBox ID="txtLinkedinUrl" runat="server" CssClass="form-control textbox" placeholder="URL Linkedin"></asp:TextBox>
                                                            <asp:Label ID="lblLinkdeinUrl" runat="server" AssociatedControlID="txtLinkedinUrl" CssClass="label" Text="URL Linkedin"></asp:Label>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Personal -->
                                <!-- Contacto -->
                                <div class="tab-pane fade show" id="pane-contacto" role="tabpanel" aria-labelledby="tab-contacto">
                                    <div class="row card-info">
                                        <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-3">DATOS DE CONTACTO</h4>
                                        <!-- Correo -->
                                        <div class="col-12 col-md-6 custom-split pe-md-4 mb-5">
                                            <asp:UpdatePanel ID="updCorreo" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="mt-2">
                                                        <h5 class="text-dark d-flex align-items-center gap-2">
                                                            <i class="fa-solid fa-envelope"></i>Correo
                                                        </h5>
                                                        <div class="row">
                                                            <div class="col-12 col-sm-6 col-md-4">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdTipoCorreo" runat="server" CssClass="form-control textbox" placeholder="Tipo Correo"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdTipoCorreo" runat="server" AssociatedControlID="ddlIdTipoCorreo" CssClass="label" Text="Tipo Correo*"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-12 col-sm-6 col-md-6">
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control textbox" MaxLength="255" placeholder="Correo"></asp:TextBox>
                                                                    <asp:Label ID="lblCorreo" runat="server" AssociatedControlID="txtCorreo" CssClass="label" Text="Correo*"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-12 col-md-2 d-flex align-items-center mb-3">
                                                                <asp:LinkButton ID="btnAgregarCorreo" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Agregar" OnClick="btnAgregarCorreo_Click" OnClientClick="sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');"><i class="fa-solid fa-plus"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="gvConsultaCorreo" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="true" AllowPaging="true" DataKeyNames="Id" OnRowDataBound="gvConsultaCorreo_RowDataBound" OnRowCommand="gvConsultaCorreo_RowCommand">
                                                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                                <PagerStyle CssClass="custom-pager" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnEliminarCorreo" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="false" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Correo -->
                                        <!-- Divisor solo en móvil -->
                                        <hr class="d-md-none my-1">
                                        <!-- Teléfono -->
                                        <div class="col-12 col-md-6 pe-md-0">
                                            <asp:UpdatePanel ID="updTelefono" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="mt-2">
                                                        <h5 class="text-dark d-flex align-items-center gap-2">
                                                            <i class="fa-solid fa-phone"></i>Teléfono
                                                        </h5>
                                                        <div class="row">
                                                            <div class="col-12 col-sm-6 col-md-3">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdTipoTelefono" runat="server" CssClass="form-control textbox" placeholder="Tipo Teléfono"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdTipoTelefono" runat="server" AssociatedControlID="ddlIdTipoTelefono" CssClass="label" Text="Tipo Teléfono*"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-12 col-sm-6 col-md-3">
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control textbox" MaxLength="10" onpaste="return false;" onkeypress="return AllowOnlyNumbers(event);" placeholder="Teléfono"></asp:TextBox>
                                                                    <asp:Label ID="lblTelefono" runat="server" AssociatedControlID="txtTelefono" CssClass="label" Text="Teléfono*"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-12 col-sm-6 col-md-4">
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="txtExtension" runat="server" CssClass="form-control textbox" MaxLength="5" onpaste="return false;" onkeypress="return AllowOnlyNumbers(event);" placeholder="Extensión"></asp:TextBox>
                                                                    <asp:Label ID="lblExtension" runat="server" AssociatedControlID="txtExtension" CssClass="label" Text="Extensión"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-12 col-md-2 d-flex align-items-center mb-3">
                                                                <asp:LinkButton ID="lbtnAgregarTelefono" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Agregar" OnClick="lbtnAgregarTelefono_Click" OnClientClick="sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');"><i class="fa-solid fa-plus"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="gvConsultaTelefono" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="true" AllowPaging="true" OnRowDataBound="gvConsultaTelefono_RowDataBound" OnRowCommand="gvConsultaTelefono_RowCommand">
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Teléfono -->
                                    </div>
                                </div>
                                <!-- Fin Contacto -->
                                <!-- Habilidades y Competencias -->
                                <div class="tab-pane fade show" id="pane-habycomp" role="tabpanel" aria-labelledby="tab-habycomp">
                                    <div class="row">
                                        <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-3">HABILIDADES Y COMPETENCIAS</h4>
                                        <!-- Area Experiencia -->
                                        <div class="col-md-10 ps-4 mb-4 mx-auto">
                                            <asp:UpdatePanel ID="updAreaExperiencia" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="mt-3 table-responsive-sm">
                                                        <h5 class="text-dark d-flex align-items-center gap-2"><i class="fa-solid fa-bars-progress"></i>Experiencia en Area</h5>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdAreaExperiencia" runat="server" CssClass="form-control textbox select2-custom" placeholder="Area"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdAreaExperiencia" runat="server" AssociatedControlID="ddlIdAreaExperiencia" CssClass="label" Text="Area"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="txtAniosExperiencia" runat="server"
                                                                        CssClass="form-control textbox"
                                                                        TextMode="Number"
                                                                        min="0"
                                                                        max="99"
                                                                        step="1"
                                                                        placeholder="Años"
                                                                        MaxLength="2"
                                                                        oninput="if(this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength); validarExperiencia();"
                                                                        onkeydown="return event.key != '.' && event.key != ',' && event.key != 'e'">
                                                                    </asp:TextBox>
                                                                    <asp:Label ID="lblAniosExperiencia" runat="server" AssociatedControlID="txtAniosExperiencia" CssClass="label" Text="Años"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-floating mb-3">
                                                                    <asp:TextBox ID="txtMesesExperiencia" runat="server"
                                                                        CssClass="form-control textbox"
                                                                        TextMode="Number"
                                                                        min="0"
                                                                        max="11"
                                                                        step="1"
                                                                        placeholder="Meses"
                                                                        MaxLength="2"
                                                                        oninput="if(parseInt(this.value) > 11) this.value = 11; if(this.value.length > 2) this.value = this.value.slice(0, 2); validarExperiencia();"
                                                                        onkeydown="return event.key != '.' && event.key != ',' && event.key != 'e' && event.key != '-'">
                                                                    </asp:TextBox>
                                                                    <asp:Label ID="lblMesesExperiencia" runat="server" AssociatedControlID="txtMesesExperiencia" CssClass="label" Text="Meses"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2 d-flex align-items-center mb-3">
                                                                <asp:LinkButton ID="lbtnAgregarAreaExperiencia" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Agregar" OnClick="lbtnAgregarAreaExperiencia_Click" OnClientClick="sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');"><i class="fa-solid fa-plus"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <asp:GridView ID="gvConsultaAreaExperiencia" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="true" AllowPaging="true" OnRowDataBound="gvConsultaAreaExperiencia_RowDataBound" OnRowCommand="gvConsultaAreaExperiencia_RowCommand">
                                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                            <PagerStyle CssClass="custom-pager" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEliminarAreaExperiencia" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Area de Interés -->
                                        <!-- Area de Interés -->
                                        <div class="col-md-10 ps-4 mb-4 mx-auto">
                                            <asp:UpdatePanel ID="updAreaInteres" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="mt-3 table-responsive-sm">
                                                        <h5 class="text-dark d-flex align-items-center gap-2"><i class="fa-solid fa-bullseye"></i>Area de Interés</h5>
                                                        <div class="row">
                                                            <div class="col-md-10">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdAreaInteres" runat="server" CssClass="form-control textbox select2-custom" placeholder="Area de Interés"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdAreaInteres" runat="server" AssociatedControlID="ddlIdAreaInteres" CssClass="label" Text="Area de Interés"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2 d-flex align-items-center mb-3">
                                                                <asp:LinkButton ID="lbtnAgregarAreaInteres" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Agregar" OnClick="lbtnAgregarAreaInteres_Click" OnClientClick="sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');"><i class="fa-solid fa-plus"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <asp:GridView ID="gvConsultaAreaInteres" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="true" AllowPaging="true" OnRowDataBound="gvConsultaAreaInteres_RowDataBound" OnRowCommand="gvConsultaAreaInteres_RowCommand">
                                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                            <PagerStyle CssClass="custom-pager" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEliminarAreaInteres" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Area de Interés -->
                                        <!-- Habilidades -->
                                        <div class="col-md-10 ps-4 mb-4 mx-auto">
                                            <asp:UpdatePanel ID="updHabilidades" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="mt-3 table-responsive-sm">
                                                        <h5 class="text-dark d-flex align-items-center gap-2"><i class="fa-solid fa-brain"></i>Habilidades</h5>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdTipoHabilidad" runat="server" CssClass="form-control textbox" placeholder="Tipo de Habilidad" AutoPostBack="True" OnSelectedIndexChanged="ddlIdTipoHabilidad_SelectedIndexChanged"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdTipoHabilidad" runat="server" AssociatedControlID="ddlIdTipoHabilidad" CssClass="label" Text="Tipo de Habilidad"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdHabilidad" runat="server" CssClass="form-control textbox" placeholder="Habilidad"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdHabilidad" runat="server" AssociatedControlID="ddlIdHabilidad" CssClass="label" Text="Habilidad"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2 d-flex align-items-center mb-3">
                                                                <asp:LinkButton ID="lbtnAgregarHabilidad" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Agregar" OnClick="lbtnAgregarHabilidad_Click" OnClientClick="sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');"><i class="fa-solid fa-plus"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <asp:GridView ID="gvConsultaHabilidad" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="true" AllowPaging="true" OnRowDataBound="gvConsultaHabilidad_RowDataBound" OnRowCommand="gvConsultaHabilidad_RowCommand">
                                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                            <PagerStyle CssClass="custom-pager" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEliminarHabilidad" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Habilidades -->
                                        <!-- Software -->
                                        <div class="col-md-10 ps-4 mb-4 mx-auto">
                                            <asp:UpdatePanel ID="updSoftware" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="mt-3 table-responsive-sm">
                                                        <h5 class="text-dark d-flex align-items-center gap-2"><i class="fa-solid fa-laptop-code"></i>Software</h5>
                                                        <div class="row">
                                                            <div class="col-md-7">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdPaqueteSoftware" runat="server" CssClass="form-control textbox select2-custom" placeholder="Paquete Software"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdPaqueteSoftware" runat="server" AssociatedControlID="ddlIdPaqueteSoftware" CssClass="label" Text="Paquete Software"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdNivelSoftware" runat="server" CssClass="form-control textbox select2-custom" placeholder="Nivel"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdNivelSoftware" runat="server" AssociatedControlID="ddlIdNivelSoftware" CssClass="label" Text="Nivel"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2 d-flex align-items-center  mb-3">
                                                                <asp:LinkButton ID="lbtnAgregarPaqueteSoftware" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Agregar" OnClick="lbtnAgregarPaqueteSoftware_Click" OnClientClick="sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');"><i class="fa-solid fa-plus"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <asp:GridView ID="gvConsultaSoftware" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="true" AllowPaging="true" OnRowDataBound="gvConsultaSoftware_RowDataBound" OnRowCommand="gvConsultaSoftware_RowCommand">
                                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                            <PagerStyle CssClass="custom-pager" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEliminarSoftware" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Software -->
                                        <!-- Idioma -->
                                        <div class="col-md-10 ps-4 mb-4 mx-auto">
                                            <asp:UpdatePanel ID="updIdioma" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="mt-3 table-responsive-sm">
                                                        <h5 class="text-dark d-flex align-items-center gap-2"><i class="fa-solid fa-language"></i>Idioma</h5>
                                                        <div class="row">
                                                            <div class="col-md-7">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdIdioma" runat="server" CssClass="form-control textbox select2-custom" placeholder="Idioma"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdIdioma" runat="server" AssociatedControlID="ddlIdIdioma" CssClass="label" Text="Idioma"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdNivelIdioma" runat="server" CssClass="form-control textbox select2-custom" placeholder="Nivel"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdNivelIdioma" runat="server" AssociatedControlID="ddlIdNivelIdioma" CssClass="label" Text="Nivel"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2 d-flex align-items-center mb-3">
                                                                <asp:LinkButton ID="lbtnAgregarIdioma" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Agregar" OnClick="lbtnAgregarIdioma_Click" OnClientClick="sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');"><i class="fa-solid fa-plus"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <asp:GridView ID="gvConsultaIdioma" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="true" AllowPaging="true" OnRowDataBound="gvConsultaIdioma_RowDataBound" OnRowCommand="gvConsultaIdioma_RowCommand">
                                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                            <PagerStyle CssClass="custom-pager" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEliminarIdioma" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Idioma -->
                                        <!-- Certificación Idioma -->
                                        <div class="col-md-10 ps-4 mb-4 mx-auto">
                                            <asp:UpdatePanel ID="updCertificacionIdioma" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="mt-3 table-responsive-sm">
                                                        <h5 class="text-dark d-flex align-items-center gap-2"><i class="fa-solid fa-certificate"></i>Certificacion de Idioma</h5>
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdIdiomaCertificacion" runat="server" OnSelectedIndexChanged="ddlIdIdiomaCertificacion_SelectedIndexChanged" CssClass="form-control textbox" placeholder="Idioma"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdIdiomaCertificacion" runat="server" AssociatedControlID="ddlIdIdiomaCertificacion" CssClass="label" Text="Idioma"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-7">
                                                                <div class="form-floating mb-3">
                                                                    <asp:DropDownList ID="ddlIdCertificacionIdioma" runat="server" CssClass="form-control textbox" placeholder="Certificación"></asp:DropDownList>
                                                                    <asp:Label ID="lblIdCertificacionIdioma" runat="server" AssociatedControlID="ddlIdCertificacionIdioma" CssClass="label" Text="Certificación"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2 d-flex align-items-center mb-3">
                                                                <asp:LinkButton ID="lbtnAgregarCertificacionIdioma" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Agregar" OnClick="lbtnAgregarCertificacionIdioma_Click" OnClientClick="sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');"><i class="fa-solid fa-plus"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <asp:GridView ID="gvConsultaCertificacionIdioma" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="true" AllowPaging="true" OnRowDataBound="gvConsultaCertificacionIdioma_RowDataBound" OnRowCommand="gvConsultaCertificacionIdioma_RowCommand">
                                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                            <PagerStyle CssClass="custom-pager" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEliminarCertificacionIdioma" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- Fin Certificación Idioma -->
                                    </div>
                                </div>
                                <!-- Fin Habilidades y Competencias -->
                                <!-- Academico -->
                                <div class="tab-pane fade show" id="pane-academico" role="tabpanel" aria-labelledby="tab-academico">
                                    <div class="row card-info">
                                        <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-3">INFORMACIÓN ACADÉMICA</h4>
                                        <div class="row">
                                            <div class="col-md-3 form-floating mb-3">
                                                <asp:DropDownList ID="ddlIdTipoSustentante" runat="server" CssClass="form-control textbox" placeholder="Sustentante" OnSelectedIndexChanged="ddlIdTipoSustentante_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label ID="lblIdTipoSustentante" runat="server" AssociatedControlID="ddlIdTipoSustentante" CssClass="label" Text="Estatus Candidato*"></asp:Label>
                                            </div>
                                            <div class="col-md-3 form-floating mb-3">
                                                <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control textbox" MaxLength="7" onkeypress="return AllowOnlyNumbers(event);" placeholder="Matrícula"></asp:TextBox>
                                                <asp:Label ID="lblMatricula" runat="server" AssociatedControlID="txtMatricula" CssClass="label" Text="Matrícula*"></asp:Label>
                                            </div>
                                            <div class="col-md-2 form-floating mb-3">
                                                <asp:DropDownList ID="ddlIdTipoGrado" runat="server" CssClass="form-control textbox" placeholder="Grado"></asp:DropDownList>
                                                <asp:Label ID="lblIdTipoGrado" runat="server" AssociatedControlID="ddlIdTipoGrado" CssClass="label" Text="Grado*"></asp:Label>
                                            </div>
                                            <div class="col-md-4 form-floating mb-3">
                                                <asp:DropDownList ID="ddlIdCarrera" runat="server" CssClass="form-control textbox select2-custom" placeholder="Carrera" OnSelectedIndexChanged="ddlIdCarrera_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:Label ID="lblIdCarrera" runat="server" AssociatedControlID="ddlIdCarrera" CssClass="label" Text="Carrera*"></asp:Label>
                                            </div>
                                        </div>
                                        <asp:PlaceHolder ID="phEstudiante" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="col-md-4 form-floating mb-3">
                                                    <asp:DropDownList ID="ddlIdPlanEstudios" runat="server" CssClass="form-control textbox" Enabled="false" placeholder="Plan" OnSelectedIndexChanged="ddlIdPlanEstudios_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:Label ID="lblIdPlanEstudios" runat="server" AssociatedControlID="ddlIdPlanEstudios" CssClass="label" Text="Plan*"></asp:Label>
                                                </div>
                                                <div class="col-md-4 form-floating mb-3">
                                                    <asp:DropDownList ID="ddlIdModalidad" runat="server" CssClass="form-control textbox" Enabled="false" placeholder="Modalidad*"></asp:DropDownList>
                                                    <asp:Label ID="lblIdModalidad" runat="server" AssociatedControlID="ddlIdModalidad" CssClass="label" Text="Modalidad*"></asp:Label>
                                                </div>
                                                <div class="col-md-4 form-floating mb-3">
                                                    <asp:DropDownList ID="ddlIdSemestre" runat="server" CssClass="form-control textbox" placeholder="Semestre"></asp:DropDownList>
                                                    <asp:Label ID="lblIdSemestre" runat="server" AssociatedControlID="ddlIdSemestre" CssClass="label" Text="Semestre*"></asp:Label>
                                                </div>
                                                <div class="col-md-4 form-floating mb-3">
                                                    <asp:DropDownList ID="ddlIdTurnoEscolar" runat="server" CssClass="form-control textbox" placeholder="Turno Escolar"></asp:DropDownList>
                                                    <asp:Label ID="lblIdTurnoEscolar" runat="server" AssociatedControlID="ddlIdTurnoEscolar" CssClass="label" Text="Turno Escolar*"></asp:Label>
                                                </div>
                                                <div class="col-md-4 form-floating mb-3">
                                                    <asp:Label ID="lblServicioSocial" runat="server" CssClass="label" Text="Realizó Servicio Social*"></asp:Label>
                                                    <div class="d-flex gap-4">
                                                        <div class="form-check">
                                                            <asp:RadioButton ID="rbServicioSi" runat="server" GroupName="ServicioSocial" CssClass="" />
                                                            <label class="form-check-label" for="<%= rbServicioSi.ClientID %>">Sí</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <asp:RadioButton ID="rbServicioNo" runat="server" GroupName="ServicioSocial" CssClass="" />
                                                            <label class="form-check-label" for="<%= rbServicioNo.ClientID %>">No</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 form-floating mb-3">
                                                    <asp:Label ID="lblPracticasProfesionales" runat="server" CssClass="label" Text="Realizó Practicas Profesionales*"></asp:Label>
                                                    <div class="d-flex gap-4">
                                                        <div class="form-check">
                                                            <asp:RadioButton ID="rbPracticasSi" runat="server" GroupName="PracticasProfesionales" CssClass="" />
                                                            <label class="form-check-label" for="<%= rbPracticasSi.ClientID %>">Sí</label>
                                                        </div>
                                                        <div class="form-check">
                                                            <asp:RadioButton ID="rbPracticasNo" runat="server" GroupName="PracticasProfesionales" CssClass="" />
                                                            <label class="form-check-label" for="<%= rbPracticasNo.ClientID %>">No</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder ID="phEgresado" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="col-md-2 form-floating mb-3">
                                                    <asp:DropDownList ID="ddlAnioIngreso" runat="server" CssClass="form-control textbox select2-custom" onchange="filtrarIngreso();"></asp:DropDownList>
                                                    <asp:Label ID="lblAnioIngreso" runat="server" AssociatedControlID="ddlAnioIngreso" CssClass="label" Text="Año Ingreso*"></asp:Label>
                                                </div>
                                                <div class="col-md-2 form-floating mb-3">
                                                    <asp:DropDownList ID="ddlAnioEgreso" runat="server" CssClass="form-control textbox select2-custom" onchange="filtrarEgreso();"></asp:DropDownList>
                                                    <asp:Label ID="lblAnioEgreso" runat="server" AssociatedControlID="ddlAnioEgreso" CssClass="label" Text="Año Egreso*"></asp:Label>
                                                </div>
                                                <div class="col-md-4 form-floating mb-3">
                                                    <asp:DropDownList ID="ddlIdEstatusAcademico" runat="server" CssClass="form-control textbox" placeholder="Estatus Académico*"></asp:DropDownList>
                                                    <asp:Label ID="lblIdEstatusAcademicoFormacion" runat="server" AssociatedControlID="ddlIdEstatusAcademico" CssClass="label" Text="Estatus Académico*"></asp:Label>
                                                </div>
                                                <div class="col-md-4 form-floating mb-3">
                                                    <asp:DropDownList ID="ddlIdEstatusTitulacion" runat="server" CssClass="form-control textbox" placeholder="Estatus Titulación de Licenciatura*"></asp:DropDownList>
                                                    <asp:Label ID="lblIdEstatusTitulacion" runat="server" AssociatedControlID="ddlIdEstatusTitulacion" CssClass="label" Text="Estatus Titulación de Licenciatura*"></asp:Label>
                                                </div>
                                            </div>
                                        </asp:PlaceHolder>
                                        <div class="row">
                                            <div class="col-md-3 form-floating mb-3">
                                                <asp:TextBox ID="txtPromedio" runat="server"
                                                    CssClass="form-control textbox"
                                                    TextMode="Number"
                                                    step="0.01"
                                                    min="1"
                                                    max="100"
                                                    oninput="validarPromedio(this);"
                                                    onblur="this.value = this.value.replace(/^0+(?=\d)/, '');"
                                                    placeholder="Promedio">
                                                </asp:TextBox>
                                                <asp:Label ID="lblPromedio" runat="server" TextMode="Number"
                                                    AssociatedControlID="txtPromedio" CssClass="label" Text="Promedio"></asp:Label>
                                            </div>
                                            <div class="col-md-9 form-floating mb-3">
                                                <asp:TextBox ID="txtOtraCarrera" runat="server" CssClass="form-control textbox" MaxLength="150" onkeypress="return AllowOnlyLetters(event);" placeholder="Otra Carrera"></asp:TextBox>
                                                <asp:Label ID="lblOtraCarrera" runat="server" AssociatedControlID="txtOtraCarrera" CssClass="label" Text="Otra Carrera"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Academico -->
                                <!--Experiencia Laboral-->
                                <div class="tab-pane fade show" id="pane-laboral" role="tabpanel" aria-labelledby="tab-laboral">
                                    <div class="row card-info">
                                        <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top"
                                                    style="background: #f7f7f7; font-weight: 400; font-size: 1.5rem; border-bottom: 3px solid #000000;">
                                                    <i style="color: #3498db;"></i>EXPERIENCIA LABORAL
                                                    <asp:LinkButton ID="btnAgregarReporte" runat="server" CssClass="btn btn-success text-white ms-auto" OnClick="btnAgregarReporte_Click">&nbsp;Agregar</asp:LinkButton>
                                                </h4>
                                                <br />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="updListaExperiencia" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:ListView ID="lvExperiencia" runat="server" OnItemDataBound="lvExperiencia_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="row row-cols-1 row-cols-md-3 g-4">
                                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <div class="col-md-12">
                                                            <div class="card-laboral h-100">
                                                                <div class="card-body-laboral">
                                                                    <h4 class="card-title">
                                                                        <%-- Usa Eval() para jalar datos de la columna --%>
                                                                        <%# Eval("Empresa") %>
                                                                    </h4>
                                                                    <h6 class="card-subtitle mb-2 text-muted">
                                                                        <%# Eval("Puesto") %>
                                                                    </h6>
                                                                    <span class="badge bg-warning text-dark mb-2 text-wrap" style="text-align: left;">
                                                                        <%# Eval("TipoTrabajo") %>
                                                                    </span>
                                                                    <%# FormatearDescripcionComoLista(Eval("Descripcion")) %>
                                                                </div>
                                                                <div class="card-footer-laboral">
                                                                    <br />
                                                                    <small class="text-muted text-laboral">Inicio: <strong><%# Eval("FechaInicio", "{0:dd/MM/yyyy}") %></strong></small>
                                                                    <br />
                                                                    <small class="text-muted text-laboral">Fin: <strong><%# (Eval("FechaFin") == null || Eval("FechaFin") == DBNull.Value) ? "Presente" : Eval("FechaFin", "{0:dd/MM/yyyy}") %></strong></small>
                                                                    <div class="mt-2 text-end">
                                                                        <asp:LinkButton
                                                                            ID="lnkEditarExperiencia"
                                                                            runat="server"
                                                                            Text="Editar"
                                                                            CssClass="btn text-white btn-xl me-2"
                                                                            CommandName="EditarExperiencia"
                                                                            OnClick="lnkEditarExperiencia_Click"
                                                                            CommandArgument='<%# Eval("Id") %>'>
                                                                        </asp:LinkButton>
                                                                        <asp:LinkButton
                                                                            ID="lnkEliminarExperiencia"
                                                                            runat="server"
                                                                            Text="Borrar"
                                                                            CssClass="btn text-white btn-xl"
                                                                            CommandName="EliminarExperiencia"
                                                                            OnClick="lnkEliminarExperiencia_Click"
                                                                            CommandArgument='<%# Eval("Id") %>'
                                                                            OnClientClick="return showConfirmation(this);">
                                                                        </asp:LinkButton>
                                                                        <asp:Label ID="lblEspacio" runat="server" AssociatedControlID="lblEspacio" CssClass="label" Text=" "></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <div class="alert alert-info" role="alert">
                                                            No se encontró experiencia laboral para este sustentante.
                                                        </div>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <!-- Modal -->
                                <div class="modal fade" id="ModalNuevo" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                                    <div class="modal-dialog modal-dialog-centered modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="card-title titulo"><i class="fa-solid fa-pen-to-square"></i>&nbsp;Añadir Experiencia Laboral</h5>
                                            </div>
                                            <asp:UpdatePanel ID="updModal" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="modal-body">
                                                        <!-- Controles -->
                                                        <div class="row g-3">
                                                            <div class="col-md-6 form-floating">
                                                                <asp:TextBox ID="txtPuesto" runat="server" CssClass="form-control textbox" MaxLength="55" placeholder="Puesto"></asp:TextBox>
                                                                <asp:Label ID="lblPuesto" runat="server" AssociatedControlID="txtPuesto" CssClass="label" Text="Puesto"></asp:Label>
                                                            </div>
                                                            <div class="col-md-6 form-floating">
                                                                <asp:TextBox ID="txtEmpresa" runat="server" CssClass="form-control textbox" MaxLength="55" placeholder="Empresa"></asp:TextBox>
                                                                <asp:Label ID="lblEmpresa" runat="server" AssociatedControlID="txtEmpresa" CssClass="label" Text="Empresa"></asp:Label>
                                                            </div>
                                                            <div class="col-md-12 form-floating">
                                                                <asp:TextBox ID="txtTipoTrabajo" runat="server"
                                                                    CssClass="form-control textbox"
                                                                    MaxLength="25"
                                                                    placeholder="Tipo de Trabajo"
                                                                    title="Tipo de Trabajo (Medio Tiempo, Tiempo Completo, Practicas Profesionales, etc.)">
                                                                </asp:TextBox>
                                                                <asp:Label ID="lblTipoTrabajo" runat="server" AssociatedControlID="txtTipoTrabajo" CssClass="label" Text="Tipo de Trabajo (Medio Tiempo, Tiempo Completo, Practicas Profesionales, etc.)"></asp:Label>
                                                            </div>
                                                            <div class="col-md-6 form-floating">
                                                                <asp:TextBox ID="txtFechaInicioLaboral" runat="server" CssClass="form-control textbox" type="date" placeholder="Fecha Inicio" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                                                                <asp:Label ID="lblFechaInicioLaboral" runat="server" AssociatedControlID="txtFechaInicioLaboral" CssClass="label" Text="Fecha Inicio"></asp:Label>
                                                            </div>
                                                            <div class="col-md-6 form-floating">
                                                                <asp:TextBox ID="txtFechaFinLaboral" runat="server" CssClass="form-control textbox" type="date" placeholder="Fecha Fin" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                                                                <asp:Label ID="lblFechaFinLaboral" runat="server" AssociatedControlID="txtFechaFinLaboral" CssClass="label" Text="Fecha Fin"></asp:Label>
                                                            </div>
                                                            <div class="col-md-10 form-floating">
                                                                <asp:TextBox ID="txtDescripcionLaboral" runat="server" CssClass="form-control textbox" onpaste="return false;" onkeypress="return AllowAlphanumeric(event);" MaxLength="55" placeholder="Actividades"></asp:TextBox>
                                                                <asp:Label ID="lblDescripcionLaboral" runat="server" AssociatedControlID="txtDescripcionLaboral" CssClass="label" Text="Actividades y/o funciones"></asp:Label>
                                                            </div>
                                                            <div class="col-md-2 d-flex align-items-center">
                                                                <asp:LinkButton ID="btnAgregarActividad" runat="server" CssClass="btn btn-success w-100 text-white" OnClick="btnAgregarActividad_Click"><i class="fa-solid fa-plus"></i></asp:LinkButton>
                                                            </div>
                                                            <!-- Tabla para mostrar la información -->
                                                            <div class="mt-3 table-responsive">
                                                                <asp:GridView ID="gvListaLaboral" runat="server" CssClass="table table-sm table-striped table-hover" PageSize="4" AllowPaging="true" OnRowDataBound="gvListaLaboral_RowDataBound" OnRowCommand="gvListaLaboral_RowCommand" OnPageIndexChanging="gvListaLaboral_PageIndexChanging">
                                                                    <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                                    <PagerStyle CssClass="custom-pager" />
                                                                    <PagerTemplate>
                                                                        <div class="pagination">
                                                                            <asp:LinkButton ID="btnFirstPage" runat="server" CommandName="Page" CommandArgument="First" CssClass="pagination-btn" ToolTip="Primera"><i class="fa-solid fa-less-than-equal"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnPrevPage" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="pagination-btn" ToolTip="Anterior"><i class="fa-solid fa-less-than"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnNextPage" runat="server" CommandName="Page" CommandArgument="Next" CssClass="pagination-btn" ToolTip="Siguiente"><i class="fa-solid fa-greater-than"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="btnLastPage" runat="server" CommandName="Page" CommandArgument="Last" CssClass="pagination-btn" ToolTip="Última"><i class="fa-solid fa-greater-than-equal"></i></asp:LinkButton>
                                                                            <asp:Label ID="lblPageInfo" runat="server" CssClass="page-info mt-2" Text='<%# String.Format("Página {0} de {1}", gvListaLaboral.PageIndex + 1, gvListaLaboral.PageCount) %>'></asp:Label>
                                                                        </div>
                                                                    </PagerTemplate>
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnEliminarDescripcion" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            <!-- Fin Tabla para mostrar la información -->
                                                        </div>
                                                    </div>
                                                    <!-- Fin Controles -->
                                                    <div class="modal-footer">
                                                        <asp:LinkButton ID="btnGrabarLaboral" runat="server" CssClass="btn btn-secondary btnGris w-25" OnClick="btnGrabarLaboral_Click"><i class="fa-regular fa-floppy-disk"></i>&nbsp;Grabar</asp:LinkButton>
                                                        <asp:LinkButton ID="btnCerrarLaboral" runat="server" CssClass="btn btn-danger btnRed w-25" OnClick="btnCerrarLaboral_Click"><i class="fa-solid fa-xmark"></i>&nbsp;Cerrar</asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Modal -->
                                <!--Fin Experiencia Laboral-->
                                <!--Certificados-->
                                <div class="tab-pane fade show" id="pane-certificados" role="tabpanel" aria-labelledby="tab-certificados">
                                    <div class="row card-info">
                                        <asp:UpdatePanel ID="updHeaderC" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top"
                                                    style="background: #f7f7f7; font-weight: 400; font-size: 1.5rem; border-bottom: 3px solid #000000;">
                                                    <i style="color: #3498db;"></i>CERTIFICADOS
                                                    <asp:LinkButton ID="btnAgregarCertificado" runat="server" CssClass="btn btn-success text-white ms-auto" OnClick="btnAgregarCertificado_Click">&nbsp;Agregar</asp:LinkButton>
                                                </h4>
                                                <br />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="updListaCertificado" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:ListView ID="lvCertificado" runat="server" OnItemDataBound="lvCertificado_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="row row-cols-1 row-cols-md-3 g-4">
                                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <div class="col-md-12">
                                                            <div class="card-laboral h-100">
                                                                <div class="card-body-laboral">
                                                                    <div class="d-flex justify-content-between align-items-center">
                                                                        <%-- Elemento de la izquierda --%>
                                                                        <div>
                                                                            <h5 class="card-title mb-2"><%-- mb-0 quita el margen inferior para mejor alineación --%>
                                                                                <%# Eval("Descripcion") %>
                                                                            </h5>
                                                                        </div>
                                                                        <%-- Elemento de la derecha --%>
                                                                        <div>
                                                                            <h6 class="card-subtitle text-muted mb-2" style="margin-right: 2vh;"><%-- mb-0 quita el margen inferior --%>
                                                                                N° <%# Eval("NumeroCertificado") %>
                                                                            </h6>
                                                                        </div>
                                                                    </div>
                                                                    <h6 class="card-subtitle mb-2 text-muted">
                                                                        <%# Eval("InstitucionEmisora") %>
                                                                    </h6>
                                                                    <h6><small class="card-subtitle mb-2 text-muted">Fecha Emisión: <strong><%# Eval("FechaEmision", "{0:dd/MM/yyyy}") %></strong></small></h6>
                                                                    <h6 class="card-subtitle mb-2 text-muted">
                                                                        <asp:HyperLink
                                                                            runat="server"
                                                                            NavigateUrl='<%# ValidarDestino(Eval("UrlVerificacion")) %>'
                                                                            Target="_blank"
                                                                            CssClass="btn btn-outline-primary btn-sm"
                                                                            Visible='<%# !string.IsNullOrEmpty(Convert.ToString(Eval("UrlVerificacion"))) %>'>
    <i class="fa-solid fa-link me-1"></i>
    Mostrar Enlace
                                                                        </asp:HyperLink>
                                                                    </h6>
                                                                    <h6 class="card-subtitle mb-2 text-muted">
                                                                        <%# (Eval("ArchivoCertificado") != DBNull.Value && !string.IsNullOrEmpty(Eval("ArchivoCertificado").ToString())) ? "Documento Cargado" : "" %>
                                                                    </h6>
                                                                </div>
                                                                <div class="card-footer-laboral">
                                                                    <div class="mt-2 text-end">

                                                                        <asp:LinkButton
                                                                            ID="lnkEliminarCertificado"
                                                                            runat="server"
                                                                            Text="Borrar"
                                                                            CssClass="btn text-white btn-xl"
                                                                            CommandName="EliminarCertificado"
                                                                            OnClick="lnkEliminarCertificado_Click"
                                                                            CommandArgument='<%# Eval("Id") %>'
                                                                            OnClientClick="return showConfirmation(this);">
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <div class="alert alert-info" role="alert">
                                                            No se encontró ningún certficado para este sustentante.
                                                        </div>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <!-- Modal -->
                                <div class="modal fade" id="ModalNuevo2" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                                    <div class="modal-dialog modal-dialog-centered modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="card-title titulo"><i class="fa-solid fa-pen-to-square"></i>&nbsp;Añadir Certificado</h5>
                                            </div>
                                            <div class="modal-body">
                                                <!-- Controles -->
                                                <div class="row g-3">
                                                    <div class="col-md-12 form-floating">
                                                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control textbox" MaxLength="150" placeholder="Descripción del Certificado"></asp:TextBox>
                                                        <asp:Label ID="lblDescripcion" runat="server" AssociatedControlID="txtDescripcion" CssClass="label" Text="Descripción del Certificado"></asp:Label>
                                                    </div>
                                                    <div class="col-md-12 form-floating">
                                                        <asp:TextBox ID="txtInstiucionEmisora" runat="server" CssClass="form-control textbox" MaxLength="100" placeholder="Institución Emisora"></asp:TextBox>
                                                        <asp:Label ID="lblInstitucionEmisora" runat="server" AssociatedControlID="txtInstiucionEmisora" CssClass="label" Text="Institución Emisora"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6 form-floating">
                                                        <asp:TextBox ID="txtFechaEmision" runat="server" CssClass="form-control textbox" type="date" placeholder="Fecha Emisión" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                                                        <asp:Label ID="lblFechaEmision" runat="server" AssociatedControlID="txtFechaEmision" CssClass="label" Text="Fecha Emisión"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6 form-floating">
                                                        <asp:TextBox ID="txtNumeroCertificado" runat="server" CssClass="form-control textbox" MaxLength="50" placeholder="Número Certificado"></asp:TextBox>
                                                        <asp:Label ID="lblNumeroCertificado" runat="server" AssociatedControlID="txtNumeroCertificado" CssClass="label" Text="Número Certificado"></asp:Label>
                                                    </div>
                                                    <div class="col-md-12 form-floating mb-3">
                                                        <asp:TextBox ID="txtUrlVerificacion" runat="server" CssClass="form-control textbox" MaxLength="255" placeholder="URL Verificación"></asp:TextBox>
                                                        <asp:Label ID="lblUrlVerificacion" runat="server" AssociatedControlID="txtUrlVerificacion" CssClass="label" Text="URL Verificación"></asp:Label>
                                                    </div>
                                                    <div class="col-md-10 d-flex align-items-center mb-3">
                                                        <asp:FileUpload ID="fuCertificado" runat="server" CssClass="form-control fileupload" accept=".pdf" />
                                                    </div>
                                                    <div class="col-md-2 d-flex align-items-center mb-3">
                                                        <asp:LinkButton ID="btnAgregarPdfCertificado" runat="server" CssClass="btn btn-success w-100 text-white" OnClick="btnAgregarPdfCertificado_Click" OnClientClick="mostrarLoaderPantalla();"><i class="fa-solid fa-plus"></i></asp:LinkButton>
                                                    </div>
                                                    <!-- Tabla para mostrar la información -->
                                                    <div class="mt-3 table-responsive">
                                                        <asp:GridView ID="gvListaCertificado" runat="server" CssClass="table table-sm table-striped table-hover" PageSize="4" AllowPaging="false" OnRowDataBound="gvListaCertificado_RowDataBound" OnRowCommand="gvListaCertificado_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEliminarCertificado" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <!-- Fin Tabla para mostrar la información -->
                                                </div>
                                            </div>
                                            <!-- Fin Controles -->
                                            <div class="modal-footer">
                                                <asp:LinkButton ID="btnGrabarCertificado" runat="server" CssClass="btn btn-secondary btnGris w-25" OnClick="btnGrabarCertificado_Click"><i class="fa-regular fa-floppy-disk"></i>&nbsp;Grabar</asp:LinkButton>
                                                <asp:LinkButton ID="btnCerrarCertificado" runat="server" CssClass="btn btn-danger btnRed w-25" OnClick="btnCerrarCertificado_Click"><i class="fa-solid fa-xmark"></i>&nbsp;Cerrar</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Modal -->
                                <!--Fin Certificados-->
                                <!-- Documentos -->
                                <div class="tab-pane fade show" id="pane-documentos" role="tabpanel" aria-labelledby="tab-documentos">
                                    <div class="row">
                                        <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-3"
                                            style="background: #f7f7f7; font-weight: 400; font-size: 1.5rem; border-bottom: 3px solid #000000;">
                                            <i style="color: #3498db;"></i>DOCUMENTOS
                                        </h4>
                                        <div class="col-md-3 form-floating align-items-end mb-0">
                                            <asp:DropDownList ID="ddlIdTipoArchivo" runat="server" CssClass="form-control textbox" placeholder="Tipo Archivo"></asp:DropDownList>
                                            <asp:Label ID="lblIdTipoArchivo" runat="server" AssociatedControlID="ddlIdTipoArchivo" CssClass="label" Text="Tipo Archivo"></asp:Label>
                                        </div>
                                        <div class="col-md-7 d-flex align-items-end mb-0">
                                            <asp:FileUpload ID="fuDocumento" runat="server" CssClass="form-control fileupload" accept=".pdf" />
                                        </div>
                                        <div class="col-md-2 form-floating d-flex align-items-end mb-0">
                                            <asp:Label ID="lblAgregarArchivo" AssociatedControlID="lbtnAgregarArchivo" runat="server" Text="" CssClass="text-light"></asp:Label>
                                            <asp:LinkButton ID="lbtnAgregarArchivo" runat="server"
                                                ToolTip="Agregar"
                                                CssClass="btn btn-success w-100 text-white"
                                                OnClick="lbtnAgregarArchivo_Click"
                                                OnClientClick="mostrarLoaderPantalla();"> 
                                                        <i class="fa fa-upload"></i>
                                            </asp:LinkButton>
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
                                        <div id="divAlertaCampos" class="alert alert-warning" runat="server" visible="false">
                                            <asp:Literal ID="ltlCamposFaltantes" runat="server"></asp:Literal>
                                        </div>
                                        <div id="divAlertaDocPesado" class="alert alert-danger " runat="server" visible="false">
                                            <asp:Label ID="lblMensajeAlerta" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Documentos -->
                            </div>
                        </div>
                        <div class="card-footer">
                            <asp:UpdatePanel ID="updFooter" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="footer-pane" data-pane="#pane-personal">
                                        <div class="row g-2">
                                            <div class="col-md-8"></div>
                                            <div class="col-md-4">
                                                <asp:LinkButton ID="btnGrabarPersonal" runat="server"
                                                    CssClass="btn btn-primary w-100"
                                                    OnClick="btnGrabarPersonal_Click">
                                                <i class="fa-regular fa-floppy-disk"></i>&nbsp;Grabar información personal
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="footer-pane d-none" data-pane="#pane-academico">
                                        <div class="row g-2">
                                            <div class="col-md-8"></div>
                                            <div class="col-md-4">
                                                <asp:LinkButton ID="btnGrabarAcademico" runat="server"
                                                    CssClass="btn btn-primary w-100"
                                                    OnClick="btnGrabarAcademico_Click">
                            <i class="fa-regular fa-floppy-disk"></i>&nbsp;Grabar información académica
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        let cropper;
        function cambiarATiempo(input) {
            if (input.type === 'time') {
                return;
            }
            input.type = 'time';
            if (input.value === '') {
                input.value = '00:00';
            }
            try {
                input.showPicker();
            } catch (e) {
                console.error("showPicker() falló", e);
            }
        }

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
                    cropper.destroy();
                    cropper = null;
                }
                // Limpiar el input para poder subir la misma imagen de nuevo
                const uploadInput = document.getElementById('uploadImage');
                if (uploadInput) {
                    uploadInput.value = '';
                }
            });
        });

        (function () {
            const KEY_TAB = "BT_Sustentante_Tab_Session";
            const KEY_FLAG = "BT_Sustentante_WasPostback";
            const defaultTabSelector = '#pane-personal';

            const tabEl = document.getElementById('perfilTabs');

            function showFooterFor(targetSelector) {
                const footerPanes = document.querySelectorAll('.card-footer .footer-pane');
                footerPanes.forEach(fp => {
                    fp.classList.toggle('d-none', fp.getAttribute('data-pane') !== targetSelector);
                });
            }

            function showTab(selector) {
                if (!tabEl) return; // Salir si no hay pestañas en esta página
                const trigger = tabEl.querySelector(`[data-bs-target="${selector}"]`);
                if (trigger) {
                    new bootstrap.Tab(trigger).show();
                } else {
                    const defaultTrigger = tabEl.querySelector(`[data-bs-target="${defaultTabSelector}"]`);
                    if (defaultTrigger) {
                        new bootstrap.Tab(defaultTrigger).show();
                    }
                }
            }

            const wasPostback = sessionStorage.getItem(KEY_FLAG);
            let activeTabSelector;

            if (wasPostback === "true") {
                sessionStorage.removeItem(KEY_FLAG);
                activeTabSelector = sessionStorage.getItem(KEY_TAB) || defaultTabSelector;
            } else {
                sessionStorage.removeItem(KEY_TAB);
                activeTabSelector = defaultTabSelector;
            }

            showTab(activeTabSelector);
            showFooterFor(activeTabSelector);

            // --- Listener de Clics (cuando el usuario cambia de pestaña) ---
            if (tabEl) {
                tabEl.addEventListener('shown.bs.tab', function (e) {
                    const pane = e.target.getAttribute('data-bs-target');
                    sessionStorage.setItem(KEY_TAB, pane);
                    showFooterFor(pane);
                });
            }
            function pageLoad() {

                // --- A. Lógica de TABS (la que ya tenías) ---
                const activeTabSelector = sessionStorage.getItem(KEY_TAB);
                if (activeTabSelector && tabEl) {
                    const trigger = document.querySelector(`[data-bs-target="${activeTabSelector}"]`);
                    if (trigger) {
                        const tab = bootstrap.Tab.getInstance(trigger) || new bootstrap.Tab(trigger);
                        tab.show();
                        showFooterFor(activeTabSelector);
                    }
                }

                const uploadInput = document.getElementById('uploadImage');
                if (uploadInput) {
                    uploadInput.addEventListener('change', uploader_handleFileChange);
                }
            }
            if (typeof (Sys) !== 'undefined' && Sys.Application) {
                Sys.Application.add_load(pageLoad);
            }

        })();

        var ingresoID = '#<%= ddlAnioIngreso.ClientID %>';
        var egresoID = '#<%= ddlAnioEgreso.ClientID %>';

        function filtrarIngreso() {
            var anioIngreso = parseInt($(ingresoID).val());
            var anioEgresoActual = parseInt($(egresoID).val());

            $(egresoID).find('option').prop('disabled', false);

            if (anioIngreso > 0) {
                $(egresoID).find('option').each(function () {
                    var anioOpcion = parseInt($(this).val());

                    if (anioOpcion > 0 && anioOpcion <= anioIngreso) {
                        $(this).prop('disabled', true);
                    }
                });
            }

            if (anioEgresoActual > 0 && anioEgresoActual <= anioIngreso) {
                $(egresoID).val('0').trigger('change.select2');
            }
        }

        function filtrarEgreso() {
            var anioIngresoActual = parseInt($(ingresoID).val());
            var anioEgreso = parseInt($(egresoID).val());

            $(ingresoID).find('option').prop('disabled', false);

            if (anioEgreso > 0) {
                $(ingresoID).find('option').each(function () {
                    var anioOpcion = parseInt($(this).val());

                    if (anioOpcion > 0 && anioOpcion >= anioEgreso) {
                        $(this).prop('disabled', true);
                    }
                });
            }
            if (anioIngresoActual > 0 && anioIngresoActual >= anioEgreso) {
                $(ingresoID).val('0').trigger('change.select2');
            }
        }

        function abrirModalLaboral() {
            var existingModal = bootstrap.Modal.getInstance(document.getElementById('ModalNuevo'));
            if (existingModal) {
                existingModal.hide();
                setTimeout(function () {
                    mostrarModalLaboralLimpio();
                }, 200);
            } else {
                mostrarModalLaboralLimpio();
            }
        }

        function mostrarModalLaboralLimpio() {
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open').css('padding-right', '');

            var myModal = new bootstrap.Modal(document.getElementById('ModalNuevo'), {
                keyboard: false,
                backdrop: 'static'
            });
            myModal.show();
        }

        function cerrarModalLaboral() {
            var myModal = bootstrap.Modal.getInstance(document.getElementById('ModalNuevo'));
            if (myModal) {
                myModal.hide();
            }
        }

        function abrirModalCertificado() {
            var existingModal2 = bootstrap.Modal.getInstance(document.getElementById('ModalNuevo2'));
            if (existingModal2) {
                existingModal2.hide();
                setTimeout(function () {
                    mostrarModalCertificadoLimpio();
                }, 200);
            } else {
                mostrarModalCertificadoLimpio();
            }
        }

        function mostrarModalCertificadoLimpio() {
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open').css('padding-right', '');

            var myModal = new bootstrap.Modal(document.getElementById('ModalNuevo2'), {
                keyboard: false,
                backdrop: 'static'
            });
            myModal.show();
        }

        function cerrarModalCertificado() {
            var myModal = bootstrap.Modal.getInstance(document.getElementById('ModalNuevo2'));
            if (myModal) {
                myModal.hide();
            }
        }

        function AllowOnlyLetters(e) {
            var charCode = (e.which) ? e.which : e.keyCode;

            if (
                (charCode > 64 && charCode < 91) ||  // A-Z
                (charCode > 96 && charCode < 123) || // a-z
                charCode == 8 ||   // Backspace
                charCode == 32 ||  // Espacio
                charCode == 241 || // ñ
                charCode == 209 || // Ñ
                // Vocales Minúsculas (á, é, í, ó, ú)
                charCode == 225 || charCode == 233 || charCode == 237 || charCode == 243 || charCode == 250 ||
                // Vocales Mayúsculas (Á, É, Í, Ó, Ú)
                charCode == 193 || charCode == 201 || charCode == 205 || charCode == 211 || charCode == 218
            ) {
                return true;
            }
            return false;
        }

        function AllowOnlyNumbers(e) {
            var charCode = (e.which) ? e.which : e.keyCode;


            if ((charCode >= 48 && charCode <= 57) || charCode == 8) {
                return true; // Permite el caracter
            }

            return false; // Bloquea todos los demás
        }

        function AllowAlphanumeric(e) {
            var charCode = (e.which) ? e.which : e.keyCode;

            if ((charCode >= 48 && charCode <= 57) ||  // Números
                (charCode >= 65 && charCode <= 90) ||  // Mayúsculas
                (charCode >= 97 && charCode <= 122) || // Minúsculas
                charCode == 8 ||   // Backspace
                charCode == 32 ||  // Espacio
                charCode == 241 || // ñ
                charCode == 209 || // Ñ
                // Vocales con acento (minúsculas: á, é, í, ó, ú)
                charCode == 225 || charCode == 233 || charCode == 237 || charCode == 243 || charCode == 250 ||
                // Vocales con acento (mayúsculas: Á, É, Í, Ó, Ú)
                charCode == 193 || charCode == 201 || charCode == 205 || charCode == 211 || charCode == 218
            ) {
                return true;
            }

            return false; // Bloquea todo lo demás
        }

        function aplicarValidacionesFechas() {

            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0');
            var yyyy = today.getFullYear();
            var todayString = yyyy + '-' + mm + '-' + dd;

            // Seleccionamos los elementos
            var $fechaInicio = $('#<%= txtFechaInicioLaboral.ClientID %>');
            var $fechaFin = $('#<%= txtFechaFinLaboral.ClientID %>');
            var $fechaEmision = $('#<%= txtFechaEmision.ClientID %>');

            // Re-aplicamos los atributos base (Max = Hoy)
            $fechaEmision.attr('max', todayString);
            $fechaInicio.attr('max', todayString);
            $fechaFin.attr('max', todayString);

            // Función auxiliar para sumar/restar días a un string YYYY-MM-DD
            function sumarDias(fechaString, dias) {
                var parts = fechaString.split('-');
                // Nota: Mes en JS es 0-11
                var fecha = new Date(parts[0], parts[1] - 1, parts[2]);
                fecha.setDate(fecha.getDate() + dias);

                var d = String(fecha.getDate()).padStart(2, '0');
                var m = String(fecha.getMonth() + 1).padStart(2, '0');
                var y = fecha.getFullYear();
                return y + '-' + m + '-' + d;
            }

            // --- EVENTO FECHA INICIO ---
            $fechaInicio.off('change').on('change', function () {
                var fechaInicioVal = $(this).val();

                if (fechaInicioVal) {
                    // REGLA: Fecha Fin debe ser MAYOR a Fecha Inicio.
                    // Por lo tanto, el mínimo de Fecha Fin es Inicio + 1 día.
                    var minFechaFin = sumarDias(fechaInicioVal, 1);

                    // Si el mínimo permitido para fin es mayor a HOY (que es el máximo global),
                    // entonces no existe un rango válido. Bloqueamos.
                    // (Esto cubre el caso: Inicio = Hoy -> MinFin = Mañana -> Bloqueado)
                    if (minFechaFin > todayString) {
                        $fechaFin.val('').prop('disabled', true).removeAttr('min');
                    }
                    else {
                        $fechaFin.prop('disabled', false);
                        $fechaFin.attr('min', minFechaFin);

                        // Validamos si el valor actual de fin rompe la regla
                        var fechaFinVal = $fechaFin.val();
                        if (fechaFinVal && fechaFinVal < minFechaFin) {
                            $fechaFin.val('');
                        }
                    }
                } else {
                    // Si limpian inicio, reseteamos fin
                    $fechaFin.prop('disabled', false).removeAttr('min');
                }
            });

            // --- EVENTO FECHA FIN ---
            $fechaFin.off('change').on('change', function () {
                var fechaFinVal = $(this).val();

                if (fechaFinVal) {
                    // REGLA: Fecha Inicio debe ser MENOR a Fecha Fin.
                    // Por lo tanto, el máximo de Fecha Inicio es Fin - 1 día.
                    var maxFechaInicio = sumarDias(fechaFinVal, -1);

                    // Aplicamos el max dinámico
                    $fechaInicio.attr('max', maxFechaInicio);

                    // Validamos si el valor actual de inicio rompe la regla
                    var fechaInicioVal = $fechaInicio.val();
                    if (fechaInicioVal && fechaInicioVal > maxFechaInicio) {
                        $fechaInicio.val('');
                    }
                } else {
                    // Si limpian fin, el max de inicio vuelve a ser HOY
                    $fechaInicio.attr('max', todayString);
                }
            });

            // --- VALIDACIÓN INICIAL ---
            // Si al cargar ya hay una fecha inicio, corremos la validación
            // para bloquear o setear el min correspondiente.
            if ($fechaInicio.val()) {
                $fechaInicio.trigger('change');
            }
            if ($fechaFin.val()) {
                $fechaFin.trigger('change');
            }
        }

        // 2. Llamada inicial
        $(document).ready(function () {
            aplicarValidacionesFechas();
        });

        // 3. Llamada post-UpdatePanel
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            aplicarValidacionesFechas();
        });

        function checkMaxValue(input) {
            if (parseInt(input.value) > 100) {
                input.value = 100; // Si escribe más de 100, lo regresa a 100
            }
            if (parseInt(input.value) < 0) {
                input.value = 0; // Si escribe más de 100, lo regresa a 100
            }
        }

        window.onload = function () {
            var hoy = new Date();

            var hace14Anios = new Date(hoy.getFullYear() - 15, hoy.getMonth(), hoy.getDate());

            var dd = String(hace14Anios.getDate()).padStart(2, '0');
            var mm = String(hace14Anios.getMonth() + 1).padStart(2, '0'); // Enero es 0
            var yyyy = hace14Anios.getFullYear();

            var fechaMax = yyyy + '-' + mm + '-' + dd;

            var inputFecha = document.getElementById('<%= txtFechaNacimiento.ClientID %>');
            if (inputFecha) {
                inputFecha.setAttribute("max", fechaMax);
            }
        };
        function mostrarLoaderPantalla() {
            // 1. Tu lógica de sesión original
            sessionStorage.setItem('BT_Sustentante_WasPostback', 'true');

            // 2. Mostrar el overlay cambiándole el display a 'flex' (para que centre)
            document.getElementById('overlayCarga').style.display = 'flex';

            return true; // Deja que continúe el evento del servidor
        }

        function validarPromedio(input) {
            // 1. Validar que no sea mayor a 100
            if (parseFloat(input.value) > 100) {
                input.value = 100;
            }

            // 2. Limitar a máximo 2 decimales (sin borrar el punto mientras escribes)
            if (input.value.includes('.')) {
                let partes = input.value.split('.');

                // Si la parte decimal tiene más de 2 dígitos, la cortamos
                if (partes[1].length > 2) {
                    input.value = partes[0] + '.' + partes[1].substring(0, 2);
                }
            }
        }

        function validarExperiencia() {
            // Obtener los elementos usando el ClientID de ASP.NET
            var txtAnios = document.getElementById('<%= txtAniosExperiencia.ClientID %>');
            var txtMeses = document.getElementById('<%= txtMesesExperiencia.ClientID %>');

            // Convertir los valores a enteros (si están vacíos, los tratamos como 0)
            var anios = parseInt(txtAnios.value) || 0;
            var meses = parseInt(txtMeses.value);

            if (anios === 0) {
                // Si hay 0 años, el mínimo de meses debe ser 1
                txtMeses.setAttribute('min', '1');

                // Si el usuario escribió un 0, lo forzamos a 1
                if (meses === 0) {
                    txtMeses.value = '1';
                }
            } else {
                // Si hay 1 o más años, sí permitimos 0 meses
                txtMeses.setAttribute('min', '0');
            }
        }
    </script>
</asp:Content>
