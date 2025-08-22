using AH.Domain.Entities;
using AH.Application.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IOperationRepository:IService
    {
        Task<Tuple<IEnumerable<Operation>, int>> GetAllAsync(OperationFilterDTO filterDTO);

        Task<IEnumerable<Appointment>> GetAllByDoctorIDAsync(int doctorID);

        Task<IEnumerable<Appointment>> GetAllByPatientIDAsync(int patientID);

        Task<Operation> GetByIdAsync(int id);
        Task<int> AddAsync(Operation operation);
        Task<bool> UpdateAsync(Operation operation);
        Task<bool> DeleteAsync(int id);
    }
}