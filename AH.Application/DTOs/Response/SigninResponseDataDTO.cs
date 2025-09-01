using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Response
{
    public class SigninResponseDataDTO
    {
        public int ID { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public SigninResponseDataDTO(int id, string role)
        {
            ID = id;
            Role = role;
            Token = string.Empty;
            RefreshToken = string.Empty;
        }

        public SigninResponseDataDTO(SigninResponseDTO signinResponseDTO)
        {
            ID = signinResponseDTO.ID;
            Role = signinResponseDTO.Role;
            Token = string.Empty;
            RefreshToken = string.Empty;
        }
    }
}