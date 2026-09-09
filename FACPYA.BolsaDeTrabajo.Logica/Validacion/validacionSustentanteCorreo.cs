using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionSustentanteCorreo
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion catalogo = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!catalogo.EsNumeroMayorACero(parametrosValidacion, "vIdTipoCorreo"))
            {
                lista.Add("Se espera seleccionar un tipo de correo");
            }

            if (!catalogo.ContieneValor(parametrosValidacion, "vCorreo"))
            {
                lista.Add("Se espera escribir un correo");
            }

            if (!catalogo.TieneFormatoCorreo(parametrosValidacion, "vCorreo"))
            {
                lista.Add("Se espera escribir un correo válido");
            }

            return lista;
        }
    }
}
