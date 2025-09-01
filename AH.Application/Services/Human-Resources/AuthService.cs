using AH.Application.DTOs.Response;
using AH.Application.IRepositories;
using AH.Application.IServices;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace AH.Application.Services
{
    public class RefreshTokenOptions
    {
        public string ExpireInMinutes { get; set; } = string.Empty;
    }

    public class AuthService : IAuthService
    {
        private readonly ISigninRepository signinRepository;
        private readonly IJwtService jwtService;
        private readonly RefreshTokenOptions _options;

        public AuthService(ISigninRepository signinRepository, IJwtService jwtService, IOptions<RefreshTokenOptions> options)
        {
            this.signinRepository = signinRepository;
            this.jwtService = jwtService;
            this._options = options.Value;
        }

        public async Task<SigninResponseDataDTO> SigninAsync(string email, string password)
        {
            var response = new SigninResponseDataDTO(await signinRepository.SigninAsync(email, password));

            // Generate access and refresh tokens
            jwtService.CreateToken(response);
            response.RefreshToken = JwtService.GenerateRefreshToken();

            bool saved = await UpdateUserRefreshTokenAsync(email, response.RefreshToken, DateTime.Now.AddMinutes(Int32.Parse(_options.ExpireInMinutes)));

            return response;
        }

        public async Task<(string? token, DateTime? ExpiryDate)> GetRefreshTokenByUserAsync(int id, string role)
        {
            return await signinRepository.GetRefreshTokenByUserAsync(id, role);
        }

        public async Task<bool> UpdateUserRefreshTokenAsync(int userId, string role, string refreshToken, DateTime expiryDate)
        {
            return await signinRepository.UpdateUserRefreshTokenAsync(userId, role, refreshToken, expiryDate);
        }

        public async Task<bool> UpdateUserRefreshTokenAsync(string email, string refreshToken, DateTime expiryDate)
        {
            return await signinRepository.UpdateUserRefreshTokenAsync(email, refreshToken, expiryDate);
        }
    }
}