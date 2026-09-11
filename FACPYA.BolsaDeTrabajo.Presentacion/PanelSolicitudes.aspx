<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="PanelSolicitudes.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.PanelSolicitudes" %>

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
                            <h5 class="card-title titulo"><i class="fa-solid fa-pen-to-square"></i>&nbsp;Panel de Solicitudes</h5>
                            <h6 class="card-subtitle mb-2 text-muted">Información y filtrado</h6>
                        </div>
                        <div class="card-body">
                            <!-- Area de filtros -->
                            <div class="row">
                                <!-- Controles para búsqueda -->
                                <div class="col-md-10">
                                    <div class="row g-3">
                                        <div class="col-md-3" id="colBtnNuevaVacante" runat="server">
                                            <div class="form-floating">
                                                <asp:LinkButton ID="btnNuevaVacante" runat="server" OnClick="btnNuevaVacante_Click" CssClass="btn btn-success w-100 text-white" ToolTip="Buscar"><i class="fa-solid fa-plus"></i>
    <span>Nueva solicitud</span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-5" id="colEstatusVacante" runat="server">
                                            <div class="form-floating">
                                                <asp:DropDownList ID="ddlEstatusVacante" runat="server" CssClass="form-control textbox"></asp:DropDownList>
                                                <asp:Label ID="lblEstatusVacante" runat="server" AssociatedControlID="ddlEstatusVacante" CssClass="label" Text="Estatus Vacante"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4" id="colTipoCandidato" runat="server">
                                            <div class="form-floating">
                                                <asp:DropDownList ID="ddlTipoCandidato" runat="server" CssClass="form-control textbox"></asp:DropDownList>
                                                <asp:Label ID="lblTipoCandidato" runat="server" AssociatedControlID="ddlTipoCandidato" CssClass="label" Text="Tipo de Candidato"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Controles para búsqueda -->
                                <!-- Botones de búsqueda -->
                                <div class="col-md-2">
                                    <div class="row g-1">
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" CssClass="btn btn-info w-100 text-white" ToolTip="Buscar"><i class="fa-solid fa-magnifying-glass"></i></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" CssClass="btn btn-warning w-100 text-white" ToolTip="Limpiar"><i class="fa-solid fa-xmark"></i></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-success w-100 text-white" ToolTip="Exportar"><i class="fa-regular fa-file-excel"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Botones de búsqueda -->
                            </div>
                            <!-- Fin Area de filtros -->
                            <!-- Tabla para mostrar la información -->
                            <div class="mt-3 table-responsive">
                                <asp:GridView ID="gvConsultaGeneral" runat="server" CssClass="table table-sm table-striped" AutoGenerateColumns="true" AllowPaging="true" PageSize="7" DataKeyNames="Id" OnRowCommand="gvConsultaGeneral_RowCommand" OnRowDataBound="gvConsultaGeneral_RowDataBound" OnPageIndexChanging="gvConsultaGeneral_PageIndexChanging">
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
                                                <asp:LinkButton ID="btnVer" class="btn btn-primary text-white" data-position="right" ToolTip="Ver" runat="server" CausesValidation="False" CommandName="Ver" CommandArgument='<%# Eval("Id") %>'><i class="fa-solid fa-eye"></i></asp:LinkButton>
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
