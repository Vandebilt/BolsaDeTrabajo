using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Logica;
using FACPYA.BolsaDeTrabajo.Logica.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class Empresa : System.Web.UI.Page
    {

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

        private int IdEmpresaSesion()
        {
            if (Session["IdEmpresa"] == null)
                return 0; // o puedes lanzar excepción, según tu lógica

            int id;

            // Intenta convertir de forma segura
            if (int.TryParse(Session["IdEmpresa"].ToString(), out id))
                return id;

            return 0; // si falla la conversión
        }

        private void AbrirModal(string ventana)
        {
            string script = $"<script type='text/javascript'>$(document).ready(function() {{ abrirModal('{ventana}'); }});</script>";
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", script, false);
        }


        protected List<bdVerEmpresa> GridViewTelefono()
        {
            var lista = logicaVerEmpresa.ConsultarTelefono(IdEmpresaSesion());

            if (lista.Count > 0)
            {
                gvConsultaTelefono.Visible = true;
                gvConsultaTelefono.DataSource = lista;
                gvConsultaTelefono.DataBind();

            }
            else
            {
                gvConsultaTelefono.Visible = false;
            }

            return lista;
        }

        protected List<bdVerEmpresa> GridViewDocumento()
        {
            var lista = logicaVerEmpresa.ConsultarDocumento(IdEmpresaSesion());

            if (lista.Count > 0)
            {
                gvConsultaDocumento.Visible = true;
                gvConsultaDocumento.DataSource = lista;
                gvConsultaDocumento.DataBind();

            }
            else
            {
                gvConsultaDocumento.Visible = false;
            }

            return lista;
        }

        protected void LeerEmpresa()
        {
            int id = IdEmpresaSesion(); // <-- AQUÍ recuperas el ID


            List<bdVerEmpresa> lista = logicaVerEmpresa.Leer(id);

            if (lista.Count > 0)
            {
                lblNombre.Text = lista[0].Nombre.ToString();
                lblGiro.Text = lista[0].Giro.ToString();
                lblTamanioEmpresa.Text = lista[0].TamanioEmpresa.ToString();
                lblTipoEmpresa.Text = lista[0].TipoEmpresa.ToString();
                lblDireccion.Text = lista[0].Direccion.ToString();
                lblCorreo.Text = lista[0].Correo.ToString();
                lblPaginaWeb.Text = lista[0].PaginaWeb.ToString();
                lblNombreContacto.Text = lista[0].ContactoNombre.ToString();
                lblPuestoContacto.Text = lista[0].ContactoPuesto.ToString();
                lblMision.Text = lista[0].Mision.ToString();
                lblVision.Text = lista[0].Vision.ToString();
                lblRegimenGastosMedicos.Text = lista[0].RegimenGastosMedicos?.ToString() ?? "N/A";
                string ruta = lista[0].RutaLogotipo;

                if (File.Exists(ruta))
                {
                    // Detectar MIME
                    string mime = MimeMapping.GetMimeMapping(ruta);

                    byte[] bytes = File.ReadAllBytes(ruta);
                    string base64 = Convert.ToBase64String(bytes);

                    string srcParaImagen = $"data:{mime};base64,{base64}";

                    imgFotoPerfil.ImageUrl = srcParaImagen; // <--- AQUÍ SE ASIGNA A LA IMAGEN
                }
                else
                {
                    imgFotoPerfil.ImageUrl = "~/Images/sin-foto.png"; // backup si no existe
                }

                GridViewTelefono();
                GridViewDocumento();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            // Es importante ejecutar esto solo la primera vez que carga la página
            if (!IsPostBack)
            {
                if (Request.QueryString["IdEmpresa"] != null)
                {
                    string idDeEmpresa = Request.QueryString["IdEmpresa"].ToString();

                }
                else
                {

                }

                LeerEmpresa();
            }
        }

        protected void gvConsultaDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string rutaDocumento = (e.CommandArgument).ToString();

            if (e.CommandName == "Ver")
            {
                Session["ArchivoPdf"] = rutaDocumento;
                iframeContenido.Attributes["src"] = "Pdf.aspx";
                AbrirModal("#ModalNuevo");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "closeModal", "$('#ModalNuevo').modal('hide');", true);

            }
        }
    }
}
