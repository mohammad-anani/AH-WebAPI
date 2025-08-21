using AH.Domain.Entities;
using AH.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public async Task<IEnumerable<Payment>> GetAllByBillIDAsync(int billID)
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