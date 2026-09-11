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
                                        <div class="col-lg-10 ps-5 pe-5">

                                            <!-- SECCIÓN EMPRESA -->
                                            <h3 class="card-header d-flex align-items-center text-dark fw-bold py-3 mb-4 border-bottom">
                                                <i class="fa-solid fa-building me-2 text-primary"></i>EMPRESA
                                            </h3>

                                            <div class="row g-3 mb-5">
                                                <div class="col-md-3">
                                                    <div class="form-floating">
                                                        <asp:TextBox ID="txtSolicitudNo" runat="server" Enabled="false" CssClass="form-control" placeholder="Solicitud No."></asp:TextBox>
                                                        <asp:Label ID="lblSolicitudNo" runat="server" AssociatedControlID="txtSolicitudNo" Text="Solicitud No."></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-9">
                                                    <div class="row g-3">
                                                        <div class="col-md-6">
                                                            <div class="form-floating">
                                                                <asp:TextBox ID="txtNombreEmpresa" runat="server" Enabled="false" CssClass="form-control" placeholder="Nombre de la Empresa"></asp:TextBox>
                                                                <asp:Label ID="lblNombreEmpresa" runat="server" AssociatedControlID="txtNombreEmpresa" Text="Nombre de la Empresa"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-floating">
                                                                <asp:TextBox ID="txtNombreyPuestoContacto" runat="server" Enabled="false" CssClass="form-control" placeholder="Nombre y Puesto del Contacto"></asp:TextBox>
                                                                <asp:Label ID="lblNombreyPuestoContacto" runat="server" AssociatedControlID="txtNombreyPuestoContacto" Text="Nombre y Puesto del Contacto"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- SECCIÓN VACANTE -->
                                            <h3 class="card-header d-flex align-items-center text-dark fw-bold py-3 mb-4 border-bottom">
                                                <i class="fa-solid fa-briefcase me-2 text-primary"></i>VACANTE
                                            </h3>

                                            <!-- Nombre del Puesto -->
                                            <div class="col-md-12 mb-4">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="txtNombrePuesto" runat="server" CssClass="form-control" MaxLength="255" placeholder="Nombre del Puesto"></asp:TextBox>
                                                    <asp:Label ID="lblNombrePuesto" runat="server" AssociatedControlID="txtNombrePuesto" Text="Nombre del Puesto"></asp:Label>
                                                </div>
                                            </div>

                                            <!-- Dirección -->
                                            <div class="col-md-12 mb-4">
                                                <div class="form-floating mb-2">
                                                    <asp:TextBox ID="txtDirección" runat="server" CssClass="form-control" MaxLength="255" placeholder="Dirección"></asp:TextBox>
                                                    <asp:Label ID="lblDirección" runat="server" AssociatedControlID="txtDirección" Text="Dirección"></asp:Label>
                                                </div>
                                                <div class="form-check ms-1" id="divUsarMismaDireccion" runat="server">
                                                    <input type="checkbox" class="form-check-input" id="chkUsarMismaDireccion" runat="server" clientidmode="Static" />
                                                    <label class="form-check-label text-muted" for="chkUsarMismaDireccion">Usar misma dirección que la Empresa</label>
                                                </div>
                                                <asp:HiddenField ID="hfDireccionEmpresa" runat="server" ClientIDMode="Static" />
                                            </div>

                                            <!-- # Vacantes -->
                                            <div class="col-md-12 mb-4">
                                                <div class="form-floating">
                                                    <asp:TextBox ID="txtNumVacantes" runat="server" TextMode="Number" CssClass="form-control" min="1" max="999" step="1" placeholder="Número de Vacantes" MaxLength="3" oninput="this.value = this.value.replace(/[^0-9]/g, '').slice(0, this.maxLength);" onkeydown="return event.key != '-' &amp;&amp; event.key != '+' &amp;&amp; event.key != '.' &amp;&amp; event.key != ',' &amp;&amp; event.key != 'e' &amp;&amp; event.key != 'E';"></asp:TextBox>
                                                    <asp:Label ID="lblNumVacantes" runat="server" AssociatedControlID="txtNumVacantes" Text="Número de Vacantes"></asp:Label>
                                                </div>
                                            </div>

                                            <!-- Carrera -->
                                            <div class="col-md-12 mb-4">
                                                <fieldset class="border rounded-3 p-4 bg-light shadow-sm">
                                                    <legend class="float-none w-auto px-3 fs-6 fw-bold text-primary">Carrera</legend>

                                                    <div class="row g-2 align-items-center">
                                                        <div class="col-md-8">
                                                            <div class="form-floating">
                                                                <asp:DropDownList ID="ddlIdCarrera" runat="server" CssClass="form-select"></asp:DropDownList>
                                                                <asp:Label ID="lblIdCarrera" runat="server" AssociatedControlID="ddlIdCarrera" Text="Seleccione Carrera"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-4">
                                                            <asp:LinkButton ID="lbtnAgregarCarrera" runat="server" CssClass="btn btn-success w-100 h-100 py-3 text-white fw-bold shadow-sm" ToolTip="Agregar" OnClick="lbtnAgregarCarrera_Click">
                        <i class="fa-solid fa-plus me-2"></i>AGREGAR
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                    <div class="table-responsive mt-3">
                                                        <asp:GridView ID="gvConsultaCarrera" runat="server" CssClass="table table-sm table-striped table-hover bg-white border" AutoGenerateColumns="true" OnRowCommand="gvConsultaCarrera_RowCommand" OnRowDataBound="gvConsultaCarrera_RowDataBound">
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
                                                </fieldset>
                                            </div>
                                            <!-- Fin Carrera -->



                                            <div class="row mb-4 align-items-center">
                                                <!-- Contenedor agrupado para los checkboxes de Estudiante/Egresado -->
                                                <div class="col-md-12 mb-4">
                                                    <div class="p-3 border rounded-3 bg-white shadow-sm d-flex align-items-center gap-4">
                                                        <span class="fw-bold text-secondary me-3">Estado del candidato:</span>
                                                        <div class="form-check form-switch m-0">
                                                            <input type="checkbox" class="form-check-input" id="chkEstudiante" runat="server" clientidmode="Static" />
                                                            <label class="form-check-label" for="chkEstudiante">Estudiante</label>
                                                        </div>
                                                        <div class="form-check form-switch m-0">
                                                            <input type="checkbox" class="form-check-input" id="chkEgresado" runat="server" clientidmode="Static" />
                                                            <label class="form-check-label" for="chkEgresado">Egresado</label>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>

                                            <!-- Sección Semestre (se oculta/muestra por JS según "Estudiante") -->
                                            <div class="col-md-12 mb-4" id="seccionSemestre">
                                                <fieldset class="border rounded-3 p-4 bg-light shadow-sm">
                                                    <legend class="float-none w-auto px-3 fs-6 fw-bold text-primary">Semestre</legend>
                                                    <div class="row g-2 align-items-center">
                                                        <div class="col-md-8">
                                                            <div class="form-floating">
                                                                <asp:DropDownList ID="ddlIdSemestre" runat="server" CssClass="form-select"></asp:DropDownList>
                                                                <asp:Label ID="lblIdSemestre" runat="server" AssociatedControlID="ddlIdSemestre" Text="Seleccione Semestre"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-4">
                                                            <asp:LinkButton ID="lbtnAgregarSemestre" runat="server" CssClass="btn btn-success w-100 h-100 py-3 text-white fw-bold shadow-sm" ToolTip="Agregar" OnClick="lbtnAgregarSemestre_Click">
                        <i class="fa-solid fa-plus me-2"></i>AGREGAR
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                    <div class="table-responsive mt-3">
                                                        <asp:GridView ID="gvConsultaSemestre" runat="server" CssClass="table table-sm table-striped table-hover bg-white border" AutoGenerateColumns="true" OnRowCommand="gvConsultaSemestre_RowCommand" OnRowDataBound="gvConsultaSemestre_RowDataBound">
                                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                            <PagerStyle CssClass="custom-pager" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEliminarSemestre" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </div>
                                            <!-- Fin Semestre -->

                                            <!-- Experiencia -->
                                            <div class="col-md-12 mb-4">
                                                <fieldset class="border rounded-3 p-4 bg-light shadow-sm">
                                                    <legend class="float-none w-auto px-3 fs-6 fw-bold text-primary">Experiencia</legend>

                                                    <div class="row g-2 align-items-center">
                                                        <div class="col-md-6">
                                                            <div class="form-floating">
                                                                <asp:DropDownList ID="ddlIdAreaExperiencia" runat="server" CssClass="form-select select2-custom"></asp:DropDownList>
                                                                <asp:Label ID="lblIdAreaExperiencia" runat="server" AssociatedControlID="ddlIdAreaExperiencia" Text="Área"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <div class="form-floating">
                                                                <asp:TextBox ID="txtAniosExperiencia" runat="server" CssClass="form-control" TextMode="Number" min="0" max="99" step="1" placeholder="Años" MaxLength="2" oninput="if(this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength); validarExperiencia();" onkeydown="return event.key != '.' && event.key != ',' && event.key != 'e'"></asp:TextBox>
                                                                <asp:Label ID="lblAniosExperiencia" runat="server" AssociatedControlID="txtAniosExperiencia" Text="Años"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <div class="form-floating">
                                                                <asp:TextBox ID="txtMesesExperiencia" runat="server" CssClass="form-control" TextMode="Number" min="0" max="11" step="1" placeholder="Meses" MaxLength="2" oninput="if(parseInt(this.value) > 11) this.value = 11; if(this.value.length > 2) this.value = this.value.slice(0, 2); validarExperiencia();" onkeydown="return event.key != '.' && event.key != ',' && event.key != 'e' && event.key != '-'"></asp:TextBox>
                                                                <asp:Label ID="lblMesesExperiencia" runat="server" AssociatedControlID="txtMesesExperiencia" Text="Meses"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <asp:LinkButton ID="lbtnAgregarAreaExperiencia" runat="server" CssClass="btn btn-success w-100 h-100 py-3 text-white fw-bold shadow-sm" ToolTip="Agregar" OnClick="lbtnAgregarAreaExperiencia_Click">
                        <i class="fa-solid fa-plus me-2"></i>AGREGAR
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                    <div class="table-responsive mt-3">
                                                        <asp:GridView ID="gvConsultaExperiencia" runat="server" CssClass="table table-sm table-striped table-hover bg-white border" AutoGenerateColumns="true" OnRowCommand="gvConsultaExperiencia_RowCommand" OnRowDataBound="gvConsultaExperiencia_RowDataBound">
                                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                            <PagerStyle CssClass="custom-pager" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEliminarExperiencia" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </div>
                                            <!-- Fin Experiencia -->

                                            <!-- Sexo -->
                                            <div class="col-md-12 mb-4">
                                                <div class="form-floating">
                                                    <asp:DropDownList ID="ddlIdGenero" runat="server" CssClass="form-select" placeholder="Sexo"></asp:DropDownList>
                                                    <asp:Label ID="lblGenero" runat="server" AssociatedControlID="ddlIdGenero" Text="Sexo"></asp:Label>
                                                </div>
                                            </div>

                                            <!-- Municipio -->
                                            <div class="col-md-12 mb-4">
                                                <fieldset class="border rounded-3 p-4 bg-light shadow-sm">
                                                    <legend class="float-none w-auto px-3 fs-6 fw-bold text-primary">Municipio</legend>

                                                    <div class="row g-2 align-items-center">
                                                        <div class="col-md-8">
                                                            <div class="form-floating">
                                                                <asp:DropDownList ID="ddlIdMunicipio" runat="server" CssClass="form-select"></asp:DropDownList>
                                                                <asp:Label ID="lblIdMunicipio" runat="server" AssociatedControlID="ddlIdMunicipio" Text="Seleccione Municipio"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-4">
                                                            <asp:LinkButton ID="lbtnAgregarMunicipio" runat="server" CssClass="btn btn-success w-100 h-100 py-3 text-white fw-bold shadow-sm" ToolTip="Agregar" OnClick="lbtnAgregarMunicipio_Click">
<i class="fa-solid fa-plus me-2"></i>AGREGAR
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                            <div class="table-responsive mt-3">
                                <asp:GridView ID="gvConsultaMunicipio" runat="server" CssClass="table table-sm table-striped table-hover bg-white border" AutoGenerateColumns="true" OnRowCommand="gvConsultaMunicipio_RowCommand" OnRowDataBound="gvConsultaMunicipio_RowDataBound">
                                    <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                    <PagerStyle CssClass="custom-pager" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEliminarMunicipio" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                                                </fieldset>
                                            </div>
                                            <!-- Fin Municipio -->

                                            <!-- Rango de Edad -->
                                            <div class="col-md-12 mb-4">
                                                <div class="border rounded-3 p-3 bg-white shadow-sm h-100">
                                                    <label class="form-label fw-bold text-secondary mb-3">Rango de Edad</label>
                                                    <div class="range-slider-container position-relative mt-2">
                                                        <div class="d-flex justify-content-between mb-2">
                                                            <div class="slider-value badge" id="lblMin">15</div>
                                                            <div class="slider-value badge" id="lblMax">70</div>
                                                        </div>

                                                        <div class="slider-track-bg"></div>
                                                        <div class="slider-progress" id="sliderProgress"></div>

                                                        <input type="range" id="rangoMin" runat="server" clientidmode="Static" min="15" max="70" value="20" class="w-100">
                                                        <input type="range" id="rangoMax" runat="server" clientidmode="Static" min="15" max="70" value="60" class="w-100 position-absolute top-50 start-0 translate-middle-y">

                                                        <asp:HiddenField ID="hfEdadMinima" runat="server" Value="15" />
                                                        <asp:HiddenField ID="hfEdadMaxima" runat="server" Value="70" />
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- Actividades -->
                                            <div class="col-md-12 mb-4">
                                                <fieldset class="border rounded-3 p-4 bg-light shadow-sm">
                                                    <legend class="float-none w-auto px-3 fs-6 fw-bold text-primary">Actividades</legend>
                                                    <div class="row g-2 align-items-center">
                                                        <div class="col-md-10">
                                                            <div class="form-floating">
                                                                <asp:TextBox ID="txtActividad" runat="server" CssClass="form-control" placeholder="Actividades y/o funciones"></asp:TextBox>
                                                                <asp:Label ID="lblActividad" runat="server" AssociatedControlID="txtActividad" Text="Actividades y/o funciones"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:LinkButton ID="lbntAgregarActividad" runat="server" CssClass="btn btn-success w-100 h-100 py-3 text-white fw-bold shadow-sm" ToolTip="Agregar" OnClick="lbntAgregarActividad_Click">
                        <i class="fa-solid fa-plus me-2"></i>AGREGAR
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                    <div class="table-responsive mt-3">
                                                        <asp:GridView ID="gvConsultaActividades" runat="server" CssClass="table table-sm table-striped table-hover bg-white border" AutoGenerateColumns="true" OnRowCommand="gvConsultaActividades_RowCommand" OnRowDataBound="gvConsultaActividades_RowDataBound">
                                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                            <PagerStyle CssClass="custom-pager" />
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEliminarActividades" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' OnClientClick="return showConfirmation(this);"><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </fieldset>
                                            </div>
                                            <!-- Fin Actividades -->

                                            <!-- Horarios -->
                                            <div class="col-md-12 mb-4">
                                                <div class="row g-3">
                                                    <div class="col-md-4">
                                                        <div class="form-floating">
                                                            <asp:DropDownList ID="ddlIdTiempoDisponible" runat="server" CssClass="form-select" placeholder="Horario"></asp:DropDownList>
                                                            <asp:Label ID="lblTipoTurno" runat="server" AssociatedControlID="ddlIdTiempoDisponible" Text="Horario"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-floating">
                                                            <asp:DropDownList ID="ddlHoraInicio" runat="server" CssClass="form-select">
                                                                <asp:ListItem Value="" Text="Seleccione..."></asp:ListItem>
                                                                <asp:ListItem Value="07:00" Text="07:00 AM"></asp:ListItem>
                                                                <asp:ListItem Value="07:15" Text="07:15 AM"></asp:ListItem>
                                                                <asp:ListItem Value="07:30" Text="07:30 AM"></asp:ListItem>
                                                                <asp:ListItem Value="07:45" Text="07:45 AM"></asp:ListItem>
                                                                <asp:ListItem Value="08:00" Text="08:00 AM"></asp:ListItem>
                                                                <asp:ListItem Value="08:15" Text="08:15 AM"></asp:ListItem>
                                                                <asp:ListItem Value="08:30" Text="08:30 AM"></asp:ListItem>
                                                                <asp:ListItem Value="08:45" Text="08:45 AM"></asp:ListItem>
                                                                <asp:ListItem Value="09:00" Text="09:00 AM"></asp:ListItem>
                                                                <asp:ListItem Value="09:15" Text="09:15 AM"></asp:ListItem>
                                                                <asp:ListItem Value="09:30" Text="09:30 AM"></asp:ListItem>
                                                                <asp:ListItem Value="09:45" Text="09:45 AM"></asp:ListItem>
                                                                <asp:ListItem Value="10:00" Text="10:00 AM"></asp:ListItem>
                                                                <asp:ListItem Value="10:15" Text="10:15 AM"></asp:ListItem>
                                                                <asp:ListItem Value="10:30" Text="10:30 AM"></asp:ListItem>
                                                                <asp:ListItem Value="10:45" Text="10:45 AM"></asp:ListItem>
                                                                <asp:ListItem Value="11:00" Text="11:00 AM"></asp:ListItem>
                                                                <asp:ListItem Value="11:15" Text="11:15 AM"></asp:ListItem>
                                                                <asp:ListItem Value="11:30" Text="11:30 AM"></asp:ListItem>
                                                                <asp:ListItem Value="11:45" Text="11:45 AM"></asp:ListItem>
                                                                <asp:ListItem Value="12:00" Text="12:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="12:15" Text="12:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="12:30" Text="12:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="12:45" Text="12:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="13:00" Text="13:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="13:15" Text="13:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="13:30" Text="13:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="13:45" Text="13:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="14:00" Text="14:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="14:15" Text="14:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="14:30" Text="14:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="14:45" Text="14:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="15:00" Text="15:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="15:15" Text="15:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="15:30" Text="15:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="15:45" Text="15:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="16:00" Text="16:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="16:15" Text="16:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="16:30" Text="16:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="16:45" Text="16:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="17:00" Text="17:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="17:15" Text="17:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="17:30" Text="17:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="17:45" Text="17:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="18:00" Text="18:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="18:15" Text="18:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="18:30" Text="18:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="18:45" Text="18:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="19:00" Text="19:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="19:15" Text="19:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="19:30" Text="19:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="19:45" Text="19:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="20:00" Text="20:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="20:15" Text="20:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="20:30" Text="20:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="20:45" Text="20:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="21:00" Text="21:00 PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblHoraInicio" runat="server" AssociatedControlID="ddlHoraInicio" Text="Hora de Inicio"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-floating">
                                                            <asp:DropDownList ID="ddlHoraFin" runat="server" CssClass="form-select">
                                                                <asp:ListItem Value="" Text="Seleccione..."></asp:ListItem>
                                                                <asp:ListItem Value="07:00" Text="07:00 AM"></asp:ListItem>
                                                                <asp:ListItem Value="07:15" Text="07:15 AM"></asp:ListItem>
                                                                <asp:ListItem Value="07:30" Text="07:30 AM"></asp:ListItem>
                                                                <asp:ListItem Value="07:45" Text="07:45 AM"></asp:ListItem>
                                                                <asp:ListItem Value="08:00" Text="08:00 AM"></asp:ListItem>
                                                                <asp:ListItem Value="08:15" Text="08:15 AM"></asp:ListItem>
                                                                <asp:ListItem Value="08:30" Text="08:30 AM"></asp:ListItem>
                                                                <asp:ListItem Value="08:45" Text="08:45 AM"></asp:ListItem>
                                                                <asp:ListItem Value="09:00" Text="09:00 AM"></asp:ListItem>
                                                                <asp:ListItem Value="09:15" Text="09:15 AM"></asp:ListItem>
                                                                <asp:ListItem Value="09:30" Text="09:30 AM"></asp:ListItem>
                                                                <asp:ListItem Value="09:45" Text="09:45 AM"></asp:ListItem>
                                                                <asp:ListItem Value="10:00" Text="10:00 AM"></asp:ListItem>
                                                                <asp:ListItem Value="10:15" Text="10:15 AM"></asp:ListItem>
                                                                <asp:ListItem Value="10:30" Text="10:30 AM"></asp:ListItem>
                                                                <asp:ListItem Value="10:45" Text="10:45 AM"></asp:ListItem>
                                                                <asp:ListItem Value="11:00" Text="11:00 AM"></asp:ListItem>
                                                                <asp:ListItem Value="11:15" Text="11:15 AM"></asp:ListItem>
                                                                <asp:ListItem Value="11:30" Text="11:30 AM"></asp:ListItem>
                                                                <asp:ListItem Value="11:45" Text="11:45 AM"></asp:ListItem>
                                                                <asp:ListItem Value="12:00" Text="12:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="12:15" Text="12:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="12:30" Text="12:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="12:45" Text="12:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="13:00" Text="13:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="13:15" Text="13:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="13:30" Text="13:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="13:45" Text="13:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="14:00" Text="14:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="14:15" Text="14:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="14:30" Text="14:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="14:45" Text="14:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="15:00" Text="15:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="15:15" Text="15:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="15:30" Text="15:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="15:45" Text="15:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="16:00" Text="16:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="16:15" Text="16:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="16:30" Text="16:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="16:45" Text="16:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="17:00" Text="17:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="17:15" Text="17:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="17:30" Text="17:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="17:45" Text="17:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="18:00" Text="18:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="18:15" Text="18:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="18:30" Text="18:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="18:45" Text="18:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="19:00" Text="19:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="19:15" Text="19:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="19:30" Text="19:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="19:45" Text="19:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="20:00" Text="20:00 PM"></asp:ListItem>
                                                                <asp:ListItem Value="20:15" Text="20:15 PM"></asp:ListItem>
                                                                <asp:ListItem Value="20:30" Text="20:30 PM"></asp:ListItem>
                                                                <asp:ListItem Value="20:45" Text="20:45 PM"></asp:ListItem>
                                                                <asp:ListItem Value="21:00" Text="21:00 PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblHoraFin" runat="server" AssociatedControlID="ddlHoraFin" Text="Hora de Fin"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- Rango de Sueldo y Lugar de trabajo -->
                                            <div class="col-md-12 mb-4">
                                                <div class="border rounded-3 p-3 bg-white shadow-sm h-100">
                                                    <label class="form-label fw-bold text-secondary mb-3">Rango de Sueldo</label>
                                                    <div class="range-slider-container position-relative mt-2">
                                                        <div class="d-flex justify-content-between mb-2">
                                                            <div class="slider-value badge" id="lblSueldoMin">$10,000</div>
                                                            <div class="slider-value badge" id="lblSueldoMax">$50,000</div>
                                                        </div>

                                                        <div class="slider-track-bg"></div>
                                                        <div class="slider-progress" id="sliderProgressSueldo"></div>

                                                        <input type="range" id="rangoSueldoMin" runat="server" clientidmode="Static" min="0" max="100000" step="1000" value="10000" class="w-100">
                                                        <input type="range" id="rangoSueldoMax" runat="server" clientidmode="Static" min="0" max="100000" step="1000" value="50000" class="w-100 position-absolute top-50 start-0 translate-middle-y">

                                                        <asp:HiddenField ID="hfSueldoMinimo" runat="server" Value="10000" />
                                                        <asp:HiddenField ID="hfSueldoMaximo" runat="server" Value="50000" />
                                                    </div>

                                                    <div class="form-check form-switch mt-4 ms-1">
                                                        <input type="checkbox" class="form-check-input" id="chkMostrarSueldo" runat="server" clientidmode="Static" />
                                                        <label class="form-check-label text-muted" for="chkMostrarSueldo">Mostrar el sueldo a los candidatos</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 mb-4">
                                                <div class="form-floating h-100">
                                                    <asp:TextBox ID="txtLugarTrabajo" runat="server" CssClass="form-control h-100" MaxLength="55" placeholder="Lugar de Trabajo"></asp:TextBox>
                                                    <asp:Label ID="lblLugarTrabajo" runat="server" AssociatedControlID="txtLugarTrabajo" Text="Lugar de Trabajo"></asp:Label>
                                                </div>
                                            </div>

                                            <!-- Idioma -->
                                            <div class="col-md-12 mb-4">
                                                <fieldset class="border rounded-3 p-4 bg-light shadow-sm">
                                                    <legend class="float-none w-auto px-3 fs-6 fw-bold text-primary">Idioma</legend>
                                                    <div class="row g-2 align-items-center">
                                                        <div class="col-md-6">
                                                            <div class="form-floating">
                                                                <asp:DropDownList ID="ddlIdIdioma" runat="server" CssClass="form-select" placeholder="Idioma"></asp:DropDownList>
                                                                <asp:Label ID="lblIdIdioma" runat="server" AssociatedControlID="ddlIdIdioma" Text="Seleccione Idioma"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-floating">
                                                                <asp:DropDownList ID="ddlIdNivelIdioma" runat="server" CssClass="form-select" placeholder="Nivel"></asp:DropDownList>
                                                                <asp:Label ID="lblIdNivelIdioma" runat="server" AssociatedControlID="ddlIdNivelIdioma" Text="Nivel"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:LinkButton ID="lbtnAgregarIdioma" runat="server" CssClass="btn btn-success w-100 h-100 py-3 text-white fw-bold shadow-sm" ToolTip="Agregar" OnClick="lbtnAgregarIdioma_Click">
                        <i class="fa-solid fa-plus me-2"></i>AGREGAR
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>

                                                    <div class="table-responsive mt-3">
                                                        <asp:GridView ID="gvConsultaIdioma" runat="server" CssClass="table table-sm table-striped table-hover bg-white border" AutoGenerateColumns="true" OnRowCommand="gvConsultaIdioma_RowCommand" OnRowDataBound="gvConsultaIdioma_RowDataBound">
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
                                                </fieldset>
                                            </div>
                                            <!-- Fin Idioma -->

                                            <!-- Checkbox de Aviso de Privacidad -->
                                            <div class="col-12 mt-4 mb-5">
                                                <div class="p-4 border rounded-3 bg-light shadow-sm text-center">
                                                    <div class="form-check d-inline-block text-start">
                                                        <input type="checkbox" class="form-check-input" id="chkAvisoPrivacidad" runat="server" />
                                                        <label class="form-check-label ms-2" for="chkAvisoPrivacidad">
                                                            He leído y acepto el 
                    <a href="#" data-bs-toggle="modal" data-bs-target="#modalAvisoPrivacidad" class="text-primary fw-bold text-decoration-none">Aviso de Privacidad</a> *
                                                        </label>
                                                    </div>
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
                                        <asp:LinkButton ID="btnGrabar" runat="server" CssClass="btn btn-primary w-100" OnClick="btnGrabar_Click">
                         <i class="fa-regular fa-floppy-disk"></i>&nbsp;Grabar
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnVolver" runat="server" CssClass="btn btn-secondary w-100" OnClick="btnVolver_Click" Visible="false">
                         <i class="fa-solid fa-arrow-left"></i>&nbsp;Volver
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

        document.addEventListener("DOMContentLoaded", function () {
            const rangoMin = document.getElementById('rangoMin');
            const rangoMax = document.getElementById('rangoMax');
            const progreso = document.getElementById('sliderProgress');
            const lblMin = document.getElementById('lblMin');
            const lblMax = document.getElementById('lblMax');

            // Controles de ASP.NET
            const hfMin = document.getElementById('<%= hfEdadMinima.ClientID %>') || document.getElementById('hfEdadMinima');
            const hfMax = document.getElementById('<%= hfEdadMaxima.ClientID %>') || document.getElementById('hfEdadMaxima');

            function actualizarSlider(e) {
                let minVal = parseInt(rangoMin.value);
                let maxVal = parseInt(rangoMax.value);
                const minGap = 1;

                // Leer los límites reales del input (15 y 70)
                const sliderMin = parseInt(rangoMin.min);
                const sliderMax = parseInt(rangoMin.max);

                // Evitar que el mínimo pase al máximo y viceversa
                if (maxVal - minVal <= minGap) {
                    if (e && e.target.id === 'rangoMin') {
                        rangoMin.value = maxVal - minGap;
                        minVal = parseInt(rangoMin.value);
                    } else {
                        rangoMax.value = minVal + minGap;
                        maxVal = parseInt(rangoMax.value);
                    }
                }

                // NUEVA FÓRMULA: Calcula el porcentaje tomando en cuenta que arrancamos en 15
                const minPercent = ((minVal - sliderMin) / (sliderMax - sliderMin)) * 100;
                const maxPercent = ((maxVal - sliderMin) / (sliderMax - sliderMin)) * 100;

                // Mover la barra azul de progreso
                progreso.style.left = minPercent + "%";
                progreso.style.width = (maxPercent - minPercent) + "%";

                // Mover las etiquetas numéricas
                lblMin.style.left = minPercent + "%";
                lblMin.textContent = minVal;

                lblMax.style.left = maxPercent + "%";
                lblMax.textContent = maxVal;

                // Guardar valores para que tu servidor ASP.NET los pueda leer
                if (hfMin) hfMin.value = minVal;
                if (hfMax) hfMax.value = maxVal;
            }

            // Escuchar los eventos
            rangoMin.addEventListener('input', actualizarSlider);
            rangoMax.addEventListener('input', actualizarSlider);

            // Inicializar al cargar la página
            actualizarSlider();
        });

        document.addEventListener("DOMContentLoaded", function () {
            // Capturamos los controles generados por ASP.NET
            // Usamos selectores flexibles por si ASP.NET cambia el ID final
            const ddlInicio = document.querySelector('select[id$="ddlHoraInicio"]');
            const ddlFin = document.querySelector('select[id$="ddlHoraFin"]');

            function validarHorarios(evento) {
                const valInicio = ddlInicio.value;
                const valFin = ddlFin.value;

                // 1. Bloquear opciones incorrectas en "Hora de Fin"
                Array.from(ddlFin.options).forEach(option => {
                    // Ignorar la opción vacía "Seleccione..."
                    if (option.value === "") return;

                    // Si la opción de Fin es MENOR o IGUAL a la de Inicio, la deshabilitamos
                    if (valInicio && option.value <= valInicio) {
                        option.disabled = true;
                    } else {
                        option.disabled = false;
                    }
                });

                // 2. Bloquear opciones incorrectas en "Hora de Inicio"
                Array.from(ddlInicio.options).forEach(option => {
                    if (option.value === "") return;

                    // Si la opción de Inicio es MAYOR o IGUAL a la de Fin, la deshabilitamos
                    if (valFin && option.value >= valFin) {
                        option.disabled = true;
                    } else {
                        option.disabled = false;
                    }
                });

                // 3. Prevención de errores al cambiar de golpe
                // Si el usuario cambia el inicio y provoca un cruce (ej. Inicio > Fin), borramos el fin
                if (valInicio && valFin && valInicio >= valFin) {
                    if (evento.target === ddlInicio) ddlFin.value = "";
                    if (evento.target === ddlFin) ddlInicio.value = "";
                }
            }

            // Escuchar cada vez que el usuario cambia una opción
            if (ddlInicio && ddlFin) {
                ddlInicio.addEventListener('change', validarHorarios);
                ddlFin.addEventListener('change', validarHorarios);

                // Ejecutar una vez al inicio por si la página carga con datos pre-llenados
                validarHorarios();
            }
        });

        document.addEventListener("DOMContentLoaded", function () {
            // 1. Obtener elementos (Revisa que estos IDs coincidan EXACTAMENTE con tu HTML)
            const rangoMin = document.getElementById('rangoSueldoMin');
            const rangoMax = document.getElementById('rangoSueldoMax');
            const lblMin = document.getElementById('lblSueldoMin');
            const lblMax = document.getElementById('lblSueldoMax');
            const progress = document.getElementById('sliderProgressSueldo');

            // Si falta algún elemento, el código se detiene aquí y te avisa en la consola del navegador
            if (!rangoMin || !rangoMax || !lblMin || !lblMax || !progress) {
                console.error("Error: Faltan elementos del Slider. Verifica que los IDs en el HTML sean correctos.");
                return;
            }

            // 2. Truco para ASP.NET: Busca los inputs cuyo ID "termine en" tu nombre, evita errores de sintaxis
            const hfMin = document.querySelector('[id$="hfSueldoMinimo"]');
            const hfMax = document.querySelector('[id$="hfSueldoMaximo"]');

            const formatoMoneda = new Intl.NumberFormat('es-MX', {
                style: 'currency',
                currency: 'MXN',
                minimumFractionDigits: 0
            });

            function actualizarSliderSueldo() {
                // Limites seguros extraídos del HTML
                let minLimit = parseInt(rangoMin.getAttribute('min')) || 0;
                let maxLimit = parseInt(rangoMin.getAttribute('max')) || 100000;

                let valMin = parseInt(rangoMin.value) || minLimit;
                let valMax = parseInt(rangoMax.value) || maxLimit;

                // Evitar que se crucen
                if (valMin >= valMax) {
                    rangoMin.value = valMax - parseInt(rangoMin.getAttribute('step') || 1000);
                    valMin = parseInt(rangoMin.value);
                }
                if (valMax <= valMin) {
                    rangoMax.value = valMin + parseInt(rangoMax.getAttribute('step') || 1000);
                    valMax = parseInt(rangoMax.value);
                }

                // Actualizar ASP.NET
                if (hfMin) hfMin.value = valMin;
                if (hfMax) hfMax.value = valMax;

                // Cálculo de porcentajes para la posición correcta
                const minPercent = ((valMin - minLimit) / (maxLimit - minLimit)) * 100;
                const maxPercent = ((valMax - minLimit) / (maxLimit - minLimit)) * 100;

                // Mover la barra de color
                progress.style.left = minPercent + "%";
                progress.style.width = (maxPercent - minPercent) + "%";

                // Mover los textos
                lblMin.innerText = formatoMoneda.format(valMin);
                lblMin.style.left = minPercent + "%";

                lblMax.innerText = formatoMoneda.format(valMax);
                lblMax.style.left = maxPercent + "%";

                // Solución para que no se encimen (salto hacia arriba)
                if ((maxPercent - minPercent) < 10) {
                    lblMax.style.top = "10px";  // El texto mayor sube si están muy juntos
                    lblMin.style.top = "55px";
                } else {
                    lblMin.style.top = "55px";  // Ambos abajo si hay espacio
                    lblMax.style.top = "55px";
                }
            }

            // Escuchar cambios e inicializar
            rangoMin.addEventListener('input', actualizarSliderSueldo);
            rangoMax.addEventListener('input', actualizarSliderSueldo);
            actualizarSliderSueldo();
        });

        document.addEventListener("DOMContentLoaded", function () {
            const chkEstudiante = document.getElementById("chkEstudiante");
            const chkEgresado = document.getElementById("chkEgresado");
            const seccionSemestre = document.getElementById("seccionSemestre");

            // Función que muestra u oculta el semestre
            function toggleSemestre() {
                if (chkEstudiante.checked) {
                    seccionSemestre.classList.remove("d-none");
                } else {
                    seccionSemestre.classList.add("d-none");
                }
            }

            // Evento cuando haces clic en Estudiante
            chkEstudiante.addEventListener("change", function () {
                if (this.checked) {
                    chkEgresado.checked = false; // Desmarca Egresado
                }
                toggleSemestre(); // Actualiza la sección de semestre
            });

            // Evento cuando haces clic en Egresado
            chkEgresado.addEventListener("change", function () {
                if (this.checked) {
                    chkEstudiante.checked = false; // Desmarca Estudiante
                }
                toggleSemestre(); // Actualiza la sección de semestre
            });

            // Evaluar estado al cargar la página (por si ocurre un PostBack)
            toggleSemestre();
        });

        document.addEventListener("DOMContentLoaded", function () {
            const chkMismaDir = document.getElementById("chkUsarMismaDireccion");
            const hfDirEmpresa = document.getElementById("hfDireccionEmpresa");
            const txtDireccion = document.querySelector('input[id$="txtDirección"]');

            if (!chkMismaDir || !txtDireccion) return;

            function aplicarDireccionEmpresa() {
                // En modo lectura el checkbox se deshabilita en el servidor; no tocar el readonly del campo
                if (chkMismaDir.disabled) return;

                if (chkMismaDir.checked) {
                    txtDireccion.value = hfDirEmpresa ? hfDirEmpresa.value : "";
                    txtDireccion.readOnly = true;
                    txtDireccion.classList.add("bg-light");
                    // Dispara el evento para que la etiqueta flotante se acomode
                    txtDireccion.dispatchEvent(new Event("input", { bubbles: true }));
                } else {
                    txtDireccion.readOnly = false;
                    txtDireccion.classList.remove("bg-light");
                }
            }

            chkMismaDir.addEventListener("change", aplicarDireccionEmpresa);

            // Estado al cargar (por si el PostBack dejó el check marcado)
            aplicarDireccionEmpresa();
        });
    </script>
</asp:Content>
