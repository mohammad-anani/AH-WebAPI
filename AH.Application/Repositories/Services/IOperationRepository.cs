using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IOperationRepository:IService
    {
        Task<IEnumerable<Operation>> GetAllAsync();

        Task<IEnumerable<Appointment>> GetAllByDoctorIDAsync(int doctorID);

        Task<IEnumerable<Appointment>> GetAllByPatientIDAsync(int patientID);

        Task<Operation> GetByIdAsync(int id);
        Task<int> AddAsync(Operation operation);
        Task<bool> UpdateAsync(Operation operation);
        Task<bool> DeleteAsync(int id);
    }
}