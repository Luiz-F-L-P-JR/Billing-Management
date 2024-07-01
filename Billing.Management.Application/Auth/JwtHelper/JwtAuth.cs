
using Billing.Management.Application.Auth.JwtHelper.Interface;
using Billing.Management.Domain.Auth.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Billing.Management.Application.Auth.JwtHelper
{
    public class JwtAuth : IJwtAuth
    {
        private readonly IConfiguration? _configuration;

        public JwtAuth(IConfiguration? configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateToken(UserAuth user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name)
                }),

                Expires = DateTime.UtcNow.AddHours(24),

                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
