using InnoClinic.Shared.Constants;
using InnoClinic.Shared.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InnoClinic.Shared.Generators
{
    public static class InternalServiceTokenGenerator
    {
        public static string Generate(JwtSettings settings)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, Roles.InternalService)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
