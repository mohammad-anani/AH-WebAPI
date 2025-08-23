namespace AH.Application.IRepositories
{
    public interface IService
    {
        Task<bool> StartAsync(int id, string? notes);

        Task<bool> CancelAsync(int id, string? notes);

        Task<bool> CompleteAsync(int id, string? notes, string result);

        Task<bool> RescheduleAsync(int id, string? notes, DateTime newScheduledDate);
    }
}