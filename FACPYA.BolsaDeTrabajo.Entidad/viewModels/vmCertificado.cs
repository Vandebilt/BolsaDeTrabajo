using System;

namespace FACPYA.BolsaDeTrabajo.Entidad.viewModels
{
    public class vmCertificado
    {
        public string Descripcion { get; set; }
        public string InstitucionEmisora { get; set; }
        public DateTime FechaEmision { get; set; }
        public string NumeroCertificado { get; set; }
        public string UrlVerificacion { get; set; }
        public string ArchivoCertificado { get; set; }
    }
}
