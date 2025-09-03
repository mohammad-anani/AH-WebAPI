using AH.Application.DTOs.Form;
using AH.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace AH.Application.DTOs.Update
{
    public class UpdateServiceDTO : ServiceFormDTO
    {
        public UpdateServiceDTO()
        {
        }

        public UpdateServiceDTO(int patientID, DateTime scheduledDate, DateTime? actualStartingDate, string reason, string? result, DateTime? resultDate, string status, string? notes, int billAmount, int createdByReceptionistID)
        {
            Reason = reason;
            Notes = notes;
        }

        public Service ToService()
        {
            return new Service(
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
            );
        }
    }
}