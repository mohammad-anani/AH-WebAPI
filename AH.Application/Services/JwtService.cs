using AH.Application.DTOs.Response;
using AH.Application.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public SigninResponseDataDTO CreateToken(SigninResponseDataDTO user)
        {
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("role", user.Role),
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

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            user.Token = tokenString;
            user.RefreshToken = GenerateRefreshToken();

            return user;
        }

        public static string GenerateRefreshToken(int size = 64)
        {
            var randomBytes = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes)
                          .Replace('+', '-')
                          .Replace('/', '_')
                          .TrimEnd('=');
        }

        public (ClaimsPrincipal? Principal, bool IsExpired) GetPrincipalFromJwtToken(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return (null, true);

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _options.Audience,
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
                ValidateLifetime = false // allow reading expired tokens
            };

            var jwtHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = jwtHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (securityToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return (null, true);
                }

                // Determine expiration using the validated token's ValidTo (UTC)
                var isExpired = DateTime.UtcNow >= jwtToken.ValidTo.ToUniversalTime();
                return (principal, isExpired);
            }
            catch
            {
                return (null, true);
            }
        }
    }
}