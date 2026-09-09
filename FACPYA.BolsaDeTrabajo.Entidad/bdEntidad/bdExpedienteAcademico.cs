using System;

namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdExpedienteAcademico
    {
        public int IdExpedienteAcademico { get; set; }
        public int IdSustentante { get; set; }
        public int IdTipoSustentante { get; set; }
        public string Matricula { get; set; }
        public int IdTipoGrado { get; set; }
        public int IdCarrera { get; set; }
        public int? IdSemestre { get; set; }
        public int? IdPlanEstudio { get; set; }
        public int? IdModalidad { get; set; }
        public int? IdTurnoEscolar { get; set; }
        public DateTime? AnioIngreso { get; set; }
        public DateTime? AnioEgreso { get; set; }
        public int? IdEstatusAcademico { get; set; }
        public int? IdEstatusTitulacion { get; set; }
        public bool HasServicioSocial { get; set; }
        public bool HasPracticasProfesionales { get; set; }
        public decimal? Promedio { get; set; }
        public string OtraCarrera { get; set; }

    }
}
