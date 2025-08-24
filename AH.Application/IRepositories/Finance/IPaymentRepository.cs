using AH.Application.DTOs.Extra;
using AH.Application.DTOs.Filter.Finance;
using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IPaymentRepository
    {
        Task<ListResponseDTO<PaymentRowDTO>> GetAllByBillIDAsync(PaymentFilterDTO filterDTO);

        Task<Payment> GetByIDAsync(int id);

        Task<int> AddAsync(Payment payment);

        Task<bool> DeleteAsync(int id);
    }
}