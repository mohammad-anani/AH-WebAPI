using AH.Application.DTOs.Response;
using AH.Application.IRepositories;
using AH.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISigninRepository signinRepository;
        private readonly IJwtService jwtService;

        public AuthService(ISigninRepository signinRepository, IJwtService jwtService)
        {
            this.signinRepository = signinRepository;
            this.jwtService = jwtService;
        }

        public async Task<SigninResponseDataDTO> SigninAsync(string email, string password)
        {
            var response = new SigninResponseDataDTO(await signinRepository.SigninAsync(email, password));

            //add jwt token generation logic here and set to response.Token
            response.Token = jwtService.CreateToken(response);

            return response;
        }
    }
}