using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class PanelSolicitudes : System.Web.UI.Page
    {
        // Variable global para determinar la acción a realizar en el botón "Grabar"
        private int vpId
        {
            get
            {
                if (ViewState["vpId"] != null)
                    return (int)ViewState["vpId"];

                return 0;
            }
            set
            {
                ViewState["vpId"] = value;
            }
        }

        #region Id de sesión
        public int IdUsuarioSesion()
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return 0; // Esto no se ejecutará, pero es necesario para que compile.
            }

            bdUsuario vSesion = (bdUsuario)Session["usuario"];
            return vSesion.IdUsuario;
        }

        // Empresa: solo ve sus propias vacantes. Administrador (1) y Subadministrador (3): ven todas.
        private int? IdEmpresaFiltro()
        {
            bdUsuario vSesion = (bdUsuario)Session["usuario"];

            if (vSesion.IdRol == 4)
                return vSesion.IdEmpresa;

            return null;
        }
        #endregion

        #region Utilerías
        // Instancia de la pantalla maestra para el uso de funciones públicas
        Inicio master = new Inicio();

        // Mostrar mensaje
        private void MostrarMensaje(string mensaje, string tipo, string url = null)
        {
            string script = url != null
                ? $"mostrarMensaje('{mensaje}', '{tipo}', '{url}')"
                : $"mostrarMensaje('{mensaje}', '{tipo}')";

            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", script, true);
        }
        // Cargar DropDownLists (catálogos): Estatus Vacante = Accion 32, Tipo de Candidato = Accion 15
        protected void CargarDropDownLists()
        {
            master.CargarDropDownList(ddlEstatusVacante, 32, null);
            master.CargarDropDownList(ddlTipoCandidato, 15, null);
        }
        #endregion

        // Cargar GridView (de la empresa en sesión, o todas si es Administrador/Subadministrador), aplicando los filtros
        protected List<DataRow> CargarGridView()
        {
            int? pIdEstatus = int.TryParse(ddlEstatusVacante.SelectedValue, out int est) && est != 0 ? (int?)est : null;
            int? pIdTipoCandidato = int.TryParse(ddlTipoCandidato.SelectedValue, out int tcan) && tcan != 0 ? (int?)tcan : null;

            var lista = logicaSolicitud.Consultar(IdEmpresaFiltro(), pIdEstatus, pIdTipoCandidato);

            if (lista.Count > 0)
            {
                gvConsultaGeneral.Visible = true;
                master.CargarGridView(lista, gvConsultaGeneral);
            }
            else
            {
                gvConsultaGeneral.Visible = false;
                MostrarMensaje("No se encontraron solicitudes con los filtros seleccionados", "info", null);
            }

            // Se guarda en sesión para poder paginar sin volver a consultar la BD
            Session["PanelSolicitudes_Lista"] = lista;

            return lista;
        }

        // Oculta columnas internas (Id, IdEmpresa) del GridView
        protected void gvConsultaGeneral_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells.Count > 2)
                {
                    e.Row.Cells[1].Visible = false; // Id
                    e.Row.Cells[2].Visible = false; // IdEmpresa

                    GridViewRow headerRow = gvConsultaGeneral.HeaderRow;
                    if (headerRow != null)
                    {
                        headerRow.Cells[1].Visible = false;
                        headerRow.Cells[2].Visible = false;
                    }
                }
            }
        }

        // Ver (solo lectura) - manda a Vacante.aspx con el Id de la solicitud encriptado (mismo esquema que ListadoCandidato -> Candidato)
        protected void gvConsultaGeneral_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                int pId = Convert.ToInt32(e.CommandArgument);

                byte[] idBytes = Encoding.UTF8.GetBytes(pId.ToString());
                byte[] encryptedBytes = MachineKey.Protect(idBytes, "VerIdSolicitud"); // "frase secreta" propia de esta pantalla
                string token = HttpUtility.UrlEncode(Convert.ToBase64String(encryptedBytes));

                Response.Redirect("Vacante.aspx?token=" + token);
            }
        }

        // Paginación
        protected void gvConsultaGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                gvConsultaGeneral.PageIndex = e.NewPageIndex;

                if (Session["PanelSolicitudes_Lista"] != null)
                {
                    var lista = (List<DataRow>)Session["PanelSolicitudes_Lista"];
                    if (lista.Count > 0)
                        master.CargarGridView(lista, gvConsultaGeneral);
                }
                else
                {
                    CargarGridView();
                }

                e.Cancel = true;
            }
        }

        // "Nueva Vacante": solo aplica a cuentas de Empresa. Para Administrador/Subadministrador se quita
        // la columna completa (no solo el botón) y los filtros se acomodan para ocupar el espacio libre.
        private void ConfigurarLayoutFiltros()
        {
            bdUsuario vSesion = (bdUsuario)Session["usuario"];

            if (vSesion.IdRol != 4)
            {
                colBtnNuevaVacante.Visible = false;
                colEstatusVacante.Attributes["class"] = "col-md-6";
                colTipoCandidato.Attributes["class"] = "col-md-6";
            }
        }

        // Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDropDownLists();
                ConfigurarLayoutFiltros();
                CargarGridView();
            }
        }

        protected void btnNuevaVacante_Click(object sender, EventArgs e)
        {
            // Nota: no se usa btnNuevaVacante.Enabled = false para bloquear esto, porque un LinkButton
            // deshabilitado no llega a hacer postback y entonces nunca podríamos mostrar el aviso.
            bdUsuario vSesion = (bdUsuario)Session["usuario"];
            var activas = logicaSolicitud.Consultar(vSesion.IdEmpresa, 1, null); // 1 = Activa

            if (activas.Count > 0)
            {
                MostrarMensaje("Ya cuentas con una solicitud activa. Solo se puede tener una solicitud activa a la vez.", "warning", null);
                return;
            }

            Response.Redirect("Vacante.aspx");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGridView();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlEstatusVacante.ClearSelection();
            ddlTipoCandidato.ClearSelection();
            CargarGridView();
        }
    }
}
