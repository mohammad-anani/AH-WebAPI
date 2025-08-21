using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IOperationDoctorRepository
    {
        Task<IEnumerable<OperationDoctor>> GetAllByOperationIDAsync(int operationID);
        Task<int> AddUpdateAsync(OperationDoctor operationDoctor);
       
    }
}