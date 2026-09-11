using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionIdiomaSolicitud
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vIdIdioma"))
            {
                lista.Add("Alerta: Se espera seleccionar un Idioma");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vIdNivelIdioma"))
            {
                lista.Add("Alerta: Se espera seleccionar un Nivel de idioma");
            }

            return lista;
        }
    }
}
