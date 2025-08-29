using AH.Application.DTOs.Create;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for OperationDoctor business operations.
    /// Provides a business layer abstraction over operation doctor repository operations.
    /// </summary>
    public interface IOperationDoctorService
    {
        /// <summary>
        /// Retrieves a paginated list of operation doctors for a specific operation.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for operation doctor search including operation ID</param>
        /// <returns>Response containing operation doctor row DTOs and count</returns>
        Task<ServiceResult<(IEnumerable<OperationDoctorRowDTO> items, int count)>> GetAllByOperationIDAsync(OperationDoctorFilterDTO filterDTO);
    }
}