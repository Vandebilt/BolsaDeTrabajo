using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionEmpresa
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.ContieneValor(parametrosValidacion, "vNombre"))
            {
                lista.Add("Alerta: Se espera escribir un Nombre de Empresa");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vGiro"))
            {
                lista.Add("Alerta: Se espera escribir un Giro de la Empresa");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vIdTipoEmpresa"))
            {
                lista.Add("Alerta: Se espera seleccionar un Tipo de Empresa");
            }

            if (!validacion.EsNumeroMayorACero(parametrosValidacion, "vIdTamanioEmpresa"))
            {
                lista.Add("Alerta: Se espera seleccionar un Tamaño de Empresa");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vDireccion"))
            {
                lista.Add("Alerta: Se espera seleccionar una Dirección");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vCorreo"))
            {
                lista.Add("Alerta: Se espera escribir un Correo");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vPaginaWeb"))
            {
                lista.Add("Alerta: Se espera escribir una Página Web");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vContactoNombre"))
            {
                lista.Add("Alerta: Se espera escribir un Nombre de Contacto");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vContactoPuesto"))
            {
                lista.Add("Alerta: Se espera seleccionar un Puesto de Contacto");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vMision"))
            {
                lista.Add("Alerta: Se espera escribir una Misión");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vVision"))
            {
                lista.Add("Alerta: Se espera escribir una Visión");
            }

            return lista;

        }
    }
}
