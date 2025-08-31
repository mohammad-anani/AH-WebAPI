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
    public class SigninService : ISigninService
    {
        private readonly ISigninRepository signinRepository;

        public SigninService(ISigninRepository signinRepository)
        {
            this.signinRepository = signinRepository;
        }

        public async Task<SigninResponseDataDTO> SigninAsync(string email, string password)
        {
            return new SigninResponseDataDTO(await signinRepository.SigninAsync(email, password));
        }
    }
}