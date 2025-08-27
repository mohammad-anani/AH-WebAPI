using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using System.Data;
using AH.Application.DTOs.Entities;
using Microsoft.Data.SqlClient;

namespace AH.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ILogger<AppointmentRepository> _logger;

        public AppointmentRepository(ILogger<AppointmentRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllAsync(AppointmentFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["PreviousAppointmentID"] = (filterDTO.PreviousAppointmentID, SqlDbType.Int, null, null),
                ["DoctorID"] = (filterDTO.DoctorID, SqlDbType.Int, null, null),
            };

            return await ReusableCRUD.GetAllAsync<AppointmentRowDTO, AppointmentFilterDTO>("Fetch_Appointments", _logger, filterDTO, cmd =>
            {
                ServiceHelper.AddServiceParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new AppointmentRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("PatientFullName"),
                                    converter.ConvertValue<string>("DoctorFullName"),
                                    converter.ConvertValue<bool>("IsFollowUp"),
                                    converter.ConvertValue<DateTime>("ScheduledDate"),
                                    converter.ConvertValue<string>("Status"), converter.ConvertValue<bool>("IsPaid"))
            , parameters);
        }

        public async Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllByDoctorIDAsync(int doctorID)
        {
            var filterDTO = new AppointmentFilterDTO { DoctorID = doctorID };
            return await this.GetAllAsync(filterDTO);
        }

        public async Task<GetAllResponseDTO<AppointmentRowDTO>> GetAllByPatientIDAsync(int patientID)
        {
            var filterDTO = new AppointmentFilterDTO { PatientID = patientID };
            return await this.GetAllAsync(filterDTO);
        }

        public async Task<GetByIDResponseDTO<AppointmentDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<AppointmentDTO>("Fetch_AppointmentByID", _logger, id, null, (reader, converter) =>
            new AppointmentDTO(converter.ConvertValue<int>("ID"), ReadAppointment(reader, "Previous"),
DoctorRepository.ReadDoctor(reader),

                    ServiceHelper.ReadService(reader)

            ));
        }

        public async Task<int> AddAsync(Appointment appointment)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddFromPreviousAppointmentAsync(Appointment appointment)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_Appointment", _logger, id);
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

        public static AppointmentRowDTO ReadAppointment(SqlDataReader reader, string? prefix)
        {
            var converter = new ConvertingHelper(reader);
            return new AppointmentRowDTO(converter.ConvertValue<int>(prefix + "AppointmenID"),
                                        converter.ConvertValue<string>(prefix + "AppointmentPatientFullName"),
                                        converter.ConvertValue<string>(prefix + "AppointmentDoctorFullName"),
                                        converter.ConvertValue<bool>(prefix + "AppointmentIsFollowUp"),
                                        converter.ConvertValue<DateTime>(prefix + "AppointmentScheduledDate"),
                                        converter.ConvertValue<string>(prefix + "AppointmentStatus"),
                                        converter.ConvertValue<bool>(prefix + "AppointmentIsPaid"));
        }
    }
}