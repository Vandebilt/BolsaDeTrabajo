using System.Collections.Generic;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionExpedienteAcademico
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vTipoSustentante"))
            {
                lista.Add("Alerta: Se espera seleccionar un Tipo de Sustentante");
                return lista;
            }

            // 1. Primero extraemos el valor (ajusta esta línea dependiendo de cómo funcione tu clase o diccionario)
            string matricula = parametrosValidacion["vMatricula"].ToString();

            // 2. Validamos si está vacío
            if (!validacion.ContieneValor(parametrosValidacion, "vMatricula"))
            {
                lista.Add("Alerta: Se espera escribir una Matrícula.");
            }
            // 3. Si no está vacío, validamos que sean exactamente 7 dígitos
            else if (matricula.Length != 7 || !matricula.All(char.IsDigit))
            {
                lista.Add("Alerta: La matrícula contener 7 números.");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vTipoGrado"))
            {
                lista.Add("Alerta: Se espera seleccionar un Grado");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vCarrera"))
            {
                lista.Add("Alerta: Se espera seleccionar una Carrera");
            }


            
            string tipoSustentante = parametrosValidacion["vTipoSustentante"];

            switch (tipoSustentante)
            {
                case "1":
                    if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vPlan"))
                    {
                        lista.Add("Alerta: Se espera seleccionar un Plan de Estudios");
                    }
                    if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vModalidad"))
                    {
                        lista.Add("Alerta: Se espera seleccionar una Modalidad");
                    }
                    if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vSemestre"))
                    {
                        lista.Add("Alerta: Se espera seleccionar un Semestre");
                    }

                    if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vTurnoEscolar"))
                    {
                        lista.Add("Alerta: Se espera seleccionar un Turno Escolar");
                    }

                    if (!validacion.ContieneValor(parametrosValidacion, "vTieneServicio"))
                    {
                        lista.Add("Alerta: Debe especificar si realizó Servicio Social (Seleccione Sí o No).");
                    }

                    if (!validacion.ContieneValor(parametrosValidacion, "vTienePracticas"))
                    {
                        lista.Add("Alerta: Debe especificar si realizó Prácticas Profesionales (Seleccione Sí o No).");
                    }

                    break;

                case "2":

                    if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vAnioIngreso"))
                    {
                        lista.Add("Alerta: Se espera seleccionar un Año de Ingreso");
                        return lista;
                    }

                    if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vAnioEgreso"))
                    {
                        lista.Add("Alerta: Se espera seleccionar un Año de Egreso");
                        return lista;
                    }

                    if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vEstatusAcademico"))
                    {
                        lista.Add("Alerta: Se espera seleccionar un Estatus Académico");
                        return lista;
                    }

                    if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vEstatusTitulacion"))
                    {
                        lista.Add("Alerta: Se espera seleccionar un Estatus de Titulación");
                        return lista;
                    }

                    break;
            }

            return lista;


        }
    }
}
