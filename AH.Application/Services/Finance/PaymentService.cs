using AH.Application.DTOs.Create;
using AH.Application.DTOs.Entities;
using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Application.IServices;

namespace AH.Application.Services
{
    /// <summary>
    /// Service implementation for Payment business operations.
    /// Acts as a business layer wrapper around the payment repository.
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        /// <summary>
        /// Initializes a new instance of the PaymentService.
        /// </summary>
        /// <param name="paymentRepository">The payment repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of payments for a specific bill based on filter criteria.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for payment search including bill ID</param>
        /// <returns>Response containing payment row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<GetAllResponseDataDTO<PaymentRowDTO>>> GetAllByBillIDAsync(PaymentFilterDTO filterDTO)
        {
            var response = await _paymentRepository.GetAllByBillIDAsync(filterDTO);
            var data = new GetAllResponseDataDTO<PaymentRowDTO>(response);return ServiceResult<GetAllResponseDataDTO<PaymentRowDTO>>.Create(data, response.Exception); ;
        }

        /// <summary>
        /// Retrieves a specific payment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the payment</param>
        /// <returns>Payment DTO with complete information or null if not found</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<PaymentDTO>> GetByIDAsync(int id)
        {
            var response = await _paymentRepository.GetByIDAsync(id);
            return ServiceResult<PaymentDTO>.Create(response.Item, response.Exception);
        }

        /// <summary>
        /// Creates a new payment in the system.
        /// </summary>
        /// <param name="createPaymentDTO">The payment create DTO containing creation information</param>
        /// <returns>The ID of the newly created payment</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<int>> AddAsync(CreatePaymentDTO createPaymentDTO)
        {
            var payment = createPaymentDTO.ToPayment();
            var response = await _paymentRepository.AddAsync(payment);
            return ServiceResult<int>.Create(response.ID, response.Exception);
        }

        /// <summary>
        /// Deletes a payment from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the payment to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var response = await _paymentRepository.DeleteAsync(id);
            return ServiceResult<bool>.Create(response.Success, response.Exception);
        }
    }
}