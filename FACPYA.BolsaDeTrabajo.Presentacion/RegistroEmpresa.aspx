<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroEmpresa.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.RegistroEmpresa" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Bolsa de Trabajo</title>
    <link rel="shortcut icon" type="image/png" href="Recursos/img/iconoFacpya.png" />
    <!-- Icono -->
    <!-- Recursos externos de diseño -->
    <link rel="stylesheet" type="text/css" href="Recursos/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Recursos/css/login.css" />
    <link rel="stylesheet" type="text/css" href="Recursos/css/alerta.css" />
    <script src="Recursos/js/alerta.js"></script>
    <!-- Fin Recursos internos de diseño -->
    <!-- Recursos internos de diseño -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.7.9/dist/sweetalert2.all.min.js"></script>
    <script src="https://kit.fontawesome.com/551079ac96.js" crossorigin="anonymous"></script>
    <!-- Fin Recursos internos de diseño -->
</head>
<body>
    <!-- Encabezado -->
    <header>
        <div>
            <br />
            <h2 class="encabezado">Universidad Autónoma de Nuevo León</h2>
            <!-- Títulos del encabezado -->
            <h2 class="encabezado">Facultad de Contaduría Pública y Administración</h2>
            <div>
                <hr />
                <!-- Línea divisoria entre los textos -->
            </div>
            <!-- Título de Bolsa de Trabajo -->
            <h2 class="titulo-bolsadetrabajo">BOLSA DE TRABAJO</h2>
        </div>
    </header>
    <!-- Fin de Encabezado -->
    <!-- Mensajes de alerta -->
    <script type="text/javascript">
        function alerta(titulo, icono, ventana) {
            Swal.fire({
                title: titulo,
                text: "Mensaje del sistema",
                icon: icono,
                confirmButtonText: "Aceptar",
                confirmButtonColor: "#333"
            }).then((result) => {
                $(ventana).modal('show');
            })
        }
    </script>
    <!-- Fin Mensajes de alerta -->
    <form id="form1" runat="server" autocomplete="off">
        <!-- Login -->
        <div class="section-login">
            <div class="container-login d-flex overflow-hidden">
                <!-- Columna del logo -->
                <div class="col-logo d-flex align-items-center justify-content-center p-0 m-0">
                    <img src="Recursos/img/logoFacpya.png" alt="logo de FACPYA" class="img-fluid w-100 h-100 object-fit-cover" />
                </div>
                <!-- Fin Columna del logo -->
                <!-- Columna del formulario -->
                <div class="col-form p-4">
                    <!-- Formulario Registro de cuenta -->
                    <div id="divRegistroCuenta" runat="server" visible="true">
                        <h6 class="title text-center">Registro de Empresa</h6>
                        <div class="col-md-12 mb-3">
                            <div class="form-floating">
                                <asp:TextBox ID="txtCorreoRegistro" runat="server"
                                    CssClass="form-control textbox"
                                    MaxLength="255" placeholder="Correo"></asp:TextBox>
                                <asp:Label ID="lblCorreoRegistro" runat="server"
                                    AssociatedControlID="txtCorreoRegistro"
                                    CssClass="label"
                                    Text="Correo"></asp:Label>
                            </div>
                        </div>
                        <!-- CONTRASEÑA -->
                        <div class="col-md-12 mb-3">
                            <div class="form-floating position-relative">
                                <asp:TextBox ID="txtContraseniaRegistro" runat="server"
                                    CssClass="form-control textbox pe-5"
                                    MaxLength="50"
                                    TextMode="Password"
                                    placeholder="Contraseña"
                                    autocomplete="new-password"></asp:TextBox>
                                <asp:Label ID="lblContraseniaRegistro" runat="server"
                                    AssociatedControlID="txtContraseniaRegistro"
                                    CssClass="label"
                                    Text="Contraseña"></asp:Label>
                                <!-- Botón para mostrar/ocultar contraseña -->
                                <button type="button"
                                    class="btn position-absolute border-0 bg-transparent p-0"
                                    style="bottom: 8px; right: 12px; z-index: 10;"
                                    onclick="togglePassword('<%= txtContraseniaRegistro.ClientID %>', 'toggleIcon1')">
                                    <i id="toggleIcon1" class="fa fa-eye"></i>
                                </button>
                            </div>
                        </div>
                        <!-- CONFIRMAR CONTRASEÑA -->
                        <div class="col-md-12 mb-3">
                            <div class="form-floating position-relative">
                                <asp:TextBox ID="txtConfirmarContraseniaRegistro" runat="server"
                                    CssClass="form-control textbox pe-5"
                                    MaxLength="50" TextMode="Password"
                                    placeholder="Confirmar Contraseña"
                                    autocomplete="new-password"></asp:TextBox>
                                <asp:Label ID="lblConfirmarContrasenia" runat="server"
                                    AssociatedControlID="txtConfirmarContraseniaRegistro"
                                    CssClass="label"
                                    Text="Confirmar Contraseña"></asp:Label>
                                <button type="button"
                                    class="btn position-absolute border-0 bg-transparent p-0"
                                    style="bottom: 8px; right: 12px; z-index: 10;"
                                    onclick="togglePassword('<%= txtConfirmarContraseniaRegistro.ClientID %>', 'toggleIcon2')">
                                    <i id="toggleIcon2" class="fa fa-eye"></i>
                                </button>
                            </div>
                        </div>
                        <asp:LinkButton ID="btnCrearCuenta" runat="server" OnClick="btnCrearCuenta_Click" CssClass="btn btn-primary btnPrimary w-100">CREAR CUENTA</asp:LinkButton>
                    </div>
                    <!-- Recuperar contraseña -->
                    <div class="row">
                        <div class="form-floating mb-6">
                        </div>
                        <div class="modal fade" id="miModal" tabindex="-1" role="dialog" aria-labelledby="modalRecuperacion" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="modalRecuperacion">Recuperar contraseña</h5>
                                    </div>
                                    <div class="modal-body">
                                        <div class="col-md-12 mb-3 text-center">
                                            <p>Ingrese su correo electrónico para la recuperacion de la contraseña.</p>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-floating">
                                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control textbox" MaxLength="30" placeholder="Correo Electrónico"></asp:TextBox>
                                                <asp:Label ID="lblCorreo" runat="server" AssociatedControlID="txtCorreo" CssClass="label" Text="Correo Electrónico"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:LinkButton ID="btnGrabar" runat="server" CssClass="btn btn-primary btnGris w-25">
                                            <i class="fa-regular fa-paper-plane"></i>&nbsp;Enviar
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn btn-danger btnRed w-25">
                                            <i class="fa-solid fa-xmark"></i>&nbsp;Cerrar
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 mb-3">
                            <div class="d-flex gap-2 gap-md-4 flex-column flex-md-row justify-content-md-center justify-content-center text-center mt-4">
                                <asp:LinkButton ID="lnkbtnLogin" runat="server" href="Login.aspx" CssClass="link-secondary text-decoration-none">Ir al Inicio de Sesión</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <!-- Fin Recuperar contraseña -->
                    <script>
                        function togglePassword(inputId, iconId) {
                            const input = document.getElementById(inputId);
                            const icon = document.getElementById(iconId);

                            if (input.type === "password") {
                                input.type = "text";
                                icon.classList.remove("fa-eye");
                                icon.classList.add("fa-eye-slash");
                            } else {
                                input.type = "password";
                                icon.classList.remove("fa-eye-slash");
                                icon.classList.add("fa-eye");
                            }
                        }
                    </script>
                    <!-- CSS para ocultar el ojo nativo -->
                    <style>
                        /* Ocultar el icono nativo de mostrar/ocultar contraseña en Chrome, Edge y otros basados en WebKit */
                        input[type="password"]::-ms-reveal,
                        input[type="password"]::-ms-clear,
                        input[type="password"]::-webkit-textfield-decoration-container {
                            display: none !important;
                            appearance: none;
                        }

                        input[type="password"]::-webkit-credentials-auto-fill-button {
                            visibility: hidden;
                        }

                        input[type="password"]::-webkit-clear-button,
                        input[type="password"]::-webkit-inner-spin-button {
                            display: none !important;
                        }
                    </style>
                    <!-- Modal omitido aquí por brevedad -->
                </div>
            </div>
        </div>
    </form>
    <!-- Fin Login -->
    <!-- Pie de Página -->
    <div class="footer">
        <p>Secretaría de Tecnologías de Información y Comunicación</p>
        <p>© CTIC. FACPYA - <span id="year"></span></p>
    </div>
    <script type="text/javascript">
        // Actualiza el año automáticamente
        document.getElementById("year").innerText = new Date().getFullYear();
    </script>
    <!-- Recursos de funcionalidad -->
    <script src="Recursos/js/jquery-latest.js"></script>
    <script src="Recursos/js/bootstrap.min.js"></script>
    <!-- Fin Recursos de funcionalidad -->
</body>
</html>