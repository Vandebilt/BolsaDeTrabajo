using System;

namespace FACPYA.BolsaDeTrabajo.Entidad.viewModels
{
    public class vmExpedienteAcademico
    {
        public string EstatusAcademico { get; set; }
        public string TipoGrado { get; set; }
        public string Carrera { get; set; }
        public string Semestre { get; set; }
        public string TurnoEscolar { get; set; }
        public string EstatusTitulacion { get; set; }
        public decimal? Promedio {  get; set; }
        public DateTime? AnioIngreso { get; set; }
        public DateTime? AnioEgreso { get; set; }
    }
}
