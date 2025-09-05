using AH.Application.DTOs.Create;
using AH.Application.DTOs.Response;
using AH.Application.IRepositories;
using AH.Application.IServices;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AuthService> logger;

        public AuthService(ISigninRepository signinRepository, IJwtService jwtService, IOptions<RefreshTokenOptions> options,ILogger<AuthService> logger)
        {
            this.signinRepository = signinRepository;
            this.jwtService = jwtService;
            this._options = options.Value;
            this.logger = logger;
        }

        public async Task<SigninResponseDataDTO> SigninAsync(string email, string password)
        {
            var response = await signinRepository.SigninAsync(email, CreatePersonDTO.HashPassword(password));

            if(response.Exception!=null)
            {
                logger.LogError(response.Exception.Message);
            }

            var responseData = new SigninResponseDataDTO(response);

            // Generate access and refresh tokens
            jwtService.CreateToken(responseData);
            responseData.RefreshToken = JwtService.GenerateRefreshToken();

            bool saved = await UpdateUserRefreshTokenAsync(email, responseData.RefreshToken, DateTime.Now.AddMinutes(Int32.Parse(_options.ExpireInMinutes)));

            return responseData;
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