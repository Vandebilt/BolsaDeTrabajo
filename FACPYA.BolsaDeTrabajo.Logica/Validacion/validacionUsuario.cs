using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionUsuario
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion validacion = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!validacion.ContieneValor(parametrosValidacion, "vCorreo"))
            {
                lista.Add("Alerta: Se espera escribir un correo");
            }

            if (!validacion.TieneFormatoCorreo(parametrosValidacion, "vCorreo"))
            {
                lista.Add("Alerta: Se espera escribir un correo válido");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vContrasenia"))
            {
                lista.Add("Alerta: Se espera escribir una contraseña");
            }

            if (!validacion.TieneLongitudMinima(parametrosValidacion, "vContrasenia", 8))
            {
                lista.Add("Alerta: Se espera escribir una contraseña de mínimo 8 caracteres");
            }

            if (!validacion.ContieneValor(parametrosValidacion, "vConfirmarContrasenia"))
            {
                lista.Add("Alerta: Se espera escribir una confirmación de contraseña");
            }

            if (!validacion.SonIguales(parametrosValidacion, "vContrasenia", "vConfirmarContrasenia"))
            {
                lista.Add("Alerta: Las contraseñas no coinciden");
            }

            return lista;
        }
    }
}
