using AH.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.IServices
{
    public interface ISigninService
    {
        public Task<SigninResponseDataDTO> SigninAsync(string email, string password);
    }
}