using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionGradoSustentante
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.ContieneValor(parametrosValidacion, "vMatricula"))
            {
                lista.Add("Alerta: Se espera escribir una matricula");
            }
            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vCarrera"))
            {
                lista.Add("Alerta: Se espera seleccionar una carrera");
            }
            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vSemestre"))
            {
                lista.Add("Alerta: Se espera seleccionar un semestre");
            }
            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vTurnoEscolar"))
            {
                lista.Add("Alerta: Se espera seleccionar un turno escolar");
            }

            return lista;

        }
    }
}
