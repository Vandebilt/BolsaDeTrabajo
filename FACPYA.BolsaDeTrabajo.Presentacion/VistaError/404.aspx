<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.VistaError._404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
   <meta name="viewport" content="width=device-width, initial-scale=1.0" />
   <title>Error 404</title>
   <link rel="stylesheet" href="https://unpkg.com/bootstrap@5.3.3/dist/css/bootstrap.min.css" />

   <!-- Estilos de diseño -->
   <style>
       body, html {
           height: 100%;
           margin: 0;
           display: flex;
           justify-content: center;
           align-items: center;
           background-color: #f4f4f4;
       }

       #form1 {
           text-align: center;
       }
   </style>
   <!-- Fin Estilos de diseño -->
</head>
<body>
   <form id="form1" runat="server">
    <div>
        <img src="../recursos/img/elefanteAbstracto.png" alt="Elefante FACPYA" width="300" />
        <h3 class="mt-3">¡Ups! Página no encontrada<font color="silver"> | Error 404</font></h3>
        <p>Puede que la página que busca no exista o el enlace esté incorrecto.</p>
        <asp:LinkButton ID="btnVolverInicio" runat="server" CssClass="btn btn-danger" OnClick="btnVolverInicio_Click">Volver a inicio</asp:LinkButton>
    </div>
</form>
</body>
</html>
