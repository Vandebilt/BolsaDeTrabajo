using System.Collections.Generic;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionArea
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.ContieneValor(parametrosValidacion, "vArea"))
            {
                lista.Add("Alerta: Se espera escribir un Area");
            }

            return lista;
        }
    }
}
