namespace AH.Application.IRepositories
{
    public interface IEmployee
    {
        Task<bool> LeaveAsync(int employeeID);
    }
}