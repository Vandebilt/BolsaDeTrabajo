using System;

namespace FACPYA.BolsaDeTrabajo.Entidad.bdEntidad
{
    public class bdEmpresa
    {
       public int IdEmpresa { get; set; }
       public int IdEstatus { get; set; }
       public int IdUsuario { get; set; }
       public int IdTipoEmpresa { get; set; }
       public string Folio { get; set; }
       public string Nombre { get; set; }
       public string Giro { get; set; }
       public int IdTamanioEmpresa { get; set; }
       public string Direccion { get; set; }
       public string Correo { get; set; }
       public string PaginaWeb { get; set; }
       public string ContactoNombre { get; set; }
       public string ContactoPuesto { get; set; }
       public string Mision { get; set; }
       public string Vision { get; set; }
       public string RegimenGastosMedicos { get; set; }
       public bool HasAvisoPrivacidad { get; set; }
       public int UsuarioRegistro { get; set; }
       public int UsuarioModificacion { get; set; }
       public string Logotipo { get; set; }
       public int IdEstatusImg { get; set; }
       public int IdArchivoEmpresa { get; set; }
       public string Estatus { get; set; }
       public string RetroPerfil { get; set; }

            
   
    }
}
