<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="Empresa.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.Empresa" %>

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
            <div class="col-lg-10">
                <div class="row p-2">
                    <div class="card h-90vh">
                        <div class="card-body contenido">
                            <div class="tab-content" id="perfilTabsContent">
                                <!-- Información personal -->
                                <div class="tab-pane fade show active" id="pane-personal" role="tabpanel" aria-labelledby="tab-personal">
                                    <div class="row g-4 p-3">
                                        <div class="col-md-3 text-center">
                                            <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 rounded-top mb-4">LOGOTIPO</h4>
                                            <asp:Image
                                                ID="imgFotoPerfil"
                                                runat="server"
                                                CssClass="img-fluid rounded"
                                                Width="100%" />

                                        </div>
                                        <div class="col-md-9">
                                            <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 rounded-top mb-4">INFORMACIÓN DE LA EMPRESA</h4>
                                            <div class="row g-3">
                                                <div class="col-md-6">
                                                    <span class="data-label">Nombre de la Empresa</span>
                                                    <asp:Label ID="lblNombre" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-6">
                                                    <span class="data-label">Giro</span>
                                                    <asp:Label ID="lblGiro" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-6">
                                                    <span class="data-label">Tamaño de la Empresa</span>
                                                    <asp:Label ID="lblTamanioEmpresa" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-6">
                                                    <span class="data-label">Tipo de Empresa</span>
                                                    <asp:Label ID="lblTipoEmpresa" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-6">
                                                    <span class="data-label">Correo</span>
                                                    <asp:Label ID="lblCorreo" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-6">
                                                    <span class="data-label">URL Página Web</span>
                                                    <asp:Label ID="lblPaginaWeb" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-12">
                                                    <span class="data-label">Dirección</span>
                                                    <asp:Label ID="lblDireccion" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-6">
                                                    <span class="data-label">Nombre de Contacto</span>
                                                    <asp:Label ID="lblNombreContacto" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-6">
                                                    <span class="data-label">Puesto de Contacto</span>
                                                    <asp:Label ID="lblPuestoContacto" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-12">
                                                    <span class="data-label">Misión</span>
                                                    <asp:Label ID="lblMision" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-12">
                                                    <span class="data-label">Visión</span>
                                                    <asp:Label ID="lblVision" runat="server" CssClass="data-value" />
                                                </div>
                                                <div class="col-md-12">
                                                    <span class="data-label">Régimen de Gastos Médicos</span>
                                                    <asp:Label ID="lblRegimenGastosMedicos" runat="server" CssClass="data-value" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Información personal -->

                                <!-- Información de Contacto -->
                                <div class="row p-3">
                                    <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 rounded-top mb-4">DATOS DE CONTACTO</h4>
                                    <h5 class="text-dark d-flex align-items-center gap-2 mb-3"><i class="fa-solid fa-phone"></i>Teléfonos Registrados</h5>

                                    <div class="col-md-6 mb-5 mx-auto">
                                        <asp:GridView ID="gvConsultaTelefono" runat="server" CssClass="table table-sm table-striped table-hover text-center" AutoGenerateColumns="False" AllowPaging="true">
                                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                            <PagerStyle CssClass="custom-pager" />
                                            <Columns>
                                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                                <asp:BoundField DataField="Extension" HeaderText="Ext." />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="alert alert-info">No hay teléfonos registrados.</div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <!-- Fin Información de Contacto -->

                                <!-- Documentos -->
                                <div class="row p-3">
                                    <h4 class="card-header d-flex align-items-center text-dark fw-bold py-3 rounded-top mb-4">DOCUMENTO</h4>
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
