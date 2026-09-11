using System;

namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdSolicitud
    {
        public int IdSolicitud { get; set; }
        public int IdEstatus { get; set; }
        public int IdEmpresa { get; set; }

        // Datos de la Empresa (usados en LeerEmpresa)
        public string Nombre { get; set; }
        public string PuestoNombreContacto { get; set; }

        // Datos de la Solicitud
        public int IdGenero { get; set; }
        public int IdTiempoDisponible { get; set; }
        public string NombrePuesto { get; set; }
        public string LugarTrabajo { get; set; }
        public string Direccion { get; set; }
        public string Actividades { get; set; }
        public bool? HasEstudiantes { get; set; }
        public bool? HasEgresados { get; set; }
        public bool? MostrarSueldo { get; set; }
        public bool HasAvisoPrivacidad { get; set; }
        public int NumVacantes { get; set; }
        public int EdadMinima { get; set; }
        public int EdadMaxima { get; set; }
        public TimeSpan HoraMinima { get; set; }
        public TimeSpan HoraMaxima { get; set; }
        public decimal SueldoMinimo { get; set; }
        public decimal SueldoMaximo { get; set; }

        // Auditoría
        public int UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int UsuarioBaja { get; set; }
        public DateTime? FechaBaja { get; set; }

    }
}
