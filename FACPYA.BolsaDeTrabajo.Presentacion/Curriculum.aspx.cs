using FACPYA.BolsaDeTrabajo.Entidad.viewModels;
using FACPYA.BolsaDeTrabajo.Logica;
using FACPYA.BolsaDeTrabajo.Logica.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class Curriculum : System.Web.UI.Page
    {
        private string path = HttpRuntime.AppDomainAppPath;
        private TokenHelper _tokenHelper = new TokenHelper(ConfigurationManager.AppSettings["jwtSecretKey"]);
        private int _idUsuario;

        public string TokenActual { get; set; }

        // Establece la ruta de la imagen en el atributo src de la etiqueta img
        private string ConvertImageToBase64(string pRuta)
        {
            byte[] bytes = File.ReadAllBytes(pRuta);
            // Convierte el array de bytes en una cadena base64
            string base64String = Convert.ToBase64String(bytes);
            return $"data:image/jpeg;base64,{base64String}";
        }

        // Prepara la plantilla de Experiencias en base a la lista de Experiencia Laboral
        private string GenerarExperienciasLaborales(List<vmExperienciaLaboral> pExperiencias)
        {
            // 1. VALIDACIÓN DE SEGURIDAD
            // Si la lista es nula o vacía, retornamos vacío inmediatamente.
            // Esto hace que en el HTML no aparezca NI el título NI la sección.
            if (pExperiencias == null || pExperiencias.Count == 0)
            {
                return string.Empty;
            }

            // Usamos StringBuilder para ir sumando los trabajos (es más rápido y correcto)
            System.Text.StringBuilder trabajosAcumulados = new System.Text.StringBuilder();

            // 2. LEER PLANTILLA (Solo una vez fuera del loop para no alentar el sistema)
            string rawPlantilla = File.ReadAllText($"{path}/Plantillas/Experiencia.htm");

            // 3. ORDENAMIENTO (Ponemos los trabajos "Actuales" primero, y luego los más recientes)
            var listaOrdenada = pExperiencias
                                .OrderByDescending(x => x.FechaFin == null) // Primero los nulos (Actuales)
                                .ThenByDescending(x => x.FechaFin);         // Luego por fecha

            foreach (var experiencia in listaOrdenada)
            {
                // Copiamos la plantilla virgen para usarla en este ítem
                string plantillaItem = rawPlantilla;

                // Lógica de fechas
                string fechaFin = (experiencia.FechaFin == null) ? "Actual" : Convert.ToDateTime(experiencia.FechaFin).ToShortDateString();
                string fechaInicio = experiencia.FechaInicio.ToShortDateString();

                // Reemplazos básicos
                plantillaItem = plantillaItem.Replace("{company}", experiencia.Empresa);
                plantillaItem = plantillaItem.Replace("{role}", experiencia.Puesto);
                plantillaItem = plantillaItem.Replace("{status}", experiencia.Estatus ?? ""); // Manejo de nulls en estatus
                plantillaItem = plantillaItem.Replace("{start_date}", fechaInicio);
                plantillaItem = plantillaItem.Replace("{end_date}", fechaFin);


                // Lógica de Responsabilidades
                if (experiencia.Responsabilidades != null && experiencia.Responsabilidades.Count > 0)
                {
                    plantillaItem = plantillaItem.Replace("{responsability_display}", "block");

                    // Construimos los <li> iterando la lista
                    string responsabilidadesHtml = string.Join("", experiencia.Responsabilidades.Select(r => $"<li>{r}</li>"));

                    plantillaItem = plantillaItem.Replace("{responsibilities}", responsabilidadesHtml);
                }
                else
                {
                    plantillaItem = plantillaItem.Replace("{responsability_display}", "none");
                    plantillaItem = plantillaItem.Replace("{responsibilities}", "");
                }

                // ACUMULAMOS (Antes tenías 'trabajos = plantilla', eso borraba lo anterior)
                trabajosAcumulados.Append(plantillaItem);
            }

            // 4. CONSTRUCCIÓN FINAL DEL BLOQUE
            // Aquí es donde envolvemos todo en el section y el h2.
            // Como ya pasamos la validación inicial, sabemos que hay datos.
            string resultadoFinal = $@"
            <section class=""mb-2"">
                <h2 id=""experiencia"" class=""section-title"">
                    EXPERIENCIA PROFESIONAL
                </h2>
                {trabajosAcumulados.ToString()}
            </section>";

            return resultadoFinal;
        }

        // Genera la sección de Educación en base a la historia academica del sustentante
        private string GenerarEducacion(List<vmExpedienteAcademico> pExpedienteAcademico)
        {
            string listaEducacion = string.Empty;

            if (pExpedienteAcademico != null)
            {
                // 
                foreach (var educacion in pExpedienteAcademico)
                {
                    string plantilla = File.ReadAllText($"{path}/Plantillas/Educacion.htm");

                    string turnoOEstatusAcademico = (string.IsNullOrEmpty(educacion.TurnoEscolar)) ? educacion.EstatusAcademico : $"TURNO {educacion.TurnoEscolar}";
                    string semestreOEStatusTitulacion = (string.IsNullOrEmpty(educacion.Semestre)) ? $"ESTADO TITULACIÓN {educacion.EstatusTitulacion}" : $"{educacion.Semestre} SEMESTRE";
                    string promedio = string.Empty;
                    string fechas = string.Empty;

                    if (educacion.Promedio != null)
                        promedio = $"PROMEDIO: {educacion.Promedio}";

                    if (educacion.AnioIngreso != null && educacion.AnioEgreso != null)
                        fechas = $"{Convert.ToDateTime(educacion.AnioIngreso).Year} - {Convert.ToDateTime(educacion.AnioEgreso).Year}";


                    plantilla = plantilla.Replace("{status_academic_education}", turnoOEstatusAcademico);
                    plantilla = plantilla.Replace("{educational_career}", $"{educacion.TipoGrado} EN {educacion.Carrera}");
                    plantilla = plantilla.Replace("{semester}", semestreOEStatusTitulacion);
                    plantilla = plantilla.Replace("{average}", promedio);
                    plantilla = plantilla.Replace("{dates}", fechas);

                    listaEducacion += plantilla;
                }

            }

            return listaEducacion;
        }

        private string GenerarHabilidades(List<vmHabilidad> pHabilidades)
        {
            // 1. VALIDACIÓN: Si no hay habilidades, no mostramos nada (ni título).
            if (pHabilidades == null || pHabilidades.Count == 0)
            {
                return string.Empty;
            }

            // Agrupación de datos (Tu lógica original se mantiene)
            var listaHabilidades = new Dictionary<string, List<string>>();
            pHabilidades.ForEach(habilidad =>
            {
                if (!listaHabilidades.ContainsKey(habilidad.TipoHabilidad))
                {
                    listaHabilidades[habilidad.TipoHabilidad] = new List<string>();
                }
                listaHabilidades[habilidad.TipoHabilidad].Add(habilidad.Habilidad);
            });

            // Usamos StringBuilder para construir el HTML interno
            System.Text.StringBuilder gridContent = new System.Text.StringBuilder();

            // Leemos la plantilla UNA sola vez fuera del bucle
            string rawPlantilla = File.ReadAllText($"{path}/Plantillas/Habilidades.htm");

            gridContent.Append("<div class=\"grid\">");

            foreach (var grupo in listaHabilidades)
            {
                string plantillaItem = rawPlantilla;
                plantillaItem = plantillaItem.Replace("{type}", grupo.Key);

                // Construimos los <li> de este grupo
                System.Text.StringBuilder skillsBuilder = new System.Text.StringBuilder();
                foreach (var habilidad in grupo.Value)
                {
                    skillsBuilder.Append($"<li>{habilidad}</li>");
                }

                plantillaItem = plantillaItem.Replace("{skills}", skillsBuilder.ToString());
                gridContent.Append(plantillaItem);
            }

            gridContent.Append("</div>");

            // 2. RETORNO FINAL: Envolvemos todo el grid en la section con su H2
            string resultadoFinal = $@"
            <section class=""mb-2"">
                <h2 class=""section-title"">
                    HABILIDADES
                </h2>
                {gridContent.ToString()}
            </section>";

            return resultadoFinal;
        }

        private string GenerarIdiomas(List<vmIdioma> pIdiomas)
        {
            // 1. VALIDACIÓN: Si no hay idiomas, adiós sección.
            if (pIdiomas == null || pIdiomas.Count == 0)
            {
                return string.Empty;
            }

            System.Text.StringBuilder gridContent = new System.Text.StringBuilder();
            string rawPlantilla = File.ReadAllText($"{path}/Plantillas/Idiomas.htm");

            gridContent.Append("<div class=\"grid\">");

            foreach (var idioma in pIdiomas)
            {
                string plantillaItem = rawPlantilla;
                plantillaItem = plantillaItem.Replace("{language_description}", idioma.Idioma);
                plantillaItem = plantillaItem.Replace("{language_level}", idioma.NivelIdioma);

                gridContent.Append(plantillaItem);
            }

            gridContent.Append("</div>");

            // 2. RETORNO FINAL con título
            string resultadoFinal = $@"
            <section class=""mb-2"">
                <h2 class=""section-title"">
                    IDIOMAS
                </h2>
                {gridContent.ToString()}
            </section>";

            return resultadoFinal;
        }

        private string GenerarCertificados(List<vmCertificado> pCertificados)
        {
            // 1. VALIDACIÓN: Si no hay certificados, retornamos vacío.
            // Esto asegura que desaparezca el título H2 de la vista final.
            if (pCertificados == null || pCertificados.Count == 0)
            {
                return string.Empty;
            }

            System.Text.StringBuilder certificadosAcumulados = new System.Text.StringBuilder();

            // Leemos la plantilla UNA sola vez fuera del bucle (Optimización)
            string rawPlantilla = File.ReadAllText($"{path}/Plantillas/Certificados.htm");

            foreach (var certificado in pCertificados)
            {
                string plantillaItem = rawPlantilla;

                plantillaItem = plantillaItem.Replace("{certificate_institution}", certificado.InstitucionEmisora);
                plantillaItem = plantillaItem.Replace("{certificate_description}", certificado.Descripcion);
                plantillaItem = plantillaItem.Replace("{certificate_key}", certificado.NumeroCertificado);

                // Validamos la fecha por si acaso viene nula, aunque sea struct
                plantillaItem = plantillaItem.Replace("{certificate_date}", certificado.FechaEmision.ToShortDateString());

                certificadosAcumulados.Append(plantillaItem);
            }

            // 2. CONSTRUCCIÓN DEL BLOQUE
            // Aquí definimos el título y la sección. Solo se ejecuta si hay datos.
            string resultadoFinal = $@"
    <section class=""mb-2"">
        <h2 class=""section-title"">
            CERTIFICADOS
        </h2>
        {certificadosAcumulados.ToString()}
    </section>";

            return resultadoFinal;
        }

        // Obtiene la plantilla e incrusta los datos del usuario en la estructura del CV
        private string GenerarCV()
        {
            string plantilla = File.ReadAllText($"{path}/Plantillas/Curriculum.htm");

            // Encontrar usuario
            vmCurriculum curriculum = logicaSustentante.ObtenerCurriculum(_idUsuario);

            plantilla = plantilla.Replace("{image}",
                (!string.IsNullOrEmpty(curriculum.Imagen)) ? ConvertImageToBase64(curriculum.Imagen) : ""
            );
            plantilla = plantilla.Replace("{name}", curriculum.Sustentante.Nombre);
            plantilla = plantilla.Replace("{address}", curriculum.Sustentante.Colonia);
            plantilla = plantilla.Replace("{phone}", curriculum.Telefonos[0].Telefono);
            plantilla = plantilla.Replace("{email}", curriculum.Correo);
            plantilla = plantilla.Replace("{summary}", curriculum.Sustentante.Biografia);
            plantilla = plantilla.Replace("{linkedin_display}", (string.IsNullOrEmpty(curriculum.Sustentante.LinkedinUrl)) ? "none" : "block");
            plantilla = plantilla.Replace("{linkedin_url}", curriculum.Sustentante.LinkedinUrl);
            plantilla = plantilla.Replace("{bloque_experiencia_completo}", GenerarExperienciasLaborales(curriculum.ExperienciaLaboral));
            plantilla = plantilla.Replace("{education}", GenerarEducacion(curriculum.ExpedienteAcademico));
            plantilla = plantilla.Replace("{bloque_habilidades_completo}", GenerarHabilidades(curriculum.Habilidades));
            plantilla = plantilla.Replace("{bloque_idiomas_completo}", GenerarIdiomas(curriculum.Idiomas));
            plantilla = plantilla.Replace("{bloque_certificados_completo}", GenerarCertificados(curriculum.Certificados));

            return plantilla;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Acceder al parámetro IdUsuario
                if (Request.QueryString["token"] != null)
                {
                    ClaimsPrincipal token = _tokenHelper.ValidarToken(Request.QueryString["token"]);
                    if (token != null)
                    {
                        // Convierte el string JSON en una cadena JSON pura
                        // Desmonta el jwt y lo trasnforma en un Object any
                        var data = JsonConvert.DeserializeObject<dynamic>(token.FindFirst("data").Value);
                        // Asigna a _idUsuario el ID del sustentante almacenado en el token
                        _idUsuario = (int)data.id;
                        // Genera el CV
                        template_cv.InnerHtml = GenerarCV();
                    }
                }
            }
        }
    }
}