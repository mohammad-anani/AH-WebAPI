using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using AH.Application.DTOs.Entities;

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
                EmployeeHelper.AddEmployeeFilterParameters(filterDTO, cmd);
            }, (reader, converter) =>

                new ReceptionistRowDTO(converter.ConvertValue<int>("ID"),
                                    converter.ConvertValue<string>("FullName"))
          , null);
        }

        public async Task<GetByIDResponseDTO<ReceptionistDTO>> GetByIDAsync(int id)
        {
            return await ReusableCRUD.GetByID<ReceptionistDTO>("Fetch_ReceptionistByID", _logger, id, null, (reader, converter) =>
            {
                EmployeeDTO employee = EmployeeHelper.ReadEmployee(reader);
                return new ReceptionistDTO(converter.ConvertValue<int>("ID"), employee);
            }
            );
        }

        public async Task<CreateResponseDTO> AddAsync(Receptionist receptionist)
        {
            return await ReusableCRUD.AddAsync("Create_Receptionist", _logger, (cmd) =>
            {
                EmployeeHelper.AddCreateEmployeeParameters(receptionist.Employee, cmd);
            });
        }

        public async Task<SuccessResponseDTO> UpdateAsync(Receptionist receptionist)
        {
            return await ReusableCRUD.UpdateAsync("Update_Receptionist", _logger, receptionist.ID, (cmd) =>
            {
                EmployeeHelper.AddUpdateEmployeeParameters(receptionist.Employee, cmd);
            });
        }

        public async Task<DeleteResponseDTO> DeleteAsync(int id)
        {
            return await ReusableCRUD.DeleteAsync("Delete_Receptionist", _logger, id);
        }

        public async Task<SuccessResponseDTO> LeaveAsync(int ID)
        {
            return await ReusableCRUD.ExecuteByIDAsync("Leave_Receptionist", _logger, ID, null);
        }
    }
}