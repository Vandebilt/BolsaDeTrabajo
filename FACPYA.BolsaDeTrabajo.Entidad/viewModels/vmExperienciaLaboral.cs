using System;
using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Entidad.viewModels
{
    public class vmExperienciaLaboral
    {
        public string Estatus { get; set; }
        public string Puesto { get; set; }
        public string Empresa { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public List<string> Responsabilidades { get; set; }
    }
}
