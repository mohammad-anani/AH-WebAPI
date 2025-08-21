using AH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllByBillIDAsync(int billID);
        Task<Payment> GetByIdAsync(int id);
        Task<int> AddAsync(Payment payment);
       
        Task<bool> DeleteAsync(int id);
    }
}