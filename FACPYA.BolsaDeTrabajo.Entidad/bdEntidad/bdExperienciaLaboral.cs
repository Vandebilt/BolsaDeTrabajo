using System;

namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdExperienciaLaboral
    {
        public int IdExperienciaLaboral { get; set; }
        public int IdSustentante { get; set; }
        public string Puesto { get; set; }
        public string Empresa { get; set; }
        public string TipoTrabajo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Descripcion { get; set; }


    }
   
}
