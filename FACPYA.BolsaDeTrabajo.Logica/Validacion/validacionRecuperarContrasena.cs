using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionRecuperarContrasena
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion catalogo = new catalogoValidacion();
            List<string> lista = new List<string>();

            if (!catalogo.ContieneValor(parametrosValidacion, "vCorreo"))
            {
                lista.Add("Se espera escribir un correo");
            }

            return lista;
        }
    }
}
