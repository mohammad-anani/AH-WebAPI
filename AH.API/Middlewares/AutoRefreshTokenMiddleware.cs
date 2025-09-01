using AH.Application.DTOs.Response;
using AH.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AH.API.Middleware
{
    public class AutoRefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _refreshExpireMinutes;

        public AutoRefreshTokenMiddleware(RequestDelegate next, IOptions<AH.Application.Services.RefreshTokenOptions> refreshOptions)
        {
            _next = next;
            _refreshExpireMinutes = int.TryParse(refreshOptions.Value.ExpireInMinutes, out var m) ? m : 60;
        }

        public async Task InvokeAsync(HttpContext context, IJwtService jwtService, IAuthService authService)
        {
            // Prefer standard Authorization header, fallback to custom "token"
            string authHeader = context.Request.Headers["Authorization"].FirstOrDefault() ?? string.Empty;
            string token = string.Empty;
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", System.StringComparison.OrdinalIgnoreCase))
                token = authHeader.Substring("Bearer ".Length).Trim();
            if (string.IsNullOrEmpty(token))
                token = context.Request.Headers["token"].FirstOrDefault()?.Split(' ').Last() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(token))
            {
                // No token provided: let pipeline continue (for AllowAnonymous endpoints)
                await _next(context);
                return;
            }

            var (principal, isExpired) = jwtService.GetPrincipalFromJwtToken(token);

            if (principal == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Token");
                return;
            }

            if (!isExpired)
            {
                context.User = principal;
                await _next(context);
                return;
            }

            // Token expired → try refresh
            var refreshToken = context.Request.Headers["refreshToken"].FirstOrDefault();
            if (string.IsNullOrEmpty(refreshToken))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token expired, no refresh token.");
                return;
            }

            // Extract user ID and role from expired token
            int userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            string role = principal.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

            var (storedRefreshToken, expiryDate) = await authService.GetRefreshTokenByUserAsync(userId, role);
            if (storedRefreshToken == null || storedRefreshToken != refreshToken || !expiryDate.HasValue || expiryDate.Value <= DateTime.UtcNow)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Refresh token mismatch or expired.");
                return;
            }

            // Create new tokens and persist new refresh token (rotate)
            var newTokens = jwtService.CreateToken(new SigninResponseDataDTO(userId, role));
            var newRefreshExpiry = DateTime.UtcNow.AddMinutes(_refreshExpireMinutes);
            await authService.UpdateUserRefreshTokenAsync(userId, role, newTokens.RefreshToken, newRefreshExpiry);

            // Assign updated ClaimsPrincipal
            var newPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            }, "Jwt"));
            context.User = newPrincipal;

            // Send new tokens via headers
            context.Response.Headers["Authorization"] = $"Bearer {newTokens.Token}";
            context.Response.Headers["token"] = newTokens.Token;
            context.Response.Headers["refreshToken"] = newTokens.RefreshToken ?? string.Empty;

            await _next(context);
        }
    }

    public static class AutoRefreshTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseAutoRefreshToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AutoRefreshTokenMiddleware>();
        }
    }
}