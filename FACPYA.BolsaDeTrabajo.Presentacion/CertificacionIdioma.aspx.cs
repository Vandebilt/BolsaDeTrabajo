using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Entidad.viewModels;
using FACPYA.BolsaDeTrabajo.Logica;
using FACPYA.BolsaDeTrabajo.Logica.Validacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class CertificacionIdioma : System.Web.UI.Page
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

        // UsuarioSesion
        public int IdUsuarioSesion()
        {
            int pIdUsuario;
            bdUsuario vSesion = (bdUsuario)Session["usuario"];
            pIdUsuario = vSesion.IdUsuario;

            return pIdUsuario;
        }

        #region Métodos
        // Mostrar mensaje
        private void MostrarMensaje(string mensaje, string tipo, string url = null)
        {
            string script = url != null
                ? $"mostrarMensaje('{mensaje}', '{tipo}', '{url}')"
                : $"mostrarMensaje('{mensaje}', '{tipo}')";

            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", script, true);
        }

        // Limpiar formulario
        protected void LimpiarFormulario()
        {
            txtCertificacionIdioma.Text = string.Empty;
            ddlIdIdioma.ClearSelection();
            ddlBusquedaIdEstatus.ClearSelection();
            txtBusquedaCertificacion.Text = string.Empty;
            ddlBusquedaIdIdioma.ClearSelection();
            vpId = 0;
        }
        #endregion

        #region Invocar funciones públicas
        // Instancia de la pantantalla maestra para el uso de funciones públicas
        Inicio master = new Inicio();

        // Cargar GridView
        protected List<DataRow> CargarGridView(int? pIdEstatus, string pCertificacion, int? pIdIdioma)
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaCertificacionIdioma.Consultar(pIdEstatus, pCertificacion, pIdIdioma);

            if (lista.Count > 0)
            {
                gvConsultaGeneral.Visible = true;
                master.CargarGridView(lista, gvConsultaGeneral);
                FuncionDeshabilitarControles(gvConsultaGeneral);
            }
            else
            {
                gvConsultaGeneral.Visible = false;
                MostrarMensaje("No se ha encontrado información con los parámetros de búsqueda seleccionados", "info", null);
            }

            gvConsultaGeneral.PageIndexChanging += new GridViewPageEventHandler(gvConsultaGeneral_PageIndexChanging);

            return lista;
        }

        // Cargar DropDownLists
        protected void CargarDropDownLists()
        {
            master.CargarDropDownList(ddlBusquedaIdEstatus, 27, null);
            master.CargarDropDownList(ddlBusquedaIdIdioma, 7, null);
            master.CargarDropDownList(ddlIdIdioma, 7, null);
        }

        // Seleccionar y eliminar
        protected void gvConsultaGeneral_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar" || e.CommandName == "Eliminar" || e.CommandName == "Suspender")
            {
                int pId = Convert.ToInt32(e.CommandArgument);
                List<bdCertificacionIdioma> lista = logicaCertificacionIdioma.Leer(pId);

                if (e.CommandName == "Seleccionar")
                {
                    vpId = pId;
                    txtCertificacionIdioma.Text = lista[0].Descripcion.ToString();
                    ddlIdIdioma.SelectedValue = lista[0].IdIdioma.ToString();
                }
                else if (e.CommandName == "Eliminar")
                {
                    // Guarda el ID de la Habilidad y muestra un mensaje de confirmación
                    Session["_Id"] = pId;
                    ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", $"mostrarConfirmacion('Confirmación de solicitud','¿Estás seguro de eliminar el registro seleccionado?','CertificacionIdioma','confirmarEliminacion','')", true);
                }
                else if (e.CommandName == "Suspender")
                {
                    // Si el registro está en ESTATUS 1 = 'ACTIVO' se Suspende
                    if (lista[0].IdEstatus == 1)
                    {
                        // Guarda el ID de la Habilidad y muestra un mensaje de confirmación
                        Session["_Id"] = pId;
                        ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", $"mostrarConfirmacion('Confirmación de solicitud','¿Estás seguro de suspender el registro seleccionado?','CertificacionIdioma','confirmarSuspender','')", true);
                    }
                    // Si el registro está en ESTATUS 7 = 'SUSPENDIDO' se Activa
                    else if (lista[0].IdEstatus == 7)
                    {
                        // Guarda el ID de la Habilidad y muestra un mensaje de confirmación
                        Session["_Id"] = pId;
                        ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", $"mostrarConfirmacion('Confirmación de solicitud','¿Estás seguro de reactivar el registro seleccionado?','CertificacionIdioma','confirmarSuspender','')", true);
                    }
                }
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // Carga de elementos principales dentro del módulo
            if (!IsPostBack)
            {
                Session["FiltroSustentante"] = null;
                Session.Remove("sesionFiltro");
                CargarGridView(null, null, null);
                CargarDropDownLists();
                vpId = 0;
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
                        FuncionDeshabilitarControles(gvConsultaGeneral);
                }
                else
                {
                    // Si no hay datos filtrados en la sesión, carga todos los datos
                    CargarGridView(null, null, null);
                }

                e.Cancel = true;
            }
        }

        // Función para deshabilitar la edición
        public void FuncionDeshabilitarControles(GridView pGridView)
        {
            foreach (GridViewRow row in pGridView.Rows)
            {
                LinkButton btnSuspender = row.FindControl("btnSuspender") as LinkButton;

                // Obtener el valor de la celda en la columna 7 (índice 6) para determinar si el botón PowerBI debe estar habilitado
                string valorCelda = "";
                if (row.Cells.Count > 7)
                {
                    valorCelda = System.Web.HttpUtility.HtmlDecode(row.Cells[7].Text).Trim();
                    if (valorCelda == "&nbsp;") valorCelda = "";
                }

                // Detectar si el valor de la celda es 7
                if (valorCelda == "7")
                {
                    // Cambiar el color del botón
                    btnSuspender.CssClass = btnSuspender.CssClass.Replace("btn-danger", "").Trim();
                    btnSuspender.CssClass += "btn btn-success";

                    // Cambiar el icono del botón
                    btnSuspender.Text = "<i class='fa-solid fa-plus'></i>";
                    btnSuspender.ToolTip = "Reactivar";
                }
            }
        }

        // Ocultar Columnas
        protected void gvConsultaGeneral_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 1)
                {
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[7].Visible = false;

                    GridViewRow headerRow = gvConsultaGeneral.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[3].Visible = false;
                        headerRow.Cells[4].Visible = false;
                        headerRow.Cells[7].Visible = false;
                    }
                }
            }
        }

        // Guardar y editar información
        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            // Validación de campos y formatos de entrada
            Dictionary<object, string> diccionario = new Dictionary<object, string>
            {
                { "vCertificacion", txtCertificacionIdioma.Text },
                { "vIdIdioma", ddlIdIdioma.SelectedValue },
            };

            List<string> listaValidacion = validacionCertificacionIdioma.Validar(diccionario);

            if (listaValidacion.Count == 0)
            {
                bdCertificacionIdioma parametro = new bdCertificacionIdioma
                {
                    IdCertificacionIdioma = vpId,
                    Descripcion = txtCertificacionIdioma.Text.ToUpper(),
                    IdIdioma = Convert.ToInt32(ddlIdIdioma.SelectedValue),
                    UsuarioRegistro = IdUsuarioSesion(),
                    UsuarioModificacion = IdUsuarioSesion()
                };

                // Evalúa si se trata de un nuevo registro o edición por medio de la variable pública (vpId)
                Tuple<string, string> control;

                if (vpId == 0)
                {
                    // Crear
                    control = logicaCertificacionIdioma.Crear(parametro);
                }
                else
                {
                    // Editar
                    control = logicaCertificacionIdioma.Editar(parametro);
                }

                MostrarMensaje(control.Item1, control.Item2, null);
                LimpiarFormulario();
                CargarGridView(null, null, null);
            }
            else
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
            }
        }

        // Cancelar Registro/Modificación
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            txtCertificacionIdioma.Text = string.Empty;
            ddlIdIdioma.ClearSelection();
        }

        // Buscar fitro
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int? pIdEstatus = int.TryParse(ddlBusquedaIdEstatus.SelectedValue, out int esta) && esta != 0 ? (int?)esta : null;
            string pCertificacion = string.IsNullOrEmpty(txtBusquedaCertificacion.Text.Trim()) ? null : txtBusquedaCertificacion.Text.Trim();
            int? pIdIdioma = int.TryParse(ddlBusquedaIdIdioma.SelectedValue, out int idi) && idi != 0 ? (int?)idi : null;

            // Crea una variable de tipo sesión para mantener el filtro dentro de la paginación
            var lista = CargarGridView(pIdEstatus, pCertificacion, pIdIdioma);
            Session["sesionFiltro"] = lista;
        }

        // Limpiar
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            CargarGridView(null, null, null);
            Session.Remove("sesionFiltro");
        }

        #region Confirmaciones
        [WebMethod]
        public static string confirmarEliminacion(string pConfirmacion)
        {
            // Página de Idioma
            CertificacionIdioma certificacionIdioma = new CertificacionIdioma();

            // Verifica si la confirmación es válida
            if (pConfirmacion.Equals("Confirmación"))
            {
                // Obtiene el ID del empleado desde la sesión
                int pIdCertificacionIdioma = (int)certificacionIdioma.Session["_Id"];
                var lista = logicaCertificacionIdioma.Leer(pIdCertificacionIdioma);

                if (lista.Count > 0)
                {
                    bdCertificacionIdioma parametro = new bdCertificacionIdioma
                    {
                        IdCertificacionIdioma = pIdCertificacionIdioma,
                        UsuarioBaja = certificacionIdioma.IdUsuarioSesion()
                    };

                    // Eliminar de la base de datos
                    var control = logicaCertificacionIdioma.Eliminar(parametro);
                    certificacionIdioma.MostrarMensaje(control.Item1, control.Item2, null);

                    // Retorna mensaje de error
                    if (control.Item2 != "success")
                    {
                        return control.Item1;
                    }

                    Page page = new Page();

                }
            }
            return "200"; //Estatus OK
        }

        [WebMethod]
        public static string confirmarSuspender(string pConfirmacion)
        {
            // Página de Idioma
            CertificacionIdioma certificacionIdioma = new CertificacionIdioma();

            // Verifica si la confirmación es válida
            if (pConfirmacion.Equals("Confirmación"))
            {
                // Obtiene el ID del empleado desde la sesión
                int pIdCertificacionIdioma = (int)certificacionIdioma.Session["_Id"];
                var lista = logicaCertificacionIdioma.Leer(pIdCertificacionIdioma);

                if (lista.Count > 0)
                {
                    bdCertificacionIdioma parametro = new bdCertificacionIdioma
                    {
                        IdCertificacionIdioma = pIdCertificacionIdioma,
                        UsuarioModificacion = certificacionIdioma.IdUsuarioSesion()
                    };

                    Tuple<string, string> control = null;

                    if (lista[0].IdEstatus == 1)
                    {
                        // SUSPENDE
                        control = logicaCertificacionIdioma.Suspender(parametro);
                    }
                    else if (lista[0].IdEstatus == 7)
                    {
                        // ACTIVA
                        control = logicaCertificacionIdioma.Activar(parametro);
                    }

                    certificacionIdioma.MostrarMensaje(control.Item1, control.Item2, null);

                    // Retorna mensaje de error
                    if (control.Item2 != "success")
                    {
                        return control.Item1;
                    }

                    Page page = new Page();

                }
            }
            return "200"; //Estatus OK
        }
        #endregion

        #region Exportar Excel
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Requerido para evitar el error de tiempo de ejecución
            // "El control 'GridView' debe colocarse dentro de una etiqueta de formulario con runat=server".
            // además dentro del diseño de la pantalla (.aspx) debe colocarse la propiedad 'EnableEventValidation="false" en la eqtiqueta principal de asp'
        }

        // Exportar a Excel
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            int? pIdEstatus = int.TryParse(ddlBusquedaIdEstatus.SelectedValue, out int esta) && esta != 0 ? (int?)esta : null;
            string pCertificacion = string.IsNullOrEmpty(txtBusquedaCertificacion.Text.Trim()) ? null : txtBusquedaCertificacion.Text.Trim();
            int? pIdIdioma = int.TryParse(ddlBusquedaIdIdioma.SelectedValue, out int idi) && idi != 0 ? (int?)idi : null;

            // Ejecuta la acción especial para el filtrado y descarga
            DataTable dataTable = logicaCertificacionIdioma.ExportarExcel(pIdEstatus, pCertificacion, pIdIdioma);
            dataTable.TableName = "REPORTE CERTIFICACION IDIOMA"; // Establece el nombre de la pestaña en el libro

            // Nombre del archivo a descargar
            logicaExportarExcel.ExportarMicrosoftExcel(dataTable, "REPORTE CERTIFICACION IDIOMA " + DateTime.Now.ToString("dd-MM-yyyy"));
        }
        #endregion
    }
}