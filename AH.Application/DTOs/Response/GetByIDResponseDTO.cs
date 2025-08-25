using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Response
{
    public class GetByIDResponseDTO<T>
    {
        public T? Item { get; set; }

        public Exception? Exception { get; set; }

        public GetByIDResponseDTO(T? item, Exception? exception)
        {
            Item = item;
            Exception = exception;
        }
    }
}