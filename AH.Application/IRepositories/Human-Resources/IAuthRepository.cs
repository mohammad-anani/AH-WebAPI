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

        public Task<(string? token, DateTime? ExpiryDate)> GetRefreshTokenByUserAsync(int id, string role);

        public Task<bool> UpdateUserRefreshTokenAsync(int userId, string role, string refreshToken, DateTime expiryDate);

        public Task<bool> UpdateUserRefreshTokenAsync(string email, string refreshToken, DateTime expiryDate);
    }
}

//add getuserrefreshtoken to iservices then update