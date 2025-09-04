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
            // Ensure key length meets HS256 requirement (>= 256 bits / 32 bytes)
            var keyBytes = Encoding.UTF8.GetBytes(_options.Key ?? string.Empty);
            if (keyBytes.Length < 32)
            {
                // Pad the key deterministically to 32 bytes minimum
                Array.Resize(ref keyBytes, 32);
            }
            var key = new SymmetricSecurityKey(keyBytes);
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

            var keyBytes = Encoding.UTF8.GetBytes(_options.Key ?? string.Empty);
            if (keyBytes.Length < 32)
            {
                Array.Resize(ref keyBytes, 32);
            }

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _options.Audience,
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
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