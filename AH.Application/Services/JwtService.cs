using AH.Application.DTOs.Response;
using AH.Application.IServices;
using Jose;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AH.Application.Services
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Key { get; set; } = default!;
        public int ExpireInMinutes { get; set; } = 10; // default to 10 min
    }

    public class JwtService : IJwtService
    {
        private readonly JwtOptions _options;
        private readonly SigningCredentials _creds;

        public JwtService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            _creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        public string CreateToken(SigninResponseDataDTO user)
        {
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Role, user.Role),     // enables [Authorize(Roles="Admin")]
                new Claim("role", user.Role),               // optional extra
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_options.ExpireInMinutes),
                signingCredentials: _creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}