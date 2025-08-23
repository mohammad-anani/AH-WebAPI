using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IOperationDoctorRepository
    {
        Task<(IEnumerable<OperationDoctor> Items, int Count)> GetAllByOperationIDAsync(int operationID);

        Task<int> AddUpdateAsync(OperationDoctor operationDoctor);
    }
}