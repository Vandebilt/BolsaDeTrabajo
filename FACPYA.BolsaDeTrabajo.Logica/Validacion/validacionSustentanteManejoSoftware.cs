using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionSustentanteManejoSoftware
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion catalogo = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!catalogo.EsNumeroMayorACero(parametrosValidacion, "vIdSoftware"))
            {
                lista.Add("Se espera seleccionar un paquete de software");
            }

            if (!catalogo.EsNumeroMayorACero(parametrosValidacion, "vIdNivelSoftware"))
            {
                lista.Add("Se espera seleccionar un nivel de manejo de software");
            }

            return lista;
        }
    }
}
