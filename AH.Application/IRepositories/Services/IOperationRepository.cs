using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IOperationRepository : IService
    {
        Task<GetAllResponseDTO<OperationRowDTO>> GetAllAsync(OperationFilterDTO filterDTO);

        Task<GetAllResponseDTO<OperationRowDTO>> GetAllByDoctorIDAsync(int doctorID);

        Task<GetAllResponseDTO<OperationRowDTO>> GetAllByPatientIDAsync(int patientID);

        Task<GetByIDResponseDTO<Operation>> GetByIDAsync(int id);

        Task<int> AddAsync(Operation operation);

        Task<bool> UpdateAsync(Operation operation);

        Task<bool> DeleteAsync(int id);
    }
}