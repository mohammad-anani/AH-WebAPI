using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IServices
{
    /// <summary>
    /// Service interface for Payment business operations.
    /// Provides a business layer abstraction over payment repository operations.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Retrieves a paginated list of payments for a specific bill based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for payment search including bill ID</param>
        /// <returns>Response containing payment row DTOs and count</returns>
        Task<ServiceResult<(IEnumerable<PaymentRowDTO> items, int count)>> GetAllByBillIDAsync(PaymentFilterDTO filterDTO);

        /// <summary>
        /// Retrieves a specific payment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the payment</param>
        /// <returns>Payment DTO with complete information or null if not found</returns>
        Task<PaymentDTO?> GetByIDAsync(int id);

        /// <summary>
        /// Creates a new payment in the system.
        /// </summary>
        /// <param name="createPaymentDTO">The payment create DTO containing creation information</param>
        /// <returns>The ID of the newly created payment</returns>
        Task<ServiceResult<int>> AddAsync(CreatePaymentDTO createPaymentDTO);

        /// <summary>
        /// Deletes a payment from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the payment to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}