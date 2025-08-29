using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;

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
                ServiceHelper.AddServiceFilterParameters(filterDTO, cmd);
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

        public async Task<CreateResponseDTO> AddAsync(Appointment appointment)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["DoctorID"] = (appointment.Doctor.ID, SqlDbType.Int, null, null),
            };

            return await ReusableCRUD.AddAsync("Create_Appointment", _logger, cmd =>
            {
                ServiceHelper.AddCreateServiceParameters(appointment.Service, cmd);

                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            });
        }

        public async Task<CreateResponseDTO> AddFromPreviousAppointmentAsync(CreateAppointmentFromPreviousDTO app)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["TestOrderID"] = (app.AppointmentID, SqlDbType.Int, null, null),
                ["ScheduledDate"] = (app.ScheduledDate, SqlDbType.DateTime, null, null),
                ["Notes"] = (app.Notes, SqlDbType.NVarChar, -1, null),
                ["CreatedByReceptionistID"] = (app.CreatedByReceptionistID, SqlDbType.Int, null, null),
                ["Status"] = (3, SqlDbType.TinyInt, null, null)
            };

            return await ReusableCRUD.AddAsync("Create_AppointmentFromPreviousAppointment", _logger, cmd =>
            {
                SqlParameterHelper.AddParametersFromDictionary(cmd, parameters);
            });
        }

        public async Task<SuccessResponseDTO> UpdateAsync(Appointment appointment)
        {
            return await ReusableCRUD.UpdateAsync("Update_Appointment", _logger, appointment.ID, cmd =>
            {
                ServiceHelper.AddCreateServiceParameters(appointment.Service, cmd);
            });
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_Appointment", _logger, id);
        }

        public async Task<SuccessResponseDTO> StartAsync(int id, string? notes)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Start_Appointment", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> CancelAsync(int id, string? notes)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Cancel_Appointment", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> CompleteAsync(int id, string? notes, string result)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Notes"] = (notes, SqlDbType.NVarChar, null, null),
                ["Result"] = (result, SqlDbType.NVarChar, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Complete_Appointment", _logger, id, extraParams);
        }

        public async Task<SuccessResponseDTO> RescheduleAsync(int id, string? notes, DateTime newScheduledDate)
        {
            var extraParams = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["ScheduledDate"] = (newScheduledDate, SqlDbType.DateTime, null, null),
            };

            return await ReusableCRUD.ExecuteByIDAsync("Reschedule_Appointment", _logger, id, extraParams);
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