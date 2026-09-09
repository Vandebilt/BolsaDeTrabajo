using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionSustentanteExperienciaLaboral
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.ContieneValor(parametrosValidacion, "vPuesto"))
            {
                lista.Add("Alerta: Se espera escribir un puesto");
            }
            if (!validacion.ContieneValor(parametrosValidacion, "vEmpresa"))
            {
                lista.Add("Alerta: Se espera escribir una empresa");
            }
            if (!validacion.ContieneValor(parametrosValidacion, "vFechaInicio"))
            {
                lista.Add("Alerta: Se espera seleccionar una fecha inicio");
            }
            if (!validacion.ContieneValor(parametrosValidacion, "vTipoTrabajo"))
            {
                lista.Add("Alerta: Se espera escribir el tipo de trabajo");
            }
            return lista;
        }
    }
}
