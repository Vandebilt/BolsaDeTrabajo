using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionSolicitud
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.ContieneValor(parametrosValidacion, "vNombrePuesto"))
            {
                lista.Add("Alerta: Se espera escribir el Nombre del Puesto");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vDireccion"))
            {
                lista.Add("Alerta: Se espera escribir la Dirección");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vNumVacantes"))
            {
                lista.Add("Alerta: Se espera indicar el Número de Vacantes");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vIdGenero"))
            {
                lista.Add("Alerta: Se espera seleccionar un Sexo");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vIdTiempoDisponible"))
            {
                lista.Add("Alerta: Se espera seleccionar un Horario");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vHoraMinima"))
            {
                lista.Add("Alerta: Se espera seleccionar la Hora de Inicio");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vHoraMaxima"))
            {
                lista.Add("Alerta: Se espera seleccionar la Hora de Fin");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vLugarTrabajo"))
            {
                lista.Add("Alerta: Se espera escribir el Lugar de Trabajo");
            }

            // Estado del candidato: al menos uno (Estudiante / Egresado)
            if (!validacion.ContieneValor(parametrosValidacion, "vHasEstudiantes")
                && !validacion.ContieneValor(parametrosValidacion, "vHasEgresados"))
            {
                lista.Add("Alerta: Se espera indicar el Estado del candidato (Estudiante y/o Egresado)");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vHasAvisoPrivacidad"))
            {
                lista.Add("Alerta: Para continuar, debes aceptar el Aviso de Privacidad");
            }

            return lista;
        }
    }
}
