namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdTelefonoSustentante
    {
        public int IdTelefonoSustentante { get; set; }
        public int IdEstatus { get; set; }
        public int IdSustentante { get; set; }
        public int IdTipoTelefono { get; set; }
        public string Telefono { get; set; }
        public string Extension { get; set; }
    }
}
