using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionSustentanteAreaExperiencia
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion catalogo = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!catalogo.EsNumeroMayorACero(parametrosValidacion, "vIdArea"))
            {
                lista.Add("Se espera seleccionar un area");
            }

            // Verificamos si NO contiene años Y TAMBIÉN NO contiene meses
            if (!catalogo.ContieneValor(parametrosValidacion, "vAnios") &&
                !catalogo.ContieneValor(parametrosValidacion, "vMeses"))
            {
                lista.Add("Alerta: Se espera escribir al menos un año o un mes");
            }


            return lista;
        }
    }
}
