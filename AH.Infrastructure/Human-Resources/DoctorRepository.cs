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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ILogger<DoctorRepository> _logger;

        public DoctorRepository(ILogger<DoctorRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<DoctorRowDTO>> GetAllAsync(DoctorFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["Specialization"] = (filterDTO.Specialization, SqlDbType.NVarChar, 100, null),
                ["CostPerAppointmentFrom"] = (filterDTO.CostPerAppointmentFrom, SqlDbType.Int, null, null),
                ["CostPerAppointmentTo"] = (filterDTO.CostPerAppointmentTo, SqlDbType.Int, null, null)
            };

            return await ReusableCRUD.GetAllAsync<DoctorRowDTO, DoctorFilterDTO>("Fetch_Doctors", _logger, filterDTO, cmd =>
            {
                EmployeeHelper.AddEmployeeFilterParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new DoctorRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("FullName"), converter.ConvertValue<string>("Specialization"))
            , parameters);
        }

        public async Task<GetByIDResponseDTO<DoctorDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<DoctorDTO>("Fetch_DoctorByID", _logger, id, null, (reader, converter) =>
            {
                EmployeeDTO employee = EmployeeHelper.ReadEmployee(reader);
                return new DoctorDTO(converter.ConvertValue<int>("ID"), employee, converter.ConvertValue<int>("CostPerAppointment"),
                    converter.ConvertValue<string>("Specialization"));
            });
        }

        public async Task<CreateResponseDTO> AddAsync(Doctor doctor)
        {
            var param = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Spcecialization"] = (doctor.Specialization, SqlDbType.NVarChar, 100, null),
                ["CostPerAppointment"] = (doctor.CostPerAppointment, SqlDbType.Int, null, null)
            };

            return await ReusableCRUD.AddAsync("Create_Doctor", _logger, (cmd) =>
            {
                EmployeeHelper.AddCreateEmployeeParameters(doctor.Employee, cmd);

                SqlParameterHelper.AddParametersFromDictionary(cmd, param);
            });
        }

        public async Task<SuccessResponseDTO> UpdateAsync(Doctor doctor)
        {
            var param = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>()
            {
                ["Spcecialization"] = (doctor.Specialization, SqlDbType.NVarChar, 100, null),
                ["CostPerAppointment"] = (doctor.CostPerAppointment, SqlDbType.Int, null, null)
            };

            return await ReusableCRUD.UpdateAsync("Update_Doctor", _logger, doctor.ID, (cmd) =>
            {
                EmployeeHelper.AddUpdateEmployeeParameters(doctor.Employee, cmd);
                SqlParameterHelper.AddParametersFromDictionary(cmd, param);
            });
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_Doctor", _logger, id);
        }

        public async Task<SuccessResponseDTO> LeaveAsync(int ID)
        {
            return await ReusableCRUD.ExecuteByIDAsync("Leave_Doctor", _logger, ID, null);
        }

        public static DoctorRowDTO ReadDoctor(SqlDataReader reader)
        {
            ConvertingHelper converter = new ConvertingHelper(reader);

            return new DoctorRowDTO(
                    converter.ConvertValue<int>("DoctorID"),
                    converter.ConvertValue<string>("DoctorFullName"),
                    converter.ConvertValue<string>("DoctorSpecialization")
                    );
        }
    }
}