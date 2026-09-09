using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionSustentanteCertificado
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.ContieneValor(parametrosValidacion, "vDescripcion"))
            {
                lista.Add("Alerta: Se espera escribir una descripcion");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vInstitucionEmisora"))
            {
                lista.Add("Alerta: Se espera escribir una institucion emisora");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vFechaEmision"))
            {
                lista.Add("Alerta: Se espera seleccionar una fecha emisión");
            }
            
            return lista;
        }
    }
}
