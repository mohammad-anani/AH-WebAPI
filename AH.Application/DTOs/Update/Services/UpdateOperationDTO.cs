using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities.Services;
using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Update
{
    public class UpdateOperationDTO : UpdateServiceDTO
    {
        [Required(ErrorMessage = "Operation ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Operation ID must be a positive number")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Operation name is required")]
        [Range(10, 100, ErrorMessage = "Operation name must be between 10 and 100")]
        public int OperationName { get; set; }

        [Required(ErrorMessage = "Department ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Department ID must be a positive number")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(int.MaxValue, MinimumLength = 10, ErrorMessage = "Description must be at least 10 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Operation doctors list is required")]
        [MinLength(1, ErrorMessage = "At least 1 doctor is required")]
        [MaxLength(5, ErrorMessage = "Maximum 5 doctors allowed")]
        public List<OperationDoctorDTO> OperationDoctors { get; set; }

        public UpdateOperationDTO() : base()
        {
            ID = -1;
            OperationName = -1;
            DepartmentID = -1;
            Description = string.Empty;
            OperationDoctors = new List<OperationDoctorDTO>();
        }

        public UpdateOperationDTO(int operationName, int departmentID, string description, List<OperationDoctorDTO> operationDoctors, int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
            : base(patientID, scheduledDate, actualStartingDate, reason, result, resultDate, status, notes, billAmount, createdByReceptionistID)
        {
            if (operationDoctors.Count > 5 || operationDoctors.Count == 0)
            {
                throw new ArgumentException("OperationDoctors list must contain between 1 and 5 items.");
            }

            OperationName = operationName;
            DepartmentID = departmentID;
            Description = description;
            OperationDoctors = operationDoctors;
        }

        public Operation ToOperation()
        {
            return new Operation(
                OperationName,
                new Department(DepartmentID),
                Description,
                base.ToService()
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
                row["DoctorID"] = operationDoctor.DoctorID;
                row["Role"] = operationDoctor.Role;
                table.Rows.Add(row);
            });

            return table;
        }
    }
}