using FACPYA.BolsaDeTrabajo.Datos;
using FACPYA.BolsaDeTrabajo.Entidad.bdEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace FACPYA.BolsaDeTrabajo.Logica
{
    public class logicaEmpresa
    {
        //Crear
        public static Tuple<string, string> Crear(bdEmpresa parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 1),
                    new SqlParameter("@IdUsuario", parametro.IdUsuario),
                    new SqlParameter("@IdTipoEmpresa", parametro.IdTipoEmpresa),
                    new SqlParameter("@Folio", parametro.Folio),
                    new SqlParameter("@Nombre", parametro.Nombre),
                    new SqlParameter("@Giro", parametro.Giro),
                    new SqlParameter("@IdTamanioEmpresa", parametro.IdTamanioEmpresa),
                    new SqlParameter("@Direccion", parametro.Direccion),
                    new SqlParameter("@Correo", parametro.Correo),
                    new SqlParameter("@PaginaWeb", parametro.PaginaWeb),
                    new SqlParameter("@ContactoNombre", parametro.ContactoNombre),
                    new SqlParameter("@ContactoPuesto", parametro.ContactoPuesto),
                    new SqlParameter("@Mision", parametro.Mision),
                    new SqlParameter("@Vision", parametro.Vision),
                    new SqlParameter("@RegimenGastosMedicos", parametro.RegimenGastosMedicos),
                    new SqlParameter("@HasAvisoPrivacidad", parametro.HasAvisoPrivacidad),
                    new SqlParameter("@UsuarioRegistro", parametro.UsuarioRegistro)
               };

                tabla = bdConexion.EjecutarStoredProcedure("spEmpresa", parametros);

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
        public static List<bdEmpresa> Leer(int pIdEmpresa)
        {
            List<bdEmpresa> lista = new List<bdEmpresa>();
            SqlParameter[] parametros = {
                new SqlParameter("@Accion", 2),
                new SqlParameter("@IdEmpresa", pIdEmpresa)
            };

            DataTable tabla = bdConexion.EjecutarStoredProcedure("spEmpresa", parametros);

            if (tabla.Rows.Count > 0)
            {
                lista = (from DataRow fila in tabla.Rows
                         select new bdEmpresa
                         {
                             IdEmpresa = Convert.ToInt32(fila["Id"]),
                             IdEstatus = Convert.ToInt32(fila["IdEstatus"]),
                             IdTipoEmpresa = fila["IdTipoEmpresa"] != DBNull.Value ? Convert.ToInt32(fila["IdTipoEmpresa"]) : 0,
                             Nombre = fila["Nombre"] != DBNull.Value ? fila["Nombre"].ToString() : string.Empty,
                             Giro = fila["Giro"] != DBNull.Value ? fila["Giro"].ToString() : string.Empty,
                             IdTamanioEmpresa = fila["IdTamanioEmpresa"] != DBNull.Value ? Convert.ToInt32(fila["IdTamanioEmpresa"]) : 0,
                             Direccion = fila["Direccion"] != DBNull.Value ? fila["Direccion"].ToString() : string.Empty,
                             Correo = fila["Correo"] != DBNull.Value ? fila["Correo"].ToString() : string.Empty,
                             PaginaWeb = fila["PaginaWeb"] != DBNull.Value ? fila["PaginaWeb"].ToString() : string.Empty,
                             ContactoNombre = fila["ContactoNombre"] != DBNull.Value ? fila["ContactoNombre"].ToString() : string.Empty,
                             ContactoPuesto = fila["ContactoPuesto"] != DBNull.Value ? fila["ContactoPuesto"].ToString() : string.Empty,
                             Mision = fila["Mision"] != DBNull.Value ? fila["Mision"].ToString() : string.Empty,
                             Vision = fila["Vision"] != DBNull.Value ? fila["Vision"].ToString() : string.Empty,
                             RegimenGastosMedicos = fila["RegimenGastosMedicos"] != DBNull.Value ? fila["RegimenGastosMedicos"].ToString() : string.Empty,
                             HasAvisoPrivacidad = fila["HasAvisoPrivacidad"] != DBNull.Value ? Convert.ToBoolean(fila["HasAvisoPrivacidad"]) : false,
                             IdArchivoEmpresa = fila["IdArchivoEmpresa"] != DBNull.Value ? Convert.ToInt32(fila["IdArchivoEmpresa"]) : 0,
                             Logotipo = fila["Logotipo"] != DBNull.Value ? fila["Logotipo"].ToString() : string.Empty,
                             Estatus = fila["Estatus"] != DBNull.Value ? fila["Estatus"].ToString() : string.Empty,
                             IdEstatusImg = fila["IdEstatusImg"] != DBNull.Value ? Convert.ToInt32(fila["IdEstatusImg"]) : 0,
                             RetroPerfil = fila["RetroPerfil"] != DBNull.Value ? fila["RetroPerfil"].ToString() : string.Empty,


                         }).ToList();
            }

            return lista;
        }

        //Editar
        public static Tuple<string, string> Editar(bdEmpresa parametro)
        {
            string mensaje;
            string tipoMensaje = string.Empty;

            try
            {
                DataTable tabla = new DataTable();
                SqlParameter[] parametros = {
                    new SqlParameter("@Accion", 3),
                    new SqlParameter("@IdEmpresa", parametro.IdEmpresa),
                    new SqlParameter("@IdTipoEmpresa", parametro.IdTipoEmpresa),
                    new SqlParameter("@Folio", parametro.Folio),
                    new SqlParameter("@Nombre", parametro.Nombre),
                    new SqlParameter("@Giro", parametro.Giro),
                    new SqlParameter("@IdTamanioEmpresa", parametro.IdTamanioEmpresa),
                    new SqlParameter("@Direccion", parametro.Direccion),
                    new SqlParameter("@Correo", parametro.Correo),
                    new SqlParameter("@PaginaWeb", parametro.PaginaWeb),
                    new SqlParameter("@ContactoNombre", parametro.ContactoNombre),
                    new SqlParameter("@ContactoPuesto", parametro.ContactoPuesto),
                    new SqlParameter("@Mision", parametro.Mision),
                    new SqlParameter("@Vision", parametro.Vision),
                    new SqlParameter("@RegimenGastosMedicos", parametro.RegimenGastosMedicos),
                    new SqlParameter("@HasAvisoPrivacidad", parametro.HasAvisoPrivacidad),
                    new SqlParameter("@UsuarioModificacion", parametro.UsuarioModificacion)
                };

                tabla = bdConexion.EjecutarStoredProcedure("spEmpresa", parametros);

                mensaje = tabla.Rows[0]["Mensaje"].ToString();
                tipoMensaje = tabla.Rows[0]["TipoMensaje"].ToString();
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return new Tuple<string, string>(mensaje, tipoMensaje);
        }

        public static List<bdRuta> Leer(Func<int> idEmpresaSesion)
        {
            throw new NotImplementedException();
        }



        //// Leer
        //public static List<bdSustentante> ValidarSustentante(int pIdSustentante)
        //{
        //    List<bdSustentante> lista = new List<bdSustentante>();
        //    SqlParameter[] parametros = {
        //        new SqlParameter("@Accion", 5),
        //        new SqlParameter("@IdSustentante", pIdSustentante)
        //    };

        //    DataTable tabla = bdConexion.EjecutarStoredProcedure("spSustentante", parametros);

        //    if (tabla.Rows.Count > 0)
        //    {
        //        lista = (from DataRow fila in tabla.Rows
        //                 select new bdSustentante
        //                 {
        //                     // Leemos las nuevas columnas que devuelve el SQL
        //                     Titulo = fila["Titulo"].ToString(),
        //                     Faltantes = fila["Faltantes"].ToString()
        //                 }).ToList();
        //    }

        //    return lista;
        //}

        // CV
        //    public static vmCurriculum ObtenerCurriculum(int pIdSustentante)
        //    {
        //        vmCurriculum oCurriculum = new vmCurriculum();

        //        // 1. Validación preventiva: Si el ID es 0 o inválido, no vayas a la BD
        //        if (pIdSustentante <= 0) return oCurriculum;

        //        SqlParameter[] parametros =
        //        {
        //    new SqlParameter("@pIdSustentante", pIdSustentante)
        //};

        //        try
        //        {
        //            DataTable tabla = bdConexion.EjecutarStoredProcedure("spPerfilSustentante", parametros);

        //            if (tabla.Rows.Count > 0)
        //            {
        //                // Usamos una fila variable para facilitar la lectura
        //                DataRow fila = tabla.Rows[0];

        //                oCurriculum = new vmCurriculum
        //                {
        //                    // MÉTODOS AUXILIARES PARA EVITAR ERRORES SI VIENE NULL O VACÍO
        //                    ExpedienteAcademico = DeserializarSeguro<List<vmExpedienteAcademico>>(fila["EXPEDIENTE_ACADEMICO"]),
        //                    ExperienciaLaboral = DeserializarSeguro<List<vmExperienciaLaboral>>(fila["EXPERIENCIA_LABORAL"]),

        //                    // Para Sustentante, como es una lista de 1, validamos que no venga vacía
        //                    Sustentante = DeserializarSeguro<List<vmSustentante>>(fila["SUSTENTANTE"])?.FirstOrDefault() ?? new vmSustentante(),
        //                    Correo = fila["CORREO"] != DBNull.Value ? fila["CORREO"].ToString() : string.Empty,
        //                    Telefonos = DeserializarSeguro<List<vmTelefono>>(fila["TELEFONOS"]),
        //                    Habilidades = DeserializarSeguro<List<vmHabilidad>>(fila["HABILIDADES"]),
        //                    Idiomas = DeserializarSeguro<List<vmIdioma>>(fila["IDIOMAS"]),
        //                    ManejoSoftware = DeserializarSeguro<List<vmManejoSoftware>>(fila["MANEJO_SOFTWARE"]),
        //                    Certificados = DeserializarSeguro<List<vmCertificado>>(fila["CERTIFICADOS"]),
        //                    Imagen = fila["IMAGEN"] != DBNull.Value ? fila["IMAGEN"].ToString() : string.Empty
        //                };
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Loguear el error si tienes un sistema de logs
        //            // throw; // Opcional: volver a lanzar el error o retornar el objeto vacío
        //        }

        //        return oCurriculum;
        //    }

        //    // --- AGREGA ESTE MÉTODO AUXILIAR EN TU CLASE ---
        //    // Sirve para que si el JSON viene nulo, vacío o corrupto, no truene el sistema
        //    private static T DeserializarSeguro<T>(object dbValue) where T : class, new()
        //    {
        //        if (dbValue == DBNull.Value || dbValue == null) return new T(); // Retorna lista vacía si es null

        //        string json = dbValue.ToString();
        //        if (string.IsNullOrWhiteSpace(json)) return new T();

        //        try
        //        {
        //            return JsonConvert.DeserializeObject<T>(json) ?? new T();
        //        }
        //        catch
        //        {
        //            return new T(); // Si el JSON está mal formado, no rompe, retorna vacío
        //        }
        //    }
    }
}
