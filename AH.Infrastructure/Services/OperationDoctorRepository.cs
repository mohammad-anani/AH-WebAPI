using AH.Domain.Entities;
using AH.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Infrastructure.Repositories
{
    public class OperationDoctorRepository : IOperationDoctorRepository
    {
        public async Task<Tuple<IEnumerable<OperationDoctor>, int>> GetAllByOperationIDAsync(int operationID)
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