using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Entities.Services;
using AH.Application.DTOs.Form;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AH.Application.DTOs.Create
{
    public class CreateOperationDTO : CreateServiceDTO
    {
        [Required(ErrorMessage = "Operation name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Department ID must be a positive number")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Bill amount is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Bill amount must be a positive number")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Description must be at least 10 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Operation doctors list is required")]
        [MinLength(1, ErrorMessage = "At least 1 doctor is required")]
        [MaxLength(5, ErrorMessage = "Maximum 5 doctors allowed")]
        public List<OperationDoctorDTO> OperationDoctors { get; set; } = new();

        public CreateOperationDTO() : base()
        {
        }

        public CreateOperationDTO(string operationName, int departmentID, string description, List<OperationDoctorDTO> operationDoctors, int patientID, DateTime scheduledDate, string reason, string? notes, int billAmount, int createdByReceptionistID)
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
            PatientID = patientID;
            ScheduledDate = scheduledDate;
            Reason = reason;
            Notes = notes;
            Amount = billAmount;
            CreatedByReceptionistID = createdByReceptionistID;
        }

        public Operation ToOperation()
        {
            return new Operation(
                Name,
                new Department(DepartmentID),
                Description,
                new Service(
                    new Patient(PatientID),
                    ScheduledDate,
                    null,
                    Reason,
                    null,
                    null,
                    "Scheduled",
                    Notes,
                    new Bill(-1, Amount, 0),
                    new Receptionist(CreatedByReceptionistID)
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

            OperationDoctors.ForEach(operationDoctor =>
            {
                var row = table.NewRow();
                row["DoctorID"] = operationDoctor.ID;
                row["Role"] = operationDoctor.Role;
                table.Rows.Add(row);
            });

            return table;
        }
    }
}