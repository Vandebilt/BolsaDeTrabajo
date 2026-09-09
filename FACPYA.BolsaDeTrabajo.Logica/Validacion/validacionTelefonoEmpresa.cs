using System.Collections.Generic;

namespace FACPYA.BolsaDeTrabajo.Logica.Validacion
{
    public class validacionTelefonoEmpresa
    {
        public static List<string> Validar(Dictionary<object, string> parametrosValidacion)
        {
            catalogoValidacion catalogo = new catalogoValidacion();
            List<string> lista = new List<string>();


            if (!catalogo.ContieneValor(parametrosValidacion, "vTelefono"))
            {
                lista.Add("Se espera escribir un teléfono");
            }

            return lista;
        }
    }
}
