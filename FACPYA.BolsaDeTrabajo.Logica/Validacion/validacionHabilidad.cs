using System.Collections.Generic;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionHabilidad
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vIdTipoHabilidad"))
            {
                lista.Add("Alerta: Se espera seleccionar un tipo de habilidad");
            }
            if (!validacion.ContieneValor(parametrosValidacion, "vHabilidad"))
            {
                lista.Add("Alerta: Se espera escribir una habilidad");
            }
            
            return lista;
        }
    }
}
