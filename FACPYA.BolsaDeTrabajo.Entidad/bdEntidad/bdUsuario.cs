namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdUsuario
    {
        public int IdUsuario { get; set; }
        public int IdEstatus { get; set; }
        public int IdRol { get; set; }
        public int IdSustentante { get; set; }
        public int IdEmpresa { get; set; }
        public string Cuenta { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
        public int UsuarioRegistro { get; set; }
        public int UsuarioModificacion { get; set; }
        public int UsuarioBaja { get; set; }
    }
}
