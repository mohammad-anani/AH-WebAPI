using AH.Domain.Entities;
using AH.Application.DTOs.Row;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IPaymentRepository
    {
        Task<Tuple<IEnumerable<PaymentRowDTO>, int>> GetAllByBillIDAsync(int billID);
        Task<Payment> GetByIdAsync(int id);
        Task<int> AddAsync(Payment payment);
       
        Task<bool> DeleteAsync(int id);
    }
}