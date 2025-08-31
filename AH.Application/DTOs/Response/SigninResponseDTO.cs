using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Response
{
    public class SigninResponseDTO
    {
        public int ID { get; set; }

        public string Role { get; set; }

        private Exception? Exception { get; set; }

        public SigninResponseDTO(int id, string role, Exception? exception)
        {
            ID = id;
            Role = role;
            Exception = exception;
        }
    }
}