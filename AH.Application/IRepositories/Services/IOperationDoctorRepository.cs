using AH.Application.DTOs.Extra;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IOperationDoctorRepository
    {
        Task<ListResponseDTO<OperationDoctor>> GetAllByOperationIDAsync(int operationID);

        Task<int> AddUpdateAsync(OperationDoctor operationDoctor);
    }
}