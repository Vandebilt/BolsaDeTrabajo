using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class catalogoValidacion
    {
        public bool ContieneValor(Dictionary<object, string> parametros, string clave)
        {
            return parametros.TryGetValue(clave, out string valor) && !string.IsNullOrEmpty(valor);
        }

        public bool EsNumeroMayorACero(Dictionary<object, string> parametros, string clave)
        {
            return int.TryParse(parametros[clave], out int valor) && valor > 0;
        }

        public bool EsNumeroMayorOIgualACero(Dictionary<object, string> parametros, string clave)
        {
            return int.TryParse(parametros[clave], out int valor) && valor >= 0;
        }

        public bool FechaEsAnterior(Dictionary<object, string> parametros, string claveFecha1, string claveFecha2)
        {
            if (parametros.TryGetValue(claveFecha1, out string fecha1Str) &&
                parametros.TryGetValue(claveFecha2, out string fecha2Str))
            {
                if (DateTime.TryParse(fecha1Str, out DateTime fecha1) &&
                    DateTime.TryParse(fecha2Str, out DateTime fecha2))
                {
                    return fecha1 < fecha2;
                }
            }
            return false;
        }

        public bool ContieneSoloLetras(Dictionary<object, string> parametros, string clave)
        {
            string patron = "^[a-zA-ZáéíóúÁÉÍÓÚñÑ. ]+$";
            return Regex.IsMatch(parametros[clave], patron);
        }

        public bool ContieneTextoYNumeros(Dictionary<object, string> parametros, string clave)
        {
            string patron = "^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\\s\\-.:;,.]+$";
            return Regex.IsMatch(parametros[clave], patron);
        }

        public bool TieneFormatoCorreo(Dictionary<object, string> parametros, string clave)
        {
            string patronCorreo = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(parametros[clave], patronCorreo);
        }

        public bool TieneLongitudMinima(Dictionary<object, string> parametros, string clave, int longitudMinima)
        {
            return parametros[clave].Length >= longitudMinima;
        }

        public bool SonIguales(Dictionary<object, string> parametros, string clave1, string clave2)
        {
            if (!parametros.ContainsKey(clave1) || !parametros.ContainsKey(clave2))
                return false;

            string valor1 = parametros[clave1];
            string valor2 = parametros[clave2];

            return valor1 == valor2;
        }

        public bool EsSeleccionBooleanaValida(Dictionary<object, string> parametros, string claveSi, string claveNo)
        {
            // Reutilizamos tu lógica de ContieneValor internamente
            bool si = ContieneValor(parametros, claveSi);
            bool no = ContieneValor(parametros, claveNo);

            return si ^ no;
        }
    }
}
