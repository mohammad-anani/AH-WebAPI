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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ILogger<DoctorRepository> _logger;

        public DoctorRepository(ILogger<DoctorRepository> logger)
        {
            _logger = logger;
        }

        public async Task<ListResponseDTO<DoctorRowDTO>> GetAllAsync(DoctorFilterDTO filterDTO)
        {
            var parameters = new Dictionary<string, (object? Value, SqlDbType Type, int? Size, ParameterDirection? Direction)>
            {
                ["Specialization"] = (filterDTO.Specialization, SqlDbType.NVarChar, 100, null),
                ["CostPerAppointmentFrom"] = (filterDTO.CostPerAppointmentFrom, SqlDbType.Int, null, null),
                ["CostPerAppointmentTo"] = (filterDTO.CostPerAppointmentTo, SqlDbType.Int, null, null)
            };

            return await ReusableCRUD.GetAllAsync<DoctorRowDTO, DoctorFilterDTO>("Fetch_Doctors", _logger, filterDTO, cmd =>
            {
                EmployeeHelper.AddEmployeeParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new DoctorRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("FullName"), converter.ConvertValue<string>("Specialization"))
            , parameters);
        }

        public async Task<Doctor> GetByIDAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Doctor doctor)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Doctor doctor)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> LeaveAsync(int employeeID)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}