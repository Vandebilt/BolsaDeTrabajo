using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaVerEmpresa
    {
        // Leer
        public static List<bdVerEmpresa> Leer(int pIdEmpresa)
        {
            List<bdVerEmpresa> lista = new List<bdVerEmpresa>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 1),
                new SqlParameter("@IdEmpresa", pIdEmpresa)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerEmpresa", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdVerEmpresa
                         {
                             IdEmpresa = fila["IdEmpresa"] != DBNull.Value ? Convert.ToInt32(fila["IdEmpresa"]) : 0,
                             Nombre = fila["Nombre"]?.ToString() ?? string.Empty,
                             Giro = fila["Giro"]?.ToString() ?? string.Empty,
                             TamanioEmpresa = fila["TamanioEmpresa"]?.ToString() ?? string.Empty,
                             TipoEmpresa = fila["TipoEmpresa"]?.ToString() ?? string.Empty,
                             Direccion = fila["Direccion"]?.ToString() ?? string.Empty,
                             Correo = fila["Correo"]?.ToString() ?? string.Empty,
                             PaginaWeb = fila["PaginaWeb"]?.ToString() ?? string.Empty,
                             ContactoNombre = fila["ContactoNombre"]?.ToString() ?? string.Empty,
                             ContactoPuesto = fila["ContactoPuesto"]?.ToString() ?? string.Empty,
                             Mision = fila["Mision"]?.ToString() ?? string.Empty,
                             Vision = fila["Vision"]?.ToString() ?? string.Empty,
                             RutaLogotipo = fila["RutaLogotipo"]?.ToString() ?? string.Empty,

                         }).ToList();
            }

            return lista;
        }



        public static List<bdVerEmpresa> ConsultarTelefono(int? pIdEmpresa)
        {
            List<bdVerEmpresa> lista = new List<bdVerEmpresa>();

            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdEmpresa", pIdEmpresa),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerEmpresa", parametros);

            lista = (from DataRow fila in tabla.Rows
                     select new bdVerEmpresa
                     {
                         Telefono = fila["Telefono"]?.ToString() ?? string.Empty,
                         Extension = fila["Extension"]?.ToString() ?? string.Empty,
                     }).ToList();

            return lista;
        }

        //Consultar
        public static List<bdVerEmpresa> ConsultarDocumento(int? pIdEmpresa)
        {
            List<bdVerEmpresa> lista = new List<bdVerEmpresa>();

            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdEmpresa", pIdEmpresa),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerEmpresa", parametros);

            lista = (from DataRow fila in tabla.Rows
                     select new bdVerEmpresa
                     {
                         IdEmpresa = Convert.ToInt32(fila["Id"]),
                         TipoArchivo = fila["TipoArchivo"]?.ToString() ?? string.Empty,
                         Estatus = fila["Estatus"]?.ToString() ?? string.Empty,
                         RetroAlimentacion = fila["RetroAlimentacion"]?.ToString() ?? string.Empty,
                         RutaDocumento = fila["RutaDocumento"]?.ToString() ?? string.Empty,
                     }).ToList();

            return lista;
        }
    }
}
