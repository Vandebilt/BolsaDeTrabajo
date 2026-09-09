using ClosedXML.Excel;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class ListadoSustentante : System.Web.UI.Page
    {
        // Variable global para determinar la acción a realizar en el botón "Grabar"
        private int vpId;

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

            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", script, true);
        }


        // Limpiar formulario
        protected void LimpiarFormulario()
        {
            ddlIdBusquedaIdTipoSustentante.ClearSelection();
            ddlIdBusquedaIdAreaExperiencia.ClearSelection();
            ddlBusquedaIdCarrera.ClearSelection();
            txtBusquedaNombre.Text = string.Empty;
            txtBusquedaFolio.Text = string.Empty;
            txtBusquedaAniosExperiencia.Text = string.Empty;
            vpId = 0;
        }
        #endregion

        #region Invocar funciones públicas
        // Instancia de la pantantalla maestra para el uso de funciones públicas
        Inicio master = new Inicio();

        // Lógica para CargarDropDownLists()
        protected void CargarDropDownLists()
        {
            master.CargarDropDownList(ddlIdBusquedaIdTipoSustentante, 15, null);
            master.CargarDropDownList(ddlIdBusquedaIdAreaExperiencia, 6, null);
            master.CargarDropDownList(ddlBusquedaIdCarrera, 12, null);
        }

        // Lógica para BindSustentantes()
        private void BindSustentantes(int? pIdTipoSustentante, string pNombre, string pFolio, int? pIdAreaExperiencia, string pAnios, int? pIdCarrera)
        {
            try
            {
                // Llama a tu clase de lógica estática
                List<DataRow> listaSustentantes = logicaListadoSustentante.Consultar(pIdTipoSustentante, pNombre, pFolio, pIdAreaExperiencia, pAnios, pIdCarrera);

                // Comprueba si la lista tiene datos
                if (listaSustentantes.Count > 0)
                {
                    DataTable dtSustentantes = listaSustentantes.CopyToDataTable();

                    lvSustentantes.Visible = true;

                    // Enlaza los datos al ListView
                    lvSustentantes.DataSource = dtSustentantes;
                    lvSustentantes.DataBind();
                }
                else
                {
                    MostrarMensaje("No se ha encontrado información con los parámetros de búsqueda seleccionados", "info", null);
                    lvSustentantes.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                // Si hay un error, enlazamos datos vacíos para mostrar el EmptyDataTemplate
                lvSustentantes.DataSource = null;
                lvSustentantes.DataBind();
            }
        }

        // Método para la paginación (el que te daba error antes)
        protected void lvSustentantes_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            // Establecemos las nuevas propiedades de la página (a qué página vamos)
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

            // Leemos el filtro guardado en la Sesión
            int? pIdTipoSustentante = Session["FiltroSustentante"] as int?;
            string pNombre = Session["FiltroSustentante"] as string;
            string pFolio = Session["FiltroSustentante"] as string;
            int? pIdAreaExperiencia = Session["FiltroSustentante"] as int?;
            string pAnios = Session["FiltroSustentante"] as string;
            int? pIdCarrera = Session["FiltroSustentante"] as int?;


            // Volvemos a cargar los datos, pero esta vez respetando el filtro
            BindSustentantes(pIdTipoSustentante, pNombre, pFolio, pIdAreaExperiencia, pAnios, pIdCarrera);
        }

        // Método para los comandos
        protected void lvSustentantes_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                // Obtenemos el ID del sustentante desde el CommandArgument
                string sustentanteId = e.CommandArgument.ToString();

                // Guardamos el ID en la sesión para su uso posterior
                Session["IdSustentante"] = sustentanteId;

                // Encriptamos el ID para pasarlo como token en la URL
                byte[] idBytes = Encoding.UTF8.GetBytes(sustentanteId);
                byte[] encryptedBytes = MachineKey.Protect(idBytes, "VerIdSustentante"); // Usa una "frase secreta"
                string token = Convert.ToBase64String(encryptedBytes);

                // 1. Primero construimos la URL y la guardamos en una variable string
                string urlDestino = $"Candidato.aspx?token={token}";

                // 2. Asignamos esa string a la Session
                Session["RutaSustentante"] = urlDestino;

                // 3. Finalmente hacemos la redirección usando esa string
                Response.Redirect(urlDestino, false);
                Context.ApplicationInstance.CompleteRequest(); // Recomendado al usar 'false' para terminar la ejecución limpiamente

            }
        }

        // Método para convertir la ruta física de la imagen a Base64
        protected string ConvertirRutaAImagenBase64(object rutaFisica)
        {
            // Validar que la ruta no sea nula o vacía y que el archivo exista
            try
            {
                string ruta = rutaFisica.ToString();
                if (string.IsNullOrEmpty(ruta) || !File.Exists(ruta))
                {
                    return ""; // <-- AQUÍ SE SALE SI NO LA ENCUENTRA
                }

                // Determinar el tipo de imagen (MIME type) por la extensión
                string extension = Path.GetExtension(ruta).ToLowerInvariant();
                string mimeType = "image/jpeg"; // Default

                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg":
                        mimeType = "image/jpeg";
                        break;
                    case ".png":
                        mimeType = "image/png";
                        break;
                        // ... (otros case si se necesita)
                }

                // Leer el archivo y convertirlo a Base64
                // Lee todos los bytes (la data cruda) de la imagen
                byte[] imageBytes = File.ReadAllBytes(ruta);

                // Convierte esa data cruda en un texto larguísimo (Base64)
                string base64String = Convert.ToBase64String(imageBytes);

                // Devolver el string con el formato "Data URI"
                // Esto es lo que entiende el <img src="...">
                return string.Format("data:{0};base64,{1}", mimeType, base64String);
            }
            catch (Exception ex)
            {
                return ""; // Si algo falla en la conversión, tampoco truena
            }
        }

        // Método para leer la información del sustentante
        protected void LeerSustentante(int pIdSustentante)
        {
            // Llamada a la lógica para obtener los datos del sustentante
            List<bdValidacionSustentante> lista = logicaValidacionSustentante.Leer(pIdSustentante);
        }

        #endregion

        // Carga de Pagina
        protected void Page_Load(object sender, EventArgs e)
        {
            // Carga de elementos principales dentro del módulo
            if (!IsPostBack)
            {
                // Cargar los DropDownLists con la información de la base de datos
                CargarDropDownLists();
                Session["FiltroSustentante"] = null;

                // Cargar el ListView con todos los sustentantes al inicio
                BindSustentantes(null, null, null, null, null, null);

                // Limpiar el formulario de búsqueda
                Session.Remove("sesionFiltro");
                vpId = 0;
            }
        }

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

        protected void btnLinkedIn_Click(object sender, EventArgs e)
        {

        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // 1. Obtener el valor del filtro (descomentado)
            int? pIdTipoSustentante = int.TryParse(ddlIdBusquedaIdTipoSustentante.SelectedValue, out int are) && are != 0 ? (int?)are : null;
            string pNombre = string.IsNullOrEmpty(txtBusquedaNombre.Text.Trim()) ? null : txtBusquedaNombre.Text.Trim();
            string pFolio = string.IsNullOrEmpty(txtBusquedaFolio.Text.Trim()) ? null : txtBusquedaFolio.Text.Trim();
            int? pIdAreaExperiencia = int.TryParse(ddlIdBusquedaIdAreaExperiencia.SelectedValue, out int area) && area != 0 ? (int?)area : null;
            string pAnios = string.IsNullOrEmpty(txtBusquedaAniosExperiencia.Text.Trim()) ? null : txtBusquedaAniosExperiencia.Text.Trim();
            int? pIdCarrera = int.TryParse(ddlBusquedaIdCarrera.SelectedValue, out int car) && car != 0 ? (int?)car : null;

            // 2. Guardar el filtro en la Sesión para que la paginación lo recuerde
            Session["FiltroSustentante"] = pIdTipoSustentante;
            Session["FiltroSustentante"] = pNombre;
            Session["FiltroSustentante"] = pFolio;
            Session["FiltroSustentante"] = pIdAreaExperiencia;
            Session["FiltroSustentante"] = pAnios;
            Session["FiltroSustentante"] = pIdCarrera;

            // 3. IMPORTANTE: Reiniciar la paginación a la primera página
            //    Si filtras y te quedas en la página 5 (y solo hay 2 resultados), dará error.
            DataPager1.SetPageProperties(0, DataPager1.MaximumRows, true);

            // 4. Llamar al método de enlace con el filtro aplicado
            //    Tu método BindSustentantes ya se encarga de enlazar los datos al ListView (lvSustentantes)
            BindSustentantes(pIdTipoSustentante, pNombre, pFolio, pIdAreaExperiencia, pAnios, pIdCarrera);
        }

        // Limpiar
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            BindSustentantes(null, null, null, null, null, null);
            lvSustentantes.Visible = true;
            Session.Remove("sesionFiltro");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Requerido para evitar el error de tiempo de ejecución
            // "El control 'GridView' debe colocarse dentro de una etiqueta de formulario con runat=server".
            // además dentro del diseño de la pantalla (.aspx) debe colocarse la propiedad 'EnableEventValidation="false" en la eqtiqueta principal de asp'
        }


        protected void btnExportar_Click(object sender, EventArgs e)
        {
            // 1. Obtener el valor del filtro (se mantiene exactamente igual)
            int? pIdTipoSustentante = int.TryParse(ddlIdBusquedaIdTipoSustentante.SelectedValue, out int are) && are != 0 ? (int?)are : null;
            string pNombre = string.IsNullOrEmpty(txtBusquedaNombre.Text.Trim()) ? null : txtBusquedaNombre.Text.Trim();
            string pFolio = string.IsNullOrEmpty(txtBusquedaFolio.Text.Trim()) ? null : txtBusquedaFolio.Text.Trim();
            int? pIdAreaExperiencia = int.TryParse(ddlIdBusquedaIdAreaExperiencia.SelectedValue, out int area) && area != 0 ? (int?)area : null;
            string pAnios = string.IsNullOrEmpty(txtBusquedaAniosExperiencia.Text.Trim()) ? null : txtBusquedaAniosExperiencia.Text.Trim();
            int? pIdCarrera = int.TryParse(ddlBusquedaIdCarrera.SelectedValue, out int car) && car != 0 ? (int?)car : null;

            // 2. Obtener los datos filtrados y limpios de la base de datos
            DataTable dataTable = logicaListadoSustentante.ExportarExcel(pIdTipoSustentante, pNombre, pFolio, pIdAreaExperiencia, pAnios, pIdCarrera);

            // 3. Definir la ruta física de la plantilla en el servidor web
            // Cambia "~/Formatos/MiPlantilla.xlsx" por la ubicación real de tu archivo dentro del proyecto
            string rutaPlantilla = Server.MapPath("~/recursos/FDEVI02-03  Candidatos.xlsx");

            if (!File.Exists(rutaPlantilla))
            {
                // Opcional: Manejar el error si el archivo de plantilla no se encuentra
                return;
            }

            // 4. Cargar y procesar la plantilla con ClosedXML
            using (var workbook = new XLWorkbook(rutaPlantilla))
            {
                // Seleccionar la primera hoja
                var worksheet = workbook.Worksheet(1);

                int filaEncabezados = 3; // Fila donde se encuentran tus headers estáticos
                int filaActual = filaEncabezados + 1; // Los datos comienzan justo abajo

                // Mapear los encabezados de la fila 3: NombreHeader -> NumeroColumna
                var columnasExcel = worksheet.Row(filaEncabezados).CellsUsed()
                    .ToDictionary(
                        c => c.GetString().Trim(),
                        c => c.Address.ColumnNumber,
                        StringComparer.OrdinalIgnoreCase // Evita problemas de mayúsculas/minúsculas
                    );

                // Vaciar el DataTable haciendo match con las columnas mapeadas
                foreach (DataRow fila in dataTable.Rows)
                {
                    foreach (DataColumn columna in dataTable.Columns)
                    {
                        if (columnasExcel.TryGetValue(columna.ColumnName.Trim(), out int numeroColumnaExcel))
                        {
                            worksheet.Cell(filaActual, numeroColumnaExcel).Value = fila[columna]?.ToString() ?? "";
                        }
                    }
                    filaActual++;
                }

               
                if (filaActual > 4)
                {
                    worksheet.Columns().AdjustToContents(4, filaActual - 1);
                }

                if (filaActual > 3)
                {
                    worksheet.Columns().AdjustToContents(3, filaActual - 1);
                }

                // 5. Preparar la descarga HTTP para el navegador web
                string nombreArchivo = "REPORTE CANDIDATOS ACTIVOS " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                // Guardar el libro en memoria primero
                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    byte[] excelBytes = memoryStream.ToArray(); // Extraer los bytes puros

                    // Limpiar absolutamente todo rastro de HTML previo
                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();

                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", $"attachment;filename=\"{nombreArchivo}\"");
                    Response.AddHeader("Content-Length", excelBytes.Length.ToString()); // Excel agradece saber el tamaño exacto

                    // Escribir los bytes puros
                    Response.BinaryWrite(excelBytes);

                    Response.Flush();

                    // Esta línea es la clave mágica: le dice a ASP.NET que ignore el resto del HTML de la página
                    Response.SuppressContent = true;

                    // Finalizar la petición de forma segura
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }

        // Busqueda por Area de Experiencia
        protected void ddlIdBusquedaIdAreaExperiencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBusquedaAniosExperiencia.Enabled = true;
            DataPager1.SetPageProperties(0, DataPager1.MaximumRows, true);
        }
    }
}