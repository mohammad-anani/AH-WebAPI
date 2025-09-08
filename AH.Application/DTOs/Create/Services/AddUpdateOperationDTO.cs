using AH.Application.DTOs.Entities.Services;
using AH.Domain.Entities;
using System.Data;

namespace AH.Application.DTOs.Create
{
    public class AddUpdateOperationDTO
    {
        public Operation Operation { get; set; }
        public List<OperationDoctorDTO> OperationDoctors { get; set; }

        public AddUpdateOperationDTO(Operation operation, List<OperationDoctorDTO> operationDoctors)
        {
            if (operationDoctors.Count > 5 || operationDoctors.Count == 0)
            {
                throw new ArgumentException("OperationDoctors list must contain between 1 and 5 items.");
            }

            OperationDoctors = operationDoctors;
            Operation = operation;
        }

        public DataTable ToDatatable()
        {
            var table = new DataTable();
            table.Columns.Add("DoctorID", typeof(int));
            table.Columns.Add("Role", typeof(string));

            OperationDoctors.ForEach(OperationDoctors =>
            {
                var row = table.NewRow();
                row["DoctorID"] = OperationDoctors.ID;
                row["Role"] = OperationDoctors.Role;
                table.Rows.Add(row);
            });

            return table;
        }
    }
}