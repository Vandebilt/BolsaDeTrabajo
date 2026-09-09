<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="Curriculum.aspx.cs" Inherits="FACPYA.BolsaDeTrabajo.Presentacion.Curriculum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style scoped>
        /* --- 1. ESTILOS GENERALES (VISTA EN PANTALLA) --- */
        .centrar-todo {
            display: flex;
            flex-direction: column;
            align-items: center;
            /* CAMBIOS CLAVE AQUÍ: */
            justify-content: flex-start;
            padding-top: 20px; /* Un poco de aire, pero no demasiado */
            /* ESTO QUITA EL SCROLL: */
            /* Le decimos: "Mide el 100% de la pantalla MENOS 70px que mide el menú de arriba" */
            min-height: calc(100vh - 70px);
            width: 100%;
        }

        .contenedor-documento {
            width: 100%;
            max-width: 1000px; /* Limita el ancho del CV para que no se deforme */
        }

        /* --- 2. ESTILOS PARA IMPRESIÓN (CTRL + P) --- */
        @media print {
            /* Ocultar todo lo que no sea el CV */
            body * {
                visibility: hidden;
            }

            /* Limpiar el contenedor gris para que no estorbe en la hoja */
            .centrar-todo {
                background-color: white !important; /* Ahorra tinta */
                padding-top: 10px; /* Reducimos de 40px a 10px (o incluso 0 si prefieres) */
                min-height: auto !important; /* Quita la altura forzada */
                display: block !important; /* Quita comportamiento flex */
            }

            /* Hacer visible solo el CV */
            #curriculum, #curriculum * {
                visibility: visible;
            }

            /* Posicionar el CV en la esquina de la hoja */
            #curriculum {
                position: absolute;
                left: 0;
                top: 0;
                width: 100%;
                margin: 0;
                padding: 0;
                box-shadow: none !important; /* Quita sombras para impresión limpia */
            }

            /* Asegurar que los botones no salgan */
            .btn, .btn-exportar {
                display: none !important;
            }
        }
    </style>
    <main class="centrar-todo">
        <div class="contenedor-documento">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <a href="javascript:void(0);"
                    onclick="$('#ModalNuevo').modal('hide'); window.location.replace('<%= Session["RutaSustentante"] %>');"
                    class="btn btn-primary d-inline-flex align-items-center">
                    <i class="fa-solid fa-arrow-left me-2"></i>Volver al perfil
                </a>
                <button type="button" id="btn-exportar" class="btn-exportar btn btn-secondary d-inline-flex align-items-center m-0">
                    <i class="fa-solid fa-file-export me-2"></i>Exportar
                </button>
            </div>
            <div id="curriculum" class="bg-white shadow rounded">
                <section id="template_cv" runat="server">
                </section>
            </div>
        </div>
    </main>
    <script type="text/javascript">
        let btn_exportar = document.querySelector('#btn-exportar').addEventListener('click', (event) => {
            event.preventDefault();

            window.print()
        })
    </script>
</asp:Content>
