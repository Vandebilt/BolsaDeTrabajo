using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionExperienciaSolicitud
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vIdAreaInteres"))
            {
                lista.Add("Alerta: Se espera seleccionar un Área de experiencia");
            }

            // Se espera al menos un año o un mes
            if (!validacion.ContieneValor(parametrosValidacion, "vAnios") &&
                !validacion.ContieneValor(parametrosValidacion, "vMeses"))
            {
                lista.Add("Alerta: Se espera escribir al menos un año o un mes");
            }

            return lista;
        }
    }
}
