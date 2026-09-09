using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionEmpresaDocumento
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();


            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vEstatus"))
            {
                lista.Add("Alerta: Se espera seleccionar una revision");
            }
           

            return lista;

        }
    }
}
