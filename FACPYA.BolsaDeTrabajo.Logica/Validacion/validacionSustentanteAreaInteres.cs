using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionSustentanteAreaInteres
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion catalogo = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!catalogo.EsNumeroMayorACero(parametrosValidacion, "vIdAreaInteres"))
            {
                lista.Add("Se espera seleccionar un area de interés");
            }

            return lista;
        }
    }
}
