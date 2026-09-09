namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdGradoSustentante
    {
        public int IdGradoSustentante { get; set; }
        public int IdSustentante { get; set; }
        public int IdCarrera { get; set; }
        public int IdSemestre { get; set; }
        public int IdTurnoEscolar { get; set; }
        public string OtraCarrera { get; set; }
        public string Matricula { get; set; }
        public decimal Promedio { get; set; }
    }
}
