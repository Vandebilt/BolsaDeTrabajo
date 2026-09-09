using System;

namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdSustentanteCertificado
    {
        public string IdCertificadoSustentante { get; set; }
        public int IdEstatus { get; set; }
        public int IdSustentante { get; set; }
        public string Descripcion { get; set; }
        public string InstitucionEmisora { get; set; }
        public DateTime FechaEmision { get; set; }
        public string NumeroCertificado { get; set; }
        public string UrlVerificacion { get; set; }
        public string ArchivoCertificado { get; set; }

    }
}
