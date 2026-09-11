using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Logica;
using FACPYA.BolsaDeTrabajo.Logica.Validacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class Vacante : System.Web.UI.Page
    {
        // Claves de las listas en Sesión (se mandan a BD hasta "Grabar")
        private const string SES_CARRERA = "Vacante_Carrera";
        private const string SES_MUNICIPIO = "Vacante_Municipio";
        private const string SES_SEMESTRE = "Vacante_Semestre";
        private const string SES_EXPERIENCIA = "Vacante_Experiencia";
        private const string SES_IDIOMA = "Vacante_Idioma";
        private const string SES_ACTIVIDAD = "Vacante_Actividad";

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

        // Indica si la pantalla se abrió en modo lectura (Ver desde PanelSolicitudes)
        private bool EnModoLectura
        {
            get
            {
                return ViewState["EnModoLectura"] != null && (bool)ViewState["EnModoLectura"];
            }
            set
            {
                ViewState["EnModoLectura"] = value;
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

        public int IdEmpresaSesion()
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return 0; // Esto no se ejecutará, pero es necesario para que compile.
            }

            bdUsuario vSesion = (bdUsuario)Session["usuario"];
            return vSesion.IdEmpresa;
        }

        public int IdEstatusSesion()
        {
            int idEmpresa = IdEmpresaSesion();

            if (idEmpresa > 0)
            {
                var empresa = logicaEmpresa.Leer(idEmpresa);

                if (empresa != null)
                {
                    return empresa[0].IdEstatus;
                }
            }
            return 0;
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

        // Recupera (o crea) la lista de sesión correspondiente
        private DataTable ObtenerLista(string clave)
        {
            if (Session[clave] == null)
                Session[clave] = CrearTabla(clave);

            return (DataTable)Session[clave];
        }

        // Estructura de cada lista de sesión: Id (temporal) + FK(s) + texto(s) a mostrar
        private DataTable CrearTabla(string clave)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));

            switch (clave)
            {
                case SES_CARRERA:
                    dt.Columns.Add("IdCarrera", typeof(int));
                    dt.Columns.Add("Carrera", typeof(string));
                    break;

                case SES_MUNICIPIO:
                    dt.Columns.Add("IdMunicipio", typeof(int));
                    dt.Columns.Add("Municipio", typeof(string));
                    break;

                case SES_SEMESTRE:
                    dt.Columns.Add("IdSemestre", typeof(int));
                    dt.Columns.Add("Semestre", typeof(string));
                    break;

                case SES_EXPERIENCIA:
                    dt.Columns.Add("IdAreaInteres", typeof(int));
                    dt.Columns.Add("Anios", typeof(decimal));
                    dt.Columns.Add("Area", typeof(string));
                    dt.Columns.Add("Tiempo", typeof(string));
                    break;

                case SES_IDIOMA:
                    dt.Columns.Add("IdIdioma", typeof(int));
                    dt.Columns.Add("IdNivelIdioma", typeof(int));
                    dt.Columns.Add("Idioma", typeof(string));
                    dt.Columns.Add("Nivel", typeof(string));
                    break;

                case SES_ACTIVIDAD:
                    dt.Columns.Add("Actividad", typeof(string));
                    break;
            }

            return dt;
        }

        // Id temporal para poder eliminar filas de la lista de sesión
        private int NuevoId(DataTable dt)
        {
            if (dt.Rows.Count == 0) return 1;
            return dt.AsEnumerable().Max(r => r.Field<int>("Id")) + 1;
        }

        // Enlaza el GridView con su lista de sesión
        private void BindGrid(GridView gv, DataTable dt)
        {
            gv.DataSource = dt;
            gv.DataBind();
            gv.Visible = dt.Rows.Count > 0;
        }

        // Oculta las columnas indicadas (índice 0 = botón Eliminar)
        private void OcultarColumnas(GridViewRow row, params int[] indices)
        {
            if (row.RowType == DataControlRowType.DataRow || row.RowType == DataControlRowType.Header)
            {
                foreach (int i in indices)
                {
                    if (i < row.Cells.Count)
                        row.Cells[i].Visible = false;
                }
            }
        }

        // Elimina la fila (por Id temporal) de la lista de sesión y refresca el GridView
        private void EliminarDeLista(string clave, GridView gv, object commandArgument)
        {
            int id = Convert.ToInt32(commandArgument);
            DataTable dt = ObtenerLista(clave);
            DataRow fila = dt.AsEnumerable().FirstOrDefault(r => r.Field<int>("Id") == id);

            if (fila != null)
                dt.Rows.Remove(fila);

            BindGrid(gv, dt);
        }

        protected void CargarDropDownLists()
        {
            master.CargarDropDownList(ddlIdCarrera, 12, null);
            master.CargarDropDownList(ddlIdSemestre, 13, null);
            master.CargarDropDownList(ddlIdAreaExperiencia, 6, null);
            master.CargarDropDownList(ddlIdGenero, 1, null);
            master.CargarDropDownList(ddlIdMunicipio, 3, null);
            master.CargarDropDownList(ddlIdTiempoDisponible, 26, null);
            master.CargarDropDownList(ddlIdIdioma, 7, null);
            master.CargarDropDownList(ddlIdNivelIdioma, 8, null);
        }

        protected void LeerEmpresa()
        {
            List<bdSolicitud> lista = logicaSolicitud.LeerEmpresa(IdEmpresaSesion());

            if (lista.Count > 0)
            {
                txtNombreEmpresa.Text = lista[0].Nombre;
                txtNombreyPuestoContacto.Text = lista[0].PuestoNombreContacto;

                // Dirección registrada de la Empresa (para "Usar misma dirección que la Empresa")
                hfDireccionEmpresa.Value = lista[0].Direccion ?? string.Empty;
            }
        }

        private void RefrescarGrids()
        {
            BindGrid(gvConsultaCarrera, ObtenerLista(SES_CARRERA));
            BindGrid(gvConsultaMunicipio, ObtenerLista(SES_MUNICIPIO));
            BindGrid(gvConsultaSemestre, ObtenerLista(SES_SEMESTRE));
            BindGrid(gvConsultaExperiencia, ObtenerLista(SES_EXPERIENCIA));
            BindGrid(gvConsultaIdioma, ObtenerLista(SES_IDIOMA));
            BindGrid(gvConsultaActividades, ObtenerLista(SES_ACTIVIDAD));
        }

        private void LimpiarListas()
        {
            Session.Remove(SES_CARRERA);
            Session.Remove(SES_MUNICIPIO);
            Session.Remove(SES_SEMESTRE);
            Session.Remove(SES_EXPERIENCIA);
            Session.Remove(SES_IDIOMA);
            Session.Remove(SES_ACTIVIDAD);
        }

        private void ManejarMensajesGlobales()
        {
            if (Session["MensajePendiente"] == null) return;

            string mensaje = Session["MensajePendiente"].ToString();
            string tipo = Session["TipoMensaje"]?.ToString() ?? "info";

            MostrarMensaje(mensaje, tipo, null);

            Session.Remove("MensajePendiente");
            Session.Remove("TipoMensaje");
        }
        #endregion

        // Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDropDownLists();

                int idSolicitud;
                bool esVista = TryDesencriptarIdSolicitud(Request.QueryString["token"], out idSolicitud);

                if (esVista)
                {
                    // Ver (solo lectura): viene de PanelSolicitudes.aspx
                    // EnModoLectura debe quedar en true ANTES de LeerSolicitud, porque ésta hace
                    // DataBind() de los grids (dispara RowDataBound) y ahí se decide qué columnas ocultar.
                    EnModoLectura = true;
                    LeerSolicitud(idSolicitud);
                    EstablecerModoLectura(true);
                }
                else
                {
                    // Nueva Vacante: se parte de listas vacías
                    LimpiarListas();
                    LeerEmpresa();
                    RefrescarGrids();
                }

                ManejarMensajesGlobales();
            }
        }

        #region Ver (solo lectura)
        // Desencripta el token de la URL (mismo esquema que ListadoCandidato -> Candidato: MachineKey.Protect/Unprotect)
        private bool TryDesencriptarIdSolicitud(string token, out int idSolicitud)
        {
            idSolicitud = 0;

            if (string.IsNullOrEmpty(token)) return false;

            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(token);
                byte[] idBytes = MachineKey.Unprotect(encryptedBytes, "VerIdSolicitud");
                string idTexto = Encoding.UTF8.GetString(idBytes);

                return int.TryParse(idTexto, out idSolicitud) && idSolicitud > 0;
            }
            catch
            {
                // Token inválido, alterado o de otra pantalla: se trata como si no viniera Id
                return false;
            }
        }

        // Regresa al listado de solicitudes
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("PanelSolicitudes.aspx");
        }

        // Carga la cabecera y el detalle de una solicitud existente, en modo lectura
        private void LeerSolicitud(int idSolicitud)
        {
            List<bdSolicitud> lista = logicaSolicitud.Leer(idSolicitud);

            if (lista.Count == 0)
            {
                MostrarMensaje("No se encontró la solicitud solicitada", "warning", null);
                Response.Redirect("PanelSolicitudes.aspx");
                return;
            }

            bdSolicitud solicitud = lista[0];

            txtSolicitudNo.Text = solicitud.IdSolicitud.ToString().PadLeft(4, '0');
            txtNombreEmpresa.Text = solicitud.Nombre;

            // Nombre y puesto del contacto (no viene en Leer, se toma de LeerEmpresa)
            List<bdSolicitud> empresa = logicaSolicitud.LeerEmpresa(solicitud.IdEmpresa);
            if (empresa.Count > 0)
            {
                txtNombreyPuestoContacto.Text = empresa[0].PuestoNombreContacto;
            }

            txtNombrePuesto.Text = solicitud.NombrePuesto;
            txtDirección.Text = solicitud.Direccion;
            txtNumVacantes.Text = solicitud.NumVacantes.ToString();
            txtLugarTrabajo.Text = solicitud.LugarTrabajo;

            ddlIdGenero.SelectedValue = solicitud.IdGenero.ToString();
            ddlIdTiempoDisponible.SelectedValue = solicitud.IdTiempoDisponible.ToString();
            ddlHoraInicio.SelectedValue = solicitud.HoraMinima.ToString(@"hh\:mm");
            ddlHoraFin.SelectedValue = solicitud.HoraMaxima.ToString(@"hh\:mm");

            hfEdadMinima.Value = solicitud.EdadMinima.ToString();
            hfEdadMaxima.Value = solicitud.EdadMaxima.ToString();
            hfSueldoMinimo.Value = ((int)solicitud.SueldoMinimo).ToString();
            hfSueldoMaximo.Value = ((int)solicitud.SueldoMaximo).ToString();

            // Reposiciona los sliders con los valores reales de la solicitud
            rangoMin.Attributes["value"] = solicitud.EdadMinima.ToString();
            rangoMax.Attributes["value"] = solicitud.EdadMaxima.ToString();
            rangoSueldoMin.Attributes["value"] = ((int)solicitud.SueldoMinimo).ToString();
            rangoSueldoMax.Attributes["value"] = ((int)solicitud.SueldoMaximo).ToString();

            chkEstudiante.Checked = solicitud.HasEstudiantes ?? false;
            chkEgresado.Checked = solicitud.HasEgresados ?? false;
            chkMostrarSueldo.Checked = solicitud.MostrarSueldo ?? false;
            chkAvisoPrivacidad.Checked = solicitud.HasAvisoPrivacidad;

            // Detalle (tablas hijas), en solo lectura
            CargarGridSoloLectura(gvConsultaCarrera, logicaCarreraSolicitud.Consultar(idSolicitud));
            CargarGridSoloLectura(gvConsultaMunicipio, logicaMunicipioSolicitud.Consultar(idSolicitud));
            CargarGridSoloLectura(gvConsultaSemestre, logicaSemestreSolicitud.Consultar(idSolicitud));
            CargarGridSoloLectura(gvConsultaExperiencia, logicaExperienciaSolicitud.Consultar(idSolicitud));
            CargarGridSoloLectura(gvConsultaIdioma, logicaIdiomaSolicitud.Consultar(idSolicitud));

            // Actividades: se guardaron concatenadas en un solo campo; se listan igual que al capturarlas
            DataTable dtActividad = CrearTabla(SES_ACTIVIDAD);
            if (!string.IsNullOrWhiteSpace(solicitud.Actividades))
            {
                string[] renglones = solicitud.Actividades.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string renglon in renglones)
                {
                    DataRow fila = dtActividad.NewRow();
                    fila["Id"] = NuevoId(dtActividad);
                    fila["Actividad"] = renglon;
                    dtActividad.Rows.Add(fila);
                }
            }
            BindGrid(gvConsultaActividades, dtActividad);
        }

        // Enlaza un GridView de detalle directo desde el SP "Consultar" (no desde la lista de sesión)
        private void CargarGridSoloLectura(GridView gv, List<DataRow> lista)
        {
            if (lista.Count > 0)
            {
                master.CargarGridView(lista, gv);
                gv.Visible = true;
            }
            else
            {
                gv.Visible = false;
            }
        }

        // Deshabilita/oculta todo lo capturable, igual que EstablecerModoLectura en PerfilEmpresa/PerfilCandidato
        private void EstablecerModoLectura(bool estaEnModoLectura)
        {
            EnModoLectura = estaEnModoLectura;

            // Datos generales
            txtNombrePuesto.ReadOnly = estaEnModoLectura;
            txtDirección.ReadOnly = estaEnModoLectura;
            txtNumVacantes.ReadOnly = estaEnModoLectura;
            txtLugarTrabajo.ReadOnly = estaEnModoLectura;
            ddlIdGenero.Enabled = !estaEnModoLectura;
            ddlIdTiempoDisponible.Enabled = !estaEnModoLectura;
            ddlHoraInicio.Enabled = !estaEnModoLectura;
            ddlHoraFin.Enabled = !estaEnModoLectura;
            chkEstudiante.Disabled = estaEnModoLectura;
            chkEgresado.Disabled = estaEnModoLectura;
            chkMostrarSueldo.Disabled = estaEnModoLectura;
            chkAvisoPrivacidad.Disabled = estaEnModoLectura;

            if (estaEnModoLectura)
            {
                rangoMin.Attributes["disabled"] = "disabled";
                rangoMax.Attributes["disabled"] = "disabled";
                rangoSueldoMin.Attributes["disabled"] = "disabled";
                rangoSueldoMax.Attributes["disabled"] = "disabled";
            }

            // "Usar misma dirección que la Empresa": es solo ayuda de captura, no un dato guardado -> se oculta por completo
            divUsarMismaDireccion.Visible = !estaEnModoLectura;

            // Carrera: el selector para agregar se oculta por completo (solo queda la lista ya capturada)
            // Nota: el botón Eliminar de cada grid NO se oculta con Columns[0].Visible (eso saca la celda
            // de la colección y recorre los índices de las demás columnas en RowDataBound); se oculta
            // por fila, igual que el resto, dentro del propio *_RowDataBound.
            ddlIdCarrera.Visible = !estaEnModoLectura;
            lblIdCarrera.Visible = !estaEnModoLectura;
            lbtnAgregarCarrera.Visible = !estaEnModoLectura;

            // Municipio
            ddlIdMunicipio.Visible = !estaEnModoLectura;
            lblIdMunicipio.Visible = !estaEnModoLectura;
            lbtnAgregarMunicipio.Visible = !estaEnModoLectura;

            // Semestre
            ddlIdSemestre.Visible = !estaEnModoLectura;
            lblIdSemestre.Visible = !estaEnModoLectura;
            lbtnAgregarSemestre.Visible = !estaEnModoLectura;

            // Experiencia (Área, Años y Meses se ocultan; el horario/turno no forma parte de esto)
            ddlIdAreaExperiencia.Visible = !estaEnModoLectura;
            lblIdAreaExperiencia.Visible = !estaEnModoLectura;
            txtAniosExperiencia.Visible = !estaEnModoLectura;
            lblAniosExperiencia.Visible = !estaEnModoLectura;
            txtMesesExperiencia.Visible = !estaEnModoLectura;
            lblMesesExperiencia.Visible = !estaEnModoLectura;
            lbtnAgregarAreaExperiencia.Visible = !estaEnModoLectura;

            // Idioma
            ddlIdIdioma.Visible = !estaEnModoLectura;
            lblIdIdioma.Visible = !estaEnModoLectura;
            ddlIdNivelIdioma.Visible = !estaEnModoLectura;
            lblIdNivelIdioma.Visible = !estaEnModoLectura;
            lbtnAgregarIdioma.Visible = !estaEnModoLectura;

            // Actividades
            txtActividad.Visible = !estaEnModoLectura;
            lblActividad.Visible = !estaEnModoLectura;
            lbntAgregarActividad.Visible = !estaEnModoLectura;

            // Grabar / Volver
            btnGrabar.Visible = !estaEnModoLectura;
            btnVolver.Visible = estaEnModoLectura;
        }
        #endregion

        #region Carrera
        protected void lbtnAgregarCarrera_Click(object sender, EventArgs e)
        {
            if (EnModoLectura) return;

            var diccionario = new Dictionary<object, string>
            {
                { "vIdCarrera", ddlIdCarrera.SelectedValue }
            };

            List<string> listaValidacion = validacionCarreraSolicitud.Validar(diccionario);
            if (listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            int idCarrera = Convert.ToInt32(ddlIdCarrera.SelectedValue);
            DataTable dt = ObtenerLista(SES_CARRERA);

            if (dt.AsEnumerable().Any(r => r.Field<int>("IdCarrera") == idCarrera))
            {
                MostrarMensaje("La Carrera que intenta registrar ya existe en la lista", "warning", null);
                ddlIdCarrera.ClearSelection();
                return;
            }

            DataRow fila = dt.NewRow();
            fila["Id"] = NuevoId(dt);
            fila["IdCarrera"] = idCarrera;
            fila["Carrera"] = ddlIdCarrera.SelectedItem.Text;
            dt.Rows.Add(fila);

            BindGrid(gvConsultaCarrera, dt);
            ddlIdCarrera.ClearSelection();
        }

        protected void gvConsultaCarrera_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (EnModoLectura)
                OcultarColumnas(e.Row, 0, 1, 2); // Eliminar, Id, IdCarrera
            else
                OcultarColumnas(e.Row, 1, 2); // Id, IdCarrera
        }

        protected void gvConsultaCarrera_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (EnModoLectura) return;

            if (e.CommandName == "Eliminar")
                EliminarDeLista(SES_CARRERA, gvConsultaCarrera, e.CommandArgument);
        }
        #endregion

        #region Municipio
        protected void lbtnAgregarMunicipio_Click(object sender, EventArgs e)
        {
            if (EnModoLectura) return;

            var diccionario = new Dictionary<object, string>
            {
                { "vIdMunicipio", ddlIdMunicipio.SelectedValue }
            };

            List<string> listaValidacion = validacionMunicipioSolicitud.Validar(diccionario);
            if (listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            int idMunicipio = Convert.ToInt32(ddlIdMunicipio.SelectedValue);
            DataTable dt = ObtenerLista(SES_MUNICIPIO);

            if (dt.AsEnumerable().Any(r => r.Field<int>("IdMunicipio") == idMunicipio))
            {
                MostrarMensaje("El Municipio que intenta registrar ya existe en la lista", "warning", null);
                ddlIdMunicipio.ClearSelection();
                return;
            }

            DataRow fila = dt.NewRow();
            fila["Id"] = NuevoId(dt);
            fila["IdMunicipio"] = idMunicipio;
            fila["Municipio"] = ddlIdMunicipio.SelectedItem.Text;
            dt.Rows.Add(fila);

            BindGrid(gvConsultaMunicipio, dt);
            ddlIdMunicipio.ClearSelection();
        }

        protected void gvConsultaMunicipio_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (EnModoLectura)
                OcultarColumnas(e.Row, 0, 1, 2); // Eliminar, Id, IdMunicipio
            else
                OcultarColumnas(e.Row, 1, 2); // Id, IdMunicipio
        }

        protected void gvConsultaMunicipio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (EnModoLectura) return;

            if (e.CommandName == "Eliminar")
                EliminarDeLista(SES_MUNICIPIO, gvConsultaMunicipio, e.CommandArgument);
        }
        #endregion

        #region Semestre
        protected void lbtnAgregarSemestre_Click(object sender, EventArgs e)
        {
            if (EnModoLectura) return;

            var diccionario = new Dictionary<object, string>
            {
                { "vIdSemestre", ddlIdSemestre.SelectedValue }
            };

            List<string> listaValidacion = validacionSemestreSolicitud.Validar(diccionario);
            if (listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            int idSemestre = Convert.ToInt32(ddlIdSemestre.SelectedValue);
            DataTable dt = ObtenerLista(SES_SEMESTRE);

            if (dt.AsEnumerable().Any(r => r.Field<int>("IdSemestre") == idSemestre))
            {
                MostrarMensaje("El Semestre que intenta registrar ya existe en la lista", "warning", null);
                ddlIdSemestre.ClearSelection();
                return;
            }

            DataRow fila = dt.NewRow();
            fila["Id"] = NuevoId(dt);
            fila["IdSemestre"] = idSemestre;
            fila["Semestre"] = ddlIdSemestre.SelectedItem.Text;
            dt.Rows.Add(fila);

            BindGrid(gvConsultaSemestre, dt);
            ddlIdSemestre.ClearSelection();
        }

        protected void gvConsultaSemestre_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (EnModoLectura)
                OcultarColumnas(e.Row, 0, 1, 2); // Eliminar, Id, IdSemestre
            else
                OcultarColumnas(e.Row, 1, 2); // Id, IdSemestre
        }

        protected void gvConsultaSemestre_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (EnModoLectura) return;

            if (e.CommandName == "Eliminar")
                EliminarDeLista(SES_SEMESTRE, gvConsultaSemestre, e.CommandArgument);
        }
        #endregion

        #region Experiencia
        protected void lbtnAgregarAreaExperiencia_Click(object sender, EventArgs e)
        {
            if (EnModoLectura) return;

            var diccionario = new Dictionary<object, string>
            {
                { "vIdAreaInteres", ddlIdAreaExperiencia.SelectedValue },
                { "vAnios", txtAniosExperiencia.Text },
                { "vMeses", txtMesesExperiencia.Text },
            };

            List<string> listaValidacion = validacionExperienciaSolicitud.Validar(diccionario);
            if (listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            int idArea = Convert.ToInt32(ddlIdAreaExperiencia.SelectedValue);
            DataTable dt = ObtenerLista(SES_EXPERIENCIA);

            if (dt.AsEnumerable().Any(r => r.Field<int>("IdAreaInteres") == idArea))
            {
                MostrarMensaje("El Área que intenta registrar ya existe en la lista", "warning", null);
                ddlIdAreaExperiencia.ClearSelection();
                return;
            }

            string sAnios = string.IsNullOrWhiteSpace(txtAniosExperiencia.Text) ? "0" : txtAniosExperiencia.Text.Trim();
            string sMeses = string.IsNullOrWhiteSpace(txtMesesExperiencia.Text) ? "0" : txtMesesExperiencia.Text.Trim();

            if (sAnios == "0" && sMeses == "0")
            {
                MostrarMensaje("Debe ingresar un tiempo válido (años o meses deben ser mayores a 0)", "warning", null);
                txtAniosExperiencia.Text = string.Empty;
                txtMesesExperiencia.Text = string.Empty;
                return;
            }

            decimal anios = decimal.Parse(sAnios + "." + sMeses, CultureInfo.InvariantCulture);

            DataRow fila = dt.NewRow();
            fila["Id"] = NuevoId(dt);
            fila["IdAreaInteres"] = idArea;
            fila["Anios"] = anios;
            fila["Area"] = ddlIdAreaExperiencia.SelectedItem.Text;
            fila["Tiempo"] = $"{sAnios} año(s) {sMeses} mes(es)";
            dt.Rows.Add(fila);

            BindGrid(gvConsultaExperiencia, dt);
            ddlIdAreaExperiencia.ClearSelection();
            txtAniosExperiencia.Text = string.Empty;
            txtMesesExperiencia.Text = string.Empty;
        }

        protected void gvConsultaExperiencia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // En modo lectura los datos vienen de spExperienciaSolicitud (Id, IdAreaInteres, Area, Tiempo -> sin Anios)
            if (EnModoLectura)
                OcultarColumnas(e.Row, 0, 1, 2); // Eliminar, Id, IdAreaInteres
            else
                OcultarColumnas(e.Row, 1, 2, 3); // Id, IdAreaInteres, Anios
        }

        protected void gvConsultaExperiencia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (EnModoLectura) return;

            if (e.CommandName == "Eliminar")
                EliminarDeLista(SES_EXPERIENCIA, gvConsultaExperiencia, e.CommandArgument);
        }
        #endregion

        #region Idioma
        protected void lbtnAgregarIdioma_Click(object sender, EventArgs e)
        {
            if (EnModoLectura) return;

            var diccionario = new Dictionary<object, string>
            {
                { "vIdIdioma", ddlIdIdioma.SelectedValue },
                { "vIdNivelIdioma", ddlIdNivelIdioma.SelectedValue }
            };

            List<string> listaValidacion = validacionIdiomaSolicitud.Validar(diccionario);
            if (listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            int idIdioma = Convert.ToInt32(ddlIdIdioma.SelectedValue);
            int idNivel = Convert.ToInt32(ddlIdNivelIdioma.SelectedValue);
            DataTable dt = ObtenerLista(SES_IDIOMA);

            if (dt.AsEnumerable().Any(r => r.Field<int>("IdIdioma") == idIdioma))
            {
                MostrarMensaje("El Idioma que intenta registrar ya existe en la lista", "warning", null);
                ddlIdIdioma.ClearSelection();
                ddlIdNivelIdioma.ClearSelection();
                return;
            }

            DataRow fila = dt.NewRow();
            fila["Id"] = NuevoId(dt);
            fila["IdIdioma"] = idIdioma;
            fila["IdNivelIdioma"] = idNivel;
            fila["Idioma"] = ddlIdIdioma.SelectedItem.Text;
            fila["Nivel"] = ddlIdNivelIdioma.SelectedItem.Text;
            dt.Rows.Add(fila);

            BindGrid(gvConsultaIdioma, dt);
            ddlIdIdioma.ClearSelection();
            ddlIdNivelIdioma.ClearSelection();
        }

        protected void gvConsultaIdioma_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // En modo lectura los datos vienen de spIdiomaSolicitud (Id, IdIdioma, Idioma, Nivel -> sin IdNivelIdioma)
            if (EnModoLectura)
                OcultarColumnas(e.Row, 0, 1, 2); // Eliminar, Id, IdIdioma
            else
                OcultarColumnas(e.Row, 1, 2, 3); // Id, IdIdioma, IdNivelIdioma
        }

        protected void gvConsultaIdioma_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (EnModoLectura) return;

            if (e.CommandName == "Eliminar")
                EliminarDeLista(SES_IDIOMA, gvConsultaIdioma, e.CommandArgument);
        }
        #endregion

        #region Actividades
        protected void lbntAgregarActividad_Click(object sender, EventArgs e)
        {
            if (EnModoLectura) return;

            string actividad = (txtActividad.Text ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(actividad))
            {
                MostrarMensaje("Alerta: Se espera escribir una Actividad y/o función", "warning", null);
                return;
            }

            DataTable dt = ObtenerLista(SES_ACTIVIDAD);

            if (dt.AsEnumerable().Any(r => string.Equals(r.Field<string>("Actividad"), actividad, StringComparison.OrdinalIgnoreCase)))
            {
                MostrarMensaje("La Actividad que intenta registrar ya existe en la lista", "warning", null);
                return;
            }

            DataRow fila = dt.NewRow();
            fila["Id"] = NuevoId(dt);
            fila["Actividad"] = actividad;
            dt.Rows.Add(fila);

            BindGrid(gvConsultaActividades, dt);
            txtActividad.Text = string.Empty;
        }

        protected void gvConsultaActividades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (EnModoLectura)
                OcultarColumnas(e.Row, 0, 1); // Eliminar, Id
            else
                OcultarColumnas(e.Row, 1); // Id
        }

        protected void gvConsultaActividades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (EnModoLectura) return;

            if (e.CommandName == "Eliminar")
                EliminarDeLista(SES_ACTIVIDAD, gvConsultaActividades, e.CommandArgument);
        }
        #endregion

        #region Grabar
        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (EnModoLectura) return;

            string horaInicio = ddlHoraInicio.SelectedValue;
            string horaFin = ddlHoraFin.SelectedValue;

            // Si marcó "Usar misma dirección que la Empresa" se toma la registrada de la Empresa
            string direccion = chkUsarMismaDireccion.Checked
                ? (hfDireccionEmpresa.Value ?? string.Empty).Trim()
                : txtDirección.Text.Trim();

            // Validación de campos de la cabecera
            var diccionario = new Dictionary<object, string>
            {
                { "vNombrePuesto", txtNombrePuesto.Text.Trim() },
                { "vDireccion", direccion },
                { "vNumVacantes", txtNumVacantes.Text.Trim() },
                { "vIdGenero", ddlIdGenero.SelectedValue },
                { "vIdTiempoDisponible", ddlIdTiempoDisponible.SelectedValue },
                { "vHoraMinima", horaInicio },
                { "vHoraMaxima", horaFin },
                { "vLugarTrabajo", txtLugarTrabajo.Text.Trim() },
                { "vHasEstudiantes", chkEstudiante.Checked ? "1" : "" },
                { "vHasEgresados", chkEgresado.Checked ? "1" : "" },
                { "vHasAvisoPrivacidad", chkAvisoPrivacidad.Checked ? "1" : "" },
            };

            List<string> listaValidacion = validacionSolicitud.Validar(diccionario);
            if (listaValidacion == null) listaValidacion = new List<string>();

            // Validaciones sobre las listas de sesión
            DataTable dtCarrera = ObtenerLista(SES_CARRERA);
            DataTable dtMunicipio = ObtenerLista(SES_MUNICIPIO);
            DataTable dtSemestre = ObtenerLista(SES_SEMESTRE);
            DataTable dtExperiencia = ObtenerLista(SES_EXPERIENCIA);
            DataTable dtIdioma = ObtenerLista(SES_IDIOMA);
            DataTable dtActividad = ObtenerLista(SES_ACTIVIDAD);

            if (dtCarrera.Rows.Count == 0)
                listaValidacion.Add("Alerta: Se espera registrar al menos una Carrera");

            if (dtMunicipio.Rows.Count == 0)
                listaValidacion.Add("Alerta: Se espera registrar al menos un Municipio");

            if (chkEstudiante.Checked && dtSemestre.Rows.Count == 0)
                listaValidacion.Add("Alerta: Se espera registrar al menos un Semestre para Estudiantes");

            if (dtActividad.Rows.Count == 0)
                listaValidacion.Add("Alerta: Se espera registrar al menos una Actividad y/o función");

            // Horario coherente
            if (!string.IsNullOrEmpty(horaInicio) && !string.IsNullOrEmpty(horaFin) &&
                string.CompareOrdinal(horaInicio, horaFin) >= 0)
            {
                listaValidacion.Add("Alerta: La Hora de Inicio debe ser menor que la Hora de Fin");
            }

            if (listaValidacion.Count > 0)
            {
                MostrarMensaje(listaValidacion[0], "warning", null);
                return;
            }

            // Concatena las actividades en el campo único Actividades
            string actividades = string.Join(Environment.NewLine,
                dtActividad.AsEnumerable().Select(r => r.Field<string>("Actividad")));

            // Cabecera de la solicitud
            bdSolicitud parametro = new bdSolicitud
            {
                IdEmpresa = IdEmpresaSesion(),
                IdGenero = int.Parse(ddlIdGenero.SelectedValue),
                IdTiempoDisponible = int.Parse(ddlIdTiempoDisponible.SelectedValue),
                NombrePuesto = txtNombrePuesto.Text.Trim().ToUpper(),
                LugarTrabajo = txtLugarTrabajo.Text.Trim().ToUpper(),
                Direccion = direccion.ToUpper(),
                Actividades = string.IsNullOrWhiteSpace(actividades) ? null : actividades,
                HasEstudiantes = chkEstudiante.Checked,
                HasEgresados = chkEgresado.Checked,
                MostrarSueldo = chkMostrarSueldo.Checked,
                HasAvisoPrivacidad = chkAvisoPrivacidad.Checked,
                NumVacantes = int.Parse(txtNumVacantes.Text.Trim()),
                EdadMinima = int.Parse(hfEdadMinima.Value),
                EdadMaxima = int.Parse(hfEdadMaxima.Value),
                HoraMinima = TimeSpan.Parse(horaInicio),
                HoraMaxima = TimeSpan.Parse(horaFin),
                SueldoMinimo = decimal.Parse(hfSueldoMinimo.Value, CultureInfo.InvariantCulture),
                SueldoMaximo = decimal.Parse(hfSueldoMaximo.Value, CultureInfo.InvariantCulture),
                UsuarioRegistro = IdUsuarioSesion(),
            };

            Tuple<string, string> control = logicaSolicitud.Crear(parametro);

            if (control.Item2 != "success" || parametro.IdSolicitud <= 0)
            {
                MostrarMensaje(control.Item1, string.IsNullOrEmpty(control.Item2) ? "error" : control.Item2, null);
                return;
            }

            int idSolicitud = parametro.IdSolicitud;

            // Detalle -> tablas hijas
            foreach (DataRow r in dtCarrera.Rows)
                logicaCarreraSolicitud.Crear(new bdCarreraSolicitud { IdSolicitud = idSolicitud, IdCarrera = (int)r["IdCarrera"] });

            foreach (DataRow r in dtMunicipio.Rows)
                logicaMunicipioSolicitud.Crear(new bdMunicipioSolicitud { IdSolicitud = idSolicitud, IdMunicipio = (int)r["IdMunicipio"] });

            foreach (DataRow r in dtSemestre.Rows)
                logicaSemestreSolicitud.Crear(new bdSemestreSolicitud { IdSolicitud = idSolicitud, IdSemestre = (int)r["IdSemestre"] });

            foreach (DataRow r in dtExperiencia.Rows)
                logicaExperienciaSolicitud.Crear(new bdExperienciaSolicitud
                {
                    IdSolicitud = idSolicitud,
                    IdAreaInteres = (int)r["IdAreaInteres"],
                    Anios = (decimal)r["Anios"]
                });

            foreach (DataRow r in dtIdioma.Rows)
                logicaIdiomaSolicitud.Crear(new bdIdiomaSolicitud
                {
                    IdSolicitud = idSolicitud,
                    IdIdioma = (int)r["IdIdioma"],
                    IdNivelIdioma = (int)r["IdNivelIdioma"]
                });

            LimpiarListas();

            Session["MensajePendiente"] = control.Item1;
            Session["TipoMensaje"] = control.Item2;

            Response.Redirect("PanelSolicitudes.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        #endregion
    }
}
