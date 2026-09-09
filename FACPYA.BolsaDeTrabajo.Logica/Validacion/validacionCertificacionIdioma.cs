using System.Collections.Generic;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionCertificacionIdioma
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.ContieneValor(parametrosValidacion, "vCertificacion"))
            {
                lista.Add("Alerta: Se espera escribir una Certificación");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vIdIdioma"))
            {
                lista.Add("Alerta: Se espera seleccionar un Idioma");
            }

            

            return lista;
        }
    }
}
