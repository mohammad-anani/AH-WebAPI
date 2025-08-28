using AH.Application.DTOs.Filter;
using AH.Application.DTOs.Response;
using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Application.IServices;

namespace AH.Application.Services
{
    /// <summary>
    /// Service implementation for OperationDoctor business operations.
    /// Acts as a business layer wrapper around the operation doctor repository.
    /// </summary>
    public class OperationDoctorService : IOperationDoctorService
    {
        private readonly IOperationDoctorRepository _operationDoctorRepository;

        /// <summary>
        /// Initializes a new instance of the OperationDoctorService.
        /// </summary>
        /// <param name="operationDoctorRepository">The operation doctor repository instance</param>
        /// <exception cref="ArgumentNullException">Thrown when repository is null</exception>
        public OperationDoctorService(IOperationDoctorRepository operationDoctorRepository)
        {
            _operationDoctorRepository = operationDoctorRepository ?? throw new ArgumentNullException(nameof(operationDoctorRepository));
        }

        /// <summary>
        /// Retrieves a paginated list of operation doctors for a specific operation.
        /// </summary>
        /// <param name="filterDTO">Filter criteria for operation doctor search including operation ID</param>
        /// <returns>Response containing operation doctor row DTOs and count</returns>
        /// <exception cref="InvalidOperationException">Thrown when repository operation fails</exception>
        public async Task<GetAllResponseDataDTO<OperationDoctorRowDTO>> GetAllByOperationIDAsync(OperationDoctorFilterDTO filterDTO)
        {
            var response = await _operationDoctorRepository.GetAllByOperationIDAsync(filterDTO);

            if (response.Exception != null)
            {
                throw new InvalidOperationException("Failed to retrieve operation doctors by operation ID.", response.Exception);
            }

            return new GetAllResponseDataDTO<OperationDoctorRowDTO>(response);
        }
    }
}