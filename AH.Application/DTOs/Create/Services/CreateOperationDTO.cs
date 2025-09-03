using AH.Application.DTOs.Entities.Services;
using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AH.Application.DTOs.Create
{
    public class CreateOperationDTO : OperationFormDTO
    {
        public CreateOperationDTO() : base()
        {
        }

        public CreateOperationDTO(int operationName, int departmentID, string description, List<OperationDoctorDTO> operationDoctors, int patientID, DateTime scheduledDate, string reason, string? notes, int billAmount, int createdByReceptionistID)
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
            // The following fields belong to CreateServiceDTO which is not a base anymore. Keep ToOperation using direct construction
            _patientID = patientID;
            _scheduledDate = scheduledDate;
            Reason = reason;
            Notes = notes;
            _billAmount = billAmount;
            _createdByReceptionistID = createdByReceptionistID;
        }

        // Local fields to carry create-only service data
        private int _patientID;
        private DateTime _scheduledDate;
        private int _billAmount;
        private int _createdByReceptionistID;

        public Operation ToOperation()
        {
            return new Operation(
                OperationName,
                new Department(DepartmentID),
                Description,
                new Service(
                    new Patient(_patientID),
                    _scheduledDate,
                    null,
                    Reason,
                    null,
                    null,
                    "",
                    Notes,
                    new Bill(-1, _billAmount, 0),
                    new Receptionist(_createdByReceptionistID)
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
                row["DoctorID"] = operationDoctor.DoctorID;
                row["Role"] = operationDoctor.Role;
                table.Rows.Add(row);
            });

            return table;
        }
    }
}