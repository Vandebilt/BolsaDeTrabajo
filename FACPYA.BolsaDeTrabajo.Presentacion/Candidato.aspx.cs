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
    public partial class Sustentante : System.Web.UI.Page
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
        private int IdSustentanteSesion()
        {
            if (Session["IdSustentante"] == null)
                return 0; // o puedes lanzar excepción, según tu lógica

            int id;

            // Intenta convertir de forma segura
            if (int.TryParse(Session["IdSustentante"].ToString(), out id))
                return id;

            return 0; // si falla la conversión
        }

        //RETORNA RUTA DE CERTIFICADO
        public string RetornarRutaCertificado()
        {
            List<bdRuta> ruta = logicaRuta.LeerRuta(3);
            return ruta.Count > 0 ? ruta[0].Ruta : string.Empty;
        }

        private void AbrirModal(string ventana)
        {
            string script = $"<script type='text/javascript'>$(document).ready(function() {{ abrirModal('{ventana}'); }});</script>";
            ScriptManager.RegisterStartupScript(this, GetType(), "AbrirModal", script, false);
        }


        protected List<bdVerSustentante> GridViewCorreo()
        {
            var lista = logicaVerSustentante.ConsultarCorreo(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaCorreo.Visible = true;
                gvConsultaCorreo.DataSource = lista;
                gvConsultaCorreo.DataBind();
            }
            else
            {
                gvConsultaCorreo.Visible = false;
            }

            return lista;
        }

        protected List<bdVerSustentante> GridViewTelefono()
        {
            var lista = logicaVerSustentante.ConsultarTelefono(IdSustentanteSesion());

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

        protected List<bdVerSustentante> GridViewAreaExperiencia()
        {
            var lista = logicaVerSustentante.ConsultarAreaExperiencia(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaAreaExperiencia.Visible = true;
                gvConsultaAreaExperiencia.DataSource = lista;
                gvConsultaAreaExperiencia.DataBind();

            }
            else
            {
                SeccAreaExperiencia.Visible = false;
            }

            return lista;
        }
        protected List<bdVerSustentante> GridViewAreaInteres()
        {
            var lista = logicaVerSustentante.ConsultarAreaInteres(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaAreaInteres.Visible = true;
                gvConsultaAreaInteres.DataSource = lista;
                gvConsultaAreaInteres.DataBind();

            }
            else
            {
                SeccAreaInteres.Visible = false;
            }

            return lista;
        }
        protected List<bdVerSustentante> GridViewHabilidad()
        {
            var lista = logicaVerSustentante.ConsultarHabilidad(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaHabilidad.Visible = true;
                gvConsultaHabilidad.DataSource = lista;
                gvConsultaHabilidad.DataBind();

            }
            else
            {
                SeccHabilidad.Visible = false;
            }

            return lista;
        }
        protected List<bdVerSustentante> GridViewSoftware()
        {
            var lista = logicaVerSustentante.ConsultarSoftware(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaSoftware.Visible = true;
                gvConsultaSoftware.DataSource = lista;
                gvConsultaSoftware.DataBind();

            }
            else
            {
                SeccSoftware.Visible = false;
            }

            return lista;
        }
        protected List<bdVerSustentante> GridViewIdioma()
        {
            var lista = logicaVerSustentante.ConsultarIdioma(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaIdioma.Visible = true;
                gvConsultaIdioma.DataSource = lista;
                gvConsultaIdioma.DataBind();

            }
            else
            {
                SeccIdioma.Visible = false;
            }

            return lista;
        }

        protected List<bdVerSustentante> GridViewCertificacionIdioma()
        {
            var lista = logicaVerSustentante.ConsultarCertificacionIdioma(IdSustentanteSesion());

            if (lista.Count > 0)
            {
                gvConsultaCertificacionIdioma.Visible = true;
                gvConsultaCertificacionIdioma.DataSource = lista;
                gvConsultaCertificacionIdioma.DataBind();

            }
            else
            {
                SeccCertificacionIdioma.Visible = false;
            }

            return lista;
        }
        protected List<bdVerSustentante> GridViewDocumento()
        {
            var lista = logicaVerSustentante.ConsultarDocumento(IdSustentanteSesion());

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

        protected void LeerSustentante()
        {
            int id = IdSustentanteSesion(); // <-- AQUÍ recuperas el ID


            List<bdVerSustentante> lista = logicaVerSustentante.Leer(id);

            if (lista.Count > 0)
            {
                lblNombre.Text = lista[0].Nombre.ToString();
                lblPrimerApellido.Text = lista[0].PrimerApellido.ToString();
                lblSegundoApellido.Text = lista[0].SegundoApellido.ToString();

                if (lista[0].FechaNacimiento.HasValue)
                {
                    lblFechaNacimiento.Text = lista[0].FechaNacimiento.Value
                        .ToString("dd 'de' MMMM 'del' yyyy", new System.Globalization.CultureInfo("es-ES"));
                }
                else
                {
                    lblFechaNacimiento.Text = "";
                }

                lblGenero.Text = lista[0].Genero.ToString();
                lblEstadoCivil.Text = lista[0].EstadoCivil.ToString();
                lblNacionalidad.Text = lista[0].Nacionalidad.ToString();
                lblMunicipio.Text = lista[0].Municipio.ToString();
                lblColonia.Text = lista[0].Colonia.ToString();
                lblCalle.Text = lista[0].Calle.ToString();
                lblNumeroCasa.Text = lista[0].NumeroCasa.ToString();
                lblSueldoDeseado.Text = lista[0].SueldoDeseado.ToString();
                lblHorarioDisponible.Text = lista[0].HorarioDisponible.ToString();

                //// Suponiendo que esto es lo que trae la lista:
                //string horarioBruto = lista[0].HorarioDisponible.ToString();
                //// Ej: "05:05:00:000 - 17:04:00:000"

                //var partes = horarioBruto.Split('-');
                //string inicioStr = partes[0].Trim();
                //string finStr = partes[1].Trim();

                //// Aceptamos varios formatos posibles
                //string[] formatos = { "HH:mm:ss:fff", "HH:mm:ss.fff", "HH:mm:ss" };

                //DateTime horaInicio = DateTime.ParseExact(inicioStr, formatos, CultureInfo.InvariantCulture, DateTimeStyles.None);
                //DateTime horaFin = DateTime.ParseExact(finStr, formatos, CultureInfo.InvariantCulture, DateTimeStyles.None);

                //// Formato 5:05 am - 5:04 pm
                //lblHorarioDisponible.Text = $"{horaInicio:h:mm tt} - {horaFin:h:mm tt}".ToLower();
                lblTrabaja.Text = lista[0].TrabajaActualmente.ToString();
                litBiografia.Text = lista[0].Biografia.ToString();


                string ruta = lista[0].RutaImagenPerfil;

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

                lblCorreoPrincipal.Text = lista[0].Correo.ToString();
                lblTelefonoPrincipal.Text = lista[0].Telefono.ToString();
                if (!string.IsNullOrWhiteSpace(lista[0].LinkedinUrl))
                {
                    hlLinkedin.Text = "ENLACE";
                    hlLinkedin.NavigateUrl = lista[0].LinkedinUrl;
                    hlLinkedin.Visible = true;
                }
                else
                {
                    hlLinkedin.Visible = false; // Ocultar si no hay URL
                }

                GridViewCorreo();
                GridViewTelefono();
                GridViewAreaExperiencia();
                GridViewAreaInteres();
                GridViewHabilidad();
                GridViewSoftware();
                GridViewIdioma();
                GridViewCertificacionIdioma();

                lblIdTipoSustentante.Text = lista[0].TipoSustentante.ToString();
                lblMatricula.Text = lista[0].Matricula.ToString();
                lblIdTipoGrado.Text = lista[0].TipoGrado.ToString();
                lblIdCarrera.Text = lista[0].Carrera.ToString();

                if (lista[0].TipoSustentante.ToString() == "ESTUDIANTE")
                {
                    phEstudiante.Visible = true;
                    lblIdSemestre.Text = lista[0].Semestre.ToString();
                    lblIdTurnoEscolar.Text = lista[0].TurnoEscolar.ToString();
                    lblIdPlanEstudios.Text = lista[0].Plan.ToString();
                    lblIdModalidad.Text = lista[0].Modalidad.ToString();
                    lblServicioSocial.Text = lista[0].HasServicioSocial.ToString();
                    lblPracticasProfesionales.Text = lista[0].HasPracticasProfesionales.ToString();
                }
                else if (lista[0].TipoSustentante.ToString() == "EGRESADO")
                {
                    phEgresado.Visible = true;
                    lblAnioIngreso.Text = DateTime.Parse(lista[0].AnioIngreso).Year.ToString();
                    lblAnioEgreso.Text = DateTime.Parse(lista[0].AnioEgreso).Year.ToString();
                    lblIdEstatusAcademico.Text = lista[0].EstatusAcademico.ToString();
                    lblIdEstatusTitulacion.Text = lista[0].EstatusTitulacion.ToString();

                }

                lblPromedio.Text = lista[0].Promedio.ToString();
                lblOtraCarrera.Text = lista[0].OtraCarrera.ToString();

                CargarExperienciaLaboral();
                CargarCertificados();
                GridViewDocumento();

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // Es importante ejecutar esto solo la primera vez que carga la página
            if (!IsPostBack)
            {
                if (Request.QueryString["IdSustentante"] != null)
                {
                    string idDelSustentante = Request.QueryString["IdSustentante"].ToString();

                }
                else
                {

                }

                LeerSustentante();
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

        public string FormatearFechaFin(object fechaFinObj)
        {
            if (fechaFinObj == null || fechaFinObj == DBNull.Value)
                return "Presente";

            DateTime fechaFin;

            if (DateTime.TryParse(fechaFinObj.ToString(), out fechaFin))
                return fechaFin.ToString("dd/MM/yyyy");

            // Si no se puede parsear, regresa el valor tal cual
            return fechaFinObj.ToString();
        }



        private void CargarExperienciaLaboral()
        {
            // Ahora recibe un DataTable
            DataTable datosExperiencia = logicaVerSustentante.ConsultarExperienciaLaboral(IdSustentanteSesion());

            // 2. Asigna el DataTable directamente
            lvExperiencia.DataSource = datosExperiencia;

            // 3. ¡El paso más importante! "Dibuja" los datos en el HTML
            lvExperiencia.DataBind();
        }

        private void CargarCertificados()
        {
            // Ahora recibe un DataTable
            DataTable datosCertificado = logicaVerSustentante.ConsultarCertificado(IdSustentanteSesion());

            // 2. Asigna el DataTable directamente
            lvCertificado.DataSource = datosCertificado;

            // 3. ¡El paso más importante! "Dibuja" los datos en el HTML
            lvCertificado.DataBind();
        }

        protected string FormatearDescripcionComoLista(object descripcionObj)
        {
            string json = descripcionObj as string;

            // Si la descripción está vacía o no es un JSON, no hagas nada.
            if (string.IsNullOrEmpty(json) || !json.Trim().StartsWith("{"))
            {
                // Devuelve el texto original (pero "codificado" para HTML por seguridad)
                return HttpUtility.HtmlEncode(json);
            }

            try
            {
                // 1. Usamos el deserializador de JSON que viene con ASP.NET
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var data = serializer.Deserialize<Dictionary<string, object>>(json);

                // 2. Buscamos la llave "responsibilities"
                if (data != null && data.ContainsKey("RESPONSIBILITIES"))
                {
                    var responsabilidades = data["RESPONSIBILITIES"] as System.Collections.ArrayList;

                    if (responsabilidades != null && responsabilidades.Count > 0)
                    {
                        // 3. Construimos el HTML de la lista (<ul>)
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<ul class='mb-0'>"); // mb-0 quita el margen inferior

                        foreach (object item in responsabilidades)
                        {
                            // Usamos HtmlEncode por seguridad, por si el texto tiene < o >
                            sb.AppendFormat("<li>{0}</li>", HttpUtility.HtmlEncode(item.ToString()));
                        }
                        sb.Append("</ul>");

                        return sb.ToString();
                    }
                }

                // Si el JSON no tiene el formato esperado, devuelve el JSON tal cual
                return HttpUtility.HtmlEncode(json);
            }
            catch (Exception)
            {
                // Si falla el parsing, solo devuelve el texto original
                return HttpUtility.HtmlEncode(json);
            }
        }
        public string TokenActual { get; set; }
        protected void btnCargarCv_Click(object sender, EventArgs e)
        {
            // 1. Intentamos recuperar el ID
            int idSustentante = IdSustentanteSesion();

            // 2. Si devuelve 0 (o -1), significa que la sesión expiró
            if (idSustentante <= 0)
            {
                // Opcional: Mostrar mensaje de "Tu sesión ha expirado"

                // Redirigir al Login
                Response.Redirect("~/Login.aspx");
                return; // Detenemos la ejecución aquí para que no intente generar el token
            }

            // 3. Si la sesión está viva, continuamos normal
            string token = new TokenHelper(ConfigurationManager.AppSettings["jwtSecretKey"])
                               .GenerarToken("token_sustentante", new { id = idSustentante });

            Response.Redirect($"~/Curriculum.aspx?token={token}");
        }

        protected void btnVerCertificado_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Recuperar el botón
                LinkButton btn = (LinkButton)sender;

                // 2. Recuperar el ID del argumento (Este ID es el de la tabla de Certificados)
                string idCertificado = btn.CommandArgument.ToString();

                // 3. Consultar a la Base de Datos para obtener el nombre del archivo real
                // NOTA: Aquí debes usar tu lógica de negocio para traer el registro del certificado por su ID.
                // Estoy asumiendo que tienes un método similar a este:
                var lista = logicaSustentanteCertificado.Leer(idCertificado);

                if (lista != null && !string.IsNullOrEmpty(lista[0].ArchivoCertificado))
                {
                    string nombreArchivo = lista[0].ArchivoCertificado;

                    // 4. Construir la ruta (Asumo que los certificados se guardan en una ruta específica)
                    // Si usas la misma ruta de documentos general:
                    string RutaBase = RetornarRutaCertificado();
                    string rutaCompleta = RutaBase + nombreArchivo;

                    // 5. Pasar a sesión y abrir el visor (Lógica para PDF)
                    Session["ArchivoPdf"] = rutaCompleta;

                    // Apuntar el iframe al visor
                    iframeContenido.Attributes["src"] = "Pdf.aspx";

                    // Abrir el modal (asegúrate de que el ID del modal sea el correcto para ver documentos)
                    AbrirModal("#ModalNuevo");
                }
                else
                {
                    // Opcional: Mostrar mensaje si no se encuentra el archivo en BD
                    // MostrarAlerta("No se encontró el archivo físico.");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine(ex.Message);
            }
        }

        protected string ValidarDestino(object urlObj)
        {
            string url = Convert.ToString(urlObj);

            // 1. Si está vacío, devolvemos vacío (para ocultar el botón después)
            if (string.IsNullOrEmpty(url)) return "";

            // 2. CASO EXTERNO (http/https)
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                // Solo verificamos que la URL tenga formato válido
                bool esUrlValida = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (!esUrlValida)
                {
                    // El texto no es un link válido -> Mandar a tu 404
                    return ResolveUrl("~/404.aspx");
                }

                // Si el formato es válido, lo dejamos pasar.
                // NOTA: Si la página externa está caída, el navegador mostrará el error de esa página, no el tuyo.
                return url;
            }

            // 3. CASO LOCAL (Archivos en tu servidor)
            else
            {
                try
                {
                    // Verificamos si existe físicamente
                    string rutaFisica = Server.MapPath(url);
                    if (!System.IO.File.Exists(rutaFisica))
                    {
                        // Archivo local borrado o no encontrado -> Mandar a tu 404
                        return ResolveUrl("~/404.aspx");
                    }
                    return url;
                }
                catch
                {
                    // Si la ruta local tiene caracteres ilegales -> Mandar a tu 404
                    return ResolveUrl("~/404.aspx");
                }
            }
        }
    }
}
