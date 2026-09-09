using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class Usuario : Page
    {
        // Variable global para determinar la acción a realizar en el botón "Grabar"
        public static int vpId;

        #region Métodos

        // UsuarioSesion
        public int IdUsuarioSesion()
        {
            int pIdUsuario;
            bdUsuario vSesion = (bdUsuario)Session["usuario"];
            pIdUsuario = vSesion.IdUsuario;

            return pIdUsuario;
        }

        // Mostrar mensaje
        private void MostrarMensaje(string mensaje, string tipo, string modal)
        {
            ClientScript.RegisterStartupScript(GetType(), "Mensaje", "alerta('" + mensaje + "', '" + tipo + "', '" + modal + "')", true);
        }

        // Limpiar formulario
        protected void LimpiarFormulario()
        {
            CargarDropDownLists();
            vpId = 0;
        }
        #endregion

        #region Invocar funciones públicas
        // Instancia de la pantantalla maestra para el uso de funciones públicas
        Inicio master = new Inicio();

        protected void CargarDropDownLists()
        {
            //master.CargarDropDownList(ddlIdRol, 1, null);
            //master.CargarDropDownList(ddlBusquedaIdRol, 1, null);
            //master.CargarDropDownList(ddlIdEmpleado, 2, 0);
        }

        protected List<DataRow> CargarGridView(int? pIdUsuario, int? pIdRol, string pCuenta, string pCorreo)
        {
            // Accesa a la función para llenar los datos y activa el Grid para la paginación
            var lista = logicaUsuario.Consultar(pIdUsuario, pIdRol, pCuenta, pCorreo);

            if (lista.Count > 0)
            {
                gvConsultaGeneral.Visible = true;
                master.CargarGridView(lista, gvConsultaGeneral);
            }
            else
            {
                gvConsultaGeneral.Visible = false;
                MostrarMensaje("No se ha encontrado información con los parámetros de búsqueda seleccionados", "info", null);
            }

            gvConsultaGeneral.PageIndexChanging += new GridViewPageEventHandler(gvConsultaGeneral_PageIndexChanging);

            return lista;
        }

        // Seleccionar y eliminar
        protected void gvConsultaGeneral_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar" || e.CommandName == "Eliminar")
            {
                int pId = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Seleccionar")
                {
                    List<bdUsuario> lista = logicaUsuario.Leer(pId);
                    if (lista.Count > 0)
                    {
                        vpId = pId;
                    }
                }
                else if (e.CommandName == "Eliminar")
                {
                    // Guarda el ID del usuario y muestra un mensaje de confirmación
                    Session["_Id"] = pId;
                    ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", $"mostrarConfirmacion('Confirmación de solicitud','¿Estás seguro de eliminar el registro seleccionado?','Usuario','confirmarEliminacion','')", true);

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
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;

                    GridViewRow headerRow = gvConsultaGeneral.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[2].Visible = false;
                        headerRow.Cells[3].Visible = false;
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
                    CargarGridView(null, null, null, null);
                }

                e.Cancel = true;
            }
        }
        #endregion

        // Cargar Página
        protected void Page_Load(object sender, EventArgs e)
        {
            // Carga de elementos principales dentro del módulo
            if (!IsPostBack)
            {
                CargarDropDownLists();
                CargarGridView(null, null, null, null);
                Session.Remove("sesionFiltro");
                vpId = 0;
            }
        }
    }
}