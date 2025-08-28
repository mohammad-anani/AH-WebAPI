using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IOperationDoctorRepository
    {
        Task<GetAllResponseDTO<OperationDoctorRowDTO>> GetAllByOperationIDAsync(OperationDoctorFilterDTO filterDTO);
    }
}