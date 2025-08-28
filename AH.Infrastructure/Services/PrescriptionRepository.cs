using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AH.Infrastructure.Repositories
{
    public class PrescriptionRepository : IPrescriptionService
    {
        private readonly ILogger<PrescriptionRepository> _logger;

        public PrescriptionRepository(ILogger<PrescriptionRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<PrescriptionRowDTO>> GetAllByAppointmentIDAsync(PrescriptionFilterDTO filterDTO)
        {
            var extraParameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["AppointmentID"] = (filterDTO.AppointmentID, SqlDbType.Int, null, null),
                ["Page"] = (filterDTO.Page, SqlDbType.Int, null, null),
            };

            int totalCount = -1;
            List<PrescriptionRowDTO> items = new List<PrescriptionRowDTO>();
            ConvertingHelper converter = new ConvertingHelper();
            RowCountOutputHelper rowCountOutputHelper = new RowCountOutputHelper();

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                 "Fetch_Prescriptions", _logger, cmd =>
                 {
                     SqlParameterHelper.AddParametersFromDictionary(cmd, extraParameters);

                     rowCountOutputHelper.AddToCommand(cmd);
                 }, (reader, cmd) =>
                 {
                     items.Add(new PrescriptionRowDTO(
                         converter.ConvertValue<int>("ID"),
                         converter.ConvertValue<int>("AppointmentID"),
                         converter.ConvertValue<string>("Medication"),
                         converter.ConvertValue<string>("Dosage"),
                         converter.ConvertValue<string>("Frequency"),
                         converter.ConvertValue<bool>("IsOnMedication")));
                 }, null, (reader, cmd) => { converter = new ConvertingHelper(reader); });

            totalCount = rowCountOutputHelper.GetRowCount();

            return new GetAllResponseDTO<PrescriptionRowDTO>(items, totalCount, ex);
        }

        public async Task<CreateResponseDTO> AddAsync(Prescription prescription)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["AppointmentID"] = (prescription.Appointment.ID, SqlDbType.Int, null, null),
                ["Diagnosis"] = (prescription.Diagnosis, SqlDbType.NVarChar, 256, null),
                ["Medication"] = (prescription.Medication, SqlDbType.NVarChar, 256, null),
                ["Dosage"] = (prescription.Dosage, SqlDbType.NVarChar, 50, null),
                ["Frequency"] = (prescription.Frequency, SqlDbType.NVarChar, 256, null),
                ["MedicationStart"] = (prescription.MedicationStart, SqlDbType.DateTime, null, null),
                ["MedicationEnd"] = (prescription.MedicationEnd, SqlDbType.DateTime, null, null),
                ["Notes"] = (prescription.Notes, SqlDbType.NVarChar, -1, null)
            };

            return await ReusableCRUD.AddAsync("Create_Prescription", _logger, cmd =>
            {
                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            });
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_Prescription", _logger, id);
        }

        public async Task<GetByIDResponseDTO<PrescriptionDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<PrescriptionDTO>("Fetch_PrescriptionByID", _logger, id, null, (reader, converter) =>
               new PrescriptionDTO(converter.ConvertValue<int>("ID"),
                                   AppointmentRepository.ReadAppointment(reader, ""),
                                   converter.ConvertValue<string>("Diagnosis"),

                                   converter.ConvertValue<string>("Medication"),
                                   converter.ConvertValue<string>("Dosage"),
                                   converter.ConvertValue<string>("Frequency"),
                                   converter.ConvertValue<DateTime>("MedicationStart"),
                                converter.ConvertValue<DateTime>("MedicationEnd"), converter.ConvertValue<string>("Notes")
   )
               );
        }

        public async Task<SuccessResponseDTO> UpdateAsync(Prescription prescription)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["Diagnosis"] = (prescription.Diagnosis, SqlDbType.NVarChar, 256, null),
                ["Medication"] = (prescription.Medication, SqlDbType.NVarChar, 256, null),
                ["Dosage"] = (prescription.Dosage, SqlDbType.NVarChar, 50, null),
                ["Frequency"] = (prescription.Frequency, SqlDbType.NVarChar, 256, null),
                ["MedicationStart"] = (prescription.MedicationStart, SqlDbType.DateTime, null, null),
                ["MedicationEnd"] = (prescription.MedicationEnd, SqlDbType.DateTime, null, null),
                ["Notes"] = (prescription.Notes, SqlDbType.NVarChar, -1, null)
            };

            return await ReusableCRUD.UpdateAsync("Update_Prescription", _logger, prescription.ID, cmd =>
            {
                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            });
        }
    }
}