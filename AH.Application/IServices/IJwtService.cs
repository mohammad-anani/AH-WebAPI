using AH.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.IServices
{
    public interface IJwtService
    {
        SigninResponseDataDTO CreateToken(SigninResponseDataDTO user);

        (ClaimsPrincipal? Principal, bool IsExpired) GetPrincipalFromJwtToken(string? token);
    }
}