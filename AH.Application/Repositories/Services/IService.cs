using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.Repositories
{
    public interface IService
    {
        Task<bool> StartAsync(int id,string? notes);

        Task<bool> CancelAsync(int id,string? notes);

        Task<bool>  CompleteAsync(int id,string? notes,string result);

        Task<bool> RescheduleAsync(int id,string? notes,DateTime newScheduledDate);



    }
}
