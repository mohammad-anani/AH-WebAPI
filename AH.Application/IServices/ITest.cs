using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;

namespace AH.Application.IServices
{
    public interface ITest
    {
        public Task<ListResponseDTO<AdminRowDTO>> GetAllAsync(AdminFilterDTO filterDTO);
    }
}