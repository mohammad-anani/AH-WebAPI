using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Services
{
    public class Test:ITest
    {

        IAdminRepository _adminRepository;
        public Test(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<(IEnumerable<AdminRowDTO> Items, int Count)> GetAllAsync(AdminFilterDTO filterDTO)
        {

            return await _adminRepository.GetAllAsync(filterDTO);
        }
    }
}
