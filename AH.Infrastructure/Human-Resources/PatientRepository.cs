using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AH.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ILogger<PatientRepository> _logger;

        public PatientRepository(ILogger<PatientRepository> logger)
        {
            _logger = logger;
        }

        public async Task<ListResponseDTO<PatientRowDTO>> GetAllAsync(PatientFilterDTO filterDTO)
        {
            return await ReusableCRUD.GetAllAsync<PatientRowDTO, PatientFilterDTO>("Fetch_Patients", _logger, filterDTO, cmd =>
            {
                PersonHelper.AddPersonParameters(filterDTO, cmd);
                ReceptionistAuditHelper.AddReceptionistAuditParameters(filterDTO.CreatedByReceptionistID,
                    filterDTO.CreatedAtFrom, filterDTO.CreatedAtTo, cmd);
            }, (reader, converter) =>

                new PatientRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("FullName")), null
            );
        }

        public async Task<ListResponseDTO<PatientRowDTO>> GetForDoctorAsync(int doctorID)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<Patient> GetByIDAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Patient patient)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}