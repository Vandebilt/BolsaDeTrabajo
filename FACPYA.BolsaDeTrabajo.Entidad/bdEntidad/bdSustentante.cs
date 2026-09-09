using System;

namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdSustentante
    {
        public int IdSustentante { get; set; }
        public int IdEstatus { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoSustentante { get; set; }
        public int IdGenero { get; set; }
        public int IdEstadoCivil { get; set; }
        public int IdMunicipio { get; set; }
        public int IdTiempoDisponible { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Folio { get; set; }
        public string Nacionalidad { get; set; }
        public string Calle { get; set; }
        public string NumeroCasa { get; set; }
        public string Colonia { get; set; }
        public string HorarioDisponible { get; set; }
        public bool TrabajaActualmente { get; set; }
        public int SueldoDeseado { get; set; }
        public string LinkedinUrl { get; set; }
        public string Biografia { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public byte[] bytesArchivo { get; set; }
        public int IdArchivoSustentante { get; set; }
        public string Fotografia { get; set; }
        public string Titulo { get; set; }
        public string Faltantes { get; set; }

        // Llama datos en string para la gestion de sustentantes
        public string TipoSustentante { get; set; }
        public string Genero { get; set; }
        public string EstadoCivil { get; set; }
        public string Municipio { get; set; }
        public string trabajaActualmente { get; set; }
        public string sueldoDeseado { get; set; }
        public string fechaNacimiento { get; set; }
        public string Edad { get; set; }
        public string Estatus { get; set; }
        public int IdEstatusImg { get; set; }

    }
}
