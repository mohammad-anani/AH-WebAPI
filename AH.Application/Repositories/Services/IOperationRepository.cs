using AH.Domain.Entities;
using AH.Application.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Application.DTOs.Row;

namespace AH.Application.Repositories
{
    public interface IOperationRepository:IService
    {
        Task<Tuple<IEnumerable<OperationRowDTO>, int>> GetAllAsync(OperationFilterDTO filterDTO);

        Task<Tuple<IEnumerable<OperationRowDTO>, int>> GetAllByDoctorIDAsync(int doctorID);

        Task<Tuple<IEnumerable<OperationRowDTO>, int>> GetAllByPatientIDAsync(int patientID);

        Task<Operation> GetByIdAsync(int id);
        Task<int> AddAsync(Operation operation);
        Task<bool> UpdateAsync(Operation operation);
        Task<bool> DeleteAsync(int id);
    }
}