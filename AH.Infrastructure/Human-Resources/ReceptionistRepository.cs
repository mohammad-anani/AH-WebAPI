using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;

namespace AH.Infrastructure.Repositories
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private readonly ILogger<ReceptionistRepository> _logger;

        public ReceptionistRepository(ILogger<ReceptionistRepository> logger)
        {
            _logger = logger;
        }

        public async Task<GetAllResponseDTO<ReceptionistRowDTO>> GetAllAsync(ReceptionistFilterDTO filterDTO)
        {
            return await ReusableCRUD.GetAllAsync<ReceptionistRowDTO, ReceptionistFilterDTO>("Fetch_Receptionists", _logger, filterDTO, cmd =>
            {
                EmployeeHelper.AddEmployeeParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new ReceptionistRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("FullName"))
          , null);
        }

        public async Task<GetByIDResponseDTO<ReceptionistDTODTO>> GetByIDAsync(int id)
        {
            Receptionist receptionist = new Receptionist();

            Exception? ex = await ReusableCRUD.GetByID<Admin>("Fetch_ReceptionistByID", _logger, id, null, (reader, converter) =>
            {
                Employee employee = EmployeeHelper.ReadEmployee(reader);
                receptionist = new Receptionist(converter.ConvertValue<int>("ID"), employee);
            }
            );

            return new GetByIDResponseDTO<Receptionist>(receptionist, ex);
        }

        public async Task<int> AddAsync(Receptionist receptionist)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Receptionist receptionist)
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