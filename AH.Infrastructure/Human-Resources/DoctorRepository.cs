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
                EmployeeHelper.AddEmployeeParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new DoctorRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("FullName"), converter.ConvertValue<string>("Specialization"))
            , parameters);
        }

        public async Task<GetByIDResponseDTO<Doctor>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<Doctor>("Fetch_DoctorByID", _logger, id, null, (reader, converter) =>
            {
                Employee employee = EmployeeHelper.ReadEmployee(reader);
                return new Doctor(converter.ConvertValue<int>("ID"), employee, converter.ConvertValue<int>("CostPerAppointment"),
                    converter.ConvertValue<string>("Specialization"));
            });
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

        public static Doctor ReadDoctor(SqlDataReader reader)
        {
            ConvertingHelper converter = new ConvertingHelper(reader);

            return new Doctor()
            {
                ID = converter.ConvertValue<int>("DoctorID"),
                Employee =
                {
                    Person=
                new Person
                {
                    FirstName = converter.ConvertValue<string>("DoctorFirstName"),
                    MiddleName = converter.ConvertValue<string>("DoctorMiddleName"),
                    LastName = converter.ConvertValue<string>("DoctorLastName"),
                }
                }
            };
        }
    }
}