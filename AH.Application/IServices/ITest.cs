using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.IServices
{
    public interface ITest
    {

        public Task<(IEnumerable<AdminRowDTO> Items, int Count)> GetAllAsync(AdminFilterDTO filterDTO);

    }
}
