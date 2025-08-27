using AH.Application.DTOs.Response;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;
using AH.Application.DTOs.Entities;

namespace AH.Application.IRepositories
{
    public interface IOperationRepository : IService
    {
        Task<GetAllResponseDTO<OperationRowDTO>> GetAllAsync(OperationFilterDTO filterDTO);

        Task<GetAllResponseDTO<OperationRowDTO>> GetAllByDoctorIDAsync(int doctorID);

        Task<GetAllResponseDTO<OperationRowDTO>> GetAllByPatientIDAsync(int patientID);

        Task<GetByIDResponseDTO<OperationDTO>> GetByIDAsync(int id);

        Task<CreateResponseDTO> AddAsync(Operation operation);

        Task<SuccessResponseDTO> UpdateAsync(Operation operation);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}