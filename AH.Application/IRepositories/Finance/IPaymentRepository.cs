using AH.Application.DTOs.Row;
using AH.Domain.Entities;

namespace AH.Application.IRepositories
{
    public interface IPaymentRepository
    {
        Task<(IEnumerable<PaymentRowDTO> Items, int Count)> GetAllByBillIDAsync(int billID);

        Task<Payment> GetByIdAsync(int id);

        Task<int> AddAsync(Payment payment);

        Task<bool> DeleteAsync(int id);
    }
}