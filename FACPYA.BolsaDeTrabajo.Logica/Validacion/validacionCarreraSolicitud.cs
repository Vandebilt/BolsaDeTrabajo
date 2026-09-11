using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionCarreraSolicitud
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vIdCarrera"))
            {
                lista.Add("Alerta: Se espera seleccionar una Carrera");
            }

            return lista;
        }
    }
}
