using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FACPYA.BolsaDeTrabajo.Logica.Helpers
{
    public class TokenHelper
    {
        private readonly string secretKey;
        public TokenHelper(string secretKey)
        {
            this.secretKey = secretKey;
        }

        // Método para generar un JSON Web Token
        public string GenerarToken(string pNombreToken, dynamic pDatos)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey); // Encriptamos la 'secretKey' por motivos de seguridad

            // Configura los parámetros del token, incluyendo las datos, expiración y firma.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                // Aquí puedes especificar todos los datos que necesitas guardar. Puedes utilizar los predefinidos con 'ClaimTypes' o crear nuevos utilizando comillas dobles
                new Claim(ClaimTypes.NameIdentifier, pNombreToken),
                new Claim("data", JsonConvert.SerializeObject(pDatos))
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Tiempo de expiración del token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Crea el token a partir de la descripción anterior.
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // Devuelve el token en formato string (compacto).
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal ValidarToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            // Define los parámetros de validación para el token.
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // No valida el emisor del token.
                ValidateAudience = false, // No valida la audiencia del token.
                ValidateIssuerSigningKey = true, // Valida que el token esté firmado correctamente.
                IssuerSigningKey = new SymmetricSecurityKey(key), // Clave secreta usada para validar la firma.
                ValidateLifetime = true, // Valida que el token no esté expirado.
                ClockSkew = TimeSpan.FromMinutes(5) // Permite un margen de 5 minutos para la expiración.
            };

            try
            {
                // Intenta validar el token. Si es válido, devuelve las reclamaciones.
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return principal;
            }
            catch
            {
                // Si ocurre un error (token inválido o expirado), devuelve null.
                return null; // Token inválido o expirado
            }
        }
    }
}
