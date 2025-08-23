using AH.Application.IRepositories;
using AH.Domain.Entities;

namespace AH.Infrastructure.Repositories
{
    public class OperationDoctorRepository : IOperationDoctorRepository
    {
        public async Task<(IEnumerable<OperationDoctor> Items, int Count)> GetAllByOperationIDAsync(int operationID)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddUpdateAsync(OperationDoctor operationDoctor)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}