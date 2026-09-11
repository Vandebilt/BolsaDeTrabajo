using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaSolicitud
    {
        //Crear
        public static Tuple<string, string> Crear(bdSolicitud parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdEmpresa", parametro.IdEmpresa),
                    new SqlParameter("@IdGenero", parametro.IdGenero),
                    new SqlParameter("@IdTiempoDisponible", parametro.IdTiempoDisponible),
                    new SqlParameter("@NombrePuesto", parametro.NombrePuesto),
                    new SqlParameter("@LugarTrabajo", parametro.LugarTrabajo),
                    new SqlParameter("@Direccion", parametro.Direccion),
                    new SqlParameter("@Actividades", (object)parametro.Actividades ?? DBNull.Value),
                    new SqlParameter("@HasEstudiantes", (object)parametro.HasEstudiantes ?? DBNull.Value),
                    new SqlParameter("@HasEgresados", (object)parametro.HasEgresados ?? DBNull.Value),
                    new SqlParameter("@MostrarSueldo", (object)parametro.MostrarSueldo ?? DBNull.Value),
                    new SqlParameter("@HasAvisoPrivacidad", parametro.HasAvisoPrivacidad),
                    new SqlParameter("@NumVacantes", parametro.NumVacantes),
                    new SqlParameter("@EdadMinima", parametro.EdadMinima),
                    new SqlParameter("@EdadMaxima", parametro.EdadMaxima),
                    new SqlParameter("@HoraMinima", parametro.HoraMinima),
                    new SqlParameter("@HoraMaxima", parametro.HoraMaxima),
                    new SqlParameter("@SueldoMinimo", parametro.SueldoMinimo),
                    new SqlParameter("@SueldoMaximo", parametro.SueldoMaximo),
                    new SqlParameter("@UsuarioRegistro", parametro.UsuarioRegistro)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spSolicitud", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();

                // Recupera el IdSolicitud recién creado (SELECT SCOPE_IDENTITY() AS IdSolicitud en el SP)
                if (tabla.Columns.Contains("IdSolicitud") && tabla.Rows[0]["IdSolicitud"] != DBNull.Value)
                {
                    parametro.IdSolicitud = Convert.ToInt32(tabla.Rows[0]["IdSolicitud"]);
                }
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        // Consultar (de la empresa en sesión, o todas si pIdEmpresa es null -> Administrador)
        // pIdEstatus: null trae solo activas (1); pTipoCandidato: 1=Estudiante, 2=Egresado, null=Todos
        public static List<DataRow> Consultar(int? pIdEmpresa, int? pIdEstatus = null, int? pIdTipoCandidato = null)
        {
            List<DataRow> lista = new List<DataRow>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 4),
                new SqlParameter("@IdEmpresa", pIdEmpresa),
                new SqlParameter("@IdEstatus", pIdEstatus),
                new SqlParameter("@IdTipoCandidato", pIdTipoCandidato),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spSolicitud", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = tabla.AsEnumerable().ToList();
            }

            return lista;
        }

        // Leer Empresa
        public static List<bdSolicitud> LeerEmpresa(int pIdEmpresa)
        {
            List<bdSolicitud> lista = new List<bdSolicitud>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdEmpresa", pIdEmpresa)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spSolicitud", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdSolicitud
                         {
                             IdEmpresa = Convert.ToInt32(fila["Id"]),
                             Nombre = fila["Nombre"].ToString(),
                             PuestoNombreContacto = fila["PuestoNombreContacto"].ToString(),
                             Direccion = fila["Direccion"].ToString(),

                         }).ToList();
            }

            return lista;
        }

        // Leer
        public static List<bdSolicitud> Leer(int pIdSolicitud)
        {
            List<bdSolicitud> lista = new List<bdSolicitud>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSolicitud", pIdSolicitud)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spSolicitud", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdSolicitud
                         {
                             IdSolicitud = Convert.ToInt32(fila["Id"]),
                             Nombre = fila["Nombre"].ToString(),
                             IdEstatus = Convert.ToInt32(fila["IdEstatus"]),
                             IdEmpresa = Convert.ToInt32(fila["IdEmpresa"]),
                             IdGenero = Convert.ToInt32(fila["IdGenero"]),
                             IdTiempoDisponible = Convert.ToInt32(fila["IdTiempoDisponible"]),
                             NombrePuesto = fila["NombrePuesto"].ToString(),
                             LugarTrabajo = fila["LugarTrabajo"].ToString(),
                             Direccion = fila["Direccion"].ToString(),
                             Actividades = fila["Actividades"] == DBNull.Value ? null : fila["Actividades"].ToString(),
                             HasEstudiantes = fila["HasEstudiantes"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(fila["HasEstudiantes"]),
                             HasEgresados = fila["HasEgresados"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(fila["HasEgresados"]),
                             MostrarSueldo = fila["MostrarSueldo"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(fila["MostrarSueldo"]),
                             HasAvisoPrivacidad = Convert.ToBoolean(fila["HasAvisoPrivacidad"]),
                             NumVacantes = Convert.ToInt32(fila["NumVacantes"]),
                             EdadMinima = Convert.ToInt32(fila["EdadMinima"]),
                             EdadMaxima = Convert.ToInt32(fila["EdadMaxima"]),
                             HoraMinima = (TimeSpan)fila["HoraMinima"],
                             HoraMaxima = (TimeSpan)fila["HoraMaxima"],
                             SueldoMinimo = Convert.ToDecimal(fila["SueldoMinimo"]),
                             SueldoMaximo = Convert.ToDecimal(fila["SueldoMaximo"]),

                         }).ToList();
            }

            return lista;
        }
    }
}
