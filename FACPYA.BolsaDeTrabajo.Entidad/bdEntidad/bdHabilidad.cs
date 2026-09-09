namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdHabilidad
    {
        public int IdHabilidad {  get; set; }
        public int IdEstatus { get; set; }
        public int? IdTipoHabilidad { get; set; }
        public string Descripcion { get; set; }
        public string Habilidad { get; set; }
        public int UsuarioRegistro { get; set; }
        public int UsuarioModificacion { get; set; }
        public int UsuarioBaja { get; set; }    
    }
}
