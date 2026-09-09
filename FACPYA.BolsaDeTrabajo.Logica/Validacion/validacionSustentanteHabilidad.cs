using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionSustentanteHabilidad
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion catalogo = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!catalogo.EsNumeroMayorACero(parametrosValidacion, "vIdTipoHabilidad"))
            {
                lista.Add("Se espera seleccionar un tipo de habilidad");
            }

            if (!catalogo.EsNumeroMayorACero(parametrosValidacion, "vIdHabilidad"))
            {
                lista.Add("Se espera seleccionar una habilidad");
            }

            return lista;
        }
    }
}
