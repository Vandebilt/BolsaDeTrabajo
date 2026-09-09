using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaVerSustentante
    {
        // Leer
        public static List<bdVerSustentante> Leer(int pIdSustentante)
        {
            List<bdVerSustentante> lista = new List<bdVerSustentante>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 1),
                new SqlParameter("@IdSustentante", pIdSustentante)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdVerSustentante
                         {
                             IdSustentante = fila["IdSustentante"] != DBNull.Value ? Convert.ToInt32(fila["IdSustentante"]) : 0,
                             Nombre = fila["Nombre"]?.ToString() ?? string.Empty,
                             PrimerApellido = fila["PrimerApellido"]?.ToString() ?? string.Empty,
                             SegundoApellido = fila["SegundoApellido"]?.ToString() ?? string.Empty,

                             FechaNacimiento = fila["FechaNacimiento"] != DBNull.Value
                                 ? Convert.ToDateTime(fila["FechaNacimiento"])
                                 : (DateTime?)null,
                             Genero = fila["Genero"]?.ToString() ?? string.Empty,
                             EstadoCivil = fila["EstadoCivil"]?.ToString() ?? string.Empty,
                             Nacionalidad = fila["Nacionalidad"]?.ToString() ?? string.Empty,
                             Municipio = fila["Municipio"]?.ToString() ?? string.Empty,
                             Colonia = fila["Colonia"]?.ToString() ?? string.Empty,
                             Calle = fila["Calle"]?.ToString() ?? string.Empty,
                             NumeroCasa = fila["NumeroCasa"]?.ToString() ?? string.Empty,
                             SueldoDeseado = fila["SueldoDeseado"] != DBNull.Value ? Convert.ToDecimal(fila["SueldoDeseado"]) : 0,
                             Biografia = fila["Biografia"]?.ToString() ?? string.Empty,
                             HorarioDisponible = fila["HorarioDisponible"]?.ToString() ?? string.Empty,
                             TrabajaActualmente = fila["TrabajaActualmente"]?.ToString() ?? "NO", // ← viene como 'SI' o 'NO'
                             IdArchivoSustentante = fila["IdArchivoSustentante"] != DBNull.Value ? Convert.ToInt32(fila["IdArchivoSustentante"]) : 0,
                             RutaImagenPerfil = fila["RutaImagenPerfil"]?.ToString() ?? string.Empty,
                             Correo = fila["Correo"]?.ToString() ?? string.Empty,
                             Telefono = fila["Telefono"]?.ToString() ?? string.Empty,
                             LinkedinUrl = fila["LinkedinUrl"]?.ToString() ?? string.Empty,
                             TipoSustentante = fila["TipoSustentante"]?.ToString() ?? string.Empty,
                             Matricula = fila["Matricula"]?.ToString() ?? string.Empty,
                             TipoGrado = fila["TipoGrado"]?.ToString() ?? string.Empty,
                             Carrera = fila["Carrera"]?.ToString() ?? string.Empty,
                             Plan = fila["PlanEstudio"]?.ToString() ?? string.Empty,
                             Modalidad = fila["Modalidad"]?.ToString() ?? string.Empty,
                             HasServicioSocial = fila["HasServicioSocial"]?.ToString() ?? string.Empty,
                             HasPracticasProfesionales = fila["HasPracticasProfesionales"]?.ToString() ?? string.Empty,
                             Semestre = fila["Semestre"]?.ToString() ?? string.Empty,
                             TurnoEscolar = fila["TurnoEscolar"]?.ToString() ?? string.Empty,
                             EstatusAcademico = fila["EstatusAcademico"]?.ToString() ?? string.Empty,
                             EstatusTitulacion = fila["EstatusTitulacion"]?.ToString() ?? string.Empty,
                             AnioIngreso = fila["AnioIngreso"]?.ToString() ?? string.Empty,
                             AnioEgreso = fila["AnioEgreso"]?.ToString() ?? string.Empty,
                             Promedio = fila["Promedio"]?.ToString() ?? string.Empty,
                             OtraCarrera = fila["OtraCarrera"]?.ToString() ?? string.Empty,
                          

                         }).ToList();
            }

            return lista;
        }

        //Consultar
        public static List<bdVerSustentante> ConsultarCorreo(int? pIdSustentante)
        {
            List<bdVerSustentante> lista = new List<bdVerSustentante>();

            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);

            lista = (from DataRow fila in tabla.Rows
                     select new bdVerSustentante
                     {
                         TipoCorreo = fila["Tipo"]?.ToString() ?? string.Empty,
                         Correo = fila["Correo"]?.ToString() ?? string.Empty,
                     }).ToList();

            return lista;
        }

        public static List<bdVerSustentante> ConsultarTelefono(int? pIdSustentante)
        {
            List<bdVerSustentante> lista = new List<bdVerSustentante>();

            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 3),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);

            lista = (from DataRow fila in tabla.Rows
                     select new bdVerSustentante
                     {
                         TipoTelefono = fila["Tipo"]?.ToString() ?? string.Empty,
                         Telefono = fila["Telefono"]?.ToString() ?? string.Empty,
                         Extension = fila["Extension"]?.ToString() ?? string.Empty,
                     }).ToList();

            return lista;
        }

        public static List<bdVerSustentante> ConsultarAreaInteres(int? pIdSustentante)
        {
            List<bdVerSustentante> lista = new List<bdVerSustentante>();

            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 4),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);

            lista = (from DataRow fila in tabla.Rows
                     select new bdVerSustentante
                     {
                         AreaInteres = fila["AreaInteres"]?.ToString() ?? string.Empty,
                     }).ToList();

            return lista;
        }
        public static List<bdVerSustentante> ConsultarHabilidad(int? pIdSustentante)
        {
            List<bdVerSustentante> lista = new List<bdVerSustentante>();

            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 5),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);

            lista = (from DataRow fila in tabla.Rows
                     select new bdVerSustentante
                     {
                         TipoHabilidad = fila["TipoHabilidad"]?.ToString() ?? string.Empty,
                         Habilidad = fila["Habilidad"]?.ToString() ?? string.Empty,
                     }).ToList();

            return lista;
        }
        public static List<bdVerSustentante> ConsultarSoftware(int? pIdSustentante)
        {
            List<bdVerSustentante> lista = new List<bdVerSustentante>();

            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 6),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);

            lista = (from DataRow fila in tabla.Rows
                     select new bdVerSustentante
                     {
                         PaqueteSoftware = fila["PaqueteSoftware"]?.ToString() ?? string.Empty,
                         Nivel = fila["Nivel"]?.ToString() ?? string.Empty,
                     }).ToList();

            return lista;
        }
        public static List<bdVerSustentante> ConsultarIdioma(int? pIdSustentante)
        {
            List<bdVerSustentante> lista = new List<bdVerSustentante>();

            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 7),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);

            lista = (from DataRow fila in tabla.Rows
                     select new bdVerSustentante
                     {
                         Idioma = fila["Idioma"]?.ToString() ?? string.Empty,
                         Nivel = fila["Nivel"]?.ToString() ?? string.Empty,
                     }).ToList();

            return lista;
        }

        //Consultar
        // Cambia el tipo de retorno a DataTable
        public static DataTable ConsultarExperienciaLaboral(int? pIdSustentante)
        {
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 8),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            // Simplemente devuelve la tabla
            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);
            return tabla;
        }
        //Consultar
        // Cambia el tipo de retorno a DataTable
        public static DataTable ConsultarCertificado(int? pIdSustentante)
        {
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 9),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            // Simplemente devuelve la tabla
            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);
            return tabla;
        }

        public static List<bdVerSustentante> ConsultarDocumento(int? pIdSustentante)
        {
            List<bdVerSustentante> lista = new List<bdVerSustentante>();

            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 10),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);

            lista = (from DataRow fila in tabla.Rows
                     select new bdVerSustentante
                     {
                         IdArchivoSustentante = Convert.ToInt32(fila["Id"]),
                         TipoArchivo = fila["TipoArchivo"]?.ToString() ?? string.Empty,
                         Estatus = fila["Estatus"]?.ToString() ?? string.Empty,
                         RetroAlimentacion = fila["RetroAlimentacion"]?.ToString() ?? string.Empty,
                         RutaDocumento = fila["RutaDocumento"]?.ToString() ?? string.Empty,
                     }).ToList();

            return lista;
        }

        public static List<bdVerSustentante> ConsultarAreaExperiencia(int? pIdSustentante)
        {
            List<bdVerSustentante> lista = new List<bdVerSustentante>();

            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 11),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);

            lista = (from DataRow fila in tabla.Rows
                     select new bdVerSustentante
                     {
                         AreaExperiencia = fila["AreaExperiencia"]?.ToString() ?? string.Empty,
                         Anios = fila["Anios"]?.ToString() ?? string.Empty,
                     }).ToList();

            return lista;
        }

        public static List<bdVerSustentante> ConsultarCertificacionIdioma(int? pIdSustentante)
        {
            List<bdVerSustentante> lista = new List<bdVerSustentante>();

            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 12),
                new SqlParameter("@IdSustentante", pIdSustentante),
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spVerSustentante", parametros);

            lista = (from DataRow fila in tabla.Rows
                     select new bdVerSustentante
                     {
                         Idioma = fila["Idioma"]?.ToString() ?? string.Empty,
                         Certificación = fila["Certificación"]?.ToString() ?? string.Empty,
                     }).ToList();

            return lista;
        }

    }
}
