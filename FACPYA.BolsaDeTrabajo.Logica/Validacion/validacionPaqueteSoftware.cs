using System.Collections.Generic;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionPaqueteSoftware
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.ContieneValor(parametrosValidacion, "vPaqueteSoftware"))
            {
                lista.Add("Alerta: Se espera escribir un Paquete de Software");
            }

            return lista;
        }
    }
}
