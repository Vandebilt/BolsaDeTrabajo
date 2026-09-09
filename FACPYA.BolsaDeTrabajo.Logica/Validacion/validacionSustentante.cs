using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionSustentante
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.ContieneValor(parametrosValidacion, "vNombre"))
            {
                lista.Add("Alerta: Se espera escribir un nombre");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vPrimerApellido"))
            {
                lista.Add("Alerta: Se espera escribir un apellido parteno");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vFechaNacimiento"))
            {
                lista.Add("Alerta: Se espera seleccionar una fecha de nacimiento");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vGenero"))
            {
                lista.Add("Alerta: Se espera seleccionar un sexo");
            }
            
            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vEstadoCivil"))
            {
                lista.Add("Alerta: Se espera seleccionar un estado civil");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vNacionalidad"))
            {
                lista.Add("Alerta: Se espera escribir una nacionalidad");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vMunicipio"))
            {
                lista.Add("Alerta: Se espera seleccionar un municipio");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vColonia"))
            {
                lista.Add("Alerta: Se espera escribir una colonia");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vCalle"))
            {
                lista.Add("Alerta: Se espera escribir una calle");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vNumeroDeCasa"))
            {
                lista.Add("Alerta: Se espera escribir un número de casa");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vTiempoDisponible"))
            {
                lista.Add("Alerta: Se espera seleccionar un horario disponible");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vSueldoDeseado"))
            {
                lista.Add("Alerta: Se espera escribir un sueldo deseado");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vBiografia"))
            {
                lista.Add("Alerta: Se espera escribir una biografía");
            }

            return lista;

        }
    }
}
