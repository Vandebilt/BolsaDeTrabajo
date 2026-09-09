<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="ValidacionCandidato.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.ValidacionSustentante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Contenido principal -->
    <div class="container-fluid p-3">
        <div class="row">
            <!-- Segunda columna a la derecha -->
            <div class="col-lg-11 mx-auto">
                <!-- Area de consulta información -->
                <div class="row p-2">
                    <div class="card h-90vh">
                        <div class="text-center p-2">
                            <h5 class="card-title titulo"><i class="fa-solid fa-pen-to-square"></i>&nbsp;Validación de Documentos</h5>
                            <h6 class="card-subtitle mb-2 text-muted">Información y filtrado</h6>
                        </div>
                        <div class="card-body">
                            <!-- Area de filtros -->
                            <div class="row">
                                <!-- Controles para búsqueda -->
                                <div class="col-md-10">
                                    <div class="row g-3">
                                        <div class="col-md-3">
                                            <div class="form-floating">
                                                <asp:DropDownList ID="ddlIdBusquedaIdTipoSustentante" runat="server" CssClass="form-control textbox select2-custom" placeholder="Tipo Candidato"></asp:DropDownList>
                                                <asp:Label ID="lblBusquedaIdTipoSustentante" runat="server" AssociatedControlID="ddlIdBusquedaIdTipoSustentante" CssClass="label" Text="Tipo Candidato"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-floating">
                                                <asp:TextBox ID="txtBusquedaNombre" runat="server" CssClass="form-control textbox" MaxLength="255" placeholder="Nombre"></asp:TextBox>
                                                <asp:Label ID="lblBusquedaNombre" runat="server" AssociatedControlID="txtBusquedaNombre" CssClass="label" Text="Nombre"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-floating">
                                                <asp:DropDownList ID="ddlBusquedaIdTipoArchivo" runat="server" CssClass="form-control textbox select2-custom" placeholder="Tpo Archivo"></asp:DropDownList>
                                                <asp:Label ID="lblBusquedaIdTipoArchivo" runat="server" AssociatedControlID="ddlBusquedaIdTipoArchivo" CssClass="label" Text="Tpo Archivo"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Controles para búsqueda -->
                                <!-- Botones de búsqueda -->
                                <div class="col-md-2">
                                    <div class="row g-1">
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-info w-100 text-white" ToolTip="Buscar" OnClick="btnBuscar_Click"><i class="fa-solid fa-magnifying-glass"></i></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="btnLimpiar" runat="server" CssClass="btn btn-warning w-100 text-white" ToolTip="Limpiar" OnClick="btnLimpiar_Click"><i class="fa-solid fa-xmark"></i></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Exportar" OnClick="btnExportar_Click"><i class="fa-regular fa-file-excel"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Botones de búsqueda -->
                            </div>
                            <!-- Fin Area de filtros -->
                            <!-- Tabla para mostrar la información -->
                            <div class="mt-3 table-responsive">
                                <asp:GridView ID="gvConsultaGeneral" runat="server" CssClass="table table-sm table-striped" AutoGenerateColumns="true" AllowPaging="true" PageSize="7" DataKeyNames="Id" OnRowCommand="gvConsultaGeneral_RowCommand" OnPageIndexChanging="gvConsultaGeneral_PageIndexChanging" OnRowDataBound="gvConsultaGeneral_RowDataBound">
                                    <RowStyle CssClass="py-0 m-0" />
                                    <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                    <PagerStyle CssClass="custom-pager" />
                                    <PagerTemplate>
                                        <div class="pagination">
                                            <asp:LinkButton ID="btnFirstPage" runat="server" CommandName="Page" CommandArgument="First" CssClass="pagination-btn" ToolTip="Primera"><i class="fa-solid fa-less-than-equal"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnPrevPage" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="pagination-btn" ToolTip="Anterior"><i class="fa-solid fa-less-than"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnNextPage" runat="server" CommandName="Page" CommandArgument="Next" CssClass="pagination-btn" ToolTip="Siguiente"><i class="fa-solid fa-greater-than"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnLastPage" runat="server" CommandName="Page" CommandArgument="Last" CssClass="pagination-btn" ToolTip="Última"><i class="fa-solid fa-greater-than-equal"></i></asp:LinkButton>
                                            <asp:Label ID="lblPageInfo" runat="server" CssClass="page-info mt-2" Text='<%# String.Format("Página {0} de {1}", gvConsultaGeneral.PageIndex + 1, gvConsultaGeneral.PageCount) %>'></asp:Label>
                                        </div>
                                    </PagerTemplate>
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSeleccionar" class="btn btn-warning text-white" data-position="right" ToolTip="Seleccionar" runat="server" CausesValidation="False" CommandName="Seleccionar" CommandArgument='<%# Eval("Id") %>'><i class="fa fa-pen-to-square"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <!-- Fin Tabla para mostrar la información -->
                        </div>
                    </div>
                </div>
                <!-- Fin Area de consulta información -->
            </div>
            <!-- Fin Segunda columna a la derecha -->
        </div>
    </div>
    <!-- Fin Contenido principal -->
    <!-- Modal -->
    <div class="modal fade" id="miModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content">
                <div class="modal-header header-sustentante">
                    <asp:TextBox ID="txtNombreCompleto" runat="server"
                        CssClass="form-control titulo-sustentante fs-2"
                        ReadOnly="true"
                        TabIndex="-1">  </asp:TextBox>
                </div>
                <div class="modal-body body-sustentante">
                    <div class="mt-3 table-responsive">
                        <asp:GridView ID="gvConsultaDocumento" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="false" AllowPaging="true" PageSize="12" DataKeyNames="Id" OnRowCommand="gvConsultaDocumento_RowCommand" OnRowDataBound="gvConsultaDocumento_RowDataBound">
                            <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                            <PagerStyle CssClass="custom-pager" />
                            <PagerTemplate>
                                <div class="pagination">
                                    <asp:LinkButton ID="btnFirstPage" runat="server" CommandName="Page" CommandArgument="First" CssClass="pagination-btn" ToolTip="Primera"><i class="fa-solid fa-less-than-equal"></i></asp:LinkButton>
                                    <asp:LinkButton ID="btnPrevPage" runat="server" CommandName="Page" CommandArgument="Prev" CssClass="pagination-btn" ToolTip="Anterior"><i class="fa-solid fa-less-than"></i></asp:LinkButton>
                                    <asp:LinkButton ID="btnNextPage" runat="server" CommandName="Page" CommandArgument="Next" CssClass="pagination-btn" ToolTip="Siguiente"><i class="fa-solid fa-greater-than"></i></asp:LinkButton>
                                    <asp:LinkButton ID="btnLastPage" runat="server" CommandName="Page" CommandArgument="Last" CssClass="pagination-btn" ToolTip="Última"><i class="fa-solid fa-greater-than-equal"></i></asp:LinkButton>
                                    <asp:Label ID="lblPageInfo" runat="server" CssClass="page-info mt-2" Text='<%# String.Format("Página {0} de {1}", gvConsultaDocumento.PageIndex + 1, gvConsultaDocumento.PageCount) %>'></asp:Label>
                                </div>
                            </PagerTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnVer" class="btn btnVer text-white" data-position="right" ToolTip="Ver" runat="server" CausesValidation="False" CommandName="Ver" CommandArgument='<%# Eval("Id") %>'><i class="fa fa-eye"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TipoArchivo" HeaderText="Archivo" />
                                <asp:TemplateField HeaderText="Revisión">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlIdEstatus" runat="server" CssClass="form-control textbox ddl-estatus"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rechazo">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlIdTipoRechazo" runat="server" CssClass="form-control textbox ddl-rechazo" Enabled="false"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retroalimentacion">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRetroalimentacion" runat="server" CssClass="form-control textbox txt-retroalimentacion"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <!-- Fin Controles -->
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnGrabar" runat="server" CssClass="btn btn-secondary btnGris w-25" OnClick="btnGrabar_Click"><i class="fa-regular fa-floppy-disk"></i>&nbsp;Grabar</asp:LinkButton>
                    <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn btn-danger btnRed w-25" OnClick="btnCerrar_Click"><i class="fa-solid fa-xmark"></i>&nbsp;Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <!-- Fin Modal -->
    <div class="modal fade" id="ModalNuevo" tabindex="-1" role="dialog" aria-labelledby="modalDocumento" aria-hidden="true">
        <div class="modal-dialog modal-fullscreen" role="document">
            <div class="modal-content">
               <%-- <div class="modal-header">
                    <h5 class="modal-title" id="modalEvidencia">DOCUMENTO
                    </h5>
                </div>--%>
                <div class="modal-body">
                    <!-- Iframe para cargar contenido dinámico -->
                    <iframe id="iframeContenido" runat="server" style="width: 100%; min-height: 95%; border: 0;"></iframe>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnCerrarPdf" runat="server" OnClick="btnCerrarPdf_Click" CssClass="btn btn-danger btnRed w-25"><i class="fa-solid fa-xmark"></i>&nbsp;Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalNuevoFoto" tabindex="-1" role="dialog" aria-labelledby="modalFotografia" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <%--<div class="modal-header">
                    <h5 class="modal-title" id="modalFotografia">FOTOGRAFÍA
                    </h5>
                </div>--%>
                <div class="modal-body">
                    <!-- Iframe para cargar contenido dinámico -->
                    <asp:Image ID="imgContenido" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnCerrarFoto" runat="server" OnClick="btnCerrarFoto_Click" CssClass="btn btn-danger btnRed w-25"><i class="fa-solid fa-xmark"></i>&nbsp;Cerrar</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <script src="/Scripts/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        // Función para habilitar o deshabilitar el ddl-rechazo según el valor del ddl-estatus
        function pageLoad(sender, args) {

            $('.ddl-estatus').each(function () {
                var ddlEstatus = $(this);

                var row = ddlEstatus.closest('tr');
                var ddlRechazo = row.find('.ddl-rechazo');
                var txtRetro = row.find('.txt-retroalimentacion');

                function actualizarEstadoRechazo() {
                    var valorRechazar = "5"; // Valor que HABILITA el ddlRechazo
                    var valorMantener = "4"; // Valor que DESHABILITA pero NO LIMPIA
                    var estatusVal = ddlEstatus.val(); // Valor actual del ddl-estatus

                    if (estatusVal == valorRechazar) {
                        ddlRechazo.prop('disabled', false);
                    //    txtRetro.val("");
                    } else if (estatusVal == valorMantener) {
                        ddlRechazo.prop('disabled', true);
                        ddlRechazo.val("0");
                    //    txtRetro.val("");
                    } else {
                        ddlRechazo.prop('disabled', true);
                        ddlRechazo.val("0");
                        //txtRetro.val("");

                    }

                }
                actualizarEstadoRechazo();

                ddlEstatus.off('change.miGrid').on('change.miGrid', function () {
                    actualizarEstadoRechazo();
                });
            });
        }
    </script>
</asp:Content>
