using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionFormacionSustentante
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            
            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vTipoGrado"))
            {
                lista.Add("Alerta: Se espera seleccionar una tipo de grado");
            }
            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vCarrera"))
            {
                lista.Add("Alerta: Se espera seleccionar una carrera");
            }
            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vEstatusAcademico"))
            {
                lista.Add("Alerta: Se espera seleccionar un estatus académico");
            }
            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vEstatusTitulacion"))
            {
                lista.Add("Alerta: Se espera seleccionar un estatus de titulación");
            }
            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vAnioIngreso"))
            {
                lista.Add("Alerta: Se espera seleccionar un año de ingreso");
            }
            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vAnioEgreso"))
            {
                lista.Add("Alerta: Se espera seleccionar un año de egreso");
            }

          


            return lista;

        }
    }
}
