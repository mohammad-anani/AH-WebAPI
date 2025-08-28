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
    public class CreateOperationDTO : CreateServiceDTO
    {
        public int OperationName { get; set; }
        public int DepartmentID { get; set; }
        public string Description { get; set; }
        public List<OperationDoctorDTO> OperationDoctors { get; set; }

        public CreateOperationDTO() : base()
        {
            OperationName = -1;
            DepartmentID = -1;
            Description = string.Empty;
            OperationDoctors = new List<OperationDoctorDTO>();
        }

        public CreateOperationDTO(int operationName, int departmentID, string description, List<OperationDoctorDTO> operationDoctors, int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
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

        public AddUpdateOperationDTO ToAddUpdateOperationDTO()
        {
            return new AddUpdateOperationDTO(
                new Operation(
                OperationName,
                new Department(DepartmentID),
                Description,
                base.ToService()
                    ), OperationDoctors
            );
        }
    }
}