using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Row
{
    public class DepartmentRowDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }

        public DepartmentRowDTO(int id, string name, string phone)
        {
            ID = id;
            Name = name;
            Phone = phone;
        }
        public DepartmentRowDTO()
        {
            ID = -1;
            Name = string.Empty;
            Phone = string.Empty;
        }
    }
}
