using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.IServices
{
    public interface IPersonService
    {

        public Task<bool> EmailAlreadyExists(string email);
    }
}
