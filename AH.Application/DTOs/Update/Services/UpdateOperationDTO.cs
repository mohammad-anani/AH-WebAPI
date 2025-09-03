using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities.Services;
using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AH.Application.DTOs.Update
{
    public class UpdateOperationDTO : OperationFormDTO
    {
        [BindNever]
        [Required(ErrorMessage = "Operation ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Operation ID must be a positive number")]
        public int ID { get; set; }

        public UpdateOperationDTO() : base()
        {
            ID = -1;
        }

        public UpdateOperationDTO(int operationName, int departmentID, string description, List<OperationDoctorDTO> operationDoctors, int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
            : base()
        {
            if (operationDoctors.Count > 5 || operationDoctors.Count == 0)
            {
                throw new ArgumentException("OperationDoctors list must contain between 1 and 5 items.");
            }

            OperationName = operationName;
            DepartmentID = departmentID;
            Description = description;
            OperationDoctors = operationDoctors;
            Reason = reason;
            Notes = notes;
        }

        public Operation ToOperation()
        {
            return new Operation(
                OperationName,
                new Department(DepartmentID),
                Description,
                new Service(
                    new Patient(-1),
                    DateTime.MinValue,
                    null,
                    Reason,
                    null,
                    null,
                    "",
                    Notes,
                    new Bill(-1, 0, 0),
                    new Receptionist(-1)
                )
            );
        }

        public AddUpdateOperationDTO ToAddUpdateOperationDTO()
        {
            return new AddUpdateOperationDTO(
                ToOperation(),
                OperationDoctors
            );
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