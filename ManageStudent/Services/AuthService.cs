using ManageStudent.Options;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ManageStudent.Services
{
    public class AuthService(IOptions<JwtSettings> settings) : IAuthService
    {

        private readonly IOptions<JwtSettings> _settings = settings;
        private readonly JwtSecurityTokenHandler _handler = new();

        public async Task<string> GenerateJwtToken(int userId, string email, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Value.Issuers,
                audience: _settings.Value.Audiences,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                },
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );


            return _handler.WriteToken(token);

        }
    }
}
