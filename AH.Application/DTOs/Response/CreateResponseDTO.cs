using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Response
{
    public class CreateResponseDTO
    {
        public int ID { get; set; }

        public Exception? Exception { get; set; }

        public CreateResponseDTO(int ID, Exception? exception)
        {
            this.ID = ID;
            this.Exception = exception;
        }
    }
}