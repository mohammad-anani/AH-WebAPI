using AH.Application.DTOs.Response;
using AH.Application.IServices;
using Jose;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
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

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            user.Token = tokenString;
            user.RefreshToken = GenerateRefreshToken();

            return user;
        }

        public static string GenerateRefreshToken(int size = 64)
        {
            // size = number of random bytes (64 = 512-bit token)
            var randomBytes = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            // Convert to URL-safe base64 string
            return Convert.ToBase64String(randomBytes)
                          .Replace('+', '-')  // URL safe
                          .Replace('/', '_')  // URL safe
                          .TrimEnd('=');      // remove padding
        }

        public (ClaimsPrincipal? Principal, bool IsExpired) GetPrincipalFromJwtToken(string? token)
        {
            if (token == null)
                return (null, true);

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _options.Audience,
                ValidateIssuer = true,
                ValidIssuer = _options.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
                ValidateLifetime = false // We'll handle expiration manually
            };

            var jwtHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal;

            try
            {
                principal = jwtHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (securityToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return (null, true);
                }

                // Check expiration manually
                var expClaim = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp)?.Value;
                bool isExpired = true;
                if (!string.IsNullOrEmpty(expClaim) && long.TryParse(expClaim, out var expSeconds))
                {
                    var expDate = DateTimeOffset.FromUnixTimeSeconds(expSeconds).UtcDateTime;
                    isExpired = DateTime.UtcNow >= expDate;
                }

                return (principal, isExpired);
            }
            catch
            {
                return (null, true); // invalid token treated as expired
            }
        }
    }
}