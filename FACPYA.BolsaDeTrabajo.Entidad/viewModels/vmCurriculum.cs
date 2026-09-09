using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Entidad.viewModels
{
    public class vmCurriculum
    {
        public List<vmExpedienteAcademico> ExpedienteAcademico { get; set; }
        public List<vmExperienciaLaboral> ExperienciaLaboral { get; set; }
        public vmSustentante Sustentante { get; set; }
        public string Correo { get; set; }
        public List<vmTelefono> Telefonos { get; set; }
        public List<vmHabilidad> Habilidades { get; set; }
        public List<vmIdioma> Idiomas { get; set; }
        public List<vmManejoSoftware> ManejoSoftware { get; set; }
        public List<vmCertificado> Certificados { get; set; }
        public string Imagen { get; set; }
    }
}
