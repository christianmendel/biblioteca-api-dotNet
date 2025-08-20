using Biblioteca.Models;
using Biblioteca.Settings.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Biblioteca.Services
{
    public class TokenService
    {
        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("_id", usuario._id),
            };
            var secretKey = Encoding.UTF8.GetBytes(TokenSettings.SecretKey);
            var chave = new SymmetricSecurityKey(secretKey);

            var signingCredentials =
                new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    expires: DateTime.UtcNow.AddMinutes(TokenSettings.ExpiresInMinutes),
                    claims: claims,
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}