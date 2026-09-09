<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="PaqueteSoftware.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.PaqueteSoftware" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid p-3">
        <div class="row justify-content-center">
            <div class="col-lg-3">
                <!-- Area de consulta información -->
                <div class="row p-2">
                    <div class="card h-50vh">
                        <div class="text-center p-2">
                            <h5 class="card-title titulo"><i class="fa-solid fa-pen-to-square"></i>&nbsp;Gestión de información</h5>
                            <h6 class="card-subtitle mb-2 text-muted">Módulo Paquete de Software</h6>
                        </div>
                        <div class="card-body">
                            <!-- Controles -->
                            <div class="row g-3">
                                <div class="col-md-12 mb-5">
                                    <div class="form-floating">
                                        <asp:TextBox ID="txtPaqueteSoftware" runat="server" CssClass="form-control textbox" MaxLength="100" onkeypress="return AllowOnlyLetters(event);" placeholder="Paquete de Software"></asp:TextBox>
                                        <asp:Label ID="lblPaqueteSoftware" runat="server" AssociatedControlID="txtPaqueteSoftware" CssClass="label" Text="Paquete de Software"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="row g-1">
                                    <div class="col-md-6">
                                        <asp:LinkButton ID="btnGrabar" OnClick="btnGrabar_Click" runat="server" CssClass="btn btn-secondary btnGris w-100 "><i class="fa-regular fa-floppy-disk"></i>&nbsp;Grabar</asp:LinkButton>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:LinkButton ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" CssClass="btn btn-danger btnRed w-100"><i class="fa-solid fa-ban"></i>&nbsp;Cancelar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Area de ayuda -->
                <div class="row p-2">
                    <div class="card h-28vh">
                        <div class="px-4 pt-2">
                            <h5 class="card-title titulo"><i class="fa-solid fa-circle-info"></i>&nbsp;Información</h5>
                            <h6 class="card-subtitle mb-0 text-muted">Módulo de ayuda</h6>
                        </div>
                        <div class="card-body">
                            <!-- Agregar información de ayuda -->
                            <div class="alert alert-info">
                                ALTA, BAJA Y MODIFICACION DE LOS PAQUETES DE SOFTWARE.
                            </div>
                            <!-- Fin Agregar información de ayuda -->
                        </div>
                    </div>
                </div>
                <!-- Fin Area de ayuda -->
            </div>
            <!-- Segunda columna a la derecha -->
            <div class="col-lg-8">
                <!-- Area de consulta información -->
                <div class="row p-2">
                    <div class="card h-90vh">
                        <div class="text-center p-1">
                            <h5 class="card-title titulo"><i class="fa-solid fa-pen-to-square"></i>&nbsp;Paquete de Software</h5>
                            <h6 class="card-subtitle mb-0 text-muted">Información y filtrado</h6>
                        </div>
                        <div class="card-body">
                            <!-- Area de filtros -->
                            <div class="row mt-0">
                                <!-- Controles para búsqueda -->
                                <div class="col-md-10">
                                    <div class="row g-3">
                                        <div class="col-md-3">
                                            <div class="form-floating">
                                                <asp:DropDownList ID="ddlBusquedaIdEstatus" runat="server" CssClass="form-control textbox select2-custom" placeholder="Estatus"></asp:DropDownList>
                                                <asp:Label ID="lblBusquedaIdEstatus" runat="server" AssociatedControlID="ddlBusquedaIdEstatus" CssClass="label" Text="Estatus"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-9">
                                            <div class="form-floating">
                                                <asp:TextBox ID="txtBusquedaPaqueteSoftware" runat="server" CssClass="form-control textbox" MaxLength="100" placeholder="Paquete de Software"></asp:TextBox>
                                                <asp:Label ID="lblBusquedaPaqueteSoftware" runat="server" AssociatedControlID="txtBusquedaPaqueteSoftware" CssClass="label" Text="Paquete de Software"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Controles para búsqueda -->
                                <!-- Botones de búsqueda -->
                                <div class="col-md-2">
                                    <div class="row g-1">
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-info w-100 text-white" OnClick="btnBuscar_Click" ToolTip="Buscar"><i class="fa-solid fa-magnifying-glass"></i></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="btnLimpiar" runat="server" CssClass="btn btn-warning w-100 text-white" OnClick="btnLimpiar_Click" ToolTip="Limpiar"><i class="fa-solid fa-xmark"></i></asp:LinkButton>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-success w-100 text-white" OnClick="btnExportar_Click" ToolTip="Exportar"><i class="fa-regular fa-file-excel"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <!-- Fin Botones de búsqueda -->
                            </div>
                            <!-- Fin Area de filtros -->
                            <!-- Tabla para mostrar la información -->
                            <div class="mt-2 table-responsive">
                                <asp:GridView ID="gvConsultaGeneral" runat="server" CssClass="table table-sm table-striped table-hover" AutoGenerateColumns="true" AllowPaging="true" PageSize="8" DataKeyNames="Id" OnPageIndexChanging="gvConsultaGeneral_PageIndexChanging" OnRowDataBound="gvConsultaGeneral_RowDataBound" OnRowCommand="gvConsultaGeneral_RowCommand">
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
                                            <ItemStyle CssClass="px-3 text-center" Width="15px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSeleccionar" class="btn btn-warning text-white" data-position="right" ToolTip="Seleccionar" runat="server" CausesValidation="False" CommandName="Seleccionar" CommandArgument='<%# Eval("Id") %>'><i class="fa-solid fa-pen-to-square"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemStyle CssClass="px-3 text-center" Width="15px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEliminar" class="btn btn-danger" data-position="right" ToolTip="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'><i class="fa-solid fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemStyle CssClass="px-3 text-center" Width="15px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSuspender" class="btn btn-secondary" data-position="right" ToolTip="Suspender" runat="server" CausesValidation="False" CommandName="Suspender" CommandArgument='<%# Eval("Id") %>'><i class="fa-solid fa-ban"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <!-- Fin Tabla para mostrar la información -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>