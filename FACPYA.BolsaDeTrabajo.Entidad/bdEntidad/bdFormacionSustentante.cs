using System;

namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdFormacionSustentante
    {
        public int IdFormacionSustentante { get; set; }
        public int IdSustentante { get; set; }
        public int IdTipoGrado { get; set; }
        public int IdCarrera { get; set; }
        public int IdEstatusAcademico { get; set; }
        public int IdEstatusTitulacion { get; set; }
        public string OtraCarrera { get; set; }
        public DateTime? AnioIngreso { get; set; }
        public DateTime? AnioEgreso { get; set; }
        public decimal Promedio { get; set; }
    }
}
