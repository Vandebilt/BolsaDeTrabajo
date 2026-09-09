using System.Collections.Generic;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionIdioma
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.ContieneValor(parametrosValidacion, "vClave"))
            {
                lista.Add("Alerta: Se espera escribir una clave");
            }
            if (!validacion.ContieneValor(parametrosValidacion, "vIdioma"))
            {
                lista.Add("Alerta: Se espera escribir un idioma");
            }


            return lista;
        }
    }
}
