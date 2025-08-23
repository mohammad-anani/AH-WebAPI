using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IOperationRepository : IService
    {
        Task<(IEnumerable<OperationRowDTO> Items, int Count)> GetAllAsync(OperationFilterDTO filterDTO);

        Task<(IEnumerable<OperationRowDTO> Items, int Count)> GetAllByDoctorIDAsync(int doctorID);

        Task<(IEnumerable<OperationRowDTO> Items, int Count)> GetAllByPatientIDAsync(int patientID);

        Task<Operation> GetByIdAsync(int id);

        Task<int> AddAsync(Operation operation);

        Task<bool> UpdateAsync(Operation operation);

        Task<bool> DeleteAsync(int id);
    }
}