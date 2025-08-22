using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Row
{
    public class ReceptionistRowDTO
    {

        public int ID { get; set; }
        public string FullName { get; set; }

        public ReceptionistRowDTO(int id, string fullName)
        {
            ID = id;
            FullName = fullName;
        }

        public ReceptionistRowDTO()
        {
            ID = -1;
            FullName = string.Empty;
        }
    }
}