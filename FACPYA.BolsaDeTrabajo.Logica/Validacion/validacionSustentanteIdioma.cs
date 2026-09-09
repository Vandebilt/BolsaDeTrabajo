using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionSustentanteIdioma
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion catalogo = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!catalogo.EsNumeroMayorACero(parametrosValidacion, "vIdIdioma"))
            {
                lista.Add("Se espera seleccionar un idioma");
            }

            if (!catalogo.EsNumeroMayorACero(parametrosValidacion, "vIdNivelIdioma"))
            {
                lista.Add("Se espera seleccionar un nivel de idioma");
            }

            return lista;
        }
    }
}
