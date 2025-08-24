using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IOperationDoctorRepository
    {
        Task<ListResponseDTO<OperationDoctorRowDTO>> GetAllByOperationIDAsync(OperationDoctorFilterDTO filterDTO);

        Task<int> AddUpdateAsync(OperationDoctor operationDoctor);
    }
}