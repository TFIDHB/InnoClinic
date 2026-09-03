using InnoClinic.Shared.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Appointments.API.Tests.Integration
{
    public static class JwtTestTokenFactory
    {
        public const string Secret = "test_secret_key_1234567890_1234567890_1234567890";
        public const string Issuer = "InnoClinic.Test";
        public const string Audience = "InnoClinic.Test.Clients";

        private static string Create(Guid accountId, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string CreatePatientToken(Guid accountId)
        {
            return Create(accountId, Roles.Patient);
        }

        public static string CreateDoctorToken(Guid accountId)
        {
            return Create(accountId, Roles.Doctor);
        }
    }
}