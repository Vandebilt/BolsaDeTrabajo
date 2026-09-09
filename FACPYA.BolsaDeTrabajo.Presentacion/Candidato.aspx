<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="Candidato.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.Sustentante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .data-label {
            font-size: 0.8rem;
            color: #6c757d;
            margin-bottom: 5px;
            display: block;
        }

        .data-value {
            font-size: 1.1rem;
            font-weight: 500;
            color: #212529;
            padding: 5px 8px;
            background-color: #f8f9fa;
            border-radius: 5px;
            border: 1px solid #e9ecef;
            display: block;
            width: 100%;
            min-height: 38px;
            word-wrap: break-word;
        }

        .card-laboral-view {
            border: 1px solid #ddd;
            border-radius: 4px;
            margin-bottom: 1.5rem;
            box-shadow: 0 2px 4px rgba(0,0,0,0.05);
            background-color: #fff;
        }

            .card-laboral-view .card-header-view {
                background: #ADA996; /* fallback for old browsers */
                background: -webkit-linear-gradient(to right, #EAEAEA, #DBDBDB, #F2F2F2, #ADA996); /* Chrome 10-25, Safari 5.1-6 */
                background: linear-gradient(to right, #EAEAEA, #DBDBDB, #F2F2F2, #ADA996); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                border-bottom: 1px solid #ddd;
                padding: 0.75rem 1.25rem;
                border-top-left-radius: 4px;
                border-top-right-radius: 4px;
            }

            .card-laboral-view .card-body-view {
                padding: 1.25rem;
                color: black;
            }

            .card-laboral-view .card-footer-view {
                background-color: #fcfcfc;
                border-top: 1px solid #eee;
                padding: 0.75rem 1.25rem;
                color: #6c757d;
                border-bottom-left-radius: 4px;
                border-bottom-right-radius: 4px;
            }

        .nav-tabs .nav-link.active {
            color: #000 !important;
        }

        #perfilTabs .nav-link {
            display: flex; /* Para alinear icono y texto */
            align-items: center; /* Centrado vertical */
            text-align: left;
            width: 100%;
            padding: 12px 15px; /* Más espacio interno */
            font-size: 15px !important;
            font-weight: 500;
            color: #555; /* Color de texto suave */
            border-radius: 8px; /* Bordes redondeados modernos */
            transition: all 0.3s ease;
            border: 1px solid transparent; /* Evita saltos al hacer hover */
            /* Manejo del texto largo */
            white-space: normal !important;
            line-height: 1.2 !important;
        }

        /* === REEMPLAZA TU CSS DE #perfilTabs CON ESTO === */
        #perfilTabs {
            display: flex !important;
            flex-direction: column !important;
            gap: 8px; /* Espacio entre botones */
        }

            #perfilTabs .nav-item {
                width: 100%;
            }

            #perfilTabs .nav-link {
                display: flex; /* Para alinear icono y texto */
                align-items: center; /* Centrado vertical */
                text-align: left;
                width: 100%;
                padding: 12px 15px; /* Más espacio interno */
                font-size: 15px !important;
                font-weight: 500;
                color: #555; /* Color de texto suave */
                border-radius: 2px; /* Bordes redondeados modernos */
                transition: all 0.3s ease;
                border: 1px solid transparent; /* Evita saltos al hacer hover */
                white-space: normal !important;
                line-height: 1.2 !important;
            }

                /* Estilo para el icono dentro del link */
                #perfilTabs .nav-link i {
                    font-size: 18px;
                    width: 30px; /* Ancho fijo para que el texto se alinee perfectamente */
                    text-align: center;
                    margin-right: 5px;
                }

                /* Hover (mouse encima) */
                #perfilTabs .nav-link:hover {
                    background-color: #f0f2f5;
                    color: #0067a3; /* Tu color azul corporativo */
                    transform: translateX(5px); /* Pequeña animación de movimiento */
                }

        /* Pestaña activa */
        .nav-pills .nav-link.active,
        .nav-pills .show > .nav-link {
            background-color: #161d26 !important; /* Azul en lugar de gris oscuro */
            color: white !important;
            box-shadow: 0 4px 6px rgba(0, 103, 163, 0.3); /* Sombra suave */
            font-weight: 600;
        }

            .nav-pills .nav-link.active i {
                color: white !important;
            }

        .seccion-perfil-wrapper {
            background: linear-gradient(135deg, #eef2f7, #ffffff);
            border-radius: 18px;
            padding: 25px;
            box-shadow: 0px 10px 25px rgba(0, 0, 0, 0.08);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid p-3">
        <div class="row justify-content-center">
            <div class="col-lg-3">
                <div class="row p-2">
                    <div class="card h-28vh">
                        <div class="card-body contenido">
                            <ul class="nav nav-pills flex-column mb-3" id="perfilTabs" role="tablist">
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link active" id="tab-personal" data-bs-toggle="tab" data-bs-target="#pane-personal" type="button" role="tab" aria-controls="pane-personal" aria-selected="true"><i class="fa-solid fa-circle-info me-2"></i>Personal</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-contacto" data-bs-toggle="tab" data-bs-target="#pane-contacto" type="button" role="tab" aria-controls="pane-contacto" aria-selected="false"><i class="fa-solid fa-address-card me-2"></i>Contacto</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-habycomp" data-bs-toggle="tab" data-bs-target="#pane-habycomp" type="button" role="tab" aria-controls="pane-habycomp" aria-selected="false"><i class="fa-solid fa-gears me-2"></i>Habilidades</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-academico" data-bs-toggle="tab" data-bs-target="#pane-academico" type="button" role="tab" aria-controls="pane-academico" aria-selected="false"><i class="fa-solid fa-school-flag me-2"></i>Académico</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-laboral" data-bs-toggle="tab" data-bs-target="#pane-laboral" type="button" role="tab" aria-controls="pane-laboral" aria-selected="false"><i class="fa-solid fa-briefcase me-2"></i>Experiencia</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-certificados" data-bs-toggle="tab" data-bs-target="#pane-certificados" type="button" role="tab" aria-controls="pane-certificados" aria-selected="false"><i class="fa-solid fa-certificate me-2"></i>Certificados</button>
                                </li>
                                <li class="nav-item" role="presentation">
                                    <button class="nav-link" id="tab-documentos" data-bs-toggle="tab" data-bs-target="#pane-documentos" type="button" role="tab" aria-controls="pane-documentos" aria-selected="false"><i class="fa-solid fa-file me-2"></i>Documentos</button>
                                </li>
                            </ul>
                        </div>
                        <div class="d-flex justify-content-center align-items-center mb-3 ">
                            <div>
                                <asp:Button ID="btnCargarCv" runat="server" Text="Ver Currículum" CssClass="btn btn-warning me-2" OnClick="btnCargarCv_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-8">
                <div class="row p-2">
                    <div class="card h-90vh">
                        <div class="card-body contenido">
                            <div class="tab-content" id="perfilTabsContent">
                                <!-- Información personal -->
                                <div class="tab-pane fade show active" id="pane-personal" role="tabpanel" aria-labelledby="tab-personal">
                                    <div class="row g-4 p-3">
                                        <div class="col-md-3 text-center">
                                            <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-4">FOTOGRAFÍA</h4>
                                            <asp:Image
                                                ID="imgFotoPerfil"
                                                runat="server"
                                                CssClass="img-fluid rounded"
                                                Width="100%" />
                                            <h5 class="fw-bold text-dark mt-4 mb-3">CONTACTO RÁPIDO</h5>
                                            <div class="text-start">
                                                <span class="data-label">Correo Principal</span>
                                                <asp:Label ID="lblCorreoPrincipal" runat="server" CssClass="data-value" />
                                                <span class="data-label mt-2">Teléfono Principal</span>
                                                <asp:Label ID="lblTelefonoPrincipal" runat="server" CssClass="data-value" />
                                                <span class="data-label mt-2">Enlace Linkedin</span>
                                                <asp:HyperLink ID="hlLinkedin" runat="server" CssClass="data-value" Target="_blank" NavigateUrl="#" />
                                            </div>
                                        </div>
                                        <div class="col-md-9">
                                            <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-4">DATOS PERSONALES</h4>
                                            <div class="row g-3">
                                                <div class="col-md-4">
                                                    <span class="data-label">Nombre(s)</span>
                                                    <asp:Label ID="lblNombre" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">Apellido Paterno</span>
                                                    <asp:Label ID="lblPrimerApellido" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">Apellido Materno</span>
                                                    <asp:Label ID="lblSegundoApellido" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">Fecha de Nacimiento</span>
                                                    <asp:Label ID="lblFechaNacimiento" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">Género</span>
                                                    <asp:Label ID="lblGenero" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">Estado Civil</span>
                                                    <asp:Label ID="lblEstadoCivil" runat="server" CssClass="data-value" />
                                                </div>
                                            </div>
                                            <h5 class="fw-bold text-dark mt-4 mb-3">DIRECCIÓN</h5>
                                            <div class="row g-3">
                                                <div class="col-md-4">
                                                    <span class="data-label">Nacionalidad</span>
                                                    <asp:Label ID="lblNacionalidad" runat="server" CssClass="data-value" Text="[Nacionalidad]" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">Municipio</span>
                                                    <asp:Label ID="lblMunicipio" runat="server" CssClass="data-value" Text="[Municipio]" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">Colonia</span>
                                                    <asp:Label ID="lblColonia" runat="server" CssClass="data-value" Text="[Colonia]" />
                                                </div>
                                                <div class="col-md-8">
                                                    <span class="data-label">Calle</span>
                                                    <asp:Label ID="lblCalle" runat="server" CssClass="data-value" Text="[Calle]" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">Número de Casa</span>
                                                    <asp:Label ID="lblNumeroCasa" runat="server" CssClass="data-value" Text="[Número]" />
                                                </div>
                                            </div>
                                            <h5 class="fw-bold text-dark mt-4 mb-3">DATOS LABORALES</h5>
                                            <div class="row g-3">
                                                <div class="col-md-4">
                                                    <span class="data-label">Sueldo Deseado</span>
                                                    <asp:Label ID="lblSueldoDeseado" runat="server" CssClass="data-value" Text="[$0.00]" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">Horario Disponible</span>
                                                    <asp:Label ID="lblHorarioDisponible" runat="server" CssClass="data-value" Text="[Horario]" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">¿Trabaja Actualmente?</span>
                                                    <asp:Label ID="lblTrabaja" runat="server" CssClass="data-value" Text="[Si/No]" />
                                                </div>
                                            </div>
                                            <h5 class="fw-bold text-dark mt-4 mb-3">BIOGRAFÍA</h5>
                                            <div class="row g-3">
                                                <div class="col-12">
                                                    <span class="text-dark">
                                                        <asp:Literal ID="litBiografia" runat="server" />
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Información personal -->

                                <!-- Información de Contacto -->
                                <div class="tab-pane fade" id="pane-contacto" role="tabpanel" aria-labelledby="tab-contacto">
                                    <div class="row p-3">
                                        <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-4">DATOS DE CONTACTO</h4>
                                        <div class="col-md-6 mb-5">
                                            <h5 class="text-dark d-flex align-items-center gap-2 mb-3"><i class="fa-solid fa-envelope"></i>Correos Registrados</h5>
                                            <asp:GridView ID="gvConsultaCorreo" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="False" AllowPaging="true" DataKeyNames="Id">
                                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                <PagerStyle CssClass="custom-pager" />
                                                <Columns>
                                                    <asp:BoundField DataField="TipoCorreo" HeaderText="Tipo" />
                                                    <asp:BoundField DataField="Correo" HeaderText="Correo" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="alert alert-info">No hay correos registrados.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-6 mb-5">
                                            <h5 class="text-dark d-flex align-items-center gap-2 mb-3"><i class="fa-solid fa-phone"></i>Teléfonos Registrados</h5>
                                            <asp:GridView ID="gvConsultaTelefono" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="False" AllowPaging="true">
                                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                <PagerStyle CssClass="custom-pager" />
                                                <Columns>
                                                    <asp:BoundField DataField="TipoTelefono" HeaderText="Tipo" />
                                                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                                    <asp:BoundField DataField="Extension" HeaderText="Ext." />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="alert alert-info">No hay teléfonos registrados.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Información de Contacto -->

                                <!-- Habilidades y Competencias -->
                                <div class="tab-pane fade" id="pane-habycomp" role="tabpanel" aria-labelledby="tab-habycomp">
                                    <div class="row justify-content-center p-3">
                                        <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-4">HABILIDADES Y COMPETENCIAS</h4>
                                        <div id="SeccAreaExperiencia" runat="server" visible="true" class="col-md-8 mb-5">
                                            <h5 class="text-dark d-flex align-items-center gap-2 mb-3"><i class="fa-solid fa-bars-progress"></i>Experiencia en Area</h5>
                                            <asp:GridView ID="gvConsultaAreaExperiencia" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="False" AllowPaging="true">
                                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                <PagerStyle CssClass="custom-pager" />
                                                <Columns>
                                                    <asp:BoundField DataField="AreaExperiencia" HeaderText="Área de Experiencia" />
                                                    <asp:BoundField DataField="Anios" HeaderText="Años" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="alert alert-info">No hay áreas de experiencia registradas.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                        <div id="SeccAreaInteres" runat="server" visible="true" class="col-md-8 mb-5">
                                            <h5 class="text-dark d-flex align-items-center gap-2 mb-3"><i class="fa-solid fa-bullseye"></i>Areas de Interés</h5>
                                            <asp:GridView ID="gvConsultaAreaInteres" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="False" AllowPaging="true">
                                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                <PagerStyle CssClass="custom-pager" />
                                                <Columns>
                                                    <asp:BoundField DataField="AreaInteres" HeaderText="Área de Interés" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="alert alert-info">No hay áreas de interés registradas.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                        <div id="SeccHabilidad" runat="server" visible="true" class="col-md-8 mb-5">
                                            <h5 class="text-dark d-flex align-items-center gap-2 mb-3"><i class="fa-solid fa-brain"></i>Habilidades</h5>
                                            <asp:GridView ID="gvConsultaHabilidad" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="False" AllowPaging="true">
                                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                <PagerStyle CssClass="custom-pager" />
                                                <Columns>
                                                    <asp:BoundField DataField="TipoHabilidad" HeaderText="Tipo" />
                                                    <asp:BoundField DataField="Habilidad" HeaderText="Habilidad" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="alert alert-info">No hay habilidades registradas.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                        <div id="SeccSoftware" runat="server" visible="true" class="col-md-8 mb-5">
                                            <h5 class="text-dark d-flex align-items-center gap-2 mb-3"><i class="fa-solid fa-laptop-code"></i>Software</h5>
                                            <asp:GridView ID="gvConsultaSoftware" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="False" AllowPaging="true">
                                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                <PagerStyle CssClass="custom-pager" />
                                                <Columns>
                                                    <asp:BoundField DataField="PaqueteSoftware" HeaderText="Software" />
                                                    <asp:BoundField DataField="Nivel" HeaderText="Nivel" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="alert alert-info">No hay software registrado.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                        <div id="SeccIdioma" runat="server" visible="true" class="col-md-8 mb-5">
                                            <h5 class="text-dark d-flex align-items-center gap-2 mb-3"><i class="fa-solid fa-language"></i>Idiomas</h5>
                                            <asp:GridView ID="gvConsultaIdioma" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="False" AllowPaging="true">
                                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                <PagerStyle CssClass="custom-pager" />
                                                <Columns>
                                                    <asp:BoundField DataField="Idioma" HeaderText="Idioma" />
                                                    <asp:BoundField DataField="Nivel" HeaderText="Nivel" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="alert alert-info">No hay idiomas registrados.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                        <div id="SeccCertificacionIdioma" runat="server" visible="true" class="col-md-8 mb-5">
                                            <h5 class="text-dark d-flex align-items-center gap-2 mb-3"><i class="fa-solid fa-language"></i>Certificacion de Idioma</h5>
                                            <asp:GridView ID="gvConsultaCertificacionIdioma" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="False" AllowPaging="true">
                                                <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                                <PagerStyle CssClass="custom-pager" />
                                                <Columns>
                                                    <asp:BoundField DataField="Idioma" HeaderText="Idioma" />
                                                    <asp:BoundField DataField="Certificación" HeaderText="Certificación" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="alert alert-info">No hay certificaciones de idioma registrados.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Habilidades y Competencias -->

                                <!-- Información Académica -->
                                <div class="tab-pane fade" id="pane-academico" role="tabpanel" aria-labelledby="tab-academico">
                                    <div class="row p-3">
                                        <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-4">INFORMACIÓN ACADÉMICA</h4>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <span class="data-label">Tipo Sustentante</span>
                                                <asp:Label ID="lblIdTipoSustentante" runat="server" CssClass="data-value" Text="[Tipo]" />
                                            </div>
                                            <div class="col-md-3">
                                                <span class="data-label">Matrícula</span>
                                                <asp:Label ID="lblMatricula" runat="server" CssClass="data-value" Text="[Matrícula]" />
                                            </div>
                                            <div class="col-md-2">
                                                <span class="data-label">Grado</span>
                                                <asp:Label ID="lblIdTipoGrado" runat="server" CssClass="data-value" Text="[Grado]" />
                                            </div>
                                            <div class="col-md-4">
                                                <span class="data-label">Carrera</span>
                                                <asp:Label ID="lblIdCarrera" runat="server" CssClass="data-value" Text="[Carrera]" />
                                            </div>
                                        </div>
                                        <asp:PlaceHolder ID="phEstudiante" runat="server" Visible="false">
                                            <h5 class="fw-bold text-dark mb-0 mt-5">Datos de Estudiante</h5>
                                            <div class="row g-3">
                                                <div class="col-md-3 ">
                                                    <span class="data-label">Plan</span>
                                                    <asp:Label ID="lblIdPlanEstudios" runat="server" CssClass="data-value" Text="[Plan]" />
                                                </div>
                                                <div class="col-md-3">
                                                    <span class="data-label">Modalidad</span>
                                                    <asp:Label ID="lblIdModalidad" runat="server" CssClass="data-value" Text="[Modalidad]" />
                                                </div>
                                                <div class="col-md-3">
                                                    <span class="data-label">Servicio Social</span>
                                                    <asp:Label ID="lblServicioSocial" runat="server" CssClass="data-value" Text="[Servicio Social]" />
                                                </div>
                                                <div class="col-md-3">
                                                    <span class="data-label">Prácticas Profesionales</span>
                                                    <asp:Label ID="lblPracticasProfesionales" runat="server" CssClass="data-value" Text="[Prácticas Profesionales]" />
                                                </div>
                                                <div class="col-md-6">
                                                    <span class="data-label">Semestre</span>
                                                    <asp:Label ID="lblIdSemestre" runat="server" CssClass="data-value" Text="[Semestre]" />
                                                </div>
                                                <div class="col-md-6">
                                                    <span class="data-label">Turno Escolar</span>
                                                    <asp:Label ID="lblIdTurnoEscolar" runat="server" CssClass="data-value" Text="[Turno]" />
                                                </div>
                                            </div>
                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder ID="phEgresado" runat="server" Visible="false">
                                            <h5 class="fw-bold text-dark mb-0 mt-5">Datos de Egresado</h5>
                                            <div class="row g-3">
                                                <div class="col-md-2">
                                                    <span class="data-label">Año Ingreso</span>
                                                    <asp:Label ID="lblAnioIngreso" runat="server" CssClass="data-value" Text="[Año]" />
                                                </div>
                                                <div class="col-md-2">
                                                    <span class="data-label">Año Egreso</span>
                                                    <asp:Label ID="lblAnioEgreso" runat="server" CssClass="data-value" Text="[Año]" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">Estatus Académico</span>
                                                    <asp:Label ID="lblIdEstatusAcademico" runat="server" CssClass="data-value" Text="[Estatus]" />
                                                </div>
                                                <div class="col-md-4">
                                                    <span class="data-label">Estatus Titulación de Licenciatura</span>
                                                    <asp:Label ID="lblIdEstatusTitulacion" runat="server" CssClass="data-value" Text="[Estatus]" />
                                                </div>
                                            </div>
                                        </asp:PlaceHolder>
                                        <h5 class="fw-bold text-dark mb-0 mt-5">Datos Complementarios</h5>
                                        <div class="row g-3">
                                            <div class="col-md-3">
                                                <span class="data-label">Promedio</span>
                                                <asp:Label ID="lblPromedio" runat="server" CssClass="data-value" Text="[Promedio]" />
                                            </div>
                                            <div class="col-md-9">
                                                <span class="data-label">Otra Carrera</span>
                                                <asp:Label ID="lblOtraCarrera" runat="server" CssClass="data-value" Text="[Otra Carrera]" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Información Académica -->

                                <!-- Experiencia Laboral -->
                                <div class="tab-pane fade" id="pane-laboral" role="tabpanel" aria-labelledby="tab-laboral">
                                    <div class="row p-3">
                                        <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-4">EXPERIENCIA LABORAL</h4>
                                        <asp:ListView ID="lvExperiencia" runat="server">
                                            <LayoutTemplate>
                                                <div class="row row-cols-1">
                                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <div class="col-md-12">
                                                    <div class="card-laboral-view">
                                                        <div class="card-header-view">
                                                            <h5 class="mb-0 fw-bold text-dark"><%# Eval("Empresa") %></h5>
                                                            <h6 class="text-secondary mt-1 mb-0"><%# Eval("Puesto") %></h6>
                                                            <span class="badge bg-warning text-dark mb-2">
                                                                <%# Eval("TipoTrabajo") %>
                                                            </span>
                                                        </div>
                                                        <div class="card-body-view">
                                                            LABORES:
                                                            <asp:Literal
                                                                ID="litDescripcion"
                                                                runat="server"
                                                                Mode="PassThrough"
                                                                Text='<%# FormatearDescripcionComoLista(Eval("Descripcion")) %>' />
                                                        </div>
                                                        <div class="card-footer-view d-flex justify-content-start">
                                                            <div class="me-4">
                                                                <small class="text-muted">Inicio:</small><br />
                                                                <strong class="text-dark"><%# Eval("FechaInicio", "{0:dd/MM/yyyy}") %></strong>
                                                            </div>
                                                            <div>
                                                                <small class="text-muted">Fin:</small><br />
                                                                <strong class="text-dark"><%# FormatearFechaFin(Eval("FechaFin")) %></strong>
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
                                    </div>
                                </div>
                                <!-- Fin Experiencia Laboral -->

                                <!-- Certificados -->
                                <div class="tab-pane fade" id="pane-certificados" role="tabpanel" aria-labelledby="tab-certificados">
                                    <div class="row p-3">
                                        <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-4">CERTIFICADOS</h4>
                                        <asp:ListView ID="lvCertificado" runat="server">
                                            <LayoutTemplate>
                                                <div class="row row-cols-1">
                                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <div class="col-md-12">
                                                    <div class="card-laboral-view">
                                                        <div class="card-header-view">
                                                            <h5 class="mb-0 fw-bold text-dark"><%# Eval("Descripcion") %></h5>
                                                            <h6 class="text-secondary mt-1 mb-0"><%# Eval("InstitucionEmisora") %></h6>
                                                        </div>
                                                        <div class="card-body-view">
                                                            <div class="mb-3">
                                                                <small class="text-muted d-block">Número de Certificado</small>
                                                                <strong class="text-dark"><%# Eval("NumeroCertificado") %></strong>
                                                            </div>
                                                            <div class="mb-3">
                                                                <small class="text-muted d-block">Fecha de Emisión</small>
                                                                <strong class="text-dark"><%# Eval("FechaEmision", "{0:dd/MM/yyyy}") %></strong>
                                                            </div>
                                                            <div class="mt-4 d-flex flex-wrap gap-2">
                                                                <asp:HyperLink
                                                                    runat="server"
                                                                    NavigateUrl='<%# ValidarDestino(Eval("UrlVerificacion")) %>'
                                                                    Target="_blank"
                                                                    CssClass="btn btn-outline-primary btn-sm"
                                                                    Visible='<%# !string.IsNullOrEmpty(Convert.ToString(Eval("UrlVerificacion"))) %>'>
                                                                <i class="fa-solid fa-link me-1"></i>
                                                                Mostrar Enlace
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnVerCertificado"
                                                                    runat="server"
                                                                    CssClass="btn btn-outline-secondary btn-sm"
                                                                    OnClick="btnVerCertificado_Click"
                                                                    CommandArgument='<%# Eval("Id") %>'
                                                                    Visible='<%# (Eval("ArchivoCertificado") != DBNull.Value && !string.IsNullOrEmpty(Eval("ArchivoCertificado").ToString())) %>'>
                                                                <i class="fa-solid fa-eye me-1"></i>
                                                                Ver Documento
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <div class="alert alert-info" role="alert">
                                                    No se encontró ningún certificado para este sustentante.
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                                <!-- Fin Certificados -->

                                <!-- Documentos -->
                                <div class="tab-pane fade" id="pane-documentos" role="tabpanel" aria-labelledby="tab-documentos">
                                    <div class="row p-3">
                                        <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 shadow-sm rounded-top mb-4">DOCUMENTOS</h4>
                                        <asp:GridView ID="gvConsultaDocumento" runat="server"
                                            CssClass="table table-sm table-striped table-hover"
                                            AutoGenerateColumns="False"
                                            AllowPaging="true"
                                            OnRowCommand="gvConsultaDocumento_RowCommand"
                                            DataKeyNames="Id">
                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                            <PagerStyle CssClass="custom-pager" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acción">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnVer" class="btn btn-primary btn-sm"
                                                            ToolTip="Ver"
                                                            runat="server"
                                                            CausesValidation="False"
                                                            CommandName="Ver"
                                                            CommandArgument='<%# Eval("RutaDocumento") %>'>
                                                        <i class="fa-solid fa-eye"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TipoArchivo" HeaderText="Tipo de Documento" />
                                                <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="alert alert-info">No hay documentos registrados.</div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <!-- Fin Documentos -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalNuevo" tabindex="-1" role="dialog" aria-labelledby="modalDocumento" aria-hidden="true">
        <div class="modal-dialog modal-fullscreen" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalEvidencia">Documento
                    </h5>
                </div>
                <div class="modal-body">
                    <iframe id="iframeContenido" runat="server" style="width: 100%; min-height: 95%; border: 0;"></iframe>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnCerrarPdf" runat="server" CssClass="btn btn-danger btnRed w-25"><i class="fa-solid fa-xmark"></i>&nbsp;Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfTabActivo" runat="server" />
    <script>
        $(document).ready(function () {
            // 1. Si hay un valor guardado, activar ese tab al cargar la página
            var tabId = $('#<%= hfTabActivo.ClientID %>').val();
            if (tabId) {
                $('#' + tabId).tab('show');
            }

            // 2. Cada vez que cambies de tab, guarda su ID en el HiddenField
            $('button[data-bs-toggle="tab"]').on('shown.bs.tab', function (e) {
                // Guardamos el ID del botón del tab (ej: tab-certificados)
                $('#<%= hfTabActivo.ClientID %>').val($(e.target).attr('id'));
            });
        });
    </script>
</asp:Content>
