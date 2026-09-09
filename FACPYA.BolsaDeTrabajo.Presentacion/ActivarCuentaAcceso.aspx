<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivarCuentaAcceso.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.ActivarCuentaAcceso" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Bolsa de Trabajo</title>
    <link rel="shortcut icon" type="image/png" href="Recursos/img/iconoFacpya.png" />
    <!-- Icono -->

    <!-- Recursos de diseño -->
    <link rel="stylesheet" href="https://unpkg.com/bootstrap@5.3.3/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.13.1/font/bootstrap-icons.min.css" />
    <link href="recursos/css/Login.css" rel="stylesheet" />
    <link href="recursos/css/alerta.css" rel="stylesheet" />
    <!-- Fin Recursos de diseño -->

    <!-- Recursos de funcionalidad -->
    <script src="recursos/js/alerta.js"></script>
    <!-- Fin Recursos de funcionalidad -->

</head>
<body>
    <!-- Mensajes de alerta -->
    <script type="text/javascript">
        function mostrarMensaje(titulo, icono, accion) {
            swal({
                title: titulo,
                text: "Mensaje del sistema",
                type: icono,
                allowOutsideClick: true,
                html: true
            }, function () {
                if (accion === "error") {
                    // Recarga después de OK
                    location.reload();
                }
            });
        }
    </script>

    <!-- Fin Mensajes de alerta -->
    <!-- Encabezado -->
    <header>
        <div>
            <br />
            <h2 class="titulo-bolsadetrabajo">ACTIVAR CUENTA</h2>
            <div>
                <hr />
                <!-- Línea divisoria entre los textos -->
            </div>
            <!-- Título de Bolsa de Trabajo -->
        </div>
    </header>
    <!-- Fin de Encabezado -->


    <form id="form1" runat="server" autocomplete="off">
        <!-- Login -->
        <div class="section-codigo">
            <div class="container-codigo d-flex overflow-hidden">
                <!-- Columna del logo -->
                <div class="col-logo d-flex align-items-center justify-content-center p-0 m-0">
                    <img src="Recursos/img/logoFacpya.png" alt="logo de FACPYA" class="img-fluid w-100 h-100 object-fit-cover" />
                </div>
                <!-- Fin Columna del logo -->

                <!-- Columna del formulario -->
                <div class="col-form p-4">
                    <%--                <div class="col-12 col-md-6 d-flex align-items-center justify-content-center">--%>
                    <!-- Formulario Inicio de sesión -->
                    <div id="divIniciarSesion" runat="server" visible="true">
                        <div class="centrar-vertical">
                            <h6 class="title text-center">Ingresar código</h6>
                            <div class="form-floating mb-2">
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control textbox"
                                    placeholder="Código" MaxLength="6"></asp:TextBox>
                                <asp:Label ID="lblCodigo" runat="server" class="form-label" Text="Código"
                                    AssociatedControlID="txtCodigo"></asp:Label>
                            </div>
                            <asp:Button ID="btnActivarCuenta" runat="server" CssClass="btn btn-danger btnRed" Text="Activar Cuenta" OnClick="btnActivarCuenta_Click" />
                        </div>
                    </div>
                    <!-- Fin Formulario Inicio de sesión -->
                    <!-- Modal omitido aquí por brevedad -->
                </div>
            </div>
        </div>
        <!-- Fin Login -->

        <!-- Pie de Página -->
        <div class="footer">
            <p>Secretaría de Desarrollo de Sistemas</p>
            <p>© STIC. FACPYA - <span id="year"></span></p>
        </div>

        <script type="text/javascript">
            // Actualiza el año automáticamente
            document.getElementById("year").innerText = new Date().getFullYear();
        </script>
        <!-- Recursos de funcionalidad -->
        <script src="Recursos/js/jquery-latest.js"></script>
        <script src="Recursos/js/bootstrap.min.js"></script>
        <!-- Fin Recursos de funcionalidad -->
    </form>
</body>
</html>


