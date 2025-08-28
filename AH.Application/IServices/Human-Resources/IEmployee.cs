using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.IServices
{
    public interface IEmployee
    {
        public Task<bool> LeaveAsync(int id);
    }
}