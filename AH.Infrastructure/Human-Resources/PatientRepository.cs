using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task<GetAllResponseDTO<PatientRowDTO>> GetAllAsync(PatientFilterDTO filterDTO)
        {
            return await ReusableCRUD.GetAllAsync<PatientRowDTO, PatientFilterDTO>("Fetch_Patients", _logger, filterDTO, cmd =>
            {
                PersonHelper.AddPersonParameters(filterDTO, cmd);
                ReceptionistAuditHelper.AddReceptionistAuditParameters(filterDTO.CreatedByReceptionistID,
                    filterDTO.CreatedAtFrom, filterDTO.CreatedAtTo, cmd);
            }, (reader, converter) =>

                new PatientRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("FullName"), converter.ConvertValue<int>("Age"),
                                    converter.ConvertValue<string>("Phone")), null
            );
        }

        public async Task<GetAllResponseDTO<PatientRowDTO>> GetAllForDoctorAsync(int doctorID, PatientFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["DoctorID"] = (doctorID, SqlDbType.Int, null, null),
            };

            return await ReusableCRUD.GetAllAsync<PatientRowDTO, PatientFilterDTO>("Fetch_PatientsForDoctor", _logger, filterDTO, cmd =>
            {
                PersonHelper.AddPersonParameters(filterDTO, cmd);
                ReceptionistAuditHelper.AddReceptionistAuditParameters(filterDTO.CreatedByReceptionistID,
                    filterDTO.CreatedAtFrom, filterDTO.CreatedAtTo, cmd);
            }, (reader, converter) =>

                new PatientRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("FullName"), converter.ConvertValue<int>("Age"),
                                    converter.ConvertValue<string>("Phone")), null
      );
        }

        public async Task<GetByIDResponseDTO<PatientDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<PatientDTO>("Fetch_PatientByID", _logger, id, null, (reader, converter) =>
            {
                Person person = PersonHelper.ReadPerson(reader);
                return new PatientDTO(converter.ConvertValue<int>("ID"), person, ReceptionistAuditHelper.ReadReceptionist(reader),
                    converter.ConvertValue<DateTime>("CreatedAt"));
            });
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

        public static PatientRowDTO ReadPatient(SqlDataReader reader)
        {
            ConvertingHelper converter = new ConvertingHelper(reader);

            return new PatientRowDTO(

                 converter.ConvertValue<int>("PatientID"),

 converter.ConvertValue<string>("PatientFullName"), converter.ConvertValue<int>("Age"), converter.ConvertValue<string>("Phone"));
        }
    }
}