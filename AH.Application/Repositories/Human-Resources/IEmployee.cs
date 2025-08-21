using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IEmployee
    {
        Task<bool> LeaveAsync(int employeeID);
    }
}
