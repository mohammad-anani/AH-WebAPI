using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Application.IServices;

namespace AH.Application.Services
{
    public class Test : ITest
    {
        private IAdminRepository _adminRepository;

        public Test(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<ListResponseDTO<AdminRowDTO>> GetAllAsync(AdminFilterDTO filterDTO)
        {
            return await _adminRepository.GetAllAsync(filterDTO);
        }
    }
}