using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class Inicio : System.Web.UI.MasterPage
    {
        #region Funciones públicas
        //Carga de información en los DropDownList
        public void CargarDropDownList(DropDownList pDropDownList, int pAccion, int? pIdFiltro)
        {
            var catalogoDatos = logicaCatalogo.ddlCargaCatalogo(pAccion, pIdFiltro);

            pDropDownList.DataValueField = "IdCatalogo";
            pDropDownList.DataTextField = "Descripcion";
            pDropDownList.DataSource = catalogoDatos;
            pDropDownList.DataBind();

            pDropDownList.Items.Insert(0, new ListItem("Seleccione", "0"));
        }

        // Carga de información en los GridView
        public void CargarGridView(List<DataRow> lista, GridView pGridView)
        {
            if (lista.Count > 0)
            {
                DataTable dataTable = lista[0].Table.Clone(); // Evita la duplicidad

                foreach (DataRow row in lista)
                {
                    dataTable.ImportRow(row);
                }

                pGridView.DataSource = dataTable;
                pGridView.DataBind();
            }
        }

        // Abrir modal
        private void AbrirModal(string ventana)
        {
            string script = $"<script type='text/javascript'>$(document).ready(function() {{ abrirModal('{ventana}'); }});</script>";
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", script, false);
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            // Obtiene el nombre de la paginas y bloquea el acceso a quien quiera acceder por medio de la url
            string cadena = HttpContext.Current.Request.CurrentExecutionFilePath;
            string[] separado = cadena.Split('/');
            string pagina = separado[separado.Length - 1];

            // No almacena la cache de la sesión, evita que al cerrar sesión pueda regresar
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            // Obtiene el valor del usuario ingresado y la página web a la que intenta acceder
            bdUsuario vSesion = (bdUsuario)Session["usuario"];
            //string pagina = HttpContext.Current.Request.Url.Segments.LastOrDefault();

            // Comprobar que exista una sesion iniciada
            if (Session["usuario"] == null)
            {
                Response.Write("<script>alert('Procedimiento restringido, favor de iniciar sesión')</script>");
                Response.Write("<script>setTimeout(\"location.href='Login.aspx'\",0)</script>");
            } // Responsable de Indicador / Capturista
            else if (Session["usuario"] != null)
            {
                // ADMINISTRADOR
                if (vSesion.IdRol == 1)
                {
                    lnkManual.Visible = true;
                    liGrupoCandidato.Visible = true;
                    liGrupoEmpresa.Visible = true;
                    liGrupoMod.Visible = true;

                    // Bloquea acceso a paginas de diferente rol
                    if (pagina != "ValidacionCandidato.aspx" &&
                        pagina != "ListadoCandidato.aspx" &&
                        pagina != "Empresa.aspx" &&
                        pagina != "ValidacionEmpresa.aspx" &&
                        pagina != "ListadoEmpresa.aspx" &&
                        pagina != "Candidato.aspx" && pagina != "Curriculum.aspx" &&
                        pagina != "Area.aspx" && pagina != "Habilidad.aspx" &&
                        pagina != "PaqueteSoftware.aspx" &&
                        pagina != "Idioma.aspx" &&
                        pagina != "CertificacionIdioma.aspx")
                    {
                        Response.Write("<script>alert('Lo sentimos, no cuenta con permisos para esta sección')</script>");
                        Response.Write("<script>setTimeout(\"location.href='ListadoCandidato.aspx'\",0)</script>");
                    }
                }
                // SUSTENTANTE
                else if (vSesion.IdRol == 2)
                {
                    liPerfilCandidato.Visible = true;

                    // Bloquea acceso a paginas de diferente rol
                    if (pagina != "PerfilCandidato.aspx")
                    {
                        Response.Write("<script>alert('Lo sentimos, no cuenta con permisos para esta sección')</script>");
                        Response.Write("<script>setTimeout(\"location.href='PerfilCandidato.aspx'\",0)</script>");
                    }
                }
                // SUBADMINISTRADOR
                else if (vSesion.IdRol == 3)
                {
                    liListadoCandidato.Visible = true;
                    lnkManual.Visible = true;

                    // Bloquea acceso a paginas de diferente rol
                    if (pagina != "ListadoCandidato.aspx" && pagina != "Candidato.aspx" && pagina != "Curriculum.aspx" && pagina != "Area.aspx" && pagina != "Habilidad.aspx" && pagina != "PaqueteSoftware.aspx" && pagina != "Idioma.aspx")
                    {
                        Response.Write("<script>alert('Lo sentimos, no cuenta con permisos para esta sección')</script>");
                        Response.Write("<script>setTimeout(\"location.href='ListadoCandidato.aspx'\",0)</script>");
                    }
                }
                // EMPRESA
                else if (vSesion.IdRol == 4)
                {
                    List<bdEmpresa> lista = logicaEmpresa.Leer(vSesion.IdEmpresa);

                    if (lista[0].IdEstatus == 4)
                    {
                        liPanelEmpresa.Visible = true;
                    }

                    liPerfilEmpresa.Visible = true;

                    // Bloquea acceso a paginas de diferente rol
                    if (pagina != "PerfilEmpresa.aspx" && pagina != "PanelEmpresa.aspx" && pagina != "Vacante.aspx")
                    {
                        Response.Write("<script>alert('Lo sentimos, no cuenta con permisos para esta sección')</script>");
                        Response.Write("<script>setTimeout(\"location.href='liPerfilEmpresa.aspx'\",0)</script>");
                    }
                }
            }

            if (liUsuario.Attributes["class"] != null)
            {
                liUsuario.Attributes["class"] = liUsuario.Attributes["class"].Replace(" active-nav", "");
            }

            if (liPerfilCandidato.Attributes["class"] != null)
            {
                liPerfilCandidato.Attributes["class"] = liPerfilCandidato.Attributes["class"].Replace(" active-nav", "");
            }

            if (liPerfilEmpresa.Attributes["class"] != null)
            {
                liPerfilEmpresa.Attributes["class"] = liPerfilEmpresa.Attributes["class"].Replace(" active-nav", "");
            }

            if (liListadoCandidato.Attributes["class"] != null)
            {
                liListadoCandidato.Attributes["class"] = liListadoCandidato.Attributes["class"].Replace(" active-nav", "");
            }

            if (liValidacionCandidato.Attributes["class"] != null)
            {
                liValidacionCandidato.Attributes["class"] = liValidacionCandidato.Attributes["class"].Replace(" active-nav", "");
            }

            if (liPanelEmpresa.Attributes["class"] != null)
            {
                liPanelEmpresa.Attributes["class"] = liPanelEmpresa.Attributes["class"].Replace(" active-nav", "");
            }

            if (liArea.Attributes["class"] != null)
            {
                liArea.Attributes["class"] = liArea.Attributes["class"].Replace(" active-nav", "");
            }

            if (liHabilidad.Attributes["class"] != null)
            {
                liHabilidad.Attributes["class"] = liHabilidad.Attributes["class"].Replace(" active-nav", "");
            }

            if (liPaqueteSoftware.Attributes["class"] != null)
            {
                liPaqueteSoftware.Attributes["class"] = liPaqueteSoftware.Attributes["class"].Replace(" active-nav", "");
            }

            if (liIdioma.Attributes["class"] != null)
            {
                liIdioma.Attributes["class"] = liIdioma.Attributes["class"].Replace(" active-nav", "");
            }

            if (liGrupoMod.Attributes["class"] != null)
            {
                liGrupoMod.Attributes["class"] = liGrupoMod.Attributes["class"].Replace(" active-nav", "");
            }

            if (liGrupoCandidato.Attributes["class"] != null)
            {
                liGrupoCandidato.Attributes["class"] = liGrupoCandidato.Attributes["class"].Replace(" active-nav", "");
            }

            string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath).ToLower();

            if (currentPage == "usuario.aspx")
            {
                liUsuario.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "perfilcandidato.aspx") // Asegúrate que esté en minúsculas
            {
                liPerfilCandidato.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "perfilempresa.aspx") // Asegúrate que esté en minúsculas
            {
                liPerfilEmpresa.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "panelempresa.aspx") // Asegúrate que esté en minúsculas
            {
                liPanelEmpresa.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "listadocandidato.aspx") // Asegúrate que esté en minúsculas
            {
                liListadoCandidato.Attributes["class"] += " active-nav";
                liGrupoCandidato.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "validacioncandidato.aspx") // Asegúrate que esté en minúsculas
            {
                liValidacionCandidato.Attributes["class"] += " active-nav";
                liGrupoCandidato.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "area.aspx") // Asegúrate que esté en minúsculas
            {
                liArea.Attributes["class"] += " active-nav";
                liGrupoMod.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "habilidad.aspx") // Asegúrate que esté en minúsculas
            {
                liHabilidad.Attributes["class"] += " active-nav";
                liGrupoMod.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "paquetesoftware.aspx") // Asegúrate que esté en minúsculas
            {
                liPaqueteSoftware.Attributes["class"] += " active-nav";
                liGrupoMod.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "idioma.aspx") // Asegúrate que esté en minúsculas
            {
                liIdioma.Attributes["class"] += " active-nav";
                liGrupoMod.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "certificacionidioma.aspx") // Asegúrate que esté en minúsculas
            {
                liCertificacionIdioma.Attributes["class"] += " active-nav";
                liGrupoMod.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "validacionempresa.aspx") // Asegúrate que esté en minúsculas
            {
                liValidacionEmpresa.Attributes["class"] += " active-nav";
                liGrupoEmpresa.Attributes["class"] += " active-nav";
            }

            else if (currentPage == "listadoempresa.aspx") // Asegúrate que esté en minúsculas
            {
                liListadoEmpresa.Attributes["class"] += " active-nav";
                liGrupoEmpresa.Attributes["class"] += " active-nav";
            }

            string rutaPdf = string.Empty;

            switch (vSesion.IdRol)
            {
                case 1: // Administrador
                    rutaPdf = "~/recursos/Manual de usuario Administrador.pdf";
                    break;
            }

            lnkManual.NavigateUrl = ResolveUrl(rutaPdf);
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}