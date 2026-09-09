using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using FACPYA.BolsaDeTrabajo.Entidad.viewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaSustentante
    {
        //Crear
        public static Tuple<string, string> Crear(bdSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdUsuario", parametro.IdUsuario),
                    new SqlParameter("@IdTipoSustentante", parametro.IdTipoSustentante),
                    new SqlParameter("@IdGenero", parametro.IdGenero),
                    new SqlParameter("@IdEstadoCivil", parametro.IdEstadoCivil),
                    new SqlParameter("@IdMunicipio", parametro.IdMunicipio),
                    new SqlParameter("@IdTiempoDisponible", parametro.IdTiempoDisponible),
                    new SqlParameter("@Nombre", parametro.Nombre),
                    new SqlParameter("@PrimerApellido", parametro.PrimerApellido),
                    new SqlParameter("@SegundoApellido", parametro.SegundoApellido),
                    new SqlParameter("@Nacionalidad", parametro.Nacionalidad),
                    new SqlParameter("@Calle", parametro.Calle),
                    new SqlParameter("@NumeroCasa", parametro.NumeroCasa),
                    new SqlParameter("@Colonia", parametro.Colonia),
                    new SqlParameter("@HorarioDisponible", parametro.HorarioDisponible),
                    new SqlParameter("@TrabajaActualmente", parametro.TrabajaActualmente),
                    new SqlParameter("@SueldoDeseado", parametro.SueldoDeseado),
                    new SqlParameter("@LinkedinUrl", parametro.LinkedinUrl),
                    new SqlParameter("@Biografia", parametro.Biografia),
               };

                tabla = bdConexion.EjecutarStoredProcedure("spSustentante", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        // Leer
        public static List<bdSustentante> Leer(int pIdSustentante)
        {
            List<bdSustentante> lista = new List<bdSustentante>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdSustentante", pIdSustentante)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdSustentante
                         {
                             IdSustentante = Convert.ToInt32(fila["Id"]),
                             IdTipoSustentante = fila["IdTipoSustentante"] != DBNull.Value ? Convert.ToInt32(fila["IdTipoSustentante"]) : 0,
                             IdEstatus = fila["IdEstatus"] != DBNull.Value ? Convert.ToInt32(fila["IdEstatus"]) : 0,
                             TipoSustentante = fila["TipoSustentante"] != DBNull.Value ? fila["TipoSustentante"].ToString() : string.Empty,
                             IdGenero = fila["IdGenero"] != DBNull.Value ? Convert.ToInt32(fila["IdGenero"]) : 0,
                             IdEstadoCivil = fila["IdEstadoCivil"] != DBNull.Value ? Convert.ToInt32(fila["IdEstadoCivil"]) : 0,
                             IdTiempoDisponible = fila["IdTiempoDisponible"] != DBNull.Value ? Convert.ToInt32(fila["IdTiempoDisponible"]) : 0,
                             IdMunicipio = fila["IdMunicipio"] != DBNull.Value ? Convert.ToInt32(fila["IdMunicipio"]) : 0,
                             Nombre = fila["Nombre"] != DBNull.Value ? fila["Nombre"].ToString() : string.Empty,
                             PrimerApellido = fila["PrimerApellido"] != DBNull.Value ? fila["PrimerApellido"].ToString() : string.Empty,
                             SegundoApellido = fila["SegundoApellido"] != DBNull.Value ? fila["SegundoApellido"].ToString() : string.Empty,
                             Nacionalidad = fila["Nacionalidad"] != DBNull.Value ? fila["Nacionalidad"].ToString() : string.Empty,
                             Calle = fila["Calle"] != DBNull.Value ? fila["Calle"].ToString() : string.Empty,
                             NumeroCasa = fila["NumeroCasa"] != DBNull.Value ? fila["NumeroCasa"].ToString() : string.Empty,
                             Colonia = fila["Colonia"] != DBNull.Value ? fila["Colonia"].ToString() : string.Empty,
                             HorarioDisponible = fila["HorarioDisponible"] != DBNull.Value ? fila["HorarioDisponible"].ToString() : string.Empty,
                             TrabajaActualmente = fila["TrabajaActualmente"] != DBNull.Value ? Convert.ToBoolean(fila["TrabajaActualmente"]) : false,
                             SueldoDeseado = fila["SueldoDeseado"] != DBNull.Value ? Convert.ToInt32(fila["SueldoDeseado"]) : 0,
                             LinkedinUrl = fila["LinkedinUrl"] != DBNull.Value ? fila["LinkedinUrl"].ToString() : string.Empty,
                             Biografia = fila["Biografia"] != DBNull.Value ? fila["Biografia"].ToString() : string.Empty,
                             FechaNacimiento = fila["FechaNacimiento"] != DBNull.Value ? Convert.ToDateTime(fila["FechaNacimiento"]) : (DateTime?)null,     
                             IdArchivoSustentante = fila["IdArchivoSustentante"] != DBNull.Value ? Convert.ToInt32(fila["IdArchivoSustentante"]) : 0,
                             Fotografia = fila["Fotografia"] != DBNull.Value ? fila["Fotografia"].ToString() : string.Empty,
                             Estatus = fila["Estatus"] != DBNull.Value ? fila["Estatus"].ToString() : string.Empty,
                             IdEstatusImg = fila["IdEstatusImg"] != DBNull.Value ? Convert.ToInt32(fila["IdEstatusImg"]) : 0,


                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> Editar(bdSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 3),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@Nombre", parametro.Nombre),
                    new SqlParameter("@PrimerApellido", parametro.PrimerApellido),
                    new SqlParameter("@SegundoApellido", parametro.SegundoApellido),
                    new SqlParameter("@FechaNacimiento", parametro.FechaNacimiento),
                    new SqlParameter("@IdGenero", parametro.IdGenero),
                    new SqlParameter("@IdEstadoCivil", parametro.IdEstadoCivil),
                    new SqlParameter("@IdMunicipio", parametro.IdMunicipio),
                    new SqlParameter("@IdTiempoDisponible", parametro.IdTiempoDisponible),
                    new SqlParameter("@Nacionalidad", parametro.Nacionalidad),
                    new SqlParameter("@Calle", parametro.Calle),
                    new SqlParameter("@NumeroCasa", parametro.NumeroCasa),
                    new SqlParameter("@Colonia", parametro.Colonia),
                    new SqlParameter("@SueldoDeseado", parametro.SueldoDeseado),
                    new SqlParameter("@HorarioDisponible", parametro.HorarioDisponible),
                    new SqlParameter("@TrabajaActualmente", parametro.TrabajaActualmente),
                    new SqlParameter("@LinkedinUrl", parametro.LinkedinUrl),
                    new SqlParameter("@Biografia", parametro.Biografia),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spSustentante", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        


        //Editar
        public static Tuple<string, string> EditarTipoSustentante(bdSustentante parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 4),
                    new SqlParameter("@IdSustentante", parametro.IdSustentante),
                    new SqlParameter("@IdTipoSustentante", parametro.IdTipoSustentante),
                };

                tabla = bdConexion.EjecutarStoredProcedure("spSustentante", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        // Leer
        public static List<bdSustentante> ValidarSustentante(int pIdSustentante)
        {
            List<bdSustentante> lista = new List<bdSustentante>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 5),
                new SqlParameter("@IdSustentante", pIdSustentante)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spSustentante", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdSustentante
                         {
                             // Leemos las nuevas columnas que devuelve el SQL
                             Titulo = fila["Titulo"].ToString(),
                             Faltantes = fila["Faltantes"].ToString()
                         }).ToList();
            }

            return lista;
        }

        // CV
        public static vmCurriculum ObtenerCurriculum(int pIdSustentante)
        {
            vmCurriculum oCurriculum = new vmCurriculum();

            // 1. Validación preventiva: Si el ID es 0 o inválido, no vayas a la BD
            if (pIdSustentante <= 0) return oCurriculum;

            SqlParameter[] parametros =
            {
        new SqlParameter("@pIdSustentante", pIdSustentante)
    };

            try
            {
                DataTable tabla = bdConexion.EjecutarStoredProcedure("spPerfilSustentante", parametros);

                if (tabla.Rows.Count > 0)
                {
                    // Usamos una fila variable para facilitar la lectura
                    DataRow fila = tabla.Rows[0];

                    oCurriculum = new vmCurriculum
                    {
                        // MÉTODOS AUXILIARES PARA EVITAR ERRORES SI VIENE NULL O VACÍO
                        ExpedienteAcademico = DeserializarSeguro<List<vmExpedienteAcademico>>(fila["EXPEDIENTE_ACADEMICO"]),
                        ExperienciaLaboral = DeserializarSeguro<List<vmExperienciaLaboral>>(fila["EXPERIENCIA_LABORAL"]),

                        // Para Sustentante, como es una lista de 1, validamos que no venga vacía
                        Sustentante = DeserializarSeguro<List<vmSustentante>>(fila["SUSTENTANTE"])?.FirstOrDefault() ?? new vmSustentante(),
                        Correo = fila["CORREO"] != DBNull.Value ? fila["CORREO"].ToString() : string.Empty,
                        Telefonos = DeserializarSeguro<List<vmTelefono>>(fila["TELEFONOS"]),
                        Habilidades = DeserializarSeguro<List<vmHabilidad>>(fila["HABILIDADES"]),
                        Idiomas = DeserializarSeguro<List<vmIdioma>>(fila["IDIOMAS"]),
                        ManejoSoftware = DeserializarSeguro<List<vmManejoSoftware>>(fila["MANEJO_SOFTWARE"]),
                        Certificados = DeserializarSeguro<List<vmCertificado>>(fila["CERTIFICADOS"]),
                        Imagen = fila["IMAGEN"] != DBNull.Value ? fila["IMAGEN"].ToString() : string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                // Loguear el error si tienes un sistema de logs
                // throw; // Opcional: volver a lanzar el error o retornar el objeto vacío
            }

            return oCurriculum;
        }

        // --- AGREGA ESTE MÉTODO AUXILIAR EN TU CLASE ---
        // Sirve para que si el JSON viene nulo, vacío o corrupto, no truene el sistema
        private static T DeserializarSeguro<T>(object dbValue) where T : class, new()
        {
            if (dbValue == DBNull.Value || dbValue == null) return new T(); // Retorna lista vacía si es null

            string json = dbValue.ToString();
            if (string.IsNullOrWhiteSpace(json)) return new T();

            try
            {
                return JsonConvert.DeserializeObject<T>(json) ?? new T();
            }
            catch
            {
                return new T(); // Si el JSON está mal formado, no rompe, retorna vacío
            }
        }
    }
}
