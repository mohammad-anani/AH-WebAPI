using AH.Application.DTOs.Response;
using AH.Application.IServices;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AH.API.Middleware
{
    public class AutoRefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public AutoRefreshTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJwtService jwtService, IAuthService authService)
        {
            // Get token from header
            string token = context.Request.Headers["token"].FirstOrDefault()?.Split(" ").Last() ?? string.Empty;

            // Parse token into ClaimsPrincipal, check if expired
            var (principal, isExpired) = jwtService.GetPrincipalFromJwtToken(token);

            if (principal == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Token");
                return; // stop pipeline
            }

            if (!isExpired)
            {
                // Token valid → continue normally
                context.User = principal;
                await _next(context);
                return;
            }

            // Token expired → try refresh
            var refreshToken = context.Request.Headers["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token expired, no refresh token.");
                return;
            }

            // Extract user ID and role from expired token
            int userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            string role = principal.FindFirst(ClaimTypes.Role)?.Value ?? "";

            // Validate refresh token using userId
            var (storedRefreshToken, expiryDate) = await authService.GetRefreshTokenByUserAsync(userId, role);
            if (storedRefreshToken == null || storedRefreshToken != refreshToken || expiryDate <= DateTime.UtcNow)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Refresh token mismatch or expired.");
                return;
            }

            // Create new tokens
            var newTokens = jwtService.CreateToken(new SigninResponseDataDTO(userId, role));

            // Build ClaimsPrincipal directly from new token (no extra parsing)
            var newPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            }, "Jwt"));

            // Assign the new ClaimsPrincipal to HttpContext.User
            context.User = newPrincipal;

            // Send new tokens to client via custom headers
            context.Response.Headers["token"] = newTokens.Token;
            context.Response.Headers["refreshToken"] = newTokens.RefreshToken ?? "";

            // Continue to next middleware/controller
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