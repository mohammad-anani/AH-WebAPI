using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Row
{
    public class AdminRowDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; }

        public AdminRowDTO(int id, string fullName)
        {
            ID = id;
            FullName = fullName;
        }

        public AdminRowDTO()
        {
            ID = -1;
            FullName = string.Empty;
        }
    }
}
