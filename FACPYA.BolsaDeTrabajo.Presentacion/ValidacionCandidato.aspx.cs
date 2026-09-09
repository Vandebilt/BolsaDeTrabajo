using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Logica;
using FACPYA.BolsaDeTrabajo.Logica.Validacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class ValidacionSustentante : System.Web.UI.Page
    {
        // Variable global para determinar la acción a realizar en el botón "Grabar"
        private int vpId
        {
            get
            {
                // Si existe en el ViewState, lo leemos
                if (ViewState["vpId"] != null)
                    return (int)ViewState["vpId"];

                return 0; // Valor por defecto si no existe
            }
            set
            {
                // Lo guardamos en el ViewState para que sobreviva a los PostBacks
                ViewState["vpId"] = value;
            }
        }

        #region Métodos
        // UsuarioSesion
        public int IdUsuarioSesion()
        {
            int pIdUsuario;
            bdUsuario vSesion = (bdUsuario)Session["usuario"];
            pIdUsuario = vSesion.IdUsuario;

            return pIdUsuario;
        }

        //RETORNA RUTA DE FOTOGRAFIAS
        public string RetornarRutaFotografia()
        {
            List<bdRuta> ruta = logicaRuta.LeerRuta(1);
            return ruta.Count > 0 ? ruta[0].Ruta : string.Empty;
        }

        //RETORNA RUTA DE DOCUMENTOS
        public string RetornarRutaDocumento()
        {
            List<bdRuta> ruta = logicaRuta.LeerRuta(2);
            return ruta.Count > 0 ? ruta[0].Ruta : string.Empty;
        }

        // Mostrar mensaje
        private void MostrarMensaje(string mensaje, string tipo, string url = null)
        {
            string script = url != null
                ? $"mostrarMensaje('{mensaje}', '{tipo}', '{url}')"
                : $"mostrarMensaje('{mensaje}', '{tipo}')";

            // CAMBIO AQUÍ: Usa ScriptManager en lugar de ClientScript
            // El 'this' se refiere al control actual (la página o un control dentro del UpdatePanel)
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", script, true);
        }

        // Abrir modal
        private void AbrirModal(string ventana)
        {
            string script = $"<script type='text/javascript'>$(document).ready(function() {{ abrirModal('{ventana}'); }});</script>";
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", script, false);
        }

        // Limpiar formulario
        protected void LimpiarFormulario()
        {
            ddlIdBusquedaIdTipoSustentante.ClearSelection();
            ddlBusquedaIdTipoArchivo.ClearSelection();
            txtBusquedaNombre.Text = string.Empty;
            CargarDropDownLists();
            vpId = 0;
        }
        #endregion

        #region Invocar funciones públicas
        // Instancia de la pantantalla maestra para el uso de funciones públicas
        Inicio master = new Inicio();

        // Cargar DropDownLists
        protected void CargarDropDownLists()
        {
            master.CargarDropDownList(ddlIdBusquedaIdTipoSustentante, 15, null);
            master.CargarDropDownList(ddlBusquedaIdTipoArchivo, 23, null);
        }

        // Cargar GridView
        protected List<DataRow> CargarGridView(int? pIdTipoSustentante, string pNombre, int? pIdTipoArchivo)
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaValidacionSustentante.Consultar(pIdTipoSustentante, pNombre, pIdTipoArchivo);
            bool tieneTipoSustentante = ddlIdBusquedaIdTipoSustentante.SelectedValue != "0";
            bool tieneNombre = !string.IsNullOrWhiteSpace(txtBusquedaNombre.Text);
            bool tieneTipoArchivo = ddlBusquedaIdTipoArchivo.SelectedValue != "0";

            // Mostrar el GridView si hay resultados, de lo contrario mostrar mensaje
            if (lista.Count > 0)
            {
                gvConsultaGeneral.Visible = true;
                master.CargarGridView(lista, gvConsultaGeneral);
            }
            else if (tieneTipoSustentante || tieneNombre || tieneTipoArchivo && lista.Count == 0)
            {
                MostrarMensaje("No se ha encontrado información con los parámetros de búsqueda seleccionados", "info", null);
                gvConsultaGeneral.Visible = false;
            }
            else 
            {
                gvConsultaGeneral.Visible = false;
            }
            
            gvConsultaGeneral.PageIndexChanging += new GridViewPageEventHandler(gvConsultaGeneral_PageIndexChanging);

            return lista;
        }

        // Leer Sustentante
        protected void LeerSustentante(int pIdSustentante)
        {
            List<bdValidacionSustentante> lista = logicaValidacionSustentante.Leer(pIdSustentante);
            txtNombreCompleto.Text = lista[0].Nombre.ToString();
        }

        // Seleccionar y eliminar
        protected void gvConsultaGeneral_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar" || e.CommandName == "Eliminar")
            {
                // Obtener el Id del sustentante desde el CommandArgument
                int pId = Convert.ToInt32(e.CommandArgument);
                vpId = pId;

                if (e.CommandName == "Seleccionar")
                {
                    Tuple<string, string> control;

                    List<bdValidacionSustentante> lista = logicaValidacionSustentante.LeerUsuarioAsignacion(pId);
                    List<bdValidacionSustentante> lista2 = logicaValidacionSustentante.Leer(pId);

                    // Verifica si el candidato ya está en revisión por otro usuario
                    if (lista2.Count > 0)
                    {
                        // Verifica si el candidato ya está en revisión por otro usuario
                        if (lista[0].IdUsuarioAsignacion != IdUsuarioSesion() && lista[0].IdUsuarioAsignacion != 0)
                        {
                            MostrarMensaje("El candidato ya se encuentra en revisión", "info", null);
                            CargarGridView(null, null, null);
                            return;
                        }

                        // Si el candidato no está en revisión por otro usuario, asigna el candidato al usuario actual
                        bdValidacionSustentante parametro = new bdValidacionSustentante
                        {
                            IdUsuario = IdUsuarioSesion(),
                            IdSustentante = pId
                        };

                        control = logicaValidacionSustentante.UsuarioAsignacion(parametro);

                        // Si la asignación fue exitosa, abre el modal y carga los datos del sustentante
                        vpId = pId;
                        AbrirModal("#miModal");
                        LeerSustentante(pId);
                        CargarGridViewDocumento(pId, null);
                    }
                    else
                    {
                        CargarGridView(null, null, null);
                    }
                }
            }
        }

        // Oculta columnas de GridView
        protected void gvConsultaGeneral_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    e.Row.Cells[1].Visible = false;

                    GridViewRow headerRow = gvConsultaGeneral.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[1].Visible = false;
                    }
                }
            }
        }

        // Paginación
        protected void gvConsultaGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Comprobar que exista un índice en la tabla filtrada
            if (e.NewPageIndex >= 0)
            {
                gvConsultaGeneral.PageIndex = e.NewPageIndex;

                // Verifica si hay datos filtrados en la sesión
                if (Session["sesionFiltro"] != null)
                {
                    // Carga los datos filtrados desde la sesión
                    var listaFiltrada = (List<DataRow>)Session["sesionFiltro"];
                    if (listaFiltrada.Count > 0)
                        master.CargarGridView(listaFiltrada, gvConsultaGeneral);
                }
                else
                {
                    // Si no hay datos filtrados en la sesión, carga todos los datos
                    CargarGridView(null, null, null);
                }

                e.Cancel = true;
            }
        }
        #endregion

        // GvDocumento
        protected List<DataRow> CargarGridViewDocumento(int? pIdSustentante, string pTipoArchivo)
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaValidacionSustentante.ConsultarDocumento(pIdSustentante, pTipoArchivo);

            if (lista.Count > 0)
            {
                gvConsultaDocumento.Visible = true;
                master.CargarGridView(lista, gvConsultaDocumento);
            }
            else
            {
                gvConsultaDocumento.Visible = false;
                MostrarMensaje("No se ha encontrado información con los parámetros de búsqueda seleccionados", "info", null);
            }

            gvConsultaDocumento.PageIndexChanging += new GridViewPageEventHandler(gvConsultaGeneral_PageIndexChanging);

            return lista;
        }

        // Oculta columnas de GridViewDocumento
        protected void gvConsultaDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlEstatusEnFila = (DropDownList)e.Row.FindControl("ddlIdEstatus");

                // 3. Verificar que se encontró
                if (ddlEstatusEnFila != null)
                {
                    // 4. ¡AQUÍ SÍ! Llama a tu método master para cargar ESTE DDL específico
                    master.CargarDropDownList(ddlEstatusEnFila, 21, null);

                    string idActual = DataBinder.Eval(e.Row.DataItem, "ESTATUS").ToString();

                    // Buscamos ese valor en el DDL que acabamos de llenar
                    if (ddlEstatusEnFila.Items.FindByValue(idActual) != null)
                    {
                        ddlEstatusEnFila.SelectedValue = idActual;
                    }
                }

                DropDownList ddlTipoRechazoEnFila = (DropDownList)e.Row.FindControl("ddlIdTipoRechazo");

                // 3. Verificar que se encontró
                if (ddlTipoRechazoEnFila != null)
                {
                    master.CargarDropDownList(ddlTipoRechazoEnFila, 22, null);
                }

                if (e.Row.Cells.Count > 1)
                {
                    //e.Row.Cells[4].Visible = false;
                    //e.Row.Cells[6].Visible = false;

                    GridViewRow headerRow = gvConsultaDocumento.HeaderRow;
                    if (headerRow != null)
                    {
                        //headerRow.Cells[4].Visible = false;
                        //headerRow.Cells[6].Visible = false;
                    }
                }
            }
        }

        // Carga de Pagina
        protected void Page_Load(object sender, EventArgs e)
        {
            // Carga de elementos principales dentro del módulo
            if (!IsPostBack)
            {
                logicaValidacionSustentante.UsuarioAsignacionNull(IdUsuarioSesion());
                CargarDropDownLists();
                CargarGridView(null, null, null);
                Session.Remove("sesionFiltro");
                vpId = 0;
            }
        }

        // Función para obtener el índice de la columna por nombre
        private int GetColumnIndexByName(GridView grid, string columnName)
        {
            if (grid.HeaderRow != null)
            {
                for (int i = 0; i < grid.HeaderRow.Cells.Count; i++)
                {
                    if (grid.HeaderRow.Cells[i].Text.Trim().Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    {
                        return i;
                    }
                }
            }
            return -1; // No encontrada
        }

        // Función para manejar el evento RowCommand del GridView de documentos
        protected void gvConsultaDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                // 1. Obtener el Id del archivo sustentante desde el CommandArgument
                int pId = Convert.ToInt32(e.CommandArgument);

                List<bdSustentanteArchivo> lista = logicaSustentanteArchivo.Leer(pId);

                // 2. Obtener el tipo de archivo desde la fila correspondiente
                LinkButton btn = (LinkButton)e.CommandSource;
                GridViewRow row = (GridViewRow)btn.NamingContainer;

                // 3. Obtener el índice de la columna "Archivo" usando la función GetColumnIndexByName
                int colIndex = GetColumnIndexByName(gvConsultaDocumento, "Archivo");
                string tipoArchivo = row.Cells[colIndex].Text.Trim().Replace("&nbsp;", "");

                if (colIndex != -1)
                {
                    if (tipoArchivo == "KARDEX")
                    {
                        // 4. Obtener la ruta del documento y el nombre del archivo
                        string Ruta = RetornarRutaDocumento();
                        string Documento = lista[0].NombreDocumento.ToString();
                        string ArchivoPdf = Ruta + Documento;

                        Session["ArchivoPdf"] = ArchivoPdf;
                        iframeContenido.Attributes["src"] = "Pdf.aspx";
                        AbrirModal("#ModalNuevo");
                    }
                    else if (tipoArchivo == "FOTOGRAFIA")
                    {
                        // 4. Obtener la ruta de la fotografía y el nombre del archivo
                        string Archivo = lista[0].NombreDocumento.ToString();

                        Session["NombreImagen"] = RetornarRutaFotografia() + Archivo;

                        if (Session["NombreImagen"] != null)
                        {
                            // Verifica si la imagen temporal existe en la sesión.
                            if (Session["NombreImagen"] != null)
                            {
                                string rutaSegundoASPX = $"Pdf.aspx?id={Archivo}";

                                imgContenido.Attributes["src"] = rutaSegundoASPX;
                                AbrirModal("#ModalNuevoFoto");

                            }
                        }
                    }
                }
            }
        }

        // Cerrar modal de PDF
        protected void btnCerrarPdf_Click(object sender, EventArgs e)
        {
            AbrirModal("#miModal");
            LeerSustentante(vpId);
            Session["ArchivoPdf"] = null;
        }

        // Cerrar modal de fotografía
        protected void btnCerrarFoto_Click(object sender, EventArgs e)
        {
            AbrirModal("#miModal");
            LeerSustentante(vpId);
            Session["NombreImagen"] = null;
        }

        // Cerrar modal de validación
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            vpId = 0;
            Tuple<string, string> control;
            control = logicaValidacionSustentante.UsuarioAsignacionNull(IdUsuarioSesion());
            CargarGridView(null, null, null);
        }

        // Función para manejar el evento Click del botón "Grabar"
        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            // Validación de campos y preparación de datos
            List<string> erroresValidacion = new List<string>();
            List<bdValidacionSustentante> parametrosParaGuardar = new List<bdValidacionSustentante>();

            // Inicializamos la bandera para detectar si hay documentos rechazados
            System.Text.StringBuilder sbHtmlCorreo = new System.Text.StringBuilder();
            sbHtmlCorreo.Append("<ul>"); // Abrimos la lista HTML

            bool hayRechazados = false;

            // Iteramos sobre cada fila del GridView de documentos
            foreach (GridViewRow row in gvConsultaDocumento.Rows)
            {
                // Verificamos que la fila sea de tipo DataRow
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Obtenemos los controles de la fila
                    DropDownList ddlEstatus = (DropDownList)row.FindControl("ddlIdEstatus");
                    DropDownList ddlRechazo = (DropDownList)row.FindControl("ddlIdTipoRechazo");
                    TextBox txtRetro = (TextBox)row.FindControl("txtRetroalimentacion");

                    string tipoArchivo = row.Cells[1].Text;

                    // Validamos que los controles no sean nulos antes de continuar
                    if (ddlEstatus == null || ddlRechazo == null || txtRetro == null) continue;

                    // Obtenemos los valores seleccionados o ingresados por el usuario
                    string valorEstatus = ddlEstatus.SelectedValue;
                    string valorRechazo = ddlRechazo.SelectedValue;
                    string valorRetro = txtRetro.Text.Trim();
                    int idArchivoSustentante = Convert.ToInt32(gvConsultaDocumento.DataKeys[row.RowIndex]["Id"]);

                    Dictionary<object, string> diccionario = new Dictionary<object, string>
                    {
                        { "vEstatus", valorEstatus }
                    };
                    List<string> listaValidacionFila = validacionSustentanteDocumento.Validar(diccionario);

                    // Si hay errores de validación, los agregamos a la lista de errores y continuamos con la siguiente fila
                    if (listaValidacionFila.Count > 0)
                    {
                        erroresValidacion.Add(listaValidacionFila[0]);
                        continue;
                    }

                    // Convertimos el valor del estatus a entero para comparaciones posteriores
                    int valorEstatusInt = int.Parse(valorEstatus);
                    int? idTipoRechazoParaGuardar = null;

                    // Si el estatus es "RECHAZADO" (5), verificamos que se haya seleccionado un tipo de rechazo
                    if (valorEstatusInt == 5)
                    {
                        if (string.IsNullOrEmpty(valorRechazo) || valorRechazo == "0")
                        {
                            erroresValidacion.Add("Es necesario seleccionar el tipo de rechazo requerido para la fila " + (row.RowIndex + 1));
                            continue;
                        }
                        idTipoRechazoParaGuardar = int.Parse(valorRechazo);
                    }

                    // Construimos el HTML para el correo electrónico según el estatus del documento
                    string estatusTexto = "";
                    string detalleTexto = "";

                    if (valorEstatus == "5") // RECHAZADO
                    {
                        // IMPORTANTE: Marcamos la bandera como verdadera
                        hayRechazados = true;
                        estatusTexto = "<span style='color:red;'>NO APROBADO</span>";
                        detalleTexto = "<div style='margin-top: 0px; margin-left: 30px; font-size: 0.95em;'>";
                        detalleTexto += $"<div><strong>MOTIVO:</strong> {ddlRechazo.SelectedItem.Text}</div>";

                        if (!string.IsNullOrEmpty(valorRetro))
                        {
                            detalleTexto += $"<div style='margin-top: 2px;'><strong>OBSERVACIONES:</strong> {valorRetro}</div>";
                        }
                        detalleTexto += "</div>";
                    }
                    else // APROBADO
                    {
                        estatusTexto = "<span style='color:green;'>APROBADO</span>";

                        if (!string.IsNullOrEmpty(valorRetro))
                        {
                            detalleTexto = "<div style='margin-top: 0px; margin-left: 30px; font-size: 0.95em;'>";
                            detalleTexto += $"<div style='margin-top: 2px;'><strong>OBSERVACIONES:</strong> {valorRetro}</div>";
                            detalleTexto += "</div>";
                        }
                    }

                    // Agregamos la fila a la lista
                    sbHtmlCorreo.Append($"<li style='margin-bottom: 5px;'><strong>{tipoArchivo}:</strong> {estatusTexto} {detalleTexto}</li>");

                    bdValidacionSustentante parametro = new bdValidacionSustentante
                    {
                        IdArchivoSustentante = idArchivoSustentante,
                        IdEstatus = valorEstatusInt,
                        IdTipoRechazo = idTipoRechazoParaGuardar,
                        RetroAlimentacion = valorRetro
                    };

                    parametrosParaGuardar.Add(parametro);

                }
            }

            // 3. AGREGAR EL MENSAJE FINAL (Despues de cerrar el ciclo)
            sbHtmlCorreo.Append("</ul>"); // Cerramos la lista de documentos (si no la habías cerrado antes)

            sbHtmlCorreo.Append("<br/>"); // Un salto de linea para separar

            if (hayRechazados)
            {
                // Mensaje si HUBO al menos un rechazo
                sbHtmlCorreo.Append("<div style='color: #D8000C; border: 1px solid #D8000C; padding: 10px; border-radius: 5px;'>");
                sbHtmlCorreo.Append("<strong>ATENCIÓN:</strong> Es necesario volver a cargar el/los documento(s) marcado(s) como <strong>NO APROBADO</strong> para continuar con el proceso.");
                sbHtmlCorreo.Append("</div>");
            }
            else
            {
                // Mensaje si TODO fue aprobado (la bandera se mantuvo en false)
                sbHtmlCorreo.Append("<div style='color: #4F8A10; border: 1px solid #4F8A10; padding: 10px; border-radius: 5px;'>");
                sbHtmlCorreo.Append("<strong>¡FELICIDADES!</strong> Tus archivos han sido admitidos correctamente. Tu perfil se encuentra listo para ser candidato en la bolsa de trabajo.");
                sbHtmlCorreo.Append("</div>");
            }

            if (erroresValidacion.Count > 0)
            {
                MostrarMensaje(erroresValidacion[0], "warning", "#miModal");
                AbrirModal("#miModal");
                return;
            }

            // Guardar los datos en la base de datos y enviar correo si es necesario
            if (parametrosParaGuardar.Count > 0)
            {
                Tuple<string, string> control = null;

                // Guardar cada documento y mostrar el mensaje correspondiente
                foreach (bdValidacionSustentante parametro in parametrosParaGuardar)
                {
                    control = logicaValidacionSustentante.ValidarDocumentos(parametro);
                    MostrarMensaje(control.Item1, control.Item2, null);
                }

                // Construir el HTML final para el correo electrónico
                string htmlFinalParaSql = sbHtmlCorreo.ToString();

                // Enviar correo si hay un Id válido
                if (vpId != 0)
                {
                    logicaEnviarCorreo.RevisionDocumentos(vpId, htmlFinalParaSql,2);
                }

                CargarGridView(null, null, null);
            }

            LimpiarFormulario();
        }

        // Función para manejar el evento Click del botón "Buscar"
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener el valor del filtro
            int? pIdTipoSustentante = int.TryParse(ddlIdBusquedaIdTipoSustentante.SelectedValue, out int tsus) && tsus != 0 ? (int?)tsus : null;
            int? pIdTipoArchivo = int.TryParse(ddlBusquedaIdTipoArchivo.SelectedValue, out int tarc) && tarc != 0 ? (int?)tarc : null;
            string pNombre = string.IsNullOrEmpty(txtBusquedaNombre.Text.Trim()) ? null : txtBusquedaNombre.Text.Trim();

            // Crea una variable de tipo sesión para mantener el filtro dentro de la paginación
            var lista = CargarGridView(pIdTipoSustentante, pNombre, pIdTipoArchivo);
            Session["sesionFiltro"] = lista;
        }

        // Función para manejar el evento Click del botón "Limpiar"
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            CargarGridView(null, null, null);
            Session.Remove("sesionFiltro");
        }

        // Función para manejar el evento Click del botón "Exportar"
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            // 1. Obtener el valor del filtro (descomentado)
            int? pIdTipoSustentante = int.TryParse(ddlIdBusquedaIdTipoSustentante.SelectedValue, out int tsus) && tsus != 0 ? (int?)tsus : null;
            int? pIdTipoArchivo = int.TryParse(ddlBusquedaIdTipoArchivo.SelectedValue, out int tarc) && tarc != 0 ? (int?)tarc : null;
            string pNombre = string.IsNullOrEmpty(txtBusquedaNombre.Text.Trim()) ? null : txtBusquedaNombre.Text.Trim();

            // Ejecuta la acción especial para el filtrado y descarga
            DataTable dataTable = logicaValidacionSustentante.ExportarExcel(pIdTipoSustentante, pNombre, pIdTipoArchivo);
            dataTable.TableName = "REPORTE VALIDACION SUSTENTANTE"; // Establece el nombre de la pestaña en el libro

            // Nombre del archivo a descargar
            logicaExportarExcel.ExportarMicrosoftExcel(dataTable, "REPORTE VALIDACION SUSTENTANTE " + DateTime.Now.ToString("dd-MM-yyyy"));

        }
    }
}
