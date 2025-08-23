using AH.Application.DTOs.Row;
using AH.Application.IRepositories;
using AH.Domain.Entities;

namespace AH.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public async Task<(IEnumerable<PaymentRowDTO> Items, int Count)> GetAllByBillIDAsync(int billID)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(Payment payment)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Implementation placeholder
            throw new NotImplementedException();
        }
    }
}