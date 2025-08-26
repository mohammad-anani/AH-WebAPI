using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System.Data;
using AH.Application.DTOs.Entities;

namespace AH.Infrastructure.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly ILogger<OperationRepository> _logger;

        public OperationRepository(ILogger<OperationRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<OperationRowDTO>> GetAllAsync(OperationFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["DepartmentID"] = (filterDTO.DepartmentID, SqlDbType.Int, null, null),
                ["Name"] = (filterDTO.Name, SqlDbType.NVarChar, 100, null),
                ["Description"] = (filterDTO.Description, SqlDbType.NVarChar, -1, null),
            };

            return await ReusableCRUD.GetAllAsync<OperationRowDTO, OperationFilterDTO>("Fetch_Operations", _logger, filterDTO, cmd =>
            {
                ServiceHelper.AddServiceParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new OperationRowDTO(converter.ConvertValue<int>("ID"),
                converter.ConvertValue<string>("Name"),
                                    converter.ConvertValue<string>("PatientFullName"),
                                    converter.ConvertValue<DateTime>("ScheduledDate"),
                                    converter.ConvertValue<string>("Status"), converter.ConvertValue<bool>("IsPaid"))
            , parameters);
        }

        public async Task<GetAllResponseDTO<OperationRowDTO>> GetAllByDoctorIDAsync(int doctorID)
        {
            var filterDTO = new OperationFilterDTO { /* set any default values if needed */ };
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["DoctorID"] = (doctorID, SqlDbType.Int, null, null),
            };

            return await ReusableCRUD.GetAllAsync<OperationRowDTO, OperationFilterDTO>("Fetch_OperationsForDoctor", _logger, filterDTO, cmd =>
            {
                ServiceHelper.AddServiceParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new OperationRowDTO(converter.ConvertValue<int>("ID"),
                converter.ConvertValue<string>("Name"),
                                    converter.ConvertValue<string>("PatientFullName"),
                                    converter.ConvertValue<DateTime>("ScheduledDate"),
                                    converter.ConvertValue<string>("Status"), converter.ConvertValue<bool>("IsPaid"))
            , parameters);
        }

        public async Task<GetAllResponseDTO<OperationRowDTO>> GetAllByPatientIDAsync(int patientID)
        {
            var filterDTO = new OperationFilterDTO { PatientID = patientID };
            return await this.GetAllAsync(filterDTO);
        }

        public async Task<GetByIDResponseDTO<OperationDTO>> GetByIDAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Operation operation)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Operation operation)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> StartAsync(int id, string? notes)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> CancelAsync(int id, string? notes)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> CompleteAsync(int id, string? notes, string result)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> RescheduleAsync(int id, string? notes, DateTime newScheduledDate)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}