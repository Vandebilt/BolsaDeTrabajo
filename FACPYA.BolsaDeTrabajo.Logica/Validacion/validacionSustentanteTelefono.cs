using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionSustentanteTelefono
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion catalogo = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!catalogo.EsNumeroMayorACero(parametrosValidacion, "vIdTipoTelefono"))
            {
                lista.Add("Se espera seleccionar un tipo de teléfono");
            }

            if (!catalogo.ContieneValor(parametrosValidacion, "vTelefono"))
            {
                lista.Add("Se espera escribir un teléfono");
            }

            return lista;
        }
    }
}
