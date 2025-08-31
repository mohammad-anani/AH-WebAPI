using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;

namespace AH.Application.IRepositories
{
    public interface IOperationRepository : IService
    {
        Task<GetAllResponseDTO<OperationRowDTO>> GetAllAsync(OperationFilterDTO filterDTO);

        Task<GetAllResponseDTO<OperationRowDTO>> GetAllByDoctorIDAsync(int doctorID, OperationFilterDTO filterDTO);

        Task<GetAllResponseDTO<OperationRowDTO>> GetAllByPatientIDAsync(OperationFilterDTO filterDTO);

        Task<GetByIDResponseDTO<OperationDTO>> GetByIDAsync(int id);

        Task<GetAllResponseDTO<PaymentRowDTO>> GetPaymentsAsync(ServicePaymentsDTO filterDTO);

        Task<CreateResponseDTO> AddAsync(AddUpdateOperationDTO operationDTO);

        Task<SuccessResponseDTO> UpdateAsync(AddUpdateOperationDTO operationDTO);

        Task<DeleteResponseDTO> DeleteAsync(int id);
    }
}