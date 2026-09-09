namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdPaqueteSoftware
    {
        public int IdPaqueteSoftware { get; set; }
        public int IdEstatus { get; set; }
        public string PaqueteSoftware { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioRegistro { get; set; }
        public int UsuarioModificacion { get; set; }
        public int UsuarioBaja { get; set; }
    }
}
