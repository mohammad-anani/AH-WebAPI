using AH.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.IRepositories
{
    public interface ISigninRepository
    {
        public Task<SigninResponseDTO> SigninAsync(string email, string password);
    }
}