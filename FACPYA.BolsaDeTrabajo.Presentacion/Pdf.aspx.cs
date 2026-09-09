using System;

namespace FACPYA.BolsaDeTrabajo.Presentacion
{
    public partial class Pdf : System.Web.UI.Page
    {
        // Función para obtener la ruta del archivo PDF desde la sesión
        protected string ArchivoPdf()
        {
            string ArchivoPdf = Session["ArchivoPdf"].ToString();
            return ArchivoPdf;
        }

        // Función para obtener la ruta del archivo de imagen desde la sesión
        protected string ArchivoImagen()
        {
            string Archivo = Session["NombreImagen"].ToString();
            return Archivo;
        }

        // Evento Page_Load que se ejecuta cuando se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ArchivoPdf"] != null)
            {
                string rutaPdf = ArchivoPdf(); // Se obtiene la ruta desde la función ArchivoPdf()

                // Verificar si la ruta no es nula o vacía y si el archivo existe antes de intentar transmitirlo
                if (!string.IsNullOrEmpty(rutaPdf) && System.IO.File.Exists(rutaPdf))
                {
                    Response.ContentType = "application/pdf";
                    Response.AppendHeader("Content-Disposition", "inline; filename=archivo.pdf");
                    Response.TransmitFile(rutaPdf);
                    Response.End();
                    Session["ArchivoPdf"] = null; // Correcto
                }
            }
            else if (Session["NombreImagen"] != null)
            {
                // Ruta de la imagen
                string physicalFileName = ArchivoImagen();

                // Cargar la imagen original
                // Configurar el tipo de contenido (asegúrate que coincida con el archivo)
                Response.ContentType = "image/jpeg";

                // Enviar el archivo físico directamente al stream de respuesta
                Response.WriteFile(physicalFileName);

                // Finalizar la respuesta
                Response.End();

                // ¡AQUÍ ESTÁ LA CORRECCIÓN!
                Session["NombreImagen"] = null;
            }
        }
    }
}