using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;
using AH.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;

namespace AH.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ILogger<AdminRepository> _logger;

        public AdminRepository(ILogger<AdminRepository> logger)
        {
            _logger = logger;
        }

        public async Task<ListResponseDTO<AdminRowDTO>> GetAllAsync(AdminFilterDTO filterDTO)
        {
            int totalCount = -1;
            List<AdminRowDTO> admins = new List<AdminRowDTO>();

            RowCountOutputHelper rowCountOutputHelper = new RowCountOutputHelper();

            Exception? ex = await ADOHelper.ExecuteReaderAsync(
                 "Fetch_Admins", _logger, cmd =>
                 {
                     EmployeeHelper.AddEmployeeParameters(filterDTO)(cmd);

                     FilterableHelper.AddFilterParameters(filterDTO.Sort, filterDTO.Order, filterDTO.Page, cmd);

                     rowCountOutputHelper.AddToCommand(cmd);
                 }, (reader, cmd) =>
                 {
                     var converter = new ConvertingHelper(reader);
                     admins.Add(new AdminRowDTO(converter.ConvertValue<int>("ID"),
                        converter.ConvertValue<string>("FullName")

                         ));
                 });

            totalCount = rowCountOutputHelper.GetRowCount();

           
            return new ListResponseDTO<AdminRowDTO>(admins,totalCount,ex);
        }

        public async Task<Admin> GetByIdAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Admin admin)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Admin admin)
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