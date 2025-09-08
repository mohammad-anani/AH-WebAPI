using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities.Services;
using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AH.Application.DTOs.Update
{
    public class UpdateOperationDTO : UpdateServiceDTO
    {
        [BindNever]
        [Required(ErrorMessage = "Operation ID is required")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Operation name is required")]
        [Range(10, 100, ErrorMessage = "Operation name must be between 10 and 100")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Department ID must be a positive number")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Description must be at least 10 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Operation doctors list is required")]
        [MinLength(1, ErrorMessage = "At least 1 doctor is required")]
        [MaxLength(5, ErrorMessage = "Maximum 5 doctors allowed")]
        public List<OperationDoctorDTO> OperationDoctors { get; set; } = new();

        public UpdateOperationDTO() : base()
        {
            ID = -1;
        }

        public UpdateOperationDTO(string operationName, int departmentID, string description, List<OperationDoctorDTO> operationDoctors, int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
            : base()
        {
            if (operationDoctors.Count > 5 || operationDoctors.Count == 0)
            {
                throw new ArgumentException("OperationDoctors list must contain between 1 and 5 items.");
            }

            Name = operationName;
            DepartmentID = departmentID;
            Description = description;
            OperationDoctors = operationDoctors;
            Reason = reason;
            Notes = notes;
        }

        public Operation ToOperation()
        {
            return new Operation(
                ID,
                Name,
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
                row["DoctorID"] = OperationDoctors.ID;
                row["Role"] = OperationDoctors.Role;
                table.Rows.Add(row);
            });

            return table;
        }
    }
}