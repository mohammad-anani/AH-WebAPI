using AH.Application.DTOs.Entities.Services;
using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Create
{
    public class AddUpdateOperationDoctorDTO
    {
        public List<OperationDoctorDTO> OperationDoctors { get; set; }

        public int OperationID { get; set; }

        public AddUpdateOperationDoctorDTO(List<OperationDoctorDTO> operationDoctors, int operationID)
        {
            if (operationDoctors.Count > 5 || operationDoctors.Count == 0)
            {
                throw new ArgumentException("OperationDoctors list must contain between 1 and 5 items.");
            }

            OperationDoctors = operationDoctors;
            OperationID = operationID;
        }

        public DataTable ToDatatable()
        {
            var table = new DataTable();
            table.Columns.Add("DoctorID", typeof(int));
            table.Columns.Add("Role", typeof(string));

            OperationDoctors.ForEach(OperationDoctors =>
            {
                var row = table.NewRow();
                row["DoctorID"] = OperationDoctors.DoctorID;
                row["Role"] = OperationDoctors.Role;
                table.Rows.Add(row);
            });

            return table;
        }
    }
}