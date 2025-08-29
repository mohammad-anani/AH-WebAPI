using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;

namespace AH.Application.IRepositories
{
    public interface IOperationDoctorRepository
    {
        Task<GetAllResponseDTO<OperationDoctorRowDTO>> GetAllByOperationIDAsync(OperationDoctorFilterDTO filterDTO);
    }
}