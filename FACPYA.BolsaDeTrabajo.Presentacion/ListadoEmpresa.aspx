<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="ListadoEmpresa.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.ListadoEmpresa" %>

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
                            <h5 class="card-title titulo"><i class="fa-solid fa-user"></i>&nbsp;Empresas</h5>
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
                                                <asp:TextBox ID="txtBusquedaFolio" runat="server" CssClass="form-control textbox" MaxLength="255" placeholder="Folio"></asp:TextBox>
                                                <asp:Label ID="lblBusquedaFolio" runat="server" AssociatedControlID="txtBusquedaFolio" CssClass="label" Text="Folio"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-floating">
                                                <asp:TextBox ID="txtBusquedaNombre" runat="server" CssClass="form-control textbox" MaxLength="255" placeholder="Nombre"></asp:TextBox>
                                                <asp:Label ID="lblBusquedaNombre" runat="server" AssociatedControlID="txtBusquedaNombre" CssClass="label" Text="Nombre"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-floating">
                                                <asp:DropDownList ID="ddlBusquedaIdTipoEmpresa" runat="server" CssClass="form-control textbox select2-custom" placeholder="Tipo de Empresa"></asp:DropDownList>
                                                <asp:Label ID="lblBusquedaIdTipoEmpresa" runat="server" AssociatedControlID="ddlBusquedaIdTipoEmpresa" CssClass="label" Text="Tipo de Empresa"></asp:Label>
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
                            <div class="mt-3">
                                <asp:UpdatePanel ID="upEmpresas" runat="server" UpdateMode="Conditional" CssClass="fade-panel">
                                    <ContentTemplate>
                                        <asp:ListView ID="lvEmpresas" runat="server" DataKeyNames="Id"
                                            OnPagePropertiesChanging="lvEmpresas_PagePropertiesChanging" OnItemCommand="lvEmpresas_ItemCommand">
                                            <LayoutTemplate>
                                                <div class="sus-card-grid">
                                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton
                                                    ID="cardLinkButton"
                                                    runat="server"
                                                    CssClass="sus-card"
                                                    CommandName="Seleccionar"
                                                    CommandArgument='<%# Eval("Id") %>'>
                                                    <%-- El resto de tu contenido va EXACTAMENTE IGUAL DENTRO --%>
                                                    <div class="sus-card-header">
                                                        <div class="sus-profile-image">
                                                            <img src='<%# ConvertirRutaAImagenBase64(Eval("RutaLogotipo")) %>' alt="Logotipo" />
                                                        </div>
                                                    </div>
                                                   <div class="sus-card-body">
                                                        <h1 class="sus-nombre">
                                                            <%# Eval("NOMBRE DE LA EMPRESA") %> <br />
                                                        </h1>
                                                        <p class="sus-descripcion d-flex align-items-start">
                                                            <span>                                    
                                                                Folio: <%# Eval("Folio") %>
                                                            </span>
                                                        </p>
                                                    </div>
                                                </asp:LinkButton>
                                                <%-- Cerramos el LinkButton aquí --%>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <div class="alert alert-info" role="alert">
                                                    No se encontraron empresas con los criterios especificados.
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <!-- Fin Tabla para mostrar la información -->
                        </div>
                        <!-- Fin card-body -->
                        <!-- Inicio Card Footer -->
                        <div class="card-footer">
                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvEmpresas"
                                PageSize="16" CssClass="sus-pagination">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True"
                                        ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"
                                        ButtonCssClass="sus-pagination-btn" FirstPageText="<i class='fa-solid fa-less-than-equal'></i>"
                                        LastPageText="<i class='fa-solid fa-greater-than-equal'></i>" />
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="False"
                                        ShowLastPageButton="False" ButtonCssClass="sus-pagination-btn"
                                        NextPageText="<i class='fa-solid fa-greater-than'></i>"
                                        PreviousPageText="<i class='fa-solid fa-less-than'></i>" />
                                    <asp:TemplatePagerField>
                                        <PagerTemplate>
                                            <asp:Label ID="lblPageInfo" runat="server" CssClass="sus-page-info"
                                                Text='<%# String.Format("Página {0} de {1}", (Container.StartRowIndex / Container.PageSize) + 1, (Container.TotalRowCount + Container.PageSize - 1) / Container.PageSize) %>'></asp:Label>
                                        </PagerTemplate>
                                    </asp:TemplatePagerField>
                                </Fields>
                            </asp:DataPager>
                        </div>
                        <!-- Fin Card Footer -->
                    </div>
                </div>
                <!-- Fin Area de consulta información -->
            </div>
            <!-- Fin Segunda columna a la derecha -->
        </div>
    </div>
    <!-- Fin Contenido principal -->
</asp:Content>
