namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdEmpresaArchivo
    {
        public int IdArchivoEmpresa { get; set; }
        public int IdTipoArchivo { get; set; }
        public int IdEmpresa { get; set; }
        public int IdUsuarioAsignacion { get; set; }
        public int IdTipoRechazo { get; set; }
        public int IdEstatus { get; set; }
        public string NombreDocumento { get; set; }
        public string RetroAlimentacion { get; set; }
    }
}
